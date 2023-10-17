using System;
using DynVis.Core.IO;

namespace DynVis.Core.Surface
{
    public interface IAxes : ICloneable, IStringable
    {
        /// <summary>
        /// Название / коментарий
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// Размерность
        /// </summary>
        ScaleDimension ScaleDimension { get; set; }
        /// <summary>
        /// Начальное значение
        /// </summary>
        double MaxValue { get; }
        /// <summary>
        /// Конечное значение
        /// </summary>
        double MinValue { get; }

        /// <summary>
        /// Тип поведения в конечной точке
        /// </summary>
        CoordinateType TypeMaxValue { get; set; }
        /// <summary>
        /// Тип поведения в начальной точке
        /// </summary>
        CoordinateType TypeMinValue { get; set; }
    }

    /// <summary>
    /// Тип поведение координаты на краю поверхности
    /// Ended - поверхность обрывается
    /// Periodic - поверхность начинается с противоположенного конца
    /// Mirrored - поверхность отражается зеркально
    /// </summary>
    public enum CoordinateType { Ended, Periodic, Mirrored }
}
