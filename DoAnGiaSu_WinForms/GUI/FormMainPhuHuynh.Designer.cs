namespace DoAnGiaSu_WinForms.GUI
{
    partial class FormMainPhuHuynh
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
            btnDanhGia = new System.Windows.Forms.Button();
            panel1 = new System.Windows.Forms.Panel();
            panelSidebar = new System.Windows.Forms.Panel();
            btnDangBaiMoi = new System.Windows.Forms.Button();
            btnQuanLyBai = new System.Windows.Forms.Button();
            lblLogo = new System.Windows.Forms.Label();
            btnDangXuat = new System.Windows.Forms.Button();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            btnTaiLai = new System.Windows.Forms.Button();
            flpBaiDangCuaToi = new System.Windows.Forms.FlowLayoutPanel();
            btnDuyetGiaSu = new System.Windows.Forms.Button();
            tabPage2 = new System.Windows.Forms.TabPage();
            panel1.SuspendLayout();
            panelSidebar.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Transparent;
            panel1.Controls.Add(tabControl1);
            panel1.Controls.Add(panelSidebar);
            panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            panel1.Location = new System.Drawing.Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1200, 700);
            panel1.TabIndex = 0;
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = System.Drawing.Color.FromArgb(200, 40, 55, 80);
            panelSidebar.Controls.Add(btnDangXuat);
            panelSidebar.Controls.Add(btnDangBaiMoi);
            panelSidebar.Controls.Add(btnQuanLyBai);
            panelSidebar.Controls.Add(lblLogo);
            panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            panelSidebar.Location = new System.Drawing.Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new System.Drawing.Size(250, 700);
            panelSidebar.TabIndex = 0;
            // 
            // btnDangBaiMoi
            // 
            btnDangBaiMoi.Dock = System.Windows.Forms.DockStyle.Top;
            btnDangBaiMoi.FlatAppearance.BorderSize = 0;
            btnDangBaiMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDangBaiMoi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            btnDangBaiMoi.ForeColor = System.Drawing.Color.White;
            btnDangBaiMoi.Location = new System.Drawing.Point(0, 140);
            btnDangBaiMoi.Name = "btnDangBaiMoi";
            btnDangBaiMoi.Size = new System.Drawing.Size(250, 60);
            btnDangBaiMoi.TabIndex = 2;
            btnDangBaiMoi.Text = "Đăng bài mới";
            btnDangBaiMoi.UseVisualStyleBackColor = true;
            btnDangBaiMoi.Click += btnDangBaiMoi_Click;
            // 
            // btnQuanLyBai
            // 
            btnQuanLyBai.Dock = System.Windows.Forms.DockStyle.Top;
            btnQuanLyBai.FlatAppearance.BorderSize = 0;
            btnQuanLyBai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnQuanLyBai.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            btnQuanLyBai.ForeColor = System.Drawing.Color.White;
            btnQuanLyBai.Location = new System.Drawing.Point(0, 80);
            btnQuanLyBai.Name = "btnQuanLyBai";
            btnQuanLyBai.Size = new System.Drawing.Size(250, 60);
            btnQuanLyBai.TabIndex = 1;
            btnQuanLyBai.Text = "Bài đăng của tôi";
            btnQuanLyBai.UseVisualStyleBackColor = true;
            btnQuanLyBai.Click += btnQuanLyBai_Click;
            // 
            // lblLogo
            // 
            lblLogo.Dock = System.Windows.Forms.DockStyle.Top;
            lblLogo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblLogo.ForeColor = System.Drawing.Color.White;
            lblLogo.Location = new System.Drawing.Point(0, 0);
            lblLogo.Name = "lblLogo";
            lblLogo.Size = new System.Drawing.Size(250, 80);
            lblLogo.TabIndex = 0;
            lblLogo.Text = "PHỤ HUYNH";
            lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDangXuat
            // 
            btnDangXuat.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnDangXuat.FlatAppearance.BorderSize = 0;
            btnDangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDangXuat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            btnDangXuat.ForeColor = System.Drawing.Color.White;
            btnDangXuat.Location = new System.Drawing.Point(0, 620);
            btnDangXuat.Name = "btnDangXuat";
            btnDangXuat.Size = new System.Drawing.Size(250, 80);
            btnDangXuat.TabIndex = 3;
            btnDangXuat.Text = "Đăng xuất";
            btnDangXuat.UseVisualStyleBackColor = true;
            btnDangXuat.Click += btnDangXuat_Click;
            // 
            // tabControl1
            // 
            tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            tabControl1.ItemSize = new System.Drawing.Size(0, 1);
            tabControl1.Location = new System.Drawing.Point(250, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(950, 700);
            tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = System.Drawing.Color.Transparent;
            tabPage1.Controls.Add(flpBaiDangCuaToi);
            tabPage1.Controls.Add(btnTaiLai);
            tabPage1.Controls.Add(btnDuyetGiaSu);
            tabPage1.Controls.Add(btnDanhGia);
            tabPage1.Location = new System.Drawing.Point(4, 5);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(942, 691);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Bài đăng";
            // 
            // flpBaiDangCuaToi
            // 
            flpBaiDangCuaToi.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            flpBaiDangCuaToi.AutoScroll = true;
            flpBaiDangCuaToi.BackColor = System.Drawing.Color.White;
            flpBaiDangCuaToi.Location = new System.Drawing.Point(30, 30);
            flpBaiDangCuaToi.Name = "flpBaiDangCuaToi";
            flpBaiDangCuaToi.Size = new System.Drawing.Size(880, 550);
            flpBaiDangCuaToi.TabIndex = 0;
            // 
            // btnTaiLai
            // 
            btnTaiLai.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnTaiLai.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            btnTaiLai.FlatAppearance.BorderSize = 0;
            btnTaiLai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnTaiLai.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btnTaiLai.ForeColor = System.Drawing.Color.White;
            btnTaiLai.Location = new System.Drawing.Point(750, 600);
            btnTaiLai.Name = "btnTaiLai";
            btnTaiLai.Size = new System.Drawing.Size(160, 45);
            btnTaiLai.TabIndex = 2;
            btnTaiLai.Text = "Tải lại trang";
            btnTaiLai.UseVisualStyleBackColor = false;
            btnTaiLai.Click += btnTaiLai_Click;
            // 
            // btnDuyetGiaSu
            // 
            btnDuyetGiaSu.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btnDuyetGiaSu.BackColor = System.Drawing.Color.FromArgb(255, 193, 7);
            btnDuyetGiaSu.FlatAppearance.BorderSize = 0;
            btnDuyetGiaSu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDuyetGiaSu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btnDuyetGiaSu.ForeColor = System.Drawing.Color.Black;
            btnDuyetGiaSu.Location = new System.Drawing.Point(520, 600);
            btnDuyetGiaSu.Name = "btnDuyetGiaSu";
            btnDuyetGiaSu.Size = new System.Drawing.Size(160, 45);
            btnDuyetGiaSu.TabIndex = 4;
            btnDuyetGiaSu.Text = "Xem/Duyệt Gia Sư";
            btnDuyetGiaSu.UseVisualStyleBackColor = false;
            // 
            // btnDanhGia
            // 
            btnDanhGia.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            btnDanhGia.BackColor = System.Drawing.Color.FromArgb(111, 66, 193);
            btnDanhGia.Enabled = false;
            btnDanhGia.FlatAppearance.BorderSize = 0;
            btnDanhGia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnDanhGia.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            btnDanhGia.ForeColor = System.Drawing.Color.White;
            btnDanhGia.Location = new System.Drawing.Point(30, 600);
            btnDanhGia.Name = "btnDanhGia";
            btnDanhGia.Size = new System.Drawing.Size(160, 45);
            btnDanhGia.TabIndex = 5;
            btnDanhGia.Text = "Đánh giá Gia sư";
            btnDanhGia.Visible = false;
            btnDanhGia.UseVisualStyleBackColor = false;
            btnDanhGia.Click += btnDanhGia_Click;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = System.Drawing.Color.Transparent;
            tabPage2.Location = new System.Drawing.Point(4, 5);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(942, 691);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Đăng bài mới";
            // 
            // FormMainPhuHuynh
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1200, 700);
            Controls.Add(panel1);
            Name = "FormMainPhuHuynh";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Load += FormMainPhuHuynh_Load;
            panel1.ResumeLayout(false);
            panelSidebar.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Button btnDangBaiMoi;
        private System.Windows.Forms.Button btnQuanLyBai;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel flpBaiDangCuaToi;
        private System.Windows.Forms.Button btnXoaBai;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.Button btnSuaBai;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Button btnDuyetGiaSu;
        private System.Windows.Forms.Button btnDanhGia;
    }
}