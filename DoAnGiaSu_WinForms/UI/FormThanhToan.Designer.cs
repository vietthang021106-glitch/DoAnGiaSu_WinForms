namespace DoAnGiaSu_WinForms.GUI
{
    partial class FormThanhToan
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
            picQR = new PictureBox();
            lblSoTien = new Label();
            lblNoiDung = new Label();
            btnDong = new Button();
            btnTaiAnh = new Button();
            picMinhChung = new PictureBox();
            lblMinhChung = new Label();
            ((System.ComponentModel.ISupportInitialize)picQR).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picMinhChung).BeginInit();
            SuspendLayout();
            // 
            // picQR
            // 
            picQR.Location = new Point(50, 80);
            picQR.Name = "picQR";
            picQR.Size = new Size(250, 250);
            picQR.SizeMode = PictureBoxSizeMode.StretchImage;
            picQR.TabIndex = 0;
            picQR.TabStop = false;
            // 
            // lblSoTien
            // 
            lblSoTien.AutoSize = true;
            lblSoTien.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSoTien.Location = new Point(50, 30);
            lblSoTien.Name = "lblSoTien";
            lblSoTien.Size = new Size(130, 21);
            lblSoTien.TabIndex = 1;
            lblSoTien.Text = "Số tiền phí: 0 VNĐ";
            // 
            // lblNoiDung
            // 
            lblNoiDung.AutoSize = true;
            lblNoiDung.Location = new Point(50, 340);
            lblNoiDung.Name = "lblNoiDung";
            lblNoiDung.Size = new Size(120, 15);
            lblNoiDung.TabIndex = 2;
            lblNoiDung.Text = "Nội dung chuyển khoản";
            // 
            // btnTaiAnh
            // 
            btnTaiAnh.Location = new Point(350, 340);
            btnTaiAnh.Name = "btnTaiAnh";
            btnTaiAnh.Size = new Size(100, 30);
            btnTaiAnh.TabIndex = 4;
            btnTaiAnh.Text = "Tải ảnh lên";
            btnTaiAnh.UseVisualStyleBackColor = true;
            btnTaiAnh.Click += btnTaiAnh_Click;
            // 
            // picMinhChung
            // 
            picMinhChung.BorderStyle = BorderStyle.FixedSingle;
            picMinhChung.Location = new Point(350, 80);
            picMinhChung.Name = "picMinhChung";
            picMinhChung.Size = new Size(250, 250);
            picMinhChung.SizeMode = PictureBoxSizeMode.Zoom;
            picMinhChung.TabIndex = 5;
            picMinhChung.TabStop = false;
            // 
            // lblMinhChung
            // 
            lblMinhChung.AutoSize = true;
            lblMinhChung.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblMinhChung.Location = new Point(350, 30);
            lblMinhChung.Name = "lblMinhChung";
            lblMinhChung.Size = new Size(139, 21);
            lblMinhChung.TabIndex = 6;
            lblMinhChung.Text = "Ảnh minh chứng:";
            // 
            // btnDong
            // 
            btnDong.Location = new Point(250, 390);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(150, 35);
            btnDong.TabIndex = 3;
            btnDong.Text = "Xác nhận đã chuyển";
            btnDong.UseVisualStyleBackColor = true;
            btnDong.Click += btnDong_Click;
            // 
            // FormThanhToan
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 450);
            Controls.Add(lblMinhChung);
            Controls.Add(picMinhChung);
            Controls.Add(btnTaiAnh);
            Controls.Add(btnDong);
            Controls.Add(lblNoiDung);
            Controls.Add(lblSoTien);
            Controls.Add(picQR);
            Name = "FormThanhToan";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thanh toán phí hoa hồng";
            Load += FormThanhToan_Load;
            ((System.ComponentModel.ISupportInitialize)picQR).EndInit();
            ((System.ComponentModel.ISupportInitialize)picMinhChung).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox picQR;
        private Label lblSoTien;
        private Label lblNoiDung;
        private Button btnDong;
        private Button btnTaiAnh;
        private PictureBox picMinhChung;
        private Label lblMinhChung;
    }
}