using System;
using Sce.PlayStation.Core;
using Aperture3D.Nodes;
using Sce.PlayStation.Core.Graphics;

namespace Aperture3D.ShaderConfigs
{
		public class Simple:IShaderNode
	{
		private static ShaderProgram simpleShader;
		private Vector4 Color; 
		
		public Simple () : this(Vector4.UnitW)
		{
			
		}
		
		public Simple(Vector4 col)
		{
			if(simpleShader == null)
			{
				simpleShader = new ShaderProgram(VFS.GetFileBytes("vfs1:/shaders/Simple.cgx"));
				simpleShader.SetAttributeBinding(0, "a_Position");
				simpleShader.SetAttributeBinding(1, "a_TexCoord");
				simpleShader.SetUniformBinding(0, "WorldViewProj");
				simpleShader.SetUniformBinding(1, "MaterialColor");
			}
			
			Color = col;
		}
		
		public void SetColor(Vector4 Color)
		{
			this.Color = Color;
		}

		#region IShaderNode implementation
		public override ShaderProgram GetShaderProgram ()
		{
			return simpleShader;
		}

		public override void SetShaderProgramOptions (RenderNode renderer)
		{
			Matrix4 WVP = RootNode.GetCurrentScene().ProjectionMatrix * RootNode.GetCurrentScene().Camera.ViewMatrix * renderer.WorldMatrix;
			simpleShader.SetUniformValue(0, ref WVP);
			simpleShader.SetUniformValue(1, ref Color);
			RootNode.graphicsContext.SetTexture(1, renderer[1]);
		}

		public override void UnSetShaderProgramOptions ()
		{
			
		}
		#endregion

		#region IDisposable implementation
		public override void Dispose ()
		{
			simpleShader.Dispose();
			simpleShader = null;
		}
		#endregion
	}
}

