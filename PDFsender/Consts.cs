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
        public const string EmptyAccount = "-1";
        public const string AccountRow = "A";
        public const string EmailRow = "D";
        public const string SecondEmailRow = "F";
        public const string PrintRow = "G";
        public const string PrintValue = "#";
        public const string ExcelPassword = "alibaba";
        public const string CacheFile = "DATA.txt";
        public static string DesktopLocation = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public const string PrintName = @"\קובץ_להדפסה.pdf";
        public const string Subject = "כותרת למייל";
        public const string Title = "כותרת לתוכנה";
        public const string EndOfPageSeperator = "סימון לסוף הדף";
        public static System.util.RectangleJ AccountArea = new System.util.RectangleJ(455, 750, 42, 28);
        public static Regex AccountRegex = new Regex(@"(?<Account>[0-9]*)", RegexOptions.Compiled);
        public const bool ReverseAccount = false;
        public const bool AccountIsNumber = true;
        public const int RelativeMonth = -1;
    }
}
