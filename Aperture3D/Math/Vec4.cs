using System;
using ProtoBuf;
using System.Text;
#if PSM
using PssVector4 = Sce.PlayStation.Core.Vector4;
#endif

namespace Aperture3D.Math
{
	   [ProtoContract]
    public struct Vec4 : IEquatable<Vec4>
    {
        #region Private Fields

        private static Vec4 zeroVector = new Vec4();
        private static Vec4 unitVector = new Vec4(1f, 1f, 1f, 1f);
        private static Vec4 unitXVector = new Vec4(1f, 0f, 0f, 0f);
        private static Vec4 unitYVector = new Vec4(0f, 1f, 0f, 0f);
        private static Vec4 unitZVector = new Vec4(0f, 0f, 1f, 0f);
        private static Vec4 unitWVector = new Vec4(0f, 0f, 0f, 1f);

        #endregion Private Fields


        #region Public Fields
        
        [ProtoMember(1)]
        public float X;

        [ProtoMember(2)]
        public float Y;
      
        [ProtoMember(3)]
        public float Z;
      
        [ProtoMember(4)]
        public float W;

        #endregion Public Fields


        #region Properties

        public static Vec4 Zero
        {
            get { return zeroVector; }
        }

        public static Vec4 One
        {
            get { return unitVector; }
        }

        public static Vec4 UnitX
        {
            get { return unitXVector; }
        }

        public static Vec4 UnitY
        {
            get { return unitYVector; }
        }

        public static Vec4 UnitZ
        {
            get { return unitZVector; }
        }

        public static Vec4 UnitW
        {
            get { return unitWVector; }
        }

        #endregion Properties


        #region Constructors

        public Vec4(float x, float y, float z, float w)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.W = w;
        }

        public Vec4(Vec2 value, float z, float w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = z;
            this.W = w;
        }

        public Vec4(Vec3 value, float w)
        {
            this.X = value.X;
            this.Y = value.Y;
            this.Z = value.Z;
            this.W = w;
        }

        public Vec4(float value)
        {
            this.X = value;
            this.Y = value;
            this.Z = value;
            this.W = value;
        }

        #endregion


        #region Public Methods

        public static Vec4 Add(Vec4 value1, Vec4 value2)
        {
            value1.W += value2.W;
            value1.X += value2.X;
            value1.Y += value2.Y;
            value1.Z += value2.Z;
            return value1;
        }

        public static void Add(ref Vec4 value1, ref Vec4 value2, out Vec4 result)
        {
            result.W = value1.W + value2.W;
            result.X = value1.X + value2.X;
            result.Y = value1.Y + value2.Y;
            result.Z = value1.Z + value2.Z;
        }

        public static Vec4 Barycentric(Vec4 value1, Vec4 value2, Vec4 value3, float amount1, float amount2)
        {
#if(USE_FARSEER)
            return new Vec4(
                SilverSpriteMathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                SilverSpriteMathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2),
                SilverSpriteMathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2),
                SilverSpriteMathHelper.Barycentric(value1.W, value2.W, value3.W, amount1, amount2));
#else
            return new Vec4(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2),
                MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2),
                MathHelper.Barycentric(value1.W, value2.W, value3.W, amount1, amount2));
#endif
        }

        public static void Barycentric(ref Vec4 value1, ref Vec4 value2, ref Vec4 value3, float amount1, float amount2, out Vec4 result)
        {
#if(USE_FARSEER)
            result = new Vec4(
                SilverSpriteMathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                SilverSpriteMathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2),
                SilverSpriteMathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2),
                SilverSpriteMathHelper.Barycentric(value1.W, value2.W, value3.W, amount1, amount2));
#else
            result = new Vec4(
                MathHelper.Barycentric(value1.X, value2.X, value3.X, amount1, amount2),
                MathHelper.Barycentric(value1.Y, value2.Y, value3.Y, amount1, amount2),
                MathHelper.Barycentric(value1.Z, value2.Z, value3.Z, amount1, amount2),
                MathHelper.Barycentric(value1.W, value2.W, value3.W, amount1, amount2));
#endif
        }

        public static Vec4 CatmullRom(Vec4 value1, Vec4 value2, Vec4 value3, Vec4 value4, float amount)
        {
#if(USE_FARSEER)
            return new Vec4(
                SilverSpriteMathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                SilverSpriteMathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount),
                SilverSpriteMathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount),
                SilverSpriteMathHelper.CatmullRom(value1.W, value2.W, value3.W, value4.W, amount));
