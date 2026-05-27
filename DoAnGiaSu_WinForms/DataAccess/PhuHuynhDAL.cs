using Microsoft.Data.SqlClient;
using DoAnGiaSu_WinForms.Model;
using System;

namespace DoAnGiaSu_WinForms.DAL
{
    public class PhuHuynhDAL
    {
        DBConnection db = new DBConnection();

        public bool ThemPhuHuynh(PhuHuynh ph)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = @"INSERT INTO PHUHUYNH (HoTen, SDT, MaQuan, SoNhaDuong, MaTK) 
                                VALUES (@ten, @sdt, @quan, @dc, @matk)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ten", ph.HoTen);
                cmd.Parameters.AddWithValue("@sdt", ph.SDT);
                cmd.Parameters.AddWithValue("@quan", ph.MaQuan);
                cmd.Parameters.AddWithValue("@dc", ph.SoNhaDuong);
                cmd.Parameters.AddWithValue("@matk", ph.MaTK);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int LayMaPH(int maTK)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = "SELECT MaPH FROM PHUHUYNH WHERE MaTK = @matk";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@matk", maTK);
                conn.Open();
                object res = cmd.ExecuteScalar();
                return (res != null && res != DBNull.Value) ? Convert.ToInt32(res) : 0;
            }
        }

        public string KiemTraSoDienThoai(string sdt)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = "SELECT SDT FROM PHUHUYNH WHERE SDT = @sdt";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@sdt", sdt);
                conn.Open();
                object res = cmd.ExecuteScalar();
                if (res != null && res != DBNull.Value)
                {
                    return "Số điện thoại đã được sử dụng ở một tài khoản phụ huynh khác!";
                }
                return "";
            }
        }

        public System.Data.DataTable LayDanhSachQuan()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = "SELECT MaQuan, TenQuan FROM DM_QUANHUYEN ORDER BY TenQuan";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}