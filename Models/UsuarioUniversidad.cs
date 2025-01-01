using System;
using System.Collections.Generic;

namespace TUNIWEB.Models
{
    public class UsuarioUniversidad
    {
        public Guid idUniversidad { get; set; }
        public string usuario { get; set; }
        public string contraseña { get; set; }
        public universidad relUSU_U { get; set; }
        public ICollection<catalogoDeMapasCurriculares> relUSU_CACU { get; set; }
        public ingreso relUSU_I { get; set; }
        public egreso relUSU_E { get; set; }
        public ICollection<carBeca> relUSU_carB { get; set; }
        public ICollection<empresaAsociadas> relUSU_EA { get; set; }
        public ICollection<contactos> relUSU_CON { get; set; }
        public ICollection<carrerasimpartidas> relusu_carri { get; set; }
        public ICollection<Relacion> relUSU_REL { get; set; }
        public solicitudes relUS { get; set; }

    }
}
