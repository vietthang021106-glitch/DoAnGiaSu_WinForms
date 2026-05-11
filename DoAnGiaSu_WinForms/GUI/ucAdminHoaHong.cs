using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace DoAnGiaSu_WinForms.GUI
{
    public class ucAdminHoaHong : UserControl
    {
        private readonly Label lblMaBaiDang;
        private readonly Label lblPhuHuynh;
        private readonly Label lblMonHoc;
        private readonly Label lblMucLuong;
        private readonly Label lblHoaHong;
        private readonly Label lblTrangThai;
        private readonly Label lblMaGS;
        private readonly PictureBox picBill;
        private readonly Panel pnlFooter;
        private readonly TableLayoutPanel tblFooter;
        private readonly Button btnXemAnh;
        private readonly Button btnTuChoiBill;
        private readonly Button btnXacNhan;
        private readonly FlowLayoutPanel flpMainContent;
        private readonly TableLayoutPanel tblBody;
        private readonly Panel pnlImage;

        public event EventHandler XemAnhClicked;
        public event EventHandler TuChoiBillClicked;
        public event EventHandler XacNhanClicked;

        public ucAdminHoaHong()
        {
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.White;
            Margin = new Padding(10);
            Padding = new Padding(0);
            AutoSize = false;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Width = 380;
            Height = 300;
            MinimumSize = new Size(380, 300);
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            flpMainContent = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = false,
                AutoSize = false,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.White,
                Padding = new Padding(10, 5, 10, 5),
                Margin = new Padding(0)
            };

            lblMaBaiDang = CreateLabel(9F, FontStyle.Regular, Color.Gray, 22, false);
            lblPhuHuynh = CreateLabel(10F, FontStyle.Bold, Color.FromArgb(35, 45, 60), 24, false);
            lblMonHoc = CreateLabel(10F, FontStyle.Regular, Color.Black, 22, false);
            lblMucLuong = CreateLabel(10F, FontStyle.Regular, Color.Black, 22, false);
            lblHoaHong = CreateLabel(12F, FontStyle.Bold, Color.Red, 24, false);
            lblTrangThai = CreateLabel(10F, FontStyle.Regular, Color.DimGray, 22, false);
            lblMaGS = CreateLabel(10F, FontStyle.Regular, Color.DimGray, 22, false);

            flpMainContent.Controls.Add(lblMaBaiDang);
            flpMainContent.Controls.Add(lblPhuHuynh);
            flpMainContent.Controls.Add(lblMonHoc);
            flpMainContent.Controls.Add(lblMucLuong);
            flpMainContent.Controls.Add(lblHoaHong);
            flpMainContent.Controls.Add(lblTrangThai);
            flpMainContent.Controls.Add(lblMaGS);

            pnlImage = new Panel
            {
                Dock = DockStyle.Right,
                Width = 90,
                BackColor = Color.White,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            picBill = new PictureBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.WhiteSmoke,
                Dock = DockStyle.Fill,
                Margin = new Padding(5)
            };
            pnlImage.Controls.Add(picBill);

            tblBody = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                BackColor = Color.White,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            tblBody.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblBody.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tblBody.Controls.Add(flpMainContent, 0, 0);
            tblBody.Controls.Add(pnlImage, 1, 0);

            pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 45,
                BackColor = Color.White,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            tblFooter = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1,
                BackColor = Color.White,
                Margin = new Padding(0),
                Padding = new Padding(10, 5, 10, 8)
            };
            tblFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tblFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tblFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));

            btnXemAnh = CreateButton("Xem bill", Color.FromArgb(108, 117, 125), Color.White);
            btnTuChoiBill = CreateButton("Từ chối", Color.FromArgb(255, 193, 7), Color.Black);
            btnXacNhan = CreateButton("Xác nhận", Color.FromArgb(24, 119, 242), Color.White);

            btnXemAnh.Click += (_, _) => XemAnhClicked?.Invoke(this, EventArgs.Empty);
            btnTuChoiBill.Click += (_, _) => TuChoiBillClicked?.Invoke(this, EventArgs.Empty);
            btnXacNhan.Click += (_, _) => XacNhanClicked?.Invoke(this, EventArgs.Empty);

            tblFooter.Controls.Add(btnXemAnh, 0, 0);
            tblFooter.Controls.Add(btnTuChoiBill, 1, 0);
            tblFooter.Controls.Add(btnXacNhan, 2, 0);
            pnlFooter.Controls.Add(tblFooter);

            Controls.Add(tblBody);
            Controls.Add(pnlFooter);
        }

        private static Label CreateLabel(float size, FontStyle style, Color foreColor, int height, bool ellipsis)
        {
            return new Label
            {
                AutoSize = true,
                MaximumSize = new Size(340, 0),
                Font = new Font("Segoe UI", size, style),
                ForeColor = foreColor,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 3, 0, 3),
                AutoEllipsis = ellipsis,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private static Button CreateButton(string text, Color backColor, Color foreColor)
        {
            var btn = new Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                Height = 40,
                BackColor = backColor,
                ForeColor = foreColor,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(6, 4, 6, 4)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            ApplyRoundedRegion();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ApplyRoundedRegion();
            using Pen pen = new Pen(Color.LightGray, 1);
            var rect = ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            using var path = CreateRoundPath(rect, 8);
            e.Graphics.DrawPath(pen, path);
        }

        private void ApplyRoundedRegion()
        {
            if (Width <= 0 || Height <= 0) return;
            using var path = CreateRoundPath(new Rectangle(0, 0, Width - 1, Height - 1), 8);
            Region?.Dispose();
            Region = new Region(path);
        }

        private static GraphicsPath CreateRoundPath(Rectangle rect, int radius)
        {
            int diameter = radius * 2;
            var path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaBaiDang { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string PhuHuynh
        {
            get => lblPhuHuynh.Text;
            set => lblPhuHuynh.Text = $"Phụ huynh: {value}";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MonHoc
        {
            get => lblMonHoc.Text;
            set => lblMonHoc.Text = $"Môn học: {value}";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MucLuong
        {
            get => lblMucLuong.Text;
            set => lblMucLuong.Text = $"Mức lương: {value}";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string HoaHong
        {
            get => lblHoaHong.Text;
            set => lblHoaHong.Text = $"Hoa hồng: {value}";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TrangThai
        {
            get => lblTrangThai.Text;
            set => lblTrangThai.Text = $"Trạng thái: {value}";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MaGS
        {
            get => lblMaGS.Text;
            set => lblMaGS.Text = $"Mã GS: {value}";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string AnhBillPath
        {
            set => SetPictureFromPath(picBill, value);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image AnhBillImage
        {
            set => SetPictureFromImage(picBill, value);
        }

        private static void SetPictureFromPath(PictureBox pictureBox, string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                pictureBox.Image = null;
                return;
            }

            using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            using Image image = Image.FromStream(fs);
            pictureBox.Image = new Bitmap(image);
        }

        private static void SetPictureFromImage(PictureBox pictureBox, Image image)
        {
            pictureBox.Image = image == null ? null : new Bitmap(image);
        }
    }
}