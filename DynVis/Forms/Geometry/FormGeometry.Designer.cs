namespace DynVis.Geometry
{
    partial class FormGeometry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGeometry));
            this.geometryDraw = new DynVis.Geometry.Draw.GeometryDraw();
            this.trackBarQ1 = new System.Windows.Forms.TrackBar();
            this.trackBarQ2 = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarQ1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarQ2)).BeginInit();
            this.SuspendLayout();
            // 
            // geometryDraw
            // 
            resources.ApplyResources(this.geometryDraw, "geometryDraw");
            this.geometryDraw.EnableMenu = true;
            this.geometryDraw.Name = "geometryDraw";
            this.geometryDraw.GeometryChanged += new System.EventHandler(this.geometryDraw_GeometryChanged);
            // 
            // trackBarQ1
            // 
            resources.ApplyResources(this.trackBarQ1, "trackBarQ1");
            this.trackBarQ1.Maximum = 1000;
            this.trackBarQ1.Name = "trackBarQ1";
            this.trackBarQ1.Scroll += new System.EventHandler(this.trackBarQ_Scroll);
            // 
            // trackBarQ2
            // 
            resources.ApplyResources(this.trackBarQ2, "trackBarQ2");
            this.trackBarQ2.Maximum = 1000;
            this.trackBarQ2.Name = "trackBarQ2";
            this.trackBarQ2.Value = 1000;
            this.trackBarQ2.Scroll += new System.EventHandler(this.trackBarQ_Scroll);
            // 
            // FormGeometry
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.geometryDraw);
            this.Controls.Add(this.trackBarQ2);
            this.Controls.Add(this.trackBarQ1);
            this.Name = "FormGeometry";
            this.Load += new System.EventHandler(this.FormGeometry_Load);
            this.VisibleChanged += new System.EventHandler(this.FormGeometry_VisibleChanged);
            this.Controls.SetChildIndex(this.trackBarQ1, 0);
            this.Controls.SetChildIndex(this.trackBarQ2, 0);
            this.Controls.SetChildIndex(this.geometryDraw, 0);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarQ1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarQ2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Draw.GeometryDraw geometryDraw;
        private System.Windows.Forms.TrackBar trackBarQ1;
        private System.Windows.Forms.TrackBar trackBarQ2;
    }
}