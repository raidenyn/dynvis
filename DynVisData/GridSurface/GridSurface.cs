using DynVis.Core;
using DynVis.Core.Surface;
using DynVis.Math;

namespace DynVis.Data.GridSurface
{
    internal partial class GridSurface: BaseSurface3D
    {
        private const int DimQ1 = 0;
        private const int DimQ2 = 1;

        public bool DirectFirstDerivites = true;

        public GridSurface(double[] q1, double[] q2, double[,] e, IAxes axesQ1, IAxes axesQ2)
        {
            if (q1.Length != e.GetLength(DimQ1) || q2.Length != e.GetLength(DimQ2))
            {
                throw new ArraySizeException();
            }

            Energy = (double[,])e.Clone();
            Q1 = (double[])q1.Clone();
            Q2 = (double[])q2.Clone();

            BuildSplineCoeff(Energy, out C);

            BuildDerivationsCoeff(out CDerivationQ1, out CDerivationQ2);

            FindMinMaxE();

            AxesQ1 = new Axes(axesQ1, GetQ1MinValue, GetQ1MaxValue);
            AxesQ2 = new Axes(axesQ2, GetQ2MinValue, GetQ2MaxValue);
        }

        public GridSurface(double[] q1, double[] q2, double[,] e, double[,] dEdQ1, double[,] dEdQ2, IAxes axesQ1, IAxes axesQ2)
        {
            if (q1.Length != e.GetLength(DimQ1) || q2.Length != e.GetLength(DimQ2) ||
                q1.Length != dEdQ1.GetLength(DimQ1) || q2.Length != dEdQ1.GetLength(DimQ2) ||
                q1.Length != dEdQ2.GetLength(DimQ1) || q2.Length != dEdQ2.GetLength(DimQ2))
            {
                throw new ArraySizeException();
            }

            Energy = (double[,])e.Clone();
            Q1 = (double[])q1.Clone();
            Q2 = (double[])q2.Clone();

            BuildSplineCoeff(Energy, out C);

            BuildDerivationsCoeff(out CDerivationQ1, out CDerivationQ2);

            BuildSplineCoeff(dEdQ1, out CDerivationQ1);
            BuildSplineCoeff(dEdQ2, out CDerivationQ2);

            FindMinMaxE();

            AxesQ1 = new Axes(axesQ1, GetQ1MinValue, GetQ1MaxValue);
            AxesQ2 = new Axes(axesQ2, GetQ2MinValue, GetQ2MaxValue);
        }

        private double GetQ1MaxValue()
        {
            return Q1[Q1.Length - 1];
        }
        private double GetQ2MaxValue()
        {
            return Q2[Q2.Length - 1];
        }
        private double GetQ1MinValue()
        {
            return Q1[0];
        }
        private double GetQ2MinValue()
        {
            return Q2[0];
        }

        protected override double GetE(double q1, double q2)
        {
            double E, dEdq1, dEdq2, d2Edq1dq2;
            spline2d.splinedifferentiation2d(C, q2, q1, out E, out dEdq2, out dEdq1, out d2Edq1dq2);
            SetCashValue(q1, q2, ValueType.dEdq1, dEdq1);
            SetCashValue(q1, q2, ValueType.dEdq2, dEdq2);
            SetCashValue(q1, q2, ValueType.d2Edq1dq2, d2Edq1dq2);
            return E;
        }
        protected override double GetdEdq1(double q1, double q2)
        {
            if (CDerivationQ1 == null || DirectFirstDerivites)
            {
                double E, dEdq1, dEdq2, d2Edq1dq2;
                spline2d.splinedifferentiation2d(C, q2, q1, out E, out dEdq2, out dEdq1, out d2Edq1dq2);
                SetCashValue(q1, q2, ValueType.E, E);
                SetCashValue(q1, q2, ValueType.dEdq2, dEdq2);
                SetCashValue(q1, q2, ValueType.d2Edq1dq2, d2Edq1dq2);
                return dEdq1;
            } 
            else
            {
                double dEdq1, d2Edq1dq1, d2Edq1dq2, d3Edq1dq1dq2;
                spline2d.splinedifferentiation2d(CDerivationQ1, q2, q1, out dEdq1, out d2Edq1dq2, out d2Edq1dq1, out d3Edq1dq1dq2);

                SetCashValue(q1, q2, ValueType.dEdq1, dEdq1);
                SetCashValue(q1, q2, ValueType.d2Edq1dq1, d2Edq1dq1);
                return dEdq1;
            }

             


        }
        protected override double GetdEdq2(double q1, double q2)
        {
            if (CDerivationQ1 == null || DirectFirstDerivites)
            {
                double E, dEdq1, dEdq2, d2Edq1dq2;
                spline2d.splinedifferentiation2d(C, q2, q1, out E, out dEdq2, out dEdq1, out d2Edq1dq2);
                SetCashValue(q1, q2, ValueType.E, E);
                SetCashValue(q1, q2, ValueType.dEdq1, dEdq1);
                SetCashValue(q1, q2, ValueType.d2Edq1dq2, d2Edq1dq2);
                return dEdq2;

            }
            else
            {

                double dEdq2, d2Edq2dq1, d2Edq2dq2, d3Edq2dq1dq2;
                spline2d.splinedifferentiation2d(CDerivationQ2, q2, q1, out dEdq2, out d2Edq2dq2, out d2Edq2dq1,
                                                 out d3Edq2dq1dq2);
                SetCashValue(q1, q2, ValueType.dEdq2, dEdq2);
                SetCashValue(q1, q2, ValueType.d2Edq2dq2, d2Edq2dq2);
                return dEdq2;
            }
        }
        protected override double Getd2Edq1dq1(double q1, double q2)
        {
            double dEdq1, d2Edq1dq1, d2Edq1dq2, d3Edq1dq1dq2;
            spline2d.splinedifferentiation2d(CDerivationQ1, q2, q1, out dEdq1, out d2Edq1dq2, out d2Edq1dq1, out d3Edq1dq1dq2);
            SetCashValue(q1, q2, ValueType.d2Edq1dq1, d2Edq1dq1);
            return d2Edq1dq1;

        }
        protected override double Getd2Edq2dq2(double q1, double q2)
        {
            double dEdq2, d2Edq2dq1, d2Edq2dq2, d3Edq2dq1dq2;
            spline2d.splinedifferentiation2d(CDerivationQ2, q2, q1, out dEdq2, out d2Edq2dq2, out d2Edq2dq1, out d3Edq2dq1dq2);
            SetCashValue(q1, q2, ValueType.d2Edq2dq2, d2Edq2dq2);
            return d2Edq2dq2;
        }
        protected override double Getd2Edq1dq2(double q1, double q2)
        {
            /*
            double E, dEdq1, dEdq2, d2Edq1dq2;
            spline2d.splinedifferentiation2d(C, q2, q1, out E, out dEdq2, out dEdq1, out d2Edq1dq2);
            SetCashValue(q1, q2, ValueType.E, E);
            SetCashValue(q1, q2, ValueType.dEdq1, dEdq1);
            SetCashValue(q1, q2, ValueType.dEdq2, dEdq2);
            */

            double dEdq1, d2Edq1dq1, d2Edq1dq2, d3Edq1dq1dq2;
            spline2d.splinedifferentiation2d(CDerivationQ1, q2, q1, out dEdq1, out d2Edq1dq2, out d2Edq1dq1, out d3Edq1dq1dq2);
            SetCashValue(q1, q2, ValueType.dEdq1, dEdq1);
            SetCashValue(q1, q2, ValueType.d2Edq1dq1, d2Edq1dq1);
            SetCashValue(q1, q2, ValueType.d2Edq1dq2, d2Edq1dq2);

            return d2Edq1dq2;
        }

