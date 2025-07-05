using System;
using System.Collections.Generic;

namespace CinemaProject.Models;

public partial class DonHangDoAn
{
    public string IdDonHang { get; set; } = null!;

    public string? IdKhach { get; set; }

    public string? DanhSachMon { get; set; }

    public int TongTien { get; set; }

    public DateTime? ThoiGianDat { get; set; }

    public virtual ICollection<ApDungKhuyenMaiDoAn> ApDungKhuyenMaiDoAns { get; set; } = new List<ApDungKhuyenMaiDoAn>();

    public virtual KhachHang? IdKhachNavigation { get; set; }
}
