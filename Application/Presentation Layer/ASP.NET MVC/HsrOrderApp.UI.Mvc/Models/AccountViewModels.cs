using HsrOrderApp.SharedLibraries.DTO;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HsrOrderApp.UI.Mvc.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class WebApplicationUser : IUser<string>
    {
        public WebApplicationUser(UserDTO user)
        {
            UserDto = user;
        }

        public UserDTO UserDto { get; private set; }

        public string Id => UserDto.Id.ToString();

        public string UserName
        {
            get { return UserDto.UserName; }
            set { UserDto.UserName = value; }
        }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<WebApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            foreach (var role in UserDto.Roles)
            {
                userIdentity.AddClaim(new Claim(ClaimTypes.Role, role.RoleName));
            }

            return userIdentity;
        }
    }
    public enum UserPermission
    {
        NONE = -999,
        USER = 0,
        ADMIN = 1,
        STAFF = 2
    }
}
