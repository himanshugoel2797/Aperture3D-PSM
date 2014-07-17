using System;

using Aperture3D.Base;
using Aperture3D.Graphics;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core;
//using Aperture3D.Math;

namespace Aperture3D.Nodes
{
	public class RenderNode:INode
	{
		public IRenderable renderableObject{ get; set; }

		private VertexBuffer vbuffer;

		public IShaderNode shader { get; set; }

		public Texture2D[] Textures;
		private Vector3 scale = new Vector3 (1, 1, 1), translation, rotation, gRotation;
		
		public Matrix4 WorldMatrix{ get; set; }
		
		public RenderNode (IRenderable renderableObject, IShaderNode Shader):this(renderableObject)
		{
			SetShaderProgram (Shader);
		}

		public RenderNode (IRenderable renderableObject)
		{
			Textures = new Texture2D[7];
			
			this.renderableObject = renderableObject;
			//Vertex			TexCoord			//Normals			 //Tangents
			vbuffer = new VertexBuffer (renderableObject.GetVertexCount () / 3, renderableObject.GetIndexCount (), VertexFormat.Float3, VertexFormat.Float2, VertexFormat.Float3, VertexFormat.Float3);
			vbuffer.SetVertices (0, renderableObject.GetVertices ());
			
			if (renderableObject.GetTexCoords () != null)
				vbuffer.SetVertices (1, renderableObject.GetTexCoords ());
			if (renderableObject.HasNormals ())
				vbuffer.SetVertices (2, renderableObject.GetNormals ());
			if (renderableObject.GetTangents () != null)
				vbuffer.SetVertices (3, renderableObject.GetTangents ());
			
			vbuffer.SetIndices (renderableObject.GetIndices ());
		}

		public void SetShaderProgram (IShaderNode Shader)
		{
			shader = Shader;
		}
		
		public void SetWorldMatrix (ref Matrix4 worldMatrix)
		{
			WorldMatrix = Matrix4.RotationXyz(gRotation) * Matrix4.Translation (translation) * Matrix4.RotationXyz (rotation) * Matrix4.Scale (scale) * worldMatrix;
			//WorldMatrix = worldMatrix;
		}
		
		public void Scale (float x, float y, float z)
		{
			scale = new Vector3 (x, y, z);
		}
		
		public void Translate (float x, float y, float z)
		{
			translation = new Vector3 (x, y, z);
		}
		
		public void Rotate (float x, float y, float z)
		{
			rotation = new Vector3 (x, y, z);
		}
		
		public void GlobalRotation(float x, float y, float z)
		{
				gRotation = new Vector3(x ,y ,z);
		}
		
		public Texture2D this [int index] {
			get {
				return Textures [index];	
			}
			set {
				Textures [index] = value;	
			}
		}
		
		#region INode implementation
		public override void Initialize ()
		{
			WorldMatrix = Matrix4.Scale (1, 1, 1);
			
			Initialized = true;
		}
		#endregion

		#region INode implementation
		public override void Activate ()
		{
			if (!Initialized)
				Initialize ();
			
			if (shader != null)
				RootNode.graphicsContext.SetShaderProgram (shader.GetShaderProgram ());
			
			RootNode.graphicsContext.SetVertexBuffer (0, vbuffer);
			
			
				if (shader != null)
					shader.SetShaderProgramOptions (this);
			
				//Set all available textures
				for (int i = 0; i < Textures.Length; i++) {
					RootNode.graphicsContext.SetTexture (i, Textures [i]);	
				}
			
				RootNode.graphicsContext.DrawArrays (0, vbuffer.IndexCount);
			
				//Unset all set textures
				for (int i = 0; i < Textures.Length; i++) {
					RootNode.graphicsContext.SetTexture (i, null);	
				}
			
				if (shader != null)
					shader.UnSetShaderProgramOptions ();
			
				//RootNode.graphicsContext.SetShaderProgram (RootNode.depthShader.GetShaderProgram ());
				//RootNode.depthShader.SetShaderProgramOptions(this);
				//RootNode.graphicsContext.DrawArrays (0, vbuffer.IndexCount);
				//RootNode.depthShader.UnSetShaderProgramOptions ();
				RootNode.graphicsContext.SetShaderProgram (null);
			
			
			RootNode.graphicsContext.SetVertexBuffer (0, null);
		}
		#endregion

		#region IDisposable implementation
		public override void Dispose ()
		{
			vbuffer.Dispose ();
			if (shader != null)
				shader.Dispose ();
		}
		#endregion
	}
}

