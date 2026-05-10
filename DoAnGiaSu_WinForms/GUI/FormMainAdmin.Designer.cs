namespace DoAnGiaSu_WinForms.GUI
{
    partial class FormMainAdmin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            panelSidebar = new Panel();
            btnNavHoaHong = new Button();
            btnNavGiaSu = new Button();
            btnNavBaiDang = new Button();
            btnDangXuat = new Button();
            lblLogo = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            btnDuyetBai = new Button();
            dgvDuyetBai = new DataGridView();
            tabPage2 = new TabPage();
            btnDuyetGiaSu = new Button();
            btnTuChoiGiaSu = new Button();
            btnXoaGiaSu = new Button();
            dgvDuyetGiaSu = new DataGridView();
            tabPage3 = new TabPage();
            btnXacNhanTien = new Button();
            btnTuChoiTien = new Button();
            dgvHoaHong = new DataGridView();
            picMinhChung = new PictureBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDuyetBai).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDuyetGiaSu).BeginInit();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHoaHong).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picMinhChung).BeginInit();
            panel1.SuspendLayout();
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
            panel1.Size = new Size(1686, 500);
            panel1.TabIndex = 1;
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
            panelSidebar.Size = new Size(250, 500);
            panelSidebar.TabIndex = 2;
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
            // btnDangXuat
            // 
            btnDangXuat.Dock = DockStyle.Bottom;
            btnDangXuat.FlatAppearance.BorderSize = 0;
            btnDangXuat.FlatStyle = FlatStyle.Flat;
            btnDangXuat.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnDangXuat.ForeColor = Color.White;
            btnDangXuat.Location = new Point(0, 420);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.Size = new Size(250, 80);
            btnDangXuat.TabIndex = 4;
            btnDangXuat.Text = "Đăng xuất";
            btnDangXuat.UseVisualStyleBackColor = true;
            btnDangXuat.Click += btnDangXuat_Click;
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
            // tabControl1
            // 
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tabControl1.Location = new Point(250, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.ShowToolTips = true;
            tabControl1.Size = new Size(1436, 500);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.Transparent;
            tabPage1.Controls.Add(btnDuyetBai);
            tabPage1.Controls.Add(dgvDuyetBai);
            tabPage1.Location = new Point(4, 37);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1658, 437);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Quản lý bài đăng";
            tabPage1.Click += tabPage1_Click;
            // 
            // btnDuyetBai
            // 
            btnDuyetBai.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDuyetBai.BackColor = Color.FromArgb(24, 119, 242);
            btnDuyetBai.FlatAppearance.BorderSize = 0;
            btnDuyetBai.FlatStyle = FlatStyle.Flat;
            btnDuyetBai.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDuyetBai.ForeColor = Color.White;
            btnDuyetBai.Location = new Point(1353, 370);
            btnDuyetBai.Name = "btnDuyetBai";
            btnDuyetBai.Size = new Size(277, 44);
            btnDuyetBai.TabIndex = 1;
            btnDuyetBai.Text = "Xóa bài";
            btnDuyetBai.UseVisualStyleBackColor = false;
            btnDuyetBai.Click += btnDuyetBai_Click;
            // 
            // dgvDuyetBai
            // 
            dgvDuyetBai.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDuyetBai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDuyetBai.BackgroundColor = Color.White;
            dgvDuyetBai.BorderStyle = BorderStyle.None;
            dgvDuyetBai.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDuyetBai.Location = new Point(20, 20);
            dgvDuyetBai.Name = "dgvDuyetBai";
            dgvDuyetBai.RowHeadersWidth = 51;
            dgvDuyetBai.Size = new Size(1610, 330);
            dgvDuyetBai.TabIndex = 0;
            dgvDuyetBai.CellContentClick += dgvDuyetBai_CellContentClick;
            dgvDuyetBai.CellValueChanged += dgvDuyetBai_CellValueChanged;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.Transparent;
            tabPage2.Controls.Add(picMinhChung);
            tabPage2.Controls.Add(btnDuyetGiaSu);
            tabPage2.Controls.Add(btnTuChoiGiaSu);
            tabPage2.Controls.Add(btnXoaGiaSu);
            tabPage2.Controls.Add(dgvDuyetGiaSu);
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1658, 445);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Duyệt gia sư";
            // 
            // btnDuyetGiaSu
            // 
            btnDuyetGiaSu.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnDuyetGiaSu.BackColor = Color.FromArgb(40, 167, 69);
            btnDuyetGiaSu.FlatAppearance.BorderSize = 0;
            btnDuyetGiaSu.FlatStyle = FlatStyle.Flat;
            btnDuyetGiaSu.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDuyetGiaSu.ForeColor = Color.White;
            btnDuyetGiaSu.Location = new Point(1093, 370);
            btnDuyetGiaSu.Name = "btnDuyetGiaSu";
            btnDuyetGiaSu.Size = new Size(160, 44);
            btnDuyetGiaSu.TabIndex = 1;
            btnDuyetGiaSu.Text = "Duyệt";
            btnDuyetGiaSu.UseVisualStyleBackColor = false;
            btnDuyetGiaSu.Click += btnDuyetGiaSu_Click;
            // 
            // btnTuChoiGiaSu
            // 
            btnTuChoiGiaSu.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnTuChoiGiaSu.BackColor = Color.FromArgb(255, 193, 7);
            btnTuChoiGiaSu.FlatAppearance.BorderSize = 0;
            btnTuChoiGiaSu.FlatStyle = FlatStyle.Flat;
            btnTuChoiGiaSu.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnTuChoiGiaSu.ForeColor = Color.White;
            btnTuChoiGiaSu.Location = new Point(1273, 370);
            btnTuChoiGiaSu.Name = "btnTuChoiGiaSu";
            btnTuChoiGiaSu.Size = new Size(160, 44);
            btnTuChoiGiaSu.TabIndex = 2;
            btnTuChoiGiaSu.Text = "Từ chối";
            btnTuChoiGiaSu.UseVisualStyleBackColor = false;
            btnTuChoiGiaSu.Click += btnTuChoiGiaSu_Click;
            // 
            // btnXoaGiaSu
            // 
            btnXoaGiaSu.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnXoaGiaSu.BackColor = Color.FromArgb(220, 53, 69);
            btnXoaGiaSu.FlatAppearance.BorderSize = 0;
            btnXoaGiaSu.FlatStyle = FlatStyle.Flat;
            btnXoaGiaSu.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnXoaGiaSu.ForeColor = Color.White;
            btnXoaGiaSu.Location = new Point(1453, 370);
            btnXoaGiaSu.Name = "btnXoaGiaSu";
            btnXoaGiaSu.Size = new Size(160, 44);
            btnXoaGiaSu.TabIndex = 3;
            btnXoaGiaSu.Text = "Xóa";
            btnXoaGiaSu.UseVisualStyleBackColor = false;
            btnXoaGiaSu.Click += btnXoaGiaSu_Click;
            // 
            // dgvDuyetGiaSu
            // 
            dgvDuyetGiaSu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDuyetGiaSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDuyetGiaSu.BackgroundColor = Color.White;
            dgvDuyetGiaSu.BorderStyle = BorderStyle.None;
            dgvDuyetGiaSu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDuyetGiaSu.Location = new Point(20, 20);
            dgvDuyetGiaSu.Name = "dgvDuyetGiaSu";
            dgvDuyetGiaSu.RowHeadersWidth = 51;
            dgvDuyetGiaSu.Size = new Size(1610, 330);
            dgvDuyetGiaSu.TabIndex = 0;
            dgvDuyetGiaSu.CellValueChanged += dgvDuyetGiaSu_CellValueChanged;
            dgvDuyetGiaSu.CellContentClick += dgvDuyetGiaSu_CellContentClick;
            // 
            // picMinhChung
            // 
            picMinhChung.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            picMinhChung.BorderStyle = BorderStyle.FixedSingle;
            picMinhChung.Location = new Point(1280, 20);
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
            tabPage3.Controls.Add(btnTuChoiTien);
            tabPage3.Controls.Add(btnXacNhanTien);
            tabPage3.Controls.Add(dgvHoaHong);
            tabPage3.Location = new Point(4, 29);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(1658, 445);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Xác nhận Hoa hồng";
            // 
            // btnXacNhanTien
            // 
            btnXacNhanTien.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnXacNhanTien.BackColor = Color.FromArgb(24, 119, 242);
            btnXacNhanTien.FlatAppearance.BorderSize = 0;
            btnXacNhanTien.FlatStyle = FlatStyle.Flat;
            btnXacNhanTien.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnXacNhanTien.ForeColor = Color.White;
            btnXacNhanTien.Location = new Point(1327, 370);
            btnXacNhanTien.Name = "btnXacNhanTien";
            btnXacNhanTien.Size = new Size(308, 48);
            btnXacNhanTien.TabIndex = 2;
            btnXacNhanTien.Text = "Xác nhận đã nhận tiền";
            btnXacNhanTien.UseVisualStyleBackColor = false;
            btnXacNhanTien.Click += btnXacNhanTien_Click;
            // 
            // btnTuChoiTien
            // 
            btnTuChoiTien.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnTuChoiTien.BackColor = Color.FromArgb(255, 193, 7);
            btnTuChoiTien.FlatAppearance.BorderSize = 0;
            btnTuChoiTien.FlatStyle = FlatStyle.Flat;
            btnTuChoiTien.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnTuChoiTien.ForeColor = Color.Black;
            btnTuChoiTien.Location = new Point(1130, 370);
            btnTuChoiTien.Name = "btnTuChoiTien";
            btnTuChoiTien.Size = new Size(180, 48);
            btnTuChoiTien.TabIndex = 3;
            btnTuChoiTien.Text = "Từ chối bill";
            btnTuChoiTien.UseVisualStyleBackColor = false;
            btnTuChoiTien.Click += btnTuChoiTien_Click;
            // 
            // dgvHoaHong
            // 
            dgvHoaHong.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvHoaHong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHoaHong.BackgroundColor = Color.White;
            dgvHoaHong.BorderStyle = BorderStyle.None;
            dgvHoaHong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHoaHong.Location = new Point(20, 20);
            dgvHoaHong.Name = "dgvHoaHong";
            dgvHoaHong.RowHeadersWidth = 51;
            dgvHoaHong.Size = new Size(1610, 330);
            dgvHoaHong.TabIndex = 1;
            dgvHoaHong.CellContentClick += dgvHoaHong_CellContentClick;
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
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDuyetBai).EndInit();
            tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvDuyetGiaSu).EndInit();
            tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHoaHong).EndInit();
            ((System.ComponentModel.ISupportInitialize)picMinhChung).EndInit();
            panelSidebar.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dgvDuyetBai;
        private Button btnDuyetBai;
        private TabPage tabPage3;
        private Button btnXacNhanTien;
        private Button btnTuChoiTien;
        private DataGridView dgvHoaHong;
        private DataGridView dgvDuyetGiaSu;
        private Button btnDuyetGiaSu;
        private Button btnTuChoiGiaSu;
        private Button btnXoaGiaSu;
        private Panel panel1;
        private Panel panelSidebar;
        private Label lblLogo;
        private Button btnNavBaiDang;
        private Button btnNavGiaSu;
        private Button btnNavHoaHong;
        private Button btnDangXuat;
        private PictureBox picMinhChung;
    }
}