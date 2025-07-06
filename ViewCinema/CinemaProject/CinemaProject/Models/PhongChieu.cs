using System;
using System.Collections.Generic;

namespace CinemaProject.Models;

public partial class PhongChieu
{
    public string IdPhong { get; set; } = null!;

    public string? IdRap { get; set; }

    public string? TenPhong { get; set; }

    public int SoLuongGhe { get; set; }

    public int SoLuongGheDoi { get; set; }

    public int SoLuongGheVip { get; set; }

    public virtual Rap? IdRapNavigation { get; set; }

    public virtual ICollection<LichChieu> LichChieus { get; set; } = new List<LichChieu>();
}
