using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TUNIWEB.Models.Enums;

namespace TUNIWEB.Models.ClassValidation
{
    public class LoginUserClass
    {
        [DisplayName("Nombre de usuario")]
        [Required(ErrorMessage = "Es necesario tu {0} para entrar")]
        [MinLength(5, ErrorMessage = "Tu usuario debe ser de minimo 5 caracteres")]
        public string username { get; set; }

        [Required(ErrorMessage = "Es necesario una contraseña"), RegularExpression(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,16}$", ErrorMessage = "La contraseña debe tener una mayuscula una minuscula y un numero")]
        [MinLength(5, ErrorMessage = "La contraseña debe ser de minimo 10 caracteres")]
        public string contraseña { get; set; }
        public UserRolEnum tipo_usuario { get; set; }

    }
}
