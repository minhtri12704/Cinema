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

            var phong = lich.IdPhongNavigation;
            int soGhe = phong.SoLuongGhe;
            int soGheDoi = phong.SoLuongGheDoi;

            var danhSachGhe = new List<Ghe>();

            // Thêm ghế đôi (mỗi cái chiếm 2 chỗ)
            for (int i = 0; i < soGheDoi; i++)
            {
                danhSachGhe.Add(new Ghe
                {
                    IdGhe = $"D{i + 1}",
                    LoaiGhe = "Ghế đôi",
                    Gia = 145000
                });
            }

            // Thêm ghế đơn
            int soGheDon = soGhe - soGheDoi * 2;
            for (int i = 0; i < soGheDon; i++)
            {
                danhSachGhe.Add(new Ghe
                {
                    IdGhe = $"S{i + 1}",
                    LoaiGhe = "Ghế đơn",
                    Gia = 70000
                });
            }

            ViewBag.DanhSachGhe = danhSachGhe;
            return View("~/Views/CinemaView/DatGhe.cshtml", lich);
        }

    }
}
