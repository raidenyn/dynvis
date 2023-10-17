namespace DynVis.Data.GridSurface
{
    partial class ElementListEditor
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
            this.gridElements = new SourceGrid.Grid();
            this.contextMenuStripElementsGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemPastFromText = new System.Windows.Forms.ToolStripMenuItem();
            this.labelAtomCount = new System.Windows.Forms.Label();
            this.numericUpDownAtomCount = new System.Windows.Forms.NumericUpDown();
            this.contextMenuStripElementsGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomCount)).BeginInit();
            this.SuspendLayout();
            // 
            // gridElements
            // 
            this.gridElements.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gridElements.AutoStretchColumnsToFitWidth = true;
            this.gridElements.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridElements.ClipboardMode = ((SourceGrid.ClipboardMode)((((SourceGrid.ClipboardMode.Copy | SourceGrid.ClipboardMode.Cut)
                        | SourceGrid.ClipboardMode.Paste)
                        | SourceGrid.ClipboardMode.Delete)));
            this.gridElements.ContextMenuStrip = this.contextMenuStripElementsGrid;
            this.gridElements.FixedColumns = 1;
            this.gridElements.FixedRows = 1;
            this.gridElements.Location = new System.Drawing.Point(3, 3);
            this.gridElements.Name = "gridElements";
            this.gridElements.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridElements.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.gridElements.Size = new System.Drawing.Size(227, 608);
            this.gridElements.TabIndex = 0;
            this.gridElements.TabStop = true;
            this.gridElements.ToolTipText = "";
            // 
            // contextMenuStripElementsGrid
            // 
            this.contextMenuStripElementsGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemPastFromText});
            this.contextMenuStripElementsGrid.Name = "contextMenuStripElementsGrid";
            this.contextMenuStripElementsGrid.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStripElementsGrid.ShowImageMargin = false;
            this.contextMenuStripElementsGrid.Size = new System.Drawing.Size(150, 26);
            // 
            // toolStripMenuItemPastFromText
            // 
            this.toolStripMenuItemPastFromText.Name = "toolStripMenuItemPastFromText";
            this.toolStripMenuItemPastFromText.Size = new System.Drawing.Size(149, 22);
            this.toolStripMenuItemPastFromText.Text = "Вставить из текста";
            this.toolStripMenuItemPastFromText.Click += new System.EventHandler(this.toolStripMenuItemPastFromText_Click);
            // 
            // labelAtomCount
            // 
            this.labelAtomCount.AutoSize = true;
            this.labelAtomCount.Location = new System.Drawing.Point(236, 13);
            this.labelAtomCount.Name = "labelAtomCount";
            this.labelAtomCount.Size = new System.Drawing.Size(109, 13);
            this.labelAtomCount.TabIndex = 1;
            this.labelAtomCount.Text = "Количество атомов:";
            // 
            // numericUpDownAtomCount
            // 
            this.numericUpDownAtomCount.Location = new System.Drawing.Point(351, 11);
            this.numericUpDownAtomCount.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownAtomCount.Name = "numericUpDownAtomCount";
            this.numericUpDownAtomCount.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownAtomCount.TabIndex = 2;
            this.numericUpDownAtomCount.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDownAtomCount.ValueChanged += new System.EventHandler(this.numericUpDownAtomCount_ValueChanged);
            // 
            // ElementListEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.numericUpDownAtomCount);
            this.Controls.Add(this.labelAtomCount);
            this.Controls.Add(this.gridElements);
            this.Name = "ElementListEditor";
            this.Size = new System.Drawing.Size(497, 614);
            this.contextMenuStripElementsGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAtomCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SourceGrid.Grid gridElements;
        private System.Windows.Forms.Label labelAtomCount;
        private System.Windows.Forms.NumericUpDown numericUpDownAtomCount;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripElementsGrid;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPastFromText;
    }
}
