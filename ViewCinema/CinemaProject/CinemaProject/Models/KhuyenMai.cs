using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaProject.Models;

public partial class KhuyenMai
{
    [Key]
    public string IdKhuyenMai { get; set; }

    [Required]
    public string TenKhuyenMai { get; set; }

    public string? MoTa { get; set; }

    [Range(1, 100)]
    public int PhanTramGiam { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime NgayBatDau { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime NgayKetThuc { get; set; }

    public virtual ICollection<ApDungKhuyenMaiDoAn> ApDungKhuyenMaiDoAns { get; set; } = new List<ApDungKhuyenMaiDoAn>();

    public virtual ICollection<ApDungKhuyenMaiVe> ApDungKhuyenMaiVes { get; set; } = new List<ApDungKhuyenMaiVe>();
}
