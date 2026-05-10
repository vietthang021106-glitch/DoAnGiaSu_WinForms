namespace DoAnGiaSu_WinForms.GUI
{
    partial class FormDangKy
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
            txtMatKhau = new TextBox();
            txtTenDangNhap = new TextBox();
            txtXacNhanMK = new TextBox();
            btnDangNhap = new Button();
            groupBox1 = new GroupBox();
            radGiaSu = new RadioButton();
            radPhuHuynh = new RadioButton();
            panel1 = new Panel();
            btnQuayLai = new Button();
            lblVaiTro = new Label();
            lblXacNhanMK = new Label();
            lblMatKhau = new Label();
            lblTenDangNhap = new Label();
            lblTieuDe = new Label();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtMatKhau
            // 
            txtMatKhau.Font = new Font("Segoe UI", 12F);
            txtMatKhau.Location = new Point(30, 165);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.PasswordChar = '●';
            txtMatKhau.Size = new Size(360, 34);
            txtMatKhau.TabIndex = 1;
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Font = new Font("Segoe UI", 12F);
            txtTenDangNhap.Location = new Point(30, 90);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(360, 34);
            txtTenDangNhap.TabIndex = 0;
            // 
            // txtXacNhanMK
            // 
            txtXacNhanMK.Font = new Font("Segoe UI", 12F);
            txtXacNhanMK.Location = new Point(30, 240);
            txtXacNhanMK.Name = "txtXacNhanMK";
            txtXacNhanMK.PasswordChar = '●';
            txtXacNhanMK.Size = new Size(360, 34);
            txtXacNhanMK.TabIndex = 2;
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.FromArgb(24, 119, 242);
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Location = new Point(30, 395);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(360, 42);
            btnDangNhap.TabIndex = 4;
            btnDangNhap.Text = "Xác nhận đăng ký";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += new EventHandler(this.btnXacNhan_Click);
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Transparent;
            groupBox1.Controls.Add(radGiaSu);
            groupBox1.Controls.Add(radPhuHuynh);
            groupBox1.Location = new Point(30, 320);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(360, 64);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            // 
            // radGiaSu
            // 
            radGiaSu.AutoSize = true;
            radGiaSu.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            radGiaSu.ForeColor = Color.FromArgb(24, 33, 53);
            radGiaSu.Location = new Point(203, 24);
            radGiaSu.Name = "radGiaSu";
            radGiaSu.Size = new Size(83, 27);
            radGiaSu.TabIndex = 1;
            radGiaSu.TabStop = true;
            radGiaSu.Text = "Gia sư";
            radGiaSu.UseVisualStyleBackColor = true;
            // 
            // radPhuHuynh
            // 
            radPhuHuynh.AutoSize = true;
            radPhuHuynh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            radPhuHuynh.ForeColor = Color.FromArgb(24, 33, 53);
            radPhuHuynh.Location = new Point(62, 24);
            radPhuHuynh.Name = "radPhuHuynh";
            radPhuHuynh.Size = new Size(113, 27);
            radPhuHuynh.TabIndex = 0;
            radPhuHuynh.TabStop = true;
            radPhuHuynh.Text = "Phụ huynh";
            radPhuHuynh.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(185, 255, 255, 255);
            panel1.Controls.Add(btnQuayLai);
            panel1.Controls.Add(lblVaiTro);
            panel1.Controls.Add(lblXacNhanMK);
            panel1.Controls.Add(lblMatKhau);
            panel1.Controls.Add(lblTenDangNhap);
            panel1.Controls.Add(lblTieuDe);
            panel1.Controls.Add(txtTenDangNhap);
            panel1.Controls.Add(txtMatKhau);
            panel1.Controls.Add(txtXacNhanMK);
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(btnDangNhap);
            panel1.Location = new Point(180, 20);
            panel1.Name = "panel1";
            panel1.Size = new Size(420, 495);
            panel1.TabIndex = 7;
            // 
            // btnQuayLai
            // 
            btnQuayLai.BackColor = Color.White;
            btnQuayLai.FlatAppearance.BorderColor = Color.FromArgb(210, 215, 223);
            btnQuayLai.FlatStyle = FlatStyle.Flat;
            btnQuayLai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnQuayLai.ForeColor = Color.FromArgb(40, 55, 80);
            btnQuayLai.Location = new Point(30, 444);
            btnQuayLai.Name = "btnQuayLai";
            btnQuayLai.Size = new Size(360, 40);
            btnQuayLai.TabIndex = 5;
            btnQuayLai.Text = "Quay lại đăng nhập";
            btnQuayLai.UseVisualStyleBackColor = false;
            btnQuayLai.Click += new EventHandler(this.btnQuayLai_Click);
            // 
            // lblVaiTro
            // 
            lblVaiTro.AutoSize = true;
            lblVaiTro.BackColor = Color.Transparent;
            lblVaiTro.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblVaiTro.ForeColor = Color.FromArgb(24, 33, 53);
            lblVaiTro.Location = new Point(30, 292);
            lblVaiTro.Name = "lblVaiTro";
            lblVaiTro.Size = new Size(67, 25);
            lblVaiTro.TabIndex = 10;
            lblVaiTro.Text = "Vai trò";
            // 
            // lblXacNhanMK
            // 
            lblXacNhanMK.AutoSize = true;
            lblXacNhanMK.BackColor = Color.Transparent;
            lblXacNhanMK.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblXacNhanMK.ForeColor = Color.FromArgb(24, 33, 53);
            lblXacNhanMK.Location = new Point(30, 213);
            lblXacNhanMK.Name = "lblXacNhanMK";
            lblXacNhanMK.Size = new Size(159, 25);
            lblXacNhanMK.TabIndex = 9;
            lblXacNhanMK.Text = "Xác nhận mật khẩu";
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.BackColor = Color.Transparent;
            lblMatKhau.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblMatKhau.ForeColor = Color.FromArgb(24, 33, 53);
            lblMatKhau.Location = new Point(30, 138);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(86, 25);
            lblMatKhau.TabIndex = 8;
            lblMatKhau.Text = "Mật khẩu";
            // 
            // lblTenDangNhap
            // 
            lblTenDangNhap.AutoSize = true;
            lblTenDangNhap.BackColor = Color.Transparent;
            lblTenDangNhap.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblTenDangNhap.ForeColor = Color.FromArgb(24, 33, 53);
            lblTenDangNhap.Location = new Point(30, 63);
            lblTenDangNhap.Name = "lblTenDangNhap";
            lblTenDangNhap.Size = new Size(133, 25);
            lblTenDangNhap.TabIndex = 7;
            lblTenDangNhap.Text = "Tên đăng nhập";
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.BackColor = Color.Transparent;
            lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(24, 33, 53);
            lblTieuDe.Location = new Point(143, 15);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(132, 37);
            lblTieuDe.TabIndex = 6;
            lblTieuDe.Text = "ĐĂNG KÝ";
            // 
            // FormDangKy
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(800, 550);
            Controls.Add(panel1);
            Name = "FormDangKy";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HỆ THỐNG GIA SƯ ĐÀ NẴNG - ĐĂNG KÝ";
            Load += new EventHandler(this.FormDangKy_Load);
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtMatKhau;
        private TextBox txtTenDangNhap;
        private TextBox txtXacNhanMK;
        private Button btnDangNhap;
        private GroupBox groupBox1;
        private RadioButton radGiaSu;
        private RadioButton radPhuHuynh;
        private Panel panel1;
        private Button btnQuayLai;
        private Label lblVaiTro;
        private Label lblXacNhanMK;
        private Label lblMatKhau;
        private Label lblTenDangNhap;
        private Label lblTieuDe;
    }
}