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
	public class BaseEntity
	{
		//Common variables shared by all entities
		public string Name = string.Empty;

		public SolarSystem SolarSystem = null;
		public BaseEntity Parent = null;
		public Vector2d ParentOffset = Vector2d.Zero;

		public List<BaseEntity> Children = new List<BaseEntity>();

		public bool IsActive = false;

		public Vector2d Position = Vector2d.Zero;
		[System.Xml.Serialization.XmlIgnore]
		public Vector2d Render_Position = Vector2d.Zero;
		public float Z = 0;

		public Vector2d Velocity = Vector2d.Zero;
		public Vector2d Acceleration = Vector2d.Zero;

		public Vector2d Force = Vector2d.Zero;


		public double OffsetAngle = 0d;
		public double Angle = 0d;

		public double AngularVelocity = 0d;
		public double AngularAcceleration = 0d;

		public double Torque = 0d;

		public float Scale = 1f;
		public double Mass = 1d;

		[System.Xml.Serialization.XmlIgnore]
		public string DebugString = string.Empty;
		public Color DebugColor = Color.Red;
		public bool AllowDebug = false;

		//Drawing
		public Component_Render Renderer = null;

		//Components
		public Component_Orbit Orbit = null;
		public Component_Particle Particle = null;

		public Component_ShipLogic ShipLogic = null;
		public Component_BodyLogic BodyLogic = null;

			//Ship Systems
			public ShipSystem_Communication Communication = null;
			public ShipSystem_Thruster Thruster = null;
		

		//public List<IUpdatableComponent> _updateableComponents = new List<IUpdatableComponent>();
		//public Dictionary<int, IUpdatableComponent> _updatableComponents = new Dictionary<int, IUpdatableComponent>();
		//public Dictionary<int,IDrawableComponent> _drawableComponents = new Dictionary<int,IDrawableComponent>();

		public BaseEntity(){}
		public BaseEntity(string name, Vector2d initialPosition, float initialAngle, float initialScale)
		{
			Name = name;

			Position = initialPosition;
			OffsetAngle = initialAngle;
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

		public void SetParent(BaseEntity parent)
		{
			Parent = parent;
			ParentOffset = Position - parent.Position;

			parent.AddChild(this);
		}
		public void RemoveParent()
		{
			Parent = null;
			ParentOffset = Vector2d.Zero;
		}

		public void AddChild(BaseEntity child)
		{
			if (!Children.Contains(child))
				Children.Add(child);
		}
		public void RemoveChild(BaseEntity child)
		{
			if (Children.Contains(child))
			Children.Remove(child);
		}


		public void ApplyForce(Vector2d force)
		{
			//Forces.Add(force);
			Force += force;
		}
		public void ApplyOffsetForce(Vector2d force, Vector2d offset)
		{
			//AngularAcceleration = ((force * force) / (offset.Length() == 0 ? 1 : offset.Length())).Length();
			Force += force;
			Torque += ((offset.X * force.Y) - (offset.Y * force.X)) * (Math.PI / 180);

		}


		public void Update(GameTime gameTime)
		{
			if (IsActive)
			{
				//Update Components
				if (ShipLogic != null) { ShipLogic.Update(); }
				if (BodyLogic != null) { BodyLogic.Update(); }
				if (Orbit != null) { Orbit.Update(); }
				if (Particle != null) { Particle.Update(); }
				if (Communication != null) { Communication.Update(); }


				//Time
				float DeltaT = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

				//Set Forces and Torques equal to that of the parent if this entity is a child.
				if (Parent != null)
				{
					Torque = Parent.Torque;

					Force = Parent.Force;

				}

				//Update Angular motion
				AngularAcceleration = Torque;
				AngularVelocity += AngularAcceleration;
				Angle += AngularVelocity;
				//if (Angle >= MathHelper.TwoPi) { Angle = 0; }
				//if (Angle < 0) { Angle = MathHelper.TwoPi; }

				//Update positional motion
				Acceleration = Force;
				Velocity += Acceleration;
				Position += Velocity;

				//Set position relative to parent if this entity is a child.
				if (Parent != null)
				{
					Angle = Parent.Angle + OffsetAngle;
					Position = Vector2d.Transform(ParentOffset, Matrix.CreateRotationZ((float)Parent.Angle)) + Parent.Position;
				}
				


				//Update children's positions for parenting reasons
				for (int ii = 0; ii < Children.Count; ii++)
				{
					Children[ii].Update(gameTime);
				}

				//Update rendered position using Center-Of-Universe idea.
				Render_Position = (Position - ((Engine.camera.TargetExists() ? Engine.camera.TargetObject.Position : Engine.camera.Position)));
					


				//Debug updating
				if (AllowDebug)
				{
					DebugString =
						"Name:" + Name + "\n" +
						"Mass:" + Mass.ToString() + "\n" +
						"Scale:" + Scale.ToString() + "\n" +
						"Parent:" + (Parent != null ? Parent.Name : "") + "\n" +
						"    Offset:" + ParentOffset.ToString() + "\n" +
						"Children:" + Children.Count.ToString() + "\n" +

						//(Renderer != null  ? "Texture:(" + Renderer._texture.Width + "," + Renderer._texture.Height + ")" + "\n" : "") + 
						//(Parent != null ? "At Correct Position: " + (Math.Abs((Parent.Position - (Position - ParentOffset)).X) < 0.0001d && Math.Abs((Parent.Position - (Position - ParentOffset)).Y) < 0.0001d).ToString() + "\n" : "") + 
						//(Parent != null ? "Real Offset: (" + (Parent.Position.X - Position.X).ToString() + "," + (Parent.Position.Y - Position.Y).ToString() + ")" + "\n" : "") +
						//(Parent != null ? "Difference: (" + (Parent.Position.X - Position.X).ToString() + "," + (Parent.Position.Y - Position.Y).ToString() + ")" + "\n" : "") +
						//(Engine.camera.TargetExists() ? "CameraTarget:(" + Engine.cameraTargetObject.Position.X + "," + Engine.cameraTargetObject.Position.Y + ")" + "\n" : "") +
						"Position:(" + Position.X + "," + Position.Y + ")" + "\n" +
						"Render_Position:(" + Render_Position.X + "," + Render_Position.Y + ")" + "\n" +
						"Velocity:(" + Math.Round(Velocity.X, 8) + "," + Math.Round(Velocity.Y, 8) + ")" + "\n" +
						"Acceleration:(" + Acceleration.X + "," + Acceleration.Y + ")" + "\n" +
						"Force:(" + Math.Round(Force.X, 4) + "," + Math.Round(Force.Y, 4) + ")" + "\n" +
						"Angle:" + Angle.ToString() + "\n" +
						"AngVel:" + AngularVelocity.ToString() + "\n" +
						"AngAccel:" + AngularAcceleration.ToString() + "\n" +
						"Torque:" + Torque.ToString() + "\n" +

						(ShipLogic != null ? "InertiaDamper:" + ShipLogic.InertiaDamper.ToString() : "") + "\n" +
						(Orbit != null ? "AngleToParent:" + Orbit.ParentAng.ToString() : "") + "\n" +
						(Orbit != null ? "OrbitRadius:(" + Orbit.OrbitRadius.X + "," + Orbit.OrbitRadius.Y + ")" : "") + "\n" +
						(Orbit != null ? "OrbitOffset:(" + Orbit.OrbitOffset.X + "," + Orbit.OrbitOffset.Y + ")" : "") + "\n" +
						(Orbit != null ? "OrbitSpeed:" + Orbit.OrbitSpeed : "") + "\n" +
						(Orbit != null ? "OrbitDifference:(" + Orbit.Difference.X + "," + Orbit.Difference.Y + ")" : "") + "\n" +
						(Orbit != null ? "OrbitDistance:" + Orbit.Distance.ToString() : "") + "\n" +
						(Orbit != null ? "OrbitEnRoute:" + Orbit.EnRoute.ToString() : "") + "\n" +
						"";

					//Change debug color if object is the target
					if (Engine.camera.TargetObject == this)
						DebugColor = Color.Green;
					else
						DebugColor = Color.Red;
				}


				//Clear our list of forces for this update frame
				Force = Vector2d.Zero;
				Torque = 0d;
				Acceleration = Vector2d.Zero;
				AngularAcceleration = 0d;			
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
