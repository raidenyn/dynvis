namespace DynVis.Draw.Surface
{
    partial class SurfacePropertyEditor2D
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
            this.drawSurfaceEngineGDI2DBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colorButtonBackgroundColor = new DynVis.Core.Controls.ColorButton();
            this.penEditorPathLine = new DynVis.Core.Controls.PenEditor();
            this.fontEditorAxes = new DynVis.Core.Controls.FontEditor();
            this.fontEditor1 = new DynVis.Core.Controls.FontEditor();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.drawSurfaceEngineGDI2DBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Цвет фона:";
            // 
            // drawSurfaceEngineGDI2DBindingSource
            // 
            this.drawSurfaceEngineGDI2DBindingSource.DataSource = typeof(DynVis.Draw.Surface.DrawSurfaceEngineGDI_2D);
            this.drawSurfaceEngineGDI2DBindingSource.CurrentItemChanged += new System.EventHandler(this.drawSurfaceEngineGDI2DBindingSource_CurrentItemChanged);
            // 
            // colorButtonBackgroundColor
            // 
            this.colorButtonBackgroundColor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorButtonBackgroundColor.BackColor = System.Drawing.SystemColors.Control;
            this.colorButtonBackgroundColor.Color = System.Drawing.SystemColors.Control;
            this.colorButtonBackgroundColor.DataBindings.Add(new System.Windows.Forms.Binding("Color", this.drawSurfaceEngineGDI2DBindingSource, "BackgrounColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.colorButtonBackgroundColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorButtonBackgroundColor.Location = new System.Drawing.Point(16, 33);
            this.colorButtonBackgroundColor.Name = "colorButtonBackgroundColor";
            this.colorButtonBackgroundColor.Size = new System.Drawing.Size(342, 23);
            this.colorButtonBackgroundColor.TabIndex = 1;
            this.colorButtonBackgroundColor.UseVisualStyleBackColor = true;
            // 
            // penEditorPathLine
            // 
            this.penEditorPathLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.penEditorPathLine.DataBindings.Add(new System.Windows.Forms.Binding("Pen", this.drawSurfaceEngineGDI2DBindingSource, "PathPen", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.penEditorPathLine.Location = new System.Drawing.Point(16, 74);
            this.penEditorPathLine.Name = "penEditorPathLine";
            this.penEditorPathLine.Size = new System.Drawing.Size(342, 79);
            this.penEditorPathLine.TabIndex = 2;
            this.penEditorPathLine.Tittle = "Линия пути перемещения";
            // 
            // fontEditorAxes
            // 
            this.fontEditorAxes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fontEditorAxes.DataBindings.Add(new System.Windows.Forms.Binding("EditableBrush", this.drawSurfaceEngineGDI2DBindingSource, "AxesFontBrush", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.fontEditorAxes.DataBindings.Add(new System.Windows.Forms.Binding("EditableFont", this.drawSurfaceEngineGDI2DBindingSource, "AxesFont", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.fontEditorAxes.Location = new System.Drawing.Point(16, 184);
            this.fontEditorAxes.Name = "fontEditorAxes";
            this.fontEditorAxes.Size = new System.Drawing.Size(342, 88);
            this.fontEditorAxes.TabIndex = 3;
            this.fontEditorAxes.Tittle = "Значения координат";
            // 
            // fontEditor1
            // 
            this.fontEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fontEditor1.DataBindings.Add(new System.Windows.Forms.Binding("EditableBrush", this.drawSurfaceEngineGDI2DBindingSource, "AxesNoteFontBrush", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.fontEditor1.DataBindings.Add(new System.Windows.Forms.Binding("EditableFont", this.drawSurfaceEngineGDI2DBindingSource, "AxesNoteFont", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.fontEditor1.Location = new System.Drawing.Point(16, 289);
            this.fontEditor1.Name = "fontEditor1";
            this.fontEditor1.Size = new System.Drawing.Size(342, 88);
            this.fontEditor1.TabIndex = 3;
            this.fontEditor1.Tittle = "Подписи к осям";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.drawSurfaceEngineGDI2DBindingSource, "DrawGridLines", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.checkBox1.Location = new System.Drawing.Point(16, 161);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(251, 17);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.Text = "Отображать сетку значений на поверхности";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // SurfacePropertyEditor2D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.fontEditor1);
            this.Controls.Add(this.fontEditorAxes);
            this.Controls.Add(this.penEditorPathLine);
            this.Controls.Add(this.colorButtonBackgroundColor);
            this.Controls.Add(this.label1);
            this.Name = "SurfacePropertyEditor2D";
            this.Size = new System.Drawing.Size(374, 386);
            this.EditingObjectChanged += new System.EventHandler(this.SurfacePropertyEditor_EditingObjectChanged);
            ((System.ComponentModel.ISupportInitialize)(this.drawSurfaceEngineGDI2DBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource drawSurfaceEngineGDI2DBindingSource;
        private DynVis.Core.Controls.ColorButton colorButtonBackgroundColor;
        private DynVis.Core.Controls.PenEditor penEditorPathLine;
        private DynVis.Core.Controls.FontEditor fontEditorAxes;
        private DynVis.Core.Controls.FontEditor fontEditor1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}
