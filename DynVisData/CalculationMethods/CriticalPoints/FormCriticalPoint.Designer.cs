namespace DynVis.Data.CalculationMethods.CriticalPoints
{
    partial class FormCriticalPoint
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxScan = new System.Windows.Forms.GroupBox();
            this.groupBoxPoint = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.radioButtonPoint = new System.Windows.Forms.RadioButton();
            this.radioButtonScanSurface = new System.Windows.Forms.RadioButton();
            this.decimalEditorQ2 = new DynVis.Core.DecimalEditor();
            this.decimalEditorQ1 = new DynVis.Core.DecimalEditor();
            this.decimalEditorScanStepCount = new DynVis.Core.DecimalEditor();
            this.decimalEditorEplsilon = new DynVis.Core.DecimalEditor();
            this.decimalEditorOPtimizationMaxStepCount = new DynVis.Core.DecimalEditor();
            this.groupBoxScan.SuspendLayout();
            this.groupBoxPoint.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Количество шагов сканирования:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Погрешность значения производных:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Количество шагов дооптимизации:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(290, 265);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(209, 265);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBoxScan
            // 
            this.groupBoxScan.Controls.Add(this.label3);
            this.groupBoxScan.Controls.Add(this.decimalEditorScanStepCount);
            this.groupBoxScan.Controls.Add(this.decimalEditorEplsilon);
            this.groupBoxScan.Controls.Add(this.decimalEditorOPtimizationMaxStepCount);
            this.groupBoxScan.Controls.Add(this.label2);
            this.groupBoxScan.Controls.Add(this.label1);
            this.groupBoxScan.Location = new System.Drawing.Point(12, 33);
            this.groupBoxScan.Name = "groupBoxScan";
            this.groupBoxScan.Size = new System.Drawing.Size(359, 117);
            this.groupBoxScan.TabIndex = 4;
            this.groupBoxScan.TabStop = false;
            this.groupBoxScan.Text = "Сканирование поверхности";
            // 
            // groupBoxPoint
            // 
            this.groupBoxPoint.Controls.Add(this.decimalEditorQ2);
            this.groupBoxPoint.Controls.Add(this.decimalEditorQ1);
            this.groupBoxPoint.Controls.Add(this.label5);
            this.groupBoxPoint.Controls.Add(this.label4);
            this.groupBoxPoint.Location = new System.Drawing.Point(12, 179);
            this.groupBoxPoint.Name = "groupBoxPoint";
            this.groupBoxPoint.Size = new System.Drawing.Size(359, 78);
            this.groupBoxPoint.TabIndex = 5;
            this.groupBoxPoint.TabStop = false;
            this.groupBoxPoint.Text = "Точка в окрестности";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Координата 2:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Координата 1:";
            // 
            // radioButtonPoint
            // 
            this.radioButtonPoint.AutoSize = true;
            this.radioButtonPoint.Location = new System.Drawing.Point(12, 156);
            this.radioButtonPoint.Name = "radioButtonPoint";
            this.radioButtonPoint.Size = new System.Drawing.Size(213, 17);
            this.radioButtonPoint.TabIndex = 6;
            this.radioButtonPoint.Text = "Найти точку в заданной окрестности";
            this.radioButtonPoint.UseVisualStyleBackColor = true;
            this.radioButtonPoint.CheckedChanged += new System.EventHandler(this.radioButtonPoint_CheckedChanged);
            // 
            // radioButtonScanSurface
            // 
            this.radioButtonScanSurface.AutoSize = true;
            this.radioButtonScanSurface.Location = new System.Drawing.Point(12, 10);
            this.radioButtonScanSurface.Name = "radioButtonScanSurface";
            this.radioButtonScanSurface.Size = new System.Drawing.Size(181, 17);
            this.radioButtonScanSurface.TabIndex = 6;
            this.radioButtonScanSurface.Text = "Сканировать всю поверхность";
            this.radioButtonScanSurface.UseVisualStyleBackColor = true;
            this.radioButtonScanSurface.CheckedChanged += new System.EventHandler(this.radioButtonScanSurface_CheckedChanged);
            // 
            // decimalEditorQ2
            // 
            this.decimalEditorQ2.Location = new System.Drawing.Point(91, 43);
            this.decimalEditorQ2.Name = "decimalEditorQ2";
            this.decimalEditorQ2.Size = new System.Drawing.Size(245, 20);
            this.decimalEditorQ2.TabIndex = 2;
            // 
            // decimalEditorQ1
            // 
            this.decimalEditorQ1.Location = new System.Drawing.Point(91, 17);
            this.decimalEditorQ1.Name = "decimalEditorQ1";
            this.decimalEditorQ1.Size = new System.Drawing.Size(245, 20);
            this.decimalEditorQ1.TabIndex = 2;
            // 
            // decimalEditorScanStepCount
            // 
            this.decimalEditorScanStepCount.CanNotBeNull = true;
            this.decimalEditorScanStepCount.Location = new System.Drawing.Point(207, 25);
            this.decimalEditorScanStepCount.Name = "decimalEditorScanStepCount";
            this.decimalEditorScanStepCount.PositiveOnly = true;
            this.decimalEditorScanStepCount.Size = new System.Drawing.Size(129, 20);
            this.decimalEditorScanStepCount.TabIndex = 0;
            this.decimalEditorScanStepCount.Text = "0";
            // 
            // decimalEditorEplsilon
            // 
            this.decimalEditorEplsilon.CanNotBeNull = true;
            this.decimalEditorEplsilon.Location = new System.Drawing.Point(207, 54);
            this.decimalEditorEplsilon.Name = "decimalEditorEplsilon";
            this.decimalEditorEplsilon.PositiveOnly = true;
            this.decimalEditorEplsilon.Size = new System.Drawing.Size(129, 20);
            this.decimalEditorEplsilon.TabIndex = 0;
            this.decimalEditorEplsilon.Text = "0";
            // 
            // decimalEditorOPtimizationMaxStepCount
            // 
            this.decimalEditorOPtimizationMaxStepCount.CanNotBeNull = true;
            this.decimalEditorOPtimizationMaxStepCount.Location = new System.Drawing.Point(207, 80);
            this.decimalEditorOPtimizationMaxStepCount.Name = "decimalEditorOPtimizationMaxStepCount";
            this.decimalEditorOPtimizationMaxStepCount.PositiveOnly = true;
            this.decimalEditorOPtimizationMaxStepCount.Size = new System.Drawing.Size(129, 20);
            this.decimalEditorOPtimizationMaxStepCount.TabIndex = 0;
            this.decimalEditorOPtimizationMaxStepCount.Text = "0";
            // 
            // FormCriticalPoint
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonOK;
            this.ClientSize = new System.Drawing.Size(377, 300);
            this.Controls.Add(this.radioButtonScanSurface);
            this.Controls.Add(this.radioButtonPoint);
            this.Controls.Add(this.groupBoxPoint);
            this.Controls.Add(this.groupBoxScan);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCriticalPoint";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Расчет критических точек:";
            this.groupBoxScan.ResumeLayout(false);
            this.groupBoxScan.PerformLayout();
            this.groupBoxPoint.ResumeLayout(false);
            this.groupBoxPoint.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DynVis.Core.DecimalEditor decimalEditorScanStepCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private DynVis.Core.DecimalEditor decimalEditorEplsilon;
        private DynVis.Core.DecimalEditor decimalEditorOPtimizationMaxStepCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBoxScan;
        private System.Windows.Forms.GroupBox groupBoxPoint;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioButtonPoint;
        private System.Windows.Forms.RadioButton radioButtonScanSurface;
        private DynVis.Core.DecimalEditor decimalEditorQ2;
        private DynVis.Core.DecimalEditor decimalEditorQ1;
    }
}