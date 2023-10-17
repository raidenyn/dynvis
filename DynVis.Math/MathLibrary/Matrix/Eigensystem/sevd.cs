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
    ����� ����������� ����� � �������� ������������ �������

    ��������  ����  �����������  ����  ������������   �������,  �������  �  �
    ���������������� � ��������� QL/QR ��������.

    ������� ���������:
        A   -       ������������ �������, �������� ������� ��� ������
                    �������������. ������ � ���������� ��������� [0..N-1, 0..N-1]
        N   -       ������ �������
        IsUpper-    ������ ��������
        ZNeeded-    ����, ����������, ��������� �� ����������� �������.
                    ���� ZNeeded:
                     * ����� 0, �� ����������� ������� �� ������������
                     * ����� 1, �� ����������� ������� ������������

    �������� ���������:
        D   -       ����������� ����� � ������� �����������.
                    ������ � ���������� ��������� [0..N-1].
        Z   -       ���� ZNeeded:
                     * ����� 0, �� ��������������
                     * ����� 1, �������� ����������� �������
                    ������ � ���������� ��������� [0..N-1, 0..N-1], ��� ����
                    ����������� ������� �������� � �������� �������.

    ���������:
        True, ���� �������� �������
        False, ���� �������� �� ������� (��������� ������)

      -- ALGLIB --
         Copyright 2005-2008 by Bochkanov Sergey
    *************************************************************************/
    /// <summary>
    /// ����� ����������� ����� � �������� ������������ �������
    /// ��������  ����  �����������  ����  ������������   �������,  �������  �  �
    /// ���������������� � ��������� QL/QR ��������.
    /// </summary>
    /// <param name="a">������������ �������, �������� ������� ��� ������ �������������. ������ � ���������� ��������� [0..N-1, 0..N-1]</param>
    /// <param name="zneeded">����, ����������, ��������� �� ����������� �������.
    ///                ���� ZNeeded:
    ///                 * ����� 0, �� ����������� ������� �� ������������
    ///                 * ����� 1, �� ����������� ������� ������������</param>
    /// <param name="isupper">������ ��������</param>
    /// <param name="d">����������� ����� � ������� �����������.
    /// ������ � ���������� ��������� [0..N-1].</param>
    /// <param name="z">���� ZNeeded:
    ///                 * ����� 0, �� ��������������
    ///                 * ����� 1, �������� ����������� �������
    ///                ������ � ���������� ��������� [0..N-1, 0..N-1], ��� ����
    ///                ����������� ������� �������� � �������� �������.</param>
    /// <returns>True, ���� �������� �������
    ///    False, ���� �������� �� ������� (��������� ������)</returns>
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
