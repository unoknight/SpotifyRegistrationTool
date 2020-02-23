using RandomNameGeneratorLibrary;
using SpotifyRegistrationTool.Enums;
using SpotifyRegistrationTool.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using System.Windows.Controls;

namespace SpotifyRegistrationTool.Helpers
{
    public static class StringHelper
    {
        public static string GetRandomPassword(int minLength, int maxLength)
        {
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            bool digit = true;
            bool nonAlphanumeric = true;
            bool lowercase = true;
            bool uppercase = true;

            int length = random.Next(minLength, maxLength);

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }

        public static string RandomEmail()
        {
            Random random = new Random();
            int size = random.Next(9, 12);
            string randomno = "abcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                if (i == size - 1 && random.Next(1, 2) == 2)
                {
                    ch = randomno[random.Next(26, randomno.Length)];
                }
                else
                {
                    ch = randomno[random.Next(0, 25)];
                }

                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static string RandomDisplayName(GenderEnum genderEnum)
        {
            var personGenerator = new PersonNameGenerator();
            string displayName = string.Empty;
            switch (genderEnum)
            {
                case GenderEnum.Female:
                    displayName = personGenerator.GenerateRandomFemaleFirstAndLastName();
                    break;
                case GenderEnum.Male:
                    displayName = personGenerator.GenerateRandomMaleFirstAndLastName();
                    break;
                case GenderEnum.Other:
                    displayName = personGenerator.GenerateRandomFirstAndLastName();
                    break;
                default:
                    displayName = personGenerator.GenerateRandomFirstAndLastName();
                    break;
            }

            return displayName;
        }

        public static StringCollection GetLinesCollectionFromTextBox(TextBox textBox)
        {
            StringCollection lines = new StringCollection();

            // lineCount may be -1 if TextBox layout info is not up-to-date.
            int lineCount = textBox.LineCount;

            for (int line = 0; line < lineCount; line++)
                // GetLineText takes a zero-based line index.
                lines.Add(textBox.GetLineText(line));

            return lines;
        }

        public static CardInfoModel GetCardInfo(string cardInfo)
        {
            var arrStr = cardInfo.Split('|');

            CardInfoModel result = new CardInfoModel()
            {
                CardNumber = arrStr[0],
                ExpiryMonth = arrStr[1],
                ExpiryYear = arrStr[2],
                SecurityCode = arrStr[3],
                ZipCode = arrStr[4]
            };
            return result;
        }

        public static List<string> StringCollectionToList(this StringCollection collection)
        {
            List<string> result = new List<string>();

            foreach (var item in collection)
            {
                result.Add(item);
            }

            return result;
        }

        public static string GetContentJsonFile(string fileName)
        {
            var myJsonString = File.ReadAllText(fileName);
            return myJsonString;
        }

    }
}
