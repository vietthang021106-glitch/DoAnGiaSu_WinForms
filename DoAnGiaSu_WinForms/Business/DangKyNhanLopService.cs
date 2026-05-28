using System.Collections.Generic;
using DoAnGiaSu_WinForms.DataAccess;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.Business
{
    public class DangKyNhanLopService
    {
        private readonly DangKyNhanLopDAL dal = new DangKyNhanLopDAL();

        public bool GiaSuNhanLop(int maBD, int maGS) => dal.GiaSuNhanLop(maBD, maGS);

        public bool PhuHuynhDuyetGiaSu(int maBD, int maGS) => dal.PhuHuynhDuyetGiaSu(maBD, maGS);

        public bool PhuHuynhTuChoiGiaSu(int maBD, int maGS) => dal.PhuHuynhTuChoiGiaSu(maBD, maGS);

        public List<DangKyNhanLop> LayDangKyNhanLopTheoBai(int maBD) => dal.LayDangKyNhanLopTheoBai(maBD);
    }
}
