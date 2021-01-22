#region

using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.BL.BusinessComponents
{
    public class SecurityBusinessComponent
    {
        private ISecurityRepository rep;

        public SecurityBusinessComponent()
        {
        }

        public SecurityBusinessComponent(ISecurityRepository unitOfWork)
        {
            this.rep = unitOfWork;
        }

        #region User

        public User GetUserByName(string username)
        {
            User user = rep.GetUserByUsername(username);
            return user;
        }

        public User GetUserById(int userId)
        {
            User user = rep.GetUserById(userId);
            return user;
        }

        public IQueryable<User> GetUsersByCriteria(UserSearchType searchType, string userName, string roleName)
        {
            IQueryable<User> users = null;

            switch (searchType)
            {
                case UserSearchType.None:
                    users = rep.GetAllUsers();
                    break;
                case UserSearchType.ByName:
                    users = rep.GetAllUsers().Where(u => u.UserName == userName);
                    break;
                case UserSearchType.ByRole:
                    users = rep.GetAllUsers().Where(u => u.Roles.Select(r => r.RoleName == roleName).Count() > 0);
                    break;
            }

            return users;
        }

        public int StoreUser(User user, IEnumerable<ChangeItem> changeItems)
        {
            int userId = default(int);
            using (TransactionScope transaction = new TransactionScope())
            {
                userId = rep.SaveUser(user);

                foreach (ChangeItem item in changeItems)
                {
                    if (item.Object is Role)
                    {
                        Role role = (Role) item.Object;
                        switch (item.ChangeType)
                        {
                            case ChangeType.ChildInsert:
                            case ChangeType.ChildUpate:
                                rep.AddUserToRole(user, role);
                                break;
                            case ChangeType.ChildDelete:
                                rep.RemoveUserFromRole(user, role);
                                break;
                        }
                    }
                }

                transaction.Complete();
            }
            return userId;
        }

        public void DeleteUser(int userId)
        {
            rep.DeleteUser(userId);
        }

        #endregion

        public ISecurityRepository Repository
        {
            get { return this.rep; }
            set { this.rep = value; }
        }

        public Role GetRoleById(int roleId)
        {
            Role role = rep.GetRoleById(roleId);
            return role;
        }

        public IQueryable<Role> GetRolesByCriteria(RoleSearchType searchType, string roleName)
        {
            IQueryable<Role> roles = null;

            switch (searchType)
            {
                case RoleSearchType.None:
                    roles = rep.GetAllRoles();
                    break;
                case RoleSearchType.ByName:
                    roles = rep.GetAllRoles().Where(u => u.RoleName == roleName);
                    break;
            }

            return roles;
        }
    }
}