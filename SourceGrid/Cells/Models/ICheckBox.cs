#region

using DevAge.Drawing;

#endregion

namespace SourceGrid.Cells.Models
{
    /// <summary>
    /// Interface for informations about a cechkbox
    /// </summary>
    public interface ICheckBox : IModel
    {
        /// <summary>
        /// Get the status of the checkbox at the current position
        /// </summary>
        /// <param name="cellContext"></param>
        /// <returns></returns>
        CheckBoxStatus GetCheckBoxStatus(CellContext cellContext);

        /// <summary>
        /// Set the checked value
        /// </summary>
        /// <param name="cellContext"></param>
        /// <param name="pChecked">True, False or Null</param>
        void SetCheckedValue(CellContext cellContext, bool? pChecked);
    }

    /// <summary>
    /// Status of the CheckBox
    /// </summary>
    public struct CheckBoxStatus
    {
        /// <summary>
        /// Caption of the CheckBox
        /// </summary>
        public string Caption;

        /// <summary>
        /// Enable or disable the checkbox
        /// </summary>
        public bool CheckEnable;

        private CheckBoxState mCheckState;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="checkEnable"></param>
        /// <param name="bChecked"></param>
        /// <param name="caption"></param>
        public CheckBoxStatus(bool checkEnable, bool? bChecked, string caption)
        {
            CheckEnable = checkEnable;
            Caption = caption;

            mCheckState = CheckBoxState.Undefined;
            Checked = bChecked;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="checkEnable"></param>
        /// <param name="checkState"></param>
        /// <param name="caption"></param>
        public CheckBoxStatus(bool checkEnable, CheckBoxState checkState, string caption)
        {
            CheckEnable = checkEnable;
            mCheckState = checkState;
            Caption = caption;
        }

        /// <summary>
        /// Gets or sets the state of the check box.
        /// </summary>
        public CheckBoxState CheckState
        {
            get { return mCheckState; }
            set { mCheckState = value; }
        }

        /// <summary>
        /// Gets or set Checked status. Return true for Checked, false for Uncheck and null for Undefined
        /// </summary>
        public bool? Checked
        {
            get
            {
                if (CheckState == CheckBoxState.Checked)
                    return true;
                else if (CheckState == CheckBoxState.Unchecked)
                    return false;
                else
                    return null;
            }
            set
            {
                if (value == null)
                    CheckState = CheckBoxState.Undefined;
                else if (value.Value)
                    CheckState = CheckBoxState.Checked;
                else
                    CheckState = CheckBoxState.Unchecked;
            }
        }
    }
}