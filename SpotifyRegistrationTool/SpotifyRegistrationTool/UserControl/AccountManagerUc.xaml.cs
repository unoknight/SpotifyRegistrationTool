using SpotifyRegistrationTool.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Linq;
using OpenQA.Selenium.Chrome;
using SpotifyRegistrationTool.Logic;
using System.ComponentModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SpotifyRegistrationTool.Helpers;

namespace SpotifyRegistrationTool.UserControls
{
    /// <summary>
    /// Interaction logic for AccountManagerUc.xaml
    /// </summary>
    public partial class AccountManagerUc : UserControl, INotifyPropertyChanged
    {
        private bool _isStart = true;
        private ChromeDriver _driver;

        public bool IsStart
        {
            get { return _isStart; }
            set
            {
                _isStart = value;
                //PropertyChanged(this, new PropertyChangedEventArgs("IsStart"));
            }
        }

        private CancellationTokenSource cts;

        public AccountManagerUc()
        {
            InitializeComponent();

        }

        public ObservableCollection<Models.Account> _account;

        public event PropertyChangedEventHandler PropertyChanged;

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.LoadPage(mainWindow._infomationUc);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            _account = new ObservableCollection<Models.Account>(mainWindow.Accounts);
            listviewAccounts.ItemsSource = _account;
        }


        private void listviewAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedRow = listviewAccounts.SelectedItem;

