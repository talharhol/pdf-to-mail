using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;


namespace ChooseName
{
    class FolderHandler
    {
        List<PdfHandler> FilesToPrint = new List<PdfHandler>();
        Logger logger;
        FolderBrowserDialog folder = new FolderBrowserDialog();
        List<PdfHandler> pdfFiles = new List<PdfHandler>();
        string src_folder_path;
        string dst_folder_path;
        public FolderHandler(Logger logger, string src_folder_path, string dst_folder_path)
        {

            this.logger = logger;
            this.src_folder_path = src_folder_path;
            this.dst_folder_path = dst_folder_path;
            LoadDirectory();
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
                foreach (string filePath in Directory.GetFiles(src_folder_path, "*.pdf"))
                {
                    PdfHandler pdfFile = new PdfHandler(logger, filePath);
                    pdfFile.LoadPdf();
                    pdfFiles.Add(pdfFile);
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

        public void MoveFile(int fileNumber)
        {
            string path = pdfFiles[fileNumber].GetFilePath();
            File.Move(path, System.IO.Path.Combine(dst_folder_path, System.IO.Path.GetFileName(path)));
        }

        public string GetFolderPath()
        {
            return folder.SelectedPath;
        }

        public bool IsFolderValid()
        {
            return !string.IsNullOrEmpty(src_folder_path) && Directory.Exists(src_folder_path);
        }

        public void Close()
        {
            foreach(PdfHandler pdfFile in pdfFiles)
            {
                pdfFile.Close();
            }
        }
    }
}
