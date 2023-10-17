using System;
using System.Collections.Generic;
using DynVis.Core.Points;
using DynVis.Core.Progress;
using M=System.Math;
using DynVis.Core;
using DynVis.Math;

namespace DynVis.Data.CalculationMethods.CriticalPoints
{
    internal class CriticalPoints
    {
        public int stepCount = 1000;
        public double Epsilon = 1E-9;
        public int MaxStepForOptimization = 10000;

        public double Q1;
        public double Q2;

        public enum CalcModeType {ScanSurface, OnePoint}

        public CalcModeType CalcMode = CalcModeType.ScanSurface;

        public ICollection<ISurfacePoint> CalcPoints(ISurface3D pes)
        {
            switch(CalcMode)
            {
                case CalcModeType.ScanSurface:
                    return ScanSurface(pes);
                case CalcModeType.OnePoint:
                    return GetPoint(pes);
            }
            return null;
        }

        private ICollection<ISurfacePoint> ScanSurface(ISurface3D pes)
        {
            var result = new SurfacePoints();

            var Q1Step = ((pes.Axes1.MaxValue - pes.Axes1.MinValue) / stepCount);
            var Q2Step = ((pes.Axes2.MaxValue - pes.Axes2.MinValue) / stepCount);

            var PrevQ1_dEdq1 = new double[stepCount];
            var CurrQ1_dEdq1 = new double[stepCount];

            PrevQ1_dEdq1.SetAllItemsTo(i => pes.dEdq1(pes.Axes1.MinValue, pes.Axes2.MinValue + i * Q1Step));
            CurrQ1_dEdq1.SetAllItemsTo(i => pes.dEdq1(pes.Axes1.MinValue + Q1Step, pes.Axes2.MinValue + i * Q1Step));

            var MaxStep = (Q1Step + Q2Step) / 2.0;

            for (var i1 = 1; i1 < stepCount; i1++)
            {
                var q1 = pes.Axes1.MinValue + i1 * Q1Step;

                double PrevQ2_dEdq2 = pes.dEdq2(q1, pes.Axes2.MinValue);
                double CurrQ2_dEdq2 = pes.dEdq2(q1, pes.Axes2.MinValue + Q2Step);
                


                for (var i2 = 1; i2 < stepCount; i2++)
                {
                    var q2 = pes.Axes2.MinValue + i2 * Q2Step;

                    var PrevdEdq1 = PrevQ1_dEdq1[i2];
                    var PrevdEdq2 = PrevQ2_dEdq2;

                    var dEdq1 = CurrQ1_dEdq1[i2];
                    var dEdq2 = CurrQ2_dEdq2;

                    var NextdEdq1 = pes.dEdq1(q1, q2);
                    var NextdEdq2 = pes.dEdq2(q1, q2);

                    var PrevQ1 = (MathExtended.Sign(dEdq1) != MathExtended.Sign(PrevdEdq1));
                    var PrevQ2 = (MathExtended.Sign(dEdq2) != MathExtended.Sign(PrevdEdq2));
                    var NextQ1 = (MathExtended.Sign(dEdq1) != MathExtended.Sign(NextdEdq1));
                    var NextQ2 = (MathExtended.Sign(dEdq2) != MathExtended.Sign(NextdEdq2));
                    //var PrevNextQ1 = (M.Sign(PrevdEdq1) != M.Sign(NextdEdq1)) && M.Sign(PrevdEdq1) != 0;
                    //var PrevNextQ2 = (M.Sign(PrevdEdq2) != M.Sign(NextdEdq2)) && M.Sign(PrevdEdq2) != 0;

                    if ((PrevQ1 && PrevQ2) || (NextQ1 && NextQ2) || (PrevQ1 && NextQ2) || (PrevQ2 && NextQ1) /*|| (PrevNextQ1 && PrevNextQ2)*/)
                    {
                        var r = GetPointFromPosition(pes, q1, q2, MaxStep);

                        if (r != null && result.IndexOfEquialPoint(r.Point.X, r.Point.Y, Point => Point.Point, MaxStep*2) < 0)
                        {
                            result.Add(r);
                        }
                    }
                    PrevQ2_dEdq2 = dEdq2;
                    PrevQ1_dEdq1[i2] = dEdq1;

                    CurrQ1_dEdq1[i2] = NextdEdq1;
                    CurrQ2_dEdq2 = NextdEdq2;

                }

                if (Progress != null) Progress((int)(100 * i1 / (float)stepCount));
            }

            return result;
        }

        private ICollection<ISurfacePoint> GetPoint(ISurface3D pes)
        {
            var result = new SurfacePoints();

            var Step = ((pes.Axes1.MaxValue - pes.Axes1.MinValue) / stepCount);
            Step += ((pes.Axes2.MaxValue - pes.Axes2.MinValue) / stepCount);
            Step /= 2.0;

            var point = GetPointFromPosition(pes, Q1, Q2, Step);

            if (point != null)
            {
                result.Add(point);

                return result;
            }
            return null;
        }

        private SurfacePoint GetPointFromPosition(ISurface3D pes, double q1, double q2, double maxStep)
        {
            double r1 = q1, r2 = q2;
            MathExtended.FindSolve(ref r1, ref r2, pes.dEdq1, pes.dEdq2, Epsilon, maxStep*0.5, MaxStepForOptimization);

            if (pes.IsValidCoord(ref r1, ref r2))
            {
                    double e1, e2;
                    pes.GetHessEigenvalues(r1, r2, out e1, out e2);

                    var type = SurfacePointType.Stationary;

                    if (e1 < 0 && e2 < 0)
                    {
                        type = SurfacePointType.Maximum;
                    }
                    else if (e1 > 0 && e2 > 0)
                    {
                        type = SurfacePointType.Minimum;
                    }
                    else if (e1 != 0 && e2 != 0)
                    {
                        type = SurfacePointType.Minimax;
                    }

                    var Additional = String.Format("Eigenvalues: ({0:F6},{1:F6})", e1, e2);

                    return new SurfacePoint
                               {
                                   SurfacePointType = type,
                                   PointD = new PointD(r1, r2),
                                   AdditionalInformation = Additional
                               };
                
            }
            return null;
        }


        [NonSerialized]
        public ProgressChanged Progress;
    }
}
