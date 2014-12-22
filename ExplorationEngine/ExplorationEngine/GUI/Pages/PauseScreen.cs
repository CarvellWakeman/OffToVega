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
	public class PauseScreen : GUIPage
	{
		public bool Active = true;


		public dLabel Label_Logo;

		public dGroupbox Groupbox_Buttons;

		public dLabel Button_Resume;
		public dLabel Button_Options;
		public dLabel Button_ExitToMainMenu;

		public dImage Image_Resume;
		public dImage Image_Options;
		public dImage Image_ExitToMainMenu;

		public PauseScreen() : base()
		{
			Engine.Pages.Add(this);

			//Main Form
			Form_Main = new dForm("PauseScreen", new Rectangle(0, 0, (int)Engine.CurrentGameResolution.X, (int)Engine.CurrentGameResolution.Y), Engine.Square, null, false, false);
			Form_Main.OriginalColor = new Color(0, 0, 0, 150);
			Form_Main.Color = new Color(0, 0, 0, 150);
			Form_Main.DrawOnDebug = false;
			//Form_Main.IsDragable = true;
			//Form_Main.ActiveToWork = false;


			//Pause logo
			Label_Logo = new dLabel("PauseScreen_Logo", new Vector2(Engine.VirtualScreenResolution.X / 2, 0), null, Form_Main, Engine.Font_Large, "PAUSE", Color.White, true, false, false);
				Form_Main.AddControl(Label_Logo);

			//Buttons groupbox
			Groupbox_Buttons = new dGroupbox("PauseScreen_Groupbox", null, new Vector2(50, 200), null, "", Form_Main, false, false);
				Form_Main.AddControl(Groupbox_Buttons);

			//Resume button
                Button_Resume = new dLabel("PauseScreen_Resume", new Vector2d(0, 0), null, Groupbox_Buttons, Engine.Font_Large, "RESUME", Color.White, false, false, false);
				//Button_Resume = new dButton("PauseScreen_Resume", new Vector2(0, 0), Engine.ButtonsTexture, new Rectangle(2, 265, 172, 41), Groupbox_Buttons, false, false);
				Button_Resume.PressColor = Color.Purple;
				Button_Resume.ReleaseColor = Color.Red;
				Button_Resume.PlaySound = true;
				Groupbox_Buttons.AddControl(Button_Resume);
				Button_Resume.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Resume.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Resume.OnMousePress += new Engine.Handler(ButtonPress);
				Button_Resume.OnMouseRelease += new Engine.Handler(ButtonRelease);
				//Button_Resume.EmulationKey = Input.Pause;

			//Options button
                Button_Options = new dLabel("PauseScreen_Options", new Vector2d(0, 50), null, Groupbox_Buttons, Engine.Font_Large, "OPTIONS", Color.White, false, false, false);
				//Button_Options = new dButton("PauseScreen_Options", new Vector2(0, 50), Engine.ButtonsTexture, new Rectangle(2, 132, 199, 41), Groupbox_Buttons, false, false);
				Button_Options.PressColor = Color.Purple;
				Button_Options.ReleaseColor = Color.Red;
				Button_Options.PlaySound = true;
				Groupbox_Buttons.AddControl(Button_Options);
				Button_Options.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Options.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Options.OnMousePress += new Engine.Handler(ButtonPress);
				Button_Options.OnMouseRelease += new Engine.Handler(ButtonRelease);

			//ExitToMainMenu button
                Button_ExitToMainMenu = new dLabel("PauseScreen_ExitToMainMenu", new Vector2d(0, 100), null, Groupbox_Buttons, Engine.Font_Large, "EXIT TO MAINMENU", Color.White, false, false, false);
				//Button_ExitToMainMenu = new dButton("PauseScreen_ExitToMainMenu", new Vector2(0, 100), Engine.ButtonsTexture, new Rectangle(2, 309, 409, 41), Groupbox_Buttons, false, false);
				Button_ExitToMainMenu.PressColor = Color.Purple;
				Button_ExitToMainMenu.ReleaseColor = Color.Red;
				Button_ExitToMainMenu.PlaySound = true;
				Groupbox_Buttons.AddControl(Button_ExitToMainMenu);
				Button_ExitToMainMenu.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_ExitToMainMenu.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_ExitToMainMenu.OnMousePress += new Engine.Handler(ButtonPress);
				Button_ExitToMainMenu.OnMouseRelease += new Engine.Handler(ButtonRelease);

			//Button underlays
			Image_Resume = new dImage("PauseScreen_Resume", new Rectangle(-10, 0, (int)(Engine.ButtonUnderlay.Width * 0.1f), (int)(Engine.ButtonUnderlay.Height * 0.1f)), Engine.ButtonUnderlay, Groupbox_Buttons, false, false);
				Groupbox_Buttons.AddControl(Image_Resume);
			Image_Options = new dImage("PauseScreen_Options", new Rectangle(-10, 50, (int)(Engine.ButtonUnderlay.Width * 0.1f), (int)(Engine.ButtonUnderlay.Height * 0.1f)), Engine.ButtonUnderlay, Groupbox_Buttons, false, false);
				Groupbox_Buttons.AddControl(Image_Options);
			Image_ExitToMainMenu = new dImage("PauseScreen_ExitToMainMenu", new Rectangle(-10, 100, (int)(Engine.ButtonUnderlay.Width * 0.1f), (int)(Engine.ButtonUnderlay.Height * 0.1f)), Engine.ButtonUnderlay, Groupbox_Buttons, false, false);
				Groupbox_Buttons.AddControl(Image_ExitToMainMenu);
		}



		//Buttons
		public void ButtonEnter(dControl sender)
		{
			sender.textureOffset += new Vector2(50, 0);
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
				case "PauseScreen_Resume":
					Engine.UpdateGame = true;
					Engine.IsPaused = false;
					Hide(false);
					break;
				case "PauseScreen_Options":
					Engine.Options.Show(this, false);
					break;
				case "PauseScreen_ExitToMainMenu":
					Engine.ExitToMainMenu();
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
			//Active = !Engine.Options.Visible;

			//if (Active)
			//{
				base.Update();
			//}
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			//if (Active)
			//{
			base.Draw(spritebatch);
			//}
		}

	}
}
