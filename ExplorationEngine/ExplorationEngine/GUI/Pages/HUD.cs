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
	public class HUD : GUIPage
	{
		public bool Active = true;

		private SolarSystem PrevSolarsystem;
		private int PrevSolarsystemEntities;
		private bool PrevDebugState;

		public dListbox Listbox_Entities;
		public dListbox Listbox_Controls;

		public dLabel Button_HideShow;
		public dLabel Button_Orbit;
		public dLabel Button_SetCamera;
		public dLabel Button_ReleaseCamera;

		public dLabel Button_Navigation;
		public dLabel Button_Sensors;
		public dLabel Button_GalaxyMap;
		public dLabel Button_LocalMap;
		public dLabel Button_Ship;

		public dGroupbox Groupbox_Entities;
		public dGroupbox Groupbox_Shipcontrols;


		public HUD() : base()
		{
			Engine.Pages.Add(this);

			//Main Form
				Form_Main = new dForm("HUD", new Rectangle(0, 0, 0, 0), null, null, false, false);
				Form_Main.OriginalColor = new Color(0, 255, 0, 255);
				Form_Main.Color = new Color(0, 255, 0, 255);
				//Form_Main.CanFocus = false;
				//Form_Main.DrawOnDebug = false;
				Form_Main.ActiveToWork = false;
				Form_Main.UserInteract = false;
				Form_Main.BackgroundForm = true;
				//Form_Main.IsDragable = true;

			//Entities groupbox
				Groupbox_Entities = new dGroupbox("HUD_Entities", null, new Vector2(0, 0), null, "", Form_Main, false, false);
				Form_Main.AddControl(Groupbox_Entities);

			//Entities Listbox
				Texture2D TBNT = Engine.CreateTexture(200, 600, 199, 599, new Color(0, 24, 255), Color.Black);
				Listbox_Entities = new dListbox("HUD_Entities", new Vector2(-200, 30), new Vector2(TBNT.Width, TBNT.Height), TBNT, Engine.Font_MediumSmall, "Debug Entities", Groupbox_Entities, false, false);
				Groupbox_Entities.AddControl(Listbox_Entities);
				Listbox_Entities.titleColor = Color.Red;

			//Button HideShow
				Button_HideShow = new dLabel("HUD_HideShow", new Vector2(-20, 0), null, Listbox_Entities, Engine.Font_Medium, "o", Color.Gray, false, false, false);
				Button_HideShow.size = new Vector2(20, 20);
				Button_HideShow.textureOffset = new Vector2(0, -16);
				Listbox_Entities.AddControl(Button_HideShow);
				Button_HideShow.OnMouseRelease += new Engine.Handler(ButtonRelease);
				Button_HideShow.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_HideShow.OnMouseLeave += new Engine.Handler(ButtonLeave);

			//Button Set Camera
				Button_SetCamera = new dLabel("HUD_CameraSwitch", new Vector2(0, Listbox_Entities.position.Y + Listbox_Entities.size.Y), null, Listbox_Entities, Engine.Font_Small, "Set Camera", Color.Red, false, false, true);
				Button_SetCamera.PressColor = Color.Red;
				Listbox_Entities.AddControl(Button_SetCamera);
				Button_SetCamera.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_SetCamera.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_SetCamera.OnMouseRelease += new Engine.Handler(ButtonRelease);
			
			//Button Release Camera
				Button_ReleaseCamera = new dLabel("HUD_CameraRelease", new Vector2(0, Listbox_Entities.position.Y + Listbox_Entities.size.Y + Button_SetCamera.size.Y), null, Listbox_Entities, Engine.Font_Small, "Release Camera", Color.Red, false, false, true);
				Button_ReleaseCamera.PressColor = Color.Red;
				Listbox_Entities.AddControl(Button_ReleaseCamera);
				Button_ReleaseCamera.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_ReleaseCamera.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_ReleaseCamera.OnMouseRelease += new Engine.Handler(ButtonRelease);

			//Button Orbit
				Button_Orbit = new dLabel("HUD_Orbit", new Vector2(0, Listbox_Entities.position.Y + Listbox_Entities.size.Y + Button_SetCamera.size.Y + Button_ReleaseCamera.size.Y), null, Listbox_Entities, Engine.Font_Small, "Orbit Entity", Color.Red, false, false, true);
				Button_Orbit.PressColor = Color.Red;
				Listbox_Entities.AddControl(Button_Orbit);
				Button_Orbit.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Orbit.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Orbit.OnMouseRelease += new Engine.Handler(ButtonRelease);


			//Ship controls
			//Groupbox ship controls
				Groupbox_Shipcontrols = new dGroupbox("HUD_Controls", Engine.CreateTexture(120, 300, 119, 299, Color.Gray, Color.Transparent), new Vector2(0, 50), null, "", Form_Main, false, false);
				Form_Main.AddControl(Groupbox_Shipcontrols);
			//Button Navigation
				Button_Navigation = new dLabel("HUD_Nav", new Vector2(Groupbox_Shipcontrols.size.X / 2, 5), Engine.CreateTexture(110, 30, Color.Gray), Groupbox_Shipcontrols, Engine.Font_Small, "NAVIGATION", Color.White, true, false, false);
				Button_Navigation.fontOffset = (Button_Navigation.size / 2) - (Button_Navigation.Font.MeasureString(Button_Navigation.Text) / 2);
				Button_Navigation.PlaySound = true;
				Button_Navigation.EnterSound = null;
				Groupbox_Shipcontrols.AddControl(Button_Navigation);
				Button_Navigation.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Navigation.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Navigation.OnMousePress += new Engine.Handler(ButtonPress);
				Button_Navigation.OnMouseRelease += new Engine.Handler(ButtonRelease);
			//Button Sensors
				Button_Sensors = new dLabel("HUD_Sense", new Vector2(Groupbox_Shipcontrols.size.X / 2, 40), Engine.CreateTexture(110, 30, Color.Gray), Groupbox_Shipcontrols, Engine.Font_Small, "SENSORS", Color.White, true, false, false);
				Button_Sensors.fontOffset = (Button_Sensors.size / 2) - (Button_Sensors.Font.MeasureString(Button_Sensors.Text) / 2);
				Button_Sensors.PlaySound = true;
				Button_Sensors.EnterSound = null;
				Groupbox_Shipcontrols.AddControl(Button_Sensors);
				Button_Sensors.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Sensors.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Sensors.OnMousePress += new Engine.Handler(ButtonPress);
				Button_Sensors.OnMouseRelease += new Engine.Handler(ButtonRelease);

			//Button LocalMap
				Button_LocalMap = new dLabel("HUD_LocalMap", new Vector2(Groupbox_Shipcontrols.size.X / 2, 75), Engine.CreateTexture(110, 30, Color.Gray), Groupbox_Shipcontrols, Engine.Font_Small, "LOCAL MAP", Color.White, true, false, false);
				Button_LocalMap.fontOffset = (Button_LocalMap.size / 2) - (Button_LocalMap.Font.MeasureString(Button_LocalMap.Text) / 2);
				Button_LocalMap.PlaySound = true;
				Button_LocalMap.EnterSound = null;
				Groupbox_Shipcontrols.AddControl(Button_LocalMap);
				Button_LocalMap.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_LocalMap.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_LocalMap.OnMousePress += new Engine.Handler(ButtonPress);
				Button_LocalMap.OnMouseRelease += new Engine.Handler(ButtonRelease);
			//Button GalaxyMap
				Button_GalaxyMap = new dLabel("HUD_GalaxyMap", new Vector2(Groupbox_Shipcontrols.size.X / 2, 110), Engine.CreateTexture(110, 30, Color.Gray), Groupbox_Shipcontrols, Engine.Font_Small, "GALAXY MAP", Color.White, true, false, false);
				Button_GalaxyMap.fontOffset = (Button_GalaxyMap.size / 2) - (Button_GalaxyMap.Font.MeasureString(Button_GalaxyMap.Text) / 2);
				Button_GalaxyMap.PlaySound = true;
				Button_GalaxyMap.EnterSound = null;
				Groupbox_Shipcontrols.AddControl(Button_GalaxyMap);
				Button_GalaxyMap.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_GalaxyMap.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_GalaxyMap.OnMousePress += new Engine.Handler(ButtonPress);
				Button_GalaxyMap.OnMouseRelease += new Engine.Handler(ButtonRelease);

			//Button Ship
				Button_Ship = new dLabel("HUD_Ship", new Vector2(Groupbox_Shipcontrols.size.X / 2, 145), Engine.CreateTexture(110, 30, Color.Gray), Groupbox_Shipcontrols, Engine.Font_Small, "SHIP", Color.White, true, false, false);
				Button_Ship.fontOffset = (Button_Ship.size / 2) - (Button_Ship.Font.MeasureString(Button_Ship.Text) / 2);
				Button_Ship.Clickable = false;
				Groupbox_Shipcontrols.AddControl(Button_Ship);
				Button_Ship.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Ship.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Ship.OnMousePress += new Engine.Handler(ButtonPress);
				Button_Ship.OnMouseRelease += new Engine.Handler(ButtonRelease);


				////Ship controls
				////Listbox ship controls
				//Listbox_Controls = new dListbox("HUD_Controls", new Vector2(0, 50), Engine.CreateTexture(110, 300, 109, 299, Color.Gray, Color.Transparent), Engine.Font_MediumSmall, "Controls", Form_Main, false, false);
				//Form_Main.AddControl(Listbox_Controls);
				//Listbox_Controls.CanSelect = false;
				////Groupbox_Shipcontrols = new dGroupbox("HUD_Controls", Engine.CreateTexture(110, 300, 109, 299, Color.Gray, Color.Transparent), new Vector2(0, 50), null, "", Form_Main, false, false);
				////Form_Main.AddControl(Groupbox_Shipcontrols);
				////Button Navigation
				//Button_Navigation = new dLabel("HUD_Nav", new Vector2(0, 0), Engine.CreateTexture(100, 30, Color.Gray), Listbox_Controls, Engine.Font_Small, "NAVIGATION", Color.White, false, false, false);
				//Button_Navigation.fontOffset = (Button_Navigation.size / 2) - (Button_Navigation.Font.MeasureString(Button_Navigation.Text) / 2);
				//Listbox_Controls.objects.Add(Button_Navigation);
				//Button_Navigation.OnMouseEnter += new Engine.Handler(ButtonEnter);
				//Button_Navigation.OnMouseLeave += new Engine.Handler(ButtonLeave);
				//Button_Navigation.OnMousePress += new Engine.Handler(ButtonPress);
				//Button_Navigation.OnMouseRelease += new Engine.Handler(ButtonRelease);
				////Button Sensors
				//Button_Sensors = new dLabel("HUD_Sense", new Vector2(0, 0), Engine.CreateTexture(100, 30, Color.Gray), Listbox_Controls, Engine.Font_Small, "SENSORS", Color.White, false, false, false);
				//Button_Sensors.fontOffset = (Button_Sensors.size / 2) - (Button_Sensors.Font.MeasureString(Button_Sensors.Text) / 2);
				//Listbox_Controls.objects.Add(Button_Sensors);
				//Button_Sensors.OnMouseEnter += new Engine.Handler(ButtonEnter);
				//Button_Sensors.OnMouseLeave += new Engine.Handler(ButtonLeave);
				//Button_Sensors.OnMousePress += new Engine.Handler(ButtonPress);
				//Button_Sensors.OnMouseRelease += new Engine.Handler(ButtonRelease);
				////Button Map
				//Button_Map = new dLabel("HUD_Map", new Vector2(0, 0), Engine.CreateTexture(100, 30, Color.Gray), Listbox_Controls, Engine.Font_Small, "MAP", Color.White, false, false, false);
				//Button_Map.fontOffset = (Button_Map.size / 2) - (Button_Map.Font.MeasureString(Button_Map.Text) / 2);
				//Listbox_Controls.objects.Add(Button_Map);
				//Button_Map.OnMouseEnter += new Engine.Handler(ButtonEnter);
				//Button_Map.OnMouseLeave += new Engine.Handler(ButtonLeave);
				//Button_Map.OnMousePress += new Engine.Handler(ButtonPress);
				//Button_Map.OnMouseRelease += new Engine.Handler(ButtonRelease);

		}



		//Buttons
		public void ButtonEnter(dControl sender)
		{
			//sender.textureOffset += new Vector2(190, 0);
			sender.Color = Color.Purple;


		}
		public void ButtonLeave(dControl sender)
		{
			//sender.textureOffset -= new Vector2(190, 0);
			sender.Color = sender.OriginalColor;
		}

		public void ButtonPress(dControl sender)
		{
			sender.Color = sender.PressColor;
		}
		public void ButtonRelease(dControl sender)
		{
			sender.Color = sender.OriginalColor;

			switch (sender.name)
			{
				case "HUD_Nav":
					//Messagebox Mkay = new Messagebox(this, "This is a messagebox. This is a messagebox. This is a messagebox. This is a messagebox. This is a messagebox.");
					//Mkay.Show(false);

					//Hide/Show navigation
					if (Engine.Navigation.Visible)
					{
						Engine.Navigation.Hide(false);
					}
					else
					{
						Engine.Navigation.Show(this, false);
						Engine.Navigation.Form_Main.position = Engine.CurrentScreenResolution / 2 - Engine.Navigation.Form_Main.size / 2;
					}
					break;
				case "HUD_Sense":
					//Messagebox Mkay = new Messagebox(this, "This is a multiline read-only messagebox. This is a multiline read-only messagebox. This is a multiline read-only messagebox. ", false);
					//Mkay.Show(this, false);

					//Hide/Show sensors
					if (Engine.Sensors.Visible)
					{
						Engine.Sensors.Hide(false);
					}
					else
					{
						Engine.Sensors.Show(this, false);
						Engine.Sensors.Form_Main.position = Engine.CurrentScreenResolution / 2 - Engine.Sensors.Form_Main.size / 2;
					}
					break;
				case "HUD_GalaxyMap":
					Engine.GalaxyMap.Show(this, true);
					break;
				case "HUD_LocalMap":
					//Hide/Show Local Map
					if (Engine.LocalMap.Visible)
					{
						Engine.LocalMap.Hide(false);
					}
					else
					{
						Engine.LocalMap.Show(this, false);
						Engine.LocalMap.Form_Main.position = Engine.CurrentScreenResolution / 2 - Engine.LocalMap.Form_Main.size / 2;
					}
					break;



				case "HUD_HideShow":
					Listbox_Entities.offset = (Listbox_Entities.offset == new Vector2(0, 30) ? new Vector2(-200, 30) : new Vector2(0, 30));
					break;
				case "HUD_CameraSwitch":
					//Set old target (if it's a ship) to not be controlled
					if (Camera.TargetIsShip())
					{
						Camera.TargetObject.ShipLogic.IsControlled = false;
					}

					//If there's something selected
					if (Listbox_Entities.Selected != null)
					{
						BaseEntity ent = Galaxy.EntityLookup(Listbox_Entities.Selected.name);

						//Set new target, (if it's a ship) to be controlled
						if (ent.ShipLogic != null)
						{
							ent.ShipLogic.IsControlled = true;
						}

						//Set camera target
						Camera.TargetObject = ent;
					}
					break;
				case "HUD_CameraRelease":
					//Set old target (if it's a ship) to not be controlled
					if (Camera.TargetIsShip())
					{
						Camera.TargetObject.ShipLogic.IsControlled = false;
					}

					Camera.TargetObject = null;
					break;
				case "HUD_Orbit": //Orbit or deorbit

					if (Camera.TargetExists())
					{
						if (Listbox_Entities.Selected != null)
						{
							//Find the entity associated with that GUI control
							BaseEntity SelectedEnt = Galaxy.EntityLookup(Listbox_Entities.Selected.name);

							//If the selected item is not the same as the camera target
							if (Listbox_Entities.Selected.name != Camera.TargetObject.Name)
							{
								if (Camera.TargetObject.Orbit._parent == null || Listbox_Entities.Selected.name != Camera.TargetObject.Orbit._parent.Name)
								{
									//Make the camera target object orbit the selected object
									Camera.TargetObject.Orbit._parent = SelectedEnt;

									float AngleToParent = (float)(Math.Atan2(Camera.TargetObject.Position.Y - SelectedEnt.Position.Y, Camera.TargetObject.Position.X - SelectedEnt.Position.X));
									double Dist = Vector2d.Distance(SelectedEnt.Position, Camera.TargetObject.Position);

									//Camera.TargetObject.Orbit.OrbitSpeed = Camera.TargetObject.Velocity.Length();
									Camera.TargetObject.Orbit.ParentAng = (AngleToParent) * -1;
									Camera.TargetObject.Orbit.OrbitRadius = new Vector2d(Dist, Dist);
									
								}
								else
								{
									//Deorbit the camera's object
									Camera.TargetObject.Orbit._parent = null;
								}
							}
							else //Else if the selected object is the SAME as the camera's object, 
							{
								if (Camera.TargetObject.Orbit._parent != null)
								{
									//Deorbit the camera's object
									Camera.TargetObject.Orbit._parent = null;
								}
								else
								{
									//Do nothing
								}
								
							}
						}
						else if (Camera.TargetCanOrbit())
						{
							//Deorbit the camera's object
							Camera.TargetObject.Orbit._parent = null;
						}
					}

				break;
			}
		}


		public override void Show(GUIPage lastform, bool quick)
		{
			base.Show(lastform, quick);
			//Visible = true; // We're visible now!

			////Set the form and its children to active
			//Form_Main.SetActive(true, true, quick);
			//if (quick)
			//{
			//	Form_Main.alpha = 1;
			//}
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
			if (!Engine.IsPaused)
			{
				Form_Main.size = Engine.CurrentScreenResolution;

				//Set entities list position
				Groupbox_Entities.offset = new Vector2(1280, 0);


				base.Update();

				//Hide entities list unless debug mode is on.
				if (Groupbox_Entities.Active != Engine.DebugState)
					Groupbox_Entities.SetActive(Engine.DebugState, true, true);

				//Update entities list
				if (Galaxy.CurrentSolarSystem != null && (Galaxy.CurrentSolarSystem != PrevSolarsystem || Galaxy.CurrentSolarSystem.Entities.Count != PrevSolarsystemEntities))
				{
					Listbox_Entities.Items.Clear();

					for (int ii = 0; ii < Galaxy.CurrentSolarSystem.Entities.Count; ii++)
					{
						dLabel lbl = new dLabel(Galaxy.CurrentSolarSystem.Entities[ii].Name, Listbox_Entities.position, null, Listbox_Entities, Engine.Font_Small, Galaxy.CurrentSolarSystem.Entities[ii].Name, Color.White, false, false, false);
						lbl.size = new Vector2(Listbox_Entities.size.X, lbl.Font.MeasureString(lbl.Text).Y);
						Listbox_Entities.Items.Add(lbl);
					}

					//select the camera target for this new solarsystem
					if (Galaxy.CurrentSolarSystem != null)
					{
						Listbox_Entities.Selected = Listbox_Entities.ControlLookup((Camera.TargetObject != null ? Camera.TargetObject.Name : ""));
					}
				}

				//Set Camera button clickability
				Button_SetCamera.Clickable = Listbox_Entities.Selected != null;

				//Release camera button clickability
				Button_ReleaseCamera.Clickable = Camera.TargetExists();


				//Set orbit button text
				if (Camera.TargetCanOrbit())
				{
					Button_Orbit.Clickable = true;

					if (Listbox_Entities.Selected != null)
					{

						//If the selected item is not the same as the camera target
						if (Listbox_Entities.Selected.name != Camera.TargetObject.Name)
						{
							if (Camera.TargetObject.Orbit._parent == null || Listbox_Entities.Selected.name != Camera.TargetObject.Orbit._parent.Name)
							{
								Button_Orbit.Text = "Orbit Entity";
							}
							else
							{
								Button_Orbit.Text = "De-Orbit Entity";
							}

							
						}
						else //Else if the selected object is the SAME as the camera's object, 
						{
							if (Camera.TargetObject.Orbit._parent != null)
							{
								//Deorbit the camera's object
								Button_Orbit.Text = "De-Orbit Target";
							}
							else
							{
								Button_Orbit.Text = "";
							}
						}
					}
					else
					{
						if (Camera.TargetObject.Orbit._parent != null)
						{
							//Deorbit the camera's object
							Button_Orbit.Text = "De-Orbit Target";
						}
						else
						{
							Button_Orbit.Text = "";
						}
					}
				}
				else
				{
					Button_Orbit.Text = "Select Entity";
					Button_Orbit.Clickable = false;
				}


				//Set previous variables
				PrevDebugState = Engine.DebugState;
				PrevSolarsystem = Galaxy.CurrentSolarSystem;
				PrevSolarsystemEntities = (Galaxy.CurrentSolarSystem != null ? Galaxy.CurrentSolarSystem.Entities.Count : 0);
			}
		}

	}
}
