using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CinemaProject.Models
{
    public partial class PhongChieu
    {
        [Key]
        public string IdPhong { get; set; }

        [Required]
        public string IdRap { get; set; }

        public string? TenPhong { get; set; }

        public int SoLuongGhe { get; set; }

        [ForeignKey("IdRap")]
        public virtual Rap? IdRapNavigation { get; set; }

        public virtual ICollection<LichChieu> LichChieus { get; set; } = new List<LichChieu>();
    }
}
