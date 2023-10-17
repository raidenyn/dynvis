#region

using System;
using System.ComponentModel;
using DevAge.ComponentModel.Converter;

#endregion

namespace SourceGrid.Cells.Editors
{
    /// <summary>
    /// An editor to support Currency data type
    /// </summary>
    [ToolboxItem(false)]
    public class TextBoxCurrency : TextBoxNumeric
    {
        #region Constructor

        /// <summary>
        /// Construct a Model. Based on the Type specified the constructor populate AllowNull, DefaultValue, TypeConverter, StandardValues, StandardValueExclusive
        /// </summary>
        /// <param name="p_Type">The type of this model</param>
        public TextBoxCurrency(Type p_Type) : base(p_Type)
        {
            TypeConverter = new CurrencyTypeConverter(p_Type);
        }

        #endregion
    }
}