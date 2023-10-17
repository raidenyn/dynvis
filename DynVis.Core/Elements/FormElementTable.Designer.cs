namespace DynVis.Core.Elements
{
    partial class FormElementTable
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
            this.elementsTable = new DynVis.Core.Elements.ElementsTable();
            this.SuspendLayout();
            // 
            // elementsTable
            // 
            this.elementsTable.ElementNumber = 0;
            this.elementsTable.Location = new System.Drawing.Point(-1, 1);
            this.elementsTable.Name = "elementsTable";
            this.elementsTable.Size = new System.Drawing.Size(752, 343);
            this.elementsTable.TabIndex = 0;
            this.elementsTable.DoubleClickButton += new System.EventHandler(this.elementsTable_DoubleClick);
            this.elementsTable.DoubleClick += new System.EventHandler(this.elementsTable_DoubleClick);
            // 
            // FormElementTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 352);
            this.Controls.Add(this.elementsTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormElementTable";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Таблица элементов";
            this.ResumeLayout(false);

        }

        #endregion

        private ElementsTable elementsTable;
    }
}