namespace CinemaProject.Models
{
    public class TrangChuViewModel
    {
        public List<Phim> SapKhoiChieuGanNhat { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public List<Phim> PhimRaMatHomNay { get; set; }
        public List<Phim> DangChieu { get; set; }
    }
}
