using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaProject.Models;

public partial class Phim
{
    public string IdPhim { get; set; }

    public string TenPhim { get; set; } = null!;

    public string? IdTheLoai { get; set; }
    [NotMapped]
    public string? HinhAnh { get; set; } // Không ghi vào DB

    public int ThoiLuong { get; set; }

    public DateTime? NgayKhoiChieu { get; set; }

    public string? DoTuoiPhuHop { get; set; }

    public virtual TheLoai? IdTheLoaiNavigation { get; set; }

    public virtual ICollection<LichChieu> LichChieus { get; set; } = new List<LichChieu>();

    public virtual ICollection<PhimDoiTac> PhimDoiTacs { get; set; } = new List<PhimDoiTac>();
}
