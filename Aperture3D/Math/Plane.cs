using System;
using ProtoBuf;

using Math = System.Math;

namespace Aperture3D.Math
{
		internal class PlaneHelper
    {
        /// <summary>
        /// Returns a value indicating what side (positive/negative) of a plane a point is
        /// </summary>
        /// <param name="point">The point to check with</param>
        /// <param name="plane">The plane to check against</param>
        /// <returns>Greater than zero if on the positive side, less than zero if on the negative size, 0 otherwise</returns>
        public static float ClassifyPoint(ref Vec3 point, ref Plane plane)
        {
            return point.X * plane.Normal.X + point.Y * plane.Normal.Y + point.Z * plane.Normal.Z + plane.D;
        }

        /// <summary>
        /// Returns the perpendicular distance from a point to a plane
        /// </summary>
        /// <param name="point">The point to check</param>
        /// <param name="plane">The place to check</param>
        /// <returns>The perpendicular distance from the point to the plane</returns>
        public static float PerpendicularDistance(ref Vec3 point, ref Plane plane)
        {
            // dist = (ax + by + cz + d) / sqrt(a*a + b*b + c*c)
            return (float)System.Math.Abs((plane.Normal.X * point.X + plane.Normal.Y * point.Y + plane.Normal.Z * point.Z)
                                    / System.Math.Sqrt(plane.Normal.X * plane.Normal.X + plane.Normal.Y * plane.Normal.Y + plane.Normal.Z * plane.Normal.Z));
        }
    }
	
    [ProtoContract]
    public struct Plane : IEquatable<Plane>
    {
        #region Public Fields

        [ProtoMember(1)]
        public float D;

        [ProtoMember(2)]
        public Vec3 Normal;

        #endregion Public Fields


        #region Constructors

        public Plane(Vec4 value)
            : this(new Vec3(value.X, value.Y, value.Z), value.W)
        {

        }

        public Plane(Vec3 normal, float d)
        {
            Normal = normal;
            D = d;
        }

        public Plane(Vec3 a, Vec3 b, Vec3 c)
        {
            Vec3 ab = b - a;
            Vec3 ac = c - a;

            Vec3 cross = Vec3.Cross(ab, ac);
            Normal = Vec3.Normalize(cross);
            D = -(Vec3.Dot(Normal, a));
        }

        public Plane(float a, float b, float c, float d)
            : this(new Vec3(a, b, c), d)
        {

        }

        #endregion Constructors


        #region Public Methods

        public float Dot(Vec4 value)
        {
            return ((((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + (this.D * value.W));
        }

        public void Dot(ref Vec4 value, out float result)
        {
            result = (((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + (this.D * value.W);
        }

        public float DotCoordinate(Vec3 value)
        {
            return ((((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + this.D);
        }

        public void DotCoordinate(ref Vec3 value, out float result)
        {
            result = (((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z)) + this.D;
        }

        public float DotNormal(Vec3 value)
        {
            return (((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z));
        }

        public void DotNormal(ref Vec3 value, out float result)
        {
            result = ((this.Normal.X * value.X) + (this.Normal.Y * value.Y)) + (this.Normal.Z * value.Z);
        }
        
        /*
        public static void Transform(ref Plane plane, ref Quaternion rotation, out Plane result)
        {
            throw new NotImplementedException();
        }

        public static void Transform(ref Plane plane, ref Matrix matrix, out Plane result)
        {
            throw new NotImplementedException();
        }

        public static Plane Transform(Plane plane, Quaternion rotation)
        {
            throw new NotImplementedException();
        }

        public static Plane Transform(Plane plane, Matrix matrix)
        {
            throw new NotImplementedException();
        }
        */

        public void Normalize()
        {
			float factor;
			Vec3 normal = Normal;
			Normal = Vec3.Normalize(Normal);
			factor = (float)System.Math.Sqrt(Normal.X * Normal.X + Normal.Y * Normal.Y + Normal.Z * Normal.Z) / 
					(float)System.Math.Sqrt(normal.X * normal.X + normal.Y * normal.Y + normal.Z * normal.Z);
			D = D * factor;
        }

        public static Plane Normalize(Plane value)
        {
			Plane ret;
			Normalize(ref value, out ret);
			return ret;
        }

        public static void Normalize(ref Plane value, out Plane result)
        {
			float factor;
			result.Normal = Vec3.Normalize(value.Normal);
			factor = (float)System.Math.Sqrt(result.Normal.X * result.Normal.X + result.Normal.Y * result.Normal.Y + result.Normal.Z * result.Normal.Z) / 
					(float)System.Math.Sqrt(value.Normal.X * value.Normal.X + value.Normal.Y * value.Normal.Y + value.Normal.Z * value.Normal.Z);
			result.D = value.D * factor;
        }

        public static bool operator !=(Plane plane1, Plane plane2)
        {
            return !plane1.Equals(plane2);
        }

        public static bool operator ==(Plane plane1, Plane plane2)
        {
            return plane1.Equals(plane2);
        }

        public override bool Equals(object other)
        {
            return (other is Plane) ? this.Equals((Plane)other) : false;
        }

        public bool Equals(Plane other)
        {
            return ((Normal == other.Normal) && (D == other.D));
        }

        public override int GetHashCode()
        {
            return Normal.GetHashCode() ^ D.GetHashCode();
        }
		/*
        public PlaneIntersectionType Intersects(BoundingBox box)
        {
            return box.Intersects(this);
        }

        public void Intersects(ref BoundingBox box, out PlaneIntersectionType result)
        {
            box.Intersects (ref this, out result);
        }

        /*
        public PlaneIntersectionType Intersects(BoundingFrustum frustum)
        {
            return frustum.Intersects(this);
        }
        */
		/*
        public PlaneIntersectionType Intersects(BoundingSphere sphere)
        {
            return sphere.Intersects(this);
        }

        public void Intersects(ref BoundingSphere sphere, out PlaneIntersectionType result)
        {
            sphere.Intersects(ref this, out result);
        }
		*/
        public override string ToString()
        {
            return string.Format("{{Normal:{0} D:{1}}}", Normal, D);
        }

        #endregion
    }
}

