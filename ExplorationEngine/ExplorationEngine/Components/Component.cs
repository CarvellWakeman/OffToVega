#region "Using Statements"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace ExplorationEngine.Components
{
	public class Component
	{
		public virtual int ID { get; set; }

		public virtual void SelfDestruct()
		{
		}

		public virtual void Update()
		{
		}

		public virtual void Draw(SpriteBatch spritebatch)
		{
		}
	}
}
