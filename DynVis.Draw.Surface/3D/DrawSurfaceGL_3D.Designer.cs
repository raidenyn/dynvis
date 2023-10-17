namespace DynVis.Draw.Surface
{
    partial class DrawSurfaceGL_3D
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
            this.drawBoxSurface = new DynVis.Draw.DrawBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolStripMenuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemViewPoint = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemViewLine = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemViewFill = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // drawBoxSurface
            // 
            this.drawBoxSurface.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.drawBoxSurface.IsOpenGL = true;
            this.drawBoxSurface.Location = new System.Drawing.Point(0, 27);
            this.drawBoxSurface.Name = "drawBoxSurface";
            this.drawBoxSurface.Size = new System.Drawing.Size(537, 433);
            this.drawBoxSurface.TabIndex = 1;
            this.drawBoxSurface.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawSurfaceGL_3D_Paint);
            this.drawBoxSurface.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawSurfaceGL_3D_MouseMove);
            this.drawBoxSurface.Click += new System.EventHandler(this.drawBoxSurface_Click);
            this.drawBoxSurface.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawSurfaceGL_3D_MouseDown);
            this.drawBoxSurface.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DrawSurfaceGL_3D_MouseUp);
            this.drawBoxSurface.SizeChanged += new System.EventHandler(this.DrawSurfaceGL_3D_SizeChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemView});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(537, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolStripMenuItemView
            // 
            this.ToolStripMenuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemViewPoint,
            this.ToolStripMenuItemViewLine,
            this.ToolStripMenuItemViewFill});
            this.ToolStripMenuItemView.Name = "ToolStripMenuItemView";
            this.ToolStripMenuItemView.Size = new System.Drawing.Size(39, 20);
            this.ToolStripMenuItemView.Text = "Вид";
            this.ToolStripMenuItemView.DropDownOpening += new System.EventHandler(this.ToolStripMenuItemView_DropDownOpening);
            // 
            // ToolStripMenuItemViewPoint
            // 
            this.ToolStripMenuItemViewPoint.Name = "ToolStripMenuItemViewPoint";
            this.ToolStripMenuItemViewPoint.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemViewPoint.Text = "Точками";
            this.ToolStripMenuItemViewPoint.Click += new System.EventHandler(this.ToolStripMenuItemViewPoint_Click);
            // 
            // ToolStripMenuItemViewLine
            // 
            this.ToolStripMenuItemViewLine.Name = "ToolStripMenuItemViewLine";
            this.ToolStripMenuItemViewLine.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemViewLine.Text = "Линииями";
            this.ToolStripMenuItemViewLine.Click += new System.EventHandler(this.ToolStripMenuItemViewLine_Click);
            // 
            // ToolStripMenuItemViewFill
            // 
            this.ToolStripMenuItemViewFill.Name = "ToolStripMenuItemViewFill";
            this.ToolStripMenuItemViewFill.Size = new System.Drawing.Size(152, 22);
            this.ToolStripMenuItemViewFill.Text = "Сплошная";
            this.ToolStripMenuItemViewFill.Click += new System.EventHandler(this.ToolStripMenuItemViewFill_Click);
            // 
            // DrawSurfaceGL_3D
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.drawBoxSurface);
            this.Controls.Add(this.menuStrip1);
            this.Name = "DrawSurfaceGL_3D";
            this.Size = new System.Drawing.Size(537, 460);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DrawBox drawBoxSurface;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemView;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemViewPoint;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemViewLine;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemViewFill;

    }
}
