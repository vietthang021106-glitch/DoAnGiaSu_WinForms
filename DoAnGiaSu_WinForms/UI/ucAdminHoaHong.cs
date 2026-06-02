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
        private readonly TableLayoutPanel tblButtons;

        private readonly Button btnTuChoiBill;
        private readonly Button btnXacNhan;
        private readonly FlowLayoutPanel flpMainContent;
        private readonly TableLayoutPanel tblBody;
        private readonly Panel pnlImage;
        private readonly Panel pnlContent;
        private readonly Label[] contentLabels;


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
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));

            lblMaBaiDang = new Label
            {
                Text = "Mã bài: ",
                AutoSize = true,
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 9F),
                Dock = DockStyle.Left,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 3, 10, 3)
            };
            lblTrangThai = new Label
            {
                AutoSize = true,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Dock = DockStyle.Right,
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(3, 3, 10, 3)
            };
            var pnlTopHH = new Panel { Dock = DockStyle.Fill, Margin = new Padding(0) };
            pnlTopHH.Controls.Add(lblTrangThai);
            pnlTopHH.Controls.Add(lblMaBaiDang);

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
            lblMaGS = CreateLabel(10F, FontStyle.Regular, Color.DimGray);

            contentLabels = new[] { lblPhuHuynh, lblMonHoc, lblMucLuong, lblHoaHong, lblMaGS };

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
            picBill.Click += XemAnh_Click;
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

            tblButtons = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0),
                Padding = new Padding(0),
                BackColor = Color.White
            };
            tblButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tblButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));


            btnTuChoiBill = CreateButton("Từ chối", Color.Goldenrod, Color.White);
            btnXacNhan = CreateButton("Xác nhận", Color.DodgerBlue, Color.White);


            btnTuChoiBill.Click += (_, _) => TuChoiBillClicked?.Invoke(this, EventArgs.Empty);
            btnXacNhan.Click += (_, _) => XacNhanClicked?.Invoke(this, EventArgs.Empty);

            btnTuChoiBill.Anchor = AnchorStyles.None;
            btnXacNhan.Anchor = AnchorStyles.None;

            tblButtons.Controls.Add(btnTuChoiBill, 0, 0);
            tblButtons.Controls.Add(btnXacNhan, 1, 0);


            tlpRoot.Controls.Add(pnlTopHH, 0, 0);
            tlpRoot.Controls.Add(pnlContent, 0, 1);
            tlpRoot.Controls.Add(tblButtons, 0, 2);

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
                Size = new Size(110, 32),
                Margin = new Padding(3),
                BackColor = backColor,
                ForeColor = foreColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Bold)
            };
            btn.FlatAppearance.BorderSize = 0;
            SetRoundedRegion(btn, 10);
            return btn;
        }

        private static void SetRoundedRegion(Button btn, int radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(btn.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(btn.Width - radius, btn.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, btn.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();
            btn.Region = new Region(path);
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

        private string _trangThaiGoc = "";

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string TrangThai
        {
            get => _trangThaiGoc;
            set
            {
                _trangThaiGoc = value ?? "";
                if (string.IsNullOrWhiteSpace(value) || value == "ChoDuyet" || value == "Chờ duyệt")
                {
                    lblTrangThai.Text = "Chờ duyệt";
                    lblTrangThai.ForeColor = Color.Orange;
                }
                else if (value == "DaDuyet" || value == "Đã duyệt")
                {
                    lblTrangThai.Text = "Đã duyệt";
                    lblTrangThai.ForeColor = Color.Green;
                }
                else if (value == "TuChoi" || value == "Từ chối")
                {
                    lblTrangThai.Text = "Đã từ chối";
                    lblTrangThai.ForeColor = Color.Red;
                }
            }
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

        private void XemAnh_Click(object sender, EventArgs e)
        {
            if (sender is PictureBox pb && pb.Image != null)
            {
                Form frm = new Form
                {
                    Text = "Xem ảnh chi tiết",
                    Size = new Size(800, 600),
                    StartPosition = FormStartPosition.CenterScreen
                };
                PictureBox pic = new PictureBox
                {
                    Dock = DockStyle.Fill,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Image = pb.Image
                };
                frm.Controls.Add(pic);
                frm.ShowDialog();
            }
        }
    }
}