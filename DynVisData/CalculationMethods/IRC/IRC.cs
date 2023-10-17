using System;
using System.Linq;
using System.Collections.Generic;
using DynVis.Core;
using DynVis.Core.Progress;
using DynVis.Core.ReactionPath;
using DynVis.Data.Properties;
using DynVis.Math;

namespace DynVis.Data.CalculationMethods.IRC
{
    class IRC
    {
        public int MaxStepCount = 500000;
        public double MaxStep = 1E-1;

        public double Epsilon = 1E-9;

        public double OptSquereDelta = 0.025 / 1000; //Минимальное приращение координаты [ангстрем]
        public bool AutoOptimizationDelta = true;
        public bool Optimize = true;

        public IPath CalcIRC(ISurface3D PES)
        {
            var Path = new Path (PES);

            if (AutoOptimizationDelta)
            {
                OptSquereDelta = ((PES.Axes1.MaxValue - PES.Axes1.MinValue) + (PES.Axes2.MaxValue - PES.Axes2.MinValue)) / 2000;
                OptSquereDelta *= OptSquereDelta;
            }

            MaxStep = ((PES.Axes1.MaxValue - PES.Axes1.MinValue) + (PES.Axes2.MaxValue - PES.Axes2.MinValue)) / MaxStepCount;

            var q1 = PES.CurrentPoint.X;
            var q2 = PES.CurrentPoint.Y;


            var ForvardArray = GetPath(PES, q1, q2, true);
            var ReverseArray = GetPath(PES, q1, q2, false);

            if (ForvardArray != null && ReverseArray != null)
            {
                var Reverse = ReverseArray.Reverse();

                foreach (var point in Reverse)
                {
                    Path.Add(point, 0);
                }

                foreach (var point in ForvardArray)
                {
                    Path.Add(point, 0);
                }

                if (Path.Count < 2) Path = null;

                Path.Name = LangResource.MinimalEnergyPath;

                return Path;
            }

            return null;
        }

        private PointD[] GetPath(ISurface3D PES, double q1, double q2, bool forvard)
        {
            double p1, p2;
            int n = 0;

            var sign = forvard ? 1 : -1;

            var Result = new List<PointD>(MaxStepCount);

            if (GetEigenPoint(PES, q1, q2, out p1, out p2, ref n))
            {
                var PrevPoint = new PointD(q1, q2);

                double prevE = PES.E(q1, q2);
                const int prevCount = 10000;
                int prevCounter = 0;

                p1 *= sign;
                p2 *= sign;

                for (var i = 0; i < MaxStepCount; i++)
                {
                    q1 += MaxStep * p1;
                    q2 += MaxStep * p2;

                    //GetEigenPoint(PES, q1, q2, out p1, out p2, n);

                    p1 = -PES.dEdq1(q1, q2);
                    p2 = -PES.dEdq2(q1, q2);

                    MathExtended.Normalize(ref p1, ref p2);

                    if (PES.IsValidCoord(q1, q2))
                    {
                        if ((!Optimize || PrevPoint.SquearDistanceTo(q1, q2) > OptSquereDelta))
                        {
                            Result.Add(new PointD(q1, q2));
                            PrevPoint.SetValues(q1, q2);
                        }
                    }
                    else
                    {
                        break;
                    }



                    if (prevCounter >= prevCount)
                    {
                        var E = PES.E(q1, q2); 
                        
                        if (prevE < E)
                        {
                            break;
                        }

                        prevE = E;

                        prevCounter = 0;
                    } 
                    else
                    {
                        prevCounter++;
                    }
                        


                    
                }

                return Result.ToArray();
            }
            return null;
        }

        private static bool GetEigenPoint(ISurface3D PES, double q1, double q2, out double p1, out double p2, ref int n)
        {
            double[] val;
            double[,] vecs;
            PES.GetHessEigensystem(q1, q2, out val, out vecs);

            var result = true;

            if (val[0] < 0 && val[1] >= 0) n = 0;
            else
                if (val[1] < 0 && val[0] >= 0) n = 1; else result = false;


            p1 = vecs[n, 0];
            p2 = vecs[n, 1];

            return result;
        }

        [NonSerialized]
        public ProgressChanged Progress;
    }
}
