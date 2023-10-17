using System;
using DynVis.Math;

namespace DynVis.Core.Surface
{
    [Serializable]
    abstract public partial class BaseSurface3D : ISurface3D
    {
       
        public double E(double q1, double q2)
        {
            return GetCashValue(q1, q2, ValueType.E, GetE);
        }
        public double dEdq1(double q1, double q2)
        {
            return GetCashValue(q1, q2, ValueType.dEdq1, GetdEdq1);
        }
        public double dEdq2(double q1, double q2)
        {
            return GetCashValue(q1, q2, ValueType.dEdq2, GetdEdq2);
        }
        public double d2Edq1dq1(double q1, double q2)
        {
            return GetCashValue(q1, q2, ValueType.d2Edq1dq1, Getd2Edq1dq1);
        }
        public double d2Edq2dq2(double q1, double q2)
        {
            return GetCashValue(q1, q2, ValueType.d2Edq2dq2, Getd2Edq2dq2);
        }
        public double d2Edq1dq2(double q1, double q2)
        {
            return GetCashValue(q1, q2, ValueType.d2Edq1dq2, Getd2Edq1dq2);
        }
        public double d2Edq2dq1(double q1, double q2)
        {
            return GetCashValue(q1, q2, ValueType.d2Edq2dq1, Getd2Edq2dq1);
        }


        protected abstract double GetE(double q1, double q2);
        protected abstract double GetdEdq1(double q1, double q2);
        protected abstract double GetdEdq2(double q1, double q2);
        protected abstract double Getd2Edq1dq1(double q1, double q2);
        protected abstract double Getd2Edq2dq2(double q1, double q2);
        protected abstract double Getd2Edq1dq2(double q1, double q2);
        protected virtual double Getd2Edq2dq1(double q1, double q2)
        { return Getd2Edq1dq2(q1, q2); }

        protected void ClearCash()
        {
            Cash = new CashedValue[ValueTypeCount];
        }

        protected double GetCashValue(double q1, double q2, ValueType valueType, Surface3DPoint CalcValue)
        {
            bool inversDerive1, inversDerive2;
            if (IsValidCoord(ref q1, out inversDerive1, ref q2, out inversDerive2))
            {
                var cashedValue = Cash[(int) valueType] ?? new CashedValue(q1 - 1, q2, 0);
                if (cashedValue.P.Equals(q1, q2))
                {
                    return cashedValue.Value;
                }
                var Result = CalcValue(q1, q2);
                cashedValue.Set(q1, q2, Result);

                if ((inversDerive1 &&
                     (valueType == ValueType.dEdq1 ||
                      valueType == ValueType.d2Edq2dq1 ||
                      valueType == ValueType.d2Edq1dq2)) ||
                    (inversDerive2 &&
                     (valueType == ValueType.dEdq2 ||
                      valueType == ValueType.d2Edq2dq1 ||
                      valueType == ValueType.d2Edq1dq2))
                    )
                {
                    Result = -Result;
                }


                return Result;
            }
            return 0;    
        }
        protected void SetCashValue(double q1, double q2, ValueType valueType, double value)
        {
            var cashedValue = Cash[(int)valueType] ?? new CashedValue();
            cashedValue.Set(q1, q2, value);
        }

        class CashedValue
        {
            public readonly PointD P = new PointD();
            public double Value;

            public CashedValue() { }
            public CashedValue(double q1, double q2, double v)
            {
                Set(q1, q2, v);
            }

            public void Set(double q1, double q2, double v)
            {
                P.X = q1;
                P.Y = q2;
                Value = v;
            }
        }

        private const int ValueTypeCount = 7;
        protected enum ValueType { E = 0, dEdq1, dEdq2, d2Edq1dq1, d2Edq2dq2, d2Edq1dq2, d2Edq2dq1 }

        private CashedValue[] Cash = new CashedValue[ValueTypeCount];



        public double E(IPointD p)
        {
            return E(p.X, p.Y);
        }
        public double dEdq1(IPointD p)
        {
            return dEdq1(p.X, p.Y);
        }
        public double dEdq2(IPointD p)
        {
            return dEdq2(p.X, p.Y);
        }
        public double d2Edq1dq1(IPointD p)
        {
            return d2Edq1dq1(p.X, p.Y);
        }
        public double d2Edq2dq2(IPointD p)
        {
            return d2Edq2dq2(p.X, p.Y);
        }
        public double d2Edq1dq2(IPointD p)
        {
            return d2Edq1dq2(p.X, p.Y);
        }
        public double d2Edq2dq1(IPointD p)
        {
            return d2Edq2dq1(p.X, p.Y);
        }

