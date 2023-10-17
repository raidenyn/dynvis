using System;
using System.Text.RegularExpressions;
using DynVis.Core.Properties;

/*
--------------------Функции работы с размерностями----------------------------------------

Находятся на этапе разработки и осмысления 
*/

namespace DynVis.Core
{
    /// <summary>
    /// Физические значения
    /// </summary>
    public enum DimensionType { None, Lenght, ReverseLenth, Mass, Energy, Time, Force, Angle }
    
    /// <summary>
    /// Масштабные коэфициенты
    /// </summary>
    public enum ScaleCoeff
    {
        Yotta,
        Zetta,
        Hexa,
        Peta,
        Tera,
        Giga,
        Mega,
        kilo,
        hecto,
        deca,
        normal,
        deci,
        centi,
        milli,
        micro,
        nano,
        pico,
        femto,
        atto,
        zepto,
        yocto
    } ;

    /// <summary>
    /// Масштабные коэфициенты
    /// </summary>
    public class DimensionCoeff
    {
        /// <summary>
        /// Название масштаба
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// Сокращенное название
        /// </summary>
        public readonly string ShortName;
        /// <summary>
        /// Коэфициент преобразования
        /// </summary>
        public readonly double CoeffValue;
        /// <summary>
        /// Внутренее обозначение
        /// </summary>
        public readonly ScaleCoeff ScaleCoeff;

        private DimensionCoeff(string name, string shortName, double coeffValue, ScaleCoeff scaleCoeff)
        {
            Name = name;
            ScaleCoeff = scaleCoeff;
            CoeffValue = coeffValue;
            ShortName = shortName;
        }

        /// <summary>
        /// Массив масштабных коэфициентов
        /// </summary>
        internal static readonly DimensionCoeff[] Coeffs = new[]
                                                              {
                                                                  new DimensionCoeff(LangDimension.Iotta, LangDimension.Iotta_s, 1E+24, ScaleCoeff.Yotta),
                                                                  new DimensionCoeff(LangDimension.Zetta, LangDimension.Zetta_s, 1E+21, ScaleCoeff.Zetta),
                                                                  new DimensionCoeff(LangDimension.Exsa, LangDimension.Exsa_s, 1E+18, ScaleCoeff.Hexa),
                                                                  new DimensionCoeff(LangDimension.Peta, LangDimension.Peta_s, 1E+15, ScaleCoeff.Peta),
                                                                  new DimensionCoeff(LangDimension.Tera, LangDimension.Тera_s, 1E+12, ScaleCoeff.Tera),
                                                                  new DimensionCoeff(LangDimension.Giga, LangDimension.Giga_s, 1E+9, ScaleCoeff.Giga),
                                                                  new DimensionCoeff(LangDimension.Mega, LangDimension.Mega_s, 1E+6, ScaleCoeff.Mega),
                                                                  new DimensionCoeff(LangDimension.Kilo, LangDimension.Kilo_s, 1E+3, ScaleCoeff.kilo),
                                                                  new DimensionCoeff(LangDimension.Hecto, LangDimension.Hecto_s, 1E+2, ScaleCoeff.hecto),
                                                                  new DimensionCoeff(LangDimension.Deca, LangDimension.Deca_s, 1E+1, ScaleCoeff.deca),
                                                                  new DimensionCoeff(String.Empty, String.Empty, 1, ScaleCoeff.normal),
                                                                  new DimensionCoeff(LangDimension.Deci, LangDimension.Deci_s, 1E-1, ScaleCoeff.deci),
                                                                  new DimensionCoeff(LangDimension.Santi, LangDimension.Santi_s, 1E-2, ScaleCoeff.centi),
                                                                  new DimensionCoeff(LangDimension.Milli, LangDimension.Mili_s, 1E-3, ScaleCoeff.milli),
                                                                  new DimensionCoeff(LangDimension.Micro, LangDimension.Micro_s, 1E-6, ScaleCoeff.micro),
                                                                  new DimensionCoeff(LangDimension.Nano, LangDimension.Nano_s, 1E-9, ScaleCoeff.nano),
                                                                  new DimensionCoeff(LangDimension.Pico, LangDimension.Pico_s, 1E-12, ScaleCoeff.pico),
                                                                  new DimensionCoeff(LangDimension.Femto, LangDimension.Femto_s, 1E-15, ScaleCoeff.femto),
                                                                  new DimensionCoeff(LangDimension.Atto, LangDimension.Atto_s, 1E-18, ScaleCoeff.atto),
                                                                  new DimensionCoeff(LangDimension.Zepto, LangDimension.Zepto_s, 1E-21, ScaleCoeff.zepto),
                                                                  new DimensionCoeff(LangDimension.Iokto, LangDimension.Iokto_s, 1E-24, ScaleCoeff.yocto),
                                                              };


