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
            AutoSize = false;
            Size = new Size(520, 300);
            BackColor = Color.White;
            BorderStyle = BorderStyle.FixedSingle;
            Padding = new Padding(6);

            TableLayoutPanel tlpRoot = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 2,
                BackColor = Color.White,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140F));
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));

            picMinhChung = new PictureBox
            {
                Width = 120,
                Height = 170,
                SizeMode = PictureBoxSizeMode.Zoom,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(240, 243, 248),
                Margin = new Padding(2),
                Dock = DockStyle.Fill
            };

            flpThongTin = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                AutoScroll = false,
                AutoSize = false,
                WrapContents = false,
                Padding = new Padding(6),
                Margin = new Padding(0),
                BackColor = Color.White
            };

            lblHoTen = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(340, 0),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.Navy,
                Margin = new Padding(0, 0, 0, 3)
            };

            lblDanhGia = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(340, 0),
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                ForeColor = Color.DarkOrange,
                Margin = new Padding(0, 0, 0, 2)
            };

            lnkXemDanhGia = new LinkLabel
            {
                AutoSize = true,
                Text = "(Xem chi tiết)",
                LinkColor = Color.FromArgb(24, 119, 242),
                ActiveLinkColor = Color.DarkOrange,
                VisitedLinkColor = Color.FromArgb(24, 119, 242),
                Margin = new Padding(0, 0, 0, 3),
                Visible = false,
                Font = new Font("Segoe UI", 8.5F)
            };
            lnkXemDanhGia.LinkClicked += LnkXemDanhGia_LinkClicked;

            lblThongTinCaNhan = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(340, 0),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.DarkGray,
                Margin = new Padding(0, 1, 0, 1)
            };

            lblHocVan = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(340, 0),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Black,
                Margin = new Padding(0, 1, 0, 1)
            };

            lblChungChi = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(340, 0),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.DarkGreen,
                Margin = new Padding(0, 1, 0, 1)
            };

            lblThanhTich = new Label
            {
                AutoSize = true,
                MaximumSize = new Size(340, 0),
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.DarkRed,
                Margin = new Padding(0, 1, 0, 1)
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
                Text = "Chọn",
                Size = new Size(120, 40),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold),
                Dock = DockStyle.Right,
                Margin = new Padding(4)
            };
            btnChonGiaSu.FlatAppearance.BorderSize = 0;
            btnChonGiaSu.Click += (s, e) => ChonGiaSuClicked?.Invoke(this, EventArgs.Empty);

            tlpRoot.Controls.Add(picMinhChung, 0, 0);
            tlpRoot.Controls.Add(flpThongTin, 1, 0);
            tlpRoot.Controls.Add(btnChonGiaSu, 1, 1);

            Controls.Add(tlpRoot);
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
