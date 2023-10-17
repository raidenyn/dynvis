namespace DynVis.Data.CalculationMethods.LEPS.Controls
{
    partial class AtomsInteractionParamsLEPS
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
            this.R0Editor = new DynVis.Core.PhysicalValueEditor();
            this.DeEditor = new DynVis.Core.PhysicalValueEditor();
            this.BEditor = new DynVis.Core.PhysicalValueEditor();
            this.KEditor = new DynVis.Core.PhysicalValueEditor();
            this.labelR0 = new System.Windows.Forms.Label();
            this.labelDe = new System.Windows.Forms.Label();
            this.labelB = new System.Windows.Forms.Label();
            this.labelk = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.R0Editor);
            this.groupBox.Controls.Add(this.DeEditor);
            this.groupBox.Controls.Add(this.BEditor);
            this.groupBox.Controls.Add(this.KEditor);
            this.groupBox.Controls.Add(this.labelR0);
            this.groupBox.Controls.Add(this.labelDe);
            this.groupBox.Controls.Add(this.labelB);
            this.groupBox.Controls.Add(this.labelk);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(212, 132);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "A-B";
            // 
            // R0Editor
            // 
            this.R0Editor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.R0Editor.Location = new System.Drawing.Point(25, 97);
            this.R0Editor.Name = "R0Editor";
            this.R0Editor.Size = new System.Drawing.Size(181, 20);
            this.R0Editor.TabIndex = 2;
            this.R0Editor.Value = 0;
            // 
            // DeEditor
            // 
            this.DeEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.DeEditor.DimensionType = DynVis.Core.DimensionType.Energy;
            this.DeEditor.Location = new System.Drawing.Point(25, 71);
            this.DeEditor.Name = "DeEditor";
            this.DeEditor.Size = new System.Drawing.Size(181, 20);
            this.DeEditor.TabIndex = 2;
            this.DeEditor.Value = 0;
            // 
            // BEditor
            // 
            this.BEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.BEditor.DimensionType = DynVis.Core.DimensionType.ReverseLenth;
            this.BEditor.Location = new System.Drawing.Point(25, 45);
            this.BEditor.Name = "BEditor";
            this.BEditor.Size = new System.Drawing.Size(181, 20);
            this.BEditor.TabIndex = 2;
            this.BEditor.Value = 0;
            // 
            // KEditor
            // 
            this.KEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.KEditor.Location = new System.Drawing.Point(25, 19);
            this.KEditor.Name = "KEditor";
            this.KEditor.Size = new System.Drawing.Size(181, 20);
            this.KEditor.TabIndex = 2;
            this.KEditor.Value = 0;
            // 
            // labelR0
            // 
            this.labelR0.AutoSize = true;
            this.labelR0.Location = new System.Drawing.Point(6, 100);
            this.labelR0.Name = "labelR0";
            this.labelR0.Size = new System.Drawing.Size(16, 13);
            this.labelR0.TabIndex = 1;
            this.labelR0.Text = "r0";
            // 
            // labelDe
            // 
            this.labelDe.AutoSize = true;
            this.labelDe.Location = new System.Drawing.Point(6, 74);
            this.labelDe.Name = "labelDe";
            this.labelDe.Size = new System.Drawing.Size(21, 13);
            this.labelDe.TabIndex = 1;
            this.labelDe.Text = "De";
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(6, 48);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(14, 13);
            this.labelB.TabIndex = 1;
            this.labelB.Text = "B";
            // 
            // labelk
            // 
            this.labelk.AutoSize = true;
            this.labelk.Location = new System.Drawing.Point(6, 22);
            this.labelk.Name = "labelk";
            this.labelk.Size = new System.Drawing.Size(13, 13);
            this.labelk.TabIndex = 1;
            this.labelk.Text = "k";
            // 
            // AtomsInteractionParamsLEPS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "AtomsInteractionParamsLEPS";
            this.Size = new System.Drawing.Size(212, 132);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label labelk;
        private System.Windows.Forms.Label labelR0;
        private System.Windows.Forms.Label labelDe;
        private System.Windows.Forms.Label labelB;
        private Core.PhysicalValueEditor R0Editor;
        private Core.PhysicalValueEditor DeEditor;
        private Core.PhysicalValueEditor BEditor;
        private Core.PhysicalValueEditor KEditor;
    }
}
