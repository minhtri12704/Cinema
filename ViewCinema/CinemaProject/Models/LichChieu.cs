using System;
using System.Collections.Generic;

namespace CinemaProject.Models;

public partial class LichChieu
{
    public string IdLich { get; set; } = null!;

    public string? IdPhim { get; set; }

    public string? IdPhong { get; set; }

    public DateTime NgayChieu { get; set; }

    public string GioChieu { get; set; } = null!;

    public int GiaVe { get; set; }

    public virtual ICollection<DatVe> DatVes { get; set; } = new List<DatVe>();
    public virtual ICollection<BookVe> BookVes { get; set; } = new List<BookVe>();

    public virtual Phim? IdPhimNavigation { get; set; }

    public virtual PhongChieu? IdPhongNavigation { get; set; }
}
