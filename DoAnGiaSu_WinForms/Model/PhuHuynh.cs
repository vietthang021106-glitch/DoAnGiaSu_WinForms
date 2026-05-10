namespace DoAnGiaSu_WinForms.Model
{
    public class PhuHuynh
    {
        public int MaPH { get; set; }           // Mã tự tăng trong SQL
        public string HoTen { get; set; }        // Họ tên phụ huynh
        public string SDT { get; set; }          // Số điện thoại
        public int MaQuan { get; set; }          // ID của Quận/Huyện
        public string SoNhaDuong { get; set; }   // Địa chỉ chi tiết
        public int MaTK { get; set; }            // ID tài khoản liên kết
    }
}