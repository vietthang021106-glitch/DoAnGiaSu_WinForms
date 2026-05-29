using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using DoAnGiaSu_WinForms.Business;
using DoAnGiaSu_WinForms.DataAccess;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.GUI
{
    public partial class FormMainPhuHuynh : Form
    {
        private readonly string _user;
        private FormDangBai frmDangBai;

        private readonly BaiDangService baiDangService = new BaiDangService();
        private readonly PhuHuynhService phService = new PhuHuynhService();
        private readonly TaiKhoanDAL tkDal = new TaiKhoanDAL();
        private readonly DanhMucService danhMucService = new DanhMucService();

        public FormMainPhuHuynh(string username)
        {
            InitializeComponent();
            _user = username;

            ApplySameBackgroundAsLogin();
            Resize += FormMainPhuHuynh_Resize;
            Shown += FormMainPhuHuynh_Shown;
            FormClosed += FormMainPhuHuynh_FormClosed;
            AttachSizeChangedHandlers(this);

            ApplyRoundedStyle();
        }

        private void CenterPanel()
        {
            if (panel1 == null) return;
            panel1.Left = (ClientSize.Width - panel1.Width) / 2;
            panel1.Top = (ClientSize.Height - panel1.Height) / 2;
        }

        private void ApplySameBackgroundAsLogin()
        {
            var frmLogin = new FormDangNhap();
            BackgroundImage = frmLogin.BackgroundImage;
            BackgroundImageLayout = frmLogin.BackgroundImageLayout;
        }

        private void FormMainPhuHuynh_Resize(object? sender, EventArgs e)
        {
            ApplyRoundedStyle();
            CenterPanel();
        }

        private void FormMainPhuHuynh_Shown(object? sender, EventArgs e)
        {
            ApplyRoundedStyle();
            CenterPanel();
        }

        private void FormMainPhuHuynh_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Application.OpenForms["FormDangNhap"]?.Show();
        }

        private void ApplyRoundedStyle() => ApplyRoundedToControlTree(this);

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

        private void FormMainPhuHuynh_Load(object sender, EventArgs e)
        {
            Text = "Trang chủ Phụ huynh - Tài khoản: " + _user;

            frmDangBai = new FormDangBai(_user)
            {
                TopLevel = false,
                FormBorderStyle = FormBorderStyle.None,
                Dock = DockStyle.Fill,
                IsEmbedded = true
            };
            frmDangBai.OnDangBaiSuccess += FrmDangBai_OnDangBaiSuccess;

            tabPage2.Controls.Add(frmDangBai);
            frmDangBai.Show();

            LoadDataBaiDang();

            btnDanhGia.Enabled = false;
            btnDanhGia.Visible = false;

            btnDuyetGiaSu.Visible = false;

            if (btnQuanLyBai != null)
                btnQuanLyBai.BackColor = Color.FromArgb(24, 119, 242);
        }

        private void FrmDangBai_OnDangBaiSuccess(object sender, EventArgs e)
        {
            btnQuanLyBai_Click(this, EventArgs.Empty);
        }

        private ucCardBaiDangPH _selectedCard;

        private void LoadDataBaiDang()
        {
            try
            {
                int maTK = tkDal.LayMaTKTuTen(_user);
                int maPH = phService.LayMaPH(maTK);
                if (maPH <= 0) return;

                flpBaiDangCuaToi.Controls.Clear();
                _selectedCard = null;

                DataTable dt = baiDangService.LayBaiDangCuaPhuHuynhChiTiet(maPH);
                foreach (DataRow row in dt.Rows)
                {
                    int maBaiDang = row["MaBaiDang"] == DBNull.Value ? 0 : Convert.ToInt32(row["MaBaiDang"]);
                    string tenMon = row["TenMon"]?.ToString() ?? "";
                    string tenLop = row["TenLop"]?.ToString() ?? "";
                    string tenTrinhDo = row["TenTrinhDo"]?.ToString() ?? "";
                    string tenHinhThuc = row["TenHinhThuc"]?.ToString() ?? "";
                    string mucLuong = row["MucLuong"]?.ToString() ?? "";
                    string soNhaDuong = row["SoNhaDuong"]?.ToString() ?? "";
                    string yeuCauThem = row["YeuCauThem"]?.ToString() ?? "";
                    string trangThai = row["TrangThai"]?.ToString() ?? "";
                    string tenQuan = row["TenQuan"]?.ToString() ?? "Chưa xác định";

                    if (decimal.TryParse(mucLuong, out decimal luongDec))
                        mucLuong = Math.Round(luongDec, 0).ToString();

                    ucCardBaiDangPH card = new ucCardBaiDangPH();
                    card.LoadData(maBaiDang, tenMon, tenLop, tenTrinhDo, tenHinhThuc, mucLuong, soNhaDuong, yeuCauThem, trangThai, tenQuan);

                    card.XemDuyetClicked += Card_XemDuyetClicked;
                    card.SuaClicked += Card_SuaClicked;
                    card.XoaClicked += Card_XoaClicked;

                    flpBaiDangCuaToi.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải bài đăng: " + ex.Message);
            }
        }

        private void Card_XemDuyetClicked(object sender, EventArgs e)
        {
            if (sender is not ucCardBaiDangPH card) return;

            _selectedCard = card;
            int maBD = (int)card.Tag;
            string trangThai = card.TrangThaiStr ?? "";

            if (trangThai != "ChoPhuHuynhDuyet" && trangThai != "DangGiaoDich" && trangThai != "DaGiao")
            {
                MessageBox.Show("Bài đăng này chưa có Gia sư nào đăng ký hoặc nhận lớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                List<DangKyNhanLop> listDangKy = baiDangService.LayDangKyNhanLopTheoBaiList(maBD);

                if ((listDangKy == null || listDangKy.Count == 0) && (trangThai == "DangGiaoDich" || trangThai == "DaGiao"))
                {
                    DataTable dtFallback = baiDangService.LayThongTinGiaSuTuBaiDangKhiThieuDangKy(maBD);
                    if (dtFallback == null || dtFallback.Rows.Count == 0)
                    {
                        MessageBox.Show("Bài đăng này chưa có gia sư đăng ký.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    DataRow rFallback = dtFallback.Rows[0];
                    string hoTenF = rFallback["HoTen"]?.ToString() ?? ""
;
                    string sdtF = rFallback["SDT"]?.ToString() ?? "";
                    string trinhDoF = rFallback["TenTrinhDo"]?.ToString() ?? "";
                    string truongF = rFallback["TenTruong"]?.ToString() ?? "";
                    string gioiTinhF = rFallback.Table.Columns.Contains("TenGioiTinh") ? rFallback["TenGioiTinh"]?.ToString() ?? "" : "";
                    string namSinhF = rFallback.Table.Columns.Contains("NamSinh") ? rFallback["NamSinh"]?.ToString() ?? "" : "";
                    string thanhTichF = rFallback.Table.Columns.Contains("ThanhTich") ? rFallback["ThanhTich"]?.ToString() ?? "" : "";
                    string namHocF = rFallback.Table.Columns.Contains("TenNamHoc") ? rFallback["TenNamHoc"]?.ToString() ?? "" : (rFallback.Table.Columns.Contains("MaNamHoc") ? rFallback["MaNamHoc"]?.ToString() ?? "" : "");
                    string maChungChiF = rFallback.Table.Columns.Contains("TenChungChi") ? rFallback["TenChungChi"]?.ToString() ?? "" : (rFallback.Table.Columns.Contains("MaChungChi") ? rFallback["MaChungChi"]?.ToString() ?? "" : "");
                    string diemChungChiF = rFallback.Table.Columns.Contains("DiemChungChi") ? ChuanHoaHienThiDiemChungChi(rFallback["DiemChungChi"]?.ToString() ?? "") : "";
                    string anhMinhChungPathF = rFallback.Table.Columns.Contains("AnhMinhChung") ? rFallback["AnhMinhChung"]?.ToString() ?? "" : "";
                    string trangThaiDongPhiF = rFallback.Table.Columns.Contains("TrangThaiDongPhi") ? rFallback["TrangThaiDongPhi"]?.ToString() ?? "" : "";
                    int maGSf = rFallback.Table.Columns.Contains("MaGS") && rFallback["MaGS"] != DBNull.Value ? Convert.ToInt32(rFallback["MaGS"]) : 0;

                    double diemTBF = 0;
                    int luotDanhGiaF = 0;
                    try
                    {
                        DataTable dtDanhGia = phService.LayThongKeDanhGiaGiaSu(maGSf);
                        if (dtDanhGia.Rows.Count > 0)
                        {
                            DataRow rowDanhGia = dtDanhGia.Rows[0];
                            if (rowDanhGia["DiemTB"] != DBNull.Value) diemTBF = Convert.ToDouble(rowDanhGia["DiemTB"]);
                            if (rowDanhGia["LuotDanhGia"] != DBNull.Value) luotDanhGiaF = Convert.ToInt32(rowDanhGia["LuotDanhGia"]);
                        }
                    }
                    catch
                    {
                        diemTBF = 0;
                        luotDanhGiaF = 0;
                    }

                    using Form frmProfile = new Form();
                    frmProfile.Text = "Thông tin gia sư đã duyệt";
                    frmProfile.Size = new Size(500, 300);
                    frmProfile.StartPosition = FormStartPosition.CenterParent;
                    frmProfile.FormBorderStyle = FormBorderStyle.FixedDialog;
                    frmProfile.MaximizeBox = false;
                    frmProfile.MinimizeBox = false;
                    frmProfile.BackColor = Color.White;
                    frmProfile.Padding = new Padding(0);

                    Panel pnlRoot = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BackColor = Color.White,
                        Padding = new Padding(12)
                    };

                    Panel pnlBottom = new Panel
                    {
                        Dock = DockStyle.Bottom,
                        Height = 50,
                        BackColor = Color.White,
                        Padding = new Padding(0, 8, 0, 0)
                    };

                    Panel pnlImage = new Panel
                    {
                        Dock = DockStyle.Right,
                        Width = 160,
                        BackColor = Color.White,
                        Padding = new Padding(0)
                    };

                    PictureBox picMinhChung = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BackColor = Color.WhiteSmoke,
                        Margin = new Padding(0)
                    };
                    TaiAnhGiaSu(picMinhChung, anhMinhChungPathF);
                    pnlImage.Controls.Add(picMinhChung);

                    Panel pnlInfo = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BackColor = Color.White,
                        Padding = new Padding(0, 0, 12, 0)
                    };

                    FlowLayoutPanel flpInfo = new FlowLayoutPanel
                    {
                        Dock = DockStyle.Fill,
                        FlowDirection = FlowDirection.TopDown,
                        AutoScroll = true,
                        AutoSize = false,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        WrapContents = false,
                        BackColor = Color.White,
                        Margin = new Padding(0),
                        Padding = new Padding(0)
                    };

                    Label lblHoTen = new Label
                    {
                        AutoSize = true,
                        MaximumSize = new Size(300, 0),
                        Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(24, 33, 53),
                        Margin = new Padding(0, 0, 0, 6),
                        Text = hoTenF
                    };

                    Label lblSDT = new Label
                    {
                        AutoSize = true,
                        MaximumSize = new Size(300, 0),
                        Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                        Margin = new Padding(0, 0, 0, 2)
                    };

                    if (string.Equals(trangThaiDongPhiF, "DaDong", StringComparison.OrdinalIgnoreCase))
                    {
                        lblSDT.Text = "SĐT Liên Hệ: " + sdtF;
                        lblSDT.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblSDT.Text = "SĐT Liên Hệ: (Đang chờ gia sư hoàn tất thủ tục đóng phí)";
                        lblSDT.ForeColor = Color.Gray;
                    }

                    LinkLabel lnkXemDanhGia = new LinkLabel
                    {
                        AutoSize = true,
                        Text = luotDanhGiaF > 0 ? "(Xem đánh giá chi tiết)" : "",
                        LinkColor = Color.FromArgb(24, 119, 242),
                        ActiveLinkColor = Color.DarkOrange,
                        VisitedLinkColor = Color.FromArgb(24, 119, 242),
                        Margin = new Padding(0, 0, 0, 8),
                        Visible = luotDanhGiaF > 0
                    };
                    lnkXemDanhGia.LinkClicked += (s, ev) =>
                    {
                        if (maGSf > 0)
                        {
                            using Form frmChiTiet = new FormChiTietDanhGia(maGSf);
                            frmChiTiet.ShowDialog(frmProfile);
                        }
                    };

                    Label lblThongTinCaNhan = new Label
                    {
                        AutoSize = true,
                        MaximumSize = new Size(300, 0),
                        Font = new Font("Segoe UI", 10F),
                        ForeColor = Color.Black,
                        Margin = new Padding(0, 2, 0, 2),
                        Text = $"Giới tính: {gioiTinhF} - Sinh năm: {namSinhF}"
                    };

                    Label lblHocVan = new Label
                    {
                        AutoSize = true,
                        MaximumSize = new Size(300, 0),
                        Font = new Font("Segoe UI", 10F),
                        ForeColor = Color.Black,
                        Margin = new Padding(0, 2, 0, 2),
                        Text = $"{trinhDoF} - Trường: {truongF}"
                    };

                    Label lblChungChi = new Label
                    {
                        AutoSize = true,
                        MaximumSize = new Size(300, 0),
                        Font = new Font("Segoe UI", 10F),
                        ForeColor = Color.DarkGreen,
                        Margin = new Padding(0, 2, 0, 2),
                        Text = $"Chứng chỉ: {(string.IsNullOrWhiteSpace(maChungChiF) ? "-" : maChungChiF)} - Điểm: {(string.IsNullOrWhiteSpace(diemChungChiF) ? "-" : diemChungChiF)}"
                    };
                    lblChungChi.Visible = !(string.IsNullOrWhiteSpace(maChungChiF) && string.IsNullOrWhiteSpace(diemChungChiF));

                    Label lblThanhTich = new Label
                    {
                        AutoSize = true,
                        MaximumSize = new Size(300, 0),
                        Font = new Font("Segoe UI", 10F),
                        ForeColor = Color.Black,
                        Margin = new Padding(0, 2, 0, 2),
                        Text = string.IsNullOrWhiteSpace(thanhTichF) ? "-" : thanhTichF
                    };

                    flpInfo.Controls.Add(lblHoTen);
                    flpInfo.Controls.Add(lblSDT);
                    flpInfo.Controls.Add(lnkXemDanhGia);
                    flpInfo.Controls.Add(lblThongTinCaNhan);
                    flpInfo.Controls.Add(lblHocVan);
                    flpInfo.Controls.Add(lblChungChi);
                    flpInfo.Controls.Add(lblThanhTich);

                    pnlInfo.Controls.Add(flpInfo);

                    frmProfile.Controls.Add(pnlRoot);
                    pnlRoot.Controls.Add(pnlInfo);
                    pnlRoot.Controls.Add(pnlImage);
                    pnlRoot.Controls.Add(pnlBottom);

                    frmProfile.ShowDialog();
                    return;
                }

                if (trangThai == "ChoPhuHuynhDuyet")
                {
                    using FormDanhSachGiaSuUngVien frm = new FormDanhSachGiaSuUngVien(maBD);
                    DialogResult dr = frm.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        MessageBox.Show("Gia sư đã được chọn. Vui lòng chờ gia sư nộp phí.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataBaiDang();
                    }
                    return;
                }

                DangKyNhanLop dk = listDangKy.FirstOrDefault(x => string.Equals(x.TrangThai, "DaDuyet", StringComparison.OrdinalIgnoreCase)) ?? listDangKy.FirstOrDefault();
                if (dk == null)
                {
                    MessageBox.Show("Bài đăng này chưa có gia sư đăng ký.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                int maGS = dk.MaGS;

                DataTable dtDetails = baiDangService.LayThongTinGiaSuDangKy(maBD);
                DataRow rDaDuyet = null;
                if (dtDetails != null && dtDetails.Rows.Count > 0)
                {
                    foreach (DataRow row in dtDetails.Rows)
                    {
                        if (row.Table.Columns.Contains("MaGS") && row["MaGS"] != DBNull.Value && Convert.ToInt32(row["MaGS"]) == maGS)
                        {
                            rDaDuyet = row;
                            break;
                        }
                    }
                }

                DataRow r = rDaDuyet ?? (dtDetails != null && dtDetails.Rows.Count > 0 ? dtDetails.Rows[0] : null);
                string hoTen = r != null ? r["HoTen"]?.ToString() ?? "" : "";
                string sdt = r != null ? r["SDT"]?.ToString() ?? "" : "";
                string trinhDo = r != null ? r["TenTrinhDo"]?.ToString() ?? "" : "";
                string truong = r != null ? r["TenTruong"]?.ToString() ?? "" : "";
                string gioiTinh = r != null && r.Table.Columns.Contains("TenGioiTinh") ? r["TenGioiTinh"]?.ToString() ?? "" : "";
                string namSinh = r != null && r.Table.Columns.Contains("NamSinh") ? r["NamSinh"]?.ToString() ?? "" : "";
                string thanhTich = r != null && r.Table.Columns.Contains("ThanhTich") ? r["ThanhTich"]?.ToString() ?? "" : "";
                string namHoc = r != null && r.Table.Columns.Contains("TenNamHoc") ? r["TenNamHoc"]?.ToString() ?? "" : (r != null && r.Table.Columns.Contains("MaNamHoc") ? r["MaNamHoc"]?.ToString() ?? "" : "");
                string maChungChi = r != null && r.Table.Columns.Contains("TenChungChi") ? r["TenChungChi"]?.ToString() ?? "" : (r != null && r.Table.Columns.Contains("MaChungChi") ? r["MaChungChi"]?.ToString() ?? "" : "");
                string diemChungChi = r != null && r.Table.Columns.Contains("DiemChungChi") ? ChuanHoaHienThiDiemChungChi(r["DiemChungChi"]?.ToString() ?? "") : "";
                string anhMinhChungPath = r != null && r.Table.Columns.Contains("AnhMinhChung") ? r["AnhMinhChung"]?.ToString() ?? "" : "";
                string trangThaiDongPhi = r != null && r.Table.Columns.Contains("TrangThaiDongPhi") ? r["TrangThaiDongPhi"]?.ToString() ?? "" : "";
                int maGSFromRow = r != null && r.Table.Columns.Contains("MaGS") && r["MaGS"] != DBNull.Value ? Convert.ToInt32(r["MaGS"]) : 0;

                double diemTB = 0;
                int luotDanhGia = 0;
                try
                {
                    DataTable dtDanhGia = phService.LayThongKeDanhGiaGiaSu(maGSFromRow);
                    if (dtDanhGia.Rows.Count > 0)
                    {
                        DataRow rowDanhGia = dtDanhGia.Rows[0];
                        if (rowDanhGia["DiemTB"] != DBNull.Value) diemTB = Convert.ToDouble(rowDanhGia["DiemTB"]);
                        if (rowDanhGia["LuotDanhGia"] != DBNull.Value) luotDanhGia = Convert.ToInt32(rowDanhGia["LuotDanhGia"]);
                    }
                }
                catch
                {
                    diemTB = 0;
                    luotDanhGia = 0;
                }

                using (Form frmProfile = new Form())
                {
                    frmProfile.Text = "Thông tin gia sư đã duyệt";
                    frmProfile.Size = new Size(500, 300);
                    frmProfile.StartPosition = FormStartPosition.CenterParent;
                    frmProfile.FormBorderStyle = FormBorderStyle.FixedDialog;
                    frmProfile.MaximizeBox = false;
                    frmProfile.MinimizeBox = false;
                    frmProfile.BackColor = Color.White;
                    frmProfile.Padding = new Padding(0);

                    Panel pnlRoot = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BackColor = Color.White,
                        Padding = new Padding(12)
                    };

                    Panel pnlBottom = new Panel
                    {
                        Dock = DockStyle.Bottom,
                        Height = 50,
                        BackColor = Color.White,
                        Padding = new Padding(0, 8, 0, 0)
                    };

                    Panel pnlImage = new Panel
                    {
                        Dock = DockStyle.Right,
                        Width = 160,
                        BackColor = Color.White,
                        Padding = new Padding(0)
                    };

                    PictureBox picMinhChung = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        BorderStyle = BorderStyle.FixedSingle,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BackColor = Color.WhiteSmoke,
                        Margin = new Padding(0)
                    };
                    TaiAnhGiaSu(picMinhChung, anhMinhChungPath);
                    pnlImage.Controls.Add(picMinhChung);

                    Panel pnlInfo = new Panel
                    {
                        Dock = DockStyle.Fill,
                        BackColor = Color.White,
                        Padding = new Padding(0, 0, 12, 0)
                    };

                    FlowLayoutPanel flpInfo = new FlowLayoutPanel
                    {
                        Dock = DockStyle.Fill,
                        FlowDirection = FlowDirection.TopDown,
                        AutoScroll = true,
                        AutoSize = false,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        WrapContents = false,
                        BackColor = Color.White,
                        Margin = new Padding(0),
                        Padding = new Padding(0)
                    };

                    Label lblHoTen = new Label
                    {
                        AutoSize = true,
                        MaximumSize = new Size(300, 0),
                        Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(24, 33, 53),
                        Margin = new Padding(0, 0, 0, 6),
                        Text = hoTen
                    };

                    Label lblSDT = new Label
                    {
                        AutoSize = true,
                        MaximumSize = new Size(300, 0),
                        Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                        Margin = new Padding(0, 0, 0, 2)
                    };

                    if (string.Equals(trangThaiDongPhi, "DaDong", StringComparison.OrdinalIgnoreCase))
                    {
                        lblSDT.Text = "SĐT Liên Hệ: " + sdt;
                        lblSDT.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblSDT.Text = "SĐT Liên Hệ: (Đang chờ gia sư hoàn tất thủ tục đóng phí)";
                        lblSDT.ForeColor = Color.Gray;
                    }

                    LinkLabel lnkXemDanhGia = new LinkLabel
                    {
                        AutoSize = true,
                        Text = luotDanhGia > 0 ? "(Xem đánh giá chi tiết)" : "",
                        LinkColor = Color.FromArgb(24, 119, 242),
                        ActiveLinkColor = Color.DarkOrange,
                        VisitedLinkColor = Color.FromArgb(24, 119, 242),
                        Margin = new Padding(0, 0, 0, 8),
                        Visible = luotDanhGia > 0
                    };
                    lnkXemDanhGia.LinkClicked += (s, ev) =>
                    {
                        if (maGS > 0)
                        {
                            using Form frmChiTiet = new FormChiTietDanhGia(maGS);
                            frmChiTiet.ShowDialog(frmProfile);
                        }
                    };

                    Label lblThongTinCaNhan = new Label
                    {
                        AutoSize = true,
                        MaximumSize = new Size(300, 0),
                        Font = new Font("Segoe UI", 10F),
                        ForeColor = Color.Black,
                        Margin = new Padding(0, 2, 0, 2),
                        Text = $"Giới tính: {gioiTinh} - Sinh năm: {namSinh}"
                    };

                    Label lblHocVan = new Label
                    {
                        AutoSize = true,
                        MaximumSize = new Size(300, 0),
                        Font = new Font("Segoe UI", 10F),
                        ForeColor = Color.Black,
                        Margin = new Padding(0, 2, 0, 2),
                        Text = $"{trinhDo} - Trường: {truong}"
                    };

                    Label lblChungChi = new Label
                    {
                        AutoSize = true,
                        MaximumSize = new Size(300, 0),
                        Font = new Font("Segoe UI", 10F),
                        ForeColor = Color.DarkGreen,
                        Margin = new Padding(0, 2, 0, 2),
                        Text = $"Chứng chỉ: {(string.IsNullOrWhiteSpace(maChungChi) ? "-" : maChungChi)} - Điểm: {(string.IsNullOrWhiteSpace(diemChungChi) ? "-" : diemChungChi)}"
                    };
                    lblChungChi.Visible = !(string.IsNullOrWhiteSpace(maChungChi) && string.IsNullOrWhiteSpace(diemChungChi));

                    Label lblThanhTich = new Label
                    {
                        AutoSize = true,
                        MaximumSize = new Size(300, 0),
                        Font = new Font("Segoe UI", 10F),
                        ForeColor = Color.Black,
                        Margin = new Padding(0, 2, 0, 2),
                        Text = string.IsNullOrWhiteSpace(thanhTich) ? "-" : thanhTich
                    };

                    flpInfo.Controls.Add(lblHoTen);
                    flpInfo.Controls.Add(lblSDT);
                    flpInfo.Controls.Add(lnkXemDanhGia);
                    flpInfo.Controls.Add(lblThongTinCaNhan);
                    flpInfo.Controls.Add(lblHocVan);
                    flpInfo.Controls.Add(lblChungChi);
                    flpInfo.Controls.Add(lblThanhTich);

                    pnlInfo.Controls.Add(flpInfo);

                    frmProfile.Controls.Add(pnlRoot);
                    pnlRoot.Controls.Add(pnlInfo);
                    pnlRoot.Controls.Add(pnlImage);
                    pnlRoot.Controls.Add(pnlBottom);

                    frmProfile.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Card_SuaClicked(object sender, EventArgs e)
        {
            if (sender is not ucCardBaiDangPH card) return;

            int maBD = (int)card.Tag;
            string trangThai = card.TrangThaiStr ?? "";

            if (trangThai != "ChuaGiao")
            {
                MessageBox.Show("Chỉ có thể sửa bài đăng ở trạng thái Chưa giao!");
                return;
            }

            string mon = card.TenMonHoc;
            string lop = card.TenLop;
            string hinhThuc = card.TenHinhThuc;
            string luong = card.MucLuong;
            string diaChi = card.SoNhaDuong;
            string ghiChu = card.YeuCauThem;

            if (frmDangBai != null)
            {
                frmDangBai.LoadDataForEdit(maBD, mon, lop, hinhThuc, luong, diaChi, ghiChu);
                tabControl1.SelectedIndex = 1;
                btnDangBaiMoi.BackColor = Color.FromArgb(24, 119, 242);
                btnQuanLyBai.BackColor = Color.Transparent;
            }
        }

        private void Card_XoaClicked(object sender, EventArgs e)
        {
            if (sender is not ucCardBaiDangPH card) return;

            int maBD = (int)card.Tag;
            string trangThai = card.TrangThaiStr ?? "";

            if (trangThai == "DaGiao" || trangThai == "DangGiaoDich")
            {
                MessageBox.Show("Lớp đang trong quá trình giao dịch hoặc đã giao cho gia sư, không thể xóa lúc này.\nVui lòng liên hệ Admin!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Xóa bài đăng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (baiDangService.XoaBaiDang(maBD))
                {
                    MessageBox.Show("Đã xóa bài đăng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataBaiDang();
                }
            }
        }

        private void btnQuanLyBai_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            btnQuanLyBai.BackColor = Color.FromArgb(24, 119, 242);
            btnDangBaiMoi.BackColor = Color.Transparent;
            LoadDataBaiDang();
        }

        private void btnDangBaiMoi_Click(object sender, EventArgs e)
        {
            frmDangBai?.ResetForm();
            tabControl1.SelectedIndex = 1;
            btnDangBaiMoi.BackColor = Color.FromArgb(24, 119, 242);
            btnQuanLyBai.BackColor = Color.Transparent;
        }

        private void btnTaiLai_Click(object sender, EventArgs e) => LoadDataBaiDang();

        private void btnDangXuat_Click(object sender, EventArgs e) => Close();

        private void btnDanhGia_Click(object sender, EventArgs e)
        {
        }

        private static string ChuanHoaHienThiDiemChungChi(string giaTri)
        {
            if (string.IsNullOrWhiteSpace(giaTri)) return "";
            string v = giaTri.Trim();
            v = v.Replace("C?p", "Cấp");
            v = v.Replace("c?p", "cấp");
            return v;
        }

        private static void TaiAnhGiaSu(PictureBox pic, string path)
        {
            try
            {
                pic.Image?.Dispose();
                pic.Image = null;
                if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
                {
                    using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    pic.Image = Image.FromStream(fs);
                    return;
                }
            }
            catch
            {
                pic.Image = null;
            }

            Bitmap avatar = new Bitmap(Math.Max(120, pic.Width), Math.Max(120, pic.Height));
            using (Graphics g = Graphics.FromImage(avatar))
            {
                g.Clear(Color.FromArgb(240, 243, 248));
                using Pen p = new Pen(Color.FromArgb(210, 215, 223), 2);
                g.DrawRectangle(p, 1, 1, avatar.Width - 3, avatar.Height - 3);
                using Font f = new Font("Segoe UI", 11F, FontStyle.Bold);
                using Brush b = new SolidBrush(Color.FromArgb(120, 130, 145));
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString("CHƯA CÓ\nẢNH", f, b, new RectangleF(0, 0, avatar.Width, avatar.Height), sf);
            }
            pic.Image = avatar;
        }

    }
}
