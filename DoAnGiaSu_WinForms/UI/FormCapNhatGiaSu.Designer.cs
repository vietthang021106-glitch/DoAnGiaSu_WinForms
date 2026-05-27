namespace DoAnGiaSu_WinForms.GUI
{
    partial class FormCapNhatGiaSu
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
            txtCCCD = new TextBox();
            label4 = new Label();
            cboGioiTinh = new ComboBox();
            label5 = new Label();
            cboNamSinh = new ComboBox();
            label6 = new Label();
            cboTruong = new ComboBox();
            label7 = new Label();
            cboTrinhDo = new ComboBox();
            labelAnh = new Label();
            btnChonAnh = new Button();
            picMinhChung = new PictureBox();
            button1 = new Button();

            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picMinhChung).BeginInit();
            SuspendLayout();

            // panel1
            // 
            panel1.BackColor = Color.FromArgb(185, 255, 255, 255);
            panel1.Controls.Add(labelTitle);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(txtHoTen);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(txtSDT);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtCCCD);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(cboGioiTinh);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(cboNamSinh);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(cboTruong);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(cboTrinhDo);
            panel1.Controls.Add(labelAnh);
            panel1.Controls.Add(btnChonAnh);
            panel1.Controls.Add(picMinhChung);
            panel1.Controls.Add(button1);
            panel1.Location = new Point(140, 20);
            panel1.Name = "panel1";
            panel1.Size = new Size(520, 800);
            panel1.TabIndex = 0;

            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.Transparent;
            labelTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            labelTitle.ForeColor = Color.FromArgb(24, 33, 53);
            labelTitle.Location = new Point(0, 30);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(520, 41);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "HỒ SƠ GIA SƯ";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            labelTitle.MinimumSize = new Size(520, 0);

            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(24, 33, 53);
            label1.Location = new Point(60, 90);
            label1.Name = "label1";
            label1.Size = new Size(93, 25);
            label1.TabIndex = 1;
            label1.Text = "Họ và tên";

            // txtHoTen
            // 
            txtHoTen.Font = new Font("Segoe UI", 11F);
            txtHoTen.Location = new Point(60, 120);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(400, 32);
            txtHoTen.TabIndex = 2;
            txtHoTen.BorderStyle = BorderStyle.FixedSingle;

            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(24, 33, 53);
            label2.Location = new Point(60, 160);
            label2.Name = "label2";
            label2.Size = new Size(125, 25);
            label2.TabIndex = 3;
            label2.Text = "Số điện thoại";

            // txtSDT
            // 
            txtSDT.Font = new Font("Segoe UI", 11F);
            txtSDT.Location = new Point(60, 190);
            txtSDT.Name = "txtSDT";
            txtSDT.Size = new Size(400, 32);
            txtSDT.TabIndex = 4;
            txtSDT.BorderStyle = BorderStyle.FixedSingle;

            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(24, 33, 53);
            label3.Location = new Point(60, 230);
            label3.Name = "label3";
            label3.Size = new Size(90, 25);
            label3.TabIndex = 5;
            label3.Text = "Số CCCD";

            // txtCCCD
            // 
            txtCCCD.Font = new Font("Segoe UI", 11F);
            txtCCCD.Location = new Point(60, 260);
            txtCCCD.Name = "txtCCCD";
            txtCCCD.Size = new Size(400, 32);
            txtCCCD.TabIndex = 6;
            txtCCCD.BorderStyle = BorderStyle.FixedSingle;

            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(24, 33, 53);
            label4.Location = new Point(60, 300);
            label4.Name = "label4";
            label4.Size = new Size(84, 25);
            label4.TabIndex = 7;
            label4.Text = "Giới tính";

            // cboGioiTinh
            // 
            cboGioiTinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGioiTinh.Font = new Font("Segoe UI", 11F);
            cboGioiTinh.FormattingEnabled = true;
            cboGioiTinh.Location = new Point(60, 330);
            cboGioiTinh.Name = "cboGioiTinh";
            cboGioiTinh.Size = new Size(400, 33);
            cboGioiTinh.TabIndex = 8;
            cboGioiTinh.FlatStyle = FlatStyle.Flat;

            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(24, 33, 53);
            label5.Location = new Point(60, 370);
            label5.Name = "label5";
            label5.Size = new Size(93, 25);
            label5.TabIndex = 9;
            label5.Text = "Năm sinh";

            // cboNamSinh
            // 
            cboNamSinh.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNamSinh.Font = new Font("Segoe UI", 11F);
            cboNamSinh.FormattingEnabled = true;
            cboNamSinh.Location = new Point(60, 400);
            cboNamSinh.Name = "cboNamSinh";
            cboNamSinh.Size = new Size(400, 33);
            cboNamSinh.TabIndex = 10;
            cboNamSinh.FlatStyle = FlatStyle.Flat;

            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(24, 33, 53);
            label6.Location = new Point(60, 440);
            label6.Name = "label6";
            label6.Size = new Size(111, 25);
            label6.TabIndex = 11;
            label6.Text = "Trường học";

            // cboTruong
            // 
            cboTruong.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTruong.Font = new Font("Segoe UI", 11F);
            cboTruong.FormattingEnabled = true;
            cboTruong.Location = new Point(60, 470);
            cboTruong.Name = "cboTruong";
            cboTruong.Size = new Size(400, 33);
            cboTruong.TabIndex = 12;
            cboTruong.FlatStyle = FlatStyle.Flat;

            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(24, 33, 53);
            label7.Location = new Point(60, 510);
            label7.Name = "label7";
            label7.Size = new Size(84, 25);
            label7.TabIndex = 13;
            label7.Text = "Trình độ";

            // cboTrinhDo
            // 
            cboTrinhDo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrinhDo.Font = new Font("Segoe UI", 11F);
            cboTrinhDo.FormattingEnabled = true;
            cboTrinhDo.Location = new Point(60, 540);
            cboTrinhDo.Name = "cboTrinhDo";
            cboTrinhDo.Size = new Size(400, 33);
            cboTrinhDo.TabIndex = 14;
            cboTrinhDo.FlatStyle = FlatStyle.Flat;

            // labelAnh
            // 
            labelAnh.AutoSize = true;
            labelAnh.BackColor = Color.Transparent;
            labelAnh.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            labelAnh.ForeColor = Color.FromArgb(24, 33, 53);
            labelAnh.Location = new Point(60, 580);
            labelAnh.Name = "labelAnh";
            labelAnh.Size = new Size(150, 25);
            labelAnh.TabIndex = 15;
            labelAnh.Text = "Ảnh thẻ HV/GV";

            // btnChonAnh
            // 
            btnChonAnh.BackColor = Color.FromArgb(210, 215, 223);
            btnChonAnh.FlatAppearance.BorderSize = 0;
            btnChonAnh.FlatStyle = FlatStyle.Flat;
            btnChonAnh.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnChonAnh.ForeColor = Color.FromArgb(40, 55, 80);
            btnChonAnh.Location = new Point(60, 610);
            btnChonAnh.Name = "btnChonAnh";
            btnChonAnh.Size = new Size(100, 35);
            btnChonAnh.TabIndex = 16;
            btnChonAnh.Text = "Chọn ảnh";
            btnChonAnh.UseVisualStyleBackColor = false;
            btnChonAnh.Click += btnChonAnh_Click;

            // picMinhChung
            // 
            picMinhChung.BorderStyle = BorderStyle.FixedSingle;
            picMinhChung.Location = new Point(170, 585);
            picMinhChung.Name = "picMinhChung";
            picMinhChung.Size = new Size(290, 130);
            picMinhChung.SizeMode = PictureBoxSizeMode.Zoom;
            picMinhChung.TabIndex = 17;
            picMinhChung.TabStop = false;

            // button1
            // 
            button1.BackColor = Color.FromArgb(24, 119, 242);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(60, 735);
            button1.Name = "button1";
            button1.Size = new Size(400, 42);
            button1.TabIndex = 18;
            button1.Text = "Gửi Hồ Sơ Chờ Duyệt";
            button1.UseVisualStyleBackColor = false;
            button1.Click += btnLuuHoSo_Click;

            // FormCapNhatGiaSu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 850);
            Controls.Add(panel1);
            Name = "FormCapNhatGiaSu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Cập nhật hồ sơ Gia Sư";
            Load += FormCapNhatGiaSu_Load;

            ((System.ComponentModel.ISupportInitialize)picMinhChung).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label labelTitle;

        private TextBox txtHoTen;
        private TextBox txtSDT;
        private TextBox txtCCCD;
        private ComboBox cboGioiTinh;
        private ComboBox cboNamSinh;
        private ComboBox cboTruong;
        private Button button1;
        private ComboBox cboTrinhDo;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label labelAnh;
        private Button btnChonAnh;
        private PictureBox picMinhChung;
    }
}