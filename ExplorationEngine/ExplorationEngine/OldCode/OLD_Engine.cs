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
	//public class Engine : Game
	//{

	//	#region "Declarations"
	//	//Graphics
	//	public static GraphicsDeviceManager GraphicsManager;
	//	public SpriteBatch spriteBatch;
	//	public static Vector2 CurrentScreenResolution;
		
	//	//Audio
	//	private SoundEffect MainMenuMusic;

	//	//Font
	//	public static SpriteFont Font_Small;
	//	public static SpriteFont Font_Medium;
	//	public static SpriteFont Font_Large;

	//	//FPS
	//	private int FPS_Frames = 0;
	//	private float FPS_ElapsedTime = 0.0f;
	//	private float FPS = 0.0f;

	//	//Debug
	//	private List<string> DebugItems;
	//	public static bool DebugState = false;

	//	//Other
	//	Random Rand = new Random();
	//	public static bool IsPaused;

	//	//Things I shouldn't have to do, but had to anyways because of Microsoft.
	//	public static bool MouseVisible { get; set; } //Because I can't set this.IsMouseVisible because "this" is not a static field.
	//	static Engine static_this; //Because I can't use this.Exit() in a static method.


	//	//Instances

	//		//Camera
	//		public static Camera camera;

	//		//Animation
	//		private Animation animation;

	//		//Particle Emitter
	//		ParticleEmitter particleEmitter;
			
	//		//SolarSystems
	//		SolarSystems solarSystems;

	//		//GUI
	//		GUI gui;
			
		
	//		//GameStates
	//		private static GameState gameState;
	//		public enum GameState
	//		{
	//			MainMenu,
	//			NewGame,
	//			LoadGame,
	//			Map,
	//			Options,
	//			Game,
	//			GamePause,
	//			Exit
	//		}

	//	#endregion


	//	#region "Constructor/Destructor"
	//	public Engine()
	//	{
	//		GraphicsManager = new GraphicsDeviceManager(this);
	//		Content.RootDirectory = "Content";

	//		//Make a static this
	//		static_this = this;
	//	}
	//	#endregion

		

	//	#region "Initialization"
	//	protected override void Initialize()
	//	{
	//		//Cap the framerate. -1 for no limit, 0 for vsync default, int for anything else
	//		SetFrameRate(GraphicsManager, 300);


	//		//Create the SolarSystems
	//		solarSystems = new SolarSystems(this.Content);

	//		//Create the Camera
	//		camera = new Camera();

	//		//Initialize the Input
	//		Input.Initialize();

	//		//Create the GUI
	//		gui = new GUI(this.Content);

	//		//Gamestate
	//		SetGamestate(GameState.MainMenu);


	//		//Set Resolution
	//		Resolution.Init(ref GraphicsManager);
	//		Resolution.SetVirtualResolution(1280, 720);
	//		//Resolution.SetResolution(1600, 900, false);
	//		//Resolution.SetResolution(1366, 768, false);
	//		Resolution.SetResolution(1280, 720, false);
	//		this.Window.AllowUserResizing = true;
	//		MouseVisible = true;


	//		//Call ContentLoad()
	//		base.Initialize(); 


	//		//Create Menu items
	//			//MainMenu
	//			gui.CreateButton("MainMenu_NewGame", GUI.ButtonTypes.ToNewGameMenu, GameState.MainMenu, new Rectangle(2, 2, 248, 41), new Vector2(50, 400), false, false);
	//			gui.CreateButton("MainMenu_LoadGame", GUI.ButtonTypes.ToLoadGameMenu, GameState.MainMenu, new Rectangle(2, 46, 268, 41), new Vector2(50, 450), false, true);
	//			gui.CreateButton("MainMenu_Options", GUI.ButtonTypes.Options, GameState.MainMenu, new Rectangle(2, 132, 199, 41), new Vector2(50, 500), false, false);
	//			gui.CreateButton("MainMenu_Exit", GUI.ButtonTypes.Exit, GameState.MainMenu, new Rectangle(2, 176, 80, 39), new Vector2(50, 550), false, false);
				
	//			//New Game
	//			Button tab1 = gui.CreateButton("NewGame_Back", GUI.ButtonTypes.Back, GameState.NewGame, new Rectangle(2, 221, 116, 41), new Vector2(50, 50), false, false);
	//			tab1.EmulationKey = Keys.Escape;
	//			gui.CreateButton("NewGame_Create", GUI.ButtonTypes.ToNewGame, GameState.NewGame, new Rectangle(2, 353, 162, 41), new Vector2(640, 300), true, false);
	
	//			gui.NewGameTB = gui.CreateTextbox("NewGame_NewWorldName", GameState.NewGame, new Vector2(640, 200), 10);
				
	//			//LoadGame
	//			Button tab2 = gui.CreateButton("LoadGame_Back", GUI.ButtonTypes.Back, GameState.LoadGame, new Rectangle(2, 221, 116, 41), new Vector2(50, 50), false, false);
	//			tab2.EmulationKey = Keys.Escape;
				
	//			//Options
	//			Button tab3 = gui.CreateButton("Options_Back", GUI.ButtonTypes.Back, GameState.Options, new Rectangle(2, 221, 116, 41), new Vector2(50, 50), false, false);
	//			tab3.EmulationKey = Keys.Escape;
				
	//			//Pause
	//			gui.CreateButton("Pause_Resume", GUI.ButtonTypes.Resume, GameState.GamePause, new Rectangle(2, 265, 172, 41), new Vector2(50, 200), false, false);
	//			gui.CreateButton("Pause_Options", GUI.ButtonTypes.Options, GameState.GamePause, new Rectangle(2, 132, 199, 41), new Vector2(50, 250), false, false);
	//			gui.CreateButton("Pause_ExitToMainMenu", GUI.ButtonTypes.ExitToMainMenu, GameState.GamePause, new Rectangle(2, 309, 409, 41), new Vector2(50, 300), false, false);


	//		//MainMenuMusic.Play();

	//			//SolarSystems.CreateSystem();


	//		Resolution.RefreshResolution();


	//	}
	//	#endregion


	//	#region "Content load/unload"

	//	protected override void LoadContent()
	//	{
	//		//Create a new SpriteBatch, which can be used to draw textures.
	//		spriteBatch = new SpriteBatch(GraphicsDevice);


	//		//Audio
	//		MainMenuMusic = Content.Load<SoundEffect>("Audio/Solarity");

	//		//Animation
	//		Texture2D texture = Content.Load<Texture2D>("Samples/spritesheet");
	//		animation = new Animation(texture, 8, 8, 80, 4, 32, 32);

	//		//Particles
	//		List<Texture2D> textures = new List<Texture2D>();
	//		textures.Add(Content.Load<Texture2D>("Particles/circle"));
	//		//textures.Add(Content.Load<Texture2D>("Particles/star"));
	//		//textures.Add(Content.Load<Texture2D>("Particles/diamond"));
	//		particleEmitter = new ParticleEmitter(textures, new Vector2(400, 240));

	//		//Font
	//		Font_Small = Content.Load<SpriteFont>("Fonts/Dolce_Small");
	//		Font_Medium = Content.Load<SpriteFont>("Fonts/Dolce_Medium");
	//		Font_Large = Content.Load<SpriteFont>("Fonts/Dolce_Large");

	//	}

	//	protected override void UnloadContent()
	//	{
			
	//	}

	//	#endregion


	//	#region "Other"
	//	public void SetFrameRate(GraphicsDeviceManager manager, int frames)
	//	{
	//		switch (frames)
	//		{
	//			case -1:
	//				manager.SynchronizeWithVerticalRetrace = false;
	//				base.IsFixedTimeStep = false;
	//				manager.ApplyChanges();
	//				break;
	//			case 0:
	//				manager.SynchronizeWithVerticalRetrace = true;
	//				base.IsFixedTimeStep = true;
	//				manager.ApplyChanges();
	//				break;
	//			default:
	//				manager.SynchronizeWithVerticalRetrace = false; //Disable VSync
	//				base.TargetElapsedTime = TimeSpan.FromMilliseconds(1000 / Math.Min((double)frames, 2000d)); //Set the time between update calls to 1/frames'th of a second. frames = 120, TargetElapseTime = 1/120th of a second.
	//				manager.ApplyChanges(); //Apply changes
	//				break;
	//		}
	//	}

	//	public static void SetGamestate(GameState gs)
	//	{
	//		gameState = gs;

	//		switch (gs)
	//		{
	//			case GameState.MainMenu:
	//				camera.Off();
	//				MouseVisible = true;
	//				IsPaused = false;
	//				break;
	//			case GameState.NewGame:
	//				camera.Off();
	//				MouseVisible = true;
	//				break;
	//			case GameState.LoadGame:
	//				camera.Off();
	//				MouseVisible = true;
	//				break;
	//			case GameState.Game:
	//				camera.On();
	//				MouseVisible = false;
	//				break;
	//			case GameState.GamePause:
	//				camera.Off();
	//				MouseVisible = true;
	//				break;
	//			case GameState.Map:
	//				break;
	//			case GameState.Options:
	//				camera.Off();
	//				MouseVisible = true;
	//				break;
	//			case GameState.Exit:
	//				CustomExit();
	//				break;
	//		}
	//	}

	//	public static GameState GetGamestate()
	//	{
	//		return gameState;
	//	}

	//	public static void CustomExit()
	//	{
	//		//SaveLoad.Save(SaveLoad.CurrentSave, SolarSystems.entityEngine.Entities);
	//		static_this.Exit();
	//	}

	//	#endregion


	//	#region "Updating and Drawing"
	//	protected override void Update(GameTime gameTime)
	//	{
	//		//Things to upgate
	//		this.IsMouseVisible = MouseVisible;
	//		CurrentScreenResolution = new Vector2(Engine.GraphicsManager.GraphicsDevice.Viewport.Width, Engine.GraphicsManager.GraphicsDevice.Viewport.Height);

	//		//Only update things when the program is active
	//		if (this.IsActive)
	//		{
	//			KeyboardState Game_Keyboard = Keyboard.GetState();
	//			MouseState Game_Mouse = Mouse.GetState();
				

	//			// Allows the game to pause
	//			if (Input.KeyReleased(Keys.Escape))
	//			{
	//				if (!IsPaused && gameState == GameState.Game)
	//				{
	//					SetGamestate(GameState.GamePause);
	//				}
	//				else if (IsPaused && gameState == GameState.GamePause)
	//				{
	//					SetGamestate(GameState.Game);
	//				}
	//				IsPaused = !IsPaused;
	//			}

	//			//Create a system when Q is pressed. [DEBUG]
	//			if (Input.KeyReleased(Keys.Q) && gameState == GameState.Game)
	//			{
	//				SolarSystems.CreateSystem();
	//			}


	//			//FPS
	//			FPS_ElapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
	//			if (FPS_ElapsedTime >= 1000) // 1 Second has passed
	//			{
	//				FPS = FPS_Frames;
	//				FPS_Frames = 0;
	//				FPS_ElapsedTime = 0;
	//			}


	//			//DEBUG
	//			DebugItems = new List<string>()
	//			{
	//				"DEBUG:",
	//				//"   Mouse1:" + Mouse.GetState().LeftButton,
	//				//"   Mouse2:" + Mouse.GetState().RightButton,
	//				//"   MouseScrollValue:" + Mouse.GetState().ScrollWheelValue,
	//				//"   CalculatedScrollValue:" + camera.ScrollValue + "("+ camera.ScrollValue/120+")",
	//				"   ZoomLevel:" + camera.Zoom,
	//				"   "
	//				//"   Entities:" + solarSystems.entityEngine.Entities.Count
	//			};


	//			//Instance Updating and gamestate management
	//			if (gameState == GameState.Game)
	//			{
	//				//Animation
	//				//animation.Location = new Vector2(400, 200);
	//				//animation.Update(spriteBatch, gameTime);

	//				//Particles
	//				particleEmitter.EmitterLocation = new Vector2(camera.Position.X, camera.Position.Y);
	//				particleEmitter.Update();

	//				//Solar Systems
	//				solarSystems.Update();

	//			}


	//			//Camera
	//			camera.Update(this.IsActive);

	//			//Input
	//			Input.Update();

	//			//GUI
	//			gui.Update();

	//			//Map
	//			//map.Update();


	//			base.Update(gameTime);
	//		}
	//	}

	//	protected override void Draw(GameTime gameTime)
	//	{
	//		//Set the default background the user will see if nothing else is rendered
	//		GraphicsDevice.Clear(Color.Black);

	//		//Game Drawing
	//		if (gameState == GameState.Game || gameState == GameState.GamePause)
	//		{
	//			//Background drawing
	//			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Opaque, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone);
	//			spriteBatch.Draw(GUI.Background_StarField_1600X900, new Rectangle(0, 0, (int)CurrentScreenResolution.X, (int)CurrentScreenResolution.Y), Color.White);
	//			spriteBatch.End();

	//			//Game Object Drawing
	//			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.get_transformation(GraphicsManager.GraphicsDevice));
	//				//Animation
	//				//animation.Draw(spriteBatch);

	//				//Entities
	//				//entityEngine.Draw(spriteBatch);

	//				//SolarSystem
	//				solarSystems.Draw(spriteBatch);

	//				//Particles
	//				//particleEmitter.Draw(spriteBatch);

	//			spriteBatch.End();

	//			//FPS / Debug drawing
	//			spriteBatch.Begin();
	//				spriteBatch.DrawString(Font_Small, "FPS: " + FPS.ToString(), new Vector2(CurrentScreenResolution.X - 120, 20), Color.Red);
	//				for (int ii = 0; ii < DebugItems.Count; ii++)
	//				{
	//					spriteBatch.DrawString(Font_Small, DebugItems[ii], new Vector2(10, 20 * ii), Color.Red);
	//				}
	//			spriteBatch.End();
	//		}

	//		//GUI
	//		gui.Draw(spriteBatch);




	//		//FPS
	//		FPS_Frames++;

	//		//Draw again
	//		base.Draw(gameTime);
	//	}

	//	#endregion

	//}
}
