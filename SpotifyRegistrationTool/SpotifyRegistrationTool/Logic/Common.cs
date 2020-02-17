using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyRegistrationTool.Logic
{
    public static class Common
    {
        public const string APP_NAME = "Spotify Registration Premium";

        public static string[] EMAIL_DOMAINS = { "@z6vn.com" };

        public const string REGISTER_URL = "https://www.spotify.com/vn-en/signup/";

        public const string CAPTCHA_API = "https://2captcha.com/in.php?key={0}&method=userrecaptcha&googlekey={1}&pageurl={2}";

        public const string GET_PASS_CAPTCHA_API = "https://2captcha.com/res.php?key={0}&action=get&id={1}";


    }
}
