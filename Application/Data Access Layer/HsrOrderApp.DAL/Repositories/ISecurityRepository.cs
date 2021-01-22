#region

using System.Linq;
using HsrOrderApp.BL.DomainModel;

#endregion

namespace HsrOrderApp.DAL.Data.Repositories
{
    public interface ISecurityRepository
    {
        IQueryable<User> GetAllUsers();
        User GetUserById(int id);
        User GetUserByUsername(string Username);

        int SaveUser(User user);
        void AddUserToRole(User user, Role role);
        void RemoveUserFromRole(User user, Role role);
        void DeleteUser(int id);


        IQueryable<Role> GetAllRoles();
        Role GetRoleById(int id);
    }
}