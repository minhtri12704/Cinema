using System;
using System.Collections.Generic;

namespace CinemaProject.Models;

public partial class KhachHang
{
    public string IdKhach { get; set; } = null!;

    public string Ten { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? SoDienThoai { get; set; }

    public DateTime? NgayDangKy { get; set; }
    public string MatKhauKH { get; set; } = null!;

    public virtual ICollection<DatVe> DatVes { get; set; } = new List<DatVe>();
    public virtual ICollection<BookVe> BookVes { get; set; } = new List<BookVe>();

    public virtual ICollection<DonHangDoAn> DonHangDoAns { get; set; } = new List<DonHangDoAn>();
}
