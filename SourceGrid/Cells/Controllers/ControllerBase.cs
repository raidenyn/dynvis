#region

using System;
using System.ComponentModel;
using System.Windows.Forms;
using DevAge.ComponentModel;

#endregion

namespace SourceGrid.Cells.Controllers
{
    /// <summary>
    /// Implements all the method of the controller interface without any implementation
    /// </summary>
    public abstract class ControllerBase : IController
    {
        #region IController Members

        public virtual void OnMouseDown(CellContext sender, MouseEventArgs e)
        {
        }

        public virtual void OnMouseUp(CellContext sender, MouseEventArgs e)
        {
        }

        public virtual void OnMouseMove(CellContext sender, MouseEventArgs e)
        {
        }

        public virtual void OnMouseEnter(CellContext sender, EventArgs e)
        {
        }

        public virtual void OnMouseLeave(CellContext sender, EventArgs e)
        {
        }

        public virtual void OnKeyUp(CellContext sender, KeyEventArgs e)
        {
        }

        public virtual void OnKeyDown(CellContext sender, KeyEventArgs e)
        {
        }

        public virtual void OnKeyPress(CellContext sender, KeyPressEventArgs e)
        {
        }

        public virtual void OnDoubleClick(CellContext sender, EventArgs e)
        {
        }

        public virtual void OnClick(CellContext sender, EventArgs e)
        {
        }

        public virtual void OnFocusLeaving(CellContext sender, CancelEventArgs e)
        {
        }

        public virtual void OnFocusLeft(CellContext sender, EventArgs e)
        {
        }

        public virtual void OnFocusEntering(CellContext sender, CancelEventArgs e)
        {
        }

        public virtual void OnFocusEntered(CellContext sender, EventArgs e)
        {
        }

        /// <summary>
        /// Fired before the value of the cell is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnValueChanging(CellContext sender, ValueEventArgs e)
        {
        }

        /// <summary>
        /// Fired after the value of the cell is changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void OnValueChanged(CellContext sender, EventArgs e)
        {
        }

        public virtual void OnEditStarting(CellContext sender, CancelEventArgs e)
        {
        }

        public virtual void OnEditStarted(CellContext sender, EventArgs e)
        {
        }

        public virtual void OnEditEnded(CellContext sender, EventArgs e)
        {
        }

        public virtual bool CanReceiveFocus(CellContext sender, EventArgs e)
        {
            return true;
        }

        public virtual void OnDragDrop(CellContext sender, DragEventArgs e)
        {
        }

        public virtual void OnDragEnter(CellContext sender, DragEventArgs e)
        {
        }

        public virtual void OnDragLeave(CellContext sender, EventArgs e)
        {
        }

        public virtual void OnDragOver(CellContext sender, DragEventArgs e)
        {
        }

        public virtual void OnGiveFeedback(CellContext sender, GiveFeedbackEventArgs e)
        {
        }

        #endregion
    }
}