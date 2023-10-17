#region

using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

#endregion

namespace SourceGrid
{
    /// <summary>
    /// A dictionary with keys of type Control and values of type LinkedControlValue
    /// </summary>
    public class LinkedControlsList : IEnumerable<LinkedControlValue>
    {
        private readonly List<LinkedControlValue> mList = new List<LinkedControlValue>();
        private readonly Control mParent;

        public LinkedControlsList(Control parent)
        {
            mParent = parent;
        }

        #region IEnumerable<LinkedControlValue> Members

        public IEnumerator<LinkedControlValue> GetEnumerator()
        {
            return mList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return mList.GetEnumerator();
        }

        #endregion

        public void Add(LinkedControlValue linkedControl)
        {
            mList.Add(linkedControl);

            mParent.Controls.Add(linkedControl.Control);
        }

        public void Remove(LinkedControlValue linkedControl)
        {
            mList.Remove(linkedControl);

            mParent.Controls.Remove(linkedControl.Control);
        }

        public LinkedControlValue GetByControl(Control control)
        {
            for (int i = 0; i < mList.Count; i++)
            {
                if (control == mList[i].Control)
                {
                    return mList[i];
                }
            }

            return null;
        }
    }

    /// <summary>
    /// Determine the scrolling mode of the linked controls.
    /// </summary>
    public enum LinkedControlScrollMode
    {
        None = 0,
        ScrollVertical = 1,
        ScrollHorizontal = 2,
        ScrollBoth = 3,
        BasedOnPosition = 4
    }

    /// <summary>
    /// Linked control value
    /// </summary>
    public class LinkedControlValue
    {
        private readonly Control mControl;

        /// <summary>
        /// Constructor
        /// </summary>
        public LinkedControlValue(Control control, Position position)
        {
            mControl = control;
            Position = position;
            UseCellBorder = true;
            ScrollMode = LinkedControlScrollMode.BasedOnPosition;
        }

        public Control Control
        {
            get { return mControl; }
        }

        /// <summary>
        /// Gets or sets the position of the linked control.
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Gets or sets if show the cell border. True to insert the editor control inside the border of the cell, false to put the editor control over the entire cell. If you use true remember to set EnableCellDrawOnEdit == true.
        /// </summary>
        public bool UseCellBorder { get; set; }

        /// <summary>
        /// Gets or sets the scrolling mode of the control.
        /// </summary>
        public LinkedControlScrollMode ScrollMode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Position.ToString();
        }
    }
}