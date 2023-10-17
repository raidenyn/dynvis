using M=System.Math;

namespace DynVis.Math
{

    public delegate void funcvecjac(ref double[] x,
                                ref double[] fvec,
                                ref double[,] fjac,
                                ref int iflag);

    internal class levenbergmarquardt
    {

        /*************************************************************************
    Подпрограмма минимизирует сумму квадратов M нелинейных функций N аргументов
    при помощи модификации алгоритма  Левенберга-Марквардта  с  использованием
    информации о значении функции и её якобиане.

    Программист должен определить подпрограмму FuncVecJac,  которая  принимает
    массив X (массив с нумерацией элементов от 1  до  N, хранящий  аргументы),
    и, если IFlag:
        * равно 1, возвращает  вектор  значений  функций   в  массиве FVec  (в
          элементах с номерами от 1 до M), не меняя массив FJac.
        * равно 2, возвращает якобиан в массиве FJac (в элементах  с  номерами
          [1..M,1..N]), не меняя массив FVec.
    Подпрограмма  может  изменить  параметр  IFlag,  установив   его  в  любое
    отрицательное число, что досрочно прекратит работу алгоритма.

    Программист может переопределить подпрограмму LevenbergMarquardtNewIteration,
    которая вызывается на каждой новой итерации алгоритма и в которую передается
    текущая точка X. Подпрограмму имеет смысл переопределять в отладочных целях,
    например, для визуализации процесса поиска решения.

    Подпрограмма AdditionalLevenbergMarquardtStoppingCriterion может переопре-
    деляться для модификации условий останова функций.

    Входные параметры:
        N       -   число неизвестных, N>0.
        M       -   число суммируемых функций, M>=N.
        X       -   начальное приближение к решению.
                    Массив с нумерацией элементов [1..N]
        EpsG    -   условие остановки.  Итерации  прекращаются,  если  косинус
                    угла между вектором значений функций и каждым  из столбцов
                    якобиана не превосходит EpsG по абсолютной величине.
                    Фактически,  эта  величина  задает  условие  остановки  по
                    незначительности градиента функции.
        EpsF    -   условие остановки. Итерации прекращаются, если относительное
                    уменьшение суммы квадратов функций (реальное и предсказанное
                    на основе экстраполяции) не превосходит EpsF.
        EpsX    -   условие остановки. Итерации прекращаются, если относительное
                    изменение текущего приближения не превосходит EpsX.
        MaxIts  -   условие остановки. Итерации прекращаются, если их количество
                    превысило MaxIts.

    Выхо
        X       -   результирующее приближение к решению
                    Массив с нумерацией элементов [1..N]
        Info    -   причина прекращения работы подпрограммы:
                        * -1    указаны неверные параметры
                        *  0    прервано пользователем.
                        *  1    относительное уменьшение суммы квадратов функций
                                (реальное и предсказанное на основе экстраполяции)
                                не превосходит EpsF.
                        *  2    относительное изменение текущего приближения  не
                                превосходит EpsX.
                        *  3    и условие (1), и условие (2) выполняются.
                        *  4    косинус угла между вектором значений функций и
                                каждым  из столбцов  якобиана  не  превосходит
                                EpsG по абсолютной величине.
                        *  5    превышено максимальное число итераций MaxIts
                        *  6    EpsF слишком мало.  Дальшейшее  улучшение  суммы
                                квадратов функций невозможно.
                        *  7    EpsX слишком мало. Дальнейшее улучшение  решения
                                невозможно.
                        *  8    EpsG слишком мало.  Вектор  функций  ортогонален
                                столбцам якобиана с точностью, близкой к машинной.
    argonne national laboratory. minpack project. march 1980.
    burton s. garbow, kenneth e. hillstrom, jorge j. more

    Contributors:
        * Sergey Bochkanov (ALGLIB project). Translation from FORTRAN to
          pseudocode.
    *************************************************************************/

