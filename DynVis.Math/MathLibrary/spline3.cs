/*************************************************************************
Copyright (c) 2007, Sergey Bochkanov (ALGLIB project).

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

- Redistributions of source code must retain the above copyright
  notice, this list of conditions and the following disclaimer.

- Redistributions in binary form must reproduce the above copyright
  notice, this list of conditions and the following disclaimer listed
  in this license in the documentation and/or other materials
  provided with the distribution.

- Neither the name of the copyright holders nor the names of its
  contributors may be used to endorse or promote products derived from
  this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*************************************************************************/

using M = System.Math;

namespace DynVis.Math
{
    public class spline3
    {
        /*************************************************************************
        Построение таблицы коэффициентов кусочно-линейного сплайна

        Входные параметры:
            X           -   абсциссы, массив с нумерацией элементов [0..N-1]
            Y           -   значения функции,
                            массив с нумерацией элементов [0..N-1]
            N           -   число точек, N>=2

        Выходные параметры:
            C           -   таблица коэффициентов сплайна для использования в
                            подпрограмме SplineInterpolation

          -- ALGLIB PROJECT --
             Copyright 24.06.2007 by Bochkanov Sergey
        *************************************************************************/
        public static void buildlinearspline(double[] x,
            double[] y,
            int n,
            ref double[] c)
        {
            int i;

            x = (double[])x.Clone();
            y = (double[])y.Clone();

            System.Diagnostics.Debug.Assert(n >= 2, "BuildLinearSpline: N<2!");

            //
            // Sort points
            //
            heapsortpoints(x, y, n);

            //
            // Fill C:
            //  C[0]            -   length(C)
            //  C[1]            -   type(C):
            //                      3 - general cubic spline
            //  C[2]            -   N
            //  C[3]...C[3+N-1] -   x[i], i = 0...N-1
            //  C[3+N]...C[3+N+(N-1)*4-1] - coefficients table
            //
            int tblsize = 3 + n + (n - 1) * 4;
            c = new double[tblsize - 1 + 1];
            c[0] = tblsize;
            c[1] = 3;
            c[2] = n;
            for (i = 0; i <= n - 1; i++)
            {
                c[3 + i] = x[i];
            }
            for (i = 0; i <= n - 2; i++)
            {
                c[3 + n + 4 * i + 0] = y[i];
                c[3 + n + 4 * i + 1] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
                c[3 + n + 4 * i + 2] = 0;
                c[3 + n + 4 * i + 3] = 0;
            }
        }


