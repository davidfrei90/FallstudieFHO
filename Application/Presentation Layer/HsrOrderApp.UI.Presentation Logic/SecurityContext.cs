using System.Linq;
using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.SharedLibraries.SharedEnums;

namespace HsrOrderApp.UI.PresentationLogic
{
    public class SecurityContext
    {
        #region Singleton

        private static SecurityContext _instance;

        private SecurityContext()
        {
            IServiceFacade service = ServiceFacade.GetInstance();
            _user = service.GetCurrentUser();
        }

        public static SecurityContext Current()
        {
            if (_instance == null)
            {
                _instance = new SecurityContext();
            }
            return _instance;
        }

        #endregion

        private CurrentUserDTO _user;

        public bool IsLoggedIn
        {
            get { return _user != null; }
        }

        public bool IsAdmin
        {
            get
            {
                if (_user == null)
                    return false;
                RoleDTO role = _user.Roles.FirstOrDefault(r => r.RoleName == Roles.ADMIN);
                return role != null;
            }
        }
    }
}