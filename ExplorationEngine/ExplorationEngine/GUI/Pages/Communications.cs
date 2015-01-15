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
	public class Communications : GUIPage
	{
		public dGroupbox Groupbox;

		public dLabel Title;

		public dLabel Button_Close;
		public dLabel Button_Send;

		public dLabel Label_OutgoingMessages;
		public dLabel Label_IncomingMessages;

		public dLabel Label_NoShip;
		public dLabel Label_Damaged;
		//public dLabel Label_EnRoute;

		public dTextbox Textbox_Outgoing;
		public dTextbox Textbox_Incoming;
		public dTextbox Textbox_Send;

		public dListbox Listbox_Warnings;
		public dListbox Listbox_CommSystems;


		private SolarSystem PrevSolarsystem;
		private int PrevSolarsystemEntities;



		public Communications()
			: base()
		{
			Engine.guiManager.Pages.Add(this);

			//Main form
				Form_Main = new dForm("Communications", new Rectangle(0, 0, 0, 0), Engine.CreateTexture(1100, 500, 1099, 499, Color.Yellow, new Color(0,0,0,200)), null, false, false);
				Form_Main.IsDragable = true;
				Form_Main.IsFullscreen = false;

			//Title
				Title = new dLabel("Communications_Title", Vector2.Zero, null, Form_Main, Engine.Font_Large, "Communications", Color.White, false, false, false);
				Title.offset = new Vector2(Form_Main.size.X/2 - Title.Font.MeasureString(Title.Text).X/2, -Title.Font.MeasureString(Title.Text).Y);
				Form_Main.AddControl(Title);

			//Groupbox for all elements
				Groupbox = new dGroupbox("Communications_Groupbox", null, Vector2.Zero, null, null, Form_Main, false, false);
				Form_Main.AddControl(Groupbox);

			//Close button
				Texture2D CloseTexture = Engine.CreateTexture(24, 24, 23, 23, Color.Gray, Color.White);
				Button_Close = new dLabel("Communications_Close", new Vector2(Form_Main.size.X - CloseTexture.Width, 0), CloseTexture, Groupbox, null, "", Color.White, false, false, false);
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

			//Ships with comm systems Listbox
				Texture2D TBNT = Engine.CreateTexture(200, (int)Form_Main.size.Y - 50, 199, (int)Form_Main.size.Y - 51, Color.Yellow, Color.Black);
				Listbox_CommSystems = new dListbox("Communications_Bodies", new Vector2(0, 50), new Vector2(TBNT.Width, TBNT.Height), TBNT, Engine.Font_MediumSmall, "Ships", Groupbox, false, false);
				Groupbox.AddControl(Listbox_CommSystems);


			//Listbox warnings
				Listbox_Warnings = new dListbox("Communications_Warnings", new Vector2(0,0), Vector2.Zero, null, null, null, Groupbox, false, false);
				Listbox_Warnings.CanSelect = false;
				Listbox_Warnings.CanScroll = false;
				Groupbox.AddControl(Listbox_Warnings);

			//NoShip label
				Label_NoShip = new dLabel("Communications_NoShip", Vector2.Zero, null, Listbox_Warnings, Engine.Font_Small, "No Ship Selected", Color.Red, false, false, false);
			//CommSystemDamaged label
				Label_Damaged = new dLabel("Communications_Damaged", Vector2.Zero, null, Listbox_Warnings, Engine.Font_Small, "Communication System Damaged", Color.Red, false, false, false);
			//ShipMoving label
				//Label_EnRoute = new dLabel("Communications_EnRoute", Vector2.Zero, null, Listbox_Warnings, Engine.Font_Small, "Ship Is Moving", Color.Yellow, false, false, false);


			//OutGoing textbox
			Textbox_Outgoing = new dTextbox("Communications_Outgoing", Listbox_CommSystems.offset + new Vector2(Listbox_CommSystems.size.X + 20,0), Engine.CreateTexture(425, 400, 424, 399, Color.Gray, Color.Black), Engine.Font_Small, Groupbox, false, false);
			Textbox_Outgoing.BlocksParentInteraction = false;
			Textbox_Outgoing.Multiline = true;
			Textbox_Outgoing.ReadOnly = true;
			Textbox_Outgoing.DisabledColor = Color.White;
			Groupbox.AddControl(Textbox_Outgoing);

			//Outgoing textbox label
			Label_OutgoingMessages = new dLabel("Communications_OutgoingLabel", Vector2.Zero, null, Form_Main, Engine.Font_MediumSmall, "OutGoing", Color.White, false, false, false);
			Label_OutgoingMessages.offset = Textbox_Outgoing.offset + new Vector2(Label_OutgoingMessages.Font.MeasureString(Label_OutgoingMessages.Text).X / 3, -Label_OutgoingMessages.Font.MeasureString(Label_OutgoingMessages.Text).Y/2);
			Groupbox.AddControl(Label_OutgoingMessages);


			//OutGoing Send textbox
			Textbox_Send = new dTextbox("Communications_SendTextbox", Textbox_Outgoing.offset + new Vector2(0, Textbox_Outgoing.size.Y + 10), Engine.CreateTexture(350, 30, 349, 29, Color.Gray, Color.Black), Engine.Font_Small, Groupbox, false, false);
			Textbox_Send.BlocksParentInteraction = false;
			Groupbox.AddControl(Textbox_Send);

			//Send Button
			Texture2D SendTexture = Engine.CreateTexture(65, 30, 64, 29, Color.Green, Color.DarkGreen);
			Button_Send = new dLabel("Communications_SendButton", Textbox_Send.offset + new Vector2(Textbox_Send.size.X + 10,0), SendTexture, Groupbox, Engine.Font_MediumSmall, "SEND", Color.White, false, false, false);
			Button_Send.fontOffset = (Button_Send.size / 2) - (Button_Send.Font.MeasureString(Button_Send.Text) / 2) + new Vector2(0, 3);
			Button_Send.DisabledTexture = Engine.CreateTexture(SendTexture.Width, SendTexture.Height, SendTexture.Width - 4, SendTexture.Height - 4, Color.DarkGreen, Color.Gray);
			Button_Send.OriginalTexture = SendTexture;
			Button_Send.HoverTexture = Engine.CreateTexture(SendTexture.Width, SendTexture.Height, SendTexture.Width - 4, SendTexture.Height - 4, Color.Green, new Color(40, 220, 40));
			Button_Send.PressTexture = Engine.CreateTexture(SendTexture.Width, SendTexture.Height, SendTexture.Width - 4, SendTexture.Height - 4, Color.Green, Color.GreenYellow);
			Button_Send.ReleaseTexture = Button_Send.OriginalTexture;
			Button_Send.PlaySound = true;
			Groupbox.AddControl(Button_Send);
			Button_Send.OnMouseEnter += new Engine.Handler(ButtonEnter);
			Button_Send.OnMouseLeave += new Engine.Handler(ButtonLeave);
			Button_Send.OnMousePress += new Engine.Handler(ButtonPress);
			Button_Send.OnMouseRelease += new Engine.Handler(ButtonRelease);


			//Incoming textbox
			Textbox_Incoming = new dTextbox("Communications_Incoming", Textbox_Outgoing.offset + new Vector2(Textbox_Outgoing.size.X + 20, 0), Engine.CreateTexture(425, 440, 424, 439, Color.Gray, Color.Black), Engine.Font_Small, Groupbox, false, false);
			Textbox_Incoming.BlocksParentInteraction = false;
			Textbox_Incoming.Multiline = true;
			Textbox_Incoming.ReadOnly = true;
			Textbox_Incoming.DisabledColor = Color.White;
			Groupbox.AddControl(Textbox_Incoming);

			//Outgoing textbox label
			Label_IncomingMessages = new dLabel("Communications_IncomingLabel", Vector2.Zero, null, Form_Main, Engine.Font_MediumSmall, "Incoming", Color.White, false, false, false);
			Label_IncomingMessages.offset = Textbox_Incoming.offset + new Vector2(Label_IncomingMessages.Font.MeasureString(Label_IncomingMessages.Text).X, -Label_IncomingMessages.Font.MeasureString(Label_IncomingMessages.Text).Y / 2);
			Groupbox.AddControl(Label_IncomingMessages);


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
				case "Communications_Close":
					Hide(false);
					break;
				case "Communications_SendButton":
					//Find the target
					BaseEntity Target = Galaxy.EntityLookup(Listbox_CommSystems.Selected.name);

					string SendingSystem = (Engine.camera.TargetObject != null ? Engine.camera.TargetObject.Name : "");
					string ReceivingSystem = Listbox_CommSystems.Selected.name;

					//Show the data as received if our target can receive it
					if (Target.Communication != null)
					{
						//System.Windows.Forms.MessageBox.Show(Textbox_Send.Text);
						Target.Communication.CommunicationsMenu.Textbox_Incoming.Text +=  SendingSystem + ": " + Textbox_Send.Text + "\n";
					}

					//Send our data and clear the input field
					Textbox_Outgoing.Text += SendingSystem + ": " + Textbox_Send.Text + " > " + ReceivingSystem + "\n";
					Textbox_Send.Text = "";
					
					break;
			}
		}


		public override void Show(GUIPage lastform, bool quick)
		{
			base.Show(lastform, quick);

			UpdateCommEntities();
		}

		public override void Hide(bool quick)
		{
			base.Hide(quick);
		}


		public override void Refresh()
		{
			base.Refresh();
		}


		public void UpdateCommEntities()
		{

			//Update comm systems to communicate with
			Listbox_CommSystems.Items.Clear();

			Listbox_CommSystems.Selected = null;
			Listbox_CommSystems.ScrollDownTarget = 0;

			for (int ii = 0; ii < Galaxy.CurrentSolarSystem.Entities.Count; ii++)
			{
				//dLabel lbl = new dLabel(Galaxy.CurrentSolarSystem.Entities[ii], Listbox_SolarBodies.position, Listbox_SolarBodies, Engine.Font_Medium, Galaxy.CurrentSolarSystem.Entities[ii], Color.White, false, false, false);
				BaseEntity ent = Galaxy.CurrentSolarSystem.Entities[ii];
				if (Engine.camera.TargetObject != null && ent != Engine.camera.TargetObject && (ent.Parent!= null ? ent.Parent != Engine.camera.TargetObject : true) && ent.Communication != null) //If we are controlling a ship, and the other entity has a communication system
				{
					dImage img = new dImage(Galaxy.CurrentSolarSystem.Entities[ii].Name, Listbox_CommSystems.position, (ent.Renderer != null ? ent.Renderer._texture : Engine.CreateTexture(1, 1, Color.Red)), Listbox_CommSystems, false, false);
					img.size = new Vector2d(Listbox_CommSystems.size.X - 10, Listbox_CommSystems.size.X - 10);

					dLabel lbl = new dLabel(Galaxy.CurrentSolarSystem.Entities[ii].Name + "_label", img.position, null, img, Engine.Font_Small, Galaxy.CurrentSolarSystem.Entities[ii].Name, Color.White, false, false, false);
					img.AddControl(lbl);
					lbl.SetActive(Listbox_CommSystems.Active, true, false);

					img.SetActive(Listbox_CommSystems.Active, true, false);

					Listbox_CommSystems.Items.Add(img);
				}
			}
		}

		public override void Update()
		{
			base.Update();

			//Can we send the data?
			Button_Send.Clickable = Engine.camera.TargetObject != null && Listbox_CommSystems.Selected != null && Textbox_Send.Text.Length > 0;


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

			//Update solar bodies list
			if (Galaxy.CurrentSolarSystem != null && (Galaxy.CurrentSolarSystem != PrevSolarsystem || Galaxy.CurrentSolarSystem.Entities.Count != PrevSolarsystemEntities))
			{
				UpdateCommEntities();
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
