using System;
using DynVis.Forms;

namespace DynVis.Path
{
    public partial class FormPath : FormBase
    {
        public FormPath()
        {
            InitializeComponent();

            SetVisiblePathList(false);
        }

        protected override void SetEvents()
        {
            ReactionData.ReactionPathChanged += ReactionData_ReactionPathChanged;

            pathListView.PathList = ReactionData.ReactionPathList;
        }

        protected override void UnsetEvents()
        {
            ReactionData.ReactionPathChanged -= ReactionData_ReactionPathChanged;
        }

        void ReactionData_ReactionPathChanged(object sender, EventArgs e)
        {
            SetPath();
        }

        void SetPath()
        {
            if (Visible && ReactionData != null && ReactionData.CurrentReactionPath != null)
            {
                pathDraw.Path = ReactionData.CurrentReactionPath;
            }
            else
            {
                pathDraw.Path = null;
            }
        }

        private void FormPath_VisibleChanged(object sender, EventArgs e)
        {
            SetPath();
        }

        private void buttonShowPathList_Click(object sender, EventArgs e)
        {
            SetVisiblePathList(splitContainer.Panel2Collapsed);
        }

        private void SetVisiblePathList(bool visible)
        {
            splitContainer.Panel2Collapsed = !visible;
            buttonShowPathList.Text = visible ? ">" : "<";
        }
    }
}
