using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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
using Microsoft.Extensions.Configuration;
using Nancy.Json;
using Newtonsoft.Json;
using SpotifyRegistrationTool._Data;
using SpotifyRegistrationTool.Helpers;
using SpotifyRegistrationTool.Logic;
using SpotifyRegistrationTool.Models;
using SpotifyRegistrationTool.UserControls;
using SpotifyRegistrationTool.ViewModels;

namespace SpotifyRegistrationTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public InfomationUc _infomationUc;
        public AccountManagerUc _accountManagerUc;
        private Control _currentPage;
        public List<Models.Account> Accounts { get; set; }
        public StringCollection Proxies { get; set; }
        public string OldCardContactValue { get; set; } = string.Empty;
        public string[] UsingProxies { get; set; }
        public Enums.ProxyTypeEnum ProxyType { get; set; }
        public Models.Account CurrentAccountRunning;

        public readonly IConfiguration _configuration;

        public RegisterSettingModel RegisterSetting { get; set; }

        public RegisterPremiumModel RegisterPremium { get; set; }

        public string CapchaKey { get; set; }

        public DataAccess _dataAccess;

        public MainWindow(IConfiguration configuration)
        {
            _configuration = configuration;
            InitializeComponent();
            _infomationUc = new InfomationUc();
            _accountManagerUc = new AccountManagerUc();
            LoadPage(_infomationUc);
            _dataAccess = new DataAccess();
        }

        public void LoadingPage(bool isLoading)
        {
            //if (isLoading)
            //{
            //    this.gridLoading.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    this.gridLoading.Visibility = Visibility.Hidden;
            //}
        }

        public void LoadPage(Control page)
        {
            myStack.Children.Clear();
            _currentPage = page;
            _currentPage.Width = myStack.Width;
            _currentPage.Height = myStack.Height;
            myStack.Children.Add(_currentPage);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RegisterSetting = _configuration.GetSection("RegisterSetting").Get<RegisterSettingModel>();
            RegisterPremium = _configuration.GetSection("PremiumCard").Get<RegisterPremiumModel>();
            CapchaKey = _configuration.GetSection("CaptchaKey").Get<string>();

            var jsonFileAddress = StringHelper.GetContentJsonFile("addresses-us-all.min.json");
            var jsonArray = new JsonDeserializer(jsonFileAddress);
            Common.Addresses = JsonConvert.DeserializeObject<List<AddressInfo>>(jsonArray.GetObject("addresses").ToString());

            var s = _dataAccess.GetListAccount();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure exit program?", Common.APP_NAME, System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Question);
            e.Cancel = messageBoxResult == MessageBoxResult.No;
        }
    }
}
