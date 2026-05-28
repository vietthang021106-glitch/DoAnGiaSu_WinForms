using DoAnGiaSu_WinForms.DataAccess;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.Business
{
    public class TaiKhoanBLL
    {
        TaiKhoanDAL dal = new TaiKhoanDAL();

        public TaiKhoan Login(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
                return null;

            return dal.KiemTraDangNhap(tenDangNhap, matKhau);
        }

        public string RegisterAccount(string user, string pass, string confirm, string role)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
                return "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu!";

            if (pass != confirm)
                return "Mật khẩu xác nhận không khớp!";

            if (dal.LayMaTKTuTen(user) > 0)
                return "Tên đăng nhập đã tồn tại!";

            return "Thành công";
        }
    }
}