using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO.Compression;
using Ionic.Zip;

namespace Compile_OTV
{
	public partial class Form1 : Form
	{
		saveFile SaveFile = new saveFile();
		string SaveFilePath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\OTVCompiler";
		string DestFolderName;
		string SourceDirectory;
		string DestDirectory;

		public Form1()
		{
			InitializeComponent();

			//Check for save directory
			if (!System.IO.Directory.Exists(SaveFilePath))
			{
				System.IO.Directory.CreateDirectory(SaveFilePath);
			}

			LoadPath();
		}


		//When we hit Compile
		private void button1_Click(object sender, EventArgs e)
		{
			DestFolderName = textBox5.Text + textBox2.Text + "." + textBox3.Text + "." + textBox4.Text;
			SourceDirectory = textBox1.Text;
			DestDirectory = textBox6.Text;

			SaveFile.source = SourceDirectory;
			SaveFile.dest = DestDirectory;
			SavePath();

			Compile();
		}


		//SaveLoad
		public void SavePath()
		{
			//Save
			try
			{
				XmlSerializer serializer = new XmlSerializer(SaveFile.GetType()); //create a new XmlSerializer for use in serializing

				//create the file (overwrite if it exists) and serialize the object.
				using (FileStream stream = new FileStream(Path.Combine(SaveFilePath, "options.xml"), FileMode.Create))
				{
					serializer.Serialize(stream, SaveFile);
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.ToString());
			}
		}
		public void LoadPath()
		{
			//Load file
			try
			{
				//if the file exists
				if (File.Exists(SaveFilePath + "\\options.xml"))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(saveFile)); //create a new XmlSerializer for use in deserializing

					//open the file and deserialize. The 'using' statement makes sure the file is closed again, even if an error occurs.
					//using (FileStream stream = File.Open(SavePath, FileMode.Open, FileAccess.Read))
					using (FileStream stream = new FileStream(Path.Combine(SaveFilePath, "options.xml"), FileMode.Open))
					{

						SaveFile = (saveFile)serializer.Deserialize(stream);
						textBox1.Text = SaveFile.source;
						textBox6.Text = SaveFile.dest;
					}
				}
			}
			catch
			{
			}
		}


		public void Compile()
		{
			//Create directory
			System.IO.Directory.CreateDirectory(Path.Combine(DestDirectory,DestFolderName));
			
			//Contents folder paths
			string source_contents = Path.Combine(SourceDirectory, "Content");
			string destination_contents = Path.Combine(SourceDirectory, DestFolderName, "Content");

			//Find files
			List<string> files = new List<string>();
			files.AddRange(System.IO.Directory.GetFiles(SourceDirectory, "*.dll")); //Add .dlls
			files.Add(System.IO.Directory.GetFiles(textBox1.Text, "Off To Vega.exe")[0]); //Add .exe
			files.AddRange(Directory.GetFiles(source_contents, "*.*", SearchOption.AllDirectories)); //Add Contents Folder


			//Create all of the directories
			foreach (string dirPath in Directory.GetDirectories(source_contents, "*", SearchOption.AllDirectories))
			{ Directory.CreateDirectory(dirPath.Replace(source_contents, destination_contents)); }
			
			//Copy all the files
			foreach (string file in files)
			{ File.Copy(file, file.Replace(SourceDirectory, Path.Combine(SourceDirectory, DestFolderName)), false); }




			//Zip files
			using (ZipFile zip = new ZipFile())
			{
				zip.AddDirectory(Path.Combine(SourceDirectory, DestFolderName));
				zip.Comment = "This zip was created at " + System.DateTime.Now.ToString("G");
				zip.Save(Path.Combine(DestDirectory,DestFolderName) + ".zip");
			}

			//Delete made directory
			Directory.Delete(Path.Combine(SourceDirectory, DestFolderName), true);
			Directory.Delete(Path.Combine(DestDirectory, DestFolderName), true);

			//Open directory
			System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
			{
				FileName = DestDirectory,
				UseShellExecute = true,
				Verb = "open"
			});

			//Exit
			this.Close();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		//Form elements

	}

	public class saveFile
	{
		public string source;
		public string dest;

		public saveFile() { }
	}
}
