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
	public class ShipSystem_Thruster : Component
	{
		public override int ID { get; set; }

		public BaseEntity _entity;
		public string _entityName;
		
		//Thruster properties
		public bool State = false;

		//Key inputs
		public bool Toggle = false;
		public Keys Key;

		public ShipSystem_Thruster() { }
		public ShipSystem_Thruster(BaseEntity entity)
		{
			ID = 2;
			_entity = entity;

		}


		public override void SelfDestruct()
		{

		}

		public override void Update()
		{
			if (Toggle)
			{
				if (Input.KeyDown(Key))
				{
					State = !State;
				}
			}
			else
			{
				State = Input.KeyDown(Key);
			}
		}

	}
}
