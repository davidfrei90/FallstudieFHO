using HsrOrderApp.UI.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HsrOrderApp.UI.Mvc.Helpers
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public UserPermission[] RequiredPermissions
        {
            get
            {
                if (string.IsNullOrEmpty(Roles)) { return new UserPermission[0]; }
                return Roles
                    .Split(',')
                    .Select(role => (UserPermission)Enum.Parse(typeof(UserPermission), role))
                    .ToArray();
            }
            set
            {
                Roles = value == null || value.Length == 0
                    ? string.Empty
                    : string.Join(",", value);
            }
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            return true;
        }
    }
}