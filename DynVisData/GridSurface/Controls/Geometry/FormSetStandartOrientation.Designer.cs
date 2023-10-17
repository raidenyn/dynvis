namespace DynVis.Data.GridSurface
{
    partial class FormSetStandartOrientation
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
            this.comboBoxCenter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxDirectZ = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxPlaneXZ = new System.Windows.Forms.ComboBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonОК = new System.Windows.Forms.Button();
            this.geometryDraw = new DynVis.Geometry.Draw.GeometryDraw();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Установить центр на атом:";
            // 
            // comboBoxCenter
            // 
            this.comboBoxCenter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCenter.FormattingEnabled = true;
            this.comboBoxCenter.Location = new System.Drawing.Point(177, 19);
            this.comboBoxCenter.Name = "comboBoxCenter";
            this.comboBoxCenter.Size = new System.Drawing.Size(224, 21);
            this.comboBoxCenter.TabIndex = 1;
            this.comboBoxCenter.SelectedIndexChanged += new System.EventHandler(this.comboBoxCenter_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(139, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Направить ось Z на атом:";
            // 
            // comboBoxDirectZ
            // 
            this.comboBoxDirectZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDirectZ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDirectZ.FormattingEnabled = true;
            this.comboBoxDirectZ.Location = new System.Drawing.Point(177, 56);
            this.comboBoxDirectZ.Name = "comboBoxDirectZ";
            this.comboBoxDirectZ.Size = new System.Drawing.Size(224, 21);
            this.comboBoxDirectZ.TabIndex = 1;
            this.comboBoxDirectZ.SelectedIndexChanged += new System.EventHandler(this.comboBoxDirectZ_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(160, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Совместить ось XZ с атомом:";
            // 
            // comboBoxPlaneXZ
            // 
            this.comboBoxPlaneXZ.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxPlaneXZ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPlaneXZ.FormattingEnabled = true;
            this.comboBoxPlaneXZ.Location = new System.Drawing.Point(177, 89);
            this.comboBoxPlaneXZ.Name = "comboBoxPlaneXZ";
            this.comboBoxPlaneXZ.Size = new System.Drawing.Size(224, 21);
            this.comboBoxPlaneXZ.TabIndex = 1;
            this.comboBoxPlaneXZ.SelectedIndexChanged += new System.EventHandler(this.comboBoxPlaneXZ_SelectedIndexChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(326, 493);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonОК
            // 
            this.buttonОК.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonОК.Location = new System.Drawing.Point(245, 493);
            this.buttonОК.Name = "buttonОК";
            this.buttonОК.Size = new System.Drawing.Size(75, 23);
            this.buttonОК.TabIndex = 2;
            this.buttonОК.Text = "ОК";
            this.buttonОК.UseVisualStyleBackColor = true;
            this.buttonОК.Click += new System.EventHandler(this.buttonОК_Click);
            // 
            // geometryDraw
            // 
            this.geometryDraw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.geometryDraw.EnableMenu = false;
            this.geometryDraw.Location = new System.Drawing.Point(12, 140);
            this.geometryDraw.Name = "geometryDraw";
            this.geometryDraw.Size = new System.Drawing.Size(389, 347);
            this.geometryDraw.TabIndex = 3;
            this.geometryDraw.SelectionChanged += new System.EventHandler(this.geometryDraw_SelectionChanged);
            // 
            // FormSetStandartOrientation
            // 
            this.AcceptButton = this.buttonОК;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(413, 523);
            this.Controls.Add(this.geometryDraw);
            this.Controls.Add(this.buttonОК);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.comboBoxPlaneXZ);
            this.Controls.Add(this.comboBoxDirectZ);
            this.Controls.Add(this.comboBoxCenter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetStandartOrientation";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Установка стандартной ориентации";
            this.Load += new System.EventHandler(this.FormSetStandartOrientation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCenter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDirectZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxPlaneXZ;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonОК;
        private DynVis.Geometry.Draw.GeometryDraw geometryDraw;
    }
}