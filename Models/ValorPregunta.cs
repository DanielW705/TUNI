using System;

namespace TUNIWEB.Models
{
    public class ValorPregunta
    {
        public int noDePregunta { get; set; }
        public Guid idAlumno { get; set; }
        public int idPregunta { get; set; }
        public int areasTestID { get; set; }
        public valores valor { get; set; }
        public UsuarioAlumno rel_valorpregutna_us { get; set; }
        public PreguntasTestVocacional rel_valorpregunta_pregunta { get; set; }
        public areasTest rel_valor_area { get; set; }
    }
}
