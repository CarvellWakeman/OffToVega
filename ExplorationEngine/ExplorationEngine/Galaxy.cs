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
using ExplorationEngine;
using ExplorationEngine.Components;
using ExplorationEngine.Containers;
#endregion

namespace ExplorationEngine
{
	public static class Galaxy
	{
		public static string Name;
		public static double TotalMass;

		//Solar Systems
			public static List<SolarSystem> SolarSystems = new List<SolarSystem>();
			public static SolarSystem CurrentSolarSystem;

		//Entities		
		public static List<BaseEntity> Entities = new List<BaseEntity>();
		private static Dictionary<string, BaseEntity> EntitiesString = new Dictionary<string, BaseEntity>();

		public static List<Container_Ship> Ships = new List<Container_Ship>();

		public static float MaxZ = 100000;
		public static float MaxLayer = MaxZ + 1;
		public static float ParticleZModifier = 0.00001f;
		public static float ShadowZModifier = 0.00001f;
		public static float DebugZModifier = 0.00001f;



		static Galaxy()
		{

		}


		public static BaseEntity CreateEntity(string name)
		{
			//Create the entity
			BaseEntity ent = new BaseEntity(name, Vector2d.Zero, 0f, 1f);

			ent.IsActive = true;

			//Add the entity to our list of entities
			Entities.Add(ent);
			EntitiesString.Add(name, ent);

			//Return the entity
			return ent;
		}
		public static BaseEntity CreateEntity(SolarSystem parentsolarsystem, string name)
		{
			//Create the entity
			BaseEntity ent = new BaseEntity(name, Vector2d.Zero, 0f, 1f);

			ent.IsActive = true;

			//Add the entity to our list of entities
			Entities.Add(ent);
			EntitiesString.Add(name, ent);

			//Add the entity to its parent solarsystem for updating
			parentsolarsystem.Entities.Add(ent);
			ent.SolarSystem = parentsolarsystem;

			//Return the entity
			return ent;
		}

		public static void Destroy(BaseEntity entity)
		{
			Galaxy.TotalMass -= entity.Mass;

			Entities.Remove(entity);
			EntitiesString.Remove(entity.Name);

			entity.SelfDestruct();
			entity = null;
		}
		public static void DestroyEntity(int index)
		{
			if (Entities.Count > 0)
			{
				Destroy(Entities[index]);
			}
		}
		public static void DestroyEntity(string name)
		{
			if (Entities.Count > 0)
			{
				//foreach (BaseEntity e in Entities)
				//{
				//	if (e.Name == name)
				//	{
				//		Destroy(e);
				//	}
				//}
				if (EntitiesString[name] != null)
				{
					Destroy(EntitiesString[name]);
				}
			}
		}
		public static void DestroyAllEntities()
		{
			if (Entities.Count > 0)
			{
				for (int ii = 0; ii < Entities.Count; ii++)
				{
					Destroy(Entities[ii]);
				}

				Entities = new List<BaseEntity>();
				EntitiesString = new Dictionary<string, BaseEntity>();
			}
		}

