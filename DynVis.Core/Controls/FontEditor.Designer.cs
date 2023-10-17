namespace DynVis.Core.Controls
{
    partial class FontEditor
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.linkLabelFontView = new System.Windows.Forms.LinkLabel();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.brushEditor = new DynVis.Core.Controls.BrushEditor();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.brushEditor);
            this.groupBox.Controls.Add(this.linkLabelFontView);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(351, 90);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Заголовок";
            // 
            // linkLabelFontView
            // 
            this.linkLabelFontView.AutoSize = true;
            this.linkLabelFontView.Location = new System.Drawing.Point(6, 20);
            this.linkLabelFontView.Name = "linkLabelFontView";
            this.linkLabelFontView.Size = new System.Drawing.Size(28, 13);
            this.linkLabelFontView.TabIndex = 0;
            this.linkLabelFontView.TabStop = true;
            this.linkLabelFontView.Text = "Font";
            this.linkLabelFontView.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFontView_LinkClicked);
            // 
            // brushEditor
            // 
            this.brushEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.brushEditor.Location = new System.Drawing.Point(6, 41);
            this.brushEditor.Name = "brushEditor";
            this.brushEditor.Size = new System.Drawing.Size(339, 42);
            this.brushEditor.TabIndex = 1;
            this.brushEditor.EditableBrushChanged += new System.EventHandler(this.brushEditor_EditableBrushChanged);
            // 
            // FontEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "FontEditor";
            this.Size = new System.Drawing.Size(351, 90);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.FontEditor_Validating);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.LinkLabel linkLabelFontView;
        private System.Windows.Forms.FontDialog fontDialog;
        private BrushEditor brushEditor;
    }
}
