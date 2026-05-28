using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnGiaSu_WinForms.GUI
{
    public class ucCardLopDaNhan : UserControl
    {
        private Label lblMaLop;
        private Label lblTrangThai;
        private Label lblMonHoc;
        private Label lblLop;
        private Label lblPhHuynh;
        private Label lblSDT;
        private Label lblHinhThuc;
        private Label lblDiaChi;
        private Label lblKhuVuc;
        private Label lblYeuCau;
        private Label lblHocPhi;
        private FlowLayoutPanel flpBody;

        private string trangThaiDongPhi_HienTai;
        private int maGS_HienTai;
        private int maBaiDang_HienTai;
        private string trangThaiDangKy_HienTai;

        public int MaLop { get; private set; }
        public string TrangThaiStr { get; private set; }
        public string TrangThaiDongPhiStr => trangThaiDongPhi_HienTai;
        public string TrangThaiDangKyStr => trangThaiDangKy_HienTai;
        public int MaGS_HienTai_Public => maGS_HienTai;
        public int MaBaiDang_HienTai_Public => maBaiDang_HienTai;
        public decimal MucHocPhi { get; private set; }

        public ucCardLopDaNhan()
        {
            InitializeComponent();
            DoubleClick += UcCardLopDaNhan_DoubleClick;
        }

        private void InitializeComponent()
        {
            Size = new Size(420, 380);
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;

            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30,
                Padding = new Padding(10, 5, 10, 5),
                BackColor = Color.White
            };

            lblMaLop = new Label
            {
                Text = "Mã: ",
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 9F),
                Dock = DockStyle.Left
            };

            lblTrangThai = new Label
            {
                Text = "",
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Dock = DockStyle.Right,
                TextAlign = ContentAlignment.TopRight
            };

            pnlHeader.Controls.Add(lblMaLop);
            pnlHeader.Controls.Add(lblTrangThai);

            flpBody = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = false,
                Dock = DockStyle.Fill,
                WrapContents = false,
                Padding = new Padding(10, 5, 10, 5),
                BackColor = Color.White
            };

            lblMonHoc = CreateLabel(10F, Color.Black);
            lblLop = CreateLabel(11F, Color.Navy, FontStyle.Bold);
            lblPhHuynh = CreateLabel(10F, Color.Black);
            lblSDT = CreateLabel(10F, Color.Black);
            lblHinhThuc = CreateLabel(10F, Color.Black);
            lblDiaChi = CreateLabel(10F, Color.Black);
            lblKhuVuc = CreateLabel(10F, Color.Black);
            lblYeuCau = CreateLabel(10F, Color.DarkRed);
            lblHocPhi = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.Red,
                Margin = new Padding(0, 8, 0, 3)
            };

            flpBody.Controls.Add(lblMonHoc);
            flpBody.Controls.Add(lblLop);
            flpBody.Controls.Add(lblPhHuynh);
            flpBody.Controls.Add(lblSDT);
            flpBody.Controls.Add(lblHinhThuc);
            flpBody.Controls.Add(lblDiaChi);
            flpBody.Controls.Add(lblKhuVuc);
            flpBody.Controls.Add(lblYeuCau);
            flpBody.Controls.Add(lblHocPhi);

            Controls.Add(flpBody);
            Controls.Add(pnlHeader);
        }

        private static Label CreateLabel(float size, Color color, FontStyle style = FontStyle.Regular)
        {
            return new Label
            {
                AutoSize = true,
                MaximumSize = new Size(380, 0),
                Font = new Font("Segoe UI", size, style),
                ForeColor = color,
                Margin = new Padding(0, 3, 0, 3)
            };
        }

        public void LoadData(int maLop, string trangThai, string monHoc, string lop, string tenPH, string sdt, string hinhThuc, string diaChiChiTiet, string khuVuc, string yeuCauThem, string hocPhi, string trangThaiDongPhi = null, int maGS = 0, string trangThaiDangKy = null)
        {
            try
            {
                MaLop = maLop;
                TrangThaiStr = trangThai;
                maBaiDang_HienTai = maLop;
                maGS_HienTai = maGS;
                trangThaiDongPhi_HienTai = trangThaiDongPhi;
                trangThaiDangKy_HienTai = trangThaiDangKy ?? string.Empty;

                lblMaLop.Text = $"Mã: {maLop}";
                
                if (trangThaiDongPhi == "ChoAdminDuyet")
                {
                    lblTrangThai.Text = "Chờ Admin duyệt";
                    lblTrangThai.ForeColor = Color.FromArgb(255, 165, 0);
                    this.DoubleClick -= UcCardLopDaNhan_DoubleClick;
                }
                else if (trangThaiDongPhi == "DaThanhToan")
                {
                    lblTrangThai.Text = "Đã Thanh Toán";
                    lblTrangThai.ForeColor = Color.Green;
                    this.DoubleClick -= UcCardLopDaNhan_DoubleClick;
                }
                else if (trangThai == "Chờ Admin duyệt phí")
                {
                    lblTrangThai.Text = "Chờ Admin duyệt";
                    lblTrangThai.ForeColor = Color.FromArgb(255, 165, 0);
                    this.DoubleClick -= UcCardLopDaNhan_DoubleClick;
                }
                else if (trangThai == "ChuaGiao" || trangThai == "ChoDuyet" || trangThai == "ChoPhuHuynhDuyet")
                {
                    lblTrangThai.Text = trangThai;
                    lblTrangThai.ForeColor = Color.Orange;
                    this.DoubleClick += UcCardLopDaNhan_DoubleClick;
                }
                else if (trangThai == "DangGiaoDich" || trangThai == "DaDuyet" || trangThai == "Nhấp đúp để thanh toán")
                {
                    lblTrangThai.Text = "Nhấp đúp để thanh toán";
                    lblTrangThai.ForeColor = Color.Blue;
                    this.DoubleClick += UcCardLopDaNhan_DoubleClick;
                }
                else if (trangThai == "DaGiao" || trangThai == "Đã Thanh Toán")
                {
                    lblTrangThai.Text = "Đã Thanh Toán";
                    lblTrangThai.ForeColor = Color.Green;
                    this.DoubleClick -= UcCardLopDaNhan_DoubleClick;
                }
                else if (trangThai == "Đã đăng ký - chờ PH duyệt")
                {
                    lblTrangThai.Text = trangThai;
                    lblTrangThai.ForeColor = Color.Orange;
                    this.DoubleClick -= UcCardLopDaNhan_DoubleClick;
                }
                else
                {
                    lblTrangThai.Text = trangThai;
                    lblTrangThai.ForeColor = Color.Black;
                    this.DoubleClick += UcCardLopDaNhan_DoubleClick;
                }

                lblMonHoc.Text = $"Môn học: {monHoc}";
                lblLop.Text = $"Lớp: {lop}";
                lblPhHuynh.Text = $"Phụ huynh: {tenPH}";
                lblSDT.Text = $"SĐT: {sdt}";
                lblHinhThuc.Text = $"Hình thức: {hinhThuc}";
                lblDiaChi.Text = $"Địa chỉ: {diaChiChiTiet}";
                lblKhuVuc.Text = $"Khu vực: {khuVuc}";
                lblYeuCau.Text = $"Yêu cầu: {yeuCauThem}";

                if (decimal.TryParse(hocPhi, out decimal hp))
                {
                    MucHocPhi = hp;
                    lblHocPhi.Text = $"{hp:N0} đ/buổi";
                }
                else
                {
                    MucHocPhi = 0;
                    lblHocPhi.Text = $"{hocPhi} đ/buổi";
                }
            }
            catch { }
        }

        private void UcCardLopDaNhan_DoubleClick(object? sender, EventArgs e)
        {
            if (trangThaiDongPhi_HienTai == "DaDong")
            {
                MessageBox.Show("Bạn đã thanh toán phí cho lớp này rồi! Vui lòng liên hệ Phụ huynh theo SĐT trên thẻ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (trangThaiDongPhi_HienTai == "ChuaDong" || string.IsNullOrEmpty(trangThaiDongPhi_HienTai))
            {
                if (MessageBox.Show("Phụ huynh đã duyệt! Đóng phí nhận thông tin?", "Thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    MessageBox.Show("Vui lòng mở form thanh toán tương ứng để xử lý giao dịch.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
