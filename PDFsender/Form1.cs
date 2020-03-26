using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;
using ChooseName;
using System.IO;
using System.Collections.Generic;


namespace pdfScanner
{
    public partial class PDFsender : Form
    { //קובץ לדוגמה
        Outlook.Application app;
        ExcelApp excel;
        Logger logHandler;
        FolderHandler folderHandler = null;
        System.Drawing.Point MousePoint;


        private IEnumerable<PdfData> GetPdfData()
        {
            if (InitRun())
            {
                Disablebuttons();
                LoadBar.Maximum = folderHandler.NumerOfFiles();
                for (int i = 0; i < folderHandler.NumerOfFiles(); i++)
                {
                    PdfHandler currentFile;
                    currentFile = folderHandler.GetFile(i);
                    LoadBar.Value = i + 1;
                    yield return new PdfData(new Account(ExtractAccountNumber(currentFile), excel, logHandler), i);
                }
                logHandler.Log("Finished succesfully");
                Enablebuttons();
                BackToHome();
            }
        }

        private void RunMainProgram(bool isDraft = false)
        {
            GoToMainPage();
            string filename = "";
            app = new Outlook.Application();
            foreach (PdfData data in GetPdfData())
            {
                bool SentMail = false;
                string[] mails = data.account.Mails();
                PdfHandler currentFile = folderHandler.GetFile(data.FileNumber);
                if (mails.Length > 0)
                {
                    filename = currentFile.Slice(1, currentFile.NumerOfPages() / 2, data.account.Password());
                    logHandler.AddLog((isDraft ? "Draft to: " : "Mail to: ") + mails[0]);
                }
                foreach (string mail in mails)
                {
                    try
                    {
                        SendMail(mail, filename, isDraft);
                        SentMail = true;
                    }
                    catch { }
                }
                if (!SentMail || data.account.IsPrint())
                {

                    folderHandler.AddPagesToPrint(data.FileNumber);
                    logHandler.AddLog("Print account: " + data.account.GetAccount());
                }
            }
            string printPath = folderHandler.Print();
            if (printPath != "")
                RunCmdCommand("\"" + printPath + "\"");
        }

        bool InitRun()
        {
            if (folderHandler == null || !folderHandler.IsFolderValid())
            {
                logHandler.Log("Can't access pdf folder.", true);
                MessageBox.Show("Can't access pdf folder.");
                return false;
            }
            try
            {
                excel = new ExcelApp(logHandler);
            }
            catch
            {
                logHandler.Log("Can't open database file.", true);
                MessageBox.Show("Can't open database file\n(file not exists or password error)");
                return false;
            }
            folderHandler.LoadDirectory();
            return true;
        }

        string ExtractAccountNumber(PdfHandler pdfFile)
        {
            string account = "";
            string accountText = pdfFile.GetTextFromArea(1, Consts.AccountArea);
            MatchCollection matches = Consts.AccountRegex.Matches(accountText);
            if (matches.Count == 0) return Consts.EmptyAccount;
            foreach (Match match in matches)
            {
                account += match.Groups["Account"].Value;
            }
            if (Consts.ReverseAccount)
            {
                char[] myArr = account.ToCharArray();
                Array.Reverse(myArr);
                account = new string(myArr);
            }
            return account;
        }

        bool IsLastPage(string page)
        {
            if (Consts.EndOfPageSeperator == "") return true;
            return page.Contains(Consts.EndOfPageSeperator);
        }

        void SendMail(string ToMail, string filename, bool draft = false)
        {
            Outlook.MailItem mail = app.CreateItem(Outlook.OlItemType.olMailItem);
            mail.To = ToMail;
            mail.Subject = Consts.Subject;
            DateTime relativeMonth = DateTime.Now.AddMonths(Consts.RelativeMonth);
            string relativeMonthString = relativeMonth.Month.ToString() + "/" + relativeMonth.Year.ToString() + " ";
            mail.Subject += relativeMonthString;
            mail.Subject += addtotitle1.Text;
            mail.Attachments.Add(System.IO.Directory.GetCurrentDirectory().ToString() + @"\" + filename);
            if (draft)
            {
                ((Outlook._MailItem)mail).Save();
            }
            else
            {
                ((Outlook._MailItem)mail).Send();
            }
        }

        void RunCmdCommand(string command)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/C " + command;
            process.StartInfo = startInfo;
            process.Start();
        }

        public PDFsender()
        {
            InitializeComponent();
            LogHistoryContainer.Controls.Add(logHistory);
            this.Text = Consts.Title;
            logHandler = new Logger(logger, logHistory);
            if (File.Exists(Consts.CacheFile)) {
                logHandler.Log("You're good to go");
            } else {
                logHandler.Log("Please choose database file", true);

            }
            BackToHome();
        }

        private void Approve_send_Click(object sender, EventArgs e)
        {
            RunMainProgram();
        }

