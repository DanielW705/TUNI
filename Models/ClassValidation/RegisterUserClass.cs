using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TUNIWEB.Models.Enums;

namespace TUNIWEB.Models.ClassValidation
{
    public class RegisterUserClass
    {
        [Required(ErrorMessage = "Es necesario un usuario")]
        [MinLength(5, ErrorMessage = "Tu usuario debe de ser de minimo 5 caracteres")]
        public string username { get; set; }
        [MinLength(5, ErrorMessage = "Tu contraseña debe ser de minimo 5 caracteres")]
        [Required(ErrorMessage = "Es necesario una contraseña"), RegularExpression(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,16}$", ErrorMessage = "La contraseña debe tener una mayuscula una minuscula y un numero")]
        public string pasword { get; set; }
        [Required(ErrorMessage = "Es necesario validar la contraseña")]
        [Compare("pasword", ErrorMessage = "Las dos contraseñas deben coincidir")]
        public string confirmPassword { get; set; }
        public UserRolEnum tipo_usuario { get; set; }

    }
}
