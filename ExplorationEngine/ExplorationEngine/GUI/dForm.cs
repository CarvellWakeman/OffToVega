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
	public class dForm : dControl
	{
		//Form specific variables
		public Vector2 ClickOffset;
		public bool IsFullscreen = true;

		public bool IsBeingDragged;             //Is the form being dragged
		public bool IsDragable = false;         //Can the form be dragged by the user's mouse
		public bool ActiveToWork = true;        //Does the form have to be active in order to be interacted with
		public bool CanFocus = true;            //Can the user focus on the form and make it active?
		//public bool AlwaysOnTop = false;
		//public bool BackgroundForm = false;


		public dForm(string name, Rectangle positionSize, Texture2D texture, dControl parent, bool centeredX, bool centeredY)
			: base(name, new Vector2(positionSize.X, positionSize.Y), new Vector2(positionSize.Width, positionSize.Height), texture, parent, centeredX, centeredY)
		{
			//Set up event handlers
			//OnMousePress += new Handler(Control_Press);
			//OnMouseRelease += new Handler(Control_Release);
		}



		public override void Update()
		{
			//Form dragging
			if (IsDragable)
			{
				if (MouseWasReleasedWithin)
				{
					IsBeingDragged = false;
					ClickOffset = Vector2.Zero;
				}
				if (MouseWasPressedWithin && (ActiveToWork && Engine.guiManager.ActivePage.Form_Main == this || !ActiveToWork))
				{
					bool ChildrenUnder = false;

					//Two layers deep
					for (int ii = 0; ii < children.Count; ii++)
					{
						for (int iii = 0; iii < children[ii].children.Count; iii++)
						{
							if (children[ii].children[iii].BlocksParentInteraction && children[ii].children[iii].positionSize.Contains(new Rectangle((int)Input.MousePosition.X, (int)Input.MousePosition.Y, 1, 1)))
							{
								ChildrenUnder = true;
							}
						}

						if (children[ii].BlocksParentInteraction && children[ii].positionSize.Contains(new Rectangle((int)Input.MousePosition.X, (int)Input.MousePosition.Y, 1, 1)))
						{
							ChildrenUnder = true;
						}
					}

					if (!ChildrenUnder)
					{
						IsBeingDragged = true;
						ClickOffset = position - Input.MousePosition;
					}
				}
			}

			//Form dragging
			if (IsBeingDragged && MouseIsPressedWithin)
			{
				position = ClickOffset + Input.MousePosition;
			}

			//position += new Vector2((Convert.ToInt32(Input.KeyDown(Input.Arrow_Right)) - Convert.ToInt32(Input.KeyDown(Input.Arrow_Left))), (Convert.ToInt32(Input.KeyDown(Input.Arrow_Down)) - Convert.ToInt32(Input.KeyDown(Input.Arrow_Up)))) * (Input.KeyDown(Input.Shift) ? 1f : 0.1f);


			//Update base
			base.Update();
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			//Draw control
			base.Draw(spritebatch);
		}
	}
}
