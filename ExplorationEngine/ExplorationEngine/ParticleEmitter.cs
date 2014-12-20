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

namespace ExplorationEngine
{
	public class ParticleEmitter
	{
		[System.Xml.Serialization.XmlIgnore]
		public Component_Particle Manager;

		public BaseEntity _entity;
		public string _entityName;

		public Vector2d Position;
		public Vector2d Offset;
		public Matrix ParentMatrix;

		[System.Xml.Serialization.XmlIgnore]
		private List<Particle> Particles = new List<Particle>();

		private Random Rand;

		public Keys ActivationKey;

		public bool OnOff;
		public bool IsActive = true;


		public ParticleEmitter() {}
		public ParticleEmitter(Component_Particle pc, BaseEntity objectAttach, Vector2d offset, Keys activationkey)
		{
			Manager = pc;
			_entity = objectAttach;
			//Position = Vector2d.Transform(Position, Matrix.CreateRotationZ(_entity.Angle)) + _entity.Position;

			Offset = offset;
			Particles = new List<Particle>();

			Rand = new Random();

			ActivationKey = activationkey;
		}


		private Particle GenerateNewParticle()
		{
			//Vector2d velocity = ObjectAttach.Velocity + Vector2d.Zero;
			Vector2d velocity = new Vector2d((float)(Rand.NextDouble() * 2 - 1) / 2048, Math.Max((float)Rand.NextDouble(), 0.5f) / 64);//new Vector2d((float)(Rand.NextDouble() * 2 - 1), (float)(Rand.NextDouble() * 2 - 1)) ;
			float angularVelocity = 0f; //0.1f * (float)(Rand.NextDouble() * 2 - 1);
			Color color_start = Color.White;// new Color(255, 255, 130);
			Color color_end = Color.Blue; // new Color(255, 174, 53);

			float size_start = 0.0078125f;//(float)Rand.NextDouble() / 4;
			float size_end = size_start / 16;//0.1f;

			int lifespan = 25 + Rand.Next(20);

			return new Particle(this, _entity, Engine.ParticleTexture,
				velocity, 0f, angularVelocity, 
				color_start, color_end, 
				size_start, size_end, 
				lifespan);
		}


		public void Update()
		{
			//If our parent is active
			if (_entity.IsActive)
			{
				//Parent entity checking
				if (_entity == null)//& this != null)
				{
					Manager.DestroyEmitter(this);
				}
				else
				{
					//Update position
					ParentMatrix = Matrix.CreateRotationZ((float)_entity.Angle) * Matrix.CreateTranslation(new Vector3((Vector2)_entity.Render_Position, 0f));
					Position = Vector2d.Transform(Offset, ParentMatrix);

					//Position = Vector2d.Transform(Offset, Matrix.CreateRotationZ(ObjectAttach.Angle)) + ObjectAttach.Position;// +
					//Position += ObjectAttach.Velocity;

					// [Remember] This is not a very good solution, it's not modular. Emitters should work for any entity, not just ships.
					OnOff = Input.KeyDown(ActivationKey) && _entity.Renderer != null && _entity.Renderer.IsVisible && _entity.ShipLogic != null && _entity.ShipLogic.IsControlled == true;

					//Turn the emitter on or off
					if (OnOff)
					{
						int total = 1;

						for (int i = 0; i < total; i++)
						{
							
						}
						Particles.Add(GenerateNewParticle());
					}

					//Remove particles who's lifespan is <= 0
					for (int ii = Particles.Count - 1; ii > 0; ii--)
					{
						Particles[ii].Update();

						if (Particles[ii].LifeSpan <= 0)
						{
							Particles.RemoveAt(ii);
						}
					}
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			//If our parent is active
			if (_entity.IsActive)
			{
				for (int index = 0; index < Particles.Count; index++)
				{
					Particles[index].Draw(spriteBatch);
				}


				//Debug Drawing
				if (Engine.DebugState)
				{
					Rectangle sourceRectangle = new Rectangle(0, 0, Engine.EmitterTexture.Width, Engine.EmitterTexture.Height);
					Vector2d origin = new Vector2d(Engine.EmitterTexture.Width / 2, Engine.EmitterTexture.Height / 2);

					if (_entity != null)
					{
						spriteBatch.Draw(Engine.EmitterTexture, Position, sourceRectangle, Color.Red, 0f, origin, _entity.Scale, SpriteEffects.None, (_entity.Z + Galaxy.ParticleZModifier) / Galaxy.MaxLayer);
							//(float)EntityManager.Layers.Particles / (float)EntityManager.Layers.MAX_LAYER);

						//spriteBatch.DrawString(Engine.Font_MediumSmall, _entity.Name, Position, Color.Orange, 0f, Vector2.Zero, _entity.Scale, SpriteEffects.None, (float)EntityManager.Layers.Particles / (float)EntityManager.Layers.MAX_LAYER);
					}
				}
			}
		}


	}
}
