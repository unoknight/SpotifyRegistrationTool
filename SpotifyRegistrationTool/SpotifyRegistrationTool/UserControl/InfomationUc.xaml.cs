using SpotifyRegistrationTool.Helpers;
using SpotifyRegistrationTool.Logic;
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
    /// Interaction logic for InfomationUc.xaml
    /// </summary>
    public partial class InfomationUc : UserControl
    {
        public InfomationUc()
        {
            InitializeComponent();
        }

        private void btnGenerateAccount_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textboxProxy.Text) || string.IsNullOrWhiteSpace(textboxCardContact.Text))
            {

                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Proxy & Card contact is required!", Common.APP_NAME, System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MainWindow mainWindow = Window.GetWindow(this) as MainWindow;

            if (!mainWindow.OldCardContactValue.Equals(textboxCardContact.Text.Trim()))
            {
                mainWindow.Accounts = AccountLogic.GenerateAccounts(StringHelper.GetLinesCollectionFromTextBox(textboxCardContact));
                mainWindow.OldCardContactValue = textboxCardContact.Text.Trim();
            }

            mainWindow.Proxies = StringHelper.GetLinesCollectionFromTextBox(textboxProxy);

            mainWindow.LoadingPage(true);
            mainWindow.LoadPage(mainWindow._accountManagerUc);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure clear content all texbox?", Common.APP_NAME, System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                textboxProxy.Text = "";
                textboxCardContact.Text = "";
            }

        }
    }
}
