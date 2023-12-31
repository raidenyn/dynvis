/*************************************************************************
Copyright (c) 1992-2007 The University of Tennessee.  All rights reserved.

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

class tridiagonal
{
    /*************************************************************************
    ���������� ������������ �������, �������� ������� ��� ������ �������������,
    � ���������������� ������� ������������� ��������������� �������.

    �������� ������� ����� ������� Q (�������������) �  T  (����������������),
    ��� Q'*A*Q = T

    ������� ���������:
        A   -   ������������� �������
                ������ � ���������� ��������� [0..N-1, 0..N-1].
        N   -   ������ ������� A
        IsUpper-������ ��������. ���� IsUpper=True, ������� A ������ �����
                ������� �������������, � ������ ����������� �� ������������ �
                �� �������������� ����������, � ��������, ���� IsUpper=False.

    �������� ���������:
        A   -   ������� T � Q � ����������� ����� (��. ����)
        Tau -   ������ ����������, ����������� ������� H(i)
                ������ � ���������� ��������� [0..N-2]
        D   -   ������� ��������� ������������ ������� T.
                ������ � ���������� ��������� [0..N-1]
        E   -   �������� ��������� ������������ ������� T.
                ������ � ���������� ��������� [0..N-2]


    ���� IsUpper=True, �� ������� Q ��������������, ��� ������������ ������������
    ���������
        Q = H(n-2) . . . H(1) H(0).
    ��� ������ H(i) ����� ���
        H(i) = I - tau * v * v'
    ��� tau - ������������ �����, �������� � Tau[I]; v - ������������ ������,
    ������ v(i+1:n-1) = 0, v(i) = 1, v(0:i-1) �������� � A(0:i-1,i+1).


    ���� IsUpper=False, ������� Q �������������� � ����
        Q = H(0) H(2) . . . H(n-2).
    ��� ������ H(i) ����� ���
        H(i) = I - tau * v * v'
    ��� tau - ������������ �����, �������� � Tau[I]; v - ������������ ������,
    ������ v(0:i) = 0, v(i+1) = 1, v(i+2:n-1) �������� � A(i+2:n-1,i).


    ������:
     IsUpper=True, n=5:                     IsUpper=False, n=5:
         (  d   e   v1  v2  v3 )              (  d                  )
         (      d   e   v2  v3 )              (  e   d              )
         (          d   e   v3 )              (  v0  e   d          )
         (              d   e  )              (  v0  v1  e   d      )
         (                  d  )              (  v0  v1  v2  e   d  )

    ����� d � e ���������� ������� � �������� ��������� ������� T, � vi -
    ������� �������, ������������ H(i).


      -- LAPACK routine (version 3.0) --
         Univ. of Tennessee, Univ. of California Berkeley, NAG Ltd.,
         Courant Institute, Argonne National Lab, and Rice University
         October 31, 1992
    *************************************************************************/
    public static void smatrixtd(ref double[,] a,
        int n,
        bool isupper,
        ref double[] tau,
        ref double[] d,
        ref double[] e)
    {
        int i = 0;
        double alpha = 0;
        double taui = 0;
        double v = 0;
        double[] t = new double[0];
        double[] t2 = new double[0];
        double[] t3 = new double[0];
        int i_ = 0;
        int i1_ = 0;

        if( n<=0 )
        {
            return;
        }
        t = new double[n+1];
        t2 = new double[n+1];
        t3 = new double[n+1];
        if( n>1 )
        {
            tau = new double[n-2+1];
        }
        d = new double[n-1+1];
        if( n>1 )
        {
            e = new double[n-2+1];
        }
        if( isupper )
        {
            
            //
            // Reduce the upper triangle of A
            //
            for(i=n-2; i>=0; i--)
            {
                
                //
                // Generate elementary reflector H() = E - tau * v * v'
                //
                if( i>=1 )
                {
                    i1_ = (0) - (2);
                    for(i_=2; i_<=i+1;i_++)
                    {
                        t[i_] = a[i_+i1_,i+1];
                    }
                }
                t[1] = a[i,i+1];
                reflections.generatereflection(ref t, i+1, ref taui);
                if( i>=1 )
                {
                    i1_ = (2) - (0);
                    for(i_=0; i_<=i-1;i_++)
                    {
                        a[i_,i+1] = t[i_+i1_];
                    }
                }
                a[i,i+1] = t[1];
                e[i] = a[i,i+1];
                if( taui!=0 )
                {
                    
                    //
                    // Apply H from both sides to A
                    //
                    a[i,i+1] = 1;
                    
                    //
                    // Compute  x := tau * A * v  storing x in TAU
                    //
                    i1_ = (0) - (1);
                    for(i_=1; i_<=i+1;i_++)
                    {
                        t[i_] = a[i_+i1_,i+1];
                    }
                    sblas.symmetricmatrixvectormultiply(ref a, isupper, 0, i, ref t, taui, ref t3);
                    i1_ = (1) - (0);
                    for(i_=0; i_<=i;i_++)
                    {
                        tau[i_] = t3[i_+i1_];
                    }
                    
                    //
                    // Compute  w := x - 1/2 * tau * (x'*v) * v
                    //
                    v = 0.0;
                    for(i_=0; i_<=i;i_++)
                    {
                        v += tau[i_]*a[i_,i+1];
                    }
                    alpha = -(0.5*taui*v);
                    for(i_=0; i_<=i;i_++)
                    {
                        tau[i_] = tau[i_] + alpha*a[i_,i+1];
                    }
                    
                    //
                    // Apply the transformation as a rank-2 update:
                    //    A := A - v * w' - w * v'
                    //
                    i1_ = (0) - (1);
                    for(i_=1; i_<=i+1;i_++)
                    {
                        t[i_] = a[i_+i1_,i+1];
                    }
                    i1_ = (0) - (1);
                    for(i_=1; i_<=i+1;i_++)
                    {
                        t3[i_] = tau[i_+i1_];
                    }
                    sblas.symmetricrank2update(ref a, isupper, 0, i, ref t, ref t3, ref t2, -1);
                    a[i,i+1] = e[i];
                }
                d[i+1] = a[i+1,i+1];
                tau[i] = taui;
            }
            d[0] = a[0,0];
        }
        else
        {
            
            //
            // Reduce the lower triangle of A
            //
            for(i=0; i<=n-2; i++)
            {
                
                //
                // Generate elementary reflector H = E - tau * v * v'
                //
                i1_ = (i+1) - (1);
                for(i_=1; i_<=n-i-1;i_++)
                {
                    t[i_] = a[i_+i1_,i];
                }
                reflections.generatereflection(ref t, n-i-1, ref taui);
                i1_ = (1) - (i+1);
                for(i_=i+1; i_<=n-1;i_++)
                {
                    a[i_,i] = t[i_+i1_];
                }
                e[i] = a[i+1,i];
                if( taui!=0 )
                {
                    
                    //
                    // Apply H from both sides to A
                    //
                    a[i+1,i] = 1;
                    
                    //
                    // Compute  x := tau * A * v  storing y in TAU
                    //
                    i1_ = (i+1) - (1);
                    for(i_=1; i_<=n-i-1;i_++)
                    {
                        t[i_] = a[i_+i1_,i];
                    }
                    sblas.symmetricmatrixvectormultiply(ref a, isupper, i+1, n-1, ref t, taui, ref t2);
                    i1_ = (1) - (i);
                    for(i_=i; i_<=n-2;i_++)
                    {
                        tau[i_] = t2[i_+i1_];
                    }
                    
                    //
                    // Compute  w := x - 1/2 * tau * (x'*v) * v
                    //
                    i1_ = (i+1)-(i);
                    v = 0.0;
                    for(i_=i; i_<=n-2;i_++)
                    {
                        v += tau[i_]*a[i_+i1_,i];
                    }
                    alpha = -(0.5*taui*v);
                    i1_ = (i+1) - (i);
                    for(i_=i; i_<=n-2;i_++)
                    {
                        tau[i_] = tau[i_] + alpha*a[i_+i1_,i];
                    }
                    
                    //
                    // Apply the transformation as a rank-2 update:
                    //     A := A - v * w' - w * v'
                    //
                    //
                    i1_ = (i+1) - (1);
                    for(i_=1; i_<=n-i-1;i_++)
                    {
                        t[i_] = a[i_+i1_,i];
                    }
                    i1_ = (i) - (1);
                    for(i_=1; i_<=n-i-1;i_++)
                    {
                        t2[i_] = tau[i_+i1_];
                    }
                    sblas.symmetricrank2update(ref a, isupper, i+1, n-1, ref t, ref t2, ref t3, -1);
                    a[i+1,i] = e[i];
                }
                d[i] = a[i,i];
                tau[i] = taui;
            }
            d[n-1] = a[n-1,n-1];
        }
    }


    /*************************************************************************
    "����������" ������� Q, ���������� ������������ ������� � ����������������
    �����.

    ������� ���������:
        A   -   ��������� ������ ������������ SMatrixTD
        N   -   ������ �������
        IsUpper-������ �������� (�������� ������������ SMatrixTD)
        Tau -   ��������� ������ ������������ SMatrixTD

    �������� ���������:
        Q   -   ������� ��������������.
                ������ � ���������� ��������� [0..N-1, 0..N-1]


      -- ALGLIB --
         Copyright 2005-2008 by Bochkanov Sergey
    *************************************************************************/
    public static void smatrixtdunpackq(ref double[,] a,
        int n,
        bool isupper,
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
        v = new double[n+1];
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
        if( isupper )
        {
            for(i=0; i<=n-2; i++)
            {
                
                //
                // Apply H(i)
                //
                i1_ = (0) - (1);
                for(i_=1; i_<=i+1;i_++)
                {
                    v[i_] = a[i_+i1_,i+1];
                }
                v[i+1] = 1;
                reflections.applyreflectionfromtheleft(ref q, tau[i], ref v, 0, i, 0, n-1, ref work);
            }
        }
        else
        {
            for(i=n-2; i>=0; i--)
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
                reflections.applyreflectionfromtheleft(ref q, tau[i], ref v, i+1, n-1, 0, n-1, ref work);
            }
        }
    }


    /*************************************************************************
    Obsolete 1-based subroutine
    *************************************************************************/
    public static void totridiagonal(ref double[,] a,
        int n,
        bool isupper,
        ref double[] tau,
        ref double[] d,
        ref double[] e)
    {
        int i = 0;
        int ip1 = 0;
        int im1 = 0;
        int nmi = 0;
        int nm1 = 0;
        double alpha = 0;
        double taui = 0;
        double v = 0;
        double[] t = new double[0];
        double[] t2 = new double[0];
        double[] t3 = new double[0];
        int i_ = 0;
        int i1_ = 0;

        if( n<=0 )
        {
            return;
        }
        t = new double[n+1];
        t2 = new double[n+1];
        t3 = new double[n+1];
        tau = new double[Math.Max(1, n-1)+1];
        d = new double[n+1];
        e = new double[Math.Max(1, n-1)+1];
        if( isupper )
        {
            
            //
            // Reduce the upper triangle of A
            //
            for(i=n-1; i>=1; i--)
            {
                
                //
                // Generate elementary reflector H(i) = I - tau * v * v'
                // to annihilate A(1:i-1,i+1)
                //
                // DLARFG( I, A( I, I+1 ), A( 1, I+1 ), 1, TAUI );
                //
                ip1 = i+1;
                im1 = i-1;
                if( i>=2 )
                {
                    i1_ = (1) - (2);
                    for(i_=2; i_<=i;i_++)
                    {
                        t[i_] = a[i_+i1_,ip1];
                    }
                }
                t[1] = a[i,ip1];
                reflections.generatereflection(ref t, i, ref taui);
                if( i>=2 )
                {
                    i1_ = (2) - (1);
                    for(i_=1; i_<=im1;i_++)
                    {
                        a[i_,ip1] = t[i_+i1_];
                    }
                }
                a[i,ip1] = t[1];
                e[i] = a[i,i+1];
                if( taui!=0 )
                {
                    
                    //
                    // Apply H(i) from both sides to A(1:i,1:i)
                    //
                    a[i,i+1] = 1;
                    
                    //
                    // Compute  x := tau * A * v  storing x in TAU(1:i)
                    //
                    // DSYMV( UPLO, I, TAUI, A, LDA, A( 1, I+1 ), 1, ZERO, TAU, 1 );
                    //
                    ip1 = i+1;
                    for(i_=1; i_<=i;i_++)
                    {
                        t[i_] = a[i_,ip1];
                    }
                    sblas.symmetricmatrixvectormultiply(ref a, isupper, 1, i, ref t, taui, ref tau);
                    
                    //
                    // Compute  w := x - 1/2 * tau * (x'*v) * v
                    //
                    ip1 = i+1;
                    v = 0.0;
                    for(i_=1; i_<=i;i_++)
                    {
                        v += tau[i_]*a[i_,ip1];
                    }
                    alpha = -(0.5*taui*v);
                    for(i_=1; i_<=i;i_++)
                    {
                        tau[i_] = tau[i_] + alpha*a[i_,ip1];
                    }
                    
                    //
                    // Apply the transformation as a rank-2 update:
                    //    A := A - v * w' - w * v'
                    //
                    // DSYR2( UPLO, I, -ONE, A( 1, I+1 ), 1, TAU, 1, A, LDA );
                    //
                    for(i_=1; i_<=i;i_++)
                    {
                        t[i_] = a[i_,ip1];
                    }
                    sblas.symmetricrank2update(ref a, isupper, 1, i, ref t, ref tau, ref t2, -1);
                    a[i,i+1] = e[i];
                }
                d[i+1] = a[i+1,i+1];
                tau[i] = taui;
            }
            d[1] = a[1,1];
        }
        else
        {
            
            //
            // Reduce the lower triangle of A
            //
            for(i=1; i<=n-1; i++)
            {
                
                //
                // Generate elementary reflector H(i) = I - tau * v * v'
                // to annihilate A(i+2:n,i)
                //
                //DLARFG( N-I, A( I+1, I ), A( MIN( I+2, N ), I ), 1, TAUI );
                //
                nmi = n-i;
                ip1 = i+1;
                i1_ = (ip1) - (1);
                for(i_=1; i_<=nmi;i_++)
                {
                    t[i_] = a[i_+i1_,i];
                }
                reflections.generatereflection(ref t, nmi, ref taui);
                i1_ = (1) - (ip1);
                for(i_=ip1; i_<=n;i_++)
                {
                    a[i_,i] = t[i_+i1_];
                }
                e[i] = a[i+1,i];
                if( taui!=0 )
                {
                    
                    //
                    // Apply H(i) from both sides to A(i+1:n,i+1:n)
                    //
                    a[i+1,i] = 1;
                    
                    //
                    // Compute  x := tau * A * v  storing y in TAU(i:n-1)
                    //
                    //DSYMV( UPLO, N-I, TAUI, A( I+1, I+1 ), LDA, A( I+1, I ), 1, ZERO, TAU( I ), 1 );
                    //
                    ip1 = i+1;
                    nmi = n-i;
                    nm1 = n-1;
                    i1_ = (ip1) - (1);
                    for(i_=1; i_<=nmi;i_++)
                    {
                        t[i_] = a[i_+i1_,i];
                    }
                    sblas.symmetricmatrixvectormultiply(ref a, isupper, i+1, n, ref t, taui, ref t2);
                    i1_ = (1) - (i);
                    for(i_=i; i_<=nm1;i_++)
                    {
                        tau[i_] = t2[i_+i1_];
                    }
                    
                    //
                    // Compute  w := x - 1/2 * tau * (x'*v) * v
                    //
                    nm1 = n-1;
                    ip1 = i+1;
                    i1_ = (ip1)-(i);
                    v = 0.0;
                    for(i_=i; i_<=nm1;i_++)
                    {
                        v += tau[i_]*a[i_+i1_,i];
                    }
                    alpha = -(0.5*taui*v);
                    i1_ = (ip1) - (i);
                    for(i_=i; i_<=nm1;i_++)
                    {
                        tau[i_] = tau[i_] + alpha*a[i_+i1_,i];
                    }
                    
                    //
                    // Apply the transformation as a rank-2 update:
                    //     A := A - v * w' - w * v'
                    //
                    //DSYR2( UPLO, N-I, -ONE, A( I+1, I ), 1, TAU( I ), 1, A( I+1, I+1 ), LDA );
                    //
                    nm1 = n-1;
                    nmi = n-i;
                    ip1 = i+1;
                    i1_ = (ip1) - (1);
                    for(i_=1; i_<=nmi;i_++)
                    {
                        t[i_] = a[i_+i1_,i];
                    }
                    i1_ = (i) - (1);
                    for(i_=1; i_<=nmi;i_++)
                    {
                        t2[i_] = tau[i_+i1_];
                    }
                    sblas.symmetricrank2update(ref a, isupper, i+1, n, ref t, ref t2, ref t3, -1);
                    a[i+1,i] = e[i];
                }
                d[i] = a[i,i];
                tau[i] = taui;
            }
            d[n] = a[n,n];
        }
    }


    /*************************************************************************
    Obsolete 1-based subroutine
    *************************************************************************/
    public static void unpackqfromtridiagonal(ref double[,] a,
        int n,
        bool isupper,
        ref double[] tau,
        ref double[,] q)
    {
        int i = 0;
        int j = 0;
        int ip1 = 0;
        int nmi = 0;
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
        if( isupper )
        {
            for(i=1; i<=n-1; i++)
            {
                
                //
                // Apply H(i)
                //
                ip1 = i+1;
                for(i_=1; i_<=i;i_++)
                {
                    v[i_] = a[i_,ip1];
                }
                v[i] = 1;
                reflections.applyreflectionfromtheleft(ref q, tau[i], ref v, 1, i, 1, n, ref work);
            }
        }
        else
        {
            for(i=n-1; i>=1; i--)
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
                reflections.applyreflectionfromtheleft(ref q, tau[i], ref v, i+1, n, 1, n, ref work);
            }
        }
    }
}
