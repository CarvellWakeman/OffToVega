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


using ExplorationEngine.GUI;

#endregion

namespace ExplorationEngine
{
	public class Engine : Game
	{

		#region "Declarations and Definitions"
		//Graphics
		public static GraphicsDeviceManager GraphicsManager;
		public SpriteBatch spriteBatch;
		public static ContentManager content;
		public static Vector2 CurrentScreenResolution;
		public static Vector2 VirtualScreenResolution = new Vector2(1280, 720);
		
		//Audio

			//Music
			public static SoundEffect Music_Solarity;

			//GUI Audio
			public static SoundEffect Sound_Click1;
			public static SoundEffect Sound_Click2;
			public static SoundEffect Sound_Click3;
			public static SoundEffect Sound_Click4;
			public static SoundEffect Sound_Click5;
			public static SoundEffect Sound_Click6;

			//Ship
			public static SoundEffect Sound_Ping;
			public static SoundEffect Sound_Warp;

		//Audio Instances
			public static SoundEffectInstance MainMenuMusic;

		//GUI Pages
		public static MainMenu MainMenu;
		public static NewGame NewGame;
		public static LoadGame LoadGame;
		public static Options Options;
		public static PauseScreen PauseScreen;
		public static StarBackground StarBackground;
		public static GalaxyMap GalaxyMap;
		public static LocalMap LocalMap;
		public static HUD HUD;
		public static Navigation Navigation;
		public static Sensors Sensors;
		public static ShipBuilding ShipBuilding;

		public static List<GUIPage> Pages = new List<GUIPage>();
		public static List<GUIPage> OpenPages = new List<GUIPage>();
		public static List<GUIPage> ActivePages = new List<GUIPage>();
		public static GUIPage ActivePage;

		//Textures
			//GUI
			public static Texture2D Square;
			public static Texture2D Circle;
			public static Texture2D DropDownArrow;

			public static Texture2D Checkmark;

			public static Texture2D StarField;
			public static Texture2D WarpField;
			public static Texture2D MapBackground;
			public static Texture2D MainMenuBackground;
			
			public static Texture2D MainMenuLogo;
			public static Texture2D ButtonsTexture;
			public static Texture2D ButtonUnderlay;
			public static Texture2D ButtonUnderlay2;
			public static Texture2D ButtonUnderlay3;
			

			//Galaxy
			public static Texture2D Star_YellowMainSequence;
			public static Texture2D Star_WhiteDwarf;
			public static Texture2D Star_RedGiant;
			public static Texture2D Star_BlueGiant;

			public static Texture2D Planet_Debug_Medium;
			public static Texture2D Planet_ClassH_Delvor;
			public static Texture2D Planet_ClassJ_Fash;
			public static Texture2D Planet_ClassM_Etho;
			public static Texture2D Planet_ClassO_Serine;
			public static Texture2D Planet_ClassP_Antasia;
			public static Texture2D Planet_ClassY_Voshnoy;

			//private Texture2D Moon_Dusty_Medium;

			public static Texture2D Shadow;

			public static Texture2D Galaxy_Spiral1;

			//Ships
			public static Texture2D Ship_Serenity;
			public static Texture2D Ship_Debug;
				//Parts
				public static Texture2D Capasitor;
				public static Texture2D Cargo;
				public static Texture2D Chassis1;
				public static Texture2D Chassis2;
				public static Texture2D Generator;
				public static Texture2D LifeSupport;
				public static Texture2D Radiator;
				public static Texture2D Thruster;

			//Particles
			public static Texture2D EmitterTexture;
			public static Texture2D ParticleTexture;

			//Debug
			public static Texture2D ERROR;
			public static Texture2D Debug_MarkerBB;
			public static Texture2D Debug_MarkerBC;
			public static Texture2D Debug_MarkerBG;
			public static Texture2D Debug_MarkerBO;
			public static Texture2D Debug_MarkerBP;
			public static Texture2D Debug_MarkerBR;
			public static Texture2D Debug_MarkerBY;

			//Texture Definitions
			public static Dictionary<TextureIndexes, Texture2D> TextureLookup = new Dictionary<TextureIndexes, Texture2D>();

