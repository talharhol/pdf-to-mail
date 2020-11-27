using ChooseName;

namespace pdfScanner
{
    partial class PDFsender
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PDFsender));
            this.test = new System.Windows.Forms.Button();
            this.addtotitle1 = new System.Windows.Forms.TextBox();
            this.LoadBar = new System.Windows.Forms.ProgressBar();
            this.startButton = new System.Windows.Forms.Button();
            this.config = new System.Windows.Forms.Button();
            this.Approve_send = new System.Windows.Forms.Button();
            this.Cencel_send = new System.Windows.Forms.Button();
            this.draftClick = new System.Windows.Forms.Button();
            this.logger = new System.Windows.Forms.Label();
            this.logHistory = new System.Windows.Forms.Label();
            this.LogHistoryContainer = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.LogHistoryContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // test
            // 
            this.test.Font = new System.Drawing.Font("Levenim MT", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.test.Location = new System.Drawing.Point(393, 228);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(66, 30);
            this.test.TabIndex = 16;
            this.test.Text = "Test";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.Test_Click);
            // 
            // addtotitle1
            // 
            this.addtotitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.addtotitle1.Location = new System.Drawing.Point(100, 44);
            this.addtotitle1.Name = "addtotitle1";
            this.addtotitle1.Size = new System.Drawing.Size(272, 29);
            this.addtotitle1.TabIndex = 13;
            // 
            // LoadBar
            // 
            this.LoadBar.BackColor = System.Drawing.Color.Sienna;
            this.LoadBar.Location = new System.Drawing.Point(12, 273);
            this.LoadBar.Margin = new System.Windows.Forms.Padding(0);
            this.LoadBar.Name = "LoadBar";
            this.LoadBar.Size = new System.Drawing.Size(447, 18);
            this.LoadBar.Step = 1;
            this.LoadBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.LoadBar.TabIndex = 12;
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Levenim MT", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.startButton.Location = new System.Drawing.Point(165, 176);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(141, 52);
            this.startButton.TabIndex = 11;
            this.startButton.Text = "Send";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.Start_Click);
            // 
            // config
            // 
            this.config.Font = new System.Drawing.Font("Levenim MT", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.config.Location = new System.Drawing.Point(165, 118);
            this.config.Name = "config";
            this.config.Size = new System.Drawing.Size(141, 52);
            this.config.TabIndex = 19;
            this.config.Text = "Config";
            this.config.UseVisualStyleBackColor = true;
            this.config.Click += new System.EventHandler(this.config_Click);
            // 
            // Approve_send
            // 
            this.Approve_send.Font = new System.Drawing.Font("Levenim MT", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Approve_send.Location = new System.Drawing.Point(150, 239);
            this.Approve_send.Name = "Approve_send";
            this.Approve_send.Size = new System.Drawing.Size(169, 52);
            this.Approve_send.TabIndex = 21;
            this.Approve_send.Text = "Send";
            this.Approve_send.UseVisualStyleBackColor = true;
            this.Approve_send.Click += new System.EventHandler(this.Approve_send_Click);
            // 
            // Cencel_send
            // 
            this.Cencel_send.Font = new System.Drawing.Font("Levenim MT", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cencel_send.Location = new System.Drawing.Point(150, 88);
            this.Cencel_send.Name = "Cencel_send";
            this.Cencel_send.Size = new System.Drawing.Size(169, 52);
            this.Cencel_send.TabIndex = 22;
            this.Cencel_send.Text = "Cancel";
            this.Cencel_send.UseVisualStyleBackColor = true;
            this.Cencel_send.Click += new System.EventHandler(this.Cencel_send_Click);
            // 
            // draftClick
            // 
            this.draftClick.Font = new System.Drawing.Font("Levenim MT", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.draftClick.Location = new System.Drawing.Point(12, 228);
            this.draftClick.Name = "draftClick";
            this.draftClick.Size = new System.Drawing.Size(66, 30);
            this.draftClick.TabIndex = 25;
            this.draftClick.Text = "Draft";
            this.draftClick.UseVisualStyleBackColor = true;
            this.draftClick.Click += new System.EventHandler(this.DraftClick_Click);
            // 
            // logger
            // 
            this.logger.BackColor = System.Drawing.Color.Transparent;
            this.logger.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.logger.ForeColor = System.Drawing.Color.Maroon;
            this.logger.Location = new System.Drawing.Point(0, 293);
            this.logger.Name = "logger";
            this.logger.Size = new System.Drawing.Size(472, 19);
            this.logger.TabIndex = 28;
            this.logger.MouseLeave += new System.EventHandler(this.logger_MouseLeave);
            this.logger.MouseHover += new System.EventHandler(this.logger_MouseHover);
            this.logger.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.logger_MouseWheel);
            // 
            // logHistory
            // 
            this.logHistory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.logHistory.BackColor = System.Drawing.Color.Transparent;
            this.logHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.logHistory.ForeColor = System.Drawing.Color.Maroon;
            this.logHistory.Location = new System.Drawing.Point(21, 100);
            this.logHistory.Name = "logHistory";
            this.logHistory.Size = new System.Drawing.Size(447, 16);
            this.logHistory.TabIndex = 29;
            // 
            // LogHistoryContainer
            // 
            this.LogHistoryContainer.BackColor = System.Drawing.Color.LemonChiffon;
            this.LogHistoryContainer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("LogHistoryContainer.BackgroundImage")));
            this.LogHistoryContainer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LogHistoryContainer.Location = new System.Drawing.Point(0, 176);
            this.LogHistoryContainer.Margin = new System.Windows.Forms.Padding(0);
            this.LogHistoryContainer.Name = "LogHistoryContainer";
            this.LogHistoryContainer.Size = new System.Drawing.Size(469, 117);
            this.LogHistoryContainer.TabIndex = 30;
            this.LogHistoryContainer.TabStop = false;
            // 
            // PDFsender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(471, 311);
            this.Controls.Add(this.LogHistoryContainer);
            this.Controls.Add(this.logHistory);
            this.Controls.Add(this.logger);
            this.Controls.Add(this.draftClick);
            this.Controls.Add(this.Cencel_send);
            this.Controls.Add(this.Approve_send);
            this.Controls.Add(this.config);
            this.Controls.Add(this.test);
            this.Controls.Add(this.addtotitle1);
            this.Controls.Add(this.LoadBar);
            this.Controls.Add(this.startButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "PDFsender";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form1";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PDFsender_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PDFsender_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.LogHistoryContainer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.TextBox addtotitle1;
        private System.Windows.Forms.ProgressBar LoadBar;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button config;
        private System.Windows.Forms.Button Approve_send;
        private System.Windows.Forms.Button Cencel_send;
        private System.Windows.Forms.Button draftClick;
        private System.Windows.Forms.Label logger;
        private System.Windows.Forms.Label logHistory;
        private System.Windows.Forms.PictureBox LogHistoryContainer;
    }
}

