using System.Collections.Generic;


namespace ChooseName
{
    class Account
    {
        private string account;
        private ExcelApp excel;
        Logger logger;
        public Account(string account, ExcelApp excel, Logger logger)
        {
            if (Consts.AccountIsNumber)
            {
                long accountNumber;
                if (!long.TryParse(account, out accountNumber)) accountNumber = -1;
                account = accountNumber.ToString();
            }
            this.account = account;
            this.excel = excel;
            this.logger = logger;
            logger.Log("Account: " + account);
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

        public string PrimeryMail()
        {
            string mail = excel.GetMail(this);
            return mail != "" ? mail : null;
        }
        public string SeconderyMail()
        {
            string mail = excel.GetSecondMail(this);
            return mail != "" ? mail : null;
        }
        
    }
}