		public static BaseEntity CreateShip(string name, SolarSystem parentSolarSystem, double mass, float scale, Texture2D texture, Vector2d position, float angle, Vector2d velocity, float angularVelocity, bool isControlled)
		{
			if (EntityLookup(name) != null)
			{
				System.Windows.Forms.MessageBox.Show("[CreateShip]ERROR: Entity names match");
				return null;
			}
			else
			{
				//Queue our entity to be made
				BaseEntity ent = Galaxy.CreateEntity(parentSolarSystem, name);

				//Create a renderer
				ent.Renderer = new Component_Render(ent, Engine.Thruster);
				ent.Z = 1;

				//Set properties
				ent.Mass = mass;
				ent.Scale = scale;

				ent.Position = position;
				ent.Angle = angle;

				ent.Velocity = velocity;
				ent.AngularVelocity = angularVelocity;

				//Add ship logic
				ent.ShipLogic = new Component_ShipLogic(ent);
				ent.ShipLogic.IsControlled = isControlled;

				//Add particle emitters
				ent.Particle = new Component_Particle(ent);
				ent.Particle.CreateEmitter(ent, new Vector2d(0.64f, 0.20f), Input.Move_Forward);
				ent.Particle.CreateEmitter(ent, new Vector2d(-0.64f, 0.20f), Input.Move_Forward);


				//Add orbit logic
				//BaseEntity parent = CurrentSolarSystem.Entities[0];
				//ent.Orbit = new OrbitComponent(ent, parent, (parent.Renderer != null ? new Vector2d(parent.Renderer._texture.Width / 2 * parent.Scale, parent.Renderer._texture.Height / 2 * parent.Scale) : new Vector2d(1000, 1000)), Vector2d.Zero, (float)Engine.Rand.Next(2, 6) / 2f, 0f);






				if (true)
				{
					//Create a temporary ship.
					BaseEntity ent2 = CreateEntity(parentSolarSystem, name + "2_Communications");
					ent2.Scale = scale;
					ent2.Renderer = new Component_Render(ent2, Engine.LifeSupport);
					ent2.Z = 3;
					ent2.SetParent(ent);
					ent2.ParentOffset = new Vector2d(0, -256) * scale;
					ent2.Communication = new ShipSystem_Communication(ent2);

					BaseEntity ent3 = CreateEntity(parentSolarSystem, name + "3");
					ent3.Scale = scale;
					ent3.Renderer = new Component_Render(ent3, Engine.Generator);
					ent3.Z = 4;
					ent3.SetParent(ent2);
					ent3.ParentOffset = new Vector2d(0, -230) * scale;

					BaseEntity ent4 = CreateEntity(parentSolarSystem, name + "4");
					ent4.Scale = scale;
					ent4.Renderer = new Component_Render(ent4, Engine.Cargo);
					ent4.Z = 2;
					ent4.SetParent(ent2);
					ent4.ParentOffset = new Vector2d(64, 0) * scale;

					BaseEntity ent5 = CreateEntity(parentSolarSystem, name + "5");
					ent5.Scale = scale;
					ent5.Renderer = new Component_Render(ent5, Engine.Cargo);
					ent5.Z = 2;
					ent5.SetParent(ent2);
					ent5.ParentOffset = new Vector2d(-64, 0) * scale;

					BaseEntity ent6 = CreateEntity(parentSolarSystem, name + "6");
					ent6.Scale = scale;
					ent6.Renderer = new Component_Render(ent6, Engine.Chassis1);
					ent6.Z = 6;
					ent6.SetParent(ent);
					ent6.ParentOffset = new Vector2d(0, -128) * scale;

					BaseEntity ent7 = CreateEntity(parentSolarSystem, name + "7");
					ent7.Scale = scale;
					ent7.Renderer = new Component_Render(ent7, Engine.Chassis1);
					ent7.Z = 6;
					ent7.SetParent(ent);
					ent7.ParentOffset = new Vector2d(0, 128) * scale;

					BaseEntity ent8 = CreateEntity(parentSolarSystem, name + "8");
					ent8.Scale = scale;
					ent8.Renderer = new Component_Render(ent8, Engine.Radiator);
					ent8.Z = 7;
					ent8.SetParent(ent7);
					ent8.ParentOffset = new Vector2d(-64, 192) * scale;

					BaseEntity ent9 = CreateEntity(parentSolarSystem, name + "9");
					ent9.Scale = scale;
					ent9.Renderer = new Component_Render(ent9, Engine.Radiator);
					ent9.Z = 7;
					ent9.SetParent(ent7);
					ent9.ParentOffset = new Vector2d(64, 192) * scale;

					BaseEntity ent10 = CreateEntity(parentSolarSystem, name + "10");
					ent10.Scale = scale;
					ent10.Renderer = new Component_Render(ent10, Engine.Chassis1);
					ent10.Z = 6;
					ent10.SetParent(ent7);
					ent10.ParentOffset = new Vector2d(0, 386) * scale;

					BaseEntity ent11 = CreateEntity(parentSolarSystem, name + "11");
					ent11.Scale = scale;
					ent11.Renderer = new Component_Render(ent11, Engine.Chassis2);
					ent11.Z = 6;
					ent11.SetParent(ent8);
					ent11.ParentOffset = new Vector2d(-40, 192) * scale;

					BaseEntity ent12 = CreateEntity(parentSolarSystem, name + "12");
					ent12.Scale = scale;
					ent12.Renderer = new Component_Render(ent12, Engine.Chassis2);
					ent12.Z = 6;
					ent12.SetParent(ent9);
					ent12.ParentOffset = new Vector2d(40, 192) * scale;

					BaseEntity ent13 = CreateEntity(parentSolarSystem, name + "13");
					ent13.Scale = scale;
					ent13.Renderer = new Component_Render(ent13, Engine.Radiator);
					ent13.Z = 7;
					ent13.SetParent(ent11);
					ent13.ParentOffset = new Vector2d(40, 192) * scale;

					BaseEntity ent14 = CreateEntity(parentSolarSystem, name + "14");
					ent14.Scale = scale;
					ent14.Renderer = new Component_Render(ent14, Engine.Radiator);
					ent14.Z = 7;
					ent14.SetParent(ent12);
					ent14.ParentOffset = new Vector2d(-40, 192) * scale;

					BaseEntity ent15 = CreateEntity(parentSolarSystem, name + "15");
					ent15.Scale = scale;
					ent15.Renderer = new Component_Render(ent15, Engine.LifeSupport);
					ent15.Z = 3;
					ent15.SetParent(ent10);
					ent15.ParentOffset = new Vector2d(0, 370) * scale;

					BaseEntity ent16 = CreateEntity(parentSolarSystem, name + "16");
					ent16.Scale = scale;
					ent16.Renderer = new Component_Render(ent16, Engine.Cargo);
					ent16.Z = 2;
					ent16.SetParent(ent);
					ent16.ParentOffset = new Vector2d(128, -32) * scale;

					BaseEntity ent17 = CreateEntity(parentSolarSystem, name + "17");
					ent17.Scale = scale;
					ent17.Renderer = new Component_Render(ent17, Engine.Cargo);
					ent17.Z = 2;
					ent17.SetParent(ent);
					ent17.ParentOffset = new Vector2d(-128, -32) * scale;

					BaseEntity ent18 = CreateEntity(parentSolarSystem, name + "18");
					ent18.Scale = scale;
					ent18.Renderer = new Component_Render(ent18, Engine.Cargo);
					ent18.Z = 2;
					ent18.SetParent(ent);
					ent18.ParentOffset = new Vector2d(128, 32) * scale;

					BaseEntity ent19 = CreateEntity(parentSolarSystem, name + "19");
					ent19.Scale = scale;
					ent19.Renderer = new Component_Render(ent19, Engine.Cargo);
					ent19.Z = 2;
					ent19.SetParent(ent);
					ent19.ParentOffset = new Vector2d(-128, 32) * scale;

					BaseEntity ent20 = CreateEntity(parentSolarSystem, name + "20");
					ent20.Scale = scale;
					ent20.OffsetAngle = MathHelper.Pi;
					ent20.Renderer = new Component_Render(ent20, Engine.Capasitor);
					ent20.Z = 4;
					ent20.SetParent(ent8);
					ent20.ParentOffset = new Vector2d(-128, 0) * scale;

					BaseEntity ent21 = CreateEntity(parentSolarSystem, name + "21");
					ent21.Scale = scale;
					ent21.Renderer = new Component_Render(ent21, Engine.Capasitor);
					ent21.Z = 4;
					ent21.SetParent(ent9);
					ent21.ParentOffset = new Vector2d(128, 0) * scale;

					BaseEntity ent22 = CreateEntity(parentSolarSystem, name + "22");
					ent22.Scale = scale;
					ent22.Renderer = new Component_Render(ent22, Engine.Chassis2);
					ent22.Z = 6;
					ent22.SetParent(ent18);
					ent22.ParentOffset = new Vector2d(48, -96) * scale;

					BaseEntity ent23 = CreateEntity(parentSolarSystem, name + "23");
					ent23.Scale = scale;
					ent23.Renderer = new Component_Render(ent23, Engine.Chassis2);
					ent23.Z = 6;
					ent23.SetParent(ent19);
					ent23.ParentOffset = new Vector2d(-48, -96) * scale;

					BaseEntity ent24 = CreateEntity(parentSolarSystem, name + "24");
					ent24.Scale = scale;
					ent24.Renderer = new Component_Render(ent24, Engine.Chassis2);
					ent24.Z = 6;
					ent24.SetParent(ent22);
					ent24.ParentOffset = new Vector2d(0, 256) * scale;

					BaseEntity ent25 = CreateEntity(parentSolarSystem, name + "25");
					ent25.Scale = scale;
					ent25.Renderer = new Component_Render(ent25, Engine.Chassis2);
					ent25.Z = 6;
					ent25.SetParent(ent23);
					ent25.ParentOffset = new Vector2d(0, 256) * scale;

					BaseEntity ent26 = CreateEntity(parentSolarSystem, name + "26");
					ent26.Scale = scale;
					ent26.Renderer = new Component_Render(ent26, Engine.Chassis2);
					ent26.Z = 6;
					ent26.SetParent(ent24);
					ent26.ParentOffset = new Vector2d(0, 256) * scale;

					BaseEntity ent27 = CreateEntity(parentSolarSystem, name + "27");
					ent27.Scale = scale;
					ent27.Renderer = new Component_Render(ent27, Engine.Chassis2);
					ent27.Z = 6;
					ent27.SetParent(ent25);
					ent27.ParentOffset = new Vector2d(0, 256) * scale;

					BaseEntity ent28 = CreateEntity(parentSolarSystem, name + "28");
					ent28.Scale = scale;
					ent28.Renderer = new Component_Render(ent28, Engine.Chassis2);
					ent28.Z = 6;
					ent28.SetParent(ent26);
					ent28.ParentOffset = new Vector2d(0, 256) * scale;

					BaseEntity ent29 = CreateEntity(parentSolarSystem, name + "29");
					ent29.Scale = scale;
					ent29.Renderer = new Component_Render(ent29, Engine.Chassis2);
					ent29.Z = 6;
					ent29.SetParent(ent27);
					ent29.ParentOffset = new Vector2d(0, 256) * scale;

				}


				//Ship container
				Container_Ship Ship = new Container_Ship(ent, parentSolarSystem);
				Ships.Add(Ship);


				//Add Children
				for (int ii = 0; ii < 0; ii++)
				{
					//Add child entity
					//Queue our entity to be made
					//BaseEntity ent1 = EntityManager.CreateEntity(name + "_child" + ii.ToString());

					//ent1.Scale = scale;
					//ent1.Position = position + new Vector2d(((Engine.Rand.NextDouble() * 2) - 1) * 10, ((Engine.Rand.NextDouble() * 2) - 1) * 10);
					//ent1.Angle = Engine.Rand.NextDouble() * MathHelper.TwoPi;

					//Create a renderer
					//ent1.Renderer = new RenderComponent(ent1, Engine.Ship_Serenity);
					//ent1.Renderer._debugTexture = Engine.Ship_Debug;
					//ent1.Renderer.Layer = EntityManager.Layers.Ships;

					//ent1.SetParent(ent);

					//Add ship to solarsystem
					//SolarSystemLookup(parentSolarSystem).Entities.Add(ent1.Name);
				}


				Camera.TargetObject = Ship.PositionalCenter;

				Galaxy.TotalMass += mass;

				Ship.Update();

				return ent;
			}

		}

