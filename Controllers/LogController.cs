using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators.Internal;
using TUNIWEB.ClassValidation;
using TUNIWEB.Models;

namespace TUNIWEB.Controllers
{
    public class LogController : Controller
    {
        private readonly GooglereCaptchaService _googlereCaptchaServie;
        private readonly BB _bd;


        public LogController(GooglereCaptchaService googlereCaptchaService, BB bd)
        {
            _googlereCaptchaServie = googlereCaptchaService;
            _bd = bd;
        }
        public IActionResult nvoLogIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Alumno"))
                {
                    return RedirectToAction("IndexAlumno", "Principal");
                }
                if (User.IsInRole("Universidad"))
                {
                    return RedirectToAction("IndexUniversidad", "Principal");
                }
                else
                {
                    return RedirectToAction("IndexAdministrador", "Principal");
                }
            }
            else
            {
                return View();
            }
        }
        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult registerA()
        {
            return View();
        }
        public IActionResult registerU()
        {
            return View();
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Iniciodelregistro(TupleClass nvouser, string tipo, string token)
        {
            var _googleCapthcha = await _googlereCaptchaServie.recVer(token);
            if (!_googleCapthcha.success && _googleCapthcha.score <= 0.5)
            {
                ModelState.AddModelError("", "Eres un robot");
                return View("nvoLogIn", nvouser);
            }
            if (ModelState.IsValid)
            {
                if (tipo == "A")
                {
                    Operaciones.Setnvousuario(nvouser.model1);
                    return RedirectToAction("registerA", "Log");
                }
                else if (tipo == "U")
                {
                    Operaciones.Set_nvousuarioUNI(nvouser.model1);
                    return RedirectToAction("registerU", "Log");
                }
            }
            return View("LogIn", nvouser);
        }
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsPaswordInuse(TupleClass pasword)
        {
            if (!(_bd.alumnosUsuarios.Where(d => d.contraseña == pasword.model1.pasword).Any()
                || _bd.universidadesUsuario.Where(d => d.contraseña == pasword.model1.pasword).Any()))
            {
                return Json(true);
            }
            else
            {
                return Json($"La contraseña ya esta en uso");
            }
        }
        public async Task<IActionResult> IsUserInuse(TupleClass userName)
        {
            if (!(_bd.alumnosUsuarios.Where(d => d.usuario == userName.model1.username).Any()
                || _bd.universidadesUsuario.Where(d => d.usuario == userName.model1.username).Any()))
            {
                return Json(true);
            }
            else
            {
                return Json($"El usuario {userName.model1.username} ya esta en uso");
            }
        }
        [HttpPost]
        public async Task<IActionResult> subirA(DatosObligatorios data)
        {
            if (ModelState.IsValid)
            {
                Operaciones.Setnvoalumno(data);
                Operaciones.SetdatosAcademicos(data);
                var nvoperfil = Operaciones.GetperfilA();
                await _bd.alumnosUsuarios.AddAsync(nvoperfil);
                Operaciones.IdSing = nvoperfil.idAlumno;
                _bd.SaveChanges();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Operaciones.Authentificarlo("Alumno"), new AuthenticationProperties { IsPersistent = false, ExpiresUtc = DateTime.Now.AddHours(1) });
                return RedirectToAction("IndexAlumno", "Principal");
            }
            else
            {
                return View("registerA", data);
            }
        }
        [HttpPost]
        public IActionResult subirrecon(reconocimiento rec)
        {
            if (ModelState.IsValid)
            {
                Operaciones.Set_informacion(rec);
                return RedirectToAction("registerA", "Log");
            }
            return RedirectToAction("registerA", "Log");

        }
        [HttpPost]
        public IActionResult prueba(IFormFile file)
        {

            var val = Operaciones.Set_informacion(file);
            if (val == string.Empty)
            {
                return Json(true);
            }
            else
            {
                return Json($"{val}");
            }
        }
        [HttpPost]
        public IActionResult prueba2(int i)
        {
            if (Operaciones.SetcarrerasTecnicas(i))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Ingresar(TupleClass ingre, string token, string tipo1)
        {
            var _googleCapthcha = await _googlereCaptchaServie.recVer(token);
            if (!_googleCapthcha.success && _googleCapthcha.score <= 0.5)
            {
                ModelState.AddModelError("", "Eres un robot");
                return View("nvoLogIn", ingre);
            }
            if (ModelState.IsValid)
            {
                Guid id = Guid.Empty;
                if (tipo1 == "A")
                {
                    if ((id = Operaciones.existenciaA(ingre.model2, _bd)) != Guid.Empty)
                    {
                        Operaciones.IdSing = id;
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Operaciones.Authentificarlo("Alumno"), new AuthenticationProperties { IsPersistent = false, ExpiresUtc = DateTime.Now.AddHours(1) });
                        return RedirectToAction("IndexAlumno", "Principal");
                    }
                    else if (((id = Operaciones.existenciaADMON(ingre.model2, _bd)) != Guid.Empty))
                    {
                        Operaciones.IdSing = id;
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Operaciones.Authentificarlo("Administrador"), new AuthenticationProperties { IsPersistent = false, ExpiresUtc = DateTime.Now.AddHours(1) });
                        return RedirectToAction("IndexAdministrador", "Principal");
                    }
                    else
                    {
                        ModelState.AddModelError("", "No existe este usuario");
                        return View("nvoLogIn", ingre);
                    }
                }
                else if (tipo1 == "U")
                {
                    if ((id = Operaciones.existenciaU(ingre.model2, _bd)) != Guid.Empty)
                    {
                        Operaciones.IdSing = id;
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Operaciones.Authentificarlo("Universidad"), new AuthenticationProperties { IsPersistent = false });
                        return RedirectToAction("IndexUniversidad", "Principal");
                    }
                    else if ((id = Operaciones.existenciaADMON(ingre.model2, _bd)) != Guid.Empty)
                    {
                        Operaciones.IdSing = id;
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Operaciones.Authentificarlo("Administrador"), new AuthenticationProperties { IsPersistent = false });
                        return RedirectToAction("IndexAdministrador", "Principal");
                    }
                    else
                    {
                        ModelState.AddModelError("", "No existe este usuario");
                        return View("nvoLogIn", ingre);
                    }
                }
            }

            return View("nvoLogIn", ingre);

        }
        [HttpGet]
        public IActionResult _vercarreras(int id)
        {
            var arreglo = _bd.catCarreras.Where(d => d.areasCarreraId == id);
            return Json(arreglo);
        }
        [HttpGet]
        public IActionResult subircarrera(int id)
        {
            if (Operaciones.SetcarrerasDeseadas(id))
            {
                return Json(true);
            }
            else
            {
                return Json(false);
            }
        }
        [HttpGet]
        public IActionResult mostrarcarreras2(int i)
        {
            var list = _bd.catCarreras.Where(d => d.areasCarreraId == i);
            return Json(list);
        }
        [HttpGet]
        public IActionResult validacion(validarcontactos con)
        {
            if (ModelState.IsValid)
            {
                return Json(ModelState.IsValid);
            }
            else
            {
                return Json(ModelState.Values.SelectMany(d => d.Errors));
            }
        }
        [HttpPost]
        public IActionResult subircontacto(string contacto)
        {
            if (Operaciones.Set_listacontactos(contacto))
            {
                return Json(true);
            }
            return Json(false);
        }
        [HttpPost]
        public async Task<IActionResult> subirdatosUniversidad(Universidadvalidation nvouni)
        {
            if (ModelState.IsValid)
            {
                Operaciones.Set_uniob(nvouni);
                var nvoperfil = Operaciones.GetperfilU();
                await _bd.universidadesUsuario.AddAsync(nvoperfil);
                Operaciones.IdSing = nvoperfil.idUniversidad;
                _bd.SaveChanges();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Operaciones.Authentificarlo("Universidad"), new AuthenticationProperties { IsPersistent = false });
                return RedirectToAction("IndexUniversidad", "Principal");
            }
            else
            {
                return View("registerU", nvouni);
            }
        }
        [HttpGet]
        public IActionResult validacionfinal()
        {
            if (!(Operaciones.Get_listacontactos().Count() < 1 || Operaciones.Get_carrerasImpartidas().Count() < 1))
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }
        [HttpPost]
        public IActionResult subircarrera2(int i)
        {
            if (Operaciones.Set_carrerasImpartidas(i))
            {
                return Json(true);
            }
            else
            {
                return Json(true);
            }
        }
    }
}