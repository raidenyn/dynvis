using System;
using System.Drawing;

namespace DynVis.Math
{
    public interface IPointValue
    {
        PointD Point { get; }
        double Value { get; }
    }

    public interface IPointD
    {
        double X { get; }
        double Y { get; }
    }

    public interface IChangedPointD : IPointD
    {
        new double X { get; set; }
        new double Y { get; set; }

        void SetValues(double x, double y);
        void SetValues(IPointD p);
    }

    public interface IPoint3D : IPointD
    {
        double Z { get; }
    }

    public interface IChangedPoint3D : IChangedPointD, IPoint3D
    {
        void SetValues(double x, double y, double z);
        void SetValues(IPoint3D p);
    }

    [Serializable]
    public class PointD : IChangedPointD, ICloneable, IEquatable<PointD>, IEquatable<PointF>, IEquatable<Point>, IFormattable
    {
        public double X { get; set; }
        public double Y { get; set; }

        public PointD() { }
        public PointD(double x, double y)
        {
            X = x;
            Y = y;
        }
        public PointD(IPointD p) : this(p.X, p.Y) { }

        public object Clone()
        {
            return new PointD(this);
        }

        public void SetValues(double x, double y)
        {
            X = x;
            Y = y;
        }
        public void SetValues(IPointD p)
        {
            SetValues(p.X, p.Y);
        }

        public bool Equals(PointD other)
        {
            return other.X == X && other.Y == Y;
        }
        public bool Equals(PointF other)
        {
            return other.X == X && other.Y == Y;
        }
        public bool Equals(Point other)
        {
            return other.X == X && other.Y == Y;
        }
        public bool Equals(double x, double y)
        {
            return x == X && y == Y;
        }

        public override string ToString()
        {
            return String.Format("({0}, {1})", X, Y);
        }

        public static PointD operator +(PointD p1, IPointD p2)
        {
            var p = new PointD(p1);
            p.X += p2.X;
            p.Y += p2.Y;
            return p;
        }
        public static PointD operator *(PointD p1, IPointD p2)
        {
            var p = new PointD(p1);
            p.X *= p2.X;
            p.Y *= p2.Y;
            return p;
        }
        public static PointD operator /(PointD p1, IPointD p2)
        {
            var p = new PointD(p1);
            p.X /= p2.X;
            p.Y /= p2.Y;
            return p;
        }
        public static PointD operator -(PointD p1, IPointD p2)
        {
            var p = new PointD(p1);
            p.X -= p2.X;
            p.Y -= p2.Y;
            return p;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return String.Format("({0}, {1})", X.ToString(format, formatProvider), Y.ToString(format, formatProvider));
        }
    }

    [Serializable]
    public class Point3D : PointD, IChangedPoint3D, IPointValue, ICloneable, IEquatable<Point3D>, IEquatable<PointD>, IEquatable<PointF>, IEquatable<Point>, IFormattable
    {
        public double Z { get; set; }

        public Point3D() { }
        public Point3D(double x, double y, double z)
        {
            SetValues(x, y, z);
        }
        public Point3D(IPointD p) : this(p.X, p.Y, 0) { }
        public Point3D(IPoint3D p) : this(p.X, p.Y, p.Z) { }

        public new object Clone()
        {
            return new Point3D(this);
        }


        public new bool Equals(PointD other)
        {
            return other.X == X && other.Y == Y && Z == 0;
        }
        public bool Equals(Point3D other)
        {
            return other.X == X && other.Y == Y && other.Z == Z;
        }
        public new bool Equals(PointF other)
        {
            return other.X == X && other.Y == Y && Z == 0;
        }
        public new bool Equals(Point other)
        {
            return other.X == X && other.Y == Y && Z == 0;
        }
        public bool Equals(double x, double y, double z)
        {
            return x == X && y == Y && z == Z;
        }

        public override string ToString()
        {
            return String.Format("{0,16:F9} {1,16:F9} {2,16:F9}", X, Y, Z);
        }

        public new string ToString(string format, IFormatProvider formatProvider)
        {
            return ToString();// String.Format("{0} {1} {2}", X.ToString(format, formatProvider), Y.ToString(format, formatProvider), Z.ToString(format, formatProvider));
        }