        public static DimensionCoeff Coeff(ScaleCoeff coeffTypeindex)
        {
            return Coeffs[(int)coeffTypeindex];
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public interface IDimType
    {
        Dimension StandartDimension { get; }
        DimensionType Type { get; }
        Dimension[] DimensionTypes { get; }
    }

    [Savable]
    public abstract class Dimensions : IDimType
    {
        public string Name{get; protected set;}

        public abstract Dimension StandartDimension { get; }
        public abstract DimensionType Type { get; }

        public abstract Dimension[] DimensionTypes { get; }

        public static Dimensions GetDimensions(DimensionType dimensionType)
        {
            return DimensionsList[(int) dimensionType];
        }

        internal static readonly Dimensions[] DimensionsList = new Dimensions[]
                                                                  {
                                                                      new DimNone(),
                                                                      new DimLength(),
                                                                      new DimReverseLenth(), 
                                                                      new DimMass(),
                                                                      new DimEnergy(),
                                                                      new DimTime(),
                                                                      new DimForce(),
                                                                      new DimAngle()
                                                                  };

        public virtual double ConvertScale(double v, ScaleCoeff currentScaleCoeff, ScaleCoeff destinationScaleCoeff)
        {
            return v * (DimensionCoeff.Coeff(currentScaleCoeff).CoeffValue / DimensionCoeff.Coeff(destinationScaleCoeff).CoeffValue);
        }

        public override string ToString()
        {
            return Name;
        }

        public static DimensionType GetDimensionType(Dimension dimension)
        {
            foreach (var dims in DimensionsList)
            {
                foreach (var dim in dims.DimensionTypes)
                {
                    if (dim == dimension) return dims.Type;
                }
            }
            return 0;
        }
    }

    
    public class Dimension
    {
        internal readonly string WorkName;
        [Savable]
        public readonly string EngName;
        public string Name
        {
            get { return String.Format(WorkName, String.Empty); }
        }
        public readonly string ShortName;
        public readonly double Coeff;
        
        [Savable]
        public readonly DimensionType Type;

        /// <summary>
        /// Конструктор для десериализации
        /// </summary>
        public Dimension()
        {

        }

        internal Dimension(string name, string engName, string shortName, double coeff, DimensionType type)
        {
            EngName = engName;
            WorkName = name;
            Coeff = coeff;
            ShortName = shortName;
            Type = type;
        }

        public override string ToString()
        {
            return Name;
        }

        public string ToSavableString()
        {
            return EngName;
        }

        public static Dimension FromString(string nameDim)
        {
            foreach (var dimType in Dimensions.DimensionsList)
            {
                foreach (var dim in dimType.DimensionTypes)
                {
                    if (dim.EngName == nameDim) return dim;
                }
            }
            return null;

        }
    }

    
    public class ScaleDimension: ICloneable
    {
        public ScaleDimension()
        {
            Dimension = DimLength.Angstrom;
            ScaleCoeff = ScaleCoeff.normal;
        }
        
        public ScaleDimension(Dimension currentDimension)
        {
            Dimension = currentDimension;
            ScaleCoeff = ScaleCoeff.normal;
        }

        public ScaleDimension(Dimension currentDimension, ScaleCoeff currentScaleCoeff)
            : this(currentDimension)
        {
            ScaleCoeff = currentScaleCoeff;
        }

        public ScaleDimension(string str)
        {
            SetFromString(str);
        }

        [Savable]
        public Dimension Dimension
        {
            get;
            set;
        }

        [Savable]
        public ScaleCoeff ScaleCoeff
        {
            get;
            set;
        }

        public static string GetFullShortName(ScaleCoeff sc, Dimension d)
        {
            return String.Format(d.ShortName, DimensionCoeff.Coeff(sc).ShortName);
        }

        public string GetFullShortName()
        {
            return GetFullShortName(ScaleCoeff, Dimension);
        }

        public static string GetFullName(ScaleCoeff sc, Dimension d)
        {
            var C = DimensionCoeff.Coeff(sc).Name;
            var D = d.WorkName;
            return String.Format(String.IsNullOrEmpty(C) ? D : D.ToLower(), C);
        }

        public string GetFullName()
        {
            return GetFullName(ScaleCoeff, Dimension);
        }

        public override string ToString()
        {
            return GetFullName();
        }

        public object Clone()
        {
            return new ScaleDimension(Dimension, ScaleCoeff);
        }

        public string ToSavedString()
        {
            return String.Format("{0} {1}", ScaleCoeff, Dimension.ToSavableString());
        }

        public bool SetFromString(string str)
        {
            var REScale = new Regex(@"\b(?<scale>.*) ");
            var REDim = new Regex(@"\b(?'dim'.*)\b");

            ScaleCoeff scale;
            
            var temp = StringExtended.ReadValueFromString(ref str, REScale, "scale");
            if (!string.IsNullOrEmpty(temp))
            {
                scale = StringExtended.GetEnum<ScaleCoeff>(temp);
            }
            else
            {
                return false;
            }

            var dim = Dimension.FromString(StringExtended.ReadValueFromString(ref str, REDim, "dim"));

            if (dim != null)
            {
                ScaleCoeff = scale;
                Dimension = dim;
                return true;
            }
            return false;
        }
    }

    [Savable]
    public class DimensionVarlible
    {
        public DimensionVarlible(Dimension currentDimension)
        {
            ScaleDimension = new ScaleDimension(currentDimension);
        }

        public DimensionVarlible(Dimension currentDimension, ScaleCoeff currentScaleCoeff)
            : this(currentDimension)
        {
            ScaleDimension.ScaleCoeff = currentScaleCoeff;
        }

        private readonly ScaleDimension ScaleDimension;

        public Dimension CurrentDimension
        {
            get { return ScaleDimension.Dimension; }
            set
            {
                Value = ConvertTo(value, CurrentDimension, Value);
                ScaleDimension.Dimension = value;
            }
        }

        public ScaleCoeff CurrentScaleCoeff
        {
            get { return ScaleDimension.ScaleCoeff; }
            set
            {
                Value = ConvertTo(value, CurrentScaleCoeff, Value);
                ScaleDimension.ScaleCoeff = value;
            }
        }

        public double Value{get;set;}

        public static double ConvertTo(Dimension destinationDimension, Dimension currentDimension, double v)
        {
            return v * currentDimension.Coeff / destinationDimension.Coeff;
        }

        public static DimensionVarlible ConvertTo(Dimension destinationDimension, DimensionVarlible dv)
        {
            return new DimensionVarlible(destinationDimension, dv.CurrentScaleCoeff)
                       {
                           Value = ConvertTo(destinationDimension, dv.CurrentDimension, dv.Value)
                       };
        }

        public static DimensionVarlible ConvertTo(Dimension destinationDimension, ScaleCoeff destinationScaleCoeff, DimensionVarlible dv)
        {
            var Result = ConvertTo(destinationDimension, dv);
            Result.CurrentScaleCoeff = destinationScaleCoeff;
            return Result;
        }

        public double ConvertTo(Dimension destinationDimension, ScaleCoeff destinationScaleCoeff, Dimension currentDimension, ScaleCoeff currentScaleCoeff, double v)
        {
            return Dimensions.GetDimensions(CurrentDimension.Type).ConvertScale(ConvertTo(destinationDimension, currentDimension, v), currentScaleCoeff, destinationScaleCoeff);
        }

        public double ConvertTo(ScaleCoeff destinationScaleCoeff, ScaleCoeff currentScaleCoeff, double v)
        {
            return Dimensions.GetDimensions(CurrentDimension.Type).ConvertScale(v, currentScaleCoeff, destinationScaleCoeff);
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", Value, ScaleDimension.GetFullShortName());
        }

        public string GetFullName()
        {
            return ScaleDimension.GetFullName();
        }

        public string GetFullShortName()
        {
            return ScaleDimension.GetFullShortName();
        }

        public ScaleDimension GetScaleDimension()
        {
            return (ScaleDimension)ScaleDimension.Clone();
        }

        public void SetScaleDimension(ScaleDimension sd)
        {
            CurrentDimension = sd.Dimension;
            CurrentScaleCoeff = sd.ScaleCoeff;
        }
    }
}
