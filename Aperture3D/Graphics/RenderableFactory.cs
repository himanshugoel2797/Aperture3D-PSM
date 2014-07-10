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
			
			return r;
		}
		
		public static Renderable CreatePlane(int width, int height)
		{
			Renderable r = new Renderable();
			
			List<float> vertices = new List<float>();
			List<float> texcoords = new List<float>();
			List<ushort> indices = new List<ushort>();
			List<float> normals = new List<float>();
			
			float unitWidth = 1f/(float)width;
			float unitHeight = 1f/(float)height;
			
			for(int y = 0; y < height; y++)
			{
				for(int x = 0; x < width; x++)
				{
					vertices.Add(x);
					vertices.Add(0);	//Height of 0
					vertices.Add(y);
					
					indices.Add((ushort)x);
					indices.Add((ushort) (x + 1) );
					indices.Add((ushort) (width * (y+1) + x) );
					
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
			
			return r;
		}
		
	}
}

