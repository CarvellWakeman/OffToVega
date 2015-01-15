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
	public class GUIManager
	{
		//GUI Pages
		public MainMenu MainMenu;
		public Options Options;
		public NewGame NewGame;
		public LoadGame LoadGame;
		public PauseScreen PauseScreen;
		public GalaxyMap GalaxyMap;
		//Temporary [DEBUG]
		public LocalMap LocalMap;
		public HUD HUD;
		public Navigation Navigation;
		public Sensors Sensors;
		public ShipBuilding ShipBuilding;

		public List<GUIPage> Pages = new List<GUIPage>();
		public List<GUIPage> OpenPages = new List<GUIPage>();
		public GUIPage ActivePage;

		public GUIManager()
		{

		}

		//Initialize
		public void Initialize()
		{
			//Define pages
			MainMenu = new MainMenu();
			NewGame = new NewGame();
			LoadGame = new LoadGame();
			Options = new Options();
			PauseScreen = new PauseScreen();
			GalaxyMap = new GalaxyMap();

			//Temporary [DEBUG]
			LocalMap = new LocalMap();
			HUD = new HUD();
			Navigation = new Navigation();
			Sensors = new Sensors();
			ShipBuilding = new ShipBuilding();


			//Show the main page
			MainMenu.Show(null, true);
		}


		//Refresh menus
		public void Refresh()
		{
			foreach (GUIPage p in Pages) { p.Refresh(); }
		}

		//Reset menus
		public void Reset()
		{
			foreach (GUIPage p in Pages) { p.Reset(); }
		}


		//Hide menus
		public void Hide(bool quick)
		{
			foreach (GUIPage p in Pages) { p.Hide(quick); }
		}

		//Update
		public void Update()
		{
			//GUI Active Page
			if (Input.MouseLeftPressed) 
			{
				for (int i = OpenPages.Count - 1; i >= 0; i--)
				{
					if (OpenPages[i].Form_Main.ActiveToWork && OpenPages[i].Form_Main.CanFocus && OpenPages[i].Form_Main.ContainsMouse)
					{
						GUIPage p = OpenPages[i];
						OpenPages.Remove(p);
						OpenPages.Add(p);
						ActivePage = p;
						break;
					}
				}

			}

			//Update GUI pages (Should I only update GUI pages inside of "OpenPages"?)
			for (int ii = 0; ii < Pages.Count; ii++)
			{
				Pages[ii].Update();
			}
		}


		//Draw
		public void Draw(SpriteBatch spriteBatch)
		{
			//Draw Open GUI pages
			for (int ii = 0; ii < OpenPages.Count; ii++)
			{
				OpenPages[ii].Draw(spriteBatch);
			}
			//foreach (GUIPage p in OpenPages)
			//{
			//	p.Draw(spriteBatch);
			//}
		}

	}
}
