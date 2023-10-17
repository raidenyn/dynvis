using System;
using System.Collections.Generic;
using System.Linq;
using DynVis.Core.IO;
using DynVis.Core.Elements;
using DynVis.Math;

namespace DynVis.Core
{
    /// <summary>
    /// Интерфейс работы с отдельным атомом
    /// </summary>
    public interface IAtom
    {
        /// <summary>
        /// Номер элемента в таблице Менделеева
        /// </summary>
        int Element { get; }
        /// <summary>
        /// Декартова координата атома
        /// </summary>
        IPoint3D Coordinate { get; }

        int Number { get; }
    }

    /// <summary>
    /// Интерфейс работы сос вязью меду атомами
    /// </summary>
    public interface IBond
    {
        /// <summary>
        /// Первый атом в связи
        /// </summary>
        IAtom Atom1 { get; }
        /// <summary>
        /// Второй атом в связи
        /// </summary>
        IAtom Atom2 { get; }

        /// <summary>
        /// Тип связи
        /// </summary>
        BondType BondType { get; }
    }
    
    public interface IBondList: ICollection<IBond>
    {
        IBond GetBond(IAtom atom1, IAtom atom2);
    }

    /// <summary>
    /// Интерфейс работы с атомной структурой
    /// </summary>
    public interface IAtomStructure : IStaticCollection<IAtom>
    {
        IBondList BondList { get; }

        string ToString();

        event EventHandler GeometryChanged;
    }

    /// <summary>
    /// Интерфейс работы со структурой реагирующей системы на трехмерной поверхности ППЭ
    /// </summary>
    public interface IReactionSystemGeometry: ISaveFile
    {
        /// <summary>
        /// Текущая позиция реагирующий системы
        /// </summary>
        /// <param name="q1">Первая координата системы</param>
        /// <param name="q2">Вторая координата системы</param>
        /// <returns>Возращает true, если установка координат прошла успешно</returns>
        bool SetPoint(double q1, double q2);
        bool SetPoint(IPointD P);
        /// <summary>
        /// Возращает текущую геометрию системы отвечающую Q1 и Q2
        /// </summary>
        IAtomStructure AtomStructure { get; }
        /// <summary>
        /// Первая координата реагирующей системы
        /// </summary>
        double Q1 { get; }
        /// <summary>
        /// Вторая координата реагирующей системы
        /// </summary>
        double Q2 { get; }
    }

    /// <summary>
    /// Интерфейс работы с набором геометрических точек
    /// </summary>
    public interface IStructure : IStaticCollection<Point3D>, IFormattable, ICloneable
    {

    }

    static public class AtomStructureExtension
    {
        public static IEnumerable<IPoint3D> ToPointEnumerable(this IAtomStructure atomStructure)
        {
            return from atom in atomStructure select atom.Coordinate;
        }
    }
}
