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
        private readonly BaiDangDAL bdDal = new BaiDangDAL();
        private readonly GiaSuDAL gsDal = new GiaSuDAL();

        public FormMainAdmin()
        {
            InitializeComponent();
            ApplySameBackgroundAsLogin();
            Resize += FormMainAdmin_Resize;
            Shown += FormMainAdmin_Shown;
            FormClosed += FormMainAdmin_FormClosed;
            AttachSizeChangedHandlers(this);
            ApplyRoundedStyle();
            WindowState = FormWindowState.Maximized;
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
            var loginForm = Application.OpenForms["FormDangNhap"];
            loginForm?.Show();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ApplySameBackgroundAsLogin()
        {
            var frmLogin = new FormDangNhap();
            BackgroundImage = frmLogin.BackgroundImage;
            BackgroundImageLayout = frmLogin.BackgroundImageLayout;
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
            if (cmbLocTrangThai != null && cmbLocTrangThai.Items.Count > 0 && cmbLocTrangThai.SelectedIndex < 0)
            {
                cmbLocTrangThai.SelectedIndex = 0;
            }

            LoadCardBaiDang();
            LoadDataGiaSu();
            LoadDataHoaHong();
        }

        private void LoadCardBaiDang()
        {
            try
            {
                flpBaiDang.Controls.Clear();
                string trangThai = string.IsNullOrWhiteSpace(cmbLocTrangThai?.Text) ? "Tất cả" : cmbLocTrangThai.Text;
                DataTable dt = bdDal.LayTatCaBaiAdmin(trangThai);
                foreach (DataRow row in dt.Rows)
                {
                    var card = new ucAdminBaiDang
                    {
                        MaBaiDang = row["MaBaiDang"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaBaiDang"]),
                        PhuHuynh = row["HoTen"]?.ToString() ?? "",
                        MonHoc = row["TenMon"]?.ToString() ?? "",
                        Lop = row["TenLop"]?.ToString() ?? "",
                        HinhThuc = row.Table.Columns.Contains("TenHinhThuc") ? row["TenHinhThuc"]?.ToString() ?? "" : "",
                        KhuVuc = row.Table.Columns.Contains("TenQuan") ? row["TenQuan"]?.ToString() ?? "" : "",
                        MucLuong = row["MucLuong"]?.ToString() ?? "",
                        TrangThai = row["TrangThai"]?.ToString() ?? "",
                        MaGSNhan = row.Table.Columns.Contains("MaGS") && row["MaGS"] != DBNull.Value ? row["MaGS"].ToString() : ""
                    };
                    card.XoaBaiClicked += (_, _) => XoaBaiDang(card.MaBaiDang);
                    flpBaiDang.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Tab 1: " + ex.Message);
            }
        }

        private void LoadDataTatCaBai()
        {
            LoadCardBaiDang();
        }

        private void XoaBaiDang(int maBD)
        {
            if (MessageBox.Show("Xóa bài đăng này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            if (bdDal.XoaBaiDang(maBD))
                LoadCardBaiDang();
        }

        private void LoadDataHoaHong()
        {
            try
            {
                flpHoaHong.Controls.Clear();
                DataTable dt = bdDal.LayBaiChoDuyetPhi();
                foreach (DataRow row in dt.Rows)
                {
                    var card = new ucAdminHoaHong
                    {
                        MaBaiDang = row["MaBaiDang"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaBaiDang"]),
                        PhuHuynh = row.Table.Columns.Contains("TenPhuHuynh") ? row["TenPhuHuynh"]?.ToString() ?? "" : "",
                        MonHoc = row["TenMon"]?.ToString() ?? "",
                        MucLuong = row["MucLuong"]?.ToString() ?? "",
                        HoaHong = row.Table.Columns.Contains("HoaHong") ? row["HoaHong"]?.ToString() ?? "" : "",
                        TrangThai = row["TrangThai"]?.ToString() ?? "",
                        MaGS = row.Table.Columns.Contains("MaGS") && row["MaGS"] != DBNull.Value ? row["MaGS"].ToString() : ""
                    };
                    if (row.Table.Columns.Contains("AnhChuyenKhoan") && row["AnhChuyenKhoan"] is byte[] bytes && bytes.Length > 0)
                    {
                        using var ms = new MemoryStream(bytes);
                        card.AnhBillImage = Image.FromStream(ms);
                    }
                    card.XemAnhClicked += (_, _) => XemAnhChuyenKhoan(card.MaBaiDang);
                    card.TuChoiBillClicked += (_, _) => TuChoiHoaHong(card.MaBaiDang);
                    card.XacNhanClicked += (_, _) => XacNhanHoaHong(card.MaBaiDang);
                    flpHoaHong.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Tab 3: " + ex.Message);
            }
        }

        private void XemAnhChuyenKhoan(int maBD)
        {
            byte[] anh = bdDal.LayAnhChuyenKhoanTheoBaiDang(maBD);
            if (anh == null || anh.Length == 0)
            {
                MessageBox.Show("Gia sư chưa tải ảnh chuyển khoản lên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Form frm = new Form { Text = "Xem Ảnh Chuyển Khoản", Size = new Size(600, 800), StartPosition = FormStartPosition.CenterScreen };
            PictureBox pic = new PictureBox { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.Zoom };
            using MemoryStream ms = new MemoryStream(anh);
            pic.Image = Image.FromStream(ms);
            frm.Controls.Add(pic);
            frm.ShowDialog();
        }

        private void XacNhanHoaHong(int maBD)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xác nhận đã nhận hoa hồng?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (bdDal.XacNhanHoaHong(maBD))
                    LoadDataHoaHong();
            }
        }

        private void TuChoiHoaHong(int maBD)
        {
            if (MessageBox.Show("Từ chối bill chuyển khoản này?", "Xác nhận từ chối", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (bdDal.TuChoiHoaHong(maBD))
                    LoadDataHoaHong();
            }
        }

        private void LoadDataGiaSu()
        {
            try
            {
                flpGiaSu.Controls.Clear();
                DataTable dt = gsDal.LayTatCaGiaSuAdmin();
                foreach (DataRow row in dt.Rows)
                {
                    var card = new ucAdminGiaSu
                    {
                        MaGS = row["MaGS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaGS"]),
                        HoTen = row["HoTen"]?.ToString() ?? "",
                        SDT = row["SDT"]?.ToString() ?? "",
                        CCCD = row["CCCD"]?.ToString() ?? "",
                        ThanhTich = row.Table.Columns.Contains("ThanhTich") ? row["ThanhTich"]?.ToString() ?? "" : "",
                        Truong = row["TenTruong"]?.ToString() ?? "",
                        TrinhDo = row["TenTrinhDo"]?.ToString() ?? ""
                    };
                    if (row.Table.Columns.Contains("AnhMinhChung")) card.AnhThePath = row["AnhMinhChung"]?.ToString() ?? "";
                    if (row.Table.Columns.Contains("AnhBangDiem")) card.AnhBangDiemPath = row["AnhBangDiem"]?.ToString() ?? "";
                    if (row.Table.Columns.Contains("AnhChungChi")) card.AnhChungChiPath = row["AnhChungChi"]?.ToString() ?? "";
                    card.DuyetClicked += (_, _) => DuyetGiaSu(card.MaGS);
                    card.TuChoiClicked += (_, _) => TuChoiGiaSu(card.MaGS);
                    card.XoaClicked += (_, _) => XoaGiaSu(card.MaGS);
                    flpGiaSu.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Tab 2: " + ex.Message);
            }
        }

        private void DuyetGiaSu(int maGS)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn duyệt gia sư này?", "Xác nhận duyệt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (gsDal.CapNhatTrangThaiDuyet(maGS, "DaDuyet"))
                    LoadDataGiaSu();
            }
        }

        private void TuChoiGiaSu(int maGS)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn từ chối gia sư này?", "Xác nhận từ chối", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (gsDal.CapNhatTrangThaiDuyet(maGS, "TuChoi"))
                    LoadDataGiaSu();
            }
        }

        private void XoaGiaSu(int maGS)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa gia sư này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (gsDal.XoaGiaSu(maGS))
                    LoadDataGiaSu();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                    if (btnNavGiaSu != null) btnNavGiaSu.BackColor = Color.FromArgb(24, 119, 242);
                    break;
                case 2:
                    LoadDataHoaHong();
                    if (btnNavHoaHong != null) btnNavHoaHong.BackColor = Color.FromArgb(24, 119, 242);
                    break;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
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

        private void cmbLocTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadCardBaiDang();
        }
    }
}