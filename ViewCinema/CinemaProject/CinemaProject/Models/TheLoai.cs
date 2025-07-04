using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaProject.Models;

public partial class TheLoai
{
    [Key]
    [Required(ErrorMessage = "Bạn phải nhập mã thể loại (IdTheLoai)")]
    public string IdTheLoai { get; set; } = null!;
    [Required(ErrorMessage = "Tên thể loại không được để trống")]
    public string? TenTheLoai { get; set; }

    public virtual ICollection<Phim> Phims { get; set; } = new List<Phim>();
}
