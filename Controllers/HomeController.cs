using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TUNIWEB.Models;

namespace TUNIWEB.Controllers
{
    
    public class HomeController : Controller
    {
        [Authorize (Roles = "Administrador")]
        public IActionResult Index()
        {
           
            var claims = User.Claims.ToList();
            var claim = User.Claims.Select(d=>d.Value);
            return View();
        }
        [Authorize (Roles ="Alumno")]
        public IActionResult About()
        {

            var claims = User.Claims.ToList();
            var claim = User.Claims.Select(d => d.Value);
            ViewData["Message"] = "Your application description page.";
            return View();
        }
        [Authorize(Roles = "Universidad")]
        public IActionResult Contact()
        {

            var claims = User.Claims.ToList();
            var claim = User.Claims.Select(d => d.Value);
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
