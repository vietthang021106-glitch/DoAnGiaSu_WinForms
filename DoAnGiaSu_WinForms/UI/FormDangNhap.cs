using System;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DoAnGiaSu_WinForms.Business;
using DoAnGiaSu_WinForms.DataAccess;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.GUI
{
    public partial class FormDangNhap : Form
    {
        public FormDangNhap()
        {
            InitializeComponent();
            txtTenDangNhap.PlaceholderText = "Nhập tên đăng nhập...";
            txtMatKhau.PlaceholderText = "Nhập mật khẩu...";
            txtMatKhau.PasswordChar = '*';
            AcceptButton = btnDangNhap;

            Resize += FormDangNhap_Resize;
            Shown += FormDangNhap_Shown;
            AttachSizeChangedHandlers(this);

            ApplyRoundedStyle();
            CenterPanel();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            AuthService authService = new AuthService();
            string username = txtTenDangNhap.Text.Trim();
            TaiKhoan user = authService.Authenticate(username, txtMatKhau.Text.Trim());

            if (user != null)
            {
                if (user.VaiTro == "Admin")
                {
                    FormMainAdmin frm = new FormMainAdmin();
                    frm.Show();
                }
                else if (user.VaiTro == "PhuHuynh")
                {
                    FormMainPhuHuynh frm = new FormMainPhuHuynh(username);
                    frm.Show();
                }
                else if (user.VaiTro == "GiaSu")
                {
                    GiaSuDAL gsDal = new GiaSuDAL();
                    string trangThai = gsDal.KiemTraTrangThaiDuyet(username);

                    if (trangThai == "ChuaCapNhat")
                    {
                        MessageBox.Show("Bạn chưa cập nhật hồ sơ, vui lòng cập nhật.", "Thông báo");
                        FormCapNhatGiaSu frm = new FormCapNhatGiaSu(user);
                        frm.Show();
                    }
                    else if (trangThai == "ChoDuyet")
                    {
                        MessageBox.Show("Hồ sơ của bạn đang chờ Admin duyệt. Vui lòng quay lại sau!", "Thông báo");
                        return;
                    }
                    else if (trangThai == "DaDuyet")
                    {
                        FormMainGiaSu frm = new FormMainGiaSu(username);
                        frm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Trạng thái tài khoản không hợp lệ hoặc bị từ chối.", "Thông báo");
                        return;
                    }
                }

                Hide();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
            }
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            FormDangKy frm = new FormDangKy();
            frm.FormClosed += (s, args) => this.Show();
            frm.Show();
            Hide();
        }

        private void btnQuenMatKhau_Click(object sender, EventArgs e)
        {
            using FormQuenMatKhau frm = new FormQuenMatKhau();
            frm.ShowDialog(this);
        }

        private void FormDangNhap_Load(object sender, EventArgs e)
        {
            CenterPanel();
        }

        private void FormDangNhap_Shown(object? sender, EventArgs e)
        {
            ApplyRoundedStyle();
            CenterPanel();
        }

        private void FormDangNhap_Resize(object? sender, EventArgs e)
        {
            CenterPanel();
            ApplyRoundedStyle();
        }

        private void CenterPanel()
        {
            panel1.Left = (ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (ClientSize.Height - panel1.Height) / 2;
        }

        private void ApplyRoundedStyle()
        {
            ApplyRoundedToControlTree(this);
        }

        private void ApplyRoundedToControlTree(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                if (control is Panel)
                    SetRoundedRegion(control, 22);
                else if (control is TextBox || control is Button)
                    SetRoundedRegion(control, 16);

                if (control.HasChildren)
                    ApplyRoundedToControlTree(control);
            }
        }

        private void AttachSizeChangedHandlers(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                control.SizeChanged += (_, _) => ApplyRoundedStyle();

                if (control.HasChildren)
                    AttachSizeChangedHandlers(control);
            }
        }

        private static void SetRoundedRegion(Control control, int radius)
        {
            if (control.Width <= 0 || control.Height <= 0) return;

            int safeRadius = Math.Min(radius, Math.Min(control.Width, control.Height) / 2);
            int diameter = safeRadius * 2;

            using var path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, diameter, diameter, 180, 90);
            path.AddArc(control.Width - diameter, 0, diameter, diameter, 270, 90);
            path.AddArc(control.Width - diameter, control.Height - diameter, diameter, diameter, 0, 90);
            path.AddArc(0, control.Height - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            control.Region?.Dispose();
            control.Region = new Region(path);
        }
    }
}