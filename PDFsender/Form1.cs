using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Net.Mail;
using System.Net;
using S22.Imap;
using Excel = Microsoft.Office.Interop.Excel;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace pdfScanner
{
    public partial class PDFsender : Form
    { //חשבוניות כנרת
        string[] filesnames;
        string text2;
        string DataBasePath = "";
        string DASKTOPLOCATION = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string EndOfRows;
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        Outlook.Application app;
        string Subject = "חשבונית מס ";
        string Title = "חשבוניות מס כנרת";
        string DBPASS = "1234";
        string Seperator = "חתימה";
        string PrintName = @"\חשבוניות_להדפסה.pdf";
        int account_line = 6;

        OpenFileDialog file = new OpenFileDialog();

        public PDFsender()
        {
            InitializeComponent();
            this.Text = Title;
            login();
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
                EndOfRows = getNumOfColumns();
            }
            catch (Exception r)
            {
                MessageBox.Show(r.ToString());
            }
        }

        void login()
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

            this.Controls.Clear();
            this.Controls.Add(DAPI);
            this.Controls.Add(D);
        }

        bool ToPrint(string Account)
        {
            /*
             * opptional function *
             if (Account != null && Account != "" && Account != "-1")
            {
                object[,] str = xlApp.get_Range("A2", "A" + EndOfRows).Value2;
                for (int i = 1; i <= str.GetLength(0); i++)
                {
                    if (int.Parse(Account) == int.Parse(str[i, 1].ToString()))
                    {
                        if (xlApp.get_Range("G" + (i + 1).ToString()).Value2 == "#")
                            return true;
                    }

                }

            }*/
            return false;
        }

        void sendMail(string ToMail, string filename, int k)
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
                subname = "12/" + (t.Year-1).ToString() + " ";
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

        void deleteLocked()
        {
            bool IsDeleted;
            for (int i = 0; i < filesnames.Length; i++)
            {
                if (filesnames[i] != null)
                {
                    IsDeleted = true;
                    try
                    {
                        deleteFile(filesnames[i] + "_locked.pdf");
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

        bool IsLastPage(string page)
        {
            string[] SP = new string[] {Seperator};
            string[] words = page.Split(SP, StringSplitOptions.None);
            if (words.Length > 0)
                return true;
            return false;
        }

        void createPDFFile(string filename, int pagenumber, int numOfPages, PdfReader reader, string pass)
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

        void deleteFile(string filename)
        {
            File.Delete(filename);
        }

        string SearchForWord(string page)
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
            return GetAccountNumber(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(words[account_line])));

        }

        string filenamecalc(string str)
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
            xlApp.Quit();
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

        string GetSecondMailFromAccount(string Account)
        {
            if (Account != null && Account != "" && Account != "-1")
            {
                object[,] str = xlApp.get_Range("A2", "A" + EndOfRows).Value2;
                for (int i = 1; i <= str.GetLength(0); i++)
                {
                    if (int.Parse(Account) == int.Parse(str[i, 1].ToString()))
                    {
                        return xlApp.get_Range("F" + (i + 1).ToString()).Value2;
                    }

                }
            }
            return null;
        }

        string getNumOfColumns()
        {
            int i = 1;
            do
            {
                i++;

            } while (xlApp.get_Range("A" + i.ToString()).Value2 != null);
            return (i - 1).ToString();
        }

        string getPasswordByAccount(string Account)
        {
            if (Account != null && Account != "" && Account != "-1")
            {
                object[,] str = xlApp.get_Range("A2", "A" + EndOfRows).Value2;

                for (int i = 1; i <= str.GetLength(0); i++)
                {
                    if (int.Parse(Account) == int.Parse(str[i, 1].ToString()))
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

        string GetAccountNumber(string text)
        {
            string str = "";
            for (int i = 0; i < text.Length; i++)
            {
                if ((text[i] >= '0' && text[i] <= '9') || (text[i] >= 'a' && text[i] <= 'z') || (text[i] >= 'A' && text[i] <= 'Z'))
                {
                    for (int j = 0; j <= 10; j++)
                    {
                        if ((text[i + j] >= '0' && text[i + j] <= '9') || (text[i + j] >= 'a' && text[i + j] <= 'z') || (text[i + j] >= 'A' && text[i + j] <= 'Z'))
                            str += text[i + j].ToString();
                    }
                    break;
                }
            }

            if (str == null || str == "")
                return "-1";
            if (str.Length < 7)
                return "-1";
            return new string(str.Reverse().ToArray());// Reverses the string
        }

        string GetMailFromAccount(string Account)
        {
            if (Account != null && Account != "" && Account != "-1")
            {
                object[,] str = xlApp.get_Range("A2", "A" + EndOfRows).Value2;
                for (int i = 1; i <= str.GetLength(0); i++)
                {
                    if (int.Parse(Account) == int.Parse(str[i, 1].ToString()))
                    {
                        return xlApp.get_Range("D" + (i + 1).ToString()).Value2;
                    }

                }

            }
            return null;
        }

        private void D_Click_1(object sender, EventArgs e)
        {
            file.FileName = DataBasePath;
            file.Filter = "Excel|*.xlsx";
            file.ShowDialog();
            if (!(file.FileName == "" || !file.CheckFileExists || file.FileName == null))
                DataBasePath = file.FileName;
            file.Reset();
        }

        private void DAPI_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.Controls.Add(startButton);
            this.Controls.Add(LoadBar);
            this.Controls.Add(addtotitle1);
            this.Controls.Add(file1);
            this.Controls.Add(TestRun);
            this.Controls.Add(test);
            this.Controls.Add(FilePath);
            this.Controls.Add(chooseFile);
        }

        void killprocess()
        {
            xlApp.Quit();
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

        private void startButton_Click(object sender, EventArgs e)
        {

            if (!(file.FileName == "" || !file.CheckFileExists))
            {
                bool work = true;
                try
                {
                    ExcelIt();
                }
                catch
                {
                    work = false;
                    killprocess();
                    MessageBox.Show("Can't open database file");
                }
                if (work)
                {
                    startButton.Enabled = false;
                    addtotitle1.ReadOnly = true;
                    addtotitle1.Enabled = false;
                    TestRun.Enabled = false;
                    test.Enabled = false;
                    chooseFile.Enabled = false;
                    app = new Outlook.Application();
                    try
                    {
                        PdfReader reader = new PdfReader(file.FileName);
                        int intPageNum = reader.NumberOfPages;
                        int numofpages = 0;
                        int loc = 0;
                        int[] PagesNotSent = new int[intPageNum + 2];
                        for (int i = 0; i < intPageNum; i++)
                        {
                            PagesNotSent[i] = 0;
                        }
                        filesnames = new string[reader.NumberOfPages];
                        LoadBar.Maximum = reader.NumberOfPages;
                        for (int i = 1; i <= intPageNum; i++)
                        {
                            LoadBar.Value = i;
                            string text = PdfTextExtractor.GetTextFromPage(reader, i, new LocationTextExtractionStrategy());
                            text2 = PdfTextExtractor.GetTextFromPage(reader, i - numofpages, new LocationTextExtractionStrategy());
                            string Account = "";
                            if (IsLastPage(text) == false)
                            {
                                numofpages++;
                            }
                            else
                            {
                                Account = SearchForWord(text2);
                                string filename = filenamecalc("File");
                                filesnames[i - 1] = filename;
                                string PSS = getPasswordByAccount(Account);
                                string EMAIL = GetMailFromAccount(Account);
                                if (EMAIL != null && EMAIL != "")
                                {
                                    createPDFFile(filename, i, numofpages, reader, PSS);
                                    deleteFile(filename + ".pdf");
                                    try
                                    {
                                        sendMail(EMAIL, filename, 0);
                                        if (ToPrint(Account))
                                        {
                                            for (int j = numofpages; j >= 0; j--)
                                            {
                                                PagesNotSent[loc] = i - j;
                                                loc++;
                                            }
                                        }
                                    }
                                    catch
                                    {
                                        for (int j = numofpages; j >= 0; j--)
                                        {
                                            PagesNotSent[loc] = i - j;
                                            loc++;
                                        }
                                    }
                                    EMAIL = GetSecondMailFromAccount(Account);
                                    if (EMAIL != null && EMAIL != "")
                                    {
                                        try
                                        {
                                            sendMail(EMAIL, filename, 0);
                                        }
                                        catch
                                        {

                                        }
                                    }
                                }

                                else
                                {
                                    for (int j = numofpages; j >= 0; j--)
                                    {
                                        PagesNotSent[loc] = i - j;
                                        loc++;
                                    }
                                }
                                numofpages = 0;
                                deleteLocked();
                            }
                        }
                        CreateBigPDF(PagesNotSent, reader);
                        reader.Close();

                        ClearExcle();
                    }
                    catch (Exception G)
                    {
                        MessageBox.Show(G.ToString());
                        ClearExcle();
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Can't access file.");
            }
            startButton.Enabled = true;
            addtotitle1.ReadOnly = false;
            addtotitle1.Enabled = true;
            TestRun.Enabled = true;
            test.Enabled = true;
            chooseFile.Enabled = true;
            LoadBar.Value = 0;
            this.Controls.Clear();
            this.Controls.Add(DAPI);
            this.Controls.Add(D);
        }

        private void TestRun_Click(object sender, EventArgs e)/* send mail to all batabase */
        {
            file.Reset();
            file.ShowDialog();
            if (!(file.FileName == "" || !file.CheckFileExists))
            {
                bool work = true;
                try
                {
                    ExcelIt();
                }
                catch
                {
                    work = false;
                    killprocess();
                    MessageBox.Show("Can't open database file");
                }
                app = new Outlook.Application();
                if (work)
                {
                    LoadBar.Maximum = int.Parse(EndOfRows.ToString());
                    startButton.Enabled = false;
                    addtotitle1.ReadOnly = true;
                    addtotitle1.Enabled = false;
                    TestRun.Enabled = false;
                    test.Enabled = false;
                    chooseFile.Enabled = false;
                    object[,] mails = xlApp.get_Range("D2", "D" + EndOfRows).Value2;
                    object[,] mails2 = xlApp.get_Range("F2", "F" + EndOfRows).Value2;
                    for (int i = 1; i <= mails.GetLength(0); i++)
                    {
                        try
                        {

                            string AC = "";
                            if (mails[i, 1] != null)
                                AC = mails[i, 1].ToString();
                            if (AC != null && AC != "")
                            {
                                try
                                {
                                    sendMail(AC, (file.FileName), 4);
                                    string AC2 = "";
                                    if (mails2[i, 1] != null)
                                        AC2 = mails[i, 1].ToString();
                                    if (AC2 != null && AC2 != "")
                                    {
                                        try
                                        {
                                            sendMail(AC2, (file.FileName).ToString(), 4);
                                        }
                                        catch (Exception E)
                                        {
                                            MessageBox.Show(E.ToString());
                                        }
                                    }
                                }
                                catch (Exception t)
                                {
                                    MessageBox.Show(t.ToString());
                                }
                            }
                            LoadBar.Value = i;
                        }
                        catch (Exception t)
                        {
                            MessageBox.Show(t.ToString());
                        }
                    }
                    ClearExcle();
                }
            }
            startButton.Enabled = true;
            addtotitle1.ReadOnly = false;
            addtotitle1.Enabled = true;
            TestRun.Enabled = true;
            test.Enabled = true;
            chooseFile.Enabled = true;
            LoadBar.Value = 0;
            this.Controls.Clear();
            this.Controls.Add(DAPI);
            this.Controls.Add(D);
        }

        private void test_Click(object sender, EventArgs e)
        {
            if (!(file.FileName == "" || !file.CheckFileExists || file.FileName == null))
            {
                bool work = true;
                try
                {
                    ExcelIt();
                }
                catch
                {
                    work = false;
                    killprocess();
                    MessageBox.Show("Can't open database file");
                }
                if (work)
                {
                    System.IO.StreamWriter Testfile = new System.IO.StreamWriter(DASKTOPLOCATION + @"\TESTFILE.txt", false);
                    startButton.Enabled = false;
                    addtotitle1.ReadOnly = true;
                    addtotitle1.Enabled = false;
                    TestRun.Enabled = false;
                    test.Enabled = false;
                    chooseFile.Enabled = false;
                    try
                    {
                        PdfReader reader;
                        reader = new PdfReader(file.FileName);
                        int intPageNum = reader.NumberOfPages;
                        int numofpages = 0;
                        LoadBar.Maximum = reader.NumberOfPages;
                        for (int i = 1; i <= intPageNum; i++)
                        {
                            LoadBar.Value = i;
                            string text = PdfTextExtractor.GetTextFromPage(reader, i, new LocationTextExtractionStrategy());
                            text2 = PdfTextExtractor.GetTextFromPage(reader, i - numofpages, new LocationTextExtractionStrategy());
                            if (IsLastPage(text) == false)
                            {
                                numofpages++;
                            }
                            else
                            {
                                string Account = "";
                                Account = SearchForWord(text2);
                                string PSS = getPasswordByAccount(Account);
                                string EMAIL = GetMailFromAccount(Account);
                                string linetofile = " >> " + Account + "  " + (i - numofpages).ToString() + "  " + (numofpages + 1).ToString() + "  " + EMAIL + "  " + PSS;
                                Testfile.WriteLine(linetofile);
                                numofpages = 0;
                            }

                        }
                        reader.Close();
                        MessageBox.Show("FINISHED");
                    }
                    catch (Exception G)
                    {
                        MessageBox.Show(G.ToString());
                        ClearExcle();
                        this.Close();
                    }
                    Testfile.Dispose();
                    ClearExcle();
                }
            }
            startButton.Enabled = true;
            addtotitle1.ReadOnly = false;
            addtotitle1.Enabled = true;
            TestRun.Enabled = true;
            test.Enabled = true;
            chooseFile.Enabled = true;
            LoadBar.Value = 0;
            this.Controls.Clear();
            this.Controls.Add(DAPI);
            this.Controls.Add(D);
        }

        private void chooseFile_Click(object sender, EventArgs e)
        {
            file.Reset();
            file.Filter = "PDF|*.pdf";
            file.ShowDialog();
            if (!(file.FileName == "" || !file.CheckFileExists))
            {
                FilePath.Text = file.FileName;
            }
        }

    }
}
