using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMChat.Helpers
{
    public class CustomHelpers
    {
        public static string GetTime (DateTime inicio, DateTime fin)
        {
            TimeSpan time = fin - inicio;

            if (time.Days != 0)
            {
                return $"{time.Days}d {time.Hours}h";
            }
            else if (time.Hours != 0)
            {
                return $"{time.Hours}h {time.Minutes}m";
            }
            else if (time.Minutes != 0)
            {
                return $"{time.Minutes}m {time.Seconds}s";
            }

            return "0";
        }
    }
}
