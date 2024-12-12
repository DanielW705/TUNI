using System;

namespace TUNIWEB.Models
{
    public class carrerasDeseadas
    {
        public int noDeCarreraDeseada { get; set; }
        public Guid idAlumno { get; set; }
        public int idCarrera { get; set; }
        public UsuarioAlumno relCarrD_AL { get; set; }
        public catCarreras relCarrD_catcarr { get; set; }
    }
}
