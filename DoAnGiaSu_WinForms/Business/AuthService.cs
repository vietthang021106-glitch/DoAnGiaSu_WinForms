using DoAnGiaSu_WinForms.DataAccess;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.Business
{
    public class AuthService
    {
        private readonly AuthRepository authRepository = new AuthRepository();

        public TaiKhoan Authenticate(string tenDangNhap, string matKhau)
        {
            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(matKhau))
                return null;

            return authRepository.Authenticate(tenDangNhap, matKhau);
        }
    }
}
