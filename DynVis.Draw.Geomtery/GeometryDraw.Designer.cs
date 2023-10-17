namespace DynVis.Geometry.Draw
{
    partial class GeometryDraw
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
            this.contextMenuStripGeneral = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemCurrentCoord = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.drawBox = new DynVis.Draw.DrawBox();
            this.statePanel = new DynVis.Draw.Geomtery.StatePanel();
            this.contextMenuStripGeneral.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripGeneral
            // 
            this.contextMenuStripGeneral.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemCurrentCoord,
            this.toolStripSeparator1,
            this.ToolStripMenuItemProperties});
            this.contextMenuStripGeneral.Name = "contextMenuStrip";
            this.contextMenuStripGeneral.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStripGeneral.ShowImageMargin = false;
            this.contextMenuStripGeneral.Size = new System.Drawing.Size(169, 54);
            this.contextMenuStripGeneral.Opening += new System.ComponentModel.CancelEventHandler(this.GeneralContextMenu_Opening);
            // 
            // ToolStripMenuItemCurrentCoord
            // 
            this.ToolStripMenuItemCurrentCoord.Name = "ToolStripMenuItemCurrentCoord";
            this.ToolStripMenuItemCurrentCoord.Size = new System.Drawing.Size(168, 22);
            this.ToolStripMenuItemCurrentCoord.Text = "Текущие координаты";
            this.ToolStripMenuItemCurrentCoord.Click += new System.EventHandler(this.viewGeometryItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
            // 
            // ToolStripMenuItemProperties
            // 
            this.ToolStripMenuItemProperties.Name = "ToolStripMenuItemProperties";
            this.ToolStripMenuItemProperties.Size = new System.Drawing.Size(168, 22);
            this.ToolStripMenuItemProperties.Text = "Настройки";
            this.ToolStripMenuItemProperties.Click += new System.EventHandler(this.ToolStripMenuItemProperties_Click);
            // 
            // drawBox
            // 
            this.drawBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.drawBox.IsOpenGL = true;
            this.drawBox.Location = new System.Drawing.Point(0, 0);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(339, 122);
            this.drawBox.TabIndex = 1;
            this.drawBox.DoubleClick += new System.EventHandler(this.GeometryDraw_DoubleClick);
            this.drawBox.Paint += new System.Windows.Forms.PaintEventHandler(this.GeometryDraw_Paint);
            this.drawBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.drawBox_PreviewKeyDown);
            this.drawBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GeometryDraw_MouseMove);
            this.drawBox.Click += new System.EventHandler(this.drawBox_Click);
            this.drawBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GeometryDraw_MouseDown);
            this.drawBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GeometryDraw_MouseUp);
            this.drawBox.SizeChanged += new System.EventHandler(this.GeometryDraw_SizeChanged);
            // 
            // statePanel
            // 
            this.statePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statePanel.Location = new System.Drawing.Point(0, 122);
            this.statePanel.Name = "statePanel";
            this.statePanel.Size = new System.Drawing.Size(339, 28);
            this.statePanel.TabIndex = 2;
            // 
            // GeometryDraw
            // 
            this.Controls.Add(this.statePanel);
            this.Controls.Add(this.drawBox);
            this.DoubleBuffered = true;
            this.Name = "GeometryDraw";
            this.Size = new System.Drawing.Size(339, 150);
            this.Load += new System.EventHandler(this.GeometryDraw_Load);
            this.contextMenuStripGeneral.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripGeneral;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCurrentCoord;
        private DynVis.Draw.DrawBox drawBox;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemProperties;
        private DynVis.Draw.Geomtery.StatePanel statePanel;
    }
}
