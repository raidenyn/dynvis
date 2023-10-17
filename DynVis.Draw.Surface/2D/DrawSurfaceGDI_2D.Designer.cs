namespace DynVis.Draw.Surface
{
    partial class DrawSurfaceGDI_2D
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
            this.drawBox = new DynVis.Draw.DrawBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemCounterCount = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripTextBoxCounterCount = new System.Windows.Forms.ToolStripTextBox();
            this.ToolStripMenuItemSurfaceMode = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemContuor = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemContuorColor = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemContuorGradient = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemGradient = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSetProportion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawBox
            // 
            this.drawBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.drawBox.Location = new System.Drawing.Point(3, 27);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(576, 468);
            this.drawBox.TabIndex = 0;
            this.drawBox.Paint += new System.Windows.Forms.PaintEventHandler(this.drawBox_Paint);
            this.drawBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseMove);
            this.drawBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseDoubleClick);
            this.drawBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawBox_MouseDown);
            this.drawBox.SizeChanged += new System.EventHandler(this.drawBox_SizeChanged);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemView});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(582, 24);
            this.menuStrip.TabIndex = 1;
            // 
            // ToolStripMenuItemView
            // 
            this.ToolStripMenuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemCounterCount,
            this.ToolStripMenuItemSurfaceMode,
            this.toolStripSeparator1,
            this.ToolStripMenuItemSetProportion});
            this.ToolStripMenuItemView.Name = "ToolStripMenuItemView";
            this.ToolStripMenuItemView.Size = new System.Drawing.Size(39, 20);
            this.ToolStripMenuItemView.Text = "Вид";
            this.ToolStripMenuItemView.DropDownOpening += new System.EventHandler(this.contextMenuStrip_Opening);
            // 
            // ToolStripMenuItemCounterCount
            // 
            this.ToolStripMenuItemCounterCount.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxCounterCount});
            this.ToolStripMenuItemCounterCount.Name = "ToolStripMenuItemCounterCount";
            this.ToolStripMenuItemCounterCount.Size = new System.Drawing.Size(242, 22);
            this.ToolStripMenuItemCounterCount.Text = "Контуры";
            // 
            // toolStripTextBoxCounterCount
            // 
            this.toolStripTextBoxCounterCount.Name = "toolStripTextBoxCounterCount";
            this.toolStripTextBoxCounterCount.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBoxCounterCount.TextChanged += new System.EventHandler(this.toolStripTextBoxCounterCount_TextChanged);
            // 
            // ToolStripMenuItemSurfaceMode
            // 
            this.ToolStripMenuItemSurfaceMode.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemContuor,
            this.ToolStripMenuItemContuorColor,
            this.ToolStripMenuItemContuorGradient,
            this.ToolStripMenuItemGradient});
            this.ToolStripMenuItemSurfaceMode.Name = "ToolStripMenuItemSurfaceMode";
            this.ToolStripMenuItemSurfaceMode.Size = new System.Drawing.Size(242, 22);
            this.ToolStripMenuItemSurfaceMode.Text = "Стиль поверхности";
            // 
            // ToolStripMenuItemContuor
            // 
            this.ToolStripMenuItemContuor.Name = "ToolStripMenuItemContuor";
            this.ToolStripMenuItemContuor.Size = new System.Drawing.Size(199, 22);
            this.ToolStripMenuItemContuor.Text = "Контуры";
            this.ToolStripMenuItemContuor.Click += new System.EventHandler(this.ToolStripMenuItemContuor_Click);
            // 
            // ToolStripMenuItemContuorColor
            // 
            this.ToolStripMenuItemContuorColor.Name = "ToolStripMenuItemContuorColor";
            this.ToolStripMenuItemContuorColor.Size = new System.Drawing.Size(199, 22);
            this.ToolStripMenuItemContuorColor.Text = "Контуры с цветом";
            this.ToolStripMenuItemContuorColor.Click += new System.EventHandler(this.ToolStripMenuItemContuorColor_Click);
            // 
            // ToolStripMenuItemContuorGradient
            // 
            this.ToolStripMenuItemContuorGradient.Name = "ToolStripMenuItemContuorGradient";
            this.ToolStripMenuItemContuorGradient.Size = new System.Drawing.Size(199, 22);
            this.ToolStripMenuItemContuorGradient.Text = "Контуры с градиентом";
            this.ToolStripMenuItemContuorGradient.Click += new System.EventHandler(this.ToolStripMenuItemContuorGradient_Click);
            // 
            // ToolStripMenuItemGradient
            // 
            this.ToolStripMenuItemGradient.Name = "ToolStripMenuItemGradient";
            this.ToolStripMenuItemGradient.Size = new System.Drawing.Size(199, 22);
            this.ToolStripMenuItemGradient.Text = "Градиент";
            this.ToolStripMenuItemGradient.Click += new System.EventHandler(this.ToolStripMenuItemGradient_Click);
            // 
            // ToolStripMenuItemSetProportion
            // 
            this.ToolStripMenuItemSetProportion.Name = "ToolStripMenuItemSetProportion";
            this.ToolStripMenuItemSetProportion.Size = new System.Drawing.Size(242, 22);
            this.ToolStripMenuItemSetProportion.Text = "Установить пропорционально";
            this.ToolStripMenuItemSetProportion.Click += new System.EventHandler(this.ToolStripMenuItemSetProportion_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(239, 6);
            // 
            // DrawSurfaceGDI_2D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.drawBox);
            this.Name = "DrawSurfaceGDI_2D";
            this.Size = new System.Drawing.Size(582, 498);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DrawBox drawBox;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemView;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemCounterCount;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxCounterCount;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSurfaceMode;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemContuor;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemContuorColor;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemContuorGradient;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemGradient;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSetProportion;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}
