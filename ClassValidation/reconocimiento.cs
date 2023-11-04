using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TUNIWEB.ClassValidation
{
    public class reconocimiento
    {
        [Required(ErrorMessage = "El campo debe de estar lleno")]
        public IFormFile recon { get; set; }
    }
}
