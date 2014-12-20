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


namespace ExplorationEngine
{
	public class Animation
	{
		#region "Declarations"

		public Vector2d Location { get; set; }

		private Texture2D Texture;
		private int Rows = 0;
		private int Columns = 0;
		private int SubFrames = 0;
		public int CurrentFrame { get; set; }
		public int TotalFrames { get; private set; }

		private double TimeSinceLastFrame = 0;
		private double MilliSecondsPerFrame = 0;

		private int Width = 0;
		private int Height = 0;
		private int Curr_row = 0;
		private int Curr_column = 0;

		private int SubX = 0;
		private int SubY = 0;
				
		#endregion


		#region "Constructor"

		public Animation(Texture2D texture, int rows, int columns) : this(texture, rows, columns, 100, 0, 0, 0) { }
		public Animation(Texture2D texture, int rows, int columns, int millisecondsperframe) : this(texture, rows, columns, millisecondsperframe, 0, 0, 0) { }
		public Animation(Texture2D texture, int rows, int columns, int millisecondsperframe, int subtractframes) : this(texture, rows, columns, millisecondsperframe, subtractframes, 0, 0) { }
		public Animation(Texture2D texture, int rows, int columns, int millisecondsperframe, int subtractframes, int subx, int suby)
		{
			Texture = texture;
			Rows = rows;
			Columns = columns;

			MilliSecondsPerFrame = millisecondsperframe;

			SubFrames = subtractframes;

			SubX = subx;
			SubY = suby;

			//Initializations
			CurrentFrame = 0;
			TotalFrames = (Rows * Columns) - subtractframes;

			Width = (Texture.Width-SubX) / Columns;
			Height = (Texture.Height-SubY) / Rows;
		}


		#endregion


		#region "Update and Draw"

		public void Update(SpriteBatch spritebatch, GameTime gametime)
		{
			TimeSinceLastFrame += gametime.ElapsedGameTime.TotalMilliseconds;

			if (TimeSinceLastFrame >= MilliSecondsPerFrame)
			{				
				CurrentFrame = (CurrentFrame >= TotalFrames - 1 ? 0 : CurrentFrame + 1); //if the current frame is more than the max, reset, else add 1.
				TimeSinceLastFrame = 0;
			}

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Curr_row = (int)((float)CurrentFrame / (float)Columns);
			Curr_column = CurrentFrame % Columns;


			Rectangle sourceRectangle = new Rectangle(Width * Curr_column, Height * Curr_row, Width, Height);
			Rectangle destinationRectangle = new Rectangle((int)Location.X, (int)Location.Y, Width, Height);

			spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
			
		}

		#endregion

	}
}
