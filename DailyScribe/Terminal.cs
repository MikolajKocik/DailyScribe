using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyScribe
{
    public class Terminal
    {
        public void View()
        {
            Console.Title = "Daily Scribe";
            Console.ForegroundColor = ConsoleColor.Magenta;
        }
    }
}
