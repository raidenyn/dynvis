namespace DynVis.Path
{
    partial class FormPath
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPath));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.pathDraw = new DynVis.Draw.Path.PathDraw();
            this.pathListView = new DynVis.Core.ReactionPath.PathListView();
            this.buttonShowPathList = new System.Windows.Forms.Button();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.pathDraw);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pathListView);
            // 
            // pathDraw
            // 
            resources.ApplyResources(this.pathDraw, "pathDraw");
            this.pathDraw.Name = "pathDraw";
            this.pathDraw.Path = null;
            // 
            // pathListView
            // 
            resources.ApplyResources(this.pathListView, "pathListView");
            this.pathListView.Name = "pathListView";
            // 
            // buttonShowPathList
            // 
            resources.ApplyResources(this.buttonShowPathList, "buttonShowPathList");
            this.buttonShowPathList.FlatAppearance.BorderSize = 0;
            this.buttonShowPathList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.buttonShowPathList.Name = "buttonShowPathList";
            this.buttonShowPathList.UseVisualStyleBackColor = true;
            this.buttonShowPathList.Click += new System.EventHandler(this.buttonShowPathList_Click);
            // 
            // FormPath
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonShowPathList);
            this.Controls.Add(this.splitContainer);
            this.Name = "FormPath";
            this.VisibleChanged += new System.EventHandler(this.FormPath_VisibleChanged);
            this.Controls.SetChildIndex(this.splitContainer, 0);
            this.Controls.SetChildIndex(this.buttonShowPathList, 0);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Draw.Path.PathDraw pathDraw;
        private DynVis.Core.ReactionPath.PathListView pathListView;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button buttonShowPathList;
    }
}