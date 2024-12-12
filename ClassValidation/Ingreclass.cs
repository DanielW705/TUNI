using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TUNIWEB.Models;

namespace TUNIWEB.ClassValidation
{
    public class Ingreclass
    {
        [DisplayName("Nombre de usuario")]
        [Required(ErrorMessage = "Es necesario tu {0} para entrar")]
        [MinLength(5,ErrorMessage ="Tu usuario debe ser de minimo 5 caracteres")]
        //[existencia(Ingreclass)]
        public string userName { get; set; }
        [Required(ErrorMessage = "Es necesario una contraseña"), RegularExpression(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,16}$", ErrorMessage = "La contraseña debe tener una mayuscula una minuscula y un numero"),
            MinLength(5,ErrorMessage ="La contraseña debe ser de minimo 10 caracteres")]
        public string pasword { get; set; }
    }
    public class existencia : ValidationAttribute
    {
        public string _OtherAtribute { get; }
        public existencia(Ingreclass ingreclass)
        {
            _OtherAtribute = ingreclass.pasword;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string username = (string)value;
            TUNIDbContext bd = new TUNIDbContext();

            if (!(bd.alumnosUsuarios.Where(d=>d.usuario == username && d.contraseña == _OtherAtribute).Any()))
            {
                return new ValidationResult(ErrorMessage = "No existe tal usuario");
            }
            return ValidationResult.Success;
        }
    }
}
