using AutoMapper;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.Mvc.Controllers.Base;
using System.Collections.Generic;

namespace HsrOrderApp.UI.Mvc.Models
{
    public class AddressViewModel : ListViewModelBase<AddressDTO>
    {
        public AddressViewModel() { }
        public AddressViewModel(List<AddressDTO> list) { Items = list; }

        public List<AddressDTO> ItemsToDelete { get; } = new List<AddressDTO>();

        public ControllerAction LatestAddressAction { get; set; }

        public bool IsReadOnly { get; set; }
        public string ReturnController { get; set; }
        public string ReturnAction { get; set; }
        public string ReturnId { get; set; }

        public void ApplyFormAttributes(AddressDTO source)
        {
            Mapper.CreateMap<AddressDTO, AddressDTO>().ForAllMembers(opt => opt.Ignore());
            Mapper.CreateMap<AddressDTO, AddressDTO>()
                .ForMember(dest => dest.AddressLine1, map => map.MapFrom(src => src.AddressLine1))
                .ForMember(dest => dest.AddressLine2, map => map.MapFrom(src => src.AddressLine2))
                .ForMember(dest => dest.PostalCode, map => map.MapFrom(src => src.PostalCode))
                .ForMember(dest => dest.City, map => map.MapFrom(src => src.City))
                .ForMember(dest => dest.Phone, map => map.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Email, map => map.MapFrom(src => src.Email));

            Mapper.Map(source, SelectedItem);
        }
    }
}





