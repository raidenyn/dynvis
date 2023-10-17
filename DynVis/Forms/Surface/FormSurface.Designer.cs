namespace DynVis.Surface
{
    partial class FormSurface
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSurface));
            this.drawSurface = new DynVis.Draw.Surface.DrawSurface();
            this.SuspendLayout();
            // 
            // drawSurface
            // 
            resources.ApplyResources(this.drawSurface, "drawSurface");
            this.drawSurface.Name = "drawSurface";
            // 
            // FormSurface
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.drawSurface);
            this.Name = "FormSurface";
            this.VisibleChanged += new System.EventHandler(this.FormDrawSurface_VisibleChanged);
            this.Controls.SetChildIndex(this.drawSurface, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private Draw.Surface.DrawSurface drawSurface;

    }
}