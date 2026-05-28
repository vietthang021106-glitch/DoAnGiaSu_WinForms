using System.Data;
using DoAnGiaSu_WinForms.DataAccess;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.Business
{
    public class DanhGiaService
    {
        private readonly DanhGiaDAL repository = new DanhGiaDAL();

        public bool ThemDanhGia(DanhGia danhGia) => repository.ThemDanhGia(danhGia);

        public DataTable LayDanhGiaTheoGiaSu(int maGS) => repository.LayDanhGiaTheoGiaSu(maGS);

        public DataTable LayThongKeDanhGiaGiaSu(int maGS) => repository.LayThongKeDanhGiaGiaSu(maGS);
    }
}
