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
	public static class EntityManager
	{
		//public static Dictionary<string, BaseEntity> Entities = new Dictionary<string, BaseEntity>();
		//public static List<BaseEntity> Entities = new List<BaseEntity>();
		//private static Dictionary<string, BaseEntity> EntitiesString = new Dictionary<string, BaseEntity>();

		static EntityManager(){}

		//public static float MaxZ = 100000;
		//public static float MaxLayer = MaxZ + 1;
		//public static float ParticleZModifier = 0.00001f;
		//public static float ShadowZModifier = 0.00001f;
		//public static float DebugZModifier = 0.00001f;
		//public enum Layers
		//{
		//	Space,
		//	SolarBodies,
		//	Shadows,
		//	Particles,
		//	Ships,
		//	MAX_LAYER,
		//}

		/*
		public static string LastUpdated = "";

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
		public static void DestroyAll()
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
		*/

		//Lookup
		/*
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
		public static bool ContainsEntity(BaseEntity entity)
		{
			return Entities.Contains(entity);
		}
		*/

		/*
		public static void Update(GameTime gameTime)
		{
			//Update entities in our entity list
			if (Entities.Count > 0)
			{
				//for (int ii = 0; ii < Entities.Count; ii++)
				//{
				//	if (Entities[ii].IsActive)
				//	{
				//		Entities[ii].Update(gameTime);
				//	}
				//}
				Galaxy.CurrentSolarSystem.Update(gameTime);
			}
		}

		public static void Draw(SpriteBatch spriteBatch)
		{
			//Draw entities in our entity list
			//if (Entities.Count > 0)
			//{
				//for (int ii = 0; ii < Galaxy.CurrentSolarSystem.Entities.Count; ii++)
				//{
				//	BaseEntity Ent = EntityLookup(Galaxy.CurrentSolarSystem.Entities[ii]);
				//	if (Ent != null && Ent.IsActive)
				//	{
				//		Ent.Draw(spriteBatch);
				//	}
				//}
			//}
		}
		*/

	}
}
