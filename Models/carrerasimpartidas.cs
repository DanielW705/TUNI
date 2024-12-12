using System;

namespace TUNIWEB.Models
{
    public class carrerasimpartidas
    {
        public int noderelacion { get; set; }
        public Guid usuarioUniversidad { get; set; }
        public int catCarrerasId { get; set; }
        public UsuarioUniversidad relcarri_unius { get; set; }
        public catCarreras relcarri_catcarr { get; set; }
    }
}
