namespace DynVis
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSaveSurface = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSaveGeom = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSavePath = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSavePoints = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSurface = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemGeometry = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemGeomImport = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemPath = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSurfacePoints = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemViewSurface = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemViewPath = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemViewGeom = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSurfacePointsView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemCurrentPointInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemSetFormsPositions = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemAboutPrograms = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCheckUpdates = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSendCallback = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemLogEvents = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemFile,
            this.ToolStripMenuItemSurface,
            this.ToolStripMenuItemGeometry,
            this.ToolStripMenuItemPath,
            this.ToolStripMenuItemSurfacePoints,
            this.ToolStripMenuItemView,
            this.ToolStripMenuItemProperties,
            this.ToolStripMenuItemHelp});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip.Size = new System.Drawing.Size(607, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // ToolStripMenuItemFile
            // 
            this.ToolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem,
            this.toolStripSeparator1,
            this.ToolStripMenuItemExit});
            this.ToolStripMenuItemFile.Name = "ToolStripMenuItemFile";
            this.ToolStripMenuItemFile.Size = new System.Drawing.Size(48, 20);
            this.ToolStripMenuItemFile.Text = "Файл";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSaveSurface,
            this.ToolStripMenuItemSaveGeom,
            this.ToolStripMenuItemSavePath,
            this.ToolStripMenuItemSavePoints});
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            // 
            // ToolStripMenuItemSaveSurface
            // 
            this.ToolStripMenuItemSaveSurface.Name = "ToolStripMenuItemSaveSurface";
            this.ToolStripMenuItemSaveSurface.Size = new System.Drawing.Size(149, 22);
            this.ToolStripMenuItemSaveSurface.Text = "Поверхность";
            this.ToolStripMenuItemSaveSurface.Click += new System.EventHandler(this.ToolStripMenuItemSaveSurface_Click);
            // 
            // ToolStripMenuItemSaveGeom
            // 
            this.ToolStripMenuItemSaveGeom.Name = "ToolStripMenuItemSaveGeom";
            this.ToolStripMenuItemSaveGeom.Size = new System.Drawing.Size(149, 22);
            this.ToolStripMenuItemSaveGeom.Text = "Геометрию";
            this.ToolStripMenuItemSaveGeom.Click += new System.EventHandler(this.ToolStripMenuItemSaveGeom_Click);
            // 
            // ToolStripMenuItemSavePath
            // 
            this.ToolStripMenuItemSavePath.Name = "ToolStripMenuItemSavePath";
            this.ToolStripMenuItemSavePath.Size = new System.Drawing.Size(149, 22);
            this.ToolStripMenuItemSavePath.Text = "Путь реакции";
            this.ToolStripMenuItemSavePath.Click += new System.EventHandler(this.ToolStripMenuItemSavePath_Click);
            // 
            // ToolStripMenuItemSavePoints
            // 
            this.ToolStripMenuItemSavePoints.Name = "ToolStripMenuItemSavePoints";
            this.ToolStripMenuItemSavePoints.Size = new System.Drawing.Size(149, 22);
            this.ToolStripMenuItemSavePoints.Text = "Точки";
            this.ToolStripMenuItemSavePoints.Click += new System.EventHandler(this.ToolStripMenuItemSavePoints_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(129, 6);
            // 
            // ToolStripMenuItemExit
            // 
            this.ToolStripMenuItemExit.Name = "ToolStripMenuItemExit";
            this.ToolStripMenuItemExit.Size = new System.Drawing.Size(132, 22);
            this.ToolStripMenuItemExit.Text = "Выход";
            this.ToolStripMenuItemExit.Click += new System.EventHandler(this.ToolStripMenuItemExit_Click);
            // 
            // ToolStripMenuItemSurface
            // 
            this.ToolStripMenuItemSurface.Name = "ToolStripMenuItemSurface";
            this.ToolStripMenuItemSurface.Size = new System.Drawing.Size(90, 20);
            this.ToolStripMenuItemSurface.Text = "Поверхность";
            // 
            // ToolStripMenuItemGeometry
            // 
            this.ToolStripMenuItemGeometry.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemGeomImport});
            this.ToolStripMenuItemGeometry.Name = "ToolStripMenuItemGeometry";
            this.ToolStripMenuItemGeometry.Size = new System.Drawing.Size(78, 20);
            this.ToolStripMenuItemGeometry.Text = "Геометрия";
            // 
            // ToolStripMenuItemGeomImport
            // 
            this.ToolStripMenuItemGeomImport.Name = "ToolStripMenuItemGeomImport";
            this.ToolStripMenuItemGeomImport.Size = new System.Drawing.Size(162, 22);
            this.ToolStripMenuItemGeomImport.Text = "Импортировать";
            this.ToolStripMenuItemGeomImport.Paint += new System.Windows.Forms.PaintEventHandler(this.ToolStripMenuItemGeomImport_Paint);
            this.ToolStripMenuItemGeomImport.Click += new System.EventHandler(this.ToolStripMenuItemGeomImport_Click);
            // 
            // ToolStripMenuItemPath
            // 
            this.ToolStripMenuItemPath.Name = "ToolStripMenuItemPath";
            this.ToolStripMenuItemPath.Size = new System.Drawing.Size(94, 20);
            this.ToolStripMenuItemPath.Text = "Путь реакции";
            // 
            // ToolStripMenuItemSurfacePoints
            // 
            this.ToolStripMenuItemSurfacePoints.Name = "ToolStripMenuItemSurfacePoints";
            this.ToolStripMenuItemSurfacePoints.Size = new System.Drawing.Size(81, 20);
            this.ToolStripMenuItemSurfacePoints.Text = "Точки ППЭ";
            // 
            // ToolStripMenuItemView
            // 
            this.ToolStripMenuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemViewSurface,
            this.ToolStripMenuItemViewPath,
            this.ToolStripMenuItemViewGeom,
            this.ToolStripMenuItemSurfacePointsView,
            this.toolStripSeparator4,
            this.ToolStripMenuItemCurrentPointInformation,
            this.toolStripSeparator2,
            this.ToolStripMenuItemSetFormsPositions});
            this.ToolStripMenuItemView.Name = "ToolStripMenuItemView";
            this.ToolStripMenuItemView.Size = new System.Drawing.Size(39, 20);
            this.ToolStripMenuItemView.Text = "Вид";
            // 
            // ToolStripMenuItemViewSurface
            // 
            this.ToolStripMenuItemViewSurface.Name = "ToolStripMenuItemViewSurface";
            this.ToolStripMenuItemViewSurface.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItemViewSurface.Text = "Поверхность энергии";
            this.ToolStripMenuItemViewSurface.Paint += new System.Windows.Forms.PaintEventHandler(this.ToolStripMenuItemViewSurface_Paint);
            this.ToolStripMenuItemViewSurface.Click += new System.EventHandler(this.ToolStripMenuItemViewSurface_Click);
            // 
            // ToolStripMenuItemViewPath
            // 
            this.ToolStripMenuItemViewPath.Name = "ToolStripMenuItemViewPath";
            this.ToolStripMenuItemViewPath.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItemViewPath.Text = "Путь реакции";
            this.ToolStripMenuItemViewPath.Paint += new System.Windows.Forms.PaintEventHandler(this.ToolStripMenuItemViewPath_Paint);
            this.ToolStripMenuItemViewPath.Click += new System.EventHandler(this.ToolStripMenuItemViewPath_Click);
            // 
            // ToolStripMenuItemViewGeom
            // 
            this.ToolStripMenuItemViewGeom.Name = "ToolStripMenuItemViewGeom";
            this.ToolStripMenuItemViewGeom.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItemViewGeom.Text = "Геометрия";
            this.ToolStripMenuItemViewGeom.Paint += new System.Windows.Forms.PaintEventHandler(this.ToolStripMenuItemViewGeom_Paint);
            this.ToolStripMenuItemViewGeom.Click += new System.EventHandler(this.ToolStripMenuItemViewGeom_Click);
            // 
            // ToolStripMenuItemSurfacePointsView
            // 
            this.ToolStripMenuItemSurfacePointsView.Name = "ToolStripMenuItemSurfacePointsView";
            this.ToolStripMenuItemSurfacePointsView.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItemSurfacePointsView.Text = "Точки ППЭ";
            this.ToolStripMenuItemSurfacePointsView.Paint += new System.Windows.Forms.PaintEventHandler(this.ToolStripMenuItemSurfacePointsView_Paint);
            this.ToolStripMenuItemSurfacePointsView.Click += new System.EventHandler(this.ToolStripMenuItemSurfacePointsView_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(190, 6);
            // 
            // ToolStripMenuItemCurrentPointInformation
            // 
            this.ToolStripMenuItemCurrentPointInformation.Name = "ToolStripMenuItemCurrentPointInformation";
            this.ToolStripMenuItemCurrentPointInformation.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItemCurrentPointInformation.Text = "Фигуративная точка";
            this.ToolStripMenuItemCurrentPointInformation.Paint += new System.Windows.Forms.PaintEventHandler(this.ToolStripMenuItemCurrentPointInformation_Paint);
            this.ToolStripMenuItemCurrentPointInformation.Click += new System.EventHandler(this.ToolStripMenuItemCurrentPointInformation_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(190, 6);
            // 
            // ToolStripMenuItemSetFormsPositions
            // 
            this.ToolStripMenuItemSetFormsPositions.Name = "ToolStripMenuItemSetFormsPositions";
            this.ToolStripMenuItemSetFormsPositions.Size = new System.Drawing.Size(193, 22);
            this.ToolStripMenuItemSetFormsPositions.Text = "Выстроить окна";
            this.ToolStripMenuItemSetFormsPositions.Click += new System.EventHandler(this.ToolStripMenuItemSetFormsPositions_Click);
            // 
            // ToolStripMenuItemProperties
            // 
            this.ToolStripMenuItemProperties.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemOptions});
            this.ToolStripMenuItemProperties.Name = "ToolStripMenuItemProperties";
            this.ToolStripMenuItemProperties.Size = new System.Drawing.Size(70, 20);
            this.ToolStripMenuItemProperties.Text = "Свойства";
            // 
            // ToolStripMenuItemOptions
            // 
            this.ToolStripMenuItemOptions.Name = "ToolStripMenuItemOptions";
            this.ToolStripMenuItemOptions.Size = new System.Drawing.Size(133, 22);
            this.ToolStripMenuItemOptions.Text = "Настройка";
            this.ToolStripMenuItemOptions.Click += new System.EventHandler(this.ToolStripMenuItemOptions_Click);
            // 
            // ToolStripMenuItemHelp
            // 
            this.ToolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemAboutPrograms,
            this.ToolStripMenuItemCheckUpdates,
            this.ToolStripMenuItemSendCallback,
            this.toolStripSeparator3,
            this.ToolStripMenuItemLogEvents});
            this.ToolStripMenuItemHelp.Name = "ToolStripMenuItemHelp";
            this.ToolStripMenuItemHelp.Size = new System.Drawing.Size(65, 20);
            this.ToolStripMenuItemHelp.Text = "Справка";
            // 
            // ToolStripMenuItemAboutPrograms
            // 
            this.ToolStripMenuItemAboutPrograms.Name = "ToolStripMenuItemAboutPrograms";
            this.ToolStripMenuItemAboutPrograms.Size = new System.Drawing.Size(204, 22);
            this.ToolStripMenuItemAboutPrograms.Text = "О программе";
            this.ToolStripMenuItemAboutPrograms.Click += new System.EventHandler(this.ToolStripMenuItemAboutPrograms_Click);
            // 
            // ToolStripMenuItemCheckUpdates
            // 
            this.ToolStripMenuItemCheckUpdates.Name = "ToolStripMenuItemCheckUpdates";
            this.ToolStripMenuItemCheckUpdates.Size = new System.Drawing.Size(204, 22);
            this.ToolStripMenuItemCheckUpdates.Text = "Проверить обновления";
            this.ToolStripMenuItemCheckUpdates.Click += new System.EventHandler(this.ToolStripMenuItemCheckUpdates_Click);
            // 
            // ToolStripMenuItemSendCallback
            // 
            this.ToolStripMenuItemSendCallback.Name = "ToolStripMenuItemSendCallback";
            this.ToolStripMenuItemSendCallback.Size = new System.Drawing.Size(204, 22);
            this.ToolStripMenuItemSendCallback.Text = "Послать сообщение";
            this.ToolStripMenuItemSendCallback.Click += new System.EventHandler(this.ToolStripMenuItemSendCallback_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(201, 6);
            // 
            // ToolStripMenuItemLogEvents
            // 
            this.ToolStripMenuItemLogEvents.Name = "ToolStripMenuItemLogEvents";
            this.ToolStripMenuItemLogEvents.Size = new System.Drawing.Size(204, 22);
            this.ToolStripMenuItemLogEvents.Text = "Лог программы";
            this.ToolStripMenuItemLogEvents.Click += new System.EventHandler(this.ToolStripMenuItemLogEvents_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 24);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximumSize = new System.Drawing.Size(30000, 60);
            this.Name = "FormMain";
            this.Text = "DynVis";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemView;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSurface;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemGeometry;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemPath;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemGeomImport;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemAboutPrograms;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemViewGeom;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemViewSurface;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemViewPath;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSaveSurface;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSaveGeom;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSavePath;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemLogEvents;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCurrentPointInformation;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSurfacePoints;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSurfacePointsView;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSavePoints;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSetFormsPositions;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCheckUpdates;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSendCallback;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemProperties;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemOptions;
    }
}