using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;
using ChooseName;
using Consts = ChooseName.Consts;



namespace pdfScanner
{
    public partial class PDFsender : Form
    { //קובץ לדוגמה
        Outlook.Application app;
        ChooseName.ExcelApp excel;
        ChooseName.Account account;
        ChooseName.PdfHandler pdfHandler = null;
        
        string ExtractAccountNumber(int pageNumber)
        {
            string account = "";
            string accountText = pdfHandler.GetTextFromArea(pageNumber, Consts.AccountArea);
            MatchCollection matches = Consts.AccountRegex.Matches(accountText);
            if (matches.Count == 0) return "-1";
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

        bool InitRun()
        {
            if (pdfHandler == null || !pdfHandler.IsFileValid())
            {
                MessageBox.Show("Can't access pdf file.");
                return false;
            }
            try
            {
                excel = new ExcelApp();
            }
            catch
            {
                MessageBox.Show("Can't open database file\n(file not exists or password error)");
                return false;
            }
            pdfHandler.LoadPdf();
            return true;
        }

        void SendMail(string ToMail, string filename)
        {
            Outlook.MailItem mail = app.CreateItem(Outlook.OlItemType.olMailItem);
            mail.To = ToMail;
            mail.Subject = Consts.Subject;
            DateTime relativeMonth = DateTime.Now.AddMonths(Consts.RelativeMonth);
            string relativeMonthString = relativeMonth.Month.ToString() + "/" + relativeMonth.Year.ToString() + " ";
            mail.Subject += relativeMonthString;
            mail.Subject += addtotitle1.Text;
            mail.Attachments.Add(System.IO.Directory.GetCurrentDirectory().ToString() + @"\" + filename);
            ((Outlook._MailItem)mail).Send();

        }

        public PDFsender()
        {
            InitializeComponent();
            this.Text = Consts.Title;
            BackToHome();
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Test_Click(sender, e);
            if (!Proceed.Enabled)
            {
                this.Controls.Clear();
                this.Controls.Add(Approve_send);
                this.Controls.Add(Cencel_send);
            }
            

        }

        private void Test_Click(object sender, EventArgs e)
        {
            if (!InitRun())
                return;
            Disablebuttons();
            System.IO.StreamWriter Testfile = new System.IO.StreamWriter(Consts.DesktopLocation + @"\TESTFILE.txt", false);
            try
            {
                int numofpages = 0;
                LoadBar.Maximum = pdfHandler.NumerOfPages();
                Testfile.WriteLine("|Account|StartPage|Length|Password|Email");

                for (int i = 1; i <= pdfHandler.NumerOfPages(); i++)
                {
                    LoadBar.Value = i;
                    if (!IsLastPage(pdfHandler.GetTextFromPage(i)))
                    {
                        numofpages++;
                        continue;
                    }
                    account = new ChooseName.Account(ExtractAccountNumber(i - numofpages), excel);
                    string EncriptedPassword = string.Join("*", new string[account.Password().Length + 1]);
                    Testfile.WriteLine("| "
                        + account.GetAccount() + " | "
                        + (i - numofpages).ToString() + " | "
                        + (numofpages + 1).ToString() + " | "
                        + account.Mails()[0] + " | "
                        + EncriptedPassword + " |");
                    numofpages = 0;
                }
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
                this.Close();
            }
            finally
            {
                excel.Close();
                pdfHandler.Close();
                Testfile.Dispose();
            }
            Enablebuttons();
            RunCmdCommand("\"" + Consts.DesktopLocation + "\\TESTFILE.txt\"");
        }

        private void ChooseFile_Click(object sender, EventArgs e)
        {
            pdfHandler = new ChooseName.PdfHandler();
            if(pdfHandler.IsFileValid())
                FilePath.Text = pdfHandler.GetFilePath();
        }

        private void DatabasePath_Click(object sender, EventArgs e)
        {
            ChooseName.ExcelApp.SaveFilePath();
        }

        private void LoadMain_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.Controls.Add(startButton);
            this.Controls.Add(LoadBar);
            this.Controls.Add(addtotitle1);
            this.Controls.Add(file1);
            this.Controls.Add(test);
            this.Controls.Add(FilePath);
            this.Controls.Add(chooseFile);
            this.Controls.Add(Print);
            this.Controls.Add(Back);
        }

        private void Approve_send_Click(object sender, EventArgs e)
        {
            LoadMain_Click(sender, e);

            if (!InitRun())
                return;
            Disablebuttons();
            app = new Outlook.Application();
            try
            {
                int numofpages = 0;
                string filename = "";
                LoadBar.Maximum = pdfHandler.NumerOfPages();

                for (int i = 1; i <= pdfHandler.NumerOfPages(); i++)
                {
                    LoadBar.Value = i;
                    bool SentMail = false;
                    if (!IsLastPage(pdfHandler.GetTextFromPage(i)))
                    {
                        numofpages++;
                        continue;
                    }
                    account = new ChooseName.Account(ExtractAccountNumber(i - numofpages), excel);
                    foreach (string mail in account.Mails())
                    {
                        try
                        {
                            SendMail(mail, filename);
                            SentMail = true;
                        }
                        catch { }
                    }
                    if (!SentMail || account.Print())
                        pdfHandler.AddPagesToPrint(i - numofpages, numofpages);
                    numofpages = 0;
                }

                string printPath = pdfHandler.Print();
                if (printPath != "")
                    RunCmdCommand("start chrome \"" + printPath + "\"");
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
                this.Close();
            }
            finally
            {
                excel.Close();
                pdfHandler.Close();
            }
            Enablebuttons();
            BackToHome();
        }

        private void Cencel_send_Click(object sender, EventArgs e)
        {
            Enablebuttons();
            BackToHome();
        }

        private void Print_Click(object sender, EventArgs e)
        {
            if (!InitRun())
                return;
            Disablebuttons();
            app = new Outlook.Application();
            try
            {
                int numofpages = 0;
                LoadBar.Maximum = pdfHandler.NumerOfPages();

                for (int i = 1; i <= pdfHandler.NumerOfPages(); i++)
                {
                    LoadBar.Value = i;
                    if (!IsLastPage(pdfHandler.GetTextFromPage(i)))
                    {
                        numofpages++;
                        continue;
                    }

                    account = new ChooseName.Account(ExtractAccountNumber(i - numofpages), excel);
                    if (account.Mails().Length == 0 || account.Print())
                        pdfHandler.AddPagesToPrint(i - numofpages, numofpages);
                    numofpages = 0;
                }

                string printPath = pdfHandler.Print();
                if (printPath != "")
                    RunCmdCommand("\"" + printPath + "\"");
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
                this.Close();
            }
            finally
            {
                excel.Close();
                pdfHandler.Close();
            }
            Enablebuttons();
            BackToHome();
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
        }

        void BackToHome()
        {
            this.Controls.Clear();
            this.Controls.Add(Proceed);
            this.Controls.Add(DataBase);
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
        }

        private void Back_Click(object sender, EventArgs e)
        {
            BackToHome();
        }
    }
}
