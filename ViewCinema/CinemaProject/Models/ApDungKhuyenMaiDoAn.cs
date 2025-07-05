using System;
using System.Collections.Generic;

namespace CinemaProject.Models;

public partial class ApDungKhuyenMaiDoAn
{
    public string IdApDungDoAn { get; set; } = null!;

    public string? IdDonHang { get; set; }

    public string? IdKhuyenMai { get; set; }

    public int SoTienGiam { get; set; }

    public virtual DonHangDoAn? IdDonHangNavigation { get; set; }

    public virtual KhuyenMai? IdKhuyenMaiNavigation { get; set; }
}
