﻿#region "Using Statements"
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
	public class GalaxyMap : GUIPage
	{
		//GUI controls
		public dImage Image_Galaxy;

		public dLabel Label_GalaxyName;

		public dButton Button_Back;


		public Vector2 MapPosition = Vector2.Zero;
		public Vector2 PrevMapPosition;
		public Vector2 ZoomOffset = Vector2.Zero;

		public Matrix Transform;

		public Vector2 OriginalMousePos;
		public Vector2 MouseOffset;

		public List<Dot> Dots = new List<Dot>();

		public GalaxyMap() : base()
		{
			Engine.Pages.Add(this);

			//Main Form
			Form_Main = new dForm("Galaxy_Map", new Rectangle(0, 0, (int)Engine.CurrentGameResolution.X, (int)Engine.CurrentGameResolution.Y), Engine.MapBackground, null, false, false);
			Form_Main.OriginalColor = Color.Black;
			//Form_Main.IsDragable = true;

			//Galaxy Image
			Image_Galaxy = new dImage("Map_Galaxy", new Vector2(640, 360), Engine.Galaxy_Spiral1, Form_Main, true, true);
				Form_Main.AddControl(Image_Galaxy);
				MapPosition = Engine.CurrentGameResolution / 2;

			//Galaxy Name
			//spriteBatch.DrawString(Engine.Font_Large, Galaxy.Name, new Vector2(-Engine.Font_Small.MeasureString(Galaxy.Name).X * 1.5f, -550), Color.White);
				Label_GalaxyName = new dLabel("Map_GalaxyName", new Vector2(Image_Galaxy.OriginalTexture.Width / 2 - Engine.Font_Large.MeasureString(Galaxy.Name).X / 2, 0), null, Form_Main, Engine.Font_Large, Galaxy.Name, Color.White, false, false, false);
				Image_Galaxy.AddControl(Label_GalaxyName);

			//Back Button
				Button_Back = new dButton("Map_Back", new Vector2(50, 50), Engine.ButtonsTexture, new Rectangle(2, 221, 116, 41), Form_Main, false, false);
				Button_Back.HoverColor = Color.Purple;
				Button_Back.ReleaseColor = Color.Red;
				Button_Back.PlaySound = true;
				Form_Main.AddControl(Button_Back);
				Button_Back.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Back.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Back.OnMouseRelease += new Engine.Handler(ButtonRelease);
				//Button_Back.EmulationKey = Input.Pause;

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
				case "Map_Back":
					Hide(false);
					break;
			}
		}


		public void AddDot(string name, Vector2 position)
		{
			Dot dot = new Dot(name, position);
			Dots.Add(dot);
		}
		public void DeleteDot(string name)
		{
			for (int ii = 0; ii < Dots.Count; ii++)
			{
				if (Dots[ii].Name == name)
				{
					Dots.Remove(Dots[ii]);
				}
			}
		}
		public void DeleteAllDots()
		{
			//for (int ii = 0; ii < Dots.Count; ii++)
			//{
				//Dots[ii].Close();
			//}
			Dots.Clear();
		}


		public override void Show(GUIPage lastform, bool quick)
		{
			base.Show(lastform, quick);

			Camera.PrevGameZoom = Camera.ZoomIndex;

			Camera.ZoomMax = Camera.ZoomValues.IndexOf(0.6d);
			Camera.ZoomMin = Camera.ZoomValues.IndexOf(16d);
			Camera.SetZoom(Camera.PrevMapZoom);
		}

		public override void Hide(bool quick)
		{
			base.Hide(quick);

			Camera.PrevMapZoom = Camera.ZoomIndex;
			Camera.SetZoom(Camera.PrevGameZoom);
			Camera.ResetMouse();
		}


		public override void Reset()
		{
			base.Reset();

			//Camera.PrevGamePosition = Vector2.Zero;
			//Camera.Position = Vector2.Zero;
		}

		public override void Update()
		{

			//Update base
			base.Update();

			if (Visible)
			{
				//Click and drag
				if (Input.MouseLeftPressed)
				{
					OriginalMousePos = Input.MousePosition;
					PrevMapPosition = MapPosition;
				}


				if (Input.MouseState(Input.LocalMouseState.LeftButton, ButtonState.Pressed))
				{
					MouseOffset = Input.MousePosition - OriginalMousePos;
					MapPosition = PrevMapPosition + MouseOffset;
				}



				//Update Galaxy Image location
				Image_Galaxy.scale = new Vector2d(Camera.Zoom, Camera.Zoom);
				ZoomOffset = (MapPosition - (Engine.CurrentGameResolution / 2f));// *Image_Galaxy.scale;
				Image_Galaxy.position = Form_Main.position + MapPosition - (Image_Galaxy.size * Image_Galaxy._scale)/2;

				Label_GalaxyName.scale = Image_Galaxy.scale;
				Label_GalaxyName.position = Image_Galaxy.position + new Vector2((Image_Galaxy.size.X / 2 - Label_GalaxyName.Font.MeasureString(Label_GalaxyName.Text).X / 2) * Image_Galaxy.scale.X, -25);


				//Update dots
				for (int ii = 0; ii < Dots.Count; ii++)
				{
					Dots[ii].Update();
					Dots[ii]._Scale = Image_Galaxy.scale * Dots[ii].Scale;
					Dots[ii]._Position = Image_Galaxy.position + ((Dots[ii].Position + Image_Galaxy.size / 2) * Image_Galaxy.scale);
				}
			}
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);


			if (Visible)
			{
				//Draw dots
				for (int ii = 0; ii < Dots.Count; ii++)
				{
					Dots[ii].Draw(spriteBatch);
				}



				if (Engine.DebugState)
				{
					//Galaxy Center
					//spriteBatch.Draw(GUI.Circle, new Vector2(-Galaxy.Galaxy_Spiral1.Width / 2, -Galaxy.Galaxy_Spiral1.Height / 2), Color.Green);

					//Debug info
					string Str =
						"Name:" + Galaxy.Name + "\n" +
						"TotalMass:" + Galaxy.TotalMass + "\n" +
						"   " + "\n" +
						"ZoomOffset:(" + Math.Round(ZoomOffset.X) + "," + Math.Round(ZoomOffset.Y) + ")" + "\n" +
						"MapPosition:(" + Math.Round(MapPosition.X) + "," + Math.Round(MapPosition.Y) + ")" + "\n" +
						"PreviousMapPosition:(" + Math.Round(PrevMapPosition.X) + "," + Math.Round(PrevMapPosition.Y) + ")" + "\n" +
						"   " + "\n" +
						"MouseClickedPosition:(" + Math.Round(OriginalMousePos.X) + "," + Math.Round(OriginalMousePos.Y) + ")" + "\n" +
						"MouseOffset:(" + Math.Round(MouseOffset.X) + "," + Math.Round(MouseOffset.Y) + ")";


					spriteBatch.DrawString(Engine.Font_Small, Str, Image_Galaxy.position, Color.Red);
				}

			}
			
		}
	}

	public class Dot
	{
		public string Name = "";

		public Vector2 Scale = Vector2.One;
		public Vector2 _Scale = Vector2.One;

		public SolarSystem solarSystem = null;

		public Rectangle Rectangle = Rectangle.Empty;
		public Rectangle Source = Rectangle.Empty;


		public Vector2 Position;
		public Vector2 _Position;

		public bool IsHover = false;

		public Color color = Color.White;

		public Dot(string name, Vector2 position)
		{
			Name = name;
			Position = position;
			Source = new Rectangle(0, 0, Engine.Circle.Width, Engine.Circle.Height);
		}


		public void Update()
		{
			Rectangle = new Rectangle((int)(_Position.X - (Engine.Circle.Width / 2 * _Scale.X)), (int)(_Position.Y - (Engine.Circle.Height/2 * _Scale.Y)), (int)(Engine.Circle.Width * _Scale.X), (int)(Engine.Circle.Height * _Scale.Y));

			IsHover = new Rectangle(Input.LocalMouseState.X, Input.LocalMouseState.Y, 1, 1).Intersects(Rectangle);

			if (Galaxy.CurrentSolarSystem != null && Galaxy.CurrentSolarSystem.Name != Name)
			{
				Scale = (IsHover ? new Vector2(0.5f, 0.5f) : new Vector2(0.25f, 0.25f));
				color = (IsHover ? Color.Blue : Color.Indigo);
			}
			else
			{
				Scale = new Vector2(0.25f, 0.25f);
				color = Color.Red;
			}

			_Position = Engine.GalaxyMap.Image_Galaxy.position + Position;
			_Scale = Scale;

			//Clicking on the dot
			if (IsHover && Name != Galaxy.CurrentSolarSystem.Name && Input.MouseLeftReleased)
			{
				Engine.GalaxyMap.Reset();

				Engine.GalaxyMap.Hide(true);
				Engine.Navigation.Hide(true);
				Engine.Sensors.Hide(true);
				Engine.LocalMap.Hide(true);
				

				Camera.ResetMouse();

				//Solar System Stuff
				SolarSystem NewSystem = Galaxy.SolarSystemLookup(Name);

				//Dot angle stuff
				Dot CurrentDot = null;
				for (int ii = 0; ii < Engine.GalaxyMap.Dots.Count; ii++)
				{
					if (Engine.GalaxyMap.Dots[ii].Name == Galaxy.CurrentSolarSystem.Name)
					{
						CurrentDot = Engine.GalaxyMap.Dots[ii];
					}
				}

				//Transport ship
				if (Camera.TargetIsShip())
				{
					//System.Windows.Forms.MessageBox.Show("Current:" + Camera.TargetObject.ShipLogic.ParentSolarSystem + " Target:" + Name);
					Camera.TargetObject.SolarSystem.Name = NewSystem.Name; //Doing the lookup just to make sure the solar system exists
					
					//Remove ship from current system
					Galaxy.CurrentSolarSystem.RemoveEntity(Camera.TargetObject);

					//Add ship to new system
					NewSystem.AddEntity(Camera.TargetObject);
					NewSystem.CameraTargetObject = Camera.TargetObject.Name;

					//Set position and velocity back to 0
					Camera.TargetObject.Angle = Math.Atan2(CurrentDot.Position.Y - Position.Y, CurrentDot.Position.X - Position.X) - MathHelper.PiOver2;
					Camera.TargetObject.Position = new Vector2d((CurrentDot.Position - Position).X,(CurrentDot.Position - Position).Y) * (Position - CurrentDot.Position).Length() * 1000d;
					Camera.TargetObject.Velocity = Vector2.Zero;

					//Set orbit target to that of the sun
					if (Camera.TargetCanOrbit())
					{
						BaseEntity Sun = NewSystem.Entities[0];

						Camera.TargetObject.Orbit._parent = Sun;
						Camera.TargetObject.Orbit.OrbitRadius = (Sun.Renderer != null ? new Vector2d(Sun.Renderer._texture.Width / 2 * Sun.Scale, Sun.Renderer._texture.Height / 2 * Sun.Scale) : Vector2d.Zero);
					}
				}

				Galaxy.SetSolarSystem(NewSystem);
				//ShipManager.ActiveShip.ParentSolarSystem = Galaxy.CurrentSolarSystem.Name;
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Engine.Circle, _Position, Source, color, 0f, new Vector2(Engine.Circle.Width, Engine.Circle.Height) / 2, _Scale, SpriteEffects.None, 0);
			spriteBatch.DrawString(Engine.Font_Large, Name, _Position + ((new Vector2(Source.Width / 2 - (Engine.Font_Large.MeasureString(Name).X / 2 * 0.25f), -20) - new Vector2(Engine.Circle.Width, Engine.Circle.Height)/2) * _Scale), color, 0f, Vector2.Zero, _Scale * 0.25f, SpriteEffects.None, 0);

		}
	}
}
