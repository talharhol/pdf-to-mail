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
        Form config_form = new אלומות_חשבוניות.configForm();
        System.util.RectangleJ accountLocation;
        Regex mailRegex;
        Logger logHandler;
        FolderHandler folderHandler = null;
        CacheHandler cacheHandler = new CacheHandler();
        bool succeded = false;
        System.Drawing.Point MousePoint;
        


        private IEnumerable<PdfData> GetPdfData()
        {
            if (InitRun())
            {
                Disablebuttons();
                LoadBar.Maximum = folderHandler.NumerOfFiles();
                for (int i = 0; i < folderHandler.NumerOfFiles(); i++)
                {
                    LoadBar.Value = i + 1;
                    yield return new PdfData(ExtractMails(folderHandler.GetFile(i)), i);
                }
                logHandler.Log("Finished succesfully");
                succeded = true;
                Enablebuttons();
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
                List<string> mails = data.mails;
                PdfHandler currentFile = folderHandler.GetFile(data.FileNumber);
                if (mails.Count > 0)
                {
                    filename = currentFile.GetFilePath();
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
                if (SentMail)
                {

                    folderHandler.MoveFile(data.FileNumber);
                }
            }
        }

        bool InitRun()
        {
            succeded = false;
            if (!cacheHandler.IsConfiged())
            {
                config_form.Show();
            }
            if (cacheHandler.IsConfiged())
            {
                folderHandler = new FolderHandler(logHandler, cacheHandler.GetSrc(), cacheHandler.GetDst());
                accountLocation = new System.util.RectangleJ(
                    cacheHandler.GetX(), cacheHandler.GetY(), 
                    cacheHandler.GetW(), cacheHandler.GetH());
                mailRegex = new Regex(cacheHandler.GetRegex(), RegexOptions.Compiled);
                return folderHandler.IsFolderValid();
            }
            return false;
        }

        List<string> ExtractMails(PdfHandler pdfFile)
        {
            string mailText = pdfFile.GetTextFromArea(pdfFile.NumerOfPages(), accountLocation);
            List<string> mails = new List<string>();
            MatchCollection matches = mailRegex.Matches(mailText);
            if (matches.Count == 0) return mails;
            foreach (Match match in matches)
            {
                mails.Add(match.Groups["mail"].Value);
            }
            return mails;
        }

        void SendMail(string ToMail, string filename, bool draft = false)
        {
            Outlook.MailItem mail = app.CreateItem(Outlook.OlItemType.olMailItem);
            mail.To = ToMail;
            mail.Subject = addtotitle1.Text;
            mail.Attachments.Add(filename);
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
            DateTime relativeMonth = DateTime.Now.AddMonths(Consts.RelativeMonth);
            string relativeMonthString = relativeMonth.Month.ToString() + "/" + relativeMonth.Year.ToString() + " ";
            addtotitle1.Text = Consts.Subject + relativeMonthString;
            LogHistoryContainer.Controls.Add(logHistory);
            this.Text = Consts.Title;
            logHandler = new Logger(logger, logHistory);
            if (File.Exists(Consts.CacheFile)) {
                logHandler.Log("You're good to go");
            } else {
                logHandler.Log("Please config the program", true);

            }
            BackToHome();
        }

        private void Approve_send_Click(object sender, EventArgs e)
        {
            RunMainProgram();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Test_Click(sender, e);
            if (succeded)
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
            Testfile.WriteLine("filename | mails");
            bool didRun = false;
            foreach (PdfData data in GetPdfData())
            {
                didRun = true;
                string mails = data.mails.Count > 0 ? string.Join(", ", data.mails) : "";
                string filename = Path.GetFileName(folderHandler.GetFile(data.FileNumber).GetFilePath());
                logHandler.AddLog(mails);
                Testfile.WriteLine(string.Format("{0} | {1}", filename, mails));
            }
            Testfile.Dispose();
            if(didRun)
                RunCmdCommand("\"" + Consts.DesktopLocation + "\\TESTFILE.txt\"");
        }


        private void Cencel_send_Click(object sender, EventArgs e)
        {
            Enablebuttons();
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


        void Enablebuttons()
        {
            startButton.Enabled = true;
            addtotitle1.ReadOnly = false;
            addtotitle1.Enabled = true;
            test.Enabled = true;
            config.Enabled = true;
            LoadBar.Value = 0;
            draftClick.Enabled = true;
        }

        void BackToHome()
        {
            this.GoToMainPage();
        }

        void Disablebuttons()
        {
            startButton.Enabled = false;
            addtotitle1.ReadOnly = true;
            addtotitle1.Enabled = false;
            test.Enabled = false;
            config.Enabled = false;
            draftClick.Enabled = false;
        }

        private void GoToMainPage()
        {
            this.Controls.Clear();
            this.Controls.Add(config);
            this.Controls.Add(startButton);
            this.Controls.Add(LoadBar);
            this.Controls.Add(addtotitle1);
            this.Controls.Add(test);
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

        private void config_Click(object sender, EventArgs e)
        {
            config_form.Show();
        }
    }
}
