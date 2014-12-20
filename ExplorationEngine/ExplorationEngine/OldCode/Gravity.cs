#region "Using Statements"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
	public class Gravity
	{
		private EntityEngine EE;
		private List<Entity> Entities;

		//private Vector2 Force;
		private float distance;
		//private float mult;
		private Vector2 VecForce;

		public Gravity(EntityEngine e)
		{
			EE = e;
			Entities = e.Entities;
		}



		public void Update()
		{

			//First loop
			for (int ii = 0; ii < Entities.Count - 1; ii++)
			{
				//Force = Vector2.Zero;

				//Second loop
				for (int hh = ii + 1; hh < Entities.Count; hh++)
				{
					//Find the distance between the two objects. Because Fg = G * ((M1 * M2) / r^2), using Vector2.Distance() and the squaring it
					//is pointless and inefficient.
					distance = Vector2.DistanceSquared(Entities[hh].Position, Entities[ii].Position);

					//This makes sure that two planets do not attract eachother if they are touching, completely unnecessary when I add collision,
					//For now it just makes it so that the planets are not glitchy, performance is not significantly improved by removing this IF
					if (distance > (Entities[ii].MultWidth + Entities[hh].MultWidth))
					{
						//Calculate the magnitude of Fg (I'm using my own gravitational constant (G) for the sake of time (I know it's 1 at the moment, but I've been changing it)
						//mult = 0.5f * ((Entities[ii].Mass * Entities[hh].Mass) / distance);

						//Calculate the direction of the force, simply subtracting the positions and normalizing works, this fixes diagonal vectors
						//from having a larger value, and basically makes VecForce a direction.
						//VecForce = Entities[hh].Position - Entities[ii].Position;
						//VecForce.Normalize();

						//Apply force
						//Force += (VecForce * mult);

						VecForce = (Vector2.Normalize((Entities[hh].Position - Entities[ii].Position)) * (0.5f * ((Entities[ii].Mass * Entities[hh].Mass) / distance)));
						Entities[ii].Acceleration += VecForce;
						Entities[hh].Acceleration += VecForce * -1;

					}
					else
					{
						Entities[ii].Scale += (Entities[hh].Scale/2);
						Entities[ii].Mass += Entities[hh].Mass;
						Entities[ii].Texture = SolarSystems.Planet_Debug_Medium;
						Entities[ii].Acceleration += (Entities[hh].Acceleration/100) * Entities[hh].Scale;

						EE.DestroyEntity(hh);
					}
				}

				//Entities[ii].Acceleration = Force;
			}


		}

	}
	 */
}
