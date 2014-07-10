using System;
using Aperture3D.Base;
using System.Collections.Generic;
using BEPUphysics.Entities;
using Sce.PlayStation.Core;
using BEPUphysics.MathExtensions;

namespace Aperture3D.Nodes
{
	public class EntityNode:INode
	{
		internal RenderNode Body;
		internal Entity PhysicsBody;
		
		internal Vector3 Position;
		
		public EntityNode (Vector3 Position, RenderNode body, Entity physicsBody)
		{
			Body = body;
			
			PhysicsBody = physicsBody;
			this.Position = Position;
			
			Initialized = false;
		}
		
		#region implemented abstract members of Aperture3D.Base.INode
		public override void Initialize ()
		{
			//PhysicsBody.CollisionInformation.LocalPosition = Body.WorldMatrix.TransformPoint(Position);
			RootNode.GetCurrentScene().physicsSpace.Add(PhysicsBody);
			PhysicsBody.LocalInertiaTensorInverse = new Matrix3X3 ();
			if(!Body.Initialized)Body.Initialize();
			
			Initialized = true;
		}

		public override void Activate ()
		{
			if(!Initialized)Initialize();
			
			Matrix4 worldTransform = PhysicsBody.WorldTransform;
			Body.SetWorldMatrix(ref worldTransform);
			Body.Activate();
		}

		public override void Dispose ()
		{
			Body.Dispose();
			RootNode.GetCurrentScene().physicsSpace.Remove(PhysicsBody);
		}
		#endregion
	}
}

