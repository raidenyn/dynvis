namespace DynVis.Core
{
    partial class DimensionSelector
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
            this.components = new System.ComponentModel.Container();
            this.linkLabelDimension = new System.Windows.Forms.LinkLabel();
            this.ViewDimensionList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolTipFullDimensionName = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // linkLabelDimension
            // 
            this.linkLabelDimension.AutoSize = true;
            this.linkLabelDimension.Location = new System.Drawing.Point(4, 4);
            this.linkLabelDimension.Name = "linkLabelDimension";
            this.linkLabelDimension.Size = new System.Drawing.Size(14, 13);
            this.linkLabelDimension.TabIndex = 0;
            this.linkLabelDimension.TabStop = true;
            this.linkLabelDimension.Text = "A";
            this.linkLabelDimension.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDimension_LinkClicked);
            this.linkLabelDimension.Resize += new System.EventHandler(this.linkLabelDimension_Resize);
            this.linkLabelDimension.MouseHover += new System.EventHandler(this.DimensionSelector_MouseHover);
            // 
            // ViewDimensionList
            // 
            this.ViewDimensionList.Name = "ViewDimensionList";
            this.ViewDimensionList.ShowImageMargin = false;
            this.ViewDimensionList.Size = new System.Drawing.Size(36, 4);
            // 
            // DimensionSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabelDimension);
            this.Name = "DimensionSelector";
            this.Size = new System.Drawing.Size(22, 20);
            this.MouseHover += new System.EventHandler(this.DimensionSelector_MouseHover);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabelDimension;
        private System.Windows.Forms.ContextMenuStrip ViewDimensionList;
        private System.Windows.Forms.ToolTip toolTipFullDimensionName;
    }
}
