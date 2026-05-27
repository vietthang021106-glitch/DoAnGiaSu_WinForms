using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnGiaSu_WinForms.GUI
{
    public class ucCardTimLop : UserControl
    {
        private Label lblMaLop;
        private Label lblMonHoc;
        private Label lblLop;
        private Label lblHinhThuc;
        private Label lblKhuVuc;
        private Label lblYeuCau;
        private Label lblMucLuong;
        private Button btnDangKy;
        private FlowLayoutPanel flpBody;
        
        public event EventHandler<int> DangKyClicked;
        private int _maLop;

        public ucCardTimLop()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(380, 320);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;

            // Header: Mã lớp
            lblMaLop = new Label
            {
                Text = "Mã: ",
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 9F),
                Dock = DockStyle.Top,
                Height = 25,
                Padding = new Padding(10, 5, 10, 5)
            };

            // Body: FlowLayoutPanel chứa các Label chi tiết
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
                MaximumSize = new Size(340, 0),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                Margin = new Padding(0, 3, 0, 3)
            };

            lblLop = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(340, 0),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.Navy,
                Margin = new Padding(0, 3, 0, 3)
            };

            lblHinhThuc = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(340, 0),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                Margin = new Padding(0, 3, 0, 3)
            };

            lblKhuVuc = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(340, 0),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Black,
                Margin = new Padding(0, 3, 0, 3)
            };

            lblYeuCau = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(340, 0),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.DarkRed,
                Margin = new Padding(0, 3, 0, 3)
            };

            lblMucLuong = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.Red,
                Margin = new Padding(0, 8, 0, 3)
            };

            flpBody.Controls.Add(lblMonHoc);
            flpBody.Controls.Add(lblLop);
            flpBody.Controls.Add(lblHinhThuc);
            flpBody.Controls.Add(lblKhuVuc);
            flpBody.Controls.Add(lblYeuCau);
            flpBody.Controls.Add(lblMucLuong);

            // Button Đăng ký
            btnDangKy = new Button
            {
                Text = "Đăng ký",
                Size = new Size(120, 40),
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Margin = new Padding(10, 5, 10, 10)
            };
            btnDangKy.FlatAppearance.BorderSize = 0;
            btnDangKy.Click += BtnDangKy_Click;

            this.Controls.Add(flpBody);
            this.Controls.Add(btnDangKy);
            this.Controls.Add(lblMaLop);
        }

        private void BtnDangKy_Click(object sender, EventArgs e)
        {
            DangKyClicked?.Invoke(this, _maLop);
        }

        public void LoadData(int maLop, string monHoc, string lop, string hinhThuc, string khuVuc, string yeuCau, string mucLuong)
        {
            try
            {
                _maLop = maLop;
                lblMaLop.Text = $"Mã: {maLop}";
                lblMonHoc.Text = $"Môn học: {monHoc}";
                lblLop.Text = $"Lớp: {lop}";
                lblHinhThuc.Text = $"Hình thức: {hinhThuc}";
                lblKhuVuc.Text = $"Khu vực: {khuVuc}";
                lblYeuCau.Text = $"Yêu cầu: {yeuCau}";
                
                if (decimal.TryParse(mucLuong, out decimal ml))
                    lblMucLuong.Text = $"{ml:N0} đ/buổi";
                else
                    lblMucLuong.Text = $"{mucLuong} đ/buổi";
            }
            catch { }
        }
    }
}
