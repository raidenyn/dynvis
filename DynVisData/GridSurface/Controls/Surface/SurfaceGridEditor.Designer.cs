namespace DynVis.Data.GridSurface
{
    partial class SurfaceGridEditor
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
            this.gridSurface = new SourceGrid.Grid();
            this.progressNotifier = new Core.Progress.ProgressNotifier(this.components);
            this.SuspendLayout();
            // 
            // gridSurface
            // 
            this.gridSurface.ClipboardMode = ((SourceGrid.ClipboardMode)((((SourceGrid.ClipboardMode.Copy | SourceGrid.ClipboardMode.Cut)
                        | SourceGrid.ClipboardMode.Paste)
                        | SourceGrid.ClipboardMode.Delete)));
            this.gridSurface.DefaultWidth = 80;
            this.gridSurface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSurface.FixedColumns = 1;
            this.gridSurface.FixedRows = 1;
            this.gridSurface.Location = new System.Drawing.Point(0, 0);
            this.gridSurface.Name = "gridSurface";
            this.gridSurface.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridSurface.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.gridSurface.Size = new System.Drawing.Size(824, 588);
            this.gridSurface.TabIndex = 0;
            this.gridSurface.TabStop = true;
            this.gridSurface.ToolTipText = "";
            // 
            // progressNotifier
            // 
            this.progressNotifier.Comment = "Построение поверхности";
            // 
            // SurfaceGridEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridSurface);
            this.Name = "SurfaceGridEditor";
            this.Size = new System.Drawing.Size(824, 588);
            this.ResumeLayout(false);

        }

        #endregion

        private SourceGrid.Grid gridSurface;
        private Core.Progress.ProgressNotifier progressNotifier;
    }
}
