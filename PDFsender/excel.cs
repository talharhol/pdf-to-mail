using System.Text;
using System.IO;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System;

namespace ChooseName
{
    class ExcelApp
    {
        private Excel.Application xlApp;
        private Excel.Workbook xlWorkBook;
        private Excel.Worksheet xlWorkSheet;
        private string numberOfRows;

        public ExcelApp()
        {
            try
            {
                InitExcel(GetFilePath(), Consts.ExcelPassword);
            }
            catch
            {
                KillExcelProcess();
                throw;
            }
        }

        public static string SaveFilePath()
        {
            OpenFileDialog file = new OpenFileDialog();
            file.FileName = "";
            file.Filter = "Excel|*.xlsx";
            file.ShowDialog();
            if (file.FileName == "" || !file.CheckFileExists)
                return "";
            using (FileStream fs = File.Create("DATA.txt"))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(file.FileName)));
                fs.Write(info, 0, info.Length);
            }
            return file.FileName;
        }

        public string GetPassword(Account account)
        {
            return GetCellByAccount(Consts.PasswordRow, account);
        }

        public string GetMail(Account account)
        {
            return GetCellByAccount(Consts.EmailRow, account);
        }

        public string GetSecondMail(Account account)
        {
            return GetCellByAccount(Consts.SecondEmailRow, account);
        }

        public bool GetPrint(Account account)
        {
            return GetCellByAccount(Consts.PrintRow, account) == Consts.PrintValue;
        }

        public string GetCellByAccount(string column, Account account)
        {
            object[,] str = xlApp.get_Range(Consts.AccountRow + "2", Consts.AccountRow + numberOfRows).Value2;

            for (int i = 1; i <= str.GetLength(0); i++)
            {
                if (account.IsAccountMatch(str[i, 1].ToString()))
                {
                    if (xlApp.get_Range(column + (i + 1).ToString()).Value2 != null)
                        return xlApp.get_Range(column + (i + 1).ToString()).Value2.ToString();
                }

            }
            return "";
        }

        public void Close()
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
            KillExcelProcess();
        }

        private void KillExcelProcess()
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

        private void InitExcel(string filePath, string password)
        {
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(filePath, 2, true, 5, password);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets[1];
            xlApp.Visible = false;
            numberOfRows = CalcNumberOfRows();
        }

        private string GetFilePath()
        {
            if (File.Exists("DATA.txt"))
            {
                using (StreamReader sr = File.OpenText("DATA.txt"))
                {
                    string filePath = "";
                    if ((filePath = sr.ReadLine()) != null)
                    {
                        return System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(filePath));
                    }
                }
            }
            return SaveFilePath();
        }

        private string CalcNumberOfRows()
        {
            int i = 1;
            do
            {
                i++;

            } while (xlApp.get_Range("A" + i.ToString()).Value2 != null);
            return (i - 1).ToString();
        }
    }
}
