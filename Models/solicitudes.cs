using System;

namespace TUNIWEB.Models
{
    public class solicitudes
    {
        public int nodesolicitud { get; set; }
        public Guid idAlumno { get; set; }
        public Guid idUniversidad { get; set; }
        public UsuarioAlumno relsAl { get; set; }
        public UsuarioUniversidad relsU { get; set; }
    }
}
