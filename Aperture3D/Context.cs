using System;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core;

namespace Aperture3D
{
	public class Context
    {
        public static int Width {get;private set;}
        public static int Height { get; private set; }
		public static float AspectRatio {get;private set;}
		
		private static DrawMode drawMode;
		
		public GraphicsContext _Context;
		
        public Context():this(0, 0) { }
        public Context(float AspectRatio, int Width):this(Width, (int)((1f/AspectRatio)*Width)) { }
        public Context(int width, int height)
        {
            Width = width;
            Height = height;
			
            _Context = new GraphicsContext(width,height, PixelFormat.Rgba, PixelFormat.Depth24,MultiSampleMode.Msaa4x);
        	AspectRatio = _Context.Screen.AspectRatio;
			
			
			drawMode = DrawMode.Triangles;
		}
		
		public void SetDisplay(float AspectRatio, int width)
		{
			SetDisplay(width, (int)((1f/AspectRatio)*width));
		}
		public void SetDisplay(int width, int height)
		{
			int screenWidth = _Context.Screen.Width;
			int screenHeight = _Context.Screen.Height;
			
			int x = (screenWidth - width)/2;
			int y = (screenHeight - height)/2;
				
			_Context.Dispose();
			_Context = new GraphicsContext(width,height, PixelFormat.Rgba, PixelFormat.Depth24, MultiSampleMode.None);
			AspectRatio = _Context.Screen.AspectRatio;
		}
		
		public Rectangle GetDisplay()
		{
			return new Rectangle(0,0,_Context.Screen.Rectangle.Width, _Context.Screen.Rectangle.Height);
		}
		
		public void SetShaderProgram(ShaderProgram program)
		{
			_Context.SetShaderProgram(program);	
		}
		public void SetVertexBuffer(int channel, VertexBuffer vbuf)
		{
			_Context.SetVertexBuffer(channel, vbuf);	
		}
		
		public void SetDrawMode(DrawMode d){drawMode = d;}
		public void DrawArrays(int first, int count)
		{
			_Context.DrawArrays(drawMode, first,count); 	
		}
		public void DrawArrays(int first, int count, int instanceFirst, int instanceCount)
		{
			DrawInstanceArrays(first, count, instanceFirst, instanceCount);	
		}
		public void DrawInstanceArrays(int first, int count, int instanceFirst, int instanceCount)
		{
			_Context.DrawArraysInstanced(drawMode, first, count, instanceFirst, instanceCount);	
		}
		
		public void ClearAll(float r, float g, float b, float a)
		{
			_Context.SetClearColor(r,g,b,a);
			_Context.Clear(ClearMask.All);
		}
		
		public void SetTexture(int index, Texture tex)
		{
			_Context.SetTexture(index, tex);	
		}
		
		public void DepthTest(bool enable)
		{
			_Context.Enable(EnableMode.DepthTest, enable);	
		}
		
		public void Transparency(bool enable)
		{
			_Context.Enable(EnableMode.Blend, enable);
			_Context.SetBlendFunc(BlendFuncMode.Add,BlendFuncFactor.SrcAlpha, BlendFuncFactor.OneMinusSrcAlpha);
		}
		
		public void BackFaceCulling(bool enable)
		{
			_Context.SetCullFace(CullFaceMode.Back, CullFaceDirection.Ccw);
			_Context.Enable(EnableMode.CullFace, enable);
		}
		
		public void Dithering(bool enable)
		{
			_Context.Enable(EnableMode.Dither, enable);
		}
		
		public void AllFunctions(bool enable)
		{
			Transparency(enable);
			DepthTest(enable);
			BackFaceCulling(enable);
			Dithering(enable);
		}
		
		public bool DitheringStatus(){return _Context.IsEnabled(EnableMode.Dither);}
		public bool BackFaceCullingStatus(){return _Context.IsEnabled(EnableMode.CullFace);}
		public bool TransparencyStatus(){return _Context.IsEnabled(EnableMode.Blend);}
		public bool DepthTestStatus(){return _Context.IsEnabled(EnableMode.DepthTest);}
		public bool AllFunctionsStatus()
		{
			return 	(DitheringStatus() && BackFaceCullingStatus() && TransparencyStatus() && DepthTestStatus());
		}
		
		public void SetFrameBuffer(FrameBuffer buf){ _Context.SetFrameBuffer(buf);}
		
		public void SwapBuffers(){_Context.SwapBuffers();}
    }
}

