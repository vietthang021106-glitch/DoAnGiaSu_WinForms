using System.Data;
using DoAnGiaSu_WinForms.DataAccess;

namespace DoAnGiaSu_WinForms.Business
{
    public class DanhMucService
    {
        private readonly DanhMucDAL repository = new DanhMucDAL();

        public DataTable LayMonHoc() => repository.LayMonHoc();

        public DataTable LayLopHoc() => repository.LayLopHoc();

        public DataTable LayHinhThuc() => repository.LayHinhThuc();

        public DataTable LayTrinhDo() => repository.LayTrinhDo();

        public DataTable LayQuanHuyen() => repository.LayQuanHuyen();

        public DataTable LayGioiTinh() => repository.LayGioiTinh();

        public DataTable LayNamSinh() => repository.LayNamSinh();

        public DataTable LayTruong() => repository.LayTruong();

        public DataTable LayNamHoc() => repository.LayNamHoc();

        public DataTable LayChungChi() => repository.LayChungChi();

        public DataTable LayXepLoai() => repository.LayXepLoai();
    }
}
