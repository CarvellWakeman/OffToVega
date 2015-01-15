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
	public class StarBackground : GUIPage
	{

		public StarBackground() : base()
		{
			//Engine.Pages.Add(this);

			Form_Main = new dForm("Stars", new Rectangle(0, 0, (int)Engine.CurrentGameResolution.X, (int)Engine.CurrentGameResolution.Y), Engine.StarField, null, false, false);
			Form_Main.DrawOnDebug = false;
			Form_Main.ActiveToWork = false;
			Form_Main.CanFocus = false;
			Form_Main.BackgroundForm = true;
		}


		public override void Show(GUIPage lastform, bool quick)
		{
			Visible = true; // We're visible now!

			//Set the form and its children to active
			Form_Main.SetActive(true, true, quick);
			if (quick)
			{
				Form_Main.alpha = 1;
			}
		}

		public override void Hide(bool quick)
		{
			base.Hide(quick);
		}


		public override void Refresh()
		{
			base.Refresh();
			Form_Main.size = Engine.CurrentGameResolution;
		}


		public override void Update()
		{
			base.Update();
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			base.Draw(spritebatch);
		}

	}
}
