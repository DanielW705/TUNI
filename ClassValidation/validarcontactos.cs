using Microsoft.AspNetCore.JsonPatch.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TUNIWEB.ClassValidation
{
    public class validarcontactos
    {
        [escorreoonumero]
        public string contacto { get; set; }
    }
    public class escorreoonumero : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string contaco = (string)value;

            Regex num = new Regex(@"^((044)?(55)*[0-9]+){10}$");
            Regex correo = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){3})+)$");
            if (contaco != null)
            {
                if (num.IsMatch(contaco)==true || correo.IsMatch(contaco)==true)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(ErrorMessage = "Esto no es un numero ni un correo electronico");
                }

            }
            else
            {
                return new ValidationResult(ErrorMessage = "Es necesario meter el contacto");
            }

        }
    }
}
