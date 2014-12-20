using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExplorationEngine
{
	class OLD_Gravity
	{
		////First loop
		//foreach (KeyValuePair<string, Entity> e in entityEngine.Entities)
		//{
		//	//Reset the force vector
		//	Force = new Vector2();

		//	//Second loop
		//	foreach (KeyValuePair<string, Entity> e2 in entityEngine.Entities)
		//	{
		//		//Make sure the second value is not the current value from the first loop
		//		if (e2.Value != e.Value )
		//		{
		//			//Find the distance between the two objects. Because Fg = G * ((M1 * M2) / r^2), using Vector2.Distance() and the squaring it
		//			//is pointless and inefficient.
		//			distance = Vector2.DistanceSquared(e2.Value.Position, e.Value.Position);

		//			//This makes sure that two planets do not attract eachother if they are touching, completely unnecessary when I add collision,
		//			//For now it just makes it so that the planets are not glitchy, performance is not significantly improved by removing this IF
		//			if (distance > ((e.Value.Texture.Width * e.Value.Texture.Width) / 2 + (e2.Value.Texture.Width * e2.Value.Texture.Width) / 2))
		//			{
		//				//Calculate the magnitude of Fg (I'm using my own gravitational constant (G) for the sake of time (I know it's 1 at the moment, but I've been changing it)
		//				mult = 1.0f * ((e.Value.Mass * e2.Value.Mass) / distance);

		//				//Calculate the direction of the force, simply subtracting the positions and normalizing works, this fixes diagonal vectors
		//				//from having a larger value, and basically makes VecForce a direction.
		//				VecForce = e2.Value.Position - e.Value.Position;
		//				VecForce.Normalize();

		//				//Add the vector for each planet in the second loop to a force var.
		//				Force = Vector2.Add(Force, VecForce * mult);
		//			}
		//		}
		//	}

		//	//Add that force to the first loop's planet's position (later on I'll instead add to acceleration, to account for inertia)
		//	e.Value.Position += Force;
		//}

		////First loop
		//Parallel.ForEach(Entities, e =>
		//{
		//	//Reset the force vector
		//	Force = new Vector2();

		//	//Second loop
		//	Parallel.ForEach(Entities, e2 =>
		//	{
		//		//Make sure the second value is not the current value from the first loop
		//		if (e != e2)
		//		{
		//			//Find the distance between the two objects. Because Fg = G * ((M1 * M2) / r^2), using Vector2.Distance() and the squaring it
		//			//is pointless and inefficient.
		//			distance = Vector2.DistanceSquared(e2.Position, e.Position);

		//			//This makes sure that two planets do not attract eachother if they are touching, completely unnecessary when I add collision,
		//			//For now it just makes it so that the planets are not glitchy, performance is not significantly improved by removing this IF
		//			//if (distance > (e.MultWidth + e2.MultWidth)) 
		//			//{
		//				//Calculate the magnitude of Fg (I'm using my own gravitational constant (G) for the sake of time (I know it's 1 at the moment, but I've been changing it)
		//				mult = 0.001f * ((e.Mass * e2.Mass) / distance);

		//				//Calculate the direction of the force, simply subtracting the positions and normalizing works, this fixes diagonal vectors
		//				//from having a larger value, and basically makes VecForce a direction.
		//				VecForce = e2.Position - e.Position;
		//				//VecForce.Normalize();

		//				//Add the vector for each planet in the second loop to a force var.
		//				Force = Vector2.Add(Force, VecForce * mult);
		//			//}
		//		}
		//	}
		//	);
		//	//	//Add that force to the first loop's planet's position (later on I'll instead add to acceleration, to account for inertia)
		//	//e.Position += Force;
		//}
		//);


			//		//First loop
			//for (int ii = 0; ii < Entities.Count; ii++)
			//{
			//	Force = Vector2.Zero;

			//	//Second loop
			//	for (int hh = 0; hh < Entities.Count; hh++)
			//	{
			//		//Find the distance between the two objects. Because Fg = G * ((M1 * M2) / r^2), using Vector2.Distance() and the squaring it
			//		//is pointless and inefficient.
			//		distance = Vector2.DistanceSquared(Entities[hh].Position, Entities[ii].Position);

			//		//This makes sure that two planets do not attract eachother if they are touching, completely unnecessary when I add collision,
			//		//For now it just makes it so that the planets are not glitchy, performance is not significantly improved by removing this IF
			//		if (distance > (Entities[ii].MultWidth + Entities[hh].MultWidth)) 
			//		{
			//			//Calculate the magnitude of Fg (I'm using my own gravitational constant (G) for the sake of time (I know it's 1 at the moment, but I've been changing it)
			//			mult = 0.01f * ((Entities[ii].Mass * Entities[hh].Mass) / distance);

			//			//Calculate the direction of the force, simply subtracting the positions and normalizing works, this fixes diagonal vectors
			//			//from having a larger value, and basically makes VecForce a direction.
			//			VecForce = Entities[hh].Position - Entities[ii].Position;
			//			VecForce.Normalize();

			//			//Apply force
			//			Force += (VecForce * mult);

			//			//Entities[ii].Acceleration = VecForce * mult;
			//			//Entities[hh].Acceleration = (VecForce * mult) * -1;
						
			//		}
			//	}

			//	Entities[ii].Acceleration = Force;

			//}


		
			//CudafyModule km = CudafyTranslator.Cudafy();
			//GPGPU gpu = CudafyHost.GetDevice(CudafyModes.Target, CudafyModes.DeviceId);
			//gpu.LoadModule(km);

			//Define our GPU output force variable
			//Vector2[] Force = new Vector2[Entities.Count * Entities.Count];

			//float[] Masses = new float[Entities.Count];
			//float[] Mults = new float[Entities.Count];

			//Allocate a variable in the GPU
			//Vector2[] DEV_Force = gpu.Allocate<Vector2>(Force);

			//float[] DEV_Masses = gpu.Allocate<float>(Masses);
			//float[] DEV_Mults = gpu.Allocate<float>(Mults);


			//Apply mass information to CPU Masses array
			//for (int ii = 0; ii < Entities.Count; ii++)
			//{
			//	Masses[ii] = Entities[ii].Mass;
			//	Mults[ii] = 5;
			//}

			//Copy data to GPU allocated arrays from CPU arrays (Masses --> DEV_Masses)
			//gpu.CopyToDevice(Masses, DEV_Masses);
			//gpu.CopyToDevice(Mults, DEV_Mults);

			//Launch the parallel processing!
			//gpu.Launch(Entities.Count, 1).GravCalc(DEV_Masses, DEV_Mults);

			//First loop
			//for (int ii = 0; ii < Entities.Count; ii++)
			//{

				//Second loop
				//for (int hh = 0; hh < Entities.Count; hh++)
				//{
					//GravCalc();
				//}

				//Entities[ii].Acceleration = Force;

			//}

			//gpu.FreeAll();
		//}

		//[Cudafy]
		//public static void GravCalc(GThread thread, float[] masses, float[] mults)
		//{

			//if (distance > (e.MultWidth + e2.MultWidth))
			//{

				//Apply force
			//DEV_Force = new Vector2(0, 0);
					//((e2.Position - e.Position).Normalize() * (0.01f * ((e.Mass * e2.Mass) / Vector2.DistanceSquared(e.Position, e2.Position))));

				//e.Acceleration = VecForce * mult;
				//e2.Acceleration = (VecForce * mult) * -1;

			//}
		//}


	}
}
