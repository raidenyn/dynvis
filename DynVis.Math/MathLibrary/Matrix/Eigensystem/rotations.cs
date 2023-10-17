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

class rotations
{
    /*************************************************************************
    ���������� ������������������ ������������ �������� � �������

    ��������  ��������  ������� �����   ��  ������������������  ��������������
    ��������, �������� ��������� C � S. � ����������� ��  ���������  IsForward
    �������� ������������ ��������������� ���� 1 � 2, 3 � 4 � �.�. ������ (����
    IsForward=True), ���� N � N-1-��, N-2 � N-3 � �.�. ������.

    �������������� ������������ �� ��� ������� � ������ � ����� (������ �� M1
    �� M2, ������� �� N1 �� N2). ��������, �� �������� � ��������� ����������,
    �������� ��� ���������.

    ������� ���������:
        IsForward   -   ������������������ ���������� ��������
        M1,M2       -   �������������� ������������ ������ �� M1 �� M2
        N1,N2       -   �������������� ������������ ������� �� N1 �� N2
        C,S         -   ������������ ��������������.
                        ������ � ���������� ��������� [1..M2-M1]
        A           -   �������������� �������
        WORK        -   ������� ������ � ���������� ��������� [N1..N2]

    �������� ���������
        A           -   ��������������� �������

    ��������� ������������
    *************************************************************************/
    public static void applyrotationsfromtheleft(bool isforward,
        int m1,
        int m2,
        int n1,
        int n2,
        ref double[] c,
        ref double[] s,
        ref double[,] a,
        ref double[] work)
    {
        int j = 0;
        int jp1 = 0;
        double ctemp = 0;
        double stemp = 0;
        double temp = 0;
        int i_ = 0;

        if( m1>m2 | n1>n2 )
        {
            return;
        }
        
        //
        // Form  P * A
        //
        if( isforward )
        {
            if( n1!=n2 )
            {
                
                //
                // Common case: N1<>N2
                //
                for(j=m1; j<=m2-1; j++)
                {
                    ctemp = c[j-m1+1];
                    stemp = s[j-m1+1];
                    if( ctemp!=1 | stemp!=0 )
                    {
                        jp1 = j+1;
                        for(i_=n1; i_<=n2;i_++)
                        {
                            work[i_] = ctemp*a[jp1,i_];
                        }
                        for(i_=n1; i_<=n2;i_++)
                        {
                            work[i_] = work[i_] - stemp*a[j,i_];
                        }
                        for(i_=n1; i_<=n2;i_++)
                        {
                            a[j,i_] = ctemp*a[j,i_];
                        }
                        for(i_=n1; i_<=n2;i_++)
                        {
                            a[j,i_] = a[j,i_] + stemp*a[jp1,i_];
                        }
                        for(i_=n1; i_<=n2;i_++)
                        {
                            a[jp1,i_] = work[i_];
                        }
                    }
                }
            }
            else
            {
                
                //
                // Special case: N1=N2
                //
                for(j=m1; j<=m2-1; j++)
                {
                    ctemp = c[j-m1+1];
                    stemp = s[j-m1+1];
                    if( ctemp!=1 | stemp!=0 )
                    {
                        temp = a[j+1,n1];
                        a[j+1,n1] = ctemp*temp-stemp*a[j,n1];
                        a[j,n1] = stemp*temp+ctemp*a[j,n1];
                    }
                }
            }
        }
        else
        {
            if( n1!=n2 )
            {
                
                //
                // Common case: N1<>N2
                //
                for(j=m2-1; j>=m1; j--)
                {
                    ctemp = c[j-m1+1];
                    stemp = s[j-m1+1];
                    if( ctemp!=1 | stemp!=0 )
                    {
                        jp1 = j+1;
                        for(i_=n1; i_<=n2;i_++)
                        {
                            work[i_] = ctemp*a[jp1,i_];
                        }
                        for(i_=n1; i_<=n2;i_++)
                        {
                            work[i_] = work[i_] - stemp*a[j,i_];
                        }
                        for(i_=n1; i_<=n2;i_++)
                        {
                            a[j,i_] = ctemp*a[j,i_];
                        }
                        for(i_=n1; i_<=n2;i_++)
                        {
                            a[j,i_] = a[j,i_] + stemp*a[jp1,i_];
                        }
                        for(i_=n1; i_<=n2;i_++)
                        {
                            a[jp1,i_] = work[i_];
                        }
                    }
                }
            }
            else
            {
                
                //
                // Special case: N1=N2
                //
                for(j=m2-1; j>=m1; j--)
                {
                    ctemp = c[j-m1+1];
                    stemp = s[j-m1+1];
                    if( ctemp!=1 | stemp!=0 )
                    {
                        temp = a[j+1,n1];
                        a[j+1,n1] = ctemp*temp-stemp*a[j,n1];
                        a[j,n1] = stemp*temp+ctemp*a[j,n1];
                    }
                }
            }
        }
    }


