using System.Collections.Generic;

namespace TUNIWEB.Models
{
    public class catCarreras
    {
        public int idCarrera { get; set; }
        public int areasCarreraId { get; set; }
        public int areasTestId { get; set; }
        public string Carrera { get; set; }
        public areasCarrera relcarrarea { get; set; }
        public areasTest relareatestcarr { get; set; }
        public ICollection<carrerasDeseadas> relcatcarr_carrD { get; set; }
        public ICollection<carrerasimpartidas> relcarr_carri { get; set; }
    }
}
