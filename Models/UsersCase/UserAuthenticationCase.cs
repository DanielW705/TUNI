using Microsoft.AspNetCore.Mvc.Infrastructure;
using ROP;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace TUNIWEB.Models.UsersCase
{
    public class UserAuthenticationCase
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        public UserAuthenticationCase(IActionContextAccessor actionContextAccessor)
        {
            _actionContextAccessor = actionContextAccessor;
        }

        public string AuthenticateUser()
        {
            ClaimsPrincipal user = _actionContextAccessor.ActionContext.HttpContext.User;
            if (user.Identity.IsAuthenticated)
                return user.IsInRole("Alumno") ? "IndexAlumno" : "IndexUniversidad";
            else
                return null;
        }
    }
}
