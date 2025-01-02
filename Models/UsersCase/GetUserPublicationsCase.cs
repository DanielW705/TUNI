using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
    public class GetUserPublicationsCase
    {
        private readonly TUNIDbContext _TuniDbContext;
        public GetUserPublicationsCase(TUNIDbContext tuniDbContext)
        {
            _TuniDbContext = tuniDbContext;
        }

        private async Task<List<UserPublicationClass>> GetPublicaciones()
        {

            List<UserPublicationClass> publicaciones = await _TuniDbContext.publicaciones
                                                            .Select(_publicacion => new UserPublicationClass
                                                            {
                                                                ID = _publicacion.Id,
                                                                Usuario = _TuniDbContext.alumnos
                                                                          .Where(al => al.idAlumno.Equals(_publicacion.idUsuario))
                                                                          .Select(al => al.nombre)
                                                                          .Union(_TuniDbContext.universidades
                                                                          .Where(u => u.idUnversidad.Equals(_publicacion.idUsuario))
                                                                          .Select(u => u.nombre))
                                                                          .FirstOrDefault(),
                                                                fechaPublicacion = _publicacion.fechaPublicacion,
                                                                texto = _publicacion.texto,
                                                                img = _publicacion.doc == null ? null : new FileClass
                                                                {
                                                                    FileName = _publicacion.nombre_archivo,
                                                                    Route = CreateTemporalFiles.CreateTemporalyFile(_publicacion.idUsuario, "img", _publicacion.nombre_archivo, _publicacion.doc)
                                                                },
                                                                comentarios = _TuniDbContext.comentarios
                                                                              .Where(com => com.IdPublicacion == _publicacion.Id)
                                                                              .Select(_comentario =>
                                                                              new UserCommentClass
                                                                              {
                                                                                  Usuario = _TuniDbContext.alumnos
                                                                                                          .Where(al => al.idAlumno.Equals(_comentario.IdUsuario))
                                                                                                          .Select(al => al.nombre)
                                                                                                          .Union(_TuniDbContext.universidades
                                                                                                          .Where(u => u.idUnversidad.Equals(_comentario.IdUsuario))
                                                                                                          .Select(u => u.nombre))
                                                                                                          .FirstOrDefault(),
                                                                                  comentario = _comentario.comentario
                                                                              })
                                                                              .ToList(),
                                                                visitas = _publicacion.visitas,
                                                            })
                                                            .Take(5)
                                                            .ToListAsync();

            return publicaciones;
        }
        public async Task<UsersIndexViewModel> Execute()
        {
            return new UsersIndexViewModel
            {
                publicaciones = await GetPublicaciones()
            };
        }
    }
}
