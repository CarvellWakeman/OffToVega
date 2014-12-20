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

namespace ExplorationEngine
{
	public class Particle
	{
		public BaseEntity _entity;
		public ParticleEmitter Emitter;
		public Texture2D Texture { get; set; }

		public Vector2d Position { get; set; }
		public Vector2d OffsetPosition { get; set; }
		public Vector2d Velocity { get; set; }

		public float Angle { get; set; }
		public float AngularVelocity { get; set; }

		private Color Color_Start;
		private Color Color_End;
		public float ColorTransitionSpeed { get; set; }
		public Color Color_Draw { get; set; }

		public float transitiontime;

		private float Size_Start;
		private float Size_End;
		private float Size_Diff;
		public float Size_Draw { get; set; }

		private int OriginalLifeSpan;
		public int LifeSpan { get; set; }


		public Particle(
			ParticleEmitter part, BaseEntity objectAttach, 
			Texture2D texture, 
			Vector2d velocity, 
			float angle, float angularVelocity, 
			Color color_start, Color color_end, 
			float size_start, float size_end, 
			int lifespan)
		{
			Emitter = part;
			_entity = objectAttach;

			Texture = texture;

			Position = part.Position - objectAttach.Velocity;
			Velocity = velocity;

			Angle = angle;
			AngularVelocity = angularVelocity;

			Color_Start = color_start;
			Color_End = color_end;

			Size_Start = size_start;
			Size_End = size_end;
			Size_Diff = size_start - size_end;
			Size_Draw = size_start;

			OriginalLifeSpan = lifespan;
			LifeSpan = lifespan;

			ColorTransitionSpeed = (1f / lifespan);
		}


		public void Update()
		{
			Angle += AngularVelocity;


			//Position += Velocity; // Smoke type trail, when velocity is set to Ship.Velocity

			//Relative effect, like engine thrust
			OffsetPosition += Velocity;
			Position = Vector2d.Transform(OffsetPosition, Matrix.CreateRotationZ((float)_entity.Angle)) + Emitter.Position;


			Size_Draw -= (Size_Diff / (float)OriginalLifeSpan);

			transitiontime += ColorTransitionSpeed;
			Color_Draw = Color.Lerp(Color_Start, Color_End, transitiontime);


			LifeSpan -= (Size_Draw <= 0 ? LifeSpan : 1);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
			Vector2d origin = new Vector2d(Texture.Width / 2, Texture.Height / 2);

			spriteBatch.Draw(Texture, Position, sourceRectangle, Color_Draw, Angle, origin, Size_Draw, SpriteEffects.None, (_entity.Z + Galaxy.ParticleZModifier) / Galaxy.MaxLayer);
				//(float)EntityManager.Layers.Particles / (float)EntityManager.Layers.MAX_LAYER);
		}

	}
}
