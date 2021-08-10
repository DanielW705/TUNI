using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TUNIWEB.ViewComponents
{
    public class SubirreconocimentoViewComponment : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
