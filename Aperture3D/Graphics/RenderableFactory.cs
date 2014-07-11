using System;
using Sce.PlayStation.Core;
using System.Collections.Generic;

namespace Aperture3D.Graphics
{
	public static class RenderableFactory
	{
		public static Renderable LoadModel(string filename)
		{
			Renderable r = new Renderable();
			Model m = ModelLoader.Load(VFS.GenerateRealPath(filename));
			
			r.Vertices = m.Vertices.ToArray();
			r.Indices = m.Indices.ToArray();
			r.Normals = m.Normals.ToArray();
			r.TexCoords = m.TexCoords.ToArray();
			
			return CalculateTangents(r);
		}
		
		public static Renderable CreatePlane(int width, int height)
		{
			Renderable r = new Renderable();
			
			List<float> vertices = new List<float>();
			List<float> texcoords = new List<float>();
			List<ushort> indices = new List<ushort>();
			List<float> normals = new List<float>();
			
			height++;
			width++;
			
			float unitWidth = 1f/(float)width;
			float unitHeight = 1f/(float)height;
			
			for(int y = 0; y < height; y++)
			{
				for(int x = 0; x < width; x++)
				{
					vertices.Add(x);
					vertices.Add(0);	//Height of 0
					vertices.Add(y);
					
					if(y < height - 1 && x < width - 1){
						
					indices.Add((ushort) ( (width * y)        + x) );
					indices.Add((ushort) ( (width * (y + 1))  + x) );
					indices.Add((ushort) ( (width * y)        + (x + 1)) );
						
					indices.Add((ushort) ( (width * (y + 1))        + x) );
					indices.Add((ushort) ( (width * (y + 1))  + x + 1) );
					indices.Add((ushort) ( (width * y)        + (x + 1)) );
						
					}
					
					texcoords.Add((float)x * unitWidth);
					texcoords.Add((float)y * unitHeight);
					
					normals.Add(0);
					normals.Add(1);
					normals.Add(0);
					
				}
			}
			
			r.Vertices = vertices.ToArray();
			r.Indices = indices.ToArray();
			r.Normals = normals.ToArray();
			r.TexCoords = texcoords.ToArray();
			
			return CalculateTangents(r);
		}
		
		private static Renderable CalculateTangents(Renderable r)
		{
			List<float> tangents = new List<float>();
			
			for(ushort i = 0; i < r.Indices.Length; i+=3)
			{
				Vector3 v1 = new Vector3(r.Vertices[r.Indices[i] * 3], r.Vertices[r.Indices[i] * 3 + 1], r.Vertices[r.Indices[i] * 3 + 2]);
				Vector3 v2 = new Vector3(r.Vertices[r.Indices[i + 1] * 3], r.Vertices[r.Indices[i+1] * 3 + 1], r.Vertices[r.Indices[i+1] * 3 + 2]);
				Vector3 v3 = new Vector3(r.Vertices[r.Indices[i + 2] * 3], r.Vertices[r.Indices[i+2] * 3 + 1], r.Vertices[r.Indices[i+2] * 3 + 2]);
				
				Vector2 tex1 = new Vector2(r.TexCoords[r.Indices[i] * 2], r.TexCoords[r.Indices[i] * 2 + 1]);
				Vector2 tex2 = new Vector2(r.TexCoords[r.Indices[i+1] * 2], r.TexCoords[r.Indices[i+1] * 2 + 1]);
				Vector2 tex3 = new Vector2(r.TexCoords[r.Indices[i+2] * 2], r.TexCoords[r.Indices[i+2] * 2 + 1]);
				
				Vector3 e1 = v2 - v1;
				Vector3 e2 = v3 - v1;
				
				float du1 = tex2.X - tex1.X;
				float dv1 = tex2.Y - tex1.Y;
				float du2 = tex3.X - tex1.X;
				float dv2 = tex3.Y - tex1.Y;
				
				float f = 1.0f/(du1 * dv2 - du2 * dv1);
				
				Vector3 Tangent;
				
				Tangent = new Vector3(f * (dv2 * e1.X - dv1 * e2.X), 
				                      f * (dv2 * e1.Y - dv1 * e2.Y),
				                      f * (dv2 * e1.Z - dv1 * e2.Z));
				
			}
			
			r.tangents = tangents.ToArray();
			return r;
		}
	}
}

