#region

using System.Data.Linq;
using System.Linq;

#endregion

namespace HsrOrderApp.DAL.Providers.LinqToSql.Adapters
{
    internal class SecurityAdapter
    {
        internal static IQueryable<BL.DomainModel.User> AdaptUsers(EntitySet<UserInRole> userInRoleCollection)
        {
            var users = from r in userInRoleCollection.AsQueryable()
                        select AdaptUser(r.User);
            return users;
        }

        internal static BL.DomainModel.User AdaptUser(User u)
        {
            return AdaptUser(u, null);
        }

        internal static BL.DomainModel.User AdaptUser(User u, BL.DomainModel.Customer customer)
        {
            if (u == null) return null;
            BL.DomainModel.User user = new BL.DomainModel.User()
                                           {
                                               UserId = u.UserId,
                                               UserName = u.Username,
                                               Password = u.Password,
                                               Version = AdapterBase.GetVersionAsUlong(u.Version),
                                               Roles = AdaptRoles(u.UserInRoles)
                                           };
            if (customer == null && u.Customer != null)
                user.Customer = CustomerAdapter.AdaptCustomer(u.Customer, user);
            else if (customer != null)
                user.Customer = customer;

            return user;
        }

        internal static IQueryable<BL.DomainModel.Role> AdaptRoles(EntitySet<UserInRole> roleCollection)
        {
            var roles = from r in roleCollection.AsQueryable()
                        select AdaptRole(r.Role);
            return roles;
        }

        internal static BL.DomainModel.Role AdaptRole(Role r)
        {
            BL.DomainModel.Role role = new BL.DomainModel.Role()
                                           {
                                               RoleId = r.RoleId,
                                               RoleName = r.RoleName,
                                               Version = AdapterBase.GetVersionAsUlong(r.Version),
                                               Users = AdaptUsers(r.UserInRoles)
                                           };

            return role;
        }
    }
}