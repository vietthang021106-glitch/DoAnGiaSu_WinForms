using System;
using System.Data;
using Microsoft.Data.SqlClient;
using DoAnGiaSu_WinForms.Models;
using System.Collections.Generic;

namespace DoAnGiaSu_WinForms.DataAccess
{
    public class BaiDangRepository
    {
        private readonly DBConnection db = new DBConnection();

        private bool VuotGioiHan4Lop(SqlConnection conn, SqlTransaction tran, int maGS)
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
            cmd.Parameters.Add(new SqlParameter("@MaGS", SqlDbType.Int) { Value = maGS });

            int soLopDangChiemSlot = Convert.ToInt32(cmd.ExecuteScalar());
            return soLopDangChiemSlot >= 4;
        }

        private void DamBaoCotHanNopLaiBill(SqlConnection conn)
        {
            SqlCommand cmdCheck = new SqlCommand("SELECT COL_LENGTH('BAIDANG', 'HanNopLaiBill')", conn);
            if (cmdCheck.ExecuteScalar() == DBNull.Value)
            {
                SqlCommand cmdAlter = new SqlCommand("ALTER TABLE BAIDANG ADD HanNopLaiBill DATETIME NULL", conn);
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
            SqlCommand cmdDangKy = new SqlCommand(capNhatDangKy, conn);
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
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
        }

        public bool ThemBaiDang(BaiDang bd)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = @"INSERT INTO BAIDANG (MaPH, YeuCauThem, MucLuong, TrangThai, MaMon, MaLop, MaHinhThuc, MaQuan, YeuCauTrinhDo, SoNhaDuong) 
                                VALUES (@maph, @yc, @luong, 'ChuaGiao', @mon, @lop, @ht, @quan, @yctrinhdo, @dc)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@maph", bd.MaPH);
                cmd.Parameters.AddWithValue("@yc", bd.YeuCauThem);
                cmd.Parameters.AddWithValue("@luong", bd.MucLuong);
                cmd.Parameters.AddWithValue("@mon", bd.MaMon);
                cmd.Parameters.AddWithValue("@lop", bd.MaLop);
                cmd.Parameters.AddWithValue("@ht", bd.MaHinhThuc);
                cmd.Parameters.AddWithValue("@quan", (object)bd.MaQuan ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@yctrinhdo", bd.YeuCauTrinhDo);
                cmd.Parameters.AddWithValue("@dc", bd.SoNhaDuong);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable LayBaiDangCuaPhuHuynh(int maPH)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                XuLyQuaHanNopLaiBill(conn);

                string query = @"SELECT bd.MaBaiDang, m.TenMon, l.TenLop, ht.TenHinhThuc, bd.MucLuong, bd.SoNhaDuong, bd.YeuCauThem, bd.TrangThai 
                                FROM BAIDANG bd
                                JOIN DM_MONHOC m ON bd.MaMon = m.MaMon
                                JOIN DM_LOPHOC l ON bd.MaLop = l.MaLop
                                LEFT JOIN DM_HINHTHUC ht ON bd.MaHinhThuc = ht.MaHinhThuc
                                WHERE bd.MaPH = @maph
                                ORDER BY bd.MaBaiDang DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@maph", maPH);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable LayTatCaBaiAdmin()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                XuLyQuaHanNopLaiBill(conn);

                string query = @"SELECT bd.MaBaiDang, ph.HoTen, m.TenMon, l.TenLop, ht.TenHinhThuc, q.TenQuan, bd.SoNhaDuong, bd.YeuCauThem, bd.MucLuong, bd.TrangThai, bd.MaGS
                                FROM BAIDANG bd
                                LEFT JOIN PHUHUYNH ph ON bd.MaPH = ph.MaPH
                                LEFT JOIN DM_MONHOC m ON bd.MaMon = m.MaMon
                                LEFT JOIN DM_LOPHOC l ON bd.MaLop = l.MaLop
                                LEFT JOIN DM_HINHTHUC ht ON bd.MaHinhThuc = ht.MaHinhThuc
                                LEFT JOIN DM_QUANHUYEN q ON bd.MaQuan = q.MaQuan
                                ORDER BY 
                                    CASE 
                                        WHEN bd.TrangThai = 'DangGiaoDich' THEN 1
                                        WHEN bd.TrangThai = 'ChuaGiao' THEN 2
                                        WHEN bd.TrangThai = 'DaGiao' THEN 3
                                        ELSE 4
                                    END, bd.MaBaiDang DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable LayTatCaBaiAdmin(string trangThai)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                XuLyQuaHanNopLaiBill(conn);

                string query = @"SELECT bd.MaBaiDang, ph.HoTen, m.TenMon, l.TenLop, ht.TenHinhThuc, q.TenQuan, bd.SoNhaDuong, bd.YeuCauThem, bd.MucLuong, bd.TrangThai, bd.MaGS
                                FROM BAIDANG bd
                                LEFT JOIN PHUHUYNH ph ON bd.MaPH = ph.MaPH
                                LEFT JOIN DM_MONHOC m ON bd.MaMon = m.MaMon
                                LEFT JOIN DM_LOPHOC l ON bd.MaLop = l.MaLop
                                LEFT JOIN DM_HINHTHUC ht ON bd.MaHinhThuc = ht.MaHinhThuc
                                LEFT JOIN DM_QUANHUYEN q ON bd.MaQuan = q.MaQuan
                                WHERE (@TrangThai = N'Tất cả' OR bd.TrangThai = @TrangThai)
                                ORDER BY 
                                    CASE 
                                        WHEN bd.TrangThai = 'DangGiaoDich' THEN 1
                                        WHEN bd.TrangThai = 'ChuaGiao' THEN 2
                                        WHEN bd.TrangThai = 'DaGiao' THEN 3
                                        ELSE 4
                                    END, bd.MaBaiDang DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@TrangThai", trangThai);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable LayLopMoiChoGiaSu(int maGS, string orderByClause)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                XuLyQuaHanNopLaiBill(conn);

                string query = @"SELECT bd.MaBaiDang, m.TenMon, l.TenLop, ht.TenHinhThuc, bd.MucLuong, 
                                ISNULL(q.TenQuan, N'Dạy Online/Chưa rõ') AS TenQuan, 
                                bd.YeuCauThem
                                FROM BAIDANG bd
                                JOIN DM_MONHOC m ON bd.MaMon = m.MaMon
                                JOIN DM_LOPHOC l ON bd.MaLop = l.MaLop
                                LEFT JOIN DM_HINHTHUC ht ON bd.MaHinhThuc = ht.MaHinhThuc
                                LEFT JOIN DM_QUANHUYEN q ON bd.MaQuan = q.MaQuan
                                WHERE bd.TrangThai IN ('ChuaGiao', 'ChoPhuHuynhDuyet')
                                  AND bd.MaGS IS NULL
                                  AND bd.YeuCauTrinhDo = (SELECT MaTrinhDo FROM GIASU WHERE MaGS = @maGS)
                                  AND NOT EXISTS (
                                      SELECT 1
                                      FROM DANGKYNHANLOP dk
                                      WHERE dk.MaBaiDang = bd.MaBaiDang
                                        AND dk.MaGS = @maGS
                                        AND dk.TrangThai IN ('ChoDuyet', 'DaDuyet')
                                  )" + orderByClause;
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@maGS", maGS);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool GiaSuNhanLop(int maBD, int maGS)
        {
            using (SqlConnection conn = db.GetConnection())
            {
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
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception($"Lỗi khi gia sư đăng ký lớp (MaBaiDang={maBD}, MaGS={maGS}): {ex.Message}", ex);
                }
                finally
                {
                    tran.Dispose();
                }
            }
        }

        public bool PhuHuynhDuyetGiaSu(int maBD, int maGS)
        {
            if (maBD <= 0)
                throw new ArgumentException("MaBaiDang phải lớn hơn 0", nameof(maBD));
            if (maGS <= 0)
                throw new ArgumentException("MaGS phải lớn hơn 0", nameof(maGS));

            using (SqlConnection conn = db.GetConnection())
            {
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
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception($"Lỗi khi duyệt gia sư (MaBaiDang={maBD}, MaGS={maGS}): {ex.Message}", ex);
                }
                finally
                {
                    tran.Dispose();
                }
            }
        }

