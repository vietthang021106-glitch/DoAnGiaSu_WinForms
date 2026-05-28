using Microsoft.Data.SqlClient;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.DataAccess
{
    public class AuthRepository
    {
        private readonly DBConnection db = new DBConnection();

        public TaiKhoan Authenticate(string tenDangNhap, string matKhau)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = "SELECT MaTK, VaiTro FROM TAIKHOAN WHERE TenDangNhap = @tk AND MatKhau = @mk";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tk", tenDangNhap);
                cmd.Parameters.AddWithValue("@mk", matKhau);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new TaiKhoan
                        {
                            MaTK = (int)reader["MaTK"],
                            TenDangNhap = tenDangNhap,
                            VaiTro = reader["VaiTro"].ToString()
                        };
                    }
                }
            }
            return null;
        }
    }
}
