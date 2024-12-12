using System;

namespace TUNIWEB.Models
{
    public class informacion
    {
        public int idnoRecon { get; set; }
        public Guid idAlumno { get; set; }
        public string reconocimiento { get; set; }
        public byte[] doc { get; set; }
        public UsuarioAlumno info_usuario { get; set; }
    }
}
