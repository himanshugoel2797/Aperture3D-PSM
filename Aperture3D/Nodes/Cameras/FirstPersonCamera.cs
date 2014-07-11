using System;
using Sce.PlayStation.Core;
using Sce.PlayStation.Core.Input;
using BEPUphysics.Entities.Prefabs;
using BEPUphysics.MathExtensions;

using Aperture3D.Graphics;

namespace Aperture3D.Nodes.Cameras
{
	public class Camera3D : Capsule
	{
		public Vector3 Target { get; set; }

		public Vector3 Up { get; set; }

		public Vector3 Forward { get; set; }
		
		public Matrix4 ViewMatrix { get; set; }

		public Matrix4 ProjectionMatrix { get; set; }
		
		public Quaternion Rotation;
		
		public float MoveSpeed = 1f;
		public float Sensitivity = 16f;

		public float Yaw { get; set; }

		public float Pitch { get; set; }

		public float YLimit = 50;
		
		public bool IsActive { get; set; }

		public bool LookXOnly = false;
		public bool LookYOnly = false;
		public bool InvertX = false;
		public bool InvertY = false;
		public bool TouchEnabled = true;
		public bool GamePadEnabled = true;
		public Matrix4 RotationMatrix;
		private float sensibility = 0.5f;
		
		public Camera3D (Vector3 start, float height, float radius) : base(start, height, radius)
		{
			Initialize();
		}
		
		public Camera3D (Vector3 start, float height, float radius, float mass) : base(start, height, radius, mass)
		{
			Initialize();
		}
		
		private void Initialize()
		{
			ProjectionMatrix = Matrix4.Perspective (FMath.Radians (45.0f), Context.AspectRatio, 0.001f, 1000);
			this.LocalInertiaTensorInverse = new Matrix3X3 ();
			Rotation = Quaternion.Identity;
			this.Material = new BEPUphysics.Materials.Material(0.3f,0.7f,0);
			this.IsActive = true;
			this.IsAffectedByGravity = true;
			RootNode.GetCurrentScene().physicsSpace.Add(this);
		}
		
		public void Update ()
		{
			
			if (this.IsActive)
				UpdateCamera ();
			this.ModifyLinearDamping(0.6f);
		}
		
		private void UpdateCamera ()
		{
			Pitch = FMath.Clamp (Pitch, -YLimit, YLimit + 15);
			
			UpdateTouchInput ();
			UpdatePadInput ();
			
			UpdateViewMatrix ();
			RootNode.GetCurrentScene().Camera.ViewMatrix = ViewMatrix;
			RootNode.GetCurrentScene().Camera.Forward = this.Forward;
			RootNode.GetCurrentScene().Camera.Position = this.Position;
			RootNode.GetCurrentScene().Camera.Up = this.Up;
		}
		
		private void UpdateViewMatrix ()
		{
			Quaternion m_rot = Quaternion.RotationAxis (Vector3.UnitX, FMath.Radians (Pitch)) * Quaternion.RotationAxis (Vector3.UnitY, FMath.Radians (Yaw));
			m_rot.Conjugate (out Rotation);
			this.RotationMatrix = Rotation.ToMatrix4 ();
			Vector3 v = new Vector3 (0f, 0f, -1f);
			Vector3 v2 = new Vector3 (0f, 1f, 0f);
			Vector3 v3 = Matrix4.TransformVector (this.RotationMatrix, v);
			this.Forward = v3;
			this.Target = this.Position + v3;
			this.Up = Matrix4.TransformVector (this.RotationMatrix, v2);
			ViewMatrix = Matrix4.LookAt (Position, Target, Up);
		}
		
		private void AddToCameraPosition (Vector3 vectorToAdd)
		{
			Vector3 v = Matrix4.TransformVector (this.RotationMatrix, vectorToAdd);
			if (!this.IsDynamic)
				this.LinearVelocity += this.MoveSpeed * v;
			else
				this.LinearVelocity += this.MoveSpeed * new Vector3D (v.X, 0, v.Z);
			this.UpdateViewMatrix ();
		}
		
		private void UpdateTouchInput ()
		{
			if (this.TouchEnabled) {
				if (Forward.Z > 0)
					InvertY = true;
				else
					InvertY = false;
				
#if FRONT_TOUCH
				foreach (TouchData current in Input.TouchData) {
					if (!current.Skip) {
						TouchStatus status = current.Status;
						if (status == TouchStatus.Move) {
							if (!LookXOnly && !LookYOnly) {
								if (this.InvertY)
									this.Pitch += current.Y * this.Sensitivity;
								else
									this.Pitch -= current.Y * this.Sensitivity;
								
								if (this.InvertX)
									this.Yaw += current.X * this.Sensitivity;
								else
									this.Yaw -= current.X * this.Sensitivity;
							} else {
								if (LookXOnly && !LookYOnly) {
									if (this.InvertX)
										this.Yaw += current.X * this.Sensitivity;
									else
										this.Yaw -= current.X * this.Sensitivity;
								}
									
								if (LookYOnly && !LookXOnly) {
									if (this.InvertY)
										this.Pitch += current.Y * this.Sensitivity;
									else
										this.Pitch -= current.Y * this.Sensitivity;
								}
							}
						}
					}
				}
#endif
			}
		}
		
		private void UpdatePadInput ()
		{
			if (this.GamePadEnabled) {
				Yaw += Input.AnalogRightX * FMath.Radians (Sensitivity);
				Pitch += Input.AnalogRightY * FMath.Radians (Sensitivity);
				
				AddToCameraPosition(new Vector3(Input.AnalogLeftX * sensibility,0, Input.AnalogLeftY * sensibility));
				
				if (Input.ButtonsAreDown (GamePadButtons.Up)) {
					AddToCameraPosition (new Vector3 (0, 0, -sensibility));	
				}
				
				if (Input.ButtonsAreDown (GamePadButtons.Down)) {
					AddToCameraPosition (new Vector3 (0, 0, sensibility));	
				}
				
				if (Input.ButtonsAreDown (GamePadButtons.Left)) {
					AddToCameraPosition (new Vector3 (-sensibility, 0, 0));	
				}
				
				if (Input.ButtonsAreDown (GamePadButtons.Right)) {
					AddToCameraPosition (new Vector3 (sensibility, 0, 0));	
				}
			}
		}
	}
}


