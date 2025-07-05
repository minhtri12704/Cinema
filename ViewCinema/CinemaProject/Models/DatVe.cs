using System;
using System.Collections.Generic;

namespace CinemaProject.Models;

public partial class DatVe
{
    public string IdDatVe { get; set; } = null!;

    public string? IdKhach { get; set; }

    public string? IdLich { get; set; }

    public string? GheNgoi { get; set; }

    public DateTime? ThoiGianDat { get; set; }

    public int TongTien { get; set; }

    public virtual ICollection<ApDungKhuyenMaiVe> ApDungKhuyenMaiVes { get; set; } = new List<ApDungKhuyenMaiVe>();

    public virtual KhachHang? IdKhachNavigation { get; set; }

    public virtual LichChieu? IdLichNavigation { get; set; }

    public virtual ICollection<VeXemPhim> VeXemPhims { get; set; } = new List<VeXemPhim>();
}
