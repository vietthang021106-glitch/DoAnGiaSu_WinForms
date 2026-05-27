using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using DoAnGiaSu_WinForms.DAL;

namespace DoAnGiaSu_WinForms.GUI
{
    public class FormChiTietDanhGia : Form
    {
        private readonly int _maGS;

        private Label lblTieuDe;
        private FlowLayoutPanel flpDanhGia;

        public FormChiTietDanhGia(int maGS)
        {
            _maGS = maGS;
            InitializeUi();
            Load += FormChiTietDanhGia_Load;
        }

        private void InitializeUi()
        {
            Text = "Đánh giá chi tiết";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ClientSize = new Size(500, 400);
            BackColor = Color.White;

            lblTieuDe = new Label
            {
                Dock = DockStyle.Top,
                Height = 50,
                Font = new Font("Segoe UI", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(24, 33, 53),
                Text = "Đánh giá chi tiết của gia sư...",
                TextAlign = ContentAlignment.MiddleCenter
            };

            flpDanhGia = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BackColor = Color.White,
                Padding = new Padding(12)
            };

            Controls.Add(flpDanhGia);
            Controls.Add(lblTieuDe);
        }

        private void FormChiTietDanhGia_Load(object? sender, EventArgs e)
        {
            try
            {
                flpDanhGia.Controls.Clear();

                const string sql = @"SELECT ph.HoTen, dg.SoSao, dg.NoiDung, dg.NgayDanhGia
                                     FROM DANHGIA dg
                                     JOIN PHUHUYNH ph ON dg.MaPH = ph.MaPH
                                     WHERE dg.MaGS = @MaGS
                                     ORDER BY dg.NgayDanhGia DESC";

                using SqlConnection conn = new DBConnection().GetConnection();
                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@MaGS", SqlDbType.Int) { Value = _maGS });
                conn.Open();

                using SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string hoTen = reader["HoTen"]?.ToString() ?? "";
                    int soSao = reader["SoSao"] != DBNull.Value ? Convert.ToInt32(reader["SoSao"]) : 0;
                    string noiDung = reader["NoiDung"]?.ToString() ?? "";
                    string ngayDanhGia = reader["NgayDanhGia"] != DBNull.Value
                        ? Convert.ToDateTime(reader["NgayDanhGia"]).ToString("dd/MM/yyyy")
                        : "";

                    Panel pnlItem = new Panel
                    {
                        Width = 445,
                        AutoSize = true,
                        AutoSizeMode = AutoSizeMode.GrowAndShrink,
                        BackColor = Color.White,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(0, 0, 0, 10),
                        Padding = new Padding(10, 8, 10, 8)
                    };

                    Label lblTenPH = new Label
                    {
                        AutoSize = true,
                        MaximumSize = new Size(390, 0),
                        Font = new Font("Segoe UI", 10.5F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(24, 33, 53),
                        Text = hoTen,
                        Location = new Point(10, 8)
                    };

                    Label lblSao = new Label
                    {
                        AutoSize = true,
                        MaximumSize = new Size(390, 0),
                        Font = new Font("Segoe UI", 10F),
                        ForeColor = Color.Goldenrod,
                        Text = new string('⭐', Math.Max(0, Math.Min(5, soSao))),
                        Location = new Point(10, 34)
                    };

                    Label lblNgay = new Label
                    {
                        AutoSize = true,
                        Font = new Font("Segoe UI", 8.5F),
                        ForeColor = Color.Gray,
                        Text = ngayDanhGia,
                        Anchor = AnchorStyles.Top | AnchorStyles.Right
                    };
                    lblNgay.Location = new Point(pnlItem.Width - lblNgay.PreferredWidth - 20, 10);

                    Label lblNoiDung = new Label
                    {
                        AutoSize = true,
                        MaximumSize = new Size(390, 0),
                        Font = new Font("Segoe UI", 9.5F),
                        ForeColor = Color.Black,
                        Text = noiDung,
                        Location = new Point(10, 60)
                    };

                    pnlItem.Controls.Add(lblTenPH);
                    pnlItem.Controls.Add(lblSao);
                    pnlItem.Controls.Add(lblNgay);
                    pnlItem.Controls.Add(lblNoiDung);

                    pnlItem.Height = lblNoiDung.Bottom + 12;
                    flpDanhGia.Controls.Add(pnlItem);
                }

                if (flpDanhGia.Controls.Count == 0)
                {
                    Label lblEmpty = new Label
                    {
                        AutoSize = true,
                        Font = new Font("Segoe UI", 10F, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        Text = "Chưa có đánh giá nào.",
                        Margin = new Padding(8)
                    };
                    flpDanhGia.Controls.Add(lblEmpty);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải đánh giá: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
