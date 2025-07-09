using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaProject.Models;

public partial class Phim
{
    public string IdPhim { get; set; } = null!;

    public string TenPhim { get; set; } = null!;

    public string? IdTheLoai { get; set; }

    public string? HinhAnh { get; set; }

    public int ThoiLuong { get; set; }

    public DateTime? NgayKhoiChieu { get; set; }

    public string? MaDoTuoi { get; set; }
    public string? MoTa { get; set; }

    public virtual ICollection<LichChieu> LichChieus { get; set; } = new List<LichChieu>();

    public virtual ICollection<PhimDoiTac> PhimDoiTacs { get; set; } = new List<PhimDoiTac>();

    public virtual ICollection<DanhGiaPhim> DanhGiaPhims { get; set; } = new List<DanhGiaPhim>();

    public virtual DoTuoiPhuHop DoTuoiPhuHopNavigation { get; set; }

    [NotMapped]
    public List<string> TenTheLoais { get; set; } = new List<string>();

}
