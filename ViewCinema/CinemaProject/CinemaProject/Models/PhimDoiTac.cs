using System;
using System.Collections.Generic;

namespace CinemaProject.Models;

public partial class PhimDoiTac
{
    public string IdPhim { get; set; } = null!;

    public string IdDoiTac { get; set; } = null!;

    public DateTime NgayKiHopDong { get; set; }

    public virtual DoiTacPhim IdDoiTacNavigation { get; set; } = null!;

    public virtual Phim IdPhimNavigation { get; set; } = null!;
}
