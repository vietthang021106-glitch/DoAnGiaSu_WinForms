namespace DoAnGiaSu_WinForms.Model
{
    public class GiaSu
    {
        public int MaGS { get; set; }           // SQL tự tăng
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string CCCD { get; set; }
        public int MaGioiTinh { get; set; }     // Khóa ngoại ID
        public int MaNamSinh { get; set; }      // Khóa ngoại ID
        public int MaTruong { get; set; }       // Khóa ngoại ID
        public int MaTrinhDo { get; set; }      // Khóa ngoại ID
        public string TrangThaiDuyet { get; set; } // Mặc định 'ChoDuyet'
        public int MaTK { get; set; }           // Khóa ngoại ID từ TAIKHOAN
        public string AnhMinhChung { get; set; }

        public string ThanhTich { get; set; }
        public string AnhBangDiem { get; set; }
        public string AnhChungChi { get; set; }
        public int? MaNamHoc { get; set; }

        public int? MaChungChi { get; set; }
        public string DiemChungChi { get; set; }
    }
}