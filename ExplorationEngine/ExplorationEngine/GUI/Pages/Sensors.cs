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
	public class Sensors : GUIPage
	{
		public BaseEntity LastScanned = null;


		public dLabel Title;

		public dGroupbox Groupbox;

		public dLabel Button_Close;
		public dLabel Button_Scan;

		public dLabel Label_NoShip;
		public dLabel Label_NotInOrbit;

		public dListbox Listbox_Warnings;

		public dTextbox Textbox_ScanData;

		public Sensors()
			: base()
		{
			Engine.guiManager.Pages.Add(this);

			//Main form
				Form_Main = new dForm("Sensors", new Rectangle(0, 0, 0, 0), Engine.CreateTexture(950, 500, 949, 499, new Color(200, 30, 30), new Color(0,0,0,200)), null, false, false);
				Form_Main.IsDragable = true;
				Form_Main.IsFullscreen = false;

			//Page logo
				Title = new dLabel("Sensors_Title", Vector2.Zero, null, Form_Main, Engine.Font_Large, "Sensors", Color.White, false, false, false);
				Title.offset = Form_Main.position + new Vector2(Form_Main.size.X / 2, -Title.Font.MeasureString(Title.Text).Y);
				Form_Main.AddControl(Title);

			//Groupbox for all elements
				Groupbox = new dGroupbox("Sensors_Groupbox", null, Vector2.Zero, null, null, Form_Main, false, false);
				Form_Main.AddControl(Groupbox);

			//Scan button
				Texture2D ScanTexture = Engine.CreateTexture(140, 32, 138, 28, Color.Green, Color.DarkGreen);
				Button_Scan = new dLabel("Sensors_Scan", new Vector2(5, 25), ScanTexture, Groupbox, Engine.Font_MediumSmall, "SCAN", Color.White, false, false, false);
				Button_Scan.fontOffset = (Button_Scan.size / 2) - (Button_Scan.Font.MeasureString(Button_Scan.Text) / 2) + new Vector2(0, 3);
				Button_Scan.DisabledTexture = Engine.CreateTexture(ScanTexture.Width, ScanTexture.Height, ScanTexture.Width - 4, ScanTexture.Height - 4, Color.DarkGreen, Color.Gray);
				Button_Scan.OriginalTexture = ScanTexture;
				Button_Scan.HoverTexture = Engine.CreateTexture(ScanTexture.Width, ScanTexture.Height, ScanTexture.Width - 4, ScanTexture.Height - 4, Color.Green, new Color(40, 220, 40));
				Button_Scan.PressTexture = Engine.CreateTexture(ScanTexture.Width, ScanTexture.Height, ScanTexture.Width - 4, ScanTexture.Height - 4, Color.Green, Color.GreenYellow);
				Button_Scan.ReleaseTexture = Button_Scan.OriginalTexture;
				Button_Scan.PlaySound = true;
				Groupbox.AddControl(Button_Scan);
				Button_Scan.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Scan.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Scan.OnMousePress += new Engine.Handler(ButtonPress);
				Button_Scan.OnMouseRelease += new Engine.Handler(ButtonRelease);

			//Close button
				Texture2D CloseTexture = Engine.CreateTexture(24, 24, 23, 23, Color.Gray, Color.White);
				Button_Close = new dLabel("Sensors_Close", new Vector2(Form_Main.size.X - CloseTexture.Width, 0), CloseTexture, Groupbox, null, "", Color.White, false, false, false);
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

			//Scan Data Textbox
				Textbox_ScanData = new dTextbox("Sensors_Data", new Vector2(150, 25), Engine.CreateTexture(775, 450, 774, 449, Color.Gray, Color.Black), Engine.Font_Small, Groupbox, false, false);
				Groupbox.AddControl(Textbox_ScanData);
				Textbox_ScanData.BlocksParentInteraction = false;
				Textbox_ScanData.Multiline = true;
				Textbox_ScanData.ReadOnly = true;
				Textbox_ScanData.DisabledColor = Color.White;
				Textbox_ScanData.Text = "Waiting...";

			//Listbox warnings
				Listbox_Warnings = new dListbox("Sensors_Warnings", Button_Scan.offset + new Vector2(0, Button_Scan.size.Y + 2), Vector2.Zero, null, null, null, Groupbox, false, false);
				Listbox_Warnings.CanSelect = false;
				Listbox_Warnings.CanScroll = false;
				Groupbox.AddControl(Listbox_Warnings);

			//NoShip label
				Label_NoShip = new dLabel("Sensors_NoShip", Vector2.Zero, null, Listbox_Warnings, Engine.Font_Small, "No Ship Selected", Color.Red, false, false, false);
			//NotInOrbit label
				Label_NotInOrbit = new dLabel("Sensors_NotInOrbit", Vector2.Zero, null, Listbox_Warnings, Engine.Font_Small, "Not In Orbit", Color.Red, false, false, false);

			//Lastly, move the form to the center of the screen
				Form_Main.position = (Vector2)Engine.CurrentGameResolution / 2 - Form_Main.size / 2;
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
				case "Sensors_Close":
					Hide(false);
					break;
				case "Sensors_Scan":
					
					//Scan entity IF
					if (Engine.camera.TargetIsShip() && Engine.camera.TargetCanOrbit() && Engine.camera.TargetObject.Orbit._parent != null)
					{
						Engine.Sound_Ping.Play();

						BaseEntity Tar = Engine.camera.TargetObject.Orbit._parent;
						LastScanned = Tar;

						int Children = 0;
						for (int ii = 0; ii < Galaxy.CurrentSolarSystem.Entities.Count; ii++)
						{
							BaseEntity ent = Galaxy.CurrentSolarSystem.Entities[ii];
							if (ent.Orbit != null && ent.Orbit._parent == Tar && ent.ShipLogic == null)
								Children++;
						}

						Textbox_ScanData.Text =
							"Ship '" + Engine.camera.TargetObject.Name + "' is orbiting " + (Tar.BodyLogic != null ? Tar.BodyLogic.Class.ToString() : "") + " '" + Tar.Name + "'" + (Tar.BodyLogic != null ? " of type " + Tar.BodyLogic.Type.ToString() : "") + "." + "\n" +
							"\n" +

							(Tar.Orbit != null && Tar.Orbit._parent != null ? "The " + (Tar.BodyLogic != null ? Tar.BodyLogic.Class.ToString() : "") + " '" + Tar.Name + "' is orbiting " + (Tar.Orbit._parent.BodyLogic != null ? Tar.Orbit._parent.BodyLogic.Class.ToString() : "") + " '" + Tar.Orbit._parent.Name + "\n" : "") +

							Tar.Name + " has " + (Children > 0 ? Children.ToString() : "no" )+ " children." + "\n" +
							Tar.Name + " has a mass of " + Math.Round(Tar.Mass, 1) + " kg." + "\n" +

							(Tar.BodyLogic != null ? "Its core temperature is " + Math.Round(Tar.BodyLogic.CoreTemperature, 1) + " celsius." + "\n" : "") +

							(Tar.BodyLogic != null ? "Its surface temperature is " + Math.Round(Tar.BodyLogic.SurfaceTemperature, 1) + " celsius." + "\n" : "") +

							(Tar.Orbit != null && Tar.Orbit.OrbitRadius.Length() > 0 ? "It has an orbit radius of " + Math.Round((Tar.Orbit.OrbitRadius.X + Tar.Orbit.OrbitRadius.Y) / 2d, 1) + " Km." + "\n" : "") +

							(Tar.Renderer != null && Tar.Renderer._texture != null ? 
							"It has a diameter of " + (((Tar.Renderer._texture.Width + Tar.Renderer._texture.Height) / 2) * Tar.Scale).ToString() + " km." : "Its diameter cannot be measured.") + "\n" +
							
							(Tar.Renderer != null && Tar.Renderer._texture != null ?
							"The " + (Tar.BodyLogic != null ? Tar.BodyLogic.Class.ToString() : "") + " has a volume of " + Math.Round((4d / 3d) * Math.PI * (Math.Pow(Tar.Renderer._texture.Width / 2, 3)), 1) + " cubic kilometers." : "Its volume cannot be measured") + "\n" +
							
							"";
					}
					break;
			}
		}


		public override void Show(GUIPage lastform, bool quick)
		{
			base.Show(lastform, quick);

			//If our target is a ship, it can orbit, it has a parent, and the last scanned entity is not equal to the current one, change the text back to "waiting..."
			if (Engine.camera.TargetIsShip() && Engine.camera.TargetCanOrbit() && Engine.camera.TargetObject.Orbit._parent != null && LastScanned != Engine.camera.TargetObject.Orbit._parent)
			{
				Textbox_ScanData.Text = "Waiting...";
			}

		}

		public override void Hide(bool quick)
		{
			base.Hide(quick);

		}


		public override void Refresh()
		{
			base.Refresh();
		}


		public override void Update()
		{
			base.Update();

			//Can we set scan
			Button_Scan.Clickable = Engine.camera.TargetIsShip() && Engine.camera.TargetCanOrbit() && Engine.camera.TargetObject.Orbit._parent != null;

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

			//Engine.camera target isn't a ship
			if (Engine.camera.TargetCanOrbit() && Engine.camera.TargetObject.Orbit._parent == null)
			{
				if (!Listbox_Warnings.Items.Contains(Label_NotInOrbit))
				{
					Listbox_Warnings.Items.Add(Label_NotInOrbit);
					Label_NotInOrbit.SetActive(true, false, false);
				}
			}
			else
			{
				Listbox_Warnings.Items.Remove(Label_NotInOrbit);
				Label_NotInOrbit.SetActive(false, false, true);
			}


		}

		public override void Draw(SpriteBatch spritebatch)
		{
			base.Draw(spritebatch);
		}

	}
}
