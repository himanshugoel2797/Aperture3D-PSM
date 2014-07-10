using System;
using Sce.PlayStation.Core;

namespace Aperture3D.Nodes.Cameras
{
	public class CameraNode
	{
		public Matrix4 ViewMatrix{get;set;}
		
		public CameraNode ()
		{
			
		}
		public CameraNode(Matrix4 ViewMatrix){this.ViewMatrix = ViewMatrix;}
	}
}

