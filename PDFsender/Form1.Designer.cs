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
            this.TestRun = new System.Windows.Forms.Button();
            this.file1 = new System.Windows.Forms.Label();
            this.addtotitle1 = new System.Windows.Forms.TextBox();
            this.LoadBar = new System.Windows.Forms.ProgressBar();
            this.startButton = new System.Windows.Forms.Button();
            this.chooseFile = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // D
            // 
            this.D.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.D.Location = new System.Drawing.Point(131, 212);
            this.D.Name = "D";
            this.D.Size = new System.Drawing.Size(194, 30);
            this.D.TabIndex = 18;
            this.D.Text = "מיקום מאגר המידע";
            this.D.UseVisualStyleBackColor = true;
            this.D.Click += new System.EventHandler(this.D_Click_1);
            // 
            // DAPI
            // 
            this.DAPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.DAPI.Location = new System.Drawing.Point(136, 79);
            this.DAPI.Name = "DAPI";
            this.DAPI.Size = new System.Drawing.Size(180, 52);
            this.DAPI.TabIndex = 17;
            this.DAPI.Text = "שלח חשבוניות מס";
            this.DAPI.UseVisualStyleBackColor = true;
            this.DAPI.Click += new System.EventHandler(this.DAPI_Click);
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
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // TestRun
            // 
            this.TestRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.TestRun.Location = new System.Drawing.Point(12, 263);
            this.TestRun.Name = "TestRun";
            this.TestRun.Size = new System.Drawing.Size(77, 29);
            this.TestRun.TabIndex = 15;
            this.TestRun.Text = "SendToAll";
            this.TestRun.UseVisualStyleBackColor = true;
            this.TestRun.Click += new System.EventHandler(this.TestRun_Click);
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
            this.LoadBar.Location = new System.Drawing.Point(107, 221);
            this.LoadBar.Name = "LoadBar";
            this.LoadBar.Size = new System.Drawing.Size(257, 42);
            this.LoadBar.Step = 1;
            this.LoadBar.TabIndex = 12;
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.startButton.Location = new System.Drawing.Point(223, 154);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(141, 52);
            this.startButton.TabIndex = 11;
            this.startButton.Text = "Send";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // chooseFile
            // 
            this.chooseFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.chooseFile.Location = new System.Drawing.Point(55, 154);
            this.chooseFile.Name = "chooseFile";
            this.chooseFile.Size = new System.Drawing.Size(141, 52);
            this.chooseFile.TabIndex = 19;
            this.chooseFile.Text = "File";
            this.chooseFile.UseVisualStyleBackColor = true;
            this.chooseFile.Click += new System.EventHandler(this.chooseFile_Click);
            // 
            // FilePath
            // 
            this.FilePath.AutoSize = true;
            this.FilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.FilePath.Location = new System.Drawing.Point(12, 97);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(102, 20);
            this.FilePath.TabIndex = 20;
            this.FilePath.Text = "לא נבחר קובץ";
            // 
            // PDFsender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 311);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.chooseFile);
            this.Controls.Add(this.D);
            this.Controls.Add(this.DAPI);
            this.Controls.Add(this.test);
            this.Controls.Add(this.TestRun);
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
        private System.Windows.Forms.Button TestRun;
        private System.Windows.Forms.Label file1;
        private System.Windows.Forms.TextBox addtotitle1;
        private System.Windows.Forms.ProgressBar LoadBar;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button chooseFile;
        private System.Windows.Forms.Label FilePath;
    }
}

