using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaProject.Models;
using System.Collections.Generic;

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
            var dsPhim = _context.Phims.ToList();
            var danhSachTheLoai = _context.TheLoais.ToList();

            foreach (var phim in dsPhim)
            {
                var maTheLoais = phim.IdTheLoai?.Split(',') ?? new string[0];
                phim.TenTheLoais = danhSachTheLoai
                    .Where(t => maTheLoais.Contains(t.IdTheLoai))
                    .Select(t => t.TenTheLoai)
                    .ToList();
            }

            return View("~/Views/CinemaAdmin/SanPham/Product.cshtml", dsPhim);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.IdTheLoai = _context.TheLoais
                .Select(t => new SelectListItem
                {
                    Value = t.IdTheLoai,
                    Text = t.TenTheLoai
                }).ToList();

            return View("~/Views/CinemaAdmin/SanPham/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Phim phim, IFormFile HinhAnhFile)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IdTheLoai = new SelectList(_context.TheLoais, "IdTheLoai", "TenTheLoai", phim.IdTheLoai);
                return View("~/Views/CinemaAdmin/SanPham/Create.cshtml", phim);
            }

            // Xử lý ảnh nếu có
            if (HinhAnhFile != null && HinhAnhFile.Length > 0)
            {
                var extension = Path.GetExtension(HinhAnhFile.FileName).ToLower();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("HinhAnh", "Chỉ chấp nhận ảnh .jpg, .jpeg, .png");
                    ViewBag.IdTheLoai = new SelectList(_context.TheLoais, "IdTheLoai", "TenTheLoai", phim.IdTheLoai);
                    return View("~/Views/CinemaAdmin/SanPham/Create.cshtml", phim);
                }

                var fileName = Path.GetFileName(HinhAnhFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                //  Nếu ảnh chưa có trong thư mục thì mới lưu
                if (!System.IO.File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await HinhAnhFile.CopyToAsync(stream);
                    }
                }

                phim.HinhAnh = fileName;
            }

            _context.Phims.Add(phim);
            await _context.SaveChangesAsync();
            return RedirectToAction("Movie");
        }


        [HttpGet]
        public IActionResult EditPhim(string id)
        {
            var phim = _context.Phims.Find(id);
            if (phim == null) return NotFound();

            var dsTheLoai = _context.TheLoais.ToList();
            var theLoaiList = dsTheLoai.Select(tl => new SelectListItem
            {
                Text = tl.TenTheLoai,
                Value = tl.IdTheLoai,
                Selected = phim.IdTheLoai?.Split(',').Select(x => x.Trim()).Contains(tl.IdTheLoai) == true
            }).ToList();

            ViewBag.IdTheLoai = theLoaiList;

            return View("~/Views/CinemaAdmin/SanPham/Edit.cshtml", phim);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPhim(Phim phim)
        {
            // Nếu model không hợp lệ → return lại View
            if (!ModelState.IsValid)
            {
                var theLoaiDaChon = (phim.IdTheLoai ?? "")
                             .Split(',', StringSplitOptions.RemoveEmptyEntries)
                             .Select(x => x.Trim())
                             .ToList();

                ViewBag.IdTheLoai = _context.TheLoais
                    .Select(tl => new SelectListItem
                    {
                        Text = tl.TenTheLoai,
                        Value = tl.IdTheLoai,
                        Selected = theLoaiDaChon.Contains(tl.IdTheLoai)
                    }).ToList();

                return View("~/Views/CinemaAdmin/SanPham/Edit.cshtml", phim);
            }

            var phimCu = await _context.Phims.FindAsync(phim.IdPhim);
            if (phimCu == null) return NotFound();

            // Cập nhật các thông tin cơ bản
            phimCu.TenPhim = phim.TenPhim;
            phimCu.ThoiLuong = phim.ThoiLuong;
            phimCu.NgayKhoiChieu = phim.NgayKhoiChieu;
            phimCu.MaDoTuoi = phim.MaDoTuoi;
            phimCu.MoTa = phim.MoTa;
            phimCu.IdTheLoai = phim.IdTheLoai;

            // 🔍 Xử lý hình ảnh: kiểm tra nếu file không tồn tại thì mới lưu vào thư mục
            if (!string.IsNullOrEmpty(phim.HinhAnh))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", phim.HinhAnh);
                if (!System.IO.File.Exists(filePath))
                {
                    var fileFromForm = Request.Form.Files.FirstOrDefault(f => f.FileName == phim.HinhAnh);
                    if (fileFromForm != null && fileFromForm.Length > 0)
                    {
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await fileFromForm.CopyToAsync(stream);
                        }
                    }
                }

                phimCu.HinhAnh = phim.HinhAnh; // Gán tên ảnh vào DB
            }

            await _context.SaveChangesAsync();
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
        // ==============================
        // ==== PHÒNG CHIẾU ============
        // ==============================

        public IActionResult DanhSachPhongChieu()
        {
            var list = _context.PhongChieus
                   .Include(p => p.IdRapNavigation)
                   .ToList();

            return View("~/Views/CinemaAdmin/Phong/DanhSach.cshtml", list);
        }

        [HttpGet]
        public IActionResult CreatePhongChieu()
        {
            ViewBag.IdRap = new SelectList(_context.Raps, "IdRap", "TenRap");
            return View("~/Views/CinemaAdmin/Phong/CreatePhong.cshtml", new PhongChieu());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePhongChieu(PhongChieu model)
        {
            if (ModelState.IsValid)
            {
                _context.PhongChieus.Add(model);
                _context.SaveChanges();
                return RedirectToAction("DanhSachPhongChieu");
            }

            ViewBag.IdRap = new SelectList(_context.Raps, "IdRap", "TenRap", model.IdRap);
            return View("~/Views/CinemaAdmin/Phong/CreatePhong.cshtml", model);
        }

        [HttpGet]
        public IActionResult EditPhongChieu(string id)
        {
            var phong = _context.PhongChieus.Find(id);
            if (phong == null) return NotFound();

            ViewBag.IdRap = new SelectList(_context.Raps, "IdRap", "TenRap", phong.IdRap);
            return View("~/Views/CinemaAdmin/Phong/EditPhong.cshtml", phong);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPhongChieu(PhongChieu model)
        {
            if (ModelState.IsValid)
            {
                _context.PhongChieus.Update(model);
                _context.SaveChanges();
                return RedirectToAction("DanhSachPhongChieu");
            }

            ViewBag.IdRap = new SelectList(_context.Raps, "IdRap", "TenRap", model.IdRap);
            return View("~/Views/CinemaAdmin/Phong/EditPhong.cshtml", model);
        }

        public IActionResult DeletePhongChieu(string id)
        {
            var phong = _context.PhongChieus.Find(id);
            if (phong != null)
            {
                _context.PhongChieus.Remove(phong);
                _context.SaveChanges();
            }
            return RedirectToAction("DanhSachPhongChieu");
        }
        // ========================
        // MÓN ĂN VÀ THỨC UỐNG =====
        // ========================

        public IActionResult DanhSachMon()
        {
            var list = _context.MonAnvaThucUongs.ToList();
            return View("~/Views/CinemaAdmin/MonAn/DanhSachMon.cshtml", list);
        }

        [HttpGet]
        public IActionResult CreateMon()
        {
            return View("~/Views/CinemaAdmin/MonAn/CreateMon.cshtml");
        }

        [HttpPost]
        public IActionResult CreateMon(MonAnvaThucUong model)
        {
            if (ModelState.IsValid)
            {
                _context.MonAnvaThucUongs.Add(model);
                _context.SaveChanges();
                return RedirectToAction("DanhSachMon");
            }
            return View("~/Views/CinemaAdmin/MonAn/CreateMon.cshtml", model);
        }

        [HttpGet]
        public IActionResult EditMon(int id)
        {
            var mon = _context.MonAnvaThucUongs.Find(id);
            if (mon == null) return NotFound();

            return View("~/Views/CinemaAdmin/MonAn/EditMon.cshtml", mon);
        }

        [HttpPost]
        public IActionResult EditMon(MonAnvaThucUong model)
        {
            if (ModelState.IsValid)
            {
                _context.MonAnvaThucUongs.Update(model);
                _context.SaveChanges();
                return RedirectToAction("DanhSachMon");
            }
            return View("~/Views/CinemaAdmin/MonAn/EditMon.cshtml", model);
        }

        public IActionResult DeleteMon(int id)
        {
            var mon = _context.MonAnvaThucUongs.Find(id);
            if (mon != null)
            {
                _context.MonAnvaThucUongs.Remove(mon);
                _context.SaveChanges();
            }
            return RedirectToAction("DanhSachMon");
        }
        // === KHUYẾN MÃI ===

        public IActionResult DanhSachKhuyenMai()
        {
            var list = _context.KhuyenMais.ToList();
            return View("~/Views/CinemaAdmin/KhuyenMai/DanhSachKhuyenMai.cshtml", list);
        }

        [HttpGet]
        public IActionResult CreateKhuyenMai()
        {
            return View("~/Views/CinemaAdmin/KhuyenMai/CreateKhuyenMai.cshtml");
        }

        [HttpPost]
        public IActionResult CreateKhuyenMai(KhuyenMai model)
        {
            if (ModelState.IsValid)
            {
                _context.KhuyenMais.Add(model);
                _context.SaveChanges();
                return RedirectToAction("DanhSachKhuyenMai");
            }
            return View("~/Views/CinemaAdmin/KhuyenMai/CreateKhuyenMai.cshtml", model);
        }

        [HttpGet]
        public IActionResult EditKhuyenMai(string id)
        {
            var km = _context.KhuyenMais.Find(id);
            if (km == null) return NotFound();
            return View("~/Views/CinemaAdmin/KhuyenMai/EditKhuyenMai.cshtml", km);
        }

        [HttpPost]
        public IActionResult EditKhuyenMai(KhuyenMai model)
        {
            if (ModelState.IsValid)
            {
                _context.KhuyenMais.Update(model);
                _context.SaveChanges();
                return RedirectToAction("DanhSachKhuyenMai");
            }
            return View("~/Views/CinemaAdmin/KhuyenMai/EditKhuyenMai.cshtml", model);
        }

        public IActionResult DeleteKhuyenMai(string id)
        {
            var km = _context.KhuyenMais.Find(id);
            if (km != null)
            {
                _context.KhuyenMais.Remove(km);
                _context.SaveChanges();
            }
            return RedirectToAction("DanhSachKhuyenMai");
        }
        // ==============================
        // ==== LOẠI GHẾ ============
        // ==============================
        public IActionResult ListGhe()
        {
            var list = _context.Ghes.ToList();
            return View("~/Views/CinemaAdmin/Ghe/DanhSachLoaiGhe.cshtml", list);
        }
    }
}
