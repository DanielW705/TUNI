using Microsoft.AspNetCore.Http;
using ROP;
using System;
using TUNIWEB.Models.ClassValidation;
using TUNIWEB.Models.Enums;
using TUNIWEB.Models.ViewModel;

namespace TUNIWEB.Models.UsersCase
{
    public class GetUserPublicationsCase : SignInCookieUseCase
    {
        private readonly TUNIDbContext _TuniDbContext;
        public GetUserPublicationsCase(TUNIDbContext tuniDbContext, IHttpContextAccessor httpContext) : base(httpContext)
        {
            _TuniDbContext = tuniDbContext;
        }

        public Result<UsersIndexViewModel> Execute()
        {
            Result<UserDataClass> DatosUsuario = GetUserInformation();


            return new UsersIndexViewModel
            {
                
            };
        }
    }
}
