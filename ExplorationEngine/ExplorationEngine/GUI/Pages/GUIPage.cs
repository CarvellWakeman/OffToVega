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
			Engine.OpenPages.Add(this);

			//Set this page to active since it just opened
			Engine.ActivePage = this;
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
					Engine.ActivePage = LastForm;
				}

				Form_Main.alpha = 0;
				Visible = false;
				Engine.OpenPages.Remove(this);
			}

		}

		public virtual void Reset()
		{
			Form_Main.size = Engine.CurrentGameResolution;
		}

		public virtual void Update()
		{

			if (Form_Main != null)
			{
				Form_Main.Update();

				//Remove from open pages when alpha is 0
				if (Form_Main.alpha <= 0 && Engine.OpenPages.Contains(this))
				{
					//Set the last form to active
					if (LastForm != null && LastForm.Visible)
					{
						Engine.ActivePage = LastForm;
					}

					Visible = false; //*Poof* gone

					//We're closed for business
					Engine.OpenPages.Remove(this);
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
