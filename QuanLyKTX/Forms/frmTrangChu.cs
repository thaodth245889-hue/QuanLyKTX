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
using System.Drawing.Drawing2D;
using System.Reflection;

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
            NangCapGiaoDienTrangChu();
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
        private void BatDoubleBuffer(Control ctrl)
        {
            PropertyInfo prop = typeof(Control).GetProperty(
                "DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            if (prop != null) prop.SetValue(ctrl, true, null);
        }

        private void NangCapGiaoDienTrangChu()
        {
            BatDoubleBuffer(this);
            this.BackColor = Color.FromArgb(241, 245, 249); // Đồng bộ nền xám sáng với frmMain

            // 1. Nâng cấp 4 thẻ thống kê KPI (bo góc + chuyển sắc + họa tiết tròn mờ)
            TrangTriTheKPI(lblTongPhong.Parent as Panel, lblTongPhong, lblIcon1,
                Color.FromArgb(37, 99, 235), Color.FromArgb(14, 165, 233));   // Xanh dương -> Cyan
            TrangTriTheKPI(lblTongSV.Parent as Panel, lblTongSV, lblIcon2,
                Color.FromArgb(16, 185, 129), Color.FromArgb(5, 150, 105));   // Xanh ngọc lục bảo
            TrangTriTheKPI(lblPhongTrong.Parent as Panel, lblPhongTrong, lblIcon3,
                Color.FromArgb(245, 158, 11), Color.FromArgb(234, 88, 12));   // Vàng cam san hô
            TrangTriTheKPI(lblPhongDay.Parent as Panel, lblPhongDay, lblIcon4,
                Color.FromArgb(244, 63, 94), Color.FromArgb(225, 29, 72));    // Đỏ hồng hiện đại

            // 2. Nâng cấp khung GroupBox & bảng DataGridView tình trạng phòng
            TrangTriBangTinhTrangPhong();

            // 3. Nâng cấp 2 biểu đồ (đưa Legend lên trên để không bị cắt mất ở cạnh dưới)
            TrangTriBieuDoDienNuoc();
            TrangTriBieuDoPhong();
        }

        private void TrangTriTheKPI(Panel card, Label lblGiaTri, Label lblIcon, Color mauDau, Color mauCuoi)
        {
            if (card == null) return;
            BatDoubleBuffer(card);

            // Bo góc khung thẻ và tự cập nhật khi đổi kích thước cửa sổ
            Action capNhatBoGoc = () =>
            {
                if (card.Width <= 0 || card.Height <= 0) return;
                using (GraphicsPath path = TaoBoGoc(new Rectangle(0, 0, card.Width, card.Height), 14))
                {
                    card.Region = new Region(path);
                }
            };
            capNhatBoGoc();
            card.SizeChanged += (s, e) => { capNhatBoGoc(); card.Invalidate(); };

            // Vẽ nền chuyển sắc + 2 vòng tròn trang trí mờ hiện đại
            card.Paint += (s, e) =>
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = card.ClientRectangle;
                if (rect.Width <= 0 || rect.Height <= 0) return;

                using (LinearGradientBrush bg = new LinearGradientBrush(rect, mauDau, mauCuoi, 35f))
                {
                    g.FillRectangle(bg, rect);
                }

                // Họa tiết vòng tròn kính mờ góc phải
                using (SolidBrush deco1 = new SolidBrush(Color.FromArgb(30, 255, 255, 255)))
                using (SolidBrush deco2 = new SolidBrush(Color.FromArgb(18, 255, 255, 255)))
                {
                    g.FillEllipse(deco1, rect.Width - 88, -28, 115, 115);
                    g.FillEllipse(deco2, rect.Width - 52, rect.Height - 46, 85, 85);
                }

                // Đĩa tròn mờ làm nổi bật Icon
                if (lblIcon != null)
                {
                    int badgeSize = 54;
                    int bx = lblIcon.Left + (lblIcon.Width - badgeSize) / 2;
                    int by = lblIcon.Top + (lblIcon.Height - badgeSize) / 2;
                    using (SolidBrush badge = new SolidBrush(Color.FromArgb(42, 255, 255, 255)))
                    {
                        g.FillEllipse(badge, bx, by, badgeSize, badgeSize);
                    }
                }
            };

            // Chỉnh chữ bên trong thẻ
            foreach (Control c in card.Controls)
            {
                if (c is Label lbl)
                {
                    lbl.BackColor = Color.Transparent;
                    lbl.ForeColor = Color.White;

                    if (lbl == lblGiaTri)
                    {
                        lbl.Font = new Font("Segoe UI", 26f, FontStyle.Bold);
                    }
                    else if (lbl != lblIcon)
                    {
                        lbl.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                        lbl.ForeColor = Color.FromArgb(240, 249, 255);
                    }
                }
            }

            if (lblIcon != null)
            {
                lblIcon.Left = card.Width - lblIcon.Width - 18;
                lblIcon.Top = (card.Height - lblIcon.Height) / 2 + 4;
                lblIcon.Anchor = AnchorStyles.Right;
            }
        }

        private void TrangTriBangTinhTrangPhong()
        {
            BatDoubleBuffer(dgvThongKePhong);

            // Biến GroupBox bao ngoài thành thẻ trắng bo góc có thanh nhấn xanh
            if (dgvThongKePhong.Parent is GroupBox grp)
            {
                BatDoubleBuffer(grp);
                string tieuDe = string.IsNullOrWhiteSpace(grp.Text) ? "Danh sách tình trạng phòng" : grp.Text;

                grp.Paint += (s, e) =>
                {
                    Graphics g = e.Graphics;
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.Clear(this.BackColor);

                    Rectangle cardRect = new Rectangle(1, 1, grp.Width - 3, grp.Height - 3);
                    using (GraphicsPath path = TaoBoGoc(cardRect, 12))
                    using (SolidBrush whiteBg = new SolidBrush(Color.White))
                    using (Pen borderPen = new Pen(Color.FromArgb(226, 232, 240), 1.2f))
                    {
                        g.FillPath(whiteBg, path);
                        g.DrawPath(borderPen, path);
                    }

                    // Vạch nhấn dọc màu xanh + tiêu đề bảng sắc nét
                    Rectangle bar = new Rectangle(14, 13, 4, 18);
                    using (GraphicsPath barPath = TaoBoGoc(bar, 2))
                    using (SolidBrush barBrush = new SolidBrush(Color.FromArgb(37, 99, 235)))
                    {
                        g.FillPath(barBrush, barPath);
                    }
                    using (Font titleFont = new Font("Segoe UI", 10.5f, FontStyle.Bold))
                    {
                        TextRenderer.DrawText(g, tieuDe, titleFont, new Point(24, 12), Color.FromArgb(15, 23, 42));
                    }
                };

                dgvThongKePhong.Location = new Point(14, 42);
                dgvThongKePhong.Size = new Size(grp.ClientSize.Width - 28, grp.ClientSize.Height - 54);
                dgvThongKePhong.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            }

            // Định dạng DataGridView phẳng hiện đại
            dgvThongKePhong.BorderStyle = BorderStyle.None;
            dgvThongKePhong.BackgroundColor = Color.White;
            dgvThongKePhong.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvThongKePhong.GridColor = Color.FromArgb(241, 245, 249);
            dgvThongKePhong.RowHeadersVisible = false;
            dgvThongKePhong.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThongKePhong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvThongKePhong.AllowUserToResizeRows = false;

            dgvThongKePhong.EnableHeadersVisualStyles = false;
            dgvThongKePhong.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvThongKePhong.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvThongKePhong.ColumnHeadersHeight = 36;
            dgvThongKePhong.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Padding = new Padding(8, 0, 8, 0),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };

            dgvThongKePhong.RowTemplate.Height = 32;
            dgvThongKePhong.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(30, 41, 59),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                SelectionBackColor = Color.FromArgb(239, 246, 255),
                SelectionForeColor = Color.FromArgb(29, 78, 216),
                Padding = new Padding(8, 0, 8, 0)
            };
            dgvThongKePhong.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            // Căn giữa các cột số và trạng thái
            string[] cotCanGiua = { "Loại", "Sức chứa", "Đang ở", "Chỗ trống", "Trạng thái" };
            foreach (string colName in cotCanGiua)
            {
                if (dgvThongKePhong.Columns.Contains(colName))
                {
                    dgvThongKePhong.Columns[colName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgvThongKePhong.Columns[colName].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
            }

            // Tô màu nổi bật cột Trạng thái (Còn chỗ = xanh lá, Đã đầy = đỏ)
            dgvThongKePhong.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.Value == null) return;
                string colName = dgvThongKePhong.Columns[e.ColumnIndex].Name;
                if (colName == "Trạng thái")
                {
                    string val = e.Value.ToString();
                    e.CellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                    if (val.Contains("Đã đầy"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                        e.CellStyle.BackColor = Color.FromArgb(254, 242, 242);
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(5, 150, 105);
                        e.CellStyle.BackColor = Color.FromArgb(236, 253, 245);
                    }
                }
            };

            dgvThongKePhong.ClearSelection();
        }

        private void TrangTriBieuDoDienNuoc()
        {
            chartDienNuoc.BackColor = Color.White;
            chartDienNuoc.BorderlineColor = Color.FromArgb(226, 232, 240);
            chartDienNuoc.BorderlineDashStyle = ChartDashStyle.Solid;
            chartDienNuoc.BorderlineWidth = 1;

            if (chartDienNuoc.Titles.Count > 0)
            {
                chartDienNuoc.Titles[0].Font = new Font("Segoe UI", 10f, FontStyle.Bold);
                chartDienNuoc.Titles[0].ForeColor = Color.FromArgb(15, 23, 42);
            }

            // Đưa Legend lên trên để luôn hiển thị rõ, không bị cắt ở đáy
            if (chartDienNuoc.Legends.Count > 0)
            {
                Legend lg = chartDienNuoc.Legends[0];
                lg.Docking = Docking.Top;
                lg.Alignment = StringAlignment.Center;
                lg.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
                lg.ForeColor = Color.FromArgb(71, 85, 105);
            }

            if (chartDienNuoc.ChartAreas.Count > 0)
            {
                ChartArea area = chartDienNuoc.ChartAreas[0];
                area.BackColor = Color.White;
                area.AxisX.LineColor = Color.FromArgb(203, 213, 225);
                area.AxisY.LineColor = Color.Transparent;
                area.AxisY.MajorTickMark.Enabled = false;
                area.AxisY.MajorGrid.LineColor = Color.FromArgb(241, 245, 249);
                area.AxisX.LabelStyle.ForeColor = Color.FromArgb(71, 85, 105);
                area.AxisY.LabelStyle.ForeColor = Color.FromArgb(100, 116, 139);
                area.AxisX.LabelStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8.5f);
                area.AxisX.TitleForeColor = Color.FromArgb(100, 116, 139);
                area.AxisY.TitleForeColor = Color.FromArgb(100, 116, 139);
            }

            foreach (Series s in chartDienNuoc.Series)
            {
                s["PointWidth"] = "0.55";
                s.LabelForeColor = Color.FromArgb(30, 41, 59);
                if (s.Name.Contains("Điện")) s.Color = Color.FromArgb(245, 158, 11);
                if (s.Name.Contains("Nước")) s.Color = Color.FromArgb(14, 165, 233);
            }
        }

        private void TrangTriBieuDoPhong()
        {
            chartPhong.BackColor = Color.White;
            chartPhong.BorderlineColor = Color.FromArgb(226, 232, 240);
            chartPhong.BorderlineDashStyle = ChartDashStyle.Solid;
            chartPhong.BorderlineWidth = 1;

            if (chartPhong.Titles.Count > 0)
            {
                chartPhong.Titles[0].Font = new Font("Segoe UI", 10f, FontStyle.Bold);
                chartPhong.Titles[0].ForeColor = Color.FromArgb(15, 23, 42);
            }

            // Đưa Legend lên trên để thấy ngay màu nào là Đang ở / Phòng trống / Đã đầy
            if (chartPhong.Legends.Count > 0)
            {
                Legend lg = chartPhong.Legends[0];
                lg.Docking = Docking.Top;
                lg.Alignment = StringAlignment.Center;
                lg.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
                lg.ForeColor = Color.FromArgb(71, 85, 105);
            }

            if (chartPhong.Series.Count > 0)
            {
                Series s = chartPhong.Series[0];
                s["DoughnutRadius"] = "55";
                s.BorderColor = Color.White;
                s.BorderWidth = 2;
                s.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
            }
        }

        private GraphicsPath TaoBoGoc(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }

}
