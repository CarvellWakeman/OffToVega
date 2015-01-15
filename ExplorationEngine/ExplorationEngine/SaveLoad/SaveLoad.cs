#region "Using Statements"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace ExplorationEngine
{
	public class SaveLoad
	{

		//Saves
		public SortedList<string, string> Saves; //Name, filename
		public SaveFile CurrentSaveFile;
		public OptionsSaveFile OptionsSaveFile;
		public ClassificationsSaveFile ClassificationsSaveFile;

		//File locations
		public string Documents;
		public string BaseSaveFolder;
		public string SaveGameFolder;

		public string SaveDirectory;
		public string SaveGamesDirectory;
		
		public string SavePath;


		public SaveLoad()
		{
			//Variable setup
				//Saves
				Saves = new SortedList<string, string>();//Name, filename

				//File locations
				Documents = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				BaseSaveFolder = "\\Off To Vega";
				SaveGameFolder = "\\SaveGames";

				SaveDirectory = Documents + BaseSaveFolder;
				SaveGamesDirectory = Documents + BaseSaveFolder + SaveGameFolder;
		

			//Make sure we have the proper directories
			DirectoryCheck();

			//Load up the list of save files
			ReloadSaveFiles();
		}


		public void DirectoryCheck()
		{
			//Create the save folder main directory if it doesn't exist
			if (!System.IO.Directory.Exists(Documents + BaseSaveFolder))
			{
				System.IO.Directory.CreateDirectory(Documents + BaseSaveFolder);
			}

			//Create the SaveGames subdirectory if it doesn't exist and the main directory does
			if (System.IO.Directory.Exists(Documents + BaseSaveFolder) && !System.IO.Directory.Exists(Documents + BaseSaveFolder + SaveGameFolder))
			{
				System.IO.Directory.CreateDirectory(Documents + BaseSaveFolder + SaveGameFolder);
			}
		}

		public void ReloadSaveFiles()
		{
			DirectoryCheck();

			Saves.Clear();

			DirectoryInfo d = new DirectoryInfo(SaveGamesDirectory);
			foreach (var file in d.GetFiles("*.xml"))
			{
				Saves.Add(file.Name.Remove(file.Name.IndexOf('.')), file.DirectoryName);
				//System.Windows.Forms.MessageBox.Show(file.FullName);
			}
		}


		private int retryAttempts = 0;
        public void SaveOptions() //Options saving
        {
			DirectoryCheck();

			if (retryAttempts <= 2)
			{
				//Set settings in options file
				OptionsSaveFile = new OptionsSaveFile();

				//Save the file
				SavePath = Path.Combine(SaveDirectory, "options.xml"); //Set the current saved path to

				XmlSerializer serializer = new XmlSerializer(OptionsSaveFile.GetType()); //create a new XmlSerializer for use in serializing

				//Try saving
				try
				{
					//File.Delete(SavePath);

					//create the file (overwrite if it exists) and serialize the object.
					using (FileStream stream = new FileStream(SavePath, FileMode.Create))//File.Open(SavePath, FileMode.Create, FileAccess.Write))
					{
						serializer.Serialize(stream, OptionsSaveFile);
					}
					retryAttempts = 0;
				}
				catch
				{
					retryAttempts++;
					SaveOptions();
				}
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Could not access options file, changes were not saved.");
			}
        }
		public void SaveClassifications() //Classifications saving
		{
			DirectoryCheck();

			if (retryAttempts <= 2)
			{
				//Set classifications file
				ClassificationsSaveFile = new ClassificationsSaveFile("classifications");
				//ClassificationsSaveFile.LoadData();

				//Save the file
				SavePath = Path.Combine(SaveDirectory, "classifications.xml"); //Set the current saved path to

				XmlSerializer serializer = new XmlSerializer(ClassificationsSaveFile.GetType()); //create a new XmlSerializer for use in serializing

				//Try saving
				try
				{
					//File.Delete(SavePath);

					//create the file (overwrite if it exists) and serialize the object.
					using (FileStream stream = new FileStream(SavePath, FileMode.Create))//File.Open(SavePath, FileMode.Create, FileAccess.Write))
					{
						serializer.Serialize(stream, ClassificationsSaveFile);
					}
					retryAttempts = 0;
				}
				catch
				{
					retryAttempts++;
					SaveClassifications();
				}
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Could not access classifications file, changes were not saved.");
			}
		}
		public void Save(SaveFile saveFile) //Game save file saving
		{
			DirectoryCheck();

			CurrentSaveFile = null;

			SavePath = Path.Combine(SaveGamesDirectory, saveFile.SaveFileName + ".xml"); //Set the current saved path to

			//Try to save
			try
			{
				//if (File.Exists(SavePath))
				//{
					//File.Delete(SavePath);

					XmlSerializer serializer = new XmlSerializer(saveFile.GetType()); //create a new XmlSerializer for use in serializing

					//create the file (overwrite if it exists) and serialize the object.
					using (FileStream stream = new FileStream(SavePath, FileMode.Create))
					{
						serializer.Serialize(stream, saveFile);
					}
				//}
			}
			catch (Exception ex)
			{
				throw new Exception("Error while saving file '" + saveFile.SaveFileName + "' to: " + SavePath + " with error: " + ex.ToString(), ex);
			}

			
		}

        public bool LoadOptions() //Options loading
        {
			DirectoryCheck();

            SavePath = Path.Combine(SaveDirectory, "options.xml"); //Define the current save path

            //The save file that gets returned
            OptionsSaveFile SaveFileReturn;

            //Try to load
            try
            {
                //if the file exists
                if (File.Exists(SavePath))
                {
					XmlSerializer serializer = new XmlSerializer(typeof(OptionsSaveFile)); //create a new XmlSerializer for use in deserializing

                    //open the file and deserialize. The 'using' statement makes sure the file is closed again, even if an error occurs.
                    //using (FileStream stream = File.Open(SavePath, FileMode.Open, FileAccess.Read))
					using(FileStream stream = new FileStream(SavePath, FileMode.Open))
                    {

						SaveFileReturn = (OptionsSaveFile)serializer.Deserialize(stream);
                    }
					

                    //Set the current options file
                    OptionsSaveFile = SaveFileReturn;

					
                    //Load the data as game objects
                    if (SaveFileReturn == null)
                    {
                        System.Windows.Forms.MessageBox.Show("Error loading settings file.");
                    }
                    else
                    {
                        //Options
                        Engine.DebugState = SaveFileReturn.DebugState;
						Engine.DebugGUI = SaveFileReturn.DebugGUI;
						
						Engine.SetResolution((int)SaveFileReturn.ScreenResolution.X, (int)SaveFileReturn.ScreenResolution.Y, SaveFileReturn.FullScreen);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("Error loading options save file:" + SavePath + " with error: " + ex.ToString(), ex);
            }
        }
		public bool LoadClassifications() //classifications loading
		{
			DirectoryCheck();

			SavePath = Path.Combine(SaveDirectory, "classifications.xml"); //Define the current save path

			//The save file that gets returned
			ClassificationsSaveFile SaveFileReturn;

			//Try to load
			try
			{
				//if the file exists
				if (File.Exists(SavePath))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(ClassificationsSaveFile)); //create a new XmlSerializer for use in deserializing

					//open the file and deserialize. The 'using' statement makes sure the file is closed again, even if an error occurs.
					//using (FileStream stream = File.Open(SavePath, FileMode.Open, FileAccess.Read))
					using (FileStream stream = new FileStream(SavePath, FileMode.Open))
					{
						SaveFileReturn = (ClassificationsSaveFile)serializer.Deserialize(stream);
					}


					//Set the current options file
					ClassificationsSaveFile = SaveFileReturn;


					//Load the data as game objects
					if (SaveFileReturn == null)
					{
						System.Windows.Forms.MessageBox.Show("Error loading classifications file.");
					}
					else
					{
						//Load classifications
						Engine.stellarClassifications.Classifications.Clear();
						Engine.stellarClassifications.Classifications = SaveFileReturn.Classifications;

						//Convert the texture indexes from the classifications into textures
						if (Engine.stellarClassifications.Classifications.Count > 0)
						{
							for (int ii = 0; ii < Engine.stellarClassifications.Classifications.Count; ii++)
							{
								if (Engine.stellarClassifications.Classifications[ii].TextureIndexes.Count > 0)
								{
									Engine.stellarClassifications.Classifications[ii].Textures.Clear();

									for (int iii = 0; iii < Engine.stellarClassifications.Classifications[ii].TextureIndexes.Count; iii++)
									{
										Engine.stellarClassifications.Classifications[ii].Textures.Add(Engine.GetTexture(Engine.stellarClassifications.Classifications[ii].TextureIndexes[iii]));
									}
								}
							}
						}
					}
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				return false;
				throw new Exception("Error loading options save file:" + SavePath + " with error: " + ex.ToString(), ex);
			}
		}
		public bool Load(string name) //Load game save Files
		{
			DirectoryCheck();

			SavePath = Path.Combine(SaveGamesDirectory, name + ".xml"); //Define the current save path

			//The save file that gets returned
			SaveFile SaveFileReturn;

			//Try to load
			try
			{
				//if the file exists
				if (File.Exists(SavePath))
				{
					
					XmlSerializer serializer = new XmlSerializer(typeof(SaveFile)); //create a new XmlSerializer for use in deserializing

					//open the file and deserialize. The 'using' statement makes sure the file is closed again, even if an error occurs.
					using (FileStream stream = File.Open(SavePath, FileMode.Open, FileAccess.Read))
					{
						SaveFileReturn = (SaveFile)serializer.Deserialize(stream);
					}

                    //Set the current save file
                    CurrentSaveFile = SaveFileReturn;

					
                    //Load the data as game objects
                    if (SaveFileReturn == null)
                    {
                        System.Windows.Forms.MessageBox.Show("Save file:" + name + " contains no data.");
                    }
                    else
                    {
						Engine.Reset();


						//Replicate SolarSystems
						//Galaxy.SolarSystems = SaveFileReturn.SolarSystemList;
						foreach (SolarSystem s in SaveFileReturn.SolarSystemList)
						{
							//Create SolarSystem
							SolarSystem TempSolarSystem = new SolarSystem(s.Name, s.MapDotPosition);
							TempSolarSystem.CameraTargetObject = s.CameraTargetObject;
							//TempSolarSystem.PrevGameZoom = s.PrevGameZoom;
							//TempSolarSystem.PrevMapZoom = s.PrevMapZoom;
							TempSolarSystem.Zoom = s.Zoom;
							TempSolarSystem.Entities = s.Entities;
							

							//Add this solarsystem to the entities list
							Galaxy.SolarSystems.Add(TempSolarSystem);

						}
						//Galaxy.SolarSystems = SaveFileReturn.SolarSystemList;


						//Replicate Entities
						foreach (BaseEntity e in SaveFileReturn.Entities)
						//for (int ii = 0; ii < SaveFileReturn.Entities.Count; ii++)
						{
							//BaseEntity e = SaveFileReturn.Entities[ii];
							BaseEntity ent = Galaxy.CreateEntity(e.Name);
							
							ent.Angle = e.Angle;
							ent.AngularVelocity = e.AngularVelocity;

							ent.IsActive = e.IsActive;

							ent.Acceleration = e.Acceleration;
							ent.Velocity = e.Velocity;
							ent.Position = e.Position;

							ent.Mass = e.Mass;
							ent.Scale = e.Scale;

							ent.Renderer = (e.Renderer != null ? e.Renderer : null);
							ent.ShipLogic = (e.ShipLogic != null ? e.ShipLogic : null);
							ent.BodyLogic = (e.BodyLogic != null ? e.BodyLogic : null);
							ent.Orbit = (e.Orbit != null ? e.Orbit : null);
							ent.Particle = (e.Particle != null ? e.Particle : null);
							ent.Communication = (e.Communication != null ? e.Communication : null);

							
							if (ent.Renderer != null)
							{
								ent.Renderer._texture = Engine.GetTexture(ent.Renderer.TextureIndex);
								ent.Renderer._entity = ent;
								ent.Renderer._entityName = "";
							}
							if (ent.ShipLogic != null)
							{
								ent.ShipLogic._entity = ent;
								ent.ShipLogic._entityName = "";
							}
							if (ent.BodyLogic != null)
							{
								ent.BodyLogic.Shadow = Engine.GetTexture(ent.BodyLogic.ShadowIndex);
								ent.BodyLogic._entity = ent;
								ent.BodyLogic._entityName = "";
							}
							if (ent.Orbit != null)
							{
								ent.Orbit._entity = ent;
								ent.Orbit._entityName = "";
								//ParentName is converted within OrbitComponent, that way the creation order of entities doesn't matter.
							}
							if (ent.Particle != null)
							{
								ent.Particle._entity = ent;
								ent.Particle._entityName = "";

								//Replace emitters
								for (int iii = 0; iii < ent.Particle.ParticleEmitters.Count; iii++)
								{
									ent.Particle.ParticleEmitters[iii].Manager = ent.Particle;
									ent.Particle.ParticleEmitters[iii]._entityName = "";
									ent.Particle.ParticleEmitters[iii]._entity = ent;
								}
							}
							//ShipSystems
							if (ent.Communication != null)
							{
								ent.Communication._entity = ent;
								ent.Communication._entityName = "";
							}

						}


                        //Other Settings
						//Galaxy.CurrentSolarSystem = Galaxy.SolarSystemLookup(SaveFileReturn.CurrentSolarSystem);
						Galaxy.SetSolarSystem(Galaxy.SolarSystemLookup(SaveFileReturn.CurrentSolarSystem));
						//Galaxy.SolarSystemLookup(SaveFileReturn.CurrentSolarSystem).SetSystem(true);

						//System.Windows.Forms.MessageBox.Show("CSS:" + Galaxy.CurrentSolarSystem.Name);

						Engine.camera.TargetObject = Galaxy.EntityLookup(SaveFileReturn.CameraTargetObject);
						//Engine.camera.SetZoom(SaveFileReturn.CameraZoomLevel);

                    }
                    return true;
				}
				else
				{
					
					return false;
				}
			}
			catch (Exception ex)
			{
				return false;
				throw new Exception("Error loading save file:" + SavePath + " with error: " + ex.ToString(), ex);
			}
		}

		public void DeleteSave(string name)
		{
			if (File.Exists(Path.Combine(SaveGamesDirectory, name + ".xml")))
			{
				System.IO.File.Delete(Path.Combine(SaveGamesDirectory, name + ".xml"));
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("Save File '" + name + "' in '" + SaveGamesDirectory + "' could not be found");
			}
		}
	}
}
