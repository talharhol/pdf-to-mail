using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Consts = ChooseName.Consts;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf.fonts.cmaps;

namespace ChooseName
{
    class PdfHandler
    {
        private OpenFileDialog file = new OpenFileDialog();
        private PdfReader reader;
        private int[] pagesToPrint;
        Queue<string> filenames = new Queue<string>();
        private int loc = 0;

        public PdfHandler()
        {
            file.Filter = "PDF|*.pdf";
            file.ShowDialog();
        }

        public void LoadPdf()
        {
            if (IsFileValid())
            {
                reader = new PdfReader(file.FileName);
                pagesToPrint = new int[NumerOfPages()];
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

        public void AddPagesToPrint(int startPage, int numberOfPages)
        {
            for (int i = 0; i <= 0; i++)
            {
                pagesToPrint[loc] = startPage + i;
                loc++;
            }
        }

        public string Print()
        {
            if (pagesToPrint[0] != 0)
            {
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                PdfCopy copy = new PdfCopy(document, new FileStream(Consts.DesktopLocation + Consts.PrintName, FileMode.Create));
                document.Open();
                for (int i = 0; pagesToPrint[i] != 0; i++)
                {
                    copy.AddPage(copy.GetImportedPage(reader, pagesToPrint[i]));
                }
                document.Close();
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
