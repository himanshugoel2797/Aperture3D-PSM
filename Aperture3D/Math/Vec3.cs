using System;
using System.Text;
using ProtoBuf;
#if PSM
using psmVector3 = Sce.PlayStation.Core.Vector3;
#endif
#if BEPU
using BEPUphysics;
using bepuVector3 = BEPUphysics.MathExtensions.Vector3D;
#endif

namespace Aperture3D.Math
{
	[ProtoContract]
    public struct Vec3 : IEquatable<Vec3>
    {
        #region Private Fields

        private static  Vec3 zero = new Vec3(0f, 0f, 0f);
        private static  Vec3 one = new Vec3(1f, 1f, 1f);
        private static  Vec3 unitX = new Vec3(1f, 0f, 0f);
        private static  Vec3 unitY = new Vec3(0f, 1f, 0f);
        private static  Vec3 unitZ = new Vec3(0f, 0f, 1f);
        private static  Vec3 up = new Vec3(0f, 1f, 0f);
        private static  Vec3 down = new Vec3(0f, -1f, 0f);
        private static  Vec3 right = new Vec3(1f, 0f, 0f);
        private static Vec3 left = new Vec3(-1f, 0f, 0f);
        private static Vec3 forward = new Vec3(0f, 0f, -1f);
        private static Vec3 backward = new Vec3(0f, 0f, 1f);

        #endregion Private Fields


        #region Public Fields
        
        [ProtoMember(1)]
        public float X;
      
        [ProtoMember(2)]
        public float Y;
      
        [ProtoMember(3)]
        public float Z;

        #endregion Public Fields


        #region Properties

        public static Vec3 Zero
        {
            get { return zero; }
        }

        public static Vec3 One
        {
            get { return one; }
        }

        public static Vec3 UnitX
        {
            get { return unitX; }
        }

        public static Vec3 UnitY
        {
            get { return unitY; }
        }

        public static Vec3 UnitZ
        {
            get { return unitZ; }
        }

        public static Vec3 Up
        {
            get { return up; }
        }

        public static Vec3 Down
        {
            get { return down; }
        }

        public static Vec3 Right
        {
            get { return right; }
        }

        public static Vec3 Left
        {
            get { return left; }
        }

        public static Vec3 Forward
        {
            get { return forward; }
        }

        public static Vec3 Backward
        {
            get { return backward; }
        }

        #endregion Properties


        #region Constructors

        public Vec3(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }


        public Vec3(float value)
        {
            this.X = value;
            this.Y = value;
            this.Z = value;
        }


        public Vec3(Vec2 value, float z)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
        }


        #endregion Constructors


        #region Public Methods

        public static Vec3 Add(Vec3 value1, Vec3 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            value1.Z += value2.Z;
            return value1;
        }

        public static void Add(ref Vec3 value1, ref Vec3 value2, out Vec3 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
        }

