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

class reflections
{
    /*************************************************************************
    ��������� ������������� �������������� ���������

    ������������ ���������� ������������ ��������� H ������� N, �����, ��� ���
    ��������� X ����������� ��������� ���������:

        ( X(1) )   ( Beta )
    H * (  ..  ) = (  0   )
        ( X(n) )   (  0   )
        
    ���
                  ( V(1) )
    H = 1 - Tau * (  ..  ) * ( V(1), ..., V(n) )
                  ( V(n) )

    ������ ������ ���������� ������� V ����� �������.
                  
    ������� ���������:
        X       -   ������. ������ � ���������� ��������� [1..N]
        N       -   ������� ���������
        
    �������� ���������:
        X       -   ���������� � 2 �� N ���������� �������� V. ������
                    ���������� ���������� ���������� Beta.
        Tau     -   ��������� �������� Tau. ����� 0 (���� ������ X - �������),
                    � ��������� ������ 1 <= Tau <= 2.

    ������ ������������ �������� ������������ ����������� DLARFG �� ����������
    LAPACK. ���������������� �����������, �� �����������  ���������� ���������
    �������, ����� ������������� ���������� ����������  �����������  ���������
    �����.

    ��������� � �����������:
        24.12.2005 ������ sign(Alpha) �� ����������� sign � �������� ���

      -- LAPACK auxiliary routine (version 3.0) --
         Univ. of Tennessee, Univ. of California Berkeley, NAG Ltd.,
         Courant Institute, Argonne National Lab, and Rice University
         September 30, 1994
    *************************************************************************/
    public static void generatereflection(ref double[] x,
        int n,
        ref double tau)
    {
        int j = 0;
        double alpha = 0;
        double xnorm = 0;
        double v = 0;
        double beta = 0;
        double mx = 0;
        int i_ = 0;

        
        //
        // Executable Statements ..
        //
        if( n<=1 )
        {
            tau = 0;
            return;
        }
        
        //
        // XNORM = DNRM2( N-1, X, INCX )
        //
        alpha = x[1];
        mx = 0;
        for(j=2; j<=n; j++)
        {
            mx = Math.Max(Math.Abs(x[j]), mx);
        }
        xnorm = 0;
        if( mx!=0 )
        {
            for(j=2; j<=n; j++)
            {
                xnorm = xnorm+AP.Math.Sqr(x[j]/mx);
            }
            xnorm = Math.Sqrt(xnorm)*mx;
        }
        if( xnorm==0 )
        {
            
            //
            // H  =  I
            //
            tau = 0;
            return;
        }
        
        //
        // general case
        //
        mx = Math.Max(Math.Abs(alpha), Math.Abs(xnorm));
        beta = -(mx*Math.Sqrt(AP.Math.Sqr(alpha/mx)+AP.Math.Sqr(xnorm/mx)));
        if( alpha<0 )
        {
            beta = -beta;
        }
        tau = (beta-alpha)/beta;
        v = 1/(alpha-beta);
        for(i_=2; i_<=n;i_++)
        {
            x[i_] = v*x[i_];
        }
        x[1] = beta;
    }


