using System;
using Sce.PlayStation.Core;

namespace Aperture3D
{
	public static class MatrixExtensions
	{
		public static Matrix4 CreateBillboard(Vector3 objPos, Vector3 camPos, Vector3 up, Vector3 forward)
		{
			Matrix4 m;
			CreateBillboard(ref objPos,ref camPos, ref up, forward, out m);
			return m;
		}
		
		public static void CreateBillboard(ref Vector3 objectPosition, ref Vector3 cameraPosition,
            ref Vector3 cameraUpVector, Vector3 cameraForwardVector, out Matrix4 result)
        {
            Vector3 look = cameraPosition - objectPosition;
			look = look.Normalize();
			
			Vector3 right = cameraUpVector.Cross(look).Normalize();
			Vector3 up = look.Cross(right).Normalize();
			//Matrix4 mat = Matrix4.LookAt(cameraPosition, cameraForwardVector, cameraUpVector);
			//Vector3 right = new Vector3(mat.M11, mat.M21, mat.M31);
			//right = right.Normalize();
			
			//Vector3 up = new Vector3(mat.M12, mat.M22, mat.M32);
			//up = up.Normalize();
			
            result.M11 = right.X;
            result.M12 = right.Y;
            result.M13 = right.Z;
            result.M14 = 0;
            result.M21 = up.X;
            result.M22 = up.Y;
            result.M23 = up.Z;
            result.M24 = 0;
            result.M31 = look.X;
            result.M32 = look.Y;
            result.M33 = look.Z;
            result.M34 = 0;
            result.M41 = objectPosition.X;
            result.M42 = objectPosition.Y;
            result.M43 = objectPosition.Z;
            result.M44 = 1;
        }
	}
}

