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
using ExplorationEngine.Components;
#endregion

namespace ExplorationEngine
{
	public class StellarClassifications
	{
		//Random Generator
		public Random Rand = new Random();

		//Default classifications
		public List<Classification> DefaultClassifications;

		public List<Classification> Classifications;

		public enum Body_Classes
		{
			Unknown = 0,
			Star = 1,
			Planet = 2,
			Planetoid = 3,
			Asteroid = 4,
			Comet = 5
		}
		public enum Body_Types
		{
			Unknown = 0,
			WhiteDwarf = 1,
			MainSequence = 2,
			RedGiant = 3,
			BlueGiant = 4,
			ClassH = 5,
			ClassJ = 6,
			ClassM = 7,
			ClassO = 8,
			ClassP = 9,
			ClassY = 10
		}


		public StellarClassifications()
		{
			//Lists
			DefaultClassifications = new List<Classification>();
			Classifications = new List<Classification>();


			//Star - MainSequence
			Classification MainSequence = new Classification();
				MainSequence.Class = StellarClassifications.Body_Classes.Star;
				MainSequence.Type = StellarClassifications.Body_Types.MainSequence;
				MainSequence.Rarity = 87;

				MainSequence.Textures = new List<Texture2D>()
				{
					Engine.Star_YellowMainSequence
				};

				MainSequence.MassExponent = Math.Pow(10, 30);
				MainSequence.MassMin = 0.8d;
				MainSequence.MassMax = 1.04d;

				MainSequence.CoreTemperatureMin = 14843720d;
				MainSequence.CoreTemperatureMax = 17245861d;

				MainSequence.SurfaceTemperatureMin = 4900d;
				MainSequence.SurfaceTemperatureMax = 6200d;

				MainSequence.ScaleMin = 800f;
				MainSequence.ScaleMax = 1200f;
			DefaultClassifications.Add(MainSequence);

			//Star - WhiteDwarf
			Classification WhiteDwarf = new Classification();
				WhiteDwarf.Class = StellarClassifications.Body_Classes.Star;
				WhiteDwarf.Type = StellarClassifications.Body_Types.WhiteDwarf;
				WhiteDwarf.Rarity = 6;


				WhiteDwarf.Textures = new List<Texture2D>()
				{
					Engine.Star_WhiteDwarf
				};

				WhiteDwarf.MassExponent = Math.Pow(10, 30);
				WhiteDwarf.MassMin = 1.4d;
				WhiteDwarf.MassMax = 2.1d;

				WhiteDwarf.CoreTemperatureMin = 984201d;
				WhiteDwarf.CoreTemperatureMax = 172004d;

				WhiteDwarf.SurfaceTemperatureMin = 90000d;
				WhiteDwarf.SurfaceTemperatureMax = 140000d;

				WhiteDwarf.ScaleMin = 200f;
				WhiteDwarf.ScaleMax = 400f;
			DefaultClassifications.Add(WhiteDwarf);

			//Star - RedGiant
			Classification RedGiant = new Classification();
				RedGiant.Class = StellarClassifications.Body_Classes.Star;
				RedGiant.Type = StellarClassifications.Body_Types.RedGiant;
				RedGiant.Rarity = 5;


				RedGiant.Textures = new List<Texture2D>()
				{
					Engine.Star_RedGiant
				};

				RedGiant.MassExponent = Math.Pow(10, 30);
				RedGiant.MassMin = 0.08d;
				RedGiant.MassMax = 0.45d;

				RedGiant.CoreTemperatureMin = 580870040d;
				RedGiant.CoreTemperatureMax = 628400303d;

				RedGiant.SurfaceTemperatureMin = 2000d;
				RedGiant.SurfaceTemperatureMax = 4000d;

				RedGiant.ScaleMin = 4200f;
				RedGiant.ScaleMax = 5300f;
			DefaultClassifications.Add(RedGiant);

			//Star - BlueGiant
			Classification BlueGiant = new Classification();
				BlueGiant.Class = StellarClassifications.Body_Classes.Star;
				BlueGiant.Type = StellarClassifications.Body_Types.BlueGiant;
				BlueGiant.Rarity = 2;


				BlueGiant.Textures = new List<Texture2D>()
				{
					Engine.Star_BlueGiant
				};

				BlueGiant.MassExponent = Math.Pow(10, 30);
				BlueGiant.MassMin = 16d;
				BlueGiant.MassMax = 20d;

				BlueGiant.CoreTemperatureMin = 14803824d;
				BlueGiant.CoreTemperatureMax = 22245092d;

				BlueGiant.SurfaceTemperatureMin = 18000d;
				BlueGiant.SurfaceTemperatureMax = 26000d;

				BlueGiant.ScaleMin = 10000f;
				BlueGiant.ScaleMax = 16000f;
			DefaultClassifications.Add(BlueGiant);



			//Planet - ClassH(Desert)
			Classification Delvor = new Classification();
				Delvor.Class = StellarClassifications.Body_Classes.Planet;
				Delvor.Type = StellarClassifications.Body_Types.ClassH;
				Delvor.Rarity = 1;

				Delvor.Textures = new List<Texture2D>()
				{
					Engine.Planet_ClassH_Delvor
				};

				Delvor.MassExponent = Math.Pow(10, 23);
				Delvor.MassMin = 0.8d;
				Delvor.MassMax = 1.4d;

				Delvor.CoreTemperatureMin = 5000d;
				Delvor.CoreTemperatureMax = 6000d;

				Delvor.SurfaceTemperatureMin = -200d;
				Delvor.SurfaceTemperatureMax = 10d;

				Delvor.AngularVelocityMin = 0.3f;
				Delvor.AngularVelocityMax = 1.3f;

				Delvor.ScaleMin = 5.32f;
				Delvor.ScaleMax = 11f;

				Delvor.RadiusMin = 107477000d;
				Delvor.RadiusMax = 108939000d;

				Delvor.OrbitSpeedMin = 0.5f;
				Delvor.OrbitSpeedMax = 3f;
			DefaultClassifications.Add(Delvor);

			//Planet - ClassM(Terrestrial)
			Classification Etho = new Classification();
				Etho.Class = StellarClassifications.Body_Classes.Planet;
				Etho.Type = StellarClassifications.Body_Types.ClassM;
				Etho.Rarity = 1;

				Etho.Textures = new List<Texture2D>()
				{
					Engine.Planet_ClassM_Etho
				};

				Etho.MassExponent = Math.Pow(10, 24);
				Etho.MassMin = 0.7d;
				Etho.MassMax = 4d;

				Etho.CoreTemperatureMin = 5000d;
				Etho.CoreTemperatureMax = 6000d;

				Etho.SurfaceTemperatureMin = 4d;
				Etho.SurfaceTemperatureMax = 60d;

				Etho.AngularVelocityMin = 0.3f;
				Etho.AngularVelocityMax = 1.3f;

				Etho.ScaleMin = 9f;
				Etho.ScaleMax = 50f;

				Etho.RadiusMin = 147098290d;
				Etho.RadiusMax = 152098232d;

				Etho.OrbitSpeedMin = 0.5f;
				Etho.OrbitSpeedMax = 3f;
			DefaultClassifications.Add(Etho);

			//Planet - ClassO(WaterWorld)
			Classification Serine = new Classification();
				Serine.Class = StellarClassifications.Body_Classes.Planet;
				Serine.Type = StellarClassifications.Body_Types.ClassO;
				Serine.Rarity = 1;

				Serine.Textures = new List<Texture2D>()
				{
					Engine.Planet_ClassO_Serine
				};
				
				Serine.MassExponent = Math.Pow(10, 24);
				Serine.MassMin = 3d;
				Serine.MassMax = 12d;
				
				Serine.CoreTemperatureMin = 4800d;
				Serine.CoreTemperatureMax = 5600d;
				
				Serine.SurfaceTemperatureMin = -10d;
				Serine.SurfaceTemperatureMax = 80d;
				
				Serine.AngularVelocityMin = 0.3f;
				Serine.AngularVelocityMax = 1.3f;
				
				Serine.ScaleMin = 6f;
				Serine.ScaleMax = 90f;
				
				Serine.RadiusMin = 130098290d;
				Serine.RadiusMax = 142098232d;
				
				Serine.OrbitSpeedMin = 0.5f;
				Serine.OrbitSpeedMax = 3f;
			DefaultClassifications.Add(Serine);

			//Planet - ClassP(IceWorld)
			Classification Antasia = new Classification();
				Antasia.Class = StellarClassifications.Body_Classes.Planet;
				Antasia.Type = StellarClassifications.Body_Types.ClassP;
				Antasia.Rarity = 1;

				Antasia.Textures = new List<Texture2D>()
				{
					Engine.Planet_ClassP_Antasia
				};

				Antasia.MassExponent = Math.Pow(10, 24);
				Antasia.MassMin = 0.01d;
				Antasia.MassMax = 1d;

				Antasia.CoreTemperatureMin = 500d;
				Antasia.CoreTemperatureMax = 4000d;

				Antasia.SurfaceTemperatureMin = -230d;
				Antasia.SurfaceTemperatureMax = 4d;

				Antasia.AngularVelocityMin = 0.3f;
				Antasia.AngularVelocityMax = 1.3f;

				Antasia.ScaleMin = 0.4f;
				Antasia.ScaleMax = 9f;

				Antasia.RadiusMin = 3004419704d;
				Antasia.RadiusMax = 4553946490d;

				Antasia.OrbitSpeedMin = 0.5f;
				Antasia.OrbitSpeedMax = 3f;
			DefaultClassifications.Add(Antasia);

			//Planet - ClassY(HellWorld)
			Classification Voshnoy = new Classification();
				Voshnoy.Class = StellarClassifications.Body_Classes.Planet;
				Voshnoy.Type = StellarClassifications.Body_Types.ClassY;
				Voshnoy.Rarity = 1;

				Voshnoy.Textures = new List<Texture2D>()
				{
					Engine.Planet_ClassY_Voshnoy
				};

				Voshnoy.MassExponent = Math.Pow(10, 24);
				Voshnoy.MassMin = 1d;
				Voshnoy.MassMax = 1d;

				Voshnoy.CoreTemperatureMin = 6000d;
				Voshnoy.CoreTemperatureMax = 8000d;

				Voshnoy.SurfaceTemperatureMin = 380d;
				Voshnoy.SurfaceTemperatureMax = 620d;

				Voshnoy.AngularVelocityMin = 0.3f;
				Voshnoy.AngularVelocityMax = 1.3f;

				Voshnoy.ScaleMin = 9f;
				Voshnoy.ScaleMax = 50f;

				Voshnoy.RadiusMin = 36001200d;
				Voshnoy.RadiusMax = 69816900d;

				Voshnoy.OrbitSpeedMin = 0.5f;
				Voshnoy.OrbitSpeedMax = 3f;
			DefaultClassifications.Add(Voshnoy);

			//Planet - ClassJ(GasGiant)
			Classification Fash = new Classification();
				Fash.Class = StellarClassifications.Body_Classes.Planet;
				Fash.Type = StellarClassifications.Body_Types.ClassJ;
				Fash.Rarity = 1;

				Fash.Textures = new List<Texture2D>()
				{
					Engine.Planet_ClassJ_Fash
				};

				Fash.MassExponent = Math.Pow(10, 27);
				Fash.MassMin = 0.7d;
				Fash.MassMax = 12d;

				Fash.CoreTemperatureMin = 20000d;
				Fash.CoreTemperatureMax = 28000d;

				Fash.SurfaceTemperatureMin = -300d;
				Fash.SurfaceTemperatureMax = -50d;

				Fash.AngularVelocityMin = 0.1f;
				Fash.AngularVelocityMax = 1.0f;

				Fash.ScaleMin = 80f;
				Fash.ScaleMax = 140f;

				Fash.RadiusMin = 816520800d;
				Fash.RadiusMax = 1513325783d;

				Fash.OrbitSpeedMin = 0.5f;
				Fash.OrbitSpeedMax = 3f;
			DefaultClassifications.Add(Fash);


			//Make our classifications default
			Classifications = DefaultClassifications;
		}

