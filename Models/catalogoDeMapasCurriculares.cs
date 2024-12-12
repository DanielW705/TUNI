using System;

namespace TUNIWEB.Models
{
    public class catalogoDeMapasCurriculares
    {
        public int noDeMapaCurricular { get; set; }
        public Guid idUniversidad { get; set; }
        public int idCarrera { get; set; }
        public string mapacurricular { get; set; }
        public byte[] doc { get; set; }
        public UsuarioUniversidad relCATCU_USU { get; set; }
    }
}
