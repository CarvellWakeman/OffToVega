using Microsoft.Xna.Framework;
using System;
using System.Text;
using ExplorationEngine;

namespace ExplorationEngine
{
	//Vector2d/vector3d class is meant to replace Vector2/vector3 class in the standard XNA framework. It uses double precision instead of single float precision.
	//Found at: https://github.com/kohlditz/vector3d/blob/master/Vector2d.cs
	public struct Vector2d
		{
			public const double kEpsilon = 1E-05d;
			public double X;
			public double Y;

			public double this[int index]
			{
				get
				{
					switch (index)
					{
						case 0:
							return this.X;
						case 1:
							return this.Y;
						default:
							throw new IndexOutOfRangeException("Invalid Vector2d index!");
					}
				}
				set
				{
					switch (index)
					{
						case 0:
							this.X = value;
							break;
						case 1:
							this.Y = value;
							break;
						default:
							throw new IndexOutOfRangeException("Invalid Vector2d index!");
					}
				}
			}


			static public implicit operator Microsoft.Xna.Framework.Vector2(Vector2d value)
			{
				return new Vector2((float)value.X, (float)value.Y);
			}

			static public implicit operator Vector2d(Microsoft.Xna.Framework.Vector2 value)
			{
				return new Vector2d((double)value.X, (double)value.Y);
			}


			public Vector2d Normalized
			{
				get
				{
					Vector2d vector2d = new Vector2d(this.X, this.Y);
					vector2d.Normalize();
					return vector2d;
				}
			}

			public double Magnitude
			{
				get
				{
					return Math.Sqrt(this.X * this.X + this.Y * this.Y);
				}
			}

			public static Vector2d Zero
			{
				get
				{
					return new Vector2d(0.0d, 0.0d);
				}
			}

			public static Vector2d One
			{
				get
				{
					return new Vector2d(1d, 1d);
				}
			}

			public static Vector2d Up
			{
				get
				{
					return new Vector2d(0.0d, 1d);
				}
			}

			public static Vector2d Right
			{
				get
				{
					return new Vector2d(1d, 0.0d);
				}
			}

			public Vector2d(double X, double Y)
			{
				this.X = X;
				this.Y = Y;
			}

			//public static implicit operator Vector2d(Vector3d v) {
			//     return new Vector2d(v.X, v.Y);
			//}

			//public static implicit operator Vector3d(Vector2d v) {
			//     return new Vector3d(v.X, v.Y, 0.0d);
			//}

			public static Vector2d operator +(Vector2d a, Vector2d b)
			{
				return new Vector2d(a.X + b.X, a.Y + b.Y);
			}

			public static Vector2d operator -(Vector2d a, Vector2d b)
			{
				return new Vector2d(a.X - b.X, a.Y - b.Y);
			}

			public static Vector2d operator -(Vector2d a)
			{
				return new Vector2d(-a.X, -a.Y);
			}

			public static Vector2d operator *(Vector2d a, double d)
			{
				return new Vector2d(a.X * d, a.Y * d);
			}

			public static Vector2d operator *(float d, Vector2d a)
			{
				return new Vector2d(a.X * d, a.Y * d);
			}

			public static Vector2d operator *(Vector2d a, Vector2d b)
			{
				return new Vector2d(a.X * b.X, a.Y * b.Y);
			}

			public static Vector2d operator /(Vector2d a, double d)
			{
				return new Vector2d(a.X / d, a.Y / d);
			}

			public static Vector2d operator /(Vector2d a, Vector2d b)
			{
				return new Vector2d(a.X / b.X, a.Y / b.Y);
			}

			public static bool operator ==(Vector2d lhs, Vector2d rhs)
			{
				return (lhs - rhs).Magnitude < kEpsilon;
				//return Vector2d.SqrMagnitude(lhs - rhs) < 0.0 / 1.0;
			}

			public static bool operator !=(Vector2d lhs, Vector2d rhs)
			{
				return (lhs - rhs).Magnitude >= kEpsilon;
				//return (double)Vector2d.SqrMagnitude(lhs - rhs) >= 0.0 / 1.0;
			}