            if (selectedRow != null)
            {
                btnViewDetail.IsEnabled = true;
            }
            else
            {
                btnViewDetail.IsEnabled = false;
            }
        }

        [Obsolete]
        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            IsStart = true;
            cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            btnStop.IsEnabled = true;
            btnStart.IsEnabled = false;
            btnBack.IsEnabled = false;

            try
            {
                var task = Task.Run(() =>
                {
                    token.ThrowIfCancellationRequested();
                    GenerateListTaskFromListView(mainWindow);

                    while (true)
                    {
                        if (token.IsCancellationRequested)
                        {
                            // Clean up here, then...
                            token.ThrowIfCancellationRequested();
                        }
                    }
                }, token);
                await task;
            }
            catch (OperationCanceledException)
            {
                if (_driver != null)
                {
                    _driver.Close();
                }

                ClearRunningAccount();
                IsStart = false;
                btnStop.IsEnabled = false;
                btnStart.IsEnabled = true;
                btnBack.IsEnabled = true;
            }
            finally
            {
                cts.Dispose();
            }
        }

        [Obsolete]
        private void GenerateListTaskFromListView(MainWindow mainWindow)
        {
            foreach (var item in _account)
            {
                RunRegistration(mainWindow: mainWindow, account: item);
            }
        }

        [Obsolete]
        private void RunRegistration(MainWindow mainWindow, Models.Account account, string proxy = "", ProxyTypeEnum proxyType = ProxyTypeEnum.Socks5)
        {

            ClearRunningAccount();

            RunningAccount(account.Guid);

            try
            {
                var proxies = mainWindow.Proxies.StringCollectionToList();
                _driver = DriverManager.StartDriver(proxyType: mainWindow.ProxyType, proxy: proxies.FirstOrDefault().Trim());

                _driver.Navigate().GoToUrl(mainWindow.RegisterSetting.Url);

                if (Registration(mainWindow, account))
                {
                    RegistrationPremium(mainWindow, account);
                }
                _driver.Close();
            }
            catch (Exception ex)
            {
                if (_driver != null)
                {
                    _driver.Close();
                }
            }
        }

        [Obsolete]
        void RegistrationPremium(MainWindow mainWindow, Models.Account account)
        {
            Random random = new Random();
            Thread.Sleep(random.Next(10000, 15000));

            _driver.Navigate().GoToUrl(mainWindow.RegisterPremium.Url);
            var cardInfo = StringHelper.GetCardInfo(account.CardContact);
            Thread.Sleep(random.Next(10000, 15000));

            _driver.SwitchTo().Frame(_driver.FindElementWait(mainWindow.RegisterPremium.IFrame, 10));
            Thread.Sleep(random.Next(1000, 2000));

            var cardNumberElement = _driver.FindElementWait(mainWindow.RegisterPremium.CardNumber);
            _driver.FillTextToTextBox(cardNumberElement, cardInfo.CardNumber);
            Thread.Sleep(random.Next(1000, 2000));

            var expiryMonthElement = _driver.FindElementWait(mainWindow.RegisterPremium.ExpiryMonth);
            var selectMonthElement = new SelectElement(expiryMonthElement);
            selectMonthElement.SelectByText(cardInfo.ExpiryMonth);
            Thread.Sleep(random.Next(1000, 2000));

            var expiryYearElement = _driver.FindElementWait(mainWindow.RegisterPremium.ExpiryYear);
            var selectYearElement = new SelectElement(expiryYearElement);
            selectYearElement.SelectByText(cardInfo.ExpiryYear);
            Thread.Sleep(random.Next(1000, 2000));

            var secureCodeElement = _driver.FindElementWait(mainWindow.RegisterPremium.SecurityCode);
            _driver.FillTextToTextBox(secureCodeElement, cardInfo.SecurityCode);
            Thread.Sleep(random.Next(1000, 2000));

            var zipCodeElement = _driver.FindElementWait(mainWindow.RegisterPremium.ZipCode);
            _driver.FillTextToTextBox(zipCodeElement, cardInfo.ZipCode);
            Thread.Sleep(random.Next(1000, 2000));

            var submitElement = _driver.FindElementWait(mainWindow.RegisterPremium.ZipCode);
            _driver.ClickElement(submitElement);

            Thread.Sleep(random.Next(10000, 15000));
        }


        [Obsolete]
        bool Registration(MainWindow mainWindow, Models.Account account)
        {
            Random random = new Random();
            Thread.Sleep(10000);
            _driver.Navigate().GoToUrl(mainWindow.RegisterSetting.Url);


            var emailElement = _driver.FindElementWait(mainWindow.RegisterSetting.Email);

            _driver.FillTextToTextBox(emailElement, account.Email);

            Thread.Sleep(random.Next(1000, 2000));

            var confirmEmailElement = _driver.FindElementWait(mainWindow.RegisterSetting.ConfirmEmail);
            _driver.FillTextToTextBox(confirmEmailElement, account.Email);
            Thread.Sleep(random.Next(1000, 2000));

            var passwordElement = _driver.FindElementWait(mainWindow.RegisterSetting.Password);
            _driver.FillTextToTextBox(passwordElement, account.Password);
            Thread.Sleep(random.Next(1000, 2000));

            var displayNameElement = _driver.FindElementWait(mainWindow.RegisterSetting.DisplayName);
            _driver.FillTextToTextBox(displayNameElement, account.DisplayName);
            Thread.Sleep(random.Next(1000, 2000));

            var dayElement = _driver.FindElementWait(mainWindow.RegisterSetting.Day);
            _driver.FillTextToTextBox(dayElement, account.BirthDate.Day.ToString());
            Thread.Sleep(random.Next(1000, 2000));

            var dropdownMonthElement = _driver.FindElementWait(mainWindow.RegisterSetting.Month);
            _driver.ClickElement(dropdownMonthElement);
            Thread.Sleep(random.Next(1000, 2000));

            var selectMonthElement = new SelectElement(dropdownMonthElement);
            selectMonthElement.SelectByValue(account.BirthDate.Month > 10 ? account.BirthDate.Month.ToString() : "0" + account.BirthDate.Month.ToString());
            Thread.Sleep(random.Next(1000, 2000));

            var yearElement = _driver.FindElementWait(mainWindow.RegisterSetting.Year);
            _driver.FillTextToTextBox(yearElement, account.BirthDate.Year.ToString());
            Thread.Sleep(random.Next(1000, 2000));


            if (account.Gender == GenderEnum.Male)
            {
                var maleElement = _driver.FindElementWait(mainWindow.RegisterSetting.Male);
                _driver.ClickElement(maleElement);
            }
            else if (account.Gender == GenderEnum.Female)
            {
                var femaleElement = _driver.FindElementWait(mainWindow.RegisterSetting.Female);
                _driver.ClickElement(femaleElement);
            }
            else if (account.Gender == GenderEnum.Other)
            {
                var neutralElement = _driver.FindElementWait(mainWindow.RegisterSetting.Neutral);
                _driver.ClickElement(neutralElement);
            }

            Thread.Sleep(random.Next(1000, 2000));

            _driver.ExecuteScript($"document.querySelector('{mainWindow.RegisterSetting.CaptchaResponse.Value}').style.display='block'");

            var submitElement = _driver.FindElementWait(mainWindow.RegisterSetting.SubmitForm);

            var siteKeyElement = _driver.FindElementWait(mainWindow.RegisterSetting.SiteKey);

            string siteKey = string.Empty;
            if (siteKeyElement != null)
            {
                siteKey = siteKeyElement.GetAttribute("data-sitekey");
            }

            var apiUrl = String.Format(Common.CAPTCHA_API, mainWindow.CapchaKey, siteKey, mainWindow.RegisterSetting.Url);
            string responseID = DriverManager.GetApi(apiUrl);

            if (!string.IsNullOrEmpty(responseID))
            {
                string idToken = responseID.Split('|')[1];
                var confirmTokenApi = string.Format(Common.GET_PASS_CAPTCHA_API, mainWindow.CapchaKey, idToken);
                Thread.Sleep(1000 * 60);
                string responseToken = DriverManager.GetApi(confirmTokenApi);

                if (string.IsNullOrEmpty(responseToken) || responseID == "CAPCHA_NOT_READY")
                {
                    Thread.Sleep(1000 * 60);
                    responseToken = DriverManager.GetApi(confirmTokenApi);
                }

                if (!string.IsNullOrEmpty(responseToken))
                {

                    if (responseToken.Contains('|') && responseToken.Split('|')[0] == "OK")
                    {
                        var arrToken = responseToken.Split('|');
                        string captchaTokenResult = arrToken[1];
                        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                        js.ExecuteScript($"document.getElementById('g-recaptcha-response').innerHTML='{captchaTokenResult}';");
                        Thread.Sleep(random.Next(1000, 2000));
                        _driver.ClickElement(submitElement);
                        Thread.Sleep(10000);
                        return true;
                    }
                    else
                    {
                        Thread.Sleep(1000 * 60);
                        string responseTokenSeconds = DriverManager.GetApi(confirmTokenApi);
                        var arrTokennew = responseTokenSeconds.Split('|');
                        if (arrTokennew[0] == "OK")
                        {
                            string captchaTokenResult = arrTokennew[1];
                            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
                            js.ExecuteScript($"document.getElementById('g-recaptcha-response').innerHTML='{captchaTokenResult}';");

                            Thread.Sleep(random.Next(1000, 2000));
                            _driver.ClickElement(submitElement);
                            Thread.Sleep(10000);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        void ClearRunningAccount()
        {
            foreach (var item in _account)
            {
                item.IsRunning = false;
            }
        }

        void RunningAccount(string guid)
        {
            var acc = _account.FirstOrDefault(t => t.Guid == guid);
            if (acc != null)
            {
                acc.IsRunning = true;
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            cts.Cancel();
        }


    }
}
