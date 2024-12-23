using Microsoft.AspNetCore.Mvc;

namespace TUNIWEB.ViewComponents
{
    public class SubirReconocimentoViewComponment : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
