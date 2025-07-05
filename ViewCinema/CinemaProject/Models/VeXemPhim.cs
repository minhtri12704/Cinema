using System;
using System.Collections.Generic;

namespace CinemaProject.Models;

public partial class VeXemPhim
{
    public string IdVe { get; set; } = null!;

    public string? IdDatVe { get; set; }

    public string? Ghe { get; set; }

    public int? Gia { get; set; }

    public virtual DatVe? IdDatVeNavigation { get; set; }
}