			public void Set(double new_x, double new_y)
			{
				this.X = new_x;
				this.Y = new_y;
			}

			//May not work
			public static Vector2d Lerp(Vector2d from, Vector2d to, double t)
			{
				//t = Mathd.Clamp01(t);
				return new Vector2d(from.X + (to.X - from.X) * t, from.Y + (to.Y - from.Y) * t);
			}

			public static Vector2d MoveTowards(Vector2d current, Vector2d target, double maxDistanceDelta)
			{
				Vector2d vector2 = target - current;
				double magnitude = vector2.Magnitude;
				if (magnitude <= maxDistanceDelta || magnitude == 0.0d)
					return target;
				else
					return current + vector2 / magnitude * maxDistanceDelta;
			}

			public static Vector2d Scale(Vector2d a, Vector2d b)
			{
				return new Vector2d(a.X * b.X, a.Y * b.Y);
			}

			public void Scale(Vector2d scale)
			{
				this.X *= scale.X;
				this.Y *= scale.Y;
			}

			public void Normalize()
			{
				double magnitude = this.Magnitude;
				if (magnitude > 9.99999974737875E-06)
					this = this / magnitude;
				else
					this = Vector2d.Zero;
			}

			public static Vector2d Transform(Vector2d position, Matrix matrix)
			{
				Transform(ref position, ref matrix, out position);
				return position;
			}

			public static void Transform(ref Vector2d position, ref Matrix matrix, out Vector2d result)
			{
				result = new Vector2d((position.X * matrix.M11) + (position.Y * matrix.M21) + matrix.M41,
									 (position.X * matrix.M12) + (position.Y * matrix.M22) + matrix.M42);
			}

			public static Vector2d Transform(Vector2d value, Quaternion rotation)
			{
				throw new NotImplementedException();
			}

			public static void Transform(ref Vector2d value, ref Quaternion rotation, out Vector2d result)
			{
				throw new NotImplementedException();
			}

			public static void Transform(Vector2d[] sourceArray, ref Matrix matrix, Vector2d[] destinationArray)
			{
				throw new NotImplementedException();
			}

			public static void Transform(Vector2d[] sourceArray, ref Quaternion rotation, Vector2d[] destinationArray)
			{
				throw new NotImplementedException();
			}

			public static void Transform(Vector2d[] sourceArray, int sourceIndex, ref Matrix matrix, Vector2d[] destinationArray, int destinationIndex, int length)
			{
				throw new NotImplementedException();
			}

			public static void Transform(Vector2d[] sourceArray, int sourceIndex, ref Quaternion rotation, Vector2d[] destinationArray, int destinationIndex, int length)
			{
				throw new NotImplementedException();
			}

			public static Vector2d TransformNormal(Vector2d normal, Matrix matrix)
			{
				Vector2d.TransformNormal(ref normal, ref matrix, out normal);
				return normal;
			}

			public static void TransformNormal(ref Vector2d normal, ref Matrix matrix, out Vector2d result)
			{
				result = new Vector2d((normal.X * matrix.M11) + (normal.Y * matrix.M21),
									 (normal.X * matrix.M12) + (normal.Y * matrix.M22));
			}

			public static void TransformNormal(Vector2d[] sourceArray, ref Matrix matrix, Vector2d[] destinationArray)
			{
				throw new NotImplementedException();
			}

			public static void TransformNormal(Vector2d[] sourceArray, int sourceIndex, ref Matrix matrix, Vector2d[] destinationArray, int destinationIndex, int length)
			{
				throw new NotImplementedException();
			}

			public override string ToString()
			{
				StringBuilder sb = new StringBuilder(24);
				sb.Append("{X:");
				sb.Append(this.X);
				sb.Append(" Y:");
				sb.Append(this.Y);
				sb.Append("}");
				return sb.ToString();
			}

