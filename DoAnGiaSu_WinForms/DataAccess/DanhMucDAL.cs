using System.Data;
using Microsoft.Data.SqlClient;

namespace DoAnGiaSu_WinForms.DataAccess
{
    public class DanhMucDAL
    {
        private readonly DBConnection db = new DBConnection();

        private DataTable LayDanhMucTheoSql(string query)
        {
            using SqlConnection conn = db.GetConnection();
            using SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public DataTable LayMonHoc() => LayDanhMucTheoSql("SELECT MaMon, TenMon FROM DM_MONHOC ORDER BY TenMon");

        public DataTable LayLopHoc() => LayDanhMucTheoSql("SELECT MaLop, TenLop FROM DM_LOPHOC ORDER BY TenLop");

        public DataTable LayHinhThuc() => LayDanhMucTheoSql("SELECT MaHinhThuc, TenHinhThuc FROM DM_HINHTHUC ORDER BY TenHinhThuc");

        public DataTable LayTrinhDo() => LayDanhMucTheoSql("SELECT MaTrinhDo, TenTrinhDo FROM DM_TRINHDO ORDER BY TenTrinhDo");

        public DataTable LayQuanHuyen() => LayDanhMucTheoSql("SELECT MaQuan, TenQuan FROM DM_QUANHUYEN ORDER BY TenQuan");

        public DataTable LayGioiTinh() => LayDanhMucTheoSql("SELECT MaGioiTinh, TenGioiTinh FROM DM_GIOITINH ORDER BY TenGioiTinh");

        public DataTable LayNamSinh() => LayDanhMucTheoSql("SELECT MaNamSinh, Nam FROM DM_NAMSINH ORDER BY Nam");

        public DataTable LayTruong() => LayDanhMucTheoSql("SELECT MaTruong, TenTruong FROM DM_TRUONG ORDER BY TenTruong");

        public DataTable LayNamHoc() => LayDanhMucTheoSql("SELECT MaNamHoc, TenNamHoc FROM DM_NAMHOC ORDER BY TenNamHoc");

        public DataTable LayChungChi() => LayDanhMucTheoSql("SELECT MaChungChi, TenChungChi FROM DM_CHUNGCHI ORDER BY TenChungChi");

        public DataTable LayXepLoai() => LayDanhMucTheoSql("SELECT MaXepLoai, TenXepLoai FROM DM_XEPLOAI ORDER BY TenXepLoai");
    }
}
