using System.Diagnostics;
using M = System.Math;

namespace DynVis.Math
{
    public class spline2d
    {
        /*************************************************************************
        Построение билинейного интерполирующего сплайна

        Входные параметры:
            X   -   массив абсцисс прямоугольной сетки на которой строится сплайн.
                    Нумерация элементов: [0..N-1]
            Y   -   массив ординат прямоугольной сетки на которой строится сплайн.
                    Нумерация элементов: [0..M-1]
            F   -   матрица значений функции в узлах сетки.
                    Нумерация элементов: [0..M-1, 0..N-1]
            M   -   размер сетки по оси Y, M>=2
            N   -   размер сетки по оси X, N>=2
        
        Выходные параметры:
            C   -   таблица  коэффициентов  для   использования   в   подпрограмме
                    SplineInterpolation2D.

          -- ALGLIB PROJECT --
             Copyright 05.07.2007 by Bochkanov Sergey
        *************************************************************************/

        public static void buildbilinearspline(double[] x,
                                               double[] y,
                                               double[,] f,
                                               int m,
                                               int n,
                                               out double[] c)
        {
            int i;
            int j;
            int k;
            double t;

            x = (double[])x.Clone();
            y = (double[])y.Clone();
            f = (double[,])f.Clone();

            Debug.Assert(n >= 2 & m >= 2, "BuildBilinearSpline: N<2 or M<2!");

            //
            // Sort points
            //
            for (j = 0; j <= n - 1; j++)
            {
                k = j;
                for (i = j + 1; i <= n - 1; i++)
                {
                    if (x[i] < x[k])
                    {
                        k = i;
                    }
                }
                if (k != j)
                {
                    for (i = 0; i <= m - 1; i++)
                    {
                        t = f[i, j];
                        f[i, j] = f[i, k];
                        f[i, k] = t;
                    }
                    t = x[j];
                    x[j] = x[k];
                    x[k] = t;
                }
            }
            for (i = 0; i <= m - 1; i++)
            {
                k = i;
                for (j = i + 1; j <= m - 1; j++)
                {
                    if (y[j] < y[k])
                    {
                        k = j;
                    }
                }
                if (k != i)
                {
                    for (j = 0; j <= n - 1; j++)
                    {
                        t = f[i, j];
                        f[i, j] = f[k, j];
                        f[k, j] = t;
                    }
                    t = y[i];
                    y[i] = y[k];
                    y[k] = t;
                }
            }

            //
            // Fill C:
            //  C[0]            -   length(C)
            //  C[1]            -   type(C):
            //                      -1 = bilinear interpolant
            //                      -3 = general cubic spline
            //                           (see BuildBicubicSpline)
            //  C[2]:
            //      N (x count)
            //  C[3]:
            //      M (y count)
            //  C[4]...C[4+N-1]:
            //      x[i], i = 0...N-1
            //  C[4+N]...C[4+N+M-1]:
            //      y[i], i = 0...M-1
            //  C[4+N+M]...C[4+N+M+(N*M-1)]:
            //      f(i,j) table. f(0,0), f(0, 1), f(0,2) and so on...
            //
            int tblsize = 4 + n + m + n * m;
            c = new double[tblsize - 1 + 1];
            c[0] = tblsize;
            c[1] = -1;
            c[2] = n;
            c[3] = m;
            for (i = 0; i <= n - 1; i++)
            {
                c[4 + i] = x[i];
            }
            for (i = 0; i <= m - 1; i++)
            {
                c[4 + n + i] = y[i];
            }
            for (i = 0; i <= m - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    var shift = i * n + j;
                    c[4 + n + m + shift] = f[i, j];
                }
            }
        }


        /*************************************************************************
        Построение бикубического интерполирующего сплайна

        Входные параметры:
            X   -   массив абсцисс прямоугольной сетки на которой строится сплайн.
                    Нумерация элементов: [0..N-1]
            Y   -   массив ординат прямоугольной сетки на которой строится сплайн.
                    Нумерация элементов: [0..M-1]
            F   -   матрица значений функции в узлах сетки.
                    Нумерация элементов: [0..M-1, 0..N-1]
            M   -   размер сетки по оси Y
            N   -   размер сетки по оси X

        Выходные параметры:
            C   -   таблица  коэффициентов  для   использования   в   подпрограмме
                    SplineInterpolation2D.

          -- ALGLIB PROJECT --
             Copyright 05.07.2007 by Bochkanov Sergey
        *************************************************************************/

