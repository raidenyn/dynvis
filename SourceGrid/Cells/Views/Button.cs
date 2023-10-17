#region

using System;
using DevAge.Drawing;
using DevAge.Drawing.VisualElements;

#endregion

namespace SourceGrid.Cells.Views
{
    /// <summary>
    /// Summary description for a 3D themed Button.
    /// </summary>
    [Serializable]
    public class Button : Cell
    {
        /// <summary>
        /// Represents a Button with the ability to draw an Image. Disable also the selection border using the OwnerDrawSelectionBorder = true.
        /// </summary>
        public new static readonly Button Default;

        #region Constructors

        static Button()
        {
            Default = new Button();
        }

        /// <summary>
        /// Use default setting
        /// </summary>
        public Button()
        {
            Background = new ButtonThemed();
        }

        /// <summary>
        /// Copy constructor.  This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
        /// </summary>
        /// <param name="p_Source"></param>
        public Button(Button p_Source) : base(p_Source)
        {
            Background = (IButton) p_Source.Background.Clone();
        }

        #endregion

        #region Clone

        /// <summary>
        /// Clone this object. This method duplicate all the reference field (Image, Font, StringFormat) creating a new instance.
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new Button(this);
        }

        #endregion

        #region Visual Elements

        public new IButton Background
        {
            get { return (IButton) base.Background; }
            set { base.Background = value; }
        }

        protected override void PrepareView(CellContext context)
        {
            base.PrepareView(context);

            if (context.CellRange.Contains(context.Grid.MouseDownPosition))
                Background.Style = ButtonStyle.Pressed;
            else if (context.CellRange.Contains(context.Grid.MouseCellPosition))
                Background.Style = ButtonStyle.Hot;
            else if (context.CellRange.Contains(context.Grid.Selection.ActivePosition))
                Background.Style = ButtonStyle.Focus;
            else
                Background.Style = ButtonStyle.Normal;
        }

        #endregion
    }
}