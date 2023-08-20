using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClubMembershipApplication
{
    public static class CommonOutputFormat
    {
        public enum FontTheme
        {
            Default,
            Danger,
            Success
        }
        public static void ChangeFontColor(FontTheme fontTheme)
        {
            switch (fontTheme)
            {
                case FontTheme.Danger:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case FontTheme.Success:
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }
        }
    }
}
