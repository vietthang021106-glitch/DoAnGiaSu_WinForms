using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DoAnGiaSu_WinForms.GUI
{
    public class ucAdminHoaHong : UserControl
    {
        private readonly TableLayoutPanel tlpRoot;
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
        private readonly Panel pnlContent;
        private readonly Label[] contentLabels;

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
            Width = 350;
            Height = 300;
            MinimumSize = new Size(350, 300);
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

            lblMaBaiDang = new Label
            {
                Text = "Mã bài: ",
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

            lblPhuHuynh = CreateLabel(10F, FontStyle.Bold, Color.FromArgb(35, 45, 60));
            lblMonHoc = CreateLabel(10F, FontStyle.Regular, Color.Black);
            lblMucLuong = CreateLabel(10F, FontStyle.Regular, Color.Black);
            lblHoaHong = CreateLabel(12F, FontStyle.Bold, Color.Red);
            lblTrangThai = CreateLabel(10F, FontStyle.Regular, Color.DimGray);
            lblMaGS = CreateLabel(10F, FontStyle.Regular, Color.DimGray);

            contentLabels = new[] { lblPhuHuynh, lblMonHoc, lblMucLuong, lblHoaHong, lblTrangThai, lblMaGS };

            flpMainContent.Controls.Add(lblPhuHuynh);
            flpMainContent.Controls.Add(lblMonHoc);
            flpMainContent.Controls.Add(lblMucLuong);
            flpMainContent.Controls.Add(lblHoaHong);
            flpMainContent.Controls.Add(lblMaGS);

            pnlImage = new Panel
            {
                Dock = DockStyle.Right,
                Width = 70,
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
                Margin = new Padding(4)
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
            tblBody.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 70F));
            tblBody.Controls.Add(flpMainContent, 0, 0);
            tblBody.Controls.Add(pnlImage, 1, 0);

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
                Padding = new Padding(6, 2, 6, 4)
            };
            tblFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tblFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tblFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));

            btnXemAnh = CreateButton("Xem ảnh", Color.FromArgb(108, 117, 125), Color.White);
            btnTuChoiBill = CreateButton("Từ chối", Color.Goldenrod, Color.White);
            btnXacNhan = CreateButton("Xác nhận", Color.DodgerBlue, Color.White);

            btnXemAnh.Margin = new Padding(4, 0, 4, 0);
            btnTuChoiBill.Margin = new Padding(4, 0, 4, 0);
            btnXacNhan.Margin = new Padding(4, 0, 4, 0);

            btnXemAnh.Click += (_, _) => XemAnhClicked?.Invoke(this, EventArgs.Empty);
            btnTuChoiBill.Click += (_, _) => TuChoiBillClicked?.Invoke(this, EventArgs.Empty);
            btnXacNhan.Click += (_, _) => XacNhanClicked?.Invoke(this, EventArgs.Empty);

            tblFooter.Controls.Add(btnXemAnh, 0, 0);
            tblFooter.Controls.Add(btnTuChoiBill, 1, 0);
            tblFooter.Controls.Add(btnXacNhan, 2, 0);
            pnlFooter.Controls.Add(tblFooter);

            tlpRoot.Controls.Add(lblMaBaiDang, 0, 0);
            tlpRoot.Controls.Add(pnlContent, 0, 1);
            tlpRoot.Controls.Add(pnlFooter, 0, 2);

            Controls.Add(tlpRoot);
            UpdateContentLayout();
        }

        private static Label CreateLabel(float size, FontStyle style, Color foreColor)
        {
            return new Label
            {
                AutoSize = true,
                MaximumSize = new Size(240, 0),
                Font = new Font("Segoe UI", size, style),
                ForeColor = foreColor,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 3, 0, 3),
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
                Margin = new Padding(6, 0, 6, 0)
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void UpdateContentLayout()
        {
            if (flpMainContent == null || flpMainContent.IsDisposed || contentLabels == null)
            {
                return;
            }

            int contentWidth = Math.Max(120,
                flpMainContent.ClientSize.Width
                - flpMainContent.Padding.Left
                - flpMainContent.Padding.Right
                - SystemInformation.VerticalScrollBarWidth);

            foreach (var label in contentLabels)
            {
                if (label != null && !label.IsDisposed)
                {
                    label.MaximumSize = new Size(contentWidth, 0);
                }
            }

            if (tblFooter != null && !tblFooter.IsDisposed && btnXemAnh != null && btnTuChoiBill != null && btnXacNhan != null)
            {
                int footerWidth = Math.Max(120, tblFooter.ClientSize.Width - tblFooter.Padding.Left - tblFooter.Padding.Right);
                int totalMarginPerButton = btnXemAnh.Margin.Left + btnXemAnh.Margin.Right;
                int availableForButtons = Math.Max(0, footerWidth - totalMarginPerButton * 3);
                int minWidthForTuChoi = TextRenderer.MeasureText(btnTuChoiBill.Text, btnTuChoiBill.Font).Width + 20;
                int minWidthForXacNhan = TextRenderer.MeasureText(btnXacNhan.Text, btnXacNhan.Font).Width + 20;
                int minWidthForXem = TextRenderer.MeasureText(btnXemAnh.Text, btnXemAnh.Font).Width + 20;
                int requiredMin = Math.Max(minWidthForTuChoi, Math.Max(minWidthForXacNhan, minWidthForXem));
                int buttonWidth = Math.Max(requiredMin, availableForButtons / 3);
                tblFooter.ColumnStyles.Clear();
                tblFooter.ColumnCount = 3;
                tblFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                tblFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
                tblFooter.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
                btnXemAnh.Dock = DockStyle.Fill;
                btnTuChoiBill.Dock = DockStyle.Fill;
                btnXacNhan.Dock = DockStyle.Fill;
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
        public int MaBaiDang
        {
            get;
            set
            {
                field = value;
                lblMaBaiDang.Text = $"Mã bài: {value}";
            }
        }

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
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private static void SetPictureFromImage(PictureBox pictureBox, Image image)
        {
            pictureBox.Image = image == null ? null : new Bitmap(image);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}