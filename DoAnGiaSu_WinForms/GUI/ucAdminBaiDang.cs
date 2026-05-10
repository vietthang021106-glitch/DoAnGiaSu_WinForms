using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnGiaSu_WinForms.GUI
{
    public class ucAdminBaiDang : UserControl
    {
        private readonly TableLayoutPanel tlpRoot;
        private readonly Panel pnlContent;
        private readonly FlowLayoutPanel flpMainContent;
        private readonly TableLayoutPanel tlpButtons;
        private readonly Button btnDuyet;
        private readonly Button btnXoa;
        private readonly Label lblMaBaiDang;
        private readonly Label lblMonHoc;
        private readonly Label lblLop;
        private readonly Label lblHinhThuc;
        private readonly Label lblKhuVuc;
        private readonly Label lblMucLuong;
        private readonly Label lblPhuHuynh;
        private readonly Label lblTrangThai;
        private readonly Label lblMaGSNhan;
        private readonly Label lblYeuCau;

        public event EventHandler DuyetClicked;
        public event EventHandler XoaBaiClicked;

        public ucAdminBaiDang()
        {
            BorderStyle = BorderStyle.None;
            BackColor = Color.White;
            Margin = new Padding(10);
            Padding = new Padding(0);
            Width = 320;
            Height = 450;
            MinimumSize = new Size(320, 450);
            DoubleBuffered = true;

            tlpRoot = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(0),
                Padding = new Padding(0),
                BackColor = Color.White
            };
            tlpRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(15),
                Margin = new Padding(0)
            };

            flpMainContent = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };

            lblMaBaiDang = CreateLabel(9F, FontStyle.Regular, Color.Gray, 0, "Mã bài: ");
            lblMonHoc = CreateLabel(12F, FontStyle.Bold, Color.FromArgb(35, 45, 60), 0, "Môn học: ");
            lblLop = CreateLabel(10F, FontStyle.Bold, Color.DarkBlue, 0, "Lớp: ");
            lblHinhThuc = CreateLabel(10F, FontStyle.Regular, Color.DimGray, 0, "Hình thức: ");
            lblKhuVuc = CreateLabel(10F, FontStyle.Regular, Color.DimGray, 0, "Khu vực: ");
            lblMucLuong = CreateLabel(12F, FontStyle.Bold, Color.Red, 0, "Mức lương: ");
            lblPhuHuynh = CreateLabel(10F, FontStyle.Regular, Color.DimGray, 0, "Phụ huynh: ");
            lblYeuCau = CreateLabel(10F, FontStyle.Regular, Color.DimGray, 0, "Yêu cầu: ");
            lblTrangThai = CreateLabel(10F, FontStyle.Regular, Color.DimGray, 0, "Trạng thái: ");
            lblMaGSNhan = CreateLabel(10F, FontStyle.Regular, Color.DimGray, 0, "Mã GS nhận: ");

            flpMainContent.Controls.Add(lblMaBaiDang);
            flpMainContent.Controls.Add(lblMonHoc);
            flpMainContent.Controls.Add(lblLop);
            flpMainContent.Controls.Add(lblHinhThuc);
            flpMainContent.Controls.Add(lblKhuVuc);
            flpMainContent.Controls.Add(lblMucLuong);
            flpMainContent.Controls.Add(lblPhuHuynh);
            flpMainContent.Controls.Add(lblYeuCau);
            flpMainContent.Controls.Add(lblTrangThai);
            flpMainContent.Controls.Add(lblMaGSNhan);

            pnlContent.Controls.Add(flpMainContent);

            tlpButtons = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0),
                Padding = new Padding(0),
                BackColor = Color.White
            };
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tlpButtons.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            btnDuyet = new Button
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Text = "Duyệt"
            };
            btnDuyet.FlatAppearance.BorderSize = 0;
            btnDuyet.Click += (_, _) => DuyetClicked?.Invoke(this, EventArgs.Empty);

            btnXoa = new Button
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                BackColor = Color.Crimson,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Text = "Xóa"
            };
            btnXoa.FlatAppearance.BorderSize = 0;
            btnXoa.Click += (_, _) => XoaBaiClicked?.Invoke(this, EventArgs.Empty);

            tlpButtons.Controls.Add(btnDuyet, 0, 0);
            tlpButtons.Controls.Add(btnXoa, 1, 0);

            tlpRoot.Controls.Add(pnlContent, 0, 0);
            tlpRoot.Controls.Add(tlpButtons, 0, 1);

            Controls.Add(tlpRoot);
        }

        private static Label CreateLabel(float size, FontStyle style, Color foreColor, int height, string prefix)
        {
            return new Label
            {
                AutoSize = true,
                MaximumSize = new Size(280, 0),
                Font = new Font("Segoe UI", size, style),
                ForeColor = foreColor,
                BackColor = Color.Transparent,
                Margin = new Padding(0, 0, 0, 10),
                TextAlign = ContentAlignment.MiddleLeft,
                Text = prefix
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(Pens.LightGray, 0, 0, Width - 1, Height - 1);
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
        public string Lop
        {
            get => lblLop.Text;
            set => lblLop.Text = $"Lớp: {value}";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string HinhThuc
        {
            get => lblHinhThuc.Text;
            set => lblHinhThuc.Text = $"Hình thức: {value}";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string KhuVuc
        {
            get => lblKhuVuc.Text;
            set => lblKhuVuc.Text = $"Khu vực: {value}";
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
        public string TrangThai
        {
            get => lblTrangThai.Text;
            set => lblTrangThai.Text = $"Trạng thái: {value}";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MaGSNhan
        {
            get => lblMaGSNhan.Text;
            set => lblMaGSNhan.Text = $"Mã GS nhận: {value}";
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string YeuCau
        {
            get => lblYeuCau.Text;
            set => lblYeuCau.Text = $"Yêu cầu: {value}";
        }
    }
}