#region

using System;
using System.Windows.Forms;

#endregion

namespace SourceGrid.Cells.Controllers
{
    /// <summary>
    /// Allow to customize the cursor of a cell. The cell must implement ICellCursor. This behavior can be shared between multiple cells.
    /// </summary>
    public class MouseCursor : ControllerBase
    {
        public static readonly MouseCursor Default = new MouseCursor(Cursors.Default, true);
        public static readonly MouseCursor Hand = new MouseCursor(Cursors.Hand, true);
        private readonly bool mApplyOnMouseEnter;

        #region Constructor

        public MouseCursor(Cursor cursor, bool applyOnMouseEnter)
        {
            mApplyOnMouseEnter = applyOnMouseEnter;
            Cursor = cursor;
        }

        #endregion

        public Cursor Cursor { get; set; }

        public override void OnMouseEnter(CellContext sender, EventArgs e)
        {
            base.OnMouseEnter(sender, e);

            if (mApplyOnMouseEnter)
                ApplyCursor(sender, e);
        }

        public override void OnMouseLeave(CellContext sender, EventArgs e)
        {
            base.OnMouseLeave(sender, e);

            if (mApplyOnMouseEnter)
                ResetCursor(sender, e);
        }

        /// <summary>
        /// Change the cursor with the cursor of the cell
        /// </summary>
        public virtual void ApplyCursor(CellContext sender, EventArgs e)
        {
            if (Cursor != null)
                sender.Grid.Cursor = Cursor;
        }

        /// <summary>
        /// Reset the original cursor
        /// </summary>
        public virtual void ResetCursor(CellContext sender, EventArgs e)
        {
            if (Cursor != null)
            {
                if (sender.Grid.Cursor == Cursor)
                    sender.Grid.Cursor = null;
            }
        }
    }
}