using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using DoAnGiaSu_WinForms.Business;
using DoAnGiaSu_WinForms.DataAccess;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.GUI
{
    public partial class FormDangBai : Form
    {
        private const decimal MucLuongToiThieu = 50000m;
        private const decimal MucLuongToiDa = 2000000m;

        private string _user;
        private bool _isEmbedded = false;
        private DataTable _dsLopHocGoc;
        private bool _dangLocLop;

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public bool IsEmbedded
        {
            get => _isEmbedded;
            set
            {
                _isEmbedded = value;
                if (_isEmbedded)
                {
                    if (this.Controls.ContainsKey("panel1"))
                    {
                        this.Controls["panel1"].BackColor = Color.FromArgb(185, 255, 255, 255);
                    }
                }
            }
        }
        public event EventHandler OnDangBaiSuccess;

        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public int MaBaiDangEdit { get; set; } = 0;

        GiaSuDAL gsDal = new GiaSuDAL();
        BaiDangService baiDangService = new BaiDangService();
        PhuHuynhDAL phDal = new PhuHuynhDAL();
        TaiKhoanDAL tkDal = new TaiKhoanDAL();
        private readonly DanhMucService danhMucService = new DanhMucService();

        public FormDangBai(string username)
        {
            InitializeComponent();
            this._user = username;

            ApplySameBackgroundAsLogin();
            Resize += FormDangBai_Resize;
            Shown += FormDangBai_Shown;
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

        private void FormDangBai_Shown(object? sender, EventArgs e)
        {
            ApplyRoundedStyle();
            CenterPanel();
        }

        private void FormDangBai_Resize(object? sender, EventArgs e)
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
                else if (control is Button)
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

        private void FormDangBai_Load(object sender, EventArgs e)
        {
            try
            {
                cboMonHoc.DataSource = danhMucService.LayMonHoc();
                cboMonHoc.DisplayMember = "TenMon";
                cboMonHoc.ValueMember = "MaMon";

                _dsLopHocGoc = danhMucService.LayLopHoc();
                cboLopHoc.DataSource = _dsLopHocGoc;
                cboLopHoc.DisplayMember = "TenLop";
                cboLopHoc.ValueMember = "MaLop";

                cboHinhThuc.DataSource = danhMucService.LayHinhThuc();
                cboHinhThuc.DisplayMember = "TenHinhThuc";
                cboHinhThuc.ValueMember = "MaHinhThuc";

                LayDanhSachQuan();

                cmbYeuCauTrinhDo.DataSource = danhMucService.LayTrinhDo();
                cmbYeuCauTrinhDo.DisplayMember = "TenTrinhDo";
                cmbYeuCauTrinhDo.ValueMember = "MaTrinhDo";

                cboMonHoc.SelectedIndexChanged -= cboMonHoc_SelectedIndexChanged;
                cboMonHoc.SelectedIndexChanged += cboMonHoc_SelectedIndexChanged;
                LocDanhSachLopTheoMon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboMonHoc_SelectedIndexChanged(object? sender, EventArgs e)
        {
            LocDanhSachLopTheoMon();
        }

        private void LocDanhSachLopTheoMon()
        {
            if (_dangLocLop || _dsLopHocGoc == null || _dsLopHocGoc.Rows.Count == 0) return;

            try
            {
                _dangLocLop = true;

                string monHoc = cboMonHoc.Text?.Trim() ?? "";
                string lopDangChon = cboLopHoc.Text?.Trim() ?? "";

                HashSet<string> tapLopDuocPhep = LayTapLopDuocPhepTheoMon(monHoc);

                DataTable dtLoc = _dsLopHocGoc.Clone();
                foreach (DataRow row in _dsLopHocGoc.Rows)
                {
                    string tenLop = row.Table.Columns.Contains("TenLop") ? row["TenLop"]?.ToString() ?? "" : "";
                    if (tapLopDuocPhep == null || tapLopDuocPhep.Contains(ChuanHoaChuoi(tenLop)))
                    {
                        dtLoc.ImportRow(row);
                    }
                }

                cboLopHoc.DataSource = dtLoc;
                cboLopHoc.DisplayMember = "TenLop";
                cboLopHoc.ValueMember = "MaLop";

                if (!string.IsNullOrWhiteSpace(lopDangChon))
                {
                    string lopChuanHoa = ChuanHoaChuoi(lopDangChon);
                    for (int i = 0; i < cboLopHoc.Items.Count; i++)
                    {
                        if (cboLopHoc.Items[i] is DataRowView drv)
                        {
                            string tenLop = drv["TenLop"]?.ToString() ?? "";
                            if (ChuanHoaChuoi(tenLop) == lopChuanHoa)
                            {
                                cboLopHoc.SelectedIndex = i;
                                break;
                            }
                        }
                    }
                }

                if (cboLopHoc.SelectedIndex < 0 && cboLopHoc.Items.Count > 0)
                {
                    cboLopHoc.SelectedIndex = 0;
                }
            }
            finally
            {
                _dangLocLop = false;
            }
        }

        private static HashSet<string> TaoTapLopTuDen(int tuLop, int denLop)
        {
            HashSet<string> tap = new HashSet<string>();
            for (int i = tuLop; i <= denLop; i++)
            {
                tap.Add(ChuanHoaChuoi($"Lớp {i}"));
            }
            return tap;
        }

        private static HashSet<string> LayTapLopDuocPhepTheoMon(string tenMon)
        {
            string mon = ChuanHoaChuoi(tenMon);
            bool CoTu(string key) => mon.Contains(ChuanHoaChuoi(key));

            if (CoTu("Hóa Học"))
            {
                var tap = TaoTapLopTuDen(8, 12);
                tap.Add(ChuanHoaChuoi("Luyện thi Đại học"));
                return tap;
            }

            if (CoTu("Vật Lý") || CoTu("Sinh Học") || CoTu("Lịch Sử") || CoTu("Địa Lý") || CoTu("GDCD"))
            {
                var tap = TaoTapLopTuDen(6, 12);
                tap.Add(ChuanHoaChuoi("Luyện thi Đại học"));
                return tap;
            }

            if (CoTu("Toán Học") || CoTu("Ngữ Văn"))
            {
                var tap = TaoTapLopTuDen(1, 12);
                tap.Add(ChuanHoaChuoi("Luyện thi Đại học"));
                return tap;
            }

            if (CoTu("Toán Tư Duy"))
            {
                return TaoTapLopTuDen(1, 5);
            }

            if (CoTu("Rèn Chữ"))
            {
                var tap = TaoTapLopTuDen(1, 5);
                tap.Add(ChuanHoaChuoi("Luyện chữ đẹp"));
                return tap;
            }

            if (CoTu("Tiếng Anh"))
            {
                var tap = TaoTapLopTuDen(1, 12);
                tap.Add(ChuanHoaChuoi("Luyện thi Đại học"));
                tap.Add(ChuanHoaChuoi("Luyện thi IELTS"));
                tap.Add(ChuanHoaChuoi("Luyện thi TOEIC"));
                tap.Add(ChuanHoaChuoi("Tiếng Anh Giao Tiếp"));
                tap.Add(ChuanHoaChuoi("Sinh viên Đại học"));
                tap.Add(ChuanHoaChuoi("Người đi làm"));
                return tap;
            }

            if (CoTu("Tiếng Nhật") || CoTu("Tiếng Hàn") || CoTu("Tiếng Trung"))
            {
                var tap = TaoTapLopTuDen(6, 12);
                tap.Add(ChuanHoaChuoi("Sinh viên Đại học"));
                tap.Add(ChuanHoaChuoi("Người đi làm"));
                return tap;
            }

            if (CoTu("Lập trình C#") || CoTu("Lập trình C") || CoTu("Lập trình Web"))
            {
                var tap = TaoTapLopTuDen(10, 12);
                tap.Add(ChuanHoaChuoi("Sinh viên Đại học"));
                tap.Add(ChuanHoaChuoi("Người đi làm"));
                return tap;
            }

            if (CoTu("Piano") || CoTu("Guitar") || CoTu("Mỹ Thuật"))
            {
                var tap = TaoTapLopTuDen(1, 12);
                tap.Add(ChuanHoaChuoi("Lớp năng khiếu (Đàn, Hát)"));
                tap.Add(ChuanHoaChuoi("Người đi làm"));
                return tap;
            }

            if (CoTu("Tin Học"))
            {
                var tap = TaoTapLopTuDen(3, 12);
                tap.Add(ChuanHoaChuoi("Sinh viên Đại học"));
                tap.Add(ChuanHoaChuoi("Người đi làm"));
                return tap;
            }

            return null;
        }

        private static string ChuanHoaChuoi(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "";
            string normalized = input.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            foreach (char c in normalized)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(c);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        public void LoadDataForEdit(int maBD, string tenMon, string tenLop, string hinhThuc, string luong, string diaChi, string ghiChu)
        {
            MaBaiDangEdit = maBD;
            cboMonHoc.Text = tenMon;
            cboLopHoc.Text = tenLop;
            cboHinhThuc.Text = hinhThuc;
            txtHocPhi.Text = luong;
            txtDiaChiDay.Text = diaChi;
            txtGhiChu.Text = ghiChu;

            btnXacNhanDang.Text = "Cập Nhật Bài Đăng";
            if (this.Controls.ContainsKey("panel1") && this.Controls["panel1"].Controls.ContainsKey("labelTitle"))
            {
                this.Controls["panel1"].Controls["labelTitle"].Text = "CẬP NHẬT BÀI ĐĂNG";
            }
        }

        public void ResetForm()
        {
            MaBaiDangEdit = 0;
            txtHocPhi.Text = "";
            txtDiaChiDay.Text = "";
            txtGhiChu.Text = "";
            btnXacNhanDang.Text = "Đăng Bài Tìm Gia Sư";
            if (this.Controls.ContainsKey("panel1") && this.Controls["panel1"].Controls.ContainsKey("labelTitle"))
            {
                this.Controls["panel1"].Controls["labelTitle"].Text = "ĐĂNG BÀI TÌM GIA SƯ";
            }
        }

        private void btnXacNhanDang_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtHocPhi.Text) || string.IsNullOrEmpty(txtDiaChiDay.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ!");
                    return;
                }

                if (cmbQuan.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn Quận/Huyện!");
                    return;
                }

                string hocPhiText = (txtHocPhi.Text ?? string.Empty).Trim();
                if (!decimal.TryParse(hocPhiText, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal hocPhi))
                {
                    MessageBox.Show("Mức lương không hợp lệ. Vui lòng nhập số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (hocPhi < MucLuongToiThieu || hocPhi > MucLuongToiDa)
                {
                    MessageBox.Show($"Mức lương/buổi phải từ {MucLuongToiThieu:N0} đến {MucLuongToiDa:N0} VNĐ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int maTK = tkDal.LayMaTKTuTen(_user);
                int maPH = phDal.LayMaPH(maTK);

                BaiDang bd = new BaiDang
                {
                    MaBaiDang = MaBaiDangEdit,
                    MaMon = Convert.ToInt32(cboMonHoc.SelectedValue),
                    MaLop = Convert.ToInt32(cboLopHoc.SelectedValue),
                    MaHinhThuc = Convert.ToInt32(cboHinhThuc.SelectedValue),
                    MucLuong = hocPhi,
                    SoNhaDuong = txtDiaChiDay.Text.Trim(),
                    YeuCauThem = txtGhiChu.Text.Trim(),
                    MaPH = maPH,
                    MaQuan = Convert.ToInt32(cmbQuan.SelectedValue),
                    YeuCauTrinhDo = Convert.ToInt32(cmbYeuCauTrinhDo.SelectedValue)
                };

                bool success = false;
                if (MaBaiDangEdit > 0)
                {
                    success = baiDangService.SuaBaiDang(bd);
                }
                else
                {
                    success = baiDangService.ThemBaiDang(bd);
                }

                if (success)
                {
                    MessageBox.Show("Thành công!");
                    if (IsEmbedded)
                    {
                        ResetForm();
                        OnDangBaiSuccess?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LayDanhSachQuan()
        {
            try
            {
                var dt = danhMucService.LayQuanHuyen();
                cmbQuan.DataSource = dt;
                cmbQuan.DisplayMember = "TenQuan";
                cmbQuan.ValueMember = "MaQuan";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh sách quận: " + ex.Message);
            }
        }
    }
}