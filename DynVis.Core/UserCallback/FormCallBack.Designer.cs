namespace DynVis.Core.UserCallback
{
    partial class FormCallBack
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCallBack));
            this.userMessage = new DynVis.Core.UserCallBack.UserMessage();
            this.labelTittle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // userMessage
            // 
            this.userMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.userMessage.Location = new System.Drawing.Point(2, 90);
            this.userMessage.MessageText = "";
            this.userMessage.Name = "userMessage";
            this.userMessage.Size = new System.Drawing.Size(512, 267);
            this.userMessage.TabIndex = 0;
            this.userMessage.Tittle = "Текст Вашего послания:";
            this.userMessage.UserMail = "";
            this.userMessage.SendMessage += new System.EventHandler(this.userMessage_SendMessage);
            // 
            // labelTittle
            // 
            this.labelTittle.AutoSize = true;
            this.labelTittle.Location = new System.Drawing.Point(12, 9);
            this.labelTittle.Name = "labelTittle";
            this.labelTittle.Size = new System.Drawing.Size(445, 78);
            this.labelTittle.TabIndex = 1;
            this.labelTittle.Text = resources.GetString("labelTittle.Text");
            // 
            // FormCallBack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 361);
            this.Controls.Add(this.labelTittle);
            this.Controls.Add(this.userMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCallBack";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Форма обратной связи";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DynVis.Core.UserCallBack.UserMessage userMessage;
        private System.Windows.Forms.Label labelTittle;
    }
}