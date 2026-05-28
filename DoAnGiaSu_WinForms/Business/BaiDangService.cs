using System.Data;
using DoAnGiaSu_WinForms.DataAccess;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.Business
{
    public class BaiDangService
    {
        private readonly BaiDangRepository repository = new BaiDangRepository();
        private readonly DangKyNhanLopService dangKyService = new DangKyNhanLopService();

        public bool ThemBaiDang(BaiDang bd) => repository.ThemBaiDang(bd);

        public DataTable LayBaiDangCuaPhuHuynh(int maPH) => repository.LayBaiDangCuaPhuHuynh(maPH);

        public DataTable LayBaiDangCuaPhuHuynhChiTiet(int maPH) => repository.LayBaiDangCuaPhuHuynhChiTiet(maPH);

        public DataTable LayTatCaBaiAdmin() => repository.LayTatCaBaiAdmin();

        public DataTable LayTatCaBaiAdmin(string trangThai) => repository.LayTatCaBaiAdmin(trangThai);

        public DataTable LayLopMoiChoGiaSu(int maGS, string orderByClause) => repository.LayLopMoiChoGiaSu(maGS, orderByClause);

        public bool GiaSuNhanLop(int maBD, int maGS) => dangKyService.GiaSuNhanLop(maBD, maGS);

        public bool PhuHuynhDuyetGiaSu(int maBD, int maGS) => dangKyService.PhuHuynhDuyetGiaSu(maBD, maGS);

        public bool PhuHuynhTuChoiGiaSu(int maBD, int maGS) => dangKyService.PhuHuynhTuChoiGiaSu(maBD, maGS);

        public DataTable LayThongTinGiaSuDangKy(int maBD) => repository.LayThongTinGiaSuDangKy(maBD);

        public DataTable LayBaiChoDuyetPhi() => repository.LayBaiChoDuyetPhi();

        public bool XacNhanHoaHong(int maBD) => repository.XacNhanHoaHong(maBD);

        public bool TuChoiHoaHong(int maBD) => repository.TuChoiHoaHong(maBD);

        public bool XoaBaiDang(int maBD) => repository.XoaBaiDang(maBD);

        public bool SuaBaiDang(BaiDang bd) => repository.SuaBaiDang(bd);

        public DataTable LayLopDaGiaoChoGiaSu(int maGS) => repository.LayLopDaGiaoChoGiaSu(maGS);

        public bool CapNhatAnhChuyenKhoan(int maBD, byte[] anhCK) => repository.CapNhatAnhChuyenKhoan(maBD, anhCK);

        public byte[] LayAnhChuyenKhoanTheoBaiDang(int maBD) => repository.LayAnhChuyenKhoanTheoBaiDang(maBD);

        public bool CapNhatNhanh(int maBD, string cot, string giaTri) => repository.CapNhatNhanh(maBD, cot, giaTri);

        public DataTable LayThongTinGiaSuTuBaiDangKhiThieuDangKy(int maBD) => repository.LayThongTinGiaSuTuBaiDangKhiThieuDangKy(maBD);

        public DataTable LayTatCaGiaSuAdmin() => repository.LayTatCaGiaSuAdmin();

        public List<DangKyNhanLop> LayDangKyNhanLopTheoBaiList(int maBD) => dangKyService.LayDangKyNhanLopTheoBai(maBD);
    }
}
