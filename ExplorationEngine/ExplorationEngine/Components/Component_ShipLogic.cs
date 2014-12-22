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

namespace ExplorationEngine.Components
{
	public class Component_ShipLogic : Component
	{
		public override int ID { get; set; }

		public BaseEntity _entity;
		public string _entityName;

		public bool IsControlled;

		//Movement
		public bool InertiaDamper = true;
		private bool MoveKeyDown = false;

		public Vector2d Direction;

		public float AngleTarget = 0f;

		//Warp Image
		[System.Xml.Serialization.XmlIgnore]
		int r = 0;
		[System.Xml.Serialization.XmlIgnore]
		public List<dControl> WarpStars = new List<dControl>();

		public Component_ShipLogic() { }
		public Component_ShipLogic(BaseEntity entity)
		{
			ID = 1;
			_entity = entity;

			//if (entity.Renderer != null)
			//	entity.Renderer.Layer = EntityManager.Layers.Ships;

			//_entity.ThrowEvent += (sender, args) => { ReceiveMessage(); };
		}

		//void ReceiveMessage()
		//{
		//	System.Windows.Forms.MessageBox.Show("this");
		//}

		public override void SelfDestruct()
		{
			IsControlled = false;
			//_entity = null;
		}


		public override void Update()
		{
			if (_entity.Renderer != null)
				_entity.Renderer.IsVisible = Galaxy.CurrentSolarSystem.Name == _entity.SolarSystem.Name;

			if (_entity.SolarSystem.Name == Galaxy.CurrentSolarSystem.Name)
			{

                if (Engine.camera.TargetObject == _entity) //_entity.Renderer.IsVisible
				{
					//Is the ship being told to move at all
					//MoveKeyDown = ((Input.KeyDown(Input.Move_Forward) || Input.KeyDown(Input.Move_Backward)));

					//Toggle the velocity damper
					InertiaDamper = (Input.KeyReleased(Input.InertiaDamper) ? !InertiaDamper : InertiaDamper);

					//Direction
					Direction = new Vector2d((float)Math.Sin(_entity.Angle), -(float)Math.Cos(_entity.Angle));

					//Add velocity based on move keys
					if (Input.KeyDown(Input.Move_Forward))
					{
						//_entity.ApplyForce(Direction / _entity.Mass * 10000);
					}
					if (Input.KeyDown(Input.Move_Backward))
					{
						//_entity.ApplyForce(-Direction / _entity.Mass * 10000);
					}

					//Subtract velocity if damper is on and no move keys are pressed
					if (Input.KeyUp(Input.Move_Forward) && Input.KeyUp(Input.Move_Backward))
					{
						if (InertiaDamper)
						{
							//_entity.ApplyForce(-(_entity.Velocity / _entity.Mass) * 200);
						}
					}

					//_entity.AngularVelocity -= (!MoveKeyDown ? (_entity.AngularVelocity / _entity.Mass) * 500 : 0f);


					//Add angle based on mouse
					//Vector2d Dir = new Vector2d(Mouse.GetState().X, Mouse.GetState().Y) - Engine.CurrentScreenResolution / 2;
					//_entity.Angle = (float)(Math.Atan2(Dir.Y, Dir.X)) + (float)MathHelper.PiOver2;


					//Angle ship with left and right strafe buttons
					//AngleTarget += (Input.KeyDownInt(Input.Move_Right) - Input.KeyDownInt(Input.Move_Left)) * (MathHelper.Pi / 180);
					//if (AngleTarget >= MathHelper.TwoPi || AngleTarget <= -MathHelper.TwoPi) { AngleTarget = 0; }

					//double strength = (30000d / _entity.Mass) * (MathHelper.Pi / 180);
					//_entity.AngularVelocity = (_entity.Angle < AngleTarget ? strength : (_entity.Angle > AngleTarget ? -strength : 0f)) * (Math.Abs(AngleTarget - _entity.Angle) * 10);
					//_entity.AngularVelocity = (Input.KeyDownInt(Input.Move_Right) - Input.KeyDownInt(Input.Move_Left)) * strength;

					//float diff = AngleTarget - _entity.Angle;
					//_entity.AngularAcceleration = (Input.KeyDownInt(Input.Move_Right) - Input.KeyDownInt(Input.Move_Left)) * (MathHelper.Pi / 180) * strength;

					//Zero out angle to avoid jittering
					//if (_entity.Angle - strength/2 < AngleTarget && _entity.Angle + strength/2 > AngleTarget) { _entity.Angle = AngleTarget; }


					//Angle ship based on offset force from thrusters
					if (Input.KeyDown(Input.Move_Right))
					{
						_entity.ApplyOffsetForce(new Vector2d(0, 0.0001f), new Vector2d(1, 0));
					}
					if (Input.KeyDown(Input.Move_Left))
					{
						_entity.ApplyOffsetForce(new Vector2d(0, 0.0001f), new Vector2d(-1, 0));
					}
					//Subtract angular velocity if damper is on and no move keys are pressed
					if (Input.KeyUp(Input.Move_Left) && Input.KeyUp(Input.Move_Right))
					{
						if (InertiaDamper)
						{
							_entity.ApplyOffsetForce(new Vector2d(0,-(_entity.AngularVelocity / _entity.Mass) * 50000), new Vector2d(1,0));
						}
					}

					//Make a warp effect if we're going fast
					if (_entity.Orbit != null && _entity.Orbit.EnRoute == true && _entity.Orbit.Distance > 1000f)
					{

						if (WarpStars.Count < 100)
						{
							r++;

							if (r > 15)
							{
								r = 0;

								dImage img = new dImage("WarpImage", -_entity.Orbit.Difference * WarpStars.Count, Engine.WarpField, Engine.StarBackground.Form_Main, false, false);
								img.size = Engine.CurrentGameResolution;
								Engine.StarBackground.Form_Main.AddControl(img);
								img.SetActive(true, true, false);

								WarpStars.Add(img);
							}
						}
					}
					else
					{
						if (WarpStars.Count > 0)
						{
							//r++;

							//if (r > 1)
							//{
							//	r = 0;

								if (Engine.StarBackground.Form_Main.children[Engine.StarBackground.Form_Main.children.Count - 1].name == "WarpImage")
								{
									Engine.StarBackground.Form_Main.childrenToDelete.Add(Engine.StarBackground.Form_Main.children[Engine.StarBackground.Form_Main.children.Count - 1]);
									WarpStars.Remove(Engine.StarBackground.Form_Main.children[Engine.StarBackground.Form_Main.children.Count - 1]);
								}
							//}



						}
					}
					//Debug
					if (Engine.DebugState)
					{
						if (_entity.Renderer != null)
						{
							//_entity.Renderer._color.A = 150;
						}
					}
					else
					{
						//_entity.Renderer._color.A = 255;
					}
					//_entity.Scale = (Engine.DebugState == true && _entity.Renderer._debugTexture != null ? 1.5f : 0.0625f);
					//}
				}



				//Zero out velocity to avoid really long trailing zeros
				if (Math.Abs(_entity.Velocity.X) <= 0.01d && InertiaDamper && !MoveKeyDown) { _entity.Velocity.X = 0; }
				if (Math.Abs(_entity.Velocity.Y) <= 0.01d && InertiaDamper && !MoveKeyDown) { _entity.Velocity.Y = 0; }
				if (Math.Abs(_entity.AngularVelocity) < 0.0000001d && !MoveKeyDown) { _entity.AngularVelocity = 0f; }
			}
			else
			{
				if (WarpStars.Count > 0)
				{
					for (int ii = 0; ii < Engine.StarBackground.Form_Main.children.Count; ii++)
					{
						if (Engine.StarBackground.Form_Main.children[ii].name == "WarpImage")
						{
							Engine.StarBackground.Form_Main.childrenToDelete.Add(Engine.StarBackground.Form_Main.children[ii]);
						}
					}
				}
			}
		}

	}
}
