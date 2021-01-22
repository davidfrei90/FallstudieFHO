using AutoMapper;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.SharedLibraries.SharedEnums;
using System.Collections.Generic;
using System.Linq;

namespace HsrOrderApp.UI.Mvc.Models
{
    public class UserListViewModel : ListViewModelBase<UserListDTO>
    {
        public UserListViewModel() { }
        public UserListViewModel(List<UserListDTO> list) { Items = list; }
    }
    public class UserViewModel : DetailViewModelBase<UserDTO>
    {
        public UserViewModel() { }
        public UserViewModel(UserDTO model, RoleViewModel roles, bool isNew) : base(model, isNew)
        {
            _Roles = Roles;
        }

        private RoleViewModel _Roles;
        public RoleViewModel Roles
        {
            get
            {
                if (_Roles != null)
                {
                    _Roles.Items = Model.Roles.ToList();
                }
                return _Roles;
            }
            set
            {
                _Roles = value;
                Model.Roles = _Roles.Items;
            }
        }

        public List<CustomerListDTO> Customers { get; set; }

        public void ApplyFormAttributes(UserDTO source)
        {
            Mapper.CreateMap<UserDTO, UserDTO>().ForAllMembers(opt => opt.Ignore());
            Mapper.CreateMap<UserDTO, UserDTO>()
                .ForMember(dest => dest.UserName, map => map.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Password, map => map.MapFrom(src => src.Password))
                .ForMember(dest => dest.CustomerId, map => map.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.CustomerName, map => map.MapFrom(src => src.CustomerName));

            Mapper.Map(source, Model);
        }
    }
}