using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SpotifyRegistrationTool.ViewModels
{
    public class GridLoadingVisibleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool isVisibile = false;

        public bool IsVisible {
            get {
                return isVisibile;
            }
            set {
                isVisibile = value;
                NotifyPropertyChanged("IsVisible");
            }
        }

        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }
}
