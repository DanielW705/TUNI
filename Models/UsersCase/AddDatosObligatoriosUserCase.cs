using Microsoft.AspNetCore.Http;
using ROP;
using System;
using System.IO;
using System.Threading.Tasks;
using TUNIWEB.Models.ViewModel;

namespace TUNIWEB.Models.UsersCase
{
    public class AddDatosObligatoriosUserCase : SignInCookieUseCase
    {
        private readonly TUNIDbContext _TuniDbContext;
        public AddDatosObligatoriosUserCase(TUNIDbContext tuniDbContext, IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
            _TuniDbContext = tuniDbContext;
        }

        private async Task<Result<DatosObligatoriosViewModel>> AddDatosObligatoriosAlumno(DatosObligatoriosViewModel datosObligatorios, Guid idAlumno)
        {
            Alumno nuevoAlumno = new Alumno()
            {
                idAlumno = idAlumno,
                nombre = datosObligatorios.Nombre,
                apPaterno = datosObligatorios.Apellido_paterno,
                apMaterno = datosObligatorios.Apellido_maternos
            };

            await _TuniDbContext.alumnos.AddAsync(nuevoAlumno);

            try
            {
                await _TuniDbContext.SaveChangesAsync();
                return datosObligatorios;
            }
            catch (Exception ex)
            {
                return Result.Failure<DatosObligatoriosViewModel>(ex.Message);
            }
        }

        private async Task<Result<bool>> AddDatosAcademicosAlumno(DatosObligatoriosViewModel datosObligatorios, Guid idAlumno)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                datosObligatorios.boleta.CopyTo(stream);
                DatosAcademicos datosAcademicos = new DatosAcademicos()
                {
                    idAlumno = idAlumno,
                    boletaGlobal = datosObligatorios.boleta.FileName,
                    doc = stream.ToArray()
                };
                await _TuniDbContext.datosAcademicos.AddAsync(datosAcademicos);
            }
            try
            {
                await _TuniDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return Result.Failure<bool>(ex.Message);
            }
        }

        public async Task<Result<bool>> Execute(DatosObligatoriosViewModel datosObligatorios)
        {
            Result<Guid> userGuid = GetUserGuid();
            if (userGuid.Success)
                return await AddDatosObligatoriosAlumno(datosObligatorios, userGuid.Value)
                    .Bind(x => AddDatosAcademicosAlumno(x, userGuid.Value));
            else
                return Result.NotFound<bool>(userGuid.Errors);
        }
    }
}
