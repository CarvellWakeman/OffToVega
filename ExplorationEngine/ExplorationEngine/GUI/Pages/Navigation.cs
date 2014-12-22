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
	public class Navigation : GUIPage
	{
		public dGroupbox Groupbox;

		public dLabel Title;

		public dLabel Button_Close;
		public dLabel Button_SetCourse;
		public dLabel Button_DeOrbit;

		public dLabel Label_NoShip;
		public dLabel Label_InOrbit;
		public dLabel Label_EnRoute;

		public dListbox Listbox_Warnings;
		public dListbox Listbox_SolarBodies;


		private SolarSystem PrevSolarsystem;
		private int PrevSolarsystemEntities;


		public Navigation()
			: base()
		{
			Engine.Pages.Add(this);

			//Main form
				Form_Main = new dForm("Navigation", new Rectangle(0, 0, 0, 0), Engine.CreateTexture(950, 500, 949, 499, new Color(0, 24, 255), new Color(0,0,0,200)), null, false, false);
				Form_Main.IsDragable = true;

			//Title
				Title = new dLabel("Navigation_Title", Vector2.Zero, null, Form_Main, Engine.Font_Large, "Navigation", Color.White, false, false, false);
				Title.offset = Form_Main.position + new Vector2(Form_Main.size.X/2, -Title.Font.MeasureString(Title.Text).Y);
				Form_Main.AddControl(Title);

			//Groupbox for all elements
				Groupbox = new dGroupbox("Navigation_Groupbox", null, Vector2.Zero, null, null, Form_Main, false, false);
				Form_Main.AddControl(Groupbox);

			//Close button
				Texture2D CloseTexture = Engine.CreateTexture(24, 24, 23, 23, Color.Gray, Color.White);
				Button_Close = new dLabel("Navigation_Close", new Vector2(Form_Main.size.X - CloseTexture.Width, 0), CloseTexture, Groupbox, null, "", Color.White, false, false, false);
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

			//Entities Listbox
				Texture2D TBNT = Engine.CreateTexture(200, (int)Form_Main.size.Y - 25, 199, (int)Form_Main.size.Y - 26, new Color(0, 24, 255), Color.Black);
				Listbox_SolarBodies = new dListbox("Navigation_Bodies", new Vector2(0, 25), new Vector2(TBNT.Width, TBNT.Height), TBNT, Engine.Font_MediumSmall, "Solar Bodies", Groupbox, false, false);
				Groupbox.AddControl(Listbox_SolarBodies);

			//Set Course button
				Texture2D GoTexture = Engine.CreateTexture(152, 32, 148, 28, Color.Green, Color.DarkGreen);
				Button_SetCourse = new dLabel("Navigation_SetCourse", new Vector2(Listbox_SolarBodies.position.X + Listbox_SolarBodies.size.X + 5, 25), GoTexture, Groupbox, Engine.Font_MediumSmall, "SET COURSE", Color.White, false, false, false);
				Button_SetCourse.fontOffset = (Button_SetCourse.size / 2) - (Button_SetCourse.Font.MeasureString(Button_SetCourse.Text) / 2) + new Vector2(0,3);
				Button_SetCourse.DisabledTexture = Engine.CreateTexture(GoTexture.Width, GoTexture.Height, GoTexture.Width - 4, GoTexture.Height - 4, Color.DarkGreen, Color.Gray);
				Button_SetCourse.OriginalTexture = GoTexture;
				Button_SetCourse.HoverTexture = Engine.CreateTexture(GoTexture.Width, GoTexture.Height, GoTexture.Width - 4, GoTexture.Height - 4, Color.Green, new Color(40, 220, 40));
				Button_SetCourse.PressTexture = Engine.CreateTexture(GoTexture.Width, GoTexture.Height, GoTexture.Width - 4, GoTexture.Height - 4, Color.Green, Color.GreenYellow);
				Button_SetCourse.ReleaseTexture = Button_SetCourse.OriginalTexture;
				Button_SetCourse.PlaySound = true;
				Groupbox.AddControl(Button_SetCourse);
				Button_SetCourse.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_SetCourse.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_SetCourse.OnMousePress += new Engine.Handler(ButtonPress);
				Button_SetCourse.OnMouseRelease += new Engine.Handler(ButtonRelease);
			//De-Orbit button
				Texture2D DeOrbitTexture = Engine.CreateTexture(152, 32, 148, 28, Color.Red, Color.DarkRed);
				Button_DeOrbit = new dLabel("Navigation_DeOrbit", new Vector2(Listbox_SolarBodies.position.X + Listbox_SolarBodies.size.X + 5, 25), DeOrbitTexture, Groupbox, Engine.Font_MediumSmall, "De-Orbit", Color.White, false, false, false);
				Button_DeOrbit.fontOffset = (Button_DeOrbit.size / 2) - (Button_DeOrbit.Font.MeasureString(Button_DeOrbit.Text) / 2) + new Vector2(0, 3);
				Button_DeOrbit.DisabledTexture = Engine.CreateTexture(DeOrbitTexture.Width, DeOrbitTexture.Height, DeOrbitTexture.Width - 4, DeOrbitTexture.Height - 4, Color.DarkRed, Color.Gray);
				Button_DeOrbit.OriginalTexture = DeOrbitTexture;
				Button_DeOrbit.HoverTexture = Engine.CreateTexture(DeOrbitTexture.Width, DeOrbitTexture.Height, DeOrbitTexture.Width - 4, DeOrbitTexture.Height - 4, Color.Red, new Color(216, 37, 64));
				Button_DeOrbit.PressTexture = Engine.CreateTexture(DeOrbitTexture.Width, DeOrbitTexture.Height, DeOrbitTexture.Width - 4, DeOrbitTexture.Height - 4, Color.Red, Color.OrangeRed);
				Button_DeOrbit.ReleaseTexture = Button_DeOrbit.OriginalTexture;
				Button_DeOrbit.PlaySound = true;
				Groupbox.AddControl(Button_DeOrbit);
				Button_DeOrbit.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_DeOrbit.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_DeOrbit.OnMousePress += new Engine.Handler(ButtonPress);
				Button_DeOrbit.OnMouseRelease += new Engine.Handler(ButtonRelease);

			//Listbox warnings
				Listbox_Warnings = new dListbox("Navigation_Warnings", Button_SetCourse.offset + new Vector2(0, Button_SetCourse.size.Y + 2), Vector2.Zero, null, null, null, Groupbox, false, false);
				Listbox_Warnings.CanSelect = false;
				Listbox_Warnings.CanScroll = false;
				Groupbox.AddControl(Listbox_Warnings);

			//NoShip label
				Label_NoShip = new dLabel("Navigation_NoShip", Vector2.Zero, null, Listbox_Warnings, Engine.Font_Small, "No Ship Selected", Color.Red, false, false, false);
			//AlreadyInOrbit label
				Label_InOrbit = new dLabel("Navigation_InOrbit", Vector2.Zero, null, Listbox_Warnings, Engine.Font_Small, "In Orbit", Color.Green, false, false, false);
			//EnRoute label
				Label_EnRoute = new dLabel("Navigation_EnRoute", Vector2.Zero, null, Listbox_Warnings, Engine.Font_Small, "En Route", Color.Yellow, false, false, false);


			//Lastly, move the form to the center of the screen
			Form_Main.position = Engine.CurrentGameResolution / 2 - Form_Main.size / 2;
		}


		//Buttons
		public void ButtonEnter(dControl sender)
		{
			sender.Color = sender.HoverColor;
			sender.Texture = sender.HoverTexture;
		}
		public void ButtonLeave(dControl sender)
		{
			sender.Color = sender.OriginalColor;
			sender.Texture = sender.OriginalTexture;
		}

		public void ButtonPress(dControl sender)
		{
			sender.Color = sender.PressColor;
			sender.Texture = sender.PressTexture;
		}
		public void ButtonRelease(dControl sender)
		{
			sender.Color = sender.ReleaseColor;
			sender.Texture = sender.ReleaseTexture;

			switch (sender.name)
			{
				case "Navigation_Close":
					Hide(false);
					break;
				case "Navigation_SetCourse":
					//Find the entity associated with the selected GUI control
					BaseEntity SelectedEnt = Galaxy.EntityLookup(Listbox_SolarBodies.Selected.name);

					//If the Engine.camera's target has an orbit module and is a ship
                    if (Engine.camera.TargetCanOrbit() && Engine.camera.TargetIsShip())
					{
						//If the selected item is not the same as the Engine.camera target (just in case)
						if (Listbox_SolarBodies.Selected.name != Engine.camera.TargetObject.Name)
						{
							//Make the Engine.camera target object orbit the selected object
							Engine.camera.TargetObject.Orbit._parent = SelectedEnt;

							float AngleToParent = (float)(Math.Atan2(Engine.camera.TargetObject.Position.Y - SelectedEnt.Position.Y, Engine.camera.TargetObject.Position.X - SelectedEnt.Position.X));
							//float Dist = Vector2d.Distance(SelectedEnt.Position, Engine.cameraTargetObject.Position);

							Engine.camera.TargetObject.Orbit.OrbitSpeed = 4f; //Engine.camera.TargetObject.Velocity.Length();
							Engine.camera.TargetObject.Orbit.ParentAng = (AngleToParent) * -1;
							Engine.camera.TargetObject.Orbit.OrbitRadius = (SelectedEnt.Renderer != null ? new Vector2d((SelectedEnt.Renderer._texture.Width / 2 * SelectedEnt.Scale), (SelectedEnt.Renderer._texture.Height / 2 * SelectedEnt.Scale)) : new Vector2d(1, 1));
							//Engine.camera.TargetObject.Orbit.OrbitRadius = new Vector2d(100, 100);

							Engine.camera.TargetObject.Angle = AngleToParent - MathHelper.PiOver2;

							//Engine.camera.TargetObject.Orbit.OrbitSpeed = 0f;
							//Engine.camera.TargetObject.Orbit.ParentAng = MathHelper.Pi;
							//Engine.camera.TargetObject.Orbit.OrbitRadius = new Vector2d(100, 100);

							//Hide(true);
						}
					}
					break;
				case "Navigation_DeOrbit":
					//If the Engine.camera's target has an orbit module and is a ship
					if (Engine.camera.TargetCanOrbit() && Engine.camera.TargetIsShip())
					{
						Engine.camera.TargetObject.Orbit._parent = null;
					}
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
			base.Update();


			//Can we set a course?
			Button_SetCourse.Clickable = Engine.camera.TargetIsShip() && Listbox_SolarBodies.Selected != null && Engine.camera.TargetCanOrbit() && (Engine.camera.TargetObject.Orbit._parent != null ? Engine.camera.TargetObject.Orbit._parent.Name != Listbox_SolarBodies.Selected.name : true);
			
			//Should there even be a set course button?
			if (Engine.camera.TargetCanOrbit() && Listbox_SolarBodies.Selected != null && (Engine.camera.TargetObject.Orbit._parent != null ? Engine.camera.TargetObject.Orbit._parent.Name == Listbox_SolarBodies.Selected.name : false) && (Button_SetCourse.Active || !Button_DeOrbit.Active))
			{
				Button_SetCourse.SetActive(false, true, false);
				Button_DeOrbit.SetActive(true, true, false);
			}
			else if (Engine.camera.TargetCanOrbit() && Listbox_SolarBodies.Selected != null && (Engine.camera.TargetObject.Orbit._parent != null ? Engine.camera.TargetObject.Orbit._parent.Name != Listbox_SolarBodies.Selected.name : true) && (!Button_SetCourse.Active || Button_DeOrbit.Active))
			{
				Button_SetCourse.SetActive(true, true, false);
				Button_DeOrbit.SetActive(false, true, false);
			}
			if (Listbox_SolarBodies.Selected == null)
			{
				if (Button_SetCourse.Active == true)
				{
					Button_SetCourse.SetActive(false, true, false);
				}
				if (Button_DeOrbit.Active == true)
				{
					Button_DeOrbit.SetActive(false, true, false);
				}
			}

			//Engine.camera target isn't a ship
			if (!Engine.camera.TargetIsShip())
			{
				if (!Listbox_Warnings.Items.Contains(Label_NoShip))
				{
					Listbox_Warnings.Items.Add(Label_NoShip);
					Label_NoShip.SetActive(true, false, false);
				}
			}
			else
			{
				Listbox_Warnings.Items.Remove(Label_NoShip);
				Label_NoShip.SetActive(false, false, true);
			}

			//Selected planet is already being orbited or is soon to be orbited
			if (Listbox_SolarBodies.Selected != null && Engine.camera.TargetCanOrbit() && Engine.camera.TargetObject.Orbit._parent != null && Engine.camera.TargetObject.Orbit._parent.Name.Equals(Listbox_SolarBodies.Selected.name))
			{
				if (Engine.camera.TargetObject.Orbit.EnRoute == true)
				{
					Listbox_Warnings.Items.Remove(Label_InOrbit);
					Label_InOrbit.SetActive(false, false, true);

					if (!Listbox_Warnings.Items.Contains(Label_EnRoute))
					{
						Listbox_Warnings.Items.Add(Label_EnRoute);
						Label_EnRoute.SetActive(true, false, false);
					}
				}
				else
				{
					Listbox_Warnings.Items.Remove(Label_EnRoute);
					Label_EnRoute.SetActive(false, false, true);

					if (!Listbox_Warnings.Items.Contains(Label_InOrbit))
					{
						Listbox_Warnings.Items.Add(Label_InOrbit);
						Label_InOrbit.SetActive(true, false, false);
					}
				}
			}
			else
			{
				Listbox_Warnings.Items.Remove(Label_InOrbit);
				Label_InOrbit.SetActive(false, false, true);

				Listbox_Warnings.Items.Remove(Label_EnRoute);
				Label_EnRoute.SetActive(false, false, true);
			}



			//Update solar bodies list
			if (Galaxy.CurrentSolarSystem != null && (Galaxy.CurrentSolarSystem != PrevSolarsystem || Galaxy.CurrentSolarSystem.Entities.Count != PrevSolarsystemEntities))
			{
				Listbox_SolarBodies.Items.Clear();

				Listbox_SolarBodies.Selected = null;
				Listbox_SolarBodies.ScrollDownTarget = 0;

				for (int ii = 0; ii < Galaxy.CurrentSolarSystem.Entities.Count; ii++)
				{
					//dLabel lbl = new dLabel(Galaxy.CurrentSolarSystem.Entities[ii], Listbox_SolarBodies.position, Listbox_SolarBodies, Engine.Font_Medium, Galaxy.CurrentSolarSystem.Entities[ii], Color.White, false, false, false);
					BaseEntity ent = Galaxy.CurrentSolarSystem.Entities[ii];
					if (ent.BodyLogic != null && ent.ShipLogic == null || ent.BodyLogic != null && ent.ShipLogic != null) //If the entity is a planet and not a ship, or if the entity is "both".
					{
						dImage img = new dImage(Galaxy.CurrentSolarSystem.Entities[ii].Name, Listbox_SolarBodies.position, (ent.Renderer != null ? ent.Renderer._texture : Engine.CreateTexture(1, 1, Color.Red)), Listbox_SolarBodies, false, false);
						img.size = new Vector2d(Listbox_SolarBodies.size.X - 10, Listbox_SolarBodies.size.X - 10);

						img.SetActive(Listbox_SolarBodies.Active, true, false);

						Listbox_SolarBodies.Items.Add(img);
					}
				}

				//select the Engine.camera target for this new solarsystem
				//if (Galaxy.CurrentSolarSystem != null)
				//{
				//	string Engine.cameraTarget = Galaxy.CurrentSolarSystem.Engine.cameraTargetObject;
				//	BaseEntity entity = EntityManager.EntityLookup(Engine.cameraTarget);
				//	Listbox_SolarBodies.Selected = Listbox_SolarBodies.ControlLookup((entity != null ? entity.Name : ""));
				//}
			}

			//Set previous variables
			PrevSolarsystem = Galaxy.CurrentSolarSystem;
			PrevSolarsystemEntities = (Galaxy.CurrentSolarSystem != null ? Galaxy.CurrentSolarSystem.Entities.Count : 0);
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			base.Draw(spritebatch);
		}

	}
}
