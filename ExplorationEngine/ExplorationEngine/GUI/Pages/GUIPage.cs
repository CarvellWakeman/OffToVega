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
	public abstract class GUIPage
	{
		public bool Visible;

		public bool UserInteract = true;

		public GUIPage LastForm = null;

		public dForm Form_Main = new dForm("Form", new Rectangle(0,0,500,300), Engine.CreateTexture(1,1,Color.White),null, false, false);

		public GUIPage()
		{
			
		}


		public virtual void Show(GUIPage lastform, bool quick)
		{
			//Reset the click offset incase the user was holding onto the window since last time
			Form_Main.ClickOffset = Vector2.Zero;

			LastForm = lastform;

			Visible = true; // We're visible now!

			//Set the form and its children to active
			Form_Main.SetActive(true, true, quick);
			if (quick)
			{
				Form_Main.alpha = 1;
			}

			//Add me to the list of open pages
			Engine.guiManager.OpenPages.Add(this);

			//Set this page to active since it just opened
			Engine.guiManager.ActivePage = this;
		}
		public virtual void Hide(bool quick)
		{
			//No longer active
			//IsClosing = true;
			Form_Main.SetActive(false, true, quick);
			
			if (quick)
			{
				//Set the last form to active
				if (LastForm != null && LastForm.Visible)
				{
					Engine.guiManager.ActivePage = LastForm;
				}

				Form_Main.alpha = 0;
				Visible = false;
				Engine.guiManager.OpenPages.Remove(this);
			}

		}

		public virtual void Refresh()
		{
			if (Form_Main.IsFullscreen)
			{
				Form_Main.size = Engine.CurrentGameResolution;
			}
		}

		public virtual void Reset()
		{
			
		}
		

		public virtual void Update()
		{
			if (Form_Main.IsFullscreen) { Form_Main.size = Engine.CurrentGameResolution; }

			if (Form_Main != null)
			{
				Form_Main.Update();


				//Can we interact?
				if (this.Form_Main.ActiveToWork)
				{
					if (Engine.guiManager.ActivePage == this && this.UserInteract == false)
					{
						Form_Main.SetUserInteract(true, true);
					}
					else if (Engine.guiManager.ActivePage != this && this.UserInteract == true)
					{
						Form_Main.SetUserInteract(false, true);
					}
				}


				//Remove from open pages when alpha is 0
				if (Form_Main.alpha <= 0 && Engine.guiManager.OpenPages.Contains(this))
				{
					//Set the last form to active
					if (LastForm != null && LastForm.Visible)
					{
						Engine.guiManager.ActivePage = LastForm;
					}

					Visible = false; //*Poof* gone

					//We're closed for business
					Engine.guiManager.OpenPages.Remove(this);
				}
			}
		}

		public virtual void Draw(SpriteBatch spritebatch)
		{
			if (Form_Main != null)
			{
				if (Visible)
				{
					Form_Main.Draw(spritebatch);
				}
			}
		}

	}
}
