namespace DoAnGiaSu_WinForms.Models
{
    public class GiaSu
    {
        public int MaGS { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public string CCCD { get; set; }
        public int MaGioiTinh { get; set; }
        public int MaNamSinh { get; set; }
        public int MaTruong { get; set; }
        public int MaTrinhDo { get; set; }
        public string TrangThaiDuyet { get; set; }
        public int MaTK { get; set; }
        public string AnhMinhChung { get; set; }
        public string AnhBangDiem { get; set; }
        public string AnhChungChi { get; set; }
        public int? MaNamHoc { get; set; }

        public int? MaChungChi { get; set; }
        public string DiemChungChi { get; set; }
        public string DiemGPA { get; set; }
        public int? MaXepLoai { get; set; }
    }

    public static class GiaSuSql
    {
        public const string TrangThaiChoDuyet = "ChoDuyet";
        public const string LayDanhMuc = "SELECT * FROM {0}";
        public const string ThemGiaSu = @"INSERT INTO GIASU (HoTen, SDT, CCCD, MaGioiTinh, MaNamSinh, MaTruong, MaTrinhDo, TrangThaiDuyet, MaTK, AnhMinhChung, AnhBangDiem, AnhChungChi, MaNamHoc, MaChungChi, DiemChungChi, DiemGPA, MaXepLoai) 
                                VALUES (@hoten, @sdt, @cccd, @magt, @mans, @matruong, @matd, @trangthai, @matk, @anh, @anhbangdiem, @anhchungchi, @manamhoc, @machungchi, @diemchungchi, @diemgpa, @maxeploai)";
        public const string LayMaGSMoiNhatTheoMaTK = "SELECT TOP 1 MaGS FROM GIASU WHERE MaTK = @matk ORDER BY MaGS DESC";
        public const string ThemChiTietChungChiGiaSu = @"INSERT INTO CHITIET_CHUNGCHI_GS (MaGS, MaChungChi, DiemChungChi, AnhChungChi)
                                 VALUES (@maGS, @maCC, @diem, @anh)";
        public const string KiemTraTonTai = "SELECT CCCD, SDT FROM GIASU WHERE CCCD = @cccd OR SDT = @sdt";
        public const string LayTatCaGiaSuAdmin = @"SELECT GS.MaGS, GS.HoTen, GS.SDT, GS.CCCD,
                                GS.AnhMinhChung, GS.AnhBangDiem, GS.AnhChungChi,
                                ISNULL('GPA: ' + GS.DiemGPA, XL.TenXepLoai) AS ThanhTich,
                                GT.TenGioiTinh, NS.Nam, NH.TenNamHoc,
                                ISNULL(CC.TenChungChi + ': ' + GS.DiemChungChi, '') AS ThongTinChungChi,
                                GS.MaNamHoc, GS.MaChungChi, GS.DiemChungChi, DMCC.TenChungChi,
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
                                LEFT JOIN DM_GIOITINH GT ON GS.MaGioiTinh = GT.MaGioiTinh
                                LEFT JOIN DM_NAMSINH NS ON GS.MaNamSinh = NS.MaNamSinh
                                LEFT JOIN DM_NAMHOC NH ON GS.MaNamHoc = NH.MaNamHoc
                                LEFT JOIN DM_TRUONG T ON GS.MaTruong = T.MaTruong
                                LEFT JOIN DM_TRINHDO TD ON GS.MaTrinhDo = TD.MaTrinhDo
                                LEFT JOIN DM_CHUNGCHI DMCC ON GS.MaChungChi = DMCC.MaChungChi
                                LEFT JOIN DM_CHUNGCHI CC ON GS.MaChungChi = CC.MaChungChi
                                LEFT JOIN DM_XEPLOAI XL ON GS.MaXepLoai = XL.MaXepLoai
                                LEFT JOIN DANHGIA DG ON DG.MaGS = GS.MaGS
                                GROUP BY GS.MaGS, GS.HoTen, GS.SDT, GS.CCCD,
                                         GS.AnhMinhChung, GS.AnhBangDiem, GS.AnhChungChi,
                                         GS.DiemGPA, XL.TenXepLoai, GT.TenGioiTinh, NS.Nam, NH.TenNamHoc,
                                         CC.TenChungChi, GS.DiemChungChi, GS.MaNamHoc,
                                         GS.MaChungChi, DMCC.TenChungChi,
                                         T.TenTruong, TD.TenTrinhDo, GS.TrangThaiDuyet
                                ORDER BY
                                    CASE
                                        WHEN GS.TrangThaiDuyet = 'ChoDuyet' THEN 1
                                        WHEN GS.TrangThaiDuyet = 'DaDuyet' THEN 2
                                        ELSE 3
                                    END, GS.MaGS DESC";
        public const string CapNhatTrangThaiDuyet = "UPDATE GIASU SET TrangThaiDuyet = @tt WHERE MaGS = @ma";
        public const string LayMaTKTheoMaGS = "SELECT MaTK FROM GIASU WHERE MaGS = @ma";
        public const string XoaGiaSuById = "DELETE FROM GIASU WHERE MaGS = @ma";
        public const string XoaTaiKhoanById = "DELETE FROM TAIKHOAN WHERE MaTK = @maTK";
        public const string KiemTraTrangThaiDuyet = @"SELECT gs.TrangThaiDuyet FROM GIASU gs
                                JOIN TAIKHOAN tk ON gs.MaTK = tk.MaTK
                                WHERE tk.TenDangNhap = @tk";
        public const string LayMaGS = @"SELECT gs.MaGS FROM GIASU gs
                                JOIN TAIKHOAN tk ON gs.MaTK = tk.MaTK
                                WHERE tk.TenDangNhap = @tk";
    }
}