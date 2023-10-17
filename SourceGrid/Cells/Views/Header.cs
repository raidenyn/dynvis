#region

using System;
using DevAge.Drawing;
using DevAge.Drawing.VisualElements;

#endregion

namespace SourceGrid.Cells.Views
{
    /// <summary>
    /// Summary description for a 3D Header.
    /// </summary>
    [Serializable]
    public class Header : Cell
    {
        /// <summary>
        /// Represents a default Header, with a 3D border and a LightGray BackColor
        /// </summary>
        public new static readonly Header Default;

        public new static RectangleBorder DefaultBorder = RectangleBorder.NoBorder;

        #region Constructors

        static Header()
        {
            Default = new Header();
        }

        /// <summary>
        /// Use default setting
        /// </summary>
        public Header()
        {
            Background = new HeaderThemed();
            Border = DefaultBorder;
        }

        /// <summary>
        /// Copy constructor.  This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
        /// </summary>
        /// <param name="p_Source"></param>
        public Header(Header p_Source) : base(p_Source)
        {
            Background = (IHeader) p_Source.Background.Clone();
        }

        #endregion

        #region Clone

        /// <summary>
        /// Clone this object. This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new Header(this);
        }

        #endregion

        #region Visual Elements

        public new IHeader Background
        {
            get { return (IHeader) base.Background; }
            set { base.Background = value; }
        }

        protected override void PrepareView(CellContext context)
        {
            base.PrepareView(context);

            if (context.CellRange.Contains(context.Grid.MouseDownPosition))
                Background.Style = ControlDrawStyle.Pressed;
            else if (context.CellRange.Contains(context.Grid.MouseCellPosition))
                Background.Style = ControlDrawStyle.Hot;
            else
                Background.Style = ControlDrawStyle.Normal;
        }

        #endregion
    }
}