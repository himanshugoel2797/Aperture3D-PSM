using System;
using Aperture3D.Nodes;
using Aperture3D.Nodes.Cameras;
using Sce.PlayStation.Core;
using Aperture3D;
using Aperture3D.Graphics;
using Aperture3D.ShaderConfigs;
using System.Collections.Generic;
using BEPUphysics.Entities.Prefabs;

namespace Tests.Scenes
{
	public class RenderableFactoryTest : SceneNode
	{		
		Camera3D camera3d;
		EntityNode entity, e2;
		RenderNode obj, obj2;
		List<RenderNode> objs;
		
		public override void Initialize ()
		{
			base.Initialize();
			
			physicsSpace.ForceUpdater.AllowMultithreading = true;
			physicsSpace.ForceUpdater.Gravity = new BEPUphysics.MathExtensions.Vector3D(0, -0.5f, 0);
			
			//Setup the camera
			ProjectionMatrix = Matrix4.Perspective(FMath.Radians(45.0f), Context.AspectRatio, 1f,100f);
			Camera = new CameraNode(Matrix4.LookAt(new Vector3(50,50,0), Vector3.Zero, Vector3.UnitY));
			camera3d = new Camera3D(new Vector3(10,10,10), 1.5f,5f,2);
			
			objs = new List<RenderNode>();
			
			//obj2 = new RenderNode(RenderableFactory.LoadModel("vfs0:/Application/Resources/0ground.a3d"), new Simple(Vector4.UnitW));
			
			obj2 = new RenderNode(RenderableFactory.CreatePlane(10,10),new Simple());
			obj2[1] = new Sce.PlayStation.Core.Graphics.Texture2D("Application/Resources/uvTest.jpg", false);
			obj2.Scale(5,5,5);
			e2 = new EntityNode(Vector3.Zero, obj2, new Box(new Vector3(0,0,-1), 200, 1, 200));
			//e2 = new EntityNode(Vector3.Zero, obj2, RenderableFactory.GenerateCollisionMesh(obj2.renderableObject));
			
			obj = new RenderNode(RenderableFactory.LoadModel("vfs0:/Application/Resources/0kirito.a3d"), new Simple());
			obj[1] = new Sce.PlayStation.Core.Graphics.Texture2D("Application/Resources/kirito.png", false);
			
			entity = new EntityNode(Vector3.One, obj, new Capsule(Vector3.Zero, 0.25f, 0.05f, 1));
			
			
			//RootNode.graphicsContext.SetDrawMode(Sce.PlayStation.Core.Graphics.DrawMode.TriangleStrip);
			
			AddNode(new MethodInvokerNode(Render));
		}
		
		public void Render()
		{
			Console.WriteLine(RootNode.FramesPerSecond);
			
			RootNode.graphicsContext.ClearAll(0f,0.5f,1.0f,1.0f);
			RootNode.graphicsContext.AllFunctions(true);
			
			
			camera3d.Update();
		
			//foreach(RenderNode o in objs)
			//{
				//o.Activate();	
			//}
			
			//obj[1] = RootNode.DepthTexture;
			//obj.Activate();
			
			e2.Activate();
			entity.Activate();
			
			
			RootNode.graphicsContext.AllFunctions(false);
		}
		
		
	}
}