        /*************************************************************************
        Построение таблицы коэффициентов кубического сплайна

        Входные параметры:
            X           -   абсциссы, массив с нумерацией элементов [0..N-1]
            Y           -   значения функции,
                            массив с нумерацией элементов [0..N-1]
            N           -   число точек, N>=2
            BoundLType  -   тип граничного условия (левая граница)
            BoundL      -   значение первой (или второй, в зависимости от
                            BoundType) производной сплайна на левой границе
            BoundRType  -   тип граничного условия (правая граница)
            BoundR      -   значение первой (или второй, в зависимости от
                            BoundType) производной сплайна на правой границе

        Выходные параметры:
            C           -   таблица коэффициентов сплайна для использования в
                            подпрограмме SplineInterpolation

        Параметры BoundLType/BoundRType задают тип граничного условия и  принимают
        следующие значения:
            * 0, что соответствует граничному условию типа "сплайн, завершающийся
              параболой" (при этом значения BoundL/BoundR игнорируются).
            * 1, что соответствует граничному условию для первой производной
            * 2, что соответствует граничному условию для второй производной

          -- ALGLIB PROJECT --
             Copyright 23.06.2007 by Bochkanov Sergey
        *************************************************************************/
        public static void buildcubicspline(double[] x,
            double[] y,
            int n,
            int boundltype,
            double boundl,
            int boundrtype,
            double boundr,
            ref double[] c)
        {
            var d = new double[0];
            int i;

            x = (double[])x.Clone();
            y = (double[])y.Clone();

            System.Diagnostics.Debug.Assert(n >= 2, "BuildCubicSpline: N<2!");
            System.Diagnostics.Debug.Assert(boundltype == 0 | boundltype == 1 | boundltype == 2, "BuildCubicSpline: incorrect BoundLType!");
            System.Diagnostics.Debug.Assert(boundrtype == 0 | boundrtype == 1 | boundrtype == 2, "BuildCubicSpline: incorrect BoundRType!");
            var a1 = new double[n - 1 + 1];
            var a2 = new double[n - 1 + 1];
            var a3 = new double[n - 1 + 1];
            var b = new double[n - 1 + 1];

            //
            // Special case:
            // * N=2
            // * parabolic terminated boundary condition on both ends
            //
            if (n == 2 & boundltype == 0 & boundrtype == 0)
            {

                //
                // Change task type
                //
                boundltype = 2;
                boundl = 0;
                boundrtype = 2;
                boundr = 0;
            }

            //
            //
            // Sort points
            //
            heapsortpoints(x, y, n);

            //
            // Left boundary conditions
            //
            if (boundltype == 0)
            {
                a1[0] = 0;
                a2[0] = 1;
                a3[0] = 1;
                b[0] = 2 * (y[1] - y[0]) / (x[1] - x[0]);
            }
            if (boundltype == 1)
            {
                a1[0] = 0;
                a2[0] = 1;
                a3[0] = 0;
                b[0] = boundl;
            }
            if (boundltype == 2)
            {
                a1[0] = 0;
                a2[0] = 2;
                a3[0] = 1;
                b[0] = 3 * (y[1] - y[0]) / (x[1] - x[0]) - 0.5 * boundl * (x[1] - x[0]);
            }

            //
            // Central conditions
            //
            for (i = 1; i <= n - 2; i++)
            {
                a1[i] = x[i + 1] - x[i];
                a2[i] = 2 * (x[i + 1] - x[i - 1]);
                a3[i] = x[i] - x[i - 1];
                b[i] = 3 * (y[i] - y[i - 1]) / (x[i] - x[i - 1]) * (x[i + 1] - x[i]) + 3 * (y[i + 1] - y[i]) / (x[i + 1] - x[i]) * (x[i] - x[i - 1]);
            }

            //
            // Right boundary conditions
            //
            if (boundrtype == 0)
            {
                a1[n - 1] = 1;
                a2[n - 1] = 1;
                a3[n - 1] = 0;
                b[n - 1] = 2 * (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]);
            }
            if (boundrtype == 1)
            {
                a1[n - 1] = 0;
                a2[n - 1] = 1;
                a3[n - 1] = 0;
                b[n - 1] = boundr;
            }
            if (boundrtype == 2)
            {
                a1[n - 1] = 1;
                a2[n - 1] = 2;
                a3[n - 1] = 0;
                b[n - 1] = 3 * (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]) + 0.5 * boundr * (x[n - 1] - x[n - 2]);
            }

            //
            // Solve
            //
            solvetridiagonal(a1, a2, a3, b, n, ref d);

