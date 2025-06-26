using System;
using System.Collections.Generic;

namespace CinemaProject.Models;

public partial class TaiKhoanNhanVien
{
    public string IdTaiKhoan { get; set; } = null!;

    public string? IdNhanVien { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public virtual NhanVien? IdNhanVienNavigation { get; set; }
}
