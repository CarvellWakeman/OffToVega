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
	public class Component_Orbit : Component
	{
		public override int ID { get; set; }

		public BaseEntity _entity;
		public string _entityName = string.Empty;
		public BaseEntity _parent;
		public string _parentName = string.Empty;

		public string ParentSolarSystem = string.Empty;


		//Orbit
		public Vector2d OrbitRadius;
		public double OrbitSpeed;
		public Vector2d OrbitOffset;

		public Vector2d OrbitAroundPoint;
		public Vector2d OrbitLocation;
		public double GravityModifier;

		public Vector2d Difference;
		public double Distance;

		//Angle
		public double InitialAngle;
		//public double AngleToParent;
		public double AngMultiplier;
		public double ParentAng;

		//Properties
		//public Texture2D Shadow { get; set; }
		//public bool HasSolarShadow;
		//public bool IsSun;
		//public double SolarShadowAngle { get; set; }
		public bool EnRoute = false;


		public Component_Orbit() { }
		public Component_Orbit(BaseEntity entity, BaseEntity parent, Vector2d orbitRadius, Vector2d orbitOffset, float orbitSpeed, float initialAngle)
		{
			ID = 3;
			_entity = entity;

			OrbitRadius = orbitRadius;
			OrbitOffset = orbitOffset;
			OrbitSpeed = orbitSpeed;
			InitialAngle = initialAngle;

			ParentAng = initialAngle * (MathHelper.Pi / 180);

			//Set the parent if one was provided, and set the position to the initial position
			if (parent != null)
			{
				_parent = parent;
				_entity.Position = parent.Position + new Vector2d((float)Math.Cos(ParentAng) * OrbitRadius.X, -(float)Math.Sin(ParentAng) * OrbitRadius.Y);
			}

		}


		public override void SelfDestruct()
		{
			//_entity = null;
		}


		public override void Update()
		{

			//Convert parent name back to a parent
			if (_parentName != "" && _parent == null)
			{
				if (Galaxy.EntityLookup(_parentName) != null)
				{
					_parent = Galaxy.EntityLookup(_parentName);
					_parentName = "null";
				}
			}

			//Calulate Variables only relevant if body is a child
			if (_parent != null)
			{

				
				OrbitAroundPoint = _parent.Position;

				//AngleToParent = Math.Atan2(Position.Y - OrbitAroundPoint.Y, Position.X - OrbitAroundPoint.X) * (180 / Math.PI);

				//Orbit
				if (OrbitRadius.X > 0 && OrbitRadius.Y > 0)
				{
					GravityModifier = (_entity.Position - OrbitAroundPoint).Length() / 1000d;

					AngMultiplier = (AngMultiplier < 360 ? (0.01f / (OrbitRadius.X / OrbitRadius.Y != 1 ? GravityModifier : 1)) : 0);
					ParentAng += (ParentAng >= MathHelper.TwoPi ? -MathHelper.TwoPi : (AngMultiplier * OrbitSpeed) * (MathHelper.Pi / 180)) ;

					OrbitLocation = OrbitAroundPoint + new Vector2d((float)Math.Cos(ParentAng) * OrbitRadius.X, -(float)Math.Sin(ParentAng) * OrbitRadius.Y);

					
					Difference = (OrbitLocation - _entity.Position);
					
					//Distance = Vector2d.Distance(OrbitLocation, _entity.Position);
					Distance = Difference.Length();


					//Difference.Normalize();
					//_entity.ApplyForce((Difference/_entity.Mass) * Distance);


					_entity.ApplyForce(( (Difference) / (Distance) ));


					//float slowing_distance = 1;
					//Difference = (OrbitLocation - _entity.Position);
					//Distance = Difference.Length();
					//float ramped_speed = _entity.Velocity.Length() * (Distance / slowing_distance);
					//float clipped_speed = Math.Min(ramped_speed, _entity.Velocity.Length());
					//Vector2d targetLinearVelocity = Vector2d.Difference.scale(clipped_speed / Distance);
					//_entity.Acceleration = targetLinearVelocity - _entity.Velocity;
					
					
					if (Difference.Length() <= 10000d)
					{
						//_entity.Position = OrbitLocation;
						//_entity.Velocity = (_parent != null ? _parent.Velocity : Vector2d.Zero) + Difference;
						

						EnRoute = false;
					}
					else
					{
						Difference.Normalize();
						//_entity.Velocity = Difference * (Distance); // MathHelper.SmoothStep(0f, 10f, 0.5f)
						//ss = MathHelper.Clamp(Distance, 0, Distance);
						//_entity.Velocity = (_parent != null ? _parent.Velocity : Vector2d.Zero) + Difference * (Distance / 100);

						EnRoute = true;
					}
					//_entity.Position = OrbitLocation;

					
					//Difference = (OrbitLocation - _entity.Position);
					//Distance = Difference.Length();

					//float max_speed = 1;

					//Vector2d desired_velocity = (max_speed / Distance) * Difference;
					//_entity.Acceleration = desired_velocity - _entity.Velocity;


					//mAnimation += (float)gt.ElapsedGameTime.TotalSeconds / 500.0f;
 
					//interpolate 
					//_entity.Position = Vector2d.SmoothStep(_entity.Position, OrbitLocation, mAnimation); 
					
				

				}

			}

		}
	}
}
