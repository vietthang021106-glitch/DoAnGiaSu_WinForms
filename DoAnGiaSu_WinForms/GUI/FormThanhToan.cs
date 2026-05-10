using System;
using System.IO;
using System.Windows.Forms;
using DoAnGiaSu_WinForms.DAL;

namespace DoAnGiaSu_WinForms.GUI
{
    public partial class FormThanhToan : Form
    {
        private decimal _hocPhi;
        private int _maBaiDang;
        private string _tenGiaSu;
        private byte[] _anhChuyenKhoan = null;

        public FormThanhToan(decimal hocPhi, int maBD, string tenGS)
        {
            InitializeComponent();
            this._hocPhi = hocPhi;
            this._maBaiDang = maBD;
            this._tenGiaSu = tenGS;
        }

        // Hàm chính để hiện mã QR
        private void FormThanhToan_Load(object sender, EventArgs e)
        {
            // Tính phí theo công thức mới: mức lương x2
            decimal phiHoaHong = _hocPhi * 2m;

            // THÔNG TIN NGÂN HÀNG ADMIN
            string bankID = "MB"; // Mã ngân hàng (MB, VCB, ICB...)
            string accountNo = "0123456789"; // SỐ TÀI KHOẢN CỦA BẠN
            string accountName = "NGUYEN VAN ADMIN"; // TÊN CỦA BẠN

            // Nội dung chuyển khoản
            string description = $"PHI_LOP_{_maBaiDang}_{_tenGiaSu}";

            // Hiển thị lên Label
            lblSoTien.Text = $"Số tiền phí (Mức lương x2): {phiHoaHong:N0} VNĐ";
            lblNoiDung.Text = $"Nội dung: {description}";

            // API VietQR động
            string qrUrl = $"https://img.vietqr.io/image/{bankID}-{accountNo}-compact.png?amount={phiHoaHong}&addInfo={description}&accountName={accountName}";

            // Load QR vào ảnh
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

        // Nút bấm đóng Form
        private void btnDong_Click(object sender, EventArgs e)
        {
            if (_anhChuyenKhoan == null)
            {
                MessageBox.Show("Vui lòng tải ảnh minh chứng chuyển khoản lên trước khi xác nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xác nhận đã chuyển thanh toán và gửi ảnh minh chứng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BaiDangDAL bdDal = new BaiDangDAL();
                if (bdDal.CapNhatAnhChuyenKhoan(_maBaiDang, _anhChuyenKhoan))
                {
                    MessageBox.Show("Hệ thống đã ghi nhận giao dịch và ảnh minh chứng. Vui lòng đợi Admin phê duyệt để xem địa chỉ lớp học!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi lưu ảnh minh chứng!");
                }
            }
        }
    }
}