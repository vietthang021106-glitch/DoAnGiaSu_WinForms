using System.Drawing;

namespace DoAnGiaSu_WinForms.GUI
{
    public sealed class AdminBaiDangCardModel
    {
        public int MaBaiDang { get; set; }
        public string PhuHuynh { get; set; }
        public string MonHoc { get; set; }
        public string Lop { get; set; }
        public string HinhThuc { get; set; }
        public string KhuVuc { get; set; }
        public string MucLuong { get; set; }
        public string TrangThai { get; set; }
        public string MaGSNhan { get; set; }
    }

    public sealed class AdminGiaSuCardModel
    {
        public int MaGS { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string CCCD { get; set; }
        public string ThanhTich { get; set; }
        public string Truong { get; set; }
        public string TrinhDo { get; set; }
        public string AnhThePath { get; set; }
        public string AnhBangDiemPath { get; set; }
        public string AnhChungChiPath { get; set; }
        public Image AnhTheImage { get; set; }
        public Image AnhBangDiemImage { get; set; }
        public Image AnhChungChiImage { get; set; }
    }

    public sealed class AdminHoaHongCardModel
    {
        public int MaBaiDang { get; set; }
        public string PhuHuynh { get; set; }
        public string MonHoc { get; set; }
        public string MucLuong { get; set; }
        public string HoaHong { get; set; }
        public string TrangThai { get; set; }
        public string MaGS { get; set; }
        public string AnhBillPath { get; set; }
        public Image AnhBillImage { get; set; }
    }
}