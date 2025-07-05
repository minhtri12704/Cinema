using System.ComponentModel.DataAnnotations;

namespace CinemaProject.Models
{
    public class MonAnvaThucUong
    {
        [Key]
        public int MaMon { get; set; }

        [Required]
        public string TenMon { get; set; }

        [Required]
        public string Loai { get; set; }

        [Required]
        public decimal Gia { get; set; }

        public string? MoTa { get; set; }

        public bool TrangThai { get; set; } = true;
    }
}
