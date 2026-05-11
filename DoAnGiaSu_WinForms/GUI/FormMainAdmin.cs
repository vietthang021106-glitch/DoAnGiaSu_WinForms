using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using DoAnGiaSu_WinForms.DAL;

namespace DoAnGiaSu_WinForms.GUI
{
    public partial class FormMainAdmin : Form
    {
        private bool _isFormLoaded = false;
        private readonly BaiDangDAL bdDal = new BaiDangDAL();
        private readonly GiaSuDAL gsDal = new GiaSuDAL();

        public FormMainAdmin()
        {
            InitializeComponent();
            typeof(FlowLayoutPanel).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(flpGiaSu, true);
            typeof(FlowLayoutPanel).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(flpBaiDang, true);
            flpBaiDang.SizeChanged += (_, _) => UpdateBaiDangLayout();
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

        private async void FormMainAdmin_Shown(object? sender, EventArgs e)
        {
            ApplyRoundedStyle();
            CenterPanel();
            await LoadCardBaiDangAsync();
            await LoadDanhSachGiaSuAsync();
            LoadDataHoaHong();
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
            _isFormLoaded = true;
        }

        public async Task LoadCardBaiDangAsync()
        {
            try
            {
                flpBaiDang.SuspendLayout();
                flpBaiDang.Controls.Clear();
                string trangThai = string.IsNullOrWhiteSpace(cmbLocTrangThai?.Text) ? "Tất cả" : cmbLocTrangThai.Text;
                var cards = new List<Control>();

                using SqlConnection conn = new DBConnection().GetConnection();
                await conn.OpenAsync();

                const string query = @"SELECT bd.MaBaiDang, ph.HoTen, m.TenMon, l.TenLop, td.TenTrinhDo, ht.TenHinhThuc, q.TenQuan, bd.SoNhaDuong, bd.YeuCauThem, bd.MucLuong, bd.TrangThai, bd.MaGS
                                FROM BAIDANG bd
                                LEFT JOIN PHUHUYNH ph ON bd.MaPH = ph.MaPH
                                LEFT JOIN DM_MONHOC m ON bd.MaMon = m.MaMon
                                LEFT JOIN DM_LOPHOC l ON bd.MaLop = l.MaLop
                                LEFT JOIN DM_TRINHDO td ON bd.YeuCauTrinhDo = td.MaTrinhDo
                                LEFT JOIN DM_HINHTHUC ht ON bd.MaHinhThuc = ht.MaHinhThuc
                                LEFT JOIN DM_QUANHUYEN q ON bd.MaQuan = q.MaQuan
                                WHERE (@TrangThai = N'Tất cả' OR bd.TrangThai = @TrangThai)
                                ORDER BY 
                                    CASE 
                                        WHEN bd.TrangThai = 'DangGiaoDich' THEN 1
                                        WHEN bd.TrangThai = 'ChuaGiao' THEN 2
                                        WHEN bd.TrangThai = 'DaGiao' THEN 3
                                        ELSE 4
                                    END, bd.MaBaiDang DESC";

                using SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var card = new ucAdminBaiDang
                    {
                        MaBaiDang = reader["MaBaiDang"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MaBaiDang"]),
                        PhuHuynh = reader["HoTen"]?.ToString() ?? "",
                        MonHoc = reader["TenMon"]?.ToString() ?? "",
                        Lop = reader["TenLop"]?.ToString() ?? "",
                        TrinhDo = reader["TenTrinhDo"]?.ToString() ?? "",
                        HinhThuc = reader["TenHinhThuc"]?.ToString() ?? "",
                        KhuVuc = reader["TenQuan"]?.ToString() ?? "",
                        MucLuong = reader["MucLuong"]?.ToString() ?? "",
                        TrangThai = reader["TrangThai"]?.ToString() ?? "",
                        MaGSNhan = reader["MaGS"] != DBNull.Value ? reader["MaGS"].ToString() : "",
                        YeuCau = reader["YeuCauThem"]?.ToString() ?? ""
                    };
                    card.XoaBaiClicked += (_, _) => XoaBaiDang(card.MaBaiDang);
                    card.MinimumSize = new Size(330, 380);
                    card.Margin = new Padding(10);
                    cards.Add(card);
                }

                flpBaiDang.Controls.AddRange(cards.ToArray());
                UpdateBaiDangLayout();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Tab 1: " + ex.Message);
            }
            finally
            {
                flpBaiDang.ResumeLayout(true);
                flpBaiDang.PerformLayout();
                flpBaiDang.Refresh();
            }
        }

        private void UpdateBaiDangLayout()
        {
            if (flpBaiDang == null || flpBaiDang.Controls.Count == 0) return;

            const int fixedColumns = 5;
            int cardMargin = 10;
            var sample = flpBaiDang.Controls[0];
            cardMargin = sample.Margin.Horizontal / 2;

            int usableWidth = flpBaiDang.ClientSize.Width - flpBaiDang.Padding.Horizontal;
            if (usableWidth <= 0) return;

            int targetWidth = (usableWidth / fixedColumns) - (cardMargin * 2);
            targetWidth = Math.Max(120, targetWidth);

            flpBaiDang.SuspendLayout();
            foreach (Control control in flpBaiDang.Controls)
            {
                int targetHeight = control.MinimumSize.Height > 0 ? control.MinimumSize.Height : control.Height;
                control.MinimumSize = new Size(targetWidth, targetHeight);
                control.MaximumSize = new Size(targetWidth, 0);
                control.Width = targetWidth;
            }
            flpBaiDang.ResumeLayout();
        }

        private void LoadDataTatCaBai()
        {
            _ = LoadCardBaiDangAsync();
        }

        private void XoaBaiDang(int maBD)
        {
            if (MessageBox.Show("Xóa bài đăng này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            if (bdDal.XoaBaiDang(maBD))
                _ = LoadCardBaiDangAsync();
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

        public async Task LoadDanhSachGiaSuAsync()
        {
            try
            {
                flpGiaSu.SuspendLayout();
                flpGiaSu.Controls.Clear();

                var cards = new List<Control>();

                using SqlConnection conn = new DBConnection().GetConnection();
                await conn.OpenAsync();

                const string query = @"SELECT GS.MaGS, GS.HoTen, GS.SDT, GS.CCCD,
                                GS.AnhMinhChung, GS.AnhBangDiem, GS.AnhChungChi,
                                ISNULL('GPA: ' + GS.DiemGPA, XL.TenXepLoai) AS ThanhTich,
                                GT.TenGioiTinh, NS.Nam, NH.TenNamHoc,
                                ISNULL(CC.TenChungChi + ': ' + GS.DiemChungChi, '') AS ThongTinChungChi,
                                GS.MaNamHoc, GS.MaChungChi, GS.DiemChungChi, DMCC.TenChungChi,
                                T.TenTruong, TD.TenTrinhDo, GS.TrangThaiDuyet,
                                AVG(CAST(DG.SoSao AS FLOAT)) AS DiemTrungBinh,
                                COUNT(DG.MaDanhGia) AS SoLuotDanhGia,
                                CASE
                                    WHEN COUNT(DG.MaDanhGia) > 0 THEN
                                        CONVERT(NVARCHAR(10), CAST(ROUND(AVG(CAST(DG.SoSao AS FLOAT)), 1) AS DECIMAL(10,1)))
                                        + N' ⭐ (' + CAST(COUNT(DG.MaDanhGia) AS NVARCHAR(20)) + N' đánh giá)'
                                    ELSE N'Chưa có đánh giá'
                                END AS colRating
                                FROM GIASU GS
                                LEFT JOIN DM_GIOITINH GT ON GS.MaGioiTinh = GT.MaGioiTinh
                                LEFT JOIN DM_NAMSINH NS ON GS.MaNamSinh = NS.MaNamSinh
                                LEFT JOIN DM_NAMHOC NH ON GS.MaNamHoc = NH.MaNamHoc
                                LEFT JOIN DM_TRUONG T ON GS.MaTruong = T.MaTruong
                                LEFT JOIN DM_TRINHDO TD ON GS.MaTrinhDo = TD.MaTrinhDo
                                LEFT JOIN DM_CHUNGCHI DMCC ON GS.MaChungChi = DMCC.MaChungChi
                                LEFT JOIN DM_CHUNGCHI CC ON GS.MaChungChi = CC.MaChungChi
                                LEFT JOIN DM_XEPLOAI XL ON GS.MaXepLoai = XL.MaXepLoai
                                LEFT JOIN DANHGIA DG ON DG.MaGS = GS.MaGS
                                GROUP BY GS.MaGS, GS.HoTen, GS.SDT, GS.CCCD,
                                         GS.AnhMinhChung, GS.AnhBangDiem, GS.AnhChungChi,
                                         GS.DiemGPA, XL.TenXepLoai, GT.TenGioiTinh, NS.Nam, NH.TenNamHoc,
                                         CC.TenChungChi, GS.DiemChungChi, GS.MaNamHoc,
                                         GS.MaChungChi, DMCC.TenChungChi,
                                         T.TenTruong, TD.TenTrinhDo, GS.TrangThaiDuyet
                                ORDER BY
                                    CASE
                                        WHEN GS.TrangThaiDuyet = 'ChoDuyet' THEN 1
                                        WHEN GS.TrangThaiDuyet = 'DaDuyet' THEN 2
                                        ELSE 3
                                    END, GS.MaGS DESC";

                using SqlCommand cmd = new SqlCommand(query, conn);
                using SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var card = new ucAdminGiaSu
                    {
                        MaGS = reader["MaGS"] == DBNull.Value ? 0 : Convert.ToInt32(reader["MaGS"]),
                        HoTen = reader["HoTen"]?.ToString() ?? "",
                        SDT = reader["SDT"]?.ToString() ?? "",
                        CCCD = reader["CCCD"]?.ToString() ?? "",
                        GioiTinh = reader["TenGioiTinh"]?.ToString() ?? "",
                        NamSinh = reader["Nam"]?.ToString() ?? "",
                        ThanhTich = reader["ThanhTich"]?.ToString() ?? "",
                        NamHoc = reader["TenNamHoc"]?.ToString() ?? "",
                        Truong = reader["TenTruong"]?.ToString() ?? "",
                        TrinhDo = reader["TenTrinhDo"]?.ToString() ?? "",
                        ChungChi = reader["ThongTinChungChi"]?.ToString() ?? ""
                    };

                    card.UrlAnhThe = reader["AnhMinhChung"]?.ToString() ?? "";
                    card.UrlAnhBang = reader["AnhBangDiem"]?.ToString() ?? "";
                    card.UrlAnhChungChi = reader["AnhChungChi"]?.ToString() ?? "";

                    card.DuyetClicked += (_, _) => DuyetGiaSu(card.MaGS);
                    card.TuChoiClicked += (_, _) => TuChoiGiaSu(card.MaGS);
                    card.XoaClicked += (_, _) => XoaGiaSu(card.MaGS);
                    card.MinimumSize = new Size(330, 350);
                    card.Margin = new Padding(10);
                    cards.Add(card);
                }

                flpGiaSu.Controls.AddRange(cards.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Tab 2: " + ex.Message);
            }
            finally
            {
                flpGiaSu.ResumeLayout();
            }
        }

        private void DuyetGiaSu(int maGS)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn duyệt gia sư này?", "Xác nhận duyệt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (gsDal.CapNhatTrangThaiDuyet(maGS, "DaDuyet"))
                    _ = LoadDanhSachGiaSuAsync();
            }
        }

        private void TuChoiGiaSu(int maGS)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn từ chối gia sư này?", "Xác nhận từ chối", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (gsDal.CapNhatTrangThaiDuyet(maGS, "TuChoi"))
                    _ = LoadDanhSachGiaSuAsync();
            }
        }

        private void XoaGiaSu(int maGS)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa gia sư này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (gsDal.XoaGiaSu(maGS))
                    _ = LoadDanhSachGiaSuAsync();
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
                    _ = LoadDanhSachGiaSuAsync();
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
            if (!_isFormLoaded) return;
            _ = LoadCardBaiDangAsync();
        }
    }
}