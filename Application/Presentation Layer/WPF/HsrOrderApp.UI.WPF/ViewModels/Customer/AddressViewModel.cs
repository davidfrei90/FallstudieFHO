#region

using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.ViewModels.Address;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Customer
{
    public class AddressViewModel : ListViewModelBase<AddressDTO>
    {
        public AddressViewModel(CustomerDTO customer) : base(customer)
        {
        }

        protected override void LoadData()
        {
            foreach (AddressDTO address in ((CustomerDTO) ParentObject).Addresses)
                Items.Add(address);
        }

        protected override void New()
        {
            AddressDTO newAddress = new AddressDTO();
            AddressDetailViewModel detailModelView = new AddressDetailViewModel(newAddress, true);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                ParentObject.MarkChildForInsertion(newAddress);
                Items.Add(newAddress);
                SelectedItem = newAddress;
            }
        }

        protected override void Delete()
        {
            ParentObject.MarkChildForDeletion(SelectedItem);
            Items.Remove(SelectedItem);
            SelectedItem = null;
        }

        protected override void Edit()
        {
            AddressDTO editAddress = SelectedItem.Clone();
            AddressDetailViewModel detailModelView = new AddressDetailViewModel(editAddress, false);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                int index = Items.IndexOf(SelectedItem);
                Items.Remove(SelectedItem);
                Items.Insert(index, editAddress);
                SelectedItem = editAddress;
                ParentObject.MarkChildForUpdate(editAddress);
            }
        }
    }
}