			public enum TextureIndexes
			{
				ERROR,
				Star_YellowMainSequence,
				Star_WhiteDwarf,
				Star_RedGiant,
				Star_BlueGiant,
				Planet_Debug_Medium,
				Planet_ClassH_Delvor,
				Planet_ClassJ_Fash,
				Planet_ClassM_Etho,
				Planet_ClassO_Serine,
				Planet_ClassP_Antasia,
				Planet_ClassY_Voshnoy,
				Shadow,
				Galaxy_Spiral1,
				Ship_Serenity,
				Ship_Debug,
				Debug_MarkerBB,
				Debug_MarkerBC,
				Debug_MarkerBG,
				Debug_MarkerBO,
				Debug_MarkerBP,
				Debug_MarkerBR,
				Debug_MarkerBY,
				EmitterTexture,
				ParticleTexture,
				Capasitor,
				Cargo,
				Chassis1,
				Chassis2,
				Generator,
				LifeSupport,
				Radiator,
				Thruster
			}



		//Font
		public static SpriteFont Font_VerySmall;
		public static SpriteFont Font_Small;
		public static SpriteFont Font_MediumSmall;
		public static SpriteFont Font_Medium;
		public static SpriteFont Font_Large;

		//FPS
		private int FPS_Frames = 0;
		private float FPS_ElapsedTime = 0.0f;
		private float FPS = 0.0f;

		//Debug
		private List<string> DebugItems;
		private string ActivePagesString = "";
		private string OpenPagesString = "";
		public static bool DebugState = false;
		public static bool DebugGUI = false;

		//Events
		public delegate void Handler(dControl sender);


		//Other
		public static Random Rand = new Random();
		

		//Things I shouldn't have to do, but had to anyways because of Microsoft.
		public static bool MouseVisible { get; set; } //Because I can't set this.IsMouseVisible because "this" is not a static field.
		public static Engine static_this; //Because I can't use this.Exit() in a static method.


		//Instances
			//Animation
			public static Animation animation;
			
			//Save Load
			public static SaveLoad saveLoad;

			//Stellar Classifications
			public static StellarClassifications stellarClassifications;
			
		//Game States
			public static bool IsPaused;
			public static bool DrawGame = false;
			public static bool UpdateGame = false;
		#endregion


		#region "Constructor/Destructor"
		public Engine()
		{
			GraphicsManager = new GraphicsDeviceManager(this);
			content = Content;
			content.RootDirectory = "Content";
		}
		#endregion
		

		#region "Initialization"
		protected override void Initialize()
		{
			//Set window title
			Window.Title = "Off To Vega";

			//Make a static this for access outside of Engine (window.title mostly)
			static_this = this;


			//Cap the framerate. -1 for no limit, 0 for vsync default, int for anything else
			SetFrameRate(GraphicsManager, 300);
			//static_this.IsFixedTimeStep = false;

			//Set Resolution
			SetResolution(1280, 720, false);
			//this.Window.AllowUserResizing = false;
			MouseVisible = true;

			//Assign the X button to the form closing function
			System.Windows.Forms.Form ThisForm = System.Windows.Forms.Form.FromHandle(Window.Handle) as System.Windows.Forms.Form;
			if (ThisForm != null)
				ThisForm.FormClosing += FormClosing;


			//Call ContentLoad()
			base.Initialize();


			//Create the galaxy
			Galaxy.Name = "NGC 1440";
			//galaxy = new Galaxy("NGC 4414");


			//Initialize the Input
			Input.Initialize();

			//Create the GUI
			//gui = new GUI(this.Content);


			//Create save load system
			saveLoad = new SaveLoad();

			//Create stellar classification
			stellarClassifications = new StellarClassifications();
			stellarClassifications.Save();


			//Load options save file
			saveLoad.LoadOptions();


			//GUI creation
				//Define pages
				MainMenu = new MainMenu();
				NewGame = new NewGame();
				LoadGame = new LoadGame();
				Options = new Options();
				PauseScreen = new PauseScreen();
				StarBackground = new StarBackground();
				GalaxyMap = new GalaxyMap();
				LocalMap = new LocalMap();
				HUD = new HUD();
				Navigation = new Navigation();
				Sensors = new Sensors();
				ShipBuilding = new ShipBuilding();

				MainMenuMusic.Pitch = -0.3f;
				MainMenuMusic.IsLooped = true;

				//Show MainMenu
				MainMenu.Show(null, true);
				StarBackground.Show(null, true);
				

				//MainMenuMusic.Play();
				
		}
		#endregion


		#region "Content load/unload"

