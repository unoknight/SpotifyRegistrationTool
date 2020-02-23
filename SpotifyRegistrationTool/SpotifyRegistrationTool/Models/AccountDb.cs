using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyRegistrationTool.Models
{
    public class AccountDb
    {
        public string Guid { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string Password { get; set; }

        public string Gender { get; set; }

        public string BirthDate { get; set; }

        public string CardContact { get; set; }

        public string Status { get; set; }

        public string Address { get; set; }
    }
}