    /*************************************************************************
    ���������� ������������������ ������������ �������� � �������

    ��������  ��������  ������� ������   ��  ������������������  ��������������
    ��������, �������� ��������� C � S. � ����������� ��  ���������   IsForward
    �������� ������������ ��������������� ���� 1 � 2,  3  �  4  � �.�.  �������
    (���� IsForward=True), ���� N-�� � N-1-��, N-2-�� � N-3-�� � �.�. �������.

    �������������� ������������ �� ��� ������� � ������ � ����� (������ �� M1
    �� M2, ������� �� N1 �� N2). ��������, �� �������� � ��������� ����������,
    �������� ��� ���������.

    ������� ���������:
        IsForward   -   ������������������ ���������� ��������
        M1,M2       -   �������������� ������������ ������ �� M1 �� M2
        N1,N2       -   �������������� ������������ ������� �� N1 �� N2
        C,S         -   ������������ ��������������.
                        ������ � ���������� ��������� [1..N2-N1]
        A           -   �������������� �������
        WORK        -   ������� ������ � ���������� ��������� [M1..M2]

    �������� ���������
        A           -   ��������������� �������

    ��������� ������������
    *************************************************************************/
    public static void applyrotationsfromtheright(bool isforward,
        int m1,
        int m2,
        int n1,
        int n2,
        ref double[] c,
        ref double[] s,
        ref double[,] a,
        ref double[] work)
    {
        int j = 0;
        int jp1 = 0;
        double ctemp = 0;
        double stemp = 0;
        double temp = 0;
        int i_ = 0;

        
        //
        // Form A * P'
        //
        if( isforward )
        {
            if( m1!=m2 )
            {
                
                //
                // Common case: M1<>M2
                //
                for(j=n1; j<=n2-1; j++)
                {
                    ctemp = c[j-n1+1];
                    stemp = s[j-n1+1];
                    if( ctemp!=1 | stemp!=0 )
                    {
                        jp1 = j+1;
                        for(i_=m1; i_<=m2;i_++)
                        {
                            work[i_] = ctemp*a[i_,jp1];
                        }
                        for(i_=m1; i_<=m2;i_++)
                        {
                            work[i_] = work[i_] - stemp*a[i_,j];
                        }
                        for(i_=m1; i_<=m2;i_++)
                        {
                            a[i_,j] = ctemp*a[i_,j];
                        }
                        for(i_=m1; i_<=m2;i_++)
                        {
                            a[i_,j] = a[i_,j] + stemp*a[i_,jp1];
                        }
                        for(i_=m1; i_<=m2;i_++)
                        {
                            a[i_,jp1] = work[i_];
                        }
                    }
                }
            }
            else
            {
                
                //
                // Special case: M1=M2
                //
                for(j=n1; j<=n2-1; j++)
                {
                    ctemp = c[j-n1+1];
                    stemp = s[j-n1+1];
                    if( ctemp!=1 | stemp!=0 )
                    {
                        temp = a[m1,j+1];
                        a[m1,j+1] = ctemp*temp-stemp*a[m1,j];
                        a[m1,j] = stemp*temp+ctemp*a[m1,j];
                    }
                }
            }
        }
        else
        {
            if( m1!=m2 )
            {
                
                //
                // Common case: M1<>M2
                //
                for(j=n2-1; j>=n1; j--)
                {
                    ctemp = c[j-n1+1];
                    stemp = s[j-n1+1];
                    if( ctemp!=1 | stemp!=0 )
                    {
                        jp1 = j+1;
                        for(i_=m1; i_<=m2;i_++)
                        {
                            work[i_] = ctemp*a[i_,jp1];
                        }
                        for(i_=m1; i_<=m2;i_++)
                        {
                            work[i_] = work[i_] - stemp*a[i_,j];
                        }
                        for(i_=m1; i_<=m2;i_++)
                        {
                            a[i_,j] = ctemp*a[i_,j];
                        }
                        for(i_=m1; i_<=m2;i_++)
                        {
                            a[i_,j] = a[i_,j] + stemp*a[i_,jp1];
                        }
                        for(i_=m1; i_<=m2;i_++)
                        {
                            a[i_,jp1] = work[i_];
                        }
                    }
                }
            }
            else
            {
                
                //
                // Special case: M1=M2
                //
                for(j=n2-1; j>=n1; j--)
                {
                    ctemp = c[j-n1+1];
                    stemp = s[j-n1+1];
                    if( ctemp!=1 | stemp!=0 )
                    {
                        temp = a[m1,j+1];
                        a[m1,j+1] = ctemp*temp-stemp*a[m1,j];
                        a[m1,j] = stemp*temp+ctemp*a[m1,j];
                    }
                }
            }
        }
    }


    /*************************************************************************
    ������������ ���������� ������������ ��������, �����, ���

    [  CS  SN  ]  .  [ F ]  =  [ R ]
    [ -SN  CS  ]     [ G ]     [ 0 ]

    CS**2 + SN**2 = 1
    *************************************************************************/
    public static void generaterotation(double f,
        double g,
        ref double cs,
        ref double sn,
        ref double r)
    {
        double f1 = 0;
        double g1 = 0;

        if( g==0 )
        {
            cs = 1;
            sn = 0;
            r = f;
        }
        else
        {
            if( f==0 )
            {
                cs = 0;
                sn = 1;
                r = g;
            }
            else
            {
                f1 = f;
                g1 = g;
                r = Math.Sqrt(AP.Math.Sqr(f1)+AP.Math.Sqr(g1));
                cs = f1/r;
                sn = g1/r;
                if( Math.Abs(f)>Math.Abs(g) & cs<0 )
                {
                    cs = -cs;
                    sn = -sn;
                    r = -r;
                }
            }
        }
    }
}
