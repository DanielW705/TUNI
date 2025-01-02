using System;
using System.Collections.Generic;

namespace TUNIWEB.Models.ClassValidation
{
    public class UserPublicationClass
    {
        public Guid ID { get; set; }

        public string Usuario { get; set; }

        public DateTime fechaPublicacion { get; set; }

        public string texto { get; set; }

        public FileClass img { get; set; }

        public List<UserCommentClass> comentarios { get; set; }
        public int visitas { get; set; }
    }
}
