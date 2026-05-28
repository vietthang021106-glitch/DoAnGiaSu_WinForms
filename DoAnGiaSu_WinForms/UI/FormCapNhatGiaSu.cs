using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using DoAnGiaSu_WinForms.Business;
using DoAnGiaSu_WinForms.DataAccess;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.GUI
{
    public partial class FormCapNhatGiaSu : Form
    {
        private TaiKhoan _tkDangKy;
        private string duongDanAnh = "";
        private string duongDanAnhBangDiem = "";
        private string duongDanAnhChungChi = "";
        private bool extendedUiInitialized = false;

        private Label lblNamHoc;
        private ComboBox cboNamHoc;
        private Label lblThanhTich;
        private TextBox txtThanhTich;
        private Label lblXepLoai;
        private ComboBox cboXepLoai;
        private Label lblChungChi;
        private ComboBox cboChungChi;
        private Label lblDiemChungChi;
        private TextBox txtDiemChungChi;
        private Label lblAnhBangDiem;
        private Button btnChonAnhBangDiem;
        private PictureBox picBangDiem;
        private Label lblAnhChungChi;
        private Button btnChonAnhChungChi;
        private PictureBox picChungChi;
        private Label lblGPA;
        private TextBox txtGPA;

        private readonly DanhMucService danhMucService = new DanhMucService();
        private readonly PhuHuynhService phService = new PhuHuynhService();
        private readonly GiaSuDAL gsDal = new GiaSuDAL();
        private readonly TaiKhoanDAL tkDal = new TaiKhoanDAL();

        public FormCapNhatGiaSu(TaiKhoan tk)
        {
            InitializeComponent();
            _tkDangKy = tk;

            ApplySameBackgroundAsLogin();
            Resize += FormCapNhatGiaSu_Resize;
            Shown += FormCapNhatGiaSu_Shown;
            FormClosed += FormCapNhatGiaSu_FormClosed;
            AttachSizeChangedHandlers(this);
            AutoScroll = true;
            ApplyRoundedStyle();
            CenterPanel();
        }

        private void ApplySameBackgroundAsLogin()
        {
            var frmLogin = new FormDangNhap();
            BackgroundImage = frmLogin.BackgroundImage;
            BackgroundImageLayout = frmLogin.BackgroundImageLayout;
        }

        private void CenterPanel()
        {
            if (!Controls.ContainsKey("panel1")) return;
            Control panel1 = Controls["panel1"];
            int margin = 20;
            int left = (ClientSize.Width - panel1.Width) / 2;
            if (left < margin) left = margin;
            int top = panel1.Height + margin * 2 <= ClientSize.Height ? (ClientSize.Height - panel1.Height) / 2 : margin;
            panel1.Location = new Point(left, top);
            AutoScrollMinSize = new Size(panel1.Width + margin * 2, panel1.Height + margin * 2);
        }

        private void FormCapNhatGiaSu_Shown(object? sender, EventArgs e)
        {
            ApplyRoundedStyle();
            CenterPanel();
        }

        private void FormCapNhatGiaSu_FormClosed(object? sender, FormClosedEventArgs e)
        {
            var formDangKy = Application.OpenForms["FormDangKy"];
            if (formDangKy != null)
                formDangKy.Show();
        }

        private void FormCapNhatGiaSu_Resize(object? sender, EventArgs e)
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

        private void FormCapNhatGiaSu_Load(object sender, EventArgs e)
        {
            Text = "Hồ sơ gia sư của tài khoản: " + _tkDangKy.TenDangNhap;
            LoadAllComboBox();
            InitializeExtendedTutorControls();
            cboTrinhDo.SelectedIndexChanged -= cboTrinhDo_SelectedIndexChanged;
            cboTrinhDo.SelectedIndexChanged += cboTrinhDo_SelectedIndexChanged;
            UpdateHocVanUI();
        }

        private void LoadAllComboBox()
        {
            try
            {
                cboGioiTinh.DataSource = danhMucService.LayGioiTinh();
                cboGioiTinh.DisplayMember = "TenGioiTinh";
                cboGioiTinh.ValueMember = "MaGioiTinh";

                cboTruong.DataSource = danhMucService.LayTruong();
                cboTruong.DisplayMember = "TenTruong";
                cboTruong.ValueMember = "MaTruong";

                cboTrinhDo.DataSource = danhMucService.LayTrinhDo();
                cboTrinhDo.DisplayMember = "TenTrinhDo";
                cboTrinhDo.ValueMember = "MaTrinhDo";

                cboNamSinh.DataSource = danhMucService.LayNamSinh();
                cboNamSinh.DisplayMember = "Nam";
                cboNamSinh.ValueMember = "MaNamSinh";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách: " + ex.Message);
            }
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            ofd.Title = "Chọn ảnh minh chứng";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                duongDanAnh = ofd.FileName;
                picMinhChung.Image = Image.FromFile(duongDanAnh);
            }
        }

        private void btnChonAnhBangDiem_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            ofd.Title = "Chọn ảnh bảng điểm";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                duongDanAnhBangDiem = ofd.FileName;
                picBangDiem.Image = Image.FromFile(duongDanAnhBangDiem);
            }
        }

        private void btnChonAnhChungChi_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            ofd.Title = "Chọn ảnh chứng chỉ";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                duongDanAnhChungChi = ofd.FileName;
                picChungChi.Image = Image.FromFile(duongDanAnhChungChi);
            }
        }

        private void btnLuuHoSo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtSDT.Text) || string.IsNullOrWhiteSpace(txtCCCD.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ Họ tên, SDT và CCCD!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(duongDanAnh))
                {
                    MessageBox.Show("Vui lòng chọn ảnh thẻ sinh viên hoặc giáo viên làm minh chứng!");
                    return;
                }

                if (string.IsNullOrWhiteSpace(duongDanAnhBangDiem))
                {
                    MessageBox.Show("Vui lòng chọn ảnh bảng điểm hoặc bằng tốt nghiệp!");
                    return;
                }

                bool laSV = LaSinhVien();
                if (laSV && (cboNamHoc == null || cboNamHoc.SelectedItem == null || string.IsNullOrWhiteSpace(cboNamHoc.Text)))
                {
                    MessageBox.Show("Vui lòng chọn năm học cho gia sư là sinh viên!");
                    return;
                }

                if (!laSV && (cboXepLoai == null || cboXepLoai.SelectedItem == null || string.IsNullOrWhiteSpace(cboXepLoai.Text)))
                {
                    MessageBox.Show("Vui lòng chọn xếp loại bằng cho giáo viên!");
                    return;
                }

                string sdt = txtSDT.Text.Trim();
                string cccd = txtCCCD.Text.Trim();

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
                    MessageBox.Show("Không tìm thấy ID tài khoản! Vui lòng thử lại.");
                    return;
                }

                GiaSu gs = new GiaSu
                {
                    HoTen = txtHoTen.Text.Trim(),
                    SDT = sdt,
                    CCCD = cccd,
                    MaGioiTinh = Convert.ToInt32(cboGioiTinh.SelectedValue),
                    MaNamSinh = Convert.ToInt32(cboNamSinh.SelectedValue),
                    MaTruong = Convert.ToInt32(cboTruong.SelectedValue),
                    MaTrinhDo = Convert.ToInt32(cboTrinhDo.SelectedValue),
                    TrangThaiDuyet = GiaSuSql.TrangThaiChoDuyet,
                    AnhMinhChung = CopyProofImage(duongDanAnh, _tkDangKy.TenDangNhap, "the"),
                    AnhBangDiem = CopyProofImage(duongDanAnhBangDiem, _tkDangKy.TenDangNhap, "bang"),
                    DiemChungChi = txtDiemChungChi?.Text.Trim() ?? "",
                    MaNamHoc = null,
                    DiemGPA = null,
                    MaXepLoai = null,
                    MaChungChi = null,
                    AnhChungChi = ""
                };

                if (laSV)
                {
                    gs.MaNamHoc = cboNamHoc?.SelectedValue != null && int.TryParse(cboNamHoc.SelectedValue.ToString(), out int maNamHoc) ? maNamHoc : null;
                    gs.DiemGPA = txtGPA?.Text.Trim();
                    gs.MaXepLoai = null;
                }
                else
                {
                    gs.MaNamHoc = null;
                    gs.DiemGPA = null;
                    gs.MaXepLoai = cboXepLoai?.SelectedValue != null && int.TryParse(cboXepLoai.SelectedValue.ToString(), out int maXepLoai) ? maXepLoai : null;
                }

                if (cboChungChi != null && cboChungChi.SelectedItem != null)
                {
                    string tenChungChi = cboChungChi.Text?.Trim() ?? "";
                    if (!string.IsNullOrWhiteSpace(tenChungChi) && !tenChungChi.Equals("Không có", StringComparison.OrdinalIgnoreCase))
                    {
                        if (cboChungChi.SelectedValue != null && int.TryParse(cboChungChi.SelectedValue.ToString(), out int maCC))
                            gs.MaChungChi = maCC;
                        else if (int.TryParse(cboChungChi.Text, out int maCCTuText))
                            gs.MaChungChi = maCCTuText;

                        if (string.IsNullOrWhiteSpace(duongDanAnhChungChi))
                        {
                            MessageBox.Show("Vui lòng chọn ảnh minh chứng chứng chỉ ngoại ngữ!");
                            return;
                        }

                        gs.AnhChungChi = CopyProofImage(duongDanAnhChungChi, _tkDangKy.TenDangNhap, "cc");
                    }
                }

                gs.MaTK = maTK;

                if (gsDal.ThemGiaSu(gs))
                {
                    MessageBox.Show("Hồ sơ đã gửi thành công! Vui lòng chờ Admin duyệt hồ sơ trước khi đăng nhập.", "Thông báo");
                    var loginForm = Application.OpenForms["FormDangNhap"];
                    if (loginForm != null)
                        loginForm.Show();
                    else
                        new FormDangNhap().Show();

                    var formDangKy = Application.OpenForms["FormDangKy"];
                    if (formDangKy != null)
                        formDangKy.Close();

                    Hide();
                }
                else
                {
                    MessageBox.Show("Gửi hồ sơ thất bại! Kiểm tra lại kết nối SQL.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message);
            }
        }

        private void InitializeExtendedTutorControls()
        {
            if (extendedUiInitialized || !Controls.ContainsKey("panel1")) return;

            Control pnl = Controls["panel1"];

            lblNamHoc = new Label { Text = "Năm học", AutoSize = true, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold), ForeColor = Color.FromArgb(24, 33, 53), Location = new Point(60, 730) };
            cboNamHoc = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 11F), FlatStyle = FlatStyle.Flat, Location = new Point(60, 760), Size = new Size(180, 33) };

            lblThanhTich = new Label { Text = "Điểm GPA", AutoSize = true, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold), ForeColor = Color.FromArgb(24, 33, 53), Location = new Point(260, 730) };
            txtThanhTich = new TextBox { Font = new Font("Segoe UI", 11F), BorderStyle = BorderStyle.FixedSingle, Location = new Point(260, 760), Size = new Size(200, 32) };

            lblXepLoai = new Label { Text = "Xếp loại Bằng", AutoSize = true, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold), ForeColor = Color.FromArgb(24, 33, 53), Location = new Point(260, 730), Visible = false };
            cboXepLoai = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 11F), FlatStyle = FlatStyle.Flat, Location = new Point(260, 760), Size = new Size(200, 33), Visible = false };

            lblAnhBangDiem = new Label { Text = "Ảnh Bảng điểm", AutoSize = true, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold), ForeColor = Color.FromArgb(24, 33, 53), Location = new Point(60, 805) };
            btnChonAnhBangDiem = new Button { Text = "Chọn ảnh", BackColor = Color.FromArgb(210, 215, 223), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(40, 55, 80), Location = new Point(60, 835), Size = new Size(100, 35) };
            btnChonAnhBangDiem.FlatAppearance.BorderSize = 0;
            btnChonAnhBangDiem.Click += btnChonAnhBangDiem_Click;
            picBangDiem = new PictureBox { BorderStyle = BorderStyle.FixedSingle, Location = new Point(170, 805), Size = new Size(290, 105), SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.White };

            lblChungChi = new Label { Text = "Chứng chỉ", AutoSize = true, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold), ForeColor = Color.FromArgb(24, 33, 53), Location = new Point(60, 920) };
            cboChungChi = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = new Font("Segoe UI", 11F), FlatStyle = FlatStyle.Flat, Location = new Point(60, 950), Size = new Size(180, 33) };
            lblDiemChungChi = new Label { Text = "Điểm chứng chỉ", AutoSize = true, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold), ForeColor = Color.FromArgb(24, 33, 53), Location = new Point(260, 920) };
            txtDiemChungChi = new TextBox { Font = new Font("Segoe UI", 11F), BorderStyle = BorderStyle.FixedSingle, Location = new Point(260, 950), Size = new Size(200, 32) };

            lblAnhChungChi = new Label { Text = "Ảnh chứng chỉ", AutoSize = true, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold), ForeColor = Color.FromArgb(24, 33, 53), Location = new Point(60, 995) };
            btnChonAnhChungChi = new Button { Text = "Chọn ảnh", BackColor = Color.FromArgb(210, 215, 223), FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 9F, FontStyle.Bold), ForeColor = Color.FromArgb(40, 55, 80), Location = new Point(60, 1025), Size = new Size(100, 35) };
            btnChonAnhChungChi.FlatAppearance.BorderSize = 0;
            btnChonAnhChungChi.Click += btnChonAnhChungChi_Click;
            picChungChi = new PictureBox { BorderStyle = BorderStyle.FixedSingle, Location = new Point(170, 995), Size = new Size(290, 105), SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.White, Visible = true };

            lblGPA = new Label { Text = "GPA", AutoSize = true, BackColor = Color.Transparent, Font = new Font("Segoe UI", 10.5F, FontStyle.Bold), ForeColor = Color.FromArgb(24, 33, 53), Location = new Point(60, 1130) };
            txtGPA = new TextBox { Font = new Font("Segoe UI", 11F), BorderStyle = BorderStyle.FixedSingle, Location = new Point(60, 1160), Size = new Size(200, 32), Visible = false };

            pnl.Controls.Add(lblNamHoc);
            pnl.Controls.Add(cboNamHoc);
            pnl.Controls.Add(lblThanhTich);
            pnl.Controls.Add(txtThanhTich);
            pnl.Controls.Add(lblXepLoai);
            pnl.Controls.Add(cboXepLoai);
            pnl.Controls.Add(lblAnhBangDiem);
            pnl.Controls.Add(btnChonAnhBangDiem);
            pnl.Controls.Add(picBangDiem);
            pnl.Controls.Add(lblChungChi);
            pnl.Controls.Add(cboChungChi);
            pnl.Controls.Add(lblDiemChungChi);
            pnl.Controls.Add(txtDiemChungChi);
            pnl.Controls.Add(lblAnhChungChi);
            pnl.Controls.Add(btnChonAnhChungChi);
            pnl.Controls.Add(picChungChi);
            pnl.Controls.Add(lblGPA);
            pnl.Controls.Add(txtGPA);

            if (button1 != null)
                button1.Location = new Point(60, 1220);

            pnl.Size = new Size(520, 1280);
            ClientSize = new Size(800, 920);

            LoadBoSungDanhMuc();
            AttachSizeChangedHandlers(pnl);
            ApplyRoundedStyle();
            extendedUiInitialized = true;
        }

        private void LoadBoSungDanhMuc()
        {
            try
            {
                cboNamHoc.DataSource = danhMucService.LayNamHoc();
                cboNamHoc.ValueMember = "MaNamHoc";
                cboNamHoc.DisplayMember = "TenNamHoc";
            }
            catch
            {
                cboNamHoc.DataSource = null;
                cboNamHoc.Items.Clear();
                cboNamHoc.Items.AddRange(new object[] { "1", "2", "3", "4", "5", "6" });
            }

            try
            {
                cboXepLoai.DataSource = danhMucService.LayXepLoai();
                cboXepLoai.ValueMember = "MaXepLoai";
                cboXepLoai.DisplayMember = "TenXepLoai";
            }
            catch
            {
                cboXepLoai.DataSource = null;
                cboXepLoai.Items.Clear();
                cboXepLoai.Items.AddRange(new object[] { "Xuất sắc", "Giỏi", "Khá", "Trung bình" });
                if (cboXepLoai.Items.Count > 0) cboXepLoai.SelectedIndex = 0;
            }

            try
            {
                cboChungChi.DataSource = danhMucService.LayChungChi();
                cboChungChi.ValueMember = "MaChungChi";
                cboChungChi.DisplayMember = "TenChungChi";
            }
            catch
            {
                cboChungChi.DataSource = null;
                cboChungChi.Items.Clear();
                cboChungChi.Items.AddRange(new object[] { "Không có", "IELTS", "TOEIC", "HSK", "JLPT" });
                cboChungChi.SelectedIndex = 0;
            }
        }

        private void cboTrinhDo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            UpdateHocVanUI();
        }

        private bool LaSinhVien()
        {
            string text = cboTrinhDo.Text?.Trim().ToLower() ?? "";
            return text.Contains("sinh viên") || text.Contains("sinh vien");
        }

        private void UpdateHocVanUI()
        {
            bool laSV = LaSinhVien();

            if (lblNamHoc != null) lblNamHoc.Visible = laSV;
            if (cboNamHoc != null) cboNamHoc.Visible = laSV;
            if (lblThanhTich != null) lblThanhTich.Visible = laSV;
            if (txtThanhTich != null) txtThanhTich.Visible = laSV;
            if (lblGPA != null) lblGPA.Visible = laSV;
            if (txtGPA != null) txtGPA.Visible = laSV;
            if (lblXepLoai != null) lblXepLoai.Visible = !laSV;
            if (cboXepLoai != null) cboXepLoai.Visible = !laSV;
            if (lblAnhBangDiem != null) lblAnhBangDiem.Text = laSV ? "Ảnh Bảng điểm" : "Ảnh Bằng Tốt Nghiệp";
        }

        private static string CopyProofImage(string sourcePath, string username, string suffix)
        {
            if (string.IsNullOrWhiteSpace(sourcePath) || !File.Exists(sourcePath)) return "";

            string folderPath = Path.Combine(Application.StartupPath, "MinhChung");
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            string fileExtension = Path.GetExtension(sourcePath);
            string newFileName = $"{username}_{suffix}_{DateTime.Now.Ticks}{fileExtension}";
            string destPath = Path.Combine(folderPath, newFileName);
            File.Copy(sourcePath, destPath, true);
            return destPath;
        }
    }
}