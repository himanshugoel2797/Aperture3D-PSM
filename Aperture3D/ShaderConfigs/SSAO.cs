using System;
using Aperture3D.Nodes;
using Aperture3D.Graphics;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Graphics;

namespace Aperture3D.ShaderConfigs
{
	public class SSAO : IShaderNode
	{
		private static Depth depthShader;
		private static ShaderProgram ssaoShader;
		private static RenderNode fsq;
		
		public SSAO ()
		{
			if(depthShader == null)
			{
				depthShader = new Depth();
			}
			
			if(ssaoShader == null)
			{
				ssaoShader = new ShaderProgram(VFS.GetFileBytes("vfs1:/shaders/SSAO.cgx"));
				ssaoShader.SetAttributeBinding(0, "a_Position");
				ssaoShader.SetAttributeBinding(1, "a_TexCoord");
			}
			
			if(fsq == null)
			{
				fsq = new RenderNode(RenderableFactory.CreatePlane(1, 1), this);	
			}
		}

		#region implemented abstract members of Aperture3D.Nodes.IShaderNode
		public override ShaderProgram GetShaderProgram ()
		{
			return ssaoShader;
		}

		public override void SetShaderProgramOptions (RenderNode renderer)
		{
			RootNode.graphicsContext.SetShaderProgram(depthShader.GetShaderProgram());
		}

		public override void UnSetShaderProgramOptions ()
		{
			
		}

		public override void Dispose ()
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}

