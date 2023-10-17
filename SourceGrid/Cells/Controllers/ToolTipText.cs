#region

using System;
using System.Drawing;
using System.Windows.Forms;
using SourceGrid.Cells.Models;

#endregion

namespace SourceGrid.Cells.Controllers
{
    /// <summary>
    /// Allow to customize the tooltiptext of a cell. This class read the tooltiptext from the ICellToolTipText.GetToolTipText.  This behavior can be shared between multiple cells.
    /// </summary>
    public class ToolTipText : ControllerBase
    {
        /// <summary>
        /// Default tooltiptext
        /// </summary>
        public static readonly ToolTipText Default = new ToolTipText();

        private Color mBackColor = Color.Empty;
        private Color mForeColor = Color.Empty;
        private ToolTipIcon mToolTipIcon = ToolTipIcon.None;

        private string mToolTipTitle = string.Empty;

        public string ToolTipTitle
        {
            get { return mToolTipTitle; }
            set { mToolTipTitle = value; }
        }

        public ToolTipIcon ToolTipIcon
        {
            get { return mToolTipIcon; }
            set { mToolTipIcon = value; }
        }

        public bool IsBalloon { get; set; }

        public Color BackColor
        {
            get { return mBackColor; }
            set { mBackColor = value; }
        }

        public Color ForeColor
        {
            get { return mForeColor; }
            set { mForeColor = value; }
        }

        public override void OnMouseEnter(CellContext sender, EventArgs e)
        {
            base.OnMouseEnter(sender, e);

            ApplyToolTipText(sender, e);
        }

        public override void OnMouseLeave(CellContext sender, EventArgs e)
        {
            base.OnMouseLeave(sender, e);

            ResetToolTipText(sender, e);
        }


        /// <summary>
        /// Change the cursor with the cursor of the cell
        /// </summary>
        /// <param name="e"></param>
        protected virtual void ApplyToolTipText(CellContext sender, EventArgs e)
        {
            IToolTipText toolTip;
            if ((toolTip = (IToolTipText) sender.Cell.Model.FindModel(typeof (IToolTipText))) != null)
            {
                string text = toolTip.GetToolTipText(sender);
                if (text != null && text.Length > 0)
                {
                    sender.Grid.ToolTipText = text;
                    sender.Grid.ToolTip.ToolTipTitle = ToolTipTitle;
                    sender.Grid.ToolTip.ToolTipIcon = ToolTipIcon;
                    sender.Grid.ToolTip.IsBalloon = IsBalloon;
                    if (BackColor.IsEmpty == false)
                        sender.Grid.ToolTip.BackColor = BackColor;
                    if (ForeColor.IsEmpty == false)
                        sender.Grid.ToolTip.ForeColor = ForeColor;
                }
            }
        }

        /// <summary>
        /// Reset the original cursor
        /// </summary>
        /// <param name="e"></param>
        protected virtual void ResetToolTipText(CellContext sender, EventArgs e)
        {
            IToolTipText toolTip;
            if ((toolTip = (IToolTipText) sender.Cell.Model.FindModel(typeof (IToolTipText))) != null)
            {
                sender.Grid.ToolTipText = null;
            }
        }
    }
}