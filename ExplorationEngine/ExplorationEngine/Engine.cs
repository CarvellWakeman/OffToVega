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

		public static Vector2I CurrentGameResolution;
        public static Vector2I CurrentScreenResolution;
		public static Vector2 VirtualScreenResolution = new Vector2(1280, 720);
        public static Vector2 MinimumSupportedResolution = new Vector2(800, 600);
		
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

		//Background
		public static Texture2D Background;
		//public static GalaxyMap GalaxyMap;
		//public static LocalMap LocalMap;
		//public static HUD HUD;
		//public static Navigation Navigation;
		//public static Sensors Sensors;
		//public static ShipBuilding ShipBuilding;


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
			//public static Texture2D ButtonsTexture;
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
		//private string ActivePagesString = "";
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
			//GUIManager
			public static GUIManager guiManager;
			
			//Animation
			public static Animation animation;
			
			//Save Load
			public static SaveLoad saveLoad;

			//Stellar Classifications
			public static StellarClassifications stellarClassifications;

            //camera
            public static Camera camera;
            public static Camera gameCamera;
			
		//Game States
			public static bool IsPaused = false;
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

			//Make a static this for access outside of Engine
			static_this = this;


            //Find the resolution of the monitor the window is on.
            CurrentScreenResolution = new Vector2I(
				System.Windows.Forms.Screen.FromControl(System.Windows.Forms.Control.FromHandle(static_this.Window.Handle)).Bounds.Width,
                System.Windows.Forms.Screen.FromControl(System.Windows.Forms.Control.FromHandle(static_this.Window.Handle)).Bounds.Height);


			//Cap the framerate. -1 for no limit, 0 for vsync default, int for anything else
			SetFrameRate(GraphicsManager, 300);
			//static_this.IsFixedTimeStep = false;

			//Set Resolution
			SetResolution(1280, 720, false);
			//this.Window.AllowUserResizing = true;
			MouseVisible = true;

			//Assign the X button to the form closing function
			System.Windows.Forms.Form ThisForm = System.Windows.Forms.Form.FromHandle(Window.Handle) as System.Windows.Forms.Form;
			if (ThisForm != null)
				ThisForm.FormClosing += FormClosing;


			//Call ContentLoad()
			base.Initialize();


			//Create the galaxy
			Galaxy.Name = "NGC 1440";


			//Initialize the Input
			Input.Initialize();


			//Create save load system
			saveLoad = new SaveLoad();

			//Create stellar classification
			stellarClassifications = new StellarClassifications();
			stellarClassifications.Save();

            //Create the game camera
            gameCamera = new Camera();
            //Set the camera to the game camera
            camera = gameCamera;

			//Load options save file
			saveLoad.LoadOptions();


			//Background
			Background = Engine.StarField;


			//Create the GUIManager
			guiManager = new GUIManager();
			guiManager.Initialize();


			//Music
			MainMenuMusic.Pitch = -0.3f;
			MainMenuMusic.IsLooped = true;

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
			Font_Large = Content.Load<SpriteFont>("Fonts/Dolce_Medium");


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

				//ButtonsTexture = Content.Load<Texture2D>("GUI/Buttons");

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
        public static void print(string message) { System.Windows.Forms.MessageBox.Show(message); }

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
            //Fullscreen and resolution management
            if (fullscreen == true)
            {
                //Set resolution to monitor resolution
                GraphicsManager.PreferredBackBufferWidth = CurrentScreenResolution.X;
                GraphicsManager.PreferredBackBufferHeight = CurrentScreenResolution.Y;

                //Apply changes the first time (to avoid window.IsBorderless from not working properly)
                GraphicsManager.ApplyChanges();

                //Set borderless to true
                static_this.Window.IsBorderless = true;
                //Set window position to upper left corner
                System.Windows.Forms.Control.FromHandle(static_this.Window.Handle).Location = new System.Drawing.Point(0, 0);

                //Set our current resolution
                CurrentGameResolution = CurrentScreenResolution;
            }
            else
            {
                if ((width <= CurrentScreenResolution.X) && (height <= CurrentScreenResolution.Y))
                {
                    //Set resolution to supplied resolution
                    GraphicsManager.PreferredBackBufferWidth = width;
                    GraphicsManager.PreferredBackBufferHeight = height;

                    //Apply changes the first time (to avoid window.IsBorderless from not working properly)
                    GraphicsManager.ApplyChanges();

                    //Set borderless to false
                    static_this.Window.IsBorderless = false;


                    //Set our current resolution
                    CurrentGameResolution = new Vector2(width, height);
                }
                else
                {
                    //Set resolution to current monitor resolution
                    GraphicsManager.PreferredBackBufferWidth = CurrentScreenResolution.X;
                    GraphicsManager.PreferredBackBufferHeight = CurrentScreenResolution.Y;

                    //Apply changes the first time (to avoid window.IsBorderless from not working properly)
                    GraphicsManager.ApplyChanges();

                    //Set borderless to false
                    static_this.Window.IsBorderless = false;
                    
                    //Set our current resolution
                    CurrentGameResolution = CurrentScreenResolution;
                }
            
            }
            
            //Apply changes
            GraphicsManager.ApplyChanges();


			//Make menus refresh
			if (guiManager != null) { guiManager.Refresh(); }

			//if (MainMenu != null) MainMenu.Refresh();
			//if (NewGame != null) NewGame.Refresh();
			//if (LoadGame != null) LoadGame.Refresh();
			//if (Options != null) Options.Refresh();
			//if (PauseScreen != null) PauseScreen.Refresh();
			//if (StarBackground != null) StarBackground.Refresh();
			//if (GalaxyMap != null) GalaxyMap.Refresh();
			//if (LocalMap != null) LocalMap.Reset(); //This GUI page is not a fullscreen page, so it does not need to be reset when the resolution changes
			//if (HUD != null) HUD.Refresh();
			//if (ShipBuilding != null) ShipBuilding.Refresh();
		}
		public static bool IsFullscreen() { return static_this.Window.IsBorderless; }


		public static void Reset()
		{
			//Clear the old stuff
			Galaxy.DestroyAllSolarSystems();
			//ShipManager.DestroyAll();
			Galaxy.DestroyAllEntities();
			//ParticleManager.DestroyAll();
			//GalaxyMap.Dots.Clear();
			guiManager.Reset();
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
			//CurrentGameResolution = new Vector2(GraphicsManager.PreferredBackBufferWidth, GraphicsManager.PreferredBackBufferHeight);
            //CurrentScreenResolution = new Vector2I(System.Windows.Forms.Screen.FromControl(System.Windows.Forms.Control.FromHandle(static_this.Window.Handle)).Bounds.Width,
            //        System.Windows.Forms.Screen.FromControl(System.Windows.Forms.Control.FromHandle(static_this.Window.Handle)).Bounds.Height);

			//Only update things when the program is active
			if (this.IsActive)
			{
				//Input
				Input.Update();


				// && ActivePage == HUD
				#region "Debug Keys"
				//Create a system when Q is pressed. [DEBUG]
				if (UpdateGame && !IsPaused && Input.KeyReleased(Keys.Q))
				{
					Galaxy.SetSolarSystem(Galaxy.CreateSolarSystem());
				}
				//Create 100 systems when Y is pressed. [DEBUG]
				if (UpdateGame && !IsPaused && Input.KeyReleased(Keys.Y))
				{
					for (int ii = 0; ii < 100; ii++)
					{
						Galaxy.SetSolarSystem(Galaxy.CreateSolarSystem());
					}

					//Galaxy.CurrentSolarSystem = Galaxy.SolarSystems[Galaxy.SolarSystems.Count - 1];
				}
				//Create a ship when U is pressed. [DEBUG]
				if (UpdateGame && !IsPaused && Input.KeyReleased(Keys.U))
				{
					if (camera.TargetIsShip())
					{
						camera.TargetObject.ShipLogic.IsControlled = false;
					}
					Galaxy.CreateShip("Ship_" + Galaxy.Entities.Count.ToString(), Galaxy.CurrentSolarSystem, 128140, 0.0025f, Engine.Ship_Serenity, Vector2d.Zero, 0f, Vector2d.Zero, 0f, true);
				}
				//Delete the last entity when Z is pressed [DEBUG]
				if (UpdateGame && !IsPaused && Input.KeyReleased(Keys.Z))
				{
					Galaxy.DestroyEntity(Galaxy.Entities.Count - 1);
				}
				//Open the ship building menu when V is pressed [DEBUG]
				if (UpdateGame && !IsPaused && Input.KeyReleased(Keys.V))
				{
					//if (!ShipBuilding.Visible)
					//{
					//	ShipBuilding.Show(null, true);
					//}
					//else if (ShipBuilding.Visible)
					//{
					//	ShipBuilding.Hide(false);
					//}
				}
				#endregion
				

				//Camera
				camera.Update(gameTime);

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

				//GUI Update
				guiManager.Update();


				//FPS
				FPS_ElapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
				if (FPS_ElapsedTime >= 1000) // 1 Second has passed
				{
					FPS = FPS_Frames;
					FPS_Frames = 0;
					FPS_ElapsedTime = 0;
				}

				//Open pages [DEBUG]
				OpenPagesString = "";
				for (int ii = 0; ii < guiManager.OpenPages.Count; ii++)
				{
					OpenPagesString += guiManager.OpenPages[ii].Form_Main.name + ", ";
				}


				//DEBUG
				DebugItems = new List<string>()
				{
					"DEBUG:",
					"Engine:",
					"      UpdateGame:" + UpdateGame.ToString(),
					"      DrawGame:" + DrawGame.ToString(),
					"      IsPaused:" + IsPaused.ToString(),
					"      ScreenResolution:" + CurrentScreenResolution.ToString(),
					"      GameResolution:" + CurrentGameResolution.ToString(),
					"Input:",
					"GUI:",
					"      ActiveForm:" + (guiManager.ActivePage.Form_Main != null ? guiManager.ActivePage.Form_Main.name : "null"),
					//"      ActivePages(" + guiManager.ActivePages.Count.ToString() + "):", //+ ActivePagesString,
					"      OpenPages(" + guiManager.OpenPages.Count.ToString() + "):" + OpenPagesString,
					//"      Forms:" + GUIManager.Forms.Count,
					//"      LastClicked:" + GUIManager.LastClickedForm,
					//"      FormsUnderMouseOnClick:" + GUIManager.ActiveForms.Count.ToString(),
					"camera:", 
					//"      ZoomIndex:" + camera.ZoomIndex,
					"      ZoomTarget:" + camera.GetZoomTarget(),
					"      ZoomLevel:" + camera.GetZoomLevel(),
					"      Sensitivity:" + camera.Sensitivity,
					"      camera Target:" + (camera.TargetExists() ? camera.TargetObject.Name : "null"),
					"      Position:" + camera.Position.ToString(),
					"      IsEqual:" + (camera.TargetExists() ? (camera.Position == camera.TargetObject.Position).ToString() : ""),
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
				//background
				spriteBatch.Begin();
					spriteBatch.Draw(Background, new Rectangle(0, 0, Engine.CurrentGameResolution.X, Engine.CurrentGameResolution.Y), Color.White);
				spriteBatch.End();

				//EntitiesF
				spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, null, null, null, null, camera.Transform);
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
				guiManager.Draw(spriteBatch);

				//FPS / Debug drawing
					spriteBatch.DrawString(Font_Small, FPS.ToString(), new Vector2(CurrentGameResolution.X - 40, 15), Color.Green);

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
