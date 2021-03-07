using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseName
{
    class PdfData
    {
        public int PageNumber;
        public int NumberOfPages;
        public Account account;
        public PdfData(Account account, int pageNumber, int numberOfPages)
        {
            this.account = account;
            PageNumber = pageNumber;
            NumberOfPages = numberOfPages;
        }

        public List<int> getPages()
        {
            List<int> pages = new List<int>();
            for (int i=0; i < this.NumberOfPages; i++)
            {
                pages.Add(this.PageNumber + i);
            }
            return pages;
        }
    }
}
