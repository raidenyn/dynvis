namespace DynVis.Forms
{
    partial class FormBase
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormBase));
            this.buttonAllwaysOntop = new System.Windows.Forms.Button();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // buttonAllwaysOntop
            // 
            resources.ApplyResources(this.buttonAllwaysOntop, "buttonAllwaysOntop");
            this.buttonAllwaysOntop.AutoEllipsis = true;
            this.buttonAllwaysOntop.FlatAppearance.BorderSize = 0;
            this.buttonAllwaysOntop.ImageList = this.imageList;
            this.buttonAllwaysOntop.Name = "buttonAllwaysOntop";
            this.buttonAllwaysOntop.UseCompatibleTextRendering = true;
            this.buttonAllwaysOntop.UseVisualStyleBackColor = true;
            this.buttonAllwaysOntop.Click += new System.EventHandler(this.buttonAllwaysOntop_Click);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "pushpin1.gif");
            this.imageList.Images.SetKeyName(1, "pushpin.gif");
            // 
            // FormBase
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonAllwaysOntop);
            this.Name = "FormBase";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.FormBase_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBase_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonAllwaysOntop;
        private System.Windows.Forms.ImageList imageList;
    }
}