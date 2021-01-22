using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace HsrOrderApp.UI.Silverlight.ViewModels.Base
{
    public abstract class ViewModelBase:INotifyPropertyChanged
    {
        public abstract void LoadData();
        public event PropertyChangedEventHandler PropertyChanged;

        protected void UpdateState(string state, List<Control> controls)
        {
            //VisualStateManager.GoToState(this, state, true);

            MainPage mp = App.Current.RootVisual as MainPage;
            if (mp != null)
                VisualStateManager.GoToState(mp, state, true);
        }

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
