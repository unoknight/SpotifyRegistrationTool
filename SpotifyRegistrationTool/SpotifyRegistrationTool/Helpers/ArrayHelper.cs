using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyRegistrationTool.Helpers
{
    public static class ArrayHelper
    {
        public static string GetRandomElementFromStringArray(this string[] arr)
        {
            Random random = new Random();
            return arr[random.Next(0, arr.Length - 1)];
        }
    }
}