        protected override double Getd2Edq2dq1(double q1, double q2)
        {
            double dEdq2, d2Edq2dq1, d2Edq2dq2, d3Edq2dq1dq2;
            spline2d.splinedifferentiation2d(CDerivationQ2, q2, q1, out dEdq2, out d2Edq2dq2, out d2Edq2dq1, out d3Edq2dq1dq2);
            SetCashValue(q1, q2, ValueType.dEdq2, dEdq2);
            SetCashValue(q1, q2, ValueType.d2Edq2dq2, d2Edq2dq2);
            SetCashValue(q1, q2, ValueType.d2Edq2dq1, d2Edq2dq1);

            return d2Edq2dq1;
        }

        public override IAxes Axes1
        {
            get { return AxesQ1; }
        }

        public override IAxes Axes2
        {
            get { return AxesQ2; }
        }

        private readonly Axes AxesQ1;
        private readonly Axes AxesQ2;


        private readonly ScaleDimension _EnergyDimension = new ScaleDimension(DimEnergy.Сalorie, ScaleCoeff.kilo);
        public override ScaleDimension EnergyDimension
        {
            get { return _EnergyDimension; }
        }

        private readonly double[,] Energy;
        private readonly double[] Q1;
        private readonly double[] Q2;

        public double[] GetQ1ArrayClone()
        {
            return (double[]) Q1.Clone();
        }
        public double[] GetQ2ArrayClone()
        {
            return (double[])Q2.Clone();
        }

        private readonly double[] C;
        private readonly double[] CDerivationQ1;
        private readonly double[] CDerivationQ2;

        private void BuildSplineCoeff(double[,] matrix, out double[] coeff)
        {
            var q1Count = matrix.GetLength(0);
            var q2Count = matrix.GetLength(1);

            double[] Q1Array = Q1;
            double[] Q2Array = Q2;

            spline2d.buildbicubicspline(Q2Array, Q1Array, matrix, q1Count, q2Count, out coeff);
        }

        private void BuildDerivationsCoeff(out double[] cDerivationQ1, out double[] cDerivationQ2)
        {
            double[,] dEdq1Array, dEdq2Array;

            BuildDerivationMatrix(out dEdq1Array, out dEdq2Array);

            BuildSplineCoeff(dEdq1Array, out cDerivationQ1);
            BuildSplineCoeff(dEdq2Array, out cDerivationQ2);
        }

        private void BuildDerivationMatrix(out double[,] dEdq1Array, out double[,] dEdq2Array)
        {
            var q1Count = Q1.Length;
            var q2Count = Q2.Length;
            
            dEdq1Array = new double[q1Count, q2Count];
            dEdq2Array = new double[q1Count, q2Count];

            for (var i1 = 0; i1 < q1Count; i1++ )
            {
                for (var i2 = 0; i2 < q2Count; i2++)
                {
                    var q1 = Q1[i1];
                    var q2 = Q2[i2];
                    
                    dEdq1Array[i1, i2] = GetdEdq1(q1, q2);
                    dEdq2Array[i1, i2] = GetdEdq2(q1, q2);
                }
            }
        } 

        private void FindMinMaxE()
        {
            Energy.FindMinMax2D(_MinE, _MaxE);
        }

        public double AvgGridStepQ1()
        {
            return (Axes1.MaxValue - Axes1.MinValue)/Q1.Length;
        }
        public double AvgGridStepQ2()
        {
            return (Axes2.MaxValue - Axes2.MinValue) / Q2.Length;
        }
    }
}
