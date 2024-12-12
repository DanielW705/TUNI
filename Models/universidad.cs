using System;

namespace TUNIWEB.Models
{
    public class universidad
    {
        public Guid idUnversidad { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public UsuarioUniversidad relU_USU { get; set; }
    }
}
