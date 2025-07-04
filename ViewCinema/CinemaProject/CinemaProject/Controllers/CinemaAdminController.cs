using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaProject.Models;

namespace CinemaProject.Controllers
{
    public class CinemaAdminController : Controller
    {
        private readonly RapChieuPhimContext _context;

        public CinemaAdminController(RapChieuPhimContext context)
        {
            _context = context;
        }

        // === KHÁCH HÀNG ===

        public IActionResult KhachHang()
        {
            var khachhangList = _context.KhachHangs.ToList();
            return View("~/Views/CinemaAdmin/KhachHang/KhachHang.cshtml", khachhangList);
        }

        [HttpGet]
        public IActionResult EditKhachHang(string id)
        {
            var khach = _context.KhachHangs.Find(id);
            if (khach == null) return NotFound();
            return View("~/Views/CinemaAdmin/KhachHang/Edit.cshtml", khach);
        }

        [HttpPost]
        public IActionResult EditKhachHang(KhachHang model)
        {
            var existing = _context.KhachHangs.Find(model.IdKhach);
            if (existing == null) return NotFound();

            existing.Ten = model.Ten;
            existing.Email = model.Email;
            existing.SoDienThoai = model.SoDienThoai;
            existing.MatKhauKH = model.MatKhauKH;

            _context.SaveChanges();
            return RedirectToAction("KhachHang");
        }

        public IActionResult DeleteKhachHang(string id)
        {
            var kh = _context.KhachHangs.Find(id);
            if (kh != null)
            {
                _context.KhachHangs.Remove(kh);
                _context.SaveChanges();
            }
            return RedirectToAction("KhachHang");
        }

        // === ĐĂNG XUẤT ===

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "CinemaView");
        }

        // === PHIM ===

        public IActionResult Movie()
        {
            var dsPhim = _context.Phims.Include(p => p.IdTheLoaiNavigation).ToList();
            return View("~/Views/CinemaAdmin/SanPham/Product.cshtml", dsPhim);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.IdTheLoai = new SelectList(_context.TheLoais, "IdTheLoai", "TenTheLoai");
            return View("~/Views/CinemaAdmin/SanPham/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Phim phim)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IdTheLoai = new SelectList(_context.TheLoais, "IdTheLoai", "TenTheLoai", phim.IdTheLoai);
                return View("~/Views/CinemaAdmin/SanPham/Create.cshtml", phim);
            }

            _context.Phims.Add(phim);
            _context.SaveChanges();
            return RedirectToAction("Movie");
        }

        [HttpGet]
        public IActionResult EditPhim(string id)
        {
            var phim = _context.Phims.Find(id);
            if (phim == null) return NotFound();

            ViewBag.IdTheLoai = new SelectList(_context.TheLoais, "IdTheLoai", "TenTheLoai", phim.IdTheLoai);
            return View("~/Views/CinemaAdmin/SanPham/Edit.cshtml", phim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPhim(Phim phim)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IdTheLoai = new SelectList(_context.TheLoais, "IdTheLoai", "TenTheLoai", phim.IdTheLoai);
                return View("~/Views/CinemaAdmin/SanPham/Edit.cshtml", phim);
            }

            try
            {
                _context.Update(phim);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Phims.Any(p => p.IdPhim == phim.IdPhim))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction("Movie");
        }

        public IActionResult DeletePhim(string id)
        {
            var phim = _context.Phims.Find(id);
            if (phim != null)
            {
                _context.Phims.Remove(phim);
                _context.SaveChanges();
            }
            return RedirectToAction("Movie");
        }

        // === ĐẶT VÉ ===

        public IActionResult BookVe()
        {
            var dsDatVe = _context.BookVes.ToList();
            return View("~/Views/CinemaAdmin/BookVe/BookVe.cshtml", dsDatVe);
        }

        [HttpPost]
        public IActionResult HuyVe(string id)
        {
            var ve = _context.BookVes.FirstOrDefault(v => v.IdBookVe == id);
            if (ve != null && ve.TrangThai != "Đã hủy")
            {
                ve.TrangThai = "Đã hủy";
                _context.SaveChanges();
            }
            return RedirectToAction("BookVe");
        }

        // === THỂ LOẠI ===

        public IActionResult DanhSachTheLoai()
        {
            var theloais = _context.TheLoais.ToList();
            return View("~/Views/CinemaAdmin/DanhMuc/DanhSachTheLoai.cshtml", theloais);
        }

        [HttpGet]
        public IActionResult CreateTheLoai()
        {
            return View("~/Views/CinemaAdmin/DanhMuc/CreateTheLoai.cshtml");
        }

        [HttpPost]
        public IActionResult CreateTheLoai(TheLoai model)
        {
            if (ModelState.IsValid)
            {
                // Giữ nguyên IdTheLoai do người dùng nhập
                _context.TheLoais.Add(model);
                _context.SaveChanges();
                return RedirectToAction("DanhSachTheLoai");
            }

            return View("~/Views/CinemaAdmin/DanhMuc/CreateTheLoai.cshtml", model);
        }
        


        [HttpGet]
        public IActionResult EditTheLoai(string id)
        {
            var theloai = _context.TheLoais.Find(id);
            return View("~/Views/CinemaAdmin/DanhMuc/EditTheLoai.cshtml", theloai);
        }

        [HttpPost]
        public IActionResult EditTheLoai(TheLoai model)
        {
            if (ModelState.IsValid)
            {
                _context.TheLoais.Update(model);
                _context.SaveChanges();
                return RedirectToAction("DanhSachTheLoai");
            }
            return View("~/Views/CinemaAdmin/DanhMuc/EditTheLoai.cshtml", model);
        }

        public IActionResult DeleteTheLoai(string id)
        {
            var theloai = _context.TheLoais.Find(id);
            if (theloai != null)
            {
                _context.TheLoais.Remove(theloai);
                _context.SaveChanges();
            }
            return RedirectToAction("DanhSachTheLoai");
        }
    }
}
