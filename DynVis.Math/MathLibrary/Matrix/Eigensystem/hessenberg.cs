/*************************************************************************
Copyright (c) 1992-2007 The University of Tennessee. All rights reserved.

Contributors:
    * Sergey Bochkanov (ALGLIB project). Translation from FORTRAN to
      pseudocode.

See subroutines comments for additional copyrights.

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

using System;

class hessenberg
{
    /*************************************************************************
    Приведение квадратной матрицы к верхней форме Хессенберга: Q'*A*Q = H,
    где Q - ортогональная матрица, H - матрица Хессенберга.

    Входные параметры:
        A   -   матрица A. Нумерация элементов: [0..N-1, 0..N-1]
        N   -   размер матрицы A

    Выходные параметры:
        A   -   матрицы Q и R в компактной форме (см. ниже)
        Tau -   массив скалярных множителей, участвующих в формировании
                матрицы Q. Нумерация элементов [0.. N-1]

    После завершения работы подпрограммы на главной диагонали матрицы A, нижней
    побочной диагонали и выше главной диагонали располагаются элементы матрицы H.
    В массиве Tau и под нижней побочной диагональю матрицы A располагаются
    элементы, формирующие матрицу Q, следующим способом:

    Матрица Q представляется, как произведение элементарных отражений

    Q = H(0)*H(2)*...*H(n-2),

    где каждое H(i) имеет вид

    H(i) = 1 - tau * v * (v^T)

    где tau скалярная величина, хранящаяся в Tau[I], а v - вещественный вектор
    у которого v(0:i)=0, v(i+1)=1, v(i+2:n-1) хранится в элементах A(i+2:n-1,i).

      -- LAPACK routine (version 3.0) --
         Univ. of Tennessee, Univ. of California Berkeley, NAG Ltd.,
         Courant Institute, Argonne National Lab, and Rice University
         October 31, 1992
    *************************************************************************/
    public static void rmatrixhessenberg(ref double[,] a,
        int n,
        ref double[] tau)
    {
        double v = 0;
        double[] t = new double[0];
        double[] work = new double[0];
        int i_ = 0;
        int i1_ = 0;

        System.Diagnostics.Debug.Assert(n>=0, "RMatrixHessenberg: incorrect N!");
        
        //
        // Quick return if possible
        //
        if( n<=1 )
        {
            return;
        }
        tau = new double[n-2+1];
        t = new double[n+1];
        work = new double[n-1+1];
        for(var i=0; i<=n-2; i++)
        {
            
            //
            // Compute elementary reflector H(i) to annihilate A(i+2:ihi,i)
            //
            i1_ = (i+1) - (1);
            for(i_=1; i_<=n-i-1;i_++)
            {
                t[i_] = a[i_+i1_,i];
            }
            reflections.generatereflection(ref t, n-i-1, ref v);
            i1_ = (1) - (i+1);
            for(i_=i+1; i_<=n-1;i_++)
            {
                a[i_,i] = t[i_+i1_];
            }
            tau[i] = v;
            t[1] = 1;
            
            //
            // Apply H(i) to A(1:ihi,i+1:ihi) from the right
            //
            reflections.applyreflectionfromtheright(ref a, v, ref t, 0, n-1, i+1, n-1, ref work);
            
            //
            // Apply H(i) to A(i+1:ihi,i+1:n) from the left
            //
            reflections.applyreflectionfromtheleft(ref a, v, ref t, i+1, n-1, i+1, n-1, ref work);
        }
    }


    /*************************************************************************
    "распаковка" матрицы Q, приводящей A к верхней форме Хессенберга

    Входные параметры:
        A       -   результат работы подпрограммы RMatrixHessenberg
        N       -   размер матрицы A
        Tau     -   скалярные множители, формирующие Q.
                    Результат работы RMatrixHessenberg

    Выходные параметры:
        Q       -   матрица Q. Массив с нумерацией элементов [0..N-1, 0..N-1].

      -- ALGLIB --
         Copyright 2005 by Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixhessenbergunpackq(ref double[,] a,
        int n,
        ref double[] tau,
        ref double[,] q)
    {
        int i = 0;
        int j = 0;
        double[] v = new double[0];
        double[] work = new double[0];
        int i_ = 0;
        int i1_ = 0;

        if( n==0 )
        {
            return;
        }
        
        //
        // init
        //
        q = new double[n-1+1, n-1+1];
        v = new double[n-1+1];
        work = new double[n-1+1];
        for(i=0; i<=n-1; i++)
        {
            for(j=0; j<=n-1; j++)
            {
                if( i==j )
                {
                    q[i,j] = 1;
                }
                else
                {
                    q[i,j] = 0;
                }
            }
        }
        
        //
        // unpack Q
        //
        for(i=0; i<=n-2; i++)
        {
            
            //
            // Apply H(i)
            //
            i1_ = (i+1) - (1);
            for(i_=1; i_<=n-i-1;i_++)
            {
                v[i_] = a[i_+i1_,i];
            }
            v[1] = 1;
            reflections.applyreflectionfromtheright(ref q, tau[i], ref v, 0, n-1, i+1, n-1, ref work);
        }
    }


    /*************************************************************************
    "распаковка" матрицы H (результата приводения A к верхней форме Хессенберга)

    Входные параметры:
        A       -   результат работы подпрограммы RMatrixHessenberg
        N       -   размер матрицы A

    Выходные параметры:
        H       -   матрица H. Массив с нумерацией элементов [0..N-1, 0..N-1].

      -- ALGLIB --
         Copyright 2005 by Bochkanov Sergey
    *************************************************************************/
    public static void rmatrixhessenbergunpackh(ref double[,] a,
        int n,
        ref double[,] h)
    {
        int i = 0;
        int j = 0;
        int i_ = 0;

        if( n==0 )
        {
            return;
        }
        h = new double[n-1+1, n-1+1];
        for(i=0; i<=n-1; i++)
        {
            for(j=0; j<=i-2; j++)
            {
                h[i,j] = 0;
            }
            j = Math.Max(0, i-1);
            for(i_=j; i_<=n-1;i_++)
            {
                h[i,i_] = a[i,i_];
            }
        }
    }


    /*************************************************************************
    Obsolete 1-based subroutine.
    See RMatrixHessenberg for 0-based replacement.
    *************************************************************************/
    public static void toupperhessenberg(ref double[,] a,
        int n,
        ref double[] tau)
    {
        int i = 0;
        int ip1 = 0;
        int nmi = 0;
        double v = 0;
        double[] t = new double[0];
        double[] work = new double[0];
        int i_ = 0;
        int i1_ = 0;

        System.Diagnostics.Debug.Assert(n>=0, "ToUpperHessenberg: incorrect N!");
        
        //
        // Quick return if possible
        //
        if( n<=1 )
        {
            return;
        }
        tau = new double[n-1+1];
        t = new double[n+1];
        work = new double[n+1];
        for(i=1; i<=n-1; i++)
        {
            
            //
            // Compute elementary reflector H(i) to annihilate A(i+2:ihi,i)
            //
            ip1 = i+1;
            nmi = n-i;
            i1_ = (ip1) - (1);
            for(i_=1; i_<=nmi;i_++)
            {
                t[i_] = a[i_+i1_,i];
            }
            reflections.generatereflection(ref t, nmi, ref v);
            i1_ = (1) - (ip1);
            for(i_=ip1; i_<=n;i_++)
            {
                a[i_,i] = t[i_+i1_];
            }
            tau[i] = v;
            t[1] = 1;
            
            //
            // Apply H(i) to A(1:ihi,i+1:ihi) from the right
            //
            reflections.applyreflectionfromtheright(ref a, v, ref t, 1, n, i+1, n, ref work);
            
            //
            // Apply H(i) to A(i+1:ihi,i+1:n) from the left
            //
            reflections.applyreflectionfromtheleft(ref a, v, ref t, i+1, n, i+1, n, ref work);
        }
    }


    /*************************************************************************
    Obsolete 1-based subroutine.
    See RMatrixHessenbergUnpackQ for 0-based replacement.
    *************************************************************************/
    public static void unpackqfromupperhessenberg(ref double[,] a,
        int n,
        ref double[] tau,
        ref double[,] q)
    {
        int i = 0;
        int j = 0;
        double[] v = new double[0];
        double[] work = new double[0];
        int ip1 = 0;
        int nmi = 0;
        int i_ = 0;
        int i1_ = 0;

        if( n==0 )
        {
            return;
        }
        
        //
        // init
        //
        q = new double[n+1, n+1];
        v = new double[n+1];
        work = new double[n+1];
        for(i=1; i<=n; i++)
        {
            for(j=1; j<=n; j++)
            {
                if( i==j )
                {
                    q[i,j] = 1;
                }
                else
                {
                    q[i,j] = 0;
                }
            }
        }
        
        //
        // unpack Q
        //
        for(i=1; i<=n-1; i++)
        {
            
            //
            // Apply H(i)
            //
            ip1 = i+1;
            nmi = n-i;
            i1_ = (ip1) - (1);
            for(i_=1; i_<=nmi;i_++)
            {
                v[i_] = a[i_+i1_,i];
            }
            v[1] = 1;
            reflections.applyreflectionfromtheright(ref q, tau[i], ref v, 1, n, i+1, n, ref work);
        }
    }


    /*************************************************************************
    Obsolete 1-based subroutine.
    See RMatrixHessenbergUnpackH for 0-based replacement.
    *************************************************************************/
    public static void unpackhfromupperhessenberg(ref double[,] a,
        int n,
        ref double[] tau,
        ref double[,] h)
    {
        int i = 0;
        int j = 0;
        int i_ = 0;

        if( n==0 )
        {
            return;
        }
        h = new double[n+1, n+1];
        for(i=1; i<=n; i++)
        {
            for(j=1; j<=i-2; j++)
            {
                h[i,j] = 0;
            }
            j = Math.Max(1, i-1);
            for(i_=j; i_<=n;i_++)
            {
                h[i,i_] = a[i,i_];
            }
        }
    }
}
