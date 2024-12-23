using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Claims;
using TUNIWEB.Models;
using static TUNIWEB.ClassValidation.structure;

namespace TUNIWEB.ClassValidation
{
    public class Operaciones
    {
        #region UNiversidad
        private static UsuarioUniversidad _nvousuarioUNI;
        private static universidad _nvouniversidad;
        private static ingreso _nvoingreso;
        private static egreso _nvoegreso;
        private static UsuarioAlumno _nvousuario;
        private static List<carrerasimpartidas> _carrerasImpartidas = new List<carrerasimpartidas>();
        private static List<string> _contactosyasubidos = new List<string>();
        private static List<int> _carrerasImpartidasyasubidas = new List<int>();
        private static List<contactos> _listacontactos = new List<contactos>();
        #endregion
        #region alumno
        private static Alumno _nvoalumno;
        private static DatosAcademicos _datosAcademicos;
        private static List<carreraTecnica> _carrerasTecnicas = new List<carreraTecnica>();
        private static List<informacion> _informacion = new List<informacion>();
        private static List<carrerasDeseadas> _carrerasDeseadas = new List<carrerasDeseadas>();
        private static List<int> _yasubidos = new List<int>();
        private static List<int> _yasubidosTest = new List<int>();
        public static string[,] arreglo;//{ { "1", "Pregunta 1" }, { "2", "Pregunta2" } };
        public static valores[] arreglo2;
        #endregion
        #region general 
        private static Guid _idSing;
        public static int i = 0;
        #endregion
        #region getusuados
        #region universidad
        #endregion
        #region alumnos
        #endregion
        #endregion
        public static Guid IdSing { get { return _idSing; } set { _idSing = value; } }
        public static Guid existenciaA(Ingreclass ingre, TUNIDbContext bd)
        {
            if (bd.alumnosUsuarios.Where(d => d.usuario == ingre.userName && d.contraseña == ingre.pasword).Any())
            {
                Guid guid = (from d in bd.alumnosUsuarios where (d.usuario == ingre.userName && d.contraseña == ingre.pasword) select d.idAlumno).FirstOrDefault();
                return guid;
            }
            else
            {
                return Guid.Empty;
            }
        }
        public static Guid existenciaU(Ingreclass ingre, TUNIDbContext bd)
        {
            if (bd.universidadesUsuario.Where(d => d.usuario == ingre.userName && d.contraseña == ingre.pasword).Any())
            {
                Guid guid = (from d in bd.universidadesUsuario where (d.usuario == ingre.userName && d.contraseña == ingre.pasword) select d.idUniversidad).FirstOrDefault();
                return guid;
            }
            else
            {
                return Guid.Empty;
            }
        }
        public static Guid existenciaADMON(Ingreclass ingre, TUNIDbContext bd)
        {
            if (bd.Administradores.Where(d => d.username == ingre.userName && d.contraseña == ingre.pasword).Any())
            {
                Guid guid = (from d in bd.Administradores where (d.username == ingre.userName && d.contraseña == ingre.pasword) select d.idAmon).FirstOrDefault();
                return guid;
            }
            else
            {
                return Guid.Empty;
            }
        }
        public static universidad Get_nvouniversidad()
        {
            return _nvouniversidad;
        }

        public static void Set_uniob(Universidadvalidation value)
        {
            _nvouniversidad = new universidad { nombre = value.nombredelainstitucion, direccion = value.direccion };
            using (MemoryStream stream = new MemoryStream())
            {
                value.metodoi.CopyTo(stream);
                _nvoingreso = new ingreso { metodoIngreso = value.metodoi.FileName, doc = stream.ToArray() };
                value.metodoe.CopyTo(stream);
                _nvoegreso = new egreso { nivelEgreso = value.metodoe.FileName, doc = stream.ToArray() };
                stream.Close();
            }
        }
        private static UsuarioUniversidad Get_nvousuarioUNI()
        {
            return _nvousuarioUNI;
        }

        public static void Set_nvousuarioUNI(UserRegister us)
        {
            _nvousuarioUNI = new UsuarioUniversidad { usuario = us.username, contraseña = us.pasword };
        }
        public static List<contactos> Get_listacontactos()
        {
            return _listacontactos;
        }

        public static bool Set_listacontactos(string value)
        {
            if (!_contactosyasubidos.Contains(value))
            {
                _contactosyasubidos.Add(value);
                _listacontactos.Add(new contactos { contacto = value });
                return false;
            }
            else
            {
                return true;
            }
        }
        public static List<carrerasimpartidas> Get_carrerasImpartidas()
        {
            return _carrerasImpartidas;
        }
        public static bool Set_carrerasImpartidas(int value)
        {
            if (!(_carrerasImpartidasyasubidas.Contains(value)))
            {
                _carrerasImpartidasyasubidas.Add(value);
                _carrerasImpartidas.Add(new carrerasimpartidas { catCarrerasId = value });
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void matrizpreguntas(TUNIDbContext bd)
        {
            var list = (from p in bd.preguntasDelTestVocacional
                        select new PreguntasTestVocacional
                        {
                            PregunataId = p.PregunataId,
                            pregunta = p.pregunta
                        }).AsQueryable();
            var list2 = list.ToArray();
            arreglo = new string[2, list2.Length];
            if (arreglo2 == null)
            {
                arreglo2 = new valores[list2.Length];
            }
            for (int k = 0; k < list2.Length; k++)
            {
                arreglo[0, k] = list2[k].PregunataId.ToString();
                arreglo[1, k] = list2[k].pregunta.ToString();
            }
        }
        public static Tuple<string, string> retutup()
        {
            if (!(i >= (arreglo.Length / 2)))
            {

                Tuple<string, string> tupla = new Tuple<string, string>(Operaciones.arreglo[0, i], Operaciones.arreglo[1, i]);
                return tupla;


            }
            else
            {
                return null;
            }
        }
        public static void darvalores(valores val)
        {
            if (i < arreglo2.Length)
            {
                arreglo2[i] = val;
                i++;
            }
        }
        public static valores[] retuval()
        {
            return arreglo2;
        }
        public static List<ValorPregunta> realizarlaaccion(valores[] arr, string id)
        {
            List<ValorPregunta> valorant = new List<ValorPregunta>();
            for (int k = 0; k < arr.Length; k++)
            {
                var objt = new ValorPregunta
                {
                    idAlumno = Guid.Parse(id),
                    idPregunta = k + 1,
                    areasTestID = mayora10(k + 1),
                    valor = arr[k]
                };
                valorant.Add(objt);
            }
            return valorant;
        }
        public static int mayora10(int i)
        {
            int retornable = 0;
            if (i > 10)
            {
                do
                {
                    retornable = i % 10;
                    if (retornable == 0)
                    {
                        retornable = 10;
                    }
                } while (retornable > 10);
            }
            else
            {
                retornable = i;
            }
            return retornable;
        }
        private static List<informacion> Get_informacion()
        {
            return _informacion;
        }

        public static void Set_informacion(reconocimiento value)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                value.recon.CopyTo(stream);
                _informacion.Add(new informacion { reconocimiento = value.recon.FileName, doc = stream.ToArray() });
                stream.Close();
            }
        }
        public static string Set_informacion(IFormFile value)
        {

            using (MemoryStream stream = new MemoryStream())
            {

                value.CopyTo(stream);
                _informacion.Add(new informacion { reconocimiento = value.FileName, doc = stream.ToArray() });
                stream.Close();
                return "";
            }
        }
        private static UsuarioAlumno Getnvousuario()
        {
            return _nvousuario;
        }

