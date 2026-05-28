using System.Data;
using DoAnGiaSu_WinForms.DataAccess;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.Business
{
    public class PhuHuynhService
    {
        private readonly PhuHuynhDAL repository = new PhuHuynhDAL();

        public bool ThemPhuHuynh(PhuHuynh ph) => repository.ThemPhuHuynh(ph);

        public int LayMaPH(int maTK) => repository.LayMaPH(maTK);

        public string KiemTraSoDienThoai(string sdt) => repository.KiemTraSoDienThoai(sdt);

        public DataTable LayDanhSachQuan() => repository.LayDanhSachQuan();

        public DataTable LayThongKeDanhGiaGiaSu(int maGS) => repository.LayThongKeDanhGiaGiaSu(maGS);
    }
}
