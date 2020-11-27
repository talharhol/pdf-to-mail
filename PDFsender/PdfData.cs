using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseName
{
    class PdfData
    {
        public int FileNumber;
        public List<string> mails;
        public PdfData(List<string> mails, int fileNumber)
        {
            this.mails = mails;
            FileNumber = fileNumber;
        }
    }
}
