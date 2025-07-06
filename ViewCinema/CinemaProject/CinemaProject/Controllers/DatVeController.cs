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
            if (string.IsNullOrEmpty(id))
                return NotFound();

            // Lấy lịch chiếu (phim + phòng + rạp)
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

            // Danh sách vị trí ghế vip theo ưu tiên từ A05 đến B09
            var gheVipUuTien = new List<string>();
            for (char r = 'A'; r <= 'B'; r++)
            {
                for (int c = 5; c <= 9; c++)
                {
                    gheVipUuTien.Add($"{r}{c.ToString("D2")}");
                }
            }

            var gheVipThucTe = gheVipUuTien.Take(soGheVip).ToHashSet();

            // === GHẾ ĐƠN (bao gồm GHẾ VIP nếu trùng ID) ===
            for (int i = 0; i < soGheDon; i++)
            {
                int col = count % gheMoiHang + 1;
                if (col == 1 && i != 0) row++;

                string rowChar = ((char)('A' + row)).ToString();
                string colStr = col.ToString("D2");
                string maGhe = $"{rowChar}{colStr}";

                if (gheVipThucTe.Contains(maGhe))
                {
                    danhSachGhe.Add(new
                    {
                        Id = maGhe,
                        IdGhe = giaGheVip.IdGhe,
                        Gia = giaGheVip.Gia
                    });
                }
                else
                {
                    danhSachGhe.Add(new
                    {
                        Id = maGhe,
                        IdGhe = giaGheDon.IdGhe,
                        Gia = giaGheDon.Gia
                    });
                }

                count++;
            }

            // === THÊM GHẾ ẢO (GHẾ TRỐNG) ===
            int le = soGheDon % gheMoiHang;
            if (le != 0)
            {
                int gheAo = gheMoiHang - le;
                for (int i = 0; i < gheAo; i++)
                {
                    danhSachGhe.Add(new
                    {
                        Id = "",
                        IdGhe = gheTrong.IdGhe,
                        Gia = gheTrong.Gia
                    });
                }
            }

            // === GHẾ ĐÔI ===
            string hangDoi = ((char)('A' + row + 1)).ToString();
            for (int i = 1; i <= soGheDoi; i++)
            {
                string colStr = i.ToString("D2");
                danhSachGhe.Add(new
                {
                    Id = $"{hangDoi}{colStr}",
                    IdGhe = giaGheDoi.IdGhe,
                    Gia = giaGheDoi.Gia
                });
            }

            ViewBag.DanhSachGhe = danhSachGhe;
            return View("~/Views/CinemaView/DatGhe.cshtml", lich);
        }



    }
}
