//#define TESTS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using Assimp;
using Assimp.Configs;
using System.Diagnostics;
using System.Reflection;
using ProtoBuf;

namespace ModelConverter
{
	[ProtoContract]
    public class Model
	{
		[ProtoMember(1)]
		public string Name;
		[ProtoMember(2)]
		public List<float> Vertices;
		[ProtoMember(3)]
		public List<ushort> Indices;
		[ProtoMember(4)]
		public List<float> TexCoords;
		[ProtoMember(5)]
		public List<float> Normals;
	}
	
	class Program
	{
		static void Main (string[] args)
		{
#if TESTS
            File.Delete("test.xml");
            File.Delete("test.bin");

            Console.WriteLine("Converting");

            var modelsOrig1 = ConvertXML("Tech Demo Tex.dae", "test.xml");
            var modelsOrig2 = ConvertBIN("Tech Demo Tex.dae", "test.bin");

            Console.WriteLine("Converted");


            var modelsLoad1 = LoadXML("test.xml");
            var modelsLoad2 = LoadBIN("test.bin");

            Console.WriteLine("XML-XML Test : " + Compare(modelsOrig1, modelsLoad1).ToString());
            Console.WriteLine("BIN-BIN Test : " + Compare(modelsOrig2, modelsLoad2).ToString());
            Console.WriteLine("XML-BIN Test : " + Compare(modelsOrig1, modelsLoad2).ToString());
            Console.WriteLine("BIN-XML Test : " + Compare(modelsOrig2, modelsLoad1).ToString());

            Console.Read();
#else

			if (args.Length >= 2) {
				ConvertProtoBUF (args [0], args [1]);
			} else if (args.Length == 1) {
				ConvertProtoBUF (args [0], Path.Combine (Path.GetDirectoryName (args [0]), Path.GetFileNameWithoutExtension (args [0]) + ".a3d"));
			} else {
				Console.WriteLine ("\n Aperture 3D Model Converter  v" + Assembly.GetExecutingAssembly ().GetName ().Version.ToString ());
				Console.WriteLine ("Usage: \n \t ModelConverter [Input File] [Output File]");
			}
#endif
		}
		
		static List<Model> ConvertProtoBUF (string filenameIN, string filenameOUT)
		{
			AssimpImporter importer = new AssimpImporter (); //Initialize Assimp.NET

			List<Model> models = SceneToCustom (importer.ImportFile (filenameIN)); //Import file and set it up for a serializeable struct

			int index = 0;
			
			foreach (Model model in models) {
				using (Stream outwrite = File.OpenWrite (Path.Combine (Path.GetDirectoryName (filenameOUT), index.ToString () + Path.GetFileName (filenameOUT)))) {     //Create output file

					Serializer.Serialize (outwrite, model);     //Do the magic, data serialized to xml file
					
				}
				index++;
			}
            

			return models;
		}
		
		static Model LoadXML (string filename)
		{
			XmlSerializer deserializer = new XmlSerializer (typeof(Model));    //Initialize XML Deserializer

			Stream model = File.OpenRead (filename); //Read the model data

			var modelList = (Model)deserializer.Deserialize (model); //Deserialize the correct file

			model.Close ();  //Dispose stream
			model.Dispose ();


			return modelList;   //Return the data
		}

		static Model LoadBIN (string filename)
		{
			BinaryFormatter deserializer = new BinaryFormatter ();   //Binary Deserializer

			Stream model = File.OpenRead (filename);     //Open model file to read

			var modelList = (Model)deserializer.Deserialize (model); //Deserialize file

			model.Close ();  //Dispose Stream
			model.Dispose ();


			return modelList;   //return data
		}

