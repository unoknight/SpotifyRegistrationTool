using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SpotifyRegistrationTool.Enums;
using SpotifyRegistrationTool.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace SpotifyRegistrationTool.Logic
{
    public static class DriverManager
    {

        static Random random = new Random();

        public static ChromeDriver StartDriver(ProxyTypeEnum proxyType = ProxyTypeEnum.Http, string proxy = "", bool isDisplay = true)
        {
            var driverService = ChromeDriverService.CreateDefaultService();
            driverService.HideCommandPromptWindow = true;

            ChromeOptions options = new ChromeOptions();
            if (!string.IsNullOrEmpty(proxy))
            {
                switch (proxyType)
                {
                    case ProxyTypeEnum.Socks5:
                        options.AddArguments($@"--proxy-server=socks5://{proxy}");
                        break;
                    case ProxyTypeEnum.Socks4:
                        options.AddArguments($@"--proxy-server=socks4://{proxy}");
                        break;
                    case ProxyTypeEnum.Http:
                        options.AddArguments($@"--proxy-server=http://{proxy}");
                        break;
                    default:
                        break;
                }
            }

            options.AddArguments("no-sandbox");

            if (!isDisplay)
            {
                options.AddArguments("headless");
                options.AddArguments("--silent-launch");
                options.AddArguments("-no-startup-window");

            }

            ChromeDriver driver = new ChromeDriver(driverService, options);
            driver.Manage().Window.Maximize();
            return driver;
        }


        public static string GetApi(string url)
        {
            try
            {
                HttpClient client = new HttpClient();

                ServicePointManager.ServerCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
                var request = client.GetAsync(url).Result;

                if (request.IsSuccessStatusCode)
                {
                    var responseText = request.Content.ReadAsStringAsync().Result;
                    return responseText;
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static void ClickElement(this ChromeDriver driver, IWebElement element)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element).Click().Perform();
        }

        [Obsolete]
        public static IWebElement FindElementWait(this ChromeDriver driver, TagSettingModel tag,int seconds = 2)
        {
            try
            {
                By by = null;

                if (tag.Type == "xpath")
                {
                    by = By.XPath(tag.Value);
                }

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
                IWebElement element = wait.Until(ExpectedConditions.ElementExists(by));
                return element;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static void FillTextToTextBox(this ChromeDriver driver, IWebElement element, string content)
        {
            string[] inputTagsName = new string[] { "input", "textarea" };

            var arrContent = content.ToCharArray();

            Actions action = new Actions(driver);
            action.MoveToElement(element).Click().Perform();
            element.Clear();

            Thread.Sleep(random.Next(1000, 2000));
            for (int i = 0; i < arrContent.Length; i++)
            {
                element.SendKeys(arrContent[i].ToString());
                Thread.Sleep(random.Next(200, 400));
            }
        }

    }
}
