using System;

namespace TUNIWEB.Models
{
    public class carBeca
    {
        public int noBeca { get; set; }
        public Guid idUniversidad { get; set; }
        public string becaInstitucional { get; set; }
        public byte[] doc { get; set; }
        public UsuarioUniversidad relCB_USU { get; set; }
    }
}
