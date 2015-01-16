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

namespace ExplorationEngine.Containers
{
	public class Container_Ship : BaseEntity
	{
		//Entities
		public List<BaseEntity> Entities = new List<BaseEntity>();
		//private List<Vector2d> Points = new List<Vector2d>();
		//public BaseEntity Base = null;

		//Galaxy relations
		//public SolarSystem ParentSolarSystem;

		//Positional entities
		//public BaseEntity CenterOfMass = null;
		//public BaseEntity PositionalCenter = null;

		//Properties
		public double TotalMass = 1;
		public float AverageScale = 1;

		//Other
		//private int PreviousEntitiesInSolarSystem = 0;


		public Container_Ship() { }
		public Container_Ship(string name, Vector2d initialPosition, float initialAngle, float initialScale)
			: base(name, initialPosition, initialAngle, initialScale)
		{
			//Base = BaseEnt;

			//CenterOfMass = Galaxy.CreateEntity(solarsystem, Base.Name + "_CenterOfMass");
			//PositionalCenter = Galaxy.CreateEntity(solarsystem, Base.Name + "_PositionalCenter");

			//Parent markers to base entity
			//CenterOfMass.Parent = Base;
			//PositionalCenter.Parent = Base;
			//Base.AddChild(CenterOfMass);
			//Base.AddChild(PositionalCenter);

			//Don't allow debug
			//CenterOfMass.AllowDebug = false;
			//PositionalCenter.AllowDebug = false;


			//CenterOfMass.Z = Galaxy.MaxZ - 1;
			//PositionalCenter.Z = Galaxy.MaxZ - 1;

			//CenterOfMass.Renderer = new Component_Render(CenterOfMass, null);
			//PositionalCenter.Renderer = new Component_Render(PositionalCenter, null);

			//CenterOfMass.Renderer._debugTexture = Engine.Debug_MarkerBY;
			//PositionalCenter.Renderer._debugTexture = Engine.Debug_MarkerBP;


			//Update things
			ShipRefresh();
		}


		//Add and remove entities
		public void AddEntity(BaseEntity ent)
		{
			if (!Entities.Contains(ent))
			{
				Entities.Add(ent);
			}

		}
		public void AddEntities(List<BaseEntity> entities)
		{
			for (int ii = 0; ii < entities.Count; ii++)
			{
				if (!Entities.Contains(entities[ii]) && entities[ii] != CenterOfMass && entities[ii] != PositionalCenter)
				{
					Entities.Add(entities[ii]);

					//Add the children's children and onward
					if (entities[ii].Children.Count > 0)
						AddEntities(entities[ii].Children);
				}
			}
		}
		public void RemoveEntity(BaseEntity ent)
		{
			if (Entities.Contains(ent))
			{
				Entities.Remove(ent);
			}
		}
		public void RemoveEntities(List<BaseEntity> entities)
		{
			for (int ii = 0; ii < entities.Count; ii++)
			{
				if (Entities.Contains(entities[ii]))
				{
					Entities.Remove(entities[ii]);
				}
			}
		}
		

		//Ship information refreshing
		public void ShipRefresh()
		{
			//Entities.Clear();

			//AddEntities(Base.Children);

			//CalculateCenters();
		}


		//Calculate center of mass and positional center (as well as total mass)
		public void CalculateCenters()
		{

			//Reset total mass
			//TotalMass = Base.Mass;
			//AverageScale = Base.Scale;
			//CenterOfMass.ParentOffset = Vector2d.Zero;
			//PositionalCenter.ParentOffset = Vector2d.Zero;
			//Points.Clear();

			double x = 0;
			double y = 0;
			//If our ship has at least one entity
			if (Entities.Count > 0)
			{
				for (int ii = 0; ii < Entities.Count; ii++)
				{
					//System.Windows.Forms.MessageBox.Show("for loop");
					//Physical center regardless of mass
					BaseEntity parent = Entities[ii];
					while (parent.Parent != null && parent != null)
					{
						//System.Windows.Forms.MessageBox.Show("while loop:" + parent.Name);
						x += parent.ParentOffset.X;
						y += parent.ParentOffset.Y;

						//System.Windows.Forms.MessageBox.Show("x:" + x.ToString() + ", y:" + y.ToString());

						parent = parent.Parent;
					}

					//x += Entities[ii].ParentOffset.X;
					//y += Entities[ii].ParentOffset.Y;

					
					//Points.Add(new Vector2d(x, y));

					PositionalCenter.ParentOffset.X = x;
					PositionalCenter.ParentOffset.Y = y;

					//Center of mass
					CenterOfMass.ParentOffset.X = Entities[ii].Mass * x;
					CenterOfMass.ParentOffset.Y = Entities[ii].Mass * y;

					//Total mass of the ship
					TotalMass += Entities[ii].Mass;

					//Average scale
					AverageScale += Entities[ii].Scale;
				}

				//Add all the points to the positional center
				//int num = 0;
				//for (int ii = 0; ii < Points.Count; ii++)
				//{
				//	for (int iii = 0; iii < Points.Count; iii++)
				//	{
				//		if (Points[iii] == Points[ii] && iii != ii)
				//		{
				//			num++;
				//			Points.RemoveAt(iii);
				//		}
				//	}
				//}
				//System.Windows.Forms.MessageBox.Show((Points.Count + 1).ToString());
				//Find Average scale for scaling COM and PC markers
				AverageScale /= (Entities.Count > 0 ? Entities.Count : 1);
				CenterOfMass.Scale = AverageScale;
				PositionalCenter.Scale = AverageScale;

				//Divide by total mass to scale the position, but account for the possibility of a ship with 0 mass
				//CenterOfMass.ParentOffset.X /= (TotalMass == 0 ? 1 : TotalMass);
				//CenterOfMass.ParentOffset.Y /= (TotalMass == 0 ? 1 : TotalMass);
				//PositionalCenter.ParentOffset.X /= (Entities.Count + 1 - num);
				//PositionalCenter.ParentOffset.Y /= (Entities.Count + 1 - num);

				//Offset the position based on what entity in the ship the markers is parented to
				CenterOfMass.ParentOffset = CenterOfMass.Parent.Position + CenterOfMass.ParentOffset;
				//PositionalCenter.ParentOffset = PositionalCenter.Parent.Position + PositionalCenter.ParentOffset;

			}
		}

	}
}
