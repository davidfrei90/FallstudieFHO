#region

using System;
using System.Data.Linq;
using System.Linq;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.DAL.Providers.LinqToSql.Adapters;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

#endregion

namespace HsrOrderApp.DAL.Providers.LinqToSql.Repositories
{
    public class SecurityRepository : RepositoryBase, ISecurityRepository
    {
        public SecurityRepository(HsrOrderAppDataContext db) : base(db)
        {
        }

        public SecurityRepository(string connectionString) : base(connectionString)
        {
        }

        #region Users

        public IQueryable<HsrOrderApp.BL.DomainModel.User> GetAllUsers()
        {
            var users = from u in this.db.Users
                        select SecurityAdapter.AdaptUser(u);
            return users;
        }

        public HsrOrderApp.BL.DomainModel.User GetUserById(int id)
        {
            try
            {
                var users = from u in this.db.Users
                            where u.UserId == id
                            select SecurityAdapter.AdaptUser(u);
                return users.First();
            }
            catch (ArgumentNullException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return new MissingUser();
            }
        }

        public HsrOrderApp.BL.DomainModel.User GetUserByUsername(string username)
        {
            try
            {
                var users = from u in this.db.Users
                            where u.Username == username
                            select SecurityAdapter.AdaptUser(u);
                return users.First();
            }
            catch (ArgumentNullException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return new MissingUser();
            }
        }

        public int SaveUser(HsrOrderApp.BL.DomainModel.User user)
        {
            try
            {
                User dbUser = new User();
                bool isNew = false;
                if (user.UserId == default(int) || user.UserId <= 0)
                    isNew = true;

                dbUser.UserId = user.UserId;
                dbUser.Version = user.Version.ToTimestamp();
                dbUser.Password = user.Password;
                dbUser.Username = user.UserName;
                if (user.Customer != null)
                    dbUser.CustomerId = user.Customer.CustomerId;
                else
                    dbUser.Customer = null;

                if (isNew)
                    db.Users.InsertOnSubmit(dbUser);
                else
                    db.Users.Attach(dbUser, true);
                db.SubmitChanges();
                user.UserId = dbUser.UserId;
                return dbUser.UserId;
            }
            catch (ChangeConflictException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return default(int);
            }
        }

        public void AddUserToRole(HsrOrderApp.BL.DomainModel.User user, HsrOrderApp.BL.DomainModel.Role role)
        {
            try
            {
                UserInRole dbUserInRole = db.UserInRoles.FirstOrDefault(ur => ur.RoleId == role.RoleId && ur.UserId == user.UserId);
                if (dbUserInRole != null)
                {
                    // user already in role
                    return;
                }
                dbUserInRole = new UserInRole();
                dbUserInRole.Role = db.Roles.FirstOrDefault(r => r.RoleId == role.RoleId);
                dbUserInRole.User = db.Users.FirstOrDefault(u => u.UserId == user.UserId);
                db.UserInRoles.InsertOnSubmit(dbUserInRole);
                db.SubmitChanges();
            }
            catch (ChangeConflictException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
            }
        }

        public void RemoveUserFromRole(HsrOrderApp.BL.DomainModel.User user, HsrOrderApp.BL.DomainModel.Role role)
        {
            try
            {
                User dbUser = db.Users.FirstOrDefault(u => u.UserId == user.UserId);
                UserInRole dbUserInRole = dbUser.UserInRoles.FirstOrDefault(u => u.RoleId == role.RoleId);
                if (dbUserInRole == null)
                {
                    // user not in role anyway
                    return;
                }
                db.UserInRoles.DeleteOnSubmit(dbUserInRole);
                db.SubmitChanges();
            }
            catch (ChangeConflictException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
            }
        }

        public void DeleteUser(int id)
        {
            User cu = db.Users.FirstOrDefault(c => c.UserId == id);
            if (cu != null)
            {
                db.Users.DeleteOnSubmit(cu);
                db.SubmitChanges();
            }
        }

        #endregion

        #region Roles

        public IQueryable<HsrOrderApp.BL.DomainModel.Role> GetAllRoles()
        {
            var roles = from r in this.db.Roles
                        select SecurityAdapter.AdaptRole(r);

            return roles;
        }

        public HsrOrderApp.BL.DomainModel.Role GetRoleById(int roleId)
        {
            try
            {
                var roles = from r in this.db.Roles
                            where r.RoleId == roleId
                            select SecurityAdapter.AdaptRole(r);

                return roles.First();
            }
            catch (ArgumentNullException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return new MissingRole();
            }
        }

        #endregion
    }
}