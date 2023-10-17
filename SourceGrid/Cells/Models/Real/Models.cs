#region

using System;
using DevAge.ComponentModel;
using DevAge.ComponentModel.Validator;
using DevAge.Drawing;

#endregion

namespace SourceGrid.Cells.Models
{
    public class NullValueModel : IValueModel
    {
        public static readonly NullValueModel Default = new NullValueModel();

        #region IValueModel Members

        public object GetValue(CellContext cellContext)
        {
            return null;
        }

        public void SetValue(CellContext cellContext, object p_Value)
        {
            throw new ApplicationException("This model doesn't support editing");
        }

        #endregion

        public string GetDisplayText(CellContext cellContext)
        {
            return null;
        }
    }

    /// <summary>
    /// A model that contains the value of cell. Usually used for a Real Cell or cells with a static text.
    /// </summary>
    public class ValueModel : IValueModel
    {
        private object m_Value;

        public ValueModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        public ValueModel(object val)
        {
            m_Value = val;
        }

        #region IValueModel Members

        public object GetValue(CellContext cellContext)
        {
            return m_Value;
        }

        public void SetValue(CellContext cellContext, object p_Value)
        {
            var valArgs = new ValueEventArgs(p_Value);
            if (cellContext.Grid != null)
                cellContext.Grid.Controller.OnValueChanging(cellContext, valArgs);
            m_Value = valArgs.Value;
            if (cellContext.Grid != null)
                cellContext.Grid.Controller.OnValueChanged(cellContext, EventArgs.Empty);
        }

        #endregion
    }

    /// <summary>
    /// CheckBox model.
    /// </summary>
    public class CheckBox : ICheckBox
    {
        private string m_Caption;

        public string Caption
        {
            get { return m_Caption; }
            set { m_Caption = value; }
        }

        #region ICheckBox Members

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cellContext"></param>
        /// <returns></returns>
        public CheckBoxStatus GetCheckBoxStatus(CellContext cellContext)
        {
            bool enableEdit = false;
            if (cellContext.Cell.Editor != null && cellContext.Cell.Editor.EnableEdit)
                enableEdit = true;

            object val = cellContext.Cell.Model.ValueModel.GetValue(cellContext);
            if (val == null)
                return new CheckBoxStatus(enableEdit, CheckBoxState.Undefined, m_Caption);
            else if (val is bool)
                return new CheckBoxStatus(enableEdit, (bool) val, m_Caption);
            else
                throw new SourceGridException("Cell value not supported for this cell. Expected bool value or null.");
        }

        /// <summary>
        /// Set the checked value
        /// </summary>
        /// <param name="cellContext"></param>
        /// <param name="pChecked"></param>
        public void SetCheckedValue(CellContext cellContext, bool? pChecked)
        {
            if (cellContext.Cell.Editor != null && cellContext.Cell.Editor.EnableEdit)
                cellContext.Cell.Editor.SetCellValue(cellContext, pChecked);
        }

        #endregion
    }

    public class SortableHeader : ISortableHeader
    {
        private SortStatus m_SortStatus = new SortStatus(HeaderSortStyle.None, null);

        public SortStatus SortStatus
        {
            get { return m_SortStatus; }
            set { m_SortStatus = value; }
        }

        #region ISortableHeader Members

        public SortStatus GetSortStatus(CellContext cellContext)
        {
            return m_SortStatus;
        }

        public void SetSortMode(CellContext cellContext, HeaderSortStyle pStyle)
        {
            m_SortStatus.Style = pStyle;
        }

        #endregion
    }

    public class ToolTip : IToolTipText
    {
        private string m_ToolTipText;

        public string ToolTipText
        {
            get { return m_ToolTipText; }
            set { m_ToolTipText = value; }
        }

        #region IToolTipText Members

        public string GetToolTipText(CellContext cellContext)
        {
            return m_ToolTipText;
        }

        #endregion
    }

    public class Image : IImage
    {
        private System.Drawing.Image mImage;

        public Image()
        {
        }

        public Image(System.Drawing.Image image)
        {
            mImage = image;
        }

        public System.Drawing.Image ImageValue
        {
            get { return mImage; }
            set { mImage = value; }
        }

        #region IImage Members

        /// <summary>
        /// Get the image of the specified cell. 
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public System.Drawing.Image GetImage(CellContext cellContext)
        {
            return mImage;
        }

        #endregion
    }

    /// <summary>
    /// Model that implements the IImage interface, used to read the Image directly from the Value of the cell.
    /// </summary>
    public class ValueImage : IImage
    {
        public static readonly ValueImage Default = new ValueImage();

        private readonly ValidatorTypeConverter imageConverter =
            new ValidatorTypeConverter(typeof (System.Drawing.Image));

        #region IImage Members

        public System.Drawing.Image GetImage(CellContext cellContext)
        {
            object val = cellContext.Cell.Model.ValueModel.GetValue(cellContext);
            return (System.Drawing.Image) imageConverter.ObjectToValue(val);
        }

        #endregion
    }
}