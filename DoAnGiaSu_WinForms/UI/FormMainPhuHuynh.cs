using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using DoAnGiaSu_WinForms.DAL;

namespace DoAnGiaSu_WinForms.GUI
{
    public partial class FormMainPhuHuynh : Form
    {
        private readonly string _user;
        private FormDangBai frmDangBai;

        private readonly BaiDangDAL bdDal = new BaiDangDAL();
        private readonly PhuHuynhDAL phDal = new PhuHuynhDAL();
        private readonly TaiKhoanDAL tkDal = new TaiKhoanDAL();

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

            // Ẩn nút "Xem/Duyệt Gia Sư" cũ (ngoài Form) vì đã có nút Xem trong mỗi thẻ Card
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
                int maPH = phDal.LayMaPH(maTK);
                if (maPH <= 0) return;

                flpBaiDangCuaToi.Controls.Clear();
                _selectedCard = null;

                using (SqlConnection conn = new DBConnection().GetConnection())
                {
                    conn.Open();
                    const string sql = @"SELECT bd.MaBaiDang, m.TenMon, l.TenLop, td.TenTrinhDo, ht.TenHinhThuc, 
                                                bd.MucLuong, bd.SoNhaDuong, bd.YeuCauThem, bd.TrangThai, q.TenQuan
                                         FROM BAIDANG bd
                                         JOIN DM_MONHOC m ON bd.MaMon = m.MaMon
                                         JOIN DM_LOPHOC l ON bd.MaLop = l.MaLop
                                         LEFT JOIN DM_TRINHDO td ON bd.YeuCauTrinhDo = td.MaTrinhDo
                                         LEFT JOIN DM_HINHTHUC ht ON bd.MaHinhThuc = ht.MaHinhThuc
                                         LEFT JOIN DM_QUANHUYEN q ON bd.MaQuan = q.MaQuan
                                         WHERE bd.MaPH = @maPH
                                         ORDER BY bd.MaBaiDang DESC";

                    using SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@maPH", SqlDbType.Int) { Value = maPH });

                    using SqlDataReader reader = cmd.ExecuteReader();
                    {
                        while (reader.Read())
                        {
                            int maBaiDang = (int)reader["MaBaiDang"];
                            string tenMon = reader["TenMon"]?.ToString() ?? "";
                            string tenLop = reader["TenLop"]?.ToString() ?? "";
                            string tenTrinhDo = reader["TenTrinhDo"]?.ToString() ?? "";
                            string tenHinhThuc = reader["TenHinhThuc"]?.ToString() ?? "";
                            string mucLuong = reader["MucLuong"]?.ToString() ?? "";
                            string soNhaDuong = reader["SoNhaDuong"]?.ToString() ?? "";
                            string yeuCauThem = reader["YeuCauThem"]?.ToString() ?? "";
                            string trangThai = reader["TrangThai"]?.ToString() ?? "";
                            string tenQuan = reader["TenQuan"]?.ToString() ?? "Chưa xác định";

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

            DataTable dt = bdDal.LayThongTinGiaSuDangKy(maBD);

            if (dt.Rows.Count == 0 && (trangThai == "DangGiaoDich" || trangThai == "DaGiao"))
            {
                dt = LayThongTinGiaSuTuBaiDangKhiThieuDangKy(maBD);
            }

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("Bài đăng này chưa có gia sư đăng ký.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            DataRow rDaDuyet = null;
            foreach (DataRow row in dt.Rows)
            {
                if ((row["TrangThaiDangKy"]?.ToString() ?? "") == "DaDuyet")
                {
                    rDaDuyet = row;
                    break;
                }
            }

            DataRow r = rDaDuyet ?? dt.Rows[0];
            string hoTen = r["HoTen"]?.ToString() ?? "";
            string sdt = r["SDT"]?.ToString() ?? "";
            string trinhDo = r["TenTrinhDo"]?.ToString() ?? "";
            string truong = r["TenTruong"]?.ToString() ?? "";
            string gioiTinh = r.Table.Columns.Contains("TenGioiTinh") ? r["TenGioiTinh"]?.ToString() ?? "" : "";
            string namSinh = r.Table.Columns.Contains("NamSinh") ? r["NamSinh"]?.ToString() ?? "" : "";
            string thanhTich = r.Table.Columns.Contains("ThanhTich") ? r["ThanhTich"]?.ToString() ?? "" : "";
            string namHoc = r.Table.Columns.Contains("TenNamHoc") ? r["TenNamHoc"]?.ToString() ?? "" : (r.Table.Columns.Contains("MaNamHoc") ? r["MaNamHoc"]?.ToString() ?? "" : "");
            string maChungChi = r.Table.Columns.Contains("TenChungChi")
                ? r["TenChungChi"]?.ToString() ?? ""
                : (r.Table.Columns.Contains("MaChungChi") ? r["MaChungChi"]?.ToString() ?? "" : "");
            string diemChungChi = r.Table.Columns.Contains("DiemChungChi") ? ChuanHoaHienThiDiemChungChi(r["DiemChungChi"]?.ToString() ?? "") : "";
            string anhMinhChungPath = r.Table.Columns.Contains("AnhMinhChung") ? r["AnhMinhChung"]?.ToString() ?? "" : "";
            string trangThaiDongPhi = r.Table.Columns.Contains("TrangThaiDongPhi") ? r["TrangThaiDongPhi"]?.ToString() ?? "" : "";
            int maGS = r.Table.Columns.Contains("MaGS") && r["MaGS"] != DBNull.Value ? Convert.ToInt32(r["MaGS"]) : 0;

            double diemTB = 0;
            int luotDanhGia = 0;
            try
            {
                using SqlConnection connDanhGia = new DBConnection().GetConnection();
                const string sqlDanhGia = @"SELECT CAST(AVG(CAST(SoSao AS FLOAT)) AS FLOAT) AS DiemTB, COUNT(*) AS LuotDanhGia
                                             FROM DANHGIA
                                             WHERE MaGS = @MaGS";
                using SqlCommand cmdDanhGia = new SqlCommand(sqlDanhGia, connDanhGia);
                cmdDanhGia.Parameters.Add(new SqlParameter("@MaGS", SqlDbType.Int) { Value = maGS });
                connDanhGia.Open();
                using SqlDataReader rdDanhGia = cmdDanhGia.ExecuteReader();
                if (rdDanhGia.Read())
                {
                    if (rdDanhGia["DiemTB"] != DBNull.Value) diemTB = Convert.ToDouble(rdDanhGia["DiemTB"]);
                    if (rdDanhGia["LuotDanhGia"] != DBNull.Value) luotDanhGia = Convert.ToInt32(rdDanhGia["LuotDanhGia"]);
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
                    lblSDT.Text = "SĐT Liên Hệ: (Vui lòng thanh toán phí hoa hồng để xem)";
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
                    ForeColor = Color.DarkRed,
                    Margin = new Padding(0, 2, 0, 2),
                    Text = $"Thành tích: {(string.IsNullOrWhiteSpace(thanhTich) ? "-" : thanhTich)}"
                };
                lblThanhTich.Visible = !string.IsNullOrWhiteSpace(thanhTich);

                Label lblDiemTB = new Label
                {
                    AutoSize = true,
                    MaximumSize = new Size(300, 0),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(120, 60, 0),
                    Margin = new Padding(0, 2, 0, 2),
                    Text = $"Đánh giá trung bình: {Math.Round(diemTB, 1):0.0}/5"
                };

                flpInfo.Controls.Add(lblHoTen);
                flpInfo.Controls.Add(lblSDT);
                flpInfo.Controls.Add(lnkXemDanhGia);
                flpInfo.Controls.Add(lblThongTinCaNhan);
                flpInfo.Controls.Add(lblHocVan);
                flpInfo.Controls.Add(lblChungChi);
                flpInfo.Controls.Add(lblThanhTich);
                flpInfo.Controls.Add(lblDiemTB);

                pnlInfo.Controls.Add(flpInfo);

                Button btnDong = new Button
                {
                    Text = "Đóng",
                    Dock = DockStyle.Fill,
                    BackColor = Color.FromArgb(24, 119, 242),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };
                btnDong.FlatAppearance.BorderSize = 0;
                btnDong.Click += (s, ev) => frmProfile.Close();

                pnlBottom.Controls.Add(btnDong);
                pnlRoot.Controls.Add(pnlInfo);
                pnlRoot.Controls.Add(pnlImage);
                frmProfile.Controls.Add(pnlRoot);
                frmProfile.Controls.Add(pnlBottom);
                frmProfile.ShowDialog();
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
                if (bdDal.XoaBaiDang(maBD))
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

        private DataTable LayThongTinGiaSuTuBaiDangKhiThieuDangKy(int maBD)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = new DBConnection().GetConnection())
            {
                const string sql = @"SELECT bd.MaGS, gs.HoTen, gs.SDT, gs.CCCD,
                                     tr.TenTruong, td.TenTrinhDo,
                                     gs.AnhMinhChung, gs.AnhBangDiem,
                                     ISNULL('GPA: ' + gs.DiemGPA, xl.TenXepLoai) AS ThanhTich, gs.MaNamHoc,
                                     gs.MaChungChi, dmcc.TenChungChi, gs.DiemChungChi,
                                     gt.TenGioiTinh, ns.Nam AS NamSinh,
                                     CAST('DaDuyet' AS NVARCHAR(20)) AS TrangThaiDangKy
                                     FROM BAIDANG bd
                                     JOIN GIASU gs ON bd.MaGS = gs.MaGS
                                     LEFT JOIN DM_TRUONG tr ON gs.MaTruong = tr.MaTruong
                                     LEFT JOIN DM_TRINHDO td ON gs.MaTrinhDo = td.MaTrinhDo
                                     LEFT JOIN DM_GIOITINH gt ON gs.MaGioiTinh = gt.MaGioiTinh
                                     LEFT JOIN DM_NAMSINH ns ON gs.MaNamSinh = ns.MaNamSinh
                                     LEFT JOIN DM_CHUNGCHI dmcc ON gs.MaChungChi = dmcc.MaChungChi
                                     LEFT JOIN DM_XEPLOAI xl ON gs.MaXepLoai = xl.MaXepLoai
                                     WHERE bd.MaBaiDang = @ma";

                using SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.Add(new SqlParameter("@ma", SqlDbType.Int) { Value = maBD });
                da.Fill(dt);
            }

            return dt;
        }
    }
}
