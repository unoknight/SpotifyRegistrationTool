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

        public MainWindow()
        {
            InitializeComponent();
            _infomationUc = new InfomationUc();
            _accountManagerUc = new AccountManagerUc();
            LoadPage(_infomationUc);
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
            //LoadingPage(true);
        }
    }
}