        public static void Setnvousuario(UserRegister value)
        {
            _nvousuario = new UsuarioAlumno { usuario = value.username, contraseña = value.pasword };
        }


        private static Alumno Getnvoalumno()
        {
            return _nvoalumno;
        }

        //public static void Setnvoalumno(DatosObligatorios value)
        //{
        //    _nvoalumno = new Alumno { nombre = value.Nombre, apPaterno = value.Ap, apMaterno = value.Am };
        //}


        private static DatosAcademicos GetdatosAcademicos()
        {
            return _datosAcademicos;
        }

        public static void SetdatosAcademicos(DatosObligatorios value)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                value.boleta.CopyTo(stream);
                stream.Close();
                _datosAcademicos = new DatosAcademicos { boletaGlobal = value.boleta.FileName, doc = stream.ToArray() };
            }
        }


        private static List<carreraTecnica> GetcarrerasTecnicas()
        {
            return _carrerasTecnicas;
        }

        public static bool SetcarrerasTecnicas(int value)
        {
            if (!_yasubidos.Contains(value))
            {
                _yasubidos.Add(value);
                _carrerasTecnicas.Add(new carreraTecnica { catalogoCarrerasTecnicasId = value });
                return true;
            }
            else
            {
                return false;
            }
        }
        private static List<carrerasDeseadas> GetcarrerasDeseadas()
        {
            return _carrerasDeseadas;
        }

        public static bool SetcarrerasDeseadas(int value)
        {
            if (!_yasubidosTest.Contains(value))
            {
                _yasubidosTest.Add(value);
                _carrerasDeseadas.Add(new carrerasDeseadas { idCarrera = value });
                return true;

            }
            else
            {
                return false;
            }
        }
        public static UsuarioAlumno GetperfilA()
        {
            var nvoperfilAlumno = new UsuarioAlumno
            {
                usuario = _nvousuario.usuario,
                contraseña = _nvousuario.contraseña,
                relUs_Al = Getnvoalumno(),
                relDaac_Us = GetdatosAcademicos(),
                relUs_Cart = GetcarrerasTecnicas(),
                rel_us_info = Get_informacion(),
                relAL_CARRD = GetcarrerasDeseadas()
            };
            return nvoperfilAlumno;
        }
        public static UsuarioUniversidad GetperfilU()
        {
            var nvoperfilUniversidad = new UsuarioUniversidad
            {
                usuario = _nvousuarioUNI.usuario,
                relUSU_U = _nvouniversidad,
                contraseña = _nvousuarioUNI.contraseña,
                relUSU_E = _nvoegreso,
                relUSU_I = _nvoingreso,
                relUSU_CON = _listacontactos,
                relusu_carri = _carrerasImpartidas
            };
            return nvoperfilUniversidad;
        }
        public static ClaimsPrincipal Authentificarlo(string rol)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier,_idSing.ToString()),
                new Claim(ClaimTypes.Role,  rol)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            return principal;
        }
        #region inicializado de la base de datos
        public static void inicializar(TUNIDbContext bd)
        {
            #region bytes
            string path = System.Reflection.Assembly.GetEntryAssembly().Location;
            var directory = Path.GetDirectoryName(path);
            string base64 = "JVBERi0xLjcNCiW1tbW1DQoxIDAgb2JqDQo8PC9UeXBlL0NhdGFsb2cvUGFnZXMgMiAwIFIvTGFuZyhlcykgL1N0cnVjdFRyZWVSb290IDE1IDAgUi9NYXJrSW5mbzw8L01hcmtlZCB0cnVlPj4vTWV0YWRhdGEgMjcgMCBSL1ZpZXdlclByZWZlcmVuY2VzIDI4IDAgUj4+DQplbmRvYmoNCjIgMCBvYmoNCjw8L1R5cGUvUGFnZXMvQ291bnQgMS9LaWRzWyAzIDAgUl0gPj4NCmVuZG9iag0KMyAwIG9iag0KPDwvVHlwZS9QYWdlL1BhcmVudCAyIDAgUi9SZXNvdXJjZXM8PC9Gb250PDwvRjEgNSAwIFIvRjIgMTIgMCBSPj4vRXh0R1N0YXRlPDwvR1MxMCAxMCAwIFIvR1MxMSAxMSAwIFI+Pi9Qcm9jU2V0Wy9QREYvVGV4dC9JbWFnZUIvSW1hZ2VDL0ltYWdlSV0gPj4vTWVkaWFCb3hbIDAgMCA2MTIgNzkyXSAvQ29udGVudHMgNCAwIFIvR3JvdXA8PC9UeXBlL0dyb3VwL1MvVHJhbnNwYXJlbmN5L0NTL0RldmljZVJHQj4+L1RhYnMvUy9TdHJ1Y3RQYXJlbnRzIDA+Pg0KZW5kb2JqDQo0IDAgb2JqDQo8PC9GaWx0ZXIvRmxhdGVEZWNvZGUvTGVuZ3RoIDI1Mz4+DQpzdHJlYW0NCnicjVDLasMwELwL9A9zTAqxV7IeNhgd7DxoqQ8hhhZCDjmkPtW0zf9DV7JLGuihghU7w8xIDPLDx3lEXedd+7gG5c/nccDicl11r8sQ0KxbfEpBGcVTKQ2C49tXGl8XKV4eMErR9FLkWwXlMq0t+jcpFAsJTJUu8x6eXKYK9O8s3B0UYbhyKoYJqhnupDjWRLokso7HE5kyGKaoCCvleTGGecvARsAi0wSt4t7OClvx6Im82ebIRMTAlD+rzPbHqoOq7mx+IqI9kS6qiolM30i28qYyds404YT+SYoNd7OX4r8l6r9KNNpm5e8OU3NTXwss7x7CpmvxDbeOXl8NCmVuZHN0cmVhbQ0KZW5kb2JqDQo1IDAgb2JqDQo8PC9UeXBlL0ZvbnQvU3VidHlwZS9UeXBlMC9CYXNlRm9udC9BcmlhbC1Cb2xkTVQvRW5jb2RpbmcvSWRlbnRpdHktSC9EZXNjZW5kYW50Rm9udHMgNiAwIFIvVG9Vbmljb2RlIDIzIDAgUj4+DQplbmRvYmoNCjYgMCBvYmoNClsgNyAwIFJdIA0KZW5kb2JqDQo3IDAgb2JqDQo8PC9CYXNlRm9udC9BcmlhbC1Cb2xkTVQvU3VidHlwZS9DSURGb250VHlwZTIvVHlwZS9Gb250L0NJRFRvR0lETWFwL0lkZW50aXR5L0RXIDEwMDAvQ0lEU3lzdGVtSW5mbyA4IDAgUi9Gb250RGVzY3JpcHRvciA5IDAgUi9XIDI1IDAgUj4+DQplbmRvYmoNCjggMCBvYmoNCjw8L09yZGVyaW5nKElkZW50aXR5KSAvUmVnaXN0cnkoQWRvYmUpIC9TdXBwbGVtZW50IDA+Pg0KZW5kb2JqDQo5IDAgb2JqDQo8PC9UeXBlL0ZvbnREZXNjcmlwdG9yL0ZvbnROYW1lL0FyaWFsLUJvbGRNVC9GbGFncyAzMi9JdGFsaWNBbmdsZSAwL0FzY2VudCA5MDUvRGVzY2VudCAtMjEwL0NhcEhlaWdodCA3MjgvQXZnV2lkdGggNDc5L01heFdpZHRoIDI2MjgvRm9udFdlaWdodCA3MDAvWEhlaWdodCAyNTAvTGVhZGluZyAzMy9TdGVtViA0Ny9Gb250QkJveFsgLTYyOCAtMjEwIDIwMDAgNzI4XSAvRm9udEZpbGUyIDI0IDAgUj4+DQplbmRvYmoNCjEwIDAgb2JqDQo8PC9UeXBlL0V4dEdTdGF0ZS9CTS9Ob3JtYWwvY2EgMT4+DQplbmRvYmoNCjExIDAgb2JqDQo8PC9UeXBlL0V4dEdTdGF0ZS9CTS9Ob3JtYWwvQ0EgMT4+DQplbmRvYmoNCjEyIDAgb2JqDQo8PC9UeXBlL0ZvbnQvU3VidHlwZS9UcnVlVHlwZS9OYW1lL0YyL0Jhc2VGb250L0FyaWFsLUJvbGRNVC9FbmNvZGluZy9XaW5BbnNpRW5jb2RpbmcvRm9udERlc2NyaXB0b3IgMTMgMCBSL0ZpcnN0Q2hhciAzMi9MYXN0Q2hhciAzMi9XaWR0aHMgMjYgMCBSPj4NCmVuZG9iag0KMTMgMCBvYmoNCjw8L1R5cGUvRm9udERlc2NyaXB0b3IvRm9udE5hbWUvQXJpYWwtQm9sZE1UL0ZsYWdzIDMyL0l0YWxp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YnR5cGUvWE1ML0xlbmd0aCAzMDkxPj4NCnN0cmVhbQ0KPD94cGFja2V0IGJlZ2luPSLvu78iIGlkPSJXNU0wTXBDZWhpSHpyZVN6TlRjemtjOWQiPz48eDp4bXBtZXRhIHhtbG5zOng9ImFkb2JlOm5zOm1ldGEvIiB4OnhtcHRrPSIzLjEtNzAxIj4KPHJkZjpSREYgeG1sbnM6cmRmPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5LzAyLzIyLXJkZi1zeW50YXgtbnMjIj4KPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgIHhtbG5zOnBkZj0iaHR0cDovL25zLmFkb2JlLmNvbS9wZGYvMS4zLyI+CjxwZGY6UHJvZHVjZXI+TWljcm9zb2Z0wq4gV29yZCBwYXJhIE1pY3Jvc29mdCAzNjU8L3BkZjpQcm9kdWNlcj48L3JkZjpEZXNjcmlwdGlvbj4KPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgIHhtbG5zOmRjPSJodHRwOi8vcHVybC5vcmcvZGMvZWxlbWVudHMvMS4xLyI+CjxkYzpjcmVhdG9yPjxyZGY6U2VxPjxyZGY6bGk+RGFuaWVsIEdsem10ejwvcmRmOmxpPjwvcmRmOlNlcT48L2RjOmNyZWF0b3I+PC9yZGY6RGVzY3JpcHRpb24+CjxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PSIiICB4bWxuczp4bXA9Imh0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC8iPgo8eG1wOkNyZWF0b3JUb29sPk1pY3Jvc29mdMKuIFdvcmQgcGFyYSBNaWNyb3NvZnQgMzY1PC94bXA6Q3JlYXRvclRvb2w+PHhtcDpDcmVhdGVEYXRlPjIwMjQtMTItMTBUMjM6MDU6MDQtMDY6MDA8L3htcDpDcmVhdGVEYXRlPjx4bXA6TW9kaWZ5RGF0ZT4yMDI0LTEyLTEwVDIzOjA1OjA0LTA2OjAwPC94bXA6TW9kaWZ5RGF0ZT48L3JkZjpEZXNjcmlwdGlvbj4KPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9IiIgIHhtbG5zOnhtcE1NPSJodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvbW0vIj4KPHhtcE1NOkRvY3VtZW50SUQ+dXVpZDpEMzdCNkQyMy1COUEyLTQ1RkItQTFBMi04MThFNTRENjM1MzQ8L3htcE1NOkRvY3VtZW50SUQ+PHhtcE1NOkluc3RhbmNlSUQ+dXVpZDpEMzdCNkQyMy1COUEyLTQ1RkItQTFBMi04MThFNTRENjM1MzQ8L3htcE1NOkluc3RhbmNlSUQ+PC9yZGY6RGVzY3JpcHRpb24+CiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAo8L3JkZjpSREY+PC94OnhtcG1ldGE+PD94cGFja2V0IGVuZD0idyI/Pg0KZW5kc3RyZWFtDQplbmRvYmoNCjI4IDAgb2JqDQo8PC9EaXNwbGF5RG9jVGl0bGUgdHJ1ZT4+DQplbmRvYmoNCjI5IDAgb2JqDQo8PC9UeXBlL1hSZWYvU2l6ZSAyOS9XWyAxIDQgMl0gL1Jvb3QgMSAwIFIvSW5mbyAxNCAwIFIvSURbPDIzNkQ3QkQzQTJCOUZCNDVBMUEyODE4RTU0RDYzNTM0PjwyMzZEN0JEM0EyQjlGQjQ1QTFBMjgxOEU1NEQ2MzUzND5dIC9GaWx0ZXIvRmxhdGVEZWNvZGUvTGVuZ3RoIDEwOT4+DQpzdHJlYW0NCnicLcw9DkBQEATgef5DpVE5iGM4hUah4wCOotQo9RKtSHR6vRPwzNhiv2SzM4Cd5zF2p8DHIE5ibuIWYhYH8Rqhoz+JiwS5WEg4Ao7tzOAIV3jCF0b8n4HNRSvjcUWSjbTq7EpRk34HXj7cEdkNCmVuZHN0cmVhbQ0KZW5kb2JqDQp4cmVmDQowIDMwDQowMDAwMDAwMDE1IDY1NTM1IGYNCjAwMDAwMDAwMTcgMDAwMDAgbg0KMDAwMDAwMDE2MyAwMDAwMCBuDQowMDAwMDAwMjE5IDAwMDAwIG4NCjAwMDAwMDA0OTcgMDAwMDAgbg0KMDAwMDAwMDgyNCAwMDAwMCBuDQowMDAwMDAwOTUyIDAwMDAwIG4NCjAwMDAwMDA5ODAgMDAwMDAgbg0KMDAwMDAwMTEzNSAwMDAwMCBuDQowMDAwMDAxMjA4IDAwMDAwIG4NCjAwMDAwMDE0NTYgMDAwMDAgbg0KMDAwMDAwMTUxMCAwMDAwMCBuDQowMDAwMDAxNTY0IDAwMDAwIG4NCjAwMDAwMDE3MzEgMDAwMDAgbg0KMDAwMDAwMTk2MyAwMDAwMCBuDQowMDAwMDAwMDE2IDY1NTM1IGYNCjAwMDAwMDAwMTcgNjU1MzUgZg0KMDAwMDAwMDAxOCA2NTUzNSBmDQowMDAwMDAwMDE5IDY1NTM1IGYNCjAwMDAwMDAwMjAgNjU1MzUgZg0KMDAwMDAwMDAyMSA2NTUzNSBmDQowMDAwMDAwMDIyIDY1NTM1IGYNCjAwMDAwMDAwMDAgNjU1MzUgZg0KMDAwMDAwMjY2MiAwMDAwMCBuDQowMDAwMDAzMDIzIDAwMDAwIG4NCjAwMDAwMzA0MDMgMDAwMDAgbg0KMDAwMDAzMDU0NCAwMDAwMCBuDQowMDAwMDMwNTcxIDAwMDAwIG4NCjAwMDAwMzM3NDUgMDAwMDAgbg0KMDAwMDAzMzc5MCAwMDAwMCBuDQp0cmFpbGVyDQo8PC9TaXplIDMwL1Jvb3QgMSAwIFIvSW5mbyAxNCAwIFIvSURbPDIzNkQ3QkQzQTJCOUZCNDVBMUEyODE4RTU0RDYzNTM0PjwyMzZEN0JEM0EyQjlGQjQ1QTFBMjgxOEU1NEQ2MzUzND5dID4+DQpzdGFydHhyZWYNCjM0MTAwDQolJUVPRg0KeHJlZg0KMCAwDQp0cmFpbGVyDQo8PC9TaXplIDMwL1Jvb3QgMSAwIFIvSW5mbyAxNCAwIFIvSURbPDIzNkQ3QkQzQTJCOUZCNDVBMUEyODE4RTU0RDYzNTM0PjwyMzZEN0JEM0EyQjlGQjQ1QTFBMjgxOEU1NEQ2MzUzND5dIC9QcmV2IDM0MTAwL1hSZWZTdG0gMzM3OTA+Pg0Kc3RhcnR4cmVmDQozNDg1Nw0KJSVFT0Y=";
            byte[] bindoc = Convert.FromBase64String(base64);
            #endregion
            #region reinicio de las llaves
            /*
DBCC CHECKIDENT (AreasTests, RESEED, 0)
DBCC CHECKIDENT (carBecas, RESEED, 0)
DBCC CHECKIDENT (carrerasDeseadas, RESEED, 0)
DBCC CHECKIDENT (carrerasImpartadas, RESEED, 0)
DBCC CHECKIDENT (carreraTecnicas, RESEED, 0)
DBCC CHECKIDENT (catalogoCarrerasT, RESEED, 0)
DBCC CHECKIDENT (catalogoDeMapasCuarriculares, RESEED, 0)
DBCC CHECKIDENT (catAreasCarrera, RESEED, 0)
DBCC CHECKIDENT (catCarreras, RESEED, 0)
DBCC CHECKIDENT (contactos, RESEED, 0)
DBCC CHECKIDENT (empresaAsociadas, RESEED, 0)
DBCC CHECKIDENT (informaciones, RESEED, 0)
DBCC CHECKIDENT (preguntasDelTestVocacional, RESEED, 0)
DBCC CHECKIDENT (valorPreguntas, RESEED, 0)
*/
            #endregion
            #region storeprocedure
            string procedure = @"
USE [TUNIWEB]
GO
/****** Object:  StoredProcedure [dbo].[Realizarcalculodeltest]    Script Date: 13/06/2020 02:16:00 p. m. ******/
            SET ANSI_NULLS ON
            GO
SET QUOTED_IDENTIFIER ON
GO

---=======================================
--Author:		Daniel Gonzalez Martinez
-- Create date: 25 / 05 / 2020
-- Description: select* from alumnosUsuarios
--select* from carrerasDeseadas
--select* from Relaciones
--delete from alumnosUsuarios
--  Este store procedure realiza la accion de buscar el test del alumno que se necesita
-- =============================================
CREATE PROCEDURE Realizarcalculodeltest
@idalumno uniqueidentifier
AS
BEGIN
/**********/
declare @query table(i int)
/**********************/
            /****Selecciona las areas que salio mejor calculado de la base de datos *****/
insert into @query select Top 3 areasTestID as total from valorPreguntas where idAlumno = @idalumno group by areasTestID order by Sum(valor) desc
 /***************Inserta en carreras deseadas las carreras que salio mejor **************************/
 insert into carrerasDeseadas select @idalumno, c.idCarrera from @query q inner join catCarreras c on q.i = c.areasTestId order by q.i
 /*********Realiza la relacion con las carreras deseadas**************/
 insert into Relaciones select distinct carde.idAlumno, cari.usuarioUniversidad from carrerasDeseadas carde inner join carrerasImpartadas cari on carde.idCarrera = cari.catCarrerasId where idAlumno = @idalumno
END

USE [TUNIWEB]
GO
/****** Object:  StoredProcedure [dbo].[Realizarlarelacion]    Script Date: 13/06/2020 02:49:43 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE Realizarlarelacion
@idalumno uniqueidentifier
AS
BEGIN
declare @query table(idA uniqueidentifier , idu uniqueidentifier )
declare @query2 table(idA uniqueidentifier , idu uniqueidentifier )
insert into @query  select distinct car.idAlumno, ci.usuarioUniversidad from carrerasDeseadas car join carrerasImpartadas ci on car.idCarrera = ci.catCarrerasId where car.idAlumno = @idalumno
if((select COUNT(re.idAlumno) from @query cu , rechazos re where (cu.ida = re.idAlumno) and (cu.idu = re.idUniversidad) ) > 0)
Begin
/*******/
insert into @query2 select cu.ida, cu.idu from @query cu , rechazos re where (cu.ida != re.idAlumno) and (cu.idu != re.idUniversidad)
/*******/
END
else IF((select COUNT(ac.idalumno) from @query cu , aceptados ac where (cu.ida = ac.idAlumno) and (cu.idu = ac.iduniversidad) )>0)
Begin
/*********/
insert into @query2 select cu.ida, cu.idu from @query cu , aceptados ac where (cu.ida != ac.idAlumno) and (cu.idu != ac.iduniversidad)
/******/
end
else IF((select COUNT(cu.idA)from @query cu, solicitar sol where (cu.idA = sol.idAlumno) and (cu.idu = sol.idUniversidad))>0)
BEGIN
insert into @query2 select cu.idA, cu.idu from @query cu, solicitar sol where (cu.idA != sol.idAlumno) and (cu.idu != sol.idUniversidad)
end
/***Sino se cumple****/
else
begin
/**********/
insert into @query2 select* from @query 
/**********/
end
insert into Relaciones   select*from  @query2
return 1
END";

            #endregion
            Guid idnvo1 = Guid.Empty, udnvo2 = Guid.Empty;

            if (!bd.universidadesUsuario.Any())
            {
                var nvousu = new UsuarioUniversidad
                {
                    usuario = "IPN RULES",
                    contraseña = "E3dsadas",
                    relusu_carri = new List<carrerasimpartidas> {
                          new carrerasimpartidas{ catCarrerasId = 1},
                          new carrerasimpartidas{ catCarrerasId = 2 },
                          new carrerasimpartidas{ catCarrerasId = 3 }
                     },
                    relUSU_U = new universidad
                    {
                        nombre = "Universidad de prueba",
                        direccion = "CDMX",
                    }
                };
                bd.universidadesUsuario.Add(nvousu);
            }
            if (!bd.alumnosUsuarios.Any())
            {

                var nvousa = new List<UsuarioAlumno>{
                    new UsuarioAlumno
                {
                    usuario = "Daniel",
                    contraseña = "Ae129%dsa",
                    relUs_Al = new Alumno{
                        nombre ="Daniel",
                        apPaterno="Gonzalez",
                        apMaterno ="Martinez"
                    },
                        relDaac_Us  = new DatosAcademicos{
                            boletaGlobal="GG.pdf",
                            doc= bindoc
                        },
                    relUSA_VALP = new List<ValorPregunta>
                     {
                         new ValorPregunta{ idPregunta=1,areasTestID=1,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=2,areasTestID=2,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=3,areasTestID=3,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=4,areasTestID=4,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=5,areasTestID=5,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=6,areasTestID=6,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=7,areasTestID=7,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=8,areasTestID=8,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=9,areasTestID=9,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=10,areasTestID=10,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=11,areasTestID=1,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=12,areasTestID=2,valor= valores.Lo_detesto},
                         new ValorPregunta{ idPregunta=13,areasTestID=3,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=14,areasTestID=4,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=15,areasTestID=5,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=16,areasTestID=6,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=17,areasTestID=7,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=18,areasTestID=8,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=19,areasTestID=9,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=20,areasTestID=10,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=21,areasTestID=1,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=22,areasTestID=2,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=23,areasTestID=3,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=24,areasTestID=4,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=25,areasTestID=5,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=26,areasTestID=6,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=27,areasTestID=7,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=28,areasTestID=8,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=29,areasTestID=9,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=30,areasTestID=10,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=31,areasTestID=1,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=31,areasTestID=2,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=33,areasTestID=3,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=34,areasTestID=4,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=35,areasTestID=5,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=36,areasTestID=6,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=37,areasTestID=7,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=38,areasTestID=8,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=39,areasTestID=9,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=40,areasTestID=10,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=41,areasTestID=1,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=42,areasTestID=2,valor= valores.Lo_detesto},
                         new ValorPregunta{ idPregunta=43,areasTestID=3,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=44,areasTestID=4,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=45,areasTestID=5,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=46,areasTestID=6,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=47,areasTestID=7,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=48,areasTestID=8,valor= valores.Lo_detesto },
                         new ValorPregunta{ idPregunta=49,areasTestID=9,valor= valores.Me_encanta },
                         new ValorPregunta{ idPregunta=50,areasTestID=10,valor= valores.Lo_detesto },
                     }
                },
                    new UsuarioAlumno
                    {
                        usuario = "Pepillo pillo",
                        contraseña ="fasfasdf12F",
                        relUs_Al = new Alumno{
                        nombre ="Pepe",
                        apPaterno="Juarez",
                        apMaterno ="no me acuerdo"},
                        relAL_CARRD = new List<carrerasDeseadas>{
                            new carrerasDeseadas
                            {
                                idCarrera = 1
                            },
                            new carrerasDeseadas
                            {
                                idCarrera = 2
                            },
                            new carrerasDeseadas
                            {
                                idCarrera = 3
                            }
                        },
                        relDaac_Us  = new DatosAcademicos{
                            boletaGlobal="GG.pdf",
                            doc= bindoc
                        }
                    }
                };
                bd.alumnosUsuarios.AddRange(nvousa);
                idnvo1 = nvousa[0].idAlumno;
                udnvo2 = nvousa[1].idAlumno;
            }
            bd.SaveChanges();
            if (idnvo1 != Guid.Empty && udnvo2 != Guid.Empty)
            {
                bd.Database.ExecuteSqlCommand("exec Realizarcalculodeltest @idalumno = {0}", idnvo1);
                bd.Database.ExecuteSqlCommand("exec Realizarlarelacion @idalumno = {0}", udnvo2);
            }
        }
        #endregion
        #region Alumno
        public static List<resultados> resul(TUNIDbContext bd, string id)
        {
            using (SqlConnection conn = new SqlConnection(bd.Database.GetDbConnection().ConnectionString))
            {
                SqlCommand commando = new SqlCommand("select t.areaDelTest, SUM(v.valor)  as total from valorPreguntas v inner join AreasTests t on v.areasTestID = t.areaTestId  where idAlumno= @ID group by t.areaDelTest order by Sum(v.valor) desc", conn);
                commando.Parameters.Add("@ID", SqlDbType.UniqueIdentifier);
                commando.Parameters["@ID"].Value = Guid.Parse(id);
                try
                {
                    conn.Open();
                    SqlDataReader lector = commando.ExecuteReader();
                    List<resultados> resul = new List<resultados>();
                    while (lector.Read())
                    {
                        resul.Add(new resultados { area = lector[0].ToString(), total = Convert.ToInt32(lector[1]) });
                    }
                    conn.Close();
                    return resul;
                }
                catch (Exception ex)
                {
                    var xd = ex.Message;
                    return null;
                }
            }
        }
        #region resultados

        #endregion
        #region DatosAcademicos del alumno
        public static usuarioA retudet(string id, TUNIDbContext bd, IHostingEnvironment host)
        {
            var lista = (from d in bd.alumnosUsuarios
                         where (d.idAlumno == Guid.Parse(id))
                         join a in bd.alumnos
                         on d.idAlumno equals a.idAlumno
                         join ac in bd.datosAcademicos
                         on d.idAlumno equals ac.idAlumno
                         select new usuarioA
                         {
                             idA = d.idAlumno,
                             usuario = d.usuario,
                             nombre = a.nombre,
                             ap = a.apPaterno,
                             am = a.apMaterno,
                             arch = ac.boletaGlobal,
                             ruta = CrearArchivosDatA(host, id, ac.boletaGlobal, retornararchivodatac(bd, id))
                         }).AsEnumerable().FirstOrDefault();
            return lista;
        }
        public static byte[] retornararchivodatac(TUNIDbContext bd, string id)
        {
            var archivo = (from d in bd.alumnosUsuarios
                           where (d.idAlumno == Guid.Parse(id))
                           join datc in bd.datosAcademicos
                           on d.idAlumno equals datc.idAlumno
                           select datc.doc).FirstOrDefault();
            return archivo;
        }
        public static string CrearArchivosDatA(IHostingEnvironment hosting, string idA, string namearch, byte[] archivo)
        {
            var path = Path.Combine(hosting.WebRootPath + @"\files" + @"\" + idA + @"\datosAcademicos");
            var pathcompleto = path + @"\" + namearch;
            var pathbueno = @"/files" + @"/" + idA + @"/datosAcademicos" + @"/" + namearch;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if (!File.Exists(pathcompleto))
            {
                File.WriteAllBytes(pathcompleto, archivo);
                return pathbueno;
            }
            else
            {
                return pathbueno;
            }
        }
        #endregion
        #region reconocimientos del alumno
        public static List<archivosrecon> retarreglo(TUNIDbContext bd, string id)
        {
            var lista = (from d in bd.alumnosUsuarios
                         where (d.idAlumno == Guid.Parse(id))
                         join re in bd.informaciones
                         on d.idAlumno equals re.idAlumno
                         select new archivosrecon
                         {
                             nombre = re.reconocimiento,
                             archivo = re.doc
                         }).ToList();
            return lista;
        }
        public static List<recon> retornarrutas(string idA, IHostingEnvironment host, List<archivosrecon> arrr)
        {
            if (!(arrr.Count < 1))
            {
                List<recon> rutas = new List<recon>();
                var parh = Path.Combine(host.WebRootPath + @"\files" + @"\" + idA + @"\reconocimientos");
                if (!Directory.Exists(parh))
                {
                    Directory.CreateDirectory(parh);
                }
                for (int i = 0; i < arrr.Count(); i++)
                {
                    var recono = new recon();
                    var nombreyextencion = retornarnombreyextencion(arrr[i].nombre);
                    var pathcompleto = parh + @"\" + nombreyextencion.Item1 + "(" + i + ")" + nombreyextencion.Item2;
                    File.WriteAllBytes(pathcompleto, arrr[i].archivo);
                    var pathbueno = @"/files" + @"/" + idA + @"/reconocimientos" + @"/" + nombreyextencion.Item1 + "(" + i + ")" + nombreyextencion.Item2;
                    recono.ruta = pathbueno;
                    recono.namerecon = nombreyextencion.Item1 + "(" + i + ")" + nombreyextencion.Item2;
                    rutas.Add(recono);
                }
                return rutas;
            }
            return null;
        }
        #region complementos
        public static Tuple<string, string> retornarnombreyextencion(string name)
        {
            Tuple<string, string> tupla = null;
            for (int i = 0; i < name.Length; i++)
            {
                switch (name[i])
                {
                    case '.':
                        {
                            tupla = new Tuple<string, string>(item1: name.Substring(0, i), item2: name.Substring(i, 4));
                            return tupla;
                        }
                }
            }
            return tupla;
        }
        #endregion
        #endregion
        #region carrerastecnicas
        public static List<carrerastecnicas> returncarrt(string id, TUNIDbContext bd)
        {
            var lista = (from d in bd.alumnosUsuarios
                         where (d.idAlumno == Guid.Parse(id))
                         join cart in bd.carreraTecnicas on d.idAlumno
                         equals cart.idAlumno
                         join catt in bd.catalogoCarrerasT on cart.catalogoCarrerasTecnicasId
                         equals catt.carreTecnicaId
                         select new carrerastecnicas
                         {
                             carreratecsel = catt.carreraTecnica
                         }).ToList();
            if (lista.Count() == 0)
            {
                return null;
            }
            else
            {
                return lista;
            }
        }
        #endregion
        #region carrerasdeseada
        public static List<carrerasdeseadas> retucarrd(TUNIDbContext bd, string id)
        {

            var lista = (from d in bd.alumnosUsuarios
                         where (d.idAlumno == Guid.Parse(id))
                         join card in bd.carrerasDeseadas
                         on d.idAlumno equals card.idAlumno
                         join cat in bd.catCarreras
                         on card.idCarrera equals cat.idCarrera
                         select new carrerasdeseadas
                         {
                             idare = cat.areasCarreraId,
                             carrera = cat.Carrera
                         }).ToList();
            if (lista.Count() == 0)
            {
                return null;
            }
            else
            {
                return lista;
            }
        }
        #region compomentes
        public static List<areascarrera> returnareas(TUNIDbContext bd, string id)
        {
            var lista = (from d in bd.alumnosUsuarios
                         where (d.idAlumno == Guid.Parse(id))
                         join card in bd.carrerasDeseadas on
                         d.idAlumno equals card.idAlumno
                         join cat in bd.catCarreras
                         on card.idCarrera equals cat.idCarrera
                         join cata in bd.catAreasCarrera
                         on cat.areasCarreraId equals cata.idArea
                         orderby cata.idArea
                         select new areascarrera
                         {
                             idarea = cata.idArea,
                             area = cata.area
                         }).Distinct().ToList();
            if (lista.Count() == 0)
            {
                return null;
            }
            else
            {

                return lista;
            }
        }
        #endregion
        #endregion
        #region TestVocacional
        public static int returncont(TUNIDbContext bd, string id)
        {
            var list = (from d in bd.valorPreguntas
                        where (d.idAlumno == Guid.Parse(id))
                        join pre in bd.preguntasDelTestVocacional
                        on d.idPregunta equals pre.PregunataId
                        join ar in bd.AreasTests
                        on d.areasTestID equals ar.areaTestId
                        orderby d.idPregunta
                        select new TestVocacional
                        {
                            nodepreguta = pre.PregunataId,
                            pregunta = pre.pregunta,
                            area = ar.areaDelTest,
                            valores = d.valor
                        }).ToList();
            return list.Count();
        }
        public static string returntest(TUNIDbContext bd, string id, IHostingEnvironment host)
        {
            var list = (from d in bd.valorPreguntas
                        where (d.idAlumno == Guid.Parse(id))
                        join pre in bd.preguntasDelTestVocacional
                        on d.idPregunta equals pre.PregunataId
                        join ar in bd.AreasTests
                        on d.areasTestID equals ar.areaTestId
                        orderby d.idPregunta
                        select new TestVocacional
                        {
                            nodepreguta = pre.PregunataId,
                            pregunta = pre.pregunta,
                            area = ar.areaDelTest,
                            valores = d.valor
                        }).ToList();
            var dat = returndatat(list);
            ExcelPackage exepg = new ExcelPackage();
            ExcelWorksheet wssheet1 = exepg.Workbook.Worksheets.Add("Paguina 1");
            wssheet1.Cells["A1"].LoadFromDataTable(dat, true);
            wssheet1.Protection.IsProtected = false;
            wssheet1.Protection.AllowSelectLockedCells = false;
            wssheet1.Cells[wssheet1.Dimension.Address].AutoFitColumns();
            string path = host.WebRootPath + @"\files" + @"\" + id + @"\testvocacional";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            for (int i = 0; i < wssheet1.Dimension.Columns; i++)
            {
                wssheet1.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                wssheet1.Cells[1, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                wssheet1.Cells[1, i + 1].Style.Font.Name = "Arial Black";
                wssheet1.Cells[1, i + 1].Style.Font.Color.SetColor(Color.White);
                wssheet1.Cells[1, i + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                wssheet1.Cells[1, i + 1].Style.Border.Top.Style = ExcelBorderStyle.Double;
                wssheet1.Cells[1, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Double;
                wssheet1.Cells[1, i + 1].Style.Border.Right.Style = ExcelBorderStyle.Double;
                wssheet1.Cells[1, i + 1].Style.Border.Bottom.Color.SetColor(Color.White);
                wssheet1.Cells[1, i + 1].Style.Border.Top.Color.SetColor(Color.White);
                wssheet1.Cells[1, i + 1].Style.Border.Left.Color.SetColor(Color.White);
                wssheet1.Cells[1, i + 1].Style.Border.Right.Color.SetColor(Color.White);
                wssheet1.Cells[1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                wssheet1.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#5ad7b4"));
                for (int j = 1; j < wssheet1.Dimension.Rows; j++)
                {
                    wssheet1.Cells[j + 1, i + 1].Style.Font.Name = "Arial";
                    wssheet1.Cells[j + 1, i + 1].Style.Font.Size = 12;
                    wssheet1.Cells[j + 1, i + 1].Style.Font.Color.SetColor(Color.Black);
                    wssheet1.Cells[j + 1, i + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    wssheet1.Cells[j + 1, i + 1].Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml("#bde2e9"));

                    if (i == 0)
                    {
                        wssheet1.Cells[j + 1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }
                    if (i == 1)
                    {
                        wssheet1.Cells[j + 1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    if (i == 3)
                    {
                        wssheet1.Cells[j + 1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    wssheet1.Cells[j + 1, i + 1].Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                    wssheet1.Cells[j + 1, i + 1].Style.Border.Top.Style = ExcelBorderStyle.Double;
                    wssheet1.Cells[j + 1, i + 1].Style.Border.Left.Style = ExcelBorderStyle.Double;
                    wssheet1.Cells[j + 1, i + 1].Style.Border.Right.Style = ExcelBorderStyle.Double;
                    wssheet1.Cells[j + 1, i + 1].Style.Border.Bottom.Color.SetColor(Color.White);
                    wssheet1.Cells[j + 1, i + 1].Style.Border.Top.Color.SetColor(Color.White);
                    wssheet1.Cells[j + 1, i + 1].Style.Border.Left.Color.SetColor(Color.White);
                    wssheet1.Cells[j + 1, i + 1].Style.Border.Right.Color.SetColor(Color.White);
                }

            }
            exepg.SaveAs(
                new FileInfo(path + @"\testvocacional.xlsx")
                );
            return @"/files" + @"/" + id + @"/testvocacional" + @"/testvocacional.xlsx";
        }
        #region datatable
        public static DataTable returndatat(List<TestVocacional> lista)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("No de Pregunta", typeof(int));
            dt.Columns.Add("Pregunta", typeof(string));
            dt.Columns.Add("Area", typeof(string));
            dt.Columns.Add("Valor", typeof(string));
            foreach (var datos in lista)
            {
                dt.Rows.Add(datos.nodepreguta, datos.pregunta, datos.area, datos.valores.ToString().Replace("_", " "));
            }
            return dt;
        }
        #endregion
        #endregion
        #region solicitudes
        public static int solicitud(string idA, string idU, TUNIDbContext bd)
        {
            var enviados = (from d in bd.solicitar
                            where (d.idAlumno == Guid.Parse(idA)
                            && d.idUniversidad == Guid.Parse(idU))
                            select new solicitudes
                            {
                                idAlumno = d.idAlumno,
                                idUniversidad = d.idUniversidad
                            }).ToList();
            if (enviados.Count() < 1)
            {

                bd.solicitar.Add(new solicitudes { idAlumno = Guid.Parse(idA), idUniversidad = Guid.Parse(idU) });
                int o = bd.SaveChanges();
                if (o == 1)
                {
                    return o;
                }
                else
                {
                    return o;
                }

            }
            else
            {
                return 3;
            }
        }
        #endregion
        #region relaciones
        public static List<relaciones> rel(string id, TUNIDbContext _bd)
        {
            var list = (from d in _bd.alumnosUsuarios
                        where (d.idAlumno == Guid.Parse(id))
                        join ree in _bd.Relaciones on d.idAlumno equals ree.idAlumno
                        join un in _bd.universidades on ree.idUniversidad equals un.idUnversidad
                        select new relaciones
                        {
                            idUn = un.idUnversidad,
                            universidad = un.nombre
                        }).ToList();

            if (list.Count() == 0)
            {
                return null;
            }
            else
            {
                return list;
            }
        }
        public static void trye(string id, TUNIDbContext _bd)
        {
            var objt = (from d in _bd.alumnosUsuarios
                        where (d.idAlumno == Guid.Parse(id))
                        join recal in _bd.Relaciones on d.idAlumno equals recal.idAlumno
                        select new Relacion
                        {
                            nodeRelacion = recal.nodeRelacion,
                            idAlumno = d.idAlumno,
                            idUniversidad = recal.idUniversidad
                        }).ToList();
            if (objt.Count() > 0)
            {
                _bd.Relaciones.RemoveRange(objt);
                _bd.SaveChanges();
            }
            _bd.Database.ExecuteSqlCommand("exec Realizarlarelacion @idalumno = {0}", id);
        }
        #endregion
        #endregion
        #region Universidad
        #region usuariouni
        public static usuariouniversidad usuarioU(TUNIDbContext bd, string id)
        {
            var list = (from d in bd.universidadesUsuario
                        where (d.idUniversidad == Guid.Parse(id))
                        select new usuariouniversidad
                        {
                            idusuario = d.idUniversidad,
                            usuario = d.usuario
                        }).FirstOrDefault();
            return list;
        }
        public static uni Uni(TUNIDbContext bd, string id)
        {
            var list = (from d in bd.universidadesUsuario
                        where (d.idUniversidad == Guid.Parse(id))
                        join u in bd.universidades
                        on d.idUniversidad equals u.idUnversidad
                        select new uni
                        {
                            nombredelaInst = u.nombre,
                            direccion = u.direccion
                        }).FirstOrDefault();
            return list;
        }
        #endregion
        #region  carreras impartidas
        public static List<carrerasi> retucarrimp(TUNIDbContext bd, string id)
        {
            var list = (from d in bd.universidadesUsuario
                        where (d.idUniversidad == Guid.Parse(id))
                        join cari in bd.carrerasImpartadas
                        on d.idUniversidad equals cari.usuarioUniversidad
                        join catca in bd.catCarreras
                        on cari.catCarrerasId equals catca.idCarrera
                        select new carrerasi
                        {
                            idarea = catca.areasCarreraId,
                            carrera = catca.Carrera
                        }).ToList();
            return list;
        }
        #region compoment
        public static List<areascarrera> returai(TUNIDbContext bd, string id)
        {
            var list = (from d in bd.universidadesUsuario
                        where (d.idUniversidad == Guid.Parse(id))
                        join cari in bd.carrerasImpartadas
                        on d.idUniversidad equals cari.usuarioUniversidad
                        join catca in bd.catCarreras
                        on cari.catCarrerasId equals catca.idCarrera
                        join ari in bd.catAreasCarrera
                        on catca.areasCarreraId equals ari.idArea
                        select new areascarrera
                        {
                            idarea = ari.idArea,
                            area = ari.area
                        }).Distinct().ToList();
            return list;
        }
        #endregion
        #endregion
        #region ingreso
        public static ingre retuingre(IHostingEnvironment host, string id, TUNIDbContext bd)
        {
            var list = (from d in bd.universidadesUsuario
                        where (d.idUniversidad == Guid.Parse(id))
                        join ing in bd.ingresos
                        on d.idUniversidad equals ing.idUniversidad
                        select new ingre
                        {
                            name = ing.metodoIngreso,
                            rut = retunrut(host, ing.doc, id, ing.metodoIngreso)
                        }).FirstOrDefault();
            return list;
        }
        public static string retunrut(IHostingEnvironment host, byte[] arch, string id, string name)
        {
            var path = host.WebRootPath + @"\files\" + id + @"\metodo de ingreso";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var pathbueno = path + @"\" + name;
            if (!File.Exists(pathbueno))
            {
                File.WriteAllBytes(pathbueno, arch);
            }
            var pathbuenobueno = @"/files/" + id + @"/metodo de ingreso/" + name;
            return pathbuenobueno;
        }
        #endregion
        #region egre
        public static egre returnegre(IHostingEnvironment host, string id, TUNIDbContext bd)
        {
            var list = (from d in bd.universidadesUsuario
                        where (d.idUniversidad == Guid.Parse(id))
                        join egr in bd.egresos
                        on d.idUniversidad equals egr.idUniversidad
                        select new egre
                        {
                            name = egr.nivelEgreso,
                            rut = returnrute(host, id, egr.nivelEgreso, egr.doc)
                        }).FirstOrDefault();
            return list;
        }
        public static string returnrute(IHostingEnvironment host, string id, string name, byte[] arch)
        {
            var pth = host.WebRootPath + @"\files\" + id + @"\egreso";
            if (!Directory.Exists(pth))
            {
                Directory.CreateDirectory(pth);
            }
            var pthbueno = pth + @"\" + name;
            var pthbuenobueno = @"/files/" + id + @"/egreso/" + name;
            if (!File.Exists(pthbueno))
            {
                File.WriteAllBytes(pthbueno, arch);
                return pthbuenobueno;
            }
            return pthbuenobueno;
        }
        #endregion
        #region contacto
        public static List<contacto> obtenersol(string id, TUNIDbContext bd)
        {
            var lista = (from d in bd.universidadesUsuario
                         where (d.idUniversidad == Guid.Parse(id))
                         join sol in bd.solicitar
                         on d.idUniversidad equals sol.idUniversidad
                         join al in bd.alumnos
                         on sol.idAlumno equals al.idAlumno
                         select new contacto
                         {
                             id = sol.idAlumno,
                             nombre = al.nombre
                         }).ToList();
            if (lista.Count() < 1)
            {
                return null;
            }
            return lista;
        }
        public static solicitudes sol(TUNIDbContext bd, string id)
        {
            var objt = bd.solicitar.Where(d => d.idAlumno == Guid.Parse(id) && d.idUniversidad == Operaciones.IdSing).FirstOrDefault();
            return objt;
        }
        #endregion
        #endregion
    }
}

