using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using ROP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUNIWEB.Models.ClassValidation;
using TUNIWEB.Models.Enums;
using TUNIWEB.Models.ViewModel;
using TUNIWEB.Utilities;

namespace TUNIWEB.Models.UsersCase
{
    public class GetUserProfileCase : SignInCookieUseCase
    {
        private readonly TUNIDbContext _TuniDbContext;
        public GetUserProfileCase(TUNIDbContext tuniDbContext, IHttpContextAccessor httpContext) : base(httpContext)
        {
            _TuniDbContext = tuniDbContext;
        }

        private async Task<Result<AlumnoProfileClass>> GetUsuarioAlumno(UserDataClass userData)
        {
            UsuarioAlumno usuarioAlumno = await _TuniDbContext
                                                .alumnosUsuarios
                                                .FirstOrDefaultAsync(a => a.idAlumno.Equals(Guid.Parse(userData.UserID)));

            if (usuarioAlumno is null)
                return Result.NotFound<AlumnoProfileClass>("No se encontro al alumno");
            else
                return new AlumnoProfileClass { UsuarioAlumno = usuarioAlumno };
        }

        private async Task<Result<AlumnoProfileClass>> GetAlumno(AlumnoProfileClass alumnoPerfil)
        {
            alumnoPerfil.Alumno = await _TuniDbContext
                            .alumnos
                            .FirstOrDefaultAsync(a => a.idAlumno.Equals(alumnoPerfil.UsuarioAlumno.idAlumno));
            return alumnoPerfil;
        }
        private async Task<Result<AlumnoProfileClass>> GetInformacion(AlumnoProfileClass alumnoPerfil)
        {
            List<informacion> informacion = await _TuniDbContext
                                       .informaciones
                                       .Where(a => a.idAlumno.Equals(alumnoPerfil.UsuarioAlumno.idAlumno))
                                       .ToListAsync();

            informacion.ForEach(inf =>
            {
                FileClass infFile = new FileClass()
                {
                    FileName = inf.reconocimiento,
                    Route = CreateTemporalFiles.CreateTemporalyFile(inf.idAlumno, "Reconocimientos", inf.reconocimiento, inf.doc)
                };
                alumnoPerfil.InformacionAlumno.Add(infFile);
            });

            return alumnoPerfil;
        }
        private async Task<Result<AlumnoProfileClass>> GetCarrerasTecnicas(AlumnoProfileClass alumnoPerfil)
        {
            alumnoPerfil.CarreraTecnicaDelAlumno = await _TuniDbContext
                                                   .carreraTecnicas
                                                   .Where(_carreraTecnica => _carreraTecnica.idAlumno.Equals(alumnoPerfil.UsuarioAlumno.idAlumno))
                                                    .Join(_TuniDbContext.catalogoCarrerasT,
                                                    _catalogoCarrerasT => _catalogoCarrerasT.catalogoCarrerasTecnicasId,
                                                    _carreraTecnica => _carreraTecnica.carreTecnicaId,
                                                    (_catalogoCarrT, _carreraTecnica) => _carreraTecnica)
                                                   .ToListAsync();
            return alumnoPerfil;
        }

        private async Task<Result<AlumnoProfileClass>> GetDatosAcademicos(AlumnoProfileClass alumnoPerfil)
        {
            DatosAcademicos datosAcademicos = await _TuniDbContext
                                                .datosAcademicos
                                                .FirstOrDefaultAsync(dato_academico => dato_academico.idAlumno.Equals(alumnoPerfil.UsuarioAlumno.idAlumno));
            if (!(datosAcademicos is null))
                alumnoPerfil.DatosAcademicosAlumno = new FileClass
                {
                    FileName = datosAcademicos.boletaGlobal,
                    Route = CreateTemporalFiles.CreateTemporalyFile(datosAcademicos.idAlumno, "BoletaGlobal", datosAcademicos.boletaGlobal, datosAcademicos.doc)
                };
            return alumnoPerfil;
        }
        private async Task<Result<UniversidadProfileClass>> GetUsuarioUniversidad(UserDataClass userData)
        {
            UsuarioUniversidad usuarioUniversidad = await _TuniDbContext
                                                .universidadesUsuario
                                                .FirstOrDefaultAsync(a => a.idUniversidad.Equals(Guid.Parse(userData.UserID)));

            if (usuarioUniversidad is null)
                return Result.NotFound<UniversidadProfileClass>("No se encontro al alumno");
            else
                return new UniversidadProfileClass { UsuarioUniversidad = usuarioUniversidad };
        }
        private async Task<Result<UniversidadProfileClass>> GetUniversidad(UniversidadProfileClass universidadPerfil)
        {
            universidadPerfil.universidad = await _TuniDbContext
                            .universidades
                            .FirstOrDefaultAsync(a => a.idUnversidad.Equals(universidadPerfil.UsuarioUniversidad.idUniversidad));
            return universidadPerfil;
        }
        private async Task<Result<UniversidadProfileClass>> GetCarrerasImpartidasPorLaUniversidad(UniversidadProfileClass universidadPerfil)
        {
            universidadPerfil.CarrerasImpartidasPorLaUniversidad = await _TuniDbContext
                                                                    .carrerasImpartadas
                                                                    .Where(_carrerasImpartidas => _carrerasImpartidas.usuarioUniversidad.Equals(universidadPerfil.UsuarioUniversidad.idUniversidad))
                                                                    .Join(_TuniDbContext.catCarreras,
                                                                           _carrerasImpartidas => _carrerasImpartidas.catCarrerasId,
                                                                           _catCarreras => _catCarreras.idCarrera,
                                                                           (_carrerasImpartidas, _catCarreras) => _catCarreras)
                                                                    .ToListAsync();
            return universidadPerfil;
        }
        private async Task<Result<UniversidadProfileClass>> GetMetodoDeIngreso(UniversidadProfileClass universidadPerfil)
        {
            ingreso MetodoDeIngreso = await _TuniDbContext
                                      .ingresos
                                      .FirstOrDefaultAsync(universidad => universidad.idUniversidad.Equals(universidadPerfil.UsuarioUniversidad.idUniversidad));
            if (!(MetodoDeIngreso is null))
                universidadPerfil.MetodoDeIngreso = new FileClass
                {
                    FileName = MetodoDeIngreso.metodoIngreso,
                    Route = CreateTemporalFiles.CreateTemporalyFile(universidadPerfil.UsuarioUniversidad.idUniversidad, "MetodoDeIngreso", MetodoDeIngreso.metodoIngreso, MetodoDeIngreso.doc)
                };
            return universidadPerfil;
        }

