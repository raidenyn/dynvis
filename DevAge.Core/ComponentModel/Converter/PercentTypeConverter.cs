#region

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

#endregion

namespace DevAge.ComponentModel.Converter
{
    /// <summary>
    /// A TypeConverter that support string conversion from and to string with the percent symbol.
    /// Support Conversion for Float, Double and Decimal
    /// </summary>
    public class PercentTypeConverter :
#if !MINI
        TypeConverter
#else
		DevAge.ComponentModel.TypeConverter
#endif
    {
        #region Constructors

        public PercentTypeConverter(Type p_BaseType)
        {
            BaseType = p_BaseType;
        }

        public PercentTypeConverter(Type p_BaseType,
                                    string p_Format) : this(p_BaseType)
        {
            Format = p_Format;
        }

        #endregion

        #region Member Variables

#if !MINI
        private Type m_BaseType;
        private TypeConverter m_BaseTypeConverter;
        private bool m_ConsiderAllStringAsPercent = true;
        private string m_Format = "P";
        private NumberStyles m_NumberStyles = NumberStyles.Number;

        public TypeConverter BaseTypeConverter
#else
		private DevAge.ComponentModel.TypeConverter m_BaseTypeConverter;
		public DevAge.ComponentModel.TypeConverter BaseTypeConverter
#endif
        {
            get { return m_BaseTypeConverter; }
            set { m_BaseTypeConverter = value; }
        }

        public Type BaseType
        {
            get { return m_BaseType; }
            set
            {
                if (value != typeof (double) &&
                    value != typeof (float) &&
                    value != typeof (decimal))
                    throw new ArgumentException("Type not supported", "BaseType");

#if !MINI
                m_BaseTypeConverter = TypeDescriptor.GetConverter(value);
#else
				m_BaseTypeConverter = DevAge.ComponentModel.TypeDescriptor.GetConverter(value);
#endif

                m_BaseType = value;
            }
        }

        public string Format
        {
            get { return m_Format; }
            set { m_Format = value; }
        }

        /// <summary>
        /// If true and the user insert a string with no percent symbel the value is divided by 100, otherwise not.
        /// </summary>
        public bool ConsiderAllStringAsPercent
        {
            get { return m_ConsiderAllStringAsPercent; }
            set { m_ConsiderAllStringAsPercent = value; }
        }

//		private System.IFormatProvider m_FormatProvider = null;
//		public IFormatProvider FormatProvider
//		{
//			get{return m_FormatProvider;}
//			set{m_FormatProvider = value;}
//		}
//

        public NumberStyles NumberStyles
        {
            get { return m_NumberStyles; }
            set { m_NumberStyles = value; }
        }

        #endregion

        #region Implementation TypeConverter

        public override bool CanConvertFrom(ITypeDescriptorContext context,
                                            Type sourceType)
        {
            if (sourceType == typeof (string))
                return true;
            else
                return m_BaseTypeConverter.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context,
                                          Type destinationType)
        {
            if (destinationType == typeof (string))
                return true;
            else
                return m_BaseTypeConverter.CanConvertTo(context, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            return m_BaseTypeConverter.CreateInstance(context, propertyValues);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value != null && value.GetType() == typeof (string))
            {
                if (BaseType == typeof (double))
                    return StringToDouble((string) value, NumberStyles, GetCulture(culture).NumberFormat,
                                          ConsiderAllStringAsPercent);
                else if (BaseType == typeof (decimal))
                    return StringToDecimal((string) value, NumberStyles, GetCulture(culture).NumberFormat,
                                           ConsiderAllStringAsPercent);
                else if (BaseType == typeof (float))
                    return StringToFloat((string) value, NumberStyles, GetCulture(culture).NumberFormat,
                                         ConsiderAllStringAsPercent);
                else
                    throw new ArgumentException("Not supported type");
            }
            else
                return m_BaseTypeConverter.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                                         Type destinationType)
        {
            if (destinationType == typeof (string) && value != null)
            {
                if (BaseType == typeof (double))
                    return DoubleToString((double) value, Format, GetCulture(culture).NumberFormat);
                else if (BaseType == typeof (decimal))
                    return DecimalToString((decimal) value, Format, GetCulture(culture).NumberFormat);
                else if (BaseType == typeof (float))
                    return FloatToString((float) value, Format, GetCulture(culture).NumberFormat);
                else
                    return m_BaseTypeConverter.ConvertTo(context, culture, value, destinationType);
            }
            else
                return m_BaseTypeConverter.ConvertTo(context, culture, value, destinationType);
        }


        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return m_BaseTypeConverter.GetCreateInstanceSupported(context);
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value,
                                                                   Attribute[] attributes)
        {
            return m_BaseTypeConverter.GetProperties(context, value, attributes);
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return m_BaseTypeConverter.GetPropertiesSupported(context);
        }

        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return m_BaseTypeConverter.GetStandardValues(context);
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return m_BaseTypeConverter.GetStandardValuesExclusive(context);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return m_BaseTypeConverter.GetStandardValuesSupported(context);
        }

        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            if (value != null && value.GetType() == typeof (string))
            {
                //I try to convert it
                try
                {
                    ConvertFrom(context, CultureInfo.CurrentCulture, value);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
                return m_BaseTypeConverter.IsValid(context, value);
        }

        #endregion

        #region Member Utility Functino

        private CultureInfo GetCulture(CultureInfo p_RequestedCulture)
        {
            if (p_RequestedCulture == null)
                return CultureInfo.CurrentCulture;
            else
                return p_RequestedCulture;
        }

        #endregion

        #region Conversion String Methods

        public static bool IsPercentString(string p_strVal, IFormatProvider provider)
        {
            NumberFormatInfo l_Info;
            if (provider == null)
                l_Info = CultureInfo.CurrentCulture.NumberFormat;
            else
                l_Info = (NumberFormatInfo) provider.GetFormat(typeof (NumberFormatInfo));

            if (p_strVal.IndexOf(l_Info.PercentSymbol) == -1)
                return false;
            else
                return true;
        }

        public static double StringToDouble(string p_strVal,
                                            NumberStyles style,
                                            IFormatProvider provider,
                                            bool p_ConsiderAllStringAsPercent)
        {
            bool l_IsPercentString = IsPercentString(p_strVal, provider);
            if (l_IsPercentString)
            {
                return double.Parse(p_strVal.Replace("%", ""), style, provider)/100.0;
            }
            else
            {
                if (p_ConsiderAllStringAsPercent)
                    return double.Parse(p_strVal, style, provider)/100.0;
                else
                    return double.Parse(p_strVal, style, provider);
            }
        }

        public static float StringToFloat(string p_strVal,
                                          NumberStyles style,
                                          IFormatProvider provider,
                                          bool p_ConsiderAllStringAsPercent)
        {
            bool l_IsPercentString = IsPercentString(p_strVal, provider);
            if (l_IsPercentString)
            {
                return float.Parse(p_strVal.Replace("%", ""), style, provider)/100;
            }
            else
            {
                if (p_ConsiderAllStringAsPercent)
                    return float.Parse(p_strVal, style, provider)/100;
                else
                    return float.Parse(p_strVal, style, provider);
            }
        }

        public static decimal StringToDecimal(string p_strVal,
                                              NumberStyles style,
                                              IFormatProvider provider,
                                              bool p_ConsiderAllStringAsPercent)
        {
            bool l_IsPercentString = IsPercentString(p_strVal, provider);
            if (l_IsPercentString)
            {
                return decimal.Parse(p_strVal.Replace("%", ""), style, provider)/100.0M;
            }
            else
            {
                if (p_ConsiderAllStringAsPercent)
                    return decimal.Parse(p_strVal, style, provider)/100.0M;
                else
                    return decimal.Parse(p_strVal, style, provider);
            }
        }


        public static string DoubleToString(double p_Val, String format, IFormatProvider provider)
        {
            return p_Val.ToString(format, provider);
        }

        public static string FloatToString(float p_Val, String format, IFormatProvider provider)
        {
            return p_Val.ToString(format, provider);
        }

        public static string DecimalToString(decimal p_Val, String format, IFormatProvider provider)
        {
            return p_Val.ToString(format, provider);
        }

        #endregion
    }
}