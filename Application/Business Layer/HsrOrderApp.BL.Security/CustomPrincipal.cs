#region

using System;
using System.Linq;
using System.Security.Principal;
using HsrOrderApp.BL.BusinessComponents;
using HsrOrderApp.BL.BusinessComponents.DependencyInjection;
using HsrOrderApp.BL.DomainModel;

#endregion

namespace HsrOrderApp.BL.Security
{
    public class CustomPrincipal : IPrincipal
    {
        private IIdentity _identity;
        private User _user;
        private SecurityBusinessComponent sb;

        public CustomPrincipal(IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            _identity = identity;

            sb = DependencyInjectionHelper.GetSecurityBusinessComponent();
            this._user = sb.GetUserByName(identity.Name);

            if (_user == null)
            {
                throw new Exception("unknown user");
            }
        }

        public User User
        {
            get { return this._user; }
        }

        #region IPrincipal Members

        public IIdentity Identity
        {
            get { return this._identity; }
        }

        public bool IsInRole(string roleName)
        {
            Role role = _user.Roles.FirstOrDefault(r => r.RoleName == roleName);
            return (role != null);
        }

        #endregion
    }
}