using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyRegistrationTool.Models
{
    public class RegisterSettingModel
    {
        public string Url { get; set; }
        public TagSettingModel SiteKey { get; set; }
        public TagSettingModel CaptchaResponse { get; set; }
        public TagSettingModel Email { get; set; }
        public TagSettingModel ConfirmEmail { get; set; }
        public TagSettingModel Password { get; set; }
        public TagSettingModel DisplayName { get; set; }
        public TagSettingModel Day { get; set; }
        public TagSettingModel Month { get; set; }
        public TagSettingModel Year { get; set; }
        public TagSettingModel Male { get; set; }
        public TagSettingModel Female { get; set; }
        public TagSettingModel Neutral { get; set; }
        public TagSettingModel SubmitForm { get; set; }

    }


    public class RegisterPremiumModel
    {
        public string Url { get; set; }
        public TagSettingModel CardNumber { get; set; }
        public TagSettingModel ExpiryMonth { get; set; }
        public TagSettingModel ExpiryYear { get; set; }
        public TagSettingModel SecurityCode { get; set; }
        public TagSettingModel ZipCode { get; set; }
        public TagSettingModel SubmitForm { get; set; }
    }

    public class TagSettingModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
