using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharper.Logger
{
    public static class Log
    {
        public static void Info(string message)
        {
            Console.WriteLine(message);
        }

        public static void Info(string message, bool lineBreak)
        {
            //Console.ForegroundColor = ConsoleColor.White;

            //if (lineBreak)
        }
    }
}
