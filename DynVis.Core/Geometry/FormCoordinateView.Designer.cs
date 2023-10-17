namespace DynVis.Geometry
{
    partial class FormCoordinateView
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
            this.buttonClose = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemSaveTo = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSaveToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSaveToClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.gridCoordinate = new SourceGrid.Grid();
            this.квадратToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(370, 556);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "Закрыть";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSaveTo,
            this.квадратToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(457, 24);
            this.menuStrip.TabIndex = 2;
            this.menuStrip.Text = "menuStrip";
            // 
            // ToolStripMenuItemSaveTo
            // 
            this.ToolStripMenuItemSaveTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSaveToFile,
            this.ToolStripMenuItemSaveToClipboard});
            this.ToolStripMenuItemSaveTo.Name = "ToolStripMenuItemSaveTo";
            this.ToolStripMenuItemSaveTo.Size = new System.Drawing.Size(77, 20);
            this.ToolStripMenuItemSaveTo.Text = "Сохранить";
            // 
            // ToolStripMenuItemSaveToFile
            // 
            this.ToolStripMenuItemSaveToFile.Name = "ToolStripMenuItemSaveToFile";
            this.ToolStripMenuItemSaveToFile.Size = new System.Drawing.Size(164, 22);
            this.ToolStripMenuItemSaveToFile.Text = "В файл";
            this.ToolStripMenuItemSaveToFile.Click += new System.EventHandler(this.ToolStripMenuItemSaveToFile_Click);
            // 
            // ToolStripMenuItemSaveToClipboard
            // 
            this.ToolStripMenuItemSaveToClipboard.Name = "ToolStripMenuItemSaveToClipboard";
            this.ToolStripMenuItemSaveToClipboard.Size = new System.Drawing.Size(164, 22);
            this.ToolStripMenuItemSaveToClipboard.Text = "В буфер обмена";
            this.ToolStripMenuItemSaveToClipboard.Click += new System.EventHandler(this.ToolStripMenuItemSaveToClipboard_Click);
            // 
            // gridCoordinate
            // 
            this.gridCoordinate.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridCoordinate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridCoordinate.ColumnsCount = 5;
            this.gridCoordinate.FixedColumns = 1;
            this.gridCoordinate.FixedRows = 1;
            this.gridCoordinate.Location = new System.Drawing.Point(2, 27);
            this.gridCoordinate.Name = "gridCoordinate";
            this.gridCoordinate.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridCoordinate.RowsCount = 1;
            this.gridCoordinate.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.gridCoordinate.Size = new System.Drawing.Size(454, 523);
            this.gridCoordinate.TabIndex = 0;
            this.gridCoordinate.TabStop = true;
            this.gridCoordinate.ToolTipText = "";
            // 
            // квадратToolStripMenuItem
            // 
            this.квадратToolStripMenuItem.Name = "квадратToolStripMenuItem";
            this.квадратToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.квадратToolStripMenuItem.Text = "Квадрат";
            this.квадратToolStripMenuItem.Click += new System.EventHandler(this.квадратToolStripMenuItem_Click);
            // 
            // FormCoordinateView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 591);
            this.ControlBox = false;
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.gridCoordinate);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCoordinateView";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Текущие координаты";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SourceGrid.Grid gridCoordinate;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSaveTo;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSaveToFile;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSaveToClipboard;
        private System.Windows.Forms.ToolStripMenuItem квадратToolStripMenuItem;

    }
}