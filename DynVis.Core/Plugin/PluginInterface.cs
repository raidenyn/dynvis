using System;
using System.Drawing;


namespace DynVis.Core.Plugin
{
    /// <summary>
    /// Базовый интерфейс плагина Динвиза
    /// </summary>
    public interface IDynVisPlugin
    {
        string PluginName { get; }
        string CaptionText { get; }
        Image CaptionImage { get; }
    }




    /// <summary>
    /// Интерфейс плагина расчета поверхности
    /// </summary>
    public interface IDynVisSurfacePlugin : IDynVisPlugin
    {
        ReactionData CreateReactionData();
    }

    /// <summary>
    /// Атрибут плагина расчета поверхности
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DynVisSurfacePlugin : Attribute
    {

    }




    /// <summary>
    /// Интерфейс плагина расчета пути
    /// </summary>
    public interface IDynVisPathPlugin : IDynVisPlugin
    {
        ICalculationPath CalculationPath { get; }
    }

    /// <summary>
    ///  Атрибут плагина расчета пути
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DynVisPathPlugin : Attribute
    {

    }




    /// <summary>
    /// Интерфейс плагина расчета точек
    /// </summary>
    public interface IDynVisPointsPlugin : IDynVisPlugin
    {
        ICalcSurfacePoints CalculationPoints { get; }
    }

    /// <summary>
    /// Атрибут плагина расчета точек
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DynVisPointsPlugin : Attribute
    {

    }
}
