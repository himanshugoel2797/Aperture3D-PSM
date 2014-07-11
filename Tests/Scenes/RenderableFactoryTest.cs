using System;
using Aperture3D.Nodes;
using Aperture3D.Nodes.Cameras;
using Sce.PlayStation.Core;
using Aperture3D;
using Aperture3D.Graphics;
using Aperture3D.ShaderConfigs;

namespace Tests.Scenes
{
	public class RenderableFactoryTest : SceneNode
	{		
		Camera3D camera3d;
		RenderNode obj;
		
		public override void Initialize ()
		{
			base.Initialize();
			
			//Setup the camera
			ProjectionMatrix = Matrix4.Perspective(FMath.Radians(45.0f), Context.AspectRatio, 0.1f,1000f);
			Camera = new CameraNode(Matrix4.LookAt(new Vector3(10,10,0), Vector3.Zero, Vector3.UnitY));
			camera3d = new Camera3D(new Vector3(10,10,0), 1.5f,5f,200);
			
			obj = new RenderNode(RenderableFactory.CreatePlane(10,10), new Ubershader());
			
			obj[0] = obj[1] = obj[2] = new Sce.PlayStation.Core.Graphics.Texture2D(VFS.GenerateRealPath("vfs0:/Application/Resources/uvTest.jpg"), false);
			
			RootNode.graphicsContext.SetDrawMode(Sce.PlayStation.Core.Graphics.DrawMode.TriangleStrip);
			
			AddNode(new MethodInvokerNode(Render));
		}
		
		public void Render()
		{
			RootNode.graphicsContext.ClearAll(0f,0.5f,1.0f,1.0f);
			RootNode.graphicsContext.AllFunctions(true);
			
			
			camera3d.Update();
		
			obj.Activate();
			
			RootNode.graphicsContext.AllFunctions(false);
		}
		
		
	}
}

