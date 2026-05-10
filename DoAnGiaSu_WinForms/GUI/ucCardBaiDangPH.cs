using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnGiaSu_WinForms.GUI
{
    public class ucCardBaiDangPH : UserControl
    {
        private Label lblMaBai;
        private Label lblTrangThai;
        private Label lblMonHoc;
        private Label lblLop;
        private Label lblHinhThuc;
        private Label lblMucLuong;
        private Label lblDiaChi;
        private Label lblYeuCau;
        private Label lblKhuVuc;
        private Button btnXemDuyet;
        private Button btnSua;
        private Button btnXoa;
        private FlowLayoutPanel flpBody;
        private Panel pnlHeader;
        private Panel pnlFooter;

        // Private storage fields
        private string _tenMonHoc;
        private string _tenLop;
        private string _tenHinhThuc;
        private string _mucLuong;
        private string _soNhaDuong;
        private string _yeuCauThem;
        private string _trangThai;
        private string _tenQuan;

        // Public properties
        public string TenMonHoc => _tenMonHoc;
        public string TenLop => _tenLop;
        public string TenHinhThuc => _tenHinhThuc;
        public string MucLuong => _mucLuong;
        public string SoNhaDuong => _soNhaDuong;
        public string YeuCauThem => _yeuCauThem;
        public string TrangThaiStr => _trangThai;
        public string TenQuan => _tenQuan;

        public event EventHandler XemDuyetClicked;
        public event EventHandler SuaClicked;
        public event EventHandler XoaClicked;

        public ucCardBaiDangPH()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(300, 380);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;

            // ========== HEADER PANEL ==========
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 30,
                Padding = new Padding(10, 5, 10, 5),
                BackColor = Color.White
            };

            lblMaBai = new Label
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

            pnlHeader.Controls.Add(lblMaBai);
            pnlHeader.Controls.Add(lblTrangThai);

            // ========== BODY FLOWLAYOUTPANEL ==========
            flpBody = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = false,
                Dock = DockStyle.Fill,
                WrapContents = false,
                Padding = new Padding(10, 5, 10, 5),
                BackColor = Color.White
            };

            // Các Label chi tiết - Mỗi label độc lập, tự động rớt dòng
            lblMonHoc = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(270, 0),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                Margin = new Padding(0, 3, 0, 3)
            };

            lblLop = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(270, 0),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.Navy,
                Margin = new Padding(0, 3, 0, 3)
            };

            lblHinhThuc = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(270, 0),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                Margin = new Padding(0, 3, 0, 3)
            };

            lblMucLuong = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(270, 0),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.Red,
                Margin = new Padding(0, 5, 0, 3)
            };

            lblDiaChi = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(270, 0),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                Margin = new Padding(0, 3, 0, 3)
            };

            lblYeuCau = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(270, 0),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.DarkRed,
                Margin = new Padding(0, 3, 0, 3)
            };

            lblKhuVuc = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(270, 0),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Green,
                Margin = new Padding(0, 3, 0, 3)
            };

            flpBody.Controls.Add(lblMonHoc);
            flpBody.Controls.Add(lblLop);
            flpBody.Controls.Add(lblHinhThuc);
            flpBody.Controls.Add(lblMucLuong);
            flpBody.Controls.Add(lblDiaChi);
            flpBody.Controls.Add(lblKhuVuc);
            flpBody.Controls.Add(lblYeuCau);

            // ========== FOOTER PANEL (3 Buttons) ==========
            pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 45,
                Padding = new Padding(8, 5, 8, 5),
                BackColor = Color.FromArgb(240, 243, 248)
            };

            // Sử dụng FlowLayoutPanel để sắp xếp 3 nút
            FlowLayoutPanel flpFooterButtons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false,
                AutoSize = false,
                Padding = new Padding(0),
                Margin = new Padding(0),
                BackColor = Color.FromArgb(240, 243, 248)
            };

            btnXoa = new Button
            {
                Text = "Xóa",
                Size = new Size(80, 30),
                BackColor = Color.Crimson,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Margin = new Padding(5, 5, 5, 5),
                TabIndex = 2
            };
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.Click += BtnXoa_Click;

            btnSua = new Button
            {
                Text = "Sửa",
                Size = new Size(80, 30),
                BackColor = Color.Orange,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Margin = new Padding(5, 5, 5, 5),
                TabIndex = 1
            };
            btnSua.FlatAppearance.BorderSize = 0;
            btnSua.Click += BtnSua_Click;

            btnXemDuyet = new Button
            {
                Text = "Xem",
                Size = new Size(80, 30),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Margin = new Padding(5, 5, 5, 5),
                TabIndex = 0
            };
            btnXemDuyet.FlatAppearance.BorderSize = 0;
            btnXemDuyet.Click += BtnXemDuyet_Click;

            flpFooterButtons.Controls.Add(btnXoa);
            flpFooterButtons.Controls.Add(btnSua);
            flpFooterButtons.Controls.Add(btnXemDuyet);

            pnlFooter.Controls.Add(flpFooterButtons);

            // Thêm các Panel vào UserControl
            this.Controls.Add(flpBody);
            this.Controls.Add(pnlFooter);
            this.Controls.Add(pnlHeader);
        }

        private void BtnXemDuyet_Click(object sender, EventArgs e)
        {
            XemDuyetClicked?.Invoke(this, EventArgs.Empty);
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            SuaClicked?.Invoke(this, EventArgs.Empty);
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            XoaClicked?.Invoke(this, EventArgs.Empty);
        }

        public void LoadData(int maBaiDang, string tenMon, string tenLop, string tenHinhThuc, 
                            string mucLuong, string soNhaDuong, string yeuCauThem, string trangThai, string tenQuan = "")
        {
            try
            {
                // Lưu dữ liệu vào field
                _tenMonHoc = tenMon;
                _tenLop = tenLop;
                _tenHinhThuc = tenHinhThuc;
                _mucLuong = mucLuong;
                _soNhaDuong = soNhaDuong;
                _yeuCauThem = yeuCauThem;
                _trangThai = trangThai;
                _tenQuan = tenQuan;

                this.Tag = maBaiDang;

                lblMaBai.Text = $"Mã: {maBaiDang}";
                lblTrangThai.Text = trangThai;

                // Đổi màu theo trạng thái
                if (trangThai == "ChuaGiao")
                    lblTrangThai.ForeColor = Color.Orange;
                else if (trangThai == "DangGiaoDich")
                    lblTrangThai.ForeColor = Color.Blue;
                else if (trangThai == "DaGiao")
                    lblTrangThai.ForeColor = Color.Green;
                else if (trangThai == "ChoPhuHuynhDuyet")
                    lblTrangThai.ForeColor = Color.Purple;
                else
                    lblTrangThai.ForeColor = Color.Black;

                // ========== LOGIC ẨN/HIỆN NÚT DỰA TRÊN TRẠNG THÁI ==========
                if (trangThai == "ChuaGiao")
                {
                    // Chưa giao: Hiển thị cả 3 nút (cho phép sửa/xóa)
                    btnXemDuyet.Visible = true;
                    btnSua.Visible = true;
                    btnXoa.Visible = true;
                }
                else if (trangThai == "DangGiaoDich" || trangThai == "DaGiao" || trangThai == "ChoPhuHuynhDuyet")
                {
                    // Đang giao dịch, đã giao, hoặc chờ phụ huynh duyệt: Chỉ hiển thị nút Xem (không cho sửa/xóa)
                    btnXemDuyet.Visible = true;
                    btnSua.Visible = false;
                    btnXoa.Visible = false;
                }
                else
                {
                    // Các trạng thái khác: Hiển thị đủ 3 nút
                    btnXemDuyet.Visible = true;
                    btnSua.Visible = true;
                    btnXoa.Visible = true;
                }

                // Gán giá trị cho từng Label
                lblMonHoc.Text = $"Môn học: {tenMon}";
                lblLop.Text = $"Lớp: {tenLop}";
                lblHinhThuc.Text = $"Hình thức: {tenHinhThuc}";

                if (decimal.TryParse(mucLuong, out decimal ml))
                    lblMucLuong.Text = $"Mức lương: {ml:N0} đ/buổi";
                else
                    lblMucLuong.Text = $"Mức lương: {mucLuong} đ/buổi";

                lblDiaChi.Text = $"Địa chỉ dạy: {soNhaDuong}";
                lblYeuCau.Text = $"Yêu cầu: {yeuCauThem}";
                lblKhuVuc.Text = $"Khu vực: {tenQuan}";
            }
            catch { }
        }
    }
}
