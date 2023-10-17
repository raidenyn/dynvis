#region

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using BorderStyle=DevAge.Drawing.BorderStyle;

#endregion

namespace SourceGrid.Cells.Editors
{
    /// <summary>
    ///  An editor that use a UITypeEditor to edit the cell.
    /// </summary>
    [ToolboxItem(false)]
    public class TextBoxUITypeEditor : TextBoxButton
    {
        #region Constructor

        /// <summary>
        /// Construct a Model. Based on the Type specified the constructor populate AllowNull, DefaultValue, TypeConverter, StandardValues, StandardValueExclusive
        /// </summary>
        /// <param name="p_Type">The type of this model</param>
        public TextBoxUITypeEditor(Type p_Type) : base(p_Type)
        {
        }

        #endregion

        #region Edit Control

        /// <summary>
        /// Gets the control used for editing the cell.
        /// </summary>
        public new DevAge.Windows.Forms.TextBoxUITypeEditor Control
        {
            get { return (DevAge.Windows.Forms.TextBoxUITypeEditor) base.Control; }
        }

        /// <summary>
        /// Create the editor control
        /// </summary>
        /// <returns></returns>
        protected override Control CreateControl()
        {
            var editor = new DevAge.Windows.Forms.TextBoxUITypeEditor();
            editor.BorderStyle = BorderStyle.None;
            editor.Validator = this;

            object objEditor = TypeDescriptor.GetEditor(base.ValueType, typeof (UITypeEditor));
            UITypeEditor uiEditor = null;
            if (editor != null)
                uiEditor = (UITypeEditor) objEditor;

            editor.UITypeEditor = uiEditor;
            return editor;
        }

        #endregion
    }
}