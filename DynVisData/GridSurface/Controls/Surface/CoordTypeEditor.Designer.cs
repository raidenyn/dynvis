namespace DynVis.Data.GridSurface
{
    partial class CoordTypeEditor
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
            this.radioButtonEnd = new System.Windows.Forms.RadioButton();
            this.radioButtonPeriodic = new System.Windows.Forms.RadioButton();
            this.radioButtonMirrored = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // radioButtonEnd
            // 
            this.radioButtonEnd.AutoSize = true;
            this.radioButtonEnd.Checked = true;
            this.radioButtonEnd.Location = new System.Drawing.Point(14, 14);
            this.radioButtonEnd.Name = "radioButtonEnd";
            this.radioButtonEnd.Size = new System.Drawing.Size(76, 17);
            this.radioButtonEnd.TabIndex = 1;
            this.radioButtonEnd.TabStop = true;
            this.radioButtonEnd.Text = "Обрывать";
            this.radioButtonEnd.UseVisualStyleBackColor = true;
            this.radioButtonEnd.CheckedChanged += new System.EventHandler(this.radioButtonEnd_CheckedChanged);
            // 
            // radioButtonPeriodic
            // 
            this.radioButtonPeriodic.AutoSize = true;
            this.radioButtonPeriodic.Location = new System.Drawing.Point(14, 51);
            this.radioButtonPeriodic.Name = "radioButtonPeriodic";
            this.radioButtonPeriodic.Size = new System.Drawing.Size(88, 17);
            this.radioButtonPeriodic.TabIndex = 1;
            this.radioButtonPeriodic.Text = "Продолжать";
            this.radioButtonPeriodic.UseVisualStyleBackColor = true;
            this.radioButtonPeriodic.CheckedChanged += new System.EventHandler(this.radioButtonPeriodic_CheckedChanged);
            // 
            // radioButtonMirrored
            // 
            this.radioButtonMirrored.AutoSize = true;
            this.radioButtonMirrored.Location = new System.Drawing.Point(14, 88);
            this.radioButtonMirrored.Name = "radioButtonMirrored";
            this.radioButtonMirrored.Size = new System.Drawing.Size(75, 17);
            this.radioButtonMirrored.TabIndex = 1;
            this.radioButtonMirrored.Text = "Отражать";
            this.radioButtonMirrored.UseVisualStyleBackColor = true;
            this.radioButtonMirrored.CheckedChanged += new System.EventHandler(this.radioButtonMirrored_CheckedChanged);
            // 
            // CoordTypeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioButtonMirrored);
            this.Controls.Add(this.radioButtonPeriodic);
            this.Controls.Add(this.radioButtonEnd);
            this.Name = "CoordTypeEditor";
            this.Size = new System.Drawing.Size(116, 122);
            this.Load += new System.EventHandler(this.CoordTypeEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButtonEnd;
        private System.Windows.Forms.RadioButton radioButtonPeriodic;
        private System.Windows.Forms.RadioButton radioButtonMirrored;
    }
}
