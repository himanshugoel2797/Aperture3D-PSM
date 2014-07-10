using System;

using Aperture3D.Base;
using Aperture3D.Graphics;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core;

namespace Aperture3D.Nodes
{
	public class RenderNode:INode
	{
		public IRenderable renderableObject{get;set;}
		private VertexBuffer vbuffer;
		public IShaderNode shader {get;set;}
		
		private Vector3 scale = new Vector3(1,1,1), translation, rotation;
		
		public Matrix4 WorldMatrix{get;private set;}
		
		public RenderNode (IRenderable renderableObject, IShaderNode Shader):this(renderableObject){ SetShaderProgram(Shader); }
		public RenderNode (IRenderable renderableObject)
		{
			this.renderableObject = renderableObject;
																												//Vertex			TexCoord			//Normals
			vbuffer = new VertexBuffer(renderableObject.GetVertexCount()/3, renderableObject.GetIndexCount(), VertexFormat.Float3, VertexFormat.Float2, VertexFormat.Float3);
			vbuffer.SetVertices(0, renderableObject.GetVertices());
			if(renderableObject.GetTexCoords() != null)vbuffer.SetVertices(1, renderableObject.GetTexCoords());
			if(renderableObject.HasNormals())vbuffer.SetVertices(2, renderableObject.GetNormals());
			vbuffer.SetIndices(renderableObject.GetIndices());
		}
		public void SetShaderProgram(IShaderNode Shader)
		{
			shader = Shader;
		}
		
		public void SetWorldMatrix(ref Matrix4 worldMatrix){
			WorldMatrix = Matrix4.RotationXyz(rotation) * Matrix4.Translation(translation) * Matrix4.Scale(scale) * worldMatrix;
			//WorldMatrix = worldMatrix;
		}
		
		public void Scale(float x, float y, float z)
		{
			scale = new Vector3(x,y,z);
		}
		
		public void Translate(float x, float y, float z)
		{
			translation = new Vector3(x,y,z);
		}
		
		public void Rotate(float x, float y, float z)
		{
			rotation = new Vector3(x,y,z);
		}
		
		#region INode implementation
		public override void Initialize ()
		{
			WorldMatrix = Matrix4.Scale(1,1,1);
			
			Initialized = true;
		}
		#endregion

		#region INode implementation
		public override void Activate ()
		{
			if(!Initialized)Initialize();
			
			if(shader!=null)RootNode.graphicsContext.SetShaderProgram(shader.GetShaderProgram());
			
			RootNode.graphicsContext.SetVertexBuffer(0, vbuffer);
			
			if(shader!= null)shader.SetShaderProgramOptions(this);
			
			RootNode.graphicsContext.DrawArrays(0, vbuffer.IndexCount);
			
			if(shader!= null)shader.UnSetShaderProgramOptions();
			if(shader!= null)RootNode.graphicsContext.SetShaderProgram(null);
			
			RootNode.graphicsContext.SetVertexBuffer(0, null);
		}
		#endregion

		#region IDisposable implementation
		public override void Dispose ()
		{
			vbuffer.Dispose();
			if(shader!=null)shader.Dispose();
		}
		#endregion
	}
}

