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

        public bool DangKy(TaiKhoan tk)
        {
            if (tk == null || string.IsNullOrWhiteSpace(tk.TenDangNhap) || string.IsNullOrWhiteSpace(tk.MatKhau))
                return false;
            return dal.DangKy(tk);
        }

        public bool KiemTraSDT(string tenDangNhap, string sdt)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(sdt))
                return false;
            return dal.KiemTraSDT(tenDangNhap, sdt);
        }

        public bool CapNhatMatKhau(string tenDangNhap, string matKhauMoi)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhauMoi))
                return false;
            return dal.CapNhatMatKhau(tenDangNhap, matKhauMoi);
        }

        public int LayMaTKTuTen(string tenDangNhap)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap))
                return 0;
            return dal.LayMaTKTuTen(tenDangNhap);
        }

        public string LayVaiTro(string tenDangNhap)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap))
                return "";
            return dal.LayVaiTro(tenDangNhap);
        }
    }
}