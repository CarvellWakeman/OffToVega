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
	public class OptionsSaveFile
	{
		//Data in this save file
        public bool DebugState;
		public bool DebugGUI;

		public Vector2d ScreenResolution;
		public bool FullScreen;


		public OptionsSaveFile()
		{
            DebugState = Engine.DebugState;
			DebugGUI = Engine.DebugGUI;

			ScreenResolution = Engine.CurrentGameResolution;
			FullScreen = Engine.IsFullscreen();
		}
	}
}
