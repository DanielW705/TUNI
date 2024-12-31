using System;

namespace TUNIWEB.Models
{
    public class Comentarios
    {
        public int Id { get; set; }

        public Guid IdPublicacion { get; set; }

        public Guid IdUsuario { get; set; }

        public string comentario { get; set; }

        public UsuarioAlumno relCom_USA { get; set; }

        public UsuarioUniversidad relCom_USU { get; set; }
        public Publicaciones relCom_Pub { get; set; }
    }
}