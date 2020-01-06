using System;
using System.Text;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;
using ChooseName;
using Consts = ChooseName.Consts;


namespace pdfScanner
{
    public partial class PDFsender : Form
    { //קובץ לדוגמה
        string FirstPage;
        Outlook.Application app;
        ChooseName.ExcelApp excel;
        ChooseName.PdfHandler pdfHandler = null;

        string SearchForAccountNumner(string page)
        {
            string[] words = page.Split('\n');
            /*
             * helps to detect the accunt line *
             * string str = "";
            for (int i = 0; i < words.Length; i++)
			{
			    str += i.ToString() +"   " + words[i] + "\n";
			}
            MessageBox.Show(str);*/
            return GetAccountNumber(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(words[Consts.AccountLine])));

        }

        string GetAccountNumber(string text)
        {
            string str = "";
            /*
             * 
             * Insert Your Code Here
             * 
             */
            if (str == null || str == "")
                return "-1";
            return str;
        }

        bool IsLastPage(string page)
        {
            if (Consts.Seperator == "")
                return true;
            return page.Contains(Consts.Seperator);
        }

        bool InitRun()
        {
            if (pdfHandler == null || !pdfHandler.IsFileValid())
            {
                MessageBox.Show("Can't access file.");
                Enablebuttons();
                BackToHome();
                return false;
            }
            try
            {
                excel = new ChooseName.ExcelApp();
            }
            catch
            {
                MessageBox.Show("Can't open database file");
                Enablebuttons();
                BackToHome();
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
            DateTime t = DateTime.Now;
            string subname = ((t.Month) - 1).ToString() + "/" + t.Year.ToString() + " ";
            if (t.Month == 1)
            {
                subname = "12/" + (t.Year - 1).ToString() + " ";
            }
            mail.Subject += subname;
            mail.Subject += addtotitle1.Text;
            mail.Attachments.Add(System.IO.Directory.GetCurrentDirectory().ToString() + @"\" + filename);
            try
            {
                ((Outlook._MailItem)mail).Send();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
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
            if (!DAPI.Enabled)
            {
                this.Controls.Clear();
                this.Controls.Add(Approve_send);
                this.Controls.Add(Cencel_send);
            }
            

        }

        private void Test_Click(object sender, EventArgs e)
        {
            if (!InitRun())
            {
                Enablebuttons();
                return;
            }

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
                    string text = pdfHandler.GetTextFromPage(i);
                    FirstPage = pdfHandler.GetTextFromPage(i - numofpages);
                    if (!IsLastPage(text))
                    {
                        numofpages++;
                        continue;
                    }

                    string Account = SearchForAccountNumner(FirstPage);
                    string PSS = excel.GetPassword(Account);
                    string EMAIL = excel.GetMail(Account);
                    PSS = string.Join("*", new string[PSS.Length + 1]);
                    string linetofile = "| " + Account + " | " + (i - numofpages).ToString() + " | " + (numofpages + 1).ToString() + " | " + EMAIL + " | " + PSS + " |";
                    Testfile.WriteLine(linetofile);
                    numofpages = 0;
                }
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
                excel.Close();
                pdfHandler.Close();
                Testfile.Dispose();
                this.Close();
            }

            Testfile.Dispose();
            excel.Close();
            pdfHandler.Close();
            Enablebuttons();
            RunCmdCommand("start \"" + Consts.DesktopLocation + "\\TESTFILE.txt\"");
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
        }

        private void Approve_send_Click(object sender, EventArgs e)
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
            if (!InitRun())
            {
                Enablebuttons();
                BackToHome();
                return;
            }

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
                    string text = pdfHandler.GetTextFromPage(i);
                    FirstPage = pdfHandler.GetTextFromPage(i - numofpages);
                    string Account = "";
                    if (IsLastPage(text) == false)
                    {
                        numofpages++;
                        continue;
                    }

                    Account = SearchForAccountNumner(FirstPage);
                    string PSS = excel.GetPassword(Account);
                    string EMAIL = excel.GetMail(Account);

                    if (EMAIL == null || EMAIL == "")
                    {
                        pdfHandler.AddPagesToPrint(i - numofpages, numofpages);
                        numofpages = 0;
                        continue;
                    }
                    filename = pdfHandler.Slice(i - numofpages, numofpages, PSS);
                    try
                    {
                        SendMail(EMAIL, filename);
                        if (excel.GetPrint(Account))
                        {
                            pdfHandler.AddPagesToPrint(i - numofpages, numofpages);
                        }
                    }
                    catch
                    {
                        pdfHandler.AddPagesToPrint(i - numofpages, numofpages);
                    }

                    EMAIL = excel.GetSecondMail(Account);
                    if (EMAIL != "")
                    {
                        try
                        {
                            SendMail(EMAIL, filename);
                        }
                        catch
                        {

                        }
                    }
                    numofpages = 0;
                }

                string printPath = pdfHandler.Print();
                if (printPath != "")
                    RunCmdCommand("start chrome \"" + printPath + "\"");
                excel.Close();
                pdfHandler.Close();
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
                excel.Close();
                pdfHandler.Close();
                this.Close();
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
            {
                Enablebuttons();
                BackToHome();
                return;
            }

            Disablebuttons();

            app = new Outlook.Application();
            try
            {
                int numofpages = 0;
                LoadBar.Maximum = pdfHandler.NumerOfPages();

                for (int i = 1; i <= pdfHandler.NumerOfPages(); i++)
                {
                    LoadBar.Value = i;
                    string text = pdfHandler.GetTextFromPage(i);
                    FirstPage = pdfHandler.GetTextFromPage(i - numofpages);
                    string Account = "";
                    if (!IsLastPage(text))
                    {
                        numofpages++;
                        continue;
                    }

                    Account = SearchForAccountNumner(FirstPage);
                    string EMAIL = excel.GetMail(Account);

                    if (EMAIL == null || EMAIL == "" || excel.GetPrint(Account))
                        pdfHandler.AddPagesToPrint(i - numofpages, numofpages);

                    numofpages = 0;
                }

                string printPath = pdfHandler.Print();
                if (printPath != "")
                    RunCmdCommand("start chrome \"" + printPath + "\"");
                excel.Close();
                pdfHandler.Close();
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
                excel.Close();
                pdfHandler.Close();
                this.Close();
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
        }

        void BackToHome()
        {
            this.Controls.Clear();
            this.Controls.Add(DAPI);
            this.Controls.Add(D);
            DAPI.Enabled = true;
        }

        void Disablebuttons()
        {
            startButton.Enabled = false;
            addtotitle1.ReadOnly = true;
            addtotitle1.Enabled = false;
            test.Enabled = false;
            chooseFile.Enabled = false;
            Print.Enabled = false;
            DAPI.Enabled = false;
        }
    }
}
