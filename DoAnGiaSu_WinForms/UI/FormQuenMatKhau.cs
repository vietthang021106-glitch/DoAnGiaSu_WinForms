using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DoAnGiaSu_WinForms.Business;

namespace DoAnGiaSu_WinForms.GUI
{
    public class FormQuenMatKhau : Form
    {
        private TextBox txtTenDangNhap;
        private TextBox txtSDT;
        private Button btnKiemTra;
        private TextBox txtMatKhauMoi;
        private TextBox txtXacNhanMK;
        private Button btnDoiMatKhau;
        private Panel pnlDoiMatKhau;
        private Panel panel1;

        public FormQuenMatKhau()
        {
            InitializeUi();
        }

        private void InitializeUi()
        {
            Text = "Quên mật khẩu";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(480, 300);
            BackColor = Color.White;
            AutoSize = false;
            AutoScroll = true;

            panel1 = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(18),
                BackColor = Color.White
            };

            Label lblTieuDe = new Label
            {
                Dock = DockStyle.Top,
                Height = 45,
                Text = "QUÊN MẬT KHẨU",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 33, 53),
                TextAlign = ContentAlignment.MiddleCenter
            };

            Label lblTenDangNhap = new Label
            {
                AutoSize = true,
                Text = "Tên đăng nhập",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 33, 53),
                Location = new Point(18, 58)
            };

            txtTenDangNhap = new TextBox
            {
                Location = new Point(18, 84),
                Size = new Size(430, 30),
                Font = new Font("Segoe UI", 11F)
            };

            Label lblSDT = new Label
            {
                AutoSize = true,
                Text = "Số điện thoại",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 33, 53),
                Location = new Point(18, 124)
            };

            txtSDT = new TextBox
            {
                Location = new Point(18, 150),
                Size = new Size(430, 30),
                Font = new Font("Segoe UI", 11F)
            };

            btnKiemTra = new Button
            {
                Text = "Kiểm tra",
                Location = new Point(18, 192),
                Size = new Size(430, 40),
                BackColor = Color.FromArgb(24, 119, 242),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnKiemTra.FlatAppearance.BorderSize = 0;
            btnKiemTra.Click += btnKiemTra_Click;

            pnlDoiMatKhau = new Panel
            {
                Location = new Point(18, 248),
                Size = new Size(430, 120),
                Visible = false,
                BackColor = Color.White
            };

            Label lblMatKhauMoi = new Label
            {
                AutoSize = true,
                Text = "Mật khẩu mới",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 33, 53),
                Location = new Point(0, 0)
            };

            txtMatKhauMoi = new TextBox
            {
                Location = new Point(0, 24),
                Size = new Size(430, 30),
                Font = new Font("Segoe UI", 11F),
                UseSystemPasswordChar = true
            };

            Label lblXacNhan = new Label
            {
                AutoSize = true,
                Text = "Nhập lại mật khẩu mới",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 33, 53),
                Location = new Point(0, 58)
            };

            txtXacNhanMK = new TextBox
            {
                Location = new Point(0, 82),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 11F),
                UseSystemPasswordChar = true
            };

            btnDoiMatKhau = new Button
            {
                Text = "Đổi mật khẩu",
                Location = new Point(308, 80),
                Size = new Size(122, 32),
                BackColor = Color.FromArgb(34, 177, 76),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnDoiMatKhau.FlatAppearance.BorderSize = 0;
            btnDoiMatKhau.Click += btnDoiMatKhau_Click;

            pnlDoiMatKhau.Controls.Add(lblMatKhauMoi);
            pnlDoiMatKhau.Controls.Add(txtMatKhauMoi);
            pnlDoiMatKhau.Controls.Add(lblXacNhan);
            pnlDoiMatKhau.Controls.Add(txtXacNhanMK);
            pnlDoiMatKhau.Controls.Add(btnDoiMatKhau);

            panel1.Controls.Add(lblTieuDe);
            panel1.Controls.Add(lblTenDangNhap);
            panel1.Controls.Add(txtTenDangNhap);
            panel1.Controls.Add(lblSDT);
            panel1.Controls.Add(txtSDT);
            panel1.Controls.Add(btnKiemTra);
            panel1.Controls.Add(pnlDoiMatKhau);

            Controls.Add(panel1);
        }

        private void CenterPanel()
        {
            this.CenterToScreen();
        }

        private void btnKiemTra_Click(object? sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            string sdt = txtSDT.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                TaiKhoanBLL tkBll = new TaiKhoanBLL();
                if (tkBll.KiemTraSDT(username, sdt))
                {
                    txtTenDangNhap.Enabled = false;
                    txtSDT.Enabled = false;
                    btnKiemTra.Enabled = false;
                    btnKiemTra.BackColor = Color.Gray;

                    pnlDoiMatKhau.Visible = true;
                    this.Height += 120;
                    CenterPanel();

                    MessageBox.Show("Xác minh thành công! Vui lòng đặt mật khẩu mới.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc số điện thoại không khớp với hệ thống!", "Lỗi xác minh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDoiMatKhau_Click(object? sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            string newPass = txtMatKhauMoi.Text.Trim();
            string confirmPass = txtXacNhanMK.Text.Trim();

            if (string.IsNullOrWhiteSpace(newPass) || string.IsNullOrWhiteSpace(confirmPass))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới và xác nhận!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPass != confirmPass)
            {
                MessageBox.Show("Mật khẩu không khớp!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                TaiKhoanBLL tkBll = new TaiKhoanBLL();
                if (tkBll.CapNhatMatKhau(username, newPass))
                {
                    MessageBox.Show("Đổi mật khẩu thành công! Vui lòng đăng nhập lại.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đổi mật khẩu thất bại. Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
