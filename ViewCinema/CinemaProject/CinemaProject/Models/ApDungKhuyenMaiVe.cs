using System;
using System.Collections.Generic;

namespace CinemaProject.Models;

public partial class ApDungKhuyenMaiVe
{
    public string IdApDung { get; set; } = null!;

    public string? IdDatVe { get; set; }

    public string? IdKhuyenMai { get; set; }

    public int SoTienGiam { get; set; }

    public virtual DatVe? IdDatVeNavigation { get; set; }

    public virtual KhuyenMai? IdKhuyenMaiNavigation { get; set; }
}
