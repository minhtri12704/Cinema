using Microsoft.AspNetCore.Mvc;

namespace CinemaProject.Controllers
{
    public class CinemaViewController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
