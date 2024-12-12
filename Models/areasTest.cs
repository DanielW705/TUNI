using System.Collections.Generic;

namespace TUNIWEB.Models
{
    public class areasTest
    {
        public int areaTestId { get; set; }
        public string areaDelTest { get; set; }
        public ICollection<catCarreras> relareasTestcarr { get; set; }
        public ICollection<ValorPregunta> rel_area_valor { get; set; }
    }
}
