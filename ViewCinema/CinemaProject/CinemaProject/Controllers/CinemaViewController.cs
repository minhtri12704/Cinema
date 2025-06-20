using CinemaProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaProject.Controllers
{
    public class CinemaViewController : Controller
    {
        private readonly RapChieuPhimContext _context;

        public CinemaViewController(RapChieuPhimContext context)
        {
            _context = context;
        }

        public IActionResult Home()
        {
            var dsPhim = _context.Phims.ToList();
            return View(dsPhim);
        }   
    }
}