		protected override void LoadContent()
		{
			//Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);


			//Audio
			MainMenuMusic = Content.Load<SoundEffect>("Audio/Songs/Solarity").CreateInstance();

			Sound_Click1 = Content.Load<SoundEffect>("Audio/GUI/Clicks/Click1");
			Sound_Click2 = Content.Load<SoundEffect>("Audio/GUI/Clicks/Click2");
			Sound_Click3 = Content.Load<SoundEffect>("Audio/GUI/Clicks/Click3");
			Sound_Click4 = Content.Load<SoundEffect>("Audio/GUI/Clicks/Click4");
			Sound_Click5 = Content.Load<SoundEffect>("Audio/GUI/Clicks/Click5");
			Sound_Click6 = Content.Load<SoundEffect>("Audio/GUI/Clicks/Click6");

			Sound_Ping = Content.Load<SoundEffect>("Audio/Environment/Ships/Ping");

			//Animation
			//Texture2D texture = Content.Load<Texture2D>("Samples/spritesheet");
			//animation = new Animation(texture, 8, 8, 80, 4, 32, 32);

			//Font
			Font_VerySmall = Content.Load<SpriteFont>("Fonts/Dolce_VerySmall");
			Font_Small = Content.Load<SpriteFont>("Fonts/Dolce_Small");
			Font_Medium = Content.Load<SpriteFont>("Fonts/Dolce_Medium");
			Font_MediumSmall = Content.Load<SpriteFont>("Fonts/Dolce_MediumSmall");
			Font_Large = Content.Load<SpriteFont>("Fonts/Dolce_Large");


			//GUI
				StarField = Content.Load<Texture2D>("Starfields/StarField_1600X900");
				WarpField = Content.Load<Texture2D>("StarFields/WarpField");
				MapBackground = Content.Load<Texture2D>("GUI/Background/MapBackground");
				MainMenuBackground = Content.Load<Texture2D>("GUI/Background/MainMenuBackground");

				Square = Content.Load<Texture2D>("GUI/Square");
				Circle = Content.Load<Texture2D>("GUI/Circle");
				DropDownArrow = Content.Load<Texture2D>("GUI/DropDownArrow");

				Checkmark = Content.Load<Texture2D>("GUI/Checkmark");
				MainMenuLogo = Content.Load<Texture2D>("GUI/MainMenu");

				ButtonsTexture = Content.Load<Texture2D>("GUI/Buttons");

				ButtonUnderlay = Content.Load<Texture2D>("GUI/Underlay");
				ButtonUnderlay2 = Content.Load<Texture2D>("GUI/Underlay2");
				ButtonUnderlay2 = Content.Load<Texture2D>("GUI/Underlay2");

				
			//Galaxy
				//Stars
				Star_YellowMainSequence = Content.Load<Texture2D>("Stars/YellowMainSequence");
				Star_WhiteDwarf = Content.Load<Texture2D>("Stars/WhiteDwarf");
				Star_RedGiant = Content.Load<Texture2D>("Stars/RedGiant");
				Star_BlueGiant = Content.Load<Texture2D>("Stars/BlueGiant");

				//Planets
				Planet_ClassH_Delvor = Content.Load<Texture2D>("Planets/ClassH/Delvor");
				Planet_ClassJ_Fash = Content.Load<Texture2D>("Planets/ClassJ/Fash");
				Planet_ClassM_Etho = Content.Load<Texture2D>("Planets/ClassM/Etho");
				Planet_ClassO_Serine = Content.Load<Texture2D>("Planets/ClassO/Serine");
				Planet_ClassP_Antasia = Content.Load<Texture2D>("Planets/ClassP/Antasia");
				Planet_ClassY_Voshnoy = Content.Load<Texture2D>("Planets/ClassY/Voshnoy");

				Shadow = Content.Load<Texture2D>("Planets/Other/Shadow");

				//Galaxies
				Galaxy_Spiral1 = Content.Load<Texture2D>("Galaxies/Spiral/1");

				//Debug
				ERROR = Content.Load<Texture2D>("Debug/ERROR");
				Planet_Debug_Medium = Content.Load<Texture2D>("Debug/Planets/Planet_Debug");
				Debug_MarkerBB = Content.Load<Texture2D>("Debug/Markers/MarkerBB");
				Debug_MarkerBC = Content.Load<Texture2D>("Debug/Markers/MarkerBC");
				Debug_MarkerBG = Content.Load<Texture2D>("Debug/Markers/MarkerBG");
				Debug_MarkerBO = Content.Load<Texture2D>("Debug/Markers/MarkerBO");
				Debug_MarkerBP = Content.Load<Texture2D>("Debug/Markers/MarkerBP");
				Debug_MarkerBR = Content.Load<Texture2D>("Debug/Markers/MarkerBR");
				Debug_MarkerBY = Content.Load<Texture2D>("Debug/Markers/MarkerBY");

				//Ships
				Ship_Serenity = Content.Load<Texture2D>("Ships/Serenity");
				Ship_Debug = Content.Load<Texture2D>("Ships/Debug");
					//Parts
					Capasitor = Content.Load<Texture2D>("Ships/parts/Capasitor");
					Cargo = Content.Load<Texture2D>("Ships/parts/Cargo");
					Chassis1 = Content.Load<Texture2D>("Ships/parts/Chassis1");
					Chassis2 = Content.Load<Texture2D>("Ships/parts/Chassis2");
					Generator = Content.Load<Texture2D>("Ships/parts/Generator");
					LifeSupport = Content.Load<Texture2D>("Ships/parts/LifeSupport");
					Radiator = Content.Load<Texture2D>("Ships/parts/Radiator");
					Thruster = Content.Load<Texture2D>("Ships/parts/Thruster");

				//Particles
				EmitterTexture = Content.Load<Texture2D>("Particles/square");
				ParticleTexture = Content.Load<Texture2D>("Particles/circle");

			//Add to the index of textures
				TextureLookup.Add(TextureIndexes.ERROR, ERROR);
				TextureLookup.Add(TextureIndexes.Star_YellowMainSequence, Star_YellowMainSequence);
				TextureLookup.Add(TextureIndexes.Star_WhiteDwarf, Star_WhiteDwarf);
				TextureLookup.Add(TextureIndexes.Star_RedGiant, Star_RedGiant);
				TextureLookup.Add(TextureIndexes.Star_BlueGiant, Star_BlueGiant);
				TextureLookup.Add(TextureIndexes.Planet_Debug_Medium, Planet_Debug_Medium);
				TextureLookup.Add(TextureIndexes.Planet_ClassH_Delvor, Planet_ClassH_Delvor);
				TextureLookup.Add(TextureIndexes.Planet_ClassJ_Fash, Planet_ClassJ_Fash);
				TextureLookup.Add(TextureIndexes.Planet_ClassM_Etho, Planet_ClassM_Etho);
				TextureLookup.Add(TextureIndexes.Planet_ClassO_Serine, Planet_ClassO_Serine);
				TextureLookup.Add(TextureIndexes.Planet_ClassP_Antasia, Planet_ClassP_Antasia);
				TextureLookup.Add(TextureIndexes.Planet_ClassY_Voshnoy, Planet_ClassY_Voshnoy);
				TextureLookup.Add(TextureIndexes.Shadow, Shadow);
				TextureLookup.Add(TextureIndexes.Galaxy_Spiral1, Galaxy_Spiral1);
				TextureLookup.Add(TextureIndexes.Ship_Serenity, Ship_Serenity);
				TextureLookup.Add(TextureIndexes.Ship_Debug, Ship_Debug);
				TextureLookup.Add(TextureIndexes.EmitterTexture, EmitterTexture);
				TextureLookup.Add(TextureIndexes.ParticleTexture, ParticleTexture);

		}

