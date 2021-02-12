using AutoMapper;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.SharedLibraries.SharedEnums;
using System.Collections.Generic;
using System.Linq;

namespace HsrOrderApp.UI.Mvc.Models
{
    public class SupplierConditionListViewModel : ListViewModelBase<SupplierConditionListDTO>
    {
        public SupplierConditionListViewModel() { }
        public SupplierConditionListViewModel(List<SupplierConditionListDTO> list) { Items = list; }
    }
    public class SupplierConditionViewModel : DetailViewModelBase<SupplierConditionDTO>
    {
        public SupplierConditionViewModel() { }
        public SupplierConditionViewModel(SupplierConditionDTO model, bool isNew) : base(model, isNew)
        {
           
        }

        

        public List<SupplierListDTO> Suppliers { get; set; }
        //public List<string> OrderStates { get; set; }

        public void ApplyFormAttributes(SupplierConditionDTO source)
        {
            Mapper.CreateMap<SupplierConditionDTO, SupplierConditionDTO>().ForAllMembers(opt => opt.Ignore());
            Mapper.CreateMap<SupplierConditionDTO, SupplierConditionDTO>()
                .ForMember(dest => dest.StandardPrice, map => map.MapFrom(src => src.StandardPrice))
                .ForMember(dest => dest.LastReceiptCost, map => map.MapFrom(src => src.LastReceiptCost))
                .ForMember(dest => dest.LastReceiptDate, map => map.MapFrom(src => src.LastReceiptDate))
                .ForMember(dest => dest.MinOrderQty, map => map.MapFrom(src => src.MinOrderQty))
                .ForMember(dest => dest.MaxOrderQty, map => map.MapFrom(src => src.MaxOrderQty));

            Mapper.Map(source, Model);
        }
    }
}