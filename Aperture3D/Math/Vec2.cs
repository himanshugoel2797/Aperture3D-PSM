using System;
using ProtoBuf;
using System.Globalization;

#if BEPU
using BEPUphysics;
using BepuVector2 = BEPUphysics.MathExtensions.Vector2D;
#endif

namespace Aperture3D.Math
{
	   [ProtoContract]
    public struct Vec2 : IEquatable<Vec2>
    {
        #region Private Fields

        private static Vec2 zeroVector = new Vec2(0f, 0f);
        private static Vec2 unitVector = new Vec2(1f, 1f);
        private static Vec2 unitXVector = new Vec2(1f, 0f);
        private static Vec2 unitYVector = new Vec2(0f, 1f);

        #endregion Private Fields


        #region Public Fields
      
        [ProtoMember(1)]
        public float X;
        
        [ProtoMember(2)]
        public float Y;

        #endregion Public Fields


        #region Properties

        public static Vec2 Zero
        {
            get { return zeroVector; }
        }

        public static Vec2 One
        {
            get { return unitVector; }
        }

        public static Vec2 UnitX
        {
            get { return unitXVector; }
        }

        public static Vec2 UnitY
        {
            get { return unitYVector; }
        }

        #endregion Properties


        #region Constructors

        public Vec2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }
		 
        public Vec2(float value)
        {
            this.X = value;
            this.Y = value;
        }

        #endregion Constructors


        #region Public Methods

        public static Vec2 Add(Vec2 value1, Vec2 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            return value1;
        }

        public static void Add(ref Vec2 value1, ref Vec2 value2, out Vec2 result)
        {
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
        }