        public static void levenbergmarquardtminimize(int n,
                                                      int m,
                                                      ref double[] x,
                                                      double epsg,
                                                      double epsf,
                                                      double epsx,
                                                      int maxits,
                                                      funcvecjac funcvecjac,
                                                      ref int info)
        {
            double factor;
            int i;
            int j;
            int l;
            double actred;
            double delta = 0;
            double dirder;
            double fnorm;
            double fnorm1;
            double gnorm;
            double par;
            double pnorm;
            double prered;
            double ratio;
            double sum;
            double temp = 0;
            double temp1;
            double temp2;
            double xnorm = 0;
            int i_;


            //
            // Factor is a positive input variable used in determining the
            // initial step bound. This bound is set to the product of
            // factor and the euclidean norm of diag*x if nonzero, or else
            // to factor itself. in most cases factor should lie in the
            // interval (.1,100.).
            // 100.0 is a generally recommended value.
            //
            factor = 100.0;

            //
            // mode is an integer input variable. if mode = 1, the
            // variables will be scaled internally. if mode = 2,
            // the scaling is specified by the input diag. other
            // values of mode are equivalent to mode = 1.
            //
            const int mode = 1;

            //
            // diag is an array of length n. if mode = 1
            // diag is internally set. if mode = 2, diag
            // must contain positive entries that serve as
            // multiplicative scale factors for the variables.
            //
            var diag = new double[n + 1];

            //
            // Initialization
            //
            var qtf = new double[n + 1];
            var fvec = new double[m + 1];
            var fjac = new double[m + 1, n + 1];
            var w2 = new double[n + 1, m + 1];
            var ipvt = new int[n + 1];
            var wa1 = new double[n + 1];
            var wa2 = new double[n + 1];
            var wa3 = new double[n + 1];
            var wa4 = new double[m + 1];
            const double p1 = 1.0E-1;
            const double p5 = 5.0E-1;
            const double p25 = 2.5E-1;
            const double p75 = 7.5E-1;
            const double p0001 = 1.0E-4;
            info = 0;
            int njev = 0;

            //
            // check the input parameters for errors.
            //
            if (n <= 0 | m < n)
            {
                info = -1;
                return;
            }
            if (epsf < 0 | epsx < 0 | epsg < 0)
            {
                info = -1;
                return;
            }
            if (factor <= 0)
            {
                info = -1;
                return;
            }


            //
            // evaluate the function at the starting point
            // and calculate its norm.
            //
            int iflag = 1;
            funcvecjac(ref x, ref fvec, ref fjac, ref iflag);
            int nfev = 1;
            if (iflag < 0)
            {
                info = 0;
                return;
            }
            fnorm = 0.0;
            for (i_ = 1; i_ <= m; i_++)
            {
                fnorm += fvec[i_] * fvec[i_];
            }
            fnorm = M.Sqrt(fnorm);

            //
            // initialize levenberg-marquardt parameter and iteration counter.
            //
            par = 0;
            int iter = 1;

            //
            // beginning of the outer loop.
            //
            while (true)
            {
                //
                // New iteration
                //
                levenbergmarquardtnewiteration(ref x);

                //
                // calculate the jacobian matrix.
                //
                iflag = 2;
                funcvecjac(ref x, ref fvec, ref fjac, ref iflag);
                njev = njev + 1;
                if (iflag < 0)
                {
                    info = 0;
                    return;
                }

                //
                // compute the qr factorization of the jacobian.
                //
                levenbergmarquardtqrfac(m, n, ref fjac, true, ref ipvt, ref wa1, ref wa2, ref wa3, ref w2);

                //
                // on the first iteration and if mode is 1, scale according
                // to the norms of the columns of the initial jacobian.
                //
                if (iter == 1)
                {
                    if (mode != 2)
                    {
                        for (j = 1; j <= n; j++)
                        {
                            diag[j] = wa2[j];
                            if (wa2[j] == 0)
                            {
                                diag[j] = 1;
                            }
                        }
                    }

                    //
                    // on the first iteration, calculate the norm of the scaled x
                    // and initialize the step bound delta.
                    //
                    for (j = 1; j <= n; j++)
                    {
                        wa3[j] = diag[j] * x[j];
                    }
                    xnorm = 0.0;
                    for (i_ = 1; i_ <= n; i_++)
                    {
                        xnorm += wa3[i_] * wa3[i_];
                    }
                    xnorm = M.Sqrt(xnorm);
                    delta = factor * xnorm;
                    if (delta == 0)
                    {
                        delta = factor;
                    }
                }

                //
                // form (q transpose)*fvec and store the first n components in
                // qtf.
                //
                for (i = 1; i <= m; i++)
                {
                    wa4[i] = fvec[i];
                }
                for (j = 1; j <= n; j++)
                {
                    if (fjac[j, j] != 0)
                    {
                        sum = 0;
                        for (i = j; i <= m; i++)
                        {
                            sum = sum + fjac[i, j] * wa4[i];
                        }
                        temp = -(sum / fjac[j, j]);
                        for (i = j; i <= m; i++)
                        {
                            wa4[i] = wa4[i] + fjac[i, j] * temp;
                        }
                    }
                    fjac[j, j] = wa1[j];
                    qtf[j] = wa4[j];
                }

                //
                // compute the norm of the scaled gradient.
                //
                gnorm = 0;
                if (fnorm != 0)
                {
                    for (j = 1; j <= n; j++)
                    {
                        l = ipvt[j];
                        if (wa2[l] != 0)
                        {
                            sum = 0;
                            for (i = 1; i <= j; i++)
                            {
                                sum = sum + fjac[i, j] * (qtf[i] / fnorm);
                            }
                            gnorm = M.Max(gnorm, M.Abs(sum / wa2[l]));
                        }
                    }
                }

                //
                // test for convergence of the gradient norm.
                //
                if (gnorm <= epsg)
                {
                    info = 4;
                }
                if (info != 0)
                {
                    return;
                }

                //
                // rescale if necessary.
                //
                if (mode != 2)
                {
                    for (j = 1; j <= n; j++)
                    {
                        diag[j] = M.Max(diag[j], wa2[j]);
                    }
                }

                //
                // beginning of the inner loop.
                //
                while (true)
                {
                    //
                    // determine the levenberg-marquardt parameter.
                    //
                    levenbergmarquardtpar(n, ref fjac, ref ipvt, ref diag, ref qtf, delta, ref par, ref wa1, ref wa2,
                                          ref wa3, ref wa4);

                    //
                    // store the direction p and x + p. calculate the norm of p.
                    //
                    for (j = 1; j <= n; j++)
                    {
                        wa1[j] = -wa1[j];
                        wa2[j] = x[j] + wa1[j];
                        wa3[j] = diag[j] * wa1[j];
                    }
                    pnorm = 0.0;
                    for (i_ = 1; i_ <= n; i_++)
                    {
                        pnorm += wa3[i_] * wa3[i_];
                    }
                    pnorm = M.Sqrt(pnorm);

                    //
                    // on the first iteration, adjust the initial step bound.
                    //
                    if (iter == 1)
                    {
                        delta = M.Min(delta, pnorm);
                    }

                    //
                    // evaluate the function at x + p and calculate its norm.
                    //
                    iflag = 1;
                    funcvecjac(ref wa2, ref wa4, ref fjac, ref iflag);
                    nfev = nfev + 1;
                    if (iflag < 0)
                    {
                        info = 0;
                        return;
                    }
                    fnorm1 = 0.0;
                    for (i_ = 1; i_ <= m; i_++)
                    {
                        fnorm1 += wa4[i_] * wa4[i_];
                    }
                    fnorm1 = M.Sqrt(fnorm1);

                    //
                    // compute the scaled actual reduction.
                    //
                    actred = -1;
                    if (p1 * fnorm1 < fnorm)
                    {
                        actred = 1 - AP.Math.Sqr(fnorm1 / fnorm);
                    }

                    //
                    // compute the scaled predicted reduction and
                    // the scaled directional derivative.
                    //
                    for (j = 1; j <= n; j++)
                    {
                        wa3[j] = 0;
                        l = ipvt[j];
                        temp = wa1[l];
                        for (i = 1; i <= j; i++)
                        {
                            wa3[i] = wa3[i] + fjac[i, j] * temp;
                        }
                    }
                    temp1 = 0.0;
                    for (i_ = 1; i_ <= n; i_++)
                    {
                        temp1 += wa3[i_] * wa3[i_];
                    }
                    temp1 = M.Sqrt(temp1) / fnorm;
                    temp2 = M.Sqrt(par) * pnorm / fnorm;
                    prered = AP.Math.Sqr(temp1) + AP.Math.Sqr(temp2) / p5;
                    dirder = -(AP.Math.Sqr(temp1) + AP.Math.Sqr(temp2));

                    //
                    // compute the ratio of the actual to the predicted
                    // reduction.
                    //
                    ratio = 0;
                    if (prered != 0)
                    {
                        ratio = actred / prered;
                    }

                    //
                    // update the step bound.
                    //
                    if (ratio > p25)
                    {
                        if (par == 0 | ratio >= p75)
                        {
                            delta = pnorm / p5;
                            par = p5 * par;
                        }
                    }
                    else
                    {
                        if (actred >= 0)
                        {
                            temp = p5;
                        }
                        if (actred < 0)
                        {
                            temp = p5 * dirder / (dirder + p5 * actred);
                        }
                        if (p1 * fnorm1 >= fnorm | temp < p1)
                        {
                            temp = p1;
                        }
                        delta = temp * M.Min(delta, pnorm / p1);
                        par = par / temp;
                    }

                    //
                    // test for successful iteration.
                    //
                    if (ratio >= p0001)
                    {
                        //
                        // successful iteration. update x, fvec, and their norms.
                        //
                        for (j = 1; j <= n; j++)
                        {
                            x[j] = wa2[j];
                            wa2[j] = diag[j] * x[j];
                        }
                        for (i = 1; i <= m; i++)
                        {
                            fvec[i] = wa4[i];
                        }
                        xnorm = 0.0;
                        for (i_ = 1; i_ <= n; i_++)
                        {
                            xnorm += wa2[i_] * wa2[i_];
                        }
                        xnorm = M.Sqrt(xnorm);
                        fnorm = fnorm1;
                        iter = iter + 1;
                    }

                    //
                    // tests for convergence.
                    //
                    if (M.Abs(actred) <= epsf & prered <= epsf & p5 * ratio <= 1)
                    {
                        info = 1;
                    }
                    if (delta <= epsx * xnorm)
                    {
                        info = 2;
                    }
                    if (M.Abs(actred) <= epsf & prered <= epsf & p5 * ratio <= 1 & info == 2)
                    {
                        info = 3;
                    }
                    if (info != 0)
                    {
                        return;
                    }

                    //
                    // tests for termination and stringent tolerances.
                    //
                    if (iter >= maxits & maxits > 0)
                    {
                        info = 5;
                    }
                    if (M.Abs(actred) <= AP.Math.MachineEpsilon & prered <= AP.Math.MachineEpsilon & p5 * ratio <= 1)
                    {
                        info = 6;
                    }
                    if (delta <= AP.Math.MachineEpsilon * xnorm)
                    {
                        info = 7;
                    }
                    if (gnorm <= AP.Math.MachineEpsilon)
                    {
                        info = 8;
                    }
                    if (info != 0)
                    {
                        return;
                    }

                    //
                    // end of the inner loop. repeat if iteration unsuccessful.
                    //
                    if (ratio < p0001)
                    {
                        continue;
                    }
                    break;
                }

                //
                // Termination criterion
                //
                if (additionallevenbergmarquardtstoppingcriterion(iter))
                {
                    info = 0;
                    return;
                }

                //
                // end of the outer loop.
                //
            }
        }