		protected override void UnloadContent(){}

		#endregion


		#region "Other"
		public void SetFrameRate(GraphicsDeviceManager manager, int frames)
		{
			switch (frames)
			{
				case -1:
					manager.SynchronizeWithVerticalRetrace = false;
					base.IsFixedTimeStep = false;
					manager.ApplyChanges();
					break;
				case 0:
					manager.SynchronizeWithVerticalRetrace = true;
					base.IsFixedTimeStep = true;
					manager.ApplyChanges();
					break;
				default:
					manager.SynchronizeWithVerticalRetrace = false; //Disable VSync
					base.TargetElapsedTime = TimeSpan.FromMilliseconds(1000 / Math.Min((double)frames, 2000d)); //Set the time between update calls to 1/frames'th of a second. frames = 120, TargetElapseTime = 1/120th of a second.
					manager.ApplyChanges(); //Apply changes
					break;
			}
		}

		public static void SetResolution(int width, int height, bool fullscreen)
		{
            GraphicsManager.PreferredBackBufferWidth = width;
            GraphicsManager.PreferredBackBufferHeight = height;
            GraphicsManager.IsFullScreen = fullscreen;
            GraphicsManager.ApplyChanges();

            CurrentScreenResolution = new Vector2(width, height);

            /*
			// If we aren't using a full screen mode, the height and width of the window can
			// be set to anything equal to or smaller than the actual screen size.
			if (fullscreen == false)
			{
				if ((width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width) && (height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
				{
                    
					//.Position = new Point(screen.Bounds.X, screen.Bounds.Y);
					GraphicsManager.PreferredBackBufferWidth = width;
					GraphicsManager.PreferredBackBufferHeight = height;
					//static_this.Window.Position = Microsoft.Xna.Framework.Point.Zero;
					static_this.Window.IsBorderless = fullscreen;
					GraphicsManager.ApplyChanges();
				}
			}
			else
			{
				// If we are using full screen mode, we should check to make sure that the display
				// adapter can handle the video mode we are trying to set. To do this, we will
				// iterate through the display modes supported by the adapter and check them against
				// the mode we want to set.
				foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
				{
					// Check the width and height of each mode against the passed values
					if ((dm.Width == width) && (dm.Height == height))
					{
						// The mode is supported, so set the buffer formats, apply changes and return
						GraphicsManager.PreferredBackBufferWidth = width;
						GraphicsManager.PreferredBackBufferHeight = height;

						static_this.Window.IsBorderless = fullscreen;
						System.Windows.Forms.Control.FromHandle(static_this.Window.Handle).Location = new System.Drawing.Point(0, 0);

						GraphicsManager.ApplyChanges();
					}
				}
             

			}
            
            */


			//Make menus fullscreen
			if (MainMenu != null) MainMenu.Reset();
			if (NewGame != null) NewGame.Reset();
			if (LoadGame != null) LoadGame.Reset();
			if (Options != null) Options.Reset();
			if (PauseScreen != null) PauseScreen.Reset();
			if (StarBackground != null) StarBackground.Reset();
			if (GalaxyMap != null) GalaxyMap.Reset();
			//if (LocalMap != null) LocalMap.Reset(); //This GUI page is not a fullscreen page, so it does not need to be reset when the resolution changes
			if (HUD != null) HUD.Reset();
			if (ShipBuilding != null) ShipBuilding.Reset();
		}
		public static bool IsFullscreen()
		{
			return GraphicsManager.IsFullScreen;
		}


