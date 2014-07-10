using System;
using Aperture3D.Base;
using BEPUphysics;
using Sce.PlayStation.Core;
using Aperture3D.Nodes.Cameras;

namespace Aperture3D.Nodes
{
	public class SceneNode:INode
	{
		public Space physicsSpace;
		public Matrix4 ProjectionMatrix;
		public CameraNode Camera;
		
		public SceneNode ()
		{
			Initialized = false;
		}
		
		public override void Initialize()
		{
			Children = new System.Collections.Generic.List<Aperture3D.Base.INode>();
			physicsSpace = new Space();
			
			Initialized = true;
		}
		
		public void AddNode(INode node)
		{
			Children.Add(node);
			node.Parent = this;
		}
		
		public void UpdatePhysics(float timestep)
		{
			physicsSpace.Update(timestep/TimeSpan.TicksPerMillisecond);
		}
		
		public override void Dispose()
		{
			
		}

		#region INode implementation
		public override void Activate ()
		{
			foreach(INode child in Children)
			{
				if(!child.Initialized)child.Initialize();
				child.Activate();	
			}
		}
		#endregion


	}
}

