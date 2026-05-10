using System;
using System.Data;
using Microsoft.Data.SqlClient;
using DoAnGiaSu_WinForms.Model;

namespace DoAnGiaSu_WinForms.DAL
{
    public class GiaSuDAL
    {
        DBConnection db = new DBConnection();

        // Lấy dữ liệu các bảng DM_... đổ vào ComboBox
        public DataTable LayDanhMuc(string tableName)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = $"SELECT * FROM {tableName}";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Gửi hồ sơ mới vào bảng GIASU
        public bool ThemGiaSu(GiaSu gs)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = @"INSERT INTO GIASU (HoTen, SDT, CCCD, MaGioiTinh, MaNamSinh, MaTruong, MaTrinhDo, TrangThaiDuyet, MaTK, AnhMinhChung, AnhBangDiem, AnhChungChi, MaNamHoc, MaChungChi, DiemChungChi, DiemGPA, MaXepLoai) 
                                VALUES (@hoten, @sdt, @cccd, @magt, @mans, @matruong, @matd, 'ChoDuyet', @matk, @anh, @anhbangdiem, @anhchungchi, @manamhoc, @machungchi, @diemchungchi, @diemgpa, @maxeploai)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@hoten", gs.HoTen);
                cmd.Parameters.AddWithValue("@sdt", gs.SDT);
                cmd.Parameters.AddWithValue("@cccd", gs.CCCD);
                cmd.Parameters.AddWithValue("@magt", gs.MaGioiTinh);
                cmd.Parameters.AddWithValue("@mans", gs.MaNamSinh);
                cmd.Parameters.AddWithValue("@matruong", gs.MaTruong);
                cmd.Parameters.AddWithValue("@matd", gs.MaTrinhDo);
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
                string query = "SELECT TOP 1 MaGS FROM GIASU WHERE MaTK = @matk ORDER BY MaGS DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
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
                string query = @"INSERT INTO CHITIET_CHUNGCHI_GS (MaGS, MaChungChi, DiemChungChi, AnhChungChi)
                                 VALUES (@maGS, @maCC, @diem, @anh)";
                SqlCommand cmd = new SqlCommand(query, conn);
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
                string query = "SELECT CCCD, SDT FROM GIASU WHERE CCCD = @cccd OR SDT = @sdt";
                SqlCommand cmd = new SqlCommand(query, conn);
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
                string query = @"SELECT GS.MaGS, GS.HoTen, GS.SDT, GS.CCCD,
                                GS.AnhMinhChung, GS.AnhBangDiem, GS.AnhChungChi,
                                ISNULL('GPA: ' + GS.DiemGPA, XL.TenXepLoai) AS ThanhTich, 
                                GS.MaNamHoc,
                                GS.MaChungChi, GS.DiemChungChi,
                                DMCC.TenChungChi,
                                T.TenTruong, TD.TenTrinhDo, GS.TrangThaiDuyet,
                                AVG(CAST(DG.SoSao AS FLOAT)) AS DiemTrungBinh,
                                COUNT(DG.MaDanhGia) AS SoLuotDanhGia,
                                CASE
                                    WHEN COUNT(DG.MaDanhGia) > 0 THEN
                                        CONVERT(NVARCHAR(10), CAST(ROUND(AVG(CAST(DG.SoSao AS FLOAT)), 1) AS DECIMAL(10,1)))
                                        + N' ⭐ (' + CAST(COUNT(DG.MaDanhGia) AS NVARCHAR(20)) + N' đánh giá)'
                                    ELSE N'Chưa có đánh giá'
                                END AS colRating
                                FROM GIASU GS
                                LEFT JOIN DM_TRUONG T ON GS.MaTruong = T.MaTruong
                                LEFT JOIN DM_TRINHDO TD ON GS.MaTrinhDo = TD.MaTrinhDo
                                LEFT JOIN DM_CHUNGCHI DMCC ON GS.MaChungChi = DMCC.MaChungChi
                                LEFT JOIN DM_XEPLOAI XL ON GS.MaXepLoai = XL.MaXepLoai
                                LEFT JOIN DANHGIA DG ON DG.MaGS = GS.MaGS
                                GROUP BY GS.MaGS, GS.HoTen, GS.SDT, GS.CCCD,
                                         GS.AnhMinhChung, GS.AnhBangDiem, GS.AnhChungChi,
                                         GS.DiemGPA, XL.TenXepLoai, GS.MaNamHoc,
                                         GS.MaChungChi, GS.DiemChungChi, DMCC.TenChungChi,
                                         T.TenTruong, TD.TenTrinhDo, GS.TrangThaiDuyet
                                ORDER BY
                                    CASE
                                        WHEN GS.TrangThaiDuyet = 'ChoDuyet' THEN 1
                                        WHEN GS.TrangThaiDuyet = 'DaDuyet' THEN 2
                                        ELSE 3
                                    END, GS.MaGS DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool CapNhatTrangThaiDuyet(int maGS, string trangThai)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = "UPDATE GIASU SET TrangThaiDuyet = @tt WHERE MaGS = @ma";
                SqlCommand cmd = new SqlCommand(query, conn);
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
                    string queryTutor = "SELECT MaTK FROM GIASU WHERE MaGS = @ma";
                    SqlCommand getMaTK = new SqlCommand(queryTutor, conn);
                    getMaTK.Parameters.AddWithValue("@ma", maGS);
                    object maTkObj = getMaTK.ExecuteScalar();

                    string queryGS = "DELETE FROM GIASU WHERE MaGS = @ma";
                    SqlCommand cmdGS = new SqlCommand(queryGS, conn);
                    cmdGS.Parameters.AddWithValue("@ma", maGS);
                    cmdGS.ExecuteNonQuery();

                    if(maTkObj != null)
                    {
                        string queryTK = "DELETE FROM TAIKHOAN WHERE MaTK = @maTK";
                        SqlCommand cmdTK = new SqlCommand(queryTK, conn);
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
                string query = @"SELECT gs.TrangThaiDuyet FROM GIASU gs
                                JOIN TAIKHOAN tk ON gs.MaTK = tk.MaTK
                                WHERE tk.TenDangNhap = @tk";
                SqlCommand cmd = new SqlCommand(query, conn);
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
                string query = @"SELECT gs.MaGS FROM GIASU gs
                                JOIN TAIKHOAN tk ON gs.MaTK = tk.MaTK
                                WHERE tk.TenDangNhap = @tk";
                SqlCommand cmd = new SqlCommand(query, conn);
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