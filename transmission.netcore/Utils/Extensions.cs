using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace transmission.netcore.Utils
{
    public static class Extensions {
        public static string ToSizeString(this long number) {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (number == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(number);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(number) * num).ToString() + suf[place];
        }
    }
}