        public static void buildbicubicspline(double[] x,
                                              double[] y,
                                              double[,] f,
                                              int m,
                                              int n,
                                              out double[] c)
        {
            int i;
            int j;
            int k;
            double t;
            var dx = new double[0, 0];
            var dy = new double[0, 0];
            var dxy = new double[0, 0];

            x = (double[])x.Clone();
            y = (double[])y.Clone();
            f = (double[,])f.Clone();

            Debug.Assert(n >= 2 & m >= 2, "BuildBicubicSpline: N<2 or M<2!");

            //
            // Sort points
            //
            for (j = 0; j <= n - 1; j++)
            {
                k = j;
                for (i = j + 1; i <= n - 1; i++)
                {
                    if (x[i] < x[k])
                    {
                        k = i;
                    }
                }
                if (k != j)
                {
                    for (i = 0; i <= m - 1; i++)
                    {
                        t = f[i, j];
                        f[i, j] = f[i, k];
                        f[i, k] = t;
                    }
                    t = x[j];
                    x[j] = x[k];
                    x[k] = t;
                }
            }
            for (i = 0; i <= m - 1; i++)
            {
                k = i;
                for (j = i + 1; j <= m - 1; j++)
                {
                    if (y[j] < y[k])
                    {
                        k = j;
                    }
                }
                if (k != i)
                {
                    for (j = 0; j <= n - 1; j++)
                    {
                        t = f[i, j];
                        f[i, j] = f[k, j];
                        f[k, j] = t;
                    }
                    t = y[i];
                    y[i] = y[k];
                    y[k] = t;
                }
            }

            //
            // Fill C:
            //  C[0]            -   length(C)
            //  C[1]            -   type(C):
            //                      -1 = bilinear interpolant
            //                           (see BuildBilinearInterpolant)
            //                      -3 = general cubic spline
            //  C[2]:
            //      N (x count)
            //  C[3]:
            //      M (y count)
            //  C[4]...C[4+N-1]:
            //      x[i], i = 0...N-1
            //  C[4+N]...C[4+N+M-1]:
            //      y[i], i = 0...M-1
            //  C[4+N+M]...C[4+N+M+(N*M-1)]:
            //      f(i,j) table. f(0,0), f(0, 1), f(0,2) and so on...
            //  C[4+N+M+N*M]...C[4+N+M+(2*N*M-1)]:
            //      df(i,j)/dx table.
            //  C[4+N+M+2*N*M]...C[4+N+M+(3*N*M-1)]:
            //      df(i,j)/dy table.
            //  C[4+N+M+3*N*M]...C[4+N+M+(4*N*M-1)]:
            //      d2f(i,j)/dxdy table.
            //
            int tblsize = 4 + n + m + 4 * n * m;
            c = new double[tblsize - 1 + 1];
            c[0] = tblsize;
            c[1] = -3;
            c[2] = n;
            c[3] = m;
            for (i = 0; i <= n - 1; i++)
            {
                c[4 + i] = x[i];
            }
            for (i = 0; i <= m - 1; i++)
            {
                c[4 + n + i] = y[i];
            }
            bicubiccalcderivatives(f, x, y, m, n, ref dx, ref dy, ref dxy);
            for (i = 0; i <= m - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    var shift = i * n + j;
                    c[4 + n + m + shift] = f[i, j];
                    c[4 + n + m + n * m + shift] = dx[i, j];
                    c[4 + n + m + 2 * n * m + shift] = dy[i, j];
                    c[4 + n + m + 3 * n * m + shift] = dxy[i, j];
                }
            }
        }


        /*************************************************************************
        Интерполяция двухмерным (билинейным или бикубическим) сплайном

        Входные параметры:
            C   -   таблица коэффициентов, рассчитанная  подпрограммой  построения
                    сплайна.
            X, Y-   точка, в которой вычисляется значение сплайна

        Результат:
            Значение интерполирующего сплайна в точке (X,Y)

          -- ALGLIB PROJECT --
             Copyright 05.07.2007 by Bochkanov Sergey
        *************************************************************************/

        public static double splineinterpolation2d(double[] c,
                                                   double x,
                                                   double y)
        {
            double v;
            double vx;
            double vy;
            double vxy;

            splinedifferentiation2d(c, x, y, out v, out vx, out vy, out vxy);
            double result = v;
            return result;
        }


        /*************************************************************************
        Интерполяция двухмерным (билинейным или бикубическим) сплайном.
        Также осуществлятся вычисление градиента и перекрестной производной.

        Входные параметры:
            C   -   таблица коэффициентов, рассчитанная  подпрограммой  построения
                    сплайна.
            X, Y-   точка, в которой вычисляется значение сплайна

        Результат:
            F   -   значение интерполирующего сплайна
            FX  -   dF/dX
            FY  -   dF/dY
            FXY -   d2F/dXdY

          -- ALGLIB PROJECT --
             Copyright 05.07.2007 by Bochkanov Sergey
        *************************************************************************/

        public static void splinedifferentiation2d(double[] c,
                                                   double x,
                                                   double y,
                                                   out double f,
                                                   out double fx,
                                                   out double fy,
                                                   out double fxy)
        {
            int h;

            Debug.Assert((int)M.Round(c[1]) == -1 | (int)M.Round(c[1]) == -3,
                         "TwoDimensionalInterpolation: incorrect C!");
            var n = (int)M.Round(c[2]);
            var m = (int)M.Round(c[3]);

            //
            // Binary search in the [ x[0], ..., x[n-2] ] (x[n-1] is not included)
            //
            int l = 4;
            int r = 4 + n - 2 + 1;
            while (l != r - 1)
            {
                h = (l + r) / 2;
                if (c[h] >= x)
                {
                    r = h;
                }
                else
                {
                    l = h;
                }
            }
            double t = (x - c[l]) / (c[l + 1] - c[l]);
            double dt = 1.0 / (c[l + 1] - c[l]);
            int ix = l - 4;

            //
            // Binary search in the [ y[0], ..., y[m-2] ] (y[m-1] is not included)
            //
            l = 4 + n;
            r = 4 + n + (m - 2) + 1;
            while (l != r - 1)
            {
                h = (l + r) / 2;
                if (c[h] >= y)
                {
                    r = h;
                }
                else
                {
                    l = h;
                }
            }
            double u = (y - c[l]) / (c[l + 1] - c[l]);
            double du = 1.0 / (c[l + 1] - c[l]);
            int iy = l - (4 + n);

            //
            // Prepare F, dF/dX, dF/dY, d2F/dXdY
            //
            f = 0;
            fx = 0;
            fy = 0;
            fxy = 0;

            //
            // Bilinear interpolation
            //
            if ((int)M.Round(c[1]) == -1)
            {
                var shift1 = 4 + n + m;
                var y1 = c[shift1 + n * iy + ix];
                var y2 = c[shift1 + n * iy + (ix + 1)];
                var y3 = c[shift1 + n * (iy + 1) + (ix + 1)];
                var y4 = c[shift1 + n * (iy + 1) + ix];
                f = (1 - t) * (1 - u) * y1 + t * (1 - u) * y2 + t * u * y3 + (1 - t) * u * y4;
                fx = (-((1 - u) * y1) + (1 - u) * y2 + u * y3 - u * y4) * dt;
                fy = (-((1 - t) * y1) - t * y2 + t * y3 + (1 - t) * y4) * du;
                fxy = (y1 - y2 + y3 - y4) * du * dt;
                return;
            }

            //
            // Bicubic interpolation
            //
            if ((int)M.Round(c[1]) == -3)
            {
                //
                // Prepare info
                //
                var t0 = 1;
                var t1 = t;
                var t2 = AP.Math.Sqr(t);
                var t3 = t * t2;
                var u0 = 1;
                var u1 = u;
                var u2 = AP.Math.Sqr(u);
                var u3 = u * u2;
                var sf = 4 + n + m;
                var sfx = 4 + n + m + n * m;
                var sfy = 4 + n + m + 2 * n * m;
                var sfxy = 4 + n + m + 3 * n * m;
                var s1 = n * iy + ix;
                var s2 = n * iy + (ix + 1);
                var s3 = n * (iy + 1) + (ix + 1);
                var s4 = n * (iy + 1) + ix;

                //
                // Calculate
                //
                var v = +(1 * c[sf + s1]);
                f = f + v * t0 * u0;
                v = +(1 * c[sfy + s1] / du);
                f = f + v * t0 * u1;
                fy = fy + 1 * v * t0 * u0 * du;
                v = -(3 * c[sf + s1]) + 3 * c[sf + s4] - 2 * c[sfy + s1] / du - 1 * c[sfy + s4] / du;
                f = f + v * t0 * u2;
                fy = fy + 2 * v * t0 * u1 * du;
                v = +(2 * c[sf + s1]) - 2 * c[sf + s4] + 1 * c[sfy + s1] / du + 1 * c[sfy + s4] / du;
                f = f + v * t0 * u3;
                fy = fy + 3 * v * t0 * u2 * du;
                v = +(1 * c[sfx + s1] / dt);
                f = f + v * t1 * u0;
                fx = fx + 1 * v * t0 * u0 * dt;
                v = +(1 * c[sfxy + s1] / (dt * du));
                f = f + v * t1 * u1;
                fx = fx + 1 * v * t0 * u1 * dt;
                fy = fy + 1 * v * t1 * u0 * du;
                fxy = fxy + 1 * v * t0 * u0 * dt * du;
                v = -(3 * c[sfx + s1] / dt) + 3 * c[sfx + s4] / dt - 2 * c[sfxy + s1] / (dt * du) - 1 * c[sfxy + s4] / (dt * du);
                f = f + v * t1 * u2;
                fx = fx + 1 * v * t0 * u2 * dt;
                fy = fy + 2 * v * t1 * u1 * du;
                fxy = fxy + 2 * v * t0 * u1 * dt * du;
                v = +(2 * c[sfx + s1] / dt) - 2 * c[sfx + s4] / dt + 1 * c[sfxy + s1] / (dt * du) + 1 * c[sfxy + s4] / (dt * du);
                f = f + v * t1 * u3;
                fx = fx + 1 * v * t0 * u3 * dt;
                fy = fy + 3 * v * t1 * u2 * du;
                fxy = fxy + 3 * v * t0 * u2 * dt * du;
                v = -(3 * c[sf + s1]) + 3 * c[sf + s2] - 2 * c[sfx + s1] / dt - 1 * c[sfx + s2] / dt;
                f = f + v * t2 * u0;
                fx = fx + 2 * v * t1 * u0 * dt;
                v = -(3 * c[sfy + s1] / du) + 3 * c[sfy + s2] / du - 2 * c[sfxy + s1] / (dt * du) - 1 * c[sfxy + s2] / (dt * du);
                f = f + v * t2 * u1;
                fx = fx + 2 * v * t1 * u1 * dt;
                fy = fy + 1 * v * t2 * u0 * du;
                fxy = fxy + 2 * v * t1 * u0 * dt * du;
                v = +(9 * c[sf + s1]) - 9 * c[sf + s2] + 9 * c[sf + s3] - 9 * c[sf + s4] + 6 * c[sfx + s1] / dt + 3 * c[sfx + s2] / dt -
                    3 * c[sfx + s3] / dt - 6 * c[sfx + s4] / dt + 6 * c[sfy + s1] / du - 6 * c[sfy + s2] / du - 3 * c[sfy + s3] / du +
                    3 * c[sfy + s4] / du + 4 * c[sfxy + s1] / (dt * du) + 2 * c[sfxy + s2] / (dt * du) + 1 * c[sfxy + s3] / (dt * du) +
                    2 * c[sfxy + s4] / (dt * du);
                f = f + v * t2 * u2;
                fx = fx + 2 * v * t1 * u2 * dt;
                fy = fy + 2 * v * t2 * u1 * du;
                fxy = fxy + 4 * v * t1 * u1 * dt * du;
                v = -(6 * c[sf + s1]) + 6 * c[sf + s2] - 6 * c[sf + s3] + 6 * c[sf + s4] - 4 * c[sfx + s1] / dt - 2 * c[sfx + s2] / dt +
                    2 * c[sfx + s3] / dt + 4 * c[sfx + s4] / dt - 3 * c[sfy + s1] / du + 3 * c[sfy + s2] / du + 3 * c[sfy + s3] / du -
                    3 * c[sfy + s4] / du - 2 * c[sfxy + s1] / (dt * du) - 1 * c[sfxy + s2] / (dt * du) - 1 * c[sfxy + s3] / (dt * du) -
                    2 * c[sfxy + s4] / (dt * du);
                f = f + v * t2 * u3;
                fx = fx + 2 * v * t1 * u3 * dt;
                fy = fy + 3 * v * t2 * u2 * du;
                fxy = fxy + 6 * v * t1 * u2 * dt * du;
                v = +(2 * c[sf + s1]) - 2 * c[sf + s2] + 1 * c[sfx + s1] / dt + 1 * c[sfx + s2] / dt;
                f = f + v * t3 * u0;
                fx = fx + 3 * v * t2 * u0 * dt;
                v = +(2 * c[sfy + s1] / du) - 2 * c[sfy + s2] / du + 1 * c[sfxy + s1] / (dt * du) + 1 * c[sfxy + s2] / (dt * du);
                f = f + v * t3 * u1;
                fx = fx + 3 * v * t2 * u1 * dt;
                fy = fy + 1 * v * t3 * u0 * du;
                fxy = fxy + 3 * v * t2 * u0 * dt * du;
                v = -(6 * c[sf + s1]) + 6 * c[sf + s2] - 6 * c[sf + s3] + 6 * c[sf + s4] - 3 * c[sfx + s1] / dt - 3 * c[sfx + s2] / dt +
                    3 * c[sfx + s3] / dt + 3 * c[sfx + s4] / dt - 4 * c[sfy + s1] / du + 4 * c[sfy + s2] / du + 2 * c[sfy + s3] / du -
                    2 * c[sfy + s4] / du - 2 * c[sfxy + s1] / (dt * du) - 2 * c[sfxy + s2] / (dt * du) - 1 * c[sfxy + s3] / (dt * du) -
                    1 * c[sfxy + s4] / (dt * du);
                f = f + v * t3 * u2;
                fx = fx + 3 * v * t2 * u2 * dt;
                fy = fy + 2 * v * t3 * u1 * du;
                fxy = fxy + 6 * v * t2 * u1 * dt * du;
                v = +(4 * c[sf + s1]) - 4 * c[sf + s2] + 4 * c[sf + s3] - 4 * c[sf + s4] + 2 * c[sfx + s1] / dt + 2 * c[sfx + s2] / dt -
                    2 * c[sfx + s3] / dt - 2 * c[sfx + s4] / dt + 2 * c[sfy + s1] / du - 2 * c[sfy + s2] / du - 2 * c[sfy + s3] / du +
                    2 * c[sfy + s4] / du + 1 * c[sfxy + s1] / (dt * du) + 1 * c[sfxy + s2] / (dt * du) + 1 * c[sfxy + s3] / (dt * du) +
                    1 * c[sfxy + s4] / (dt * du);
                f = f + v * t3 * u3;
                fx = fx + 3 * v * t2 * u3 * dt;
                fy = fy + 3 * v * t3 * u2 * du;
                fxy = fxy + 9 * v * t2 * u2 * dt * du;
            }
        }


        /*************************************************************************
        Распаковка сплайна

        Входные параметры:
            C   -   массив коэффициентов, вычисленный подпрограммой для
                    построения билинейного или бикубического сплайна.

        Выходные параметры:
            M   -   размер сетки по оси Y
            N   -   размер сетки по оси X
            Tbl -   таблица коэффициентов сплайна. Массив с  нумерацией  элементов
                    [0..(N-1)*(M-1)-1, 0..19]. Каждая строка матрицы соответствует
                    одной ячейке сетки. Сетка обходится сначала слева направо (т.е.
                    в порядке возрастания X), затем снизу вверх  (т.е.  в  порядке
                    возрастания Y).
                    Формат матрицы:
                    For I = 0...M-2, J=0..N-2:
                        K =  I*(N-1)+J
                        Tbl[K,0] = X[j]
                        Tbl[K,1] = X[j+1]
                        Tbl[K,2] = Y[i]
                        Tbl[K,3] = Y[i+1]
                        Tbl[K,4] = C00
                        Tbl[K,5] = C01
                        Tbl[K,6] = C02
                        Tbl[K,7] = C03
                        Tbl[K,8] = C10
                        Tbl[K,9] = C11
                        ...
                        Tbl[K,19] = C33
                    На каждой из ячеек сетки сплайн имеет вид:
                        t = x-x[j]
                        u = y-y[i]
                        S(x) = SUM(c[i,j]*(x^i)*(y^j), i=0..3, j=0..3)

          -- ALGLIB PROJECT --
             Copyright 29.06.2007 by Bochkanov Sergey
        *************************************************************************/

        public static void splineunpack2d(ref double[] c,
                                          ref int m,
                                          ref int n,
                                          ref double[,] tbl)
        {
            int i;
            Debug.Assert((int)M.Round(c[1]) == -3 | (int)M.Round(c[1]) == -1, "SplineUnpack2D: incorrect C!");
            n = (int)M.Round(c[2]);
            m = (int)M.Round(c[3]);
            tbl = new double[(n - 1) * (m - 1) - 1 + 1, 19 + 1];

            //
            // Fill
            //
            for (i = 0; i <= m - 2; i++)
            {
                for (var j = 0; j <= n - 2; j++)
                {
                    var p = i * (n - 1) + j;
                    tbl[p, 0] = c[4 + j];
                    tbl[p, 1] = c[4 + j + 1];
                    tbl[p, 2] = c[4 + n + i];
                    tbl[p, 3] = c[4 + n + i + 1];
                    var dt = 1 / (tbl[p, 1] - tbl[p, 0]);
                    var du = 1 / (tbl[p, 3] - tbl[p, 2]);

                    //
                    // Bilinear interpolation
                    //
                    if ((int)M.Round(c[1]) == -1)
                    {
                        for (var k = 4; k <= 19; k++)
                        {
                            tbl[p, k] = 0;
                        }
                        var shift = 4 + n + m;
                        var y1 = c[shift + n * i + j];
                        var y2 = c[shift + n * i + (j + 1)];
                        var y3 = c[shift + n * (i + 1) + (j + 1)];
                        var y4 = c[shift + n * (i + 1) + j];
                        tbl[p, 4] = y1;
                        tbl[p, 4 + 1 * 4 + 0] = y2 - y1;
                        tbl[p, 4 + 0 * 4 + 1] = y4 - y1;
                        tbl[p, 4 + 1 * 4 + 1] = y3 - y2 - y4 + y1;
                    }

                    //
                    // Bicubic interpolation
                    //
                    if ((int)M.Round(c[1]) == -3)
                    {
                        var sf = 4 + n + m;
                        var sfx = 4 + n + m + n * m;
                        var sfy = 4 + n + m + 2 * n * m;
                        var sfxy = 4 + n + m + 3 * n * m;
                        var s1 = n * i + j;
                        var s2 = n * i + (j + 1);
                        var s3 = n * (i + 1) + (j + 1);
                        var s4 = n * (i + 1) + j;
                        tbl[p, 4 + 0 * 4 + 0] = +(1 * c[sf + s1]);
                        tbl[p, 4 + 0 * 4 + 1] = +(1 * c[sfy + s1] / du);
                        tbl[p, 4 + 0 * 4 + 2] = -(3 * c[sf + s1]) + 3 * c[sf + s4] - 2 * c[sfy + s1] / du - 1 * c[sfy + s4] / du;
                        tbl[p, 4 + 0 * 4 + 3] = +(2 * c[sf + s1]) - 2 * c[sf + s4] + 1 * c[sfy + s1] / du + 1 * c[sfy + s4] / du;
                        tbl[p, 4 + 1 * 4 + 0] = +(1 * c[sfx + s1] / dt);
                        tbl[p, 4 + 1 * 4 + 1] = +(1 * c[sfxy + s1] / (dt * du));
                        tbl[p, 4 + 1 * 4 + 2] = -(3 * c[sfx + s1] / dt) + 3 * c[sfx + s4] / dt - 2 * c[sfxy + s1] / (dt * du) -
                                              1 * c[sfxy + s4] / (dt * du);
                        tbl[p, 4 + 1 * 4 + 3] = +(2 * c[sfx + s1] / dt) - 2 * c[sfx + s4] / dt + 1 * c[sfxy + s1] / (dt * du) +
                                              1 * c[sfxy + s4] / (dt * du);
                        tbl[p, 4 + 2 * 4 + 0] = -(3 * c[sf + s1]) + 3 * c[sf + s2] - 2 * c[sfx + s1] / dt - 1 * c[sfx + s2] / dt;
                        tbl[p, 4 + 2 * 4 + 1] = -(3 * c[sfy + s1] / du) + 3 * c[sfy + s2] / du - 2 * c[sfxy + s1] / (dt * du) -
                                              1 * c[sfxy + s2] / (dt * du);
                        tbl[p, 4 + 2 * 4 + 2] = +(9 * c[sf + s1]) - 9 * c[sf + s2] + 9 * c[sf + s3] - 9 * c[sf + s4] +
                                              6 * c[sfx + s1] / dt + 3 * c[sfx + s2] / dt - 3 * c[sfx + s3] / dt - 6 * c[sfx + s4] / dt +
                                              6 * c[sfy + s1] / du - 6 * c[sfy + s2] / du - 3 * c[sfy + s3] / du + 3 * c[sfy + s4] / du +
                                              4 * c[sfxy + s1] / (dt * du) + 2 * c[sfxy + s2] / (dt * du) + 1 * c[sfxy + s3] / (dt * du) +
                                              2 * c[sfxy + s4] / (dt * du);
                        tbl[p, 4 + 2 * 4 + 3] = -(6 * c[sf + s1]) + 6 * c[sf + s2] - 6 * c[sf + s3] + 6 * c[sf + s4] -
                                              4 * c[sfx + s1] / dt - 2 * c[sfx + s2] / dt + 2 * c[sfx + s3] / dt + 4 * c[sfx + s4] / dt -
                                              3 * c[sfy + s1] / du + 3 * c[sfy + s2] / du + 3 * c[sfy + s3] / du - 3 * c[sfy + s4] / du -
                                              2 * c[sfxy + s1] / (dt * du) - 1 * c[sfxy + s2] / (dt * du) - 1 * c[sfxy + s3] / (dt * du) -
                                              2 * c[sfxy + s4] / (dt * du);
                        tbl[p, 4 + 3 * 4 + 0] = +(2 * c[sf + s1]) - 2 * c[sf + s2] + 1 * c[sfx + s1] / dt + 1 * c[sfx + s2] / dt;
                        tbl[p, 4 + 3 * 4 + 1] = +(2 * c[sfy + s1] / du) - 2 * c[sfy + s2] / du + 1 * c[sfxy + s1] / (dt * du) +
                                              1 * c[sfxy + s2] / (dt * du);
                        tbl[p, 4 + 3 * 4 + 2] = -(6 * c[sf + s1]) + 6 * c[sf + s2] - 6 * c[sf + s3] + 6 * c[sf + s4] -
                                              3 * c[sfx + s1] / dt - 3 * c[sfx + s2] / dt + 3 * c[sfx + s3] / dt + 3 * c[sfx + s4] / dt -
                                              4 * c[sfy + s1] / du + 4 * c[sfy + s2] / du + 2 * c[sfy + s3] / du - 2 * c[sfy + s4] / du -
                                              2 * c[sfxy + s1] / (dt * du) - 2 * c[sfxy + s2] / (dt * du) - 1 * c[sfxy + s3] / (dt * du) -
                                              1 * c[sfxy + s4] / (dt * du);
                        tbl[p, 4 + 3 * 4 + 3] = +(4 * c[sf + s1]) - 4 * c[sf + s2] + 4 * c[sf + s3] - 4 * c[sf + s4] +
                                              2 * c[sfx + s1] / dt + 2 * c[sfx + s2] / dt - 2 * c[sfx + s3] / dt - 2 * c[sfx + s4] / dt +
                                              2 * c[sfy + s1] / du - 2 * c[sfy + s2] / du - 2 * c[sfy + s3] / du + 2 * c[sfy + s4] / du +
                                              1 * c[sfxy + s1] / (dt * du) + 1 * c[sfxy + s2] / (dt * du) + 1 * c[sfxy + s3] / (dt * du) +
                                              1 * c[sfxy + s4] / (dt * du);
                    }

                    //
                    // Rescale Cij
                    //
                    for (var ci = 0; ci <= 3; ci++)
                    {
                        for (var cj = 0; cj <= 3; cj++)
                        {
                            tbl[p, 4 + ci * 4 + cj] = tbl[p, 4 + ci * 4 + cj] * M.Pow(dt, ci) * M.Pow(du, cj);
                        }
                    }
                }
            }
        }


        /*************************************************************************
        Линейная замена аргумента двухмерного сплайна

        Входные параметры:
            C       -   массив коэффициентов, вычисленный подпрограммой для
                        построения сплайна S(x).
            AX, BX  -   коэффициенты преобразования x = A*t + B
            AY, BY  -   коэффициенты преобразования y = A*u + B

        Выходные параметры:
            C   -   преобразованный сплайн

          -- ALGLIB PROJECT --
             Copyright 30.06.2007 by Bochkanov Sergey
        *************************************************************************/

        public static void spline2dlintransxy(ref double[] c,
                                              double ax,
                                              double bx,
                                              double ay,
                                              double by)
        {
            int i;
            int j;
            double v;

            var typec = (int)M.Round(c[1]);
            Debug.Assert(typec == -3 | typec == -1, "Spline2DLinTransXY: incorrect C!");
            var n = (int)M.Round(c[2]);
            var m = (int)M.Round(c[3]);
            var x = new double[n - 1 + 1];
            var y = new double[m - 1 + 1];
            var f = new double[m - 1 + 1, n - 1 + 1];
            for (j = 0; j <= n - 1; j++)
            {
                x[j] = c[4 + j];
            }
            for (i = 0; i <= m - 1; i++)
            {
                y[i] = c[4 + n + i];
            }
            for (i = 0; i <= m - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    f[i, j] = c[4 + n + m + i * n + j];
                }
            }

            //
            // Special case: AX=0 or AY=0
            //
            if (ax == 0)
            {
                for (i = 0; i <= m - 1; i++)
                {
                    v = splineinterpolation2d(c, bx, y[i]);
                    for (j = 0; j <= n - 1; j++)
                    {
                        f[i, j] = v;
                    }
                }
                if (typec == -3)
                {
                    buildbicubicspline(x, y, f, m, n, out c);
                }
                if (typec == -1)
                {
                    buildbilinearspline(x, y, f, m, n, out c);
                }
                ax = 1;
                bx = 0;
            }
            if (ay == 0)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    v = splineinterpolation2d(c, x[j], by);
                    for (i = 0; i <= m - 1; i++)
                    {
                        f[i, j] = v;
                    }
                }
                if (typec == -3)
                {
                    buildbicubicspline(x, y, f, m, n, out c);
                }
                if (typec == -1)
                {
                    buildbilinearspline(x, y, f, m, n, out c);
                }
                ay = 1;
                by = 0;
            }

            //
            // General case: AX<>0, AY<>0
            // Unpack, scale and pack again.
            //
            for (j = 0; j <= n - 1; j++)
            {
                x[j] = (x[j] - bx) / ax;
            }
            for (i = 0; i <= m - 1; i++)
            {
                y[i] = (y[i] - by) / ay;
            }
            if (typec == -3)
            {
                buildbicubicspline(x, y, f, m, n, out c);
            }
            if (typec == -1)
            {
                buildbilinearspline(x, y, f, m, n, out c);
            }
        }


        /*************************************************************************
        Линейное преобразование двухмерного сплайна

        Входные параметры:
            C       -   массив коэффициентов, вычисленный подпрограммой для
                        построения сплайна S(x).
            A, B    -   коэффициенты преобразования S2(x,y) = A*S(x,y) + B

        Выходные параметры:
            C   -   преобразованный сплайн

          -- ALGLIB PROJECT --
             Copyright 30.06.2007 by Bochkanov Sergey
        *************************************************************************/

        public static void spline2dlintransf(ref double[] c,
                                             double a,
                                             double b)
        {
            int i;
            int j;

            var typec = (int)M.Round(c[1]);
            Debug.Assert(typec == -3 | typec == -1, "Spline2DLinTransXY: incorrect C!");
            var n = (int)M.Round(c[2]);
            var m = (int)M.Round(c[3]);
            var x = new double[n - 1 + 1];
            var y = new double[m - 1 + 1];
            var f = new double[m - 1 + 1, n - 1 + 1];
            for (j = 0; j <= n - 1; j++)
            {
                x[j] = c[4 + j];
            }
            for (i = 0; i <= m - 1; i++)
            {
                y[i] = c[4 + n + i];
            }
            for (i = 0; i <= m - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    f[i, j] = a * c[4 + n + m + i * n + j] + b;
                }
            }
            if (typec == -3)
            {
                buildbicubicspline(x, y, f, m, n, out c);
            }
            if (typec == -1)
            {
                buildbilinearspline(x, y, f, m, n, out c);
            }
        }


        /*************************************************************************
        Копирование двухмерного сплайна.

        Входные параметры:
            C   -   массив коэффициентов, вычисленный подпрограммой для
                    построения сплайна.

        Выходные параметры:
            CC  -   копия сплайна

          -- ALGLIB PROJECT --
             Copyright 29.06.2007 by Bochkanov Sergey
        *************************************************************************/

        public static void spline2dcopy(ref double[] c,
                                        ref double[] cc)
        {
            spline3.splinecopy(ref c, ref cc);
        }


        /*************************************************************************
        Ресэмплирование бикубическим сплайном.

            Процедура получает значения  функции  на  сетке  OldWidth*OldHeight  и
        путем интерполяции бикубическим  сплайном  вычисляет  значения  функции  в
        узлах Декартовой сетки размером NewWidth*NewHeight. Новая сетка может быть
        как более, так и менее плотная, чем старая.

        Входные параметры:
            A           - массив значений функции на старой сетке.
                          Нумерация элементов [0..OldHeight-1,
                          0..OldWidth-1]
            OldHeight   - старый размер сетки, OldHeight>1
            OldWidth    - старый размер сетки, OldWidth>1
            NewHeight   - новый размер сетки, NewHeight>1
            NewWidth    - новый размер сетки, NewWidth>1

        Выходные параметры:
            B           - массив значений функции на новой сетке.
                          Нумерация элементов [0..NewHeight-1,
                          0..NewWidth-1]

          -- ALGLIB routine --
             15 May, 2007
             Copyright by Bochkanov Sergey
        *************************************************************************/

        public static void bicubicresamplecartesian(ref double[,] a,
                                                    int oldheight,
                                                    int oldwidth,
                                                    ref double[,] b,
                                                    int newheight,
                                                    int newwidth)
        {
            var c = new double[0];
            int i;
            int j;

            Debug.Assert(oldwidth > 1 & oldheight > 1, "BicubicResampleCartesian: width/height less than 1");
            Debug.Assert(newwidth > 1 & newheight > 1, "BicubicResampleCartesian: width/height less than 1");

            //
            // Prepare
            //
            var mw = M.Max(oldwidth, newwidth);
            var mh = M.Max(oldheight, newheight);
            b = new double[newheight - 1 + 1, newwidth - 1 + 1];
            var buf = new double[oldheight - 1 + 1, newwidth - 1 + 1];
            var x = new double[M.Max(mw, mh) - 1 + 1];
            var y = new double[M.Max(mw, mh) - 1 + 1];

            //
            // Horizontal interpolation
            //
            for (i = 0; i <= oldheight - 1; i++)
            {
                //
                // Fill X, Y
                //
                for (j = 0; j <= oldwidth - 1; j++)
                {
                    x[j] = (j) / ((double)(oldwidth - 1));
                    y[j] = a[i, j];
                }

                //
                // Interpolate and place result into temporary matrix
                //
                spline3.buildcubicspline(x, y, oldwidth, 0, 0.0, 0, 0.0, ref c);
                for (j = 0; j <= newwidth - 1; j++)
                {
                    buf[i, j] = spline3.splineinterpolation(ref c, (j) / ((double)(newwidth - 1)));
                }
            }

            //
            // Vertical interpolation
            //
            for (j = 0; j <= newwidth - 1; j++)
            {
                //
                // Fill X, Y
                //
                for (i = 0; i <= oldheight - 1; i++)
                {
                    x[i] = (i) / ((double)(oldheight - 1));
                    y[i] = buf[i, j];
                }

                //
                // Interpolate and place result into B
                //
                spline3.buildcubicspline(x, y, oldheight, 0, 0.0, 0, 0.0, ref c);
                for (i = 0; i <= newheight - 1; i++)
                {
                    b[i, j] = spline3.splineinterpolation(ref c, (i) / ((double)(newheight - 1)));
                }
            }
        }


        /*************************************************************************
        Ресэмплирование билинейным сплайном.

            Процедура получает значения  функции  на  сетке  OldWidth*OldHeight  и
        путем интерполяции билинейным  сплайном  вычисляет  значения   функции   в
        узлах Декартовой сетки размером NewWidth*NewHeight. Новая сетка может быть
        как более, так и менее плотная, чем старая.

        Входные параметры:
            A           - массив значений функции на старой сетке.
                          Нумерация элементов [0..OldHeight-1,
                          0..OldWidth-1]
            OldHeight   - старый размер сетки, OldHeight>1
            OldWidth    - старый размер сетки, OldWidth>1
            NewHeight   - новый размер сетки, NewHeight>1
            NewWidth    - новый размер сетки, NewWidth>1

        Выходные параметры:
            B           - массив значений функции на новой сетке.
                          Нумерация элементов [0..NewHeight-1,
                          0..NewWidth-1]

          -- ALGLIB routine --
             09.07.2007
             Copyright by Bochkanov Sergey
        *************************************************************************/

        public static void bilinearresamplecartesian(ref double[,] a,
                                                     int oldheight,
                                                     int oldwidth,
                                                     ref double[,] b,
                                                     int newheight,
                                                     int newwidth)
        {
            int i;

            b = new double[newheight - 1 + 1, newwidth - 1 + 1];
            for (i = 0; i <= newheight - 1; i++)
            {
                for (var j = 0; j <= newwidth - 1; j++)
                {
                    var l = i * (oldheight - 1) / (newheight - 1);
                    if (l == oldheight - 1)
                    {
                        l = oldheight - 2;
                    }
                    var u = (i) / ((double)(newheight - 1)) * (oldheight - 1) - l;
                    var c = j * (oldwidth - 1) / (newwidth - 1);
                    if (c == oldwidth - 1)
                    {
                        c = oldwidth - 2;
                    }
                    var t = (j * (oldwidth - 1)) / ((double)(newwidth - 1)) - c;
                    b[i, j] = (1 - t) * (1 - u) * a[l, c] + t * (1 - u) * a[l, c + 1] + t * u * a[l + 1, c + 1] + (1 - t) * u * a[l + 1, c];
                }
            }
        }


        /*************************************************************************
        Obsolete subroutine for backwards compatibility
        *************************************************************************/

        public static void bicubicresample(int oldwidth,
                                           int oldheight,
                                           int newwidth,
                                           int newheight,
                                           ref double[,] a,
                                           ref double[,] b)
        {
            bicubicresamplecartesian(ref a, oldheight, oldwidth, ref b, newheight, newwidth);
        }


        /*************************************************************************
        Obsolete subroutine for backwards compatibility
        *************************************************************************/

        public static void bilinearresample(int oldwidth,
                                            int oldheight,
                                            int newwidth,
                                            int newheight,
                                            ref double[,] a,
                                            ref double[,] b)
        {
            bilinearresamplecartesian(ref a, oldheight, oldwidth, ref b, newheight, newwidth);
        }


        /*************************************************************************
        Internal subroutine.
        Calculation of the first derivatives and the cross-derivative.
        *************************************************************************/

        private static void bicubiccalcderivatives(double[,] a, double[] x, double[] y, int m, int n, ref double[,] dx, ref double[,] dy, ref double[,] dxy)
        {
            int i;
            int j;
            var c = new double[0];
            double s;
            double ds;
            double d2s;

            dx = new double[m - 1 + 1, n - 1 + 1];
            dy = new double[m - 1 + 1, n - 1 + 1];
            dxy = new double[m - 1 + 1, n - 1 + 1];

            //
            // dF/dX
            //
            var xt = new double[n - 1 + 1];
            var ft = new double[n - 1 + 1];
            for (i = 0; i <= m - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    xt[j] = x[j];
                    ft[j] = a[i, j];
                }
                spline3.buildcubicspline(xt, ft, n, 0, 0.0, 0, 0.0, ref c);
                for (j = 0; j <= n - 1; j++)
                {
                    spline3.splinedifferentiation(c, x[j], out s, out ds, out d2s);
                    dx[i, j] = ds;
                }
            }

            //
            // dF/dY
            //
            xt = new double[m - 1 + 1];
            ft = new double[m - 1 + 1];
            for (j = 0; j <= n - 1; j++)
            {
                for (i = 0; i <= m - 1; i++)
                {
                    xt[i] = y[i];
                    ft[i] = a[i, j];
                }
                spline3.buildcubicspline(xt, ft, m, 0, 0.0, 0, 0.0, ref c);
                for (i = 0; i <= m - 1; i++)
                {
                    spline3.splinedifferentiation(c, y[i], out s, out ds, out d2s);
                    dy[i, j] = ds;
                }
            }

            //
            // d2F/dXdY
            //
            xt = new double[n - 1 + 1];
            ft = new double[n - 1 + 1];
            for (i = 0; i <= m - 1; i++)
            {
                for (j = 0; j <= n - 1; j++)
                {
                    xt[j] = x[j];
                    ft[j] = dy[i, j];
                }
                spline3.buildcubicspline(xt, ft, n, 0, 0.0, 0, 0.0, ref c);
                for (j = 0; j <= n - 1; j++)
                {
                    spline3.splinedifferentiation(c, x[j], out s, out ds, out d2s);
                    dxy[i, j] = ds;
                }
            }
        }
    }
}