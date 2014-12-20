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
#endregion

namespace ExplorationEngine.Components
{
	public class Component_Particle : Component
	{
		public override int ID { get; set; }

		public BaseEntity _entity;
		public string _entityName;

		public List<ParticleEmitter> ParticleEmitters = new List<ParticleEmitter>();


		public Component_Particle() { }
		public Component_Particle(BaseEntity entity)
		{
			ID = 4;
			_entity = entity;
		}


		public ParticleEmitter CreateEmitter(BaseEntity objectAttach, Vector2 offset, Keys activationkey)
		{
			ParticleEmitter prt = new ParticleEmitter(this, objectAttach, offset, activationkey);

			ParticleEmitters.Add(prt);

			return prt;
		}
		public void DestroyEmitter(ParticleEmitter partEmit)
		{
			if (ParticleEmitters.Count > 0)
			{
				ParticleEmitters.Remove(partEmit);
			}
		}
		public void DestroyAll()
		{
			if (ParticleEmitters.Count > 0)
			{
				//I'll pretend that particles have no mass.
				ParticleEmitters.Clear();
			}
		}


		public override void SelfDestruct()
		{
			for (int ii = 0; ii < ParticleEmitters.Count; ii++)
			{
				ParticleEmitters[ii] = null;
			}
			
			ParticleEmitters.Clear();
		}

		public override void Update()
		{
			for (int ii = 0; ii < ParticleEmitters.Count; ii++)
			{
				ParticleEmitters[ii].Update();
			}
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			for (int ii = 0; ii < ParticleEmitters.Count; ii++)
			{
				ParticleEmitters[ii].Draw(spritebatch);
			}
		}
	}
}
