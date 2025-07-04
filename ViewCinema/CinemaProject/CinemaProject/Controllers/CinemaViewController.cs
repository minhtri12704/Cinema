using CinemaProject.Models;
using Microsoft.AspNetCore.Mvc;

public class CinemaViewController : Controller
{
    private readonly RapChieuPhimContext _context;

    public IActionResult Search(string query)
    {
        var movies = _context.Phims
            .Where(m => m.TenPhim.Contains(query))
            .ToList();

        // Tạm thêm hình (gắn sẵn theo TenPhim hoặc IdPhim)
        foreach (var phim in movies)
        {
            phim.HinhAnh = $"{phim.IdPhim}.jpg"; // Gán cứng tên file ảnh tương ứng
        }

        return View("Search", movies);
    }

    public CinemaViewController(RapChieuPhimContext context)
    {
        _context = context;
    }

    public IActionResult Home(int page = 1)
    {
        int pageSize = 4;
        var today = new DateTime(2025,06,20);

        var sapKhoiChieu = _context.Phims
            .Where(p => p.NgayKhoiChieu > today)
            .OrderBy(p => p.NgayKhoiChieu);

        var model = new TrangChuViewModel
        {
            SapKhoiChieuGanNhat = sapKhoiChieu.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
            TotalPages = (int)Math.Ceiling(sapKhoiChieu.Count() / (double)pageSize),
            CurrentPage = page,

            PhimRaMatHomNay = _context.Phims
                .Where(p => p.NgayKhoiChieu == today).ToList(),

            DangChieu = _context.Phims
                .Where(p => p.NgayKhoiChieu <= today).ToList()
        };

        return View(model);
    }



    [HttpGet]
    public IActionResult Login()
    {
        return View("Login");
    }
    // AccountController.cs
    public IActionResult Signup()
    {
        return View("Signup");
    }


    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Kiểm tra đăng nhập với nhân viên
            var nhanVien = _context.TaiKhoanNhanViens
                .FirstOrDefault(u => u.TenDangNhap == model.TenDangNhap && u.MatKhau == model.MatKhau);
            if (nhanVien != null)
            {
                HttpContext.Session.SetString("username", model.TenDangNhap);
                HttpContext.Session.SetString("role", "NhanVien");

                // Chuyển đến trang KhachHang trong folder CinemaAdmin/khachhang
                return RedirectToAction("KhachHang", "CinemaAdmin", new { area = "khachhang" });
            }

            // Kiểm tra đăng nhập với khách hàng
            var khachHang = _context.KhachHangs
                .FirstOrDefault(u => u.Email == model.TenDangNhap && u.MatKhauKH == model.MatKhau);
            if (khachHang != null)
            {
                HttpContext.Session.SetString("username", model.TenDangNhap);
                HttpContext.Session.SetString("role", "KhachHang");

                // Chuyển đến trang Home trong CinemaView
                return RedirectToAction("Home", "CinemaView");
            }

            ViewBag.Error = "Đăng nhập thất bại. Vui lòng kiểm tra thông tin.";
        }

        return View("Login", model);
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear(); // Xóa session
        return RedirectToAction("Login");
    }
    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Tìm idKhach cuối cùng (giá trị KHxx lớn nhất)
            var lastKhach = _context.KhachHangs
                                    .OrderByDescending(k => k.IdKhach)
                                    .FirstOrDefault();

            int nextNumber = 1; // Mặc định nếu chưa có khách hàng nào

            if (lastKhach != null)
            {
                // Lấy phần số trong idKhach (VD: "KH17" → 17)
                var numberPart = lastKhach.IdKhach.Substring(2);
                if (int.TryParse(numberPart, out int parsedNumber))
                {
                    nextNumber = parsedNumber + 1;
                }
            }

            // Tạo id mới theo dạng KH01, KH02, ...
            string newId = "KH" + nextNumber.ToString("D2");

            var newKH = new KhachHang
            {
                IdKhach = newId,
                Ten = model.Ten,
                Email = model.Email,
                SoDienThoai = model.SoDienThoai,
                NgayDangKy = DateTime.Now,
                MatKhauKH = model.MatKhauKH
            };

            _context.KhachHangs.Add(newKH);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        ViewBag.Error = "Vui lòng kiểm tra lại thông tin.";
        return View("Signup",model);
    }

}
    