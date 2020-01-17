using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChooseName
{
    class Consts
    {
        public const string PasswordRow = "E";
        public const string AccountRow = "A";
        public const string EmailRow = "D";
        public const string SecondEmailRow = "F";
        public const string PrintRow = "G";
        public const string PrintValue = "#";
        public const string ExcelPassword = "alibaba";
        public static string DesktopLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public const string PrintName = @"\דפי_תקציב_להדפסה.pdf";
        public const string Subject = "דף תקציב ";
        public const string Title = "שליחת דפי תקציב";
        public const string EndOfPageSeperator = "#";
        public static System.util.RectangleJ AccountArea = new System.util.RectangleJ((int)(4.32*72), (int)(10.91*72), (int)(0.9*72), (int)(0.5*72));
        public static Regex AccountRegex = new Regex(@"(?<Account>[0-9]*)", RegexOptions.Compiled);
        public const bool ReverseAccount = false;
        public const bool AccountIsNumber = true;
        public const int RelativeMonth = -1;
    }
}