#else
            return new Vec4(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount),
                MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount),
                MathHelper.CatmullRom(value1.W, value2.W, value3.W, value4.W, amount));
#endif
        }

        public static void CatmullRom(ref Vec4 value1, ref Vec4 value2, ref Vec4 value3, ref Vec4 value4, float amount, out Vec4 result)
        {
#if(USE_FARSEER)
            result = new Vec4(
                SilverSpriteMathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                SilverSpriteMathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount),
                SilverSpriteMathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount),
                SilverSpriteMathHelper.CatmullRom(value1.W, value2.W, value3.W, value4.W, amount));
#else
            result = new Vec4(
                MathHelper.CatmullRom(value1.X, value2.X, value3.X, value4.X, amount),
                MathHelper.CatmullRom(value1.Y, value2.Y, value3.Y, value4.Y, amount),
                MathHelper.CatmullRom(value1.Z, value2.Z, value3.Z, value4.Z, amount),
                MathHelper.CatmullRom(value1.W, value2.W, value3.W, value4.W, amount));
#endif
        }

        public static Vec4 Clamp(Vec4 value1, Vec4 min, Vec4 max)
        {
            return new Vec4(
                MathHelper.Clamp(value1.X, min.X, max.X),
                MathHelper.Clamp(value1.Y, min.Y, max.Y),
                MathHelper.Clamp(value1.Z, min.Z, max.Z),
                MathHelper.Clamp(value1.W, min.W, max.W));
        }

        public static void Clamp(ref Vec4 value1, ref Vec4 min, ref Vec4 max, out Vec4 result)
        {
            result = new Vec4(
                MathHelper.Clamp(value1.X, min.X, max.X),
                MathHelper.Clamp(value1.Y, min.Y, max.Y),
                MathHelper.Clamp(value1.Z, min.Z, max.Z),
                MathHelper.Clamp(value1.W, min.W, max.W));
        }

        public static float Distance(Vec4 value1, Vec4 value2)
        {
            return (float)System.Math.Sqrt(DistanceSquared(value1, value2));
        }

        public static void Distance(ref Vec4 value1, ref Vec4 value2, out float result)
        {
            result = (float)System.Math.Sqrt(DistanceSquared(value1, value2));
        }

        public static float DistanceSquared(Vec4 value1, Vec4 value2)
        {
            float result;
            DistanceSquared(ref value1, ref value2, out result);
            return result;
        }

        public static void DistanceSquared(ref Vec4 value1, ref Vec4 value2, out float result)
        {
            result = (value1.W - value2.W) * (value1.W - value2.W) +
                     (value1.X - value2.X) * (value1.X - value2.X) +
                     (value1.Y - value2.Y) * (value1.Y - value2.Y) +
                     (value1.Z - value2.Z) * (value1.Z - value2.Z);
        }

        public static Vec4 Divide(Vec4 value1, Vec4 value2)
        {
            value1.W /= value2.W;
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            value1.Z /= value2.Z;
            return value1;
        }

        public static Vec4 Divide(Vec4 value1, float divider)
        {
            float factor = 1f / divider;
            value1.W *= factor;
            value1.X *= factor;
            value1.Y *= factor;
            value1.Z *= factor;
            return value1;
        }

        public static void Divide(ref Vec4 value1, float divider, out Vec4 result)
        {
            float factor = 1f / divider;
            result.W = value1.W * factor;
            result.X = value1.X * factor;
            result.Y = value1.Y * factor;
            result.Z = value1.Z * factor;
        }

        public static void Divide(ref Vec4 value1, ref Vec4 value2, out Vec4 result)
        {
            result.W = value1.W / value2.W;
            result.X = value1.X / value2.X;
            result.Y = value1.Y / value2.Y;
            result.Z = value1.Z / value2.Z;
        }

        public static float Dot(Vec4 vector1, Vec4 vector2)
        {
            return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
        }

        public static void Dot(ref Vec4 vector1, ref Vec4 vector2, out float result)
        {
            result = vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
        }

        public override bool Equals(object obj)
        {
            return (obj is Vec4) ? this == (Vec4)obj : false;
        }

        public bool Equals(Vec4 other)
        {
            return this.W == other.W
                && this.X == other.X
                && this.Y == other.Y
                && this.Z == other.Z;
        }

        public override int GetHashCode()
        {
            return (int)(this.W + this.X + this.Y + this.Y);
        }

        public static Vec4 Hermite(Vec4 value1, Vec4 tangent1, Vec4 value2, Vec4 tangent2, float amount)
        {
            Vec4 result = new Vec4();
            Hermite(ref value1, ref tangent1, ref value2, ref tangent2, amount, out result);
            return result;
        }

        public static void Hermite(ref Vec4 value1, ref Vec4 tangent1, ref Vec4 value2, ref Vec4 tangent2, float amount, out Vec4 result)
        {
#if(USE_FARSEER)
            result.W = SilverSpriteMathHelper.Hermite(value1.W, tangent1.W, value2.W, tangent2.W, amount);
            result.X = SilverSpriteMathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            result.Y = SilverSpriteMathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
            result.Z = SilverSpriteMathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount);
