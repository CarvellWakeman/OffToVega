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
	public class dGridbox : dControl
	{
		//Gridbox specific variables
		public string Title = "Gridbox";
		public SpriteFont Font = Engine.Font_Medium;
		public Color titleColor = Color.White;

		public Texture2D GridItemBackdrop = null;

		public int ItemsWide = 0;
		public int ItemsTall = 0;

		public float Spacing = 1.0f;

		public Vector2 ItemSize = Vector2.Zero;

		public List<dGridboxItem> Items = new List<dGridboxItem>(); //I'm not sure yet if I should use a plain dImage and make it the responsibility of the GUI page that holds this control to generate meaning
			//from this image, or if I should create a new dataholder dControl to go in place of dImage, or even add a small data variable to the base dControl or dImage alone.


		
		public dGridbox(string name, Vector2 offset, Vector2 itemSize, int numberItemsWide, int numberItemsTall, int spacing, SpriteFont font, string title, dControl parent, bool centeredX, bool centeredY)
			: base(name, offset, new Vector2((itemSize.X * numberItemsWide) + spacing * (numberItemsWide + 1), (itemSize.Y * numberItemsTall) + spacing * (numberItemsTall + 1)),
			Engine.CreateTexture((int)(itemSize.X * numberItemsWide) + spacing * (numberItemsWide + 1), 
			(int)(itemSize.Y * numberItemsTall) + spacing * (numberItemsTall + 1),
			(int)((itemSize.X * numberItemsWide) + spacing * (numberItemsWide + 1)) - 1,
			(int)((itemSize.Y * numberItemsTall) + spacing * (numberItemsTall + 1)) - 1,
			new Color(0,24,255), Color.Black),
			parent, centeredX, centeredY)
		{
			Spacing = spacing;
			Font = font;
			Title = title;

			ItemsWide = numberItemsWide;
			ItemsTall = numberItemsTall;

			ItemSize = itemSize;

			GridItemBackdrop = Engine.CreateTexture((int)itemSize.X, (int)itemSize.Y, Color.LightGray);


			//AddItem(new dImage("Hull1", Vector2.Zero, Engine.Capasitor, this, false, false), new dImage("bkdrp1", Vector2.Zero, GridItemBackdrop, this, false, false));
			//AddItem(new dImage("Hull2", Vector2.Zero, Engine.Cargo, this, false, false), new dImage("bkdrp2", Vector2.Zero, GridItemBackdrop, this, false, false));
			//AddItem(new dImage("Hull3", Vector2.Zero, Engine.Capasitor, this, false, false), new dImage("bkdrp3", Vector2.Zero, GridItemBackdrop, this, false, false));
			//AddItem(new dImage("Hull4", Vector2.Zero, Engine.Cargo, this, false, false), new dImage("bkdrp4", Vector2.Zero, GridItemBackdrop, this, false, false));
			//AddItem(new dImage("Hull5", Vector2.Zero, Engine.Capasitor, this, false, false), new dImage("bkdrp5", Vector2.Zero, GridItemBackdrop, this, false, false));
			//AddItem(new dImage("Hull6", Vector2.Zero, Engine.Cargo, this, false, false), new dImage("bkdrp6", Vector2.Zero, GridItemBackdrop, this, false, false));
			//AddItem(new dImage("Hull7", Vector2.Zero, Engine.Capasitor, this, false, false), new dImage("bkdrp7", Vector2.Zero, GridItemBackdrop, this, false, false));
			//AddItem(new dImage("Hull8", Vector2.Zero, Engine.Cargo, this, false, false), new dImage("bkdrp8", Vector2.Zero, GridItemBackdrop, this, false, false));
			//AddItem(new dImage("Hull9", Vector2.Zero, Engine.Capasitor, this, false, false), new dImage("bkdrp9", Vector2.Zero, GridItemBackdrop, this, false, false));
			//AddItem(new dImage("Hull10", Vector2.Zero, Engine.Cargo, this, false, false), new dImage("bkdrp10", Vector2.Zero, GridItemBackdrop, this, false, false));
			//AddItem(new dImage("Hull11", Vector2.Zero, Engine.Capasitor, this, false, false), new dImage("bkdrp11", Vector2.Zero, GridItemBackdrop, this, false, false));
			//AddItem(new dImage("Hull12", Vector2.Zero, Engine.Cargo, this, false, false), new dImage("bkdrp12", Vector2.Zero, GridItemBackdrop, this, false, false));
			//AddItem(new dImage("Hull13", Vector2.Zero, Engine.Capasitor, this, false, false), new dImage("bkdrp13", Vector2.Zero, GridItemBackdrop, this, false, false));
			//AddItem(new dImage("Hull14", Vector2.Zero, Engine.Cargo, this, false, false), new dImage("bkdrp14", Vector2.Zero, GridItemBackdrop, this, false, false));
			//AddItem(new dImage("Hull15", Vector2.Zero, Engine.Capasitor, this, false, false), new dImage("bkdrp15", Vector2.Zero, GridItemBackdrop, this, false, false));
		}


		//Items
		public void AddItem(dGridboxItem item)
		{
			AddItem(item.Image, item.Backdrop);
		}
		public void AddItem(dImage image, dImage backdrop)
		{
			//Start the rows at the number of items, and the colum at 0
			int numberInRow = Items.Count;
			int numberInColum = 0;
			//If the row contains the same or less items than the allowed number for that row, subtract a row and add a colum
			while (numberInRow - ItemsWide >= 0) { numberInRow -= ItemsWide; numberInColum++; } 

			//Add the item with appropriate offsets to the spacing and position based on the row and colum it's in.
			Items.Add(new dGridboxItem(image, backdrop, offset + new Vector2(Spacing * (numberInRow + 1), Spacing * (numberInColum + 1)) + new Vector2(ItemSize.X * numberInRow, ItemSize.Y * numberInColum), ItemSize));

			//item.OnMouseEnter += new Engine.Handler(ButtonEnter);
			//item.OnMouseLeave += new Engine.Handler(ButtonLeave);
			//item.OnMouseRelease += new Engine.Handler(ButtonRelease);
		}
		public void AddItems(List<dGridboxItem> items)
		{
			for (int ii = 0; ii < items.Count; ii++)
			{
				if (!Items.Contains(items[ii]))
				{
					AddItem(items[ii]);
				}
			}
		}
		public void RemoveItem(dGridboxItem item)
		{
			Items.Remove(item);
		}
		public void RemoveAllItemsWithTexture(Texture2D texture)
		{
			for (int ii = 0; ii < Items.Count; ii++)
			{
				if (Items[ii].Image.Texture == texture)
				{
					RemoveItem(Items[ii]);
				}
			}
		}
		public void RemoveItems(List<dGridboxItem> items)
		{
			for (int ii = 0; ii < items.Count; ii++)
			{
				if (Items.Contains(items[ii]))
				{
					RemoveItem(items[ii]);
				}
			}
		}


		//Overrides for activity and user interaction due to having custom lists of items that are not children (Items List)
		public override void SetActive(bool active, bool setchildren, bool quick)
		{
			base.SetActive(active, setchildren, quick);

			for (int ii = 0; ii < Items.Count; ii++)
			{
				Items[ii].Image.SetActive(active, setchildren, quick);
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
					Items[ii].Image.SetUserInteract(interact, setchildren);
				}
			}
		}


		public override void Update()
		{
			//Update control
			base.Update();

			//Update items
			for (int ii = 0; ii < Items.Count; ii++)
			{
				Items[ii].Update();
			}


			//Dragging of items out of gridbox
			if (UserInteract)
			{
				if (MouseIsPressedWithin)
				{
					for (int ii = 0; ii < Items.Count; ii++)
					{
						if (Items[ii].Backdrop.ContainsMouse)
						{
							System.Windows.Forms.MessageBox.Show(Items[ii].Image.name);
						}
					}
				}
			}
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			//Draw control
			base.Draw(spritebatch);

			//Drawing for list items
			for (int ii = 0; ii < Items.Count; ii++)
			{
				Items[ii].Draw(spritebatch); //We're doing some custom drawing here because of the necessary scaling and positioning for a grid

				//Debug drawing
				if (Engine.DebugGUI)
				{
					Rectangle intersect = Rectangle.Intersect(Items[ii].Image.positionSize, new Rectangle((int)position.X + 2, (int)position.Y + 2, (int)size.X - 4, (int)size.Y - 4));

					spritebatch.Draw(Engine.Square, intersect, new Color(0, 0, 205, 80));
					spritebatch.DrawString(Engine.Font_MediumSmall, "Intersect:" + intersect.X.ToString() + "," + intersect.Y.ToString() + "," + intersect.Width.ToString() + "," + intersect.Height.ToString(), new Vector2(intersect.X, intersect.Y + intersect.Height), Color.AliceBlue);
				}
			}

			//Draw title
			if (Font != null && Title != null)
			{
				spritebatch.DrawString(Font, Title, position + new Vector2(5, -Font.MeasureString(Title).Y + 12) + _textureOffset, titleColor * alpha);
			}

		}
	}


	public class dGridboxItem
	{
		public dImage Image = null;
		public dImage Backdrop = null;

		public Vector2 Offset = Vector2.Zero;

		public Vector2 Scale = Vector2.Zero;

		public Vector2 CenteredOffset = Vector2.Zero;

		public string Data = string.Empty; //Do Something with this, or whatever.


		public dGridboxItem(dImage image, dImage backdrop, Vector2 offset, Vector2 itemSize)
		{
			Image = image;
			Backdrop = backdrop;
			Offset = offset;
			


			Texture2D Texture = image.Texture;
				Scale = itemSize / new Vector2(Math.Max(Texture.Width, Texture.Height), Math.Max(Texture.Width, Texture.Height));
			Vector2 newSize = new Vector2(Texture.Width, Texture.Height) * Scale;
			float min = Math.Min(newSize.X, newSize.Y);
				CenteredOffset = new Vector2((min == newSize.X ? newSize.X / 2 : 0), (min == newSize.Y ? newSize.Y / 2 : 0));


			//Image.scale = Scale;
			Image.SetActive(true, true, true);
			Image.size = newSize;
			Image.offset = offset + CenteredOffset - Image.parent.offset;

			Backdrop.SetActive(true, true, true);
			//Backdrop.size = newSize;
			Backdrop.offset = offset - Backdrop.parent.offset;

			Data = "herrrooo";
		}

		public void Update()
		{
			Backdrop.Update();
			Image.Update();
		}

		public void Draw(SpriteBatch spritebatch)
		{
			//Draw the backdrop
			Backdrop.Draw(spritebatch);
			//spritebatch.Draw(Backdrop.Texture, Backdrop.offset, Backdrop.Color); 

			//Draw the actual item that's in this grid space
			Image.Draw(spritebatch);
			//spritebatch.Draw(Image.Texture, Offset + CenteredOffset, new Rectangle(0, 0, Image.Texture.Width, Image.Texture.Height), Color.White * Image.alpha, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);
		}

	}
}
