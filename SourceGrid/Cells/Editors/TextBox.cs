#region

using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevAge.Windows.Forms;

#endregion

namespace SourceGrid.Cells.Editors
{
    /// <summary>
    /// An editor that use a TextBoxTyped for editing support.
    /// </summary>
    [ToolboxItem(false)]
    public class TextBox : EditorControlBase
    {
        #region Constructor

        /// <summary>
        /// Construct a Model. Based on the Type specified the constructor populate AllowNull, DefaultValue, TypeConverter, StandardValues, StandardValueExclusive
        /// </summary>
        /// <param name="p_Type">The type of this model</param>
        public TextBox(Type p_Type) : base(p_Type)
        {
        }

        #endregion

        #region Edit Control

        /// <summary>
        /// Gets the control used for editing the cell.
        /// </summary>
        public new DevAgeTextBox Control
        {
            get { return (DevAgeTextBox) base.Control; }
        }

        /// <summary>
        /// Create the editor control
        /// </summary>
        /// <returns></returns>
        protected override Control CreateControl()
        {
            var editor = new DevAgeTextBox();
            editor.BorderStyle = BorderStyle.None;
            editor.AutoSize = false;
            editor.Validator = this;
            return editor;
        }

        #endregion

        /// <summary>
        /// This method is called just before the edit start. You can use this method to customize the editor with the cell informations.
        /// </summary>
        /// <param name="cellContext"></param>
        /// <param name="editorControl"></param>
        protected override void OnStartingEdit(CellContext cellContext, Control editorControl)
        {
            base.OnStartingEdit(cellContext, editorControl);

            var l_TxtBox = (DevAgeTextBox) editorControl;
            l_TxtBox.WordWrap = cellContext.Cell.View.WordWrap;
            l_TxtBox.TextAlign = Utilities.ContentToHorizontalAlignment(cellContext.Cell.View.TextAlignment);

            //to set the scroll of the textbox to the initial position (otherwise the textbox use the previous scroll position)
            l_TxtBox.SelectionStart = 0;
            l_TxtBox.SelectionLength = 0;
        }

        /// <summary>
        /// Set the specified value in the current editor control.
        /// </summary>
        /// <param name="editValue"></param>
        public override void SetEditValue(object editValue)
        {
            Control.Value = editValue;
            Control.SelectAll();
        }

        /// <summary>
        /// Returns the value inserted with the current editor control
        /// </summary>
        /// <returns></returns>
        public override object GetEditedValue()
        {
            return Control.Value;
        }

        protected override void OnSendCharToEditor(char key)
        {
            Control.Text = key.ToString();
            if (Control.Text != null)
                Control.SelectionStart = Control.Text.Length;
        }
    }
}