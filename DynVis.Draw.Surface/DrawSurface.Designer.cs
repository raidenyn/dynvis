namespace DynVis.Draw.Surface
{
    partial class DrawSurface
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
            this.panelDrawSurface = new System.Windows.Forms.Panel();
            this.button2D = new System.Windows.Forms.Button();
            this.drawSurfaceGDI_2D = new DynVis.Draw.Surface.DrawSurfaceGDI_2D();
            this.drawSurfaceGL_3D = new DynVis.Draw.Surface.DrawSurfaceGL_3D();
            this.button3D = new System.Windows.Forms.Button();
            this.buttonSplit = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.panelDrawSurface.SuspendLayout();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelDrawSurface
            // 
            this.panelDrawSurface.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDrawSurface.Controls.Add(this.splitContainer);
            this.panelDrawSurface.Location = new System.Drawing.Point(3, 32);
            this.panelDrawSurface.Name = "panelDrawSurface";
            this.panelDrawSurface.Size = new System.Drawing.Size(763, 594);
            this.panelDrawSurface.TabIndex = 2;
            // 
            // button2D
            // 
            this.button2D.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2D.Location = new System.Drawing.Point(3, 3);
            this.button2D.Name = "button2D";
            this.button2D.Size = new System.Drawing.Size(44, 23);
            this.button2D.TabIndex = 3;
            this.button2D.Text = "2D";
            this.button2D.UseVisualStyleBackColor = true;
            this.button2D.Click += new System.EventHandler(this.button2D_Click);
            // 
            // drawSurfaceGDI_2D
            // 
            this.drawSurfaceGDI_2D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawSurfaceGDI_2D.Location = new System.Drawing.Point(0, 0);
            this.drawSurfaceGDI_2D.Name = "drawSurfaceGDI_2D";
            this.drawSurfaceGDI_2D.Size = new System.Drawing.Size(369, 594);
            this.drawSurfaceGDI_2D.TabIndex = 0;
            // 
            // drawSurfaceGL_3D
            // 
            this.drawSurfaceGL_3D.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawSurfaceGL_3D.Location = new System.Drawing.Point(0, 0);
            this.drawSurfaceGL_3D.Name = "drawSurfaceGL_3D";
            this.drawSurfaceGL_3D.Size = new System.Drawing.Size(390, 594);
            this.drawSurfaceGL_3D.TabIndex = 1;
            // 
            // button3D
            // 
            this.button3D.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3D.Location = new System.Drawing.Point(53, 3);
            this.button3D.Name = "button3D";
            this.button3D.Size = new System.Drawing.Size(44, 23);
            this.button3D.TabIndex = 3;
            this.button3D.Text = "3D";
            this.button3D.UseVisualStyleBackColor = true;
            this.button3D.Click += new System.EventHandler(this.button3D_Click);
            // 
            // buttonSplit
            // 
            this.buttonSplit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSplit.Location = new System.Drawing.Point(103, 3);
            this.buttonSplit.Name = "buttonSplit";
            this.buttonSplit.Size = new System.Drawing.Size(57, 23);
            this.buttonSplit.TabIndex = 3;
            this.buttonSplit.Text = "Split";
            this.buttonSplit.UseVisualStyleBackColor = true;
            this.buttonSplit.Click += new System.EventHandler(this.buttonSplit_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.drawSurfaceGDI_2D);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.drawSurfaceGL_3D);
            this.splitContainer.Size = new System.Drawing.Size(763, 594);
            this.splitContainer.SplitterDistance = 369;
            this.splitContainer.TabIndex = 2;
            // 
            // DrawSurface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSplit);
            this.Controls.Add(this.button3D);
            this.Controls.Add(this.button2D);
            this.Controls.Add(this.panelDrawSurface);
            this.Name = "DrawSurface";
            this.Size = new System.Drawing.Size(766, 629);
            this.panelDrawSurface.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DrawSurfaceGDI_2D drawSurfaceGDI_2D;
        private DrawSurfaceGL_3D drawSurfaceGL_3D;
        private System.Windows.Forms.Panel panelDrawSurface;
        private System.Windows.Forms.Button button2D;
        private System.Windows.Forms.Button button3D;
        private System.Windows.Forms.Button buttonSplit;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}
