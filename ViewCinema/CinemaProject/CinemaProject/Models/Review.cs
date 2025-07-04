using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CinemaProject.Models
{
    public partial class DanhGiaPhim
    {
        public string IdDanhGia { get; set; } = null!;

        public string IdPhim { get; set; } = null!;
        public string IdKhach { get; set; } = null!;

        public string? NoiDung { get; set; }
        public int SoSao { get; set; }
        public DateTime? NgayDanhGia { get; set; }

        public virtual Phim? Phim { get; set; }
        public virtual KhachHang? KhachHang { get; set; }
        public virtual Phim IdPhimNavigation { get; set; } = null!;
        public virtual KhachHang IdKhachNavigation { get; set; } = null!;

    }
}