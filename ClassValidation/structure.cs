using System;
using TUNIWEB.Models;

namespace TUNIWEB.ClassValidation
{
    public class structure
    {

        public struct archivosrecon
        {
            public string nombre { get; set; }
            public byte[] archivo { get; set; }
        }
        public struct carrerastecnicas
        {
            public string carreratecsel { get; set; }
        }
        public struct recon
        {
            public string namerecon { get; set; }
            public string ruta { get; set; }
        }
        public struct usuarioA
        {
            public Guid idA { get; set; }
            public string usuario { get; set; }
            public string nombre { get; set; }
            public string ap { get; set; }
            public string am { get; set; }
            public string arch { get; set; }
            public string ruta { get; set; }
        }
        public struct areascarrera
        {
            public int idarea { get; set; }
            public string area { get; set; }
        }
        public struct carrerasdeseadas
        {
            public int idare { get; set; }
            public string carrera { get; set; }
        }
        public struct relaciones
        {
            public Guid idUn { get; set; }
            public string universidad { get; set; }
        }
        public struct TestVocacional
        {
            public int nodepreguta { get; set; }
            public string pregunta { get; set; }
            public string area { get; set; }
            public valores valores { get; set; }
        }
        public struct usuariouniversidad
        {
            public Guid idusuario { get; set; }
            public string usuario { get; set; }

        }
        public struct uni
        {
            public string nombredelaInst { get; set; }
            public string direccion { get; set; }
        }
        public struct carrerasi
        {
            public int idarea { get; set; }
            public string carrera { get; set; }
        }
        public struct ingre
        {
            public string name { get; set; }
            public string rut { get; set; }
        }
        public struct egre
        {
            public string name { get; set; }
            public string rut { get; set; }
        }
        public struct contacto
        {
            public string nombre { get; set; }
            public Guid id { get; set; }
        }
        public struct resultados
        {
            public string area { get; set; }
            public int total { get; set; }
        }
    }
}