		public static BaseEntity CreateSolarBody(string name, BaseEntity parent, string parentSolarSystem, Texture2D texture, Texture2D debugTexture, float scale, double mass, Vector2d position, Vector2d velocity, Vector2d orbitRadius, Vector2d orbitOffset, float orbitSpeed, float angle, float angularVelocity, float initialOrbitAngle)
		{
			if (EntityLookup(name) != null)
			{
				System.Windows.Forms.MessageBox.Show("[CreateBody]ERROR: Entity names match");
				return null;
			}
			else
			{
				if (!SolarSystems.Contains(SolarSystemLookup(parentSolarSystem)))
				{
					System.Windows.Forms.MessageBox.Show("[CreateBody]ERROR: Provided solar system '" + parentSolarSystem + "' for entity '" + name + "' does not exist");
					return null;
				}
				else
				{
					//Queue our entity to be made
					BaseEntity ent = CreateEntity(name);

					//ent.IsActive = true;

					//Create a renderer
					ent.Renderer = new Component_Render(ent, texture);
					ent.Renderer._debugTexture = debugTexture;

					//Set properties
					ent.Mass = mass;
					ent.Scale = scale;

					ent.Position = position;
					ent.Angle = angle;

					ent.Velocity = velocity;
					ent.AngularVelocity = angularVelocity;

					//Add solar system body logic
					ent.BodyLogic = new Component_BodyLogic(ent, Engine.Shadow);
					ent.BodyLogic.ParentSolarSystem = parentSolarSystem;
					
					//Add orbit logic
					ent.Orbit = new Component_Orbit(ent, parent, orbitRadius, orbitOffset, orbitSpeed, initialOrbitAngle);
					
					
					Galaxy.TotalMass += mass;

					SolarSystemLookup(parentSolarSystem).Entities.Add(ent);
					return ent;
				}
			}
		}

