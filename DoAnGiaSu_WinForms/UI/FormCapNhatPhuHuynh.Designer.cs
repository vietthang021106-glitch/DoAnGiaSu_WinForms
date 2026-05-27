namespace DoAnGiaSu_WinForms.GUI
{
    partial class FormCapNhatPhuHuynh
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
            labelTitle = new Label();
            label1 = new Label();
            txtHoTen = new TextBox();
            label2 = new Label();
            txtSDT = new TextBox();
            label3 = new Label();
            txtDiaChi = new TextBox();
            label5 = new Label();
            cboQuan = new ComboBox();
            btnLuu = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(185, 255, 255, 255);
            panel1.Controls.Add(labelTitle);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtHoTen);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txtSDT);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtDiaChi);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(cboQuan);
            panel1.Controls.Add(btnLuu);
            panel1.Location = new Point(150, 50);
            panel1.Name = "panel1";
            panel1.Size = new Size(500, 480);
            panel1.TabIndex = 0;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.Transparent;
            labelTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            labelTitle.ForeColor = Color.FromArgb(24, 33, 53);
            labelTitle.Location = new Point(0, 30);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(500, 41);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "CẬP NHẬT HỒ SƠ PHỤ HUYNH";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            labelTitle.MinimumSize = new Size(500, 0);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(24, 33, 53);
            label1.Location = new Point(50, 100);
            label1.Name = "label1";
            label1.Size = new Size(93, 25);
            label1.TabIndex = 1;
            label1.Text = "Họ và tên";
            // 
            // txtHoTen
            // 
            txtHoTen.Font = new Font("Segoe UI", 11F);
            txtHoTen.Location = new Point(50, 130);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(400, 32);
            txtHoTen.TabIndex = 2;
            txtHoTen.BorderStyle = BorderStyle.FixedSingle;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(24, 33, 53);
            label2.Location = new Point(50, 175);
            label2.Name = "label2";
            label2.Size = new Size(125, 25);
            label2.TabIndex = 3;
            label2.Text = "Số điện thoại";
            // 
            // txtSDT
            // 
            txtSDT.Font = new Font("Segoe UI", 11F);
            txtSDT.Location = new Point(50, 205);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(400, 32);
            txtSDT.TabIndex = 4;
            txtSDT.BorderStyle = BorderStyle.FixedSingle;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(24, 33, 53);
            label3.Location = new Point(50, 250);
            label3.Name = "label3";
            label3.Size = new Size(70, 25);
            label3.TabIndex = 5;
            label3.Text = "Địa chỉ";
            // 
            // txtDiaChi
            // 
            txtDiaChi.Font = new Font("Segoe UI", 11F);
            txtDiaChi.Location = new Point(50, 280);
            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Size = new Size(400, 32);
            txtDiaChi.TabIndex = 6;
            txtDiaChi.BorderStyle = BorderStyle.FixedSingle;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(24, 33, 53);
            label5.Location = new Point(50, 325);
            label5.Name = "label5";
            label5.Size = new Size(120, 25);
            label5.TabIndex = 7;
            label5.Text = "Quận/Huyện";
            // 
            // cboQuan
            // 
            cboQuan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboQuan.Font = new Font("Segoe UI", 11F);
            cboQuan.FormattingEnabled = true;
            cboQuan.Location = new Point(50, 355);
            cboQuan.Name = "cboQuan";
            cboQuan.Size = new Size(400, 33);
            cboQuan.TabIndex = 8;
            cboQuan.FlatStyle = FlatStyle.Flat;
            // 
            // btnLuu
            // 
            btnLuu.BackColor = Color.FromArgb(24, 119, 242);
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(50, 410);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(400, 42);
            btnLuu.TabIndex = 9;
            btnLuu.Text = "Xác nhận LƯU";
            btnLuu.UseVisualStyleBackColor = false;
            btnLuu.Click += btnLuu_Click;
            // 
            // FormCapNhatPhuHuynh
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 580);
            Controls.Add(panel1);
            Name = "FormCapNhatPhuHuynh";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cập nhật hồ sơ Phụ Huynh";
            Load += FormCapNhatPhuHuynh_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label labelTitle;

        private Label label1;
        private TextBox txtHoTen;
        private Label label2;
        private TextBox txtSDT;
        private Label label3;
        private TextBox txtDiaChi;
        private Label label5;
        private ComboBox cboQuan;
        private Button btnLuu;
    }
}