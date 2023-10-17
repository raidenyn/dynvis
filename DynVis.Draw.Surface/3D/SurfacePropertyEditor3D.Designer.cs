namespace DynVis.Draw.Surface
{
    partial class SurfacePropertyEditor3D
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
            this.colorButtonBackgroud = new DynVis.Core.Controls.ColorButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxQ2Count = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxQ1Count = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxEnergyScale = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPathLineWidth = new System.Windows.Forms.TextBox();
            this.groupBoxLinePath = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.colorButtonLinePath = new DynVis.Core.Controls.ColorButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.colorButtonCurrentPointColor = new DynVis.Core.Controls.ColorButton();
            this.textBoxCurrentPointSize = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.drawSurfaceEngineGL3DBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBoxLinePath.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawSurfaceEngineGL3DBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // colorButtonBackgroud
            // 
            this.colorButtonBackgroud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorButtonBackgroud.Color = System.Drawing.SystemColors.Control;
            this.colorButtonBackgroud.DataBindings.Add(new System.Windows.Forms.Binding("Color", this.drawSurfaceEngineGL3DBindingSource, "BackgroundColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.colorButtonBackgroud.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorButtonBackgroud.Location = new System.Drawing.Point(73, 13);
            this.colorButtonBackgroud.Name = "colorButtonBackgroud";
            this.colorButtonBackgroud.Size = new System.Drawing.Size(311, 23);
            this.colorButtonBackgroud.TabIndex = 0;
            this.colorButtonBackgroud.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Цвет фона:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Количество узлов построения поверхности:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "По первой координате:";
            // 
            // textBoxQ2Count
            // 
            this.textBoxQ2Count.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQ2Count.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.drawSurfaceEngineGL3DBindingSource, "CountQ1", true));
            this.textBoxQ2Count.Location = new System.Drawing.Point(152, 79);
            this.textBoxQ2Count.Name = "textBoxQ2Count";
            this.textBoxQ2Count.Size = new System.Drawing.Size(232, 20);
            this.textBoxQ2Count.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "По второй координате:";
            // 
            // textBoxQ1Count
            // 
            this.textBoxQ1Count.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQ1Count.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.drawSurfaceEngineGL3DBindingSource, "CountQ2", true));
            this.textBoxQ1Count.Location = new System.Drawing.Point(152, 105);
            this.textBoxQ1Count.Name = "textBoxQ1Count";
            this.textBoxQ1Count.Size = new System.Drawing.Size(232, 20);
            this.textBoxQ1Count.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(224, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Относительный массштаб по оси энергии:";
            // 
            // textBoxEnergyScale
            // 
            this.textBoxEnergyScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEnergyScale.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.drawSurfaceEngineGL3DBindingSource, "ScaleE", true));
            this.textBoxEnergyScale.Location = new System.Drawing.Point(6, 172);
            this.textBoxEnergyScale.Name = "textBoxEnergyScale";
            this.textBoxEnergyScale.Size = new System.Drawing.Size(378, 20);
            this.textBoxEnergyScale.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Толщина:";
            // 
            // textBoxPathLineWidth
            // 
            this.textBoxPathLineWidth.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.drawSurfaceEngineGL3DBindingSource, "PathLineWidth", true));
            this.textBoxPathLineWidth.Location = new System.Drawing.Point(9, 32);
            this.textBoxPathLineWidth.Name = "textBoxPathLineWidth";
            this.textBoxPathLineWidth.Size = new System.Drawing.Size(159, 20);
            this.textBoxPathLineWidth.TabIndex = 6;
            // 
            // groupBoxLinePath
            // 
            this.groupBoxLinePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxLinePath.Controls.Add(this.colorButtonLinePath);
            this.groupBoxLinePath.Controls.Add(this.textBoxPathLineWidth);
            this.groupBoxLinePath.Controls.Add(this.label8);
            this.groupBoxLinePath.Controls.Add(this.label7);
            this.groupBoxLinePath.Location = new System.Drawing.Point(6, 270);
            this.groupBoxLinePath.Name = "groupBoxLinePath";
            this.groupBoxLinePath.Size = new System.Drawing.Size(378, 65);
            this.groupBoxLinePath.TabIndex = 7;
            this.groupBoxLinePath.TabStop = false;
            this.groupBoxLinePath.Text = "Линия пути реакции";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(189, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 3;
            this.label8.Text = "Цвет:";
            // 
            // colorButtonLinePath
            // 
            this.colorButtonLinePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorButtonLinePath.Color = System.Drawing.SystemColors.Control;
            this.colorButtonLinePath.DataBindings.Add(new System.Windows.Forms.Binding("Color", this.drawSurfaceEngineGL3DBindingSource, "PathLineColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.colorButtonLinePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorButtonLinePath.Location = new System.Drawing.Point(192, 30);
            this.colorButtonLinePath.Name = "colorButtonLinePath";
            this.colorButtonLinePath.Size = new System.Drawing.Size(180, 23);
            this.colorButtonLinePath.TabIndex = 7;
            this.colorButtonLinePath.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.colorButtonCurrentPointColor);
            this.groupBox1.Controls.Add(this.textBoxCurrentPointSize);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Location = new System.Drawing.Point(6, 198);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(378, 65);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Фигуративная точка";
            // 
            // colorButtonCurrentPointColor
            // 
            this.colorButtonCurrentPointColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorButtonCurrentPointColor.Color = System.Drawing.SystemColors.Control;
            this.colorButtonCurrentPointColor.DataBindings.Add(new System.Windows.Forms.Binding("Color", this.drawSurfaceEngineGL3DBindingSource, "CurrentPointColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.colorButtonCurrentPointColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorButtonCurrentPointColor.Location = new System.Drawing.Point(192, 30);
            this.colorButtonCurrentPointColor.Name = "colorButtonCurrentPointColor";
            this.colorButtonCurrentPointColor.Size = new System.Drawing.Size(180, 23);
            this.colorButtonCurrentPointColor.TabIndex = 7;
            this.colorButtonCurrentPointColor.UseVisualStyleBackColor = true;
            // 
            // textBoxCurrentPointSize
            // 
            this.textBoxCurrentPointSize.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.drawSurfaceEngineGL3DBindingSource, "CurrentPointSize", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.textBoxCurrentPointSize.Location = new System.Drawing.Point(9, 32);
            this.textBoxCurrentPointSize.Name = "textBoxCurrentPointSize";
            this.textBoxCurrentPointSize.Size = new System.Drawing.Size(159, 20);
            this.textBoxCurrentPointSize.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(189, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "Цвет:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Размер:";
            // 
            // drawSurfaceEngineGL3DBindingSource
            // 
            this.drawSurfaceEngineGL3DBindingSource.DataSource = typeof(DynVis.Draw.Surface.DrawSurfaceEngineGL_3D);
            this.drawSurfaceEngineGL3DBindingSource.CurrentItemChanged += new System.EventHandler(this.drawSurfaceEngineGL3DBindingSource_CurrentItemChanged);
            // 
            // SurfacePropertyEditor3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxLinePath);
            this.Controls.Add(this.textBoxEnergyScale);
            this.Controls.Add(this.textBoxQ1Count);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxQ2Count);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.colorButtonBackgroud);
            this.Name = "SurfacePropertyEditor3D";
            this.Size = new System.Drawing.Size(387, 338);
            this.EditingObjectChanged += new System.EventHandler(this.SurfacePropertyEditor3D_EditingObjectChanged);
            this.groupBoxLinePath.ResumeLayout(false);
            this.groupBoxLinePath.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drawSurfaceEngineGL3DBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DynVis.Core.Controls.ColorButton colorButtonBackgroud;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource drawSurfaceEngineGL3DBindingSource;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxQ2Count;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxQ1Count;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxEnergyScale;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPathLineWidth;
        private System.Windows.Forms.GroupBox groupBoxLinePath;
        private DynVis.Core.Controls.ColorButton colorButtonLinePath;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private DynVis.Core.Controls.ColorButton colorButtonCurrentPointColor;
        private System.Windows.Forms.TextBox textBoxCurrentPointSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}
