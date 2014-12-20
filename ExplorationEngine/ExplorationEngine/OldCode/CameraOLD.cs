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

/*
namespace ExplorationEngine
{
	public static class Camera
	{
		public static Matrix Transform = Matrix.CreateTranslation(Engine.CurrentScreenResolution.X * 0.5f, Engine.CurrentScreenResolution.Y * 0.5f, 0);
		//public static Matrix GameTransform = Matrix.CreateTranslation(Engine.CurrentScreenResolution.X * 0.5f, Engine.CurrentScreenResolution.Y * 0.5f, 0);
		//public static Matrix MapTransform = Matrix.CreateTranslation(Engine.CurrentScreenResolution.X * 0.5f, Engine.CurrentScreenResolution.Y * 0.5f, 0);

		public static BaseEntity TargetObject;
		public static BaseEntity PrevTargetObject;

		public static Vector2d Position = Vector2d.Zero;
		public static Vector2d PositionDifference = Vector2d.Zero;
		public static Vector2d PositionVelocity = Vector2d.Zero;
		public static Vector2d PrevPosition = Vector2d.Zero;

		public static float Rotation = 0.0f;
		public static float Sensitivity;

		public static float Zoom;
		public static int ZoomMax;
		public static int ZoomMin;
		public static int ZoomIndex;
		public static float ZoomSmooth;
		public static int ScrollValueChange;

		//State Previous zoom
		public static int PrevGameZoom;
		public static int PrevMapZoom;

		public static bool TrapMouse = false;

		public static List<float> ZoomValues = new List<float>() { 2048.0f, 1024.0f, 512.0f, 256.0f, 128.0f, 64.0f, 32.0f, 16.0f, 12.0f, 8.0f, 6.0f, 4.0f, 3.0f, 2.0f, 1.0f, 
																0.9f, 0.8f, 0.7f, 0.6f, 0.5f, 0.4f, 0.3f, 0.2f, 0.15f, 0.1f, 0.09f, 0.08f, 0.07f, 0.06f, 
																0.05f, 0.04f, 0.03f, 0.02f, 0.015f, 0.01f, 0.0075f, 0.005f, 0.00145f, 0.0005f, 0.000025f};


		//Mouse
		public static MouseState previousMouseState;

		static Camera()
        {
			//Mouse.SetPosition((int)Engine.CurrentScreenResolution.X / 2, (int)Engine.CurrentScreenResolution.Y / 2);
			previousMouseState = Mouse.GetState();

			ZoomMax = ZoomValues.Count - 1;
			ZoomMin = 0;

			PrevMapZoom = ZoomValues.IndexOf(0.06f);
			PrevGameZoom = ZoomValues.IndexOf(0.06f);
			ZoomIndex = ZoomValues.IndexOf(128.0f);
        }


		public static void ResetMouse()
		{
			if (TrapMouse)
			{
				Mouse.SetPosition((int)Engine.CurrentScreenResolution.X / 2, (int)Engine.CurrentScreenResolution.Y / 2);
				previousMouseState = Mouse.GetState();
			}

			ZoomMax = ZoomValues.Count - 1;
			ZoomMin = 0;
		}


		public static void SetZoom(int index)
		{
			ZoomIndex = index;
			ZoomSmooth = ZoomValues[index];
			Zoom = ZoomValues[index];
		}

		//Target information
		public static bool TargetExists()
		{
			return TargetObject != null;
		}
		public static bool TargetIsShip()
		{
			return TargetObject != null && TargetObject.ShipLogic != null;
		}
		public static bool TargetIsSolarBody()
		{
			return TargetObject != null && TargetObject.BodyLogic != null;
		}
		public static bool TargetCanOrbit()
		{
			return TargetObject != null && TargetObject.Orbit != null;
		}


		public static void Update(GameTime gt)
		{
			//Target management
			if (EntityManager.ContainsEntity(TargetObject) == false)
			{
				if (EntityManager.Entities.Count > 0)
				{
					TargetObject = EntityManager.Entities[EntityManager.Entities.Count - 1];
				}
			}
			else if (TargetObject.IsActive == false)
			{
				if (Galaxy.CurrentSolarSystem != null)
				{
					TargetObject = EntityManager.EntityLookup(Galaxy.CurrentSolarSystem.Entities[0]);
				}
			}


			
			if (Engine.UpdateGame)
			{
				//Everything about this is wrong.
				if (Engine.ActivePage == Engine.HUD || Engine.ActivePage == Engine.Map)
				{
					ZoomIndex -= Input.ScrollValueChange;
					ZoomIndex = Math.Max(Math.Min(ZoomIndex, ZoomValues.Count - 1), 0); //Clamp zoom index value
					ZoomIndex = Math.Max(Math.Min(ZoomIndex, ZoomMax), ZoomMin);

					ZoomSmooth += (ZoomValues[ZoomIndex] - Zoom) / 40;
					Zoom = ZoomSmooth;
				}


				if (TargetObject != null)
				{
					Position = new Vector2d(100,100);// +TargetObject.Velocity; // I don't get why I have to do this, it seems like there's a problem elsewhere.
				}
				//else
				//{
					//Sensitivity = Math.Max((float)Math.Pow(2, (((float)ZoomIndex / (float)(ZoomValues.Count - 1)) - 0.3f) * 8), 0.1f);

					//PositionDifference = new Vector2d(Mouse.GetState().X - previousMouseState.X, Mouse.GetState().Y - previousMouseState.Y);

					//PositionVelocity += PositionDifference;

					//if (TrapMouse)
					//{
					//	Mouse.SetPosition((int)Engine.CurrentScreenResolution.X / 2, (int)Engine.CurrentScreenResolution.Y / 2);
					//}

					//Position -= PositionDifference * Sensitivity;
				//}

			}


			Transform = Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
				Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
				Matrix.CreateTranslation(new Vector3(Engine.CurrentScreenResolution.X * 0.5f, Engine.CurrentScreenResolution.Y * 0.5f, 0));


			previousMouseState = Mouse.GetState();
		}
	}
}
*/