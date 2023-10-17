namespace DynVis.Points
{
    partial class FormPoints
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPoints));
            this.surfacePointsView = new DynVis.Core.Points.SurfacePointsExplorer();
            this.SuspendLayout();
            // 
            // surfacePointsView
            // 
            resources.ApplyResources(this.surfacePointsView, "surfacePointsView");
            this.surfacePointsView.Name = "surfacePointsView";
            // 
            // FormPoints
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.surfacePointsView);
            this.Name = "FormPoints";
            this.VisibleChanged += new System.EventHandler(this.FormPoints_VisibleChanged);
            this.Controls.SetChildIndex(this.surfacePointsView, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private DynVis.Core.Points.SurfacePointsExplorer surfacePointsView;
    }
}