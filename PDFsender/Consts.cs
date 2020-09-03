using System;
using System.Text.RegularExpressions;


namespace ChooseName
{
    class Consts
    {
        public const string PasswordRow = "E";
        public const string EmptyAccount = "-1";
        public const string AccountRow = "A";
        public const string EmailRow = "D";
        public const string SecondEmailRow = "F";
        public const string PrintRow = "G";
        public const string PrintValue = "#";
        public const string ExcelPassword = "1234";
        public static string DesktopLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public const string PrintName = @"\חיוב_חודשי_להדפסה.pdf";
        public const string Subject = "חיוב חודשי מעיין ברוך ";
        public const string Title = "חיוב חודשי";
        public const string EndOfPageSeperator = "כ\"הס";
        public static System.util.RectangleJ AccountArea = new System.util.RectangleJ(310, 680, 230, 22);
        public static Regex AccountRegex = new Regex(@"(?<Account>[0-9]{9,})", RegexOptions.Compiled);
        public const bool ReverseAccount = false;
        public const bool AccountIsNumber = true;
        public const int RelativeMonth = -1;
        public const string CacheFile = "DATA.txt";
    }
}
