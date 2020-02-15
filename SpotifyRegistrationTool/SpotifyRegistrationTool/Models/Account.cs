using SpotifyRegistrationTool.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyRegistrationTool.Models
{
    public class Account
    {
        public string Guid { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public GenderEnum Gender { get; set; }

        public DateTime BirthDate { get; set; }

        public string CardContact { get; set; }

        public string GenderDisplay { get { return Gender.ToString(); } }

        public string BirthDateDisplay { get { return BirthDate.ToString("yyyy-MM-dd"); } }

        public AccountStatusEnum Status { get; set; }

        public string StatusDisplay { get { return Status.ToString(); } }
    }
}
