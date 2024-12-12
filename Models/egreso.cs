using System;

namespace TUNIWEB.Models
{
    public class egreso
    {
        public Guid idUniversidad { get; set; }
        public string nivelEgreso { get; set; }
        public byte[] doc { get; set; }
        public UsuarioUniversidad relE_USU { get; set; }
    }
}
