using CinemaProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CinemaProject.Controllers
{
    public class CinemaAdminController : Controller
    {
        private readonly RapChieuPhimContext _context;

        public CinemaAdminController(RapChieuPhimContext context)
        {
            _context = context;
        }
        //hiển thị danh sách khách hàng
        public IActionResult KhachHang()
        {
            var khachhangList = _context.KhachHangs.ToList();
            return View("~/Views/CinemaAdmin/khachhang/KhachHang.cshtml", khachhangList);
        }
        //tìm kiếm id khách hàng
        [HttpGet]
        public IActionResult EditKhachHang(string id)
        {
            var khach = _context.KhachHangs.Find(id);
            if (khach == null)
                return NotFound();

            return View("~/Views/CinemaAdmin/KhachHang/Edit.cshtml", khach);
        }
        //chỉnh sửa khách hàng
        [HttpPost]
        public IActionResult EditKhachHang(KhachHang model)
        {
            var existing = _context.KhachHangs.Find(model.IdKhach);
            if (existing == null)
                return NotFound();

            existing.Ten = model.Ten;
            existing.Email = model.Email;
            existing.SoDienThoai = model.SoDienThoai;
            existing.MatKhauKH = model.MatKhauKH;

            _context.SaveChanges();
            return RedirectToAction("KhachHang");
        }

        //xóa khách hàng
        public IActionResult Delete(string id)
        {
            var kh = _context.KhachHangs.Find(id);
            if (kh != null)
            {
                _context.KhachHangs.Remove(kh);
                _context.SaveChanges();
            }
            return RedirectToAction("KhachHang");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa session
            return RedirectToAction("Login");
        }

        // NHÂN VIÊN
        //Hiển thị danh sách nhân viên
        public IActionResult NhanVien()
        {
            var nhanvienlist = _context.NhanViens.ToList();
            return View("~/Views/CinemaAdmin/nhanvien/NhanVien.cshtml", nhanvienlist);
        }
        // Xóa Nhân Viên
        public IActionResult DeleteNV(string id)
        {
            var nv = _context.NhanViens.Find(id);
            if (nv != null)
            {
                _context.NhanViens.Remove(nv);
                _context.SaveChanges();
            }
            return RedirectToAction("NhanVien");
        }
        //tìm kiếm id khách hàng
        [HttpGet]
        public IActionResult EditNhanVien(string id)
        {
            var khach = _context.NhanViens.Find(id);
            if (khach == null)
                return NotFound();

            return View("~/Views/CinemaAdmin/NhanVien/EditNV.cshtml", khach);
        }
        //chỉnh sửa Nhân Viên
        [HttpPost]
        public IActionResult EditNhanVien(NhanVien model)
        {
            var existing = _context.NhanViens.Find(model.IdNhanVien);
            if (existing == null)
                return NotFound();

            existing.Ten = model.Ten;
            existing.ViTri = model.ViTri;
            existing.Luong = model.Luong;
            existing.NgayVaoLam = model.NgayVaoLam;

            _context.SaveChanges();
            return RedirectToAction("NhanVien");
        }

        //chuyển trang thêm 
        // Hiển thị form thêm nhân viên
        [HttpGet]
        public IActionResult AddNV()
        {
            return View("~/Views/CinemaAdmin/NhanVien/AddNV.cshtml");
        }

        // Xử lý form thêm nhân viên
        [HttpPost]
        public IActionResult AddNV(NhanVien model)
        {
            if (ModelState.IsValid)
            {
                _context.NhanViens.Add(model);
                _context.SaveChanges();
                return RedirectToAction("NhanVien");
            }

            return View("~/Views/CinemaAdmin/NhanVien/AddNV.cshtml", model);
        }

    }
}
