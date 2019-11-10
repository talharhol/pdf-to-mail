using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Excel = Microsoft.Office.Interop.Excel;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace pdfScanner
{
    public partial class PDFsender : Form
    { //כנרת דף תקציב
        string[] filesnames;
        string FirstPage;
        string DataBasePath = "";
        string DASKTOPLOCATION = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string EndOfRows;
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        Outlook.Application app;
        const string Subject = "דף תקציב ";
        const string Title = "שליחת דפי תקציב";
        const string DBPASS = "alibaba";
        const string Seperator = "#";
        const string PrintName = @"\דפי_תקציב_להדפסה.pdf";
        const int MESSAGE = 4;
        const int REGULAR = 0;

        OpenFileDialog file = new OpenFileDialog();

        public PDFsender()
        {
            InitializeComponent();
            this.Text = Title;
            InitProgram();
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
            System.IO.StreamWriter Testfile = new System.IO.StreamWriter(DASKTOPLOCATION + @"\TESTFILE.txt", false);
            try
            {
                PdfReader reader;
                reader = new PdfReader(file.FileName);
                int intPageNum = reader.NumberOfPages;
                int numofpages = 0;
                LoadBar.Maximum = reader.NumberOfPages;

                Testfile.WriteLine("|Account|StartPage|Length|Password|Email");

                for (int i = 1; i <= intPageNum; i++)
                {
                    LoadBar.Value = i;
                    string text = PdfTextExtractor.GetTextFromPage(reader, i, new LocationTextExtractionStrategy());
                    FirstPage = PdfTextExtractor.GetTextFromPage(reader, i - numofpages, new LocationTextExtractionStrategy());
                    if (IsLastPage(text) == false)
                    {
                        numofpages++;
                        continue;
                    }

                    string Account = SearchForAccountNumner(FirstPage);
                    string PSS = GetPasswordByAccount(Account);
                    string EMAIL = GetMailFromAccount(Account);

                    if (PSS == null || PSS == "") {
                        PSS = "";
                    }
                    else {
                        PSS = string.Join("*", new string[PSS.Length + 1]);
                    }
                    if (EMAIL == null || EMAIL == "") EMAIL = "";

                    string linetofile = "| " + Account + " | " + (i - numofpages).ToString() + " | " + (numofpages + 1).ToString() + " | " + EMAIL + " | " + PSS + " |";
                    Testfile.WriteLine(linetofile);
                    numofpages = 0;
                }
                reader.Close();
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
                ClearExcle();
                Testfile.Dispose();
                this.Close();
            }

            Testfile.Dispose();
            ClearExcle();
            Enablebuttons();
            RunCmdCommand("start " + DASKTOPLOCATION + @"\TESTFILE.txt");
        }

        private void SendToAllDB_Click(object sender, EventArgs e)
        {
            file.Reset();
            file.ShowDialog();
            if (!InitRun())
            {
                Enablebuttons();
                BackToHome();
                return;
            }

            app = new Outlook.Application();
            LoadBar.Maximum = int.Parse(EndOfRows.ToString());
            Disablebuttons();

            object[,] mails = xlApp.get_Range("D2", "D" + EndOfRows).Value2;
            object[,] mails2 = xlApp.get_Range("F2", "F" + EndOfRows).Value2;
            for (int i = 1; i <= mails.GetLength(0); i++)
            {
                try
                {

                    string AC = "";
                    if (mails[i, 1] != null) AC = mails[i, 1].ToString();
                    if (AC == null || AC == "")
                    {
                        continue;
                    }

                    try
                    {
                        SendMail(AC, file.FileName, 4);

                        string AC2 = "";
                        if (mails2[i, 1] != null)
                            AC2 = mails[i, 1].ToString();
                        if (AC2 != null && AC2 != "")
                        {
                            try
                            {
                                SendMail(AC2, (file.FileName).ToString(), MESSAGE);
                            }
                            catch (Exception E)
                            {

                            }
                        }
                    }
                    catch (Exception t)
                    {
                        MessageBox.Show(t.ToString());
                    }
                    LoadBar.Value = i;
                }
                catch (Exception t)
                {
                    MessageBox.Show(t.ToString());
                }
            }
            ClearExcle();
            Enablebuttons();
            BackToHome();
        }

        private void ChooseFile_Click(object sender, EventArgs e)
        {
            file.Reset();
            file.Filter = "PDF|*.pdf";
            file.ShowDialog();
            if (!(file.FileName == "" || !file.CheckFileExists))
            {
                FilePath.Text = file.FileName;
            }
        }

        private void DatabasePath_Click(object sender, EventArgs e)
        {
            file.FileName = DataBasePath;
            file.Filter = "Excel|*.xlsx";
            file.ShowDialog();
            if (!(file.FileName == "" || !file.CheckFileExists || file.FileName == null))
                DataBasePath = file.FileName;
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
        }

        void ExcelIt()
        {
            if (DataBasePath == "")
            {
                do
                {
                    file.FileName = "";
                    file.Filter = "Excel|*.xlsx";
                    file.ShowDialog();
                } while (file.FileName == "" || !file.CheckFileExists);
                DataBasePath = file.FileName;
            }
            if (!File.Exists("DATA.txt"))
            {
                // Create the file.
                using (FileStream fs = File.Create("DATA.txt"))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(DataBasePath)));
                    fs.Write(info, 0, info.Length);
                }
            }
            else
            {
                //string[] lines = { DataBasePath };
                string[] lines = { System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(DataBasePath)) };
                System.IO.File.WriteAllLines("DATA.txt", lines);
            }
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(DataBasePath, 2, true, 5, DBPASS);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets[1];
            if (xlWorkBook == null)
            {
                this.Close();
            }
            xlApp.Visible = false;
            try
            {
                EndOfRows = GetNumOfColumns();
            }
            catch (Exception r)
            {
                MessageBox.Show(r.ToString());
            }
        }

        void ClearExcle()
        {
            bool didntcatch = true;
            int c = 0;
            do
            {
                c++;
                didntcatch = true;
                System.Threading.Thread.Sleep(200);
                try
                {
                    xlWorkBook.Close(false);
                }
                catch
                {
                    didntcatch = false;
                }
            } while (!didntcatch);
            this.KillExcelProcess();
        }

        void KillExcelProcess()
        {
            try
            {
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkSheet);
            }
            catch
            {
                System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName("Excel");
                System.Diagnostics.Process temp;
                for (int write = 0; write < process.Length; write++)
                {
                    for (int sort = 0; sort < process.Length - 1; sort++)
                    {
                        if (process[sort].StartTime < process[sort + 1].StartTime)
                        {
                            temp = process[sort + 1];
                            process[sort + 1] = process[sort];
                            process[sort] = temp;
                        }
                    }
                }
                process[0].Kill();
            }

        }

        void AddToNotSendFiles(int numofpages, int[] PagesNotSent, ref int loc, int CurrentPage)
        {
            for (int j = numofpages; j >= 0; j--)
            {
                PagesNotSent[loc] = CurrentPage - j;
                loc++;
            }
        }

        void InitArray(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = 0;
            }
        }

        string GetNumOfColumns()
        {
            int i = 1;
            do
            {
                i++;

            } while (xlApp.get_Range("A" + i.ToString()).Value2 != null);
            return (i - 1).ToString();
        }

        bool InitRun()
        {
            if ((file.FileName == "" || !file.CheckFileExists))
            {
                MessageBox.Show("Can't access file.");
                Enablebuttons();
                BackToHome();
                return false;
            }

            try
            {
                ExcelIt();
            }
            catch
            {
                KillExcelProcess();
                MessageBox.Show("Can't open database file");
                Enablebuttons();
                BackToHome();
                return false;
            }

            return true;
        }

        void InitProgram()
        {
            this.Controls.Clear();
            if (File.Exists("DATA.txt"))
            {
                using (StreamReader sr = File.OpenText("DATA.txt"))
                {
                    int i = 0;
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        if (i == 0)
                        {
                            DataBasePath = System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(s));
                        }
                        i++;
                    }
                }
            }

            BackToHome();
        }

        bool ToPrint(string Account)
        {
            if (Account != null && Account != "" && Account != "-1")
            {
                object[,] str = xlApp.get_Range("A2", "A" + EndOfRows).Value2;
                for (int i = 1; i <= str.GetLength(0); i++)
                {
                    if (long.Parse(Account) == long.Parse(str[i, 1].ToString()))
                    {
                        if (xlApp.get_Range("G" + (i + 1).ToString()).Value2 == "#")
                            return true;
                    }

                }

            }
            return false;
        }

        void SendMail(string ToMail, string filename, int k)
        {
            Outlook.MailItem mail = app.CreateItem(Outlook.OlItemType.olMailItem);
            mail.To = ToMail;
            mail.Subject = Subject;
            if (k == 4)
            {
                mail.Subject = "הודעה ";
            }
            DateTime t = DateTime.Now;
            string subname = ((t.Month) - 1).ToString() + "/" + t.Year.ToString() + " ";
            if (t.Month == 1)
            {
                subname = "12/" + t.Year.ToString() + " ";
            }
            if (k == 4)
                subname = " ";
            mail.Subject += subname;
            mail.Subject += addtotitle1.Text;
            if (k == 4)
            {
                mail.Attachments.Add((filename));
            }
            else
            {
                mail.Attachments.Add(System.IO.Directory.GetCurrentDirectory().ToString() + @"\" + filename + "_locked.pdf");
            }
            try
            {
                ((Outlook._MailItem)mail).Send();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
        }

        bool IsLastPage(string page)
        {
            string[] SP = new string[] {Seperator};
            string[] words = page.Split(SP, StringSplitOptions.None);

            return words.Length > 1;
        }

        string SearchForAccountNumner(string page)
        {
            string[] words = page.Split('\n');
            string str;
            int i = 0;
            do
            {
                str = GetAccountNumber(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(words[i])));
                i++;
            }
            while ((str == "" || str == null) && i < 3);
            if (str == "" || str == null)
                return "-1";
            return str;

        }

        string GetAccountNumber(string text)
        {
            string str = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] >= '0' && text[i] <= '9')
                {
                    str += text[i].ToString();
                }
            }
            if (str == null || str == "")
                return "-1";
            return str;
        }

        string GetSecondMailFromAccount(string Account)
        {
            if (Account != null && Account != "" && Account != "-1")
            {
                object[,] str = xlApp.get_Range("A2", "A" + EndOfRows).Value2;
                for (int i = 1; i <= str.GetLength(0); i++)
                {
                    if (long.Parse(Account) == long.Parse(str[i, 1].ToString()))
                    {
                        return xlApp.get_Range("F" + (i + 1).ToString()).Value2;
                    }

                }
            }
            return null;
        }

        string GetPasswordByAccount(string Account)
        {
            if (Account != null && Account != "" && Account != "-1")
            {
                object[,] str = xlApp.get_Range("A2", "A" + EndOfRows).Value2;
                for (int i = 1; i <= str.GetLength(0); i++)
                {
                    if (long.Parse(Account) == long.Parse(str[i, 1].ToString()))
                    {
                        Double str123;
                        try
                        {
                            str123 = xlApp.get_Range("E" + (i + 1).ToString()).Value2;
                        }
                        catch
                        {
                            return xlApp.get_Range("E" + (i + 1).ToString()).Value2;
                        }
                        return str123.ToString();
                    }

                }

            }
            return null;
        }

        string GetMailFromAccount(string Account)
        {
            if (Account != null && Account != "" && Account != "-1")
            {
                object[,] str = xlApp.get_Range("A2", "A" + EndOfRows).Value2;
                for (int i = 1; i <= str.GetLength(0); i++)
                {
                    if (long.Parse(Account) == long.Parse(str[i, 1].ToString()))
                    {
                        return xlApp.get_Range("D" + (i + 1).ToString()).Value2;
                    }

                }

            }
            return null;
        }

        void CreateBigPDF(int[] PagesNotSent, PdfReader reader)
        {
            if (PagesNotSent[0] != 0)
            {
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                PdfCopy copy = new PdfCopy(document, new FileStream(DASKTOPLOCATION + PrintName, FileMode.Create));
                document.Open();
                for (int i = 0; PagesNotSent[i] != 0; i++)
                {
                    copy.AddPage(copy.GetImportedPage(reader, PagesNotSent[i]));
                }
                document.Close();
            }
        }

        void SlicePdfFile(string filename, int pagenumber, int numOfPages, PdfReader reader, string pass)
        {
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            PdfCopy copy = new PdfCopy(document, new FileStream(filename + ".pdf", FileMode.Create));
            document.Open();
            for (int i = numOfPages; i >= 0; i--)
            {
                copy.AddPage(copy.GetImportedPage(reader, pagenumber - i));
            }
            document.Close();

            string WorkingFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string OutputFile = filename + "_locked.pdf";
            using (Stream input = new FileStream(filename + ".pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (Stream output = new FileStream(OutputFile, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfReader moreReader = new PdfReader(input);
                    PdfEncryptor.Encrypt(moreReader, output, true, pass, "kinneretPDF", PdfWriter.ALLOW_PRINTING);//////////
                }
            }
        }

        string FileNameCalc(string str)
        {
            string str2 = str;
            bool i;
            int num = 0;
            do
            {
                i = false;
                for (int j = 0; j < filesnames.Length; j++)
                {
                    if (filesnames[j] == str)
                    {
                        num++;
                        str = str2 + '_' + num.ToString();
                        i = true;
                    }
                }
            } while (i);
            if (num == 0)
                return str2;
            return str2 + '_' + num.ToString();
        }

        void DeleteTmpPdfFiles()
        {
            bool IsDeleted;
            for (int i = 0; i < filesnames.Length; i++)
            {
                if (filesnames[i] != null)
                {
                    IsDeleted = true;
                    try
                    {
                        DeleteFile(filesnames[i] + "_locked.pdf");
                    }
                    catch
                    {
                        IsDeleted = false;
                    }
                    if (IsDeleted)
                    {
                        filesnames[i] = null;
                    }
                }
            }
        }

        void DeleteFile(string filename)
        {
            File.Delete(filename);
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
            DAPI.Enabled = false;
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
                PdfReader reader = new PdfReader(file.FileName);
                int intPageNum = reader.NumberOfPages;
                int numofpages = 0;
                int loc = 0;
                int[] PagesNotSent = new int[intPageNum + 2];
                filesnames = new string[reader.NumberOfPages];
                LoadBar.Maximum = reader.NumberOfPages;

                InitArray(PagesNotSent);

                for (int i = 1; i <= intPageNum; i++)
                {
                    LoadBar.Value = i;
                    string text = PdfTextExtractor.GetTextFromPage(reader, i, new LocationTextExtractionStrategy());
                    FirstPage = PdfTextExtractor.GetTextFromPage(reader, i - numofpages, new LocationTextExtractionStrategy());
                    string Account = "";
                    if (IsLastPage(text) == false)
                    {
                        numofpages++;
                        continue;
                    }

                    Account = SearchForAccountNumner(FirstPage);
                    string filename = FileNameCalc("File");
                    filesnames[i - 1] = filename;
                    string PSS = GetPasswordByAccount(Account);
                    string EMAIL = GetMailFromAccount(Account);

                    if (EMAIL == null || EMAIL == "")
                    {
                        AddToNotSendFiles(numofpages, PagesNotSent, ref loc, i);
                        numofpages = 0;
                        DeleteTmpPdfFiles();
                        continue;
                    }

                    SlicePdfFile(filename, i, numofpages, reader, PSS);
                    DeleteFile(filename + ".pdf");
                    try
                    {
                        SendMail(EMAIL, filename, REGULAR);
                        if (ToPrint(Account))
                        {
                            AddToNotSendFiles(numofpages, PagesNotSent, ref loc, i);
                        }
                    }
                    catch
                    {
                        AddToNotSendFiles(numofpages, PagesNotSent, ref loc, i);
                    }

                    EMAIL = GetSecondMailFromAccount(Account);
                    if (EMAIL != null && EMAIL != "")
                    {
                        try
                        {
                            SendMail(EMAIL, filename, REGULAR);
                        }
                        catch
                        {

                        }
                    }

                    numofpages = 0;
                    DeleteTmpPdfFiles();
                }

                CreateBigPDF(PagesNotSent, reader);
                reader.Close();
                if (PagesNotSent[0] != 0)
                    RunCmdCommand("start chrome \"" + DASKTOPLOCATION + PrintName + "\"");
                ClearExcle();
            }
            catch (Exception G)
            {
                MessageBox.Show(G.ToString());
                ClearExcle();
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
    }
}
