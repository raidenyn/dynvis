namespace DynVis.Data.GridSurface
{
    partial class GeometryEditor
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
            this.gridStructure = new SourceGrid.Grid();
            this.contextMenuStripGeometry = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemPastFromText = new System.Windows.Forms.ToolStripMenuItem();
            this.gridCoordinate = new SourceGrid.Grid();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.buttonSetStandartOrientation = new System.Windows.Forms.Button();
            this.contextMenuStripGeometry.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridStructure
            // 
            this.gridStructure.AutoStretchColumnsToFitWidth = true;
            this.gridStructure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridStructure.ClipboardMode = ((SourceGrid.ClipboardMode)((((SourceGrid.ClipboardMode.Copy | SourceGrid.ClipboardMode.Cut)
                        | SourceGrid.ClipboardMode.Paste)
                        | SourceGrid.ClipboardMode.Delete)));
            this.gridStructure.ContextMenuStrip = this.contextMenuStripGeometry;
            this.gridStructure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridStructure.Location = new System.Drawing.Point(0, 0);
            this.gridStructure.Name = "gridStructure";
            this.gridStructure.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridStructure.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.gridStructure.Size = new System.Drawing.Size(361, 549);
            this.gridStructure.TabIndex = 0;
            this.gridStructure.TabStop = true;
            this.gridStructure.ToolTipText = "";
            // 
            // contextMenuStripGeometry
            // 
            this.contextMenuStripGeometry.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemPastFromText});
            this.contextMenuStripGeometry.Name = "contextMenuStripGeometry";
            this.contextMenuStripGeometry.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStripGeometry.ShowImageMargin = false;
            this.contextMenuStripGeometry.Size = new System.Drawing.Size(150, 26);
            // 
            // toolStripMenuItemPastFromText
            // 
            this.toolStripMenuItemPastFromText.Name = "toolStripMenuItemPastFromText";
            this.toolStripMenuItemPastFromText.Size = new System.Drawing.Size(149, 22);
            this.toolStripMenuItemPastFromText.Text = "Вставить из текста";
            this.toolStripMenuItemPastFromText.Click += new System.EventHandler(this.toolStripMenuItemPastFromText_Click);
            // 
            // gridCoordinate
            // 
            this.gridCoordinate.AutoStretchColumnsToFitWidth = true;
            this.gridCoordinate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.gridCoordinate.ClipboardMode = ((SourceGrid.ClipboardMode)((((SourceGrid.ClipboardMode.Copy | SourceGrid.ClipboardMode.Cut)
                        | SourceGrid.ClipboardMode.Paste)
                        | SourceGrid.ClipboardMode.Delete)));
            this.gridCoordinate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCoordinate.Location = new System.Drawing.Point(0, 0);
            this.gridCoordinate.Name = "gridCoordinate";
            this.gridCoordinate.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridCoordinate.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.gridCoordinate.Size = new System.Drawing.Size(208, 549);
            this.gridCoordinate.TabIndex = 0;
            this.gridCoordinate.TabStop = true;
            this.gridCoordinate.ToolTipText = "";
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(3, 27);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.gridCoordinate);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.gridStructure);
            this.splitContainer.Size = new System.Drawing.Size(573, 549);
            this.splitContainer.SplitterDistance = 208;
            this.splitContainer.TabIndex = 1;
            // 
            // buttonSetStandartOrientation
            // 
            this.buttonSetStandartOrientation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetStandartOrientation.Location = new System.Drawing.Point(428, 0);
            this.buttonSetStandartOrientation.Name = "buttonSetStandartOrientation";
            this.buttonSetStandartOrientation.Size = new System.Drawing.Size(148, 23);
            this.buttonSetStandartOrientation.TabIndex = 2;
            this.buttonSetStandartOrientation.Text = "Стандартная ориентация";
            this.buttonSetStandartOrientation.UseVisualStyleBackColor = true;
            this.buttonSetStandartOrientation.Click += new System.EventHandler(this.buttonSetStandartOrientation_Click);
            // 
            // GeometryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSetStandartOrientation);
            this.Controls.Add(this.splitContainer);
            this.Name = "GeometryEditor";
            this.Size = new System.Drawing.Size(579, 579);
            this.contextMenuStripGeometry.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SourceGrid.Grid gridCoordinate;
        private SourceGrid.Grid gridStructure;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripGeometry;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemPastFromText;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button buttonSetStandartOrientation;
    }
}
