namespace DynVis.Data.CalculationMethods.Dynamic
{
    partial class FormDynamicParamsEditor
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
            this.groupBoxStartingState = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.physicalValueEditorQ2 = new Core.PhysicalValueEditor();
            this.physicalValueEditorQ1 = new Core.PhysicalValueEditor();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.labelPosition = new System.Windows.Forms.Label();
            this.labelQ2 = new System.Windows.Forms.Label();
            this.labelQ1 = new System.Windows.Forms.Label();
            this.decimalEditorV2 = new Core.DecimalEditor();
            this.decimalEditorV1 = new Core.DecimalEditor();
            this.groupBoxMass = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.physicalValueEditorM2 = new Core.PhysicalValueEditor();
            this.physicalValueEditorM1 = new Core.PhysicalValueEditor();
            this.groupBoxTime = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.labelTimeStep = new System.Windows.Forms.Label();
            this.physicalValueEditorTimeStep = new Core.PhysicalValueEditor();
            this.decimalEditorMaxStepCount = new Core.DecimalEditor();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxStartingState.SuspendLayout();
            this.groupBoxMass.SuspendLayout();
            this.groupBoxTime.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxStartingState
            // 
            this.groupBoxStartingState.Controls.Add(this.label4);
            this.groupBoxStartingState.Controls.Add(this.label3);
            this.groupBoxStartingState.Controls.Add(this.physicalValueEditorQ2);
            this.groupBoxStartingState.Controls.Add(this.physicalValueEditorQ1);
            this.groupBoxStartingState.Controls.Add(this.labelSpeed);
            this.groupBoxStartingState.Controls.Add(this.labelPosition);
            this.groupBoxStartingState.Controls.Add(this.labelQ2);
            this.groupBoxStartingState.Controls.Add(this.labelQ1);
            this.groupBoxStartingState.Controls.Add(this.decimalEditorV2);
            this.groupBoxStartingState.Controls.Add(this.decimalEditorV1);
            this.groupBoxStartingState.Location = new System.Drawing.Point(12, 25);
            this.groupBoxStartingState.Name = "groupBoxStartingState";
            this.groupBoxStartingState.Size = new System.Drawing.Size(364, 107);
            this.groupBoxStartingState.TabIndex = 1;
            this.groupBoxStartingState.TabStop = false;
            this.groupBoxStartingState.Text = "Начальное состояние";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(309, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Å / фс";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(309, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Å / фс";
            // 
            // physicalValueEditorQ2
            // 
            this.physicalValueEditorQ2.Location = new System.Drawing.Point(33, 67);
            this.physicalValueEditorQ2.Name = "physicalValueEditorQ2";
            this.physicalValueEditorQ2.Size = new System.Drawing.Size(165, 20);
            this.physicalValueEditorQ2.TabIndex = 3;
            this.physicalValueEditorQ2.Value = 0;
            // 
            // physicalValueEditorQ1
            // 
            this.physicalValueEditorQ1.Location = new System.Drawing.Point(33, 41);
            this.physicalValueEditorQ1.Name = "physicalValueEditorQ1";
            this.physicalValueEditorQ1.Size = new System.Drawing.Size(165, 20);
            this.physicalValueEditorQ1.TabIndex = 3;
            this.physicalValueEditorQ1.Value = 0;
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(201, 25);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(58, 13);
            this.labelSpeed.TabIndex = 2;
            this.labelSpeed.Text = "Скорости:";
            // 
            // labelPosition
            // 
            this.labelPosition.AutoSize = true;
            this.labelPosition.Location = new System.Drawing.Point(30, 25);
            this.labelPosition.Name = "labelPosition";
            this.labelPosition.Size = new System.Drawing.Size(72, 13);
            this.labelPosition.TabIndex = 2;
            this.labelPosition.Text = "Координаты:";
            // 
            // labelQ2
            // 
            this.labelQ2.AutoSize = true;
            this.labelQ2.Location = new System.Drawing.Point(6, 70);
            this.labelQ2.Name = "labelQ2";
            this.labelQ2.Size = new System.Drawing.Size(24, 13);
            this.labelQ2.TabIndex = 1;
            this.labelQ2.Text = "Q2:";
            // 
            // labelQ1
            // 
            this.labelQ1.AutoSize = true;
            this.labelQ1.Location = new System.Drawing.Point(6, 44);
            this.labelQ1.Name = "labelQ1";
            this.labelQ1.Size = new System.Drawing.Size(24, 13);
            this.labelQ1.TabIndex = 1;
            this.labelQ1.Text = "Q1:";
            // 
            // decimalEditorV2
            // 
            this.decimalEditorV2.Location = new System.Drawing.Point(204, 67);
            this.decimalEditorV2.Name = "decimalEditorV2";
            this.decimalEditorV2.Size = new System.Drawing.Size(99, 20);
            this.decimalEditorV2.TabIndex = 0;
            this.decimalEditorV2.Text = "1";
            this.decimalEditorV2.Value = 1;
            // 
            // decimalEditorV1
            // 
            this.decimalEditorV1.Location = new System.Drawing.Point(204, 41);
            this.decimalEditorV1.Name = "decimalEditorV1";
            this.decimalEditorV1.Size = new System.Drawing.Size(99, 20);
            this.decimalEditorV1.TabIndex = 0;
            this.decimalEditorV1.Text = "1";
            this.decimalEditorV1.Value = 1;
            // 
            // groupBoxMass
            // 
            this.groupBoxMass.Controls.Add(this.label1);
            this.groupBoxMass.Controls.Add(this.label2);
            this.groupBoxMass.Controls.Add(this.physicalValueEditorM2);
            this.groupBoxMass.Controls.Add(this.physicalValueEditorM1);
            this.groupBoxMass.Location = new System.Drawing.Point(378, 25);
            this.groupBoxMass.Name = "groupBoxMass";
            this.groupBoxMass.Size = new System.Drawing.Size(205, 107);
            this.groupBoxMass.TabIndex = 1;
            this.groupBoxMass.TabStop = false;
            this.groupBoxMass.Text = "Средние массы";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Q2:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Q1:";
            // 
            // physicalValueEditorM2
            // 
            this.physicalValueEditorM2.DimensionType = Core.DimensionType.Mass;
            this.physicalValueEditorM2.Location = new System.Drawing.Point(34, 60);
            this.physicalValueEditorM2.Name = "physicalValueEditorM2";
            this.physicalValueEditorM2.Size = new System.Drawing.Size(165, 20);
            this.physicalValueEditorM2.TabIndex = 3;
            this.physicalValueEditorM2.Value = 0;
            // 
            // physicalValueEditorM1
            // 
            this.physicalValueEditorM1.DimensionType = Core.DimensionType.Mass;
            this.physicalValueEditorM1.Location = new System.Drawing.Point(34, 34);
            this.physicalValueEditorM1.Name = "physicalValueEditorM1";
            this.physicalValueEditorM1.Size = new System.Drawing.Size(165, 20);
            this.physicalValueEditorM1.TabIndex = 3;
            this.physicalValueEditorM1.Value = 0;
            // 
            // groupBoxTime
            // 
            this.groupBoxTime.Controls.Add(this.label7);
            this.groupBoxTime.Controls.Add(this.labelTimeStep);
            this.groupBoxTime.Controls.Add(this.physicalValueEditorTimeStep);
            this.groupBoxTime.Controls.Add(this.decimalEditorMaxStepCount);
            this.groupBoxTime.Location = new System.Drawing.Point(12, 138);
            this.groupBoxTime.Name = "groupBoxTime";
            this.groupBoxTime.Size = new System.Drawing.Size(364, 76);
            this.groupBoxTime.TabIndex = 1;
            this.groupBoxTime.TabStop = false;
            this.groupBoxTime.Text = "Параметры динамики";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 50);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(147, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "Максимально число шагов:";
            // 
            // labelTimeStep
            // 
            this.labelTimeStep.AutoSize = true;
            this.labelTimeStep.Location = new System.Drawing.Point(6, 22);
            this.labelTimeStep.Name = "labelTimeStep";
            this.labelTimeStep.Size = new System.Drawing.Size(77, 13);
            this.labelTimeStep.TabIndex = 1;
            this.labelTimeStep.Text = "Шаг времени:";
            // 
            // physicalValueEditorTimeStep
            // 
            this.physicalValueEditorTimeStep.DimensionType = Core.DimensionType.Time;
            this.physicalValueEditorTimeStep.Location = new System.Drawing.Point(155, 19);
            this.physicalValueEditorTimeStep.Name = "physicalValueEditorTimeStep";
            this.physicalValueEditorTimeStep.ScaleCoeff = Core.ScaleCoeff.femto;
            this.physicalValueEditorTimeStep.Size = new System.Drawing.Size(163, 20);
            this.physicalValueEditorTimeStep.TabIndex = 3;
            this.physicalValueEditorTimeStep.Value = 0;
            // 
            // decimalEditorMaxStepCount
            // 
            this.decimalEditorMaxStepCount.Location = new System.Drawing.Point(155, 47);
            this.decimalEditorMaxStepCount.Name = "decimalEditorMaxStepCount";
            this.decimalEditorMaxStepCount.Size = new System.Drawing.Size(104, 20);
            this.decimalEditorMaxStepCount.TabIndex = 0;
            this.decimalEditorMaxStepCount.Text = "1";
            this.decimalEditorMaxStepCount.Value = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(508, 188);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(427, 188);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSave,
            this.ToolStripMenuItemOpen});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(590, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItemSave
            // 
            this.ToolStripMenuItemSave.Name = "ToolStripMenuItemSave";
            this.ToolStripMenuItemSave.Size = new System.Drawing.Size(77, 20);
            this.ToolStripMenuItemSave.Text = "Сохранить";
            this.ToolStripMenuItemSave.Click += new System.EventHandler(this.ToolStripMenuItemSave_Click);
            // 
            // ToolStripMenuItemOpen
            // 
            this.ToolStripMenuItemOpen.Name = "ToolStripMenuItemOpen";
            this.ToolStripMenuItemOpen.Size = new System.Drawing.Size(66, 20);
            this.ToolStripMenuItemOpen.Text = "Открыть";
            this.ToolStripMenuItemOpen.Click += new System.EventHandler(this.ToolStripMenuItemOpen_Click);
            // 
            // FormDynamicParamsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(590, 223);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBoxTime);
            this.Controls.Add(this.groupBoxMass);
            this.Controls.Add(this.groupBoxStartingState);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormDynamicParamsEditor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Динамика реакции";
            this.Load += new System.EventHandler(this.FormDynamicParamsEditor_Load);
            this.groupBoxStartingState.ResumeLayout(false);
            this.groupBoxStartingState.PerformLayout();
            this.groupBoxMass.ResumeLayout(false);
            this.groupBoxMass.PerformLayout();
            this.groupBoxTime.ResumeLayout(false);
            this.groupBoxTime.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxStartingState;
        private System.Windows.Forms.Label labelQ1;
        private System.Windows.Forms.Label labelQ2;
        private System.Windows.Forms.GroupBox groupBoxMass;
        private System.Windows.Forms.Label label2;
        private Core.PhysicalValueEditor physicalValueEditorQ2;
        private Core.PhysicalValueEditor physicalValueEditorQ1;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label labelPosition;
        private Core.DecimalEditor decimalEditorV2;
        private Core.DecimalEditor decimalEditorV1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private Core.PhysicalValueEditor physicalValueEditorM2;
        private Core.PhysicalValueEditor physicalValueEditorM1;
        private System.Windows.Forms.GroupBox groupBoxTime;
        private System.Windows.Forms.Label labelTimeStep;
        private Core.PhysicalValueEditor physicalValueEditorTimeStep;
        private System.Windows.Forms.Label label7;
        private Core.DecimalEditor decimalEditorMaxStepCount;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOpen;
    }
}