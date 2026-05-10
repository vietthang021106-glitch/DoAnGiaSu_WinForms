using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace DoAnGiaSu_WinForms.GUI
{
    public class ucAdminGiaSu : UserControl
    {
        private readonly Label lblMaGS;
        private readonly Label lblHoTen;
        private readonly Label lblSDT;
        private readonly Label lblCCCD;
        private readonly Label lblThanhTich;
        private readonly Label lblTruong;
        private readonly Label lblTrinhDo;
        private readonly PictureBox picThe;
        private readonly PictureBox picBangDiem;
        private readonly PictureBox picChungChi;
        private readonly Panel pnlFooter;
        private readonly TableLayoutPanel tblFooter;
        private readonly Button btnDuyet;
        private readonly Button btnTuChoi;
        private readonly Button btnXoa;
        private readonly FlowLayoutPanel flpMainContent;
        private readonly TableLayoutPanel tblBody;
        private readonly Panel pnlImageStack;

        public event EventHandler DuyetClicked;
        public event EventHandler TuChoiClicked;
        public event EventHandler XoaClicked;

        public ucAdminGiaSu()
        {
            BorderStyle = BorderStyle.None;
            BackColor = Color.White;
            Margin = new Padding(12);
            Padding = new Padding(15);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Width = 420;
            MinimumSize = new Size(420, 520);
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            flpMainContent = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = false,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.White,
                Padding = new Padding(20),
                Margin = new Padding(0)
            };

            lblMaGS = CreateLabel(9F, FontStyle.Regular, Color.FromArgb(120, 120, 120), 24, false);
            lblHoTen = CreateLabel(13F, FontStyle.Bold, Color.FromArgb(35, 45, 60), 30, false);
            lblSDT = CreateLabel(10F, FontStyle.Regular, Color.FromArgb(70, 70, 70), 24, false);
            lblCCCD = CreateLabel(10F, FontStyle.Regular, Color.FromArgb(70, 70, 70), 24, false);
            lblThanhTich = CreateLabel(10F, FontStyle.Regular, Color.FromArgb(70, 70, 70), 44, true);
            lblTruong = CreateLabel(10F, FontStyle.Regular, Color.FromArgb(70, 70, 70), 44, true);
            lblTrinhDo = CreateLabel(10F, FontStyle.Regular, Color.FromArgb(70, 70, 70), 44, true);

            flpMainContent.Controls.Add(lblMaGS);
            flpMainContent.Controls.Add(lblHoTen);
            flpMainContent.Controls.Add(lblSDT);
            flpMainContent.Controls.Add(lblCCCD);
            flpMainContent.Controls.Add(lblThanhTich);
            flpMainContent.Controls.Add(lblTruong);
            flpMainContent.Controls.Add(lblTrinhDo);

            pnlImageStack = new Panel
            {
                Dock = DockStyle.Right,
                Width = 120,
                BackColor = Color.White,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            var tblImages = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            tblImages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tblImages.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tblImages.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            tblImages.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));

            picThe = CreatePictureBox();
            picBangDiem = CreatePictureBox();
            picChungChi = CreatePictureBox();

            tblImages.Controls.Add(picThe, 0, 0);
            tblImages.Controls.Add(picBangDiem, 0, 1);
            tblImages.Controls.Add(picChungChi, 0, 2);
            pnlImageStack.Controls.Add(tblImages);

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
            tblBody.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120F));
            tblBody.Controls.Add(flpMainContent, 0, 0);
            tblBody.Controls.Add(pnlImageStack, 1, 0);

            pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 55,
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
                Padding = new Padding(0)
            };
            tblFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tblFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tblFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));

            btnDuyet = CreateButton("Duyệt", Color.FromArgb(40, 167, 69), Color.White);
            btnTuChoi = CreateButton("Từ chối", Color.FromArgb(255, 193, 7), Color.Black);
            btnXoa = CreateButton("Xóa", Color.FromArgb(220, 53, 69), Color.White);

            btnDuyet.Click += (_, _) => DuyetClicked?.Invoke(this, EventArgs.Empty);
            btnTuChoi.Click += (_, _) => TuChoiClicked?.Invoke(this, EventArgs.Empty);
            btnXoa.Click += (_, _) => XoaClicked?.Invoke(this, EventArgs.Empty);

            tblFooter.Controls.Add(btnDuyet, 0, 0);
            tblFooter.Controls.Add(btnTuChoi, 1, 0);
            tblFooter.Controls.Add(btnXoa, 2, 0);
            pnlFooter.Controls.Add(tblFooter);

            Controls.Add(tblBody);
            Controls.Add(pnlFooter);
        }

        private static Label CreateLabel(float size, FontStyle style, Color foreColor, int height, bool ellipsis)
        {
            return new Label
            {
                AutoSize = true,
                MaximumSize = new Size(360, 0),
                Font = new Font("Segoe UI", size, style),
                ForeColor = foreColor,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 0, 0, 10),
                AutoEllipsis = ellipsis,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private static PictureBox CreatePictureBox()
        {
            return new PictureBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.WhiteSmoke,
                Dock = DockStyle.Fill,
                Margin = new Padding(5)
            };
        }

        private static Button CreateButton(string text, Color backColor, Color foreColor)
        {
            var btn = new Button
            {
                Text = text,
                Dock = DockStyle.Fill,
                Height = 34,
                BackColor = backColor,
                ForeColor = foreColor,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(10, 8, 10, 8)
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
        public int MaGS { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string HoTen
        {
            get => lblHoTen.Text;
            set => lblHoTen.Text = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SDT
        {
            get => lblSDT.Text;
            set => lblSDT.Text = $"SĐT: {value}";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string CCCD
        {
            get => lblCCCD.Text;
            set => lblCCCD.Text = $"CCCD: {value}";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ThanhTich
        {
            get => lblThanhTich.Text;
            set => lblThanhTich.Text = $"Thành tích: {value}";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Truong
        {
            get => lblTruong.Text;
            set => lblTruong.Text = $"Trường: {value}";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TrinhDo
        {
            get => lblTrinhDo.Text;
            set => lblTrinhDo.Text = $"Trình độ: {value}";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string AnhThePath
        {
            set => SetPictureFromPath(picThe, value);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string AnhBangDiemPath
        {
            set => SetPictureFromPath(picBangDiem, value);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string AnhChungChiPath
        {
            set => SetPictureFromPath(picChungChi, value);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image AnhTheImage
        {
            set => SetPictureFromImage(picThe, value);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image AnhBangDiemImage
        {
            set => SetPictureFromImage(picBangDiem, value);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Image AnhChungChiImage
        {
            set => SetPictureFromImage(picChungChi, value);
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