using SpotifyRegistrationTool.Enums;
using SpotifyRegistrationTool.Logic;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyRegistrationTool.Helpers
{
    public static class PersonHelper
    {
        public static GenderEnum GetRandomGender()
        {
            Random random = new Random();

            int randomNumner = random.Next(1, 3);

            if (randomNumner == 1)
                return GenderEnum.Female;

            if (randomNumner == 2)
                return GenderEnum.Male;

            if (randomNumner == 3)
                return GenderEnum.Other;

            return GenderEnum.Female;

        }

        public static string GetRandomAddress()
        {
            if (Common.Addresses == null || Common.Addresses.Count == 0)
            {
                return string.Empty;
            }
            Random random = new Random();
            int randomNumner = random.Next(0, Common.Addresses.Count - 1);
            return Common.Addresses[randomNumner].Address1;
        }
    }
}
