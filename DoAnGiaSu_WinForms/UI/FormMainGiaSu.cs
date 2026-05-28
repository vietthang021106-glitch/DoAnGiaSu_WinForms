using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DoAnGiaSu_WinForms.Business;
using DoAnGiaSu_WinForms.DataAccess;

namespace DoAnGiaSu_WinForms.GUI
{
    public partial class FormMainGiaSu : Form
    {
        private string _tenDangNhap;
        private readonly BaiDangService baiDangService = new BaiDangService();
        private readonly GiaSuDAL _gsDal = new GiaSuDAL();
        private readonly DanhMucService danhMucService = new DanhMucService();
        private DataTable _dtLopMoi;
        private bool _dangNapFilter;

        public FormMainGiaSu(string username)
        {
            InitializeComponent();
            _tenDangNhap = username;

            ApplySameBackgroundAsLogin();
            Resize += FormMainGiaSu_Resize;
            Shown += FormMainGiaSu_Shown;
            FormClosed += FormMainGiaSu_FormClosed;
            AttachSizeChangedHandlers(this);
            ApplyRoundedStyle();
        }

        private void CenterPanel()
        {
            if (panel1 == null) return;
            panel1.Left = (ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (ClientSize.Height - panel1.Height) / 2;
        }

        private void FormMainGiaSu_Resize(object sender, EventArgs e)
        {
            ApplyRoundedStyle();
            CenterPanel();
        }

        private void FormMainGiaSu_Shown(object sender, EventArgs e)
        {
            ApplyRoundedStyle();
            CenterPanel();
        }

        private void FormMainGiaSu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.OpenForms["FormDangNhap"]?.Show();
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            Close();
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

            using var path = new System.Drawing.Drawing2D.GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, diameter, diameter, 180, 90);
            path.AddArc(control.Width - diameter, 0, diameter, diameter, 270, 90);
            path.AddArc(control.Width - diameter, control.Height - diameter, diameter, diameter, 0, 90);
            path.AddArc(0, control.Height - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            control.Region?.Dispose();
            control.Region = new System.Drawing.Region(path);
        }

        private void FormMainGiaSu_Load(object sender, EventArgs e)
        {
            Text = "Trang chủ Gia sư - Xin chào: " + _tenDangNhap;

            cbMonHoc.SelectedIndexChanged += Filter_SelectedIndexChanged;
            cbLop.SelectedIndexChanged += Filter_SelectedIndexChanged;
            cbKhuVuc.SelectedIndexChanged += Filter_SelectedIndexChanged;
            cbHinhThuc.SelectedIndexChanged += Filter_SelectedIndexChanged;
            cmbSapXepGia.SelectedIndexChanged += cmbSapXepGia_SelectedIndexChanged;
            btnReset.Click += btnReset_Click;

            LoadFilterData();
            LoadLopMoi();

            if (btnNavLopMoi != null) btnNavLopMoi.BackColor = System.Drawing.Color.FromArgb(24, 119, 242);
        }

        private void btnNavLopMoi_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            btnNavLopMoi.BackColor = System.Drawing.Color.FromArgb(24, 119, 242);
            btnNavLopDaNhan.BackColor = System.Drawing.Color.Transparent;
            LoadLopMoi();
        }

