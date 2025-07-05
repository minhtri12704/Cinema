using System.ComponentModel.DataAnnotations;

namespace CinemaProject.Models
{
    public class BookVe
    {
        [Key]
        public string? IdBookVe { get; set; }

        public string? IdKhach { get; set; }

        public string IdLich { get; set; }

        public string? GheNgoi { get; set; }

        public DateTime ThoiGianDat { get; set; }

        public int? TongTien { get; set; }

        public string? TrangThai { get; set; }
        public virtual KhachHang IdKhachNavigation { get; set; }
        public virtual LichChieu IdLichNavigation { get; set; }
    }
}