#else
            result.W = MathHelper.Hermite(value1.W, tangent1.W, value2.W, tangent2.W, amount);
            result.X = MathHelper.Hermite(value1.X, tangent1.X, value2.X, tangent2.X, amount);
            result.Y = MathHelper.Hermite(value1.Y, tangent1.Y, value2.Y, tangent2.Y, amount);
            result.Z = MathHelper.Hermite(value1.Z, tangent1.Z, value2.Z, tangent2.Z, amount);
#endif
        }

        public float Length()
        {
            float result;
            DistanceSquared(ref this, ref zeroVector, out result);
            return (float)System.Math.Sqrt(result);
        }

        public float LengthSquared()
        {
            float result;
            DistanceSquared(ref this, ref zeroVector, out result);
            return result;
        }

        public static Vec4 Lerp(Vec4 value1, Vec4 value2, float amount)
        {
            return new Vec4(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount),
                MathHelper.Lerp(value1.Z, value2.Z, amount),
                MathHelper.Lerp(value1.W, value2.W, amount));
        }

        public static void Lerp(ref Vec4 value1, ref Vec4 value2, float amount, out Vec4 result)
        {
            result = new Vec4(
                MathHelper.Lerp(value1.X, value2.X, amount),
                MathHelper.Lerp(value1.Y, value2.Y, amount),
                MathHelper.Lerp(value1.Z, value2.Z, amount),
                MathHelper.Lerp(value1.W, value2.W, amount));
        }

        public static Vec4 Max(Vec4 value1, Vec4 value2)
        {
            return new Vec4(
               MathHelper.Max(value1.X, value2.X),
               MathHelper.Max(value1.Y, value2.Y),
               MathHelper.Max(value1.Z, value2.Z),
               MathHelper.Max(value1.W, value2.W));
        }

        public static void Max(ref Vec4 value1, ref Vec4 value2, out Vec4 result)
        {
            result = new Vec4(
               MathHelper.Max(value1.X, value2.X),
               MathHelper.Max(value1.Y, value2.Y),
               MathHelper.Max(value1.Z, value2.Z),
               MathHelper.Max(value1.W, value2.W));
        }

        public static Vec4 Min(Vec4 value1, Vec4 value2)
        {
            return new Vec4(
               MathHelper.Min(value1.X, value2.X),
               MathHelper.Min(value1.Y, value2.Y),
               MathHelper.Min(value1.Z, value2.Z),
               MathHelper.Min(value1.W, value2.W));
        }

        public static void Min(ref Vec4 value1, ref Vec4 value2, out Vec4 result)
        {
            result = new Vec4(
               MathHelper.Min(value1.X, value2.X),
               MathHelper.Min(value1.Y, value2.Y),
               MathHelper.Min(value1.Z, value2.Z),
               MathHelper.Min(value1.W, value2.W));
        }

        public static Vec4 Multiply(Vec4 value1, Vec4 value2)
        {
            value1.W *= value2.W;
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            value1.Z *= value2.Z;
            return value1;
        }

        public static Vec4 Multiply(Vec4 value1, float scaleFactor)
        {
            value1.W *= scaleFactor;
            value1.X *= scaleFactor;
            value1.Y *= scaleFactor;
            value1.Z *= scaleFactor;
            return value1;
        }

        public static void Multiply(ref Vec4 value1, float scaleFactor, out Vec4 result)
        {
            result.W = value1.W * scaleFactor;
            result.X = value1.X * scaleFactor;
            result.Y = value1.Y * scaleFactor;
            result.Z = value1.Z * scaleFactor;
        }

        public static void Multiply(ref Vec4 value1, ref Vec4 value2, out Vec4 result)
        {
            result.W = value1.W * value2.W;
            result.X = value1.X * value2.X;
            result.Y = value1.Y * value2.Y;
            result.Z = value1.Z * value2.Z;
        }

        public static Vec4 Negate(Vec4 value)
        {
            value = new Vec4(-value.X, -value.Y, -value.Z, -value.W);
            return value;
        }

        public static void Negate(ref Vec4 value, out Vec4 result)
        {
            result = new Vec4(-value.X, -value.Y, -value.Z,-value.W);
        }

        public void Normalize()
        {
            Normalize(ref this, out this);
        }

        public static Vec4 Normalize(Vec4 vector)
        {
            Normalize(ref vector, out vector);
            return vector;
        }

        public static void Normalize(ref Vec4 vector, out Vec4 result)
        {
            float factor;
            DistanceSquared(ref vector, ref zeroVector, out factor);
            factor = 1f / (float)System.Math.Sqrt(factor);

            result.W = vector.W * factor;
            result.X = vector.X * factor;
            result.Y = vector.Y * factor;
            result.Z = vector.Z * factor;
        }

        public static Vec4 SmoothStep(Vec4 value1, Vec4 value2, float amount)
        {
#if(USE_FARSEER)
            return new Vec4(
                SilverSpriteMathHelper.SmoothStep(value1.X, value2.X, amount),
                SilverSpriteMathHelper.SmoothStep(value1.Y, value2.Y, amount),
                SilverSpriteMathHelper.SmoothStep(value1.Z, value2.Z, amount),
                SilverSpriteMathHelper.SmoothStep(value1.W, value2.W, amount));
#else
            return new Vec4(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount),
                MathHelper.SmoothStep(value1.Z, value2.Z, amount),
                MathHelper.SmoothStep(value1.W, value2.W, amount));