		static List<Model> ConvertXML (string filenameIN, string filenameOUT)
		{
			AssimpImporter importer = new AssimpImporter (); //Initialize Assimp.NET

			List<Model> models = SceneToCustom (importer.ImportFile (filenameIN)); //Import file and set it up for a serializeable struct

			XmlSerializer serializer = new XmlSerializer (typeof(Model)); //Initialize the serializer
			
			int index = 0;
			
			foreach (Model model in models) {
				Stream outwrite = File.OpenWrite (Path.Combine (Path.GetDirectoryName (filenameOUT), index.ToString () + Path.GetFileName (filenameOUT)));     //Create output file

				serializer.Serialize (outwrite, model);      //Do the magic, data serialized to xml file

				outwrite.Close ();     //Close and dispose stream
				outwrite.Dispose ();
				index++;
			}
            

			return models;
		}

		static List<Model> ConvertBIN (string filenameIN, string filenameOUT)
		{
			AssimpImporter importer = new AssimpImporter (); //Initialize Assimp.NET

			List<Model> models = SceneToCustom (importer.ImportFile (filenameIN)); //Import file and set it up for a serializeable struct

			BinaryFormatter serializer = new BinaryFormatter (); //Initialize the serializer
			
			int index = 0;
			
			foreach (Model model in models) {
				Stream outwrite = File.OpenWrite (Path.Combine (Path.GetDirectoryName (filenameOUT), index.ToString () + Path.GetFileName (filenameOUT)));     //Create output file

				serializer.Serialize (outwrite, model);      //Do the magic, data serialized to xml file

				outwrite.Close ();     //Close and dispose stream
				outwrite.Dispose ();
				index++;
			}
			return models;
		}

		//Core of the converter - Assimp to Aperture Conversion
		static List<Model> SceneToCustom (Scene model)
		{
			List<Model> models = new List<Model> ();

			foreach (Mesh mesh in model.Meshes) {
				Model modTemp = new Model (); //Initialize Instance

				if (mesh.Name.Replace (" ", "") != string.Empty)
					modTemp.Name = mesh.Name;   //Set Name
				else
					modTemp.Name = "whatever";
                #region Set Indices
				modTemp.Indices = new List<ushort> ();

				foreach (uint index in mesh.GetIndices()) {
					modTemp.Indices.Add ((ushort)index);
				}

                #endregion

                #region Set Vertices
				modTemp.Vertices = new List<float> ();

				foreach (Vector3D vertex in mesh.Vertices) {
					modTemp.Vertices.Add (vertex.X);
					modTemp.Vertices.Add (vertex.Y);
					modTemp.Vertices.Add (vertex.Z);
				}
                #endregion

                #region Set TexCoords
				for (int counter = 0; counter < mesh.TextureCoordsChannelCount; counter++) {
					if (mesh.HasTextureCoords (counter)) {
						modTemp.TexCoords = new List<float> ();

						foreach (Vector3D vertex in mesh.GetTextureCoords(counter)) {
							modTemp.TexCoords.Add (vertex.X);
							modTemp.TexCoords.Add (vertex.Y);
						}
					} else {
						modTemp.TexCoords = new List<float> ();

						foreach (Vector3D vertex in mesh.Vertices) {
							modTemp.TexCoords.Add (vertex.X);
							modTemp.TexCoords.Add (vertex.Y);
						}
					}
				}
                #endregion
				
				#region Set Normals
				if (mesh.HasNormals) {
					modTemp.Normals = new List<float> ();
					foreach (Vector3D normal in mesh.Normals) {
						modTemp.Normals.AddRange (new float[]{normal.X, normal.Y, normal.Z});
					}
				}
				#endregion
				models.Add (modTemp);    //Finally add the model information into the list
			}

			return models;
		}

		static bool Compare (List<Model> models1, List<Model> models2)
		{
			bool toReturn = true;

			foreach (Model model1 in models1) {
				Model model2 = models2 [models1.IndexOf (model1)];

				for (int counter = 0; counter < model1.Vertices.Count; counter++) {
					toReturn = (model1.Vertices [counter] == model2.Vertices [counter]);
                    
				}

				for (int counter = 0; counter < model1.Indices.Count; counter++) {
					toReturn = (model1.Indices [counter] == model2.Indices [counter]);
				}
			}
			return toReturn;
		}
	}
}
