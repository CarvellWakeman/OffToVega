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
	public class SolarSystem
	{
		public string Name;

		public List<BaseEntity> Entities = new List<BaseEntity>();

		public string CameraTargetObject = string.Empty;
		public int Zoom = -1;

		public Vector2d MapDotPosition = Vector2d.Zero;

		public SolarSystem(){}
		public SolarSystem(string name)
		{
			Name = name;

			//MapDot creation
			double angle = Engine.Rand.NextDouble() * (double)MathHelper.TwoPi;
			double r = Math.Sqrt(Engine.Rand.NextDouble()) * (Engine.GalaxyMap.Image_Galaxy.source.Width / 2 - 20);
			MapDotPosition = new Vector2d((float)(r * Math.Cos(angle)), (float)(r * Math.Sin(angle)));

			Engine.GalaxyMap.AddDot(name, MapDotPosition);
		}
		public SolarSystem(string name, Vector2d mapdotpos)
		{
			Name = name;

			//MapDot creation
			MapDotPosition = mapdotpos;
			Engine.GalaxyMap.AddDot(name, mapdotpos);
		}

		public void AddEntity(BaseEntity entity)
		{
			Entities.Add(entity);
		}
		public void RemoveEntity(BaseEntity entity)
		{
			Entities.Remove(entity);
		}

		public void SelfDestruct()
		{
			Name = "";
			Entities.Clear();
		}


		public void SetSystem(bool active)
		{
			//Sort the current solar system's entities by orbit radius
			List<BaseEntity> CS_Entities = new List<BaseEntity>();
			foreach (BaseEntity ent in Entities)
			{
				if (ent.Orbit != null)
				{
					CS_Entities.Add(ent);
				}
			}
			List<BaseEntity> SortedList = CS_Entities.OrderBy(o => o.Orbit.OrbitRadius.Length()).ToList();

			Entities.Clear();
			foreach (BaseEntity ent in SortedList)
			{
				Entities.Add(ent);
			}

			//Switch camera target
			if (active)
			{
				if (Galaxy.CurrentSolarSystem != null)
				{
					Camera.TargetObject = Galaxy.EntityLookup(CameraTargetObject);

					if (Zoom >= 0)
					{
						//Camera.SetZoom(Zoom);
					}
				}
			}
			else
			{
				if (Camera.TargetExists())
				{
					CameraTargetObject = Camera.TargetObject.Name;
				}

				Zoom = Camera.ZoomIndex;
			}

			for (int ii = 0; ii < Entities.Count; ii++)
			{
				if (Entities[ii] != null)
				{
					Entities[ii].IsActive = active;
				}
			}
		}

		public void Update(GameTime gameTime)
		{

			//while(ii < Entities.Count && UpdateNow)
			for (int ii = 0; ii < Entities.Count; ii++)
			{
				BaseEntity Ent = Entities[ii];
				//EntityManager.LastUpdated = Ent.Name;

				if (Ent != null && Ent.IsActive && Ent.Parent == null)
				{
					Ent.Update(gameTime);
				}
				else if (Ent == null)
				{
					Entities.Remove(Entities[ii]);
				}

			}


			
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			//Draw entities in our entity list
			if (Entities.Count > 0)
			{
				//for (int ii = 0; ii < Entities.Count; ii++)
				//{
				//	if (Entities[ii].IsActive)
				//	{
				//		Entities[ii].Draw(spritebatch);
				//	}
				//}
				for (int ii = 0; ii < Entities.Count; ii++)
				{
					BaseEntity Ent = Entities[ii];
					if (Ent != null && Ent.IsActive)
					{
						Ent.Draw(spriteBatch);
					}
				}
			}
		}
	}
}
