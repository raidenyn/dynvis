namespace DynVis.Data.CalculationMethods.LEPS.Controls
{
    partial class CoordinateParamsLEPS
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
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.physicalValueEditorQ2Max = new DynVis.Core.PhysicalValueEditor();
            this.physicalValueEditorConstValue = new DynVis.Core.PhysicalValueEditor();
            this.physicalValueEditorQ2Min = new DynVis.Core.PhysicalValueEditor();
            this.physicalValueEditorQ1Max = new DynVis.Core.PhysicalValueEditor();
            this.physicalValueEditorQ1Min = new DynVis.Core.PhysicalValueEditor();
            this.labelQConst = new System.Windows.Forms.Label();
            this.labelQ2 = new System.Windows.Forms.Label();
            this.labelQ1 = new System.Windows.Forms.Label();
            this.radioButtonDistanceAngle = new System.Windows.Forms.RadioButton();
            this.radioButtonDistanceDistance = new System.Windows.Forms.RadioButton();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.label2);
            this.groupBox.Controls.Add(this.label1);
            this.groupBox.Controls.Add(this.physicalValueEditorQ2Max);
            this.groupBox.Controls.Add(this.physicalValueEditorConstValue);
            this.groupBox.Controls.Add(this.physicalValueEditorQ2Min);
            this.groupBox.Controls.Add(this.physicalValueEditorQ1Max);
            this.groupBox.Controls.Add(this.physicalValueEditorQ1Min);
            this.groupBox.Controls.Add(this.labelQConst);
            this.groupBox.Controls.Add(this.labelQ2);
            this.groupBox.Controls.Add(this.labelQ1);
            this.groupBox.Controls.Add(this.radioButtonDistanceAngle);
            this.groupBox.Controls.Add(this.radioButtonDistanceDistance);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(610, 185);
            this.groupBox.TabIndex = 0;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Координаты поверхности";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(405, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "до";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(405, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "до";
            // 
            // physicalValueEditorQ2Max
            // 
            this.physicalValueEditorQ2Max.Location = new System.Drawing.Point(430, 83);
            this.physicalValueEditorQ2Max.Name = "physicalValueEditorQ2Max";
            this.physicalValueEditorQ2Max.Size = new System.Drawing.Size(180, 20);
            this.physicalValueEditorQ2Max.TabIndex = 4;
            this.physicalValueEditorQ2Max.Value = 0;
            // 
            // physicalValueEditorConstValue
            // 
            this.physicalValueEditorConstValue.DimensionType = DynVis.Core.DimensionType.Angle;
            this.physicalValueEditorConstValue.Location = new System.Drawing.Point(240, 143);
            this.physicalValueEditorConstValue.Name = "physicalValueEditorConstValue";
            this.physicalValueEditorConstValue.Size = new System.Drawing.Size(180, 20);
            this.physicalValueEditorConstValue.TabIndex = 4;
            this.physicalValueEditorConstValue.Value = 0;
            // 
            // physicalValueEditorQ2Min
            // 
            this.physicalValueEditorQ2Min.Location = new System.Drawing.Point(240, 83);
            this.physicalValueEditorQ2Min.Name = "physicalValueEditorQ2Min";
            this.physicalValueEditorQ2Min.Size = new System.Drawing.Size(180, 20);
            this.physicalValueEditorQ2Min.TabIndex = 4;
            this.physicalValueEditorQ2Min.Value = 0;
            // 
            // physicalValueEditorQ1Max
            // 
            this.physicalValueEditorQ1Max.Location = new System.Drawing.Point(430, 33);
            this.physicalValueEditorQ1Max.Name = "physicalValueEditorQ1Max";
            this.physicalValueEditorQ1Max.Size = new System.Drawing.Size(180, 20);
            this.physicalValueEditorQ1Max.TabIndex = 4;
            this.physicalValueEditorQ1Max.Value = 0;
            // 
            // physicalValueEditorQ1Min
            // 
            this.physicalValueEditorQ1Min.Location = new System.Drawing.Point(240, 33);
            this.physicalValueEditorQ1Min.Name = "physicalValueEditorQ1Min";
            this.physicalValueEditorQ1Min.Size = new System.Drawing.Size(180, 20);
            this.physicalValueEditorQ1Min.TabIndex = 4;
            this.physicalValueEditorQ1Min.Value = 0;
            // 
            // labelQConst
            // 
            this.labelQConst.AutoSize = true;
            this.labelQConst.Location = new System.Drawing.Point(237, 129);
            this.labelQConst.Name = "labelQConst";
            this.labelQConst.Size = new System.Drawing.Size(52, 13);
            this.labelQConst.TabIndex = 2;
            this.labelQConst.Text = "Угол Q3:";
            // 
            // labelQ2
            // 
            this.labelQ2.AutoSize = true;
            this.labelQ2.Location = new System.Drawing.Point(237, 70);
            this.labelQ2.Name = "labelQ2";
            this.labelQ2.Size = new System.Drawing.Size(113, 13);
            this.labelQ2.TabIndex = 2;
            this.labelQ2.Text = "Расcтояние B-C (Q2):";
            // 
            // labelQ1
            // 
            this.labelQ1.AutoSize = true;
            this.labelQ1.Location = new System.Drawing.Point(237, 16);
            this.labelQ1.Name = "labelQ1";
            this.labelQ1.Size = new System.Drawing.Size(113, 13);
            this.labelQ1.TabIndex = 2;
            this.labelQ1.Text = "Расcтояние A-B (Q1):";
            // 
            // radioButtonDistanceAngle
            // 
            this.radioButtonDistanceAngle.AutoSize = true;
            this.radioButtonDistanceAngle.Location = new System.Drawing.Point(6, 70);
            this.radioButtonDistanceAngle.Name = "radioButtonDistanceAngle";
            this.radioButtonDistanceAngle.Size = new System.Drawing.Size(156, 17);
            this.radioButtonDistanceAngle.TabIndex = 0;
            this.radioButtonDistanceAngle.Text = "Растояние AB – Угол ABC";
            this.radioButtonDistanceAngle.UseVisualStyleBackColor = true;
            this.radioButtonDistanceAngle.CheckedChanged += new System.EventHandler(this.radioButtonDistanceAngle_CheckedChanged);
            // 
            // radioButtonDistanceDistance
            // 
            this.radioButtonDistanceDistance.AutoSize = true;
            this.radioButtonDistanceDistance.Location = new System.Drawing.Point(6, 33);
            this.radioButtonDistanceDistance.Name = "radioButtonDistanceDistance";
            this.radioButtonDistanceDistance.Size = new System.Drawing.Size(178, 17);
            this.radioButtonDistanceDistance.TabIndex = 0;
            this.radioButtonDistanceDistance.Text = "Растояние AB – Растояние BC";
            this.radioButtonDistanceDistance.UseVisualStyleBackColor = true;
            this.radioButtonDistanceDistance.CheckedChanged += new System.EventHandler(this.radioButtonDistanceDistance_CheckedChanged);
            // 
            // CoordinateParamsLEPS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "CoordinateParamsLEPS";
            this.Size = new System.Drawing.Size(610, 185);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.RadioButton radioButtonDistanceAngle;
        private System.Windows.Forms.RadioButton radioButtonDistanceDistance;
        private System.Windows.Forms.Label labelQ2;
        private System.Windows.Forms.Label labelQ1;
        private System.Windows.Forms.Label labelQConst;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Core.PhysicalValueEditor physicalValueEditorQ2Max;
        private Core.PhysicalValueEditor physicalValueEditorQ2Min;
        private Core.PhysicalValueEditor physicalValueEditorQ1Max;
        private Core.PhysicalValueEditor physicalValueEditorQ1Min;
        private Core.PhysicalValueEditor physicalValueEditorConstValue;
    }
}
