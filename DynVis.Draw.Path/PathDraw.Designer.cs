namespace DynVis.Draw.Path
{
    partial class PathDraw
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
            this.trackBarReactionCoord = new System.Windows.Forms.TrackBar();
            this.buttonMove = new System.Windows.Forms.Button();
            this.buttonMenuConfig = new System.Windows.Forms.Button();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripTextBoxStepCount = new System.Windows.Forms.ToolStripTextBox();
            this.drawBox = new DynVis.Draw.DrawBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarReactionCoord)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // trackBarReactionCoord
            // 
            this.trackBarReactionCoord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBarReactionCoord.Location = new System.Drawing.Point(0, 359);
            this.trackBarReactionCoord.Maximum = 1000;
            this.trackBarReactionCoord.Name = "trackBarReactionCoord";
            this.trackBarReactionCoord.Size = new System.Drawing.Size(603, 45);
            this.trackBarReactionCoord.TabIndex = 1;
            this.trackBarReactionCoord.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBarReactionCoord.Scroll += new System.EventHandler(this.trackBarReactionCoord_Scroll);
            // 
            // buttonMove
            // 
            this.buttonMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonMove.Location = new System.Drawing.Point(462, 395);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(119, 23);
            this.buttonMove.TabIndex = 2;
            this.buttonMove.Text = "Показать движение";
            this.buttonMove.UseVisualStyleBackColor = true;
            this.buttonMove.Click += new System.EventHandler(this.buttonMove_Click);
            // 
            // buttonMenuConfig
            // 
            this.buttonMenuConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMenuConfig.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonMenuConfig.Location = new System.Drawing.Point(580, 395);
            this.buttonMenuConfig.Name = "buttonMenuConfig";
            this.buttonMenuConfig.Size = new System.Drawing.Size(15, 23);
            this.buttonMenuConfig.TabIndex = 4;
            this.buttonMenuConfig.Text = "v";
            this.buttonMenuConfig.UseVisualStyleBackColor = true;
            this.buttonMenuConfig.Click += new System.EventHandler(this.buttonMenuConfig_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBoxStepCount});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.contextMenuStrip.ShowImageMargin = false;
            this.contextMenuStrip.Size = new System.Drawing.Size(136, 51);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // toolStripTextBoxStepCount
            // 
            this.toolStripTextBoxStepCount.MaxLength = 5;
            this.toolStripTextBoxStepCount.Name = "toolStripTextBoxStepCount";
            this.toolStripTextBoxStepCount.Size = new System.Drawing.Size(100, 23);
            this.toolStripTextBoxStepCount.TextChanged += new System.EventHandler(this.toolStripTextBoxStepCount_TextChanged);
            // 
            // drawBox
            // 
            this.drawBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.drawBox.Location = new System.Drawing.Point(0, 3);
            this.drawBox.Name = "drawBox";
            this.drawBox.Size = new System.Drawing.Size(600, 350);
            this.drawBox.TabIndex = 3;
            this.drawBox.TabStop = false;
            this.drawBox.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
            this.drawBox.SizeChanged += new System.EventHandler(this.pictureBox_SizeChanged);
            // 
            // PathDraw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonMenuConfig);
            this.Controls.Add(this.drawBox);
            this.Controls.Add(this.buttonMove);
            this.Controls.Add(this.trackBarReactionCoord);
            this.Name = "PathDraw";
            this.Size = new System.Drawing.Size(603, 421);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarReactionCoord)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.contextMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trackBarReactionCoord;
        private System.Windows.Forms.Button buttonMove;
        private DrawBox drawBox;
        private System.Windows.Forms.Button buttonMenuConfig;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxStepCount;
    }
}
