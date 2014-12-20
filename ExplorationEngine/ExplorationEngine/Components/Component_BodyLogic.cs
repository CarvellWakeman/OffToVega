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
	public class Component_BodyLogic : Component
	{
		public override int ID { get; set; }

		public BaseEntity _entity;
		public string _entityName;

		[System.Xml.Serialization.XmlIgnore]
		private BaseEntity Sun = null;
		
		public string ParentSolarSystem = string.Empty;


		//Body info
		public StellarClassifications.Body_Classes Class = StellarClassifications.Body_Classes.Unknown;
		public StellarClassifications.Body_Types Type = StellarClassifications.Body_Types.Unknown;
		public double CoreTemperature = 0d;
		public double SurfaceTemperature = 0d;


		public bool HasShadow = false;
		public Texture2D Shadow;
		public Engine.TextureIndexes ShadowIndex;
		public double SolarShadowAngle;

		public Component_BodyLogic() { }
		public Component_BodyLogic(BaseEntity entity, Texture2D shadow)
		{
			ID = 2;
			_entity = entity;
			Shadow = shadow;

			//if (entity.Renderer != null)
				//entity.Renderer.Layer = EntityManager.Layers.SolarBodies;
		}


		public override void SelfDestruct()
		{
			//_entity = null;
		}


		public override void Update()
		{
			if (Sun != null)
			{
				//Calculate Shadow Angle
				SolarShadowAngle = Math.Atan2(_entity.Position.Y - Sun.Position.Y, _entity.Position.X - Sun.Position.X);
			}
			else
			{
				Sun = Galaxy.SolarSystemLookup(ParentSolarSystem).Entities[0];
			}
		}

	}
}