		public void Save()
		{
			//Save and load classifications
			bool CouldLoad = Engine.saveLoad.LoadClassifications();
			if (!CouldLoad)
			{
				Engine.saveLoad.SaveClassifications();
			}
		}

		public BaseEntity GenerateBody(SolarSystem parentSystem, Body_Classes Class, Body_Types type = Body_Types.Unknown)
		{
			//Define our entity
			BaseEntity Entity = null;


			//If any type of the specified class is to be chosen
			if (type == Body_Types.Unknown)
			{
				//This list will determine the frequency of occurance for all classifications of the same class
				List<Body_Types> Weight = new List<Body_Types>();

				//Find all classifications in the default list that are of the same class as specified
				List<Classification> AllOfClass = new List<Classification>();
				for (int ii = 0; ii < Classifications.Count; ii++)
				{
					if (Classifications[ii].Class == Class)
					{
						AllOfClass.Add(Classifications[ii]);
					}
				}

				//Add an instance of the Body_Types to a list X times, where X is the rarity of that classification
				for (int ii = 0; ii < AllOfClass.Count; ii++)
				{
					for (int iii = 0; iii < AllOfClass[ii].Rarity; iii++)
					{
						Weight.Add(AllOfClass[ii].Type);
					}
				}

				//Randomly pick a type weighted based on rarity
				type = Weight[Rand.Next(0, Weight.Count - 1)];
			}

			//Find a classification that matches the specified type within the Classifications list
			for (int ii = 0; ii < Classifications.Count; ii++)
			{
				if (Classifications[ii].Type == type)
				{
					Entity = Classifications[ii].Generate(parentSystem);
				}
			}

			return Entity;
		}
	}

