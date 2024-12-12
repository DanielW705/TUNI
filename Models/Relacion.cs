using System;

namespace TUNIWEB.Models
{
    public class Relacion
    {
        public int nodeRelacion { get; set; }
        public Guid idAlumno { get; set; }
        public Guid idUniversidad { get; set; }
        public UsuarioAlumno relrel_USA { get; set; }
        public UsuarioUniversidad relrel_USU { get; set; }
    }
}
