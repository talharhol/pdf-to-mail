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
        public Account account;
        public PdfData(Account account, int fileNumber)
        {
            this.account = account;
            FileNumber = fileNumber;
        }
    }
}
