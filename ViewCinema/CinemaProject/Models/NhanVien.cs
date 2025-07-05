using System;
using System.Collections.Generic;

namespace CinemaProject.Models;

public partial class NhanVien
{
    public string IdNhanVien { get; set; } = null!;

    public string? IdRap { get; set; }

    public string Ten { get; set; } = null!;

    public string? ViTri { get; set; }

    public int? Luong { get; set; }

    public DateTime? NgayVaoLam { get; set; }

    public virtual Rap? IdRapNavigation { get; set; }

    public virtual ICollection<TaiKhoanNhanVien> TaiKhoanNhanViens { get; set; } = new List<TaiKhoanNhanVien>();
}
