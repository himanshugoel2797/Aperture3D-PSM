using System;

using Aperture3D.Base;
using Aperture3D.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Sce.PlayStation.Core;

namespace Aperture3D.Nodes
{
	public static class RootNode
	{
		public static Context graphicsContext;
		public static Dictionary<string, SceneNode> Children;
		public static int TargetFPS = 30;
		public static double FramesPerSecond = 0;
		public static SceneNode _currentScene;
		public static long Interval;
		
		static RootNode ()
		{
			graphicsContext = new Context ();
			Children = new Dictionary<string, SceneNode> ();
		}
		
		public static void AddSceneNode (string name, SceneNode scene)
		{
			Children.Add (name, scene);	
		}
		public static void RemoveSceneNode (string key)
		{
			Children.Remove (key);
		}
		public static SceneNode GetCurrentScene()
		{
			return _currentScene;
		}
		
		public static void SetScene (string key)
		{
			_currentScene = Children [key];
		}
		
		public static void RunGame (string key)
		{
			SetScene (key);
			RunGame ();
		}
		public static void RunGame ()
		{
			float frameCounterAvgs = 0;
			float frameCounter = 0;
			long timeCounter = 0, avgCount = 0;
			float dt = 1000f / TargetFPS;
			
			Timer physicsUpdater = new Timer((state)=>{
			
				_currentScene.physicsSpace.Update(dt);
				
			}, null, 0,(int)(dt));
			
			//General purpose interval provider
			Stopwatch timer = new Stopwatch ();
			timer.Start ();
			
			while (true) {
				//If current scene isn't initialized, initialize it
				if (!_currentScene.Initialized)
					_currentScene.Initialize ();
				
//				if(timeCounter >= TimeSpan.TicksPerSecond){
//					frameCounterAvgs = (frameCounterAvgs * avgCount) + frameCounter;
//					avgCount++;
//					frameCounterAvgs /= avgCount;
//					FramesPerSecond = (int)frameCounterAvgs;
//					frameCounter = 0;
//					timeCounter = 0;
//				}
				
				if(timeCounter >= TimeSpan.TicksPerSecond)
				{
					FramesPerSecond = (frameCounter/TimeSpan.FromTicks(timeCounter).TotalSeconds);
					frameCounter = 0;
					timeCounter = 0;
				}
				
				//_currentScene.physicsSpace.Update(dt);
				
				timeCounter += Interval;
				frameCounter++;
				Sce.PlayStation.Core.Environment.SystemEvents.CheckEvents ();
				
				
				
				Input.UpdateGamepad();
				Input.UpdateTouchData();
				Input.UpdateMotionData();
				
				
				//Activate the scene node
				_currentScene.Activate ();
				RootNode.graphicsContext.SwapBuffers();
				
				Interval = timer.ElapsedTicks;
				timer.Reset ();
				timer.Start ();
			}
		}
	}
}

