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
        List<bool> pagesToPrint = new List<bool>();
        List<string> filenames = new List<string>();
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
                for (int i = 0; i <= this.NumerOfPages(); i++)
                {
                    pagesToPrint.Add(false);
                }
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

        public string CreateSubFile(List<int> pages, string password)
        {
            DeleteTempFiles();
            string filename = GetFileName();
            string locked_filename = GetFileName("_locked");
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            PdfCopy copy = new PdfCopy(document, new FileStream(filename, FileMode.Create));
            document.Open();
            foreach (int page in pages)
            {
                copy.AddPage(copy.GetImportedPage(reader, page));
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

        public void AddPagesToPrint(List<int> pages)
        {
            foreach (int page in pages)
            {
                pagesToPrint[page] = true;
            }
        }

        public string Print()
        {
            if (pagesToPrint.Contains(true))
            {
                logger.Log("Printing...");
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                PdfCopy copy = new PdfCopy(document, new FileStream(Consts.DesktopLocation + Consts.PrintName, FileMode.Create));
                document.Open();
                for (int i = 1; i < pagesToPrint.Count; i++)
                {
                    if (pagesToPrint[i])
                    {
                        copy.AddPage(copy.GetImportedPage(reader, i));
                    }
                }
                document.Close();
                logger.Log("Printed successfully");
                return Consts.DesktopLocation + Consts.PrintName;
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
                    filenames.Add(filenamesArry[i]);
                }
            }
        }

        private string GetFileName(string appendix = "")
        {
            int index = 0;
            string addToAppendix = "";
            while (filenames.Contains("File" + addToAppendix + appendix + ".pdf"))
            {
                addToAppendix = index.ToString();
                index++;
            }
            filenames.Add("File" + addToAppendix + appendix + ".pdf");
            return "File" + addToAppendix + appendix + ".pdf";
        }
    }
}
