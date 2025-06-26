using System;
using System.Collections.Generic;

namespace CinemaProject.Models;

public partial class KhuyenMai
{
    public string IdKhuyenMai { get; set; } = null!;

    public string TenKhuyenMai { get; set; } = null!;

    public string? MoTa { get; set; }

    public int? PhanTramGiam { get; set; }

    public DateTime NgayBatDau { get; set; }

    public DateTime NgayKetThuc { get; set; }

    public virtual ICollection<ApDungKhuyenMaiDoAn> ApDungKhuyenMaiDoAns { get; set; } = new List<ApDungKhuyenMaiDoAn>();

    public virtual ICollection<ApDungKhuyenMaiVe> ApDungKhuyenMaiVes { get; set; } = new List<ApDungKhuyenMaiVe>();
}