        private async Task<Result<UniversidadProfileClass>> GetContactos(UniversidadProfileClass universidadPerfil)
        {
            universidadPerfil.Contactos = await _TuniDbContext
                                                .contactos
                                                .Where(contacto => contacto.idUniversidad.Equals(universidadPerfil.UsuarioUniversidad.idUniversidad))
                                                .ToListAsync();
            return universidadPerfil;
        }
        private async Task<Result<UniversidadProfileClass>> GetEmpresaAsociada(UniversidadProfileClass universidadPerfil)
        {
            universidadPerfil.EmpresaAsociadas = await _TuniDbContext
                                                .empresaAsociadas
                                                .Where(empresa => empresa.idUniversidad.Equals(universidadPerfil.UsuarioUniversidad.idUniversidad))
                                                .ToListAsync();
            return universidadPerfil;
        }
        private async Task<Result<UniversidadProfileClass>> GetCatalogoMapasCurriculares(UniversidadProfileClass universidadPerfil)
        {
            universidadPerfil.CatalogoDeMapasCurriculares = await _TuniDbContext
                                                            .catalogoDeMapasCuarriculares
                                                            .Where(mapa => mapa.idUniversidad.Equals(universidadPerfil.UsuarioUniversidad.idUniversidad))
                                                            .Join(_TuniDbContext.catCarreras,
                                                                   _mapa => _mapa.idCarrera,
                                                                   _catCarreras => _catCarreras.idCarrera,
                                                                   (_mapa, _catCarreras) => new MapaCurricularClass
                                                                   {
                                                                       FileName = _mapa.mapacurricular,
                                                                       Route = CreateTemporalFiles.CreateTemporalyFile(_mapa.idUniversidad, "MapaCurricular", _mapa.mapacurricular, _mapa.doc),
                                                                       Carrera = _catCarreras.Carrera
                                                                   }).ToListAsync();
            return universidadPerfil;
        }
        public async Task<Result<PerfilViewModel>> Execute()
        {
            Result<UserDataClass> DatosUsuario = GetUserInformation();

            if (DatosUsuario.Success)
            {
                UserRolEnum userRol = Enum.Parse<UserRolEnum>(DatosUsuario.Value.UserType);
                if (userRol.Equals(UserRolEnum.Alumno))
                {

                    Result<AlumnoProfileClass> perfilAlumno = await GetUsuarioAlumno(DatosUsuario.Value)
                                                .Bind(x => GetAlumno(x))
                                                .Bind(x => GetInformacion(x))
                                                .Bind(x => GetCarrerasTecnicas(x))
                                                .Bind(x => GetDatosAcademicos(x));
                    if (perfilAlumno.Success)
                        return new PerfilViewModel
                        {
                            UserRol = GetUserRol().Value,
                            PerfilDelAlumno = perfilAlumno.Value
                        };
                    else
                        return Result.Failure<PerfilViewModel>(perfilAlumno.Errors);
                }
                else
                {
                    Result<UniversidadProfileClass> perfilUniversidad = await GetUsuarioUniversidad(DatosUsuario.Value)
                                                                              .Bind(x => GetUniversidad(x))
                                                                              .Bind(x => GetCarrerasImpartidasPorLaUniversidad(x))
                                                                              .Bind(x => GetMetodoDeIngreso(x))
                                                                              .Bind(x => GetEmpresaAsociada(x))
                                                                              .Bind(x => GetCatalogoMapasCurriculares(x));
                    if (perfilUniversidad.Success)
                        return new PerfilViewModel
                        {
                            UserRol = GetUserRol().Value,
                            PerfilDeLaUniversidad = perfilUniversidad.Value
                        };
                    else
                        return Result.Failure<PerfilViewModel>(perfilUniversidad.Errors);
                }

            }
            else
                return Result.Failure<PerfilViewModel>(DatosUsuario.Errors);

        }
    }
}
