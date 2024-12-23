using TUNIWEB.Models.ClassValidation;
using TUNIWEB.Models.Enums;

namespace TUNIWEB.Models.ViewModel
{
    public class PerfilViewModel
    {
        public UserRolEnum UserRol { get; set; }

        public AlumnoProfileClass PerfilDelAlumno { get; set; }

        public UniversidadProfileClass PerfilDeLaUniversidad { get; set; }

    }
}
