namespace DoAnGiaSu_WinForms.GUI
{
    partial class FormMainAdmin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panel1 = new Panel();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            flpBaiDang = new FlowLayoutPanel();
            pnlTopFilter = new Panel();
            cmbLocTrangThai = new ComboBox();
            tabPage2 = new TabPage();
            flpGiaSu = new FlowLayoutPanel();
            picMinhChung = new PictureBox();
            tabPage3 = new TabPage();
            flpHoaHong = new FlowLayoutPanel();
            panelSidebar = new Panel();
            btnDangXuat = new Button();
            btnNavHoaHong = new Button();
            btnNavGiaSu = new Button();
            btnNavBaiDang = new Button();
            lblLogo = new Label();
            panel1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            pnlTopFilter.SuspendLayout();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picMinhChung).BeginInit();
            tabPage3.SuspendLayout();
            panelSidebar.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(tabControl1);
            panel1.Controls.Add(panelSidebar);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1775, 539);
            panel1.TabIndex = 1;
            // 
            // tabControl1
            // 
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.Location = new Point(250, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.ShowToolTips = true;
            tabControl1.Size = new Size(1525, 539);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.Transparent;
            tabPage1.Controls.Add(flpBaiDang);
            tabPage1.Controls.Add(pnlTopFilter);
            tabPage1.Location = new Point(4, 5);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new Size(1517, 530);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Quản lý bài đăng";
            tabPage1.Click += tabPage1_Click;
            // 
            // flpBaiDang
            // 
            flpBaiDang.AutoScroll = true;
            flpBaiDang.BackColor = Color.FromArgb(240, 242, 245);
            flpBaiDang.Dock = DockStyle.Fill;
            flpBaiDang.Location = new Point(0, 50);
            flpBaiDang.Margin = new Padding(0);
            flpBaiDang.Name = "flpBaiDang";
            flpBaiDang.Padding = new Padding(12);
            flpBaiDang.Size = new Size(1517, 480);
            flpBaiDang.TabIndex = 0;
            // 
            // pnlTopFilter
            // 
            pnlTopFilter.BackColor = Color.WhiteSmoke;
            pnlTopFilter.Controls.Add(cmbLocTrangThai);
            pnlTopFilter.Dock = DockStyle.Top;
            pnlTopFilter.Location = new Point(0, 0);
            pnlTopFilter.Margin = new Padding(0);
            pnlTopFilter.Name = "pnlTopFilter";
            pnlTopFilter.Padding = new Padding(12, 10, 12, 10);
            pnlTopFilter.Size = new Size(1517, 50);
            pnlTopFilter.TabIndex = 1;
            // 
            // cmbLocTrangThai
            // 
            cmbLocTrangThai.Dock = DockStyle.Left;
            cmbLocTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbLocTrangThai.FormattingEnabled = true;
            cmbLocTrangThai.Items.AddRange(new object[] { "Tất cả", "ChuaGiao", "ChoPhuHuynhDuyet", "DangGiaoDich", "DaGiao" });
            cmbLocTrangThai.Location = new Point(12, 10);
            cmbLocTrangThai.Name = "cmbLocTrangThai";
            cmbLocTrangThai.Size = new Size(220, 36);
            cmbLocTrangThai.TabIndex = 1;
            cmbLocTrangThai.SelectedIndexChanged += cmbLocTrangThai_SelectedIndexChanged;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.Transparent;
            tabPage2.Controls.Add(flpGiaSu);
            tabPage2.Controls.Add(picMinhChung);
            tabPage2.Location = new Point(4, 5);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1428, 491);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Duyệt gia sư";
            // 
            // flpGiaSu
            // 
            flpGiaSu.AutoScroll = true;
            flpGiaSu.BackColor = Color.White;
            flpGiaSu.Dock = DockStyle.Fill;
            flpGiaSu.Location = new Point(3, 3);
            flpGiaSu.Name = "flpGiaSu";
            flpGiaSu.Padding = new Padding(12);
            flpGiaSu.Size = new Size(1422, 485);
            flpGiaSu.TabIndex = 0;
            // 
            // picMinhChung
            // 
            picMinhChung.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            picMinhChung.BorderStyle = BorderStyle.FixedSingle;
            picMinhChung.Location = new Point(1050, 20);
            picMinhChung.Name = "picMinhChung";
            picMinhChung.Size = new Size(350, 330);
            picMinhChung.SizeMode = PictureBoxSizeMode.Zoom;
            picMinhChung.TabIndex = 4;
            picMinhChung.TabStop = false;
            picMinhChung.Visible = false;
            // 
            // tabPage3
            // 
            tabPage3.BackColor = Color.Transparent;
            tabPage3.Controls.Add(flpHoaHong);
            tabPage3.Location = new Point(4, 5);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1428, 491);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Xác nhận Hoa hồng";
            // 
            // flpHoaHong
            // 
            flpHoaHong.AutoScroll = true;
            flpHoaHong.BackColor = Color.White;
            flpHoaHong.Dock = DockStyle.Fill;
            flpHoaHong.Location = new Point(0, 0);
            flpHoaHong.Name = "flpHoaHong";
            flpHoaHong.Padding = new Padding(12);
            flpHoaHong.Size = new Size(1428, 491);
            flpHoaHong.TabIndex = 0;
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(200, 40, 55, 80);
            panelSidebar.Controls.Add(btnDangXuat);
            panelSidebar.Controls.Add(btnNavHoaHong);
            panelSidebar.Controls.Add(btnNavGiaSu);
            panelSidebar.Controls.Add(btnNavBaiDang);
            panelSidebar.Controls.Add(lblLogo);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(250, 539);
            panelSidebar.TabIndex = 2;
            // 
            // btnDangXuat
            // 
            btnDangXuat.Dock = DockStyle.Bottom;
            btnDangXuat.FlatAppearance.BorderSize = 0;
            btnDangXuat.FlatStyle = FlatStyle.Flat;
            btnDangXuat.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDangXuat.ForeColor = Color.White;
            btnDangXuat.Location = new Point(0, 459);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.Size = new Size(250, 80);
            btnDangXuat.TabIndex = 4;
            btnDangXuat.Text = "Đăng xuất";
            btnDangXuat.UseVisualStyleBackColor = true;
            btnDangXuat.Click += btnDangXuat_Click;
            // 
            // btnNavHoaHong
            // 
            btnNavHoaHong.Dock = DockStyle.Top;
            btnNavHoaHong.FlatAppearance.BorderSize = 0;
            btnNavHoaHong.FlatStyle = FlatStyle.Flat;
            btnNavHoaHong.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnNavHoaHong.ForeColor = Color.White;
            btnNavHoaHong.Location = new Point(0, 200);
            btnNavHoaHong.Name = "btnNavHoaHong";
            btnNavHoaHong.Size = new Size(250, 60);
            btnNavHoaHong.TabIndex = 3;
            btnNavHoaHong.Text = "Xác nhận hoa hồng";
            btnNavHoaHong.UseVisualStyleBackColor = true;
            btnNavHoaHong.Click += btnNavHoaHong_Click;
            // 
            // btnNavGiaSu
            // 
            btnNavGiaSu.Dock = DockStyle.Top;
            btnNavGiaSu.FlatAppearance.BorderSize = 0;
            btnNavGiaSu.FlatStyle = FlatStyle.Flat;
            btnNavGiaSu.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnNavGiaSu.ForeColor = Color.White;
            btnNavGiaSu.Location = new Point(0, 140);
            btnNavGiaSu.Name = "btnNavGiaSu";
            btnNavGiaSu.Size = new Size(250, 60);
            btnNavGiaSu.TabIndex = 2;
            btnNavGiaSu.Text = "Duyệt gia sư";
            btnNavGiaSu.UseVisualStyleBackColor = true;
            btnNavGiaSu.Click += btnNavGiaSu_Click;
            // 
            // btnNavBaiDang
            // 
            btnNavBaiDang.Dock = DockStyle.Top;
            btnNavBaiDang.FlatAppearance.BorderSize = 0;
            btnNavBaiDang.FlatStyle = FlatStyle.Flat;
            btnNavBaiDang.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnNavBaiDang.ForeColor = Color.White;
            btnNavBaiDang.Location = new Point(0, 80);
            btnNavBaiDang.Name = "btnNavBaiDang";
            btnNavBaiDang.Size = new Size(250, 60);
            btnNavBaiDang.TabIndex = 1;
            btnNavBaiDang.Text = "Quản lý bài đăng";
            btnNavBaiDang.UseVisualStyleBackColor = true;
            btnNavBaiDang.Click += btnNavBaiDang_Click;
            // 
            // lblLogo
            // 
            lblLogo.Dock = DockStyle.Top;
            lblLogo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblLogo.ForeColor = Color.White;
            lblLogo.Location = new Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new Size(250, 80);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "ADMIN PANEL";
            lblLogo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FormMainAdmin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1775, 539);
            Controls.Add(panel1);
            Name = "FormMainAdmin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormMainAdmin";
            Load += FormMainAdmin_Load;
            panel1.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            pnlTopFilter.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)picMinhChung).EndInit();
            tabPage3.ResumeLayout(false);
            panelSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private FlowLayoutPanel flpBaiDang;
        private FlowLayoutPanel flpGiaSu;
        private FlowLayoutPanel flpHoaHong;
        private Panel panel1;
        private Panel panelSidebar;
        private Label lblLogo;
        private Button btnNavBaiDang;
        private Button btnNavGiaSu;
        private Button btnNavHoaHong;
        private Button btnDangXuat;
        private PictureBox picMinhChung;
        private Panel pnlTopFilter;
        private ComboBox cmbLocTrangThai;
    }
}