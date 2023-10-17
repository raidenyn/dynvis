namespace DynVis.Core.Surface.IO
{
    partial class FormFileProperty
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
            this.textBoxQ1Count = new System.Windows.Forms.TextBox();
            this.labelQ1Count = new System.Windows.Forms.Label();
            this.textBoxQ2Count = new System.Windows.Forms.TextBox();
            this.labelQ2Count = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxFirstDerivative = new System.Windows.Forms.CheckBox();
            this.checkBoxSecondDerivative = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBoxQ1Count
            // 
            this.textBoxQ1Count.Location = new System.Drawing.Point(204, 12);
            this.textBoxQ1Count.Name = "textBoxQ1Count";
            this.textBoxQ1Count.Size = new System.Drawing.Size(108, 20);
            this.textBoxQ1Count.TabIndex = 0;
            // 
            // labelQ1Count
            // 
            this.labelQ1Count.AutoSize = true;
            this.labelQ1Count.Location = new System.Drawing.Point(12, 15);
            this.labelQ1Count.Name = "labelQ1Count";
            this.labelQ1Count.Size = new System.Drawing.Size(189, 13);
            this.labelQ1Count.TabIndex = 1;
            this.labelQ1Count.Text = "Число точек по первой координате:";
            // 
            // textBoxQ2Count
            // 
            this.textBoxQ2Count.Location = new System.Drawing.Point(204, 38);
            this.textBoxQ2Count.Name = "textBoxQ2Count";
            this.textBoxQ2Count.Size = new System.Drawing.Size(108, 20);
            this.textBoxQ2Count.TabIndex = 0;
            // 
            // labelQ2Count
            // 
            this.labelQ2Count.AutoSize = true;
            this.labelQ2Count.Location = new System.Drawing.Point(12, 41);
            this.labelQ2Count.Name = "labelQ2Count";
            this.labelQ2Count.Size = new System.Drawing.Size(188, 13);
            this.labelQ2Count.TabIndex = 1;
            this.labelQ2Count.Text = "Число точек по второй координате:";
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(237, 117);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // checkBoxFirstDerivative
            // 
            this.checkBoxFirstDerivative.AutoSize = true;
            this.checkBoxFirstDerivative.Checked = true;
            this.checkBoxFirstDerivative.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxFirstDerivative.Location = new System.Drawing.Point(15, 69);
            this.checkBoxFirstDerivative.Name = "checkBoxFirstDerivative";
            this.checkBoxFirstDerivative.Size = new System.Drawing.Size(187, 17);
            this.checkBoxFirstDerivative.TabIndex = 3;
            this.checkBoxFirstDerivative.Text = "Включать первые производные";
            this.checkBoxFirstDerivative.UseVisualStyleBackColor = true;
            // 
            // checkBoxSecondDerivative
            // 
            this.checkBoxSecondDerivative.AutoSize = true;
            this.checkBoxSecondDerivative.Location = new System.Drawing.Point(15, 92);
            this.checkBoxSecondDerivative.Name = "checkBoxSecondDerivative";
            this.checkBoxSecondDerivative.Size = new System.Drawing.Size(186, 17);
            this.checkBoxSecondDerivative.TabIndex = 3;
            this.checkBoxSecondDerivative.Text = "Включать вторые производные";
            this.checkBoxSecondDerivative.UseVisualStyleBackColor = true;
            // 
            // FormFileProperty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 148);
            this.Controls.Add(this.checkBoxSecondDerivative);
            this.Controls.Add(this.checkBoxFirstDerivative);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelQ2Count);
            this.Controls.Add(this.labelQ1Count);
            this.Controls.Add(this.textBoxQ2Count);
            this.Controls.Add(this.textBoxQ1Count);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFileProperty";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Число точек";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormNumberPoint_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxQ1Count;
        private System.Windows.Forms.Label labelQ1Count;
        private System.Windows.Forms.TextBox textBoxQ2Count;
        private System.Windows.Forms.Label labelQ2Count;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckBox checkBoxFirstDerivative;
        private System.Windows.Forms.CheckBox checkBoxSecondDerivative;
    }
}