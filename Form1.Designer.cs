namespace RuRu_Comms
{
    partial class Form1
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
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnConnectToServer;
        private System.Windows.Forms.Button btnSendMessage;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btnConnectToServer = new System.Windows.Forms.Button();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.IPLabel = new System.Windows.Forms.Label();
            this.IPText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(12, 84);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(360, 200);
            this.txtLog.TabIndex = 2;
            // 
            // btnConnectToServer
            // 
            this.btnConnectToServer.Location = new System.Drawing.Point(12, 43);
            this.btnConnectToServer.Name = "btnConnectToServer";
            this.btnConnectToServer.Size = new System.Drawing.Size(150, 27);
            this.btnConnectToServer.TabIndex = 3;
            this.btnConnectToServer.Text = "Connect!";
            this.btnConnectToServer.UseVisualStyleBackColor = true;
            this.btnConnectToServer.Click += new System.EventHandler(this.btnConnectToServer_Click);
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(222, 43);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(150, 27);
            this.btnSendMessage.TabIndex = 4;
            this.btnSendMessage.Text = "Say Hello";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // IPLabel
            // 
            this.IPLabel.AutoSize = true;
            this.IPLabel.Location = new System.Drawing.Point(12, 14);
            this.IPLabel.Name = "IPLabel";
            this.IPLabel.Size = new System.Drawing.Size(115, 20);
            this.IPLabel.TabIndex = 5;
            this.IPLabel.Text = "Magic Number:";
            this.IPLabel.Click += new System.EventHandler(this.IPLabel_Click);
            // 
            // IPText
            // 
            this.IPText.Location = new System.Drawing.Point(133, 11);
            this.IPText.Name = "IPText";
            this.IPText.Size = new System.Drawing.Size(100, 26);
            this.IPText.TabIndex = 6;
            this.IPText.TextChanged += new System.EventHandler(this.IPText_TextChanged);
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(389, 296);
            this.Controls.Add(this.IPText);
            this.Controls.Add(this.IPLabel);
            this.Controls.Add(this.btnConnectToServer);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.txtLog);
            this.Name = "Form1";
            this.Text = "Peer-to-Peer App";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IPLabel;
        private System.Windows.Forms.TextBox IPText;
    }
}

