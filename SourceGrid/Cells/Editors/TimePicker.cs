#region

using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevAge.ComponentModel.Converter;

#endregion

namespace SourceGrid.Cells.Editors
{
    /// <summary>
    /// Create an Editor that use a DateTimePicker as control for time editing.
    /// </summary>
    [ToolboxItem(false)]
    public class TimePicker : DateTimePicker
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TimePicker() : this("T", new[] {"T"})
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TimePicker(String toStringFormat, string[] p_ParseFormats)
        {
            var timeConverter = new DateTimeTypeConverter(toStringFormat, p_ParseFormats);
            TypeConverter = timeConverter;
        }

        #region Edit Control

        /// <summary>
        /// Gets the control used for editing the cell.
        /// </summary>
        public new System.Windows.Forms.DateTimePicker Control
        {
            get { return base.Control; }
        }

        /// <summary>
        /// Create the editor control
        /// </summary>
        /// <returns></returns>
        protected override Control CreateControl()
        {
            var dtPicker = new System.Windows.Forms.DateTimePicker();
            dtPicker.Format = DateTimePickerFormat.Time;
            dtPicker.ShowUpDown = true;
            return dtPicker;
        }

        #endregion
    }
}