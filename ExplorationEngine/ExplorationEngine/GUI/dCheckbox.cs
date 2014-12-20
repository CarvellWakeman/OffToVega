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
	public class dCheckbox : dControl
	{
		//Label specific variables
		public SpriteFont Font;
		public string Text;
		public bool Checked = false;
		public Texture2D CheckboxTexture;


		public dCheckbox(string name, Vector2 position, dControl parent, SpriteFont font, string text, Color color, bool isChecked, bool centeredX, bool centeredY)
			: base(name, position, font.MeasureString(text) + new Vector2(46, 0), null, parent, centeredX, centeredY)
		{
			//Label specific variables
			Font = font;
			Text = text;
			Checked = isChecked;

			base.OriginalColor = color;

			CheckboxTexture = Engine.CreateTexture(46, 46, 45, 45, new Color(0, 24, 255), Color.Black);
		}

		public override void Update()
		{
			//Update control
			base.Update();

			//Checking
			if (MouseWasReleasedWithin)
			{
				Checked = !Checked;
			}
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			//Draw control
			base.Draw(spritebatch);

			//Text
			spritebatch.DrawString(Font, Text, position + new Vector2(46,0), OriginalColor * alpha, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer);

			//Checkbox bounds
			spritebatch.Draw(CheckboxTexture, position, Color.White * alpha);

			//Checkmark
			if (Checked)
				spritebatch.Draw(Engine.Checkmark, position, Color.White * alpha);
		}
	}
}
