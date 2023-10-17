namespace DynVis.Core.Elements
{
    partial class ElementSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.linkLabelElement = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // linkLabelElement
            // 
            this.linkLabelElement.AutoSize = true;
            this.linkLabelElement.Location = new System.Drawing.Point(0, 0);
            this.linkLabelElement.Name = "linkLabelElement";
            this.linkLabelElement.Size = new System.Drawing.Size(14, 13);
            this.linkLabelElement.TabIndex = 0;
            this.linkLabelElement.TabStop = true;
            this.linkLabelElement.Text = "C";
            this.linkLabelElement.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelElement_LinkClicked);
            this.linkLabelElement.SizeChanged += new System.EventHandler(this.linkLabelElement_SizeChanged);
            // 
            // ElementSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabelElement);
            this.Name = "ElementSelector";
            this.Size = new System.Drawing.Size(21, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabelElement;
    }
}
