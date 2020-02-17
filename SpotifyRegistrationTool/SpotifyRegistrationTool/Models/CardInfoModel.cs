using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyRegistrationTool.Models
{
    public class CardInfoModel
    {
        public string CardNumber { get; set; }

        public string ExpiryMonth { get; set; }

        public string ExpiryYear { get; set; }

        public string SecurityCode { get; set; }

        public string ZipCode { get; set; }
    }
}
