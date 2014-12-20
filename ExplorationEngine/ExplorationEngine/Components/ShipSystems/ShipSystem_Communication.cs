using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExplorationEngine.Components
{
	public class ShipSystem_Communication : Component
	{
		public override int ID { get; set; }

		public BaseEntity _entity;
		public string _entityName;

		[System.Xml.Serialization.XmlIgnore]
		public ExplorationEngine.GUI.Communications CommunicationsMenu = new GUI.Communications();

		public ShipSystem_Communication() { }
		public ShipSystem_Communication(BaseEntity entity)
		{
			ID = 1;
			_entity = entity;

		}


		public void SendMessage(string str)
		{
			System.Windows.Forms.MessageBox.Show(str);
		}

		public override void SelfDestruct()
		{

		}

		public override void Update()
		{
			if (Input.KeyReleased(Input.Action1) && _entity.Parent != null && _entity.Parent.ShipLogic != null && _entity.Parent.ShipLogic.IsControlled)
			{
				if (CommunicationsMenu.Form_Main.Active)
					CommunicationsMenu.Hide(false);
				else
					CommunicationsMenu.Show(null, false);
			}
		}
	}
}