        /*************************************************************************
        QR-decomposition with column pivoting

        argonne national laboratory. minpack project. march 1980.
        burton s. garbow, kenneth e. hillstrom, jorge j. more
        *************************************************************************/

        private static void levenbergmarquardtqrfac(int m,
                                                    int n,
                                                    ref double[,] a,
                                                    bool pivot,
                                                    ref int[] ipvt,
                                                    ref double[] rdiag,
                                                    ref double[] acnorm,
                                                    ref double[] wa,
                                                    ref double[,] w2)
        {
            int i;
            int j;
            int jp1;
            int k;
            int kmax;
            int minmn;
            double ajnorm;
            double sum;
            double temp;
            double v;
            int i_;


            //
            // Copy from a to w2 and transpose
            //
            for (i = 1; i <= m; i++)
            {
                for (i_ = 1; i_ <= n; i_++)
                {
                    w2[i_, i] = a[i, i_];
                }
            }

            //
            // compute the initial column norms and initialize several arrays.
            //
            for (j = 1; j <= n; j++)
            {
                v = 0.0;
                for (i_ = 1; i_ <= m; i_++)
                {
                    v += w2[j, i_] * w2[j, i_];
                }
                acnorm[j] = M.Sqrt(v);
                rdiag[j] = acnorm[j];
                wa[j] = rdiag[j];
                if (pivot)
                {
                    ipvt[j] = j;
                }
            }

            //
            // reduce a to r with householder transformations.
            //
            minmn = M.Min(m, n);
            for (j = 1; j <= minmn; j++)
            {
                if (pivot)
                {
                    //
                    // bring the column of largest norm into the pivot position.
                    //
                    kmax = j;
                    for (k = j; k <= n; k++)
                    {
                        if (rdiag[k] > rdiag[kmax])
                        {
                            kmax = k;
                        }
                    }
                    if (kmax != j)
                    {
                        for (i = 1; i <= m; i++)
                        {
                            temp = w2[j, i];
                            w2[j, i] = w2[kmax, i];
                            w2[kmax, i] = temp;
                        }
                        rdiag[kmax] = rdiag[j];
                        wa[kmax] = wa[j];
                        k = ipvt[j];
                        ipvt[j] = ipvt[kmax];
                        ipvt[kmax] = k;
                    }
                }

                //
                // compute the householder transformation to reduce the
                // j-th column of a to a multiple of the j-th unit vector.
                //
                v = 0.0;
                for (i_ = j; i_ <= m; i_++)
                {
                    v += w2[j, i_] * w2[j, i_];
                }
                ajnorm = M.Sqrt(v);
                if (ajnorm != 0)
                {
                    if (w2[j, j] < 0)
                    {
                        ajnorm = -ajnorm;
                    }
                    v = 1 / ajnorm;
                    for (i_ = j; i_ <= m; i_++)
                    {
                        w2[j, i_] = v * w2[j, i_];
                    }
                    w2[j, j] = w2[j, j] + 1.0;

                    //
                    // apply the transformation to the remaining columns
                    // and update the norms.
                    //
                    jp1 = j + 1;
                    if (n >= jp1)
                    {
                        for (k = jp1; k <= n; k++)
                        {
                            sum = 0.0;
                            for (i_ = j; i_ <= m; i_++)
                            {
                                sum += w2[j, i_] * w2[k, i_];
                            }
                            temp = sum / w2[j, j];
                            for (i_ = j; i_ <= m; i_++)
                            {
                                w2[k, i_] = w2[k, i_] - temp * w2[j, i_];
                            }
                            if (pivot & rdiag[k] != 0)
                            {
                                temp = w2[k, j] / rdiag[k];
                                rdiag[k] = rdiag[k] * M.Sqrt(M.Max(0, 1 - AP.Math.Sqr(temp)));
                                if (0.05 * AP.Math.Sqr(rdiag[k] / wa[k]) <= AP.Math.MachineEpsilon)
                                {
                                    v = 0.0;
                                    for (i_ = jp1; i_ <= jp1 + m - j - 1; i_++)
                                    {
                                        v += w2[k, i_] * w2[k, i_];
                                    }
                                    rdiag[k] = M.Sqrt(v);
                                    wa[k] = rdiag[k];
                                }
                            }
                        }
                    }
                }
                rdiag[j] = -ajnorm;
            }

            //
            // Copy from w2 to a and transpose
            //
            for (i = 1; i <= m; i++)
            {
                for (i_ = 1; i_ <= n; i_++)
                {
                    a[i, i_] = w2[i_, i];
                }
            }
        }


