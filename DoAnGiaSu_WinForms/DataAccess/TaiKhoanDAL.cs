using Microsoft.Data.SqlClient;
using DoAnGiaSu_WinForms.Model;
using System;

namespace DoAnGiaSu_WinForms.DAL
{
    public class TaiKhoanDAL
    {
        DBConnection db = new DBConnection();

        public TaiKhoan KiemTraDangNhap(string tenDangNhap, string matKhau)
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

        public bool DangKy(TaiKhoan tk)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = "INSERT INTO TAIKHOAN (TenDangNhap, MatKhau, VaiTro) VALUES (@tk, @mk, @vt)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@tk", tk.TenDangNhap);
                cmd.Parameters.AddWithValue("@mk", tk.MatKhau);
                cmd.Parameters.AddWithValue("@vt", tk.VaiTro);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int LayMaTKTuTen(string tenDangNhap)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = "SELECT MaTK FROM TAIKHOAN WHERE TenDangNhap = @user";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", tenDangNhap);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }

        public string LayVaiTro(string tenDangNhap)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = "SELECT VaiTro FROM TAIKHOAN WHERE TenDangNhap = @user";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user", tenDangNhap);
                conn.Open();
                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : "";
            }
        }
    }
}