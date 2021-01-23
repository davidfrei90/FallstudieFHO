using AutoMapper;
using HsrOrderApp.SharedLibraries.DTO;
using System.Collections.Generic;
using System.Linq;

namespace HsrOrderApp.UI.Mvc.Models
{
    public class CustomerListViewModel : ListViewModelBase<CustomerListDTO>
    {
        public CustomerListViewModel() { }
        public CustomerListViewModel(List<CustomerListDTO> list) { Items = list; }
    }
    public class CustomerViewModel : DetailViewModelBase<CustomerDTO>
    {
        public CustomerViewModel() { }
        public CustomerViewModel(CustomerDTO model, AddressViewModel addresses, bool isNew) : base(model, isNew)
        {
            _addresses = addresses;
        }

        private AddressViewModel _addresses;
        public AddressViewModel Addresses
        {
            get
            {
                if(_addresses != null)
                {
                    _addresses.Items = Model.Addresses.ToList();
                }
                return _addresses;
            }
            set
            {
                _addresses = value;
                Model.Addresses = _addresses.Items;
            }
        }

        public void ApplyFormAttributes(CustomerDTO source)
        {
            Mapper.CreateMap<CustomerDTO, CustomerDTO>().ForAllMembers(opt => opt.Ignore());
            Mapper.CreateMap<CustomerDTO, CustomerDTO>()
                .ForMember(dest => dest.Salutation, map => map.MapFrom(src => src.Salutation))
                .ForMember(dest => dest.FirstName, map => map.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name));

            Mapper.Map(source, Model);
        }
    }
}