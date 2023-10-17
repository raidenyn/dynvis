#region

using System.Collections.Generic;
using DevAge.Drawing;
using DevAge.Drawing.VisualElements;

#endregion

namespace SourceGrid.Cells.Views
{
    public class ComboBox : Cell
    {
        /// <summary>
        /// Represents a default CheckBox with the CheckBox image align to the Middle Center of the cell. You must use this VisualModel with a Cell of type ICellCheckBox.
        /// </summary>
        public new static readonly ComboBox Default = new ComboBox();

        private IDropDownButton mElementDropDown = new DropDownButtonThemed();

        #region Constructors

        /// <summary>
        /// Use default setting and construct a read and write VisualProperties
        /// </summary>
        public ComboBox()
        {
            ElementDropDown.AnchorArea = new AnchorArea(float.NaN, 0, 0, 0, false, false);
        }

        /// <summary>
        /// Copy constructor. This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
        /// </summary>
        /// <param name="p_Source"></param>
        /// <param name="p_bReadOnly"></param>
        public ComboBox(ComboBox p_Source)
            : base(p_Source)
        {
            ElementDropDown = (IDropDownButton) ElementDropDown.Clone();
        }

        #endregion

        /// <summary>
        /// Gets or sets the visual element used to draw the checkbox. Default is DevAge.Drawing.VisualElements.CheckBoxThemed.
        /// </summary>
        public IDropDownButton ElementDropDown
        {
            get { return mElementDropDown; }
            set { mElementDropDown = value; }
        }

        protected override void PrepareView(CellContext context)
        {
            base.PrepareView(context);

            PrepareVisualElementDropDown(context);
        }

        protected override IEnumerable<IVisualElement> GetElements()
        {
            if (ElementDropDown != null)
                yield return ElementDropDown;

            foreach (IVisualElement v in GetBaseElements())
                yield return v;
        }

        private IEnumerable<IVisualElement> GetBaseElements()
        {
            return base.GetElements();
        }


        protected virtual void PrepareVisualElementDropDown(CellContext context)
        {
            if (context.CellRange.Contains(context.Grid.MouseCellPosition))
            {
                ElementDropDown.Style = ButtonStyle.Hot;
            }
            else
            {
                ElementDropDown.Style = ButtonStyle.Normal;
            }
        }

        #region Clone

        /// <summary>
        /// Clone this object. This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new ComboBox(this);
        }

        #endregion
    }
}