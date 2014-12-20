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
	public class SaveFile
	{
		//Data in this save file
		public string SaveFileName;

		public List<SolarSystem> SolarSystemList { get; set; }
		public List<BaseEntity> Entities { get; set; }
		//public List<Ship> ShipList;

		public string CurrentSolarSystem;

		public string CameraTargetObject;
		public int CameraZoomLevel;


		public SaveFile(){}
		public SaveFile(string name)
		{
			SaveFileName = name;

			Entities = Galaxy.Entities;

			CurrentSolarSystem = (Galaxy.CurrentSolarSystem != null ? Galaxy.CurrentSolarSystem.Name : "");
			SolarSystemList = Galaxy.SolarSystems;

			CameraTargetObject = (Camera.TargetExists() ? Camera.TargetObject.Name : "");
			CameraZoomLevel = Camera.ZoomIndex;
			//ActiveShip = (ShipManager.ActiveShip != null ? ShipManager.ActiveShip.Name : "");

			//Minor changes that must be made to the lists above to avoid extra references
			for (int ii = 0; ii < Entities.Count; ii++)
			{
				if (Entities[ii].Renderer != null)
				{
					Entities[ii].Renderer.TextureIndex = Engine.GetTextureIndex(Entities[ii].Renderer._texture);
					Entities[ii].Renderer._entityName = Entities[ii].Renderer._entity.Name;
					Entities[ii].Renderer._entity = null;
				}
				if (Entities[ii].ShipLogic != null)
				{
					Entities[ii].ShipLogic._entityName = Entities[ii].ShipLogic._entity.Name;
					Entities[ii].ShipLogic._entity = null;
				}
				if (Entities[ii].BodyLogic != null)
				{
					Entities[ii].BodyLogic.ShadowIndex = Engine.GetTextureIndex(Entities[ii].BodyLogic.Shadow);
					Entities[ii].BodyLogic._entityName = Entities[ii].BodyLogic._entity.Name;
					Entities[ii].BodyLogic._entity = null;
				}
				if (Entities[ii].Orbit != null)
				{
					Entities[ii].Orbit._entityName = Entities[ii].Orbit._entity.Name;
					Entities[ii].Orbit._entity = null;
					//parent
					if (Entities[ii].Orbit._parent != null)
					{
						Entities[ii].Orbit._parentName = Entities[ii].Orbit._parent.Name;
						Entities[ii].Orbit._parent = null;
					}
				}
				if (Entities[ii].Particle != null)
				{
					//Remove name reference to emitters
					for (int iii = 0; iii < Entities[ii].Particle.ParticleEmitters.Count; iii++)
					{
						//Entities[ii].Particle.ParticleEmitters[iii].Manager = null;
						Entities[ii].Particle.ParticleEmitters[iii]._entityName = Entities[ii].Particle.ParticleEmitters[iii]._entity.Name;
						Entities[ii].Particle.ParticleEmitters[iii]._entity = null;
					}

					Entities[ii].Particle._entityName = Entities[ii].Particle._entity.Name;
					Entities[ii].Particle._entity = null;
				}
				//ShipSystems
				if (Entities[ii].Communication != null)
				{
					Entities[ii].Communication._entityName = Entities[ii].Communication._entity.Name;
					Entities[ii].Communication._entity = null;
				}

				//SolarSystem ss = SolarSystemList[ii];
				//if (Entities[ii].Parent != null)
				//{
				//	Entities[ii].ParentName = Entities[ii].Parent.Name;
				//	Entities[ii].Parent = null;
				//}
			}
        }
	}
}
