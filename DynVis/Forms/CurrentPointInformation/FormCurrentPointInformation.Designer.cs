namespace DynVis.CurrentPointInformation
{
    partial class FormCurrentPointInformation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCurrentPointInformation));
            this.currentPointInformation = new DynVis.Core.CurrentPointInformation.CurrentPointInformation();
            this.SuspendLayout();
            // 
            // currentPointInformation
            // 
            resources.ApplyResources(this.currentPointInformation, "currentPointInformation");
            this.currentPointInformation.Name = "currentPointInformation";
            this.currentPointInformation.SizeChanged += new System.EventHandler(this.currentPointInformation_SizeChanged);
            // 
            // FormCurrentPointInformation
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.currentPointInformation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormCurrentPointInformation";
            this.Activated += new System.EventHandler(this.FormCurrentPointInformation_Activated);
            this.VisibleChanged += new System.EventHandler(this.FormCurrentPointInformation_VisibleChanged);
            this.Controls.SetChildIndex(this.currentPointInformation, 0);
            this.ResumeLayout(false);

        }

        #endregion

        private DynVis.Core.CurrentPointInformation.CurrentPointInformation currentPointInformation;
    }
}