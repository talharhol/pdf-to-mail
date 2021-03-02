using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;


namespace ChooseName
{
    class FolderHandler
    {
        List<bool> FilesToPrint = new List<bool>();
        Logger logger;
        FolderBrowserDialog folder = new FolderBrowserDialog();
        List<PdfHandler> pdfFiles = new List<PdfHandler>();

        public FolderHandler(Logger logger)
        {

            this.logger = logger;
            logger.Log("Waiting For Input");
            logger.Log("selected: " + (folder.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(folder.SelectedPath) ? folder.SelectedPath : "No older Selected"));
        }

        ~FolderHandler()
        {
            Close();
        }

        public void LoadDirectory()
        {
            if (IsFolderValid())
            {
                pdfFiles.Clear();
                FilesToPrint.Clear();
                foreach (string filePath in Directory.GetFiles(folder.SelectedPath, "*.pdf"))
                {
                    PdfHandler pdfFile = new PdfHandler(logger, filePath);
                    pdfFile.LoadPdf();
                    pdfFiles.Add(pdfFile);
                    FilesToPrint.Add(false);
                }
            }
        }

        public int NumerOfFiles()
        {
            return pdfFiles.Count;
        }

        public PdfHandler GetFile(int fileNumber)
        {
            return pdfFiles[fileNumber];
        }

        public void AddPagesToPrint(int fileNumber)
        {
            FilesToPrint[fileNumber] = true;
        }

        public string Print()
        {
            if (FilesToPrint.Count > 0)
            {
                logger.Log("Printing...");
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                PdfCopy copy = new PdfCopy(document, new FileStream(Consts.DesktopLocation + Consts.PrintName, FileMode.Create));
                document.Open();
                for (int fileIndex = 0; fileIndex < pdfFiles.Count; fileIndex++)
                {
                    for(int page = FilesToPrint[fileIndex] ? 1 : (pdfFiles[fileIndex].NumerOfPages() / 2) + 1; page <= pdfFiles[fileIndex].NumerOfPages(); page++)
                    {
                        copy.AddPage(pdfFiles[fileIndex].GetPage(page, copy));
                    }
                }
                document.Close();
                logger.Log("Printed successfully");
                return Consts.DesktopLocation + Consts.PrintName;
            }
            return "";
        }

        public string GetFolderPath()
        {
            return folder.SelectedPath;
        }

        public bool IsFolderValid()
        {
            return !string.IsNullOrEmpty(folder.SelectedPath) && Directory.Exists(folder.SelectedPath);
        }

        public void Close()
        {
            foreach(PdfHandler pdfFile in pdfFiles)
            {
                pdfFile.Close();
            }
        }

        private void DeleteTempFiles()
        {
            
        }
    }
}
