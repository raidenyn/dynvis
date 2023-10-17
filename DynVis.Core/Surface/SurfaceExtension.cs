using DynVis.Math;

namespace DynVis.Core
{
    /// <summary>
    /// Класс дополнительных функций поверхности
    /// </summary>
    public static class SurfaceExtension
    {
        /// <summary>
        /// Устанваливает текущую точку в средину поверхности
        /// </summary>
        /// <param name="surface"></param>
        public static void SetAveragePosition(this ISurface3D surface)
        {
            surface.SetPoint(MathExtended.Avg(surface.Axes1.MaxValue, surface.Axes1.MinValue),
                             MathExtended.Avg(surface.Axes2.MaxValue, surface.Axes2.MinValue));
        }

        /// <summary>
        /// Возращает собственные значения матрицы Гессиана в точке
        /// </summary>
        /// <param name="pes">Поверхность</param>
        /// <param name="q1">Первая координата</param>
        /// <param name="q2">Вторая координата</param>
        /// <param name="e1">Первое собственное значение</param>
        /// <param name="e2">Второе собственное значение</param>
        public static void GetHessEigenvalues(this ISurface3D pes, double q1, double q2, out double e1, out double e2)
        {
            MathExtended.CalcEigenvalues(pes.d2Edq1dq1(q1, q2), pes.d2Edq2dq2(q1, q2), pes.d2Edq1dq2(q1, q2), pes.d2Edq2dq1(q1, q2), out e1, out e2);
        }

        /// <summary>
        /// Возращает собственные значения матрицы Гессиана в точке
        /// </summary>
        /// <param name="pes">Поверхность</param>
        /// <param name="q1">Первая координата</param>
        /// <param name="q2">Вторая координата</param>
        /// <param name="vals">Массив в два элемента для перового и второго собственного значения</param>
        public static void GetHessEigenvalues(this ISurface3D pes, double q1, double q2, out double[] vals)
        {
            vals = new double[2];
            pes.GetHessEigenvalues(q1, q2, out vals[0], out vals[1]);
        }

        /// <summary>
        /// Возращает собственные вектора и собственные значения матрицы Гессиана в точке
        /// </summary>
        /// <param name="pes">Поверхность</param>
        /// <param name="q1">Первая координата</param>
        /// <param name="q2">Вторая координата</param>
        /// <param name="vals">Масси собсвенных значений</param>
        /// <param name="vecs">Масси собсвенных векторов</param>
        public static void GetHessEigensystem(this ISurface3D pes, double q1, double q2, out double[] vals, out double[,] vecs)
        {
            MathExtended.CalcEigensystem(pes.d2Edq1dq1(q1, q2), pes.d2Edq2dq2(q1, q2), pes.d2Edq1dq2(q1, q2), pes.d2Edq2dq1(q1, q2), out vals, out vecs);
        }
    }
}
