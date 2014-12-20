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

using ExplorationEngine.Components;
#endregion
/*
namespace ExplorationEngine
{
	public class BaseEntity
	{
		//Common variables shared by all entities
		public string Name = string.Empty;

		public bool IsActive = false;

		public Point Chunk = Point.Zero;
		public Vector2d ChunkPosition = Vector2d.Zero;

		public Vector2d Position
		{
			get
			{
				//float x = (Chunk.X * 1024) + ChunkPosition.X;
				//float y = (Chunk.Y * 1024) + ChunkPosition.Y;

				//return new Vector2d(x, y);
				return ChunkPosition;
			}
			set
			{
				int AddChunksX = (int)value.X / 1024;
				int AddChunksY = (int)value.Y / 1024;

				Chunk.X += AddChunksX;
				Chunk.Y += AddChunksY;

				ChunkPosition.X += value.X - AddChunksX;
				ChunkPosition.Y += value.Y - AddChunksY;
			}
		}

		public Vector2d Velocity = Vector2d.Zero;
		public Vector2d Acceleration = Vector2d.Zero;

		public float Angle = 0f;
		public float AngularVelocity = 0f;
		public float AngularAcceleration = 0f;

		public float Scale = 1f;
		public float Mass = 1f;

		[System.Xml.Serialization.XmlIgnore]
		public string DebugString = string.Empty;

		//Drawing
		public RenderComponent Renderer = null;

		//Possibly shared components
		//public PlayerInputComponent InputComponent = null;
		public OrbitComponent Orbit = null;
		public ParticleComponent Particle = null;

		//Special Logic components
		public ShipLogicComponent ShipLogic = null;
		public BodyLogicComponent BodyLogic = null;
		

		//public List<IUpdatableComponent> _updateableComponents = new List<IUpdatableComponent>();
		//public Dictionary<int, IUpdatableComponent> _updatableComponents = new Dictionary<int, IUpdatableComponent>();
		//public Dictionary<int,IDrawableComponent> _drawableComponents = new Dictionary<int,IDrawableComponent>();

		public BaseEntity(){}
		public BaseEntity(string name, Vector2d initialPosition, float initialAngle, float initialScale)
		{
			Name = name;

			Position = initialPosition;
			Angle = initialAngle;
			Scale = initialScale;
		}

		//public void AddComponent(IUpdatableComponent component)
		//{
		//	if (ComponentLookup(component.ID) == null)
		//	{
		//		_updateableComponents.Add(component);
		//	}
		//	else
		//	{
		//		System.Windows.Forms.MessageBox.Show("The updatable component with ID '" + component.ID + "' already exists within '" + Name + "'. It was not added.");
		//		//throw new DuplicateWaitObjectException("The updatable component with ID" + component.ID + " already exists within entity " + _name + "'s list of components");
		//	}
		//}

		
		//public IUpdatableComponent ComponentLookup(int id)
		//{
		//	IUpdatableComponent returnval = null;

		//	if (_updateableComponents.Count > 0)
		//	{
		//		for (int ii = 0; ii < _updateableComponents.Count; ii++)
		//		{
		//			if (_updateableComponents[ii].ID == id)
		//			{
		//				returnval = _updateableComponents[ii];
		//			}
		//		}
		//	}

		//	return returnval;
		//}

		public void SelfDestruct()
		{
			Renderer = null;
			//InputComponent = null;
			ShipLogic = null;
			BodyLogic = null;
			Orbit = null;
			Particle = null;

			//foreach (KeyValuePair<int, IUpdatableComponent> e in _updateableComponents)
			//{
			//	e.Value.SelfDestruct();
			//	_updateableComponents.Remove(e);
			//}
			//for (int ii = 0; ii < _updateableComponents.Count; ii++)
			//{
			//	_updateableComponents[ii].SelfDestruct();
			//	_updateableComponents.Remove(_updateableComponents[ii]);
			//}

		}


		public void Update(GameTime gameTime)
		{
			if (IsActive)
			{
				//Time
				float DeltaT = (float)gameTime.ElapsedGameTime.TotalSeconds;

				ChunkPosition.X += 1f;

				//Chunks
				if (ChunkPosition.X >= 512)
				{
					ChunkPosition.X = -512;
					Chunk.X += 1;
				}
				else if (ChunkPosition.X <= -512)
				{
					ChunkPosition.X = 512;
					Chunk.X -= 1;
				}

				if (ChunkPosition.Y >= 512)
				{
					ChunkPosition.Y = -512;
					Chunk.Y += 1;
				}
				else if (ChunkPosition.Y <= -512)
				{
					ChunkPosition.Y = 512;
					Chunk.Y -= 1;
				}




				//Update motion
				AngularVelocity += AngularAcceleration;
				Angle += AngularVelocity;
				//if (Angle >= MathHelper.TwoPi) { Angle = 0; }
				//if (Angle < 0) { Angle = MathHelper.TwoPi; }

				//Velocity += Acceleration;// *DeltaT * DeltaT;
				//Position += Velocity;// *DeltaT;

				//Update special Components
				if (ShipLogic != null) { ShipLogic.Update(gameTime); }
				if (BodyLogic != null) { BodyLogic.Update(gameTime); }
				if (Orbit != null) { Orbit.Update(gameTime); }
				if (Particle != null) { Particle.Update(gameTime); }

				//for (int ii = 0; ii < _updateableComponents.Count; ii++)
				//{
				//	_updateableComponents[ii].Update();
				//}

				//foreach (KeyValuePair<int, IUpdatableComponent> e in _updateableComponents)
				//{
				//	e.Value.Update();
				//}


				//Debug updating
				DebugString =
					"Name:" + Name + "\n" +
					"Mass:" + Mass + "\n" +
					"Chunk:(" + Chunk.X + "," + Chunk.Y + ")" + "\n" +
					"ChunkPosition:(" + ChunkPosition.X + "," + ChunkPosition.Y + ")" + "\n" +
					"Position:(" + Position.X + "," + Position.Y + ")" + "\n" +
					"Velocity:(" + Velocity.X + "," + Velocity.Y + ")" + "\n" +
					"Acceleration:(" + Acceleration.X + "," + Acceleration.Y + ")" + "\n" +
					"AngAccel:" + AngularAcceleration.ToString() + "\n" +
					"AngVel:" + AngularVelocity.ToString() + "\n" +
					"Angle:" + Angle.ToString() + "\n" +
					(ShipLogic != null ? "InertiaDamper:" + ShipLogic.InertiaDamper.ToString() : "") + "\n" +
					(Orbit != null ? "AngleToParent:" + Orbit.ParentAng.ToString() : "") + "\n" +
					(Orbit != null ? "OrbitRadius:(" + Orbit.OrbitRadius.X + "," + Orbit.OrbitRadius.Y + ")" : "") + "\n" +
					(Orbit != null ? "OrbitOffset:(" + Orbit.OrbitOffset.X + "," + Orbit.OrbitOffset.Y + ")" : "") + "\n" +
					(Orbit != null ? "OrbitSpeed:" + Orbit.OrbitSpeed : "") + "\n" +
					(Orbit != null ? "OrbitDifference:(" + Orbit.Difference.X + "," + Orbit.Difference.Y + ")" : "") + "\n" +
					(Orbit != null ? "OrbitDistance:" + Orbit.Distance.ToString() : "") + "\n" +
					(Orbit != null ? "OrbitSS:" + Orbit.ss.ToString() : "") + "\n" +
					(Orbit != null ? "OrbitEnRoute:" + Orbit.EnRoute.ToString() : "") + "\n" +
					"";
					//(BodyLogic != null ? "TEST:" + BodyLogic.ID.ToString() : "");
			}
		}

		public void Draw(SpriteBatch spritebatch)
		{
			if (IsActive)
			{
				if (Particle != null) { Particle.Draw(spritebatch); }
				if (Renderer != null) { Renderer.Draw(spritebatch); }

				//foreach (KeyValuePair<int, IDrawableComponent> e in _drawableComponents)
				//{
				//	e.Value.Draw(spritebatch);
				//}
			}
		}
	}
}
*/