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

class inv
{
    /*************************************************************************
    ��������� �������, �������� LU-�����������

    ������� ���������:
        A       -   LU-����������  �������   (���������   ������  ������������
                    RMatrixLU).
        Pivots  -   ������� ������������,  ������������� � ���� LU-����������.
                    (��������� ������ ������������ RMatrixLU).
        N       -   ����������� �������

    �������� ���������:
        A       -   �������, �������� � ��������. ������ � ����������
                    ��������� [0..N-1, 0..N-1]

    ���������:
        True,  ���� �������� ������� �������������.
        False, ���� �������� ������� �����������.

      -- LAPACK routine (version 3.0) --
         Univ. of Tennessee, Univ. of California Berkeley, NAG Ltd.,
         Courant Institute, Argonne National Lab, and Rice University
         February 29, 1992
    *************************************************************************/
    public static bool rmatrixluinverse(ref double[,] a,
        ref int[] pivots,
        int n)
    {
        double[] work;
        int i = 0;
        int j = 0;
        int jp = 0;
        double v = 0;
        int i_ = 0;
        
        //
        // Quick return if possible
        //
        if( n==0 )
        {
            return true;
        }
        work = new double[n-1+1];
        
        //
        // Form inv(U)
        //
        if( !trinverse.rmatrixtrinverse(ref a, n, true, false) )
        {
            return false;
        }
        
        //
        // Solve the equation inv(A)*L = inv(U) for inv(A).
        //
        for(j=n-1; j>=0; j--)
        {
            
            //
            // Copy current column of L to WORK and replace with zeros.
            //
            for(i=j+1; i<=n-1; i++)
            {
                work[i] = a[i,j];
                a[i,j] = 0;
            }
            
            //
            // Compute current column of inv(A).
            //
            if( j<n-1 )
            {
                for(i=0; i<=n-1; i++)
                {
                    v = 0.0;
                    for(i_=j+1; i_<=n-1;i_++)
                    {
                        v += a[i,i_]*work[i_];
                    }
                    a[i,j] = a[i,j]-v;
                }
            }
        }
        
        //
        // Apply column interchanges.
        //
        for(j=n-2; j>=0; j--)
        {
            jp = pivots[j];
            if( jp!=j )
            {
                for(i_=0; i_<=n-1;i_++)
                {
                    work[i_] = a[i_,j];
                }
                for(i_=0; i_<=n-1;i_++)
                {
                    a[i_,j] = a[i_,jp];
                }
                for(i_=0; i_<=n-1;i_++)
                {
                    a[i_,jp] = work[i_];
                }
            }
        }
        return true;
    }


    /*************************************************************************
    ��������� ������� ������ ����

    ������� ���������:
        A   -   �������. ������ � ���������� ��������� [0..N-1, 0..N-1]
        N   -   ����������� ������� A

    �������� ���������:
        A   -   �������, �������� � ��������. ������ � ���������� ���������
                [0..N-1, 0..N-1]

    ���������:
        True,  ���� �������� ������� �������������.
        False, ���� �������� ������� �����������.

      -- ALGLIB --
         Copyright 2005 by Bochkanov Sergey
    *************************************************************************/
    public static bool rmatrixinverse(ref double[,] a)
    {
        var n = a.GetLength(0);
        if (n != a.GetLength(1)) throw new ArgumentException("Matrix a not square");

        var pivots = new int[0];

        lu.rmatrixlu(ref a, n, n, ref pivots);
        bool result = rmatrixluinverse(ref a, ref pivots, n);
        return result;
    }


    /*************************************************************************
    Obsolete 1-based subroutine.

    See RMatrixLUInverse for 0-based replacement.
    *************************************************************************/
    public static bool inverselu(ref double[,] a,
        ref int[] pivots,
        int n)
    {
        bool result;
        double[] work;
        int i = 0;
        int j = 0;
        int jp = 0;
        int jp1 = 0;
        double v = 0;
        int i_ = 0;

        result = true;
        
        //
        // Quick return if possible
        //
        if( n==0 )
        {
            return result;
        }
        work = new double[n+1];
        
        //
        // Form inv(U)
        //
        if( !trinverse.invtriangular(ref a, n, true, false) )
        {
            result = false;
            return result;
        }
        
        //
        // Solve the equation inv(A)*L = inv(U) for inv(A).
        //
        for(j=n; j>=1; j--)
        {
            
            //
            // Copy current column of L to WORK and replace with zeros.
            //
            for(i=j+1; i<=n; i++)
            {
                work[i] = a[i,j];
                a[i,j] = 0;
            }
            
            //
            // Compute current column of inv(A).
            //
            if( j<n )
            {
                jp1 = j+1;
                for(i=1; i<=n; i++)
                {
                    v = 0.0;
                    for(i_=jp1; i_<=n;i_++)
                    {
                        v += a[i,i_]*work[i_];
                    }
                    a[i,j] = a[i,j]-v;
                }
            }
        }
        
        //
        // Apply column interchanges.
        //
        for(j=n-1; j>=1; j--)
        {
            jp = pivots[j];
            if( jp!=j )
            {
                for(i_=1; i_<=n;i_++)
                {
                    work[i_] = a[i_,j];
                }
                for(i_=1; i_<=n;i_++)
                {
                    a[i_,j] = a[i_,jp];
                }
                for(i_=1; i_<=n;i_++)
                {
                    a[i_,jp] = work[i_];
                }
            }
        }
        return result;
    }


    /*************************************************************************
    Obsolete 1-based subroutine.

    See RMatrixInverse for 0-based replacement.
    *************************************************************************/
    public static bool inverse(ref double[,] a,
        int n)
    {
        bool result = new bool();
        int[] pivots = new int[0];

        lu.ludecomposition(ref a, n, n, ref pivots);
        result = inverselu(ref a, ref pivots, n);
        return result;
    }
}
