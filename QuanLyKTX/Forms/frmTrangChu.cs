using QuanLyKTX.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyKTX.Forms
{
    public partial class frmTrangChu : Form
    {
        public frmTrangChu()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void frmTrangChu_Load(object sender, EventArgs e)
        {
            lblIcon1.Text = "\uE80F"; // Ngôi nhà (Tổng số phòng)
            lblIcon2.Text = "\uE716"; // Nhóm người (Sinh viên đang ở)
            lblIcon3.Text = "\uE930"; // Dấu tích tròn (Phòng còn chỗ)
            lblIcon4.Text = "\uE8F8"; // Chặn / Đã đầy (Phòng đã đầy)
            LoadThongKe();
            LoadBieuDoDienNuoc();
        }
        private void LoadThongKe()
        {
            try
            {
                string sql = @"
                    SELECT p.MaPhong AS [Mã phòng],
                           p.TenPhong AS [Tên phòng],
                           k.TenKhu AS [Khu],
                           p.LoaiPhong AS [Loại],
                           p.SucChua AS [Sức chứa],
                           COUNT(dk.MaDangKy) AS [Đang ở],
                           (p.SucChua - COUNT(dk.MaDangKy)) AS [Chỗ trống],
                           CASE WHEN COUNT(dk.MaDangKy) >= p.SucChua THEN N'Đã đầy' ELSE N'Còn chỗ' END AS [Trạng thái]
                    FROM Phong p
                    INNER JOIN Khu k ON p.MaKhu = k.MaKhu
                    LEFT JOIN DangKyPhong dk ON p.MaPhong = dk.MaPhong AND dk.TrangThai = N'Đang ở'
                    GROUP BY p.MaPhong, p.TenPhong, k.TenKhu, p.LoaiPhong, p.SucChua
                    ORDER BY p.MaPhong";

                DataTable dt = DatabaseHelper.GetDataTable(sql);
                dgvThongKePhong.DataSource = dt;

                int tongPhong = dt.Rows.Count;
                int tongSV = 0;
                int phongTrong = 0;      // 0 SV đang ở
                int phongDangOConCho = 0; // Có SV ở nhưng chưa đầy
                int phongDay = 0;        // Đủ 4/4 hoặc 6/6

                foreach (DataRow row in dt.Rows)
                {
                    int dangO = Convert.ToInt32(row["Đang ở"]);
                    int choTrong = Convert.ToInt32(row["Chỗ trống"]);
                    tongSV += dangO;

                    if (choTrong <= 0)
                        phongDay++;
                    else if (dangO == 0)
                        phongTrong++;
                    else
                        phongDangOConCho++;
                }

                lblTongPhong.Text = tongPhong.ToString();
                lblTongSV.Text = tongSV.ToString();
                lblPhongTrong.Text = (tongPhong - phongDay).ToString();
                lblPhongDay.Text = phongDay.ToString();

                // Vẽ biểu đồ số phòng ngay từ kết quả thống kê trên
                LoadBieuDoPhong(phongTrong, phongDangOConCho, phongDay);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải thống kê: " + ex.Message, "Lỗi");
            }
        }
        private void LoadBieuDoDienNuoc()
        {
            try
            {
                string sql = @"
                    SELECT MaPhong,
                           SUM(ChiSoDienMoi - ChiSoDienCu) AS TongDien,
                           SUM(ChiSoNuocMoi - ChiSoNuocCu) AS TongNuoc
                    FROM TienPhong
                    GROUP BY MaPhong
                    ORDER BY MaPhong";

                DataTable dt = DatabaseHelper.GetDataTable(sql);

                chartDienNuoc.Series.Clear();
                chartDienNuoc.Titles.Clear();
                chartDienNuoc.Legends.Clear();

                Title title = chartDienNuoc.Titles.Add("THỐNG KÊ TIÊU THỤ ĐIỆN (kWh) & NƯỚC (m³) THEO PHÒNG");
                title.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                title.ForeColor = Color.FromArgb(26, 48, 80);

                Legend legend = new Legend("Legend1") { Docking = Docking.Bottom };
                chartDienNuoc.Legends.Add(legend);

                ChartArea area = chartDienNuoc.ChartAreas[0];
                area.AxisX.MajorGrid.Enabled = false;
                area.AxisY.MajorGrid.LineColor = Color.LightGray;
                area.AxisX.Title = "Phòng";
                area.AxisY.Title = "Chỉ số tiêu thụ";

                Series sDien = new Series("Điện (kWh)")
                {
                    ChartType = SeriesChartType.Column,
                    Color = Color.FromArgb(243, 156, 18), // Màu cam
                    IsValueShownAsLabel = true,
                    Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
                };

                Series sNuoc = new Series("Nước (m³)")
                {
                    ChartType = SeriesChartType.Column,
                    Color = Color.FromArgb(33, 150, 243), // Màu xanh dương
                    IsValueShownAsLabel = true,
                    Font = new Font("Segoe UI", 8.5f, FontStyle.Bold)
                };

                if (dt.Rows.Count == 0)
                {
                    // Hiển thị nhắc nhẹ nếu chưa lập hóa đơn nào
                    title.Text = "TIÊU THỤ ĐIỆN & NƯỚC (Chưa có hóa đơn)";
                }
                else
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string maPhong = row["MaPhong"].ToString();
                        int dien = Convert.ToInt32(row["TongDien"]);
                        int nuoc = Convert.ToInt32(row["TongNuoc"]);

                        sDien.Points.AddXY(maPhong, dien);
                        sNuoc.Points.AddXY(maPhong, nuoc);
                    }
                }

                chartDienNuoc.Series.Add(sDien);
                chartDienNuoc.Series.Add(sNuoc);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi vẽ biểu đồ điện nước: " + ex.Message, "Lỗi");
            }
        }

        // Biểu đồ 2: Biểu đồ tròn (Doughnut) thống kê tình trạng số phòng
        private void LoadBieuDoPhong(int phongTrong, int phongDangOConCho, int phongDay)
        {
            chartPhong.Series.Clear();
            chartPhong.Titles.Clear();
            chartPhong.Legends.Clear();

            Title title = chartPhong.Titles.Add("TỶ LỆ TÌNH TRẠNG SỐ PHÒNG");
            title.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            title.ForeColor = Color.FromArgb(26, 48, 80);

            Legend legend = new Legend("Legend1") { Docking = Docking.Bottom };
            chartPhong.Legends.Add(legend);

            Series sPhong = new Series("Tình trạng phòng")
            {
                ChartType = SeriesChartType.Doughnut,
                IsValueShownAsLabel = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                LabelForeColor = Color.White
            };

            if (phongDangOConCho > 0)
            {
                int idx = sPhong.Points.AddXY("Đang ở (còn chỗ)", phongDangOConCho);
                sPhong.Points[idx].Color = Color.FromArgb(40, 167, 69); // Xanh lá
                sPhong.Points[idx].Label = $"{phongDangOConCho} phòng";
            }

            if (phongTrong > 0)
            {
                int idx = sPhong.Points.AddXY("Phòng trống", phongTrong);
                sPhong.Points[idx].Color = Color.FromArgb(33, 150, 243); // Xanh dương
                sPhong.Points[idx].Label = $"{phongTrong} phòng";
            }

            if (phongDay > 0)
            {
                int idx = sPhong.Points.AddXY("Phòng đã đầy", phongDay);
                sPhong.Points[idx].Color = Color.FromArgb(220, 53, 69); // Đỏ
                sPhong.Points[idx].Label = $"{phongDay} phòng";
            }

            chartPhong.Series.Add(sPhong);
        }
    }
}
