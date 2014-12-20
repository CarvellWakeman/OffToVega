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
	public class dTextbox : dControl
	{
		//Textbox specific variables
		public string Text = "";
		public string _Text = "";
		public string Prev_Text = "";

		public Color TextColor = Color.White;

		public SpriteFont Font;

		private int CursorAlpha;
		private bool CursorAlphaUp = true;

		public bool Multiline = false;
		public bool ReadOnly = false;

		public dTextbox(string name, Vector2 offset, Texture2D texture, SpriteFont font, dControl parent, bool centeredX, bool centeredY)
			: base(name, offset, new Vector2(texture.Width, texture.Height), texture, parent, centeredX, centeredY)
		{
			Font = font;
		}
		public dTextbox(string name, Vector2 offset, Vector2 size, Texture2D texture, SpriteFont font, dControl parent, bool centeredX, bool centeredY)
			: base(name, offset, size, texture, parent, centeredX, centeredY)
		{
			Font = font;
		}
		public dTextbox(string name, Vector2 offset, Texture2D texture, Rectangle source, SpriteFont font, dControl parent, bool centeredX, bool centeredY)
			: base(name, offset, new Vector2(source.Width, source.Height), texture, parent, centeredX, centeredY)
		{
			Font = font;
			base.source = source;
		}


		public override void Update()
		{
			//Update control
			base.Update();


			//Only do things if the textbox can be written to
			if (ReadOnly == false)
			{
				//Update Cursor
				if (CursorAlphaUp)
					if (CursorAlpha < 255)
						CursorAlpha++;
					else
						CursorAlphaUp = false;
				else
					if (CursorAlpha > 0)
						CursorAlpha--;
					else
						CursorAlphaUp = true;


				//Get user input
				if (Input.ChangedKey)
				{
					CursorAlpha = 500; //Set the cursor's alpha high, so that it takes a moment to start flashing again

					if (Input.FirstKeyPressed == Keys.Back) //If it's the backspace key
					{
						Text = (Text.Length > 0 ? Text.Remove(Text.Length - 1) : Text); //remove the last char
					}
					else if (Input.AcceptedKeys.Contains(Input.LastKeyPressedMod)) //If not
					{
						if (Multiline) //check if this is a multiline textbox
						{
							Text += Input.ConvertKey(Input.LastKeyPressedMod); //If it is, Add that last key that was pressed to our string
						}
						else //If it's not
						{
							if (Font.MeasureString(Text + Input.LastKeyPressedMod).X < source.Width - 5) //check to make sure that the last pressed key + the current string is NOT > the textbox's width
							{
								Text += Input.ConvertKey(Input.LastKeyPressedMod); //If it's not, Add that last key that was pressed to our string
							}
						}
					}
				}

			}
			else
			{
				TextColor = DisabledColor;
			}


			//Update textbox text
			if (Multiline && Text != Prev_Text)
			{
				_Text = parseText(Text);
			}
			else if (!Multiline)
			{
				_Text = Text;
			}

			//Set previous text
			Prev_Text = Text;
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			//Draw control
			base.Draw(spritebatch);

			//Draw textbox text
			spritebatch.DrawString(Font, _Text, position + new Vector2(4, 5), TextColor * alpha);

			//Draw cursor
			if (ReadOnly == false)
				spritebatch.Draw(Engine.Square, new Rectangle((int)(position.X + Font.MeasureString(Text).X + 3), (int)(position.Y + 2), 1, (int)(Font.MeasureString(Text).Y - 1)), new Color(CursorAlpha, CursorAlpha, CursorAlpha) * alpha);
		
		}

		private String parseText(String text)
		{
			//String line = String.Empty;
			String returnString = String.Empty;
			//char[] letterArray = text.ToCharArray();
			string[] wordArray = text.Split(' ');

			foreach (string word in wordArray)
			{
				//If the single word itself is greater than the window width
				if (Font.MeasureString(word).X > source.Width)
				{
					//Loop through every leter in the long word
					foreach (char letter in word.ToCharArray())
					{
						//If the current parsed string + the next letter + a hyphen is bigger than the sourch width
						if (Font.MeasureString(returnString + letter).X > source.Width)
						{
							//Add a hyphen and a new line
							returnString += '\n';
						}
						//Then add the next letter
						returnString += letter;
					}

					//add a whitespace at the end of this long word before the next one
					returnString += ' ';

				}
				else //else,
				{
					//if the current parsed string + the next word + ' ' is greater than the source width
					if (Font.MeasureString(returnString + word + ' ').X > source.Width)
					{
						//add a new line
						returnString += '\n';
					}
					//then add the word and a whitespace
					returnString += word + ' ';
				}

				
				//if (Font.MeasureString(line + letter).Length() > source.Width)
				//{
				//	returnString += line + '\n';
				//	line = String.Empty;
				//}

				//line += letter;
			}

			return returnString;

			//String line = String.Empty;
			//String returnString = String.Empty;
			//char[] letterArray = text.ToCharArray();

			//foreach (char letter in letterArray)
			//{
			//	if (Font.MeasureString(line + letter).Length() > source.Width)
			//	{
			//		returnString += line + '\n';
			//		line = String.Empty;
			//	}

			//	line += letter;
			//}

			//return returnString + line;
		}
	}
}
