/*************************************************************************
Copyright (c) 2005-2007, Sergey Bochkanov (ALGLIB project).

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

class sevd
{
    /*************************************************************************
    Поиск собственных чисел и векторов симметричной матрицы

    Алгоритм  ищет  собственные  пары  симметричной   матрицы,  приводя  её  к
    трехдиагональной и используя QL/QR алгоритм.

    Входные параметры:
        A   -       симметричная матрица, заданная верхним или нижним
                    треугольником. Массив с нумерацией элементов [0..N-1, 0..N-1]
        N   -       размер матрицы
        IsUpper-    формат хранения
        ZNeeded-    флаг, сообщающий, требуются ли собственные векторы.
                    Если ZNeeded:
                     * равно 0, то собственные векторы не возвращаются
                     * равно 1, то собственные векторы возвращаются

    Выходные параметры:
        D   -       собственные числа в порядке возрастания.
                    Массив с нумерацией элементов [0..N-1].
        Z   -       если ZNeeded:
                     * равно 0, не модифицируется
                     * равно 1, содержит собственные векторы
                    Массив с нумерацией элементов [0..N-1, 0..N-1], при этом
                    собственные векторы хранятся в столбцах матрицы.

    Результат:
        True, если алгоритм сошелся
        False, если алгоритм не сошелся (редчайший случай)

      -- ALGLIB --
         Copyright 2005-2008 by Bochkanov Sergey
    *************************************************************************/
    /// <summary>
    /// Поиск собственных чисел и векторов симметричной матрицы
    /// Алгоритм  ищет  собственные  пары  симметричной   матрицы,  приводя  её  к
    /// трехдиагональной и используя QL/QR алгоритм.
    /// </summary>
    /// <param name="a">симметричная матрица, заданная верхним или нижним треугольником. Массив с нумерацией элементов [0..N-1, 0..N-1]</param>
    /// <param name="zneeded">флаг, сообщающий, требуются ли собственные векторы.
    ///                Если ZNeeded:
    ///                 * равно 0, то собственные векторы не возвращаются
    ///                 * равно 1, то собственные векторы возвращаются</param>
    /// <param name="isupper">формат хранения</param>
    /// <param name="d">собственные числа в порядке возрастания.
    /// Массив с нумерацией элементов [0..N-1].</param>
    /// <param name="z">если ZNeeded:
    ///                 * равно 0, не модифицируется
    ///                 * равно 1, содержит собственные векторы
    ///                Массив с нумерацией элементов [0..N-1, 0..N-1], при этом
    ///                собственные векторы хранятся в столбцах матрицы.</param>
    /// <returns>True, если алгоритм сошелся
    ///    False, если алгоритм не сошелся (редчайший случай)</returns>
    public static bool smatrixevd(double[,] a,
        int zneeded,
        bool isupper,
        out double[] d,
        out double[,] z)
    {
        var n = a.GetLength(0);

        var tau = new double[0];
        var e = new double[0];

        d = new double[0];
        z = new double[0,0];

        a = (double[,])a.Clone();

        System.Diagnostics.Debug.Assert(zneeded==0 | zneeded==1, "SMatrixEVD: incorrect ZNeeded");
        tridiagonal.smatrixtd(ref a, n, isupper, ref tau, ref d, ref e);
        if( zneeded==1 )
        {
            tridiagonal.smatrixtdunpackq(ref a, n, isupper, ref tau, ref z);
        }
        bool result = tdevd.smatrixtdevd(ref d, e, n, zneeded, ref z);
        return result;
    }

}
