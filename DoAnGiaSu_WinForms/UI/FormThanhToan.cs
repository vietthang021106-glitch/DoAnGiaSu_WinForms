using System;
using System.IO;
using System.Windows.Forms;
using DoAnGiaSu_WinForms.Business;

namespace DoAnGiaSu_WinForms.GUI
{
    public partial class FormThanhToan : Form
    {
        private decimal _hocPhi;
        private int _maBaiDang;
        private string _tenGiaSu;
        private byte[] _anhChuyenKhoan = null;
        private readonly GiaoDichService giaoDichService = new GiaoDichService();

        public FormThanhToan(decimal hocPhi, int maBD, string tenGS)
        {
            InitializeComponent();
            this._hocPhi = hocPhi;
            this._maBaiDang = maBD;
            this._tenGiaSu = tenGS;
        }

        private void FormThanhToan_Load(object sender, EventArgs e)
        {
            decimal phiHoaHong = _hocPhi * 2m;

            string bankID = "MB";
            string accountNo = "0123456789";
            string accountName = "NGUYEN VAN ADMIN";

            string description = $"PHI_LOP_{_maBaiDang}_{_tenGiaSu}";

            lblSoTien.Text = $"Số tiền phí (Mức lương x2): {phiHoaHong:N0} VNĐ";
            lblNoiDung.Text = $"Nội dung: {description}";

            string qrUrl = $"https://img.vietqr.io/image/{bankID}-{accountNo}-compact.png?amount={phiHoaHong}&addInfo={description}&accountName={accountName}";

            picQR.ImageLocation = qrUrl;
        }

        private void btnTaiAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picMinhChung.ImageLocation = ofd.FileName;
                _anhChuyenKhoan = File.ReadAllBytes(ofd.FileName);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            if (_anhChuyenKhoan == null)
            {
                MessageBox.Show("Vui lòng tải ảnh minh chứng chuyển khoản lên trước khi xác nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xác nhận đã chuyển thanh toán và gửi ảnh minh chứng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int maTK = new DoAnGiaSu_WinForms.DataAccess.TaiKhoanDAL().LayMaTKTuTen(_tenGiaSu);
                int maGS = new DoAnGiaSu_WinForms.DataAccess.GiaSuDAL().LayMaGSMoiNhatTheoMaTK(maTK);
                decimal phiHoaHong = _hocPhi * 2m;
                if (giaoDichService.CapNhatAnhChuyenKhoan(_maBaiDang, maGS, phiHoaHong, _anhChuyenKhoan))
                {
                    MessageBox.Show("Hệ thống đã ghi nhận giao dịch và ảnh minh chứng. Vui lòng đợi Admin phê duyệt để xem địa chỉ lớp học!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi lưu ảnh minh chứng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}