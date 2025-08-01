namespace CinemaProject.Models
{
    public class ChonBapNuocViewModel
    {
            public List<ComboMonAn> Combos { get; set; }
            public List<MonAnvaThucUong> MonLe { get; set; }

        // Dữ liệu vé
        public string IdLich { get; set; }
        public string TenPhim { get; set; }
        public string TenRap { get; set; }
        public string TenPhong { get; set; }
        public string GioChieu { get; set; }
        public List<string> GheDaChon { get; set; }
        public int TongTienGhe { get; set; }
    }
}
