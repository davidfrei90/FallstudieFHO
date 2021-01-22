#region

using System;
using System.Data;
using System.Linq;
using HsrOrderApp.BL.DomainModel.SpecialCases;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Data.Objects.DataClasses;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories
{
    public class SecurityRepository : RepositoryBase, ISecurityRepository
    {
        public SecurityRepository(HsrOrderAppEntities db)
            : base(db)
        {
        }

        public SecurityRepository(string connectionString) : base(connectionString)
        {
        }

        public SecurityRepository() : base()
        {
        }

        #region Users

        public IQueryable<HsrOrderApp.BL.DomainModel.User> GetAllUsers()
        {
            var users = from u in this.db.UserSet.Include("Roles").AsEnumerable()
                        select SecurityAdapter.AdaptUser(u);

            return users.AsQueryable();
        }

        public HsrOrderApp.BL.DomainModel.User GetUserById(int id)
        {
            try
            {
                var users = from u in this.db.UserSet.Include("Roles").Include("Customer").AsEnumerable()
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
                var users = from u in this.db.UserSet.Include("Roles").AsEnumerable()
                            where String.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase)
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
                string setname = "UserSet";
                User dbUser;

                bool isNew = false;
                if (user.UserId == default(int) || user.UserId <= 0)
                {
                    isNew = true;
                    dbUser = new User();
                }
                else
                {
                    dbUser = new User() { UserId = user.UserId, Version = user.Version.ToTimestamp() };
                    dbUser.EntityKey = db.CreateEntityKey(setname, dbUser);
                    db.AttachTo(setname, dbUser);
                }
                dbUser.Version = user.Version.ToTimestamp();
                dbUser.Password = user.Password;
                dbUser.Username = user.UserName;

                if (isNew)
                {
                    if (dbUser.Customer != null)
                        dbUser.CustomerReference.EntityKey = new EntityKey("HsrOrderAppEntities.CustomerSet", "CustomerId", user.Customer.CustomerId);
                    db.AddToUserSet(dbUser);
                }
                else if (user.Customer == null)
                {
                    if (dbUser.Customer != null)
                    {
                        dbUser.Customer.UserReference.Value = null;
                    }
                    dbUser.CustomerReference.Value = null;
                }
                else
                {
                    if (dbUser.CustomerReference.GetEnsureLoadedReference().Value.CustomerId != user.Customer.CustomerId)
                    {
                        Customer newCustomer = new Customer { CustomerId = user.Customer.CustomerId };
                        db.AttachTo("CustomerSet", newCustomer);
                        dbUser.Customer = newCustomer;
                    }
                }
                db.SaveChanges();
                user.UserId = dbUser.UserId;
                return dbUser.UserId;
            }
            catch (OptimisticConcurrencyException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
                return default(int);
            }
        }

        public void AddUserToRole(HsrOrderApp.BL.DomainModel.User user, HsrOrderApp.BL.DomainModel.Role role)
        {
            try
            {
                User dbUser = db.UserSet.First(u => u.UserId == user.UserId);
                Role dbRole = db.RoleSet.First(r => r.RoleId == role.RoleId);
                dbUser.Roles.Add(dbRole);
                db.SaveChanges();
            }
            catch (OptimisticConcurrencyException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
            }
        }

        public void RemoveUserFromRole(HsrOrderApp.BL.DomainModel.User user, HsrOrderApp.BL.DomainModel.Role role)
        {
            try
            {
                User dbUser = db.UserSet.First(u => u.UserId == user.UserId);
                Role dbRole = db.RoleSet.First(r => r.RoleId == role.RoleId);
                dbUser.Roles.Attach(dbRole);
                dbUser.Roles.Remove(dbRole);
                db.SaveChanges();
            }
            catch (OptimisticConcurrencyException ex)
            {
                if (ExceptionPolicy.HandleException(ex, "DA Policy")) throw;
            }
        }

        public void DeleteUser(int id)
        {
            User cu = db.UserSet.First(c => c.UserId == id);
            if (cu != null)
            {
                db.DeleteObject(cu);
                db.SaveChanges();
            }
        }

        #endregion

        #region Roles

        public IQueryable<HsrOrderApp.BL.DomainModel.Role> GetAllRoles()
        {
            var roles = from r in this.db.RoleSet.Include("Users").AsEnumerable()
                        select SecurityAdapter.AdaptRole(r);

            return roles.AsQueryable();
        }

        public HsrOrderApp.BL.DomainModel.Role GetRoleById(int id)
        {
            try
            {
                var roles = from u in this.db.RoleSet.Include("Users").AsEnumerable()
                            where u.RoleId == id
                            select SecurityAdapter.AdaptRole(u);

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