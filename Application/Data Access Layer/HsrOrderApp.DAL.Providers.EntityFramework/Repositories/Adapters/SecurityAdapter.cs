#region

using System.Data.Objects.DataClasses;
using System.Linq;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters
{
    internal static class SecurityAdapter
    {
        internal static BL.DomainModel.User AdaptUser(EntityReference<User> u)
        {
            if (u == null || u.Value == null)
                return null;
            return AdaptUser(u.Value);
        }

        internal static IQueryable<BL.DomainModel.User> AdaptUsers(EntityCollection<User> userCollection)
        {
            if (userCollection.IsLoaded == false)
                return null;

            var users = from r in userCollection.AsEnumerable()
                        select AdaptUser(r);
            return users.AsQueryable();
        }

        internal static BL.DomainModel.User AdaptUser(User u)
        {
            BL.DomainModel.User user = new BL.DomainModel.User()
                                           {
                                               UserId = u.UserId,
                                               UserName = u.Username,
                                               Password = u.Password,
                                               Version = u.Version.ToUlong(),
                                               Roles = AdaptRoles(u.Roles)
                                           };

            user.Customer = CustomerAdapter.AdaptCustomer(u.CustomerReference, user);
            return user;
        }

        internal static IQueryable<BL.DomainModel.Role> AdaptRoles(EntityCollection<Role> roleCollection)
        {
            if (roleCollection.IsLoaded == false)
                return null;

            var roles = from r in roleCollection.AsEnumerable()
                        select AdaptRole(r);
            return roles.AsQueryable();
        }

        internal static BL.DomainModel.Role AdaptRole(Role r)
        {
            BL.DomainModel.Role role = new BL.DomainModel.Role()
                                           {
                                               RoleId = r.RoleId,
                                               RoleName = r.RoleName,
                                               Version = r.Version.ToUlong(),
                                               Users = AdaptUsers(r.Users)
                                           };

            return role;
        }
    }
}