		public static void ClearEntities()
		{
			//Clear the old stuff
			Galaxy.DestroyAllSolarSystems();
			//ShipManager.DestroyAll();
			Galaxy.DestroyAllEntities();
			//ParticleManager.DestroyAll();
			GalaxyMap.Dots.Clear();
		}

		//Override default X button
		void FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			CustomExit();
		}  
		public static void CustomExit()
		{
			saveLoad.SaveOptions();
			static_this.Exit();
		}

		public static void ExitToMainMenu()
		{
			saveLoad.CurrentSaveFile = new SaveFile((saveLoad.CurrentSaveFile != null ? saveLoad.CurrentSaveFile.SaveFileName : (Engine.NewGame.Textbox_Name.Text != null ? Engine.NewGame.Textbox_Name.Text : "ERROR")));

			saveLoad.Save(saveLoad.CurrentSaveFile);
			saveLoad.ReloadSaveFiles();

			Engine.UpdateGame = false;
			Engine.DrawGame = false;

			Engine.ClearEntities();

			Engine.MainMenu.Show(null, true);
			Engine.PauseScreen.Hide(true);
			Engine.HUD.Hide(true);
			Engine.GalaxyMap.Hide(true);
			Engine.LocalMap.Hide(true);
			Engine.Navigation.Hide(true);
		}


