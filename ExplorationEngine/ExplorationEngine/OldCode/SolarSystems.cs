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
	public class SolarSystems
	{
		#region "Declarations"
		//Graphics
		public Vector2 ScreenResolution;

		//Textures
		public static Texture2D Planet_Debug_Medium;
		public static Texture2D Planet_ClassH_Delvor;
		public static Texture2D Planet_ClassJ_Blah;
		public static Texture2D Planet_ClassM_Etho;
		public static Texture2D Planet_ClassO_Serine;
		public static Texture2D Planet_ClassP_Antasia;
		public static Texture2D Planet_ClassY_Voshnoy;

		//private Texture2D Moon_Dusty_Medium;

		public static Texture2D Shadow;

		public static Texture2D Star_YellowMainSequence;
		public static Texture2D Star_WhiteDwarf;
		public static Texture2D Star_RedGiant;
		public static Texture2D Star_BlueGiant;


		public static Dictionary<TextureIndexes, Texture2D> TextureLookup = new Dictionary<TextureIndexes, Texture2D>();

		public enum TextureIndexes
		{
			Star_YellowMainSequence,
			Star_WhiteDwarf,
			Star_RedGiant,
			Star_BlueGiant,
			Planet_Debug_Medium,
			Planet_ClassH_Delvor,
			Planet_ClassJ_Blah,
			Planet_ClassM_Etho,
			Planet_ClassO_Serine,
			Planet_ClassP_Antasia,
			Planet_ClassY_Voshnoy
		}

		public static Texture2D Debug_Fg;
		public static Texture2D Debug_Fv;
		
		//Other
		static Random Rand = new Random();

		//Entity Engine
		public static EntityEngine entityEngine;
		//BodyRarity
		public Classifications classes;
		//public Star_Rarity THROWAWAYVAR_STAR = new Star_Rarity();

		#endregion

		public SolarSystems(ContentManager Content)
		{
			//Load textures
			Planet_Debug_Medium = Content.Load<Texture2D>("Debug/Planet_Debug");
			Debug_Fg = Content.Load<Texture2D>("Debug/Fg");
			Debug_Fv = Content.Load<Texture2D>("Debug/Fv");

			Planet_ClassH_Delvor = Content.Load<Texture2D>("Planets/ClassH/Delvor");
			Planet_ClassJ_Blah = Content.Load<Texture2D>("Planets/ClassJ/blah");
			Planet_ClassM_Etho = Content.Load<Texture2D>("Planets/ClassM/Etho");
			Planet_ClassO_Serine = Content.Load<Texture2D>("Planets/ClassO/Serine");
			Planet_ClassP_Antasia = Content.Load<Texture2D>("Planets/ClassP/Antasia");
			Planet_ClassY_Voshnoy = Content.Load<Texture2D>("Planets/ClassY/Voshnoy");


			//Moon_Dusty_Medium = Content.Load<Texture2D>("Moons/Moon_Dusty_Medium");

			Shadow = Content.Load<Texture2D>("Planets/Other/Shadow");

			Star_YellowMainSequence = Content.Load<Texture2D>("Stars/YellowMainSequence");
			Star_WhiteDwarf = Content.Load<Texture2D>("Stars/WhiteDwarf");
			Star_RedGiant = Content.Load<Texture2D>("Stars/RedGiant");
			Star_BlueGiant = Content.Load<Texture2D>("Stars/BlueGiant");

			//Add to the index of textures
			TextureLookup.Add(TextureIndexes.Star_YellowMainSequence, Star_YellowMainSequence);
			TextureLookup.Add(TextureIndexes.Star_WhiteDwarf, Star_WhiteDwarf);
			TextureLookup.Add(TextureIndexes.Star_RedGiant, Star_RedGiant);
			TextureLookup.Add(TextureIndexes.Star_BlueGiant, Star_BlueGiant);
			TextureLookup.Add(TextureIndexes.Planet_Debug_Medium, Planet_Debug_Medium);
			TextureLookup.Add(TextureIndexes.Planet_ClassH_Delvor, Planet_ClassH_Delvor);
			TextureLookup.Add(TextureIndexes.Planet_ClassJ_Blah, Planet_ClassJ_Blah);
			TextureLookup.Add(TextureIndexes.Planet_ClassM_Etho, Planet_ClassM_Etho);
			TextureLookup.Add(TextureIndexes.Planet_ClassO_Serine, Planet_ClassO_Serine);
			TextureLookup.Add(TextureIndexes.Planet_ClassP_Antasia, Planet_ClassP_Antasia);
			TextureLookup.Add(TextureIndexes.Planet_ClassY_Voshnoy, Planet_ClassY_Voshnoy);

			//Create the entityEngine
			entityEngine = new EntityEngine();
			//Create the definition of our solar bodies
			classes = new Classifications();
		}

		public static void CreateSystem()
		{
			entityEngine.DestroyAll();

			Body_Star NewStar = Star_Rarity.RandomBody(Rand);
			Entity Sun = entityEngine.CreateEntity("s_1", NewStar.Texture, NewStar.Scale, NewStar.Mass, Vector2.Zero, Vector2.Zero, Vector2.Zero, Vector2.Zero, 0f, 0f, 0f, 0f, false);

			//Attempting to make the orbits start at ransom places, like adding 90 degrees at the start
			List<Entity> GeneratedPlanets = new List<Entity>();
			for (int ii = 0; ii < Rand.Next(0, 10); ii++)
			{
				Body_Planet NewPlanet = Planet_Rarity.RandomBody(Rand);
				float radius = (Sun.Texture.Width * Sun.Scale) + Rand.Next(100, 50000);
				Entity planet = entityEngine.CreateEntity("p_"+ii, NewPlanet.Texture, NewPlanet.Scale, NewPlanet.Mass, Vector2.Zero, Vector2.Zero, new Vector2(radius, radius), Vector2.Zero, Rand.Next(1, 5), 0f, 0f, 90f, true);
				GeneratedPlanets.Add(planet);
			}
			//Sun Children
			entityEngine.CreateEntityChildren(Sun, GeneratedPlanets);

			//Planet1 Children
			//entityEngine.CreateEntityChildren(Planet1,
			//	new List<Entity>()
			//	{
			//		Moon1
			//	});
		}

		public void Update()
		{
			//Entities
			entityEngine.Update();
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			entityEngine.Draw(spriteBatch);
		}

	}

	public class Classifications
	{
		private Star_Rarity starrarity;
		public SortedList<Star_Rarity.Types, List<Body_Star>> Stars = new SortedList<Star_Rarity.Types, List<Body_Star>>(4);
			private Body_Star WhiteDwarf1 = new Body_Star(SolarSystems.TextureIndexes.Star_WhiteDwarf, 0.2f, 1000f);
			private Body_Star YellowMainSequence1 = new Body_Star(SolarSystems.TextureIndexes.Star_YellowMainSequence, 1f, 1000f);
			private Body_Star RedGiant1 = new Body_Star(SolarSystems.TextureIndexes.Star_RedGiant, 4f, 800f);
			private Body_Star BlueGiant1 = new Body_Star(SolarSystems.TextureIndexes.Star_BlueGiant, 10f, 18000f);


		private Planet_Rarity planetrarity;
		public SortedList<Planet_Rarity.Types, List<Body_Planet>> Planets = new SortedList<Planet_Rarity.Types, List<Body_Planet>>();
			private Body_Planet ClassH_Delvor = new Body_Planet(SolarSystems.TextureIndexes.Planet_ClassH_Delvor, 1f, 100f);
			private Body_Planet ClassM_Etho = new Body_Planet(SolarSystems.TextureIndexes.Planet_ClassM_Etho, 1f, 100f);
			private Body_Planet ClassO_Serine = new Body_Planet(SolarSystems.TextureIndexes.Planet_ClassO_Serine, 1f, 100f);
			private Body_Planet ClassP_Antasia = new Body_Planet(SolarSystems.TextureIndexes.Planet_ClassP_Antasia, 1f, 100f);
			private Body_Planet ClassY_Voshnoy = new Body_Planet(SolarSystems.TextureIndexes.Planet_ClassY_Voshnoy, 1f, 100f);
			private Body_Planet ClassJ_Blah = new Body_Planet(SolarSystems.TextureIndexes.Planet_ClassJ_Blah, 1f, 100f);


		public Classifications()
		{
			starrarity = new Star_Rarity(this);
			planetrarity = new Planet_Rarity(this);

			List<Body_Star> WDList = new List<Body_Star>()
			{
				WhiteDwarf1
			};
			List<Body_Star> YMSList = new List<Body_Star>()
			{
				YellowMainSequence1
			};
			List<Body_Star> RGList = new List<Body_Star>()
			{
				RedGiant1
			};
			List<Body_Star> BGList = new List<Body_Star>()
			{
				BlueGiant1
			};
			Stars.Add(Star_Rarity.Types.WhiteDwarf, WDList);
			Stars.Add(Star_Rarity.Types.YellowMainSequence, YMSList);
			Stars.Add(Star_Rarity.Types.RedGiant, RGList);
			Stars.Add(Star_Rarity.Types.BlueGiant, BGList);


			List<Body_Planet> CHList = new List<Body_Planet>()
			{
				ClassH_Delvor
			};
			List<Body_Planet> CMList = new List<Body_Planet>()
			{
				ClassM_Etho,
			};
			List<Body_Planet> COList = new List<Body_Planet>()
			{
				ClassO_Serine
			};
			List<Body_Planet> CPList = new List<Body_Planet>()
			{
				ClassP_Antasia
			};
			List<Body_Planet> CYList = new List<Body_Planet>()
			{
				ClassY_Voshnoy
			};
			List<Body_Planet> CJList = new List<Body_Planet>()
			{
				ClassJ_Blah
			};
			Planets.Add(Planet_Rarity.Types.ClassH, CHList);
			Planets.Add(Planet_Rarity.Types.ClassM, CMList);
			Planets.Add(Planet_Rarity.Types.ClassO, COList);
			Planets.Add(Planet_Rarity.Types.ClassP, CPList);
			Planets.Add(Planet_Rarity.Types.ClassY, CYList);
			Planets.Add(Planet_Rarity.Types.ClassJ, CJList);

		}
	}

	public class Star_Rarity
	{
		public enum Types
		{
			WhiteDwarf = 0,
			YellowMainSequence = 1,
			RedGiant = 2,
			BlueGiant = 3
		}
		public static int TotalWeight;

		private static Classifications Classes;

		//Star percentage weights
		//White Dwarf          - 6%
		//Yellow Main Sequence - 87%
		//Red Giant            - 5%
		//Blue Giant           - 2%
		public static int GetRarity(Types star) //[Remember] Weights are wrong atm.
		{
			switch (star)
			{
				case Types.WhiteDwarf:
					return 25;
				case Types.YellowMainSequence:
					return 25;
				case Types.RedGiant:
					return 25;
				case Types.BlueGiant:
					return 25;
				default:
					return 0;
			}
		}

		public Star_Rarity(Classifications classes)
		{
			Classes = classes; //Define our classifications for each body

			for (int ii = 0; ii < Enum.GetNames(typeof(Types)).Length; ii++)
			{
				TotalWeight += GetRarity((Star_Rarity.Types)ii);
			}
		}

		
		public static Body_Star RandomBody(Random rand)
		{
			//Star generation based on weighted probability. Oh god why is this so hard.
			List<Types> StarList = new List<Types>(); //Make a list of the star types, this will hold many many copies of the type
			for (int i = 0; i < Enum.GetNames(typeof(Types)).Length; i++) //Run through the total number of items in the StarTypes Enum.
			{
				for (int ii = 0; ii < GetRarity((Types)i); ii++) //Run through the weight of each one, IE: 25% weight = 0-25 on this loop.
				{
					StarList.Add((Types)i); //Add a copy of that star type for each number from 0-X times probability, a 25% weight star is added 25 times
				}
			}
			int random = rand.Next(TotalWeight - 1); //Make a ransom from 0 - total weight of all the stars. Four 25% weight stars = 100 totalweight.
			List<Body_Star> WRS = Classes.Stars[StarList[random]]; //Get the list of individual stars from the class of star we picked randomly.
			return WRS[rand.Next(WRS.Count)]; //Pick a random star FROM the list we found above.
		}


	}
	public class Body_Star
	{
		public Texture2D Texture { get; set;}
		public float Scale { get; set;}
		public float Mass { get; set; }

		public Body_Star(SolarSystems.TextureIndexes textureindex, float scale, float mass)
		{
			Texture = SolarSystems.TextureLookup[textureindex];
			Scale = scale;
			Mass = mass;
		}
	}

	public class Planet_Rarity
	{
		public enum Types
		{
			ClassH = 0,
			ClassJ = 1,
			ClassM = 2,
			ClassO = 3,
			ClassP = 4,
			ClassY = 5
		}
		public static int TotalWeight;

		private static Classifications Classes;

		//Planet percentage weights
		
		public static int GetRarity(Types planet)
		{
			switch (planet)
			{
				case Types.ClassH:
					return 17;
				case Types.ClassJ:
					return 17;
				case Types.ClassM:
					return 17;
				case Types.ClassO:
					return 17;
				case Types.ClassP:
					return 17;
				case Types.ClassY:
					return 17;
				default:
					return 0;
			}
		}

		public Planet_Rarity(Classifications classes)
		{
			Classes = classes; //Define our classifications for each body

			for (int ii = 0; ii < Enum.GetNames(typeof(Types)).Length; ii++)
			{
				TotalWeight += GetRarity((Types)ii);
			}
		}

		public static Body_Planet RandomBody(Random rand)
		{
			//Star generation based on weighted probability. Oh god why is this so hard.
			List<Types> PlanetList = new List<Types>(); //Make a list of the star types, this will hold many many copies of the type
			for (int i = 0; i < Enum.GetNames(typeof(Types)).Length; i++) //Run through the total number of items in the StarTypes Enum.
			{
				for (int ii = 0; ii < GetRarity((Types)i); ii++) //Run through the weight of each one, IE: 25% weight = 0-25 on this loop.
				{
					PlanetList.Add((Types)i); //Add a copy of that star type for each number from 0-X times probability, a 25% weight star is added 25 times
				}
			}
			int random = rand.Next(TotalWeight); //Make a ransom from 0 - total weight of all the stars. Four 25% weight stars = 100 totalweight.
			List<Body_Planet> WRS = Classes.Planets[PlanetList[random]]; //Get the list of individual stars from the class of star we picked randomly.
			return WRS[rand.Next(WRS.Count)]; //Pick a random star FROM the list we found above.
		}

	}
	public class Body_Planet
	{
		public Texture2D Texture { get; set;}
		public float Scale { get; set;}
		public float Mass { get; set; }

		public Body_Planet(SolarSystems.TextureIndexes textureindex, float scale, float mass)
		{
			Texture = SolarSystems.TextureLookup[textureindex];
			Scale = scale;
			Mass = mass;
		}
	}

	public class Planetoid_Rarity
	{

	}
	public class Body_Planetoid
	{

	}

	*/
}
