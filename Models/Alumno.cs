using System;

namespace TUNIWEB.Models
{
    public class Alumno
    {
        public Guid idAlumno { get; set; }
        public string nombre { get; set; }
        public string apPaterno { get; set; }
        public string apMaterno { get; set; }
        public UsuarioAlumno relAl_Us { get; set; }
    }
}
