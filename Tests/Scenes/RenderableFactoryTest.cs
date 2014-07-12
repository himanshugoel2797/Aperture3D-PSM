using System;
using Aperture3D.Nodes;
using Aperture3D.Nodes.Cameras;
using Sce.PlayStation.Core;
using Aperture3D;
using Aperture3D.Graphics;
using Aperture3D.ShaderConfigs;
using System.Collections.Generic;

namespace Tests.Scenes
{
	public class RenderableFactoryTest : SceneNode
	{		
		Camera3D camera3d;
		RenderNode obj;
		List<RenderNode> objs;
		
		public override void Initialize ()
		{
			base.Initialize();
			
			//Setup the camera
			ProjectionMatrix = Matrix4.Perspective(FMath.Radians(45.0f), Context.AspectRatio, 0.1f,100f);
			Camera = new CameraNode(Matrix4.LookAt(new Vector3(10,10,0), Vector3.Zero, Vector3.UnitY));
			camera3d = new Camera3D(new Vector3(10,10,0), 1.5f,5f,200);
			
			objs = new List<RenderNode>();
			
			//obj = new RenderNode(RenderableFactory.CreatePlane(10,10), new Ubershader());
			
			for(int x = 0; x < 17; x++){
			objs.Add(new RenderNode(RenderableFactory.LoadModel("vfs0:/Application/Resources/" + x.ToString() + "miku.a3d"), new Simple()));
			objs[x][1] = new Sce.PlayStation.Core.Graphics.Texture2D("Application/Resources/miku.png", false);	
			}
			//obj[1] = new Sce.PlayStation.Core.Graphics.Texture2D(VFS.GenerateRealPath("vf" +
			//	"s0:/Application/Resources/kirito.png"), false);
			
			//RootNode.graphicsContext.SetDrawMode(Sce.PlayStation.Core.Graphics.DrawMode.TriangleStrip);
			
			AddNode(new MethodInvokerNode(Render));
		}
		
		public void Render()
		{
			RootNode.graphicsContext.ClearAll(0f,0.5f,1.0f,1.0f);
			RootNode.graphicsContext.AllFunctions(true);
			
			
			camera3d.Update();
		
			foreach(RenderNode o in objs)
			{
				o.Activate();	
			}
			
			RootNode.graphicsContext.AllFunctions(false);
		}
		
		
	}
}

