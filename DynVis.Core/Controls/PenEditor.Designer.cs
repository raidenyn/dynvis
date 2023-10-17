namespace DynVis.Core.Controls
{
    partial class PenEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.enumEditor1 = new DynVis.Core.Controls.EnumEditor();
            this.penBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.colorButton = new DynVis.Core.Controls.ColorButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.penBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Цвет:";
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.enumEditor1);
            this.groupBox.Controls.Add(this.textBoxWidth);
            this.groupBox.Controls.Add(this.colorButton);
            this.groupBox.Controls.Add(this.label3);
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(345, 72);
            this.groupBox.TabIndex = 2;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Редактор карандаша";
            // 
            // enumEditor1
            // 
            this.enumEditor1.DataBindings.Add(new System.Windows.Forms.Binding("Enum", this.penBindingSource, "DashStyle", true));
            this.enumEditor1.FormattingEnabled = true;
            this.enumEditor1.Location = new System.Drawing.Point(198, 39);
            this.enumEditor1.Name = "enumEditor1";
            this.enumEditor1.Size = new System.Drawing.Size(133, 21);
            this.enumEditor1.TabIndex = 5;
            // 
            // penBindingSource
            // 
            this.penBindingSource.DataSource = typeof(System.Drawing.Pen);
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.penBindingSource, "Width", true));
            this.textBoxWidth.Location = new System.Drawing.Point(100, 39);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.Size = new System.Drawing.Size(71, 20);
            this.textBoxWidth.TabIndex = 4;
            // 
            // colorButton
            // 
            this.colorButton.BackColor = System.Drawing.SystemColors.Control;
            this.colorButton.Color = System.Drawing.SystemColors.Control;
            this.colorButton.DataBindings.Add(new System.Windows.Forms.Binding("Color", this.penBindingSource, "Color", true));
            this.colorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorButton.Location = new System.Drawing.Point(6, 36);
            this.colorButton.Name = "colorButton";
            this.colorButton.Size = new System.Drawing.Size(75, 23);
            this.colorButton.TabIndex = 0;
            this.colorButton.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Толщина:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(195, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Стиль:";
            // 
            // PenEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "PenEditor";
            this.Size = new System.Drawing.Size(345, 72);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.penBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ColorButton colorButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource penBindingSource;
        private System.Windows.Forms.TextBox textBoxWidth;
        private EnumEditor enumEditor1;
        private System.Windows.Forms.Label label3;
    }
}
