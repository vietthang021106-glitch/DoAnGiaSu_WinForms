using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DoAnGiaSu_WinForms.Business;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.GUI
{
    public partial class FormDangKy : Form
    {
        public FormDangKy()
        {
            InitializeComponent();

            ApplySameBackgroundAsLogin();

            txtTenDangNhap.PlaceholderText = "Nhập tên đăng nhập...";
            txtMatKhau.PlaceholderText = "Nhập mật khẩu...";
            txtXacNhanMK.PlaceholderText = "Nhập lại mật khẩu...";
            txtMatKhau.PasswordChar = '●';
            txtXacNhanMK.PasswordChar = '●';
            AcceptButton = btnDangNhap;

            Resize += FormDangKy_Resize;
            Shown += FormDangKy_Shown;
            AttachSizeChangedHandlers(this);

            ApplyRoundedStyle();
            CenterPanel();
        }

        private void ApplySameBackgroundAsLogin()
        {
            var frmLogin = new FormDangNhap();
            this.BackgroundImage = frmLogin.BackgroundImage;
            this.BackgroundImageLayout = frmLogin.BackgroundImageLayout;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            TaiKhoanBLL bll = new TaiKhoanBLL();
            string user = txtTenDangNhap.Text.Trim();

            string vaiTro = radPhuHuynh.Checked ? "PhuHuynh" : "GiaSu";

            string result = bll.RegisterAccount(user, txtMatKhau.Text.Trim(), txtXacNhanMK.Text.Trim(), vaiTro);

            if (result == "Thành công")
            {
                TaiKhoan tk = new TaiKhoan { TenDangNhap = user, MatKhau = txtMatKhau.Text.Trim(), VaiTro = vaiTro };

                if (radGiaSu.Checked)
                {
                    FormCapNhatGiaSu frmGS = new FormCapNhatGiaSu(tk);
                    frmGS.Show();
                }
                else if (radPhuHuynh.Checked)
                {
                    FormCapNhatPhuHuynh frmPH = new FormCapNhatPhuHuynh(tk);
                    frmPH.Show();
                }

                this.Hide();
            }
            else
            {
                MessageBox.Show(result, "Thông báo lỗi");
            }
        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormDangKy_Load(object sender, EventArgs e)
        {
            radGiaSu.Checked = true;
            CenterPanel();
        }

        private void FormDangKy_Shown(object? sender, EventArgs e)
        {
            ApplyRoundedStyle();
            CenterPanel();
        }

        private void FormDangKy_Resize(object? sender, EventArgs e)
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
                else if (control is TextBox || control is Button || control is GroupBox)
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