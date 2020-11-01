using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;


namespace ChooseName
{
    class PdfHandler
    {
        PdfReader reader = null;
        int[] pagesToPrint;
        Queue<string> filenames = new Queue<string>();
        int loc = 0;
        Logger logger;
        readonly string filePath = "";

        public PdfHandler(Logger logger, string filePath)
        {

            this.logger = logger;
            this.filePath = filePath;
            logger.Log("current: " + filePath);
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
                reader = new PdfReader(filePath);
                pagesToPrint = new int[NumerOfPages() + 1];
                loc = 0;
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

        public PdfImportedPage GetPage(int pageNumber, PdfCopy copy)
        {
            return copy.GetImportedPage(reader, pageNumber);
        }

        public string Slice(int startPage, int length, string password)
        {
            DeleteTempFiles();
            string filename = GetFileName();
            string locked_filename = GetFileName("_locked");
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            PdfCopy copy = new PdfCopy(document, new FileStream(filename, FileMode.Create));
            document.Open();
            for (int i = 0; i <= length; i++)
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

        public string MergeFiles(List<PdfHandler> files, string password)
        {
            DeleteTempFiles();
            string filename = GetFileName();
            string locked_filename = GetFileName("_locked");
            iTextSharp.text.Document document = new iTextSharp.text.Document();
            PdfCopy copy = new PdfCopy(document, new FileStream(filename, FileMode.Create));
            document.Open();
            foreach (PdfHandler file in files)
            {
                for (int page = 1; page <= file.NumerOfPages(); page++)
                {
                    copy.AddPage(file.GetPage(page, copy));
                }
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
                pagesToPrint[loc] = startPage + i;
                loc++;
            }
        }

        public string Print()
        {
            if (pagesToPrint[0] != 0)
            {
                logger.Log("Printing...");
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                PdfCopy copy = new PdfCopy(document, new FileStream(Consts.DesktopLocation + Consts.PrintName, FileMode.Create));
                document.Open();
                for (int i = 0; pagesToPrint[i] != 0; i++)
                {
                    copy.AddPage(copy.GetImportedPage(reader, pagesToPrint[i]));
                }
                document.Close();
                logger.Log("Printed successfully");
                return Consts.DesktopLocation + Consts.PrintName;
            }
            return "";
        }

        public string GetFilePath()
        {
            return filePath;
        }

        public bool IsFileValid()
        {
            return filePath != "" && File.Exists(filePath);
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