        public static Vec3 Barycentric(Vec3 value1, Vec3 value2, Vec3 value3, float amount1, float amount2)
        {
            return new Vec3(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2),
                MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2));
        }

        public static void Barycentric(ref Vec3 value1, ref Vec3 value2, ref Vec3 value3, float amount1, float amount2, out Vec3 result)
        {
            result = new Vec3(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2),
                MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2));
        }

        public static Vec3 CatmullRom(Vec3 value1, Vec3 value2, Vec3 value3, Vec3 value4, float amount)
        {
            return new Vec3(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount),
                MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount));
        }

        public static void CatmullRom(ref Vec3 value1, ref Vec3 value2, ref Vec3 value3, ref Vec3 value4, float amount, out Vec3 result)
        {
            result = new Vec3(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount),
                MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount));
        }

        public static Vec3 Clamp(Vec3 value1, Vec3 min, Vec3 max)
        {
            return new Vec3(
                MathHelper.Clamp(value1.X, min.X, max.X),
                MathHelper.Clamp(value1.Y, min.Y, max.Y),
                MathHelper.Clamp(value1.Z, min.Z, max.Z));
        }

        public static void Clamp(ref Vec3 value1, ref Vec3 min, ref Vec3 max, out Vec3 result)
        {
            result = new Vec3(
                MathHelper.Clamp(value1.X, min.X, max.X),
                MathHelper.Clamp(value1.Y, min.Y, max.Y),
                MathHelper.Clamp(value1.Z, min.Z, max.Z));
        }

        public static Vec3 Cross(Vec3 vector1, Vec3 vector2)
        {
            Cross(ref vector1, ref vector2, out vector1);
            return vector1;
        }

        public static void Cross(ref Vec3 vector1, ref Vec3 vector2, out Vec3 result)
        {
            result = new Vec3(vector1.Y * vector2.Z - vector2.Y * vector1.Z,
                                 -(vector1.X * vector2.Z - vector2.X * vector1.Z),
                                 vector1.X * vector2.Y - vector2.X * vector1.Y);
        }

        public static float Distance(Vec3 vector1, Vec3 vector2)
        {
            float result;
            DistanceSquared(ref vector1, ref vector2, out result);
            return (float)System.Math.Sqrt(result);
        }

        public static void Distance(ref Vec3 value1, ref Vec3 value2, out float result)
        {
            DistanceSquared(ref value1, ref value2, out result);
            result = (float)System.Math.Sqrt(result);
        }

        public static float DistanceSquared(Vec3 value1, Vec3 value2)
        {
            float result;
            DistanceSquared(ref value1, ref value2, out result);
            return result;
        }

        public static void DistanceSquared(ref Vec3 value1, ref Vec3 value2, out float result)
        {
            result = (value1.X - value2.X) * (value1.X - value2.X) +
                     (value1.Y - value2.Y) * (value1.Y - value2.Y) +
                     (value1.Z - value2.Z) * (value1.Z - value2.Z);
        }

        public static Vec3 Divide(Vec3 value1, Vec3 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            value1.Z /= value2.Z;
            return value1;
        }

        public static Vec3 Divide(Vec3 value1, float value2)
        {
            float factor = 1 / value2;
            value1.X *= factor;
            value1.Y *= factor;
            value1.Z *= factor;
            return value1;
        }

        public static void Divide(ref Vec3 value1, float divisor, out Vec3 result)
        {
            float factor = 1 / divisor;
            result.X = value1.X * factor;
            result.Y = value1.Y * factor;
            result.Z = value1.Z * factor;
        }

        public static void Divide(ref Vec3 value1, ref Vec3 value2, out Vec3 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
            result.Z = value1.Z / value2.Z;
        }

        public static float Dot(Vec3 vector1, Vec3 vector2)
        {
            return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
        }

        public static void Dot(ref Vec3 vector1, ref Vec3 vector2, out float result)
        {
            result = vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vec3))
                return false;

            var other = (Vec3)obj;
            return  X == other.X &&
                    Y == other.Y &&
                    Z == other.Z;
        }

        public bool Equals(Vec3 other)
        {
            return  X == other.X && 
                    Y == other.Y &&
                    Z == other.Z;
        }

        public override int GetHashCode()
        {
            return (int)(this.X + this.Y + this.Z);
        }

        public static Vec3 Hermite(Vec3 value1, Vec3 tangent1, Vec3 value2, Vec3 tangent2, float amount)
        {
            Vec3 result = new Vec3();
            Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out result);
            return result;
        }

        public static void Hermite(ref Vec3 value1, ref Vec3 tangent1, ref Vec3 value2, ref Vec3 tangent2, float amount, out Vec3 result)
        {
            result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
            result.Z = MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount);
        }

        public float Length()
        {
            float result;
            DistanceSquared(ref this, ref zero, out result);
            return (float)System.Math.Sqrt(result);
        }

        public float LengthSquared()
        {
            float result;
            DistanceSquared(ref this, ref zero, out result);
            return result;
        }

        public static Vec3 Lerp(Vec3 value1, Vec3 value2, float amount)
        {
            return new Vec3(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount),
                MathHelper.Lerp(value1.Z, value2.Z, amount));
        }

        public static void Lerp(ref Vec3 value1, ref Vec3 value2, float amount, out Vec3 result)
        {
            result = new Vec3(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount),
                MathHelper.Lerp(value1.Z, value2.Z, amount));
        }
                
        public static Vec3 Max(Vec3 value1, Vec3 value2)
        {
            return new Vec3(
                MathHelper.Max(value1.X, value2.X),
                MathHelper.Max(value1.Y, value2.Y),
                MathHelper.Max(value1.Z, value2.Z));
        }

        public static void Max(ref Vec3 value1, ref Vec3 value2, out Vec3 result)
        {
            result = new Vec3(
                MathHelper.Max(value1.X, value2.X),
                MathHelper.Max(value1.Y, value2.Y),
                MathHelper.Max(value1.Z, value2.Z));
        }

        public static Vec3 Min(Vec3 value1, Vec3 value2)
        {
            return new Vec3(
                MathHelper.Min(value1.X, value2.X),
                MathHelper.Min(value1.Y, value2.Y),
                MathHelper.Min(value1.Z, value2.Z));
        }

        public static void Min(ref Vec3 value1, ref Vec3 value2, out Vec3 result)
        {
            result = new Vec3(
                MathHelper.Min(value1.X, value2.X),
                MathHelper.Min(value1.Y, value2.Y),
                MathHelper.Min(value1.Z, value2.Z));
        }

        public static Vec3 Multiply(Vec3 value1, Vec3 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            value1.Z *= value2.Z;
            return value1;
        }

        public static Vec3 Multiply(Vec3 value1, float scaleFactor)
        {
            value1.X *= scaleFactor;
            value1.Y *= scaleFactor;
            value1.Z *= scaleFactor;
            return value1;
        }

        public static void Multiply(ref Vec3 value1, float scaleFactor, out Vec3 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
        }

        public static void Multiply(ref Vec3 value1, ref Vec3 value2, out Vec3 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
        }

        public static Vec3 Negate(Vec3 value)
        {
            value = new Vec3(-value.X, -value.Y, -value.Z);
            return value;
        }

        public static void Negate(ref Vec3 value, out Vec3 result)
        {
            result = new Vec3(-value.X, -value.Y, -value.Z);
        }

        public void Normalize()
        {
            Normalize(ref this, out this);
        }

        public static Vec3 Normalize(Vec3 vector)
        {
            Normalize(ref vector, out vector);
            return vector;
        }

        public static void Normalize(ref Vec3 value, out Vec3 result)
        {
            float factor;
            Distance(ref value, ref zero, out factor);
            factor = 1f / factor;
            result.X = value.X * factor;
            result.Y = value.Y * factor;
            result.Z = value.Z * factor;
        }

	public static Vec3 Reflect(Vec3 vector, Vec3 normal)
	{
		// I is the original array
		// N is the normal of the incident plane
		// R = I - (2 * N * ( DotProduct[ I,N] ))
		Vec3 reflectedVector;
		// inline the dotProduct here instead of calling method
		float dotProduct = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);
		reflectedVector.X = vector.X - (2.0f * normal.X) * dotProduct;
		reflectedVector.Y = vector.Y - (2.0f * normal.Y) * dotProduct;
		reflectedVector.Z = vector.Z - (2.0f * normal.Z) * dotProduct;

		return reflectedVector;
	}

	public static void Reflect(ref Vec3 vector, ref Vec3 normal, out Vec3 result)
	{
		// I is the original array
		// N is the normal of the incident plane
		// R = I - (2 * N * ( DotProduct[ I,N] ))

		// inline the dotProduct here instead of calling method
		float dotProduct = ((vector.X * normal.X) + (vector.Y * normal.Y)) + (vector.Z * normal.Z);
		result.X = vector.X - (2.0f * normal.X) * dotProduct;
		result.Y = vector.Y - (2.0f * normal.Y) * dotProduct;
		result.Z = vector.Z - (2.0f * normal.Z) * dotProduct;

	}
		
        public static Vec3 SmoothStep(Vec3 value1, Vec3 value2, float amount)
        {
            return new Vec3(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount),
                MathHelper.SmoothStep(value1.Z, value2.Z, amount));
        }

        public static void SmoothStep(ref Vec3 value1, ref Vec3 value2, float amount, out Vec3 result)
        {
            result = new Vec3(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount),
                MathHelper.SmoothStep(value1.Z, value2.Z, amount));
        }

        public static Vec3 Subtract(Vec3 value1, Vec3 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            value1.Z -= value2.Z;
            return value1;
        }

        public static void Subtract(ref Vec3 value1, ref Vec3 value2, out Vec3 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(32);
            sb.Append("{X:");
            sb.Append(this.X);
            sb.Append(" Y:");
            sb.Append(this.Y);
            sb.Append(" Z:");
            sb.Append(this.Z);
            sb.Append("}");
            return sb.ToString();
        }

        public static Vec3 Transform(Vec3 position, Matrix matrix)
        {
            Transform(ref position, ref matrix, out position);
            return position;
        }

        public static void Transform(ref Vec3 position, ref Matrix matrix, out Vec3 result)
        {
            result = new Vec3((position.X * matrix.M11) + (position.Y * matrix.M21) + (position.Z * matrix.M31) + matrix.M41,
                                 (position.X * matrix.M12) + (position.Y * matrix.M22) + (position.Z * matrix.M32) + matrix.M42,
                                 (position.X * matrix.M13) + (position.Y * matrix.M23) + (position.Z * matrix.M33) + matrix.M43);
        }

        public static void Transform(Vec3[] sourceArray, ref Matrix matrix, Vec3[] destinationArray)
        {
            ////Debug.Assert(destinationArray.Length >= sourceArray.Length, "The destination array is smaller than the source array.");

            // TODO: Are there options on some platforms to implement a vectorized version of this?

            for (var i = 0; i < sourceArray.Length; i++)
            {
                var position = sourceArray[i];                
                destinationArray[i] =
                    new Vec3(
                        (position.X*matrix.M11) + (position.Y*matrix.M21) + (position.Z*matrix.M31) + matrix.M41,
                        (position.X*matrix.M12) + (position.Y*matrix.M22) + (position.Z*matrix.M32) + matrix.M42,
                        (position.X*matrix.M13) + (position.Y*matrix.M23) + (position.Z*matrix.M33) + matrix.M43);
            }
        }

	/// <summary>
        /// Transforms a vector by a quaternion rotation.
        /// </summary>
        /// <param name="vec">The vector to transform.</param>
        /// <param name="quat">The quaternion to rotate the vector by.</param>
        /// <returns>The result of the operation.</returns>
        public static Vec3 Transform(Vec3 vec, Quaternion quat)
        {
            Vec3 result;
            Transform(ref vec, ref quat, out result);
            return result;
        }

        /// <summary>
        /// Transforms a vector by a quaternion rotation.
        /// </summary>
        /// <param name="vec">The vector to transform.</param>
        /// <param name="quat">The quaternion to rotate the vector by.</param>
        /// <param name="result">The result of the operation.</param>
