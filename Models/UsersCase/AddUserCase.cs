using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using ROP;
using System;
using System.Linq;
using System.Threading.Tasks;
using TUNIWEB.Models.ClassValidation;

namespace TUNIWEB.Models.UsersCase
{
    public class AddUserCase : SignInCookieUseCase
    {
        private readonly TUNIDbContext _TuniDbContext;
        public AddUserCase(TUNIDbContext tuniDbContext, IHttpContextAccessor HttpContext) : base(HttpContext)
        {
            _TuniDbContext = tuniDbContext;
        }

        private async Task<Result<RegisterUserClass>> ValidateUserName(RegisterUserClass newUser)
        {
            bool userNameIsUser = false;
            if (newUser.tipo_usuario == Enums.UserRolEnum.Alumno)
                userNameIsUser = !await _TuniDbContext.alumnosUsuarios.Where(
                    a => a.usuario.Equals(newUser.username)
                    ).AnyAsync();
            else
                userNameIsUser = !await _TuniDbContext.universidadesUsuario.Where(
                    a => a.usuario.Equals(newUser.username)
                    ).AnyAsync();

            if (userNameIsUser)
                return newUser;
            else
                return Result.Failure<RegisterUserClass>("El usuario ya existe");
        }
        private async Task<Result<Guid>> ADdUserToDatabase(RegisterUserClass newUser)
        {
            EntityEntry response = null;
            Guid newUserGuid = Guid.Empty;
            try
            {
                if (newUser.tipo_usuario == Enums.UserRolEnum.Alumno)
                {
                    UsuarioAlumno nuevoUsuarioAlumno = new UsuarioAlumno()
                    {
                        usuario = newUser.username,
                        contraseña = newUser.pasword
                    };
                    response = await _TuniDbContext.alumnosUsuarios.AddAsync(nuevoUsuarioAlumno);
                    await _TuniDbContext.SaveChangesAsync();
                    newUserGuid = nuevoUsuarioAlumno.idAlumno;
                }
                else
                {
                    UsuarioUniversidad nuevoUsuarioUniversidad = new UsuarioUniversidad()
                    {
                        usuario = newUser.username,
                        contraseña = newUser.pasword
                    };
                    response = await _TuniDbContext.universidadesUsuario.AddAsync(nuevoUsuarioUniversidad);
                    await _TuniDbContext.SaveChangesAsync();
                    newUserGuid = nuevoUsuarioUniversidad.idUniversidad;
                }
                return newUserGuid;
            }
            catch (Exception ex)
            {
                return Result.Failure<Guid>(ex.Message);
            }
        }

        public async Task<Result<bool>> Execute(RegisterUserClass newUser)
        {
            Result<Unit> userAded = await ValidateUserName(newUser)
                                    .Bind(x => ADdUserToDatabase(x))
                                    .Bind(x => SignInCookieAuthentication(newUser.tipo_usuario, x));
            return userAded.Success;
        }
    }
}