        private void Print_Click(object sender, EventArgs e)
        {
            foreach (PdfData data in GetPdfData())
            {
                if (data.account.Mails().Length == 0 || data.account.IsPrint())
                {
                    logHandler.AddLog("Print");
                    folderHandler.AddPagesToPrint(data.FileNumber);
                }
            }
            string printPath = folderHandler != null ? folderHandler.Print() : "";
            if (printPath != "")
                RunCmdCommand("\"" + printPath + "\"");
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Test_Click(sender, e);
            if (!Proceed.Enabled)
            {
                this.Controls.Clear();
                this.Controls.Add(Approve_send);
                this.Controls.Add(Cencel_send);
                this.Controls.Add(logger);
            }
            

        }

        private void Test_Click(object sender, EventArgs e)
        {
            StreamWriter Testfile = new StreamWriter(Consts.DesktopLocation + @"\TESTFILE.txt", false);
            Testfile.WriteLine("|Account|FileNumber|Password|Email");
            bool didRun = false;
            foreach (PdfData data in GetPdfData())
            {
                didRun = true;
                string AccountMail = data.account.Mails().Length > 0 ? data.account.Mails()[0] : "";
                logHandler.AddLog(AccountMail);
                string EncriptedPassword = string.Join("*", new string[data.account.Password().Length + 1]);
                logHandler.AddLog(EncriptedPassword);
                Testfile.WriteLine("| "
                    + data.account.GetAccount() + " | "
                    + (data.FileNumber + 1).ToString() + " | "
                    + AccountMail + " | "
                    + EncriptedPassword + " |");
            }
            Testfile.Dispose();
            if(didRun)
                RunCmdCommand("\"" + Consts.DesktopLocation + "\\TESTFILE.txt\"");
        }

        private void ChooseFile_Click(object sender, EventArgs e)
        {
            folderHandler = new FolderHandler(logHandler);
            if (!folderHandler.IsFolderValid())
            {
                logHandler.Log("pdf file is invalid! please choose another one", true);
            }
        }

        private void DatabasePath_Click(object sender, EventArgs e)
        {
            ChooseName.ExcelApp.SaveFilePath();
        }

        private void Cencel_send_Click(object sender, EventArgs e)
        {
            Enablebuttons();
            BackToHome();
        }

        private void Back_Click(object sender, EventArgs e)
        {
            BackToHome();
        }

        private void DraftClick_Click(object sender, EventArgs e)
        {
            RunMainProgram(true);
        }

        private void CloseForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PDFsender_MouseDown(object sender, MouseEventArgs e)
        {
            MousePoint = new System.Drawing.Point(-e.X, -e.Y);
        }

        private void PDFsender_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                System.Drawing.Point mousePos = Control.MousePosition;
                mousePos.Offset(MousePoint.X, MousePoint.Y);
                Location = mousePos;
            }
        }

        private void LoadMain_Click(object sender, EventArgs e)
        {
            GoToMainPage();
        }

        void Enablebuttons()
        {
            startButton.Enabled = true;
            addtotitle1.ReadOnly = false;
            addtotitle1.Enabled = true;
            test.Enabled = true;
            chooseFile.Enabled = true;
            Print.Enabled = true;
            LoadBar.Value = 0;
            Back.Enabled = true;
            draftClick.Enabled = true;
        }

        void BackToHome()
        {
            this.Controls.Clear();
            this.Controls.Add(Proceed);
            this.Controls.Add(DataBase);
            this.Controls.Add(logger);
            Proceed.Enabled = true;
        }

        void Disablebuttons()
        {
            startButton.Enabled = false;
            addtotitle1.ReadOnly = true;
            addtotitle1.Enabled = false;
            test.Enabled = false;
            chooseFile.Enabled = false;
            Print.Enabled = false;
            Proceed.Enabled = false;
            Back.Enabled = false;
            draftClick.Enabled = false;
        }

        private void GoToMainPage()
        {
            this.Controls.Clear();
            this.Controls.Add(chooseFile);
            this.Controls.Add(startButton);
            this.Controls.Add(LoadBar);
            this.Controls.Add(addtotitle1);
            this.Controls.Add(test);
            this.Controls.Add(Print);
            this.Controls.Add(Back);
            this.Controls.Add(draftClick);
            this.Controls.Add(logger);
        }

        private void logger_MouseHover(object sender, EventArgs e)
        {
            this.Controls.Add(LogHistoryContainer);
            logger.Focus();
            LogHistoryContainer.BringToFront();
        }

        private void logger_MouseLeave(object sender, EventArgs e)
        {
            LogHistoryContainer.SendToBack();
            this.Controls[0].Focus();
            this.Controls.Remove(LogHistoryContainer);
        }

        private void logger_MouseWheel(object sender, MouseEventArgs e)
        {
            logHistory.Location = new System.Drawing.Point(logHistory.Location.X, logHistory.Location.Y + (16 * e.Delta / 120));
        }
    }
}
