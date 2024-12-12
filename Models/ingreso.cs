using System;

namespace TUNIWEB.Models
{
    public class ingreso
    {
        public Guid idUniversidad { get; set; }
        public string metodoIngreso { get; set; }
        public byte[] doc { get; set; }
        public UsuarioUniversidad relI_USU { get; set; }
    }
}
