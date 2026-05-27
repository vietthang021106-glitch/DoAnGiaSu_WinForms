using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DoAnGiaSu_WinForms.DAL; // Thêm dòng này để gọi người vận chuyển
using DoAnGiaSu_WinForms.Model; // Thêm dòng này để gọi xe chở hàng

namespace DoAnGiaSu_WinForms.GUI
{
    public partial class FormCapNhatPhuHuynh : Form
    {
        // 1. Biến để giữ tài khoản truyền từ Form Đăng ký sang
        private TaiKhoan _tkDangKy;

        // Khởi tạo các lớp xử lý
        GiaSuDAL gsDal = new GiaSuDAL();
        TaiKhoanDAL tkDal = new TaiKhoanDAL();
        PhuHuynhDAL phDal = new PhuHuynhDAL();

        // 2. Sửa hàm khởi tạo để nhận TaiKhoan
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

            // 3. Nạp danh sách Quận/Huyện vào ComboBox khi mở Form
            try
            {
                cboQuan.DataSource = gsDal.LayDanhMuc("DM_QUANHUYEN");
                cboQuan.DisplayMember = "TenQuan";
                cboQuan.ValueMember = "MaQuan";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách Quận: " + ex.Message);
            }
        }

        // 4. Sự kiện khi nhấn nút Lưu (Bạn nhớ nhấp đúp nút Lưu ở Design để tạo hàm này nhé)
        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra nhập liệu cơ bản
                if (string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtSDT.Text))
                {
                    MessageBox.Show("Vui lòng nhập Họ tên và Số điện thoại!");
                    return;
                }

                string sdt = txtSDT.Text.Trim();
                string loiTonTai = phDal.KiemTraSoDienThoai(sdt);
                if (!string.IsNullOrEmpty(loiTonTai))
                {
                    MessageBox.Show(loiTonTai, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maTK = tkDal.LayMaTKTuTen(_tkDangKy.TenDangNhap);
                if (maTK == 0)
                {
                    // Thêm tài khoản vào DB trước
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

                // Đổ dữ liệu vào xe chở hàng (Model)
                PhuHuynh ph = new PhuHuynh();
                ph.HoTen = txtHoTen.Text.Trim();
                ph.SDT = sdt;
                ph.SoNhaDuong = txtDiaChi.Text.Trim();
                ph.MaQuan = Convert.ToInt32(cboQuan.SelectedValue);
                ph.MaTK = maTK;

                // Ra lệnh cho DAL mang đi nộp vào SQL
                if (phDal.ThemPhuHuynh(ph))
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