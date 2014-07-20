using System;
using System.Collections.Generic;

using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Environment;
using Sce.PlayStation.Core.Graphics;
using Sce.PlayStation.Core.Input;
using Aperture3D.Nodes;
using Shmup.Scenes;

namespace Shmup
{
	public class AppMain
	{
		public static void Main (string[] args)
		{
			RootNode.graphicsContext.SetDisplay(960, 544);
			RootNode.TargetFPS = 60;
			RootNode.AddSceneNode("Game", new Game());
			RootNode.RunGame("Game");
		}
	}
}
