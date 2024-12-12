using System;

namespace TUNIWEB.Models
{
    public class empresaAsociadas
    {
        public int noDeEmpresaAsociada { get; set; }
        public Guid idUniversidad { get; set; }
        public string empresaAsociada { get; set; }
        public UsuarioUniversidad relEA_USU { get; set; }
    }
}
