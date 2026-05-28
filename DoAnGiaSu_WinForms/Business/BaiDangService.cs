using System.Data;
using DoAnGiaSu_WinForms.DataAccess;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.Business
{
    public class BaiDangService
    {
        private readonly BaiDangRepository repository = new BaiDangRepository();

        public bool ThemBaiDang(BaiDang bd) => repository.ThemBaiDang(bd);

        public DataTable LayBaiDangCuaPhuHuynh(int maPH) => repository.LayBaiDangCuaPhuHuynh(maPH);

        public DataTable LayTatCaBaiAdmin() => repository.LayTatCaBaiAdmin();

        public DataTable LayTatCaBaiAdmin(string trangThai) => repository.LayTatCaBaiAdmin(trangThai);

        public DataTable LayLopMoiChoGiaSu(int maGS, string orderByClause) => repository.LayLopMoiChoGiaSu(maGS, orderByClause);

        public bool GiaSuNhanLop(int maBD, int maGS) => repository.GiaSuNhanLop(maBD, maGS);

        public bool PhuHuynhDuyetGiaSu(int maBD, int maGS) => repository.PhuHuynhDuyetGiaSu(maBD, maGS);

        public bool PhuHuynhTuChoiGiaSu(int maBD, int maGS) => repository.PhuHuynhTuChoiGiaSu(maBD, maGS);

        public DataTable LayThongTinGiaSuDangKy(int maBD) => repository.LayThongTinGiaSuDangKy(maBD);

        public DataTable LayLopDaGiaoChoGiaSu(int maGS) => repository.LayLopDaGiaoChoGiaSu(maGS);

        public bool XoaBaiDang(int maBD) => repository.XoaBaiDang(maBD);

        public bool SuaBaiDang(BaiDang bd) => repository.SuaBaiDang(bd);

        public DataTable LayBaiDangCuaPhuHuynhChiTiet(int maPH) => repository.LayBaiDangCuaPhuHuynhChiTiet(maPH);

        public DataTable LayThongTinGiaSuTuBaiDangKhiThieuDangKy(int maBD) => repository.LayThongTinGiaSuTuBaiDangKhiThieuDangKy(maBD);

        public DataTable LayTatCaGiaSuAdmin() => repository.LayTatCaGiaSuAdmin();

        public DataTable LayBaiChoDuyetPhi() => new GiaoDichDAL().LayBaiChoDuyetPhi();

        public bool XacNhanHoaHong(int maBD) => new GiaoDichDAL().XacNhanHoaHong(maBD);

        public bool TuChoiHoaHong(int maBD) => new GiaoDichDAL().TuChoiHoaHong(maBD);

        public byte[] LayAnhChuyenKhoanTheoBaiDang(int maBD) => new GiaoDichDAL().LayAnhChuyenKhoanTheoBaiDang(maBD);
    }
}
