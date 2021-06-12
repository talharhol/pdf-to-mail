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
        public const string PrintName = @"\חשבונית_חודשית_להדפסה.pdf";
        public const string Subject = "חשבונית חודשית ";
        public const string Title = "חשבונית חודשית";
        public const string EndOfPageSeperator = "";
        public static System.util.RectangleJ AccountArea = new System.util.RectangleJ(117, 677, 127, 27);
        public static Regex AccountRegex = new Regex(@"(?<Account>[0-9]*)", RegexOptions.Compiled);
        public const bool ReverseAccount = false;
        public const bool AccountIsNumber = true;
        public const int RelativeMonth = -1;
        public const string CacheFile = "DATA.txt";
    }
}
