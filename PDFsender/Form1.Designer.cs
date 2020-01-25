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
            this.DataBase = new System.Windows.Forms.Button();
            this.Proceed = new System.Windows.Forms.Button();
            this.test = new System.Windows.Forms.Button();
            this.file1 = new System.Windows.Forms.Label();
            this.addtotitle1 = new System.Windows.Forms.TextBox();
            this.LoadBar = new System.Windows.Forms.ProgressBar();
            this.startButton = new System.Windows.Forms.Button();
            this.chooseFile = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.Label();
            this.Approve_send = new System.Windows.Forms.Button();
            this.Cencel_send = new System.Windows.Forms.Button();
            this.Print = new System.Windows.Forms.Button();
            this.Back = new System.Windows.Forms.Button();
            this.draftClick = new System.Windows.Forms.Button();
            this.CloseForm = new System.Windows.Forms.Button();
            this.logger = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // DataBase
            // 
            this.DataBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.DataBase.Location = new System.Drawing.Point(136, 176);
            this.DataBase.Name = "DataBase";
            this.DataBase.Size = new System.Drawing.Size(194, 30);
            this.DataBase.TabIndex = 18;
            this.DataBase.Text = "מיקום מאגר המידע";
            this.DataBase.UseVisualStyleBackColor = true;
            this.DataBase.Click += new System.EventHandler(this.DatabasePath_Click);
            // 
            // Proceed
            // 
            this.Proceed.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Proceed.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Proceed.Location = new System.Drawing.Point(150, 79);
            this.Proceed.Name = "Proceed";
            this.Proceed.Size = new System.Drawing.Size(169, 52);
            this.Proceed.TabIndex = 17;
            this.Proceed.Text = "התחל";
            this.Proceed.UseVisualStyleBackColor = true;
            this.Proceed.Click += new System.EventHandler(this.LoadMain_Click);
            // 
            // test
            // 
            this.test.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.test.Location = new System.Drawing.Point(393, 220);
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
            this.Cencel_send.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Cencel_send.Location = new System.Drawing.Point(150, 88);
            this.Cencel_send.Name = "Cencel_send";
            this.Cencel_send.Size = new System.Drawing.Size(169, 52);
            this.Cencel_send.TabIndex = 22;
            this.Cencel_send.Text = "Cencel";
            this.Cencel_send.UseVisualStyleBackColor = true;
            this.Cencel_send.Click += new System.EventHandler(this.Cencel_send_Click);
            // 
            // Print
            // 
            this.Print.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Print.Location = new System.Drawing.Point(12, 261);
            this.Print.Name = "Print";
            this.Print.Size = new System.Drawing.Size(66, 30);
            this.Print.TabIndex = 23;
            this.Print.Text = "print";
            this.Print.UseVisualStyleBackColor = true;
            this.Print.Click += new System.EventHandler(this.Print_Click);
            // 
            // Back
            // 
            this.Back.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.Back.Location = new System.Drawing.Point(12, 220);
            this.Back.Name = "Back";
            this.Back.Size = new System.Drawing.Size(66, 30);
            this.Back.TabIndex = 24;
            this.Back.Text = "Back";
            this.Back.UseVisualStyleBackColor = true;
            this.Back.Click += new System.EventHandler(this.Back_Click);
            // 
            // draftClick
            // 
            this.draftClick.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.draftClick.Location = new System.Drawing.Point(393, 261);
            this.draftClick.Name = "draftClick";
            this.draftClick.Size = new System.Drawing.Size(66, 30);
            this.draftClick.TabIndex = 25;
            this.draftClick.Text = "draft";
            this.draftClick.UseVisualStyleBackColor = true;
            this.draftClick.Click += new System.EventHandler(this.DraftClick_Click);
            // 
            // CloseForm
            // 
            this.CloseForm.BackColor = System.Drawing.Color.Transparent;
            this.CloseForm.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.CloseForm.FlatAppearance.BorderSize = 0;
            this.CloseForm.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Highlight;
            this.CloseForm.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CloseForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.CloseForm.ForeColor = System.Drawing.Color.Red;
            this.CloseForm.Location = new System.Drawing.Point(451, 2);
            this.CloseForm.Name = "CloseForm";
            this.CloseForm.Size = new System.Drawing.Size(18, 23);
            this.CloseForm.TabIndex = 27;
            this.CloseForm.Text = "X";
            this.CloseForm.UseVisualStyleBackColor = false;
            this.CloseForm.Click += new System.EventHandler(this.CloseForm_Click);
            // 
            // logger
            // 
            this.logger.AutoSize = true;
            this.logger.BackColor = System.Drawing.Color.Transparent;
            this.logger.ForeColor = System.Drawing.Color.Maroon;
            this.logger.Location = new System.Drawing.Point(0, 297);
            this.logger.Name = "logger";
            this.logger.Size = new System.Drawing.Size(0, 13);
            this.logger.TabIndex = 28;
            // 
            // PDFsender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(471, 311);
            this.Controls.Add(this.logger);
            this.Controls.Add(this.CloseForm);
            this.Controls.Add(this.draftClick);
            this.Controls.Add(this.Back);
            this.Controls.Add(this.Print);
            this.Controls.Add(this.Cencel_send);
            this.Controls.Add(this.Approve_send);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.chooseFile);
            this.Controls.Add(this.DataBase);
            this.Controls.Add(this.Proceed);
            this.Controls.Add(this.test);
            this.Controls.Add(this.file1);
            this.Controls.Add(this.addtotitle1);
            this.Controls.Add(this.LoadBar);
            this.Controls.Add(this.startButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "PDFsender";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Form1";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PDFsender_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PDFsender_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button DataBase;
        private System.Windows.Forms.Button Proceed;
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.Label file1;
        private System.Windows.Forms.TextBox addtotitle1;
        private System.Windows.Forms.ProgressBar LoadBar;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button chooseFile;
        private System.Windows.Forms.Label FilePath;
        private System.Windows.Forms.Button Approve_send;
        private System.Windows.Forms.Button Cencel_send;
        private System.Windows.Forms.Button Print;
        private System.Windows.Forms.Button Back;
        private System.Windows.Forms.Button draftClick;
        private System.Windows.Forms.Button CloseForm;
        private System.Windows.Forms.Label logger;
    }
}

