using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using ROP;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using TUNIWEB.Models.ClassValidation;
using Newtonsoft.Json;
using TUNIWEB.Models.Enums;

namespace TUNIWEB.Models.UsersCase
{
    public class SignInCookieUseCase
    {
        private readonly IHttpContextAccessor _HttpContext;

        public SignInCookieUseCase(IHttpContextAccessor HttpContext)
        {
            _HttpContext = HttpContext;
        }
        private static ClaimsPrincipal ConstructClaims(string rol)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, rol)
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims);

            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            return principal;
        }

        protected async Task<Result<Unit>> SignInCookieAuthentication(UserRolEnum tipo_usuario, Guid idUser)
        {
            await _HttpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                ConstructClaims(tipo_usuario.ToString()),
                new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.Now.AddHours(24) })
                ;

            UserDataClass data = new UserDataClass(idUser.ToString(), tipo_usuario.ToString());
            _HttpContext.HttpContext.Response.Cookies.Append("DataUser", JsonConvert.SerializeObject(data), new CookieOptions { HttpOnly = false, Expires = DateTime.Now.AddHours(24) });


            return Result.Unit;
        }
        protected Result<UserDataClass> GetUserInformation()
        {
            string userData = string.Empty;
            bool ExistCookie = _HttpContext.HttpContext.Request.Cookies.TryGetValue("DataUser", out userData);
            if (ExistCookie)
            {
                UserDataClass data = JsonConvert.DeserializeObject<UserDataClass>(userData);
                return data;
            }
            else
                return Result.NotFound<UserDataClass>("No se encontro el usuario");
        }
        protected Result<Guid> GetUserGuid()
        {
            Result<UserDataClass> UserInformation = GetUserInformation();

            if (UserInformation.Success)
            {
                return Guid.Parse(UserInformation.Value.UserID);
            }
            else
            {
                return Result.Failure<Guid>(UserInformation.Errors);
            }
        }
        protected Result<UserRolEnum> GetUserRol()
        {
            Result<UserDataClass> UserInformation = GetUserInformation();

            if (UserInformation.Success)
            {
                return Enum.Parse<UserRolEnum>(UserInformation.Value.UserType);
            }
            else
            {
                return Result.Failure<UserRolEnum>(UserInformation.Errors);
            }
        }

    }
}
