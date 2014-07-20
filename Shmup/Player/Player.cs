using System;
using Aperture3D.Base;

namespace Shmup.Player
{
	public class Player : INode
	{
		public Player ()
		{
		}

		#region implemented abstract members of Aperture3D.Base.INode
		public override void Initialize ()
		{
			throw new NotImplementedException ();
		}

		public override void Activate ()
		{
			throw new NotImplementedException ();
		}

		public override void Dispose ()
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}

