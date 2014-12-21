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
	public class Options : GUIPage
	{
		public dButton Button_Back;

		public dLabel Label_Logo;

		public dGroupbox Groupbox_Debug;

		public dLabel Button_DebugToggle;
		public dLabel Button_GUIDebugToggle;
		public dLabel Button_Apply;

		public dImage Image_DebugToggle;

		public dListbox Listbox_Resolutions;

		public dCheckbox Checkbox_Fullscreen;


		public Options() : base()
		{
			Engine.Pages.Add(this);

			//Main Form
			Form_Main = new dForm("Options", new Rectangle(0, 0, (int)Engine.CurrentGameResolution.X, (int)Engine.CurrentGameResolution.Y), Engine.StarField, null, false, false);
				//Form_Main.IsDragable = true;

			//Form logo
			Label_Logo = new dLabel("Options_Logo", new Vector2d(Engine.VirtualScreenResolution.X / 2, 0), null, Form_Main, Engine.Font_Large, "OPTIONS", Color.White, true, false, false);
				Form_Main.AddControl(Label_Logo);

			//Back button
				Button_Back = new dButton("Options_Back", new Vector2d(50, 50), Engine.ButtonsTexture, new Rectangle(2, 221, 116, 41), Form_Main, false, false);
				Button_Back.HoverColor = Color.Purple;
				Button_Back.PressColor = Color.Red;
				Button_Back.PlaySound = true;
				Form_Main.AddControl(Button_Back);
				Button_Back.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Back.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Back.OnMousePress += new Engine.Handler(ButtonPress);
				Button_Back.OnMouseRelease += new Engine.Handler(ButtonRelease);
				Button_Back.EmulationKey = Input.Pause;

			//Debug Groupbox
			Groupbox_Debug = new dGroupbox("Options_Debug", Engine.CreateTexture(400, 200, 399, 199, Color.Gray, Color.Transparent), new Vector2d(100, 150), Engine.Font_Medium, "Debug", Form_Main, false, false);
				Form_Main.AddControl(Groupbox_Debug);

			//Debug Button
				Button_DebugToggle = new dLabel("Options_DebugMode", new Vector2d(0, 0), null, Groupbox_Debug, Engine.Font_Medium, "Debug Mode:" + Engine.DebugState.ToString(), Color.White, false, false, false);
				Button_DebugToggle.HoverColor = Color.Purple;
				Button_DebugToggle.PressColor = Color.Red;
				Button_DebugToggle.PlaySound = true;
				Groupbox_Debug.AddControl(Button_DebugToggle);
				Button_DebugToggle.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_DebugToggle.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_DebugToggle.OnMousePress += new Engine.Handler(ButtonPress);
				Button_DebugToggle.OnMouseRelease += new Engine.Handler(ButtonRelease);
			//GUI Debug Button
				Button_GUIDebugToggle = new dLabel("Options_GUIDebugMode", new Vector2d(0, 50), null, Groupbox_Debug, Engine.Font_Medium, "GUI Debug Mode:" + Engine.DebugGUI.ToString(), Color.White, false, false, false);
				Button_GUIDebugToggle.HoverColor = Color.Purple;
				Button_GUIDebugToggle.PressColor = Color.Red;
				Button_GUIDebugToggle.PlaySound = true;
				Groupbox_Debug.AddControl(Button_GUIDebugToggle);
				Button_GUIDebugToggle.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_GUIDebugToggle.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_GUIDebugToggle.OnMousePress += new Engine.Handler(ButtonPress);
				Button_GUIDebugToggle.OnMouseRelease += new Engine.Handler(ButtonRelease);
			

			//Debug button hover image
			//Image_DebugToggle = new dImage("Options_DebugMode", new Rectangle(90, 150, (int)(Engine.ButtonUnderlay.Width * 0.1f), (int)(Engine.ButtonUnderlay.Height * 0.1f)), Engine.ButtonUnderlay2, Form_Main);
			//	Form_Main.AddControl(Image_DebugToggle);

			//Resolutions Listbox
			Texture2D TBNT = Engine.CreateTexture(300, 300, 299, 299, new Color(0, 24, 255), Color.Black);
			Listbox_Resolutions = new dListbox("Options_Resolutions", new Vector2d(500, 150), new Vector2d(TBNT.Width, TBNT.Height), TBNT, Engine.Font_Medium, "Resolution", Form_Main, false, false);
				Form_Main.AddControl(Listbox_Resolutions);
				Listbox_Resolutions.PlaySound = true;
				Listbox_Resolutions.EnterSound = null;
				Listbox_Resolutions.LeaveSound = null;
				Listbox_Resolutions.PressSound = null;
				Listbox_Resolutions.ReleaseSound = null;

				//find and filter out duplicate resolutions, then add them to the list.
				List<Vector2> res = new List<Vector2>();
				foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
				{
					if (!res.Contains(new Vector2(dm.Width, dm.Height)) && dm.Width >= Engine.MinimumSupportedResolution.X && dm.Height >= Engine.MinimumSupportedResolution.Y)
					{
						res.Add(new Vector2(dm.Width, dm.Height));
					}
				}
				foreach (Vector2 v in res)
				{
					dLabel lbl = new dLabel(v.X + " X " + v.Y, Listbox_Resolutions.position, null, Listbox_Resolutions, Engine.Font_Medium, v.X + " X " + v.Y, Color.White, false, false, false);
					lbl.size = new Vector2(Listbox_Resolutions.size.X - 3, lbl.Font.MeasureString(lbl.Text).Y);
					Listbox_Resolutions.Items.Add(lbl);

					//Set the selected resolution as the current one.
					if (v.X == Engine.CurrentGameResolution.X && v.Y == Engine.CurrentGameResolution.Y)
					{
						Listbox_Resolutions.Selected = lbl;
					}
				}

			//Apply button
				Button_Apply = new dLabel("Options_Apply", new Vector2d(0, Listbox_Resolutions.size.Y + 5), null, Listbox_Resolutions, Engine.Font_Medium, "Apply", Color.White, false, false, false);
				Button_Apply.HoverColor = Color.Purple;
				Button_Apply.PressColor = Color.Red;
				Button_Apply.PlaySound = true;
				Listbox_Resolutions.AddControl(Button_Apply);
				//Button_Apply.size = new Vector2d(260, 39);
				Button_Apply.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_Apply.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_Apply.OnMousePress += new Engine.Handler(ButtonPress);
				Button_Apply.OnMouseRelease += new Engine.Handler(ButtonRelease);

			//Checkbox Fullscreen
			Checkbox_Fullscreen = new dCheckbox("Options_Fullscreen", new Vector2d(100, Listbox_Resolutions.size.Y + 5), Listbox_Resolutions, Engine.Font_Medium, "FullScreen", Color.White, Engine.IsFullscreen(), false, false);
				Listbox_Resolutions.AddControl(Checkbox_Fullscreen);
				Checkbox_Fullscreen.PlaySound = true;
				Checkbox_Fullscreen.EnterSound = null;
				
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
			switch (sender.name)
			{
				case "Options_Back":
					Engine.saveLoad.SaveOptions();
					Hide(false);
					break;
				case "Options_DebugMode":
					Engine.DebugState = !Engine.DebugState;
					Button_DebugToggle.Text = "Debug Mode:" + Engine.DebugState.ToString();
					break;
				case "Options_GUIDebugMode":
					Engine.DebugGUI = !Engine.DebugGUI;
					Button_GUIDebugToggle.Text = "GUI Debug Mode:" + Engine.DebugGUI.ToString();
					break;
				case "Options_Apply":
					if (Listbox_Resolutions.Selected != null)
					{
						string res = Listbox_Resolutions.Selected.name;
						string[] split = res.Split(new string[] { " X " }, StringSplitOptions.None);
						int x = Convert.ToInt32(split[0]);
						int y = Convert.ToInt32(split[1]);
						Engine.SetResolution(x, y, Checkbox_Fullscreen.Checked);
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


            //PROBLEM: Listbox_Resolutions.Selected.name != Engine.CurrentGameResolution.X + " X " + Engine.CurrentGameResolution.Y)  <- Returns false after the first time apply is clicked when
            //Coming out of fullscreen.
            //Button_Apply.Clickable = (Engine.IsFullscreen() ?
            //                          Engine.IsFullscreen() != Checkbox_Fullscreen.Checked :
            //                          (Checkbox_Fullscreen.Checked ? true : 
            //                                Listbox_Resolutions.Selected != null && Listbox_Resolutions.Selected.name != Engine.CurrentGameResolution.X + " X " + Engine.CurrentGameResolution.Y));

		}

		public override void Draw(SpriteBatch spritebatch)
		{
			base.Draw(spritebatch);
		}

	}
}