        public bool PhuHuynhTuChoiGiaSu(int maBD, int maGS)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
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
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw new Exception($"Lỗi khi từ chối gia sư (MaBaiDang={maBD}, MaGS={maGS}): {ex.Message}", ex);
                }
                finally
                {
                    tran.Dispose();
                }
            }
        }

        public DataTable LayThongTinGiaSuDangKy(int maBD)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = @"SELECT dk.MaGS, gs.HoTen, gs.SDT, gs.CCCD,
                                tr.TenTruong, td.TenTrinhDo,
                                gs.AnhMinhChung, gs.AnhBangDiem,
                                ISNULL('GPA: ' + gs.DiemGPA, xl.TenXepLoai) AS ThanhTich, gs.MaNamHoc,
                                gs.MaChungChi, dmcc.TenChungChi, gs.DiemChungChi,
                                gt.TenGioiTinh, ns.Nam AS NamSinh, nh.TenNamHoc,
                                gd.TrangThaiDongPhi,
                                ISNULL(AVG(CAST(dg.SoSao AS FLOAT)), 0) AS DiemTB,
                                COUNT(dg.MaDanhGia) AS LuotDanhGia,
                                dk.TrangThai AS TrangThaiDangKy
                                FROM DANGKYNHANLOP dk
                                JOIN GIASU gs ON dk.MaGS = gs.MaGS
                                LEFT JOIN DM_TRUONG tr ON gs.MaTruong = tr.MaTruong
                                LEFT JOIN DM_TRINHDO td ON gs.MaTrinhDo = td.MaTrinhDo
                                LEFT JOIN DM_GIOITINH gt ON gs.MaGioiTinh = gt.MaGioiTinh
                                LEFT JOIN DM_NAMSINH ns ON gs.MaNamSinh = ns.MaNamSinh
                                LEFT JOIN DM_CHUNGCHI dmcc ON gs.MaChungChi = dmcc.MaChungChi
                                LEFT JOIN DM_NAMHOC nh ON gs.MaNamHoc = nh.MaNamHoc
                                LEFT JOIN DM_XEPLOAI xl ON gs.MaXepLoai = xl.MaXepLoai
                                LEFT JOIN GIAODICH gd ON dk.MaBaiDang = gd.MaBaiDang AND dk.MaGS = gd.MaGS
                                LEFT JOIN DANHGIA dg ON gs.MaGS = dg.MaGS
                                WHERE dk.MaBaiDang = @ma
                                GROUP BY dk.MaGS, gs.HoTen, gs.SDT, gs.CCCD,
                                         tr.TenTruong, td.TenTrinhDo, gs.AnhMinhChung, gs.AnhBangDiem,
                                         gs.DiemGPA, xl.TenXepLoai, gs.MaNamHoc, gs.MaChungChi, dmcc.TenChungChi,
                                         gs.DiemChungChi, gt.TenGioiTinh, ns.Nam, nh.TenNamHoc,
                                         gd.TrangThaiDongPhi, dk.TrangThai
                                ORDER BY CASE dk.TrangThai
                                            WHEN 'DaDuyet' THEN 1
                                            WHEN 'ChoDuyet' THEN 2
                                            ELSE 3
                                         END, dk.MaGS";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@ma", maBD);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public List<DangKyNhanLop> LayDangKyNhanLopTheoBai(int maBD)
        {
            var list = new List<DangKyNhanLop>();
            using (SqlConnection conn = db.GetConnection())
            {
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
            }
            return list;
        }

        public bool XacNhanHoaHong(int maBD)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = "UPDATE BAIDANG SET TrangThai = 'DaGiao', HanNopLaiBill = NULL WHERE MaBaiDang = @ma";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ma", maBD);
                conn.Open();
                DamBaoCotHanNopLaiBill(conn);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool TuChoiHoaHong(int maBD)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                DamBaoCotHanNopLaiBill(conn);

                string queryTrangThai = "UPDATE BAIDANG SET TrangThai = 'DangGiaoDich', HanNopLaiBill = DATEADD(DAY, 2, GETDATE()) WHERE MaBaiDang = @ma";
                SqlCommand cmdTrangThai = new SqlCommand(queryTrangThai, conn);
                cmdTrangThai.Parameters.AddWithValue("@ma", maBD);
                int soDong = cmdTrangThai.ExecuteNonQuery();

                string queryXoaAnh = "UPDATE GIAODICH SET AnhMinhChung = NULL WHERE MaBaiDang = @ma";
                SqlCommand cmdXoaAnh = new SqlCommand(queryXoaAnh, conn);
                cmdXoaAnh.Parameters.AddWithValue("@ma", maBD);
                cmdXoaAnh.ExecuteNonQuery();

                return soDong > 0;
            }
        }

        public bool XoaBaiDang(int maBD)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlTransaction tran = conn.BeginTransaction())
                    {
                        SqlCommand cmdXoaGiaoDich = new SqlCommand("DELETE FROM GIAODICH WHERE MaBaiDang = @ma", conn, tran);
                        cmdXoaGiaoDich.Parameters.AddWithValue("@ma", maBD);
                        cmdXoaGiaoDich.ExecuteNonQuery();

                        SqlCommand cmdXoaDangKy = new SqlCommand("DELETE FROM DANGKYNHANLOP WHERE MaBaiDang = @ma", conn, tran);
                        cmdXoaDangKy.Parameters.AddWithValue("@ma", maBD);
                        cmdXoaDangKy.ExecuteNonQuery();

                        SqlCommand cmdXoaBaiDang = new SqlCommand("DELETE FROM BAIDANG WHERE MaBaiDang = @ma", conn, tran);
                        cmdXoaBaiDang.Parameters.AddWithValue("@ma", maBD);
                        int soDong = cmdXoaBaiDang.ExecuteNonQuery();

                        tran.Commit();
                        return soDong > 0;
                    }
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool SuaBaiDang(BaiDang bd)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = @"UPDATE BAIDANG SET YeuCauThem = @yc, MucLuong = @luong, 
                                MaMon = @mon, MaLop = @lop, MaHinhThuc = @ht, MaQuan = @quan, YeuCauTrinhDo = @yctrinhdo, SoNhaDuong = @dc 
                                WHERE MaBaiDang = @ma";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ma", bd.MaBaiDang);
                cmd.Parameters.AddWithValue("@yc", bd.YeuCauThem);
                cmd.Parameters.AddWithValue("@luong", bd.MucLuong);
                cmd.Parameters.AddWithValue("@mon", bd.MaMon);
                cmd.Parameters.AddWithValue("@lop", bd.MaLop);
                cmd.Parameters.AddWithValue("@ht", bd.MaHinhThuc);
                cmd.Parameters.AddWithValue("@quan", (object)bd.MaQuan ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@yctrinhdo", bd.YeuCauTrinhDo);
                cmd.Parameters.AddWithValue("@dc", bd.SoNhaDuong);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable LayLopDaGiaoChoGiaSu(int maGS)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                XuLyQuaHanNopLaiBill(conn);

                string query = @"SELECT bd.MaBaiDang, m.TenMon, l.TenLop, ph.HoTen AS TenPH, 
                                 CASE WHEN bd.TrangThai = 'DaGiao' THEN ph.SDT ELSE N'*** (Chờ TTCK)' END AS SDT,
                                 ht.TenHinhThuc, q.TenQuan, 
                                 CASE WHEN bd.TrangThai = 'DaGiao' THEN bd.SoNhaDuong ELSE '***' END AS SoNhaDuong, 
                                 bd.MucLuong, bd.YeuCauThem,
                                 gd.TrangThaiDongPhi,
                                 ISNULL(dk.TrangThai, '') AS TrangThaiDangKy,
                                 CASE 
                                     WHEN dk.TrangThai = 'ChoDuyet' AND bd.TrangThai = 'ChoPhuHuynhDuyet' THEN N'Đã đăng ký - chờ PH duyệt'
                                     WHEN bd.TrangThai = 'DaGiao' THEN N'Đã Thanh Toán'
                                     WHEN gd.AnhMinhChung IS NOT NULL AND bd.TrangThai = 'DangGiaoDich' THEN N'Đợi admin duyệt'
                                     WHEN bd.TrangThai = 'DangGiaoDich' AND bd.MaGS = @maGS AND gd.AnhMinhChung IS NULL THEN N'Nhấp đúp để thanh toán'
                                     ELSE bd.TrangThai 
                                 END AS TrangThaiHienThi,
                                 bd.TrangThai,
                                 CASE WHEN gd.AnhMinhChung IS NOT NULL THEN 1 ELSE 0 END AS CoAnhChuyenKhoan
                                 FROM BAIDANG bd
                                 LEFT JOIN DANGKYNHANLOP dk ON bd.MaBaiDang = dk.MaBaiDang AND dk.MaGS = @maGS
                                 JOIN DM_MONHOC m ON bd.MaMon = m.MaMon
                                 JOIN DM_LOPHOC l ON bd.MaLop = l.MaLop
                                 JOIN PHUHUYNH ph ON bd.MaPH = ph.MaPH
                                 LEFT JOIN DM_HINHTHUC ht ON bd.MaHinhThuc = ht.MaHinhThuc
                                 LEFT JOIN DM_QUANHUYEN q ON bd.MaQuan = q.MaQuan
                                 LEFT JOIN GIAODICH gd ON bd.MaBaiDang = gd.MaBaiDang AND gd.MaGS = @maGS
                                 WHERE (bd.MaGS = @maGS AND bd.TrangThai IN ('DangGiaoDich', 'DaGiao'))
                                    OR (dk.TrangThai = 'ChoDuyet' AND bd.TrangThai = 'ChoPhuHuynhDuyet' AND bd.MaGS IS NULL)
                                 ORDER BY bd.MaBaiDang DESC";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@maGS", maGS);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool CapNhatAnhChuyenKhoan(int maBD, byte[] anhCK)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                try
                {
                    conn.Open();
                    DamBaoCotHanNopLaiBill(conn);

                    string update = "UPDATE GIAODICH SET AnhMinhChung = @anh WHERE MaBaiDang = @ma";
                    SqlCommand updateCmd = new SqlCommand(update, conn);
                    updateCmd.Parameters.Add("@anh", SqlDbType.VarBinary, -1).Value = anhCK;
                    updateCmd.Parameters.AddWithValue("@ma", maBD);
                    int soDongCapNhat = updateCmd.ExecuteNonQuery();

                    if (soDongCapNhat > 0)
                    {
                        SqlCommand clearDeadlineCmd = new SqlCommand("UPDATE BAIDANG SET HanNopLaiBill = NULL WHERE MaBaiDang = @ma", conn);
                        clearDeadlineCmd.Parameters.AddWithValue("@ma", maBD);
                        clearDeadlineCmd.ExecuteNonQuery();
                        return true;
                    }

                    string insert = "INSERT INTO GIAODICH (MaBaiDang, AnhMinhChung) VALUES (@ma, @anh)";
                    SqlCommand insertCmd = new SqlCommand(insert, conn);
                    insertCmd.Parameters.AddWithValue("@ma", maBD);
                    insertCmd.Parameters.Add("@anh", SqlDbType.VarBinary, -1).Value = anhCK;
                    bool insertOk = insertCmd.ExecuteNonQuery() > 0;
                    if (insertOk)
                    {
                        SqlCommand clearDeadlineCmd = new SqlCommand("UPDATE BAIDANG SET HanNopLaiBill = NULL WHERE MaBaiDang = @ma", conn);
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
        }

        public byte[] LayAnhChuyenKhoanTheoBaiDang(int maBD)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                string query = @"SELECT TOP 1 AnhMinhChung
                                FROM GIAODICH
                                WHERE MaBaiDang = @ma AND AnhMinhChung IS NOT NULL
                                ORDER BY DATALENGTH(AnhMinhChung) DESC";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ma", maBD);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    if (result is byte[] bytes)
                    {
                        return bytes;
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
        }

        public bool CapNhatNhanh(int maBD, string cot, string giaTri)
        {
            if (cot != "MucLuong") return false;

            using (SqlConnection conn = db.GetConnection())
            {
                string query = $"UPDATE BAIDANG SET {cot} = @val WHERE MaBaiDang = @ma";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@val", giaTri);
                cmd.Parameters.AddWithValue("@ma", maBD);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public DataTable LayBaiDangCuaPhuHuynhChiTiet(int maPH)
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                const string sql = @"SELECT bd.MaBaiDang, m.TenMon, l.TenLop, td.TenTrinhDo, ht.TenHinhThuc, 
                                                bd.MucLuong, bd.SoNhaDuong, bd.YeuCauThem, bd.TrangThai, q.TenQuan
                                         FROM BAIDANG bd
                                         JOIN DM_MONHOC m ON bd.MaMon = m.MaMon
                                         JOIN DM_LOPHOC l ON bd.MaLop = l.MaLop
                                         LEFT JOIN DM_TRINHDO td ON bd.YeuCauTrinhDo = td.MaTrinhDo
                                         LEFT JOIN DM_HINHTHUC ht ON bd.MaHinhThuc = ht.MaHinhThuc
                                         LEFT JOIN DM_QUANHUYEN q ON bd.MaQuan = q.MaQuan
                                         WHERE bd.MaPH = @maPH
                                         ORDER BY bd.MaBaiDang DESC";

                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@maPH", SqlDbType.Int) { Value = maPH });

                using SqlDataReader reader = cmd.ExecuteReader();
                System.Data.DataTable dt = new System.Data.DataTable();
                dt.Load(reader);
                return dt;
            }
        }

        public DataTable LayThongTinGiaSuTuBaiDangKhiThieuDangKy(int maBD)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = db.GetConnection())
            {
                const string sql = @"SELECT bd.MaGS, gs.HoTen, gs.SDT, gs.CCCD,
                                     tr.TenTruong, td.TenTrinhDo,
                                     gs.AnhMinhChung, gs.AnhBangDiem,
                                     ISNULL('GPA: ' + gs.DiemGPA, xl.TenXepLoai) AS ThanhTich, gs.MaNamHoc,
                                     gs.MaChungChi, dmcc.TenChungChi, gs.DiemChungChi,
                                     gt.TenGioiTinh, ns.Nam AS NamSinh,
                                     CAST('DaDuyet' AS NVARCHAR(20)) AS TrangThaiDangKy
                                     FROM BAIDANG bd
                                     JOIN GIASU gs ON bd.MaGS = gs.MaGS
                                     LEFT JOIN DM_TRUONG tr ON gs.MaTruong = tr.MaTruong
                                     LEFT JOIN DM_TRINHDO td ON gs.MaTrinhDo = td.MaTrinhDo
                                     LEFT JOIN DM_GIOITINH gt ON gs.MaGioiTinh = gt.MaGioiTinh
                                     LEFT JOIN DM_NAMSINH ns ON gs.MaNamSinh = ns.MaNamSinh
                                     LEFT JOIN DM_CHUNGCHI dmcc ON gs.MaChungChi = dmcc.MaChungChi
                                     LEFT JOIN DM_XEPLOAI xl ON gs.MaXepLoai = xl.MaXepLoai
                                     WHERE bd.MaBaiDang = @ma";

                using SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.Add(new SqlParameter("@ma", SqlDbType.Int) { Value = maBD });
                da.Fill(dt);
            }

            return dt;
        }

        public DataTable LayTatCaGiaSuAdmin()
        {
            using (SqlConnection conn = db.GetConnection())
            {
                conn.Open();
                string query = @"SELECT gs.MaGS, gs.HoTen, gs.SDT, gs.CCCD,
                                       gs.AnhMinhChung, gs.AnhBangDiem, gs.AnhChungChi,
                                       ISNULL('GPA: ' + gs.DiemGPA, xl.TenXepLoai) AS ThanhTich,
                                       gt.TenGioiTinh, ns.Nam AS NamSinh, nh.TenNamHoc,
                                       ISNULL(CC.TenChungChi + ': ' + gs.DiemChungChi, '') AS ThongTinChungChi,
                                       tr.TenTruong, td.TenTrinhDo, gs.TrangThaiDuyet
                                FROM GIASU gs
                                LEFT JOIN DM_XEPLOAI xl ON gs.MaXepLoai = xl.MaXepLoai
                                LEFT JOIN DM_GIOITINH gt ON gs.MaGioiTinh = gt.MaGioiTinh
                                LEFT JOIN DM_NAMSINH ns ON gs.MaNamSinh = ns.MaNamSinh
                                LEFT JOIN DM_NAMHOC nh ON gs.MaNamHoc = nh.MaNamHoc
                                LEFT JOIN DM_TRUONG tr ON gs.MaTruong = tr.MaTruong
                                LEFT JOIN DM_TRINHDO td ON gs.MaTrinhDo = td.MaTrinhDo
                                LEFT JOIN DM_CHUNGCHI CC ON gs.MaChungChi = CC.MaChungChi
                                ORDER BY gs.MaGS DESC";

                using SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public DataTable LayBaiChoDuyetPhi()
        {
            var gdDal = new GiaoDichDAL();
            return gdDal.LayBaiChoDuyetPhi();
        }
    }
}
