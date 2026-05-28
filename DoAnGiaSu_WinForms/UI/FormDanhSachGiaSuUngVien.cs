using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DoAnGiaSu_WinForms.Business;
using DoAnGiaSu_WinForms.DataAccess;

namespace DoAnGiaSu_WinForms.GUI
{
    public partial class FormDanhSachGiaSuUngVien : Form
    {
        private int _maBaiDangHienTai;
        private Panel _pnlBody;
        public int MaGiaSuChon { get; private set; }

        public FormDanhSachGiaSuUngVien(int maBaiDang)
        {
            InitializeComponent();
            _maBaiDangHienTai = maBaiDang;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadDataGiaSuUngVien();
        }

        private void LoadDataGiaSuUngVien()
        {
            try
            {
                BaiDangService baiDangService = new BaiDangService();
                DataTable dsDangKy = baiDangService.LayThongTinGiaSuDangKy(_maBaiDangHienTai);

                DataTable dsChoDuyet = dsDangKy.Clone();
                foreach (DataRow row in dsDangKy.Rows)
                {
                    if ((row["TrangThaiDangKy"]?.ToString() ?? "") == "ChoDuyet")
                        dsChoDuyet.ImportRow(row);
                }

                if (dsChoDuyet.Rows.Count == 0)
                {
                    MessageBox.Show("Không còn gia sư nào ở trạng thái chờ duyệt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.Cancel;
                    Close();
                    return;
                }

                BuildCardUI(dsChoDuyet);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuildCardUI(DataTable dsChoDuyet)
        {
            FlowLayoutPanel flpGiaSu = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                AutoScroll = true,
                Padding = new Padding(15),
                BackColor = Color.White
            };

            foreach (DataRow row in dsChoDuyet.Rows)
            {
                int maGS = (int)row["MaGS"];
                string hoTen = row["HoTen"]?.ToString() ?? "";
                string tenGioiTinh = row["TenGioiTinh"]?.ToString() ?? "";
                string namSinh = row["NamSinh"]?.ToString() ?? "";
                string tenTrinhDo = row["TenTrinhDo"]?.ToString() ?? "";
                string tenTruong = row["TenTruong"]?.ToString() ?? "";
                string tenNamHoc = row["TenNamHoc"]?.ToString() ?? "";
                string tenChungChi = row["TenChungChi"]?.ToString() ?? "";
                string diemChungChi = row["DiemChungChi"]?.ToString() ?? "";
                string thanhTich = row["ThanhTich"]?.ToString() ?? "";
                string anhMinhChung = row["AnhMinhChung"]?.ToString() ?? "";
                double diemTB = row.Table.Columns.Contains("DiemTB") && row["DiemTB"] != DBNull.Value ? Convert.ToDouble(row["DiemTB"]) : 0;
                int luotDanhGia = row.Table.Columns.Contains("LuotDanhGia") && row["LuotDanhGia"] != DBNull.Value ? Convert.ToInt32(row["LuotDanhGia"]) : 0;

                ucCardGiaSu card = new ucCardGiaSu();
                card.AutoSize = false;
                card.Size = new Size(520, 300);
                card.Margin = new Padding(10);
                
                card.LoadData(maGS, hoTen, tenGioiTinh, namSinh, tenTrinhDo, tenTruong,
                              tenNamHoc, tenChungChi, diemChungChi, thanhTich, anhMinhChung, diemTB, luotDanhGia);
                card.Tag = maGS;

                card.XemDanhGiaClicked += Card_XemDanhGiaClicked;
                card.ChonGiaSuClicked += Card_ChonGiaSuClicked;

                flpGiaSu.Controls.Add(card);
            }

            _pnlBody.Controls.Clear();
            _pnlBody.Controls.Add(flpGiaSu);
        }

        private void Card_XemDanhGiaClicked(object sender, EventArgs e)
        {
            if (sender is not ucCardGiaSu card) return;
            int maGS = (int)card.Tag;
            if (maGS <= 0) return;

            using Form frmChiTiet = new FormChiTietDanhGia(maGS);
            frmChiTiet.ShowDialog(this);
        }

        private void Card_ChonGiaSuClicked(object sender, EventArgs e)
        {
            if (sender is not ucCardGiaSu card) return;
            int maGS = (int)card.Tag;

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn chọn gia sư này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            if (_maBaiDangHienTai <= 0)
            {
                MessageBox.Show("Không xác định được bài đăng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                BaiDangService baiDangService = new BaiDangService();
                if (baiDangService.PhuHuynhDuyetGiaSu(_maBaiDangHienTai, maGS))
                {
                    MessageBox.Show("Chọn gia sư thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MaGiaSuChon = maGS;
                    DialogResult = DialogResult.OK;
                    Close();
                    return;
                }

                MessageBox.Show($"Không thể cập nhật dữ liệu. Vui lòng thử lại.\n(MaBaiDang={_maBaiDangHienTai}, MaGS={maGS})", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            Text = "Chọn gia sư để duyệt";
            Size = new Size(1400, 900);
            WindowState = FormWindowState.Maximized;
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;

            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 45,
                BackColor = Color.FromArgb(24, 119, 242)
            };

            Label lblHeader = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Danh sách gia sư đăng ký (chờ duyệt)"
            };
            pnlHeader.Controls.Add(lblHeader);

            _pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                BackColor = Color.White
            };

            Controls.Add(_pnlBody);
            Controls.Add(pnlHeader);
        }
    }
}