		public static Texture2D CreateTexture(int width, int height, Color color)
		{
			Texture2D tex = new Texture2D(GraphicsManager.GraphicsDevice, width, height);
			Color[] col = new Color[width * height];

			for (int i = 0; i < width * height; i++)
				col[i] = color;

			tex.SetData<Color>(col);

			return tex;
		}
		public static Texture2D CreateTexture(int width1, int height1, int width2, int height2, Color color1, Color color2)
		{
			int width_max = Math.Max(width1, width2);
			int height_max = Math.Max(height1, height2);

			int width_min = Math.Min(width1, width2);
			int height_min = Math.Min(height1, height2);

			int diffx = (width_max - width_min) / ((width_max - width_min) > 1 ? 2 : 1);
			int diffy = (height_max - height_min) / ((height_max - height_min) > 1 ? 2 : 1);

			int row = 0;
			int colum = 0;
			int pnt = 0;

			//Texture
			Texture2D tex = new Texture2D(GraphicsManager.GraphicsDevice, width_max, height_max);
			Color[] col = new Color[width_max * height_max];
			Color color = color1;

			while (row < height_max)
			{
				if (row + 1 <= diffy || row + 1 > height_max - diffy || colum + 1 <= diffx || colum + 1 > width_max - diffx)
				{
					color = color1;
				}
				else
				{
					color = color2;
				}

				pnt = (colum) + (row * width_max);

				col[pnt] = color;

				colum += (colum < width_max - 1 ? 1 : -colum);
				row += (colum >= width_max - 1 ? 1 : 0);
			}

			//Set the texture's pixel data to the array of colors
			tex.SetData<Color>(col);

			//Return new texture
			return tex;
		}

		//Texture Lookups
		public static TextureIndexes GetTextureIndex(Texture2D texture)
		{
			TextureIndexes returnval = TextureIndexes.ERROR;

			foreach (KeyValuePair<TextureIndexes, Texture2D> t in TextureLookup)
			{
				if (t.Value == texture)
				{
					returnval = t.Key;
				}
			}
			return returnval;
		}
		public static Texture2D GetTexture(TextureIndexes index)
		{
			Texture2D returnval = Engine.ERROR;

			foreach (KeyValuePair<TextureIndexes, Texture2D> t in TextureLookup)
			{
				
				if (t.Key == index)
				{
					returnval = t.Value;
					break;
				}
			}

			return returnval;
		}
		#endregion

