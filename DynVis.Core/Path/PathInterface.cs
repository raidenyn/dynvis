using System;
using System.Collections;
using DynVis.Core.IO;
using DynVis.Math;

namespace DynVis.Core
{
    /// <summary>
    /// Интерфейс пути реакции
    /// </summary>
    public interface IPath : IEnumerable, ISaveFile
    {
        /// <summary>
        /// Функция доступа к точке по ее индексу в списке
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IPathPoint Point(int index);

        /// <summary>
        /// Количество точек
        /// </summary>
        int Count{get;}

        /// <summary>
        /// Возращает точку поее индексу
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IPathPoint this[int index] { get; }

        /// <summary>
        /// Размерность значений энергии
        /// </summary>
        ScaleDimension EnergyDimension{ get; }

        /// <summary>
        /// Добавляет точку в путь реакции
        /// </summary>
        /// <param name="p"></param>
        /// <param name="time"></param>
        void Add(IPointD p, double time);
        void Add(double q1, double q2, double time);

        /// <summary>
        /// Удаляет точку
        /// </summary>
        /// <param name="index"></param>
        void Delete(int index);

        /// <summary>
        /// Индекс текущей точки
        /// </summary>
        int CurrentPointIndex{get; set;}
        /// <summary>
        /// Текущая точка
        /// </summary>
        IPathPoint CurrentPoint { get; }

        /// <summary>
        /// Индекс точки иинимального значения энергии
        /// </summary>
        int MinEnergyPointIndex { get; }
        /// <summary>
        /// Индекс точки максимального значения энергии
        /// </summary>
        int MaxEnergyPointIndex { get; }

        /// <summary>
        /// Точка минимального значения энергии
        /// </summary>
        IPathPoint MinEnergyPoint { get; }
        /// <summary>
        /// ТОчка максимальноо значения энергии
        /// </summary>
        IPathPoint MaxEnergyPoint { get; }

        /// <summary>
        /// Описание пути реакции
        /// </summary>
        string Name{get; set;}

        /// <summary>
        /// Событие возникающее при изменении текущей точки
        /// </summary>
        event EventHandler CurrentPointChanged;
    }

    /// <summary>
    /// Точка в на пути реакции
    /// </summary>
    public interface IPathPoint: IPointValue
    {
        double Time{get;}
    }

    /// <summary>
    /// Вычисляемая точка
    /// </summary>
    public interface ICalculationPath
    {
        IPath CalcPath(ISurface3D PES);
    }
}
