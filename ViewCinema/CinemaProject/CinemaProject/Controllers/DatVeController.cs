using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CinemaProject.Models;

namespace CinemaProject.Controllers
{
    public class DatVeController : Controller
    {
        private readonly RapChieuPhimContext _context;

        public DatVeController(RapChieuPhimContext context)
        {
            _context = context;
        }

        // GET: /DatVe/DatGhe/idLich
        public IActionResult DatGhe(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var lich = _context.LichChieus
                .Include(l => l.IdPhimNavigation)
                .Include(l => l.IdPhongNavigation)
                    .ThenInclude(p => p.IdRapNavigation)
                .FirstOrDefault(l => l.IdLich == id);

            if (lich == null)
                return NotFound();

            return View("~/Views/CinemaView/DatGhe.cshtml", lich);
        }
    }
}