        /*************************************************************************
        given an m by n matrix a, an n by n diagonal matrix d,
        and an m-vector b, the problem is to determine an x which
        solves the system

            a*x = b ,     d*x = 0 ,

        in the least squares sense.

        argonne national laboratory. minpack project. march 1980.
        burton s. garbow, kenneth e. hillstrom, jorge j. more
        *************************************************************************/

        private static void levenbergmarquardtqrsolv(int n,
                                                     ref double[,] r,
                                                     ref int[] ipvt,
                                                     ref double[] diag,
                                                     ref double[] qtb,
                                                     ref double[] x,
                                                     ref double[] sdiag,
                                                     ref double[] wa)
        {
            int i;
            int j;
            int jp1;
            int k;
            int kp1;
            int l;
            int nsing;
            double cs;
            double ct;
            double qtbpj;
            double sn;
            double sum;
            double t;
            double temp;


            //
            // copy r and (q transpose)*b to preserve input and initialize s.
            // in particular, save the diagonal elements of r in x.
            //
            for (j = 1; j <= n; j++)
            {
                for (i = j; i <= n; i++)
                {
                    r[i, j] = r[j, i];
                }
                x[j] = r[j, j];
                wa[j] = qtb[j];
            }

            //
            // eliminate the diagonal matrix d using a givens rotation.
            //
            for (j = 1; j <= n; j++)
            {
                //
                // prepare the row of d to be eliminated, locating the
                // diagonal element using p from the qr factorization.
                //
                l = ipvt[j];
                if (diag[l] != 0)
                {
                    for (k = j; k <= n; k++)
                    {
                        sdiag[k] = 0;
                    }
                    sdiag[j] = diag[l];

                    //
                    // the transformations to eliminate the row of d
                    // modify only a single element of (q transpose)*b
                    // beyond the first n, which is initially zero.
                    //
                    qtbpj = 0;
                    for (k = j; k <= n; k++)
                    {
                        //
                        // determine a givens rotation which eliminates the
                        // appropriate element in the current row of d.
                        //
                        if (sdiag[k] != 0)
                        {
                            if (M.Abs(r[k, k]) >= M.Abs(sdiag[k]))
                            {
                                t = sdiag[k] / r[k, k];
                                cs = 0.5 / M.Sqrt(0.25 + 0.25 * AP.Math.Sqr(t));
                                sn = cs * t;
                            }
                            else
                            {
                                ct = r[k, k] / sdiag[k];
                                sn = 0.5 / M.Sqrt(0.25 + 0.25 * AP.Math.Sqr(ct));
                                cs = sn * ct;
                            }

                            //
                            // compute the modified diagonal element of r and
                            // the modified element of ((q transpose)*b,0).
                            //
                            r[k, k] = cs * r[k, k] + sn * sdiag[k];
                            temp = cs * wa[k] + sn * qtbpj;
                            qtbpj = -(sn * wa[k]) + cs * qtbpj;
                            wa[k] = temp;

                            //
                            // accumulate the tranformation in the row of s.
                            //
                            kp1 = k + 1;
                            if (n >= kp1)
                            {
                                for (i = kp1; i <= n; i++)
                                {
                                    temp = cs * r[i, k] + sn * sdiag[i];
                                    sdiag[i] = -(sn * r[i, k]) + cs * sdiag[i];
                                    r[i, k] = temp;
                                }
                            }
                        }
                    }
                }

                //
                // store the diagonal element of s and restore
                // the corresponding diagonal element of r.
                //
                sdiag[j] = r[j, j];
                r[j, j] = x[j];
            }

            //
            // solve the triangular system for z. if the system is
            // singular, then obtain a least squares solution.
            //
            nsing = n;
            for (j = 1; j <= n; j++)
            {
                if (sdiag[j] == 0 & nsing == n)
                {
                    nsing = j - 1;
                }
                if (nsing < n)
                {
                    wa[j] = 0;
                }
            }
            if (nsing >= 1)
            {
                for (k = 1; k <= nsing; k++)
                {
                    j = nsing - k + 1;
                    sum = 0;
                    jp1 = j + 1;
                    if (nsing >= jp1)
                    {
                        for (i = jp1; i <= nsing; i++)
                        {
                            sum = sum + r[i, j] * wa[i];
                        }
                    }
                    wa[j] = (wa[j] - sum) / sdiag[j];
                }
            }

            //
            // permute the components of z back to components of x.
            //
            for (j = 1; j <= n; j++)
            {
                l = ipvt[j];
                x[l] = wa[j];
            }
        }


