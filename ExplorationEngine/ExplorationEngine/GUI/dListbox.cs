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
	public class dListbox : dControl
	{
		//ListBox specific variables
		public string Title = "Listbox";
		public SpriteFont Font = Engine.Font_Medium;
		public Color titleColor = Color.White;

		public SoundEffectInstance SelectSound = Engine.Sound_Click6.CreateInstance();

		public dControl Selected = null;
		public Texture2D SelectTexture;

		public int ScrollDownTarget = 0;
		public float ScrollDown = 0f;
		public int MaxScroll = 0;
		public float Spacing = 1.0f;

		public bool CanSelect = true;
		public bool CanScroll = true;

		public Engine.Handler OnSelectedChange;

		public Vector2 PrevResolution;

		public List<dControl> Items = new List<dControl>();

		//Buttons
		public dButton Up;
		public dButton Down;


		public dListbox(string name, Vector2 offset, Vector2 size, Texture2D texture, SpriteFont font, string title, dControl parent, bool centeredX, bool centeredY)
			: base(name, offset, size, texture, parent, centeredX, centeredY)
		{
			Font = font;
			Title = title;

			Initialize();
		}
		public dListbox(string name, Vector2 offset, Texture2D texture, SpriteFont font, string title, dControl parent, bool centeredX, bool centeredY)
			: base(name, offset, new Vector2(texture.Width, texture.Height), texture, parent, centeredX, centeredY)
		{
			Font = font;
			Title = title;			

			Initialize();
		}

		public void Initialize()
		{
			if (size.X	> 0 && size.Y > 0)
			{
				//Select texture
				SelectTexture = Engine.CreateTexture(1, 1, Color.Purple);

			}

			PrevResolution = Engine.CurrentGameResolution;

			//Up and down buttons
			if (Texture != null && OriginalTexture != null)
			{
				Up = new dButton("up", new Vector2(size.X - 17, 1), Engine.CreateTexture(16, 16, new Color(0, 100, 255)), this, false, false);
					//this.AddControl(Up);
					Up.OnMouseEnter += new Engine.Handler(ButtonEnter);
					Up.OnMouseLeave += new Engine.Handler(ButtonLeave);
					Up.OnMouseRelease += new Engine.Handler(ButtonRelease);
					Up.PlaySound = true;
					Up.EnterSound = null;
					Up.LeaveSound = null;
					Up.PressSound = null;
					Up.ReleaseSound = Engine.Sound_Click4.CreateInstance();
				Down = new dButton("down", new Vector2(size.X - 17, size.Y - 17), Engine.CreateTexture(16, 16, new Color(0, 100, 255)), this, false, false);
					//this.AddControl(Down);
					Down.OnMouseEnter += new Engine.Handler(ButtonEnter);
					Down.OnMouseLeave += new Engine.Handler(ButtonLeave);
					Down.OnMouseRelease += new Engine.Handler(ButtonRelease);
					Down.PlaySound = true;
					Down.EnterSound = null;
					Down.LeaveSound = null;
					Down.PressSound = null;
					Down.ReleaseSound = Engine.Sound_Click4.CreateInstance();
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
				case "up":
					ScrollDownTarget -= 50;
					break;
				case "down":
					ScrollDownTarget += 50;
					break;
			}
		}


		//Add and remove items
		public void AddItem(dControl item)
		{
			item.interactSize = new Vector2(size.X, item.size.Y);
			Items.Add(item);
		}
		public void AddItems(List<dControl> items)
		{
			Items.AddRange(items);
		}
		public void RemoveItem(dControl item)
		{
			Items.Remove(item);
		}
		public void RemoveItems(List<dControl> items)
		{
			for (int ii = 0; ii < items.Count; ii++)
			{
				if (Items.Contains(items[ii]))
				{
					Items.Remove(items[ii]);
				}
			}
		}


		public override void SetActive(bool active, bool setchildren, bool quick)
		{
			base.SetActive(active, setchildren, quick);

			if (Up != null)
				Up.SetActive(active, setchildren, quick);
			if (Down != null)
				Down.SetActive(active, setchildren, quick);

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

			DebugString = string.Empty;
			if (Up != null)
				Up.DebugString = string.Empty;
			if (Down != null)
				Down.DebugString = string.Empty;

			//Update scrolling
			if (CanScroll)
			{
				ScrollDownTarget += -Input.ScrollValueChange * 50 * (ContainsMouse ? 1 : 0);
				if (Input.ScrollValueChange != 0 && ScrollDownTarget < MaxScroll - (OriginalTexture != null ? OriginalTexture.Height : 0) && ScrollDownTarget > 0)
				{
					Up.ReleaseSound.Play();
				}
			}
			ScrollDownTarget = Math.Max(Math.Min(ScrollDownTarget, MaxScroll - (OriginalTexture != null ? OriginalTexture.Height : 0)), 0); //this limit isn't quite right.

			//ScrollDown += (ScrollDown < ScrollDownTarget - 1 ? 2f : (ScrollDown > ScrollDownTarget + 1 ? -2f : 0f));
			ScrollDown = ScrollDownTarget;


			MaxScroll = 0;
			//Special updating for list objects
			for (int ii = 0; ii < Items.Count; ii++)
			{
				Items[ii].Update();

				MaxScroll += (int)(Items[ii].size.Y * Spacing);

				//Update their positions
				Items[ii].offset = new Vector2(5, (Items[ii].size.Y * ii * Spacing) - ScrollDown);

				if (size.X > 0 && size.Y > 0 && OriginalTexture != null && Texture != null)
				{
					Rectangle intersect = Rectangle.Intersect(Items[ii].positionSize, new Rectangle((int)position.X + 2, (int)position.Y + 2, (int)size.X - 4, (int)size.Y - 4));

					if (intersect != Rectangle.Empty && Items[ii] != null && Items[ii].OriginalTexture != null && intersect.Width > 0 && intersect.Height > 0)
					{
						float IX = intersect.X;
						float IY = intersect.Y;
						float IW = intersect.Width;
						float IH = intersect.Height;

						float OPX = Items[ii].position.X;
						float OPY = Items[ii].position.Y;
						float OSW = Items[ii].size.X;
						float OSH = Items[ii].size.Y;

						int x = (int)(Items[ii].OriginalTexture.Width * (1 - (IW / OSW))) * (IX > OPX ? 1 : 0);
						int y = (int)(Items[ii].OriginalTexture.Height * (1 - (IH / OSH))) * (IY > OPY ? 1 : 0);
						int width = (int)(Items[ii].OriginalTexture.Width * (IX <= OPX ? (IW / OSW) : 1));
						int height = (int)(Items[ii].OriginalTexture.Height * (IY <= OPY ? (IH / OSH) : 1));


						Items[ii].source = new Rectangle(x, y, width, height);
						Items[ii].position = new Vector2(IX, IY);
						Items[ii]._size = new Vector2((IX <= OPX ? IW : Items[ii].size.X), (IY <= OPY ? IH : Items[ii].size.Y));
					}
					else
					{
						Items[ii].source = new Rectangle(0, 0, 0, 0);

						if (Items[ii].OriginalTexture == null)
						{
							//Update whether they are drawn (they are not if they are outside of listbox's bounds)
							if (Active)
							{
								if (Items[ii].offset.Y + Items[ii].size.Y > size.Y || Items[ii].offset.Y < 0)
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


			}

			//Selection box clipping
			//if (CanSelect && SelectTexture != null && Selected != null)
			//{
			//	SelectImage.Update();

			//	//SelectImage.source = Selected.source;
			//	SelectImage.size = Selected.size;
			//	SelectImage.offset = Selected.offset;

			//	Rectangle intersect = Rectangle.Intersect(SelectImage.positionSize, new Rectangle((int)position.X + 2, (int)position.Y + 2, (int)size.X - 4, (int)size.Y - 4));

			//	if (intersect != Rectangle.Empty && SelectImage != null && SelectImage.OriginalTexture != null && intersect.Width > 0 && intersect.Height > 0)
			//	{
			//		float IX = intersect.X;
			//		float IY = intersect.Y;
			//		float IW = intersect.Width;
			//		float IH = intersect.Height;

			//		float OPX = SelectImage.position.X;
			//		float OPY = SelectImage.position.Y;
			//		float OSW = SelectImage.size.X;
			//		float OSH = SelectImage.size.Y;

			//		int x = (int)(SelectImage.OriginalTexture.Width * (1 - (IW / OSW))) * (IX > OPX ? 1 : 0);
			//		int y = (int)(SelectImage.OriginalTexture.Height * (1 - (IH / OSH))) * (IY > OPY ? 1 : 0);
			//		int width = (int)(SelectImage.OriginalTexture.Width * (IX <= OPX ? (IW / OSW) : 1));
			//		int height = (int)(SelectImage.OriginalTexture.Height * (IY <= OPY ? (IH / OSH) : 1));


			//		//SelectImage.source = new Rectangle(x, y, width, height);
			//		//SelectImage.position = new Vector2(IX, IY);
			//		//SelectImage._size = new Vector2((IX <= OPX ? IW : SelectImage.size.X), (IY <= OPY ? IH : SelectImage.size.Y));
			//	}
			//	//SelectImage.source = Selected.source;
			//	//SelectImage.position = Selected.position;
			//	//SelectImage.size = Selected._size;
				

			//	//Rectangle SelectIntersect = Rectangle.Intersect(SelectImage.positionSize, new Rectangle((int)position.X + 2, (int)position.Y + 2, (int)size.X - 4, (int)size.Y - 4));
			//	//if (SelectIntersect != Rectangle.Empty && SelectIntersect.Width > 0 && SelectIntersect.Height > 0)
			//	//{

			//	//	float IX = SelectIntersect.X;
			//	//	float IY = SelectIntersect.Y;
			//	//	float IW = SelectIntersect.Width;
			//	//	float IH = SelectIntersect.Height;

			//	//	float OPX = SelectImage.position.X;
			//	//	float OPY = SelectImage.position.Y;
			//	//	float OSW = SelectImage.size.X;
			//	//	float OSH = SelectImage.size.Y;

			//	//	int x = (int)(SelectImage.OriginalTexture.Width * (1 - (IW / OSW))) * (IX > OPX ? 1 : 0);
			//	//	int y = (int)(SelectImage.OriginalTexture.Height * (1 - (IH / OSH))) * (IY > OPY ? 1 : 0);
			//	//	int width = (int)(SelectImage.OriginalTexture.Width * (IX <= OPX ? (IW / OSW) : 1));
			//	//	int height = (int)(SelectImage.OriginalTexture.Height * (IY <= OPY ? (IH / OSH) : 1));


			//	//	SelectImage.source = new Rectangle(x, y, width, height);
			//	//	//SelectImage.position = new Vector2(IX, IY);

			//	//	//SelectImage._size = new Vector2((IX <= OPX ? IW : SelectImage.size.X), (IY <= OPY ? IH : SelectImage.size.Y));
			//	//}
			//}

			//Selecting items
			if (CanSelect && MouseWasReleasedWithin && !Up.ContainsMouse && !Down.ContainsMouse && Active && UserInteract)
			{
				for (int ii = 0; ii < Items.Count; ii++)
				{
					if (Items[ii].ContainsMouse)
					{
						if (Items[ii] == Selected)
						{
							Selected = null;
						}
						else
						{
							Selected = Items[ii];
							Selected.Click();
						}
						
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

			if (Up != null)
				Up.Update();
			if (Down != null)
				Down.Update();

			//Update texture if resolution changes
			//if (PrevResolution != Engine.CurrentScreenResolution)
			//{
				//Vector2 Mult = Engine.CurrentScreenResolution / PrevResolution;
				//int X = (int)((texture.Width / PrevResolution.X) * Engine.CurrentScreenResolution.X);
				//int Y = (int)((texture.Height / PrevResolution.Y) * Engine.CurrentScreenResolution.Y);

				//System.Windows.Forms.MessageBox.Show("X:" + X.ToString() + " Y:" + Y.ToString());
				//texture = Engine.CreateTexture(X, Y, X-1, Y-1, new Color(0, 24, 255), Color.Black);
				//size = new Vector2(texture.Width, texture.Height);
				//source = new Rectangle(0,0,texture.Width, texture.Height);

				//PrevResolution = Engine.CurrentScreenResolution;
			//}

		}

		public override void Draw(SpriteBatch spritebatch)
		{
			//Draw control
			base.Draw(spritebatch);

			//Draw selection box
			if (Selected != null && Selected.Active && CanSelect)
			{
				Rectangle intersect = new Rectangle((int)Selected.position.X, (int)Selected.position.Y, (int)size.X - 8, (int)Selected.size.Y);
				//Rectangle.Intersect(Selected.positionSize, new Rectangle((int)position.X + 2, (int)position.Y + 2, (int)size.X - 4, (int)size.Y - 4));
				//SelectImage.Draw(spritebatch);
				spritebatch.Draw(SelectTexture, intersect, Color.White * alpha);
			}

			//Drawing for list items
			for (int ii = 0; ii < Items.Count; ii++)
			{
				Items[ii].Draw(spritebatch);

				if (Engine.DebugGUI)
				{
					Rectangle intersect = Rectangle.Intersect(Items[ii].positionSize, new Rectangle((int)position.X + 2, (int)position.Y + 2, (int)size.X - 4, (int)size.Y - 4));

					spritebatch.Draw(Engine.Square, intersect, new Color(0, 0, 205, 80));
					spritebatch.DrawString(Engine.Font_MediumSmall, "Intersect:" + intersect.X.ToString() + "," + intersect.Y.ToString() + "," + intersect.Width.ToString() + "," + intersect.Height.ToString(), new Vector2(intersect.X, intersect.Y + intersect.Height), Color.AliceBlue);
				}
			}



			//Draw scroll buttons
			if (Up != null)
				Up.Draw(spritebatch);
			if (Down != null)
				Down.Draw(spritebatch);

			//Draw title
			if (Font != null && Title != null)
			{
				spritebatch.DrawString(Font, Title, position + new Vector2(5, -Font.MeasureString(Title).Y + 12) + _textureOffset, titleColor * alpha);
			}
			
		}
	}
}
