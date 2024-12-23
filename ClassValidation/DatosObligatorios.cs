using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TUNIWEB.ClassValidation
{
    public class DatosObligatorios
    {
        [Required(ErrorMessage = "Debes tener un nombre"), MaxLength(15, ErrorMessage ="El nombre no debe superar los 15 caracteres"), MinLength(1,ErrorMessage ="El nombre no debe ser menor de los 5 caracteres"), DataType(DataType.Text, ErrorMessage = "Eso no es un texto")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Debes contar con dos apellidos minimo"),MaxLength(30, ErrorMessage = "El apellido Paterno no debe superar los 30 caracteres"), MinLength(5, ErrorMessage = "El apellido Paterno no debe ser menor de los 5 caracteres"),DataType(DataType.Text, ErrorMessage = "Eso no es un texto")]
        public string Apellido_paterno { get; set; }
        [Required(ErrorMessage = "Debes contrar con dos apellidos minimo"), MaxLength(30, ErrorMessage = "El apellido Materno no debe superar los 15 caracteres"), MinLength(5, ErrorMessage = "El apellido Materno no debe ser menor de los 5 caracteres"),DataType(DataType.Text, ErrorMessage ="Eso no es un texto")]
        public string Apellido_maternos { get; set; }
        public IFormFile boleta { get; set;}
    }
}
