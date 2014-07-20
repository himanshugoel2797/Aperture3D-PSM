using System;
using Aperture3D.Base;
using Aperture3D.Nodes;
using Aperture3D.Graphics;
using Aperture3D.ShaderConfigs;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core;

namespace Shmup.Environment
{
	public class SpaceSphere : INode
	{
		private RenderNode sphere;
		private Texture2D spaceBg, fgStars;
		
		public SpaceSphere ()
		{
		}

		#region implemented abstract members of Aperture3D.Base.INode
		public override void Initialize ()
		{
			sphere = 
				new RenderNode(RenderableFactory.LoadModel("vfs0:/Application/Environment/Resources/sphere.a3d")
			                        , new Simple());
			
			spaceBg = new Texture2D("Application/Environment/Resources/starMap.png", false);
			fgStars = new Texture2D("Application/Environment/Resources/Hyades.png", false);
			
			
			Initialized = true;
		}

		public override void Activate ()
		{
			RootNode.graphicsContext.AllFunctions(false);
			RootNode.graphicsContext.Transparency(true);
			
			Matrix4 tmp = Matrix4.Identity;
			sphere.Scale(500,500,500);
			
			//Always around the center
			sphere.Translate(RootNode.GetCurrentScene().Camera.Position.X,
			                 RootNode.GetCurrentScene().Camera.Position.Y,
			                 RootNode.GetCurrentScene().Camera.Position.Z);
			
			//The background moves slow
			sphere.Rotate(FMath.Radians(RootNode.GetCurrentScene().Camera.Position.Z) * 0.5f
			              , FMath.Radians(RootNode.GetCurrentScene().Camera.Position.X)-0.5f * 0.5f
			              , 0);
			sphere.SetWorldMatrix(ref tmp);
			
			sphere[1] = spaceBg;
			sphere.Activate();
			
			//The foreground is faster
			sphere.Rotate(FMath.Radians(RootNode.GetCurrentScene().Camera.Position.Z)
			              , FMath.Radians(RootNode.GetCurrentScene().Camera.Position.X)-0.5f
			              , 0);
			sphere.SetWorldMatrix(ref tmp);
			
			sphere[1] = fgStars;
			sphere.Activate();
			
			RootNode.graphicsContext.AllFunctions(true);
		}

		public override void Dispose ()
		{
			spaceBg.Dispose();
			fgStars.Dispose();
			sphere.Dispose();
		}
		#endregion
	}
}

