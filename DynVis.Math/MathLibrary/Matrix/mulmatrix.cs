using System;

class mulmatrix
{
    /*************************************************************************
    Умножение матриц A (array [0..n-1, 0..m-1] of Real) и
    матрицы B (array [0..m-1, 0..k-1] of Real).
    *************************************************************************/
    public static double[,] multiplymatrixes(
        double[,] a,
        double[,] b)
    {
        var n = a.GetLength(0);
        var m = a.GetLength(1);
        var k = b.GetLength(1);

        if (b.GetLength(0) != m) throw new ArgumentException("B first dimension must be equal A second dimension.");

        var c = new double[n, k];
        for(var i=0; i<n; i++)
        {
            for(var j=0; j<k; j++)
            {
                for(var l=0; l<m; l++)
                {
                    c[i, j] += a[i, l]*b[l, j];
                }
            }
        }

        return c;
    }

    public static double[,] multiplymatrixes(int n, int m, int k,
    Func<int, int, double> funcA,
    Func<int, int, double> funcB)
    {
        var c = new double[n,k];
        for (var i = 0; i < n; i++)
        {
            for (var j = 0; j < k; j++)
            {
                for (var l = 0; l < m; l++)
                {
                    c[i, j] += funcA(i, l) * funcB(l, j);
                }
            }
        }

        return c;
    }
}