//        public static void Transform(ref Vector3 vec, ref Quaternion quat, out Vector3 result)
//        {
//		// Taken from the OpentTK implementation of Vector3
//            // Since vec.W == 0, we can optimize quat * vec * quat^-1 as follows:
//            // vec + 2.0 * cross(quat.xyz, cross(quat.xyz, vec) + quat.w * vec)
//            Vector3 xyz = quat.Xyz, temp, temp2;
//            Vector3.Cross(ref xyz, ref vec, out temp);
//            Vector3.Multiply(ref vec, quat.W, out temp2);
//            Vector3.Add(ref temp, ref temp2, out temp);
//            Vector3.Cross(ref xyz, ref temp, out temp);
//            Vector3.Multiply(ref temp, 2, out temp);
//            Vector3.Add(ref vec, ref temp, out result);
//        }

        /// <summary>
        /// Transforms a vector by a quaternion rotation.
        /// </summary>
        /// <param name="vec">The vector to transform.</param>
        /// <param name="quat">The quaternion to rotate the vector by.</param>
        /// <param name="result">The result of the operation.</param>
        public static void Transform(ref Vec3 value, ref Quaternion rotation, out Vec3 result)
        {
            float x = 2 * (rotation.Y * value.Z - rotation.Z * value.Y);
            float y = 2 * (rotation.Z * value.X - rotation.X * value.Z);
            float z = 2 * (rotation.X * value.Y - rotation.Y * value.X);

            result.X = value.X + x * rotation.W + (rotation.Y * z - rotation.Z * y);
            result.Y = value.Y + y * rotation.W + (rotation.Z * x - rotation.X * z);
            result.Z = value.Z + z * rotation.W + (rotation.X * y - rotation.Y * x);
        }

        /// <summary>
        /// Transforms an array of vectors by a quaternion rotation.
        /// </summary>
        /// <param name="sourceArray">The vectors to transform</param>
        /// <param name="rotation">The quaternion to rotate the vector by.</param>
        /// <param name="destinationArray">The result of the operation.</param>
        public static void Transform(Vec3[] sourceArray, ref Quaternion rotation, Vec3[] destinationArray)
        {
            //Debug.Assert(destinationArray.Length >= sourceArray.Length, "The destination array is smaller than the source array.");

            // TODO: Are there options on some platforms to implement a vectorized version of this?

            for (var i = 0; i < sourceArray.Length; i++)
            {
                var position = sourceArray[i];

                float x = 2 * (rotation.Y * position.Z - rotation.Z * position.Y);
                float y = 2 * (rotation.Z * position.X - rotation.X * position.Z);
                float z = 2 * (rotation.X * position.Y - rotation.Y * position.X);

                destinationArray[i] =
                    new Vec3(
                        position.X + x * rotation.W + (rotation.Y * z - rotation.Z * y),
                        position.Y + y * rotation.W + (rotation.Z * x - rotation.X * z),
                        position.Z + z * rotation.W + (rotation.X * y - rotation.Y * x));
            }
        }


        public static Vec3 TransformNormal(Vec3 normal, Matrix matrix)
        {
            TransformNormal(ref normal, ref matrix, out normal);
            return normal;
        }

        public static void TransformNormal(ref Vec3 normal, ref Matrix matrix, out Vec3 result)
        {
            result = new Vec3((normal.X * matrix.M11) + (normal.Y * matrix.M21) + (normal.Z * matrix.M31),
                                 (normal.X * matrix.M12) + (normal.Y * matrix.M22) + (normal.Z * matrix.M32),
                                 (normal.X * matrix.M13) + (normal.Y * matrix.M23) + (normal.Z * matrix.M33));
        }

        #endregion Public methods


        #region Operators

        public static bool operator ==(Vec3 value1, Vec3 value2)
        {
            return value1.X == value2.X
                && value1.Y == value2.Y
                && value1.Z == value2.Z;
        }

        public static bool operator !=(Vec3 value1, Vec3 value2)
        {
            return !(value1 == value2);
        }

        public static Vec3 operator +(Vec3 value1, Vec3 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            value1.Z += value2.Z;
            return value1;
        }

        public static Vec3 operator -(Vec3 value)
        {
            value = new Vec3(-value.X, -value.Y, -value.Z);
            return value;
        }

        public static Vec3 operator -(Vec3 value1, Vec3 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            value1.Z -= value2.Z;
            return value1;
        }

        public static Vec3 operator *(Vec3 value1, Vec3 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            value1.Z *= value2.Z;
            return value1;
        }

        public static Vec3 operator *(Vec3 value, float scaleFactor)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            value.Z *= scaleFactor;
            return value;
        }

        public static Vec3 operator *(float scaleFactor, Vec3 value)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            value.Z *= scaleFactor;
            return value;
        }

        public static Vec3 operator /(Vec3 value1, Vec3 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            value1.Z /= value2.Z;
            return value1;
        }

        public static Vec3 operator /(Vec3 value, float divider)
        {
            float factor = 1 / divider;
            value.X *= factor;
            value.Y *= factor;
            value.Z *= factor;
            return value;
        }
		
#if PSM
		public static implicit operator psmVector3 (Vec3 a)
		{
			return new psmVector3(a.X, a.Y, a.Z);
		}
		
		public static implicit operator Vec3 (psmVector3 a)
		{
			return new Vec3(a.X, a.Y, a.Z);
		}
#elif PC
        public static implicit operator Microsoft.Xna.Framework.Vector3(Vec3 a)
        {
            return new Microsoft.Xna.Framework.Vector3(a.X, a.Y, a.Z);
        }

        public static implicit operator Vec3(Microsoft.Xna.Framework.Vector3 a)
        {
            return new Vec3(a.X, a.Y, a.Z);
        }
#endif
#if BEPU
		public static implicit operator bepuVector3 (Vec3 a)
		{
			return new bepuVector3(a.X, a.Y, a.Z);
		}
		
		public static implicit operator Vec3 (bepuVector3 a)
		{
			return new Vec3(a.X, a.Y, a.Z);
		}
#endif
        #endregion
    }
}

