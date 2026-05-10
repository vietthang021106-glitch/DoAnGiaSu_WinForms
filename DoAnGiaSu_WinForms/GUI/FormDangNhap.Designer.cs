namespace DoAnGiaSu_WinForms.GUI
{
    partial class FormDangNhap
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDangNhap));
            txtTenDangNhap = new TextBox();
            txtMatKhau = new TextBox();
            btnDangNhap = new Button();
            btnDangKy = new Button();
            btnQuenMatKhau = new Button();
            panel1 = new Panel();
            lblTaiKhoan = new Label();
            lblMatKhau = new Label();
            Label lblTieuDe;
            lblTieuDe = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Font = new Font("Segoe UI", 12F);
            txtTenDangNhap.Location = new Point(30, 90);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(360, 34);
            txtTenDangNhap.TabIndex = 0;
            // 
            // txtMatKhau
            // 
            txtMatKhau.Font = new Font("Segoe UI", 12F);
            txtMatKhau.Location = new Point(30, 165);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.PasswordChar = '*';
            txtMatKhau.Size = new Size(360, 34);
            txtMatKhau.TabIndex = 1;
            // 
            // btnDangNhap
            // 
            btnDangNhap.BackColor = Color.FromArgb(24, 119, 242);
            btnDangNhap.FlatAppearance.BorderSize = 0;
            btnDangNhap.FlatStyle = FlatStyle.Flat;
            btnDangNhap.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDangNhap.ForeColor = Color.White;
            btnDangNhap.Location = new Point(30, 225);
            btnDangNhap.Name = "btnDangNhap";
            btnDangNhap.Size = new Size(360, 42);
            btnDangNhap.TabIndex = 2;
            btnDangNhap.Text = "Đăng nhập";
            btnDangNhap.UseVisualStyleBackColor = false;
            btnDangNhap.Click += btnDangNhap_Click;
            // 
            // btnDangKy
            // 
            btnDangKy.BackColor = Color.White;
            btnDangKy.FlatAppearance.BorderColor = Color.FromArgb(210, 215, 223);
            btnDangKy.FlatStyle = FlatStyle.Flat;
            btnDangKy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnDangKy.ForeColor = Color.FromArgb(40, 55, 80);
            btnDangKy.Location = new Point(30, 276);
            btnDangKy.Name = "btnDangKy";
            btnDangKy.Size = new Size(360, 40);
            btnDangKy.TabIndex = 3;
            btnDangKy.Text = "Đăng ký";
            btnDangKy.UseVisualStyleBackColor = false;
            btnDangKy.Click += btnDangKy_Click;
            // 
            // btnQuenMatKhau
            // 
            btnQuenMatKhau.BackColor = Color.Transparent;
            btnQuenMatKhau.FlatAppearance.BorderSize = 0;
            btnQuenMatKhau.FlatStyle = FlatStyle.Flat;
            btnQuenMatKhau.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnQuenMatKhau.ForeColor = Color.FromArgb(24, 119, 242);
            btnQuenMatKhau.Location = new Point(30, 320);
            btnQuenMatKhau.Name = "btnQuenMatKhau";
            btnQuenMatKhau.Size = new Size(360, 28);
            btnQuenMatKhau.TabIndex = 4;
            btnQuenMatKhau.Text = "Quên mật khẩu?";
            btnQuenMatKhau.UseVisualStyleBackColor = false;
            btnQuenMatKhau.Click += btnQuenMatKhau_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(185, 255, 255, 255);
            panel1.Controls.Add(lblTieuDe);
            panel1.Controls.Add(lblTaiKhoan);
            panel1.Controls.Add(txtTenDangNhap);
            panel1.Controls.Add(lblMatKhau);
            panel1.Controls.Add(txtMatKhau);
            panel1.Controls.Add(btnDangNhap);
            panel1.Controls.Add(btnDangKy);
            panel1.Controls.Add(btnQuenMatKhau);
            panel1.Location = new Point(200, 55);
            panel1.Name = "panel1";
            panel1.Size = new Size(420, 360);
            panel1.TabIndex = 4;
            // 
            // lblTaiKhoan
            // 
            lblTaiKhoan.AutoSize = true;
            lblTaiKhoan.BackColor = Color.Transparent;
            lblTaiKhoan.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblTaiKhoan.ForeColor = Color.FromArgb(24, 33, 53);
            lblTaiKhoan.Location = new Point(30, 63);
            lblTaiKhoan.Name = "lblTaiKhoan";
            lblTaiKhoan.Size = new Size(88, 25);
            lblTaiKhoan.TabIndex = 4;
            lblTaiKhoan.Text = "Tài khoản";
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
            lblMatKhau.TabIndex = 5;
            lblMatKhau.Text = "Mật khẩu";
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.BackColor = Color.Transparent;
            lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.FromArgb(24, 33, 53);
            lblTieuDe.Location = new Point(133, 15);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(157, 37);
            lblTieuDe.TabIndex = 6;
            lblTieuDe.Text = "ĐĂNG NHẬP";
            // 
            // FormDangNhap
            // 
            AcceptButton = btnDangNhap;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(800, 450);
            Controls.Add(panel1);
            Name = "FormDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "HỆ THỐNG GIA SƯ ĐÀ NẴNG - ĐĂNG NHẬP";
            Load += FormDangNhap_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TextBox txtTenDangNhap;
        private TextBox txtMatKhau;
        private Button btnDangNhap;
        private Button btnDangKy;
        private Button btnQuenMatKhau;
        private Panel panel1;
        private Label lblTaiKhoan;
        private Label lblMatKhau;
    }
}