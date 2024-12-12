using System;

namespace TUNIWEB.Models
{
    public class contactos
    {
        public int noDeContacto { get; set; }
        public Guid idUniversidad { get; set; }
        public string contacto { get; set; }
        public UsuarioUniversidad relC_USU { get; set; }
    }
}
