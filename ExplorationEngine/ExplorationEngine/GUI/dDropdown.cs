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
	public class dDropdown : dControl
	{
		//ListBox specific variables
		public string Title = "Dropdown";
		public SpriteFont Font = Engine.Font_Medium;
		public Color titleColor = Color.White;

		public Texture2D DropDownTexture;
		public Vector2 DropDownSize = Vector2.Zero;

		public bool DropDownContainsMouse = false;

		public SoundEffectInstance SelectSound = Engine.Sound_Click6.CreateInstance();

		public dLabel Selected = null;

		public int ScrollDownTarget = 0;
		public float ScrollDown = 0f;
		public int MaxScroll = 0;
		public float Spacing = 1.0f;

		public bool DroppedDown = false;

		public int DropDownLength = 0;

		public bool CanSelect = true;
		public bool CanScroll = true;

		public Engine.Handler OnSelectedChange;


		public List<dLabel> Items = new List<dLabel>();

		//Buttons
		public dButton DropDown;


		public dDropdown(string name, Vector2 offset, Vector2 size, int dropDownLength, SpriteFont font, string title, dControl parent, bool centeredX, bool centeredY)
			: base(name, offset, size, Engine.CreateTexture((int)size.X, (int)size.Y, (int)size.X - 1, (int)size.Y - 1, new Color(0, 24, 255), Color.Black), parent, centeredX, centeredY)
		{
			DropDownLength = dropDownLength;
			DropDownTexture = Engine.CreateTexture((int)size.X, dropDownLength, (int)size.X - 1, dropDownLength - 1, new Color(0, 24, 255), Color.Black);
			DropDownSize = new Vector2(DropDownTexture.Width, dropDownLength);

			Font = font;
			Title = title;

			Initialize();
		}
		public dDropdown(string name, Vector2 offset, Texture2D texture, SpriteFont font, string title, dControl parent, bool centeredX, bool centeredY)
			: base(name, offset, new Vector2(texture.Width, texture.Height), texture, parent, centeredX, centeredY)
		{
			Font = font;
			Title = title;			

			Initialize();
		}

		public void Initialize()
		{
			//Override sound
			PressSound = null;
			ReleaseSound = null;

			//DropDown button
			if (Texture != null && OriginalTexture != null)
			{
				DropDown = new dButton("dropdown", Vector2.Zero, Engine.DropDownArrow, this, false, false);
					DropDown.textureOffset = new Vector2(size.X - Engine.DropDownArrow.Width * 1.5f, size.Y / 2 - Engine.DropDownArrow.Height / 2);
					//DropDown.interactPositionOffset = -DropDown.offset;
					DropDown.interactSize = size;

					//this.AddControl(Up);
					DropDown.OnMouseEnter += new Engine.Handler(ButtonEnter);
					DropDown.OnMouseLeave += new Engine.Handler(ButtonLeave);
					DropDown.OnMouseRelease += new Engine.Handler(ButtonRelease);
					DropDown.PlaySound = true;
					DropDown.EnterSound = null;
					DropDown.LeaveSound = null;
					DropDown.PressSound = null;
					DropDown.PressSound = Engine.Sound_Click2.CreateInstance();
					DropDown.ReleaseSound = Engine.Sound_Click3.CreateInstance();


				EnterSound = Engine.Sound_Click4.CreateInstance();

			}

		}


		//Buttons
		public void ButtonEnter(dControl sender)
		{
			sender.Color = Color.Blue;
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
			sender.Color = Color.Purple;

			switch (sender.name)
			{
				case "dropdown":
					DroppedDown = !DroppedDown;
					break;
			}
		}


		//Items
		public void AddItem(dLabel item)
		{
			item.interactSize = new Vector2(size.X, item.size.Y);
			Items.Add(item);
			item.OnMouseEnter += new Engine.Handler(ButtonEnter);
			item.OnMouseLeave += new Engine.Handler(ButtonLeave);
			item.OnMouseRelease += new Engine.Handler(ButtonRelease);
		}
		public void AddItems(List<dLabel> items)
		{
			for (int ii = 0; ii < items.Count; ii++)
			{
				if (!Items.Contains(items[ii]))
				{
					AddItem(items[ii]);
				}
			}
		}
		public void RemoveItem(dLabel item)
		{
			Items.Remove(item);
		}
		public void RemoveItems(List<dLabel> items)
		{
			for(int ii = 0; ii < items.Count; ii++)
			{
				if (Items.Contains(items[ii]))
				{
					RemoveItem(items[ii]);
				}
			}
		}


		public override void SetActive(bool active, bool setchildren, bool quick)
		{
			base.SetActive(active, setchildren, quick);

			if (DropDown != null)
				DropDown.SetActive(active, setchildren, quick);

			for (int ii = 0; ii < Items.Count; ii++)
			{
				Items[ii].SetActive(active, setchildren, quick);
			}

		}

		public override void SetUserInteract(bool interact, bool setchildren)
		{
			base.SetUserInteract(interact, setchildren);

			if (setchildren)
			{
				for (int ii = 0; ii < Items.Count; ii++)
				{
					//Set interactivity
					Items[ii].SetUserInteract(interact, setchildren);
				}
			}
		}

		public dControl ControlLookup(string name)
		{
			dControl control = null;

			for (int ii = 0; ii < Items.Count; ii++)
			{
				if (Items[ii].name == name)
				{
					control = Items[ii];
				}
			}

			return control;
		}

		public override void Update()
		{
			//Update control
			base.Update();

			//Update interact size
			interactSize = new Vector2(size.X, (DroppedDown ? DropDownSize.Y : size.Y));

			//Select an item if none are selected
			if (Selected == null && Items.Count > 0)
			{
				Selected = Items[0];
			}

			//Special ContainsMouse for dropdown menu
			DropDownContainsMouse = new Rectangle((int)position.X, (int)position.Y + (int)size.Y, (int)size.X, (int)size.Y +(DroppedDown ? (int)DropDownSize.Y : 0)).Contains(new Rectangle(Input.LocalMouseState.X, Input.LocalMouseState.Y, 1, 1));


			DebugString = string.Empty;
			if (DropDown != null)
				DropDown.DebugString = string.Empty;

			if (DroppedDown)
			{
				//Update scrolling
				if (CanScroll)
				{
					ScrollDownTarget += -Input.ScrollValueChange * 50 * (DropDownContainsMouse ? 1 : 0);
					if (Input.ScrollValueChange != 0 && ScrollDownTarget < MaxScroll - (DropDownTexture != null ? DropDownTexture.Height : 0) && ScrollDownTarget > 0)
					{
						EnterSound.Play();
					}
				}
				ScrollDownTarget = Math.Max(Math.Min(ScrollDownTarget, MaxScroll - (DropDownTexture != null ? DropDownTexture.Height : 0)), 0); //this limit isn't quite right.

				ScrollDown += (ScrollDown < ScrollDownTarget - 1 ? 2f : (ScrollDown > ScrollDownTarget + 1 ? -2f : 0f));
				//ScrollDown = ScrollDownTarget;


				MaxScroll = 0;
				//Special updating for list objects
				for (int ii = 0; ii < Items.Count; ii++)
				{
					Items[ii].Update();

					MaxScroll += (int)(Items[ii].size.Y * Spacing);

					//Update their positions
					Items[ii].offset = new Vector2(5, size.Y + (Items[ii].size.Y * ii * Spacing) - ScrollDown);

					if (DropDownSize.X > 0 && DropDownSize.Y > 0 && OriginalTexture != null && Texture != null)
					{
						Items[ii].source = new Rectangle(0, 0, 0, 0);

						if (Items[ii].OriginalTexture == null)
						{
							//Update whether they are drawn (they are not if they are outside of listbox's bounds)
							if (Active)
							{
								if (Items[ii].offset.Y > DropDownSize.Y || Items[ii].offset.Y - size.Y < 0)
								{
									Items[ii].SetActive(false, true, true);
								}
								else
								{
									Items[ii].SetActive(true, true, true);
								}
							}
						}
						
					}


				}


				//Selecting items
				if (CanSelect && DropDownContainsMouse && Input.MouseLeftReleased && !DropDown.ContainsMouse && Active && UserInteract)
				{
					for (int ii = 0; ii < Items.Count; ii++)
					{
						if (Items[ii].ContainsMouse)
						{
							Selected = Items[ii];
							Selected.Click();
							DroppedDown = false;

							//Event
							if (OnSelectedChange != null)
							{
								OnSelectedChange(this);
							}

							//Sound
							if (PlaySound && SelectSound != null)
								SelectSound.Play();
						}
					}
				}
			}

			if (DropDown != null)
				DropDown.Update();

		}

		public override void Draw(SpriteBatch spritebatch)
		{
			//Draw control
			base.Draw(spritebatch);


			//Drawing for list items
			if (DroppedDown)
			{
				spritebatch.Draw(DropDownTexture, new Rectangle((int)position.X, (int)position.Y + (int)size.Y, (int)size.X, DropDownLength), Color.White * 0.2f);

				for (int ii = 0; ii < Items.Count; ii++)
				{
					Items[ii].Draw(spritebatch);

					if (Engine.DebugGUI)
					{
						Rectangle intersect = Rectangle.Intersect(Items[ii].positionSize, new Rectangle((int)position.X + 2, (int)position.Y + (int)size.Y + 2, (int)DropDownSize.X - 4, (int)DropDownSize.Y - 4));

						spritebatch.Draw(Engine.Square, intersect, new Color(255, 165, 0, 80));
						spritebatch.DrawString(Engine.Font_MediumSmall, "Intersect:" + intersect.X.ToString() + "," + intersect.Y.ToString() + "," + intersect.Width.ToString() + "," + intersect.Height.ToString(), new Vector2(intersect.X, intersect.Y + intersect.Height), Color.AliceBlue);
					}
				}
			}

			//Draw scroll buttons
			if (DropDown != null)
				DropDown.Draw(spritebatch);


			//Draw selected item
			if (Font != null && Selected != null)
			{
				spritebatch.DrawString(Font, Selected.Text, position + new Vector2(4, 4) + _textureOffset, titleColor * alpha);
			}
			
		}
	}
}