			/* public override string ToString() 
			{
				string fmt = "({0:D1}, {1:D1})";
				object[] objArray = new object[2];
				int index1 = 0;
				// ISSUE: variable of a boxed type
				__Boxed<double> local1 = (ValueType) this.X;
				objArray[index1] = (object) local1;
				int index2 = 1;
				// ISSUE: variable of a boxed type
				__Boxed<double> local2 = (ValueType) this.Y;
				objArray[index2] = (object) local2;
			
				return "not implemented";
			}

			public string ToString(string format) 
			{
             
				string fmt = "({0}, {1})";
				object[] objArray = new object[2];
				int index1 = 0;
				string str1 = this.X.ToString(format);
				objArray[index1] = (object) str1;
				int index2 = 1;
				string str2 = this.Y.ToString(format);
				objArray[index2] = (object) str2;
			
				return "not implemented";
			}
			*/

			public override int GetHashCode()
			{
				return this.X.GetHashCode() ^ this.Y.GetHashCode() << 2;
			}

			public override bool Equals(object other)
			{
				if (!(other is Vector2d))
					return false;
				Vector2d vector2d = (Vector2d)other;
				if (this.X.Equals(vector2d.X))
					return this.Y.Equals(vector2d.Y);
				else
					return false;
			}

			public static double Dot(Vector2d lhs, Vector2d rhs)
			{
				return lhs.X * rhs.X + lhs.Y * rhs.Y;
			}

			//public static double Angle(Vector2d from, Vector2d to) {
			//    return Math.Acos(Math.Clamp(Vector2d.Dot(from.normalized, to.normalized), -1d, 1d)) * 57.29578d;
			//}

			public double Length()
			{
				return Math.Sqrt((X * X + Y * Y));
			}

			public double LengthSquared()
			{
				return X * X + Y * Y;
			}

			public static double Distance(Vector2d a, Vector2d b)
			{
				return (a - b).Magnitude;
			}

			public static Vector2d ClampMagnitude(Vector2d vector, double maxLength)
			{
				if (vector.SqrMagnitude() > maxLength * maxLength)
					return vector.Normalized * maxLength;
				else
					return vector;
			}

			public double SqrMagnitude()
			{
				return (this.X * this.X + this.Y * this.Y);
			}

			public static double SqrMagnitude(Vector2d a)
			{
				return (a.X * a.X + a.Y * a.Y);
			}

			public static Vector2d Min(Vector2d lhs, Vector2d rhs)
			{
				return new Vector2d(Math.Min(lhs.X, rhs.X), Math.Min(lhs.Y, rhs.Y));
			}

			public static Vector2d Max(Vector2d lhs, Vector2d rhs)
			{
				return new Vector2d(Math.Max(lhs.X, rhs.X), Math.Max(lhs.Y, rhs.Y));
			}
		}

