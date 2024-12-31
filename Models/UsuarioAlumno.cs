using System;
using System.Collections.Generic;

namespace TUNIWEB.Models
{
    public class UsuarioAlumno
    {
        public Guid idAlumno { get; set; }
        public string usuario { get; set; }
        public string contraseña { get; set; }
        public Alumno relUs_Al { get; set; }
        public DatosAcademicos relDaac_Us { get; set; }
        public ICollection<carreraTecnica> relUs_Cart { get; set; }
        public ICollection<informacion> rel_us_info { get; set; }
        public ICollection<carrerasDeseadas> relAL_CARRD { get; set; }
        public ICollection<ValorPregunta> relUSA_VALP { get; set; }
        public ICollection<Relacion> relUSA_REL { get; set; }

        public ICollection<Publicaciones> relUsa_Pu { get; set; }

        public ICollection<Comentarios> relUSA_COM { get; set; }

        public solicitudes relAs { get; set; }
    }
}
