#region "Using Statements"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace ExplorationEngine.GUI
{
	public class LocalMap : GUIPage
	{
		//GUI controls

		public dLabel Title;

		public dGroupbox Groupbox;

		public dLabel Button_Close;

		public List<dImage> Objects = new List<dImage>();

		private SolarSystem PrevSolarsystem;
		private int PrevSolarsystemEntities;

		public Vector2 MapPosition = Vector2.Zero;
		public Vector2 PrevMapPosition;

		public Vector2 OriginalMousePos;
		public Vector2 MouseOffset;

		public float DefaultZoom = 400;
		public float Zoom = 400;


		public LocalMap() : base()
		{
			Engine.Pages.Add(this);

			//Main form
				Form_Main = new dForm("LocalMap", new Rectangle(0, 0, 0, 0), Engine.CreateTexture(950, 500, 949, 499, new Color(98, 238, 255), new Color(0, 0, 0, 255)), null, false, false);
				Form_Main.IsDragable = false;

			//Title
				Title = new dLabel("LocalMap_Title", Vector2.Zero, null, Form_Main, Engine.Font_Large, "Local System", Color.White, false, false, false);
				Title.offset = Form_Main.position + new Vector2(Form_Main.size.X / 2, -Title.Font.MeasureString(Title.Text).Y);
				Form_Main.AddControl(Title);

			//Groupbox for all elements
				Groupbox = new dGroupbox("LocalMap_Groupbox", null, Vector2.Zero, null, null, Form_Main, false, false);
				Form_Main.AddControl(Groupbox);

			//Close button
				Texture2D CloseTexture = Engine.CreateTexture(24, 24, 23, 23, Color.Gray, Color.White);
				Button_Close = new dLabel("LocalMap_Close", new Vector2(Form_Main.size.X - CloseTexture.Width, 0), CloseTexture, Groupbox, null, "", Color.White, false, false, false);
				Button_Close.OriginalColor = new Color(200, 60, 06);
				Button_Close.Color = Button_Close.OriginalColor;
				Button_Close.HoverColor = Color.Red;
				Button_Close.PressColor = Color.DarkRed;
				Button_Close.ReleaseColor = Button_Close.OriginalColor;
				Button_Close.PlaySound = true;
				Button_Close.EnterSound = null;
				Groupbox.AddControl(Button_Close);
				Button_Close.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Close.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Close.OnMousePress += new Engine.Handler(ButtonPress);
				Button_Close.OnMouseRelease += new Engine.Handler(ButtonRelease);

			//Lastly, move the form to the center of the screen
			Form_Main.position = Engine.CurrentScreenResolution / 2 - Form_Main.size / 2;
		}


		//Buttons
		public void ButtonEnter(dControl sender)
		{
			sender.Color = sender.HoverColor;
		}
		public void ButtonLeave(dControl sender)
		{
			sender.Color = sender.OriginalColor;
		}
		public void ButtonPress(dControl sender)
		{
			sender.Color = sender.PressColor;
		}
		public void ButtonRelease(dControl sender)
		{
			sender.Color = sender.ReleaseColor;

			switch (sender.name)
			{
				case "LocalMap_Close":
					Hide(false);
					break;
			}
		}


		public override void Show(GUIPage lastform, bool quick)
		{
			base.Show(lastform, quick);

		}

		public override void Hide(bool quick)
		{
			base.Hide(quick);

		}


		public override void Reset()
		{
			base.Reset();

		}

		public override void Update()
		{

			//Click and drag
			if (Input.MouseLeftPressed && Form_Main.ContainsMouse)
			{
				OriginalMousePos = Input.MousePosition;
				PrevMapPosition = MapPosition;
			}

			if (Input.MouseState(Input.LocalMouseState.LeftButton, ButtonState.Pressed) && Form_Main.ContainsMouse)
			{
				MouseOffset = Input.MousePosition - OriginalMousePos;
				MapPosition = PrevMapPosition + MouseOffset;
			}

			//Zooming
			if (Form_Main.ContainsMouse)
			{
				Zoom += (Input.ScrollValueChange != 0 ? 100 * Math.Sign(Input.ScrollValueChange) : 0);
				Zoom = Math.Min(Math.Max(DefaultZoom, Zoom), DefaultZoom * 10);
			}

			//Update solar bodies list when the entities change
			if (Galaxy.CurrentSolarSystem != null && (Galaxy.CurrentSolarSystem != PrevSolarsystem || Galaxy.CurrentSolarSystem.Entities.Count != PrevSolarsystemEntities))
			{
				//Delete all bodies that are part of local map
				for (int ii = 0; ii < Groupbox.children.Count; ii++)
				{
					if (Objects.Contains(Groupbox.children[ii]))
					{
						Groupbox.childrenToDelete.Add(Groupbox.children[ii]);
					}
				}

				//Clear our list of bodies
				Objects.Clear();
				MapPosition = Vector2.Zero;
				Zoom = DefaultZoom;

				//Sort the current solar system's entities by orbit radius
				List<BaseEntity> CS_Entities = new List<BaseEntity>();
				foreach (BaseEntity ent in Galaxy.CurrentSolarSystem.Entities)
				{
					if (ent.Orbit != null)
					{
						CS_Entities.Add(ent);
					}
				}

				if (CS_Entities.Count > 0)
				{
					List<BaseEntity> SortedList = CS_Entities.OrderBy(o => o.Orbit.OrbitRadius.Length()).ToList();

					BaseEntity Biggest = CS_Entities.OrderBy(o => o.Scale).ToList()[0];

					for (int ii = 0; ii < SortedList.Count; ii++)
					{
						dImage img = new dImage(SortedList[ii].Name, Vector2.Zero, (SortedList[ii].Renderer != null ? SortedList[ii].Renderer._texture : null), Groupbox, false, false);
						img.BlocksParentInteraction = false;
						if (ii <= 0)
						{
							img.size = new Vector2(Zoom, Zoom);
							img.offset = new Vector2(0, Form_Main.size.Y - img.size.Y);
						}
						else
						{
							img.size = new Vector2(Zoom, Zoom) * (SortedList[ii].Scale / SortedList[0].Scale);
							img.offset = new Vector2(Objects[ii - 1].offset.X + Objects[ii - 1].size.X, Form_Main.size.Y - img.size.Y);
						}

						Groupbox.AddControl(img);
						img.SetActive(true, true, false);
						Objects.Add(img);
					}
				}
			}

			//Update base
			base.Update();

			//Update positions of solar objects
			for (int ii = 0; ii < Objects.Count; ii++)
			{
				if (Form_Main.size.X > 0 && Form_Main.size.Y > 0)
				{
					Rectangle intersect = Rectangle.Intersect(Objects[ii].positionSize, Form_Main.positionSize);

					if (intersect != Rectangle.Empty && Objects[ii] != null && Objects[ii].OriginalTexture != null && intersect.Width > 0 && intersect.Height > 0)
					{
						float IX = intersect.X;
						float IY = intersect.Y;
						float IW = intersect.Width;
						float IH = intersect.Height;

						float OPX = Objects[ii].position.X;
						float OPY = Objects[ii].position.Y;
						float OSW = Objects[ii].size.X;
						float OSH = Objects[ii].size.Y;

						int x = (int)(Objects[ii].OriginalTexture.Width * (1 - (IW / OSW))) * (IX > OPX ? 1 : 0);
						int y = (int)(Objects[ii].OriginalTexture.Height * (1 - (IH / OSH))) * (IY > OPY ? 1 : 0);
						int width = (int)(Objects[ii].OriginalTexture.Width * (IX <= OPX ? (IW / OSW) : 1));
						int height = (int)(Objects[ii].OriginalTexture.Height * (IY <= OPY ? (IH / OSH) : 1));


						Objects[ii].source = new Rectangle(x, y, width, height);
						Objects[ii].position = new Vector2(IX, IY);
						Objects[ii]._size = new Vector2((IX <= OPX ? IW : Objects[ii].size.X), (IY <= OPY ? IH : Objects[ii].size.Y)) * (Zoom / DefaultZoom);
					}
				}
				else
				{
					Objects[ii].source = new Rectangle(0, 0, 0, 0);
				}

				//Update whether they are drawn (they are not if they are outside of listbox's bounds)
				if (Objects[ii].offset.X + Objects[ii].size.X * (Zoom / DefaultZoom) <= 0 || Objects[ii].offset.X > Form_Main.size.X)
				{
					Objects[ii].SetActive(false, true, true);
				}
				else
				{
					Objects[ii].SetActive(true, true, true);
				}


				//Objects[ii]._size = Objects[ii].size * (Zoom / DefaultZoom); 

				if (ii <= 0)
				{
					Objects[ii].offset = new Vector2(MapPosition.X, Form_Main.size.Y - (Objects[ii].size.Y * (Zoom / DefaultZoom)));
				}
				else
				{
					Objects[ii].offset = new Vector2(Objects[ii - 1].offset.X + Objects[ii - 1].size.X * (Zoom / DefaultZoom), Form_Main.size.Y - (Objects[ii].size.Y * (Zoom / DefaultZoom)));
				}

			}


			//Set previous variables
			PrevSolarsystem = Galaxy.CurrentSolarSystem;
			PrevSolarsystemEntities = (Galaxy.CurrentSolarSystem != null ? Galaxy.CurrentSolarSystem.Entities.Count : 0);
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			if (Visible)
			{
				//Debug info
				if (Engine.DebugState)
				{
					string Str =
						"MapPosition:(" + Math.Round(MapPosition.X) + "," + Math.Round(MapPosition.Y) + ")" + "\n" +
						"PreviousMapPosition:(" + Math.Round(PrevMapPosition.X) + "," + Math.Round(PrevMapPosition.Y) + ")" + "\n" +
						"   " + "\n" +
						"MouseClickedPosition:(" + Math.Round(OriginalMousePos.X) + "," + Math.Round(OriginalMousePos.Y) + ")" + "\n" +
						"MouseOffset:(" + Math.Round(MouseOffset.X) + "," + Math.Round(MouseOffset.Y) + ")";


					spriteBatch.DrawString(Engine.Font_Small, Str, Form_Main.position, Color.Red);
				}


				//Zoom Level Percent
				string ZoomStr = (Zoom / (DefaultZoom / 100)).ToString() + "%";
				spriteBatch.DrawString(Engine.Font_MediumSmall, ZoomStr, Form_Main.position, Color.White);
			}
		}
	}

}