    public struct Vector2I
    {
        public int X;
        public int Y;

        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return this.X;
                    case 1:
                        return this.Y;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2I index!");
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        this.X = value;
                        break;
                    case 1:
                        this.Y = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector2I index!");
                }
            }
        }


        static public implicit operator Microsoft.Xna.Framework.Vector2(Vector2I value)
        {
            return new Vector2((int)value.X, (int)value.Y);
        }

        static public implicit operator Vector2I(Microsoft.Xna.Framework.Vector2 value)
        {
            return new Vector2I((int)value.X, (int)value.Y);
        }


        public Vector2I Normalized
        {
            get
            {
                Vector2I vector2i = new Vector2I(this.X, this.Y);
                vector2i.Normalize();
                return vector2i;
            }
        }

        public int Magnitude
        {
            get
            {
                return (int)Math.Sqrt(this.X * this.X + this.Y * this.Y);
            }
        }

        public static Vector2I Zero
        {
            get
            {
                return new Vector2I(0, 0);
            }
        }

        public static Vector2I One
        {
            get
            {
                return new Vector2I(1, 1);
            }
        }

        public static Vector2I Up
        {
            get
            {
                return new Vector2I(0, 1);
            }
        }

        public static Vector2I Right
        {
            get
            {
                return new Vector2I(1, 0);
            }
        }

        public Vector2I(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        //public static implicit operator Vector2I(Vector3d v) {
        //     return new Vector2d(v.X, v.Y);
        //}

        //public static implicit operator Vector3d(Vector2d v) {
        //     return new Vector3d(v.X, v.Y, 0.0d);
        //}

        public static Vector2I operator +(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2I operator -(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2I operator -(Vector2I a)
        {
            return new Vector2I(-a.X, -a.Y);
        }

        public static Vector2I operator *(Vector2I a, double d)
        {
            return new Vector2I(a.X * (int)d, a.Y * (int)d);
        }

        public static Vector2I operator *(float d, Vector2I a)
        {
            return new Vector2I(a.X * (int)d, a.Y * (int)d);
        }

        public static Vector2I operator *(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.X * b.X, a.Y * b.Y);
        }

        public static Vector2I operator /(Vector2I a, double d)
        {
            return new Vector2I(a.X / (int)d, a.Y / (int)d);
        }

        public static Vector2I operator /(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.X / b.X, a.Y / b.Y);
        }

        public static bool operator ==(Vector2I lhs, Vector2I rhs)
        {
            return lhs == rhs;
            //return Vector2d.SqrMagnitude(lhs - rhs) < 0.0 / 1.0;
        }

        public static bool operator !=(Vector2I lhs, Vector2I rhs)
        {
            return lhs != rhs;
            //return (double)Vector2d.SqrMagnitude(lhs - rhs) >= 0.0 / 1.0;
        }

        public void Set(int new_x, int new_y)
        {
            this.X = new_x;
            this.Y = new_y;
        }

        //May not work
        public static Vector2I Lerp(Vector2I from, Vector2I to, int t)
        {
            //t = Mathd.Clamp01(t);
            return new Vector2I(from.X + (to.X - from.X) * t, from.Y + (to.Y - from.Y) * t);
        }

        public static Vector2I MoveTowards(Vector2I current, Vector2I target, int maxDistanceDelta)
        {
            Vector2I vector2 = target - current;
            double magnitude = vector2.Magnitude;
            if (magnitude <= maxDistanceDelta || magnitude == 0.0d)
                return target;
            else
                return current + vector2 / magnitude * maxDistanceDelta;
        }

        public static Vector2I Scale(Vector2I a, Vector2I b)
        {
            return new Vector2I(a.X * b.X, a.Y * b.Y);
        }

        public void Scale(Vector2I scale)
        {
            this.X *= scale.X;
            this.Y *= scale.Y;
        }

        public void Normalize()
        {
            double magnitude = this.Magnitude;
            if (magnitude > 9.99999974737875E-06)
                this = this / magnitude;
            else
                this = Vector2I.Zero;
        }

        public static Vector2I Transform(Vector2I position, Matrix matrix)
        {
            Transform(ref position, ref matrix, out position);
            return position;
        }

        public static void Transform(ref Vector2I position, ref Matrix matrix, out Vector2I result)
        {
            result = new Vector2I((position.X * (int)matrix.M11) + (position.Y * (int)matrix.M21) + (int)matrix.M41,
                                 (position.X * (int)matrix.M12) + (position.Y * (int)matrix.M22) + (int)matrix.M42);
        }

        public static Vector2I Transform(Vector2I value, Quaternion rotation)
        {
            throw new NotImplementedException();
        }

        public static void Transform(ref Vector2I value, ref Quaternion rotation, out Vector2I result)
        {
            throw new NotImplementedException();
        }

        public static void Transform(Vector2I[] sourceArray, ref Matrix matrix, Vector2I[] destinationArray)
        {
            throw new NotImplementedException();
        }

        public static void Transform(Vector2I[] sourceArray, ref Quaternion rotation, Vector2I[] destinationArray)
        {
            throw new NotImplementedException();
        }

        public static void Transform(Vector2I[] sourceArray, int sourceIndex, ref Matrix matrix, Vector2I[] destinationArray, int destinationIndex, int length)
        {
            throw new NotImplementedException();
        }

        public static void Transform(Vector2I[] sourceArray, int sourceIndex, ref Quaternion rotation, Vector2I[] destinationArray, int destinationIndex, int length)
        {
            throw new NotImplementedException();
        }

        public static Vector2I TransformNormal(Vector2I normal, Matrix matrix)
        {
            Vector2I.TransformNormal(ref normal, ref matrix, out normal);
            return normal;
        }

        public static void TransformNormal(ref Vector2I normal, ref Matrix matrix, out Vector2I result)
        {
            result = new Vector2I((normal.X * (int)matrix.M11) + (normal.Y * (int)matrix.M21),
                                 (normal.X * (int)matrix.M12) + (normal.Y * (int)matrix.M22));
        }

        public static void TransformNormal(Vector2I[] sourceArray, ref Matrix matrix, Vector2I[] destinationArray)
        {
            throw new NotImplementedException();
        }

        public static void TransformNormal(Vector2I[] sourceArray, int sourceIndex, ref Matrix matrix, Vector2I[] destinationArray, int destinationIndex, int length)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(24);
            sb.Append("{X:");
            sb.Append(this.X);
            sb.Append(" Y:");
            sb.Append(this.Y);
            sb.Append("}");
            return sb.ToString();
        }

        /* public override string ToString() 
        {
            string fmt = "({0:D1}, {1:D1})";
            object[] objArray = new object[2];
            int index1 = 0;
            // ISSUE: variable of a boxed type
            __Boxed<double> local1 = (ValueType) this.X;
            objArray[index1] = (object) local1;
            int index2 = 1;
            // ISSUE: variable of a boxed type
            __Boxed<double> local2 = (ValueType) this.Y;
            objArray[index2] = (object) local2;
			
            return "not implemented";
        }

        public string ToString(string format) 
        {
             
            string fmt = "({0}, {1})";
            object[] objArray = new object[2];
            int index1 = 0;
            string str1 = this.X.ToString(format);
            objArray[index1] = (object) str1;
            int index2 = 1;
            string str2 = this.Y.ToString(format);
            objArray[index2] = (object) str2;
			
            return "not implemented";
        }
        */

        public override int GetHashCode()
        {
            return this.X.GetHashCode() ^ this.Y.GetHashCode() << 2;
        }

        public override bool Equals(object other)
        {
            if (!(other is Vector2I))
                return false;
            Vector2I vector2i = (Vector2I)other;
            if (this.X.Equals(vector2i.X))
                return this.Y.Equals(vector2i.Y);
            else
                return false;
        }

        public static int Dot(Vector2I lhs, Vector2I rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y;
        }

        //public static double Angle(Vector2d from, Vector2d to) {
        //    return Math.Acos(Math.Clamp(Vector2d.Dot(from.normalized, to.normalized), -1d, 1d)) * 57.29578d;
        //}

        public int Length()
        {
            return (int)Math.Sqrt((X * X + Y * Y));
        }

        public int LengthSquared()
        {
            return X * X + Y * Y;
        }

        public static int Distance(Vector2I a, Vector2I b)
        {
            return (a - b).Magnitude;
        }

        public static Vector2I ClampMagnitude(Vector2I vector, double maxLength)
        {
            if (vector.SqrMagnitude() > maxLength * maxLength)
                return vector.Normalized * maxLength;
            else
                return vector;
        }

        public int SqrMagnitude()
        {
            return (this.X * this.X + this.Y * this.Y);
        }

        public static int SqrMagnitude(Vector2I a)
        {
            return (a.X * a.X + a.Y * a.Y);
        }

        public static Vector2I Min(Vector2I lhs, Vector2I rhs)
        {
            return new Vector2I(Math.Min(lhs.X, rhs.X), Math.Min(lhs.Y, rhs.Y));
        }

        public static Vector2I Max(Vector2I lhs, Vector2I rhs)
        {
            return new Vector2I(Math.Max(lhs.X, rhs.X), Math.Max(lhs.Y, rhs.Y));
        }
    }

	public struct Vector3d
		{
			public const float kEpsilon = 1E-05f;
			public double X;
			public double Y;
			public double Z;

			public double this[int index]
			{
				get
				{
					switch (index)
					{
						case 0:
							return this.X;
						case 1:
							return this.Y;
						case 2:
							return this.Z;
						default:
							throw new IndexOutOfRangeException("Invalid index!");
					}
				}
				set
				{
					switch (index)
					{
						case 0:
							this.X = value;
							break;
						case 1:
							this.Y = value;
							break;
						case 2:
							this.Z = value;
							break;
						default:
							throw new IndexOutOfRangeException("Invalid Vector3d index!");
					}
				}
			}

			public Vector3d Normalized
			{
				get
				{
					return Vector3d.Normalize(this);
				}
			}


			public double magnitude
			{
				get
				{
					return Math.Sqrt(this.X * this.X + this.Y * this.Y + this.Z * this.Z);
				}
			}

			public double sqrMagnitude
			{
				get
				{
					return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
				}
			}


			public static Vector3d Zero
			{
				get
				{
					return new Vector3d(0d, 0d, 0d);
				}
			}

			public static Vector3d One
			{
				get
				{
					return new Vector3d(1d, 1d, 1d);
				}
			}

			public static Vector3d Forward
			{
				get
				{
					return new Vector3d(0d, 0d, 1d);
				}
			}

			public static Vector3d Back
			{
				get
				{
					return new Vector3d(0d, 0d, -1d);
				}
			}

			public static Vector3d Up
			{
				get
				{
					return new Vector3d(0d, 1d, 0d);
				}
			}

			public static Vector3d Down
			{
				get
				{
					return new Vector3d(0d, -1d, 0d);
				}
			}

			public static Vector3d Left
			{
				get
				{
					return new Vector3d(-1d, 0d, 0d);
				}
			}

			public static Vector3d Right
			{
				get
				{
					return new Vector3d(1d, 0d, 0d);
				}
			}

			[Obsolete("Use Vector3d.forward instead.")]
			public static Vector3d fwd
			{
				get
				{
					return new Vector3d(0d, 0d, 1d);
				}
			}

			public Vector3d(double X, double Y, double Z)
			{
				this.X = X;
				this.Y = Y;
				this.Z = Z;
			}

			public Vector3d(float X, float Y, float Z)
			{
				this.X = (double)X;
				this.Y = (double)Y;
				this.Z = (double)Z;
			}

			public Vector3d(Vector3 v3)
			{
				this.X = (double)v3.X;
				this.Y = (double)v3.Y;
				this.Z = (double)v3.Z;
			}

			public Vector3d(double X, double Y)
			{
				this.X = X;
				this.Y = Y;
				this.Z = 0d;
			}

			public static Vector3d operator +(Vector3d a, Vector3d b)
			{
				return new Vector3d(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
			}

			public static Vector3d operator -(Vector3d a, Vector3d b)
			{
				return new Vector3d(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
			}

			public static Vector3d operator -(Vector3d a)
			{
				return new Vector3d(-a.X, -a.Y, -a.Z);
			}

			public static Vector3d operator *(Vector3d a, double d)
			{
				return new Vector3d(a.X * d, a.Y * d, a.Z * d);
			}

			public static Vector3d operator *(double d, Vector3d a)
			{
				return new Vector3d(a.X * d, a.Y * d, a.Z * d);
			}

			public static Vector3d operator /(Vector3d a, double d)
			{
				return new Vector3d(a.X / d, a.Y / d, a.Z / d);
			}

			public static bool operator ==(Vector3d lhs, Vector3d rhs)
			{
				return (double)Vector3d.SqrMagnitude(lhs - rhs) < 0.0 / 1.0;
			}

			public static bool operator !=(Vector3d lhs, Vector3d rhs)
			{
				return (double)Vector3d.SqrMagnitude(lhs - rhs) >= 0.0 / 1.0;
			}

			public static explicit operator Vector3(Vector3d vector3d)
			{
				return new Vector3((float)vector3d.X, (float)vector3d.Y, (float)vector3d.Z);
			}

			//May not work
			public static Vector3d Lerp(Vector3d from, Vector3d to, double t)
			{
				//t = Math.Clamp01(t);
				return new Vector3d(from.X + (to.X - from.X) * t, from.Y + (to.Y - from.Y) * t, from.Z + (to.Z - from.Z) * t);
			}

			//public static Vector3d Slerp(Vector3d from, Vector3d to, double t) {
			//    Vector3 v3 = Vector3.Slerp((Vector3)from, (Vector3)to, (float)t);
			//    return new Vector3d(v3);
			//}

			public static void OrthoNormalize(ref Vector3d normal, ref Vector3d tangent)
			{
				//Vector3 v3normal = new Vector3();
				//Vector3 v3tangent = new Vector3();
				//v3normal = (Vector3)normal;
				//v3tangent = (Vector3)tangent;
				//Vector3.OrthoNormalize(ref v3normal, ref v3tangent);
				//normal = new Vector3d(v3normal);
				//tangent = new Vector3d(v3tangent);
				throw new NotImplementedException();
			}

			public static void OrthoNormalize(ref Vector3d normal, ref Vector3d tangent, ref Vector3d binormal)
			{
				//Vector3 v3normal = new Vector3();
				//Vector3 v3tangent = new Vector3();
				//Vector3 v3binormal = new Vector3();
				//v3normal = (Vector3)normal;
				//v3tangent = (Vector3)tangent;
				//v3binormal = (Vector3)binormal;
				//Vector3.OrthoNormalize(ref v3normal, ref v3tangent, ref v3binormal);
				//normal = new Vector3d(v3normal);
				//tangent = new Vector3d(v3tangent);
				//binormal = new Vector3d(v3binormal);
				throw new NotImplementedException();
			}

			public static Vector3d MoveTowards(Vector3d current, Vector3d target, double maxDistanceDelta)
			{
				Vector3d vector3 = target - current;
				double magnitude = vector3.magnitude;
				if (magnitude <= maxDistanceDelta || magnitude == 0.0d)
					return target;
				else
					return current + vector3 / magnitude * maxDistanceDelta;
			}

			//public static Vector3d RotateTowards(Vector3d current, Vector3d target, double maxRadiansDelta, double maxMagnitudeDelta) {
			//    Vector3 v3 = Vector3.RotateTowards((Vector3)current, (Vector3)target, (float)maxRadiansDelta, (float)maxMagnitudeDelta);
			//    return new Vector3d(v3);
			//}

			//public static Vector3d SmoothDamp(Vector3d current, Vector3d target, ref Vector3d currentVelocity, double smoothTime, double maxSpeed) {
			//    double deltaTime = (double)Time.deltaTime;
			//    return Vector3d.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
			//}

			//public static Vector3d SmoothDamp(Vector3d current, Vector3d target, ref Vector3d currentVelocity, double smoothTime) {
			//    double deltaTime = (double)Time.deltaTime;
			//    double maxSpeed = double.PositiveInfinity;
			//    return Vector3d.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
			//}

			public static Vector3d SmoothDamp(Vector3d current, Vector3d target, ref Vector3d currentVelocity, double smoothTime, double maxSpeed, double deltaTime)
			{
				smoothTime = Math.Max(0.0001d, smoothTime);
				double num1 = 2d / smoothTime;
				double num2 = num1 * deltaTime;
				double num3 = (1.0d / (1.0d + num2 + 0.479999989271164d * num2 * num2 + 0.234999999403954d * num2 * num2 * num2));
				Vector3d vector = current - target;
				Vector3d vector3_1 = target;
				double maxLength = maxSpeed * smoothTime;
				Vector3d vector3_2 = Vector3d.ClampMagnitude(vector, maxLength);
				target = current - vector3_2;
				Vector3d vector3_3 = (currentVelocity + num1 * vector3_2) * deltaTime;
				currentVelocity = (currentVelocity - num1 * vector3_3) * num3;
				Vector3d vector3_4 = target + (vector3_2 + vector3_3) * num3;
				if (Vector3d.Dot(vector3_1 - current, vector3_4 - vector3_1) > 0.0)
				{
					vector3_4 = vector3_1;
					currentVelocity = (vector3_4 - vector3_1) / deltaTime;
				}
				return vector3_4;
			}

			public void Set(double new_x, double new_y, double new_z)
			{
				this.X = new_x;
				this.Y = new_y;
				this.Z = new_z;
			}

			public static Vector3d Scale(Vector3d a, Vector3d b)
			{
				return new Vector3d(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
			}

			public void Scale(Vector3d scale)
			{
				this.X *= scale.X;
				this.Y *= scale.Y;
				this.Z *= scale.Z;
			}

			public static Vector3d Cross(Vector3d lhs, Vector3d rhs)
			{
				return new Vector3d(lhs.Y * rhs.Z - lhs.Z * rhs.Y, lhs.Z * rhs.X - lhs.X * rhs.Z, lhs.X * rhs.Y - lhs.Y * rhs.X);
			}

			public override int GetHashCode()
			{
				return this.X.GetHashCode() ^ this.Y.GetHashCode() << 2 ^ this.Z.GetHashCode() >> 2;
			}

			public override bool Equals(object other)
			{
				if (!(other is Vector3d))
					return false;
				Vector3d vector3d = (Vector3d)other;
				if (this.X.Equals(vector3d.X) && this.Y.Equals(vector3d.Y))
					return this.Z.Equals(vector3d.Z);
				else
					return false;
			}

			public static Vector3d Reflect(Vector3d inDirection, Vector3d inNormal)
			{
				return -2d * Vector3d.Dot(inNormal, inDirection) * inNormal + inDirection;
			}

			public static Vector3d Normalize(Vector3d value)
			{
				double num = Vector3d.Magnitude(value);
				if (num > 9.99999974737875E-06)
					return value / num;
				else
					return Vector3d.Zero;
			}

			public void Normalize()
			{
				double num = Vector3d.Magnitude(this);
				if (num > 9.99999974737875E-06)
					this = this / num;
				else
					this = Vector3d.Zero;
			}
			// TODO : fix formatting
			public override string ToString()
			{
				return "(" + this.X + " - " + this.Y + " - " + this.Z + ")";
			}

			public static double Dot(Vector3d lhs, Vector3d rhs)
			{
				return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
			}

			public static Vector3d Project(Vector3d vector, Vector3d onNormal)
			{
				double num = Vector3d.Dot(onNormal, onNormal);
				if (num < 1.40129846432482E-45d)
					return Vector3d.Zero;
				else
					return onNormal * Vector3d.Dot(vector, onNormal) / num;
			}

			public static Vector3d Exclude(Vector3d excludeThis, Vector3d fromThat)
			{
				return fromThat - Vector3d.Project(fromThat, excludeThis);
			}

			public static double Angle(Vector3d from, Vector3d to)
			{
				return Math.Acos(MathHelper.Clamp((float)Vector3d.Dot(from.Normalized, to.Normalized), -1f, 1f)) * 57.29578d;
			}

			public static double Distance(Vector3d a, Vector3d b)
			{
				Vector3d vector3d = new Vector3d(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
				return Math.Sqrt(vector3d.X * vector3d.X + vector3d.Y * vector3d.Y + vector3d.Z * vector3d.Z);
			}

			public static Vector3d ClampMagnitude(Vector3d vector, double maxLength)
			{
				if (vector.sqrMagnitude > maxLength * maxLength)
					return vector.Normalized * maxLength;
				else
					return vector;
			}

			public static double Magnitude(Vector3d a)
			{
				return Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z);
			}

			public static double SqrMagnitude(Vector3d a)
			{
				return a.X * a.X + a.Y * a.Y + a.Z * a.Z;
			}

			public static Vector3d Min(Vector3d lhs, Vector3d rhs)
			{
				return new Vector3d(Math.Min(lhs.X, rhs.X), Math.Min(lhs.Y, rhs.Y), Math.Min(lhs.Z, rhs.Z));
			}

			public static Vector3d Max(Vector3d lhs, Vector3d rhs)
			{
				return new Vector3d(Math.Max(lhs.X, rhs.X), Math.Max(lhs.Y, rhs.Y), Math.Max(lhs.Z, rhs.Z));
			}

			//[Obsolete("Use Vector3d.Angle instead. AngleBetween uses radians instead of degrees and was deprecated for this reason")]
			//public static double AngleBetween(Vector3d from, Vector3d to) {
			//    return Math.Acos(Math.Clamp(Vector3d.Dot(from.normalized, to.normalized), -1d, 1d));
			//}
		}
}



