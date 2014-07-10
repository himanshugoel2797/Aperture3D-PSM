using System;
using Sce.PlayStation.Core.Graphics;
using Aperture3D.Base;

namespace Aperture3D.Nodes
{
	public abstract class IShaderNode : IDisposable
	{
		public abstract ShaderProgram GetShaderProgram();
		public abstract void SetShaderProgramOptions(RenderNode renderer);
		public abstract void UnSetShaderProgramOptions();
		public abstract void Dispose();
	}
}

