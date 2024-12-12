using System;

namespace TUNIWEB.Models
{
    public class DatosAcademicos
    {
        public Guid idAlumno { get; set; }
        public string boletaGlobal { get; set; }
        public byte[] doc { get; set; }
        public UsuarioAlumno Us_relDaac { get; set; }
    }
}
