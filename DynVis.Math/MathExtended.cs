using System;
using M=System.Math;


namespace DynVis.Math
{
    public static class MathExtended
    {
        private const double RtD = 180 / M.PI;
        private const double DtR = M.PI / 180;
        private const double Pi2 = M.PI*2;

        public static double RadToDeg(double rad)
        {
            return rad * RtD;
        }
        public static double DegToRad(double deg)
        {
            return deg * DtR;
        }

        public static double Avg(double d1, double d2)
        {
            return (d1 + d2)*0.5;
        }

        public static bool Equial(double d1, double d2, double epsilon)
        {
            return M.Abs(d1 - d2) <= epsilon;
        }

        public static bool Equial(double d, double epsilon)
        {
            return M.Abs(d) <= epsilon;
        }

        /// <summary>
        /// Возращает значения угла от 0 до пи 
        /// </summary>
        /// <param name="angle">Угол в радианах</param>
        /// <returns></returns>
        public static double StandartAngle(double angle)
        {

            return angle - M.Floor(angle / Pi2) * Pi2;
        }

        /// <summary>
        /// Сравнивает два угла с учетом периодичности
        /// </summary>
        /// <param name="angle1"></param>
        /// <param name="angle2"></param>
        /// <returns></returns>
        public static bool EquialAngle(double angle1, double angle2)
        {
            return (StandartAngle(angle1) == StandartAngle(angle2));
        }

        public static bool Xor(this bool b1, bool b2)
        {
            return (b1 & !b2) || (b2 & !b1);
        }

        public static int Sign(double d)
        {
            if (double.IsNaN(d)) return 0;
            return M.Sign(d);
        }

        public static bool Parity(int i)
        {
            var hi = i/2.0;
            return M.Round(hi) == hi;
        }

        public static void Swap(ref double d1, ref double d2)
        {
            var t = d1;
            d1 = d2;
            d2 = t;
        }

        public static double FindSolve(double max, double min, Func<double, double> func, double epsilon)
        {
            double result, val;

            double maxVal = func(max);
            double minVal = func(min);

            if (M.Sign(minVal) == M.Sign(maxVal))
            {
                return min + (max - min)/2.0;
            }

            if (maxVal < minVal)
            {
                Swap(ref max, ref min);
            }
            
            do
            {
                result = min + (max - min) / 2.0;
                val = func(result);

                if (val < 0)
                {
                    min = result;
                } 
                else
                {
                    max = result;
                }
            } while (!Equial(val, epsilon));

            return result;
        }

        public static void FindSolve(ref double res1, ref double res2, Func<double, double, double> func1, Func<double, double, double> func2, double epsilon, double step, int maxStep)
        {
            var array = new[] {0, res1, res2};

            principalaxis.principalaxisminimize(ref array, epsilon, step,
                                                q =>
                                                {
                                                    var f1 = func1(q[1], q[2]);
                                                    var f2 = func2(q[1], q[2]);
                                                    return f1*f1+f2*f2;
                                                }, maxStep);

            res1 = array[1];
            res2 = array[2];
        }

        public static void CalcEigenvalues(double a11, double a22, double a12, double a21, out double e1, out double e2)
        {
            var b = a11 + a22;
            var c = a11 * a22 - a12 * a21;

            var D = M.Sqrt(b*b - 4*c);

            e1 = (b - D) * 0.5;
            e2 = (b + D) * 0.5;
        }

        public static void Normalize(ref double q1, ref double q2)
        {
            var Norm = M.Sqrt(q1*q1 + q2*q2);
            q1 /= Norm;
            q2 /= Norm;
        }

        public static void CalcEigensystem(double a11, double a22, double a12, double a21, out double[] vals, out double[,] vecs)
        {
            const int n = 2;

            vals = new double[n];
            vecs = new double[n, n];

            CalcEigenvalues(a11, a22, a12, a21, out vals[0], out vals[1]);

            vecs[0, 0] = (vals[0] - a22) / a21;
            vecs[0, 1] = 1;
            Normalize(ref vecs[0, 0], ref vecs[0, 1]);


            vecs[1, 0] = (vals[1] - a22) / a21;
            vecs[1, 1] = 1;
            Normalize(ref vecs[1, 0], ref vecs[1, 1]);
        }
    }
}