    /*************************************************************************
    ���������� ������������� ��������� � ������������� ������� �������� MxN

    �������� �������� ����� ������� �� ������������ ��������������  ���������,
    ��������    ��������   V   �   ���������   ���������   Tau  (��.  ��������
    GenerateReflection). �������������� ������������ �� ��� �������, �  ������
    � ����� (������ �� M1 �� M2, ������� �� N1 �� N2). ��������, ��  ��������
    � ��������� ����������, �������� ��� ���������.

    ������� ���������:
        C   -   �������,  �  �������  �����������  ��������������.
        Tau -   ������, �������� ��������������.
        V   -   �������, �������� ��������������. ������ � ���������� ���������
                [1..M2-M1+1]
        M1,M2   -   �������� �����, ������������� ���������������
        N1,N2   -   �������� ��������, ������������� ���������������
        WORK    -   ������� ������ � ���������� ��������� �� N1 �� N2

    �������� ���������:
        C   -   ��������� ��������� ������� ������� C �� ������� ��������������,
                ��������� Tau � V. ���� N1>N2 ��� M1>M2, �� C �� ��������.

      -- LAPACK auxiliary routine (version 3.0) --
         Univ. of Tennessee, Univ. of California Berkeley, NAG Ltd.,
         Courant Institute, Argonne National Lab, and Rice University
         September 30, 1994
    *************************************************************************/
    public static void applyreflectionfromtheleft(ref double[,] c,
        double tau,
        ref double[] v,
        int m1,
        int m2,
        int n1,
        int n2,
        ref double[] work)
    {
        double t = 0;
        int i = 0;
        int vm = 0;
        int i_ = 0;

        if( tau==0 | n1>n2 | m1>m2 )
        {
            return;
        }
        
        //
        // w := C' * v
        //
        vm = m2-m1+1;
        for(i=n1; i<=n2; i++)
        {
            work[i] = 0;
        }
        for(i=m1; i<=m2; i++)
        {
            t = v[i+1-m1];
            for(i_=n1; i_<=n2;i_++)
            {
                work[i_] = work[i_] + t*c[i,i_];
            }
        }
        
        //
        // C := C - tau * v * w'
        //
        for(i=m1; i<=m2; i++)
        {
            t = v[i-m1+1]*tau;
            for(i_=n1; i_<=n2;i_++)
            {
                c[i,i_] = c[i,i_] - t*work[i_];
            }
        }
    }


    /*************************************************************************
    ���������� ������������� ��������� � ������������� ������� �������� MxN

    �������� �������� ������ ������� �� ������������ �������������� ���������,
    ��������    ��������   V   �   ���������   ���������   Tau  (��.  ��������
    GenerateReflection). �������������� ������������ �� ��� �������, �  ������
    � ����� (������ �� M1 �� M2, ������� �� N1 �� N2). ��������, ��  ��������
    � ��������� ����������, �������� ��� ���������.

    ������� ���������:
        C   -   �������,  �  �������  �����������  ��������������.
        Tau -   ������, �������� ��������������.
        V   -   �������, �������� ��������������. ������ � ���������� ���������
                [1..N2-N1+1]
        M1,M2   -   �������� �����, ������������� ���������������
        N1,N2   -   �������� ��������, ������������� ���������������
        WORK    -   ������� ������ � ���������� ��������� �� M1 �� M2

    �������� ���������:
        C   -   ��������� ��������� ������� ������� C �� ������� ��������������,
                ��������� Tau � V. ���� N1>N2 ��� M1>M2, �� C �� ��������.

      -- LAPACK auxiliary routine (version 3.0) --
         Univ. of Tennessee, Univ. of California Berkeley, NAG Ltd.,
         Courant Institute, Argonne National Lab, and Rice University
         September 30, 1994
    *************************************************************************/
    public static void applyreflectionfromtheright(ref double[,] c,
        double tau,
        ref double[] v,
        int m1,
        int m2,
        int n1,
        int n2,
        ref double[] work)
    {
        double t = 0;
        int i = 0;
        int vm = 0;
        int i_ = 0;
        int i1_ = 0;

        if( tau==0 | n1>n2 | m1>m2 )
        {
            return;
        }
        
        //
        // w := C * v
        //
        vm = n2-n1+1;
        for(i=m1; i<=m2; i++)
        {
            i1_ = (1)-(n1);
            t = 0.0;
            for(i_=n1; i_<=n2;i_++)
            {
                t += c[i,i_]*v[i_+i1_];
            }
            work[i] = t;
        }
        
        //
        // C := C - w * v'
        //
        for(i=m1; i<=m2; i++)
        {
            t = work[i]*tau;
            i1_ = (1) - (n1);
            for(i_=n1; i_<=n2;i_++)
            {
                c[i,i_] = c[i,i_] - t*v[i_+i1_];
            }
        }
    }
}
