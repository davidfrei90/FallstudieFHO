#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.PresentationLogic;
using HsrOrderApp.UI.WPF.Properties;
using HsrOrderApp.UI.WPF.ViewModels.Base;

#endregion

namespace HsrOrderApp.UI.WPF.ViewModels.Order
{
    public class OrderViewModel : ListViewModelBase<OrderListDTO>
    {
        protected override void LoadData()
        {
            this.DisplayName = Strings.OrderViewModel_DisplayName;
            IList<OrderListDTO> orders = Service.GetAllOrders();
            foreach (OrderListDTO order in orders)
                Items.Add(order);
        }

        protected override void New()
        {
            OrderDTO newOrder = new OrderDTO();
            OrderDetailViewModel detailModelView = new OrderDetailViewModel(newOrder, true);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                Load();
                SelectedItem = Items.SingleOrDefault(dto => dto.Id == newOrder.Id);
            }
        }

        protected override void Delete()
        {
            Service.DeleteOrder(SelectedItem.Id);
            Load();
        }

        protected override void Edit()
        {
            OrderDTO selectedDto = Service.GetOrderById(SelectedItem.Id);
            OrderDetailViewModel detailModelView = new OrderDetailViewModel(selectedDto, false);
            if (NavigationService.NavigateTo("Detail", detailModelView) == NavigationResult.Ok)
            {
                Load();
                SelectedItem = Items.SingleOrDefault(dto => dto.Id == selectedDto.Id);
            }
        }
    }
}