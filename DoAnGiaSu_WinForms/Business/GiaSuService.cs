using System.Data;
using DoAnGiaSu_WinForms.DataAccess;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.Business
{
    public class GiaSuService
    {
        private readonly GiaSuDAL repository = new GiaSuDAL();

        public DataTable LayDanhMuc(string tableName) => repository.LayDanhMuc(tableName);

        public bool ThemGiaSu(GiaSu gs) => repository.ThemGiaSu(gs);

        public int LayMaGSMoiNhatTheoMaTK(int maTK) => repository.LayMaGSMoiNhatTheoMaTK(maTK);

        public bool ThemChiTietChungChiGiaSu(int maGS, int maChungChi, string diemChungChi, string anhChungChi) 
            => repository.ThemChiTietChungChiGiaSu(maGS, maChungChi, diemChungChi, anhChungChi);

        public string KiemTraTonTai(string cccd, string sdt) => repository.KiemTraTonTai(cccd, sdt);

        public DataTable LayTatCaGiaSuAdmin() => repository.LayTatCaGiaSuAdmin();

        public bool CapNhatTrangThaiDuyet(int maGS, string trangThai) => repository.CapNhatTrangThaiDuyet(maGS, trangThai);

        public bool XoaGiaSu(int maGS) => repository.XoaGiaSu(maGS);

        public string KiemTraTrangThaiDuyet(string tenDangNhap) => repository.KiemTraTrangThaiDuyet(tenDangNhap);

        public int LayMaGS(string tenDangNhap) => repository.LayMaGS(tenDangNhap);
    }
}
