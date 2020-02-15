using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SpotifyRegistrationTool.UserControls
{
    /// <summary>
    /// Interaction logic for AccountManagerUc.xaml
    /// </summary>
    public partial class AccountManagerUc : UserControl
    {

        public AccountManagerUc()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;
            mainWindow.LoadPage(mainWindow._infomationUc);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            listviewAccounts.ItemsSource = mainWindow.Accounts;
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
    }
}