		public static SolarSystem CreateSolarSystem()
		{
			//SolarSystem name
			string Name = "SS_" + SolarSystems.Count.ToString();

			//Create SolarSystem
			SolarSystem TempSolarSystem = new SolarSystem(Name);

			//Add this solarsystem to the solarSystems list
			SolarSystems.Add(TempSolarSystem);


			//Create a sun (Binary systems to come [much] later!)
			//Body_Star NewStar = Star_Rarity.RandomBody(Rand);

			//BaseEntity Sun = CreateBody(TempSolarSystem.Name + "_s_0", null, Name, NewStar.Texture, null, 27.5f,
			//(NewStar.MassMin + (float)(Rand.NextDouble() * (NewStar.MassMax - NewStar.MassMin))) * (float)Math.Pow(10, 30),
			//Vector2d.Zero, Vector2d.Zero, Vector2d.Zero, Vector2d.Zero, 0f, 0f, 0f, 0f);
			BaseEntity Sun = Engine.stellarClassifications.GenerateBody(TempSolarSystem, StellarClassifications.Body_Classes.Star);

			//Create planets
			List<BaseEntity> GeneratedPlanets = new List<BaseEntity>();
			for (int ii = 0; ii < Engine.Rand.Next(0, 10); ii++)
			{
				BaseEntity planet = Engine.stellarClassifications.GenerateBody(TempSolarSystem, StellarClassifications.Body_Classes.Planet);

				//Body_Planet NewPlanet = Planet_Rarity.RandomBody(Engine.Rand);

				////float radius = (Sun.Renderer._texture.Width * Sun.Scale) + Rand.Next(100, 50000);
				//double radius;
				//if (ii > 0)
				//{
				//	radius = Math.Max(GeneratedPlanets[ii - 1].Orbit.OrbitRadius.X, GeneratedPlanets[ii - 1].Orbit.OrbitRadius.Y) +
				//		(GeneratedPlanets[ii - 1].Renderer._texture.Width * GeneratedPlanets[ii - 1].Scale) + 30167522;
				//}
				//else
				//{
				//	radius = (Sun.Renderer._texture.Width * Sun.Scale) + Engine.Rand.Next(100, 5000);
				//}

				//BaseEntity planet = CreateBody(TempSolarSystem.Name + "_p_" + (ii + 1).ToString(), Sun, TempSolarSystem.Name, NewPlanet.Texture, Engine.Planet_Debug_Medium, NewPlanet.Scale,
				//	(NewPlanet.MassMin + (float)(Engine.Rand.NextDouble() * (NewPlanet.MassMax - NewPlanet.MassMin))) * (float)Math.Pow(10, 24),
				//	new Vector2d(radius, 0), Vector2d.Zero,
				//	new Vector2d(radius, radius), Vector2d.Zero, (float)Engine.Rand.Next(1, 6) / 2f, 0f,
				//	Engine.Rand.Next(1, 5) / 3 * (float)Engine.Rand.NextDouble() * ((float)Engine.Rand.NextDouble() < 0.1 ? 1 : -1) * (MathHelper.Pi / 180),
				//	Engine.Rand.Next(0, 360));

				if (planet.BodyLogic != null)
				{
					planet.BodyLogic.HasShadow = true;
				}

				GeneratedPlanets.Add(planet);
			}

			//Camera
			TempSolarSystem.CameraTargetObject = Sun.Name;
			TempSolarSystem.Zoom = Camera.ZoomValues.IndexOf(0.00015625d);
			//Camera.TargetObject = Sun;
			//Camera.SetZoom(Camera.ZoomValues.IndexOf(0.06f));


			//Set this system to the current one if there are no other systems or the current system is null
			if (SolarSystems.Count <= 1 || CurrentSolarSystem == null)
			{
				SetSolarSystem(TempSolarSystem);
			}

			return TempSolarSystem;
		}



