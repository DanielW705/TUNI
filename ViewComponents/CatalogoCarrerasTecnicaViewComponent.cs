using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TUNIWEB.Models;

namespace TUNIWEB.ViewComponents
{
    public class CatalogoCarrerasTecnicaViewComponent : ViewComponent
    {
        private readonly BB _bd;

        public CatalogoCarrerasTecnicaViewComponent(BB bd)
        {
            _bd = bd;
        }
        public IViewComponentResult Invoke()
        {
            var lista = _bd.catalogoCarrerasT;
            return View(lista);
        }
    }
}
