using System;
using System.Drawing;
using System.Windows.Forms;
using DoAnGiaSu_WinForms.Business;
using DoAnGiaSu_WinForms.Models;

namespace DoAnGiaSu_WinForms.GUI
{
    public class FormDanhGia : Form
    {
        private int _maGS;
        private int _maPH;
        private int _maBaiDang;
        private ComboBox cmbSoSao;
        private TextBox txtNhanXet;
        private Button btnXacNhan;

        public FormDanhGia(int maGS, int maPH, int maBaiDang)
        {
            _maGS = maGS;
            _maPH = maPH;
            _maBaiDang = maBaiDang;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Đánh giá Gia sư";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblSao = new Label { Text = "Số sao (1-5):", Location = new Point(20, 20), AutoSize = true };
            cmbSoSao = new ComboBox { Location = new Point(120, 20), Size = new Size(100, 25), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbSoSao.Items.AddRange(new object[] { 1, 2, 3, 4, 5 });
            cmbSoSao.SelectedIndex = 4;

            Label lblNhanXet = new Label { Text = "Nhận xét chi tiết:", Location = new Point(20, 60), AutoSize = true };
            txtNhanXet = new TextBox { Location = new Point(20, 90), Size = new Size(340, 100), Multiline = true };

            btnXacNhan = new Button { Text = "Gửi đánh giá", Location = new Point(130, 210), Size = new Size(120, 35), BackColor = Color.DodgerBlue, ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            btnXacNhan.FlatAppearance.BorderSize = 0;
            btnXacNhan.Click += BtnXacNhan_Click;

            this.Controls.Add(lblSao);
            this.Controls.Add(cmbSoSao);
            this.Controls.Add(lblNhanXet);
            this.Controls.Add(txtNhanXet);
            this.Controls.Add(btnXacNhan);
        }

        private void BtnXacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                DanhGia danhGia = new DanhGia
                {
                    MaGS = _maGS,
                    MaPH = _maPH,
                    MaBaiDang = _maBaiDang,
                    SoSao = int.Parse(cmbSoSao.SelectedItem.ToString()),
                    NoiDung = txtNhanXet.Text.Trim()
                };

                DanhGiaService service = new DanhGiaService();
                if (service.ThemDanhGia(danhGia))
                {
                    MessageBox.Show("Đánh giá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đánh giá thất bại. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
