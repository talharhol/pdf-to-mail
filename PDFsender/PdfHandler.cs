using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;


namespace ChooseName
{
    class PdfHandler
    {
        OpenFileDialog file = new OpenFileDialog();
        PdfReader reader = null;
        Queue<int> pagesToPrint = new Queue<int>();
        Queue<string> filenames = new Queue<string>();
        Logger logger;

        public PdfHandler(Logger logger)
        {

            this.logger = logger;
            logger.Log("Waiting for input");
            file.Filter = "PDF|*.pdf";
            file.ShowDialog();
            logger.Log("selected: " + (file.FileName != "" ? file.FileName : "No File Selected"));
        }

        ~PdfHandler()
        {
            Close();
        }

        public void LoadPdf()
        {
            if (IsFileValid())
            {
                if (reader != null)
                    reader.Close();
                reader = new PdfReader(file.FileName);
                pagesToPrint.Clear();
            }
        }

        public string GetTextFromArea(int page, System.util.RectangleJ rect)
        {
            RenderFilter[] filter = { new RegionTextRenderFilter(rect) };
            ITextExtractionStrategy strategy;
            strategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), filter);
            return PdfTextExtractor.GetTextFromPage(reader, page, strategy);
        }

        public int NumerOfPages()
        {
            return reader.NumberOfPages;
        }

        public string GetTextFromPage(int pageNumber)
        {
            return PdfTextExtractor.GetTextFromPage(reader, pageNumber, new LocationTextExtractionStrategy());
        }

        public string Slice(int startPage, int length, string password)
        {
            DeleteTempFiles();
            string filename = GetFileName();
            string locked_filename = GetFileName("_locked");
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            PdfCopy copy = new PdfCopy(document, new FileStream(filename, FileMode.Create));
            document.Open();
            for (int i = 0; i < length; i++)
            {
                copy.AddPage(copy.GetImportedPage(reader, startPage + i));
            }
            document.Close();

            using (Stream input = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (Stream output = new FileStream(locked_filename, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    PdfReader moreReader = new PdfReader(input);
                    PdfEncryptor.Encrypt(moreReader, output, true, password, "kinneretPDF", PdfWriter.ALLOW_PRINTING);
                }
            }
            return locked_filename;
        }

        public void AddPagesToPrint(int startPage, int numberOfPages)
        {
            for (int i = 0; i < numberOfPages; i++)
            {
                pagesToPrint.Enqueue(startPage + i);
            }
        }

        public string Print()
        {
            if (pagesToPrint.Count != 0)
            {
                logger.Log("Printing...");
                string filePath = Consts.DesktopLocation + string.Format(Consts.PrintName, DateTime.Now.ToString("yyyyddMMHHmm"));
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                PdfCopy copy = new PdfCopy(document, new FileStream(filePath, FileMode.Create));
                document.Open();
                for (int i = 0; i < pagesToPrint.Count; i++)
                {
                    copy.AddPage(copy.GetImportedPage(reader, pagesToPrint.Dequeue()));
                }
                document.Close();
                logger.Log("Printed successfully");
                return filePath;
            }
            return "";
        }

        public string GetFilePath()
        {
            return file.FileName;
        }

        public bool IsFileValid()
        {
            return file.FileName != "" && file.CheckFileExists;
        }

        public void Close()
        {
            if (reader != null)
                reader.Close();
            do
            {
                DeleteTempFiles();
                System.Threading.Thread.Sleep(200);
            } while (filenames.Count != 0);
        }

        private void DeleteTempFiles()
        {
            string[] filenamesArry = filenames.ToArray();
            filenames.Clear();
            for (int i = 0; i < filenamesArry.Length; i++)
            {
                try
                {
                    File.Delete(filenamesArry[i]);
                }
                catch
                {
                    filenames.Enqueue(filenamesArry[i]);
                }
            }
        }

        private string GetFileName(string appendix="")
        {
            int index = 0;
            string addToAppendix = "";
            while (filenames.Contains("File" + addToAppendix + appendix + ".pdf"))
            {
                addToAppendix = index.ToString();
                index++;
            }
            filenames.Enqueue("File" + addToAppendix + appendix + ".pdf");
            return "File" + addToAppendix + appendix + ".pdf";
        }
    }
}
