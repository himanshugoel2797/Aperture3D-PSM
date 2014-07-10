using System;
using BEPUphysics.MathExtensions;

namespace Aperture3D.Graphics
{
	public class Renderable : IRenderable
	{
		internal float[] Vertices;
		internal Vector3D[] Vertices3D;
		internal float[] TexCoords;
		internal float[] Normals;
		internal ushort[] Indices;
		
		public Renderable ()
		{
		}

		#region IRenderable implementation
		public float[] GetVertices ()
		{
			return Vertices;
		}

		public Vector3D[] GetVertices3D ()
		{
			return Vertices3D;
		}

		public float[] GetTexCoords ()
		{
			return TexCoords;
		}

		public bool HasNormals ()
		{
			return !(Normals == null);
		}

		public float[] GetNormals ()
		{
			return Normals;
		}

		public ushort[] GetIndices ()
		{
			return Indices;
		}

		public int GetIndexCount ()
		{
			if(Indices != null)return Indices.Length;
			return 0;
		}

		public int GetVertexCount ()
		{
			if(Vertices != null)return Vertices.Length;
			return 0;
		}
		#endregion
	}
}

