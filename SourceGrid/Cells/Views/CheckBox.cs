#region

using System;
using System.Collections.Generic;
using DevAge.Drawing;
using DevAge.Drawing.VisualElements;
using SourceGrid.Cells.Models;
using ICheckBox=DevAge.Drawing.VisualElements.ICheckBox;

#endregion

namespace SourceGrid.Cells.Views
{
    /// <summary>
    /// Summary description for VisualModelCheckBox.
    /// </summary>
    [Serializable]
    public class CheckBox : Cell
    {
        /// <summary>
        /// Represents a default CheckBox with the CheckBox image align to the Middle Center of the cell. You must use this VisualModel with a Cell of type ICellCheckBox.
        /// </summary>
        public new static readonly CheckBox Default = new CheckBox();

        /// <summary>
        /// Represents a CheckBox with the CheckBox image align to the Middle Left of the cell
        /// </summary>
        public static readonly CheckBox MiddleLeftAlign;

        private ContentAlignment m_CheckBoxAlignment = ContentAlignment.MiddleCenter;
        private ICheckBox mElementCheckBox = new CheckBoxThemed();

        #region Constructors

        static CheckBox()
        {
            MiddleLeftAlign = new CheckBox();
            MiddleLeftAlign.CheckBoxAlignment = ContentAlignment.MiddleLeft;
            MiddleLeftAlign.TextAlignment = ContentAlignment.MiddleLeft;
        }

        /// <summary>
        /// Use default setting and construct a read and write VisualProperties
        /// </summary>
        public CheckBox()
        {
        }

        /// <summary>
        /// Copy constructor. This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
        /// </summary>
        /// <param name="p_Source"></param>
        /// <param name="p_bReadOnly"></param>
        public CheckBox(CheckBox p_Source) : base(p_Source)
        {
            m_CheckBoxAlignment = p_Source.m_CheckBoxAlignment;
            ElementCheckBox = (ICheckBox) ElementCheckBox.Clone();
        }

        #endregion

        /// <summary>
        /// Image Alignment
        /// </summary>
        public ContentAlignment CheckBoxAlignment
        {
            get { return m_CheckBoxAlignment; }
            set { m_CheckBoxAlignment = value; }
        }

        /// <summary>
        /// Gets or sets the visual element used to draw the checkbox. Default is DevAge.Drawing.VisualElements.CheckBoxThemed.
        /// </summary>
        public ICheckBox ElementCheckBox
        {
            get { return mElementCheckBox; }
            set { mElementCheckBox = value; }
        }

        protected override void PrepareView(CellContext context)
        {
            base.PrepareView(context);

            PrepareVisualElementCheckBox(context);
        }

        protected override IEnumerable<IVisualElement> GetElements()
        {
            if (ElementCheckBox != null)
                yield return ElementCheckBox;

            foreach (IVisualElement v in GetBaseElements())
                yield return v;
        }

        private IEnumerable<IVisualElement> GetBaseElements()
        {
            return base.GetElements();
        }


        protected virtual void PrepareVisualElementCheckBox(CellContext context)
        {
            ElementCheckBox.AnchorArea = new AnchorArea(CheckBoxAlignment, false);

            var checkBoxModel = (Models.ICheckBox) context.Cell.Model.FindModel(typeof (Models.ICheckBox));
            CheckBoxStatus checkBoxStatus = checkBoxModel.GetCheckBoxStatus(context);

            if (context.CellRange.Contains(context.Grid.MouseCellPosition))
            {
                if (checkBoxStatus.CheckEnable)
                    ElementCheckBox.Style = ControlDrawStyle.Hot;
                else
                    ElementCheckBox.Style = ControlDrawStyle.Disabled;
            }
            else
            {
                if (checkBoxStatus.CheckEnable)
                    ElementCheckBox.Style = ControlDrawStyle.Normal;
                else
                    ElementCheckBox.Style = ControlDrawStyle.Disabled;
            }

            ElementCheckBox.CheckBoxState = checkBoxStatus.CheckState;


            ElementText.Value = checkBoxStatus.Caption;
        }

        #region Clone

        /// <summary>
        /// Clone this object. This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new CheckBox(this);
        }

        #endregion
    }
}