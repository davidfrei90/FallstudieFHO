namespace HsrOrderApp.UI.WPF.ViewModels.Base
{
    public interface IDetailViewModelBase
    {
        bool IsNew { get; set; }
        event SavingResultEventHandler SavingResultEvent;
    }
}