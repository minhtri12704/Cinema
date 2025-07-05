using System;
using System.Collections.Generic;

namespace CinemaProject.Models;

public partial class DoiTacPhim
{
    public string IdDoiTac { get; set; } = null!;

    public string TenCongTy { get; set; } = null!;

    public string EmailDoiTac { get; set; } = null!;

    public string? SoDienThoaiDt { get; set; }

    public string? DiaChidt { get; set; }

    public virtual ICollection<PhimDoiTac> PhimDoiTacs { get; set; } = new List<PhimDoiTac>();
}
