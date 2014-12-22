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
	public class Camera
	{
		public Matrix Transform = Matrix.CreateTranslation(Engine.CurrentGameResolution.X * 0.5f, Engine.CurrentGameResolution.Y * 0.5f, 0);
		//public Matrix GameTransform = Matrix.CreateTranslation(Engine.CurrentScreenResolution.X * 0.5f, Engine.CurrentScreenResolution.Y * 0.5f, 0);
		//public Matrix MapTransform = Matrix.CreateTranslation(Engine.CurrentScreenResolution.X * 0.5f, Engine.CurrentScreenResolution.Y * 0.5f, 0);

		public BaseEntity TargetObject;
		public BaseEntity PrevTargetObject;

		public Vector2d Position = Vector2d.Zero;
		public Vector2d PositionDifference = Vector2d.Zero;
		public Vector2d PositionVelocity = Vector2d.Zero;
		public Vector2d PrevPosition = Vector2d.Zero;

		public float Rotation = 0.0f;
		public float Sensitivity;

		double ZoomLevel;
        double ZoomTarget;

        public int ZoomSmoothing = 40;

        double ZoomMax = 2048d;
        double ZoomMin = 0.000001220703125d;

		//State Previous zoom
		public double PrevZoom;

		bool TrapMouse = false;

		//public List<double> ZoomValues = new List<double>() { 2048.0d, 1024.0d, 512.0d, 300.0d, 256.0d, 200.0d, 128.0d, 64.0d, 32.0d, 16.0d, 12.0d, 8.0d, 6.0d, 4.0d, 3.0d, 2.0d, 1.0d, 
		//														0.9d, 0.8d, 0.7d, 0.6d, 0.5d, 0.4d, 0.3d, 0.2d, 0.15d, 0.1d, 0.09d, 0.08d, 0.07d, 0.06d, 
		//														0.05d, 0.04d, 0.03d, 0.02d, 0.015d, 0.01d, 0.0075d, 0.005d, 0.0025d, 0.00125d, 0.000625d, 
		//														0.0003125d, 0.00015625d, 0.000078125d, 0.0000390625d, 0.00001953125d, 0.000009765625d, 0.0000048828125d,
		//														0.00000244140625d, 0.000001220703125d};


		//Mouse
		public MouseState previousMouseState;

		public Camera()
        {
			//Mouse.SetPosition((int)Engine.CurrentScreenResolution.X / 2, (int)Engine.CurrentScreenResolution.Y / 2);
			previousMouseState = Mouse.GetState();


            PrevZoom = 0.06d;
            ZoomTarget = 256.0d;
        }


		public void ResetMouse()
		{
			if (TrapMouse)
			{
				Mouse.SetPosition((int)Engine.CurrentGameResolution.X / 2, (int)Engine.CurrentGameResolution.Y / 2);
				previousMouseState = Mouse.GetState();
			}

		}

        public void ZoomTo(double zoom)
        {
            ZoomTarget = Math.Max(ZoomMin, Math.Min(ZoomMax, zoom));
        }

        public double GetZoomTarget()
        {
            return ZoomTarget;
        }
        public double GetZoomLevel()
        {
            return ZoomLevel;
        }
		//public void SetZoom(int index)
		//{
		//	ZoomTarget = ZoomValues[index];
        //    ZoomLevel = ZoomTarget;
		//}
        public void SetZoom(double zoom)
        {
            ZoomTarget = zoom;
            ZoomLevel = zoom;
        }



		//Target information
		public bool TargetExists()
		{
			return TargetObject != null;
		}
		public bool TargetIsShip()
		{
			return TargetObject != null && TargetObject.ShipLogic != null;
		}
		public bool TargetIsSolarBody()
		{
			return TargetObject != null && TargetObject.BodyLogic != null;
		}
		public bool TargetCanOrbit()
		{
			return TargetObject != null && TargetObject.Orbit != null;
		}


		public void Update(GameTime gt)
		{
			//Target management
			//if (EntityManager.ContainsEntity(TargetObject) == false)
			//{
			//	if (EntityManager.Entities.Count > 0)
			//	{
			//		TargetObject = EntityManager.Entities[EntityManager.Entities.Count - 1];
			//	}
			//}
			//else if (TargetObject.IsActive == false)
			//{
			//	if (Galaxy.CurrentSolarSystem != null)
			//	{
			//		TargetObject = EntityManager.EntityLookup(Galaxy.CurrentSolarSystem.Entities[0]);
			//	}
			//}

            if (Input.ScrollValueChange != 0)
            {
                ZoomTarget = (Input.ScrollValueChange > 0 ? ZoomTarget * 2 : ZoomTarget / 2);
                ZoomTarget = Math.Max(ZoomMin, Math.Min(ZoomMax, ZoomTarget));
            }


            //Smoothly move the zoom level to match the zoom target
            ZoomLevel += (ZoomTarget - ZoomLevel) / ZoomSmoothing; ;


            //Engine.camera positioning
			if (TargetObject != null)
			{
				Position = TargetObject.Position;
			}
			else
			{
				//Sensitivity = Math.Max((float)Math.Pow(2, (((float)ZoomIndex / (float)(ZoomValues.Count - 1)) - 0.3f) * 8), 0.1f);

				//PositionDifference = new Vector2d(Mouse.GetState().X - previousMouseState.X, Mouse.GetState().Y - previousMouseState.Y);

				//PositionVelocity += PositionDifference;

				//if (TrapMouse)
				//{
				//	Mouse.SetPosition((int)Engine.CurrentScreenResolution.X / 2, (int)Engine.CurrentScreenResolution.Y / 2);
				//}

				//Position -= PositionDifference * Sensitivity;
				Position.X += (Input.KeyDown(Input.Move_Right) ? 0.01 : (Input.KeyDown(Input.Move_Left) ? -0.01 : 0));
				Position.Y += (Input.KeyDown(Input.Move_Backward) ? 0.01 : (Input.KeyDown(Input.Move_Forward) ? -0.01 : 0));
			}


			
			Transform = Matrix.CreateScale(new Vector3((float)ZoomLevel, (float)ZoomLevel, 1)) *
				//Matrix.CreateTranslation(new Vector3((float)-Position.X, (float)-Position.Y, 0)) *
				Matrix.CreateTranslation(new Vector3(Engine.CurrentGameResolution.X * 0.5f, Engine.CurrentGameResolution.Y * 0.5f, 0));


			previousMouseState = Mouse.GetState();
		}
	}
}
