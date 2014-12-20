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
	public static class Input
	{
		//Keyboard
		public static KeyboardState LocalKeyboardState;
		public static MouseState LocalMouseState;

		private static KeyboardState PrevKeyboardState;
		private static MouseState PrevMouseState;


		//Mouse
		private static bool HasLeftPressed = false;
		private static bool HasLeftReleased = false;
		public static bool MouseLeftReleased = false;
		public static bool MouseLeftPressed = false;

		private static bool HasRightPressed = false;
		private static bool HasRightReleased = false;
		public static bool MouseRightReleased = false;
		public static bool MouseRightPressed = false;

		public static int ScrollValueChange = 0;


		//Key Statuses
		public static SortedList<Keys, bool> Modifiers = new SortedList<Keys, bool>();
		public static SortedList<Keys, bool> KeysDown = new SortedList<Keys, bool>();


		//Accepted keys whitelist
		public static List<Keys> AcceptedKeys = new List<Keys>()
		{
			Keys.A,
			Keys.B,
			Keys.C,
			Keys.D,
			Keys.E,
			Keys.F,
			Keys.G,
			Keys.H,
			Keys.I,
			Keys.J,
			Keys.K,
			Keys.L,
			Keys.M,
			Keys.N,
			Keys.O,
			Keys.P,
			Keys.Q,
			Keys.R,
			Keys.S,
			Keys.T,
			Keys.U,
			Keys.V,
			Keys.W,
			Keys.X,
			Keys.Y,
			Keys.Z,
			Keys.OemComma,
			Keys.OemQuestion,
			Keys.OemQuotes,
			Keys.NumPad0,
			Keys.NumPad1,
			Keys.NumPad2,
			Keys.NumPad3,
			Keys.NumPad4,
			Keys.NumPad5,
			Keys.NumPad6,
			Keys.NumPad7,
			Keys.NumPad8,
			Keys.NumPad9,
			Keys.D0,
			Keys.D1,
			Keys.D2,
			Keys.D3,
			Keys.D4,
			Keys.D5,
			Keys.D6,
			Keys.D7,
			Keys.D8,
			Keys.D9,
			Keys.Space,
			Keys.OemMinus,
			Keys.OemPeriod,
			Keys.OemComma,
			Keys.OemPipe,
			Keys.OemQuestion,
			Keys.OemQuotes,
			Keys.OemSemicolon,
			Keys.OemTilde
		};

		//Key presses
		public static bool KeyIsDown = false;


		public static List<Keys> PressedKeys = new List<Keys>();
		public static List<Keys> ReleasedKeys = new List<Keys>();

		public static Keys FirstKeyPressed = Keys.None;
		public static Keys LastKeyPressed = Keys.None;
		public static Keys LastKeyPressedMod = Keys.None;
		private static int LastNumberOfKeysPressed = 0;
		public static bool ChangedKey;





		//Bindable Keys
		public static Keys Pause = Keys.Escape;
		public static Keys Proceed = Keys.Enter;
		public static Keys Shift = Keys.LeftShift;
		public static Keys Delete = Keys.Delete;

		public static Keys Move_Forward = Keys.W;
		public static Keys Move_Backward = Keys.R;
		public static Keys Move_Left = Keys.A;
		public static Keys Move_Right = Keys.S;

		public static Keys Arrow_Up = Keys.Up;
		public static Keys Arrow_Down = Keys.Down;
		public static Keys Arrow_Left = Keys.Left;
		public static Keys Arrow_Right = Keys.Right;

		public static Keys MapToggle = Keys.M;
		public static Keys InertiaDamper = Keys.X;

		public static Keys Action1 = Keys.N;

		public static List<Keys> BindedKeys = new List<Keys>()
		{
			//MapToggle,
			//Pause,
			//Proceed,
			//Move_Forward,
			//Move_Backward,
			//Move_Left,
			//Move_Right,
			//Arrow_Up,
			//Arrow_Down,
			//Arrow_Left,
			//Arrow_Right,
			//InertiaDamper,
			//Shift
		};

		
		public static void Initialize()
		{
			//Add all keys to keysdown
			//for (int ii = 0; ii < AcceptedKeys.Count; ii++)
			//{
			//	KeysDown.Add(AcceptedKeys[ii],false);
			//}
			
			PressedKeys = Keyboard.GetState().GetPressedKeys().ToList<Keys>();
			//PressedKeys = new List<Keys>()
			//{
			//	Keys.S
			//};
		}

		#region "Keyboard interaction"

		public static char ConvertKey(Keys key)
		{
			char chr;
			bool shift = LocalKeyboardState.IsKeyDown(Keys.LeftShift) || LocalKeyboardState.IsKeyDown(Keys.RightShift);

			//Alphabet keys
			switch (key)
            {
                //Alphabet keys
                case Keys.A: if (shift) { chr = 'A'; } else { chr = 'a'; } break;
                case Keys.B: if (shift) { chr = 'B'; } else { chr = 'b'; } break;
                case Keys.C: if (shift) { chr = 'C'; } else { chr = 'c'; } break;
                case Keys.D: if (shift) { chr = 'D'; } else { chr = 'd'; } break;
                case Keys.E: if (shift) { chr = 'E'; } else { chr = 'e'; } break;
                case Keys.F: if (shift) { chr = 'F'; } else { chr = 'f'; } break;
                case Keys.G: if (shift) { chr = 'G'; } else { chr = 'g'; } break;
                case Keys.H: if (shift) { chr = 'H'; } else { chr = 'h'; } break;
                case Keys.I: if (shift) { chr = 'I'; } else { chr = 'i'; } break;
                case Keys.J: if (shift) { chr = 'J'; } else { chr = 'j'; } break;
                case Keys.K: if (shift) { chr = 'K'; } else { chr = 'k'; } break;
                case Keys.L: if (shift) { chr = 'L'; } else { chr = 'l'; } break;
                case Keys.M: if (shift) { chr = 'M'; } else { chr = 'm'; } break;
                case Keys.N: if (shift) { chr = 'N'; } else { chr = 'n'; } break;
                case Keys.O: if (shift) { chr = 'O'; } else { chr = 'o'; } break;
                case Keys.P: if (shift) { chr = 'P'; } else { chr = 'p'; } break;
                case Keys.Q: if (shift) { chr = 'Q'; } else { chr = 'q'; } break;
                case Keys.R: if (shift) { chr = 'R'; } else { chr = 'r'; } break;
                case Keys.S: if (shift) { chr = 'S'; } else { chr = 's'; } break;
                case Keys.T: if (shift) { chr = 'T'; } else { chr = 't'; } break;
                case Keys.U: if (shift) { chr = 'U'; } else { chr = 'u'; } break;
                case Keys.V: if (shift) { chr = 'V'; } else { chr = 'v'; } break;
                case Keys.W: if (shift) { chr = 'W'; } else { chr = 'w'; } break;
                case Keys.X: if (shift) { chr = 'X'; } else { chr = 'x'; } break;
                case Keys.Y: if (shift) { chr = 'Y'; } else { chr = 'y'; } break;
                case Keys.Z: if (shift) { chr = 'Z'; } else { chr = 'z'; } break;
 
                //Decimal Keys
				case Keys.D0: if (shift) { chr = ')'; } else { chr = '0'; } break;
				case Keys.D1: if (shift) { chr = '!'; } else { chr = '1'; } break;
                case Keys.D2: if (shift) { chr = '@'; } else { chr = '2'; } break;
                case Keys.D3: if (shift) { chr = '#'; } else { chr = '3'; } break;
                case Keys.D4: if (shift) { chr = '$'; } else { chr = '4'; } break;
                case Keys.D5: if (shift) { chr = '%'; } else { chr = '5'; } break;
                case Keys.D6: if (shift) { chr = '^'; } else { chr = '6'; } break;
                case Keys.D7: if (shift) { chr = '&'; } else { chr = '7'; } break;
                case Keys.D8: if (shift) { chr = '*'; } else { chr = '8'; } break;
                case Keys.D9: if (shift) { chr = '('; } else { chr = '9'; } break;
 
                //Decimal numpad Keys
                case Keys.NumPad0: chr = '0'; break;
                case Keys.NumPad1: chr = '1'; break;
                case Keys.NumPad2: chr = '2'; break;
                case Keys.NumPad3: chr = '3'; break;
                case Keys.NumPad4: chr = '4'; break;
                case Keys.NumPad5: chr = '5'; break;
                case Keys.NumPad6: chr = '6'; break;
                case Keys.NumPad7: chr = '7'; break;
                case Keys.NumPad8: chr = '8'; break;
                case Keys.NumPad9: chr = '9'; break;
                    
                //Special Keys
				//case Keys.OemTilde: if (shift) { chr = '~'; } else { chr = '`'; } break;
				//case Keys.OemSemicolon: if (shift) { chr = ':'; } else { chr = ';'; } break;
				//case Keys.OemQuotes: if (shift) { chr = '"'; } else { chr = '\''; } break;
				//case Keys.OemQuestion: if (shift) { chr = '?'; } else { chr = '/'; } break;
				//case Keys.OemPlus: if (shift) { chr = '+'; } else { chr = '='; } break;
				//case Keys.OemPipe: if (shift) { chr = '|'; } else { chr = '\\'; } break;
				//case Keys.OemPeriod: if (shift) { chr = '>'; } else { chr = '.'; } break;
				//case Keys.OemOpenBrackets: if (shift) { chr = '{'; } else { chr = '['; } break;
				//case Keys.OemCloseBrackets: if (shift) { chr = '}'; } else { chr = ']'; } break;
				case Keys.OemMinus: if (shift) { chr = '_'; } else { chr = '-'; } break;
				//case Keys.OemComma: if (shift) { chr = '<'; } else { chr = ','; } break;
                case Keys.Space: chr = ' '; break;

				default: chr = ' '; break;
            }

			return chr;
        }

		
		public static bool KeyReleased(Keys key)
		{
			return ReleasedKeys.Contains(key);
			//if (Keyboard.GetState().IsKeyUp(key) && PrevKeyboardState.IsKeyDown(key))
			//{
			//	System.Windows.Forms.MessageBox.Show(key.ToString() + " released");
			//	return true;
			//}
			//else
			//{
			//	return false;
			//}

			//if (KeyDown(key) && !KeysDown.ContainsKey(key))
			//{
			//	KeysDown.Add(key, true);
			//	return false;
			//}
			//else if (KeyUp(key) && KeysDown.ContainsKey(key))
			//{
			//	KeysDown.Remove(key);
			//	return true;
			//}
			//else
			//{
			//	return false;
			//}


			//if (!KeyIsDown && KeyDown(key))
			//{
			//	KeyIsDown = true;
			//}

			//if (KeyIsDown && KeyUp(key))
			//{
			//	KeyIsDown = false;
			//	return true;
			//}
			//else
			//{
			//	return false;
			//}
		}
		 

		//bool isKeyDown = false;
		//public void KeyReleased(object sender, System.Windows.Forms.KeyEventArgs e)
		//{
		//	if (isKeyDown)
		//		return;
		//	isKeyDown = true;
		//	// do what you want to do
		//}

		//public void control1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		//{
		//	isKeyDown = false;
		//	// do you key up event, if any. 
		//}

		public static bool KeyDown(Keys key)
		{
			return LocalKeyboardState.IsKeyDown(key);
		}
		public static int KeyDownInt(Keys key)
		{
			return Convert.ToInt32(LocalKeyboardState.IsKeyDown(key));
		}

		public static bool KeyUp(Keys key)
		{
			return LocalKeyboardState.IsKeyUp(key);
		}
		public static int KeyUpInt(Keys key)
		{
			return Convert.ToInt32(LocalKeyboardState.IsKeyUp(key));
		}

		#endregion


		#region "Mouse interaction"

		public static Vector2 MousePosition
		{
			get
			{
				return new Vector2(LocalMouseState.X, LocalMouseState.Y);
			}
			private set {}
		}

		//static bool MBC = false;
		//public static bool MousePressed(ButtonState MouseButton)
		//{
		//	if (MouseButton == ButtonState.Released && !MBC)
		//	{
		//		MBC = true;
		//		return false;
		//	}
		//	else if (MouseButton == ButtonState.Pressed && MBC)
		//	{
		//		MBC = false;
		//		return true;
		//	}
		//	else
		//	{
		//		return false;
		//	}
		//}
		//static bool MBC2 = false;
		//public static bool MouseReleased(ButtonState MouseButton)
		//{
		//	if (MouseButton == ButtonState.Pressed && !MBC2)
		//	{
		//		MBC2 = true;
		//		return false;
		//	}
		//	else if (MouseButton == ButtonState.Released && MBC2)
		//	{
		//		MBC2 = false;
		//		return true;
		//	}
		//	else
		//	{
		//		return false;
		//	}
		//}

		//static bool HasLeftClicked = false;
		//public static bool MouseReleasedLeft()
		//{
		//	if (!HasLeftClicked && LocalMouseState.LeftButton == ButtonState.Pressed)
		//	{
		//		HasLeftClicked = true;
		//	}
		//	if (HasLeftClicked && LocalMouseState.LeftButton == ButtonState.Released)
		//	{
		//		HasLeftClicked = false;
		//		return true;
		//	}
		//	else
		//	{
		//		return false;
		//	}

		//}
		public static bool MouseState(ButtonState MouseButton, ButtonState State)
		{
			return MouseButton == State;
		}

		//static bool HasClicked = false;
		//public static bool MouseReleased(ButtonState MouseButton)
		//{
		//	if (!HasClicked && MouseButton == ButtonState.Pressed)
		//	{
		//		HasClicked = true;
		//	}
		//	if (HasClicked && MouseButton == ButtonState.Released)
		//	{
		//		HasClicked = false;
		//		return true;
		//	}
		//	else
		//	{
		//		return false;
		//	}

		//}

		#endregion


		public static void Update()
		{
			LocalKeyboardState = Keyboard.GetState();
			LocalMouseState = Mouse.GetState();
			

			//Scrolling
			ScrollValueChange = (LocalMouseState.ScrollWheelValue - PrevMouseState.ScrollWheelValue) / 120;


			//Mouse checks
				//Right Pressed
				if (!HasRightPressed && LocalMouseState.RightButton == ButtonState.Pressed)
				{
					HasRightPressed = true;
					MouseRightPressed = true;
				}
				else
				{
					MouseRightPressed = false;
				}
				if (HasRightPressed && LocalMouseState.RightButton == ButtonState.Released)
				{
					HasRightPressed = false;
				}
				//Right Released
				if (!HasRightReleased && LocalMouseState.RightButton == ButtonState.Released)
				{
					HasRightReleased = true;
					MouseRightReleased = true;
				}
				else
				{
					MouseRightReleased = false;
				}
				if (HasRightReleased && LocalMouseState.RightButton == ButtonState.Pressed)
				{
					HasRightReleased = false;
				}

				//Left Pressed
				if (!HasLeftPressed && LocalMouseState.LeftButton == ButtonState.Pressed)
				{
					HasLeftPressed = true;
					MouseLeftPressed = true;
				}
				else
				{
					MouseLeftPressed = false;
				}
				if (HasLeftPressed && LocalMouseState.LeftButton == ButtonState.Released)
				{
					HasLeftPressed = false;
				}
				//Left Released
				if (!HasLeftReleased && LocalMouseState.LeftButton == ButtonState.Released)
				{
					HasLeftReleased = true;
					MouseLeftReleased = true;
				}
				else
				{
					MouseLeftReleased = false;
				}
				if (HasLeftReleased && LocalMouseState.LeftButton == ButtonState.Pressed)
				{
					HasLeftReleased = false;
				}



			//Set Pressed keys
			PressedKeys = LocalKeyboardState.GetPressedKeys().ToList<Keys>();
			ReleasedKeys.Clear();


			
			for (int ii = 0; ii < PrevKeyboardState.GetPressedKeys().Length; ii++)
			{
				//Add these old keys to the key released list. IE: Get released keys
				if (!PressedKeys.Contains(PrevKeyboardState.GetPressedKeys()[ii]))
					ReleasedKeys.Add(PrevKeyboardState.GetPressedKeys()[ii]);

				//Remove all keys from the current state that were pressed in the previous state. IE: Get the newly pressed keys.
				PressedKeys.Remove(PrevKeyboardState.GetPressedKeys()[ii]);
			}

			//Calculate last pressed keys and stuff
			ChangedKey = (PressedKeys.Count > LastNumberOfKeysPressed ? true : false);
			LastNumberOfKeysPressed = PressedKeys.Count;
			FirstKeyPressed = (ChangedKey ? PressedKeys[0] : Keys.None);
			LastKeyPressed = (ChangedKey ? PressedKeys[LastNumberOfKeysPressed - 1] : Keys.None);
			LastKeyPressedMod = (ChangedKey ? (FirstKeyPressed == Keys.LeftShift ? (PressedKeys.Count >= 2 ? PressedKeys[LastNumberOfKeysPressed - 1] : Keys.None) : LastKeyPressed) : Keys.None);

			//Key released
			for (int ii = 0; ii < BindedKeys.Count; ii++)
			{
				Keys key = BindedKeys[ii];

				if (KeyDown(key) && !KeysDown.ContainsKey(key))
				{
					KeysDown.Add(key, true);
				}
				else if (KeyUp(key) && KeysDown.ContainsKey(key))
				{
					KeysDown.Remove(key);
				}
			}

			PrevKeyboardState = LocalKeyboardState;
			PrevMouseState = LocalMouseState;


		}

	}
}
