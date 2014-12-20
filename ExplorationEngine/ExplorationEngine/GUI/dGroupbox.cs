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
	public class dGroupbox : dControl
	{
		//Groupbox specific variables
		public string Title = "Groupbox";
		public SpriteFont Font = Engine.Font_Medium;
		public Color titleColor = Color.White;


		public dGroupbox(string name, Texture2D texture, Vector2 position, SpriteFont font, string title, dControl parent, bool centeredX, bool centeredY)
			: base(name, position, Vector2.Zero, texture, parent, centeredX, centeredY)
		{
			Font = font;
			Title = title;
		}
		public dGroupbox(string name, Texture2D texture, Vector2 position, Vector2 size, SpriteFont font, string title, dControl parent, bool centeredX, bool centeredY)
			: base(name, position, size, texture, parent, centeredX, centeredY)
		{
			Font = font;
			Title = title;
		}
		public dGroupbox(string name, Texture2D texture, Rectangle positionSize, SpriteFont font, string title, dControl parent, bool centeredX, bool centeredY)
			: base(name, new Vector2(positionSize.X, positionSize.Y), new Vector2(positionSize.Width, positionSize.Height), texture, parent, centeredX, centeredY)
		{
			Font = font;
			Title = title;
		}


		public override void Draw(SpriteBatch spritebatch)
		{
			//Draw control
			base.Draw(spritebatch);

			//Draw title
			if (Font != null && Title != null)
			{
				spritebatch.DrawString(Font, Title, position + new Vector2(5, -35) + _textureOffset, titleColor * alpha);
			}
		}

	}
}
