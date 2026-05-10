using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using DoAnGiaSu_WinForms.DAL;

namespace DoAnGiaSu_WinForms.GUI
{
    public partial class FormMainAdmin : Form
    {
        BaiDangDAL bdDal = new BaiDangDAL();
        GiaSuDAL gsDal = new GiaSuDAL();
        private Panel pnlTutorPreview;
        private PictureBox picBangDiem;
        private PictureBox picChungChi;

        public FormMainAdmin()
        {
            InitializeComponent();
            ApplySameBackgroundAsLogin();
            Resize += FormMainAdmin_Resize;
            Shown += FormMainAdmin_Shown;
            FormClosed += FormMainAdmin_FormClosed;
            AttachSizeChangedHandlers(this);
            ApplyRoundedStyle();

            SetupDataGridView(dgvDuyetBai);
            SetupDataGridView(dgvDuyetGiaSu);
            SetupDataGridView(dgvHoaHong);

            EnsureTutorPreviewLayout();
            dgvDuyetGiaSu.SelectionChanged += dgvDuyetGiaSu_SelectionChanged;

            WindowState = FormWindowState.Maximized;

            // Set initial selected tab style
            tabControl1_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void CenterPanel()
        {
            if (panel1 != null)
            {
                panel1.Left = (ClientSize.Width - panel1.Width) / 2;
                panel1.Top = (ClientSize.Height - panel1.Height) / 2;
            }
        }

        private void FormMainAdmin_FormClosed(object? sender, FormClosedEventArgs e)
        {
            // Trở về màng hình đăng nhập khi đóng form
            var loginForm = Application.OpenForms["FormDangNhap"];
            loginForm?.Show();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ApplySameBackgroundAsLogin()
        {
            var frmLogin = new FormDangNhap();
            this.BackgroundImage = frmLogin.BackgroundImage;
            this.BackgroundImageLayout = frmLogin.BackgroundImageLayout;
        }

        private void FormMainAdmin_Shown(object? sender, EventArgs e)
        {
            ApplyRoundedStyle();
            CenterPanel();
        }

        private void FormMainAdmin_Resize(object? sender, EventArgs e)
        {
            ApplyRoundedStyle();
            CenterPanel();
            AdjustGiaSuGridLayout();
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

        private void FormMainAdmin_Load(object sender, EventArgs e)
        {
            LoadDataTatCaBai();
        }

        // --- TAB 1: QUẢN LÝ BÀI ĐĂNG ---
        private void LoadDataTatCaBai()
        {
            try
            {
                DataTable dt = bdDal.LayTatCaBaiAdmin();
                dgvDuyetBai.DataSource = dt;

                if (dgvDuyetBai.Columns.Count > 0)
                {
                    dgvDuyetBai.Columns["MaBaiDang"].HeaderText = "Mã bài";
                    dgvDuyetBai.Columns["MaBaiDang"].ReadOnly = true;

                    dgvDuyetBai.Columns["HoTen"].HeaderText = "Phụ huynh";
                    dgvDuyetBai.Columns["HoTen"].ReadOnly = true;

                    dgvDuyetBai.Columns["TenMon"].HeaderText = "Môn học";
                    dgvDuyetBai.Columns["TenMon"].ReadOnly = true;

                    dgvDuyetBai.Columns["TenLop"].HeaderText = "Lớp";
                    dgvDuyetBai.Columns["TenLop"].ReadOnly = true;

                    if (dgvDuyetBai.Columns.Contains("TenHinhThuc"))
                    {
                        dgvDuyetBai.Columns["TenHinhThuc"].HeaderText = "Hình thức";
                        dgvDuyetBai.Columns["TenHinhThuc"].ReadOnly = true;
                    }
                    if (dgvDuyetBai.Columns.Contains("TenQuan"))
                    {
                        dgvDuyetBai.Columns["TenQuan"].HeaderText = "Khu vực";
                        dgvDuyetBai.Columns["TenQuan"].ReadOnly = true;
                    }
                    if (dgvDuyetBai.Columns.Contains("SoNhaDuong"))
                    {
                        dgvDuyetBai.Columns["SoNhaDuong"].HeaderText = "Địa chỉ chi tiết";
                        dgvDuyetBai.Columns["SoNhaDuong"].ReadOnly = true;
                    }
                    if (dgvDuyetBai.Columns.Contains("YeuCauThem"))
                    {
                        dgvDuyetBai.Columns["YeuCauThem"].HeaderText = "Yêu cầu thêm";
                        dgvDuyetBai.Columns["YeuCauThem"].ReadOnly = true;
                    }

                    dgvDuyetBai.Columns["MucLuong"].HeaderText = "Mức lương";
                    dgvDuyetBai.Columns["MucLuong"].ReadOnly = false; // Admin có thể sửa

                    dgvDuyetBai.Columns["TrangThai"].HeaderText = "Trạng thái";
                    dgvDuyetBai.Columns["TrangThai"].ReadOnly = true;

                    if (dgvDuyetBai.Columns.Contains("MaGS"))
                    {
                        dgvDuyetBai.Columns["MaGS"].HeaderText = "Mã GS Nhận";
                        dgvDuyetBai.Columns["MaGS"].ReadOnly = true;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi Tab 1: " + ex.Message); }
        }

        // Hàm này để sửa lỗi CS0103 cho nút "Xác nhận duyệt" cũ
        private void btnDuyetBai_Click(object sender, EventArgs e)
        {
            // Bây giờ nút này đóng vai trò Xóa bài (hoặc bạn đổi Text nút thành Xóa bài)
            btnXoaBai_Click(sender, e);
        }

        private void btnXoaBai_Click(object sender, EventArgs e)
        {
            if (dgvDuyetBai.CurrentRow == null) return;
            int maBD = Convert.ToInt32(dgvDuyetBai.CurrentRow.Cells["MaBaiDang"].Value);

            if (MessageBox.Show("Xóa bài đăng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (bdDal.XoaBaiDang(maBD))
                {
                    MessageBox.Show("Đã xóa!");
                    LoadDataTatCaBai();
                }
            }
        }

        // --- TAB 3: XÁC NHẬN HOA HỒNG ---
        private void LoadDataHoaHong()
        {
            try
            {
                dgvHoaHong.DataSource = bdDal.LayBaiChoDuyetPhi();

                if (dgvHoaHong.Columns.Count > 0)
                {
                    dgvHoaHong.Columns["MaBaiDang"].HeaderText = "Mã bài";
                    dgvHoaHong.Columns["TenPhuHuynh"].HeaderText = "Phụ huynh";
                    dgvHoaHong.Columns["TenMon"].HeaderText = "Môn học";
                    dgvHoaHong.Columns["MucLuong"].HeaderText = "Mức lương";
                    dgvHoaHong.Columns["HoaHong"].HeaderText = "Hoa hồng (Mức lương x2)";
                    dgvHoaHong.Columns["TrangThai"].HeaderText = "Trạng thái";

                    if (dgvHoaHong.Columns.Contains("MaGS"))
                    {
                        dgvHoaHong.Columns["MaGS"].HeaderText = "Mã GS";
                    }

                    if (dgvHoaHong.Columns.Contains("AnhChuyenKhoan"))
                    {
                        dgvHoaHong.Columns["AnhChuyenKhoan"].Visible = false;
                    }

                    if (!dgvHoaHong.Columns.Contains("XemAnhCK"))
                    {
                        DataGridViewButtonColumn btnXemAnh = new DataGridViewButtonColumn();
                        btnXemAnh.Name = "XemAnhCK";
                        btnXemAnh.HeaderText = "Ảnh chuyển khoản";
                        btnXemAnh.Text = "Xem Ảnh";
                        btnXemAnh.UseColumnTextForButtonValue = true;
                        dgvHoaHong.Columns.Add(btnXemAnh);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi Tab 3: " + ex.Message); }
        }

        private void btnXacNhanTien_Click(object sender, EventArgs e)
        {
            if (dgvHoaHong.CurrentRow == null) return;
            int maBD = Convert.ToInt32(dgvHoaHong.CurrentRow.Cells["MaBaiDang"].Value);

            if (MessageBox.Show("Bạn có chắc chắn muốn xác nhận đã nhận hoa hồng cho bài đăng này? Lớp sẽ được chuyển sang trạng thái 'Đã giao'.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (bdDal.XacNhanHoaHong(maBD))
                {
                    MessageBox.Show("Chốt đơn! Trạng thái chuyển sang 'DaGiao'.");
                    LoadDataHoaHong();
                }
            }
        }

        private void btnTuChoiTien_Click(object sender, EventArgs e)
        {
            if (dgvHoaHong.CurrentRow == null) return;
            int maBD = Convert.ToInt32(dgvHoaHong.CurrentRow.Cells["MaBaiDang"].Value);

            if (MessageBox.Show("Từ chối bill chuyển khoản này? Lớp sẽ giữ trạng thái 'DangGiaoDich' để gia sư nộp lại bill.", "Xác nhận từ chối", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (bdDal.TuChoiHoaHong(maBD))
                {
                    MessageBox.Show("Đã từ chối bill. Gia sư cần tải lại minh chứng thanh toán.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataHoaHong();
                }
                else
                {
                    MessageBox.Show("Từ chối bill thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvHoaHong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
                if (dgvHoaHong.Columns[e.ColumnIndex].Name != "XemAnhCK") return;

                object anhObj = dgvHoaHong.Rows[e.RowIndex].Cells["AnhChuyenKhoan"].Value;
                int maBD = Convert.ToInt32(dgvHoaHong.Rows[e.RowIndex].Cells["MaBaiDang"].Value);
                bool daCoAnhHopLe = false;

                Form frmAnh = new Form();
                frmAnh.Text = "Xem Ảnh Chuyển Khoản";
                frmAnh.Size = new Size(600, 800);
                frmAnh.StartPosition = FormStartPosition.CenterScreen;
                frmAnh.BackColor = Color.Black;

                PictureBox pic = new PictureBox();
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                pic.Dock = DockStyle.Fill;

                if (anhObj == null || anhObj == DBNull.Value)
                {
                    byte[] anhTuDb = bdDal.LayAnhChuyenKhoanTheoBaiDang(maBD);
                    if (anhTuDb != null && anhTuDb.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(anhTuDb))
                        {
                            pic.Image = Image.FromStream(ms);
                            daCoAnhHopLe = true;
                        }
                    }
                }
                else if (anhObj is byte[] imageBytes)
                {
                    if (imageBytes.Length > 0)
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        pic.Image = Image.FromStream(ms);
                        daCoAnhHopLe = true;
                    }
                }
                else if (anhObj is string path && !string.IsNullOrWhiteSpace(path) && File.Exists(path))
                {
                    pic.Image = Image.FromFile(path);
                    daCoAnhHopLe = true;
                }
                else
                {
                    byte[] anhTuDb = bdDal.LayAnhChuyenKhoanTheoBaiDang(maBD);
                    if (anhTuDb != null && anhTuDb.Length > 0)
                    {
                        using (MemoryStream ms = new MemoryStream(anhTuDb))
                        {
                            pic.Image = Image.FromStream(ms);
                            daCoAnhHopLe = true;
                        }
                    }
                }

                if (!daCoAnhHopLe)
                {
                    MessageBox.Show("Gia sư chưa tải ảnh chuyển khoản lên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                frmAnh.Controls.Add(pic);
                frmAnh.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải ảnh. Dữ liệu có thể không đúng định dạng. Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset button colors
            if (btnNavBaiDang != null) btnNavBaiDang.BackColor = Color.Transparent;
            if (btnNavGiaSu != null) btnNavGiaSu.BackColor = Color.Transparent;
            if (btnNavHoaHong != null) btnNavHoaHong.BackColor = Color.Transparent;

            switch (tabControl1.SelectedIndex)
            {
                case 0: 
                    LoadDataTatCaBai(); 
                    if (btnNavBaiDang != null) btnNavBaiDang.BackColor = Color.FromArgb(24, 119, 242);
                    break;
                case 1:
                    LoadDataGiaSu();
                    AdjustGiaSuGridLayout();
                    if (btnNavGiaSu != null) btnNavGiaSu.BackColor = Color.FromArgb(24, 119, 242);
                    break;
                case 2: 
                    LoadDataHoaHong(); 
                    if (btnNavHoaHong != null) btnNavHoaHong.BackColor = Color.FromArgb(24, 119, 242);
                    break;
            }
        }

        // Hàm này để sửa lỗi CS0103 cho sự kiện Click của tabPage1
        private void tabPage1_Click(object sender, EventArgs e)
        {
        }

        private void dgvDuyetBai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvDuyetBai_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvDuyetBai.Rows[e.RowIndex];
                int maBD = Convert.ToInt32(row.Cells["MaBaiDang"].Value);
                string colName = dgvDuyetBai.Columns[e.ColumnIndex].Name;

                if (colName == "MucLuong")
                {
                    string newVal = row.Cells[e.ColumnIndex].Value?.ToString() ?? "";

                    if (bdDal.CapNhatNhanh(maBD, colName, newVal))
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnNavBaiDang_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void btnNavGiaSu_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }

        private void btnNavHoaHong_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        private void LoadDataGiaSu()
        {
            try
            {
                DataTable dt = gsDal.LayTatCaGiaSuAdmin();
                dgvDuyetGiaSu.DataSource = dt;

                if (dgvDuyetGiaSu.Columns.Count > 0)
                {
                    dgvDuyetGiaSu.Columns["MaGS"].HeaderText = "Mã GS";
                    dgvDuyetGiaSu.Columns["MaGS"].ReadOnly = true;

                    dgvDuyetGiaSu.Columns["HoTen"].HeaderText = "Họ Tên";
                    dgvDuyetGiaSu.Columns["HoTen"].ReadOnly = true;

                    dgvDuyetGiaSu.Columns["SDT"].HeaderText = "SĐT";
                    dgvDuyetGiaSu.Columns["SDT"].ReadOnly = true;

                    dgvDuyetGiaSu.Columns["CCCD"].HeaderText = "CCCD";
                    dgvDuyetGiaSu.Columns["CCCD"].ReadOnly = true;

                    dgvDuyetGiaSu.Columns["TenTruong"].HeaderText = "Trường";
                    dgvDuyetGiaSu.Columns["TenTruong"].ReadOnly = true;

                    dgvDuyetGiaSu.Columns["TenTrinhDo"].HeaderText = "Trình Độ";
                    dgvDuyetGiaSu.Columns["TenTrinhDo"].ReadOnly = true;

                    if (dgvDuyetGiaSu.Columns.Contains("MaNamHoc"))
                    {
                        dgvDuyetGiaSu.Columns["MaNamHoc"].HeaderText = "Năm học";
                        dgvDuyetGiaSu.Columns["MaNamHoc"].ReadOnly = true;
                    }
                    if (dgvDuyetGiaSu.Columns.Contains("ThanhTich"))
                    {
                        dgvDuyetGiaSu.Columns["ThanhTich"].HeaderText = "Thành tích";
                        dgvDuyetGiaSu.Columns["ThanhTich"].ReadOnly = true;
                    }

                    if (dgvDuyetGiaSu.Columns.Contains("DiemChungChi"))
                    {
                        dgvDuyetGiaSu.Columns["DiemChungChi"].HeaderText = "Điểm CC";
                        dgvDuyetGiaSu.Columns["DiemChungChi"].ReadOnly = true;
                    }

                    if (dgvDuyetGiaSu.Columns.Contains("DiemTrungBinh"))
                    {
                        dgvDuyetGiaSu.Columns["DiemTrungBinh"].Visible = false;
                    }
                    if (dgvDuyetGiaSu.Columns.Contains("SoLuotDanhGia"))
                    {
                        dgvDuyetGiaSu.Columns["SoLuotDanhGia"].Visible = false;
                    }
                    if (dgvDuyetGiaSu.Columns.Contains("colRating"))
                    {
                        dgvDuyetGiaSu.Columns["colRating"].HeaderText = "Đánh giá";
                        dgvDuyetGiaSu.Columns["colRating"].ReadOnly = true;
                        dgvDuyetGiaSu.Columns["colRating"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                    if (dgvDuyetGiaSu.Columns.Contains("ChungChi"))
                    {
                        dgvDuyetGiaSu.Columns["ChungChi"].DataPropertyName = "TenChungChi";
                        dgvDuyetGiaSu.Columns["ChungChi"].HeaderText = "Chứng chỉ";
                        dgvDuyetGiaSu.Columns["ChungChi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    else if (dgvDuyetGiaSu.Columns.Contains("TenChungChi"))
                    {
                        dgvDuyetGiaSu.Columns["TenChungChi"].HeaderText = "Chứng chỉ";
                        dgvDuyetGiaSu.Columns["TenChungChi"].ReadOnly = true;
                        dgvDuyetGiaSu.Columns["TenChungChi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                    if (dgvDuyetGiaSu.Columns.Contains("ThongTinChungChi"))
                    {
                        dgvDuyetGiaSu.Columns["ThongTinChungChi"].Visible = false;
                    }

                    dgvDuyetGiaSu.Columns["TrangThaiDuyet"].HeaderText = "Trạng Thái";
                    dgvDuyetGiaSu.Columns["TrangThaiDuyet"].ReadOnly = false;

                    if (dgvDuyetGiaSu.Columns.Contains("AnhMinhChung")) dgvDuyetGiaSu.Columns["AnhMinhChung"].Visible = false;
                    if (dgvDuyetGiaSu.Columns.Contains("AnhBangDiem")) dgvDuyetGiaSu.Columns["AnhBangDiem"].Visible = false;
                    if (dgvDuyetGiaSu.Columns.Contains("AnhChungChi")) dgvDuyetGiaSu.Columns["AnhChungChi"].Visible = false;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi Tab 2: " + ex.Message); }
        }

        private void dgvDuyetGiaSu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvDuyetGiaSu.Columns[e.ColumnIndex].Name == "XemAnh")
            {
                string path = dgvDuyetGiaSu.Rows[e.RowIndex].Cells["AnhMinhChung"].Value?.ToString() ?? "";
                if (!string.IsNullOrEmpty(path) && System.IO.File.Exists(path))
                {
                    Form frmAnh = new Form();
                    frmAnh.Text = "Xem Ảnh Minh Chứng";
                    frmAnh.Size = new Size(800, 800);
                    frmAnh.StartPosition = FormStartPosition.CenterScreen;
                    frmAnh.BackColor = Color.Black;

                    PictureBox pic = new PictureBox();
                    pic.Image = System.Drawing.Image.FromFile(path);
                    pic.SizeMode = PictureBoxSizeMode.Zoom;
                    pic.Dock = DockStyle.Fill;

                    frmAnh.Controls.Add(pic);
                    frmAnh.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Gia sư này chưa cập nhật ảnh minh chứng hoặc sai đường dẫn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dgvDuyetGiaSu_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvDuyetGiaSu.Rows[e.RowIndex];
                int maGS = Convert.ToInt32(row.Cells["MaGS"].Value);
                string colName = dgvDuyetGiaSu.Columns[e.ColumnIndex].Name;

                if (colName == "TrangThaiDuyet")
                {
                    string newVal = row.Cells[e.ColumnIndex].Value?.ToString() ?? "";
                    if (newVal != "ChoDuyet" && newVal != "DaDuyet" && newVal != "TuChoi")
                    {
                        MessageBox.Show("Trạng thái chỉ được nhập 'ChoDuyet', 'DaDuyet' hoặc 'TuChoi'", "Cảnh báo");
                        LoadDataGiaSu();
                        return;
                    }

                    if (gsDal.CapNhatTrangThaiDuyet(maGS, newVal))
                    {
                        MessageBox.Show("Cập nhật trạng thái gia sư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGiaSu();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDuyetGiaSu_Click(object sender, EventArgs e)
        {
            if (dgvDuyetGiaSu.SelectedRows.Count > 0)
            {
                int maGS = Convert.ToInt32(dgvDuyetGiaSu.SelectedRows[0].Cells["MaGS"].Value);
                if (MessageBox.Show("Bạn có chắc chắn muốn duyệt gia sư này?", "Xác nhận duyệt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (gsDal.CapNhatTrangThaiDuyet(maGS, "DaDuyet"))
                    {
                        MessageBox.Show("Đã duyệt gia sư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGiaSu();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn gia sư cần duyệt!", "Cảnh báo");
            }
        }

        private void btnTuChoiGiaSu_Click(object sender, EventArgs e)
        {
            if (dgvDuyetGiaSu.SelectedRows.Count > 0)
            {
                int maGS = Convert.ToInt32(dgvDuyetGiaSu.SelectedRows[0].Cells["MaGS"].Value);
                if (MessageBox.Show("Bạn có chắc chắn muốn từ chối gia sư này?", "Xác nhận từ chối", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (gsDal.CapNhatTrangThaiDuyet(maGS, "TuChoi"))
                    {
                        MessageBox.Show("Đã từ chối gia sư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGiaSu();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn gia sư cần từ chối!", "Cảnh báo");
            }
        }

        private void btnXoaGiaSu_Click(object sender, EventArgs e)
        {
            if (dgvDuyetGiaSu.SelectedRows.Count > 0)
            {
                int maGS = Convert.ToInt32(dgvDuyetGiaSu.SelectedRows[0].Cells["MaGS"].Value);
                DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa gia sư này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    if (gsDal.XoaGiaSu(maGS))
                    {
                        MessageBox.Show("Xóa gia sư thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataGiaSu();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra khi xóa gia sư!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn gia sư cần xóa!", "Cảnh báo");
            }
        }

        private void SetupDataGridView(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(40, 55, 80);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 45;

            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(24, 119, 242);
            dgv.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 11F);

            dgv.RowTemplate.Height = 40;
            dgv.RowHeadersVisible = false;
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.ReadOnly = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.GridColor = Color.FromArgb(210, 215, 223);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        private void EnsureTutorPreviewLayout()
        {
            if (pnlTutorPreview != null) return;

            pnlTutorPreview = new Panel
            {
                BackColor = Color.WhiteSmoke
            };

            Label lblThe = new Label
            {
                Text = "Ảnh thẻ SV/GV hoặc CCCD",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 55, 80),
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                AutoSize = false,
                Margin = new Padding(0, 0, 0, 4)
            };

            picMinhChung.BorderStyle = BorderStyle.FixedSingle;
            picMinhChung.SizeMode = PictureBoxSizeMode.Zoom;
            picMinhChung.Visible = true;
            picMinhChung.Dock = DockStyle.Fill;
            picMinhChung.Margin = new Padding(0, 0, 0, 8);

            Label lblBang = new Label
            {
                Text = "Ảnh bảng điểm / bằng cấp",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 55, 80),
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                AutoSize = false,
                Margin = new Padding(0, 0, 0, 4)
            };

            picBangDiem = new PictureBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 0, 8)
            };

            Label lblCC = new Label
            {
                Text = "Ảnh chứng chỉ",
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 55, 80),
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                AutoSize = false,
                Margin = new Padding(0, 0, 0, 4)
            };

            picChungChi = new PictureBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White,
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };

            var tblPreview = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 6,
                Padding = new Padding(8)
            };
            tblPreview.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblPreview.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tblPreview.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tblPreview.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tblPreview.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            tblPreview.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            tblPreview.RowStyles.Add(new RowStyle(SizeType.Percent, 33.34F));

            tblPreview.Controls.Add(lblThe, 0, 0);
            tblPreview.Controls.Add(picMinhChung, 0, 1);
            tblPreview.Controls.Add(lblBang, 0, 2);
            tblPreview.Controls.Add(picBangDiem, 0, 3);
            tblPreview.Controls.Add(lblCC, 0, 4);
            tblPreview.Controls.Add(picChungChi, 0, 5);

            tabPage2.Controls.Add(pnlTutorPreview);
            pnlTutorPreview.Controls.Add(tblPreview);
            pnlTutorPreview.BringToFront();

            AdjustGiaSuGridLayout();
        }

        private void AdjustGiaSuGridLayout()
        {
            if (pnlTutorPreview == null) return;

            int previewWidth = 300;
            int margin = 20;
            int top = 20;
            int buttonArea = 95;

            pnlTutorPreview.Location = new Point(tabPage2.ClientSize.Width - previewWidth - margin, top);
            pnlTutorPreview.Size = new Size(previewWidth, Math.Max(280, tabPage2.ClientSize.Height - top - buttonArea));

            dgvDuyetGiaSu.Location = new Point(20, 20);
            dgvDuyetGiaSu.Size = new Size(Math.Max(300, pnlTutorPreview.Left - 40), Math.Max(200, tabPage2.ClientSize.Height - 120));
        }

        private static void LoadImageFromPath(PictureBox pic, string path)
        {
            try
            {
                pic.Image?.Dispose();
                pic.Image = null;
                if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                {
                    pic.Image = Image.FromFile(path);
                }
            }
            catch
            {
                pic.Image = null;
            }
        }

        private void dgvDuyetGiaSu_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvDuyetGiaSu.CurrentRow == null) return;

            string anhThe = dgvDuyetGiaSu.Columns.Contains("AnhMinhChung")
                ? dgvDuyetGiaSu.CurrentRow.Cells["AnhMinhChung"]?.Value?.ToString() ?? ""
                : "";

            string anhBang = dgvDuyetGiaSu.Columns.Contains("AnhBangDiem")
                ? dgvDuyetGiaSu.CurrentRow.Cells["AnhBangDiem"]?.Value?.ToString() ?? ""
                : "";

            string anhCC = dgvDuyetGiaSu.Columns.Contains("AnhChungChi")
                ? dgvDuyetGiaSu.CurrentRow.Cells["AnhChungChi"]?.Value?.ToString() ?? ""
                : "";

            LoadImageFromPath(picMinhChung, anhThe);
            if (picBangDiem != null) LoadImageFromPath(picBangDiem, anhBang);
            if (picChungChi != null) LoadImageFromPath(picChungChi, anhCC);
        }

        private static string ChuanHoaHienThiDiemChungChi(string giaTri)
        {
            if (string.IsNullOrWhiteSpace(giaTri)) return "";

            string v = giaTri.Trim();
            v = v.Replace("C?p", "Cấp");
            v = v.Replace("c?p", "cấp");
            return v;
        }
    }
}