using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ROP;
using TUNIWEB.ClassValidation;
using TUNIWEB.Models;
using TUNIWEB.Models.UserCase;
using TUNIWEB.Models.UsersCase;
using TUNIWEB.Models.ViewModel;

namespace TUNIWEB.Controllers
{
    public class LogController : Controller
    {
        private readonly TUNIDbContext _bd;

        private readonly LoginUserCase _loginUser;

        private readonly AddUserCase _addUser;

        private readonly UserAuthenticationCase _userAuthentication;

        private readonly AddDatosObligatoriosUserCase _addDatosObligatoriosUser;
        public LogController(TUNIDbContext bd, LoginUserCase loginUser, AddUserCase addUserCase, UserAuthenticationCase userAuthentication, AddDatosObligatoriosUserCase addDatosObligatoriosUser)
        {
            _bd = bd;
            _loginUser = loginUser;
            _addUser = addUserCase;
            _userAuthentication = userAuthentication;
            _addDatosObligatoriosUser = addDatosObligatoriosUser;
        }
        public IActionResult LogIn()
        {
            string route = _userAuthentication.AuthenticateUser();
            if (route is null)
                return View();
            else
                return RedirectToAction(route, "Principal");
        }
        [HttpPost]
        public async Task<IActionResult> Ingresar(RegisterLoginViewModel registerLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                Result<bool> isLogin = await _loginUser.Execute(registerLoginViewModel.LoginUserClass);
                if (isLogin.Success)
                    return RedirectToAction(
                        registerLoginViewModel.LoginUserClass.tipo_usuario == Models.Enums.UserRolEnum.Alumno ?
                        "IndexAlumno" : "IndexUniversidad", "Principal");
                else
                {
                    ModelState.AddModelError("User error", isLogin.Errors.First().ToString());
                    return View("LogIn", registerLoginViewModel);
                }
            }
            else
                return View("LogIn", registerLoginViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Registro(RegisterLoginViewModel registerLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                Result<bool> isAdded = await _addUser.Execute(registerLoginViewModel.RegisterUserClass);
                if (isAdded.Success)
                    return RedirectToAction(
                        registerLoginViewModel.RegisterUserClass.tipo_usuario == Models.Enums.UserRolEnum.Alumno ?
                        "MoreInfoAlumno" : "MoreInfoUniversidad", "Log");
                else
                {
                    ModelState.AddModelError("User error", isAdded.Errors.First().ToString());
                    return View("LogIn", registerLoginViewModel);
                }
            }
            else
                return View("LogIn", registerLoginViewModel);
        }
        public IActionResult MoreInfoAlumno()
        {
            return View();
        }
        public IActionResult MoreInfoUniversidad()
        {
            return View();
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
        public async Task<IActionResult> SubirAlumno(DatosObligatoriosViewModel data)
        {
            if (ModelState.IsValid)
            {
                Result<bool> isAdded = await _addDatosObligatoriosUser.Execute(data);
                if (isAdded.Success)
                    return RedirectToAction("IndexAlumno", "Principal");
                else
                {
                    ModelState.AddModelError("User error", isAdded.Errors.First().ToString());
                    return View("MoreInfoAlumno", data);
                }
            }
            else
                return View("MoreInfoAlumno", data);
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