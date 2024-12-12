using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TUNIWEB.Models;

namespace TUNIWEB.ViewComponents
{
    public class AreasyCarrerasViewCompoment : ViewComponent
    {
        private readonly TUNIDbContext _bd;

        public AreasyCarrerasViewCompoment(TUNIDbContext bd)
        {
            _bd = bd;
        }
        public IViewComponentResult Invoke()
        {
            var lista = _bd.catAreasCarrera;
            return View(lista);
        }
    }
}
