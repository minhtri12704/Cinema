using System;
using System.Collections.Generic;

namespace CinemaProject.Models;

public partial class Phim
{
    public string IdPhim { get; set; } = string.Empty;

    public string TenPhim { get; set; } = null!;

    public string? IdTheLoai { get; set; }

    public int ThoiLuong { get; set; }

    public DateTime? NgayKhoiChieu { get; set; }

    public string? DoTuoiPhuHop { get; set; }

    public virtual TheLoai? IdTheLoaiNavigation { get; set; }

    public virtual ICollection<LichChieu> LichChieus { get; set; } = new List<LichChieu>();

    public virtual ICollection<PhimDoiTac> PhimDoiTacs { get; set; } = new List<PhimDoiTac>();
    public virtual ICollection<DanhGiaPhim> DanhGiaPhims { get; set; } = new List<DanhGiaPhim>();


}
