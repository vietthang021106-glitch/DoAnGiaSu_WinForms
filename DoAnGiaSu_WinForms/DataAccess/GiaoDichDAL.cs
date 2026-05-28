using System;
using System.Data;
using Microsoft.Data.SqlClient;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.DataAccess
{
    public class GiaoDichDAL
    {
        private readonly DBConnection db = new DBConnection();

        private void DamBaoCotHanNopLaiBill(SqlConnection conn)
        {
            using SqlCommand cmdCheck = new SqlCommand("SELECT COL_LENGTH('BAIDANG', 'HanNopLaiBill')", conn);
            if (cmdCheck.ExecuteScalar() == DBNull.Value)
            {
                using SqlCommand cmdAlter = new SqlCommand("ALTER TABLE BAIDANG ADD HanNopLaiBill DATETIME NULL", conn);
                cmdAlter.ExecuteNonQuery();
            }
        }

        private void XuLyQuaHanNopLaiBill(SqlConnection conn)
        {
            DamBaoCotHanNopLaiBill(conn);

            string capNhatDangKy = @"UPDATE dk
                                    SET dk.TrangThai = 'TuChoi'
                                    FROM DANGKYNHANLOP dk
                                    JOIN BAIDANG bd ON bd.MaBaiDang = dk.MaBaiDang
                                    WHERE bd.TrangThai = 'DangGiaoDich'
                                      AND bd.HanNopLaiBill IS NOT NULL
                                      AND bd.HanNopLaiBill < GETDATE()
                                      AND dk.TrangThai = 'DaDuyet'
                                      AND NOT EXISTS (
                                          SELECT 1
                                          FROM GIAODICH gd
                                          WHERE gd.MaBaiDang = bd.MaBaiDang
                                            AND gd.AnhMinhChung IS NOT NULL
                                      )";
            using SqlCommand cmdDangKy = new SqlCommand(capNhatDangKy, conn);
            cmdDangKy.ExecuteNonQuery();

            string query = @"UPDATE bd
                            SET bd.TrangThai = 'ChuaGiao',
                                bd.MaGS = NULL,
                                bd.HanNopLaiBill = NULL
                            FROM BAIDANG bd
                            WHERE bd.TrangThai = 'DangGiaoDich'
                              AND bd.HanNopLaiBill IS NOT NULL
                              AND bd.HanNopLaiBill < GETDATE()
                              AND NOT EXISTS (
                                  SELECT 1
                                  FROM GIAODICH gd
                                  WHERE gd.MaBaiDang = bd.MaBaiDang
                                    AND gd.AnhMinhChung IS NOT NULL
                              )";
            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
        }

        public bool CapNhatAnhChuyenKhoan(int maBD, int maGS, decimal soTienPhi, byte[] anhCK)
        {
            using SqlConnection conn = db.GetConnection();
            try
            {
                conn.Open();
                DamBaoCotHanNopLaiBill(conn);

                string base64 = Convert.ToBase64String(anhCK);

                using SqlCommand updateCmd = new SqlCommand("UPDATE GIAODICH SET MaGS = @maGS, SoTienPhi = @soTien, AnhMinhChung = @anh, TrangThaiDongPhi = 'ChoAdminDuyet' WHERE MaBaiDang = @ma", conn);
                updateCmd.Parameters.AddWithValue("@maGS", maGS);
                updateCmd.Parameters.AddWithValue("@soTien", soTienPhi);
                updateCmd.Parameters.AddWithValue("@ma", maBD);
                updateCmd.Parameters.Add("@anh", SqlDbType.NVarChar, -1).Value = base64;
                int soDongCapNhat = updateCmd.ExecuteNonQuery();

                if (soDongCapNhat > 0)
                {
                    using SqlCommand clearDeadlineCmd = new SqlCommand("UPDATE BAIDANG SET HanNopLaiBill = NULL WHERE MaBaiDang = @ma", conn);
                    clearDeadlineCmd.Parameters.AddWithValue("@ma", maBD);
                    clearDeadlineCmd.ExecuteNonQuery();
                    return true;
                }

                using SqlCommand insertCmd = new SqlCommand("INSERT INTO GIAODICH (MaBaiDang, MaGS, SoTienPhi, AnhMinhChung, TrangThaiDongPhi) VALUES (@ma, @maGS, @soTien, @anh, 'ChoAdminDuyet')", conn);
                insertCmd.Parameters.AddWithValue("@ma", maBD);
                insertCmd.Parameters.AddWithValue("@maGS", maGS);
                insertCmd.Parameters.AddWithValue("@soTien", soTienPhi);
                insertCmd.Parameters.Add("@anh", SqlDbType.NVarChar, -1).Value = base64;
                bool insertOk = insertCmd.ExecuteNonQuery() > 0;
                if (insertOk)
                {
                    using SqlCommand clearDeadlineCmd = new SqlCommand("UPDATE BAIDANG SET HanNopLaiBill = NULL WHERE MaBaiDang = @ma", conn);
                    clearDeadlineCmd.Parameters.AddWithValue("@ma", maBD);
                    clearDeadlineCmd.ExecuteNonQuery();
                }

                return insertOk;
            }
            catch
            {
                return false;
            }
        }

        public byte[] LayAnhChuyenKhoanTheoBaiDang(int maBD)
        {
            using SqlConnection conn = db.GetConnection();
            using SqlCommand cmd = new SqlCommand(@"SELECT TOP 1 AnhMinhChung
                                                    FROM GIAODICH
                                                    WHERE MaBaiDang = @ma AND AnhMinhChung IS NOT NULL
                                                    ORDER BY LEN(AnhMinhChung) DESC", conn);
            cmd.Parameters.AddWithValue("@ma", maBD);
            conn.Open();
            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                if (result is byte[] bytesRes)
                {
                    return bytesRes;
                }
                string s = result as string;
                if (!string.IsNullOrWhiteSpace(s))
                {
                    int comma = s.IndexOf(',');
                    if (comma >= 0 && comma + 1 < s.Length) s = s.Substring(comma + 1);
                    try
                    {
                        return Convert.FromBase64String(s);
                    }
                    catch
                    {
                        try
                        {
                            return System.Text.Encoding.Default.GetBytes(s);
                        }
                        catch
                        {
                            return null;
                        }
                    }
                }
            }

            return null;
        }

        public DataTable LayBaiChoDuyetPhi()
        {
            using SqlConnection conn = db.GetConnection();
            conn.Open();
            XuLyQuaHanNopLaiBill(conn);

            using SqlDataAdapter da = new SqlDataAdapter(@"SELECT bd.MaBaiDang, ph.HoTen AS TenPhuHuynh, m.TenMon, bd.MucLuong, CAST(bd.MucLuong * 2 AS INT) AS HoaHong, bd.TrangThai, gd.AnhMinhChung AS AnhChuyenKhoan, bd.MaGS 
                                                            FROM BAIDANG bd
                                                            JOIN PHUHUYNH ph ON bd.MaPH = ph.MaPH
                                                            JOIN DM_MONHOC m ON bd.MaMon = m.MaMon
                                                            OUTER APPLY (
                                                                SELECT TOP 1 g.AnhMinhChung
                                                                FROM GIAODICH g
                                                                WHERE g.MaBaiDang = bd.MaBaiDang AND g.AnhMinhChung IS NOT NULL
                                                                ORDER BY DATALENGTH(g.AnhMinhChung) DESC
                                                            ) gd
                                                            WHERE bd.TrangThai = 'DangGiaoDich'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool XacNhanHoaHong(int maBD)
        {
            using SqlConnection conn = db.GetConnection();
            conn.Open();
            DamBaoCotHanNopLaiBill(conn);
            using SqlCommand cmd = new SqlCommand("UPDATE BAIDANG SET TrangThai = 'DaGiao', HanNopLaiBill = NULL WHERE MaBaiDang = @ma", conn);
            cmd.Parameters.AddWithValue("@ma", maBD);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool TuChoiHoaHong(int maBD)
        {
            using SqlConnection conn = db.GetConnection();
            conn.Open();
            DamBaoCotHanNopLaiBill(conn);

            using SqlCommand cmdTrangThai = new SqlCommand("UPDATE BAIDANG SET TrangThai = 'DangGiaoDich', HanNopLaiBill = DATEADD(DAY, 2, GETDATE()) WHERE MaBaiDang = @ma", conn);
            cmdTrangThai.Parameters.AddWithValue("@ma", maBD);
            int soDong = cmdTrangThai.ExecuteNonQuery();

            using SqlCommand cmdXoaAnh = new SqlCommand("UPDATE GIAODICH SET AnhMinhChung = NULL WHERE MaBaiDang = @ma", conn);
            cmdXoaAnh.Parameters.AddWithValue("@ma", maBD);
            cmdXoaAnh.ExecuteNonQuery();

            return soDong > 0;
        }
    }
}
