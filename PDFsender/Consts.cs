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
        public const string PrintName = @"\PDF_To_Print_Unlocked_Taktziv.pdf";
        public const string Subject = "תקציב ";
        public const string Title = "אלומות תקציב";
        public const string EndOfPageSeperator = "#";
        public static System.util.RectangleJ AccountArea = new System.util.RectangleJ((int)(5.5*72), (int)(10.9*72), (int)(0.8*72), (int)(0.5*72));
        public static Regex AccountRegex = new Regex(@"(?<Account>[0-9]*)", RegexOptions.Compiled);
        public const bool ReverseAccount = false;
        public const bool AccountIsNumber = true;
        public const int RelativeMonth = -1;
        public const string CacheFile = "DATA.txt";
    }
}
