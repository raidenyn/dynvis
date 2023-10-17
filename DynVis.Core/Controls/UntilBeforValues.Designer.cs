namespace DynVis.Core
{
    partial class UntilBeforValues
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
            this.labelBefor = new System.Windows.Forms.Label();
            this.labelUntil = new System.Windows.Forms.Label();
            this.textBoxBefor = new System.Windows.Forms.TextBox();
            this.textBoxUntil = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelBefor
            // 
            this.labelBefor.AutoSize = true;
            this.labelBefor.Location = new System.Drawing.Point(166, 4);
            this.labelBefor.Name = "labelBefor";
            this.labelBefor.Size = new System.Drawing.Size(19, 13);
            this.labelBefor.TabIndex = 3;
            this.labelBefor.Text = "до";
            // 
            // labelUntil
            // 
            this.labelUntil.AutoSize = true;
            this.labelUntil.Location = new System.Drawing.Point(3, 4);
            this.labelUntil.Name = "labelUntil";
            this.labelUntil.Size = new System.Drawing.Size(20, 13);
            this.labelUntil.TabIndex = 5;
            this.labelUntil.Text = "От";
            // 
            // textBoxBefor
            // 
            this.textBoxBefor.Location = new System.Drawing.Point(191, 1);
            this.textBoxBefor.Name = "textBoxBefor";
            this.textBoxBefor.Size = new System.Drawing.Size(130, 20);
            this.textBoxBefor.TabIndex = 4;
            // 
            // textBoxUntil
            // 
            this.textBoxUntil.Location = new System.Drawing.Point(30, 1);
            this.textBoxUntil.Name = "textBoxUntil";
            this.textBoxUntil.Size = new System.Drawing.Size(130, 20);
            this.textBoxUntil.TabIndex = 2;
            // 
            // UntilBeforValues
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelBefor);
            this.Controls.Add(this.labelUntil);
            this.Controls.Add(this.textBoxBefor);
            this.Controls.Add(this.textBoxUntil);
            this.Name = "UntilBeforValues";
            this.Size = new System.Drawing.Size(324, 24);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBefor;
        private System.Windows.Forms.Label labelUntil;
        private System.Windows.Forms.TextBox textBoxBefor;
        private System.Windows.Forms.TextBox textBoxUntil;
    }
}
