using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ROP;
using System;
using System.Linq;
using System.Threading.Tasks;
using TUNIWEB.Models.ClassValidation;
using TUNIWEB.Models.UsersCase;

namespace TUNIWEB.Models.UserCase
{
    public class LoginUserCase: SignInCookieUseCase
    {
        private readonly TUNIDbContext _TuniDbContext;

        public LoginUserCase(TUNIDbContext TuniDbContext, IHttpContextAccessor HttpContext): base(HttpContext)
        {
            _TuniDbContext = TuniDbContext;
        }

        private async Task<Result<LoginUserClass>> TheUserHasCorrectCredentials(LoginUserClass loginUser)
        {
            bool userExist = false;
            if (loginUser.tipo_usuario == Enums.UserRolEnum.Alumno)
                userExist = await _TuniDbContext.alumnosUsuarios.Where(
                    a => a.usuario.Equals(loginUser.username) &&
                    a.contraseña.Equals(loginUser.contraseña)).AnyAsync();
            else
                userExist = await _TuniDbContext.universidadesUsuario.Where(
                    a => a.usuario.Equals(loginUser.username) &&
                    a.contraseña.Equals(loginUser.contraseña)).AnyAsync();

            if (!userExist)
                return Result.Failure<LoginUserClass>("El usuario no existe");
            else
                return loginUser;
        }

        private async Task<Result<Guid>> GetLogInUserGUID(LoginUserClass loginUser)
        {
            if (loginUser.tipo_usuario == Enums.UserRolEnum.Alumno)
                return await _TuniDbContext.alumnosUsuarios.Where(
                    a => a.usuario.Equals(loginUser.username) &&
                    a.contraseña.Equals(loginUser.contraseña))
                    .Select(a => a.idAlumno).FirstAsync();
            else
                return await _TuniDbContext.universidadesUsuario.Where(
                    a => a.usuario.Equals(loginUser.username) &&
                    a.contraseña.Equals(loginUser.contraseña))
                    .Select(a => a.idUniversidad).FirstAsync();
        }

        public async Task<Result<bool>> Execute(LoginUserClass loginUser)
        {
            Result<Guid> result = await TheUserHasCorrectCredentials(loginUser)
                .Bind(x => GetLogInUserGUID(x));
            if (result.Success)
                await SignInCookieAuthentication(loginUser.tipo_usuario, result.Value);
            return result.Success;
        }
    }
}
