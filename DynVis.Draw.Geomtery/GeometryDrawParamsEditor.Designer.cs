namespace DynVis.Draw.Geometry
{
    partial class GeometryDrawParamsEditor
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
            this.textBoxBondRadius = new System.Windows.Forms.TextBox();
            this.geometryDrawEngineBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.colorButtonBackgroundColor = new DynVis.Core.Controls.ColorButton();
            this.colorButtonSelectedAtomsColor = new DynVis.Core.Controls.ColorButton();
            ((System.ComponentModel.ISupportInitialize)(this.geometryDrawEngineBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxBondRadius
            // 
            this.textBoxBondRadius.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxBondRadius.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.geometryDrawEngineBindingSource, "BondRadius", true));
            this.textBoxBondRadius.Location = new System.Drawing.Point(17, 140);
            this.textBoxBondRadius.Name = "textBoxBondRadius";
            this.textBoxBondRadius.Size = new System.Drawing.Size(356, 20);
            this.textBoxBondRadius.TabIndex = 0;
            // 
            // geometryDrawEngineBindingSource
            // 
            this.geometryDrawEngineBindingSource.DataSource = typeof(DynVis.Draw.Geometry.GeometryDrawEngine);
            this.geometryDrawEngineBindingSource.CurrentItemChanged += new System.EventHandler(this.geometryDrawEngineBindingSource_CurrentItemChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-493, -143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Радиус цилиндра связи:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Цвет фона:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Цвет выделенных атомов:";
            // 
            // colorButtonBackgroundColor
            // 
            this.colorButtonBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorButtonBackgroundColor.BackColor = System.Drawing.SystemColors.Control;
            this.colorButtonBackgroundColor.Color = System.Drawing.SystemColors.Control;
            this.colorButtonBackgroundColor.DataBindings.Add(new System.Windows.Forms.Binding("Color", this.geometryDrawEngineBindingSource, "BackgroundColor", true));
            this.colorButtonBackgroundColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorButtonBackgroundColor.Location = new System.Drawing.Point(17, 33);
            this.colorButtonBackgroundColor.Name = "colorButtonBackgroundColor";
            this.colorButtonBackgroundColor.Size = new System.Drawing.Size(356, 23);
            this.colorButtonBackgroundColor.TabIndex = 4;
            this.colorButtonBackgroundColor.UseVisualStyleBackColor = true;
            // 
            // colorButtonSelectedAtomsColor
            // 
            this.colorButtonSelectedAtomsColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorButtonSelectedAtomsColor.BackColor = System.Drawing.SystemColors.Control;
            this.colorButtonSelectedAtomsColor.Color = System.Drawing.SystemColors.Control;
            this.colorButtonSelectedAtomsColor.DataBindings.Add(new System.Windows.Forms.Binding("Color", this.geometryDrawEngineBindingSource, "SelectedAtomsColor", true));
            this.colorButtonSelectedAtomsColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorButtonSelectedAtomsColor.Location = new System.Drawing.Point(17, 86);
            this.colorButtonSelectedAtomsColor.Name = "colorButtonSelectedAtomsColor";
            this.colorButtonSelectedAtomsColor.Size = new System.Drawing.Size(356, 23);
            this.colorButtonSelectedAtomsColor.TabIndex = 4;
            this.colorButtonSelectedAtomsColor.UseVisualStyleBackColor = true;
            // 
            // GeometryDrawParamsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.colorButtonSelectedAtomsColor);
            this.Controls.Add(this.colorButtonBackgroundColor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxBondRadius);
            this.Name = "GeometryDrawParamsEditor";
            this.Size = new System.Drawing.Size(389, 187);
            this.EditingObjectChanged += new System.EventHandler(this.GeometryDrawParamsEditor_EditingObjectChanged);
            this.AcceptChanges += new System.EventHandler(this.GeometryDrawParamsEditor_AcceptChanges);
            this.RollbackChanges += new System.EventHandler(this.GeometryDrawParamsEditor_RollbackChanges);
            ((System.ComponentModel.ISupportInitialize)(this.geometryDrawEngineBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxBondRadius;
        private System.Windows.Forms.BindingSource geometryDrawEngineBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private DynVis.Core.Controls.ColorButton colorButtonBackgroundColor;
        private DynVis.Core.Controls.ColorButton colorButtonSelectedAtomsColor;

    }
}
