using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using DoAnGiaSu_WinForms.DAL;

namespace DoAnGiaSu_WinForms.GUI
{
    public class frmDanhGia : Form
    {
        private readonly int _maPH;
        private readonly int _maGS;
        private readonly int _maBaiDang;

        private Label lblTieuDe;
        private Label lblSoSao;
        private Label lblNoiDung;
        private NumericUpDown numSao;
        private RichTextBox rtbNoiDung;
        private Button btnLuuDanhGia;

        public frmDanhGia(int maPH, int maGS, int maBaiDang)
        {
            _maPH = maPH;
            _maGS = maGS;
            _maBaiDang = maBaiDang;

            InitializeUi();
        }

        private void InitializeUi()
        {
            Text = "Đánh Giá";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new System.Drawing.Size(500, 390);

            lblTieuDe = new Label
            {
                Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(24, 18),
                Size = new System.Drawing.Size(452, 36),
                Text = "Đánh Giá Gia Sư",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            lblSoSao = new Label
            {
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(24, 72),
                Text = "Số sao"
            };

            numSao = new NumericUpDown
            {
                Location = new System.Drawing.Point(24, 97),
                Size = new System.Drawing.Size(150, 27),
                Minimum = 1,
                Maximum = 5,
                Value = 5
            };

            lblNoiDung = new Label
            {
                AutoSize = true,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                Location = new System.Drawing.Point(24, 143),
                Text = "Nhận xét"
            };

            rtbNoiDung = new RichTextBox
            {
                Location = new System.Drawing.Point(24, 169),
                Size = new System.Drawing.Size(452, 142)
            };

            btnLuuDanhGia = new Button
            {
                BackColor = System.Drawing.Color.FromArgb(24, 119, 242),
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(162, 329),
                Size = new System.Drawing.Size(177, 42),
                Text = "Gửi Đánh Giá",
                UseVisualStyleBackColor = false
            };
            btnLuuDanhGia.FlatAppearance.BorderSize = 0;
            btnLuuDanhGia.Click += btnLuuDanhGia_Click;

            Controls.Add(lblTieuDe);
            Controls.Add(lblSoSao);
            Controls.Add(numSao);
            Controls.Add(lblNoiDung);
            Controls.Add(rtbNoiDung);
            Controls.Add(btnLuuDanhGia);
        }

        private void btnLuuDanhGia_Click(object? sender, EventArgs e)
        {
            try
            {
                const string sql = @"INSERT INTO DANHGIA (MaPH, MaGS, MaBaiDang, SoSao, NoiDung, NgayDanhGia)
                                     VALUES (@MaPH, @MaGS, @MaBaiDang, @SoSao, @NoiDung, GETDATE())";

                using SqlConnection conn = new DBConnection().GetConnection();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@MaPH", SqlDbType.Int) { Value = _maPH });
                cmd.Parameters.Add(new SqlParameter("@MaGS", SqlDbType.Int) { Value = _maGS });
                cmd.Parameters.Add(new SqlParameter("@MaBaiDang", SqlDbType.Int) { Value = _maBaiDang });
                cmd.Parameters.Add(new SqlParameter("@SoSao", SqlDbType.Int) { Value = Convert.ToInt32(numSao.Value) });
                cmd.Parameters.Add(new SqlParameter("@NoiDung", SqlDbType.NVarChar, -1) { Value = rtbNoiDung.Text.Trim() });

                conn.Open();
                int soDong = cmd.ExecuteNonQuery();
                if (soDong > 0)
                {
                    MessageBox.Show("Gửi đánh giá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Không thể lưu đánh giá.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu đánh giá: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
