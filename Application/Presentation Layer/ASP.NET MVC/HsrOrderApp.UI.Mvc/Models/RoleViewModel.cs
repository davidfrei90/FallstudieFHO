using AutoMapper;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.Mvc.Controllers.Base;
using HsrOrderApp.UI.Mvc.Models;
using System.Collections.Generic;

namespace HsrOrderApp.UI.Mvc.Models
{
    public class RoleViewModel : ListViewModelBase<RoleDTO>
    {
        public RoleViewModel() { }
        public RoleViewModel(List<RoleDTO> list) { Items = list; }
        public List<RoleDTO> Roles { get; set; }

        public List<RoleDTO> ItemsToInsert { get; } = new List<RoleDTO>();
        public List<RoleDTO> ItemsToDelete { get; } = new List<RoleDTO>();

        public ControllerAction LatestControllerAction { get; set; }

        public bool IsReadOnly { get; set; }
        public string ReturnController { get; set; }
        public string ReturnAction { get; set; }
        public string ReturnId { get; set; }

        public void ApplyFormAttributes(RoleDTO source)
        {
            Mapper.CreateMap<RoleDTO, RoleDTO>().ForAllMembers(opt => opt.Ignore());
            Mapper.CreateMap<RoleDTO, RoleDTO>()
                .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
                .ForMember(dest => dest.RoleName, map => map.MapFrom(src => src.RoleName));

            Mapper.Map(source, SelectedItem);
        }
    }
}
