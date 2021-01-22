using AutoMapper;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.SharedLibraries.SharedEnums;
using System.Collections.Generic;
using System.Linq;

namespace HsrOrderApp.UI.Mvc.Models
{
    public class OrderListViewModel : ListViewModelBase<OrderListDTO>
    {
        public OrderListViewModel() { }
        public OrderListViewModel(List<OrderListDTO> list) { Items = list; }
    }
    public class OrderViewModel : DetailViewModelBase<OrderDTO>
    {
        public OrderViewModel() { }
        public OrderViewModel(OrderDTO model, OrderDetailViewModel orderDetails, bool isNew) : base(model, isNew)
        {
            _OrderDetails = OrderDetails;
        }

        private OrderDetailViewModel _OrderDetails;
        public OrderDetailViewModel OrderDetails
        {
            get
            {
                if (_OrderDetails != null)
                {
                    _OrderDetails.Items = Model.Details.ToList();
                }
                return _OrderDetails;
            }
            set
            {
                _OrderDetails = value;
                Model.Details = _OrderDetails.Items;
            }
        }

        public List<CustomerListDTO> Customers { get; set; }
        public List<string> OrderStates { get; set; }

        public void ApplyFormAttributes(OrderDTO source)
        {
            Mapper.CreateMap<OrderDTO, OrderDTO>().ForAllMembers(opt => opt.Ignore());
            Mapper.CreateMap<OrderDTO, OrderDTO>()
                .ForMember(dest => dest.CustomerId, map => map.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.CustomerName, map => map.MapFrom(src => src.CustomerName));

            Mapper.Map(source, Model);
        }
    }
}