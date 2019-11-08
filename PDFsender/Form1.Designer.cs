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
            this.D = new System.Windows.Forms.Button();
            this.DAPI = new System.Windows.Forms.Button();
            this.test = new System.Windows.Forms.Button();
            this.file1 = new System.Windows.Forms.Label();
            this.addtotitle1 = new System.Windows.Forms.TextBox();
            this.LoadBar = new System.Windows.Forms.ProgressBar();
            this.startButton = new System.Windows.Forms.Button();
            this.chooseFile = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.Label();
            this.Approve_send = new System.Windows.Forms.Button();
            this.Cencel_send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // D
            // 
            this.D.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.D.Location = new System.Drawing.Point(136, 176);
            this.D.Name = "D";
            this.D.Size = new System.Drawing.Size(194, 30);
            this.D.TabIndex = 18;
            this.D.Text = "מיקום מאגר המידע";
            this.D.UseVisualStyleBackColor = true;
            this.D.Click += new System.EventHandler(this.DatabasePath_Click);
            // 
            // DAPI
            // 
            this.DAPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.DAPI.Location = new System.Drawing.Point(150, 79);
            this.DAPI.Name = "DAPI";
            this.DAPI.Size = new System.Drawing.Size(169, 52);
            this.DAPI.TabIndex = 17;
            this.DAPI.Text = "התחל";
            this.DAPI.UseVisualStyleBackColor = true;
            this.DAPI.Click += new System.EventHandler(this.LoadMain_Click);
            // 
            // test
            // 
            this.test.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.test.Location = new System.Drawing.Point(392, 261);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(66, 30);
            this.test.TabIndex = 16;
            this.test.Text = "test";
            this.test.UseVisualStyleBackColor = true;
            this.test.Click += new System.EventHandler(this.Test_Click);
            // 
            // file1
            // 
            this.file1.AutoSize = true;
            this.file1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.file1.Location = new System.Drawing.Point(313, 21);
            this.file1.Name = "file1";
            this.file1.Size = new System.Drawing.Size(96, 20);
            this.file1.TabIndex = 14;
            this.file1.Text = "הוסף לכותרת";
            // 
            // addtotitle1
            // 
            this.addtotitle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.addtotitle1.Location = new System.Drawing.Point(16, 21);
            this.addtotitle1.Name = "addtotitle1";
            this.addtotitle1.Size = new System.Drawing.Size(286, 29);
            this.addtotitle1.TabIndex = 13;
            // 
            // LoadBar
            // 
            this.LoadBar.Location = new System.Drawing.Point(101, 234);
            this.LoadBar.Name = "LoadBar";
            this.LoadBar.Size = new System.Drawing.Size(257, 42);
            this.LoadBar.Step = 1;
            this.LoadBar.TabIndex = 12;
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.startButton.Location = new System.Drawing.Point(150, 176);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(141, 52);
            this.startButton.TabIndex = 11;
            this.startButton.Text = "Send";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.Start_Click);
            // 
            // chooseFile
            // 
            this.chooseFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.chooseFile.Location = new System.Drawing.Point(150, 118);
            this.chooseFile.Name = "chooseFile";
            this.chooseFile.Size = new System.Drawing.Size(141, 52);
            this.chooseFile.TabIndex = 19;
            this.chooseFile.Text = "Choose File";
            this.chooseFile.UseVisualStyleBackColor = true;
            this.chooseFile.Click += new System.EventHandler(this.ChooseFile_Click);
            // 
            // FilePath
            // 
            this.FilePath.AutoSize = true;
            this.FilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FilePath.Location = new System.Drawing.Point(12, 79);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(102, 20);
            this.FilePath.TabIndex = 20;
            this.FilePath.Text = "לא נבחר קובץ";
            // 
            // Approve_send
            // 
            this.Approve_send.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Approve_send.Location = new System.Drawing.Point(274, 118);
            this.Approve_send.Name = "Approve_send";
            this.Approve_send.Size = new System.Drawing.Size(169, 52);
            this.Approve_send.TabIndex = 21;
            this.Approve_send.Text = "OK";
            this.Approve_send.UseVisualStyleBackColor = true;
            this.Approve_send.Click += new System.EventHandler(this.Approve_send_Click);
            // 
            // Cencel_send
            // 
            this.Cencel_send.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Cencel_send.Location = new System.Drawing.Point(12, 118);
            this.Cencel_send.Name = "Cencel_send";
            this.Cencel_send.Size = new System.Drawing.Size(169, 52);
            this.Cencel_send.TabIndex = 22;
            this.Cencel_send.Text = "Cencel";
            this.Cencel_send.UseVisualStyleBackColor = true;
            this.Cencel_send.Click += new System.EventHandler(this.Cencel_send_Click);
            // 
            // PDFsender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 311);
            this.Controls.Add(this.Cencel_send);
            this.Controls.Add(this.Approve_send);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.chooseFile);
            this.Controls.Add(this.D);
            this.Controls.Add(this.DAPI);
            this.Controls.Add(this.test);
            this.Controls.Add(this.file1);
            this.Controls.Add(this.addtotitle1);
            this.Controls.Add(this.LoadBar);
            this.Controls.Add(this.startButton);
            this.Name = "PDFsender";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button D;
        private System.Windows.Forms.Button DAPI;
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.Label file1;
        private System.Windows.Forms.TextBox addtotitle1;
        private System.Windows.Forms.ProgressBar LoadBar;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button chooseFile;
        private System.Windows.Forms.Label FilePath;
        private System.Windows.Forms.Button Approve_send;
        private System.Windows.Forms.Button Cencel_send;
    }
}

