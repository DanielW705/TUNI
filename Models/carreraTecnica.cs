using System;
using System.Collections.Generic;

namespace TUNIWEB.Models
{
    public class carreraTecnica
    {
        public int noderelcat { get; set; }
        public Guid idAlumno { get; set; }
        public UsuarioAlumno relCarrT_Al { get; set; }
        public int catalogoCarrerasTecnicasId { get; set; }
        public ICollection<catalogoCarrerasTecnicas> relCat_cart { get; set; }
    }
}
