using SpotifyRegistrationTool.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.Specialized;

namespace SpotifyRegistrationTool.Logic
{
    public static class AccountLogic
    {
        public static List<Models.Account> GenerateAccounts(StringCollection cardsContact)
        {
            List<Models.Account> accounts = new List<Models.Account>();

            foreach (var card in cardsContact)
            {
                if (!string.IsNullOrEmpty(card) && card != "\r\n" && card != "\n")
                {
                    accounts.Add(GenerateAccount(card));
                }
            }

            return accounts;
        }

        private static Models.Account GenerateAccount(string cardContact)
        {
            Models.Account account = new Models.Account()
            {
                Guid = Guid.NewGuid().ToString().ToUpper(),
                BirthDate = DateTimeHelper.DateTimeRandom(new DateTime(1970, 1, 1), new DateTime(2001, 1, 1)),
                CardContact = cardContact,
                Gender = PersonHelper.GetRandomGender(),
                Email = StringHelper.RandomEmail() + Common.EMAIL_DOMAINS.GetRandomElementFromStringArray(),
                Status = Enums.AccountStatusEnum.None,
                Password = StringHelper.GetRandomPassword(9, 12),
                _isRunning = false,
            };
            account.DisplayName = StringHelper.RandomDisplayName(account.Gender);
            return account;
        }

       
    }
}
