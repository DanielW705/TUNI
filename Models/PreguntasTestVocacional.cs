using System.Collections.Generic;

namespace TUNIWEB.Models
{
    public class PreguntasTestVocacional
    {
        public int PregunataId { get; set; }
        public string pregunta { get; set; }
        public ICollection<ValorPregunta> rel_pregunta_valor { get; set; }
    }
}
