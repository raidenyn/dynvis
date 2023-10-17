namespace DynVis.Draw.Geomtery
{
    partial class StatePanel
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
            this.buttonBond = new System.Windows.Forms.Button();
            this.labelSelectionAtomList = new System.Windows.Forms.Label();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonBond
            // 
            this.buttonBond.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonBond.AutoEllipsis = true;
            this.buttonBond.CausesValidation = false;
            this.buttonBond.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBond.Location = new System.Drawing.Point(3, 4);
            this.buttonBond.Name = "buttonBond";
            this.buttonBond.Size = new System.Drawing.Size(24, 23);
            this.buttonBond.TabIndex = 2;
            this.buttonBond.Text = "B";
            this.buttonBond.UseCompatibleTextRendering = true;
            this.buttonBond.UseVisualStyleBackColor = false;
            this.buttonBond.Visible = false;
            this.buttonBond.Click += new System.EventHandler(this.buttonBond_Click);
            // 
            // labelSelectionAtomList
            // 
            this.labelSelectionAtomList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelSelectionAtomList.AutoSize = true;
            this.labelSelectionAtomList.Location = new System.Drawing.Point(33, 9);
            this.labelSelectionAtomList.Name = "labelSelectionAtomList";
            this.labelSelectionAtomList.Size = new System.Drawing.Size(0, 13);
            this.labelSelectionAtomList.TabIndex = 1;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxValue.Location = new System.Drawing.Point(306, 6);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.ReadOnly = true;
            this.textBoxValue.Size = new System.Drawing.Size(149, 20);
            this.textBoxValue.TabIndex = 0;
            // 
            // StatePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.buttonBond);
            this.Controls.Add(this.labelSelectionAtomList);
            this.Name = "StatePanel";
            this.Size = new System.Drawing.Size(458, 31);
            this.Enter += new System.EventHandler(this.StatePanel_Enter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBond;
        private System.Windows.Forms.Label labelSelectionAtomList;
        private System.Windows.Forms.TextBox textBoxValue;
    }
}
