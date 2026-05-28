using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.DataAccess
{
    public class DangKyNhanLopDAL
    {
        private readonly DBConnection db = new DBConnection();

        public bool VuotGioiHan4Lop(SqlConnection conn, SqlTransaction tran, int maGS)
        {
            string query = @"SELECT COUNT(*) 
                             FROM DANGKYNHANLOP DK
                             LEFT JOIN GIAODICH GD ON DK.MaBaiDang = GD.MaBaiDang AND DK.MaGS = GD.MaGS
                             WHERE DK.MaGS = @MaGS 
                             AND DK.TrangThai IN ('ChoDuyet', 'DaDuyet')
                             AND (
                                 GD.NgayXacNhan IS NULL 
                                 OR DATEDIFF(day, GD.NgayXacNhan, GETDATE()) < 30
                             )";
            using SqlCommand cmd = new SqlCommand(query, conn, tran);
            cmd.Parameters.Add(new SqlParameter("@MaGS", System.Data.SqlDbType.Int) { Value = maGS });
            int soLopDangChiemSlot = Convert.ToInt32(cmd.ExecuteScalar());
            return soLopDangChiemSlot >= 4;
        }

        public bool GiaSuNhanLop(int maBD, int maGS)
        {
            using SqlConnection conn = db.GetConnection();
            conn.Open();
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                if (VuotGioiHan4Lop(conn, tran, maGS))
                {
                    throw new Exception($"Gia sư MaGS={maGS} đã vượt giới hạn 4 lớp trong 30 ngày");
                }

                string queryCheck = @"SELECT COUNT(*)
                                      FROM DANGKYNHANLOP
                                      WHERE MaBaiDang = @ma AND MaGS = @maGS AND TrangThai IN ('ChoDuyet', 'DaDuyet')";
                SqlCommand cmdCheck = new SqlCommand(queryCheck, conn, tran);
                cmdCheck.Parameters.AddWithValue("@ma", maBD);
                cmdCheck.Parameters.AddWithValue("@maGS", maGS);
                int daDangKy = Convert.ToInt32(cmdCheck.ExecuteScalar());
                if (daDangKy > 0)
                {
                    throw new Exception($"Gia sư MaGS={maGS} đã đăng ký cho bài MaBaiDang={maBD}");
                }

                string queryInsert = @"INSERT INTO DANGKYNHANLOP (MaBaiDang, MaGS, TrangThai)
                                       VALUES (@ma, @maGS, 'ChoDuyet')";
                SqlCommand cmdInsert = new SqlCommand(queryInsert, conn, tran);
                cmdInsert.Parameters.AddWithValue("@ma", maBD);
                cmdInsert.Parameters.AddWithValue("@maGS", maGS);
                int soDongDangKy = cmdInsert.ExecuteNonQuery();

                string queryTrangThai = @"UPDATE BAIDANG
                                          SET TrangThai = 'ChoPhuHuynhDuyet'
                                          WHERE MaBaiDang = @ma
                                            AND MaGS IS NULL
                                            AND TrangThai IN ('ChuaGiao', 'ChoPhuHuynhDuyet')";
                SqlCommand cmdTrangThai = new SqlCommand(queryTrangThai, conn, tran);
                cmdTrangThai.Parameters.AddWithValue("@ma", maBD);
                cmdTrangThai.ExecuteNonQuery();

                tran.Commit();
                return soDongDangKy > 0;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
            finally
            {
                tran.Dispose();
            }
        }

        public bool PhuHuynhDuyetGiaSu(int maBD, int maGS)
        {
            using SqlConnection conn = db.GetConnection();
            conn.Open();
            DamBaoCotHanNopLaiBill(conn);
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                if (VuotGioiHan4Lop(conn, tran, maGS))
                {
                    throw new Exception($"Gia sư MaGS={maGS} đã vượt giới hạn 4 lớp trong 30 ngày");
                }

                string queryDuyet = @"UPDATE DANGKYNHANLOP
                                      SET TrangThai = CASE WHEN MaGS = @maGS THEN 'DaDuyet' ELSE 'TuChoi' END
                                      WHERE MaBaiDang = @ma";
                SqlCommand cmdDuyet = new SqlCommand(queryDuyet, conn, tran);
                cmdDuyet.Parameters.AddWithValue("@ma", maBD);
                cmdDuyet.Parameters.AddWithValue("@maGS", maGS);
                int duyetCount = cmdDuyet.ExecuteNonQuery();

                if (duyetCount == 0)
                {
                    throw new Exception($"Không tìm thấy đơn đăng ký của gia sư MaGS={maGS} cho bài MaBaiDang={maBD}");
                }

                string queryBaiDang = @"UPDATE BAIDANG
                                        SET TrangThai = 'DangGiaoDich', MaGS = @maGS, HanNopLaiBill = NULL
                                        WHERE MaBaiDang = @ma";
                SqlCommand cmdBaiDang = new SqlCommand(queryBaiDang, conn, tran);
                cmdBaiDang.Parameters.AddWithValue("@ma", maBD);
                cmdBaiDang.Parameters.AddWithValue("@maGS", maGS);
                int soDong = cmdBaiDang.ExecuteNonQuery();

                if (soDong == 0)
                {
                    throw new Exception($"Không tìm thấy bài đăng MaBaiDang={maBD}");
                }

                tran.Commit();
                return true;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
            finally
            {
                tran.Dispose();
            }
        }

        public bool PhuHuynhTuChoiGiaSu(int maBD, int maGS)
        {
            using SqlConnection conn = db.GetConnection();
            conn.Open();
            DamBaoCotHanNopLaiBill(conn);
            SqlTransaction tran = conn.BeginTransaction();
            try
            {
                string queryTuChoi = @"UPDATE DANGKYNHANLOP
                                       SET TrangThai = 'TuChoi'
                                       WHERE MaBaiDang = @ma AND MaGS = @maGS";
                SqlCommand cmdTuChoi = new SqlCommand(queryTuChoi, conn, tran);
                cmdTuChoi.Parameters.AddWithValue("@ma", maBD);
                cmdTuChoi.Parameters.AddWithValue("@maGS", maGS);
                cmdTuChoi.ExecuteNonQuery();

                string queryConCho = "SELECT COUNT(*) FROM DANGKYNHANLOP WHERE MaBaiDang = @ma AND TrangThai = 'ChoDuyet'";
                SqlCommand cmdConCho = new SqlCommand(queryConCho, conn, tran);
                cmdConCho.Parameters.AddWithValue("@ma", maBD);
                int soConCho = Convert.ToInt32(cmdConCho.ExecuteScalar());

                string trangThaiMoi = soConCho > 0 ? "ChoPhuHuynhDuyet" : "ChuaGiao";
                string queryBaiDang = @"UPDATE BAIDANG
                                        SET TrangThai = @trangThai, MaGS = NULL
                                        WHERE MaBaiDang = @ma AND TrangThai <> 'DaGiao'";
                SqlCommand cmdBaiDang = new SqlCommand(queryBaiDang, conn, tran);
                cmdBaiDang.Parameters.AddWithValue("@ma", maBD);
                cmdBaiDang.Parameters.AddWithValue("@trangThai", trangThaiMoi);
                int soDong = cmdBaiDang.ExecuteNonQuery();

                tran.Commit();
                return soDong > 0;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
            finally
            {
                tran.Dispose();
            }
        }

        public List<DangKyNhanLop> LayDangKyNhanLopTheoBai(int maBD)
        {
            var list = new List<DangKyNhanLop>();
            using SqlConnection conn = db.GetConnection();
            string sql = @"SELECT MaDangKy, MaBaiDang, MaGS, TrangThai, NgayDangKy FROM DANGKYNHANLOP WHERE MaBaiDang = @ma";
            using SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.SelectCommand.Parameters.AddWithValue("@ma", maBD);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow r in dt.Rows)
            {
                var dk = new DangKyNhanLop
                {
                    MaDangKy = r.Table.Columns.Contains("MaDangKy") && r["MaDangKy"] != DBNull.Value ? Convert.ToInt32(r["MaDangKy"]) : 0,
                    MaBaiDang = r.Table.Columns.Contains("MaBaiDang") && r["MaBaiDang"] != DBNull.Value ? Convert.ToInt32(r["MaBaiDang"]) : 0,
                    MaGS = r.Table.Columns.Contains("MaGS") && r["MaGS"] != DBNull.Value ? Convert.ToInt32(r["MaGS"]) : 0,
                    TrangThai = r.Table.Columns.Contains("TrangThai") ? r["TrangThai"]?.ToString() ?? "" : "",
                    NgayDangKy = r.Table.Columns.Contains("NgayDangKy") && r["NgayDangKy"] != DBNull.Value ? Convert.ToDateTime(r["NgayDangKy"]) : DateTime.MinValue
                };
                list.Add(dk);
            }
            return list;
        }

        private void DamBaoCotHanNopLaiBill(SqlConnection conn)
        {
            using SqlCommand cmdCheck = new SqlCommand("SELECT COL_LENGTH('BAIDANG', 'HanNopLaiBill')", conn);
            if (cmdCheck.ExecuteScalar() == DBNull.Value)
            {
                using SqlCommand cmdAlter = new SqlCommand("ALTER TABLE BAIDANG ADD HanNopLaiBill DATETIME NULL", conn);
                cmdAlter.ExecuteNonQuery();
            }
        }
    }
}
