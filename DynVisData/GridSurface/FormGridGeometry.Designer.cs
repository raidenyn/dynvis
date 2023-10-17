namespace DynVis.Data.GridSurface
{
    partial class FormGridGeometry
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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageElementList = new System.Windows.Forms.TabPage();
            this.elementListEditor = new DynVis.Data.GridSurface.ElementListEditor();
            this.tabPageStructures = new System.Windows.Forms.TabPage();
            this.geometryEditor = new DynVis.Data.GridSurface.GeometryEditor();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonPrev = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemLoadFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl.SuspendLayout();
            this.tabPageElementList.SuspendLayout();
            this.tabPageStructures.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl.Controls.Add(this.tabPageElementList);
            this.tabControl.Controls.Add(this.tabPageStructures);
            this.tabControl.Location = new System.Drawing.Point(-1, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(702, 439);
            this.tabControl.TabIndex = 5;
            // 
            // tabPageElementList
            // 
            this.tabPageElementList.Controls.Add(this.elementListEditor);
            this.tabPageElementList.Location = new System.Drawing.Point(4, 25);
            this.tabPageElementList.Name = "tabPageElementList";
            this.tabPageElementList.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageElementList.Size = new System.Drawing.Size(694, 410);
            this.tabPageElementList.TabIndex = 2;
            this.tabPageElementList.Text = "tabPage1";
            this.tabPageElementList.UseVisualStyleBackColor = true;
            // 
            // elementListEditor
            // 
            this.elementListEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementListEditor.Location = new System.Drawing.Point(3, 3);
            this.elementListEditor.Name = "elementListEditor";
            this.elementListEditor.Size = new System.Drawing.Size(688, 404);
            this.elementListEditor.TabIndex = 0;
            // 
            // tabPageStructures
            // 
            this.tabPageStructures.Controls.Add(this.geometryEditor);
            this.tabPageStructures.Location = new System.Drawing.Point(4, 25);
            this.tabPageStructures.Name = "tabPageStructures";
            this.tabPageStructures.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStructures.Size = new System.Drawing.Size(694, 410);
            this.tabPageStructures.TabIndex = 3;
            this.tabPageStructures.Text = "tabPage1";
            this.tabPageStructures.UseVisualStyleBackColor = true;
            // 
            // geometryEditor
            // 
            this.geometryEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.geometryEditor.Location = new System.Drawing.Point(3, 3);
            this.geometryEditor.Name = "geometryEditor";
            this.geometryEditor.Size = new System.Drawing.Size(688, 404);
            this.geometryEditor.TabIndex = 0;
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.Location = new System.Drawing.Point(434, 439);
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
            this.buttonCancel.Location = new System.Drawing.Point(622, 439);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(541, 439);
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
            this.buttonPrev.Location = new System.Drawing.Point(353, 439);
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
            this.toolStripMenuItemLoadFromFile});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(700, 24);
            this.menuStrip.TabIndex = 7;
            this.menuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuItemLoadFromFile
            // 
            this.toolStripMenuItemLoadFromFile.Name = "toolStripMenuItemLoadFromFile";
            this.toolStripMenuItemLoadFromFile.Size = new System.Drawing.Size(126, 20);
            this.toolStripMenuItemLoadFromFile.Text = "Загрузить из файла";
            this.toolStripMenuItemLoadFromFile.Click += new System.EventHandler(this.toolStripMenuItemLoadFromFile_Click);
            // 
            // FormGridGeometry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(700, 474);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonPrev);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.buttonOK);
            this.Name = "FormGridGeometry";
            this.ShowInTaskbar = false;
            this.Text = "Импорт ППЭ";
            this.Load += new System.EventHandler(this.FormImportSurface_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageElementList.ResumeLayout(false);
            this.tabPageStructures.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonPrev;
        private System.Windows.Forms.TabPage tabPageElementList;
        private System.Windows.Forms.TabPage tabPageStructures;
        private GeometryEditor geometryEditor;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemLoadFromFile;
        private ElementListEditor elementListEditor;
    }
}