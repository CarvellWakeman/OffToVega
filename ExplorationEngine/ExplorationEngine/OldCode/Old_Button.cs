#region "Using Statements"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace ExplorationEngine
{
	/*
	class Old_Button
	{
		public string Name;
		private GUI gui;
		public GUI.ButtonTypes Type;
		public Engine.GameState ActiveGameState;

		private Texture2D Texture;

		private Vector2 OriginalPosition;
		private Vector2 Position;
		private Vector2 Offset;

		private Rectangle Source;
		private Rectangle ClickRectangle;
		private Color COLOR;

		public Keys EmulationKey;

		private Vector2 WindowScale;

		public bool GO_NumberSaves;
		public bool GreyedOut;

		private Vector2 MaxOut;

		public bool IsHover;
		public bool IsMouseDown;
		public bool IsClicked;
		public bool Centered;

		//Debug
		private Color IntersectRectColor;


		public Old_Button(string name, GUI.ButtonTypes type, Engine.GameState gamestate, Texture2D texture, Rectangle source, Vector2 position, bool centered, bool go_numbersaves, GUI g)
		{
			Name = name;
			gui = g;
			Type = type;
			ActiveGameState = gamestate;
			GO_NumberSaves = go_numbersaves;
			Texture = texture;
			Position = position;
			OriginalPosition = Position;
			Source = source;
			COLOR = Color.White;

			Centered = centered;

			MaxOut = new Vector2(50, 0);

			WindowScale = new Vector2((OriginalPosition.X / Resolution.getVirtualSize().X), (OriginalPosition.Y / Resolution.getVirtualSize().Y));
		}


		public void Update()
		{
			//Update things
			ClickRectangle = new Rectangle((int)(Engine.CurrentScreenResolution.X * WindowScale.X) - (Centered ? Source.Width / 2 : 0), (int)(Engine.CurrentScreenResolution.Y * WindowScale.Y) - (Centered ? Source.Height / 2 : 0), (int)Source.Width, (int)Source.Height);


			//If the game is in the right state for us to run
			if (Engine.GetGamestate() == ActiveGameState)
			{
				//Check to make sure we are allowed to press the button
				if (GreyedOut == false)
				{
					if (Input.KeyReleased(EmulationKey))
					{
						gui.ClickButton("", Type);
					}

					IsHover = new Rectangle(Input.LocalMouseState.X, Input.LocalMouseState.Y, 1, 1).Intersects(ClickRectangle);
					if (IsHover == true && GUI.ClickedButton == null)
					{
						if (Input.LocalMouseState.LeftButton == ButtonState.Pressed)
						{
							IsMouseDown = true;
							GUI.ClickedButton = Name;
						}

						Vector2 minus = (Position + Offset) - OriginalPosition;
						Offset += (minus.X < MaxOut.X ? new Vector2(1, 0) : Vector2.Zero);
					}
					else if (IsHover == false && GUI.ClickedButton != Name)
					{
						Offset += (Position.X + Offset.X > OriginalPosition.X ? new Vector2(-1, 0) : Vector2.Zero);
					}
					//Reset clicked state
					if (Input.LocalMouseState.LeftButton == ButtonState.Released && IsMouseDown && GUI.ClickedButton == Name)
					{
						if (IsHover == true)
						{
							IsClicked = true;
							COLOR = Color.White;
							IsHover = false;
							IsMouseDown = false;
							Offset = Vector2.Zero;
							gui.ClickButton("",Type);
						}
						else
						{
							IsMouseDown = false;
							GUI.ClickedButton = null;
						}
					}

					COLOR = (IsMouseDown ? Color.Red : Color.White);
				}
				else
				{
					COLOR = Color.Gray;
				}
				
				//Debug
				if (Engine.DebugState)
					IntersectRectColor = (IsHover ? new Color(0, 255, 0, 50) : new Color(255, 0, 0, 50));
			}

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			if (Engine.GetGamestate() == ActiveGameState)
			{
				Vector2 ButtonPos = Engine.CurrentScreenResolution * WindowScale - (Centered ? new Vector2(Source.Width / 2, Source.Height / 2) : Vector2.Zero);

				spriteBatch.Draw(GUI.ButtonUnderlay, ButtonPos - new Vector2(10, 2), new Rectangle(0, 0, GUI.ButtonUnderlay.Width, GUI.ButtonUnderlay.Height), (GreyedOut ? new Color(80, 80, 80) : Color.White), 0f, Vector2.Zero, new Vector2(0.1f, 0.1f), SpriteEffects.None, 1);
				spriteBatch.Draw(Texture, ButtonPos + Offset, Source, COLOR);

				if (Engine.DebugState == true)
				{
					spriteBatch.Draw(GUI.Square, ClickRectangle, IntersectRectColor);
				}
			}
			//spriteBatch.DrawString(Engine.BigMainFont, WindowScale.ToString(), new Vector2(40, 0), Color.White);
		} 

	}
	 */
}
