namespace DoAnGiaSu_WinForms.GUI
{
    partial class FormMainGiaSu
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

        private void InitializeComponent()
        {
            panel1 = new System.Windows.Forms.Panel();
            panelSidebar = new System.Windows.Forms.Panel();
            btnNavLopDaNhan = new System.Windows.Forms.Button();
            btnNavLopMoi = new System.Windows.Forms.Button();
            lblLogo = new System.Windows.Forms.Label();
            btnDangXuat = new System.Windows.Forms.Button();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            panelFilter = new System.Windows.Forms.Panel();
            cbMonHoc = new System.Windows.Forms.ComboBox();
            cbLop = new System.Windows.Forms.ComboBox();
            cbKhuVuc = new System.Windows.Forms.ComboBox();
            cbHinhThuc = new System.Windows.Forms.ComboBox();
            cmbSapXepGia = new System.Windows.Forms.ComboBox();
            btnReset = new System.Windows.Forms.Button();
            flpTimLop = new System.Windows.Forms.FlowLayoutPanel();
            tabPage2 = new System.Windows.Forms.TabPage();
            flpLopDaNhan = new System.Windows.Forms.FlowLayoutPanel();
            panel1.SuspendLayout();
            panelSidebar.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            panelFilter.SuspendLayout();
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
            panelSidebar.Controls.Add(btnNavLopDaNhan);
            panelSidebar.Controls.Add(btnNavLopMoi);
            panelSidebar.Controls.Add(lblLogo);
            panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            panelSidebar.Location = new System.Drawing.Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new System.Drawing.Size(250, 700);
            panelSidebar.TabIndex = 0;
            // 
            // btnNavLopDaNhan
            // 
            btnNavLopDaNhan.Dock = System.Windows.Forms.DockStyle.Top;
            btnNavLopDaNhan.FlatAppearance.BorderSize = 0;
            btnNavLopDaNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNavLopDaNhan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            btnNavLopDaNhan.ForeColor = System.Drawing.Color.White;
            btnNavLopDaNhan.Location = new System.Drawing.Point(0, 140);
            btnNavLopDaNhan.Name = "btnNavLopDaNhan";
            btnNavLopDaNhan.Size = new System.Drawing.Size(250, 60);
            btnNavLopDaNhan.TabIndex = 2;
            btnNavLopDaNhan.Text = "Lớp đã nhận";
            btnNavLopDaNhan.UseVisualStyleBackColor = true;
            btnNavLopDaNhan.Click += btnNavLopDaNhan_Click;
            // 
            // btnNavLopMoi
            // 
            btnNavLopMoi.Dock = System.Windows.Forms.DockStyle.Top;
            btnNavLopMoi.FlatAppearance.BorderSize = 0;
            btnNavLopMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnNavLopMoi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            btnNavLopMoi.ForeColor = System.Drawing.Color.White;
            btnNavLopMoi.Location = new System.Drawing.Point(0, 80);
            btnNavLopMoi.Name = "btnNavLopMoi";
            btnNavLopMoi.Size = new System.Drawing.Size(250, 60);
            btnNavLopMoi.TabIndex = 1;
            btnNavLopMoi.Text = "Tìm lớp mới";
            btnNavLopMoi.UseVisualStyleBackColor = true;
            btnNavLopMoi.Click += btnNavLopMoi_Click;
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
            lblLogo.Text = "GIA SƯ";
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
            tabPage1.Controls.Add(flpTimLop);
            tabPage1.Controls.Add(panelFilter);
            tabPage1.Location = new System.Drawing.Point(4, 5);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(3);
            tabPage1.Size = new System.Drawing.Size(942, 691);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Lớp mới";
            // 
            // panelFilter
            // 
            panelFilter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panelFilter.BackColor = System.Drawing.Color.FromArgb(245, 248, 252);
            panelFilter.Controls.Add(btnReset);
            panelFilter.Controls.Add(cmbSapXepGia);
            panelFilter.Controls.Add(cbHinhThuc);
            panelFilter.Controls.Add(cbKhuVuc);
            panelFilter.Controls.Add(cbLop);
            panelFilter.Controls.Add(cbMonHoc);
            panelFilter.Location = new System.Drawing.Point(30, 20);
            panelFilter.Name = "panelFilter";
            panelFilter.Size = new System.Drawing.Size(880, 52);
            panelFilter.TabIndex = 2;
            // 
            // cbMonHoc
            // 
            cbMonHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbMonHoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            cbMonHoc.FormattingEnabled = true;
            cbMonHoc.Location = new System.Drawing.Point(10, 10);
            cbMonHoc.Name = "cbMonHoc";
            cbMonHoc.Size = new System.Drawing.Size(160, 31);
            cbMonHoc.TabIndex = 0;
            // 
            // cbLop
            // 
            cbLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbLop.Font = new System.Drawing.Font("Segoe UI", 10F);
            cbLop.FormattingEnabled = true;
            cbLop.Location = new System.Drawing.Point(180, 10);
            cbLop.Name = "cbLop";
            cbLop.Size = new System.Drawing.Size(130, 31);
            cbLop.TabIndex = 1;
            // 
            // cbKhuVuc
            // 
            cbKhuVuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbKhuVuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            cbKhuVuc.FormattingEnabled = true;
            cbKhuVuc.Location = new System.Drawing.Point(320, 10);
            cbKhuVuc.Name = "cbKhuVuc";
            cbKhuVuc.Size = new System.Drawing.Size(130, 31);
            cbKhuVuc.TabIndex = 2;
            // 
            // cbHinhThuc
            // 
            cbHinhThuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cbHinhThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            cbHinhThuc.FormattingEnabled = true;
            cbHinhThuc.Location = new System.Drawing.Point(460, 10);
            cbHinhThuc.Name = "cbHinhThuc";
            cbHinhThuc.Size = new System.Drawing.Size(130, 31);
            cbHinhThuc.TabIndex = 3;
            // 
            // cmbSapXepGia
            // 
            cmbSapXepGia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            cmbSapXepGia.Font = new System.Drawing.Font("Segoe UI", 10F);
            cmbSapXepGia.FormattingEnabled = true;
            cmbSapXepGia.Location = new System.Drawing.Point(600, 10);
            cmbSapXepGia.Name = "cmbSapXepGia";
            cmbSapXepGia.Size = new System.Drawing.Size(140, 31);
            cmbSapXepGia.TabIndex = 4;
            // 
            // btnReset
            // 
            btnReset.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnReset.BackColor = System.Drawing.Color.FromArgb(108, 117, 125);
            btnReset.FlatAppearance.BorderSize = 0;
            btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnReset.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            btnReset.ForeColor = System.Drawing.Color.White;
            btnReset.Location = new System.Drawing.Point(750, 10);
            btnReset.Name = "btnReset";
            btnReset.Size = new System.Drawing.Size(120, 31);
            btnReset.TabIndex = 5;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = false;
            // 
            // flpTimLop
            // 
            flpTimLop.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            flpTimLop.AutoScroll = true;
            flpTimLop.BackColor = System.Drawing.Color.White;
            flpTimLop.Location = new System.Drawing.Point(30, 82);
            flpTimLop.Name = "flpTimLop";
            flpTimLop.Size = new System.Drawing.Size(880, 600);
            flpTimLop.TabIndex = 0;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = System.Drawing.Color.Transparent;
            tabPage2.Controls.Add(flpLopDaNhan);
            tabPage2.Location = new System.Drawing.Point(4, 5);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(3);
            tabPage2.Size = new System.Drawing.Size(942, 691);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Lớp đã nhận";
            // 
            // flpLopDaNhan
            // 
            flpLopDaNhan.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            flpLopDaNhan.AutoScroll = true;
            flpLopDaNhan.BackColor = System.Drawing.Color.White;
            flpLopDaNhan.Location = new System.Drawing.Point(30, 30);
            flpLopDaNhan.Name = "flpLopDaNhan";
            flpLopDaNhan.Size = new System.Drawing.Size(880, 630);
            flpLopDaNhan.TabIndex = 0;
            // 
            // FormMainGiaSu
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1200, 700);
            Controls.Add(panel1);
            Name = "FormMainGiaSu";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            WindowState = System.Windows.Forms.FormWindowState.Maximized;
            Load += FormMainGiaSu_Load;
            panel1.ResumeLayout(false);
            panelSidebar.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            panelFilter.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Button btnNavLopMoi;
        private System.Windows.Forms.Button btnNavLopDaNhan;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel flpTimLop;
        private System.Windows.Forms.FlowLayoutPanel flpLopDaNhan;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.ComboBox cbMonHoc;
        private System.Windows.Forms.ComboBox cbLop;
        private System.Windows.Forms.ComboBox cbKhuVuc;
        private System.Windows.Forms.ComboBox cbHinhThuc;
        private System.Windows.Forms.ComboBox cmbSapXepGia;
        private System.Windows.Forms.Button btnReset;
    }
}