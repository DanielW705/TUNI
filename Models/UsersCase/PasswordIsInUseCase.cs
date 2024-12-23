using Microsoft.EntityFrameworkCore;
using ROP;
using System.Linq;
using System.Threading.Tasks;
namespace TUNIWEB.Models.UserCase
{
    public class PasswordIsInUseCase
    {
        private readonly TUNIDbContext TuniDbContext;

        public PasswordIsInUseCase(TUNIDbContext _TuniDbContext)
        {
            this.TuniDbContext = _TuniDbContext;
        }

        private async Task<Result<bool>> CheckIfPasswordIsInUse(string password)
        {
            bool passwordExist = await this.TuniDbContext.alumnosUsuarios.Where(
                a => a.contraseña.Equals(password)
                ).AnyAsync();

            if (passwordExist)
                return Result.Failure<bool>("Alguna de tus credenciales son incorrectas");

            return passwordExist;
        }

        public async Task<Result<bool>> Execute(string password)
        {
            return await CheckIfPasswordIsInUse(password);
        }
    }
}