		#region "Updating and Drawing"
		protected override void Update(GameTime gameTime)
		{
			//Things to update
			this.IsMouseVisible = MouseVisible;
			CurrentScreenResolution = new Vector2(GraphicsManager.PreferredBackBufferWidth, GraphicsManager.PreferredBackBufferHeight);

			//static_this.Window.Title = "CLIENTBOUNDS:" + static_this.Window.ClientBounds.ToString() + "    VIEWPORT:" + GraphicsManager.GraphicsDevice.Viewport.Bounds.ToString() +"    SET INTERNAL:" + CurrentScreenResolution.ToString();


			//Only update things when the program is active
			if (this.IsActive)
			{
				//Input
				Input.Update();
				

				//Pause Toggle
				if (Input.KeyReleased(Input.Pause) && DrawGame)
				{
					if (IsPaused)
					{
						if (ActivePage == Engine.PauseScreen || UpdateGame)
						{
							IsPaused = false;
							UpdateGame = true;

							Engine.PauseScreen.Hide(false);
						}
					}
					else
					{
						IsPaused = true;
						UpdateGame = false;


						Engine.PauseScreen.Show(null, false);
					}
				}

				//GalaxyMap Toggle
				if (GalaxyMap != null)
				{
					if (!IsPaused && DrawGame)
					{
						if (Input.KeyReleased(Input.MapToggle) && (UpdateGame || DrawGame) || GalaxyMap.Visible && Input.KeyReleased(Input.Pause))
						{
							if (!GalaxyMap.Visible)
							{
								GalaxyMap.Show(null, true);
							}
							else if (GalaxyMap.Visible)
							{
								GalaxyMap.Hide(false);
							}
						}
					}
				}


				//Create a system when Q is pressed. [DEBUG]
				if (UpdateGame && !IsPaused && ActivePage == HUD && Input.KeyReleased(Keys.Q))
				{
					Galaxy.SetSolarSystem(Galaxy.CreateSolarSystem());
				}
				//Create 100 systems when Y is pressed. [DEBUG]
				if (UpdateGame && !IsPaused && ActivePage == HUD && Input.KeyReleased(Keys.Y))
				{
					for (int ii = 0; ii < 100; ii++)
					{
						Galaxy.SetSolarSystem(Galaxy.CreateSolarSystem());
					}

					//Galaxy.CurrentSolarSystem = Galaxy.SolarSystems[Galaxy.SolarSystems.Count - 1];
				}
				//Create a ship when U is pressed. [DEBUG]
				if (UpdateGame && !IsPaused && ActivePage == HUD && Input.KeyReleased(Keys.U))
				{
					if (Camera.TargetIsShip())
					{
						Camera.TargetObject.ShipLogic.IsControlled = false;
					}
					Galaxy.CreateShip("Serenity_" + Galaxy.Entities.Count.ToString(), Galaxy.CurrentSolarSystem, 128140, 0.0025f, Engine.Ship_Serenity, Vector2d.Zero, 0f, Vector2d.Zero, 0f, true);
				}
				//Delete the last entity when Z is pressed [DEBUG]
				if (UpdateGame && !IsPaused && ActivePage == HUD && Input.KeyReleased(Keys.Z))
				{
					Galaxy.DestroyEntity(Galaxy.Entities.Count - 1);
				}
				//Open the ship building menu when V is pressed [DEBUG]
				if (UpdateGame && !IsPaused && Input.KeyReleased(Keys.V))
				{
					if (!ShipBuilding.Visible)
					{
						ShipBuilding.Show(null, true);
					}
					else if (ShipBuilding.Visible)
					{
						ShipBuilding.Hide(false);
					}
				}


				//Star background
				StarBackground.Update();

				//Camera
				Camera.Update(gameTime);

				//Game
				if (UpdateGame)
				{
					//Animation
					//animation.Location = new Vector2(400, 200);
					//animation.Update(spriteBatch, gameTime);

					//Solar Systems
					//solarSystems.Update();



					//Galaxy
					Galaxy.Update(gameTime);
					//ParticleManager.Update();
					//ShipManager.Update();

					//Entities
					//EntityManager.Update(gameTime);
				}


				OpenPagesString = "";
				for (int ii = 0; ii < OpenPages.Count; ii++)
				{
					OpenPagesString += OpenPages[ii].Form_Main.name + ", ";
				}

				//GUI
				if (Input.MouseLeftPressed) //Active Page
				{
					ActivePagesString = "";
					
					//Cycle through all of the open pages
					for (int ii = 0; ii < OpenPages.Count; ii++)
					{
						//If the page can be focused by the user and the mouse was clicked within it
						if (OpenPages[ii].Form_Main.CanFocus && OpenPages[ii].Form_Main.ContainsMouse)
						{
							//Add that window to a list, essentially like a needle stabbing through a pile of offset papers, and only stabbing the ones it goes through
							ActivePages.Add(OpenPages[ii]);
							ActivePagesString += OpenPages[ii].Form_Main.name + ", ";
						}
					}

					//If any windows were clicked
					if (ActivePages.Count > 1)
					{
						GUIPage toBeActive = ActivePages[0];
						for (int ii = 0; ii < ActivePages.Count; ii++)
						{
							//Find all clicked windows that are also open (shouldn't be necessary, more of a "just in case")
							if (OpenPages.Contains(ActivePages[ii]))
							{
								//If the current form is not a background form (IE: HUD) and the current form is higher in the OpenedPages list than the previous one
								if (ActivePages[ii].Form_Main.BackgroundForm == false && OpenPages.IndexOf(ActivePages[ii]) > OpenPages.IndexOf(toBeActive))
								{
									//Set the "form to be active" to this higher up form
									toBeActive = ActivePages[ii];
								}
							}
						}
						//If the form to be active is not the current one,
						if (toBeActive != ActivePage)
						{
							//Add the newly active form to the top of the OpenPages list
							OpenPages.Remove(toBeActive);
							OpenPages.Add(toBeActive);
						}

						//Set it to active and reset
						ActivePage = toBeActive;
						ActivePages.Clear();
					}
					//else if (ActivePages.Count == 1)
					//{
					//	ActivePage = ActivePages[0];
					//	ActivePages.Clear();
					//}

					//for (int ii = 0; ii < OpenPages.Count; ii++)
					//{
					//	if (OpenPages[ii].Form_Main.CanFocus && OpenPages[ii].Form_Main.ContainsMouse)
					//	{
					//		ActivePages.Add(OpenPages[ii]);
					//	}
					//}

					//if (ActivePages.Count > 0)
					//{
					//	if (ActivePage != null && !ActivePages.Contains(ActivePage))
					//	{
					//		ActivePage = ActivePages[ActivePages.Count - 1];
					//	}
					//	else if (ActivePage == null)
					//	{
					//		ActivePage = ActivePages[ActivePages.Count - 1];
					//	}
					//}

				}					
				//else if (ActivePages.Count > 0) //Clear active pages if they exist
				//{
				//	ActivePages.Clear();
				//}
				//else if (OpenPages.Count == 1) //Set the last open form to active if none exist
				//{
				//	ActivePage = OpenPages[0];
				//}

				//Update GUI pages (Should I only update GUI pages inside of "OpenPages"?)
				for (int ii = 0; ii < Pages.Count; ii++)
				{
					Pages[ii].Update();
				}


				//FPS
				FPS_ElapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
				if (FPS_ElapsedTime >= 1000) // 1 Second has passed
				{
					FPS = FPS_Frames;
					FPS_Frames = 0;
					FPS_ElapsedTime = 0;
				}

				

				//DEBUG
				DebugItems = new List<string>()
				{
					"DEBUG:",
					"Engine:",
					"      UpdateGame:" + UpdateGame.ToString(),
					"      DrawGame:" + DrawGame.ToString(),
					"      IsPaused:" + IsPaused.ToString(),
					"Input:",
					"GUI:",
					"      ActiveForm:" + (ActivePage.Form_Main != null ? ActivePage.Form_Main.name : "null"),
					"      ActivePages(" + ActivePages.Count.ToString() + "):" + ActivePagesString,
					"      OpenPages(" + OpenPages.Count.ToString() + "):" + OpenPagesString,
					//"      Forms:" + GUIManager.Forms.Count,
					//"      LastClicked:" + GUIManager.LastClickedForm,
					//"      FormsUnderMouseOnClick:" + GUIManager.ActiveForms.Count.ToString(),
					"Camera:", 
					"      ZoomIndex:" + Camera.ZoomIndex,
					"      ZoomValueTarget:" + Camera.ZoomValues[Camera.ZoomIndex],
					"      Zoom:" + Camera.Zoom,
					"      Sensitivity:" + Camera.Sensitivity,
					"      Camera Target:" + (Camera.TargetExists() ? Camera.TargetObject.Name : "null"),
					"      Position:" + Camera.Position.ToString(),
					"      IsEqual:" + (Camera.TargetExists() ? (Camera.Position == Camera.TargetObject.Position).ToString() : ""),
					"Entities:" + Galaxy.Entities.Count.ToString(),
					"SolarSystems: " + (Galaxy.SolarSystems != null ? Galaxy.SolarSystems.Count.ToString() : "null"),
					"      CurrentSolarSystem: " + (Galaxy.CurrentSolarSystem != null ? Galaxy.CurrentSolarSystem.Name + " (" + Galaxy.CurrentSolarSystem.Entities.Count.ToString() + ")": "null"),
				};

				//System.Threading.Thread.Sleep(1);
				base.Update(gameTime);
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			//Set the default background the user will see if nothing else is rendered
			GraphicsDevice.Clear(Color.CornflowerBlue);


			//Game Drawing
			if (DrawGame)
			{
				//Star background
				spriteBatch.Begin();
					StarBackground.Draw(spriteBatch);
				spriteBatch.End();

				//Entities
				spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, Camera.Transform);
					//Animation
					//animation.Draw(spriteBatch);
				
					//Entities
					//entityEngine.Draw(spriteBatch);

					//SolarSystem
					//solarSystems.Draw(spriteBatch);

					//Galaxy
					Galaxy.Draw(spriteBatch);
					//ShipManager.Draw(spriteBatch);
					
					//Particles
					//ParticleManager.Draw(spriteBatch);

					//Entities
					//EntityManager.Draw(spriteBatch);

				spriteBatch.End();

			}

			//Other drawable things
			spriteBatch.Begin();
				//GUI
					for (int ii = 0; ii < OpenPages.Count; ii++)
					{
						//if (Pages[ii] != ActivePage)
						OpenPages[ii].Draw(spriteBatch);
					}
					//if (ActivePage != null)
					//{
					//	ActivePage.Draw(spriteBatch);
					//}


				//FPS / Debug drawing
					spriteBatch.DrawString(Font_Small, FPS.ToString(), new Vector2(CurrentScreenResolution.X - 40, 15), Color.Green);

					if (DebugState)
					{
						for (int ii = 0; ii < DebugItems.Count; ii++)
						{
							spriteBatch.DrawString(Font_Small, DebugItems[ii], new Vector2(10, 20 * ii), Color.Green);
						}
					}
			spriteBatch.End();


			//FPS
			FPS_Frames++;

			//Draw again
			base.Draw(gameTime);
		}

		#endregion

	}
}
