#region

using System.Collections.Generic;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Order
{
    public class OrderItemViewModel : ListViewModelBase<OrderDetailDTO>
    {
        public OrderItemViewModel(OrderDTO order)
            : base(order)
        {
        }

        protected override List<CommandViewModel> CreateCommands()
        {
            if (((OrderDTO)ParentObject).OrderStatus != OrderStatus.Draft)
                return new List<CommandViewModel>();

            return new List<CommandViewModel>
                       {
                           new CommandViewModel(Strings.NewCommand, new RelayCommand(param => New(), param => CanNew())),
                           new CommandViewModel(Strings.EditCommand, new RelayCommand(param => Edit(), param => CanEdit())),
                           new CommandViewModel(Strings.DeleteCommand, new RelayCommand(param => Delete(), param => CanDelete()))
                       };
        }

        protected override void LoadData()
        {
            foreach (OrderDetailDTO orderDetail in ((OrderDTO) ParentObject).Details)
                Items.Add(orderDetail);
        }

        protected override void New()
        {
            OrderDetailDTO newOrderDetail = new OrderDetailDTO();
            OrderItemDetailViewModel detailModelView = new OrderItemDetailViewModel(newOrderDetail, true);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                ParentObject.MarkChildForInsertion(newOrderDetail);
                Items.Add(newOrderDetail);
                SelectedItem = newOrderDetail;
            }
        }

        protected override void Delete()
        {
            ParentObject.MarkChildForDeletion(SelectedItem);
            Items.Remove(SelectedItem);
        }

        protected override void Edit()
        {
            OrderDetailDTO editOrderDetail = SelectedItem.Clone();
            OrderItemDetailViewModel detailModelView = new OrderItemDetailViewModel(editOrderDetail, false);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                int index = Items.IndexOf(SelectedItem);
                Items.Remove(SelectedItem);
                Items.Insert(index, editOrderDetail);
                SelectedItem = editOrderDetail;
                ParentObject.MarkChildForUpdate(editOrderDetail);
            }
        }
    }
}