	public class Classification
	{
		//Random Generator
		public Random Rand = new Random();


		//public StellarClassifications.Star_Types Type = StellarClassifications.Star_Types.MainSequence;
		public StellarClassifications.Body_Classes Class = StellarClassifications.Body_Classes.Unknown;
		public StellarClassifications.Body_Types Type = StellarClassifications.Body_Types.Unknown;

		public int Rarity = 1;

		[System.Xml.Serialization.XmlIgnore]
		public List<Texture2D> Textures = new List<Texture2D>();
		public List<Engine.TextureIndexes> TextureIndexes = new List<Engine.TextureIndexes>();

		public double MassExponent = Math.Pow(1, 1);
		public double MassMin = 1d;
		public double MassMax = 1d;

		public double CoreTemperatureMin = 0d;
		public double CoreTemperatureMax = 0d;

		public double SurfaceTemperatureMin = 0d;
		public double SurfaceTemperatureMax = 0d;

		public float ScaleMin = 0f;
		public float ScaleMax = 0f;

		public float AngularVelocityMin = 0f;
		public float AngularVelocityMax = 0f;

		public float OrbitSpeedMin = 0f;
		public float OrbitSpeedMax = 0f;

		public double RadiusMin = 0d;
		public double RadiusMax = 0d;


		public Classification() {}

