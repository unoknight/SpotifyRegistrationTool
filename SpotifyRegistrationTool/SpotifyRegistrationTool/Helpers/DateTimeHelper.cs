using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyRegistrationTool.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime DateTimeRandom(DateTime start, DateTime end)
        {
            Random gen = new Random();
            int range = (end - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}
