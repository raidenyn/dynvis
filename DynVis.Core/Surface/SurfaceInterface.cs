using System;
using DynVis.Core.IO;
using DynVis.Core.Surface;
using DynVis.Math;

namespace DynVis.Core
{
    public interface ISurface3D : ISaveFile
    {

        //*******Функции возращают значение энергии и соответсвующие частные производные********************
        double E(double q1, double q2);
        double dEdq1(double q1, double q2);
        double dEdq2(double q1, double q2);
        double d2Edq1dq1(double q1, double q2);
        double d2Edq2dq2(double q1, double q2);
        double d2Edq1dq2(double q1, double q2);
        double d2Edq2dq1(double q1, double q2);

        double E(IPointD p);
        double dEdq1(IPointD p);
        double dEdq2(IPointD p);
        double d2Edq1dq1(IPointD p);
        double d2Edq2dq2(IPointD p);
        double d2Edq1dq2(IPointD p);
        double d2Edq2dq1(IPointD p);
        //************************************************************************************************

        /// <summary>
        /// Максимальное значение энергии
        /// </summary>
        IPointValue MaxE { get; }
        /// <summary>
        /// Минимальное значение энергии
        /// </summary>
        IPointValue MinE { get; }

        /// <summary>
        /// Ось первой координаты
        /// </summary>
        IAxes Axes1 { get; }
        /// <summary>
        /// Ось второй координаты
        /// </summary>
        IAxes Axes2 { get; }

        /// <summary>
        /// Размерность энергии
        /// </summary>
        ScaleDimension EnergyDimension { get; }

        #region Выделяемая точка
        IPointD CurrentPoint { get; }

        bool SetPoint(IPointD p);
        bool SetPoint(double q1, double q2);

        //Значения возвращаются для текущей точки
        double CurrentE { get; }
        double CurrentdEdq1 { get; }
        double CurrentdEdq2 { get; }
        double Currentd2Edq1dq1 { get; }
        double Currentd2Edq2dq2 { get; }
        double Currentd2Edq1dq2 { get; }
        double Currentd2Edq2dq1 { get; }
        #endregion

        bool IsValidCoord(double q1, double q2);
        bool IsValidCoord(ref double q1, ref double q2);
        bool IsValidCoord(IPointD p);

        event EventHandler CurrentPointChanged;
    }
}
