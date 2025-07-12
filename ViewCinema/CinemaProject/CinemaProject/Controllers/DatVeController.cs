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

        public IActionResult DatGhe(string id)
        {
            var username = HttpContext.Session.GetString("username");
            HttpContext.Session.SetString("idLich", id);
            ViewBag.CurrentStep = 0; // ✅ Đánh dấu bước hiện tại: Chọn ghế

            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }
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
            int tongGhe = phong.SoLuongGhe;
            int soGheDoi = phong.SoLuongGheDoi;
            int soGheVip = phong.SoLuongGheVip;
            int soGheDon = tongGhe - soGheDoi * 2;

            var giaGheDon = _context.Ghes.FirstOrDefault(g => g.IdGhe == "GHE_DON");
            var giaGheDoi = _context.Ghes.FirstOrDefault(g => g.IdGhe == "GHE_DOI");
            var giaGheVip = _context.Ghes.FirstOrDefault(g => g.IdGhe == "GHE_VIP");
            var gheTrong = _context.Ghes.FirstOrDefault(g => g.IdGhe == "GHE_TRONG");

            int gheMoiHang = 13;
            int row = 0;
            int count = 0;
            var danhSachGhe = new List<dynamic>();

            var gheVipUuTien = new List<string>();
            for (char r = 'A'; r <= 'B'; r++)
            {
                for (int c = 5; c <= 9; c++)
                {
                    gheVipUuTien.Add($"{r}{c.ToString("D2")}");
                }
            }

            var gheVipThucTe = gheVipUuTien.Take(soGheVip).ToHashSet();

            for (int i = 0; i < soGheDon; i++)
            {
                int col = count % gheMoiHang + 1;
                if (col == 1 && i != 0) row++;

                string rowChar = ((char)('A' + row)).ToString();
                string colStr = col.ToString("D2");
                string maGhe = $"{rowChar}{colStr}";

                if (gheVipThucTe.Contains(maGhe))
                {
                    danhSachGhe.Add(new { Id = maGhe, IdGhe = giaGheVip.IdGhe, Gia = giaGheVip.Gia });
                }
                else
                {
                    danhSachGhe.Add(new { Id = maGhe, IdGhe = giaGheDon.IdGhe, Gia = giaGheDon.Gia });
                }

                count++;
            }

            int le = soGheDon % gheMoiHang;
            if (le != 0)
            {
                int gheAo = gheMoiHang - le;
                for (int i = 0; i < gheAo; i++)
                {
                    danhSachGhe.Add(new { Id = "", IdGhe = gheTrong.IdGhe, Gia = gheTrong.Gia });
                }
            }

            string hangDoi = ((char)('A' + row + 1)).ToString();
            for (int i = 1; i <= soGheDoi; i++)
            {
                string colStr = i.ToString("D2");
                danhSachGhe.Add(new { Id = $"{hangDoi}{colStr}", IdGhe = giaGheDoi.IdGhe, Gia = giaGheDoi.Gia });
            }

            var danhSachGheDaDat = _context.BookVes
                .Where(b => b.IdLich == id && b.TrangThai == "Đang giữ chỗ")
                .Select(b => b.GheNgoi)
                .ToList();

            var gheDaDat = danhSachGheDaDat
                .SelectMany(ghe => ghe.Split(',', StringSplitOptions.RemoveEmptyEntries))
                .Select(g => g.Trim())
                .Distinct()
                .ToList();

            ViewBag.GheDaDat = gheDaDat;
            ViewBag.DanhSachGhe = danhSachGhe;
            return View("~/Views/CinemaView/DatGhe.cshtml", lich);
        }

        [HttpPost]
        public IActionResult ChonBapNuoc(string DanhSachGhe, int TongTien, string idLich)
        {
            HttpContext.Session.SetString("DanhSachGhe", DanhSachGhe ?? "");
            HttpContext.Session.SetInt32("TongTienGhe", TongTien);
            HttpContext.Session.SetString("idLich", idLich);
            ViewBag.CurrentStep = 1; // ✅ Đánh dấu bước hiện tại: Bắp nước

            var gheList = !string.IsNullOrEmpty(DanhSachGhe)
                ? DanhSachGhe.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(g => g.Trim()).ToList()
                : new List<string>();
            var tongTienGhe = HttpContext.Session.GetInt32("TongTienGhe") ?? 0;

            var lich = _context.LichChieus
                .Include(l => l.IdPhimNavigation)
                .Include(l => l.IdPhongNavigation)
                    .ThenInclude(p => p.IdRapNavigation)
                .FirstOrDefault(l => l.IdLich == idLich);

            if (lich == null)
                return NotFound();

            var model = new ChonBapNuocViewModel
            {
                GheDaChon = gheList,
                TenPhim = lich.IdPhimNavigation?.TenPhim ?? "",
                TenRap = lich.IdPhongNavigation?.IdRapNavigation?.TenRap ?? "",
                TenPhong = lich.IdPhongNavigation?.TenPhong ?? "",
                GioChieu = $"{lich.NgayChieu:dd/MM/yyyy} - {lich.GioChieu}",
                TongTienGhe = tongTienGhe,
                Combos = _context.ComboMonAns.Select(c => new ComboMonAn
                {
                    IdMonAn = c.IdMonAn,
                    CacMonAn = c.CacMonAn,
                    GiaTien = c.GiaTien
                }).ToList(),

                MonLe = _context.MonAnvaThucUongs
                    .Where(m => m.TrangThai == true)
                    .Select(m => new MonAnvaThucUong
                    {
                        MaMon = m.MaMon,
                        TenMon = m.TenMon,
                        Loai = m.Loai,
                        Gia = m.Gia,
                        MoTa = m.MoTa,
                        TrangThai = m.TrangThai
                    }).ToList()
            };

            return View("~/Views/CinemaView/ChonBapNuoc.cshtml", model);
        }
    }
}
