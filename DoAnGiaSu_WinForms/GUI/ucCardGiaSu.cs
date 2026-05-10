using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DoAnGiaSu_WinForms.GUI
{
    public class ucCardGiaSu : UserControl
    {
        private PictureBox picMinhChung;
        private FlowLayoutPanel flpThongTin;
        private Label lblHoTen;
        private Label lblDanhGia;
        private Label lblThongTinCaNhan;
        private Label lblHocVan;
        private Label lblChungChi;
        private Label lblThanhTich;
        private LinkLabel lnkXemDanhGia;
        private Button btnChonGiaSu;

        private string _hoTen;
        private string _tenGioiTinh;
        private string _namSinh;
        private string _tenTrinhDo;
        private string _tenTruong;
        private string _tenNamHoc;
        private string _tenChungChi;
        private string _diemChungChi;
        private string _thanhTich;
        private double _diemTrungBinh;
        private int _luotDanhGia;
        private int _maGS;

        public string HoTen => _hoTen;
        public string TenGioiTinh => _tenGioiTinh;
        public string NamSinh => _namSinh;
        public string TenTrinhDo => _tenTrinhDo;
        public string TenTruong => _tenTruong;
        public string TenNamHoc => _tenNamHoc;
        public string TenChungChi => _tenChungChi;
        public string DiemChungChi => _diemChungChi;
        public string ThanhTich => _thanhTich;
        public double DiemTrungBinh => _diemTrungBinh;
        public int LuotDanhGia => _luotDanhGia;

        public event EventHandler ChonGiaSuClicked;
        public event EventHandler XemDanhGiaClicked;

        public ucCardGiaSu()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            MinimumSize = new Size(450, 220);
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Padding = new Padding(8);

            // ========== LEFT PANEL - PICTURE BOX ==========
            Panel pnlLeft = new Panel
            {
                Dock = DockStyle.Left,
                Width = 140,
                BackColor = Color.White,
                Margin = new Padding(0)
            };

            picMinhChung = new PictureBox
            {
                Width = 120,
                Height = 150,
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(240, 243, 248),
                Margin = new Padding(5),
                Dock = DockStyle.Top
            };
            pnlLeft.Controls.Add(picMinhChung);

            // ========== RIGHT PANEL - INFO + BUTTON ==========
            Panel pnlRight = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10, 0, 10, 0),
                BackColor = Color.White,
                Margin = new Padding(0)
            };

            flpThongTin = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = false,
                Padding = new Padding(0),
                Margin = new Padding(0),
                BackColor = Color.White
            };

            // Họ tên - in đậm, chữ to, màu xanh dương
            lblHoTen = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(280, 0),
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.Navy,
                Margin = new Padding(0, 0, 0, 4)
            };

            // Đánh giá
            lblDanhGia = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(280, 0),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.DarkOrange,
                Margin = new Padding(0, 0, 0, 2)
            };

            // Link xem đánh giá
            lnkXemDanhGia = new LinkLabel
            {
                AutoSize = true,
                Text = "(Xem đánh giá chi tiết)",
                LinkColor = Color.FromArgb(24, 119, 242),
                ActiveLinkColor = Color.DarkOrange,
                VisitedLinkColor = Color.FromArgb(24, 119, 242),
                Margin = new Padding(0, 0, 0, 8),
                Visible = false
            };
            lnkXemDanhGia.LinkClicked += LnkXemDanhGia_LinkClicked;

            // Thông tin cá nhân (Giới tính - Năm sinh)
            lblThongTinCaNhan = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(280, 0),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.DarkGray,
                Margin = new Padding(0, 2, 0, 2)
            };

            // Học vấn (Trình độ - Năm - Trường)
            lblHocVan = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(280, 0),
                Font = new Font("Segoe UI", 9.5F),
                ForeColor = Color.Black,
                Margin = new Padding(0, 2, 0, 2)
            };

            // Chứng chỉ
            lblChungChi = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(280, 0),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.DarkGreen,
                Margin = new Padding(0, 2, 0, 2)
            };

            // Thành tích
            lblThanhTich = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(280, 0),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.DarkRed,
                Margin = new Padding(0, 2, 0, 2)
            };

            flpThongTin.Controls.Add(lblHoTen);
            flpThongTin.Controls.Add(lblDanhGia);
            flpThongTin.Controls.Add(lnkXemDanhGia);
            flpThongTin.Controls.Add(lblThongTinCaNhan);
            flpThongTin.Controls.Add(lblHocVan);
            flpThongTin.Controls.Add(lblChungChi);
            flpThongTin.Controls.Add(lblThanhTich);

            btnChonGiaSu = new Button
            {
                Text = "Duyệt Gia Sư Này",
                BackColor = Color.FromArgb(34, 177, 76),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                TabIndex = 0,
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                Location = new Point(10, 0),
                Size = new Size(180, 35)
            };
            btnChonGiaSu.FlatAppearance.BorderSize = 0;
            btnChonGiaSu.Click += (s, e) => ChonGiaSuClicked?.Invoke(this, EventArgs.Empty);

            pnlRight.Controls.Add(btnChonGiaSu);
            pnlRight.Controls.Add(flpThongTin);

            Controls.Add(pnlRight);
            Controls.Add(pnlLeft);
        }

        private void LnkXemDanhGia_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            if (_maGS <= 0) return;

            using Form frmChiTiet = new FormChiTietDanhGia(_maGS);
            frmChiTiet.ShowDialog();
            XemDanhGiaClicked?.Invoke(this, EventArgs.Empty);
        }

        public void LoadData(int maGS, string hoTen, string tenGioiTinh, string namSinh,
                            string tenTrinhDo, string tenTruong, string tenNamHoc,
                            string tenChungChi, string diemChungChi, string thanhTich,
                            string anhMinhChung, double diemTB = 0, int luotDanhGia = 0)
        {
            try
            {
                _maGS = maGS;
                _hoTen = hoTen;
                _tenGioiTinh = tenGioiTinh;
                _namSinh = namSinh;
                _tenTrinhDo = tenTrinhDo;
                _tenTruong = tenTruong;
                _tenNamHoc = tenNamHoc;
                _tenChungChi = tenChungChi;
                _diemChungChi = diemChungChi;
                _thanhTich = thanhTich;
                _diemTrungBinh = diemTB;
                _luotDanhGia = luotDanhGia;

                Tag = maGS;
                lblHoTen.Text = hoTen;

                if (_luotDanhGia > 0)
                {
                    lblDanhGia.Text = $"⭐ {Math.Round(diemTB, 1):0.0}/5 ({luotDanhGia} đánh giá)";
                    lblDanhGia.ForeColor = Color.DarkOrange;
                    lnkXemDanhGia.Visible = true;
                }
                else
                {
                    lblDanhGia.Text = "⭐ Chưa có đánh giá";
                    lblDanhGia.ForeColor = Color.Gray;
                    lnkXemDanhGia.Visible = false;
                }

                lblThongTinCaNhan.Text = $"Giới tính: {tenGioiTinh} - Sinh năm: {namSinh}";
                lblHocVan.Text = $"{tenTrinhDo} - Năm {tenNamHoc} - {tenTruong}";
                lblChungChi.Text = $"Chứng chỉ: {(string.IsNullOrWhiteSpace(tenChungChi) ? "-" : tenChungChi)} - Điểm: {(string.IsNullOrWhiteSpace(diemChungChi) ? "-" : diemChungChi)}";
                lblChungChi.Visible = !(string.IsNullOrWhiteSpace(tenChungChi) && string.IsNullOrWhiteSpace(diemChungChi));
                lblThanhTich.Text = $"Thành tích: {(string.IsNullOrWhiteSpace(thanhTich) ? "-" : thanhTich)}";
                lblThanhTich.Visible = !string.IsNullOrWhiteSpace(thanhTich);

                LoadAnhMinhChung(anhMinhChung);

                flpThongTin.AutoSize = true;
                flpThongTin.PerformLayout();

                btnChonGiaSu.Top = flpThongTin.Bottom + 10;

                int expectedHeight = btnChonGiaSu.Bottom + 15;
                if (expectedHeight < picMinhChung.Bottom + 15)
                {
                    expectedHeight = picMinhChung.Bottom + 15;
                }

                Height = expectedHeight;
            }
            catch { }
        }

        private void LoadAnhMinhChung(string anhPath)
        {
            try
            {
                picMinhChung.Image?.Dispose();
                picMinhChung.Image = null;

                if (!string.IsNullOrWhiteSpace(anhPath) && File.Exists(anhPath))
                {
                    using var fs = new FileStream(anhPath, FileMode.Open, FileAccess.Read);
                    picMinhChung.Image = Image.FromStream(fs);
                    return;
                }
            }
            catch
            {
                picMinhChung.Image = null;
            }

            try
            {
                Bitmap avatar = new Bitmap(Math.Max(120, picMinhChung.Width), Math.Max(120, picMinhChung.Height));
                using (Graphics g = Graphics.FromImage(avatar))
                {
                    g.Clear(Color.FromArgb(240, 243, 248));
                    using Pen p = new Pen(Color.FromArgb(210, 215, 223), 2);
                    g.DrawRectangle(p, 1, 1, avatar.Width - 3, avatar.Height - 3);
                    using Font f = new Font("Segoe UI", 10F, FontStyle.Bold);
                    using Brush b = new SolidBrush(Color.FromArgb(120, 130, 145));
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString("CHƯA CÓ\nẢNH", f, b, new RectangleF(0, 0, avatar.Width, avatar.Height), sf);
                }
                picMinhChung.Image = avatar;
            }
            catch
            {
                picMinhChung.BackColor = Color.FromArgb(240, 243, 248);
            }
        }
    }
}