		//Switch active solarsystem
		public static void SetSolarSystem(SolarSystem newSystem)
		{
			if (CurrentSolarSystem != null)
			{
				CurrentSolarSystem.SetSystem(false);
			}

			newSystem.SetSystem(true);

			CurrentSolarSystem = newSystem;
		}

		//Destroy systems
		public static void DestroySystem(SolarSystem solarsystem)
		{
			if (SolarSystems.Count > 0)
			{
				solarsystem = null;
				SolarSystems.Remove(solarsystem);
			}
		}
		public static void DestroySystem(string name)
		{
			if (SolarSystems.Count > 0)
			{
				foreach (SolarSystem s in SolarSystems)
				{
					if (s.Name == name)
					{
						SolarSystems.Remove(s);
					}
				}
			}
		}
		public static void DestroyAllSolarSystems()
		{
			foreach (SolarSystem s in SolarSystems)
			{
				s.SelfDestruct();
			}
			SolarSystems = new List<SolarSystem>();
		}


		//Lookups
		public static SolarSystem SolarSystemLookup(string name)
		{
			SolarSystem returnval = null;

			if (SolarSystems.Count > 0)
			{
				for (int ii = 0; ii < SolarSystems.Count; ii++)
				{
					if (SolarSystems[ii].Name == name)
					{
						returnval = SolarSystems[ii];
					}
				}
			}

			return returnval;
		}
		public static BaseEntity EntityLookup(string name)
		{
			BaseEntity returnval = null;

			if (name != null)
			{
				if (Entities.Count > 0)
				{
					//for (int ii = 0; ii < Entities.Count; ii++)
					//{
					//	if (Entities[ii].Name == name)
					//	{
					//		returnval = Entities[ii];
					//	}
					//}
					if (EntitiesString.ContainsKey(name) && EntitiesString[name] != null)
					{
						returnval = EntitiesString[name];
					}
				}
			}

			return returnval;
		}


