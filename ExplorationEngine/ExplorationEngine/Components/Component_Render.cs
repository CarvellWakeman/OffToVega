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
	public class Component_Render : Component
	{
		public override int ID { get; set; }
		//public EntityManager.Layers Layer = EntityManager.Layers.SolarBodies;

		public BaseEntity _entity;
		public string _entityName;

		public Texture2D _texture;
		public Engine.TextureIndexes TextureIndex;

		public Texture2D _debugTexture;
		public Engine.TextureIndexes DebugTextureIndex;

		public float AngleOffset = 0f;

		public bool IsVisible = true;
		public Color Color;

		public Component_Render() {}
		public Component_Render(BaseEntity entity, Texture2D texture)
		{
			ID = 0;
			Color = Color.White;
			_entity = entity;
			_texture = texture;
		}
		public Component_Render(BaseEntity entity, Texture2D texture, Color color)
		{
			ID = 0;
			Color = color;
			_entity = entity;
			_texture = texture;
		}

		public override void Draw(SpriteBatch spritebatch)
		{
			if (IsVisible)
			{
				//Rectangle Source = new Rectangle(0, 0, _texture.Width, _texture.Height);
				Vector2 Origin = (_texture != null ? new Vector2(_texture.Width / 2, _texture.Height / 2) : (_debugTexture != null ? new Vector2(_debugTexture.Width / 2, _debugTexture.Height / 2) : Vector2.Zero));


				//Draw the entity itself
				if (_texture != null || (Engine.DebugState && _debugTexture != null))
				{
					spritebatch.Draw((Engine.DebugState && _debugTexture != null ? _debugTexture : _texture), _entity.Render_Position, null, Color, (float)_entity.Angle + AngleOffset, Origin, _entity.Scale, SpriteEffects.None, (_entity.Z / Galaxy.MaxLayer));
				}


				//Shadow drawing (ugh so not modular)
				if (_entity.BodyLogic != null && _entity.BodyLogic.HasShadow)
				{
					spritebatch.Draw(_entity.BodyLogic.Shadow, _entity.Render_Position, null, Color.White, (float)_entity.BodyLogic.SolarShadowAngle, Origin, _entity.Scale, SpriteEffects.None, (_entity.Z + Galaxy.ShadowZModifier) / Galaxy.MaxLayer);
				}

				
				//Debug drawing
				if (Engine.DebugState == true && _entity.AllowDebug)
				{
					//if (Camera.TargetObject == _entity)
					//{
					spritebatch.DrawString(Engine.Font_Small, _entity.DebugString, _entity.Render_Position, _entity.DebugColor, 0f, Vector2.Zero, _entity.Scale, SpriteEffects.None, (_entity.Z + Galaxy.DebugZModifier) / Galaxy.MaxLayer);
					//}
						//spritebatch.Draw(Engine.Square, _entity.Position - new Vector2(Engine.Square.Width/2,Engine.Square.Height/2), Color.Green);
				}
			}
		}
	}
}
