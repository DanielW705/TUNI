using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.JsonPatch.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TUNIWEB.ClassValidation;
using TUNIWEB.Models;

namespace TUNIWEB.Controllers
{
    public class PrincipalController : Controller
    {
        private readonly BB _bd;
        private readonly IHostingEnvironment _host;

        public PrincipalController(BB bd, IHostingEnvironment host)
        {
            _bd = bd;
            _host = host;
        }
        [Authorize(Roles = "Administrador")]
        public IActionResult IndexAdministrador()
        {
            /* ViewBag.id = User.Claims.Where(d=>d.Type == ClaimTypes.NameIdentifier).Select(d => d.Value).FirstOrDefault();*/
            var lista = new Tuple<IEnumerable<UsuarioAlumno>, IEnumerable<UsuarioUniversidad>>
                (item1: _bd.alumnosUsuarios, item2: _bd.universidadesUsuario);
            return View(lista);
        }
        [Authorize(Roles = "Alumno")]
        public IActionResult IndexAlumno()
        {
            Operaciones.trye(Operaciones.IdSing.ToString(), _bd);
            TRITUPLE trutupla = new TRITUPLE
            {
                model1 = Operaciones.retudet(Operaciones.IdSing.ToString(), _bd, _host),
                model2 = Operaciones.retornarrutas(Operaciones.IdSing.ToString(), _host, Operaciones.retarreglo(_bd, Operaciones.IdSing.ToString())),
                model3 = Operaciones.returncarrt(Operaciones.IdSing.ToString(), _bd),
                model4 = Operaciones.retucarrd(_bd, Operaciones.IdSing.ToString()),
                modelcom4 = Operaciones.returnareas(_bd, Operaciones.IdSing.ToString()),
                modelcom5 = Operaciones.returncont(_bd, Operaciones.IdSing.ToString()),
                model6 = Operaciones.rel(Operaciones.IdSing.ToString(), _bd)
            };
            return View(trutupla);
        }
        [Authorize(Roles = "Universidad")]
        public IActionResult IndexUniversidad()
        {
            return View();
        }
        [HttpPost]
        public IActionResult InfoA(string id)
        {
            TRITUPLE trutupla = new TRITUPLE
            {
                model1 = Operaciones.retudet(id, _bd, _host),
                model2 = Operaciones.retornarrutas(id, _host, Operaciones.retarreglo(_bd, id)),
                model3 = Operaciones.returncarrt(id, _bd),
                model4 = Operaciones.retucarrd(_bd, id),
                modelcom4 = Operaciones.returnareas(_bd, id),
                modelcom5 = Operaciones.returncont(_bd, id),
            };
            return PartialView("_InfoA", trutupla);
        }
        [HttpPost]
        public IActionResult testvoca(string id)
        {
            string rute = Operaciones.returntest(_bd, id, _host);
            return File(rute, "application/vnd.ms-excel", "Testvocacional.xlsx");
        }
        [HttpPost]
        public IActionResult InfoU(string id)
        {
            Trutupleu trutuple = new Trutupleu
            {
                model1 = Operaciones.usuarioU(_bd, id),
                model2 = Operaciones.Uni(_bd, id),
                model3 = Operaciones.retucarrimp(_bd, id),
                commodel3 = Operaciones.returai(_bd, id),
                model4 = Operaciones.retuingre(_host, id, _bd),
                model5 = Operaciones.returnegre(_host, id, _bd),
            };
            return PartialView("_InfoU", trutuple);
        }
        [HttpGet]
        public IActionResult cerrarsecion()
        {
            HttpContext.SignOutAsync();
            Operaciones.IdSing = Guid.Empty;
            return RedirectToAction("nvoLogin", "Log");
        }
        [HttpGet]
        public IActionResult _testvocacional()
        {
            Operaciones.matrizpreguntas(_bd);
            return PartialView(Operaciones.retutup());
        }
        [HttpPost]
        public IActionResult _testvocacional(valores i)
        {
            Operaciones.darvalores(i);
            if (Operaciones.retutup() == null)
            {
                var lista = Operaciones.realizarlaaccion(
                    Operaciones.retuval(),
                    Operaciones.IdSing.ToString());
                _bd.valorPreguntas.AddRange(lista);
                _bd.SaveChanges();
                _bd.Database.ExecuteSqlCommand("exec Realizarcalculodeltest @idalumno = {0}", Operaciones.IdSing);
            }
            return PartialView(Operaciones.retutup());
        }
        [HttpGet]
        public IActionResult _vistaUNI(string id)
        {
            Trutupleu trutuple = new Trutupleu
            {
                model1 = Operaciones.usuarioU(_bd, id),
                model2 = Operaciones.Uni(_bd, id),
                model3 = Operaciones.retucarrimp(_bd, id),
                commodel3 = Operaciones.returai(_bd, id),
                model4 = Operaciones.retuingre(_host, id, _bd),
                model5 = Operaciones.returnegre(_host, id, _bd),
            };
            return PartialView("_vistaUNI", trutuple);
        }
        [HttpPost]
        public IActionResult realizarlasolicitud(string id)
        {
            int va = Operaciones.solicitud(Operaciones.IdSing.ToString(), id, _bd);
            if (va == 3)
            {
                return Json(3);
            }
            if (va == 1)
            {
                return Json(1);
            }
            else if (va == 0)
            {
                return Json(0);
            }
            else
            {
                return Json(-1);
            }
        }
        [HttpGet]
        public IActionResult versolicitudes()
        {
            return Json(Operaciones.obtenersol(Operaciones.IdSing.ToString(), _bd));
        }
        [HttpPost]
        public IActionResult verAlumno(string id)
        {
            TRITUPLE trutupla = new TRITUPLE
            {
                model1 = Operaciones.retudet(id, _bd, _host),
                model2 = Operaciones.retornarrutas(id, _host, Operaciones.retarreglo(_bd, id)),
                model3 = Operaciones.returncarrt(id, _bd),
                model4 = Operaciones.retucarrd(_bd, id),
                modelcom4 = Operaciones.returnareas(_bd, id),
                modelcom5 = Operaciones.returncont(_bd, id),
            };
            return PartialView("_InfoA2", trutupla);
        }
        [HttpPost]
        public IActionResult rechazo(string id)
        {
            var jo = Operaciones.sol(_bd, id);
            if (jo != null)
            {
                _bd.solicitar.Remove(jo);
            }
            _bd.rechazos.Add(new historialrechazos
            {
                idAlumno = Guid.Parse(id),
                idUniversidad = Operaciones.IdSing
            });
            int i = _bd.SaveChanges();
            return Json(i);
        }
        [HttpPost]
        public IActionResult aceptado(string id)
        {
            var jo = Operaciones.sol(_bd,id);
            if (jo != null)
            {
                _bd.solicitar.Remove(jo);
            }
            _bd.aceptados.Add(new historialdeaceptados
            {
                idalumno = Guid.Parse(id),
                iduniversidad = Operaciones.IdSing
            });
            int i = _bd.SaveChanges();
            return Json(i);
        }
        [HttpPost]
        public IActionResult resultados()
        {
            return Json(Operaciones.resul(_bd, Operaciones.IdSing.ToString()));
        }
    }
}