		//Update
		public static void Update(GameTime gameTime)
		{
			if (CurrentSolarSystem != null)
				CurrentSolarSystem.Update(gameTime);

			//Update ships
			for (int ii = 0; ii < Ships.Count; ii++)
			{
				Ships[ii].Update();
			}
		}
		//Draw
		public static void Draw(SpriteBatch spriteBatch)
		{
			if (CurrentSolarSystem != null)
				CurrentSolarSystem.Draw(spriteBatch);
		}
	}



/*
	public static class Classifications
	{
		public static SortedList<Star_Rarity.Types, List<Body_Star>> Stars = new SortedList<Star_Rarity.Types, List<Body_Star>>(4);
			private static Body_Star WhiteDwarf1 = new Body_Star(Engine.TextureIndexes.Star_WhiteDwarf, 0.2f, 1.4f, 2.1f);
			private static Body_Star YellowMainSequence1 = new Body_Star(Engine.TextureIndexes.Star_YellowMainSequence, 1f, 0.8f, 1.04f);
			private static Body_Star RedGiant1 = new Body_Star(Engine.TextureIndexes.Star_RedGiant, 4f, 0.08f, 0.45f);
			private static Body_Star BlueGiant1 = new Body_Star(Engine.TextureIndexes.Star_BlueGiant, 10f, 16f, 20f);


		public static SortedList<Planet_Rarity.Types, List<Body_Planet>> Planets = new SortedList<Planet_Rarity.Types, List<Body_Planet>>();
			private static Body_Planet ClassH_Delvor = new Body_Planet(Engine.TextureIndexes.Planet_ClassH_Delvor, 1f, 0.055f, 0.107f);
			private static Body_Planet ClassM_Etho = new Body_Planet(Engine.TextureIndexes.Planet_ClassM_Etho, 1f, 0.055f, 0.107f);
			private static Body_Planet ClassO_Serine = new Body_Planet(Engine.TextureIndexes.Planet_ClassO_Serine, 1f, 0.055f, 0.107f);
			private static Body_Planet ClassP_Antasia = new Body_Planet(Engine.TextureIndexes.Planet_ClassP_Antasia, 1f, 0.055f, 0.107f);
			private static Body_Planet ClassY_Voshnoy = new Body_Planet(Engine.TextureIndexes.Planet_ClassY_Voshnoy, 1f, 0.055f, 0.107f);
			private static Body_Planet ClassJ_Fash = new Body_Planet(Engine.TextureIndexes.Planet_ClassJ_Fash, 1f, 0.055f, 0.107f);


		static Classifications()
		{
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
				ClassJ_Fash
			};
			Planets.Add(Planet_Rarity.Types.ClassH, CHList);
			Planets.Add(Planet_Rarity.Types.ClassM, CMList);
			Planets.Add(Planet_Rarity.Types.ClassO, COList);
			Planets.Add(Planet_Rarity.Types.ClassP, CPList);
			Planets.Add(Planet_Rarity.Types.ClassY, CYList);
			Planets.Add(Planet_Rarity.Types.ClassJ, CJList);

		}
	}

	public static class Star_Rarity
	{
		public enum Types
		{
			WhiteDwarf = 0,
			YellowMainSequence = 1,
			RedGiant = 2,
			BlueGiant = 3
		}
		public static int TotalWeight;

		//Star percentage weights
		//White Dwarf          - 6%
		//Yellow Main Sequence - 87%
		//Red Giant            - 5%
		//Blue Giant           - 2%
		public static int GetRarity(Types star)
		{
			switch (star)
			{
				case Types.WhiteDwarf:
					return 6;
				case Types.YellowMainSequence:
					return 87;
				case Types.RedGiant:
					return 5;
				case Types.BlueGiant:
					return 2;
				default:
					return 0;
			}
		}

		static Star_Rarity()
		{
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
			int random = rand.Next(TotalWeight - 1); //Make a random from 0 - total weight of all the stars. Four 25% weight stars = 100 totalweight.
			List<Body_Star> WRS = Classifications.Stars[StarList[random]]; //Get the list of individual stars from the class of star we picked randomly.
			return WRS[rand.Next(WRS.Count)]; //Pick a random star FROM the list we found above and return it.
		}


	}
	public class Body_Star
	{
		public Texture2D Texture { get; set; }
		public float Scale { get; set; }
		public float MassMin { get; set; }
		public float MassMax { get; set; }

		public Body_Star(Engine.TextureIndexes textureindex, float scale, float massMin, float massMax)
		{
			Texture = Engine.GetTexture(textureindex);
			Scale = scale;
			MassMin = massMin;
			MassMax = massMax;

		}
	}

	public static class Planet_Rarity
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

		static Planet_Rarity()
		{
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
			List<Body_Planet> WRS = Classifications.Planets[PlanetList[random]]; //Get the list of individual stars from the class of star we picked randomly.
			return WRS[rand.Next(WRS.Count)]; //Pick a random star FROM the list we found above.
		}

	}
	public class Body_Planet
	{
		public Texture2D Texture { get; set; }
		public float Scale { get; set; }
		public float MassMin { get; set; }
		public float MassMax { get; set; }

		public Body_Planet(Engine.TextureIndexes textureindex, float scale, float massMin, float massMax)
		{
			Texture = Engine.GetTexture(textureindex);
			
			Scale = scale;
			MassMin = massMin;
			MassMax = massMax;
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
