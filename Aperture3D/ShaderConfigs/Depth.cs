using System;
using Aperture3D.Nodes;
using Aperture3D.Graphics;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core;

namespace Aperture3D.ShaderConfigs
{
	public class Depth : IShaderNode
	{
		private static ShaderProgram depth;
		
		public Texture2D RenderPassTarget;
		public FrameBuffer RenderPassBuf;
		
		public Depth ()
		{
			if(depth == null)
			{
				depth = new ShaderProgram(VFS.GetFileBytes("vfs1:/shaders/Depth.cgx"));
				depth.SetAttributeBinding(0, "a_Position");	
				depth.SetUniformBinding(0, "WorldViewProj");
			}
			
			RenderPassTarget = new Texture2D((int)RootNode.graphicsContext.GetDisplay().Width, 
			                                 (int)RootNode.graphicsContext.GetDisplay().Height
			                                 , false, PixelFormat.Rgba, PixelBufferOption.Renderable);
			RenderPassBuf = new FrameBuffer();
			RenderPassBuf.SetColorTarget(RenderPassTarget,0);
			
			DepthBuffer temp = new DepthBuffer(RenderPassTarget.Width, RenderPassTarget.Height, PixelFormat.Depth24Stencil8);
			RenderPassBuf.SetDepthTarget(temp);
			
		}

		#region implemented abstract members of Aperture3D.Nodes.IShaderNode
		public override ShaderProgram GetShaderProgram ()
		{
			return depth;
		}

		public override void SetShaderProgramOptions (RenderNode renderer)
		{
			Matrix4 WVP = RootNode.GetCurrentScene().ProjectionMatrix * RootNode.GetCurrentScene().Camera.ViewMatrix * renderer.WorldMatrix;
			depth.SetUniformValue(0, ref WVP);
			
			//Clear and setup the framebuffer
			RootNode.graphicsContext.ClearAll(1,1,1,1);
			RootNode.graphicsContext.SetFrameBuffer(RenderPassBuf);
		}

		public override void UnSetShaderProgramOptions ()
		{
			RootNode.graphicsContext.SetFrameBuffer(null);
		}

		public override void Dispose ()
		{
			RenderPassBuf.Dispose();
			RenderPassTarget.Dispose();
		}
		#endregion
	}
}

