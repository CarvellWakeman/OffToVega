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
	public class MainMenu : GUIPage
	{
		public dImage Image_Logo;

		public dGroupbox Groupbox_Buttons;

		public dLabel Button_NewGame;
		public dLabel Button_LoadGame;
		public dLabel Button_Options;
		public dLabel Button_Exit;

		public dImage Image_NewGame;
		public dImage Image_LoadGame;
		public dImage Image_Options;
		public dImage Image_Exit;

		public dLabel Label_ZachLerew;


		public MainMenu() : base()
		{
			Engine.Pages.Add(this);

			//Main form
			Form_Main = new dForm("MainMenu", new Rectangle(0, 0, (int)Engine.CurrentGameResolution.X, (int)Engine.CurrentGameResolution.Y), Engine.MainMenuBackground, null, false, false);
				//Form_Main.IsDragable = true;

			//My Name
			Label_ZachLerew = new dLabel("MainMenu_ZachLerew", Vector2.Zero, null, Form_Main, Engine.Font_Small, "By Zach Lerew 2013-2015", Color.White, false, false, true);
				Form_Main.AddControl(Label_ZachLerew);
				Label_ZachLerew.offset = Engine.VirtualScreenResolution - Label_ZachLerew.Font.MeasureString(Label_ZachLerew.Text) - new Vector2(20,0);

			//Logo
			Image_Logo = new dImage("MainMenu_Logo", new Rectangle(0, 0, 0, 0), Engine.MainMenuLogo, Form_Main, false, false);
				Form_Main.AddControl(Image_Logo);

			//Buttons cluster
			Groupbox_Buttons = new dGroupbox("MainMenu_Groupbox", null, new Vector2(50, 400), null, "", Form_Main, false, false);
				Form_Main.AddControl(Groupbox_Buttons);

			//New game button
                Button_NewGame = new dLabel("MainMenu_NewGame", new Vector2d(0, 0), null, Groupbox_Buttons, Engine.Font_Large, "NEW GAME", Color.White, false, false, false);
				//Button_NewGame = new dButton("MainMenu_NewGame", new Vector2(0, 0), Engine.ButtonsTexture, new Rectangle(2, 2, 248, 41), Groupbox_Buttons, false, false);
				Button_NewGame.PressColor = Color.Purple;
				Button_NewGame.ReleaseColor = Color.Red;
				Button_NewGame.PlaySound = true;
				Groupbox_Buttons.AddControl(Button_NewGame);
				Button_NewGame.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_NewGame.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_NewGame.OnMousePress += new Engine.Handler(ButtonPress);
				Button_NewGame.OnMouseRelease += new Engine.Handler(ButtonRelease);

			//Load game button
                Button_LoadGame = new dLabel("MainMenu_LoadGame", new Vector2d(0, 50), null, Groupbox_Buttons, Engine.Font_Large, "LOAD GAME", Color.White, false, false, false);
				//Button_LoadGame = new dButton("MainMenu_LoadGame", new Vector2(0, 50), Engine.ButtonsTexture, new Rectangle(2, 46, 268, 41), Groupbox_Buttons, false, false);
				Button_LoadGame.PressColor = Color.Purple;
				Button_LoadGame.ReleaseColor = Color.Red;
				Button_LoadGame.PlaySound = true;
				Groupbox_Buttons.AddControl(Button_LoadGame);
				Button_LoadGame.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_LoadGame.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_LoadGame.OnMousePress += new Engine.Handler(ButtonPress);
				Button_LoadGame.OnMouseRelease += new Engine.Handler(ButtonRelease);

			//Options button
                Button_Options = new dLabel("MainMenu_Options", new Vector2d(0, 100), null, Groupbox_Buttons, Engine.Font_Large, "OPTIONS", Color.White, false, false, false);
				//Button_Options = new dButton("MainMenu_Options", new Vector2(0, 100), Engine.ButtonsTexture, new Rectangle(2, 132, 199, 41), Groupbox_Buttons, false, false);
				Button_Options.PressColor = Color.Purple;
				Button_Options.ReleaseColor = Color.Red;
				Button_Options.PlaySound = true;
				Groupbox_Buttons.AddControl(Button_Options);
				Button_Options.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Options.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Options.OnMousePress += new Engine.Handler(ButtonPress);
				Button_Options.OnMouseRelease += new Engine.Handler(ButtonRelease);

			//Exit button
                Button_Exit = new dLabel("MainMenu_Exit", new Vector2d(0, 150), null, Groupbox_Buttons, Engine.Font_Large, "EXIT", Color.White, false, false, false);
				//Button_Exit = new dButton("MainMenu_Exit", new Vector2(0, 150), Engine.ButtonsTexture, new Rectangle(2, 176, 80, 39), Groupbox_Buttons, false, false);
				Button_Exit.PressColor = Color.Purple;
				Button_Exit.ReleaseColor = Color.Red;
				Button_Exit.PlaySound = true;
				Groupbox_Buttons.AddControl(Button_Exit);
				Button_Exit.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Exit.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Exit.OnMousePress += new Engine.Handler(ButtonPress);
				Button_Exit.OnMouseRelease += new Engine.Handler(ButtonRelease);

			Image_NewGame = new dImage("MainMenu_NewGame", new Rectangle(-10, 0, (int)(Engine.ButtonUnderlay.Width * 0.1f), (int)(Engine.ButtonUnderlay.Height * 0.1f)), Engine.ButtonUnderlay, Groupbox_Buttons, false, false);
				Groupbox_Buttons.AddControl(Image_NewGame);
			Image_LoadGame = new dImage("MainMenu_LoadGame", new Rectangle(-10, 50, (int)(Engine.ButtonUnderlay.Width * 0.1f), (int)(Engine.ButtonUnderlay.Height * 0.1f)), Engine.ButtonUnderlay, Groupbox_Buttons, false, false);
				Groupbox_Buttons.AddControl(Image_LoadGame);
			Image_Options = new dImage("MainMenu_Options", new Rectangle(-10, 100, (int)(Engine.ButtonUnderlay.Width * 0.1f), (int)(Engine.ButtonUnderlay.Height * 0.1f)), Engine.ButtonUnderlay, Groupbox_Buttons, false, false);
				Groupbox_Buttons.AddControl(Image_Options);
			Image_Exit = new dImage("MainMenu_Exit", new Rectangle(-10, 150, (int)(Engine.ButtonUnderlay.Width * 0.1f), (int)(Engine.ButtonUnderlay.Height * 0.1f)), Engine.ButtonUnderlay, Groupbox_Buttons, false, false);
				Groupbox_Buttons.AddControl(Image_Exit);
			//dForm Frm = GUIManager.CreateForm("test_form", new Rectangle(0, 0, 640, 360), CreateTexture(1, 1, Color.White));
			//GUIManager.SetActiveForm(Frm);
			//Frm.OnMousePress += new GUIManager.Handler(Frm.FormPress);
			//Frm.OnMouseRelease += new GUIManager.Handler(Frm.FormRelease);

			//dButton Btn = GUIManager.CreateButton("test_button", Vector2.Zero, new Vector2(20, 20), CreateTexture(1, 1, Color.Red), Frm);
			//Btn.Offset = new Vector2(Frm.size.X - Btn.size.X, 0);
			//dLabel Lbl = GUIManager.CreateLabel("test_label", new Vector2(1, -15), Btn, Engine.Font_Medium, "x");
			//Btn.OnMouseRelease += new GUIManager.Handler(Btn.CloseForm);
		}


		//Buttons
		public void ButtonEnter(dControl sender)
		{
			sender.textureOffset += new Vector2(50,0);
			sender.Color = sender.HoverColor;
		}
		public void ButtonLeave(dControl sender)
		{
			sender.textureOffset -= new Vector2(50, 0);
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
				case "MainMenu_NewGame":
					Engine.NewGame.Show(this, false);
					break;
				case "MainMenu_LoadGame":
					Engine.LoadGame.Show(this, false);
					break;
				case "MainMenu_Options":
					Engine.Options.Show(this, false);
					break;
				case "MainMenu_Exit":
					Engine.CustomExit();
					break;
			}
		}


		public override void Show(GUIPage lastform, bool quick)
		{
			//Engine.MainMenuMusic.Resume();

			base.Show(lastform, quick);
		}

		public override void Hide(bool quick)
		{
			//Engine.MainMenuMusic.Pause();

			base.Hide(quick);

		}


		public override void Reset()
		{
			base.Reset();
		}


		public override void Update()
		{
			base.Update();

			Engine.saveLoad.ReloadSaveFiles();
			Button_LoadGame.Clickable = Engine.saveLoad.Saves.Count > 0;
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			base.Draw(spritebatch);
		}
		
	}
}
