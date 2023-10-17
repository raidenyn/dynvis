namespace DynVis.Core
{
    partial class PhysicalValueEditor
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
            this.dimensionSelector = new DynVis.Core.DimensionSelector();
            this.decimalEditor = new DynVis.Core.DecimalEditor();
            this.SuspendLayout();
            // 
            // dimensionSelector
            // 
            this.dimensionSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dimensionSelector.Location = new System.Drawing.Point(246, 0);
            this.dimensionSelector.Name = "dimensionSelector";
            this.dimensionSelector.Size = new System.Drawing.Size(23, 20);
            this.dimensionSelector.TabIndex = 1;
            this.dimensionSelector.Value = 0;
            this.dimensionSelector.OnDimensionChanged += new System.EventHandler(this.dimensionSelector_OnDimensionChanged);
            // 
            // decimalEditor
            // 
            this.decimalEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.decimalEditor.Location = new System.Drawing.Point(0, 0);
            this.decimalEditor.Name = "decimalEditor";
            this.decimalEditor.Size = new System.Drawing.Size(240, 20);
            this.decimalEditor.TabIndex = 3;
            this.decimalEditor.TextChanged += new System.EventHandler(this.textBoxEditValue_TextChanged);
            // 
            // PhysicalValueEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.decimalEditor);
            this.Controls.Add(this.dimensionSelector);
            this.Name = "PhysicalValueEditor";
            this.Size = new System.Drawing.Size(306, 20);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DimensionSelector dimensionSelector;
        private DecimalEditor decimalEditor;
    }
}
