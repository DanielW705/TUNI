using System;
using System.Collections.Generic;

namespace TUNIWEB.Models
{
    public class Publicaciones
    {
        public Guid Id { get; set; }

        public Guid idUsuario { get; set; }

        public DateTime fechaPublicacion { get; set; }

        public int visitas {  get; set; }

        public string texto { get; set; }

        public string nombre_archivo { get; set; }

        public byte[] doc {  get; set; }

        public ICollection<Comentarios> relPub_Com {  get; set; }
    }
}
