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
	public class EntityEngine
	{
		#region "Declarations"

		//public Dictionary<string, Entity> Entities = new Dictionary<string, Entity>();
		public List<Entity> Entities = new List<Entity>();


		#endregion


		#region "Constructor and Destructor"

		public EntityEngine()
		{

		}

		#endregion


		#region "Entity Management"

		public Entity CreateEntity(string name, Texture2D texture, float scale, float mass, Vector2 position, Vector2 velocity, Vector2 orbitRadius, Vector2 orbitOffset, float orbitSpeed, float angle, float angularVelocity, float initialAngle, bool hasshadow)
		{

			//Create the parent (Give it a default parent of Entities[0] unless it IS Entities[0].
			Entity Ent = new Entity(name, texture, scale, mass, position, velocity, orbitRadius, orbitOffset, orbitSpeed, angle * (float)(Math.PI / 180), angularVelocity, initialAngle, hasshadow, (Entities.Count > 0 ? Entities[0] : null));

			if (Entities.Count <= 0)
				Ent.IsSun = true;


			Entities.Add(Ent);

			//Return the Entity
			return Ent;
		}
		public Entity CreateEntity(Entity ent)
		{
			return CreateEntity(ent.Name, ent.Texture, ent.Scale, ent.Mass, ent.Position, ent.Velocity, ent.OrbitRadius, ent.OrbitOffset, ent.OrbitSpeed,
				ent.Angle, ent.AngularVelocity, ent.InitialAngle, ent.HasSolarShadow);
		}

		public void CreateEntityChildren(Entity parent, List<Entity> children)
		{
			//Asign a parent to all children provided
			for (int ii = 0; ii < children.Count(); ii++)
			{
				children[ii].Parent = parent;
				

				if (parent != Entities[0])
					children[ii].HasLunarShadow = true;
			}
		}

		//public Entity CreateSun(string name, Texture2D texture, float scale, float mass, Vector2 position, Vector2 velocity, Vector2 orbitRadius, Vector2 orbitOffset, float orbitSpeed)
		//{
		//	//Create the parent (Give it a default parent of Entities[0] unless it IS Entities[0].
		//	Entity Ent = new Entity(name, texture, scale, mass, position, velocity, orbitRadius, orbitOffset, orbitSpeed, 0.0f, 0.0f, 0.0f, false, (Entities.Count > 0 ? Entities[0] : null));
		//	Entities.Add(Ent);

		//	//Return the Entity
		//	return Ent;
		//}


		public void DestroyEntity(int index)
		{
			if (Entities.Count > 0)
			{
				Entities[index] = null;
				Entities.Remove(Entities[index]);
			}
		}
		public void DestroyEntity(string name)
		{
			if (Entities.Count > 0)
			{
				foreach (Entity e in Entities)
				{
					if (e.Name == name)
					{
						Entities.Remove(e);
					}
				}
			}
		}

		public void DestroyAll()
		{
			if (Entities.Count > 0)
			{
				Entities.Clear();
			}
		}

		public Vector2 Pos(int index)
		{
			if (Entities.Contains(Entities[index]))
			{
				return Entities[index].Position;
			}
			else
			{
				return new Vector2(0,0); //[remember] Not sure how to handle with this
			}
		}

		#endregion


		#region "Update & Draw"

		public void Update()
		{
			for(int ii=0; ii< Entities.Count; ii++)
			{
				Entities[ii].Update();
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			for (int ii = 0; ii < Entities.Count; ii++)
			{
				Entities[ii].Draw(spriteBatch);
			}
		}

		#endregion

	}
}
