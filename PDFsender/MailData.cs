using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseName
{
    class MailData
    {
        readonly public string dstMail;
        readonly public string id;
        readonly public List<int> pages;
        readonly public string password;

        public MailData(string id, string email, List<int> pages, string password)
        {
            this.id = id;
            this.dstMail = email;
            this.pages = pages;
        }
    }
}
