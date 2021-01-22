#region

using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Address
{
    public class AddressDetailViewModel : DetailViewModelBase<AddressDTO>
    {
        public AddressDetailViewModel(AddressDTO address, bool isNew)
            : base(address, isNew)
        {
            this.DisplayName = Strings.AddressDetailViewModel_DisplayName;
        }
    }
}