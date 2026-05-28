using System.Data;
using DoAnGiaSu_WinForms.DataAccess;

namespace DoAnGiaSu_WinForms.Business
{
    public class GiaoDichService
    {
        private readonly GiaoDichDAL repository = new GiaoDichDAL();

        public bool CapNhatAnhChuyenKhoan(int maBD, int maGS, decimal soTienPhi, byte[] anhCK) => repository.CapNhatAnhChuyenKhoan(maBD, maGS, soTienPhi, anhCK);

        public byte[] LayAnhChuyenKhoanTheoBaiDang(int maBD) => repository.LayAnhChuyenKhoanTheoBaiDang(maBD);

        public DataTable LayBaiChoDuyetPhi() => repository.LayBaiChoDuyetPhi();

        public bool XacNhanHoaHong(int maBD) => repository.XacNhanHoaHong(maBD);

        public bool TuChoiHoaHong(int maBD) => repository.TuChoiHoaHong(maBD);
    }
}
