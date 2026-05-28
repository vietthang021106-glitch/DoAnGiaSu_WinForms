using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using DoAnGiaSu_WinForms.DataAccess;

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

        private string _tenDangNhapDaXacNhan;

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
            ClientSize = new Size(480, 430);
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

        private void btnKiemTra_Click(object? sender, EventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text.Trim();
            string sdt = txtSDT.Text.Trim();

            if (string.IsNullOrWhiteSpace(tenDangNhap) || string.IsNullOrWhiteSpace(sdt))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using SqlConnection conn = new DBConnection().GetConnection();
                const string sql = @"SELECT TK.MaTK 
                                     FROM TAIKHOAN TK
                                     LEFT JOIN PHUHUYNH PH ON TK.MaTK = PH.MaTK
                                     LEFT JOIN GIASU GS ON TK.MaTK = GS.MaTK
                                     WHERE TK.TenDangNhap = @TenDangNhap 
                                       AND (PH.SDT = @SDT OR GS.SDT = @SDT)";
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@SDT", sdt);
                conn.Open();

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    _tenDangNhapDaXacNhan = tenDangNhap;
                    txtTenDangNhap.Enabled = false;
                    txtSDT.Enabled = false;
                    btnKiemTra.Enabled = false;

                    pnlDoiMatKhau.Visible = true;
                    MessageBox.Show("Thông tin hợp lệ. Vui lòng nhập mật khẩu mới.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtMatKhauMoi.Focus();
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc Số điện thoại không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra thông tin: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDoiMatKhau_Click(object? sender, EventArgs e)
        {
            string mkMoi = txtMatKhauMoi.Text.Trim();
            string xacNhan = txtXacNhanMK.Text.Trim();

            if (string.IsNullOrWhiteSpace(mkMoi) || string.IsNullOrWhiteSpace(xacNhan))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới và xác nhận mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!string.Equals(mkMoi, xacNhan, StringComparison.Ordinal))
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(_tenDangNhapDaXacNhan))
            {
                MessageBox.Show("Bạn chưa xác thực thông tin tài khoản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using SqlConnection conn = new DBConnection().GetConnection();
                const string sql = @"UPDATE TAIKHOAN 
                                     SET MatKhau = @MatKhauMoi 
                                     WHERE TenDangNhap = @TenDangNhap";
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MatKhauMoi", mkMoi);
                cmd.Parameters.AddWithValue("@TenDangNhap", _tenDangNhapDaXacNhan);
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Không thể đổi mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đổi mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