        public void SetValues(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public void SetValues(IPoint3D p)
        {
            SetValues(p.X, p.Y, p.Z);
        }

        public void SetNullValues()
        {
            SetValues(0, 0, 0);
        }

        public PointD Point
        {
            get { return this; }
            set { X = value.X; Y = value.Y; }
        }
        public double Value
        {
            get { return Z; }
            set
            {
                Z = value;
            }
        }



        public void SumTo(double d)
        {
            X += d;
            Y += d;
            Z += d;
        }
        public void SubTo(double d)
        {
            X -= d;
            Y -= d;
            Z -= d;
        }
        public void SubFrom(double d)
        {
            X = d - X;
            Y = d - Y;
            Z = d - Z;
        }
        public void MultTo(double d)
        {
            X *= d;
            Y *= d;
            Z *= d;
        }
        public void DerTo(double d)
        {
            X /= d;
            Y /= d;
            Z /= d;
        }
        public void DerFrom(double d)
        {
            X = d / X;
            Y = d / Y;
            Z = d / Z;
        }

        public void SumTo(IPoint3D p)
        {
            X += p.X;
            Y += p.Y;
            Z += p.Z;
        }
        public void SubTo(IPoint3D p)
        {
            X -= p.X;
            Y -= p.Y;
            Z -= p.Z;
        }
        public void SubFrom(IPoint3D p)
        {
            X = p.X - X;
            Y = p.Y - Y;
            Z = p.Z - Z;
        }
        public void MultTo(IPoint3D p)
        {
            X *= p.X;
            Y *= p.Y;
            Z *= p.Z;
        }
        public void DerTo(IPoint3D p)
        {
            X /= p.X;
            Y /= p.Y;
            Z /= p.Z;
        }
        public void DerFrom(IPoint3D p)
        {
            X = p.X / X;
            Y = p.Y / Y;
            Z = p.Z / Z;
        } 

        public static Point3D operator +(Point3D p1, IPoint3D p2)
        {
            var p = new Point3D(p1);
            p.X += p2.X;
            p.Y += p2.Y;
            p.Z += p2.Z;
            return p;
        }
        public static Point3D operator *(Point3D p1, IPoint3D p2)
        {
            var p = new Point3D(p1);
            p.X *= p2.X;
            p.Y *= p2.Y;
            p.Z *= p2.Z;
            return p;
        }
        public static Point3D operator /(Point3D p1, IPoint3D p2)
        {
            var p = new Point3D(p1);
            p.X /= p2.X;
            p.Y /= p2.Y;
            p.Z /= p2.Z;
            return p;
        }
        public static Point3D operator -(Point3D p1, IPoint3D p2)
        {
            var p = new Point3D(p1);
            p.X -= p2.X;
            p.Y -= p2.Y;
            p.Z -= p2.Z;
            return p;
        }
        public static Point3D operator -(IPoint3D p1, Point3D p2)
        {
            var p = new Point3D(p1);
            p.X -= p2.X;
            p.Y -= p2.Y;
            p.Z -= p2.Z;
            return p;
        }
        public static Point3D operator -(Point3D p1)
        {
            var p = new Point3D {X = (-p1.X), Y = (-p1.Y), Z = (-p1.Z)};
            return p;
        }

        public static Point3D operator +(double d, Point3D p)
        {
            var result = new Point3D(p);
            result.SumTo(d);
            return result;
        }
        public static Point3D operator +(Point3D p, double d)
        {
            return d + p;
        }
        public static Point3D operator *(double d, Point3D p)
        {
            var result = new Point3D(p);
            result.X *= d;
            result.Y *= d;
            result.Z *= d;
            return result;
        }
        public static Point3D operator *(Point3D p, double d)
        {
            return d * p;
        }
        public static Point3D operator /(double d, Point3D p)
        {
            var result = new Point3D(p);
            result.X = d / result.X;
            result.Y = d / result.Y;
            result.Z = d / result.Z;
            return result;
        }
        public static Point3D operator /(Point3D p, double d)
        {
            var result = new Point3D(p);
            result.X /= d;
            result.Y /= d;
            result.Z /= d;
            return result;
        }
        public static Point3D operator -(double d, Point3D p)
        {
            var result = new Point3D(p);
            result.X = d - result.X;
            result.Y = d - result.Y;
            result.Z = d - result.Z;
            return result;
        }
        public static Point3D operator -(Point3D p, double d)
        {
            var result = new Point3D(p);
            result.X -= d;
            result.Y -= d;
            result.Z -= d;
            return result;
        }

        

        public static readonly IPoint3D NullPoint = new Point3D();
    }

    [Serializable]
    public class CalculatingValuePoint: IPointValue, ICloneable
    {
        public CalculatingValuePoint()
        {
            Point = new PointD();
        }

        public CalculatingValuePoint(PointD point, Surface3DPointD getPointValue)
        {
            Point = point;
            GetPointValue = getPointValue;
        }

        public CalculatingValuePoint(PointD point)
            : this(point, null)
        {

        }
        
        public PointD Point
        { get; set;
        }

        public double Value
        {
            get { return GetPointValue(Point); }
        }

        public Surface3DPointD GetPointValue;

        public object Clone()
        {
            return new CalculatingValuePoint(Point, GetPointValue);
        }
    }

    public delegate double Surface3DPoint(double q1, double q2);
    public delegate double Surface3DPointD(PointD p);
    public delegate void SetValuePointD(PointD p, double d);
}
