namespace DynVis.Core.CurrentPointInformation
{
    partial class CurrentPointInformation
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
            this.labelQ1 = new System.Windows.Forms.Label();
            this.labelQ2 = new System.Windows.Forms.Label();
            this.labelEnergy = new System.Windows.Forms.Label();
            this.labelQ1Der = new System.Windows.Forms.Label();
            this.labelQ2Der = new System.Windows.Forms.Label();
            this.labelQ1Der2 = new System.Windows.Forms.Label();
            this.labelQ2Der2 = new System.Windows.Forms.Label();
            this.labelQ1Q2Der2 = new System.Windows.Forms.Label();
            this.textBoxQ1dev = new System.Windows.Forms.TextBox();
            this.textBoxQ2Dev = new System.Windows.Forms.TextBox();
            this.textBoxQ1Dev2 = new System.Windows.Forms.TextBox();
            this.textBoxQ2Dev2 = new System.Windows.Forms.TextBox();
            this.textBoxQ1Q2Dev2 = new System.Windows.Forms.TextBox();
            this.physicalValueEditor2 = new DynVis.Core.PhysicalValueEditor();
            this.physicalValueEditor1 = new DynVis.Core.PhysicalValueEditor();
            this.physicalValueEditorEnergy = new DynVis.Core.PhysicalValueEditor();
            this.buttonAdditionInformation = new System.Windows.Forms.Button();
            this.groupBoxEigensystem = new System.Windows.Forms.GroupBox();
            this.textBoxE1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxE1V1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxE1V2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxE2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxE2V1 = new System.Windows.Forms.TextBox();
            this.textBoxE2V2 = new System.Windows.Forms.TextBox();
            this.groupBoxEigensystem.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelQ1
            // 
            this.labelQ1.AutoSize = true;
            this.labelQ1.Location = new System.Drawing.Point(3, 10);
            this.labelQ1.Name = "labelQ1";
            this.labelQ1.Size = new System.Drawing.Size(113, 13);
            this.labelQ1.TabIndex = 1;
            this.labelQ1.Text = "Первая координата: ";
            // 
            // labelQ2
            // 
            this.labelQ2.AutoSize = true;
            this.labelQ2.Location = new System.Drawing.Point(3, 39);
            this.labelQ2.Name = "labelQ2";
            this.labelQ2.Size = new System.Drawing.Size(111, 13);
            this.labelQ2.TabIndex = 1;
            this.labelQ2.Text = "Вторая координата: ";
            // 
            // labelEnergy
            // 
            this.labelEnergy.AutoSize = true;
            this.labelEnergy.Location = new System.Drawing.Point(3, 83);
            this.labelEnergy.Name = "labelEnergy";
            this.labelEnergy.Size = new System.Drawing.Size(55, 13);
            this.labelEnergy.TabIndex = 1;
            this.labelEnergy.Text = "Энергия: ";
            // 
            // labelQ1Der
            // 
            this.labelQ1Der.AutoSize = true;
            this.labelQ1Der.Location = new System.Drawing.Point(3, 109);
            this.labelQ1Der.Name = "labelQ1Der";
            this.labelQ1Der.Size = new System.Drawing.Size(46, 13);
            this.labelQ1Der.TabIndex = 1;
            this.labelQ1Der.Text = "dEdQ1: ";
            // 
            // labelQ2Der
            // 
            this.labelQ2Der.AutoSize = true;
            this.labelQ2Der.Location = new System.Drawing.Point(3, 135);
            this.labelQ2Der.Name = "labelQ2Der";
            this.labelQ2Der.Size = new System.Drawing.Size(43, 13);
            this.labelQ2Der.TabIndex = 1;
            this.labelQ2Der.Text = "dEdQ2:";
            // 
            // labelQ1Der2
            // 
            this.labelQ1Der2.AutoSize = true;
            this.labelQ1Der2.Location = new System.Drawing.Point(3, 161);
            this.labelQ1Der2.Name = "labelQ1Der2";
            this.labelQ1Der2.Size = new System.Drawing.Size(69, 13);
            this.labelQ1Der2.TabIndex = 1;
            this.labelQ1Der2.Text = "d2EdQ1dQ1:";
            // 
            // labelQ2Der2
            // 
            this.labelQ2Der2.AutoSize = true;
            this.labelQ2Der2.Location = new System.Drawing.Point(3, 187);
            this.labelQ2Der2.Name = "labelQ2Der2";
            this.labelQ2Der2.Size = new System.Drawing.Size(69, 13);
            this.labelQ2Der2.TabIndex = 1;
            this.labelQ2Der2.Text = "d2EdQ2dQ2:";
            // 
            // labelQ1Q2Der2
            // 
            this.labelQ1Q2Der2.AutoSize = true;
            this.labelQ1Q2Der2.Location = new System.Drawing.Point(3, 213);
            this.labelQ1Q2Der2.Name = "labelQ1Q2Der2";
            this.labelQ1Q2Der2.Size = new System.Drawing.Size(69, 13);
            this.labelQ1Q2Der2.TabIndex = 1;
            this.labelQ1Q2Der2.Text = "d2EdQ1dQ2:";
            // 
            // textBoxQ1dev
            // 
            this.textBoxQ1dev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQ1dev.Location = new System.Drawing.Point(116, 106);
            this.textBoxQ1dev.Name = "textBoxQ1dev";
            this.textBoxQ1dev.ReadOnly = true;
            this.textBoxQ1dev.Size = new System.Drawing.Size(122, 20);
            this.textBoxQ1dev.TabIndex = 2;
            // 
            // textBoxQ2Dev
            // 
            this.textBoxQ2Dev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQ2Dev.Location = new System.Drawing.Point(116, 132);
            this.textBoxQ2Dev.Name = "textBoxQ2Dev";
            this.textBoxQ2Dev.ReadOnly = true;
            this.textBoxQ2Dev.Size = new System.Drawing.Size(122, 20);
            this.textBoxQ2Dev.TabIndex = 2;
            // 
            // textBoxQ1Dev2
            // 
            this.textBoxQ1Dev2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQ1Dev2.Location = new System.Drawing.Point(116, 158);
            this.textBoxQ1Dev2.Name = "textBoxQ1Dev2";
            this.textBoxQ1Dev2.ReadOnly = true;
            this.textBoxQ1Dev2.Size = new System.Drawing.Size(122, 20);
            this.textBoxQ1Dev2.TabIndex = 2;
            // 
            // textBoxQ2Dev2
            // 
            this.textBoxQ2Dev2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQ2Dev2.Location = new System.Drawing.Point(116, 184);
            this.textBoxQ2Dev2.Name = "textBoxQ2Dev2";
            this.textBoxQ2Dev2.ReadOnly = true;
            this.textBoxQ2Dev2.Size = new System.Drawing.Size(122, 20);
            this.textBoxQ2Dev2.TabIndex = 2;
            // 
            // textBoxQ1Q2Dev2
            // 
            this.textBoxQ1Q2Dev2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxQ1Q2Dev2.Location = new System.Drawing.Point(116, 210);
            this.textBoxQ1Q2Dev2.Name = "textBoxQ1Q2Dev2";
            this.textBoxQ1Q2Dev2.ReadOnly = true;
            this.textBoxQ1Q2Dev2.Size = new System.Drawing.Size(122, 20);
            this.textBoxQ1Q2Dev2.TabIndex = 2;
            // 
            // physicalValueEditor2
            // 
            this.physicalValueEditor2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.physicalValueEditor2.Location = new System.Drawing.Point(116, 36);
            this.physicalValueEditor2.Name = "physicalValueEditor2";
            this.physicalValueEditor2.Size = new System.Drawing.Size(179, 20);
            this.physicalValueEditor2.TabIndex = 0;
            this.physicalValueEditor2.Value = 0;
            this.physicalValueEditor2.ValueChanged += new System.EventHandler(this.physicalValueEditor_ValueChanged);
            // 
            // physicalValueEditor1
            // 
            this.physicalValueEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.physicalValueEditor1.Location = new System.Drawing.Point(116, 7);
            this.physicalValueEditor1.Name = "physicalValueEditor1";
            this.physicalValueEditor1.Size = new System.Drawing.Size(179, 20);
            this.physicalValueEditor1.TabIndex = 0;
            this.physicalValueEditor1.Value = 0;
            this.physicalValueEditor1.ValueChanged += new System.EventHandler(this.physicalValueEditor_ValueChanged);
            // 
            // physicalValueEditorEnergy
            // 
            this.physicalValueEditorEnergy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.physicalValueEditorEnergy.DimensionType = DynVis.Core.DimensionType.Energy;
            this.physicalValueEditorEnergy.Location = new System.Drawing.Point(116, 80);
            this.physicalValueEditorEnergy.Name = "physicalValueEditorEnergy";
            this.physicalValueEditorEnergy.ReadOnlyValue = true;
            this.physicalValueEditorEnergy.Size = new System.Drawing.Size(179, 20);
            this.physicalValueEditorEnergy.TabIndex = 0;
            this.physicalValueEditorEnergy.Value = 0;
            // 
            // buttonAdditionInformation
            // 
            this.buttonAdditionInformation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdditionInformation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdditionInformation.Location = new System.Drawing.Point(273, 362);
            this.buttonAdditionInformation.Name = "buttonAdditionInformation";
            this.buttonAdditionInformation.Size = new System.Drawing.Size(22, 23);
            this.buttonAdditionInformation.TabIndex = 3;
            this.buttonAdditionInformation.Text = "v";
            this.buttonAdditionInformation.UseVisualStyleBackColor = true;
            this.buttonAdditionInformation.Click += new System.EventHandler(this.buttonAdditionInformation_Click);
            // 
            // groupBoxEigensystem
            // 
            this.groupBoxEigensystem.Controls.Add(this.textBoxE2V2);
            this.groupBoxEigensystem.Controls.Add(this.textBoxE2V1);
            this.groupBoxEigensystem.Controls.Add(this.textBoxE1V2);
            this.groupBoxEigensystem.Controls.Add(this.label6);
            this.groupBoxEigensystem.Controls.Add(this.textBoxE1V1);
            this.groupBoxEigensystem.Controls.Add(this.textBoxE2);
            this.groupBoxEigensystem.Controls.Add(this.label3);
            this.groupBoxEigensystem.Controls.Add(this.label5);
            this.groupBoxEigensystem.Controls.Add(this.textBoxE1);
            this.groupBoxEigensystem.Controls.Add(this.label4);
            this.groupBoxEigensystem.Controls.Add(this.label2);
            this.groupBoxEigensystem.Controls.Add(this.label1);
            this.groupBoxEigensystem.Location = new System.Drawing.Point(6, 250);
            this.groupBoxEigensystem.Name = "groupBoxEigensystem";
            this.groupBoxEigensystem.Size = new System.Drawing.Size(260, 137);
            this.groupBoxEigensystem.TabIndex = 4;
            this.groupBoxEigensystem.TabStop = false;
            this.groupBoxEigensystem.Text = "Собственые значения Гессиана";
            // 
            // textBoxE1
            // 
            this.textBoxE1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxE1.Location = new System.Drawing.Point(49, 24);
            this.textBoxE1.Name = "textBoxE1";
            this.textBoxE1.ReadOnly = true;
            this.textBoxE1.Size = new System.Drawing.Size(203, 20);
            this.textBoxE1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "EVal1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "EVec1:";
            // 
            // textBoxE1V1
            // 
            this.textBoxE1V1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxE1V1.Location = new System.Drawing.Point(49, 53);
            this.textBoxE1V1.Name = "textBoxE1V1";
            this.textBoxE1V1.ReadOnly = true;
            this.textBoxE1V1.Size = new System.Drawing.Size(80, 20);
            this.textBoxE1V1.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(130, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "EVec2:";
            // 
            // textBoxE1V2
            // 
            this.textBoxE1V2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxE1V2.Location = new System.Drawing.Point(172, 53);
            this.textBoxE1V2.Name = "textBoxE1V2";
            this.textBoxE1V2.ReadOnly = true;
            this.textBoxE1V2.Size = new System.Drawing.Size(80, 20);
            this.textBoxE1V2.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "EVal1:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "EVec1:";
            // 
            // textBoxE2
            // 
            this.textBoxE2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxE2.Location = new System.Drawing.Point(49, 82);
            this.textBoxE2.Name = "textBoxE2";
            this.textBoxE2.ReadOnly = true;
            this.textBoxE2.Size = new System.Drawing.Size(203, 20);
            this.textBoxE2.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(130, 114);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "EVec2:";
            // 
            // textBoxE2V1
            // 
            this.textBoxE2V1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxE2V1.Location = new System.Drawing.Point(49, 111);
            this.textBoxE2V1.Name = "textBoxE2V1";
            this.textBoxE2V1.ReadOnly = true;
            this.textBoxE2V1.Size = new System.Drawing.Size(80, 20);
            this.textBoxE2V1.TabIndex = 2;
            // 
            // textBoxE2V2
            // 
            this.textBoxE2V2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxE2V2.Location = new System.Drawing.Point(172, 111);
            this.textBoxE2V2.Name = "textBoxE2V2";
            this.textBoxE2V2.ReadOnly = true;
            this.textBoxE2V2.Size = new System.Drawing.Size(80, 20);
            this.textBoxE2V2.TabIndex = 2;
            // 
            // CurrentPointInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxEigensystem);
            this.Controls.Add(this.buttonAdditionInformation);
            this.Controls.Add(this.textBoxQ1Q2Dev2);
            this.Controls.Add(this.textBoxQ2Dev2);
            this.Controls.Add(this.textBoxQ1Dev2);
            this.Controls.Add(this.textBoxQ2Dev);
            this.Controls.Add(this.textBoxQ1dev);
            this.Controls.Add(this.labelQ1Q2Der2);
            this.Controls.Add(this.labelQ2Der2);
            this.Controls.Add(this.labelQ1Der2);
            this.Controls.Add(this.labelQ2Der);
            this.Controls.Add(this.labelQ1Der);
            this.Controls.Add(this.labelEnergy);
            this.Controls.Add(this.labelQ2);
            this.Controls.Add(this.labelQ1);
            this.Controls.Add(this.physicalValueEditor2);
            this.Controls.Add(this.physicalValueEditor1);
            this.Controls.Add(this.physicalValueEditorEnergy);
            this.Name = "CurrentPointInformation";
            this.Size = new System.Drawing.Size(301, 396);
            this.groupBoxEigensystem.ResumeLayout(false);
            this.groupBoxEigensystem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PhysicalValueEditor physicalValueEditorEnergy;
        private PhysicalValueEditor physicalValueEditor1;
        private System.Windows.Forms.Label labelQ1;
        private System.Windows.Forms.Label labelQ2;
        private PhysicalValueEditor physicalValueEditor2;
        private System.Windows.Forms.Label labelEnergy;
        private System.Windows.Forms.Label labelQ1Der;
        private System.Windows.Forms.Label labelQ2Der;
        private System.Windows.Forms.Label labelQ1Der2;
        private System.Windows.Forms.Label labelQ2Der2;
        private System.Windows.Forms.Label labelQ1Q2Der2;
        private System.Windows.Forms.TextBox textBoxQ1dev;
        private System.Windows.Forms.TextBox textBoxQ2Dev;
        private System.Windows.Forms.TextBox textBoxQ1Dev2;
        private System.Windows.Forms.TextBox textBoxQ2Dev2;
        private System.Windows.Forms.TextBox textBoxQ1Q2Dev2;
        private System.Windows.Forms.Button buttonAdditionInformation;
        private System.Windows.Forms.GroupBox groupBoxEigensystem;
        private System.Windows.Forms.TextBox textBoxE1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxE1V2;
        private System.Windows.Forms.TextBox textBoxE1V1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxE2V2;
        private System.Windows.Forms.TextBox textBoxE2V1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxE2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}
