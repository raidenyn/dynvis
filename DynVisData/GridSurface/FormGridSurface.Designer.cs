namespace DynVis.Data.GridSurface
{
    partial class FormGridSurface
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
            DynVis.Core.Axes axes1 = new DynVis.Core.Axes();
            DynVis.Core.ScaleDimension scaleDimension1 = new DynVis.Core.ScaleDimension();
            DynVis.Core.Axes axes2 = new DynVis.Core.Axes();
            DynVis.Core.ScaleDimension scaleDimension2 = new DynVis.Core.ScaleDimension();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCoordinate = new System.Windows.Forms.TabPage();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.coordinateEditorQ1 = new DynVis.Data.GridSurface.CoordinateEditor();
            this.coordinateEditorQ2 = new DynVis.Data.GridSurface.CoordinateEditor();
            this.tabPageEnergy = new System.Windows.Forms.TabPage();
            this.surfaceGridEditor = new DynVis.Data.GridSurface.SurfaceGridEditor();
            this.tabPageElementList = new System.Windows.Forms.TabPage();
            this.elementListEditor = new DynVis.Data.GridSurface.ElementListEditor();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl.SuspendLayout();
            this.tabPageCoordinate.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.tabPageEnergy.SuspendLayout();
            this.tabPageElementList.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl.Controls.Add(this.tabPageCoordinate);
            this.tabControl.Controls.Add(this.tabPageEnergy);
            this.tabControl.Controls.Add(this.tabPageElementList);
            this.tabControl.Location = new System.Drawing.Point(-1, -3);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(856, 365);
            this.tabControl.TabIndex = 5;
            // 
            // tabPageCoordinate
            // 
            this.tabPageCoordinate.Controls.Add(this.splitContainer);
            this.tabPageCoordinate.Location = new System.Drawing.Point(4, 25);
            this.tabPageCoordinate.Name = "tabPageCoordinate";
            this.tabPageCoordinate.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCoordinate.Size = new System.Drawing.Size(848, 336);
            this.tabPageCoordinate.TabIndex = 0;
            this.tabPageCoordinate.Text = "Координаты";
            this.tabPageCoordinate.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(3, 3);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.coordinateEditorQ1);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.coordinateEditorQ2);
            this.splitContainer.Size = new System.Drawing.Size(842, 330);
            this.splitContainer.SplitterDistance = 417;
            this.splitContainer.TabIndex = 4;
            // 
            // coordinateEditorQ1
            // 
            axes1.MaxValue = 0;
            axes1.MinValue = 0;
            axes1.Name = "Координата";
            scaleDimension1.ScaleCoeff = DynVis.Core.ScaleCoeff.normal;
            axes1.ScaleDimension = scaleDimension1;
            axes1.TypeMaxValue = DynVis.Core.Surface.CoordinateType.Ended;
            axes1.TypeMinValue = DynVis.Core.Surface.CoordinateType.Ended;
            this.coordinateEditorQ1.Axe = axes1;
            this.coordinateEditorQ1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coordinateEditorQ1.Location = new System.Drawing.Point(0, 0);
            this.coordinateEditorQ1.Name = "coordinateEditorQ1";
            this.coordinateEditorQ1.Size = new System.Drawing.Size(417, 330);
            this.coordinateEditorQ1.TabIndex = 0;
            this.coordinateEditorQ1.Title = "Первая координата Q1";
            // 
            // coordinateEditorQ2
            // 
            axes2.MaxValue = 0;
            axes2.MinValue = 0;
            axes2.Name = "Координата";
            scaleDimension2.ScaleCoeff = DynVis.Core.ScaleCoeff.normal;
            axes2.ScaleDimension = scaleDimension2;
            axes2.TypeMaxValue = DynVis.Core.Surface.CoordinateType.Ended;
            axes2.TypeMinValue = DynVis.Core.Surface.CoordinateType.Ended;
            this.coordinateEditorQ2.Axe = axes2;
            this.coordinateEditorQ2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coordinateEditorQ2.Location = new System.Drawing.Point(0, 0);
            this.coordinateEditorQ2.Name = "coordinateEditorQ2";
            this.coordinateEditorQ2.Size = new System.Drawing.Size(421, 330);
            this.coordinateEditorQ2.TabIndex = 0;
            this.coordinateEditorQ2.Title = "Вторая координата Q2";
            // 
            // tabPageEnergy
            // 
            this.tabPageEnergy.Controls.Add(this.surfaceGridEditor);
            this.tabPageEnergy.Location = new System.Drawing.Point(4, 25);
            this.tabPageEnergy.Name = "tabPageEnergy";
            this.tabPageEnergy.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEnergy.Size = new System.Drawing.Size(946, 469);
            this.tabPageEnergy.TabIndex = 1;
            this.tabPageEnergy.Text = "Энергия";
            this.tabPageEnergy.UseVisualStyleBackColor = true;
            // 
            // surfaceGridEditor
            // 
            this.surfaceGridEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.surfaceGridEditor.Location = new System.Drawing.Point(3, 3);
            this.surfaceGridEditor.Name = "surfaceGridEditor";
            this.surfaceGridEditor.Size = new System.Drawing.Size(940, 463);
            this.surfaceGridEditor.TabIndex = 0;
            // 
            // tabPageElementList
            // 
            this.tabPageElementList.Controls.Add(this.elementListEditor);
            this.tabPageElementList.Location = new System.Drawing.Point(4, 25);
            this.tabPageElementList.Name = "tabPageElementList";
            this.tabPageElementList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageElementList.Size = new System.Drawing.Size(946, 469);
            this.tabPageElementList.TabIndex = 2;
            this.tabPageElementList.Text = "tabPage1";
            this.tabPageElementList.UseVisualStyleBackColor = true;
            // 
            // elementListEditor
            // 
            this.elementListEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementListEditor.Location = new System.Drawing.Point(3, 3);
            this.elementListEditor.Name = "elementListEditor";
            this.elementListEditor.Size = new System.Drawing.Size(940, 463);
            this.elementListEditor.TabIndex = 0;
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.Location = new System.Drawing.Point(588, 368);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 6;
            this.buttonNext.Text = "Далее ->";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(776, 368);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(695, 368);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonPrev
            // 
            this.buttonPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrev.Location = new System.Drawing.Point(507, 368);
            this.buttonPrev.Name = "buttonPrev";
            this.buttonPrev.Size = new System.Drawing.Size(75, 23);
            this.buttonPrev.TabIndex = 6;
            this.buttonPrev.Text = "<- Назад";
            this.buttonPrev.UseVisualStyleBackColor = true;
            this.buttonPrev.Click += new System.EventHandler(this.buttonPrev_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemOpen});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(854, 24);
            this.menuStrip.TabIndex = 7;
            this.menuStrip.Text = "menuStrip1";
            // 
            // ToolStripMenuItemOpen
            // 
            this.ToolStripMenuItemOpen.Name = "ToolStripMenuItemOpen";
            this.ToolStripMenuItemOpen.Size = new System.Drawing.Size(66, 20);
            this.ToolStripMenuItemOpen.Text = "Открыть";
            this.ToolStripMenuItemOpen.Click += new System.EventHandler(this.ToolStripMenuItemOpen_Click);
            // 
            // FormGridSurface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(854, 403);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonPrev);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.tabControl);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormGridSurface";
            this.ShowInTaskbar = false;
            this.Text = "Сетка значений ППЭ";
            this.Load += new System.EventHandler(this.FormImportSurface_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageCoordinate.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.tabPageEnergy.ResumeLayout(false);
            this.tabPageElementList.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private CoordinateEditor coordinateEditorQ1;
        private CoordinateEditor coordinateEditorQ2;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageCoordinate;
        private System.Windows.Forms.TabPage tabPageEnergy;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonPrev;
        private SurfaceGridEditor surfaceGridEditor;
        private System.Windows.Forms.TabPage tabPageElementList;
        private ElementListEditor elementListEditor;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOpen;
    }
}