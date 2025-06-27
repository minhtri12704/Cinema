using CinemaProject.Models;
using Microsoft.AspNetCore.Mvc;

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
        return View("Home", dsPhim);
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
    //CỘNG ĐỒNG
    //Hiển thị cộng đồng
    public IActionResult Community()
    {
        return View();
    }
    //like , dislike
    [HttpPost]
    public IActionResult React([FromBody] ReactModel react)
    {
        if (string.IsNullOrEmpty(react.Type) || react.Id <= 0)
            return BadRequest();

        // TODO: Lưu vào DB (bảng Likes/Dislikes/Share)
        Console.WriteLine($"User reacted: {react.Type} on ID {react.Id}");

        return Ok();
    }

    public class ReactModel
    {
        public int Id { get; set; }
        public string Type { get; set; } // like / dislike / share
    }

    //NHÀ PHÊ BÌNH
    private static List<Critic> _critics = new List<Critic>
    {
        new Critic { Name = "Bùi An", Title = "Phóng Viên (HDVietnam)", Bio = "Yêu thích thể loại hành động và điều tra." },
        new Critic { Name = "Đào Bội Tú", Title = "Nhà phê bình tự do", Bio = "Thường viết review phim nghệ thuật và xã hội." },
        // thêm critic khác nếu cần
    };

    public IActionResult CriticDetail(string name)
    {
        var critic = new Critic
        {
            Name = name,
            Title = "Nhà phê bình tự do",
            Bio = "Một cây viết điện ảnh với góc nhìn sâu sắc và độc lập.",
            AvatarUrl = "/images/Suzume.jpg"
        };

        // Giả lập danh sách đánh giá của critic
        var reviews = new List<Review>
    {
        new Review { MovieTitle = "Parasite", Rating = 9, Comment = "Một tác phẩm xuất sắc về phân hóa xã hội." },
        new Review { MovieTitle = "Mắt Biếc", Rating = 8, Comment = "Gợi cảm xúc và gần gũi." },
        new Review { MovieTitle = "Doraemon Movie 44", Rating = 7, Comment = "Giải trí nhẹ nhàng, thích hợp cho thiếu nhi." }
    };

        var viewModel = new CriticDetailViewModel
        {
            Critic = critic,
            Reviews = reviews
        };

        return View("CriticsDetail", viewModel);
    }
}
