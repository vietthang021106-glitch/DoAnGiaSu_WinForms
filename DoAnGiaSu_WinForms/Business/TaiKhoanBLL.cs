using DoAnGiaSu_WinForms.DAL;
using DoAnGiaSu_WinForms.Model;

namespace DoAnGiaSu_WinForms.BLL
{
    // Đổi sang public để Form có thể truy cập
    public class TaiKhoanBLL
    {
        TaiKhoanDAL dal = new TaiKhoanDAL();

        public TaiKhoan Login(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
                return null;

            return dal.KiemTraDangNhap(tenDangNhap, matKhau);
        }

        // --- Dán thêm hàm này để hết lỗi ở Form Đăng Ký ---
        public string RegisterAccount(string user, string pass, string confirm, string role)
        {
            // 1. Kiểm tra rỗng
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
                return "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!";

            // 2. Kiểm tra khớp mật khẩu
            if (pass != confirm)
                return "Mật khẩu xác nhận không khớp!";

            // 3. Kiểm tra tên đăng nhập tồn tại
            if (dal.LayMaTKTuTen(user) > 0)
                return "Tên đăng nhập đã tồn tại!";

            // Trả về Thành công nhưng CHƯA THÊM VÀO CSDL
            return "Thành công";
        }
    }
}