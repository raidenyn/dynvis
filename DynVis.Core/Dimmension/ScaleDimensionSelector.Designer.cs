namespace DynVis.Core.Dimmension
{
    partial class ScaleDimensionSelector
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
            this.comboBoxDimensionType = new System.Windows.Forms.ComboBox();
            this.dimensionSelector = new DimensionSelector();
            this.SuspendLayout();
            // 
            // comboBoxDimensionType
            // 
            this.comboBoxDimensionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDimensionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDimensionType.FormattingEnabled = true;
            this.comboBoxDimensionType.Location = new System.Drawing.Point(3, 0);
            this.comboBoxDimensionType.Name = "comboBoxDimensionType";
            this.comboBoxDimensionType.Size = new System.Drawing.Size(130, 21);
            this.comboBoxDimensionType.TabIndex = 0;
            this.comboBoxDimensionType.SelectedIndexChanged += new System.EventHandler(this.comboBoxDimensionType_SelectedIndexChanged);
            // 
            // dimensionSelector
            // 
            this.dimensionSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dimensionSelector.Location = new System.Drawing.Point(139, 1);
            this.dimensionSelector.Name = "dimensionSelector";
            this.dimensionSelector.Size = new System.Drawing.Size(23, 20);
            this.dimensionSelector.TabIndex = 1;
            this.dimensionSelector.Value = 0;
            this.dimensionSelector.OnDimensionChanged += new System.EventHandler(this.dimensionSelector_OnDimensionChanged);
            this.dimensionSelector.OnDimensionTypeChanged += new System.EventHandler(this.dimensionSelector_OnDimensionTypeChanged);
            // 
            // ScaleDimensionSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dimensionSelector);
            this.Controls.Add(this.comboBoxDimensionType);
            this.Name = "ScaleDimensionSelector";
            this.Size = new System.Drawing.Size(192, 24);
            this.Load += new System.EventHandler(this.TypeDimensionSelector_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxDimensionType;
        private DimensionSelector dimensionSelector;
    }
}
