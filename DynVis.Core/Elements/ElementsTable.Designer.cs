namespace DynVis.Core.Elements
{
    partial class ElementsTable
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
            this.gridElements = new SourceGrid.Grid();
            this.labelSymbol = new System.Windows.Forms.Label();
            this.labelNumber = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelMass = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // gridElements
            // 
            this.gridElements.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gridElements.ColumnsCount = 15;
            this.gridElements.DefaultWidth = 20;
            this.gridElements.FixedColumns = 1;
            this.gridElements.FixedRows = 1;
            this.gridElements.Location = new System.Drawing.Point(0, 45);
            this.gridElements.Name = "gridElements";
            this.gridElements.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.gridElements.RowsCount = 15;
            this.gridElements.SelectionMode = SourceGrid.GridSelectionMode.Cell;
            this.gridElements.Size = new System.Drawing.Size(752, 301);
            this.gridElements.TabIndex = 0;
            this.gridElements.TabStop = true;
            this.gridElements.ToolTipText = "";
            this.gridElements.DoubleClick += new System.EventHandler(this.gridElements_DoubleClick);
            // 
            // labelSymbol
            // 
            this.labelSymbol.AutoSize = true;
            this.labelSymbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSymbol.Location = new System.Drawing.Point(23, 9);
            this.labelSymbol.Name = "labelSymbol";
            this.labelSymbol.Size = new System.Drawing.Size(36, 33);
            this.labelSymbol.TabIndex = 1;
            this.labelSymbol.Text = "C";
            this.labelSymbol.SizeChanged += new System.EventHandler(this.labelSymbol_SizeChanged);
            // 
            // labelNumber
            // 
            this.labelNumber.AutoSize = true;
            this.labelNumber.Location = new System.Drawing.Point(54, 9);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(13, 13);
            this.labelNumber.TabIndex = 2;
            this.labelNumber.Text = "6";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelName.Location = new System.Drawing.Point(96, 20);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(61, 20);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "Carbon";
            // 
            // labelMass
            // 
            this.labelMass.AutoSize = true;
            this.labelMass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMass.Location = new System.Drawing.Point(226, 20);
            this.labelMass.Name = "labelMass";
            this.labelMass.Size = new System.Drawing.Size(60, 20);
            this.labelMass.TabIndex = 2;
            this.labelMass.Text = "Масса:";
            // 
            // ElementsTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelMass);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelNumber);
            this.Controls.Add(this.labelSymbol);
            this.Controls.Add(this.gridElements);
            this.Name = "ElementsTable";
            this.Size = new System.Drawing.Size(752, 343);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SourceGrid.Grid gridElements;
        private System.Windows.Forms.Label labelSymbol;
        private System.Windows.Forms.Label labelNumber;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelMass;
    }
}
