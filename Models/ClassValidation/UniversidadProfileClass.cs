using System.Collections.Generic;

namespace TUNIWEB.Models.ClassValidation
{
    public class UniversidadProfileClass
    {
        public UsuarioUniversidad UsuarioUniversidad { get; set; }

        public universidad universidad { get; set; }

        public List<catCarreras> CarrerasImpartidasPorLaUniversidad { get; set; }

        public FileClass MetodoDeIngreso { get; set; }

        public List<contactos> Contactos { get; set; }

        public List <empresaAsociadas> EmpresaAsociadas { get; set; }

        public List <MapaCurricularClass> CatalogoDeMapasCurriculares { get; set; }
    }
}
