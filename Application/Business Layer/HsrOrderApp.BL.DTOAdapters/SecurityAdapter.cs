#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.BusinessComponents;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DTOAdapters.Helper;
using HsrOrderApp.SharedLibraries.DTO;

#endregion

namespace HsrOrderApp.BL.DtoAdapters
{
    public static class SecurityAdapter
    {
        #region UserToDTO

        public static CurrentUserDTO UserToCurrentUserDTO(User user)
        {
            CurrentUserDTO dto = new CurrentUserDTO()
                                     {
                                         Id = user.UserId,
                                         UserName = user.UserName,
                                         CustomerId = (user.Customer == null) ? 0 : user.Customer.CustomerId,
                                         CustomerName = (user.Customer == null) ? string.Empty : user.Customer.ToString(),
                                         Roles = RolesToDTOs(user.Roles)
                                     };

            return dto;
        }

        public static UserDTO UserToDTO(User user)
        {
            UserDTO dto = new UserDTO()
                              {
                                  Id = user.UserId,
                                  UserName = user.UserName,
                                  Password = user.Password,
                                  Version = user.Version,
                                  CustomerId = (user.Customer == null) ? 0 : user.Customer.CustomerId,
                                  CustomerName = (user.Customer == null) ? string.Empty : user.Customer.ToString(),
                                  Roles = RolesToDTOs(user.Roles)
                              };

            return dto;
        }

        public static IList<UserListDTO> UsersToDtos(IQueryable<User> users)
        {
            IQueryable<UserListDTO> dtos = from u in users
                                           select UserToListDto(u);
            return dtos.ToList();
        }

        private static UserListDTO UserToListDto(User u)
        {
            UserListDTO user = new UserListDTO()
                                   {
                                       Id = u.UserId,
                                       UserName = u.UserName,
                                       Roles = RolesAsString(u.Roles)
                                   };
            if (u.Customer != null)
            {
                user.CustomerName = u.Customer.ToString();
            }
            return user;
        }

        #region Private helpers

        private static string RolesAsString(IQueryable<Role> roles)
        {
            string s = string.Empty;
            bool isFirst = true;
            foreach (Role role in roles)
            {
                if (!isFirst)
                {
                    s += ", ";
                }
                s += role.RoleName;
                isFirst = false;
            }
            return s;
        }

        #endregion

        #endregion

        #region DTOToUser

        public static User DtoToUser(UserDTO dto)
        {
            User user = new User()
                            {
                                UserId = dto.Id,
                                UserName = dto.UserName,
                                Version = dto.Version,
                                Password = dto.Password,
                                Customer = (dto.CustomerId > 0) ? new Customer() {CustomerId = dto.CustomerId} : null
                            };
            ValidationHelper.Validate(user);
            return user;
        }

        #endregion

        #region RoleToDTO

        public static IList<RoleDTO> RolesToDTOs(IQueryable<Role> roles)
        {
            IQueryable<RoleDTO> roleDTOs = from r in roles
                                           select RoleToDTO(r);
            return roleDTOs.ToList();
        }

        public static RoleDTO RoleToDTO(Role r)
        {
            RoleDTO dto = new RoleDTO()
                              {
                                  Id = r.RoleId,
                                  RoleName = r.RoleName,
                                  Version = r.Version
                              };
            return dto;
        }

        #endregion

        #region DtoToRole

        public static IEnumerable<ChangeItem> GetChangeItems(UserDTO dto)
        {
            IEnumerable<ChangeItem> changeItems = from c in dto.Changes
                                                  select
                                                      new ChangeItem(c.ChangeType,
                                                                     DtoToRole((RoleDTO) c.Object));
            return changeItems;
        }

        private static Role DtoToRole(RoleDTO dto)
        {
            Role role = new Role()
                            {
                                RoleId = dto.Id,
                                RoleName = dto.RoleName,
                                Version = dto.Version
                            };
            ValidationHelper.Validate(role);
            return role;
        }

        #endregion
    }
}