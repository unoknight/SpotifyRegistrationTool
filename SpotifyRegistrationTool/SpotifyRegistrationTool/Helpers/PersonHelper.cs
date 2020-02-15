using SpotifyRegistrationTool.Enums;
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
    }
}
