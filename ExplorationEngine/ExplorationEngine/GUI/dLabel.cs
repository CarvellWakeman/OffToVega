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
	public class dLabel : dControl
	{
		//Label specific variables
		public SpriteFont Font;
		public string Text;

		public Vector2 fontOffset = Vector2.Zero;

		public Color TextColor = Color.White;

		public Keys EmulationKey;

		public bool Clickable = true;
		public bool updateSize = false;
		public bool CenterText = false;


		//public dLabel(string name, Vector2 position, dControl parent, SpriteFont font, string text, Color color, bool centeredX, bool centeredY, bool updateSize)
		//	: base(name, position, font.MeasureString(text), null, parent, centeredX, centeredY)
		//{
		//	//Label specific variables
		//	Font = font;
		//	Text = text;

		//	this.updateSize = updateSize;


		//	base.OriginalColor = color;
		//	base.Color = color;
		//}
		public dLabel(string name, Vector2 position, Texture2D texture, dControl parent, SpriteFont font, string text, Color color, bool centeredX, bool centeredY, bool updateSize)
			: base(name, position, (texture != null ? new Vector2(texture.Width, texture.Height) : (font != null ? new Vector2(font.MeasureString(text).X, font.MeasureString(text).Y/1.2f) : Vector2.Zero)), texture, parent, centeredX, centeredY)
		{
			//Label specific variables
			Font = font;
			Text = text;

			this.updateSize = updateSize;

			if (texture != null)
				TextColor = color;
			else
				base.OriginalColor = color;
				base.Color = color;
		}

		public override void Update()
		{
			//Update control
			base.Update();


			//Emulation key
			if (UserInteract && Active && alpha >= 1)
			{
				if (EmulationKey != Keys.None && Input.KeyReleased(EmulationKey))
				{
					base.Click();
				}
			}

			UserInteract = (parent == null || parent.UserInteract ? Clickable : parent.UserInteract);
			if (!Clickable)
			{
				Color = DisabledColor;
				if (DisabledTexture != null)
					Texture = DisabledTexture;
			}
			else if (Clickable && Color == DisabledColor)
			{
				Color = OriginalColor;
				Texture = OriginalTexture;
			}


			//Update size as we go
			if (updateSize)
				base.size = Font.MeasureString(Text);
	
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			//Draw control
			base.Draw(spritebatch);

			//Text drawing
			if (Font != null && Text != null)
				spritebatch.DrawString(Font, Text, position + _textureOffset + fontOffset - (CenterText ? Font.MeasureString(Text) / 2 : Vector2.Zero), (Texture != null ? TextColor : Color) * alpha, 0f, Vector2.Zero, scale, SpriteEffects.None, layer);
		}
	}
}