        protected Point3D _MaxE = new Point3D();
        protected Point3D _MinE = new Point3D();
        public virtual IPointValue MaxE
        {
            get { return _MaxE; }
        }
        public virtual IPointValue MinE
        {
            get { return _MinE; }
        }

        public abstract IAxes Axes1 { get; }
        public abstract IAxes Axes2 { get; }

        public abstract ScaleDimension EnergyDimension
        { get;
        }

        private readonly PointD _CurrentPoint = new PointD();
        public IPointD CurrentPoint
        {
            get
            {
                return _CurrentPoint;
            }
        }

        public bool SetPoint(IPointD p)
        {
            if (p != null)
            {
                return SetPoint(p.X, p.Y);
            }
            return false;
        }

        public bool SetPoint(double q1, double q2)
        {
            if (IsValidCoord(ref q1, ref q2))
            {
                if (!PointsFunc.Equals(CurrentPoint, q1, q2))
                {
                    _CurrentPoint.SetValues(q1, q2);
                    RiseCurrentPointChanged();
                }
                return true;
            }
            return false;
        }

        public double CurrentE { get { return E(CurrentPoint); } }
        public double CurrentdEdq1 { get { return dEdq1(CurrentPoint); } }
        public double CurrentdEdq2 { get { return dEdq2(CurrentPoint); } }
        public double Currentd2Edq1dq1 { get { return d2Edq1dq1(CurrentPoint); } }
        public double Currentd2Edq2dq2 { get { return d2Edq2dq2(CurrentPoint); } }
        public double Currentd2Edq1dq2 { get { return d2Edq1dq2(CurrentPoint); } }
        public double Currentd2Edq2dq1 { get { return d2Edq2dq1(CurrentPoint); } }


        private static bool IsValidCoord(ref double q, out bool inverseDerive, IAxes axes)
        {
            inverseDerive = false;

            if ((axes.TypeMaxValue == CoordinateType.Ended))
            {
                if (q > axes.MaxValue) return false;
            } 
            if  (axes.TypeMinValue == CoordinateType.Ended)
            {
                if (q < axes.MinValue) return false;
            }

            while ((q < axes.MinValue) || (q > axes.MaxValue))
            {
                if (axes.TypeMinValue == CoordinateType.Mirrored && q < axes.MinValue)
                {
                    q = 2 * axes.MinValue - q;
                    inverseDerive = !inverseDerive;
                }
                if (axes.TypeMaxValue == CoordinateType.Mirrored && q > axes.MaxValue)
                {
                    q = 2 * axes.MaxValue - q;
                    inverseDerive = !inverseDerive;
                }
                if (axes.TypeMinValue == CoordinateType.Periodic && q < axes.MinValue)
                {
                    q = axes.MaxValue - (axes.MinValue - q);
                }
                if (axes.TypeMaxValue == CoordinateType.Periodic && q > axes.MaxValue)
                {
                    q = axes.MinValue + (q - axes.MaxValue);
                }
            }
            
            return true;
        }

        private static bool IsValidCoord(ref double q, IAxes axes)
        {
            bool inverseDerive;
            return IsValidCoord(ref q, out inverseDerive, axes);
        }

        public bool IsValidCoord(double q1, double q2)
        {
            return IsValidCoord(ref q1, ref q2);
        }
        public bool IsValidCoord(ref double q1, ref double q2)
        {
            return IsValidCoord(ref q1, Axes1) && IsValidCoord(ref q2, Axes2);
        }
        public bool IsValidCoord(ref double q1, out bool inversDerive1, ref double q2, out bool inversDerive2)
        {
            inversDerive2 = false;
            return IsValidCoord(ref q1, out inversDerive1, Axes1) && IsValidCoord(ref q2, out inversDerive2, Axes2);
        }

        public bool IsValidCoord(IPointD p)
        {
            return IsValidCoord(p.X, p.Y);
        }

        public event EventHandler CurrentPointChanged;

        private void RiseCurrentPointChanged()
        {
            if (CurrentPointChanged != null) CurrentPointChanged(this, new EventArgs());
        }
    }
}
