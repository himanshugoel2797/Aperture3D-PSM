using System;
using Sce.PlayStation.Core;
using Aperture3D.Nodes;
using Sce.PlayStation.Core.Graphics;

namespace Aperture3D.ShaderConfigs
{
	public class Ubershader : IShaderNode
	{
		private static ShaderProgram ubershader;
		
		public Ubershader ()
		{
			if(ubershader == null)
			{
				ubershader = new ShaderProgram(VFS.GetFileBytes("vfs1:/shaders/Ubershader.cgx"));
				ubershader.SetAttributeBinding(0, "a_Position");
				ubershader.SetAttributeBinding(1, "a_TexCoord");
				ubershader.SetAttributeBinding(2, "a_Normal");
				ubershader.SetAttributeBinding(3, "a_Tangent");
				
				ubershader.SetUniformBinding(0, "WorldViewProj");
				ubershader.SetUniformBinding(1, "World");
				ubershader.SetUniformBinding(2, "LightDir");
				ubershader.SetUniformBinding(3, "ViewDir");
				ubershader.SetUniformBinding(4, "heightScale");
			}
		}

		#region implemented abstract members of Aperture3D.Nodes.IShaderNode
		public override ShaderProgram GetShaderProgram ()
		{
			return ubershader;
		}

		public override void SetShaderProgramOptions (RenderNode renderer)
		{
			Matrix4 WVP = RootNode.GetCurrentScene().ProjectionMatrix * RootNode.GetCurrentScene().Camera.ViewMatrix * renderer.WorldMatrix;
			ubershader.SetUniformValue(0, ref WVP);
			
			Matrix4 world = renderer.WorldMatrix;
			ubershader.SetUniformValue(1, ref world);
			
			Vector3 lightDir = Vector3.UnitX;
			ubershader.SetUniformValue(2, ref lightDir);
			
			ubershader.SetUniformValue(3, ref RootNode.GetCurrentScene().Camera.Forward);
			ubershader.SetUniformValue(4, 0.5f);
		}

		public override void UnSetShaderProgramOptions ()
		{
			
		}

		public override void Dispose ()
		{
			ubershader.Dispose();
		}
		#endregion
	}
}

