namespace DynVis.Data.CalculationMethods.LEPS
{
    partial class FormLEPSEditor
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
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.coordinateParamsLEPS = new DynVis.Data.CalculationMethods.LEPS.Controls.CoordinateParamsLEPS();
            this.BC = new DynVis.Data.CalculationMethods.LEPS.Controls.AtomsInteractionParamsLEPS();
            this.AC = new DynVis.Data.CalculationMethods.LEPS.Controls.AtomsInteractionParamsLEPS();
            this.AB = new DynVis.Data.CalculationMethods.LEPS.Controls.AtomsInteractionParamsLEPS();
            this.elementSelectorA = new DynVis.Core.Elements.ElementSelector();
            this.labelElementA = new System.Windows.Forms.Label();
            this.labelElementB = new System.Windows.Forms.Label();
            this.elementSelectorB = new DynVis.Core.Elements.ElementSelector();
            this.labelElementC = new System.Windows.Forms.Label();
            this.elementSelectorC = new DynVis.Core.Elements.ElementSelector();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(546, 375);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(465, 375);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSave,
            this.ToolStripMenuItemOpen});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(630, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItemSave
            // 
            this.ToolStripMenuItemSave.Name = "ToolStripMenuItemSave";
            this.ToolStripMenuItemSave.Size = new System.Drawing.Size(77, 20);
            this.ToolStripMenuItemSave.Text = "Сохранить";
            this.ToolStripMenuItemSave.Click += new System.EventHandler(this.ToolStripMenuItemSave_Click);
            // 
            // ToolStripMenuItemOpen
            // 
            this.ToolStripMenuItemOpen.Name = "ToolStripMenuItemOpen";
            this.ToolStripMenuItemOpen.Size = new System.Drawing.Size(66, 20);
            this.ToolStripMenuItemOpen.Text = "Открыть";
            this.ToolStripMenuItemOpen.Click += new System.EventHandler(this.ToolStripMenuItemOpen_Click);
            // 
            // coordinateParamsLEPS
            // 
            this.coordinateParamsLEPS.Location = new System.Drawing.Point(12, 187);
            this.coordinateParamsLEPS.Name = "coordinateParamsLEPS";
            this.coordinateParamsLEPS.Size = new System.Drawing.Size(612, 179);
            this.coordinateParamsLEPS.TabIndex = 2;
            // 
            // BC
            // 
            this.BC.Location = new System.Drawing.Point(424, 49);
            this.BC.Name = "BC";
            this.BC.Size = new System.Drawing.Size(200, 132);
            this.BC.TabIndex = 0;
            this.BC.Title = "B-C";
            // 
            // AC
            // 
            this.AC.Location = new System.Drawing.Point(218, 49);
            this.AC.Name = "AC";
            this.AC.Size = new System.Drawing.Size(200, 132);
            this.AC.TabIndex = 0;
            this.AC.Title = "A-C";
            // 
            // AB
            // 
            this.AB.Location = new System.Drawing.Point(12, 49);
            this.AB.Name = "AB";
            this.AB.Size = new System.Drawing.Size(200, 132);
            this.AB.TabIndex = 0;
            this.AB.Title = "A-B";
            // 
            // elementSelectorA
            // 
            this.elementSelectorA.Location = new System.Drawing.Point(111, 28);
            this.elementSelectorA.Name = "elementSelectorA";
            this.elementSelectorA.Size = new System.Drawing.Size(15, 13);
            this.elementSelectorA.TabIndex = 4;
            // 
            // labelElementA
            // 
            this.labelElementA.AutoSize = true;
            this.labelElementA.Location = new System.Drawing.Point(13, 28);
            this.labelElementA.Name = "labelElementA";
            this.labelElementA.Size = new System.Drawing.Size(98, 13);
            this.labelElementA.TabIndex = 5;
            this.labelElementA.Text = "Элемент атома A:";
            // 
            // labelElementB
            // 
            this.labelElementB.AutoSize = true;
            this.labelElementB.Location = new System.Drawing.Point(220, 29);
            this.labelElementB.Name = "labelElementB";
            this.labelElementB.Size = new System.Drawing.Size(98, 13);
            this.labelElementB.TabIndex = 7;
            this.labelElementB.Text = "Элемент атома B:";
            // 
            // elementSelectorB
            // 
            this.elementSelectorB.Location = new System.Drawing.Point(319, 29);
            this.elementSelectorB.Name = "elementSelectorB";
            this.elementSelectorB.Size = new System.Drawing.Size(15, 13);
            this.elementSelectorB.TabIndex = 6;
            // 
            // labelElementC
            // 
            this.labelElementC.AutoSize = true;
            this.labelElementC.Location = new System.Drawing.Point(421, 29);
            this.labelElementC.Name = "labelElementC";
            this.labelElementC.Size = new System.Drawing.Size(98, 13);
            this.labelElementC.TabIndex = 9;
            this.labelElementC.Text = "Элемент атома C:";
            // 
            // elementSelectorC
            // 
            this.elementSelectorC.Location = new System.Drawing.Point(519, 29);
            this.elementSelectorC.Name = "elementSelectorC";
            this.elementSelectorC.Size = new System.Drawing.Size(15, 13);
            this.elementSelectorC.TabIndex = 8;
            // 
            // FormLEPSEditor
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(630, 410);
            this.Controls.Add(this.elementSelectorC);
            this.Controls.Add(this.elementSelectorB);
            this.Controls.Add(this.elementSelectorA);
            this.Controls.Add(this.coordinateParamsLEPS);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.BC);
            this.Controls.Add(this.AC);
            this.Controls.Add(this.AB);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.labelElementC);
            this.Controls.Add(this.labelElementB);
            this.Controls.Add(this.labelElementA);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLEPSEditor";
            this.Text = "Метод ЛЭПС";
            this.Load += new System.EventHandler(this.FormLEPSEditor_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DynVis.Data.CalculationMethods.LEPS.Controls.AtomsInteractionParamsLEPS AB;
        private DynVis.Data.CalculationMethods.LEPS.Controls.AtomsInteractionParamsLEPS AC;
        private DynVis.Data.CalculationMethods.LEPS.Controls.AtomsInteractionParamsLEPS BC;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private DynVis.Data.CalculationMethods.LEPS.Controls.CoordinateParamsLEPS coordinateParamsLEPS;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOpen;
        private Core.Elements.ElementSelector elementSelectorA;
        private System.Windows.Forms.Label labelElementA;
        private System.Windows.Forms.Label labelElementB;
        private Core.Elements.ElementSelector elementSelectorB;
        private System.Windows.Forms.Label labelElementC;
        private Core.Elements.ElementSelector elementSelectorC;
    }
}