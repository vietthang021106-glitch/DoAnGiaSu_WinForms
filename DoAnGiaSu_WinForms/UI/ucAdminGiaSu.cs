using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DoAnGiaSu_WinForms.GUI
{
    public class ucAdminGiaSu : UserControl
    {
        private readonly TableLayoutPanel tlpRoot;
        private readonly Label lblMaGS;
        private readonly Label lblHoTen;
        private readonly Label lblSDT;
        private readonly Label lblCCCD;
        private readonly Label lblGioiTinh;
        private readonly Label lblNamSinh;
        private readonly Label lblThanhTich;
        private readonly Label lblNamHoc;
        private readonly Label lblTruong;
        private readonly Label lblTrinhDo;
        private readonly Label lblChungChi;
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
        private readonly Panel pnlContent;
        private readonly Label[] infoLabels;

        public event EventHandler DuyetClicked;
        public event EventHandler TuChoiClicked;
        public event EventHandler XoaClicked;

        public ucAdminGiaSu()
        {
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.White;
            Margin = new Padding(10);
            Padding = new Padding(0);
            AutoSize = false;
            MinimumSize = new Size(330, 380);
            Size = new Size(330, 380);
            DoubleBuffered = true;

            tlpRoot = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3,
                Margin = new Padding(0),
                Padding = new Padding(0),
                BackColor = Color.White
            };
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));

            lblMaGS = new Label
            {
                Text = "Mã GS: ",
                AutoSize = false,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 9F),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 3, 10, 3)
            };

            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10, 5, 10, 5),
                Margin = new Padding(0)
            };

            flpMainContent = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                AutoSize = false,
                BackColor = Color.White,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            lblHoTen = CreateLabel(10F, FontStyle.Bold, Color.FromArgb(35, 45, 60), false);
            lblSDT = CreateLabel(9F, FontStyle.Regular, Color.DimGray, false);
            lblCCCD = CreateLabel(9F, FontStyle.Regular, Color.DimGray, false);
            lblGioiTinh = CreateLabel(9F, FontStyle.Regular, Color.DimGray, false);
            lblNamSinh = CreateLabel(9F, FontStyle.Regular, Color.DimGray, false);
            lblThanhTich = CreateLabel(9F, FontStyle.Regular, Color.DimGray, true);
            lblNamHoc = CreateLabel(9F, FontStyle.Regular, Color.DimGray, true);
            lblTruong = CreateLabel(9F, FontStyle.Regular, Color.DimGray, true);
            lblTrinhDo = CreateLabel(9F, FontStyle.Regular, Color.DimGray, true);
            lblChungChi = CreateLabel(9F, FontStyle.Regular, Color.DimGray, true);

            infoLabels = new[]
            {
                lblHoTen, lblSDT, lblCCCD, lblGioiTinh, lblNamSinh,
                lblThanhTich, lblNamHoc, lblTruong, lblTrinhDo, lblChungChi
            };

            flpMainContent.Controls.Add(lblHoTen);
            flpMainContent.Controls.Add(lblSDT);
            flpMainContent.Controls.Add(lblCCCD);
            flpMainContent.Controls.Add(lblGioiTinh);
            flpMainContent.Controls.Add(lblNamSinh);
            flpMainContent.Controls.Add(lblThanhTich);
            flpMainContent.Controls.Add(lblNamHoc);
            flpMainContent.Controls.Add(lblTruong);
            flpMainContent.Controls.Add(lblTrinhDo);
            flpMainContent.Controls.Add(lblChungChi);

            pnlImageStack = new Panel
            {
                Dock = DockStyle.Right,
                Width = 90,
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
            tblImages.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tblImages.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tblImages.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));

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
            tblBody.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 90F));
            tblBody.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tblBody.Controls.Add(flpMainContent, 0, 0);
            tblBody.Controls.Add(pnlImageStack, 1, 0);

            pnlContent.Controls.Add(tblBody);

            pnlFooter = new Panel
            {
                Dock = DockStyle.Fill,
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

            btnDuyet = CreateButton("Duyệt", Color.DodgerBlue, Color.White);
            btnTuChoi = CreateButton("Từ chối", Color.Goldenrod, Color.White);
            btnXoa = CreateButton("Xóa", Color.Crimson, Color.White);

            btnDuyet.Click += (_, _) => DuyetClicked?.Invoke(this, EventArgs.Empty);
            btnTuChoi.Click += (_, _) => TuChoiClicked?.Invoke(this, EventArgs.Empty);
            btnXoa.Click += (_, _) => XoaClicked?.Invoke(this, EventArgs.Empty);

            tblFooter.Controls.Add(btnDuyet, 0, 0);
            tblFooter.Controls.Add(btnTuChoi, 1, 0);
            tblFooter.Controls.Add(btnXoa, 2, 0);
            pnlFooter.Controls.Add(tblFooter);

            tlpRoot.Controls.Add(lblMaGS, 0, 0);
            tlpRoot.Controls.Add(pnlContent, 0, 1);
            tlpRoot.Controls.Add(pnlFooter, 0, 2);

            Controls.Add(tlpRoot);
            UpdateContentLayout();
        }

        private static Label CreateLabel(float size, FontStyle style, Color foreColor, bool ellipsis)
        {
            return new Label
            {
                AutoSize = true,
                MaximumSize = new Size(240, 0),
                Font = new Font("Segoe UI", size, style),
                ForeColor = foreColor,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 3, 0, 3),
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
                Margin = new Padding(4)
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
                Margin = new Padding(6, 0, 6, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void UpdateContentLayout()
        {
            if (flpMainContent == null || flpMainContent.IsDisposed || infoLabels == null)
            {
                return;
            }

            int contentWidth = Math.Max(120,
                flpMainContent.ClientSize.Width
                - flpMainContent.Padding.Left
                - flpMainContent.Padding.Right
                - SystemInformation.VerticalScrollBarWidth);

            foreach (var label in infoLabels)
            {
                if (label != null && !label.IsDisposed)
                {
                    label.MaximumSize = new Size(contentWidth, 0);
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateContentLayout();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(Pens.LightGray, 0, 0, Width - 1, Height - 1);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MaGS
        {
            get;
            set
            {
                field = value;
                lblMaGS.Text = $"Mã GS: {value}";
            }
        }

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
        public string GioiTinh
        {
            get => lblGioiTinh.Text;
            set
            {
                lblGioiTinh.Text = $"Giới tính: {value}";
                lblGioiTinh.Visible = !string.IsNullOrWhiteSpace(value);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string NamSinh
        {
            get => lblNamSinh.Text;
            set
            {
                lblNamSinh.Text = $"Năm sinh: {value}";
                lblNamSinh.Visible = !string.IsNullOrWhiteSpace(value);
            }
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
        public string NamHoc
        {
            get => lblNamHoc.Text;
            set
            {
                lblNamHoc.Text = $"Năm học: {value}";
                lblNamHoc.Visible = !string.IsNullOrWhiteSpace(value);
            }
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
        public string ChungChi
        {
            get => lblChungChi.Text;
            set
            {
                lblChungChi.Text = $"Chứng chỉ: {value}";
                lblChungChi.Visible = !string.IsNullOrWhiteSpace(value);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UrlAnhThe
        {
            set => SetPictureFromPath(picThe, value);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UrlAnhBang
        {
            set => SetPictureFromPath(picBangDiem, value);
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string UrlAnhChungChi
        {
            set => SetPictureFromPath(picChungChi, value);
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
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private static void SetPictureFromImage(PictureBox pictureBox, Image image)
        {
            pictureBox.Image = image == null ? null : new Bitmap(image);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}