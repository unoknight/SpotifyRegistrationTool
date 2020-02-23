using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SpotifyRegistrationTool.Forms
{
    /// <summary>
    /// Interaction logic for DetailForm.xaml
    /// </summary>
    public partial class DetailForm : Window
    {
        private Models.Account _account;
        public DetailForm(Models.Account account)
        {
            InitializeComponent();
            _account = account;
        }

    }
}
