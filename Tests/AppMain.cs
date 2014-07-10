using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Aperture3D.Nodes;

using Tests.Scenes;

namespace Tests
{
	public class AppMain
	{
		public static void Main (string[] args)
		{
			RootNode.graphicsContext.SetDisplay(16f/9f, 960);
			RootNode.TargetFPS = 30;
			RootNode.AddSceneNode("RFactory", new RenderableFactoryTest());
			RootNode.RunGame("RFactory");
		}
	}
}
