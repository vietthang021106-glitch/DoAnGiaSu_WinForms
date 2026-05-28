using System;
using System.Data;
using Microsoft.Data.SqlClient;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.DataAccess
{
    public class GiaSuDAL
    {
        DBConnection db = new DBConnection();

        public DataTable LayDanhMuc(string tableName)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = string.Format(GiaSuSql.LayDanhMuc, tableName);
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool ThemGiaSu(GiaSu gs)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(GiaSuSql.ThemGiaSu, conn);
                cmd.Parameters.AddWithValue("@hoten", gs.HoTen);
                cmd.Parameters.AddWithValue("@sdt", gs.SDT);
                cmd.Parameters.AddWithValue("@cccd", gs.CCCD);
                cmd.Parameters.AddWithValue("@magt", gs.MaGioiTinh);
                cmd.Parameters.AddWithValue("@mans", gs.MaNamSinh);
                cmd.Parameters.AddWithValue("@matruong", gs.MaTruong);
                cmd.Parameters.AddWithValue("@matd", gs.MaTrinhDo);
                cmd.Parameters.AddWithValue("@trangthai", string.IsNullOrWhiteSpace(gs.TrangThaiDuyet) ? "ChoDuyet" : gs.TrangThaiDuyet);
                cmd.Parameters.AddWithValue("@matk", gs.MaTK);
                cmd.Parameters.AddWithValue("@anh", (object)gs.AnhMinhChung ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@anhbangdiem", (object)gs.AnhBangDiem ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@anhchungchi", (object)gs.AnhChungChi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@manamhoc", (object?)gs.MaNamHoc ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@machungchi", (object?)gs.MaChungChi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@diemchungchi", (object)gs.DiemChungChi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@diemgpa", (object)gs.DiemGPA ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@maxeploai", (object?)gs.MaXepLoai ?? DBNull.Value);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public int LayMaGSMoiNhatTheoMaTK(int maTK)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(GiaSuSql.LayMaGSMoiNhatTheoMaTK, conn);
                cmd.Parameters.AddWithValue("@matk", maTK);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                    return Convert.ToInt32(result);
                return 0;
            }
        }

        public bool ThemChiTietChungChiGiaSu(int maGS, int maChungChi, string diemChungChi, string anhChungChi)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(GiaSuSql.ThemChiTietChungChiGiaSu, conn);
                cmd.Parameters.AddWithValue("@maGS", maGS);
                cmd.Parameters.AddWithValue("@maCC", maChungChi);
                cmd.Parameters.AddWithValue("@diem", (object)diemChungChi ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@anh", (object)anhChungChi ?? DBNull.Value);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public string KiemTraTonTai(string cccd, string sdt)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(GiaSuSql.KiemTraTonTai, conn);
                cmd.Parameters.AddWithValue("@cccd", cccd);
                cmd.Parameters.AddWithValue("@sdt", sdt);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string cccdDb = reader["CCCD"].ToString();
                        string sdtDb = reader["SDT"].ToString();
                        if (cccdDb == cccd) return "CCCD đã được sử dụng ở một tài khoản khác!";
                        if (sdtDb == sdt) return "Số điện thoại đã được sử dụng ở một tài khoản khác!";
                    }
                }
                return "";
            }
        }

        public DataTable LayTatCaGiaSuAdmin()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter(GiaSuSql.LayTatCaGiaSuAdmin, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool CapNhatTrangThaiDuyet(int maGS, string trangThai)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(GiaSuSql.CapNhatTrangThaiDuyet, conn);
                cmd.Parameters.AddWithValue("@tt", trangThai);
                cmd.Parameters.AddWithValue("@ma", maGS);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool XoaGiaSu(int maGS)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    SqlCommand getMaTK = new SqlCommand(GiaSuSql.LayMaTKTheoMaGS, conn);
                    getMaTK.Parameters.AddWithValue("@ma", maGS);
                    object maTkObj = getMaTK.ExecuteScalar();

                    SqlCommand cmdGS = new SqlCommand(GiaSuSql.XoaGiaSuById, conn);
                    cmdGS.Parameters.AddWithValue("@ma", maGS);
                    cmdGS.ExecuteNonQuery();

                    if (maTkObj != null)
                    {
                        SqlCommand cmdTK = new SqlCommand(GiaSuSql.XoaTaiKhoanById, conn);
                        cmdTK.Parameters.AddWithValue("@maTK", maTkObj);
                        cmdTK.ExecuteNonQuery();
                    }

                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public string KiemTraTrangThaiDuyet(string tenDangNhap)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(GiaSuSql.KiemTraTrangThaiDuyet, conn);
                cmd.Parameters.AddWithValue("@tk", tenDangNhap);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value || string.IsNullOrWhiteSpace(result.ToString()))
                {
                    return "ChuaCapNhat";
                }
                return result.ToString();
            }
        }

        public int LayMaGS(string tenDangNhap)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(GiaSuSql.LayMaGS, conn);
                cmd.Parameters.AddWithValue("@tk", tenDangNhap);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                return 0;
            }
        }
    }
}