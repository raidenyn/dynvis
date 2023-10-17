using DynVis.Core.Surface;

namespace DynVis.Data.GridSurface
{
    partial class CoordinateEditor
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
            this.scaleDimensionSelector = new DynVis.Core.Dimmension.ScaleDimensionSelector();
            this.groupBoxMax = new System.Windows.Forms.GroupBox();
            this.groupBoxMin = new System.Windows.Forms.GroupBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.gridCoordValue = new SourceGrid.Grid();
            this.numericUpDownCount = new System.Windows.Forms.NumericUpDown();
            this.labelDim = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelCount = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.создатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemRegularGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.coordTypeEditorMaxType = new DynVis.Data.GridSurface.CoordTypeEditor();
            this.coordTypeEditorMinType = new DynVis.Data.GridSurface.CoordTypeEditor();
            this.groupBox.SuspendLayout();
            this.groupBoxMax.SuspendLayout();
            this.groupBoxMin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCount)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.scaleDimensionSelector);
            this.groupBox.Controls.Add(this.groupBoxMax);
            this.groupBox.Controls.Add(this.groupBoxMin);
            this.groupBox.Controls.Add(this.textBoxName);
            this.groupBox.Controls.Add(this.gridCoordValue);
            this.groupBox.Controls.Add(this.numericUpDownCount);
            this.groupBox.Controls.Add(this.labelDim);
            this.groupBox.Controls.Add(this.labelName);
            this.groupBox.Controls.Add(this.labelCount);
            this.groupBox.Controls.Add(this.menuStrip1);
            this.groupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox.Location = new System.Drawing.Point(0, 0);
            this.groupBox.MinimumSize = new System.Drawing.Size(416, 311);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(416, 311);
            this.groupBox.TabIndex = 5;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Координата";
            this.groupBox.Enter += new System.EventHandler(this.groupBox_Enter);
            // 
            // scaleDimensionSelector
            // 
            this.scaleDimensionSelector.Location = new System.Drawing.Point(291, 69);
            this.scaleDimensionSelector.Name = "scaleDimensionSelector";
            this.scaleDimensionSelector.Size = new System.Drawing.Size(155, 24);
            this.scaleDimensionSelector.TabIndex = 8;
            this.scaleDimensionSelector.ScaleDimensionChanged += new System.EventHandler(this.scaleDimensionSelector_ScaleDimensionChanged);
            // 
            // groupBoxMax
            // 
            this.groupBoxMax.Controls.Add(this.coordTypeEditorMaxType);
            this.groupBoxMax.Location = new System.Drawing.Point(283, 155);
            this.groupBoxMax.Name = "groupBoxMax";
            this.groupBoxMax.Size = new System.Drawing.Size(114, 146);
            this.groupBoxMax.TabIndex = 6;
            this.groupBoxMax.TabStop = false;
            this.groupBoxMax.Text = "Верхнее значение";
            // 
            // groupBoxMin
            // 
            this.groupBoxMin.Controls.Add(this.coordTypeEditorMinType);
            this.groupBoxMin.Location = new System.Drawing.Point(166, 155);
            this.groupBoxMin.Name = "groupBoxMin";
            this.groupBoxMin.Size = new System.Drawing.Size(111, 146);
            this.groupBoxMin.TabIndex = 5;
            this.groupBoxMin.TabStop = false;
            this.groupBoxMin.Text = "Нижнее значение";
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(170, 129);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(240, 20);
            this.textBoxName.TabIndex = 4;
            this.textBoxName.Text = "Координата";
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // gridCoordValue
            // 
            this.gridCoordValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gridCoordValue.AutoStretchColumnsToFitWidth = true;
            this.gridCoordValue.ClipboardMode = ((SourceGrid.ClipboardMode)((((SourceGrid.ClipboardMode.Copy | SourceGrid.ClipboardMode.Cut)
                        | SourceGrid.ClipboardMode.Paste)
                        | SourceGrid.ClipboardMode.Delete)));
            this.gridCoordValue.Location = new System.Drawing.Point(12, 47);
            this.gridCoordValue.Name = "gridCoordValue";
            this.gridCoordValue.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridCoordValue.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.gridCoordValue.Size = new System.Drawing.Size(145, 258);
            this.gridCoordValue.TabIndex = 0;
            this.gridCoordValue.TabStop = true;
            this.gridCoordValue.ToolTipText = "";
            // 
            // numericUpDownCount
            // 
            this.numericUpDownCount.Location = new System.Drawing.Point(170, 69);
            this.numericUpDownCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownCount.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownCount.Name = "numericUpDownCount";
            this.numericUpDownCount.Size = new System.Drawing.Size(107, 20);
            this.numericUpDownCount.TabIndex = 2;
            this.numericUpDownCount.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownCount.ValueChanged += new System.EventHandler(this.numericUpDownCount_ValueChanged);
            // 
            // labelDim
            // 
            this.labelDim.AutoSize = true;
            this.labelDim.Location = new System.Drawing.Point(288, 50);
            this.labelDim.Name = "labelDim";
            this.labelDim.Size = new System.Drawing.Size(78, 13);
            this.labelDim.TabIndex = 3;
            this.labelDim.Text = "Размерность:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(167, 113);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(60, 13);
            this.labelName.TabIndex = 3;
            this.labelName.Text = "Название:";
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(167, 50);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(100, 13);
            this.labelCount.TabIndex = 3;
            this.labelCount.Text = "Количество точек:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создатьToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(3, 16);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(410, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // создатьToolStripMenuItem
            // 
            this.создатьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemRegularGrid});
            this.создатьToolStripMenuItem.Name = "создатьToolStripMenuItem";
            this.создатьToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.создатьToolStripMenuItem.Text = "Создать";
            // 
            // ToolStripMenuItemRegularGrid
            // 
            this.ToolStripMenuItemRegularGrid.Name = "ToolStripMenuItemRegularGrid";
            this.ToolStripMenuItemRegularGrid.Size = new System.Drawing.Size(169, 22);
            this.ToolStripMenuItemRegularGrid.Text = "Регулярная сетка";
            this.ToolStripMenuItemRegularGrid.Click += new System.EventHandler(this.ToolStripMenuItemRegularGrid_Click);
            // 
            // coordTypeEditorMaxType
            // 
            this.coordTypeEditorMaxType.CoordinateType = DynVis.Core.Surface.CoordinateType.Ended;
            this.coordTypeEditorMaxType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coordTypeEditorMaxType.Location = new System.Drawing.Point(3, 16);
            this.coordTypeEditorMaxType.Name = "coordTypeEditorMaxType";
            this.coordTypeEditorMaxType.Size = new System.Drawing.Size(108, 127);
            this.coordTypeEditorMaxType.TabIndex = 0;
            this.coordTypeEditorMaxType.CoordTypeChanged += new System.EventHandler(this.coordTypeEditorMaxType_CoordTypeChanged);
            // 
            // coordTypeEditorMinType
            // 
            this.coordTypeEditorMinType.CoordinateType = DynVis.Core.Surface.CoordinateType.Ended;
            this.coordTypeEditorMinType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coordTypeEditorMinType.Location = new System.Drawing.Point(3, 16);
            this.coordTypeEditorMinType.Name = "coordTypeEditorMinType";
            this.coordTypeEditorMinType.Size = new System.Drawing.Size(105, 127);
            this.coordTypeEditorMinType.TabIndex = 0;
            this.coordTypeEditorMinType.CoordTypeChanged += new System.EventHandler(this.coordTypeEditorMinType_CoordTypeChanged);
            // 
            // CoordinateEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "CoordinateEditor";
            this.Size = new System.Drawing.Size(416, 311);
            this.Load += new System.EventHandler(this.CoordinateEditor_Load);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.groupBoxMax.ResumeLayout(false);
            this.groupBoxMin.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownCount)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.TextBox textBoxName;
        private SourceGrid.Grid gridCoordValue;
        private System.Windows.Forms.NumericUpDown numericUpDownCount;
        private System.Windows.Forms.Label labelDim;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.GroupBox groupBoxMax;
        private System.Windows.Forms.GroupBox groupBoxMin;
        private CoordTypeEditor coordTypeEditorMaxType;
        private CoordTypeEditor coordTypeEditorMinType;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem создатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemRegularGrid;
        private Core.Dimmension.ScaleDimensionSelector scaleDimensionSelector;
    }
}
