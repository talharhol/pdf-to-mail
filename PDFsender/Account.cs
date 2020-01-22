using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChooseName
{
    class Account
    {
        private string account;
        private ExcelApp excel;
        public Account(string account, ExcelApp excel)
        {
            if (Consts.AccountIsNumber)
            {
                long accountNumber;
                if (!long.TryParse(account, out accountNumber)) accountNumber = -1;
                account = accountNumber.ToString();
            }
            this.account = account;
            this.excel = excel;
        }
        public bool IsAccountMatch(string account)
        {
            if (Consts.AccountIsNumber)
            {
                long accountNumber;
                if (!long.TryParse(account, out accountNumber)) return false;
                account = accountNumber.ToString();
            }
            return this.account == account && this.account != Consts.EmptyAccount;
        }
        public string[] Mails()
        {
            List<string> mails = new List<string>();
            string mail;
            if ((mail = excel.GetMail(this)) != "") mails.Add(mail);
            if ((mail = excel.GetSecondMail(this)) != "") mails.Add(mail);

            return mails.ToArray();
        }
        public string Password()
        {
            return excel.GetPassword(this);
        }
        public bool IsPrint()
        {
            return excel.GetPrint(this);
        }
        public string GetAccount()
        {
            return account;
        }
        
    }
}
