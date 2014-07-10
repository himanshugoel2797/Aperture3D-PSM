using System;
using Aperture3D.Base;

namespace Aperture3D.Nodes
{
	public class MethodInvokerNode:INode
	{
		Action _action;
		
		public MethodInvokerNode (Action action)
		{
			_action = action;
		}

		#region implemented abstract members of Aperture3D.Base.INode
		public override void Initialize ()
		{
			Initialized = true;
		}

		public override void Activate ()
		{
			if(!Initialized)Initialize();
			_action();
		}

		public override void Dispose ()
		{
		}
		#endregion
	}
}

