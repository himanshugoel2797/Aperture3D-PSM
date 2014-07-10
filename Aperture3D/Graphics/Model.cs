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
		public static Model Load(string filename)
		{
			return LoadProtoBUF(filename);	
		}
		
		public static Model Load(Stream fileStream)
		{
			return LoadProtoBUF(fileStream);	
		}
		
		
        /// <summary>
        /// Load an Aperture 3D Model File
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>The filename</returns>
        public static Model LoadXML(string filename)
        {
            Stream fileStream = File.OpenRead(filename);
            var toReturn = Load(fileStream);
            fileStream.Close();
            fileStream.Dispose();

            return toReturn;
        }

        /// <summary>
        /// Load an Aperture 3D Model File
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns>Stream to the file</returns>
        public static Model LoadXML(Stream fileStream)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Model));   //Binary Deserializer
			
            var modelList = (Model)deserializer.Deserialize(fileStream); //Deserialize file

            return modelList;   //return data
        }
		
		/// <summary>
        /// Load an Aperture 3D Model File
        /// </summary>
        /// <param name="filename"></param>
        /// <returns>The filename</returns>
        public static Model LoadBIN(string filename)
        {
            Stream fileStream = File.OpenRead(filename);
            var toReturn = LoadBIN(fileStream);
            fileStream.Close();
            fileStream.Dispose();

            return toReturn;
        }

        /// <summary>
        /// Load an Aperture 3D Model File
        /// </summary>
        /// <param name="fileStream"></param>
        /// <returns>Stream to the file</returns>
        public static Model LoadBIN(Stream fileStream)
        {
			fileStream.ReadByte();
			fileStream.ReadByte();
            BinaryFormatter deserializer = new BinaryFormatter();   //Binary Deserializer
			
            var modelList = (Model)deserializer.Deserialize(fileStream); //Deserialize file

            return modelList;   //return data
        }
		
		public static Model LoadProtoBUF(string filename)
		{
			Model toreturn;
			using (var stream = File.Open(filename, FileMode.Open, FileAccess.Read))
			{
				toreturn = LoadProtoBUF(stream);	
			}
			return toreturn;
		}
		
		public static Model LoadProtoBUF(Stream fileStream)
		{
				return Serializer.Deserialize<Model>(fileStream);
		}
    }
}
