using System;
using Aperture3D.Nodes;
using Shmup.Environment;
using Aperture3D.Nodes.Cameras;
using Sce.PlayStation.Core;

namespace Shmup.Scenes
{
	public class Game : SceneNode
	{
		public override void Initialize ()
		{
			base.Initialize();
			
			this.AddNode(new FirstPersonCameraNode(new Vector3(10,10,10), 1.5f,5f,2));
			this.AddNode(new SpaceSphere());
		}
		
		public override void Activate ()
		{
			Console.WriteLine(RootNode.FramesPerSecond);
			
			RootNode.graphicsContext.ClearAll(0,0f,0f,1);
			base.Activate();
		}
		
	}
}

