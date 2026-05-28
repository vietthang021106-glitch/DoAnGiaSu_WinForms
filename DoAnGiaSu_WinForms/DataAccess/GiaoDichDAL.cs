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

        private void DamBaoCotTrangThaiGiaoDich(SqlConnection conn)
        {
            using SqlCommand cmdCheck = new SqlCommand("SELECT COL_LENGTH('GIAODICH', 'TrangThai')", conn);
            if (cmdCheck.ExecuteScalar() == DBNull.Value)
            {
                using SqlCommand cmdAlter = new SqlCommand("ALTER TABLE GIAODICH ADD TrangThai NVARCHAR(50) NULL DEFAULT 'ChuaNop'", conn);
                cmdAlter.ExecuteNonQuery();
            }
        }

        private void DamBaoCotNgayXacNhanGiaoDich(SqlConnection conn)
        {
            using SqlCommand cmdCheck = new SqlCommand("SELECT COL_LENGTH('GIAODICH', 'NgayXacNhan')", conn);
            if (cmdCheck.ExecuteScalar() == DBNull.Value)
            {
                using SqlCommand cmdAlter = new SqlCommand("ALTER TABLE GIAODICH ADD NgayXacNhan DATETIME NULL", conn);
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

        public bool CapNhatAnhChuyenKhoan(int maBD, byte[] anhCK)
        {
            using SqlConnection conn = db.GetConnection();
            try
            {
                conn.Open();
                DamBaoCotHanNopLaiBill(conn);
                DamBaoCotTrangThaiGiaoDich(conn);

                using SqlCommand updateCmd = new SqlCommand("UPDATE GIAODICH SET AnhMinhChung = @anh, TrangThai = 'ChoAdminDuyet' WHERE MaBaiDang = @ma", conn);
                updateCmd.Parameters.Add("@anh", SqlDbType.VarBinary, -1).Value = anhCK;
                updateCmd.Parameters.AddWithValue("@ma", maBD);
                int soDongCapNhat = updateCmd.ExecuteNonQuery();

                if (soDongCapNhat > 0)
                {
                    using SqlCommand clearDeadlineCmd = new SqlCommand("UPDATE BAIDANG SET HanNopLaiBill = NULL WHERE MaBaiDang = @ma", conn);
                    clearDeadlineCmd.Parameters.AddWithValue("@ma", maBD);
                    clearDeadlineCmd.ExecuteNonQuery();
                    return true;
                }

                using SqlCommand insertCmd = new SqlCommand("INSERT INTO GIAODICH (MaBaiDang, AnhMinhChung, TrangThai) VALUES (@ma, @anh, 'ChoAdminDuyet')", conn);
                insertCmd.Parameters.AddWithValue("@ma", maBD);
                insertCmd.Parameters.Add("@anh", SqlDbType.VarBinary, -1).Value = anhCK;
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
                                                    ORDER BY DATALENGTH(AnhMinhChung) DESC", conn);
            cmd.Parameters.AddWithValue("@ma", maBD);
            conn.Open();
            object result = cmd.ExecuteScalar();
            if (result is byte[] bytes)
            {
                return bytes;
            }

            return null;
        }

        public DataTable LayBaiChoDuyetPhi()
        {
            using SqlConnection conn = db.GetConnection();
            conn.Open();
            XuLyQuaHanNopLaiBill(conn);
            DamBaoCotTrangThaiGiaoDich(conn);

            using SqlDataAdapter da = new SqlDataAdapter(@"SELECT bd.MaBaiDang, ph.HoTen AS TenPhuHuynh, m.TenMon, bd.MucLuong, CAST(bd.MucLuong * 2 AS INT) AS HoaHong, 
                                                             COALESCE(gd.TrangThai, bd.TrangThai) AS TrangThai, 
                                                             gd.AnhMinhChung AS AnhChuyenKhoan, bd.MaGS
                                                             FROM BAIDANG bd
                                                             JOIN PHUHUYNH ph ON bd.MaPH = ph.MaPH
                                                             JOIN DM_MONHOC m ON bd.MaMon = m.MaMon
                                                             LEFT JOIN (
                                                                 SELECT TOP 1 g.MaBaiDang, g.AnhMinhChung, g.TrangThai
                                                                 FROM GIAODICH g
                                                                 WHERE g.AnhMinhChung IS NOT NULL
                                                                 ORDER BY DATALENGTH(g.AnhMinhChung) DESC
                                                             ) gd ON bd.MaBaiDang = gd.MaBaiDang
                                                             WHERE bd.TrangThai = N'DangGiaoDich' OR gd.TrangThai = N'ChoAdminDuyet'", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public bool XacNhanHoaHong(int maBD)
        {
            using SqlConnection conn = db.GetConnection();
            try
            {
                conn.Open();
                DamBaoCotHanNopLaiBill(conn);
                DamBaoCotTrangThaiGiaoDich(conn);
                DamBaoCotNgayXacNhanGiaoDich(conn);

                using SqlCommand cmdGiaoDich = new SqlCommand("UPDATE GIAODICH SET TrangThai = 'DaGiao', NgayXacNhan = GETDATE() WHERE MaBaiDang = @ma", conn);
                cmdGiaoDich.Parameters.AddWithValue("@ma", maBD);
                cmdGiaoDich.ExecuteNonQuery();

                using SqlCommand cmdBaiDang = new SqlCommand("UPDATE BAIDANG SET TrangThai = 'DaGiao', HanNopLaiBill = NULL WHERE MaBaiDang = @ma", conn);
                cmdBaiDang.Parameters.AddWithValue("@ma", maBD);
                return cmdBaiDang.ExecuteNonQuery() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool TuChoiHoaHong(int maBD)
        {
            using SqlConnection conn = db.GetConnection();
            try
            {
                conn.Open();
                DamBaoCotHanNopLaiBill(conn);
                DamBaoCotTrangThaiGiaoDich(conn);

                using SqlCommand cmdTrangThai = new SqlCommand("UPDATE BAIDANG SET TrangThai = 'DangGiaoDich', HanNopLaiBill = DATEADD(DAY, 2, GETDATE()) WHERE MaBaiDang = @ma", conn);
                cmdTrangThai.Parameters.AddWithValue("@ma", maBD);
                int soDong = cmdTrangThai.ExecuteNonQuery();

                using SqlCommand cmdXoaAnh = new SqlCommand("UPDATE GIAODICH SET AnhMinhChung = NULL, TrangThai = 'ChuaNop' WHERE MaBaiDang = @ma", conn);
                cmdXoaAnh.Parameters.AddWithValue("@ma", maBD);
                cmdXoaAnh.ExecuteNonQuery();

                return soDong > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
