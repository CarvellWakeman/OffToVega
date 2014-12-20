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
/*
namespace ExplorationEngine
{
	public static class StellarClassifications
	{
		//Random
		public static Random Rand = new Random();

		public enum Body_Types
		{
			Unknown = 0,
			Star = 1,
			Planet = 2,
			Planetoid = 3,
			Asteroid = 4,
			Comet = 5
		}

		public enum Star_Types
		{
			Any = 0,
			WhiteDwarf = 1,
			MainSequence = 2,
			RedGiant = 3,
			BlueGiant = 4
		}
		static List<Star_Types> StarWeight = new List<Star_Types>();

		public enum Planet_Types
		{
			Any = 0,
			ClassH = 1,
			ClassJ = 2,
			ClassM = 3,
			ClassO = 4,
			ClassP = 5,
			ClassY = 6
		}
		static List<Planet_Types> PlanetWeight = new List<Planet_Types>();

		static StellarClassifications()
		{
			//Add star types for rarity
			for (int ii = 0; ii < Class_MainSequence.Rarity; ii++) { StarWeight.Add(Star_Types.MainSequence); }
			for (int ii = 0; ii < Class_WhiteDwarf.Rarity; ii++) { StarWeight.Add(Star_Types.WhiteDwarf); }
			for (int ii = 0; ii < Class_RedGiant.Rarity; ii++) { StarWeight.Add(Star_Types.RedGiant); }
			for (int ii = 0; ii < Class_BlueGiant.Rarity; ii++) { StarWeight.Add(Star_Types.BlueGiant); }

			//Add planet types for rarity
			for (int ii = 0; ii < Class_Desert.Rarity; ii++) { PlanetWeight.Add(Planet_Types.ClassH); }
			for (int ii = 0; ii < Class_Terrestrial.Rarity; ii++) { PlanetWeight.Add(Planet_Types.ClassM); }
			for (int ii = 0; ii < Class_WaterWorld.Rarity; ii++) { PlanetWeight.Add(Planet_Types.ClassO); }
			for (int ii = 0; ii < Class_IceWorld.Rarity; ii++) { PlanetWeight.Add(Planet_Types.ClassP); }
			for (int ii = 0; ii < Class_HellWorld.Rarity; ii++) { PlanetWeight.Add(Planet_Types.ClassY); }
			for (int ii = 0; ii < Class_GasGiant.Rarity; ii++) { PlanetWeight.Add(Planet_Types.ClassJ); }
		}


		public static SolarSystem GenerateSolarSystem()
		{
			return null;
		}
		public static BaseEntity GenerateStar(SolarSystem parentSystem, Star_Types type = Star_Types.Any)
		{
			BaseEntity Star = null;
			Instance_Body Instance = null;

			//If any type of star is to be chosen
			if (type == Star_Types.Any)
			{
				//Randomly pick a star type weighted based on star rarity
				type = StarWeight[Rand.Next(0, StarWeight.Count - 1)];
			}

			//Generate a different star star instance based on the type of star chosen
			switch (type)
			{
				case Star_Types.MainSequence:
					Instance = Class_MainSequence.Generate();

					break;
				case Star_Types.RedGiant:
					Instance = Class_RedGiant.Generate();

					break;
				case Star_Types.WhiteDwarf:
					Instance = Class_WhiteDwarf.Generate();

					break;
				case Star_Types.BlueGiant:
					Instance = Class_BlueGiant.Generate();

					break;

			}

			//Generate star BaseEntity
			Star = Galaxy.CreateBody(parentSystem.Name + "_s_0", null, parentSystem.Name, Instance.Texture, Engine.Planet_Debug_Medium, Instance.Scale, Instance.Mass,
				Vector2d.Zero, Vector2d.Zero, Vector2d.Zero, Vector2d.Zero, 0f, 0f, 0f, 0f);

			//Assign its temperature
			Star.BodyLogic.CoreTemperature = Instance.CoreTemperature;
			Star.BodyLogic.SurfaceTemperature = Instance.SurfaceTemperature;
			//Assign its type
			Star.BodyLogic.Type = Instance.Type;

			//Return the newly made star
			return Star;
		}
		public static BaseEntity GeneratePlanet(SolarSystem parentSystem, Planet_Types type = Planet_Types.Any)
		{
			BaseEntity Planet = null;
			Instance_Body Instance = null;

			//If any type of planet is to be chosen
			if (type == Planet_Types.Any)
			{
				//Randomly pick a planet type weighted based on planet rarity
				type = PlanetWeight[Rand.Next(0, PlanetWeight.Count - 1)];
			}

			//Generate a different star star instance based on the type of star chosen
			switch (type)
			{
				case Planet_Types.ClassH:
					Instance = Class_Desert.Generate();

					break;
				case Planet_Types.ClassM:
					Instance = Class_Terrestrial.Generate();

					break;
				case Planet_Types.ClassO:
					Instance = Class_WaterWorld.Generate();

					break;
				case Planet_Types.ClassP:
					Instance = Class_IceWorld.Generate();

					break;
				case Planet_Types.ClassY:
					Instance = Class_HellWorld.Generate();

					break;
				case Planet_Types.ClassJ:
					Instance = Class_GasGiant.Generate();

					break;

			}

			//Generate planet BaseEntity
			Planet = Galaxy.CreateBody(parentSystem.Name + "_p_" + parentSystem.Entities.Count.ToString(),                               //Name
				(parentSystem.Entities.Count > 0 ? EntityManager.EntityLookup(parentSystem.Entities[0]) : null), parentSystem.Name,      //Parent, ParentSystem Name
				Instance.Texture, Engine.Planet_Debug_Medium,																	         //Texture, debug texture
				Instance.Scale, Instance.Mass,																					         //Scale, Mass
				Vector2d.Zero, Vector2d.Zero,																					         //Position, Velocity 
				Instance.OrbitRadius, Vector2d.Zero, Instance.OrbitSpeed,														         //OrbitRadius, OrbitOffset, Orbit Speed
				0f, Rand.Next(1, 4) / 3 * (float)Rand.NextDouble() * ((float)Rand.NextDouble() < 0.1 ? 1 : -1) * (MathHelper.Pi / 180), Rand.Next(0, 360));	//Angle, AngularVelocity, InitialAngleToParent

			//Set the planet's initial position
			if (Planet.Orbit != null)
				Planet.Position = Planet.Orbit._parent.Position + new Vector2d((float)Math.Cos(Planet.Orbit.InitialAngle) * Planet.Orbit.OrbitRadius.X, -(float)Math.Sin(Planet.Orbit.InitialAngle) * Planet.Orbit.OrbitRadius.Y);

			//Assign its temperature
			Planet.BodyLogic.CoreTemperature = Instance.CoreTemperature;
			Planet.BodyLogic.SurfaceTemperature = Instance.SurfaceTemperature;
			//Assign its type
			Planet.BodyLogic.Type = Instance.Type;

			//Return the newly made planet
			return Planet;
		}
		public static BaseEntity GeneratePlanetoid()
		{
			return null;
		}

	}


	//Star percentage weights
	//Yellow Main Sequence - 87%
	//White Dwarf          - 6%
	//Red Giant            - 5%
	//Blue Giant           - 2%
	public abstract class Classification
	{
		//public static StellarClassifications.Star_Types Type = StellarClassifications.Star_Types.MainSequence;
		public static StellarClassifications.Body_Types Type = StellarClassifications.Body_Types.Unknown;

		public static int Rarity = 1;

		public static List<Texture2D> Textures = new List<Texture2D>();

		public static double MassMin = 1d;
		public static double MassMax = 1d;

		public static double TemperatureMin = 0d;
		public static double TemperatureMax = 0d;

		public static float ScaleMin = 0f;
		public static float ScaleMax = 0f;

		public virtual BaseEntity Generate()
		{
			return null;
		}

	}

	public static class Class_MainSequence
	{
		//public static List<string> Names = new List<string>();

		//public static StellarClassifications.Star_Types Type = StellarClassifications.Star_Types.MainSequence;
		public static StellarClassifications.Body_Types Type = StellarClassifications.Body_Types.Star;

		public static int Rarity = 87;

		public static List<Texture2D> Textures = new List<Texture2D>();

		public static double MassMin = 0.8d;
		public static double MassMax = 1.04d;

		public static double TemperatureMin = 14843720d;
		public static double TemperatureMax = 17245861d;

		public static float ScaleMin = 0.8f;
		public static float ScaleMax = 1.2f;

		static Class_MainSequence()
		{
			Textures.Add(Engine.Star_YellowMainSequence);
		}

		public static Instance_Body Generate()
		{
			double Mass = (MassMin + (StellarClassifications.Rand.NextDouble() * (MassMax - MassMin))) * Math.Pow(10, 30);
			double Temperature = (TemperatureMin + ((float)StellarClassifications.Rand.NextDouble() * (TemperatureMax - TemperatureMin)));
			float Scale = (ScaleMin + ((float)StellarClassifications.Rand.NextDouble() * (ScaleMax - ScaleMin)));

			return new Instance_Body(Type, Textures[StellarClassifications.Rand.Next(0, Textures.Count - 1)], Vector2d.Zero, 0f, Scale, Mass, Temperature, 0d);
		}

	}
	public static class Class_WhiteDwarf
	{
		//public static List<string> Names = new List<string>();

		//public static StellarClassifications.Star_Types Type = StellarClassifications.Star_Types.WhiteDwarf;
		public static StellarClassifications.Body_Types Type = StellarClassifications.Body_Types.Star;

		public static int Rarity = 6;

		public static List<Texture2D> Textures = new List<Texture2D>();

		public static double MassMin = 1.4d;
		public static double MassMax = 2.1d;

		public static double TemperatureMin = 984201d;
		public static double TemperatureMax = 172004d;

		public static float ScaleMin = 0.2f;
		public static float ScaleMax = 0.4f;

		static Class_WhiteDwarf()
		{
			Textures.Add(Engine.Star_WhiteDwarf);
		}

		public static Instance_Body Generate()
		{
			double Mass = (MassMin + (StellarClassifications.Rand.NextDouble() * (MassMax - MassMin))) * Math.Pow(10, 30);
			double Temperature = (TemperatureMin + ((float)StellarClassifications.Rand.NextDouble() * (TemperatureMax - TemperatureMin)));
			float Scale = (ScaleMin + ((float)StellarClassifications.Rand.NextDouble() * (ScaleMax - ScaleMin)));

			return new Instance_Body(Type, Textures[StellarClassifications.Rand.Next(0, Textures.Count - 1)], Vector2d.Zero, 0f, Scale, Mass, Temperature, 0d);
		}

	}
	public static class Class_RedGiant
	{
		//public static List<string> Names = new List<string>();

		//public static StellarClassifications.Star_Types Type = StellarClassifications.Star_Types.RedGiant;
		public static StellarClassifications.Body_Types Type = StellarClassifications.Body_Types.Star;

		public static int Rarity = 5;

		public static List<Texture2D> Textures = new List<Texture2D>();

		public static double MassMin = 0.08d;
		public static double MassMax = 0.45d;

		public static double TemperatureMin = 580870040d;
		public static double TemperatureMax = 628400303d;

		public static float ScaleMin = 3.8f;
		public static float ScaleMax = 4.5f;

		static Class_RedGiant()
		{
			Textures.Add(Engine.Star_RedGiant);
		}

		public static Instance_Body Generate()
		{
			double Mass = (MassMin + (StellarClassifications.Rand.NextDouble() * (MassMax - MassMin))) * Math.Pow(10, 30);
			double Temperature = (TemperatureMin + ((float)StellarClassifications.Rand.NextDouble() * (TemperatureMax - TemperatureMin)));
			float Scale = (ScaleMin + ((float)StellarClassifications.Rand.NextDouble() * (ScaleMax - ScaleMin)));

			return new Instance_Body(Type, Textures[StellarClassifications.Rand.Next(0, Textures.Count - 1)], Vector2d.Zero, 0f, Scale, Mass, Temperature, 0d);
		}

	}
	public static class Class_BlueGiant
	{
		//public static List<string> Names = new List<string>();

		//public static StellarClassifications.Star_Types Type = StellarClassifications.Star_Types.BlueGiant;
		public static StellarClassifications.Body_Types Type = StellarClassifications.Body_Types.Star;

		public static int Rarity = 2;

		public static List<Texture2D> Textures = new List<Texture2D>();

		public static double MassMin = 16d;
		public static double MassMax = 20d;

		public static double TemperatureMin = 14803824d;
		public static double TemperatureMax = 22245092d;

		public static float ScaleMin = 10f;
		public static float ScaleMax = 14f;

		static Class_BlueGiant()
		{
			Textures.Add(Engine.Star_BlueGiant);
		}

		public static Instance_Body Generate()
		{
			double Mass = (MassMin + (StellarClassifications.Rand.NextDouble() * (MassMax - MassMin))) * Math.Pow(10, 30);
			double Temperature = (TemperatureMin + ((float)StellarClassifications.Rand.NextDouble() * (TemperatureMax - TemperatureMin)));
			float Scale = (ScaleMin + ((float)StellarClassifications.Rand.NextDouble() * (ScaleMax - ScaleMin)));

			return new Instance_Body(Type, Textures[StellarClassifications.Rand.Next(0, Textures.Count - 1)], Vector2d.Zero, 0f, Scale, Mass, Temperature, 0d);
		}

	}

	public static class Class_Desert
	{
		//public static List<string> Names = new List<string>();

		//public static StellarClassifications.Planet_Types Type = StellarClassifications.Planet_Types.ClassH;
		public static StellarClassifications.Body_Types Type = StellarClassifications.Body_Types.Planet;

		public static int Rarity = 17;

		public static List<Texture2D> Textures = new List<Texture2D>();

		public static double MassMin = 1d;
		public static double MassMax = 1d;

		public static double TemperatureMin = 0f;
		public static double TemperatureMax = 0f;

		public static float ScaleMin = 0.532f;
		public static float ScaleMax = 1.1f;

		public static double RadiusMin = 107477000d;
		public static double RadiusMax = 108939000d;

		static Class_Desert()
		{
			Textures.Add(Engine.Planet_ClassH_Delvor);
		}

		public static Instance_Body Generate()
		{
			double Radius = (RadiusMin + ((float)StellarClassifications.Rand.NextDouble() * (RadiusMax - RadiusMin)));
			Vector2d OrbitRadius = new Vector2d(Radius, Radius);

			float OrbitSpeed = (float)(Engine.Rand.NextDouble() / 4d);

			float Scale = (ScaleMin + ((float)StellarClassifications.Rand.NextDouble() * (ScaleMax - ScaleMin)));
			double Mass = (MassMin + (StellarClassifications.Rand.NextDouble() * (MassMax - MassMin))) * Scale * Math.Pow(10, 23);
			double Temperature = (TemperatureMin + ((float)StellarClassifications.Rand.NextDouble() * (TemperatureMax - TemperatureMin)));


			return new Instance_Body(Type, Textures[StellarClassifications.Rand.Next(0, Textures.Count - 1)], OrbitRadius, OrbitSpeed, Scale, Mass, Temperature, 0d);
		}

	}
	public static class Class_Terrestrial
	{
		//public static List<string> Names = new List<string>();

		//public static StellarClassifications.Planet_Types Type = StellarClassifications.Planet_Types.ClassM;
		public static StellarClassifications.Body_Types Type = StellarClassifications.Body_Types.Planet;

		public static int Rarity = 17;

		public static List<Texture2D> Textures = new List<Texture2D>();

		public static double MassMin = 1d;
		public static double MassMax = 1d;

		public static double TemperatureMin = 0f;
		public static double TemperatureMax = 0f;

		public static float ScaleMin = 0.9f;
		public static float ScaleMax = 5f;

		public static double RadiusMin = 147098290d;
		public static double RadiusMax = 152098232d;

		static Class_Terrestrial()
		{
			Textures.Add(Engine.Planet_ClassM_Etho);
		}

		public static Instance_Body Generate()
		{
			double Radius = (RadiusMin + ((float)StellarClassifications.Rand.NextDouble() * (RadiusMax - RadiusMin)));
			Vector2d OrbitRadius = new Vector2d(Radius, Radius);

			float OrbitSpeed = (float)(Engine.Rand.NextDouble() / 4d);

			float Scale = (ScaleMin + ((float)StellarClassifications.Rand.NextDouble() * (ScaleMax - ScaleMin)));
			double Mass = (MassMin + (StellarClassifications.Rand.NextDouble() * (MassMax - MassMin))) * Scale * Math.Pow(10, 24);
			double Temperature = (TemperatureMin + ((float)StellarClassifications.Rand.NextDouble() * (TemperatureMax - TemperatureMin)));


			return new Instance_Body(Type, Textures[StellarClassifications.Rand.Next(0, Textures.Count - 1)], OrbitRadius, OrbitSpeed, Scale, Mass, Temperature, 0d);
		}

	}
	public static class Class_WaterWorld
	{
		//public static List<string> Names = new List<string>();

		//public static StellarClassifications.Planet_Types Type = StellarClassifications.Planet_Types.ClassO;
		public static StellarClassifications.Body_Types Type = StellarClassifications.Body_Types.Planet;

		public static int Rarity = 17;

		public static List<Texture2D> Textures = new List<Texture2D>();

		public static double MassMin = 1d;
		public static double MassMax = 1d;

		public static double TemperatureMin = 0f;
		public static double TemperatureMax = 0f;

		public static float ScaleMin = 0.4f;
		public static float ScaleMax = 4f;

		public static double RadiusMin = 130098290d;
		public static double RadiusMax = 142098232d;

		static Class_WaterWorld()
		{
			Textures.Add(Engine.Planet_ClassO_Serine);
		}

		public static Instance_Body Generate()
		{
			double Radius = (RadiusMin + ((float)StellarClassifications.Rand.NextDouble() * (RadiusMax - RadiusMin)));
			Vector2d OrbitRadius = new Vector2d(Radius, Radius);

			float OrbitSpeed = (float)(Engine.Rand.NextDouble() / 4d);

			float Scale = (ScaleMin + ((float)StellarClassifications.Rand.NextDouble() * (ScaleMax - ScaleMin)));
			double Mass = (MassMin + (StellarClassifications.Rand.NextDouble() * (MassMax - MassMin))) * Scale * Math.Pow(10, 24);
			double Temperature = (TemperatureMin + ((float)StellarClassifications.Rand.NextDouble() * (TemperatureMax - TemperatureMin)));


			return new Instance_Body(Type, Textures[StellarClassifications.Rand.Next(0, Textures.Count - 1)], OrbitRadius, OrbitSpeed, Scale, Mass, Temperature, 0d);
		}

	}
	public static class Class_IceWorld
	{
		//public static List<string> Names = new List<string>();

		//public static StellarClassifications.Planet_Types Type = StellarClassifications.Planet_Types.ClassP;
		public static StellarClassifications.Body_Types Type = StellarClassifications.Body_Types.Planet;

		public static int Rarity = 17;

		public static List<Texture2D> Textures = new List<Texture2D>();

		public static double MassMin = 1d;
		public static double MassMax = 1d;

		public static double TemperatureMin = 0f;
		public static double TemperatureMax = 0f;

		public static float ScaleMin = 0.2f;
		public static float ScaleMax = 0.9f;

		public static double RadiusMin = 3004419704d;
		public static double RadiusMax = 4553946490d;

		static Class_IceWorld()
		{
			Textures.Add(Engine.Planet_ClassP_Antasia);
		}

		public static Instance_Body Generate()
		{
			double Radius = (RadiusMin + ((float)StellarClassifications.Rand.NextDouble() * (RadiusMax - RadiusMin)));
			Vector2d OrbitRadius = new Vector2d(Radius, Radius);

			float OrbitSpeed = (float)(Engine.Rand.NextDouble() / 4d);

			float Scale = (ScaleMin + ((float)StellarClassifications.Rand.NextDouble() * (ScaleMax - ScaleMin)));
			double Mass = (MassMin + (StellarClassifications.Rand.NextDouble() * (MassMax - MassMin))) * Scale * Math.Pow(10, 24);
			double Temperature = (TemperatureMin + ((float)StellarClassifications.Rand.NextDouble() * (TemperatureMax - TemperatureMin)));


			return new Instance_Body(Type, Textures[StellarClassifications.Rand.Next(0, Textures.Count - 1)], OrbitRadius, OrbitSpeed, Scale, Mass, Temperature, 0d);
		}

	}
	public static class Class_HellWorld
	{
		//public static List<string> Names = new List<string>();

		//public static StellarClassifications.Planet_Types Type = StellarClassifications.Planet_Types.ClassY;
		public static StellarClassifications.Body_Types Type = StellarClassifications.Body_Types.Planet;

		public static int Rarity = 17;

		public static List<Texture2D> Textures = new List<Texture2D>();

		public static double MassMin = 1d;
		public static double MassMax = 1d;

		public static double TemperatureMin = 0f;
		public static double TemperatureMax = 0f;

		public static float ScaleMin = 0.9f;
		public static float ScaleMax = 5f;

		public static double RadiusMin = 36001200d;
		public static double RadiusMax = 69816900d;

		static Class_HellWorld()
		{
			Textures.Add(Engine.Planet_ClassY_Voshnoy);
		}

		public static Instance_Body Generate()
		{
			double Radius = (RadiusMin + ((float)StellarClassifications.Rand.NextDouble() * (RadiusMax - RadiusMin)));
			Vector2d OrbitRadius = new Vector2d(Radius, Radius);

			float OrbitSpeed = (float)(Engine.Rand.NextDouble() / 4d);

			float Scale = (ScaleMin + ((float)StellarClassifications.Rand.NextDouble() * (ScaleMax - ScaleMin)));
			double Mass = (MassMin + (StellarClassifications.Rand.NextDouble() * (MassMax - MassMin))) * Scale * Math.Pow(10, 24);
			double Temperature = (TemperatureMin + ((float)StellarClassifications.Rand.NextDouble() * (TemperatureMax - TemperatureMin)));


			return new Instance_Body(Type, Textures[StellarClassifications.Rand.Next(0, Textures.Count - 1)], OrbitRadius, OrbitSpeed, Scale, Mass, Temperature, 0d);
		}

	}
	public static class Class_GasGiant
	{
		//public static List<string> Names = new List<string>();

		//public static StellarClassifications.Planet_Types Type = StellarClassifications.Planet_Types.ClassJ;
		public static StellarClassifications.Body_Types Type = StellarClassifications.Body_Types.Planet;

		public static int Rarity = 17;

		public static List<Texture2D> Textures = new List<Texture2D>();

		public static double MassMin = 1d;
		public static double MassMax = 1d;

		public static double TemperatureMin = 0f;
		public static double TemperatureMax = 0f;

		public static float ScaleMin = 8f;
		public static float ScaleMax = 14f;

		public static double RadiusMin = 816520800d;
		public static double RadiusMax = 1513325783d;

		static Class_GasGiant()
		{
			Textures.Add(Engine.Planet_ClassJ_Fash);
		}

		public static Instance_Body Generate()
		{
			double Radius = (RadiusMin + ((float)StellarClassifications.Rand.NextDouble() * (RadiusMax - RadiusMin)));
			Vector2d OrbitRadius = new Vector2d(Radius, Radius);

			float OrbitSpeed = (float)(Engine.Rand.NextDouble() / 4d);

			float Scale = (ScaleMin + ((float)StellarClassifications.Rand.NextDouble() * (ScaleMax - ScaleMin)));
			double Mass = (MassMin + (StellarClassifications.Rand.NextDouble() * (MassMax - MassMin))) * Scale * Math.Pow(10, 24);
			double Temperature = (TemperatureMin + ((float)StellarClassifications.Rand.NextDouble() * (TemperatureMax - TemperatureMin)));


			return new Instance_Body(Type, Textures[StellarClassifications.Rand.Next(0, Textures.Count - 1)], OrbitRadius, OrbitSpeed, Scale, Mass, Temperature, 0d);
		}

	}

	public class Instance_Body
	{
		//public string Name = string.Empty;

		public StellarClassifications.Body_Types Type = StellarClassifications.Body_Types.Unknown;

		public Texture2D Texture = null;

		public Vector2d OrbitRadius = Vector2d.Zero;

		public float OrbitSpeed = 1f;

		public float Scale = 1f;

		public double Mass = 1d;

		public double CoreTemperature = 0d;
		public double SurfaceTemperature = 0d;

		public Instance_Body() { }
		public Instance_Body(StellarClassifications.Body_Types type, Texture2D texture, Vector2d orbitRadius, float orbitSpeed, float scale, double mass, double coreTemperature, double surfaceTemperature)
		{
			Type = type;
			Texture = texture;
			OrbitRadius = orbitRadius;
			OrbitSpeed = orbitSpeed;
			Scale = scale;
			Mass = mass;
			CoreTemperature = coreTemperature;
			SurfaceTemperature = surfaceTemperature;
		}
	}
	//public class Instance_Star : Instance_Body
	//{
	//	public Instance_Star() { }
	//	public Instance_Star(Texture2D texture, float scale, double mass, double temperature)
	//		: base(texture, Vector2d.Zero, 0f, scale, mass, temperature)
	//	{

	//	}
	//}
	//public class Instance_Planet : Instance_Body
	//{
	//	public Instance_Planet() { }
	//	public Instance_Planet(Texture2D texture, Vector2d orbitRadius, float orbitSpeed, float scale, double mass, double temperature)
	//		: base(texture, orbitRadius, orbitSpeed, scale, mass, temperature)
	//	{

	//	}
	//}


}
*/