        private void btnNavLopDaNhan_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            btnNavLopDaNhan.BackColor = System.Drawing.Color.FromArgb(24, 119, 242);
            btnNavLopMoi.BackColor = System.Drawing.Color.Transparent;
            LoadLopDaNhan();
        }

        private void ApplySameBackgroundAsLogin()
        {
            var frmLogin = new FormDangNhap();
            BackgroundImage = frmLogin.BackgroundImage;
            BackgroundImageLayout = frmLogin.BackgroundImageLayout;
        }

        private void LoadLopMoi()
        {
            try
            {
                int maGS = _gsDal.LayMaGS(_tenDangNhap);
                _dtLopMoi = baiDangService.LayLopMoiChoGiaSu(maGS, GetOrderByClause());
                ApplyFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lớp mới: " + ex.Message);
            }
        }

        private void LoadFilterData()
        {
            _dangNapFilter = true;
            try
            {
                LoadDanhMucToCombo(cbMonHoc, danhMucService.LayMonHoc(), "TenMon", "MaMon");
                LoadDanhMucToCombo(cbLop, danhMucService.LayLopHoc(), "TenLop", "MaLop");
                LoadDanhMucToCombo(cbKhuVuc, danhMucService.LayQuanHuyen(), "TenQuan", "MaQuan");
                LoadDanhMucToCombo(cbHinhThuc, danhMucService.LayHinhThuc(), "TenHinhThuc", "MaHinhThuc");

                cmbSapXepGia.Items.Clear();
                cmbSapXepGia.Items.Add("--- Sắp xếp mặc định ---");
                cmbSapXepGia.Items.Add("Giá: Thấp đến Cao");
                cmbSapXepGia.Items.Add("Giá: Cao đến Thấp");
                cmbSapXepGia.SelectedIndex = 0;
            }
            finally
            {
                _dangNapFilter = false;
            }
        }

        private string GetOrderByClause()
        {
            return cmbSapXepGia.SelectedIndex switch
            {
                1 => " ORDER BY MucLuong ASC",
                2 => " ORDER BY MucLuong DESC",
                _ => " ORDER BY MaBaiDang DESC"
            };
        }

        private void LoadDanhMucToCombo(ComboBox combo, DataTable src, string displayColumn, string valueColumn)
        {
            if (src == null || !src.Columns.Contains(displayColumn) || !src.Columns.Contains(valueColumn))
            {
                combo.DataSource = null;
                combo.Items.Clear();
                combo.Items.Add("--- Tất cả ---");
                combo.SelectedIndex = 0;
                return;
            }

            DataTable dt = src.Clone();
            DataRow allRow = dt.NewRow();
            allRow[valueColumn] = DBNull.Value;
            allRow[displayColumn] = "--- Tất cả ---";
            dt.Rows.Add(allRow);

            foreach (DataRow row in src.Rows)
                dt.ImportRow(row);

            combo.DataSource = dt;
            combo.DisplayMember = displayColumn;
            combo.ValueMember = valueColumn;
            combo.SelectedIndex = 0;
        }

        private static string EscapeFilterValue(string value)
        {
            return (value ?? string.Empty).Replace("'", "''");
        }

        private void ApplyFilter()
        {
            if (_dangNapFilter || _dtLopMoi == null) return;

            DataView view = _dtLopMoi.DefaultView;
            string filter = "";

            void AddCondition(string condition)
            {
                if (string.IsNullOrWhiteSpace(condition)) return;
                filter += string.IsNullOrWhiteSpace(filter) ? condition : " AND " + condition;
            }

            string mon = cbMonHoc.Text?.Trim() ?? "";
            string lop = cbLop.Text?.Trim() ?? "";
            string khuVuc = cbKhuVuc.Text?.Trim() ?? "";
            string hinhThuc = cbHinhThuc.Text?.Trim() ?? "";

            if (!string.Equals(mon, "--- Tất cả ---", StringComparison.OrdinalIgnoreCase) && _dtLopMoi.Columns.Contains("TenMon"))
                AddCondition($"TenMon LIKE '%{EscapeFilterValue(mon)}%'");

            if (!string.Equals(lop, "--- Tất cả ---", StringComparison.OrdinalIgnoreCase) && _dtLopMoi.Columns.Contains("TenLop"))
                AddCondition($"TenLop LIKE '%{EscapeFilterValue(lop)}%'");

            if (!string.Equals(khuVuc, "--- Tất cả ---", StringComparison.OrdinalIgnoreCase) && _dtLopMoi.Columns.Contains("TenQuan"))
                AddCondition($"TenQuan LIKE '%{EscapeFilterValue(khuVuc)}%'");

            if (!string.Equals(hinhThuc, "--- Tất cả ---", StringComparison.OrdinalIgnoreCase) && _dtLopMoi.Columns.Contains("TenHinhThuc"))
                AddCondition($"TenHinhThuc LIKE '%{EscapeFilterValue(hinhThuc)}%'");

            view.RowFilter = filter;

            flpTimLop.Controls.Clear();
            foreach (DataRowView r in view)
            {
                int maLop = Convert.ToInt32(r["MaBaiDang"]);
                string monHoc = r["TenMon"]?.ToString() ?? "";
                string lh = r["TenLop"]?.ToString() ?? "";
                string ht = r["TenHinhThuc"]?.ToString() ?? "";
                string kv = r["TenQuan"]?.ToString() ?? "";
                string yc = r["YeuCauThem"]?.ToString() ?? "";
                string ml = r["MucLuong"]?.ToString() ?? "";

                var card = new ucCardTimLop();
                card.LoadData(maLop, monHoc, lh, ht, kv, yc, ml);
                card.DangKyClicked += Card_DangKyClicked;
                flpTimLop.Controls.Add(card);
            }
        }

        private void Card_DangKyClicked(object sender, int maLop)
        {
            var dialogResult = MessageBox.Show("Bạn có chắc chắn muốn đăng ký nhận lớp học này?", "Xác nhận đăng ký", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                int maGS = _gsDal.LayMaGS(_tenDangNhap);
                try
                {
                    if (baiDangService.GiaSuNhanLop(maLop, maGS))
                    {
                        MessageBox.Show("Đăng ký nhận lớp thành công! Vui lòng chờ Phụ huynh duyệt trước khi nộp phí.", "Thông báo");
                        LoadLopMoi();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi chi tiết: {ex.Message}", "Không thể đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Filter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cmbSapXepGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_dangNapFilter) return;
            LoadLopMoi();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _dangNapFilter = true;
            try
            {
                if (cbMonHoc.Items.Count > 0) cbMonHoc.SelectedIndex = 0;
                if (cbLop.Items.Count > 0) cbLop.SelectedIndex = 0;
                if (cbKhuVuc.Items.Count > 0) cbKhuVuc.SelectedIndex = 0;
                if (cbHinhThuc.Items.Count > 0) cbHinhThuc.SelectedIndex = 0;
                if (cmbSapXepGia.Items.Count > 0) cmbSapXepGia.SelectedIndex = 0;
            }
            finally
            {
                _dangNapFilter = false;
            }

            if (_dtLopMoi != null)
            {
                _dtLopMoi.DefaultView.RowFilter = string.Empty;
                LoadLopMoi();
            }
        }

        private void LoadLopDaNhan()
        {
            try
            {
                int maGS = _gsDal.LayMaGS(_tenDangNhap);
                DataTable dt = baiDangService.LayLopDaGiaoChoGiaSu(maGS);

                flpLopDaNhan.Controls.Clear();
                foreach (DataRow r in dt.Rows)
                {
                    int maLop = Convert.ToInt32(r["MaBaiDang"]);
                    string trangThai = r["TrangThaiHienThi"]?.ToString() ?? "";
                    string monHoc = r["TenMon"]?.ToString() ?? "";
                    string lh = r["TenLop"]?.ToString() ?? "";
                    string tenPH = r["TenPH"]?.ToString() ?? "";
                    string sdt = r["SDT"]?.ToString() ?? "";
                    string ht = r["TenHinhThuc"]?.ToString() ?? "";
                    string dc = r["SoNhaDuong"]?.ToString() ?? "";
                    string kv = r["TenQuan"]?.ToString() ?? "";
                    string yc = r["YeuCauThem"]?.ToString() ?? "";
                    string ml = r["MucLuong"]?.ToString() ?? "";
                    string trangThaiDongPhi = r.Table.Columns.Contains("TrangThaiDongPhi") ? r["TrangThaiDongPhi"]?.ToString() ?? "" : "";
                    string trangThaiDangKy = r.Table.Columns.Contains("TrangThaiDangKy") ? r["TrangThaiDangKy"]?.ToString() ?? "" : "";

                    var card = new ucCardLopDaNhan();
                    card.LoadData(maLop, trangThai, monHoc, lh, tenPH, sdt, ht, dc, kv, yc, ml, trangThaiDongPhi, maGS, trangThaiDangKy);
                    card.Cursor = Cursors.Hand;
                    card.Click += (s, e) => CardDaNhan_Clicked(card);
                    foreach (Control c in card.Controls)
                    {
                        c.Cursor = Cursors.Hand;
                        c.Click += (s, e) => CardDaNhan_Clicked(card);
                    }
                    flpLopDaNhan.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải lớp đã nhận: " + ex.Message);
            }
        }

        private void CardDaNhan_Clicked(ucCardLopDaNhan card)
        {
            string tt = card.TrangThaiDongPhiStr ?? string.Empty;
            string ttDangKy = card.TrangThaiDangKyStr ?? string.Empty;
            int maBD = card.MaLop;

            if (tt == "DaDong")
            {
                MessageBox.Show("Bạn đã thanh toán phí cho lớp này rồi! Vui lòng liên hệ Phụ huynh theo SĐT trên thẻ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ttDangKy == "ChoDuyet" || ttDangKy == "")
            {
                MessageBox.Show("Lớp này đang chờ phụ huynh duyệt, vui lòng chờ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ttDangKy == "DaDuyet" || ttDangKy == "Đã duyệt")
            {
                if (tt == "ChuaDong" || string.IsNullOrEmpty(tt))
                {
                    if (MessageBox.Show("Phụ huynh đã duyệt! Đóng phí nhận thông tin?", "Thanh toán", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        FormThanhToan frm = new FormThanhToan(card.MucHocPhi, maBD, _tenDangNhap);
                        frm.ShowDialog();
                        LoadLopDaNhan();
                    }
                    return;
                }

                if (tt == "Thanh Toán" || tt == "Đang giao dịch" || tt == "DangGiaoDich" || tt.Contains("Thanh"))
                {
                    MessageBox.Show("Bạn đã thanh toán phí cho lớp này rồi! Vui lòng liên hệ Phụ huynh theo SĐT trên thẻ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
                LoadLopDaNhan();
            else
                LoadLopMoi();
        }
    }
}
