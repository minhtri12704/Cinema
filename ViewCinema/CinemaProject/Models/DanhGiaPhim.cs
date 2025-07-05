using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CinemaProject.Models
{
    public partial class DanhGiaPhim
    {
        [Key]
        [StringLength(30)]
        public string IdDanhGia { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string IdKhach { get; set; } = null!;

        [Required]
        [StringLength(30)]
        public string IdPhim { get; set; } = null!;

        [Range(1, 10)]
        public int SoSao { get; set; }

        [StringLength(500)]
        public string? BinhLuan { get; set; }

        public DateTime NgayDanhGia { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual Phim? PhimNavigation { get; set; }
        public virtual KhachHang? KhachHangNavigation { get; set; }
    }
}
