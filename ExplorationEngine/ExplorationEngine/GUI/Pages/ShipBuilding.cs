#region "Using Statements"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace ExplorationEngine.GUI
{
	public class ShipBuilding : GUIPage
	{
		//GUI controls
		public dDropdown PartCategories;
		public dGridbox Parts;


		public ShipBuilding() : base()
		{
			Engine.Pages.Add(this);

			//Main Form
			Form_Main = new dForm("ShipBuilding", new Rectangle(0, 0, (int)Engine.CurrentGameResolution.X, (int)Engine.CurrentGameResolution.Y), Engine.MapBackground, null, false, false);
			Form_Main.OriginalColor = Color.Black;
			//Form_Main.IsDragable = true;

			//Parts in current category
			//Parts = new dGridbox("Parts",  new Vector2(0, 36), new Vector2(100, 100), 3, 5, 5, Engine.Font_MediumSmall, "", Form_Main, false, false);
			//Form_Main.AddControl(Parts);

			//Part categories
			PartCategories = new dDropdown("PartCategories", new Vector2(0, 0), new Vector2(320, 35), 300, Engine.Font_MediumSmall, "", Form_Main, false, false);
				Form_Main.AddControl(PartCategories);
				PartCategories.PlaySound = true;
				//PartCategories.AddItems(new List<dLabel>(){
				//new dLabel("Item1",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item1",Color.White,false,false,false),
				//new dLabel("Item2",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item2",Color.White,false,false,false),
				//new dLabel("Item3",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item3",Color.White,false,false,false),
				//new dLabel("Item4",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item4",Color.White,false,false,false),
				//new dLabel("Item5",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item5",Color.White,false,false,false),
				//new dLabel("Item6",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item6",Color.White,false,false,false),
				//new dLabel("Item7",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item7",Color.White,false,false,false),
				//new dLabel("Item8",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item8",Color.White,false,false,false),
				//new dLabel("Item9",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item9",Color.White,false,false,false),
				//new dLabel("Item10",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item10",Color.White,false,false,false),
				//new dLabel("Item11",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item11",Color.White,false,false,false),
				//new dLabel("Item12",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item12",Color.White,false,false,false),
				//new dLabel("Item13",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item13",Color.White,false,false,false),
				//new dLabel("Item14",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item14",Color.White,false,false,false),
				//new dLabel("Item15",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item15",Color.White,false,false,false),
				//new dLabel("Item16",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item16",Color.White,false,false,false),
				//new dLabel("Item17",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item17",Color.White,false,false,false),
				//new dLabel("Item18",Vector2.Zero,null,PartCategories,Engine.Font_MediumSmall,"Item18",Color.White,false,false,false)});



		}


		//Buttons
		public void ButtonEnter(dControl sender)
		{
			sender.Color = sender.HoverColor;
		}
		public void ButtonLeave(dControl sender)
		{
			sender.Color = sender.OriginalColor;
		}
		public void ButtonPress(dControl sender)
		{
			sender.Color = sender.PressColor;
		}
		public void ButtonRelease(dControl sender)
		{
			sender.Color = sender.ReleaseColor;

			switch (sender.name)
			{
				case "Map_Back":
					Hide(false);
					break;
			}
		}


		public override void Show(GUIPage lastform, bool quick)
		{
			base.Show(lastform, quick);
		}

		public override void Hide(bool quick)
		{
			base.Hide(quick);
		}


		public override void Reset()
		{
			base.Reset();
		}

		public override void Update()
		{

			//Update base
			base.Update();

			if (Visible)
			{
				
			}
		}
		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);


			if (Visible)
			{

				if (Engine.DebugState)
				{

				}

			}
			
		}
	}

}
