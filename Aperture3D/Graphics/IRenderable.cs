using System;
using Sce.PlayStation.Core;
using BEPUphysics.MathExtensions;

namespace Aperture3D.Graphics
{
	/// <summary>
	/// Interface which must be implemented by all objects that can be rendered to the screen.
	/// </summary>
	public interface IRenderable
	{
		float[] GetVertices();
		Vector3D[] GetVertices3D();
		float[] GetTexCoords();
		bool HasNormals();
		float[] GetNormals();
		ushort[] GetIndices();
		int GetIndexCount();
		int GetVertexCount();
	}
}

