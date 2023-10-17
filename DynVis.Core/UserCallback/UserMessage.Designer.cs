namespace DynVis.Core.UserCallBack
{
    partial class UserMessage
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
            this.buttonSend = new System.Windows.Forms.Button();
            this.checkBoxReceiveAnswer = new System.Windows.Forms.CheckBox();
            this.textBoxUserMail = new System.Windows.Forms.TextBox();
            this.textBoxUserMessage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonSend
            // 
            this.buttonSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSend.Location = new System.Drawing.Point(558, 223);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 9;
            this.buttonSend.Text = "Отправить";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // checkBoxReceiveAnswer
            // 
            this.checkBoxReceiveAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxReceiveAnswer.AutoSize = true;
            this.checkBoxReceiveAnswer.Location = new System.Drawing.Point(7, 197);
            this.checkBoxReceiveAnswer.Name = "checkBoxReceiveAnswer";
            this.checkBoxReceiveAnswer.Size = new System.Drawing.Size(213, 17);
            this.checkBoxReceiveAnswer.TabIndex = 8;
            this.checkBoxReceiveAnswer.Text = "Я хочу получить ответ на этот адрес:";
            this.checkBoxReceiveAnswer.UseVisualStyleBackColor = true;
            this.checkBoxReceiveAnswer.CheckedChanged += new System.EventHandler(this.checkBoxReceiveAnswer_CheckedChanged);
            // 
            // textBoxUserMail
            // 
            this.textBoxUserMail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUserMail.Location = new System.Drawing.Point(229, 197);
            this.textBoxUserMail.Name = "textBoxUserMail";
            this.textBoxUserMail.ReadOnly = true;
            this.textBoxUserMail.Size = new System.Drawing.Size(404, 20);
            this.textBoxUserMail.TabIndex = 7;
            // 
            // textBoxUserMessage
            // 
            this.textBoxUserMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxUserMessage.Location = new System.Drawing.Point(3, 26);
            this.textBoxUserMessage.Multiline = true;
            this.textBoxUserMessage.Name = "textBoxUserMessage";
            this.textBoxUserMessage.Size = new System.Drawing.Size(630, 165);
            this.textBoxUserMessage.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(190, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Напишите здесь Ваш комментaрий:";
            // 
            // UserMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.checkBoxReceiveAnswer);
            this.Controls.Add(this.textBoxUserMail);
            this.Controls.Add(this.textBoxUserMessage);
            this.Name = "UserMessage";
            this.Size = new System.Drawing.Size(639, 249);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.CheckBox checkBoxReceiveAnswer;
        private System.Windows.Forms.TextBox textBoxUserMail;
        private System.Windows.Forms.TextBox textBoxUserMessage;
        private System.Windows.Forms.Label label1;
    }
}