		public BaseEntity Generate(SolarSystem parentSystem)
		{
			//Define texture
				Texture2D Texture = Textures[Rand.Next(0, Textures.Count - 1)];
			//Define mass
				double Mass = (MassMin + (Rand.NextDouble() * (MassMax - MassMin))) * MassExponent;
			//Define temperature
				double CoreTemperature = (CoreTemperatureMin + (Rand.NextDouble() * (CoreTemperatureMax - CoreTemperatureMin)));
				double SurfaceTemperature = (SurfaceTemperatureMin + (Rand.NextDouble() * (SurfaceTemperatureMax - SurfaceTemperatureMin)));
			//Define scale
				float Scale = (ScaleMin + ((float)Rand.NextDouble() * (ScaleMax - ScaleMin)));
			//Define Angular velocity
				float AngularVelocity = (AngularVelocityMin + ((float)Rand.NextDouble() * (AngularVelocityMax - AngularVelocityMin)));
			//Define radius
				double Radius = (RadiusMin + (Rand.NextDouble() * (RadiusMax - RadiusMin)));
				Vector2d OrbitRadius = new Vector2d(Radius, Radius);
			//Define orbit speed
				//float OrbitSpeed = (float)(StellarClassifications.Rand.NextDouble() / 4d);
				float OrbitSpeed = (OrbitSpeedMin + ((float)Rand.NextDouble() * (OrbitSpeedMax - OrbitSpeedMin)));


			//Generate BaseEntity
			BaseEntity entity = Galaxy.CreateSolarBody(parentSystem.Name + "_" + Class.ToString() + "_" + parentSystem.Entities.Count.ToString(),//Name
				(parentSystem.Entities.Count > 0 ? parentSystem.Entities[0] : null), parentSystem.Name,         //Parent, ParentSystem Name
				Texture, null,																	                    //Texture, debug texture
				Scale, Mass,																					                            //Scale, Mass
				Vector2d.Zero, Vector2d.Zero,																					            //Position, Velocity 
				OrbitRadius, Vector2d.Zero, OrbitSpeed,														                                //OrbitRadius, OrbitOffset, Orbit Speed
				0f, AngularVelocity * (float)Rand.NextDouble() * ((float)Rand.NextDouble() < 0.1 ? 1 : -1) * (MathHelper.Pi / 180),
				Engine.stellarClassifications.Rand.Next(0, 360));	//Angle, AngularVelocity, InitialAngleToParent

			//Assign its temperatures
				entity.BodyLogic.CoreTemperature = CoreTemperature;
				entity.BodyLogic.SurfaceTemperature = SurfaceTemperature;
			//Assign its class and type
				entity.BodyLogic.Class = Class;
				entity.BodyLogic.Type = Type;

			//Add orbit logic
				//entity.Orbit = new OrbitComponent(ent, parent, orbitRadius, orbitOffset, orbitSpeed, initialOrbitAngle);

				//System.Windows.Forms.MessageBox.Show("Generate " + Type.ToString());
			return entity;
		}

	}

}
