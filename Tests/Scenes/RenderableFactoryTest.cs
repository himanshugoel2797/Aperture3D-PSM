using System;
using Aperture3D.Nodes;
using Aperture3D.Nodes.Cameras;
using Sce.PlayStation.Core;
using Aperture3D;
using Aperture3D.Graphics;
using Aperture3D.ShaderConfigs;
using System.Collections.Generic;
using BEPUphysics.Entities.Prefabs;
//using Aperture3D.Math;

namespace Tests.Scenes
{
	public class RenderableFactoryTest : SceneNode
	{		
		Camera3D camera3d;
		EntityNode entity, e2, e3;
		RenderNode obj, obj2, obj3;
		float rot2 = 0, rot3 = 0, gRot2X = 0, gRot3X = 0, gRot2Y = 0, gRot3Y = 0;
		List<RenderNode> objs;
		
		public override void Initialize ()
		{
			base.Initialize();
			
			physicsSpace.ForceUpdater.AllowMultithreading = true;
			physicsSpace.ForceUpdater.Gravity = new BEPUphysics.MathExtensions.Vector3D(0, -0.5f, 0);
			
			//Setup the camera
			ProjectionMatrix = Matrix4.Perspective(FMath.Radians(45.0f), Context.AspectRatio, 0.1f,500f);
			Camera = new CameraNode(Matrix4.LookAt(new Vector3(50,50,0), Vector3.Zero, Vector3.UnitY));
			camera3d = new Camera3D(new Vector3(10,10,10), 1.5f,5f,2);
			camera3d.Sensitivity = 32;
			
			objs = new List<RenderNode>();
			
			obj2 = new RenderNode(RenderableFactory.LoadModel("vfs0:/Application/Resources/Shmup/testWeapon.a3d"), new Simple());
			obj2[1] = new Sce.PlayStation.Core.Graphics.Texture2D("Application/Resources/Shmup/uv.png", false);
			obj2.Scale(5,5,5);
			e2 = new EntityNode(Vector3.Zero, obj2, new Box(new Vector3(0,0,-1), 2000, 1, 2000));
			
			obj3 = new RenderNode(RenderableFactory.LoadModel("vfs0:/Application/Resources/Shmup/testWeapon.a3d"), new Simple());
			obj3[1] = new Sce.PlayStation.Core.Graphics.Texture2D("Application/Resources/Shmup/uv.png", false);
			obj3.Scale(5,5,5);
			e3 = new EntityNode(Vector3.Zero, obj3, new Box(new Vector3(0,0,-1), 200, 1, 200));
			
			obj = new RenderNode(RenderableFactory.LoadModel("vfs0:/Application/Resources/0kirito.a3d"), new Simple());
			obj[1] = new Sce.PlayStation.Core.Graphics.Texture2D("Application/Resources/kirito.png", false);
			entity = new EntityNode(Vector3.One, obj, new Capsule(Vector3.Zero, 0.25f, 0.05f, 1));
			
			AddNode(new MethodInvokerNode(Render));
		}
		
		public void Render()
		{
			Console.WriteLine(RootNode.FramesPerSecond);
			Console.WriteLine(camera3d.Position);
			
			RootNode.graphicsContext.ClearAll(0f,0.5f,1.0f,1.0f);
			RootNode.graphicsContext.AllFunctions(true);
			
			
			camera3d.Update();
			
			obj2.Translate(camera3d.Position.X - 7.5f, camera3d.Position.Y - 5, camera3d.Position.Z - 11);
			obj3.Translate(camera3d.Position.X + 7.5f, camera3d.Position.Y - 5, camera3d.Position.Z - 11);
			
			
			if(Input.ButtonsAreDown(Sce.PlayStation.Core.Input.GamePadButtons.L))rot2-=0.2f;
			if(Input.ButtonsAreDown(Sce.PlayStation.Core.Input.GamePadButtons.R))rot3+=0.2f;
			
			gRot2X -= Input.AnalogRightX * 0.02f;
			gRot3X -= Input.AnalogRightX * 0.02f;//FMath.Radians (camera3d.Sensitivity);
			
			gRot2Y -= Input.AnalogRightY * 0.02f;//FMath.Radians (camera3d.Sensitivity);
			gRot3Y -= Input.AnalogRightY * 0.02f;//FMath.Radians (camera3d.Sensitivity);
				
				obj2.Rotate(gRot2Y, gRot2X, rot2);
				obj3.Rotate(gRot3Y, gRot3X, rot3);
			
			e2.Activate();
			e3.Activate();
			entity.Activate();
			
			
			RootNode.graphicsContext.AllFunctions(false);
		}
		
		
	}
}

