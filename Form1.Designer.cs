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
        private System.Windows.Forms.Button btnConnectToServer;
        private System.Windows.Forms.Button btnSendMessage;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConnectToServer = new System.Windows.Forms.Button();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.IPText = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.saveIP = new System.Windows.Forms.Button();
            this.loadIP = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.displayMesg1 = new System.Windows.Forms.TextBox();
            this.displayMesg0 = new System.Windows.Forms.TextBox();
            this.displayFeelingButton0 = new System.Windows.Forms.Button();
            this.displayFeelingButton1 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.feelingWheelPanel = new System.Windows.Forms.Panel();
            this.welcomeLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConnectToServer
            // 
            this.btnConnectToServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnConnectToServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnectToServer.Location = new System.Drawing.Point(3, 50);
            this.btnConnectToServer.Name = "btnConnectToServer";
            this.btnConnectToServer.Size = new System.Drawing.Size(362, 41);
            this.btnConnectToServer.TabIndex = 3;
            this.btnConnectToServer.Text = "Connect!";
            this.btnConnectToServer.UseVisualStyleBackColor = true;
            this.btnConnectToServer.Click += new System.EventHandler(this.btnConnectToServer_Click);
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSendMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMessage.Location = new System.Drawing.Point(371, 50);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(362, 41);
            this.btnSendMessage.TabIndex = 4;
            this.btnSendMessage.Text = "Message";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tabControl1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(742, 579);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.btnConnectToServer, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.btnSendMessage, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.splitContainer2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.welcomeLabel, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(736, 94);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(3, 3);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.IPText);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tableLayoutPanel4);
            this.splitContainer2.Size = new System.Drawing.Size(362, 41);
            this.splitContainer2.SplitterDistance = 120;
            this.splitContainer2.TabIndex = 9;
            // 
            // IPText
            // 
            this.IPText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.IPText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IPText.Location = new System.Drawing.Point(0, 0);
            this.IPText.Name = "IPText";
            this.IPText.Size = new System.Drawing.Size(120, 30);
            this.IPText.TabIndex = 8;
            this.IPText.TextChanged += new System.EventHandler(this.IPText_TextChanged_1);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.saveIP, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.loadIP, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(238, 41);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // saveIP
            // 
            this.saveIP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.saveIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveIP.Location = new System.Drawing.Point(122, 3);
            this.saveIP.Name = "saveIP";
            this.saveIP.Size = new System.Drawing.Size(113, 35);
            this.saveIP.TabIndex = 11;
            this.saveIP.Text = "Save #";
            this.saveIP.UseVisualStyleBackColor = true;
            this.saveIP.Click += new System.EventHandler(this.saveIP_Click);
            // 
            // loadIP
            // 
            this.loadIP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loadIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadIP.Location = new System.Drawing.Point(3, 3);
            this.loadIP.Name = "loadIP";
            this.loadIP.Size = new System.Drawing.Size(113, 35);
            this.loadIP.TabIndex = 2;
            this.loadIP.Text = "Load #";
            this.loadIP.UseVisualStyleBackColor = true;
            this.loadIP.Click += new System.EventHandler(this.loadIP_Click_1);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(3, 103);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(736, 473);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(759, 453);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Nerd Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 3);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(753, 447);
            this.txtLog.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(728, 431);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Messages";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.displayMesg1, 1, 1);
            this.tableLayoutPanel3.Controls.Add(this.displayMesg0, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.displayFeelingButton0, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.displayFeelingButton1, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(722, 425);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // displayMesg1
            // 
            this.displayMesg1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayMesg1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.displayMesg1.Location = new System.Drawing.Point(364, 73);
            this.displayMesg1.Multiline = true;
            this.displayMesg1.Name = "displayMesg1";
            this.displayMesg1.ReadOnly = true;
            this.displayMesg1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.displayMesg1.Size = new System.Drawing.Size(355, 349);
            this.displayMesg1.TabIndex = 7;
            // 
            // displayMesg0
            // 
            this.displayMesg0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayMesg0.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayMesg0.Location = new System.Drawing.Point(3, 73);
            this.displayMesg0.Multiline = true;
            this.displayMesg0.Name = "displayMesg0";
            this.displayMesg0.ReadOnly = true;
            this.displayMesg0.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.displayMesg0.Size = new System.Drawing.Size(355, 349);
            this.displayMesg0.TabIndex = 6;
            // 
            // displayFeelingButton0
            // 
            this.displayFeelingButton0.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayFeelingButton0.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayFeelingButton0.Location = new System.Drawing.Point(3, 3);
            this.displayFeelingButton0.Name = "displayFeelingButton0";
            this.displayFeelingButton0.Size = new System.Drawing.Size(355, 64);
            this.displayFeelingButton0.TabIndex = 0;
            this.displayFeelingButton0.Text = "You";
            this.displayFeelingButton0.UseVisualStyleBackColor = true;
            this.displayFeelingButton0.Click += new System.EventHandler(this.displayFeelingButton0_Click);
            // 
            // displayFeelingButton1
            // 
            this.displayFeelingButton1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.displayFeelingButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayFeelingButton1.Location = new System.Drawing.Point(364, 3);
            this.displayFeelingButton1.Name = "displayFeelingButton1";
            this.displayFeelingButton1.Size = new System.Drawing.Size(355, 64);
            this.displayFeelingButton1.TabIndex = 1;
            this.displayFeelingButton1.Text = "Me";
            this.displayFeelingButton1.UseVisualStyleBackColor = true;
            this.displayFeelingButton1.Click += new System.EventHandler(this.displayFeelingButton1_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.feelingWheelPanel);
            this.tabPage3.Location = new System.Drawing.Point(4, 38);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(759, 453);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Feeling Wheel";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // feelingWheelPanel
            // 
            this.feelingWheelPanel.BackColor = System.Drawing.Color.White;
            this.feelingWheelPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.feelingWheelPanel.Location = new System.Drawing.Point(3, 3);
            this.feelingWheelPanel.Name = "feelingWheelPanel";
            this.feelingWheelPanel.Size = new System.Drawing.Size(753, 447);
            this.feelingWheelPanel.TabIndex = 0;
            this.feelingWheelPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.FeelingWheelPanel_Paint);
            this.feelingWheelPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FeelingWheelPanel_MouseClick);
            this.feelingWheelPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FeelingWheelPanel_MouseMove);
            // 
            // welcomeLabel
            // 
            this.welcomeLabel.AutoSize = true;
            this.welcomeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.welcomeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcomeLabel.Location = new System.Drawing.Point(371, 0);
            this.welcomeLabel.Name = "welcomeLabel";
            this.welcomeLabel.Size = new System.Drawing.Size(362, 47);
            this.welcomeLabel.TabIndex = 10;
            this.welcomeLabel.Text = "Welcome";
            this.welcomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(742, 579);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "RuRu Comms v1.1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox IPText;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TextBox displayMesg1;
        private System.Windows.Forms.TextBox displayMesg0;
        private System.Windows.Forms.Button displayFeelingButton0;
        private System.Windows.Forms.Button displayFeelingButton1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Panel feelingWheelPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Button saveIP;
        private System.Windows.Forms.Button loadIP;
        private System.Windows.Forms.Label welcomeLabel;
    }
}

