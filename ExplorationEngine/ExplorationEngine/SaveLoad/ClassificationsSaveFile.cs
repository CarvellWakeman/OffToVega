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
	public class ClassificationsSaveFile
	{
		//Save file name
		public string SaveFileName;

		//Classification data
		public List<Classification> Classifications = new List<Classification>();

		public ClassificationsSaveFile() {}
		public ClassificationsSaveFile(string name) 
		{
			SaveFileName = name;
			Classifications = Engine.stellarClassifications.Classifications;

			//Convert the textures from the classifications into texture indexes
			if (Classifications.Count > 0)
			{
				for (int ii = 0; ii < Classifications.Count; ii++)
				{
					if (Classifications[ii].Textures.Count > 0)
					{
						Classifications[ii].TextureIndexes.Clear();

						for (int iii = 0; iii < Classifications[ii].Textures.Count; iii++)
						{
							Classifications[ii].TextureIndexes.Add(Engine.GetTextureIndex(Classifications[ii].Textures[iii]));
						}
					}
				}
			}
		}
	}
}