            //
            // Now problem is reduced to the cubic Hermite spline
            //
            buildhermitespline(x, y, d, n, out c);
        }


        /*************************************************************************
        Построение таблицы коэффициентов сплайна Эрмита

        Входные параметры:
            X           -   абсциссы, массив с нумерацией элементов [0..N-1]
            Y           -   значения функции,
                            массив с нумерацией элементов [0..N-1]
            D           -   значения производной,
                            массив с нумерацией элементов [0..N-1]
            N           -   число точек, N>=2

        Выходные параметры:
            C           -   таблица коэффициентов сплайна для использования в
                            подпрограмме SplineInterpolation

          -- ALGLIB PROJECT --
             Copyright 23.06.2007 by Bochkanov Sergey
        *************************************************************************/
        public static void buildhermitespline(double[] x,
            double[] y,
            double[] d,
            int n,
            out double[] c)
        {
            int i;

            x = (double[])x.Clone();
            y = (double[])y.Clone();
            d = (double[])d.Clone();

            System.Diagnostics.Debug.Assert(n >= 2, "BuildHermiteSpline: N<2!");

            //
            // Sort points
            //
            heapsortdpoints(x, y, d, n);

            //
            // Fill C:
            //  C[0]            -   length(C)
            //  C[1]            -   type(C):
            //                      3 - general cubic spline
            //  C[2]            -   N
            //  C[3]...C[3+N-1] -   x[i], i = 0...N-1
            //  C[3+N]...C[3+N+(N-1)*4-1] - coefficients table
            //
            int tblsize = 3 + n + (n - 1) * 4;
            c = new double[tblsize - 1 + 1];
            c[0] = tblsize;
            c[1] = 3;
            c[2] = n;
            for (i = 0; i <= n - 1; i++)
            {
                c[3 + i] = x[i];
            }
            for (i = 0; i <= n - 2; i++)
            {
                var delta = x[i + 1] - x[i];
                var delta2 = AP.Math.Sqr(delta);
                var delta3 = delta * delta2;
                c[3 + n + 4 * i + 0] = y[i];
                c[3 + n + 4 * i + 1] = d[i];
                c[3 + n + 4 * i + 2] = (3 * (y[i + 1] - y[i]) - 2 * d[i] * delta - d[i + 1] * delta) / delta2;
                c[3 + n + 4 * i + 3] = (2 * (y[i] - y[i + 1]) + d[i] * delta + d[i + 1] * delta) / delta3;
            }
        }


        /*************************************************************************
        Построение таблицы коэффициентов сплайна Акимы

        Входные параметры:
            X           -   абсциссы, массив с нумерацией элементов [0..N-1]
            Y           -   значения функции,
                            массив с нумерацией элементов [0..N-1]
            N           -   число точек, N>=5

        Выходные параметры:
            C           -   таблица коэффициентов сплайна для использования в
                            подпрограмме SplineInterpolation

          -- ALGLIB PROJECT --
             Copyright 24.06.2007 by Bochkanov Sergey
        *************************************************************************/
        public static void buildakimaspline(double[] x,
            double[] y,
            int n,
            out double[] c)
        {
            int i;

            x = (double[])x.Clone();
            y = (double[])y.Clone();

            System.Diagnostics.Debug.Assert(n >= 5, "BuildAkimaSpline: N<5!");

            //
            // Sort points
            //
            heapsortpoints(x, y, n);

            //
            // Prepare W (weights), Diff (divided differences)
            //
            var w = new double[n - 2 + 1];
            var diff = new double[n - 2 + 1];
            for (i = 0; i <= n - 2; i++)
            {
                diff[i] = (y[i + 1] - y[i]) / (x[i + 1] - x[i]);
            }
            for (i = 1; i <= n - 2; i++)
            {
                w[i] = M.Abs(diff[i] - diff[i - 1]);
            }

            //
            // Prepare Hermite interpolation scheme
            //
            var d = new double[n - 1 + 1];
            for (i = 2; i <= n - 3; i++)
            {
                if (M.Abs(w[i - 1]) + M.Abs(w[i + 1]) != 0)
                {
                    d[i] = (w[i + 1] * diff[i - 1] + w[i - 1] * diff[i]) / (w[i + 1] + w[i - 1]);
                }
                else
                {
                    d[i] = ((x[i + 1] - x[i]) * diff[i - 1] + (x[i] - x[i - 1]) * diff[i]) / (x[i + 1] - x[i - 1]);
                }
            }
            d[0] = diffthreepoint(x[0], x[0], y[0], x[1], y[1], x[2], y[2]);
            d[1] = diffthreepoint(x[1], x[0], y[0], x[1], y[1], x[2], y[2]);
            d[n - 2] = diffthreepoint(x[n - 2], x[n - 3], y[n - 3], x[n - 2], y[n - 2], x[n - 1], y[n - 1]);
            d[n - 1] = diffthreepoint(x[n - 1], x[n - 3], y[n - 3], x[n - 2], y[n - 2], x[n - 1], y[n - 1]);

            //
            // Build Akima spline using Hermite interpolation scheme
            //
            buildhermitespline(x, y, d, n, out c);
        }


        /*************************************************************************
        Вычисление интерполирующего сплайна

        Входные параметры:
            C   -   массив коэффициентов, вычисленный подпрограммой для
                    построения сплайна.
            X   -   точка, в которой вычисляется значение сплайна

        Результат:
            значение сплайна в точке X

          -- ALGLIB PROJECT --
             Copyright 23.06.2007 by Bochkanov Sergey
        *************************************************************************/
        public static double splineinterpolation(ref double[] c,
            double x)
        {
            int m;

            System.Diagnostics.Debug.Assert((int)M.Round(c[1]) == 3, "SplineInterpolation: incorrect C!");
            var n = (int)M.Round(c[2]);

            //
            // Binary search in the [ x[0], ..., x[n-2] ] (x[n-1] is not included)
            //
            var l = 3;
            var r = 3 + n - 2 + 1;
            while (l != r - 1)
            {
                m = (l + r) / 2;
                if (c[m] >= x)
                {
                    r = m;
                }
                else
                {
                    l = m;
                }
            }

            //
            // Interpolation
            //
            x = x - c[l];
            m = 3 + n + 4 * (l - 3);
            var result = c[m] + x * (c[m + 1] + x * (c[m + 2] + x * c[m + 3]));
            return result;
        }


        /*************************************************************************
        Дифференцирование сплайна

        Входные параметры:
            C   -   массив коэффициентов, вычисленный подпрограммой для
                    построения сплайна.
            X   -   точка, в которой вычисляется значение сплайна

        Выходные параметры:
            S   -   значение сплайна в точке X
            DS  -   первая производная в точке X
            D2S -   вторая производная в точке X

          -- ALGLIB PROJECT --
             Copyright 24.06.2007 by Bochkanov Sergey
        *************************************************************************/
        public static void splinedifferentiation(double[] c,
            double x,
            out double s,
            out double ds,
            out double d2s)
        {
            int m;

            System.Diagnostics.Debug.Assert((int)M.Round(c[1]) == 3, "SplineInterpolation: incorrect C!");
            var n = (int)M.Round(c[2]);

            //
            // Binary search
            //
            int l = 3;
            int r = 3 + n - 2 + 1;
            while (l != r - 1)
            {
                m = (l + r) / 2;
                if (c[m] >= x)
                {
                    r = m;
                }
                else
                {
                    l = m;
                }
            }

            //
            // Differentiation
            //
            x = x - c[l];
            m = 3 + n + 4 * (l - 3);
            s = c[m] + x * (c[m + 1] + x * (c[m + 2] + x * c[m + 3]));
            ds = c[m + 1] + 2 * x * c[m + 2] + 3 * AP.Math.Sqr(x) * c[m + 3];
            d2s = 2 * c[m + 2] + 6 * x * c[m + 3];
        }


        /*************************************************************************
        Копирование

        Входные параметры:
            C   -   массив коэффициентов, вычисленный подпрограммой для
                    построения сплайна.

        Выходные параметры:
            CC  -   копия сплайна
        

          -- ALGLIB PROJECT --
             Copyright 29.06.2007 by Bochkanov Sergey
        *************************************************************************/
        public static void splinecopy(ref double[] c,
            ref double[] cc)
        {
            int i;

            var s = (int)M.Round(c[0]);
            cc = new double[s - 1 + 1];
            for (i = 0; i <= s - 1; i++)
            {
                cc[i] = c[i];
            }
        }


        /*************************************************************************
        Распаковка сплайна

        Входные параметры:
            C   -   массив коэффициентов, вычисленный подпрограммой для
                    построения сплайна.

        Выходные параметры:
            N   -   число точек, на основе которых был построен сплайн
            Tbl -   таблица коэффициентов сплайна. Массив с нумерацией элементов
                    [0..N-2, 0..5].
                    Для I = 0..N-2:
                        Tbl[I,0] = X[i]
                        Tbl[I,1] = X[i+1]
                        Tbl[I,2] = C0
                        Tbl[I,3] = C1
                        Tbl[I,4] = C2
                        Tbl[I,5] = C3
                    Сплайн имеет вид:
                        t = x-x[i]
                        S(x) = C0 + C1*t + C2*t^2 + C3*t^3

          -- ALGLIB PROJECT --
             Copyright 29.06.2007 by Bochkanov Sergey
        *************************************************************************/
        public static void splineunpack(ref double[] c,
            ref int n,
            ref double[,] tbl)
        {
            int i;

            System.Diagnostics.Debug.Assert((int)M.Round(c[1]) == 3, "SplineUnpack: incorrect C!");
            n = (int)M.Round(c[2]);
            tbl = new double[n - 2 + 1, 5 + 1];

            //
            // Fill
            //
            for (i = 0; i <= n - 2; i++)
            {
                tbl[i, 0] = c[3 + i];
                tbl[i, 1] = c[3 + i + 1];
                tbl[i, 2] = c[3 + n + 4 * i];
                tbl[i, 3] = c[3 + n + 4 * i + 1];
                tbl[i, 4] = c[3 + n + 4 * i + 2];
                tbl[i, 5] = c[3 + n + 4 * i + 3];
            }
        }


        /*************************************************************************
        Линейная замена аргумента сплайна

        Входные параметры:
            C   -   массив коэффициентов, вычисленный подпрограммой для
                    построения сплайна S(x).
            A, B-   коэффициенты преобразования x = A*t + B

        Выходные параметры:
            C   -   преобразованный сплайн

          -- ALGLIB PROJECT --
             Copyright 30.06.2007 by Bochkanov Sergey
        *************************************************************************/
        public static void splinelintransx(ref double[] c,
            double a,
            double b)
        {
            int i;
            double v;

            System.Diagnostics.Debug.Assert((int)M.Round(c[1]) == 3, "SplineLinTransX: incorrect C!");
            var n = (int)M.Round(c[2]);

            //
            // Special case: A=0
            //
            if (a == 0)
            {
                v = splineinterpolation(ref c, b);
                for (i = 0; i <= n - 2; i++)
                {
                    c[3 + n + 4 * i] = v;
                    c[3 + n + 4 * i + 1] = 0;
                    c[3 + n + 4 * i + 2] = 0;
                    c[3 + n + 4 * i + 3] = 0;
                }
                return;
            }

            //
            // General case: A<>0.
            // Unpack, X, Y, dY/dX.
            // Scale and pack again.
            //
            var x = new double[n - 1 + 1];
            var y = new double[n - 1 + 1];
            var d = new double[n - 1 + 1];
            for (i = 0; i <= n - 1; i++)
            {
                double dv, d2v;
                x[i] = c[3 + i];
                splinedifferentiation(c, x[i], out v, out dv, out d2v);
                x[i] = (x[i] - b) / a;
                y[i] = v;
                d[i] = a * dv;
            }
            buildhermitespline(x, y, d, n, out c);
        }


        /*************************************************************************
        Линейное преобразование сплайна

        Входные параметры:
            C   -   массив коэффициентов, вычисленный подпрограммой для
                    построения сплайна S(x).
            A, B-   коэффициенты преобразования S2(x) = A*S(x) + B

        Выходные параметры:
            C   -   преобразованный сплайн

          -- ALGLIB PROJECT --
             Copyright 30.06.2007 by Bochkanov Sergey
        *************************************************************************/
        public static void splinelintransy(ref double[] c,
            double a,
            double b)
        {
            int i;

            System.Diagnostics.Debug.Assert((int)M.Round(c[1]) == 3, "SplineLinTransX: incorrect C!");
            var n = (int)M.Round(c[2]);

            //
            // Special case: A=0
            //
            for (i = 0; i <= n - 2; i++)
            {
                c[3 + n + 4 * i] = a * c[3 + n + 4 * i] + b;
                c[3 + n + 4 * i + 1] = a * c[3 + n + 4 * i + 1];
                c[3 + n + 4 * i + 2] = a * c[3 + n + 4 * i + 2];
                c[3 + n + 4 * i + 3] = a * c[3 + n + 4 * i + 3];
            }
        }


        /*************************************************************************
        Вычисление определенного интеграла от сплайна

        Входные параметры:
            C   -   массив коэффициентов, вычисленный подпрограммой для
                    построения сплайна.
            X   -   точка, в которой вычисляется интеграл

        Результат:
            значение интеграла на отрезке [A, X], где A - левая граница интервала,
            на котором построен сплайн.

          -- ALGLIB PROJECT --
             Copyright 23.06.2007 by Bochkanov Sergey
        *************************************************************************/
        public static double splineintegration(ref double[] c,
            double x)
        {
            int i;
            int m;
            double w;

            System.Diagnostics.Debug.Assert((int)M.Round(c[1]) == 3, "SplineIntegration: incorrect C!");
            var n = (int)M.Round(c[2]);

            //
            // Binary search in the [ x[0], ..., x[n-2] ] (x[n-1] is not included)
            //
            int l = 3;
            int r = 3 + n - 2 + 1;
            while (l != r - 1)
            {
                m = (l + r) / 2;
                if (c[m] >= x)
                {
                    r = m;
                }
                else
                {
                    l = m;
                }
            }

            //
            // Integration
            //
            double result = 0;
            for (i = 3; i <= l - 1; i++)
            {
                w = c[i + 1] - c[i];
                m = 3 + n + 4 * (i - 3);
                result = result + c[m] * w;
                result = result + c[m + 1] * AP.Math.Sqr(w) / 2;
                result = result + c[m + 2] * AP.Math.Sqr(w) * w / 3;
                result = result + c[m + 3] * AP.Math.Sqr(AP.Math.Sqr(w)) / 4;
            }
            w = x - c[l];
            m = 3 + n + 4 * (l - 3);
            result = result + c[m] * w;
            result = result + c[m + 1] * AP.Math.Sqr(w) / 2;
            result = result + c[m + 2] * AP.Math.Sqr(w) * w / 3;
            result = result + c[m + 3] * AP.Math.Sqr(AP.Math.Sqr(w)) / 4;
            return result;
        }


        /*************************************************************************
        Obsolete subroutine, left for backward compatibility.
        *************************************************************************/
        public static void spline3buildtable(int n,
            int diffn,
            double[] x,
            double[] y,
            double boundl,
            double boundr,
            ref double[,] ctbl)
        {
            int i;
            int j;
            double b1;
            double b2;
            double b3;
            double b4;

            x = (double[])x.Clone();
            y = (double[])y.Clone();

            n = n - 1;
            int g = (n + 1) / 2;
            do
            {
                i = g;
                do
                {
                    j = i - g;
                    var c = true;
                    do
                    {
                        if (x[j] <= x[j + g])
                        {
                            c = false;
                        }
                        else
                        {
                            var tmp = x[j];
                            x[j] = x[j + g];
                            x[j + g] = tmp;
                            tmp = y[j];
                            y[j] = y[j + g];
                            y[j + g] = tmp;
                        }
                        j = j - 1;
                    }
                    while (j >= 0 & c);
                    i = i + 1;
                }
                while (i <= n);
                g = g / 2;
            }
            while (g > 0);
            ctbl = new double[4 + 1, n + 1];
            n = n + 1;
            if (diffn == 1)
            {
                b1 = 1;
                b2 = 6 / (x[1] - x[0]) * ((y[1] - y[0]) / (x[1] - x[0]) - boundl);
                b3 = 1;
                b4 = 6 / (x[n - 1] - x[n - 2]) * (boundr - (y[n - 1] - y[n - 2]) / (x[n - 1] - x[n - 2]));
            }
            else
            {
                b1 = 0;
                b2 = 2 * boundl;
                b3 = 0;
                b4 = 2 * boundr;
            }
            var nxm1 = n - 1;
            if (n >= 2)
            {
                if (n > 2)
                {
                    var dxj = x[1] - x[0];
                    var dyj = y[1] - y[0];
                    j = 2;
                    while (j <= nxm1)
                    {
                        var dxjp1 = x[j] - x[j - 1];
                        var dyjp1 = y[j] - y[j - 1];
                        var dxp = dxj + dxjp1;
                        ctbl[1, j - 1] = dxjp1 / dxp;
                        ctbl[2, j - 1] = 1 - ctbl[1, j - 1];
                        ctbl[3, j - 1] = 6 * (dyjp1 / dxjp1 - dyj / dxj) / dxp;
                        dxj = dxjp1;
                        dyj = dyjp1;
                        j = j + 1;
                    }
                }
                ctbl[1, 0] = -(b1 / 2);
                ctbl[2, 0] = b2 / 2;
                if (n != 2)
                {
                    j = 2;
                    while (j <= nxm1)
                    {
                        var pj = ctbl[2, j - 1] * ctbl[1, j - 2] + 2;
                        ctbl[1, j - 1] = -(ctbl[1, j - 1] / pj);
                        ctbl[2, j - 1] = (ctbl[3, j - 1] - ctbl[2, j - 1] * ctbl[2, j - 2]) / pj;
                        j = j + 1;
                    }
                }
                var yppb = (b4 - b3 * ctbl[2, nxm1 - 1]) / (b3 * ctbl[1, nxm1 - 1] + 2);
                i = 1;
                while (i <= nxm1)
                {
                    j = n - i;
                    var yppa = ctbl[1, j - 1] * yppb + ctbl[2, j - 1];
                    var dx = x[j] - x[j - 1];
                    ctbl[3, j - 1] = (yppb - yppa) / dx / 6;
                    ctbl[2, j - 1] = yppa / 2;
                    ctbl[1, j - 1] = (y[j] - y[j - 1]) / dx - (ctbl[2, j - 1] + ctbl[3, j - 1] * dx) * dx;
                    yppb = yppa;
                    i = i + 1;
                }
                for (i = 1; i <= n; i++)
                {
                    ctbl[0, i - 1] = y[i - 1];
                    ctbl[4, i - 1] = x[i - 1];
                }
            }
        }


        /*************************************************************************
        Obsolete subroutine, left for backward compatibility.
        *************************************************************************/
        public static double spline3interpolate(int n,
            ref double[,] c,
            double x)
        {

            n = n - 1;
            var l = n;
            var first = 0;
            while (l > 0)
            {
                var half = l / 2;
                var middle = first + half;
                if (c[4, middle] < x)
                {
                    first = middle + 1;
                    l = l - half - 1;
                }
                else
                {
                    l = half;
                }
            }
            var i = first - 1;
            if (i < 0)
            {
                i = 0;
            }
            return c[0, i] + (x - c[4, i]) * (c[1, i] + (x - c[4, i]) * (c[2, i] + c[3, i] * (x - c[4, i])));
        }


        /*************************************************************************
        Internal subroutine. Heap sort.
        *************************************************************************/
        private static void heapsortpoints(double[] x, double[] y, int n)
        {
            int i;
            int k;
            int t;
            double tmp;


            //
            // Test for already sorted set
            //
            bool isascending = true;
            bool isdescending = true;
            for (i = 1; i <= n - 1; i++)
            {
                isascending = isascending & x[i] > x[i - 1];
                isdescending = isdescending & x[i] < x[i - 1];
            }
            if (isascending)
            {
                return;
            }
            if (isdescending)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    var j = n - 1 - i;
                    if (j <= i)
                    {
                        break;
                    }
                    tmp = x[i];
                    x[i] = x[j];
                    x[j] = tmp;
                    tmp = y[i];
                    y[i] = y[j];
                    y[j] = tmp;
                }
                return;
            }

            //
            // Special case: N=1
            //
            if (n == 1)
            {
                return;
            }

            //
            // General case
            //
            i = 2;
            do
            {
                t = i;
                while (t != 1)
                {
                    k = t / 2;
                    if (x[k - 1] >= x[t - 1])
                    {
                        t = 1;
                    }
                    else
                    {
                        tmp = x[k - 1];
                        x[k - 1] = x[t - 1];
                        x[t - 1] = tmp;
                        tmp = y[k - 1];
                        y[k - 1] = y[t - 1];
                        y[t - 1] = tmp;
                        t = k;
                    }
                }
                i = i + 1;
            }
            while (i <= n);
            i = n - 1;
            do
            {
                tmp = x[i];
                x[i] = x[0];
                x[0] = tmp;
                tmp = y[i];
                y[i] = y[0];
                y[0] = tmp;
                t = 1;
                while (t != 0)
                {
                    k = 2 * t;
                    if (k > i)
                    {
                        t = 0;
                    }
                    else
                    {
                        if (k < i)
                        {
                            if (x[k] > x[k - 1])
                            {
                                k = k + 1;
                            }
                        }
                        if (x[t - 1] >= x[k - 1])
                        {
                            t = 0;
                        }
                        else
                        {
                            tmp = x[k - 1];
                            x[k - 1] = x[t - 1];
                            x[t - 1] = tmp;
                            tmp = y[k - 1];
                            y[k - 1] = y[t - 1];
                            y[t - 1] = tmp;
                            t = k;
                        }
                    }
                }
                i = i - 1;
            }
            while (i >= 1);
        }


        /*************************************************************************
        Internal subroutine. Heap sort.
        *************************************************************************/
        private static void heapsortdpoints(double[] x,
            double[] y,
            double[] d,
            int n)
        {
            int i;
            int k;
            int t;
            double tmp;


            //
            // Test for already sorted set
            //
            var isascending = true;
            var isdescending = true;
            for (i = 1; i <= n - 1; i++)
            {
                isascending = isascending & x[i] > x[i - 1];
                isdescending = isdescending & x[i] < x[i - 1];
            }
            if (isascending)
            {
                return;
            }
            if (isdescending)
            {
                for (i = 0; i <= n - 1; i++)
                {
                    var j = n - 1 - i;
                    if (j <= i)
                    {
                        break;
                    }
                    tmp = x[i];
                    x[i] = x[j];
                    x[j] = tmp;
                    tmp = y[i];
                    y[i] = y[j];
                    y[j] = tmp;
                    tmp = d[i];
                    d[i] = d[j];
                    d[j] = tmp;
                }
                return;
            }

            //
            // Special case: N=1
            //
            if (n == 1)
            {
                return;
            }

            //
            // General case
            //
            i = 2;
            do
            {
                t = i;
                while (t != 1)
                {
                    k = t / 2;
                    if (x[k - 1] >= x[t - 1])
                    {
                        t = 1;
                    }
                    else
                    {
                        tmp = x[k - 1];
                        x[k - 1] = x[t - 1];
                        x[t - 1] = tmp;
                        tmp = y[k - 1];
                        y[k - 1] = y[t - 1];
                        y[t - 1] = tmp;
                        tmp = d[k - 1];
                        d[k - 1] = d[t - 1];
                        d[t - 1] = tmp;
                        t = k;
                    }
                }
                i = i + 1;
            }
            while (i <= n);
            i = n - 1;
            do
            {
                tmp = x[i];
                x[i] = x[0];
                x[0] = tmp;
                tmp = y[i];
                y[i] = y[0];
                y[0] = tmp;
                tmp = d[i];
                d[i] = d[0];
                d[0] = tmp;
                t = 1;
                while (t != 0)
                {
                    k = 2 * t;
                    if (k > i)
                    {
                        t = 0;
                    }
                    else
                    {
                        if (k < i)
                        {
                            if (x[k] > x[k - 1])
                            {
                                k = k + 1;
                            }
                        }
                        if (x[t - 1] >= x[k - 1])
                        {
                            t = 0;
                        }
                        else
                        {
                            tmp = x[k - 1];
                            x[k - 1] = x[t - 1];
                            x[t - 1] = tmp;
                            tmp = y[k - 1];
                            y[k - 1] = y[t - 1];
                            y[t - 1] = tmp;
                            tmp = d[k - 1];
                            d[k - 1] = d[t - 1];
                            d[t - 1] = tmp;
                            t = k;
                        }
                    }
                }
                i = i - 1;
            }
            while (i >= 1);
        }


        /*************************************************************************
        Internal subroutine. Tridiagonal solver.
        *************************************************************************/
        private static void solvetridiagonal(double[] a,
            double[] b,
            double[] c,
            double[] d,
            int n,
            ref double[] x)
        {
            int k;

            a = (double[])a.Clone();
            b = (double[])b.Clone();
            c = (double[])c.Clone();
            d = (double[])d.Clone();

            x = new double[n - 1 + 1];
            a[0] = 0;
            c[n - 1] = 0;
            for (k = 1; k <= n - 1; k++)
            {
                var t = a[k] / b[k - 1];
                b[k] = b[k] - t * c[k - 1];
                d[k] = d[k] - t * d[k - 1];
            }
            x[n - 1] = d[n - 1] / b[n - 1];
            for (k = n - 2; k >= 0; k--)
            {
                x[k] = (d[k] - c[k] * x[k + 1]) / b[k];
            }
        }


        /*************************************************************************
        Internal subroutine. Three-point differentiation
        *************************************************************************/
        private static double diffthreepoint(double t,
            double x0,
            double f0,
            double x1,
            double f1,
            double x2,
            double f2)
        {

            t = t - x0;
            x1 = x1 - x0;
            x2 = x2 - x0;
            var a = (f2 - f0 - x2 / x1 * (f1 - f0)) / (AP.Math.Sqr(x2) - x1 * x2);
            var b = (f1 - f0 - a * AP.Math.Sqr(x1)) / x1;
            return 2 * a * t + b;
        }
    }
}