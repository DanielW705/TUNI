using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TUNIWEB.ClassValidation
{
    public class Universidadvalidation
    {
        [Required(ErrorMessage = "Es necesario este campo")]
        public string nombredelainstitucion { get; set; }
        [Required(ErrorMessage = "Es neccesaria la direccion")]
        public string direccion { get; set; }
        [Required(ErrorMessage = "Es necesario el archivo")]
        public IFormFile metodoi { get; set; }
        [Required(ErrorMessage = "Es necesario el archivo")]
        public IFormFile metodoe { get; set; }
    }
}
