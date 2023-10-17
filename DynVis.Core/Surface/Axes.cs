using System;
using System.Text.RegularExpressions;
using DynVis.Core.Properties;
using DynVis.Core.Surface;


namespace DynVis.Core
{
    /// <summary>
    /// Класс описания координатной оси 
    /// </summary>
    public class Axes : IAxes
    {
        public Axes()
        {
            Name = LangGeneral.Coordinate;
            ScaleDimension = new ScaleDimension(DimLength.Angstrom);
        }

        public Axes(IAxes axes, GetCoordValue getMinValue, GetCoordValue getMaxValue)
        {
            SetAs(axes, getMinValue, getMaxValue);
        }

        public Axes(IAxes axes)
            : this(axes, null, null)
        {
        }

        protected void SetAs(IAxes axes, GetCoordValue getMinValue, GetCoordValue getMaxValue)
        {
            Name = axes.Name;
            ScaleDimension = axes.ScaleDimension;
            TypeMaxValue = axes.TypeMaxValue;
            TypeMinValue = axes.TypeMinValue;
            if (getMaxValue == null) MaxValue = axes.MaxValue;
            if (getMinValue == null) MinValue = axes.MinValue;
            GetMaxValue = getMaxValue;
            GetMinValue = getMinValue;
        }

        protected void SetAs(IAxes axes)
        {
            SetAs(axes, null, null);
        }

        /// <summary>
        /// Подпись к оси
        /// </summary>
        [Savable]
        public string Name { get; set; }

        /// <summary>
        /// Размерность оси
        /// </summary>
        [Savable]
        public ScaleDimension ScaleDimension { get; set; }
        
        /// <summary>
        /// Максимальное значение
        /// </summary>
        [Savable]
        public double MaxValue
        {
            get { return GetMaxValue != null ? GetMaxValue() : _MaxValue; }
            set
            {
                _MaxValue = value;
                GetMaxValue = null;
            }
        }

        /// <summary>
        /// Минимальное значение
        /// </summary>
        [Savable]
        public double MinValue
        {
            get { return GetMinValue != null ? GetMinValue() : _MinValue; }
            set
            {
                _MinValue = value;
                GetMinValue = null;
            }
        }

        public CoordinateType TypeMaxValue { get; set; }
        public CoordinateType TypeMinValue { get; set; }
        
        public bool SetFromString(string st, int startPosition)
        {
            var Line = st.FromPositionToEndLine(startPosition);

            var REName = new Regex(@"'(?<name>.*)'");
            var REDim = new Regex(@"\[(?'dim'.*)\]");
            var RETypeMin = new Regex(@"\((?'min'.*)\,");
            var RETypeMax = new Regex(@"(?'max'.*)\)");

            

            string name = StringExtended.ReadValueFromString(ref Line, REName, "name");

            var scaleDimension = new ScaleDimension(DimLength.Angstrom);
            var match = REDim.Match(Line);
            if (!(match.Success && scaleDimension.SetFromString(match.Groups["dim"].Value)))
            {
                return false;
            }

            CoordinateType coordMin, coordMax;

            var temp = StringExtended.ReadValueFromString(ref Line, RETypeMin, "min");
            if (!string.IsNullOrEmpty(temp))
            {
                coordMin = StringExtended.GetEnum<CoordinateType>(temp);
            } 
            else
            {
                return false;
            }

            temp = StringExtended.ReadValueFromString(ref Line, RETypeMax, "max");
            if (!string.IsNullOrEmpty(temp))
            {
                coordMax = StringExtended.GetEnum<CoordinateType>(temp);
            }
            else
            {
                return false;
            }


            if (name != null)
            {
                Name = name;
                ScaleDimension = scaleDimension;
                TypeMaxValue = coordMax;
                TypeMinValue = coordMin;

                return true;
            }

            return false;
        }

        public static Axes CreateFromString(string st, int startPosition)
        {
            var Result = new Axes();
            return Result.SetFromString(st, startPosition) ? Result : null;
        }

        protected GetCoordValue GetMaxValue;
        protected GetCoordValue GetMinValue;

        private double _MaxValue;
        private double _MinValue;

        public object Clone()
        {
            return new Axes(this, GetMinValue, GetMaxValue);
        }

        public string GetAsString()
        {
            return String.Format("'{0}' [{1}]    ({2},{3})", Name, ScaleDimension.ToSavedString(), TypeMinValue, TypeMaxValue);
        }
    }

    public delegate double GetCoordValue();
}
