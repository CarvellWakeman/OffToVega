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
	class Old_TextButton
	{
		public string Name;
		public string Text;
		public SpriteFont Font;
		private GUI gui;
		public GUI.ButtonTypes Type;
		public Engine.GameState ActiveGameState;

		private Vector2 OriginalPosition;
		private Vector2 Position;
		private Vector2 Size;
		private Rectangle ClickRectangle;
		private Color COLOR;

		private Vector2 WindowScale;
		private Vector2 WindowScaleOP;
		private Vector2 ScalePosition;

		public bool GO_NumberSaves;
		public bool GreyedOut;

		public string StoredData;

		private Vector2 MaxOut;

		public bool IsHover;
		public bool IsMouseDown;
		public bool IsClicked;

		//Debug
		private Color IntersectRectColor;


		public Old_TextButton(string name, string text, SpriteFont font, GUI.ButtonTypes type, Engine.GameState gamestate, Vector2 position, bool halfpos, bool go_numbersaves, GUI g)
		{
			Name = name;
			gui = g;
			Type = type;
			ActiveGameState = gamestate;
			GO_NumberSaves = go_numbersaves;
			Text = text;
			Font = font;
			Size = font.MeasureString(text);
			Position = position - (halfpos ? new Vector2(Size.X / 2, 0) : Vector2.Zero);
			OriginalPosition = Position;

			COLOR = Color.White;

			MaxOut = new Vector2(50, 0);

			WindowScaleOP = new Vector2((OriginalPosition.X / Resolution.getVirtualSize().X), (OriginalPosition.Y / Resolution.getVirtualSize().Y));
		}


		public void Update()
		{
			//Update things
			WindowScale = new Vector2((Position.X / Resolution.getVirtualSize().X), (Position.Y / Resolution.getVirtualSize().Y));
			ScalePosition = Engine.CurrentScreenResolution * new Vector2((OriginalPosition.X / Resolution.getVirtualSize().X), (OriginalPosition.Y / Resolution.getVirtualSize().Y));
			ClickRectangle = new Rectangle((int)ScalePosition.X, (int)ScalePosition.Y, Math.Max((int)Size.X, 100), (int)Size.Y);


			//If the game is in the right state for us to run
			if (Engine.GetGamestate() == ActiveGameState)
			{
				//Check to make sure we are allowed to press the button
				if (GreyedOut == false)
				{
					IsHover = new Rectangle(Input.LocalMouseState.X, Input.LocalMouseState.Y, 1, 1).Intersects(ClickRectangle);
					if (IsHover == true && GUI.ClickedButton == null)
					{
						if (Input.LocalMouseState.LeftButton == ButtonState.Pressed)
						{
							IsMouseDown = true;
							GUI.ClickedButton = Name;
						}

						//Vector2 minus = Position - OriginalPosition;
						//Position += (minus.X < MaxOut.X ? new Vector2(1, 0) : Vector2.Zero);
					}
					else if (IsHover == false && GUI.ClickedButton != Name)
					{
						//Position += (Position.X > OriginalPosition.X ? new Vector2(-1, 0) : Vector2.Zero);
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
							//Position = OriginalPosition;
							gui.ClickButton(Name, Type);
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
				//spriteBatch.Draw(GUI.ButtonUnderlay, Engine.CurrentScreenResolution * WindowScaleOP - new Vector2(10, 2), new Rectangle(0, 0, GUI.ButtonUnderlay.Width, GUI.ButtonUnderlay.Height), (GreyedOut ? new Color(80, 80, 80) : Color.White), 0f, Vector2.Zero, new Vector2(0.1f, 0.1f), SpriteEffects.None, 1);
				spriteBatch.DrawString(Font, Text, Engine.CurrentScreenResolution * WindowScale, COLOR);

				if (IsHover)
					spriteBatch.Draw(GUI.ButtonUnderlay2, Engine.CurrentScreenResolution * WindowScaleOP - new Vector2(10, 2), new Rectangle(0, 0, GUI.ButtonUnderlay.Width, GUI.ButtonUnderlay.Height), (GreyedOut ? new Color(80, 80, 80) : Color.White), 0f, Vector2.Zero, new Vector2(0.1f, 0.1f), SpriteEffects.None, 1);

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
