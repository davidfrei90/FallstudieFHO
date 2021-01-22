using AutoMapper;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.Mvc.Controllers.Base;
using HsrOrderApp.UI.Mvc.Models;
using System.Collections.Generic;

namespace HsrOrderApp.UI.Mvc.Models
{
    public class OrderDetailViewModel : ListViewModelBase<OrderDetailDTO>
    {
        public OrderDetailViewModel() { }
        public OrderDetailViewModel(List<OrderDetailDTO> list) { Items = list; }
        public List<ProductDTO> Products { get; set; }
        public string EstimatedDeliveryTime { get; set; }

        public List<OrderDetailDTO> ItemsToDelete { get; } = new List<OrderDetailDTO>();

        public ControllerAction LatestControllerAction { get; set; }

        public bool IsReadOnly { get; set; }
        public string ReturnController { get; set; }
        public string ReturnAction { get; set; }
        public string ReturnId { get; set; }

        public void ApplyFormAttributes(OrderDetailDTO source)
        {
            Mapper.CreateMap<OrderDetailDTO, OrderDetailDTO>().ForAllMembers(opt => opt.Ignore());
            Mapper.CreateMap<OrderDetailDTO, OrderDetailDTO>()
                .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductId, map => map.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.ProductName, map => map.MapFrom(src => src.ProductName))
                .ForMember(dest => dest.UnitPrice, map => map.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.QuantityInUnits, map => map.MapFrom(src => src.QuantityInUnits))
                .ForMember(dest => dest.TotalPrice, map => map.MapFrom(src => src.TotalPrice));

            Mapper.Map(source, SelectedItem);
        }
    }
}
