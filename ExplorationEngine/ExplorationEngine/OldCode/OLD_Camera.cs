#region "Using Statements"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion


namespace ExplorationEngine
{
	/*
		public static class Camera
	{
		public static Matrix Transform = Matrix.CreateTranslation(Engine.CurrentScreenResolution.X * 0.5f, Engine.CurrentScreenResolution.Y * 0.5f, 0);
		//public static Matrix GameTransform = Matrix.CreateTranslation(Engine.CurrentScreenResolution.X * 0.5f, Engine.CurrentScreenResolution.Y * 0.5f, 0);
		//public static Matrix MapTransform = Matrix.CreateTranslation(Engine.CurrentScreenResolution.X * 0.5f, Engine.CurrentScreenResolution.Y * 0.5f, 0);

		public static Vector2 Position = Vector2.Zero;
		public static Vector2 PositionDifference = Vector2.Zero;

		public static float Rotation = 0.0f;
		public static float Sensitivity;

		public static float Zoom;
		public static float ZoomMax;
		public static float ZoomMin;
		public static int Magnification;
		public static int ScrollValue;

		public static bool TrapMouse = false;

		private static List<float> ZoomValues = new List<float>() { 4.0f, 3.9f, 3.8f, 3.7f, 3.6f, 3.5f, 3.4f, 3.3f, 3.2f, 3.1f, 3.0f, 2.9f, 2.8f, 2.7f, 2.6f, 
																 2.5f, 2.4f, 2.3f, 2.2f, 2.1f, 2.0f, 1.9f, 1.8f, 1.7f, 1.6f, 1.5f, 1.4f, 1.3f, 1.2f, 1.1f, 
																 1.0f, 0.9f, 0.8f, 0.7f, 0.6f, 0.5f, 0.4f, 0.3f, 0.2f, 0.1f, 0.09f, 0.08f, 0.07f, 0.06f, 
																 0.05f, 0.04f, 0.03f, 0.02f, 0.01f, 0.005f};


		//Mouse
		public static MouseState previousMouseState;

		static Camera()
        {
			Zoom = 0.1f;
        }


		public static void Reset()
		{
			//previousMouseState = Mouse.GetState();
			//ScrollValue = -1080;
			//Position = Vector2.Zero;
			//Rotation = 0f;
		}


		public static void Update(bool isactive)
		{
			if (TrapMouse)
			{
				PositionDifference = new Vector2(Mouse.GetState().X - previousMouseState.X, Mouse.GetState().Y - previousMouseState.Y);
				Position -= PositionDifference;
				Mouse.SetPosition((int)Engine.CurrentScreenResolution.X / 2, (int)Engine.CurrentScreenResolution.Y / 2);
			}

			if (Engine.GetGamestate() == Engine.GameState.Game || Engine.GetGamestate() == Engine.GameState.Map)
			{
				ScrollValue += Mouse.GetState().ScrollWheelValue - previousMouseState.ScrollWheelValue;
				ScrollValue = Math.Max(Math.Min(ScrollValue, ZoomValues.IndexOf(1.0f) * 120), -(ZoomValues.Count - ZoomValues.IndexOf(1.0f) - 1) * 120);

				Magnification = Math.Abs(ScrollValue / 120);

				Zoom = ZoomValues[(ScrollValue < 0 ? 30 + Magnification : 30 - Magnification)]; //Math.Min(Math.Max(, ZoomMax), ZoomMin);

				Sensitivity = (Zoom < 1 ? ScrollValue / 120f * -(Zoom < 0.03f ? 3f : 1f) : 1f);


				Transform = Matrix.CreateTranslation(new Vector3(Position.X, Position.Y, 0) * Sensitivity) *
					Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
					Matrix.CreateTranslation(new Vector3(Engine.CurrentScreenResolution.X * 0.5f, Engine.CurrentScreenResolution.Y * 0.5f, 0));
			}

			previousMouseState = Mouse.GetState();
		}
	}
	 */

