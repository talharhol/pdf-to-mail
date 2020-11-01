using System.Collections.Generic;


namespace ChooseName
{
    class Account
    {
        private string account;
        private ExcelApp excel;
        Logger logger;
        List<PdfHandler> FilesToMail = new List<PdfHandler>();
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

        public void AddFile(PdfHandler FileToMail)
        {
            FilesToMail.Add(FileToMail);
        }
        
        public string CreateMailFile()
        {
            if (FilesToMail.Count > 0)
            {
                return FilesToMail[0].MergeFiles(FilesToMail, Password());
            }
            return "";
        }

        public List<PdfHandler> Files()
        {
            return FilesToMail;
        }

        public override bool Equals(object obj)
        {
            return obj is Account && IsAccountMatch((obj as Account).GetAccount());
        }
    }
}
