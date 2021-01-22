using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using HsrOrderApp.BL.BusinessComponents;
using HsrOrderApp.BL.BusinessComponents.DependencyInjection;
using HsrOrderApp.BL.DomainModel;

namespace HsrOrderApp.BL.Security
{
    public class HsrOrderAppRoleProvider:RoleProvider

    {
        private SecurityBusinessComponent _sb;
        private string _applicationName;

        public HsrOrderAppRoleProvider()
        {
            _sb = DependencyInjectionHelper.GetSecurityBusinessComponent();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            //Validate Input Parameters
            if(username==null || roleName== null)
                throw new ArgumentNullException();
            if (username == String.Empty || roleName == String.Empty)
                throw new ArgumentNullException();

            try
            {
                User  user = _sb.GetUserByName(username);

                foreach (var role in user.Roles)
                {
                    if(String.Compare(role.RoleName,roleName,true)==0)
                        return true;
                        
                }
                return false;
                    
            }
            catch (Exception)
            {
                return false;
            }
           
            
        }

        public override string[] GetRolesForUser(string username)
        {

            //Validate Input Parameters
            if (username == null)
                throw new ArgumentNullException();
            if (username == String.Empty)
                throw new ArgumentNullException();

           
            try
            {
                User user = _sb.GetUserByName(username);
                return (from role in user.Roles select role.RoleName).ToArray();
            }
            
            catch(Exception)
            {
                return null;
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotSupportedException();        
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotSupportedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotSupportedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotSupportedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotSupportedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotSupportedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotSupportedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotSupportedException();
        }

        public override string ApplicationName
        {
            get { return _applicationName; }
            set { _applicationName = value; }
        }

        private string GetConfigValue(string configValue, string defaultValue)
        {
            if (String.IsNullOrEmpty(configValue))
            {
                return defaultValue;
            }

            return configValue;
        }
    }
}