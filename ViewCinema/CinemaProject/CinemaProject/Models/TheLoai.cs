using System;
using System.Collections.Generic;

namespace CinemaProject.Models;

public partial class TheLoai
{
    public string IdTheLoai { get; set; } = null!;

    public string? TenTheLoai { get; set; }

    public virtual ICollection<Phim> Phims { get; set; } = new List<Phim>();
}
