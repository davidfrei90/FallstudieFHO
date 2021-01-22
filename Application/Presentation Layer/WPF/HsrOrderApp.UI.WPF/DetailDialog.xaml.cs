#region

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF
{
    /// <summary>
    /// Interaction logic for DetailDialog.xaml
    /// </summary>
    public partial class DetailDialog : Window
    {
        public static RoutedCommand OKCommand = new RoutedCommand();

        public DetailDialog(IDetailViewModelBase detailViewModel)
        {
            InitializeComponent();
            this.DataContext = detailViewModel;
            detailViewModel.SavingResultEvent += new SavingResultEventHandler(DetailDialog_SavingResultEvent);
            if (detailViewModel.IsNew == false)
                SetValidationStyle();
        }

        private void DetailDialog_SavingResultEvent(bool saveSuccessfull, string validationErrorMessage)
        {
            if (saveSuccessfull)
            {
                DialogResult = true;
            }
            else
            {
                SetValidationStyle();
            }
        }

        private void SetValidationStyle()
        {
            Style style = new Style(typeof (TextBox), (Style) this.Resources["validationStyle"]);
            this.Resources.Remove(typeof (TextBox));
            this.Resources.Add(typeof (TextBox), style);
        }
    }
}