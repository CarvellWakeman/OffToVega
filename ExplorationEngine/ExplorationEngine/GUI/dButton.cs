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
	public class dButton : dControl
	{
		//Button specific variables
		public Keys EmulationKey;
		public bool Clickable = true;


		public dButton(string name, Vector2 offset, Texture2D texture, dControl parent, bool centeredX, bool centeredY)
			: base(name, offset, new Vector2((texture != null ? texture.Width : 0), (texture != null ? texture.Height : 0)), texture, parent, centeredX, centeredY)
		{

		}
		public dButton(string name, Vector2 offset, Vector2 size, Texture2D texture, dControl parent, bool centeredX, bool centeredY)
			: base(name, offset, size, texture, parent, centeredX, centeredY)
		{

		}
		public dButton(string name, Vector2 offset, Texture2D texture, Rectangle source, dControl parent, bool centeredX, bool centeredY)
			: base(name, offset, new Vector2(source.Width, source.Height), texture, parent, centeredX, centeredY)
		{
			base.source = source;
		}


		public override void Update()
		{

			//Emulation key
			if (UserInteract && Active)
			{
				if (EmulationKey != Keys.None && Input.KeyReleased(EmulationKey))
				{
					base.Click();
				}
			}

            UserInteract = Clickable;// (parent == null || parent.UserInteract ? Clickable : parent.UserInteract);
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



			//Update control
			base.Update();
		}

		//public override void Draw(SpriteBatch spritebatch)
		//{
			//Draw control
		//	base.Draw(spritebatch);
		//}
	}
}
