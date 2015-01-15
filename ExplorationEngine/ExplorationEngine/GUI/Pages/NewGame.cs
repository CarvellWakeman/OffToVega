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
	public class NewGame : GUIPage
	{
		public dLabel Label_Logo;

		public dLabel Button_Back;
		public dLabel Button_Create;

		public dTextbox Textbox_Name;

		public NewGame() : base()
		{
			Engine.guiManager.Pages.Add(this);

			//Main form
			Form_Main = new dForm("NewGame", new Rectangle(0, 0, (int)Engine.CurrentGameResolution.X, (int)Engine.CurrentGameResolution.Y), Engine.StarField, null, false, false);
				Form_Main.IsDragable = true;
			
			//Create new game logo
			Label_Logo = new dLabel("NewGame_Logo", new Vector2d(Engine.VirtualScreenResolution.X / 2, 0), null, Form_Main, Engine.Font_Large, "CREATE NEW GAME", Color.White, true, false, false);
				Form_Main.AddControl(Label_Logo);

			//Back button
                Button_Back = new dLabel("NewGame_Back", new Vector2d(50, 50), null, Form_Main, Engine.Font_Large, "BACK", Color.White, false, false, false);
				//Button_Back = new dButton("NewGame_Back", new Vector2d(50, 50), Engine.ButtonsTexture, new Rectangle(2, 221, 116, 41), Form_Main, false, false);
				Button_Back.HoverColor = Color.Purple;
				Button_Back.ReleaseColor = Color.Red;
				Button_Back.PlaySound = true;
				Form_Main.AddControl(Button_Back);
				Button_Back.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Back.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Back.OnMouseRelease += new Engine.Handler(ButtonRelease);
				Button_Back.EmulationKey = Input.Pause;

			//Textbox
			Texture2D TBNT = Engine.CreateTexture(500, 50, 499, 49, new Color(0,24,255), Color.Black);
			Textbox_Name = new dTextbox("NewGame_Name", new Vector2d(Engine.VirtualScreenResolution.X / 2, 200), new Vector2d(TBNT.Width, TBNT.Height), TBNT, Engine.Font_Medium, Form_Main, true, true);
				Form_Main.AddControl(Textbox_Name);
				Textbox_Name.Text = "Universe" + Engine.saveLoad.Saves.Count.ToString();
			
			//Create button
                Button_Create = new dLabel("NewGame_Create", new Vector2d(Engine.VirtualScreenResolution.X / 2, 300), null, Form_Main, Engine.Font_Large, "CREATE", Color.White, true, true, false);
				//Button_Create = new dButton("NewGame_Create", new Vector2d(Engine.VirtualScreenResolution.X / 2, 300), Engine.ButtonsTexture, new Rectangle(2, 353, 162, 41), Form_Main, true, true);
				Button_Create.HoverColor = Color.Purple;
				Button_Create.ReleaseColor = Color.Red;
				Button_Create.PlaySound = true;
				Form_Main.AddControl(Button_Create);
				Button_Create.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Create.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Create.OnMouseRelease += new Engine.Handler(ButtonRelease);
				Button_Create.EmulationKey = Input.Proceed;
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
				case "NewGame_Back":
					Engine.saveLoad.SaveOptions();
					Hide(false);
				break;
				case "NewGame_Create":
				if (Textbox_Name.Text.Length > 0 && !Engine.saveLoad.Saves.ContainsKey(Textbox_Name.Text))//&& Textbox_Name.Text != "options")
				{
					Engine.Reset();

					//Create 9 solar systems
					for (int ii = 0; ii < 9; ii++)
					{
						Galaxy.CreateSolarSystem();
					}

					//Create a 10th system and set it to active
					Galaxy.SetSolarSystem(Galaxy.CreateSolarSystem());

					Galaxy.CreateShip("Serenity_" + Galaxy.Entities.Count.ToString(), Galaxy.CurrentSolarSystem, 128140, 0.0025f, Engine.Ship_Serenity, Vector2d.Zero, 0f, Vector2d.Zero, 0f, true);

					//Hide other menus
					Engine.guiManager.MainMenu.Hide(true);
					Engine.guiManager.LoadGame.Hide(true);
					Engine.guiManager.Options.Hide(true);
					Hide(true);

					//Engine
					Engine.UpdateGame = true;
					Engine.DrawGame = true;
					Engine.IsPaused = false;
					Engine.guiManager.HUD.Show(this, true);

				}
				else
				{
					//Create a messagebox warning the player
					Messagebox box = new Messagebox(this, "A save file with this name already exists.", true);
					box.Button_OK.EmulationKey = Input.Proceed;
					box.Textbox_Message.BlocksParentInteraction = false;
					box.Show(this, false);

					//Reset Create button to original color
					sender.Color = sender.OriginalColor;
				}
				break;
			}
		}




		public override void Show(GUIPage lastform, bool quick)
		{
			base.Show(lastform, quick);

			Textbox_Name.Text = "Universe" + (Engine.saveLoad.Saves.Count + 1).ToString();
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
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			base.Draw(spritebatch);
		}

	}
}
