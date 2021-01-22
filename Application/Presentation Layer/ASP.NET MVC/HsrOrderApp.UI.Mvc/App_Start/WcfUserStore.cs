using HsrOrderApp.UI.Mvc.Models;
using HsrOrderApp.UI.PresentationLogic;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;

namespace HsrOrderApp.UI.Mvc.App_Start
{
    public class WcfUserStore :
        IUserStore<WebApplicationUser>,
        //IUserPasswordStore<WebApplicationUser>,
        //IUserLoginStore<WebApplicationUser>,
        //IUserClaimStore<WebApplicationUser>,
        IUserTwoFactorStore<WebApplicationUser, string>,
        IUserLockoutStore<WebApplicationUser, string>
    {
        private IServiceFacade _service;

        public void Dispose()
        {
            _service.Dispose();
        }

        #region IUserStore<WebApplicationUser>

        public WcfUserStore(IServiceFacade service)
        {
            _service = service;
        }

        public Task CreateAsync(WebApplicationUser user)
        {
            return Task.Run(() => _service.StoreUser(user.UserDto));
        }

        public Task DeleteAsync(WebApplicationUser user)
        {
            return Task.Run(() => _service.DeleteUser(user.UserDto.Id));
        }

        public Task<WebApplicationUser> FindByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<WebApplicationUser> FindByIdAsync(string userId)
        {
            int id;
            if (int.TryParse(userId, out id))
            {
                return Task.Run(() => new WebApplicationUser(_service.GetUserById(id)));
            }
            return null;
        }

        public Task<WebApplicationUser> FindByNameAsync(string userName)
        {
            Func<WebApplicationUser> func = () =>
            {
                var userList = _service.GetUsersByName(userName);
                return userList.Count == 1
                    ? new WebApplicationUser(_service.GetUserById(userList[0].Id))
                    : null;
            };
            return Task.Run(func);
        }

        public Task UpdateAsync(WebApplicationUser user)
        {
            return Task.Run(() => _service.StoreUser(user.UserDto));
        }

        #endregion

        //#region IUserPasswordStore<WebApplicationUser>

        //public Task SetPasswordHashAsync(WebApplicationUser user, string passwordHash)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<string> GetPasswordHashAsync(WebApplicationUser user)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<bool> HasPasswordAsync(WebApplicationUser user)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        //#region Interface IUserLoginStore<WebApplicationUser>

        //public Task AddLoginAsync(WebApplicationUser user, UserLoginInfo login)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task RemoveLoginAsync(WebApplicationUser user, UserLoginInfo login)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IList<UserLoginInfo>> GetLoginsAsync(WebApplicationUser user)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<WebApplicationUser> FindAsync(UserLoginInfo login)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        //#region Interface IUserClaimStore<WebApplicationUser>

        //public Task<IList<Claim>> GetClaimsAsync(WebApplicationUser user)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task AddClaimAsync(WebApplicationUser user, Claim claim)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task RemoveClaimAsync(WebApplicationUser user, Claim claim)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion

        #region IUserTwoFactorStore<WebApplicationUser, string>

        public Task SetTwoFactorEnabledAsync(WebApplicationUser user, bool enabled)
        {
            return Task.Delay(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(WebApplicationUser user)
        {
            return Task.FromResult(false);
        }

        #endregion

        #region Interface IUserLockoutStore<WebApplicationUser, int>

        public Task<int> GetAccessFailedCountAsync(WebApplicationUser user)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetLockoutEnabledAsync(WebApplicationUser user)
        {
            return Task.FromResult(false);
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(WebApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(WebApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(WebApplicationUser user)
        {
            return Task.Delay(0);
        }

        public Task SetLockoutEnabledAsync(WebApplicationUser user, bool enabled)
        {
            return Task.Delay(0);
        }

        public Task SetLockoutEndDateAsync(WebApplicationUser user, DateTimeOffset lockoutEnd)
        {
            return Task.Delay(0);
        }

        #endregion
    }
}