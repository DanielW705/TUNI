using System.Collections.Generic;

namespace TUNIWEB.Models
{
    public class areasCarrera
    {
        public int idArea { get; set; }
        public string area { get; set; }
        public ICollection<catCarreras> relareacarr { get; set; }
    }
}