        /*************************************************************************
        given an m by n matrix a, an n by n nonsingular diagonal
        matrix d, an m-vector b, and a positive number delta,
        the problem is to determine a value for the parameter
        par such that if x solves the system

            a*x = b ,     sqrt(par)*d*x = 0 ,

        in the least squares sense, and dxnorm is the euclidean
        norm of d*x, then either par is zero and

            (dxnorm-delta) .le. 0.1*delta ,

        or par is positive and

            abs(dxnorm-delta) .le. 0.1*delta .

        argonne national laboratory. minpack project. march 1980.
        burton s. garbow, kenneth e. hillstrom, jorge j. more
        *************************************************************************/

        private static void levenbergmarquardtpar(int n,
                                                  ref double[,] r,
                                                  ref int[] ipvt,
                                                  ref double[] diag,
                                                  ref double[] qtb,
                                                  double delta,
                                                  ref double par,
                                                  ref double[] x,
                                                  ref double[] sdiag,
                                                  ref double[] wa1,
                                                  ref double[] wa2)
        {
            int i;
            int iter;
            int j;
            int jm1;
            int jp1;
            int k;
            int l;
            int nsing;
            double dxnorm;
            double dwarf;
            double fp;
            double gnorm;
            double parc;
            double parl;
            double paru;
            double sum;
            double temp;
            double v;
            int i_;

            dwarf = AP.Math.MinRealNumber;

            //
            // compute and store in x the gauss-newton direction. if the
            // jacobian is rank-deficient, obtain a least squares solution.
            //
            nsing = n;
            for (j = 1; j <= n; j++)
            {
                wa1[j] = qtb[j];
                if (r[j, j] == 0 & nsing == n)
                {
                    nsing = j - 1;
                }
                if (nsing < n)
                {
                    wa1[j] = 0;
                }
            }
            if (nsing >= 1)
            {
                for (k = 1; k <= nsing; k++)
                {
                    j = nsing - k + 1;
                    wa1[j] = wa1[j] / r[j, j];
                    temp = wa1[j];
                    jm1 = j - 1;
                    if (jm1 >= 1)
                    {
                        for (i = 1; i <= jm1; i++)
                        {
                            wa1[i] = wa1[i] - r[i, j] * temp;
                        }
                    }
                }
            }
            for (j = 1; j <= n; j++)
            {
                l = ipvt[j];
                x[l] = wa1[j];
            }

            //
            // initialize the iteration counter.
            // evaluate the function at the origin, and test
            // for acceptance of the gauss-newton direction.
            //
            iter = 0;
            for (j = 1; j <= n; j++)
            {
                wa2[j] = diag[j] * x[j];
            }
            v = 0.0;
            for (i_ = 1; i_ <= n; i_++)
            {
                v += wa2[i_] * wa2[i_];
            }
            dxnorm = M.Sqrt(v);
            fp = dxnorm - delta;
            if (fp <= 0.1 * delta)
            {
                //
                // termination.
                //
                if (iter == 0)
                {
                    par = 0;
                }
                return;
            }

            //
            // if the jacobian is not rank deficient, the newton
            // step provides a lower bound, parl, for the zero of
            // the function. otherwise set this bound to zero.
            //
            parl = 0;
            if (nsing >= n)
            {
                for (j = 1; j <= n; j++)
                {
                    l = ipvt[j];
                    wa1[j] = diag[l] * (wa2[l] / dxnorm);
                }
                for (j = 1; j <= n; j++)
                {
                    sum = 0;
                    jm1 = j - 1;
                    if (jm1 >= 1)
                    {
                        for (i = 1; i <= jm1; i++)
                        {
                            sum = sum + r[i, j] * wa1[i];
                        }
                    }
                    wa1[j] = (wa1[j] - sum) / r[j, j];
                }
                v = 0.0;
                for (i_ = 1; i_ <= n; i_++)
                {
                    v += wa1[i_] * wa1[i_];
                }
                temp = M.Sqrt(v);
                parl = fp / delta / temp / temp;
            }

            //
            // calculate an upper bound, paru, for the zero of the function.
            //
            for (j = 1; j <= n; j++)
            {
                sum = 0;
                for (i = 1; i <= j; i++)
                {
                    sum = sum + r[i, j] * qtb[i];
                }
                l = ipvt[j];
                wa1[j] = sum / diag[l];
            }
            v = 0.0;
            for (i_ = 1; i_ <= n; i_++)
            {
                v += wa1[i_] * wa1[i_];
            }
            gnorm = M.Sqrt(v);
            paru = gnorm / delta;
            if (paru == 0)
            {
                paru = dwarf / M.Min(delta, 0.1);
            }

            //
            // if the input par lies outside of the interval (parl,paru),
            // set par to the closer endpoint.
            //
            par = M.Max(par, parl);
            par = M.Min(par, paru);
            if (par == 0)
            {
                par = gnorm / dxnorm;
            }

            //
            // beginning of an iteration.
            //
            while (true)
            {
                iter = iter + 1;

                //
                // evaluate the function at the current value of par.
                //
                if (par == 0)
                {
                    par = M.Max(dwarf, 0.001 * paru);
                }
                temp = M.Sqrt(par);
                for (j = 1; j <= n; j++)
                {
                    wa1[j] = temp * diag[j];
                }
                levenbergmarquardtqrsolv(n, ref r, ref ipvt, ref wa1, ref qtb, ref x, ref sdiag, ref wa2);
                for (j = 1; j <= n; j++)
                {
                    wa2[j] = diag[j] * x[j];
                }
                v = 0.0;
                for (i_ = 1; i_ <= n; i_++)
                {
                    v += wa2[i_] * wa2[i_];
                }
                dxnorm = M.Sqrt(v);
                temp = fp;
                fp = dxnorm - delta;

                //
                // if the function is small enough, accept the current value
                // of par. also test for the exceptional cases where parl
                // is zero or the number of iterations has reached 10.
                //
                if (M.Abs(fp) <= 0.1 * delta | parl == 0 & fp <= temp & temp < 0 | iter == 10)
                {
                    break;
                }

                //
                // compute the newton correction.
                //
                for (j = 1; j <= n; j++)
                {
                    l = ipvt[j];
                    wa1[j] = diag[l] * (wa2[l] / dxnorm);
                }
                for (j = 1; j <= n; j++)
                {
                    wa1[j] = wa1[j] / sdiag[j];
                    temp = wa1[j];
                    jp1 = j + 1;
                    if (n >= jp1)
                    {
                        for (i = jp1; i <= n; i++)
                        {
                            wa1[i] = wa1[i] - r[i, j] * temp;
                        }
                    }
                }
                v = 0.0;
                for (i_ = 1; i_ <= n; i_++)
                {
                    v += wa1[i_] * wa1[i_];
                }
                temp = M.Sqrt(v);
                parc = fp / delta / temp / temp;

                //
                // depending on the sign of the function, update parl or paru.
                //
                if (fp > 0)
                {
                    parl = M.Max(parl, par);
                }
                if (fp < 0)
                {
                    paru = M.Min(paru, par);
                }

                //
                // compute an improved estimate for par.
                //
                par = M.Max(parl, par + parc);

                //
                // end of an iteration.
                //
            }

            //
            // termination.
            //
            if (iter == 0)
            {
                par = 0;
            }
        }


        /*************************************************************************
        Подпрограмма, вызываемая на каждой итерации алгоритма.

        Может переопределяться программистом для отладочных целей, например -  для
        визуализации итеративного процесса.
        *************************************************************************/

        private static void levenbergmarquardtnewiteration(ref double[] x)
        {
        }


        /*************************************************************************
        Stopping criterion for Levenberg-Marquardt
        *************************************************************************/

        private static bool additionallevenbergmarquardtstoppingcriterion(int iter)
        {
            return false;
        }
    }
}