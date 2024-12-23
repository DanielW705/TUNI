using System.Collections.Generic;

namespace TUNIWEB.Models.ClassValidation
{
    public class AlumnoProfileClass
    {
        public UsuarioAlumno UsuarioAlumno { get; set; }

        public Alumno Alumno { get; set; }

        public List<FileClass> InformacionAlumno { get; set; }

        public List<catalogoCarrerasTecnicas> CarreraTecnicaDelAlumno { get; set; }

        public FileClass DatosAcademicosAlumno { get; set; }

    }
}
