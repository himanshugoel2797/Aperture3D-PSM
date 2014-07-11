using System;
using Sce.PlayStation.Core;

namespace Aperture3D.Nodes.Cameras
{
	public class CameraNode
	{
		public Matrix4 ViewMatrix{get;set;}
		public Vector3 Forward;
		public Vector3 Up;
		public Vector3 Position;
		
		public CameraNode ()
		{
			
		}
		public CameraNode(Matrix4 ViewMatrix){this.ViewMatrix = ViewMatrix;}
	}
}

