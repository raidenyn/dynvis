#region

using System;
using System.Globalization;

#endregion

namespace DevAge.Text.FixedLength
{
    /// <summary>
    /// Required attribute to specify the field position and length
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldAttribute : Attribute
    {
        private readonly int mFieldIndex;

        private readonly int mLength;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fieldIndex">Index of the field, 0 based. Each field must have a unique progressive index</param>
        /// <param name="length">Lenght of the field when readed and writed to the string.</param>
        public FieldAttribute(int fieldIndex, int length)
        {
            mFieldIndex = fieldIndex;
            mLength = length;
        }

        public int FieldIndex
        {
            get { return mFieldIndex; }
        }

        public int Length
        {
            get { return mLength; }
        }
    }

    /// <summary>
    /// Attribute used to specify additional parse options
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ParseFormatAttribute : Attribute
    {
        /// <summary>
        /// Constructor. Use one of these properties to customize the format: CultureInfo, DateTimeFormat, NumberFormat, TrimBeforeParse.
        /// Default is Invariant culture format.
        /// </summary>
        public ParseFormatAttribute()
        {
            CultureInfo invariant = CultureInfo.InvariantCulture;

            DateTimeFormat = invariant.DateTimeFormat.ShortDatePattern;
            NumberFormat = "+00000000.0000;-00000000.0000";
            TrimBeforeParse = true;
            CultureInfo = invariant;
        }

        public CultureInfo CultureInfo { get; set; }

        public string DateTimeFormat { get; set; }

        public string NumberFormat { get; set; }

        public bool TrimBeforeParse { get; set; }
    }


    /// <summary>
    /// Attribute used to convert a specific value to another value
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ValueMappingAttribute : Attribute
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stringValue">String value</param>
        /// <param name="fieldValue">Field typed value</param>
        public ValueMappingAttribute(string stringValue, object fieldValue)
        {
            StringValue = stringValue;
            FieldValue = fieldValue;
        }

        public string StringValue { get; set; }

        public object FieldValue { get; set; }
    }

    /// <summary>
    /// Attribute used to specify the standard value (mandatory value) for a specific field.
    /// You can use this attribute for example when you want a particular field to only accept one or more standard values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class StandardValueAttribute : Attribute
    {
        /// <summary>
        /// Construcotr
        /// </summary>
        /// <param name="standardValue">Required value</param>
        public StandardValueAttribute(object standardValue)
        {
            StandardValue = standardValue;
        }

        public object StandardValue { get; set; }
    }
}