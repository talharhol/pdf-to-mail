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

        public PdfImportedPage GetPage(int pageNumber, PdfCopy copy)
        {
            return copy.GetImportedPage(reader, pageNumber);
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
        }
    }
}