	/*
	public static class Camera
	{
		public static Matrix GameTransform = Matrix.CreateTranslation(Engine.CurrentScreenResolution.X * 0.5f, Engine.CurrentScreenResolution.Y * 0.5f, 0);
		public static Matrix MapTransform = Matrix.CreateTranslation(Engine.CurrentScreenResolution.X * 0.5f, Engine.CurrentScreenResolution.Y * 0.5f, 0);

		public static Vector2 Position = Vector2.Zero;
		public static float Rotation = 0.0f;
		public static float mult;
		public static float Zoom;
		public static float zoomMax;
		public static float zoomMin;
		public static Vector2 offset;
		private static List<float> ZoomValues = new List<float>() { 4.0f, 3.9f, 3.8f, 3.7f, 3.6f, 3.5f, 3.4f, 3.3f, 3.2f, 3.1f, 3.0f, 2.9f, 2.8f, 2.7f, 2.6f, 
																 2.5f, 2.4f, 2.3f, 2.2f, 2.1f, 2.0f, 1.9f, 1.8f, 1.7f, 1.6f, 1.5f, 1.4f, 1.3f, 1.2f, 1.1f, 
																 1.0f, 0.9f, 0.8f, 0.7f, 0.6f, 0.5f, 0.4f, 0.3f, 0.2f, 0.1f, 0.09f, 0.08f, 0.07f, 0.06f, 
																 0.05f, 0.04f, 0.03f, 0.02f, 0.01f, 0.005f};
		public static bool IsMouseInGame;


		//Mouse
		public static MouseState previousMouseState;
		public static MouseState previousMapMouseState;

		public static int ScrollValue;

		static Camera()
        {
			//Initially set the mouse prev state and center it
			GameMouse(true);

			//Zoom = ZoomValues[ZoomValues.IndexOf(0.1f)];
			ScrollValue = -1100;
        }


		public static void Reset()
		{
			previousMouseState = Mouse.GetState();
			ScrollValue = -1080;
			Position = Vector2.Zero;
			Rotation = 0f;
		}


		public static Matrix get_transformation()
		{
			if (IsMouseInGame)
			{
				GameTransform =
				  Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
											 Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
											 Matrix.CreateTranslation(new Vector3(-offset.X, -offset.Y, 0)) *
											 Matrix.CreateTranslation(new Vector3(Engine.CurrentScreenResolution.X * 0.5f, Engine.CurrentScreenResolution.Y * 0.5f, 0));
				//										 Matrix.CreateRotationZ(Rotation) *
			}

			return GameTransform;
		
		}
		
		public static Matrix get_transformationMap()
		{
			//if (Mouse.GetState().LeftButton == ButtonState.Pressed)
			//{


			MapTransform = Matrix.CreateTranslation(new Vector3(-Position.X / 12, -Position.Y / 12, 0)) *
						Matrix.CreateScale(new Vector3(Zoom * 12, Zoom * 12, 1)) *
						Matrix.CreateTranslation(new Vector3(-offset.X, -offset.Y, 0)) *
						Matrix.CreateTranslation(new Vector3(Engine.CurrentScreenResolution.X * 0.5f, Engine.CurrentScreenResolution.Y * 0.5f, 0));
			//										 Matrix.CreateRotationZ(Rotation) *

			//MapTransform = Matrix.CreateTranslation(new Vector3(0, 0, 0));
			 //* Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
			//MapTransform = Matrix.CreateTranslation(new Vector3(Map.Position.X, Map.Position.Y, 0)) *
			//	Matrix.CreateTranslation(new Vector3(Engine.CurrentScreenResolution.X * 0.5f, Engine.CurrentScreenResolution.Y * 0.5f, 0)) * 
			//	Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));
			//Matrix.CreateTranslation(new Vector3(-Map.Position.X, -Map.Position.Y, 0)) * Matrix.CreateScale(new Vector3(Zoom, Zoom, 1));

				//MapTransform = Matrix.CreateTranslation(new Vector3( (Mouse.GetState().X - (float)previousMapMouseState.X), (Mouse.GetState().Y - (float)previousMapMouseState.Y), 0));
				//Map.Position = (new Vector2(Mouse.GetState().X, Mouse.GetState().Y) - new Vector2(previousMapMouseState.X, previousMapMouseState.Y));
			//}

			return MapTransform;
		}


		//OnOff
		public static void GameMouse(bool gamemouse)
		{
			if (gamemouse)
			{
				Mouse.SetPosition((int)Engine.CurrentScreenResolution.X / 2, (int)Engine.CurrentScreenResolution.Y / 2);
				previousMouseState = Mouse.GetState();
			}
			IsMouseInGame = gamemouse;
		}



		public static void Update(bool isactive)
		{
			//Map zoom limit
			if (Engine.GetGamestate()== Engine.GameState.Map)
			{
				zoomMax = 0.03f;
				zoomMin = 0.3f;
			}
			else
			{
				zoomMax = ZoomValues[ZoomValues.Count - 1];
				zoomMin = ZoomValues[0];
			}


			if (isactive)
			{
				float XDiff = previousMouseState.X - Mouse.GetState().X;
				float YDiff = previousMouseState.Y - Mouse.GetState().Y;


				ScrollValue += Mouse.GetState().ScrollWheelValue - previousMouseState.ScrollWheelValue;
				ScrollValue = Math.Max(Math.Min(ScrollValue, ZoomValues.IndexOf(1.0f) * 120), -(ZoomValues.Count - ZoomValues.IndexOf(1.0f) - 1) * 120);

				int SN = Math.Abs(ScrollValue / 120);
				Zoom = Math.Min(Math.Max(ZoomValues[(ScrollValue < 0 ? 30 + SN : 30 - SN)], zoomMax), zoomMin);


				mult = (Zoom < 1 ? ScrollValue / 120f * -(Zoom < 0.03f ? 3f : 1f) : 1f);
				Position -= new Vector2(XDiff * mult, YDiff * mult);

				if (IsMouseInGame)
				{
					Mouse.SetPosition((int)Engine.CurrentScreenResolution.X / 2, (int)Engine.CurrentScreenResolution.Y / 2);
				}
				previousMouseState = Mouse.GetState();

			}


		}
	}
	 */
}
