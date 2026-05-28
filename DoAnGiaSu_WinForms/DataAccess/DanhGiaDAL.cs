using System;
using System.Data;
using Microsoft.Data.SqlClient;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.DataAccess
{
    public class DanhGiaDAL
    {
        private readonly DBConnection db = new DBConnection();

        public bool ThemDanhGia(DanhGia danhGia)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = @"INSERT INTO DANHGIA (MaPH, MaGS, MaBaiDang, SoSao, NoiDung, NgayDanhGia)
                                 VALUES (@MaPH, @MaGS, @MaBaiDang, @SoSao, @NoiDung, GETDATE())";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPH", danhGia.MaPH);
                cmd.Parameters.AddWithValue("@MaGS", danhGia.MaGS);
                cmd.Parameters.AddWithValue("@MaBaiDang", danhGia.MaBaiDang);
                cmd.Parameters.AddWithValue("@SoSao", danhGia.SoSao);
                cmd.Parameters.AddWithValue("@NoiDung", string.IsNullOrWhiteSpace(danhGia.NoiDung) ? (object)DBNull.Value : danhGia.NoiDung);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable LayDanhGiaTheoGiaSu(int maGS)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = @"SELECT ph.HoTen, dg.SoSao, dg.NoiDung, dg.NgayDanhGia
                                 FROM DANHGIA dg
                                 JOIN PHUHUYNH ph ON dg.MaPH = ph.MaPH
                                 WHERE dg.MaGS = @MaGS
                                 ORDER BY dg.NgayDanhGia DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaGS", maGS);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public DataTable LayThongKeDanhGiaGiaSu(int maGS)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = @"SELECT CAST(AVG(CAST(SoSao AS FLOAT)) AS FLOAT) AS DiemTB, COUNT(*) AS LuotDanhGia
                                 FROM DANHGIA
                                 WHERE MaGS = @MaGS";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaGS", maGS);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
