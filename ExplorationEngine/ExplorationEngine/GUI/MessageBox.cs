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
	public class Messagebox : GUIPage
	{
		public GUIPage Parent;

		public bool Intrusive = false;

		public dGroupbox Groupbox;

		public dLabel Button_OK;

		public dTextbox Textbox_Message;



		public Messagebox(GUIPage parent, string message, bool intrusive) : base()
		{
			Parent = parent;
			Intrusive = intrusive;

			Engine.guiManager.Pages.Add(this);

			//Main form
			Form_Main = new dForm("MessageBox", new Rectangle(0, 0, 400, 400), Engine.CreateTexture(300, 300, 299, 299, new Color(0, 24, 255), Color.Black), null, false, false);
				Form_Main.IsDragable = true;

			//Groupbox for all elements
			Groupbox = new dGroupbox("MessageBox_Groupbox", null, Vector2d.Zero, null, null, Form_Main, false, false);
				Form_Main.AddControl(Groupbox);

			//OK button
				Button_OK = new dLabel("MessageBox_OK", new Vector2d(Form_Main.size.X / 2, Form_Main.size.Y - 35), Engine.CreateTexture(50, 30, 49, 29, Color.Gray, Color.Black), Groupbox, Engine.Font_MediumSmall, "OK", Color.White, true, false, false);
				Button_OK.HoverColor = Color.Purple;
				Button_OK.PressColor = Color.Red;
				Button_OK.fontOffset = new Vector2d((Button_OK.size.X - Button_OK.Font.MeasureString(Button_OK.Text).X) / 2, 0);
				Groupbox.AddControl(Button_OK);
				Button_OK.OnMouseEnter += new Engine.Handler(ButtonEnter);
				Button_OK.OnMouseLeave += new Engine.Handler(ButtonLeave);
				Button_OK.OnMousePress += new Engine.Handler(ButtonPress);
				Button_OK.OnMouseRelease += new Engine.Handler(ButtonRelease);

			//Textbox that contains the message
			Texture2D TBT = Engine.CreateTexture((int)Form_Main.size.X - 30, (int)Form_Main.size.Y - 80, (int)Form_Main.size.X - 31, (int)Form_Main.size.Y - 81, Color.Gray, Color.Black);
			Textbox_Message = new dTextbox("MessageBox_Textbox", new Vector2d(15, 35), TBT, new Rectangle(0, 0, TBT.Width, TBT.Height), Engine.Font_MediumSmall, Groupbox, false, false);
				Textbox_Message.Text = message;
				Textbox_Message.Multiline = true;
				Textbox_Message.ReadOnly = true;
				Textbox_Message.DisabledColor = Color.Gray;
				Groupbox.AddControl(Textbox_Message);


			//Lastly, move the form to the center of the screen
			Form_Main.position = (Vector2)Engine.CurrentGameResolution / 2 - Form_Main.size / 2;
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
		}
		public void ButtonRelease(dControl sender)
		{
			sender.Color = Color.Red;

			switch (sender.name)
			{
				case "MessageBox_OK":
					Hide(false);
					break;
				case "MainMenu_LoadGame":
					break;
			}
		}


		public override void Show(GUIPage lastform, bool quick)
		{
			base.Show(lastform, quick);
		}

		public override void Hide(bool quick)
		{
			Intrusive = false;
			base.Hide(quick);
		}


		public override void Refresh()
		{
			base.Refresh();
		}


		public override void Update()
		{
			base.Update();

			//Hide me if the person who opened me hides.
			if (Parent.Visible == false)
			{
				Hide(true);
			}

			//Keep us active if we're set to be intrusive
			if (Intrusive)
			{
				if (Engine.guiManager.ActivePage != this)
				{
					Engine.guiManager.ActivePage = this;
				}
			}
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			base.Draw(spritebatch);
		}

	}
}
