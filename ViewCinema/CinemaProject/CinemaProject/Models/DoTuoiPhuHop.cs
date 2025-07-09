using System.ComponentModel.DataAnnotations;

namespace CinemaProject.Models
{
    public class DoTuoiPhuHop
    {
        [Key]
        [StringLength(10)]
        public string MaDoTuoi { get; set; }

        [StringLength(255)]
        public string MoTa { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Tuổi tối thiểu phải lớn hơn hoặc bằng 0")]
        public int TuoiToiThieu { get; set; }

    }
}
