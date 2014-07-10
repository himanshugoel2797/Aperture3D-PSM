using System;
using System.Collections.Generic;

namespace Aperture3D.Base
{
	public abstract class INode:IDisposable
	{
		public INode Parent {get;set;}
		public List<INode> Children {get;set;}
		
		public string Name {get;set;}
		public bool Initialized {get;set;}
		public abstract void Initialize();
		public abstract void Activate();
		public virtual void Update(long delta){}
		
		#region IDisposable implementation
		public abstract void Dispose ();
		#endregion
	}
}

