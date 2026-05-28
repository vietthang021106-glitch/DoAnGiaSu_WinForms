using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DoAnGiaSu_WinForms.Business;
using DoAnGiaSu_WinForms.DataAccess;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.GUI
{
    public partial class FormCapNhatPhuHuynh : Form
    {
        private TaiKhoan _tkDangKy;

        private readonly PhuHuynhService phService = new PhuHuynhService();
        private readonly TaiKhoanDAL tkDal = new TaiKhoanDAL();

        public FormCapNhatPhuHuynh(TaiKhoan tk)
        {
            InitializeComponent();
            this._tkDangKy = tk;

            ApplySameBackgroundAsLogin();
            Resize += FormCapNhatPhuHuynh_Resize;
            Shown += FormCapNhatPhuHuynh_Shown;
            FormClosed += FormCapNhatPhuHuynh_FormClosed;
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

        private void CenterPanel()
        {
            if (this.Controls.ContainsKey("panel1"))
            {
                Control panel1 = this.Controls["panel1"];
                panel1.Left = (ClientSize.Width - panel1.Width) / 2;
                panel1.Top = (ClientSize.Height - panel1.Height) / 2;
            }
        }

        private void FormCapNhatPhuHuynh_Shown(object? sender, EventArgs e)
        {
            ApplyRoundedStyle();
            CenterPanel();
        }

        private void FormCapNhatPhuHuynh_FormClosed(object? sender, FormClosedEventArgs e)
        {
            var formDangKy = Application.OpenForms["FormDangKy"];
            if (formDangKy != null)
            {
                formDangKy.Show();
            }
        }

        private void FormCapNhatPhuHuynh_Resize(object? sender, EventArgs e)
        {
            CenterPanel();
            ApplyRoundedStyle();
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
                else if (control is TextBox || control is ComboBox || control is Button)
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

        private void FormCapNhatPhuHuynh_Load(object sender, EventArgs e)
        {
            this.Text = "Cập nhật hồ sơ Phụ huynh: " + _tkDangKy.TenDangNhap;

            try
            {
                cboQuan.DataSource = phService.LayDanhSachQuan();
                cboQuan.DisplayMember = "TenQuan";
                cboQuan.ValueMember = "MaQuan";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách Quận: " + ex.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    MessageBox.Show("Vui lòng nhập Họ tên và Số điện thoại!");
                    return;
                }

                string sdt = txtSDT.Text.Trim();
                string loiTonTai = phService.KiemTraSoDienThoai(sdt);
                if (!string.IsNullOrEmpty(loiTonTai))
                {
                    MessageBox.Show(loiTonTai, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maTK = tkDal.LayMaTKTuTen(_tkDangKy.TenDangNhap);
                if (maTK == 0)
                {
                    if (!tkDal.DangKy(_tkDangKy))
                    {
                        MessageBox.Show("Lỗi: Không thể tạo tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    maTK = tkDal.LayMaTKTuTen(_tkDangKy.TenDangNhap);
                }

                if (maTK == 0)
                {
                    MessageBox.Show("Lỗi: Không tìm thấy ID tài khoản!");
                    return;
                }

                PhuHuynh ph = new PhuHuynh();
                ph.HoTen = txtHoTen.Text.Trim();
                ph.SDT = sdt;
                ph.SoNhaDuong = txtDiaChi.Text.Trim();
                ph.MaQuan = Convert.ToInt32(cboQuan.SelectedValue);
                ph.MaTK = maTK;

                if (phService.ThemPhuHuynh(ph))
                {
                    MessageBox.Show("Đăng ký tài khoản và cập nhật thông tin thành công!", "Thông báo");

                    var loginForm = Application.OpenForms["FormDangNhap"];
                    if (loginForm != null)
                    {
                        loginForm.Show();
                    }
                    else
                    {
                        new FormDangNhap().Show();
                    }

                    var formDangKy = Application.OpenForms["FormDangKy"];
                    if (formDangKy != null)
                    {
                        formDangKy.Close();
                    }

                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Lưu thất bại! Kiểm tra lại kết nối Database.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }
    }
}