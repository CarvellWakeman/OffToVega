using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
//using System.Runtime.InteropServices;
using System.Text;
namespace ExplorationEngine
{
    static class Program
    {
		

		//[DllImport("msi.dll")]
		//public static extern Int32 MsiQueryProductState(string szProduct);

        static void Main(string[] args)
        {
			//System.Windows.Forms.MessageBox.Show(MsiQueryProductState("{2BFC7AA0-544C-4E3A-8796-67F3BE655BE9}").ToString());

			//Check if XNA 4.0 is installed.
			//if (MsiQueryProductState("{2BFC7AA0-544C-4E3A-8796-67F3BE655BE9}") == 5)
			//{
				using (Engine game = new Engine())
				{
					game.Run();
				}
			//}
			//else
			//{
				//System.Windows.Forms.MessageBox.Show("XNA redistributable 4.0 is not installed, setup will launch.");
				//Process.Start(System.Windows.Forms.Application.StartupPath + "xnafx40_redist.msi");
			//}

			//try
			//{
				//Check for XNA 4.0 redistributable.
				//using (RegistryKey Key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\.NETFramework\policy\v4.0\"))
					//if (Key != null)
					//{
						//System.Windows.Forms.MessageBox.Show("XNA redistributable 4.0 IS installed.");
						//using (Engine game = new Engine())
						//{
						//	game.Run();
						//}
					//}
					//else
					//{
					//	System.Windows.Forms.MessageBox.Show("XNA redistributable 4.0 is not installed, setup will launch.");
					//	Process.Start(System.Windows.Forms.Application.StartupPath + "xnafx40_redist.msi");
					//}

			//}
			//catch (Exception ex)
			//{
			//	throw new Exception(ex.ToString());
			//}
        }
    }
}