#endif
        }

        public static void SmoothStep(ref Vec4 value1, ref Vec4 value2, float amount, out Vec4 result)
        {
#if(USE_FARSEER)
            result = new Vec4(
                SilverSpriteMathHelper.SmoothStep(value1.X, value2.X, amount),
                SilverSpriteMathHelper.SmoothStep(value1.Y, value2.Y, amount),
                SilverSpriteMathHelper.SmoothStep(value1.Z, value2.Z, amount),
                SilverSpriteMathHelper.SmoothStep(value1.W, value2.W, amount));
#else
            result = new Vec4(
                MathHelper.SmoothStep(value1.X, value2.X, amount),
                MathHelper.SmoothStep(value1.Y, value2.Y, amount),
                MathHelper.SmoothStep(value1.Z, value2.Z, amount),
                MathHelper.SmoothStep(value1.W, value2.W, amount));
#endif
        }

        public static Vec4 Subtract(Vec4 value1, Vec4 value2)
        {
            value1.W -= value2.W;
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            value1.Z -= value2.Z;
            return value1;
        }

        public static void Subtract(ref Vec4 value1, ref Vec4 value2, out Vec4 result)
        {
            result.W = value1.W - value2.W;
            result.X = value1.X - value2.X;
            result.Y = value1.Y - value2.Y;
            result.Z = value1.Z - value2.Z;
        }

        public static Vec4 Transform(Vec2 position, Matrix matrix)
        {
            Vec4 result;
            Transform(ref position, ref matrix, out result);
            return result;
        }

        public static Vec4 Transform(Vec3 position, Matrix matrix)
        {
            Vec4 result;
            Transform(ref position, ref matrix, out result);
            return result;
        }

        public static Vec4 Transform(Vec4 vector, Matrix matrix)
        {
            Transform(ref vector, ref matrix, out vector);
            return vector;
        }

        public static void Transform(ref Vec2 position, ref Matrix matrix, out Vec4 result)
        {
            result = new Vec4((position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41,
                                 (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42,
                                 (position.X * matrix.M13) + (position.Y * matrix.M23) + matrix.M43,
                                 (position.X * matrix.M14) + (position.Y * matrix.M24) + matrix.M44);
        }

        public static void Transform(ref Vec3 position, ref Matrix matrix, out Vec4 result)
        {
            result = new Vec4((position.X * matrix.M11) + (position.Y * matrix.M21) + (position.Z * matrix.M31) + matrix.M41,
                                 (position.X * matrix.M12) + (position.Y * matrix.M22) + (position.Z * matrix.M32) + matrix.M42,
                                 (position.X * matrix.M13) + (position.Y * matrix.M23) + (position.Z * matrix.M33) + matrix.M43,
                                 (position.X * matrix.M14) + (position.Y * matrix.M24) + (position.Z * matrix.M34) + matrix.M44);
        }

        public static void Transform(ref Vec4 vector, ref Matrix matrix, out Vec4 result)
        {
            result = new Vec4((vector.X * matrix.M11) + (vector.Y * matrix.M21) + (vector.Z * matrix.M31) + (vector.W * matrix.M41),
                                 (vector.X * matrix.M12) + (vector.Y * matrix.M22) + (vector.Z * matrix.M32) + (vector.W * matrix.M42),
                                 (vector.X * matrix.M13) + (vector.Y * matrix.M23) + (vector.Z * matrix.M33) + (vector.W * matrix.M43),
                                 (vector.X * matrix.M14) + (vector.Y * matrix.M24) + (vector.Z * matrix.M34) + (vector.W * matrix.M44));
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
            sb.Append(" W:");
            sb.Append(this.W);
            sb.Append("}");
            return sb.ToString();
        }

        #endregion Public Methods


        #region Operators

        public static Vec4 operator -(Vec4 value)
        {
            return new Vec4(-value.X, -value.Y, -value.Z, -value.W);
        }

        public static bool operator ==(Vec4 value1, Vec4 value2)
        {
            return value1.W == value2.W
                && value1.X == value2.X
                && value1.Y == value2.Y
                && value1.Z == value2.Z;
        }

        public static bool operator !=(Vec4 value1, Vec4 value2)
        {
            return !(value1 == value2);
        }

        public static Vec4 operator +(Vec4 value1, Vec4 value2)
        {
            value1.W += value2.W;
            value1.X += value2.X;
            value1.Y += value2.Y;
            value1.Z += value2.Z;
            return value1;
        }

        public static Vec4 operator -(Vec4 value1, Vec4 value2)
        {
            value1.W -= value2.W;
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            value1.Z -= value2.Z;
            return value1;
        }

        public static Vec4 operator *(Vec4 value1, Vec4 value2)
        {
            value1.W *= value2.W;
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            value1.Z *= value2.Z;
            return value1;
        }

        public static Vec4 operator *(Vec4 value1, float scaleFactor)
        {
            value1.W *= scaleFactor;
            value1.X *= scaleFactor;
            value1.Y *= scaleFactor;
            value1.Z *= scaleFactor;
            return value1;
        }

        public static Vec4 operator *(float scaleFactor, Vec4 value1)
        {
            value1.W *= scaleFactor;
            value1.X *= scaleFactor;
            value1.Y *= scaleFactor;
            value1.Z *= scaleFactor;
            return value1;
        }

        public static Vec4 operator /(Vec4 value1, Vec4 value2)
        {
            value1.W /= value2.W;
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            value1.Z /= value2.Z;
            return value1;
        }

        public static Vec4 operator /(Vec4 value1, float divider)
        {
            float factor = 1f / divider;
            value1.W *= factor;
            value1.X *= factor;
            value1.Y *= factor;
            value1.Z *= factor;
            return value1;
        }
		
#if PSM
		public static implicit operator PssVector4(Vec4 v)
        {
            return new PssVector4(v.X, v.Y, v.Z, v.W);
        }
		
		public static implicit operator Vec4(PssVector4 v)
        {
            return new Vec4(v.X, v.Y, v.Z, v.W);
        }
#elif PC
        public static implicit operator Microsoft.Xna.Framework.Vector4(Vec4 v)
        {
            return new Microsoft.Xna.Framework.Vector4(v.X, v.Y, v.Z, v.W);
        }

        public static implicit operator Vec4(Microsoft.Xna.Framework.Vector4 v)
        {
            return new Vec4(v.X, v.Y, v.Z, v.W);
        }

        public static implicit operator Microsoft.Xna.Framework.Color(Vec4 v)
        {
            return new Microsoft.Xna.Framework.Color(v.X, v.Y, v.Z, v.W);
        }

        public static implicit operator Vec4(Microsoft.Xna.Framework.Color v)
        {
            return new Vec4(v.R, v.G, v.B, v.A);
        }
#endif
        #endregion Operators
    }
}

