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
	public class dImage : dControl
	{


		public dImage(string name, Vector2 position, Texture2D texture, dControl parent, bool centeredX, bool centeredY)
			: base(name, position, (texture != null ? new Vector2(texture.Width, texture.Height) : Vector2.Zero), texture, parent, centeredX, centeredY)
		{

		}
		public dImage(string name, Vector2 position, Vector2 size, Texture2D texture, dControl parent, bool centeredX, bool centeredY)
			: base(name, position, size, texture, parent, centeredX, centeredY)
		{

		}
		public dImage(string name, Rectangle positionSize, Texture2D texture, dControl parent, bool centeredX, bool centeredY)
			: base(name, new Vector2(positionSize.X, positionSize.Y), new Vector2(positionSize.Width, positionSize.Height), texture, parent, centeredX, centeredY)
		{

		}
		public dImage(string name, Vector2 position, Texture2D texture, Rectangle source, dControl parent, bool centeredX, bool centeredY)
			: base(name, position, new Vector2(source.Width, source.Height), texture, parent, centeredX, centeredY)
		{
			base.source = source;
		}
		public dImage(string name, Vector2 position, Vector2 size, Texture2D texture, Rectangle source, dControl parent, bool centeredX, bool centeredY)
			: base(name, position, size, texture, parent, centeredX, centeredY)
		{
			base.source = source;
		}

	}
}
