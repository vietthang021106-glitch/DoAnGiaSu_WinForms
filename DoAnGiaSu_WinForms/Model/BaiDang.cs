namespace DoAnGiaSu_WinForms.Model
{
    public class BaiDang
    {
        public int MaBaiDang { get; set; }
        public int MaPH { get; set; }
        public string YeuCauThem { get; set; } // Thay cho GhiChu
        public decimal MucLuong { get; set; }  // Thay cho HocPhi
        public string TrangThai { get; set; }
        public int MaMon { get; set; }         // Thay cho MaMonHoc
        public int MaLop { get; set; }         // Thay cho MaLopHoc
        public int MaHinhThuc { get; set; }
        public int MaQuan { get; set; }
        public int YeuCauTrinhDo { get; set; }
        public string SoNhaDuong { get; set; }
    }
}