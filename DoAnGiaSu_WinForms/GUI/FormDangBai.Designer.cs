namespace DoAnGiaSu_WinForms.GUI
{
    partial class FormDangBai
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
            cboMonHoc = new ComboBox();
            label2 = new Label();
            cboLopHoc = new ComboBox();
            label3 = new Label();
            cboHinhThuc = new ComboBox();
            label4 = new Label();
            txtHocPhi = new TextBox();
            lblKhuVuc = new Label();
            cmbQuan = new ComboBox();
            label7 = new Label();
            cmbYeuCauTrinhDo = new ComboBox();
            label5 = new Label();
            txtDiaChiDay = new TextBox();
            label6 = new Label();
            txtGhiChu = new TextBox();
            btnXacNhanDang = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(185, 255, 255, 255);
            panel1.Controls.Add(labelTitle);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(cboMonHoc);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(cboLopHoc);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(cboHinhThuc);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(txtHocPhi);
            panel1.Controls.Add(lblKhuVuc);
            panel1.Controls.Add(cmbQuan);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(cmbYeuCauTrinhDo);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(txtDiaChiDay);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(txtGhiChu);
            panel1.Controls.Add(btnXacNhanDang);
            panel1.Location = new Point(50, 30);
            panel1.Name = "panel1";
            panel1.Size = new Size(700, 660);
            panel1.TabIndex = 0;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.Transparent;
            labelTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            labelTitle.ForeColor = Color.FromArgb(24, 33, 53);
            labelTitle.Location = new Point(0, 30);
            labelTitle.MinimumSize = new Size(700, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(700, 46);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "ĐĂNG BÀI TÌM GIA SƯ";
            labelTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(24, 33, 53);
            label1.Location = new Point(50, 100);
            label1.Name = "label1";
            label1.Size = new Size(140, 25);
            label1.TabIndex = 1;
            label1.Text = "Chọn môn học:";
            // 
            // cboMonHoc
            // 
            cboMonHoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMonHoc.FlatStyle = FlatStyle.Flat;
            cboMonHoc.Font = new Font("Segoe UI", 12F);
            cboMonHoc.FormattingEnabled = true;
            cboMonHoc.Location = new Point(50, 130);
            cboMonHoc.Name = "cboMonHoc";
            cboMonHoc.Size = new Size(280, 36);
            cboMonHoc.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label2.ForeColor = Color.FromArgb(24, 33, 53);
            label2.Location = new Point(370, 100);
            label2.Name = "label2";
            label2.Size = new Size(129, 25);
            label2.TabIndex = 3;
            label2.Text = "Chọn lớp học:";
            // 
            // cboLopHoc
            // 
            cboLopHoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLopHoc.FlatStyle = FlatStyle.Flat;
            cboLopHoc.Font = new Font("Segoe UI", 12F);
            cboLopHoc.FormattingEnabled = true;
            cboLopHoc.Location = new Point(370, 130);
            cboLopHoc.Name = "cboLopHoc";
            cboLopHoc.Size = new Size(280, 36);
            cboLopHoc.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label3.ForeColor = Color.FromArgb(24, 33, 53);
            label3.Location = new Point(50, 180);
            label3.Name = "label3";
            label3.Size = new Size(138, 25);
            label3.TabIndex = 5;
            label3.Text = "Hình thức dạy:";
            // 
            // cboHinhThuc
            // 
            cboHinhThuc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHinhThuc.FlatStyle = FlatStyle.Flat;
            cboHinhThuc.Font = new Font("Segoe UI", 12F);
            cboHinhThuc.FormattingEnabled = true;
            cboHinhThuc.Location = new Point(50, 210);
            cboHinhThuc.Name = "cboHinhThuc";
            cboHinhThuc.Size = new Size(280, 36);
            cboHinhThuc.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label4.ForeColor = Color.FromArgb(24, 33, 53);
            label4.Location = new Point(370, 180);
            label4.Name = "label4";
            label4.Size = new Size(215, 25);
            label4.TabIndex = 7;
            label4.Text = "Mức lương (VNĐ/Buổi):";
            // 
            // txtHocPhi
            // 
            txtHocPhi.BorderStyle = BorderStyle.FixedSingle;
            txtHocPhi.Font = new Font("Segoe UI", 12F);
            txtHocPhi.Location = new Point(370, 210);
            txtHocPhi.Name = "txtHocPhi";
            txtHocPhi.Size = new Size(280, 34);
            txtHocPhi.TabIndex = 8;
            // 
            // lblKhuVuc
            // 
            lblKhuVuc.AutoSize = true;
            lblKhuVuc.BackColor = Color.Transparent;
            lblKhuVuc.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            lblKhuVuc.ForeColor = Color.FromArgb(24, 33, 53);
            lblKhuVuc.Location = new Point(50, 260);
            lblKhuVuc.Name = "lblKhuVuc";
            lblKhuVuc.Size = new Size(263, 25);
            lblKhuVuc.TabIndex = 9;
            lblKhuVuc.Text = "Chọn khu vực (Quận/Huyện):";
            // 
            // cmbQuan
            // 
            cmbQuan.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbQuan.FlatStyle = FlatStyle.Flat;
            cmbQuan.Font = new Font("Segoe UI", 12F);
            cmbQuan.FormattingEnabled = true;
            cmbQuan.Location = new Point(50, 290);
            cmbQuan.Name = "cmbQuan";
            cmbQuan.Size = new Size(600, 36);
            cmbQuan.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label7.ForeColor = Color.FromArgb(24, 33, 53);
            label7.Location = new Point(50, 335);
            label7.Name = "label7";
            label7.Size = new Size(155, 25);
            label7.TabIndex = 11;
            label7.Text = "Yêu cầu trình độ:";
            // 
            // cmbYeuCauTrinhDo
            // 
            cmbYeuCauTrinhDo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbYeuCauTrinhDo.FlatStyle = FlatStyle.Flat;
            cmbYeuCauTrinhDo.Font = new Font("Segoe UI", 12F);
            cmbYeuCauTrinhDo.FormattingEnabled = true;
            cmbYeuCauTrinhDo.Location = new Point(50, 365);
            cmbYeuCauTrinhDo.Name = "cmbYeuCauTrinhDo";
            cmbYeuCauTrinhDo.Size = new Size(600, 36);
            cmbYeuCauTrinhDo.TabIndex = 12;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label5.ForeColor = Color.FromArgb(24, 33, 53);
            label5.Location = new Point(50, 415);
            label5.Name = "label5";
            label5.Size = new Size(111, 25);
            label5.TabIndex = 13;
            label5.Text = "Địa chỉ dạy:";
            // 
            // txtDiaChiDay
            // 
            txtDiaChiDay.BorderStyle = BorderStyle.FixedSingle;
            txtDiaChiDay.Font = new Font("Segoe UI", 12F);
            txtDiaChiDay.Location = new Point(50, 445);
            txtDiaChiDay.Name = "txtDiaChiDay";
            txtDiaChiDay.Size = new Size(600, 34);
            txtDiaChiDay.TabIndex = 14;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
            label6.ForeColor = Color.FromArgb(24, 33, 53);
            label6.Location = new Point(50, 495);
            label6.Name = "label6";
            label6.Size = new Size(150, 25);
            label6.TabIndex = 15;
            label6.Text = "Ghi chú/Yêu cầu";
            // 
            // txtGhiChu
            // 
            txtGhiChu.BorderStyle = BorderStyle.FixedSingle;
            txtGhiChu.Font = new Font("Segoe UI", 12F);
            txtGhiChu.Location = new Point(50, 525);
            txtGhiChu.Multiline = true;
            txtGhiChu.Name = "txtGhiChu";
            txtGhiChu.Size = new Size(600, 70);
            txtGhiChu.TabIndex = 16;
            // 
            // btnXacNhanDang
            // 
            btnXacNhanDang.BackColor = Color.FromArgb(24, 119, 242);
            btnXacNhanDang.FlatAppearance.BorderSize = 0;
            btnXacNhanDang.FlatStyle = FlatStyle.Flat;
            btnXacNhanDang.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnXacNhanDang.ForeColor = Color.White;
            btnXacNhanDang.Location = new Point(50, 605);
            btnXacNhanDang.Name = "btnXacNhanDang";
            btnXacNhanDang.Size = new Size(600, 52);
            btnXacNhanDang.TabIndex = 17;
            btnXacNhanDang.Text = "Đăng Bài Tìm Gia Sư";
            btnXacNhanDang.UseVisualStyleBackColor = false;
            btnXacNhanDang.Click += btnXacNhanDang_Click;
            // 
            // FormDangBai
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 730);
            Controls.Add(panel1);
            Name = "FormDangBai";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng bài tìm gia sư";
            Load += FormDangBai_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label labelTitle;
        private ComboBox cboMonHoc;
        private ComboBox cboLopHoc;
        private ComboBox cboHinhThuc;
        private TextBox txtHocPhi;
        private TextBox txtDiaChiDay;
        private TextBox txtGhiChu;
        private Button btnXacNhanDang;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label lblKhuVuc;
        private ComboBox cmbQuan;
        private Label label5;
        private Label label6;
        private Label label7;
        private ComboBox cmbYeuCauTrinhDo;
    }
}