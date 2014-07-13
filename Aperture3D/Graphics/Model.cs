using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

using ProtoBuf;

namespace Aperture3D.Graphics
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

	public static class ModelLoader
	{
		public static Model Load (string filename)
		{
			if(Path.GetExtension(filename).Contains("obj"))return LoadOBJ(filename);
			else return LoadProtoBUF(filename);
		}
		
		public static Model Load (Stream fileStream)
		{
			return LoadProtoBUF (fileStream);	
		}
		
		public static Model LoadOBJ (string filename)
		{
			Model m = new Model ();
			
			List<float> vertices = new List<float> ();
			List<float> TexCoords = new List<float> ();
			List<float> normals = new List<float> ();
			List<ushort> vindexes = new List<ushort> ();
			List<ushort> tindexes = new List<ushort> ();
			List<ushort> nindexes = new List<ushort> ();
			
			string mtlPath = "", mtlName = "";
			Stream fileStream = VFS.OpenFile (filename);
			
			using (StreamReader r = new StreamReader(fileStream)) {
				while (!r.EndOfStream) {
				
					string line = r.ReadLine ();
					string[] parts;
					
					switch (line [0]) {
					case 'o':
						m.Name = line.Remove (0, 2).Trim ();
						break;
					case 'v':
						parts = line.Remove (0, 2).Trim ().Split (' ');
						
						//Split the vertex data
						float[] partsF;
						if (parts.Length == 3)
							partsF = new float[] {float.Parse (parts [0]), float.Parse (parts [1]), float.Parse (parts [2])};
						else
							partsF = new float[] {float.Parse (parts [0]), float.Parse (parts [1])};
						
						if (line [1] == ' ') {
							vertices.Add (partsF [0]);
							vertices.Add (partsF [1]);
							vertices.Add (partsF [2]);
						} else if (line [1] == 'n') {
							normals.Add (partsF [0]);
							normals.Add (partsF [1]);
							normals.Add (partsF [2]);
						} else if (line [1] == 't') {
							TexCoords.Add (partsF [0]);
							TexCoords.Add (partsF [1]);	
						}	
						break;
					case 'f':
						parts = line.Remove (0, 2).Trim ().Split (' ');
						ushort[,] pieces = new ushort[3, 3];
						for (int x = 0; x < parts.Length; x++) {
							string[] subparts = parts [x].Split ('/');
							vindexes.Add ((ushort)(ushort.Parse (subparts [0]) - 1));
							tindexes.Add ((ushort)(ushort.Parse (subparts [1]) - 1));
							nindexes.Add ((ushort)(ushort.Parse (subparts [2]) - 1));
						}
						break;
					default:	//Ignore if we don't know what to do
						if (line.StartsWith ("mtllib"))
							mtlPath = line.Remove (0, 7);
						else if (line.StartsWith ("usemtl"))
							mtlName = line.Remove (0, 7);
						break;
					}
				}
			}
			
			m.Indices = new List<ushort>(vindexes.ToArray());
			m.Normals = new List<float>(vindexes.Count);
			m.TexCoords = new List<float>(vindexes.Count);
			m.Vertices = new List<float>(vindexes.Count);
					
			
			for (int c = 0; c < vindexes.Count; c++) {
				m.Vertices.Add(vertices [vindexes [c] * 3]);
				m.Vertices.Add(vertices [vindexes [c] * 3 + 1]);
				m.Vertices.Add(vertices [vindexes [c] * 3 + 2]);
				
				m.Normals.Add(normals [nindexes [c] * 3]);
				m.Normals.Add(normals [nindexes [c] * 3 + 1]);
				m.Normals.Add(normals [nindexes [c] * 3 + 2]);
				
				m.TexCoords.Add(TexCoords [tindexes [c] * 2]);
				m.TexCoords.Add(TexCoords [tindexes [c] * 2 + 1]);
				
				m.Indices.Add((ushort)c);
			}
			
			return m;
		}
		
		/// <summary>
		/// Load an Aperture 3D Model File
		/// </summary>
		/// <param name="filename"></param>
		/// <returns>The filename</returns>
		public static Model LoadXML (string filename)
		{
			Stream fileStream = File.OpenRead (filename);
			var toReturn = Load (fileStream);
			fileStream.Close ();
			fileStream.Dispose ();

			return toReturn;
		}

		/// <summary>
		/// Load an Aperture 3D Model File
		/// </summary>
		/// <param name="fileStream"></param>
		/// <returns>Stream to the file</returns>
		public static Model LoadXML (Stream fileStream)
		{
			XmlSerializer deserializer = new XmlSerializer (typeof(Model));   //Binary Deserializer
			
			var modelList = (Model)deserializer.Deserialize (fileStream); //Deserialize file

			return modelList;   //return data
		}
		
		/// <summary>
		/// Load an Aperture 3D Model File
		/// </summary>
		/// <param name="filename"></param>
		/// <returns>The filename</returns>
		public static Model LoadBIN (string filename)
		{
			Stream fileStream = File.OpenRead (filename);
			var toReturn = LoadBIN (fileStream);
			fileStream.Close ();
			fileStream.Dispose ();

			return toReturn;
		}

		/// <summary>
		/// Load an Aperture 3D Model File
		/// </summary>
		/// <param name="fileStream"></param>
		/// <returns>Stream to the file</returns>
		public static Model LoadBIN (Stream fileStream)
		{
			fileStream.ReadByte ();
			fileStream.ReadByte ();
			BinaryFormatter deserializer = new BinaryFormatter ();   //Binary Deserializer
			
			var modelList = (Model)deserializer.Deserialize (fileStream); //Deserialize file

			return modelList;   //return data
		}
		
		public static Model LoadProtoBUF (string filename)
		{
			Model toreturn;
			using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read)) {
				toreturn = LoadProtoBUF (stream);	
			}
			return toreturn;
		}
		
		public static Model LoadProtoBUF (Stream fileStream)
		{
			return Serializer.Deserialize<Model> (fileStream);
		}
	}
}
