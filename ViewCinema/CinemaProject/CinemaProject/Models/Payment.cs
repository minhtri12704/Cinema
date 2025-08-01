namespace CinemaProject.Models
{
    public class Payment
    {
        public string TenPhim { get; set; }
    public string TenRap { get; set; }
    public string TenPhong { get; set; }
    public string GioChieu { get; set; }

    public List<string> GheDaChon { get; set; }
        public decimal TongTienGhe { get; set; }

        public List<ComboMonAn> ComboDaChon { get; set; } = new();
    public List<MonAnvaThucUong> MonLeDaChon { get; set; } = new();
        public decimal TongTienCombo { get; set; }
        public decimal TongTienMonLe { get; set; }
        public decimal TongThanhToan { get; set; }
    }
}