        public static Vec2 Barycentric(Vec2 value1, Vec2 value2, Vec2 value3, float amount1, float amount2)
        {
            return new Vec2(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2));
        }

        public static void Barycentric(ref Vec2 value1, ref Vec2 value2, ref Vec2 value3, float amount1, float amount2, out Vec2 result)
        {
            result = new Vec2(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2));
        }

        public static Vec2 CatmullRom(Vec2 value1, Vec2 value2, Vec2 value3, Vec2 value4, float amount)
        {
            return new Vec2(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
        }

        public static void CatmullRom(ref Vec2 value1, ref Vec2 value2, ref Vec2 value3, ref Vec2 value4, float amount, out Vec2 result)
        {
            result = new Vec2(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount));
        }

        public static Vec2 Clamp(Vec2 value1, Vec2 min, Vec2 max)
        {
            return new Vec2(
                MathHelper.Clamp(value1.X, min.X, max.X),
                MathHelper.Clamp(value1.Y, min.Y, max.Y));
        }

        public static void Clamp(ref Vec2 value1, ref Vec2 min, ref Vec2 max, out Vec2 result)
        {
            result = new Vec2(
                MathHelper.Clamp(value1.X, min.X, max.X),
                MathHelper.Clamp(value1.Y, min.Y, max.Y));
        }

        public static float Distance(Vec2 value1, Vec2 value2)
        {
			float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			return (float)System.Math.Sqrt((v1 * v1) + (v2 * v2));
        }

        public static void Distance(ref Vec2 value1, ref Vec2 value2, out float result)
        {
			float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
            result = (float)System.Math.Sqrt((v1 * v1) + (v2 * v2));
        }

        public static float DistanceSquared(Vec2 value1, Vec2 value2)
        {
			float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			return (v1 * v1) + (v2 * v2);
        }

        public static void DistanceSquared(ref Vec2 value1, ref Vec2 value2, out float result)
        {
			float v1 = value1.X - value2.X, v2 = value1.Y - value2.Y;
			result = (v1 * v1) + (v2 * v2);
        }

        public static Vec2 Divide(Vec2 value1, Vec2 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            return value1;
        }

        public static void Divide(ref Vec2 value1, ref Vec2 value2, out Vec2 result)
        {
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
        }

        public static Vec2 Divide(Vec2 value1, float divider)
        {
            float factor = 1 / divider;
            value1.X *= factor;
            value1.Y *= factor;
            return value1;
        }

        public static void Divide(ref Vec2 value1, float divider, out Vec2 result)
        {
            float factor = 1 / divider;
            result.X = value1.X * factor;
            result.Y = value1.Y * factor;
        }

        public static float Dot(Vec2 value1, Vec2 value2)
        {
            return (value1.X * value2.X) + (value1.Y * value2.Y);
        }

        public static void Dot(ref Vec2 value1, ref Vec2 value2, out float result)
        {
            result = (value1.X * value2.X) + (value1.Y * value2.Y);
        }

        public override bool Equals(object obj)
        {
			if(obj is Vec2)
			{
				return Equals((Vec2)obj);
			}
			
            return false;
        }

        public bool Equals(Vec2 other)
        {
            return (X == other.X) && (Y == other.Y);
        }
		
		public static Vec2 Reflect(Vec2 vector, Vec2 normal)
		{
			Vec2 result;
			float val = 2.0f * ((vector.X * normal.X) + (vector.Y * normal.Y));
			result.X = vector.X - (normal.X * val);
			result.Y = vector.Y - (normal.Y * val);
			return result;
		}
		
		public static void Reflect(ref Vec2 vector, ref Vec2 normal, out Vec2 result)
		{
			float val = 2.0f * ((vector.X * normal.X) + (vector.Y * normal.Y));
			result.X = vector.X - (normal.X * val);
			result.Y = vector.Y - (normal.Y * val);
		}
		
        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public static Vec2 Hermite(Vec2 value1, Vec2 tangent1, Vec2 value2, Vec2 tangent2, float amount)
        {
            Vec2 result = new Vec2();
            Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out result);
            return result;
        }

        public static void Hermite(ref Vec2 value1, ref Vec2 tangent1, ref Vec2 value2, ref Vec2 tangent2, float amount, out Vec2 result)
        {
            result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
        }

        public float Length()
        {
			return (float)System.Math.Sqrt((X * X) + (Y * Y));
        }

        public float LengthSquared()
        {
			return (X * X) + (Y * Y);
        }

        public static Vec2 Lerp(Vec2 value1, Vec2 value2, float amount)
        {
            return new Vec2(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount));
        }

        public static void Lerp(ref Vec2 value1, ref Vec2 value2, float amount, out Vec2 result)
        {
            result = new Vec2(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount));
        }

        public static Vec2 Max(Vec2 value1, Vec2 value2)
        {
            return new Vec2(value1.X > value2.X ? value1.X : value2.X, 
			                   value1.Y > value2.Y ? value1.Y : value2.Y);
        }

        public static void Max(ref Vec2 value1, ref Vec2 value2, out Vec2 result)
        {
            result.X = value1.X > value2.X ? value1.X : value2.X;
			result.Y = value1.Y > value2.Y ? value1.Y : value2.Y;
        }

        public static Vec2 Min(Vec2 value1, Vec2 value2)
        {
            return new Vec2(value1.X < value2.X ? value1.X : value2.X, 
			                   value1.Y < value2.Y ? value1.Y : value2.Y); 
        }

        public static void Min(ref Vec2 value1, ref Vec2 value2, out Vec2 result)
        {
            result.X = value1.X < value2.X ? value1.X : value2.X;
			result.Y = value1.Y < value2.Y ? value1.Y : value2.Y;
		}

        public static Vec2 Multiply(Vec2 value1, Vec2 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            return value1;
        }

        public static Vec2 Multiply(Vec2 value1, float scaleFactor)
        {
            value1.X *= scaleFactor;
            value1.Y *= scaleFactor;
            return value1;
        }

        public static void Multiply(ref Vec2 value1, float scaleFactor, out Vec2 result)
        {
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
        }

        public static void Multiply(ref Vec2 value1, ref Vec2 value2, out Vec2 result)
        {
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
        }

        public static Vec2 Negate(Vec2 value)
        {
            value.X = -value.X;
            value.Y = -value.Y;
            return value;
        }

        public static void Negate(ref Vec2 value, out Vec2 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
        }

        public void Normalize()
        {
			float val = 1.0f / (float)System.Math.Sqrt((X * X) + (Y * Y));
			X *= val;
			Y *= val;
        }

        public static Vec2 Normalize(Vec2 value)
        {
			float val = 1.0f / (float)System.Math.Sqrt((value.X * value.X) + (value.Y * value.Y));
			value.X *= val;
			value.Y *= val;
            return value;
        }

        public static void Normalize(ref Vec2 value, out Vec2 result)
        {
			float val = 1.0f / (float)System.Math.Sqrt((value.X * value.X) + (value.Y * value.Y));
			result.X = value.X * val;
			result.Y = value.Y * val;
        }

        public static Vec2 SmoothStep(Vec2 value1, Vec2 value2, float amount)
        {
            return new Vec2(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount));
        }

        public static void SmoothStep(ref Vec2 value1, ref Vec2 value2, float amount, out Vec2 result)
        {
            result = new Vec2(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount));
        }

        public static Vec2 Subtract(Vec2 value1, Vec2 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            return value1;
        }

        public static void Subtract(ref Vec2 value1, ref Vec2 value2, out Vec2 result)
        {
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
        }

        public static Vec2 Transform(Vec2 position, Matrix matrix)
        {
            Transform(ref position, ref matrix, out position);
            return position;
        }

        public static void Transform(ref Vec2 position, ref Matrix matrix, out Vec2 result)
        {
            result = new Vec2((position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41,
                                 (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42);
        }

        public static Vec2 Transform(Vec2 position, Quaternion quat)
        {
            Transform(ref position, ref quat, out position);
            return position;
        }

        public static void Transform(ref Vec2 position, ref Quaternion quat, out Vec2 result)
        {
            Quaternion v = new Quaternion(position.X, position.Y, 0, 0), i, t;
            Quaternion.Inverse(ref quat, out i);
            Quaternion.Multiply(ref quat, ref v, out t);
            Quaternion.Multiply(ref t, ref i, out v);

            result = new Vec2(v.X, v.Y);
        }
		
		public static void Transform (
			Vec2[] sourceArray,
			ref Matrix matrix,
			Vec2[] destinationArray)
		{
			Transform(sourceArray, 0, ref matrix, destinationArray, 0, sourceArray.Length);
		}

		
		public static void Transform (
			Vec2[] sourceArray,
			int sourceIndex,
			ref Matrix matrix,
			Vec2[] destinationArray,
			int destinationIndex,
			int length)
		{
			for (int x = 0; x < length; x++) {
				var position = sourceArray[sourceIndex + x];
				var destination = destinationArray[destinationIndex + x];
				destination.X = (position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41;
				destination.Y = (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42;
				destinationArray[destinationIndex + x] = destination;
			}
		}

        public static Vec2 TransformNormal(Vec2 normal, Matrix matrix)
        {
            Vec2.TransformNormal(ref normal, ref matrix, out normal);
            return normal;
        }

        public static void TransformNormal(ref Vec2 normal, ref Matrix matrix, out Vec2 result)
        {
            result = new Vec2((normal.X * matrix.M11) + (normal.Y * matrix.M21),
                                 (normal.X * matrix.M12) + (normal.Y * matrix.M22));
        }

        public override string ToString()
        {
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
        	return string.Format(currentCulture, "{{X:{0} Y:{1}}}", new object[] { 
				this.X.ToString(currentCulture), this.Y.ToString(currentCulture) });
        }

        #endregion Public Methods


        #region Operators

        public static Vec2 operator -(Vec2 value)
        {
            value.X = -value.X;
            value.Y = -value.Y;
            return value;
        }


        public static bool operator ==(Vec2 value1, Vec2 value2)
        {
            return value1.X == value2.X && value1.Y == value2.Y;
        }


        public static bool operator !=(Vec2 value1, Vec2 value2)
        {
            return value1.X != value2.X || value1.Y != value2.Y;
        }


        public static Vec2 operator +(Vec2 value1, Vec2 value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            return value1;
        }


        public static Vec2 operator -(Vec2 value1, Vec2 value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            return value1;
        }


        public static Vec2 operator *(Vec2 value1, Vec2 value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            return value1;
        }


        public static Vec2 operator *(Vec2 value, float scaleFactor)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            return value;
        }


        public static Vec2 operator *(float scaleFactor, Vec2 value)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            return value;
        }


        public static Vec2 operator /(Vec2 value1, Vec2 value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            return value1;
        }


        public static Vec2 operator /(Vec2 value1, float divider)
        {
            float factor = 1 / divider;
            value1.X *= factor;
            value1.Y *= factor;
            return value1;
        }
		
#if PSM
		public static implicit operator Vec2 (Sce.PlayStation.Core.Vector2 a)
		{
			return new Vec2(a.X, a.Y);	
		}
		
		public static implicit operator Sce.PlayStation.Core.Vector2 (Vec2 a)
		{
			return new Sce.PlayStation.Core.Vector2(a.X, a.Y);	
		}
#elif PC
        public static implicit operator Vec2(Microsoft.Xna.Framework.Vector2 a)
        {
            return new Vec2(a.X, a.Y);
        }

        public static implicit operator Microsoft.Xna.Framework.Vector2(Vec2 a)
        {
            return new Microsoft.Xna.Framework.Vector2(a.X, a.Y);
        }
#endif

#if BEPU
		public static implicit operator Vec2 (BepuVector2 a)
		{
			return new Vec2(a.X, a.Y);	
		}
		
		public static implicit operator BepuVector2 (Vec2 a)
		{
			return new BepuVector2(a.X, a.Y);	
		}
#endif
        #endregion Operators
    }
}

