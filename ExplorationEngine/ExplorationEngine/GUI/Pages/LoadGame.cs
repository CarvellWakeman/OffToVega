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
	public class LoadGame : GUIPage
	{
		public dLabel Button_Back;
		public dLabel Button_Play;
		public dLabel Button_Delete;

		public dLabel Label_Logo;

		public dListbox Listbox_Saves;

		private int PrevNumberSaves = 0;


		public LoadGame() : base()
		{
			Engine.Pages.Add(this);

			//Main Form
			Form_Main = new dForm("LoadGame", new Rectangle(0, 0, (int)Engine.CurrentGameResolution.X, (int)Engine.CurrentGameResolution.Y), Engine.StarField, null, false, false);
				//Form_Main.IsDragable = true;
			
			//LoadGame logo
			Label_Logo = new dLabel("LoadGame_Logo", new Vector2(Engine.VirtualScreenResolution.X / 2, 0), null, Form_Main, Engine.Font_Large, "LOAD GAME", Color.White, true, false, false);
				Form_Main.AddControl(Label_Logo);

			//Back button
                Button_Back = new dLabel("LoadGame_Back", new Vector2d(50, 50), null, Form_Main, Engine.Font_Large, "BACK", Color.White, false, false, false);
                Button_Back.HoverColor = Color.Purple;
				Button_Back.ReleaseColor = Color.Red;
				Button_Back.PlaySound = true;
				Form_Main.AddControl(Button_Back);
				Button_Back.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Back.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Back.OnMouseRelease += new Engine.Handler(ButtonRelease);
				Button_Back.EmulationKey = Input.Pause;

			//Play button
				Button_Play = new dLabel("LoadGame_Play", new Vector2(1150, 650), null, Form_Main, Engine.Font_Medium, "Play", Color.White, false, false, false);
				Button_Play.HoverColor = Color.Purple;
				Button_Play.ReleaseColor = Color.Red;
				Button_Play.PlaySound = true;
				Form_Main.AddControl(Button_Play);
				//Button_Play.size = new Vector2(260, 39);
				Button_Play.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Play.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Play.OnMouseRelease += new Engine.Handler(ButtonRelease);
				Button_Play.EmulationKey = Input.Proceed;

			//Delete button
				Button_Delete = new dLabel("LoadGame_Delete", new Vector2(50, 650), null, Form_Main, Engine.Font_Medium, "Delete", Color.White, false, false, false);
				Button_Delete.HoverColor = Color.DarkRed;
				Button_Delete.PressColor = Color.OrangeRed;
				Button_Delete.PlaySound = true;
				Form_Main.AddControl(Button_Delete);
				Button_Delete.EmulationKey = Input.Delete;
				//Button_Delete.size = new Vector2(260, 39);
				Button_Delete.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Delete.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Delete.OnMouseRelease += new Engine.Handler(ButtonRelease);
				
			//Saves Listbox
			Texture2D TBNT = Engine.CreateTexture(900, 450, 899, 449, new Color(0,24,255), Color.Black);
			Listbox_Saves = new dListbox("LoadGame_Saves", new Vector2(200, 150), new Vector2(TBNT.Width, TBNT.Height), TBNT, Engine.Font_Medium, "Saves", Form_Main, false, false);
				Form_Main.AddControl(Listbox_Saves);
				Listbox_Saves.PlaySound = true;
				Listbox_Saves.EnterSound = null;
				Listbox_Saves.LeaveSound = null;
				Listbox_Saves.PressSound = null;
				Listbox_Saves.ReleaseSound = null;

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
				case "LoadGame_Back":
					Engine.saveLoad.SaveOptions();
					Hide(false);
					break;
				case "LoadGame_Play":
					if (Listbox_Saves.Selected != null)
					{
						if (Engine.saveLoad.Load(Listbox_Saves.Selected.name) == true)
						{

							//Engine.camera.ResetMouse();
							//Galaxy.CurrentSolarSystem = Galaxy.SolarSystems[0];

							//Engine
							Engine.UpdateGame = true;
							Engine.DrawGame = true;
							Engine.IsPaused = false;
							Engine.HUD.Show(this, true);

							Listbox_Saves.Selected = null;

							//Hide other menus
							Engine.MainMenu.Hide(true);
							Engine.LoadGame.Hide(true);
							Engine.Options.Hide(true);
							Hide(true);
						}
						else
						{
							System.Windows.Forms.MessageBox.Show("Error loading save file '" + Listbox_Saves.Selected.name + "'");
						}
					}

					break;
				case "LoadGame_Delete":
					if (Listbox_Saves.Selected != null)
					{
						Engine.saveLoad.DeleteSave(Listbox_Saves.Selected.name);
						Listbox_Saves.DeleteComponent(Listbox_Saves.Selected);
						Listbox_Saves.Selected = null;
					}
					break;
			}
		}




		public override void Show(GUIPage lastform, bool quick)
		{
			Engine.saveLoad.ReloadSaveFiles();

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

			//Make the play button and delete button clickable or not
			Button_Play.Clickable = Listbox_Saves.Selected != null;
			Button_Delete.Clickable = Listbox_Saves.Selected != null;

			//Update saves list
			if (Engine.saveLoad.Saves.Count != PrevNumberSaves)
			{
				Listbox_Saves.Items.Clear();

				for (int ii = 0; ii < Engine.saveLoad.Saves.Count; ii++)
				{
					dLabel lbl = new dLabel(Engine.saveLoad.Saves.Keys[ii], Listbox_Saves.position, null, Listbox_Saves, Engine.Font_Medium, Engine.saveLoad.Saves.Keys[ii], Color.White, false, false, false);
					lbl.size = new Vector2(Listbox_Saves.size.X - 3, lbl.Font.MeasureString(lbl.Text).Y);

					Listbox_Saves.Items.Add(lbl);
				}
			}

			PrevNumberSaves = Engine.saveLoad.Saves.Count;
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			base.Draw(spritebatch);
		}

	}
}
