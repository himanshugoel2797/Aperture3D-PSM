using System;
using Aperture3D.Nodes;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core;

namespace Tests.ShaderConfigs
{
	public class Starfield : IShaderNode
	{
		private ShaderProgram starfield;
		private float time = 0;
		
		public Starfield ()
		{
			if(starfield == null)
			{
				starfield = new ShaderProgram("Application/shaders/Starfield.cgx");
				starfield.SetAttributeBinding(0, "a_Position");
				starfield.SetAttributeBinding(1, "a_TexCoord");
				
				starfield.SetUniformBinding(0, "WorldViewProj");
				starfield.SetUniformBinding(1, "iGlobalTime");
				starfield.SetUniformBinding(2, "iMouse");
			}
		}

		#region implemented abstract members of Aperture3D.Nodes.IShaderNode
		public override Sce.PlayStation.Core.Graphics.ShaderProgram GetShaderProgram ()
		{
			return starfield;
		}

		public override void SetShaderProgramOptions (RenderNode renderer)
		{
			time+=0.002f;
			Matrix4 wp = RootNode.GetCurrentScene().ProjectionMatrix * RootNode.GetCurrentScene().Camera.ViewMatrix * renderer.WorldMatrix;
			starfield.SetUniformValue(0, ref wp);
			starfield.SetUniformValue(1, time);
			starfield.SetUniformValue(2, new float[]{0,0});
		}

		public override void UnSetShaderProgramOptions ()
		{
			
		}

		public override void Dispose ()
		{
			starfield.Dispose();
		}
		#endregion
	}
}

