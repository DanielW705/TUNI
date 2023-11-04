using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Newtonsoft.Json.Serialization;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading.Tasks;
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
        public static Guid existenciaA(Ingreclass ingre, BB bd)
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
        public static Guid existenciaU(Ingreclass ingre, BB bd)
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
        public static Guid existenciaADMON(Ingreclass ingre, BB bd)
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
        public static void matrizpreguntas(BB bd)
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

        public static void Setnvoalumno(DatosObligatorios value)
        {
            _nvoalumno = new Alumno { nombre = value.Nombre, apPaterno = value.Ap, apMaterno = value.Am };
        }


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
        public static void inicializar(BB bd)
        {
            #region bytes
            string path = System.Reflection.Assembly.GetEntryAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);
            var bindoc = File.ReadAllBytes(directory + @"\GG.pdf");
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
        public static List<resultados> resul(BB bd, string id)
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
        public static usuarioA retudet(string id, BB bd, IHostingEnvironment host)
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
        public static byte[] retornararchivodatac(BB bd, string id)
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
        public static List<archivosrecon> retarreglo(BB bd, string id)
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
        public static List<carrerastecnicas> returncarrt(string id, BB bd)
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
        public static List<carrerasdeseadas> retucarrd(BB bd, string id)
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
        public static List<areascarrera> returnareas(BB bd, string id)
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
        public static int returncont(BB bd, string id)
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
        public static string returntest(BB bd, string id, IHostingEnvironment host)
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
        public static int solicitud(string idA, string idU, BB bd)
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
        public static List<relaciones> rel(string id, BB _bd)
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
        public static void trye(string id, BB _bd)
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
        public static usuariouniversidad usuarioU(BB bd, string id)
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
        public static uni Uni(BB bd, string id)
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
        public static List<carrerasi> retucarrimp(BB bd, string id)
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
        public static List<areascarrera> returai(BB bd, string id)
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
        public static ingre retuingre(IHostingEnvironment host, string id, BB bd)
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
        public static egre returnegre(IHostingEnvironment host, string id, BB bd)
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
        public static List<contacto> obtenersol(string id, BB bd)
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
        public static solicitudes sol(BB bd, string id)
        {
            var objt = bd.solicitar.Where(d => d.idAlumno == Guid.Parse(id) && d.idUniversidad == Operaciones.IdSing).FirstOrDefault();
            return objt;
        }
        #endregion
        #endregion
    }
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
    public class TRITUPLE
    {
        public usuarioA model1 { get; set; }
        public List<recon> model2 { get; set; }
        public List<carrerastecnicas> model3 { get; set; }
        public List<carrerasdeseadas> model4 { get; set; }
        public List<areascarrera> modelcom4 { get; set; }
        public int modelcom5 { get; set; }
        public List<relaciones> model6 { get; set; }
    }
    public class Trutupleu
    {
        public usuariouniversidad model1 { get; set; }
        public uni model2 { get; set; }
        public List<carrerasi> model3 { get; set; }
        public List<areascarrera> commodel3 { get; set; }
        public ingre model4 { get; set; }
        public egre model5 { get; set; }
    }
}

