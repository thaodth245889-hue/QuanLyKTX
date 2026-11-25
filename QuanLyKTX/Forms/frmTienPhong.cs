using QuanLyKTX.DAL;
using QuanLyKTX.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKTX.Forms
{
    public partial class frmTienPhong : Form
    {
        private bool isAdding = false;
        private bool isLoadingCombo = false;
        private decimal donGiaPhongHienTai = 700000m;
        public frmTienPhong()
        {
            InitializeComponent();
        }

        private void frmTienPhong_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            LoadData();
            SetControlState(false);
            UIHelper.LamDepFormQuanLy(this);
        }
        private void LoadComboBoxes()
        {
            isLoadingCombo = true;
            DataTable dtPhong = DatabaseHelper.GetDataTable(
                "SELECT MaPhong, (MaPhong + ' - ' + TenPhong) AS HienThi FROM Phong ORDER BY MaPhong");
            cboPhongTP.DataSource = dtPhong;
            cboPhongTP.DisplayMember = "HienThi";
            cboPhongTP.ValueMember = "MaPhong";
            isLoadingCombo = false;

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] { "Chưa đóng", "Đã đóng" });
            cboTrangThai.SelectedIndex = 0;
        }

        private void LoadData(string locThangNam = "")
        {
            string sql = @"
                SELECT MaTien AS [Mã HĐ],
                       MaPhong AS [Mã phòng],
                       ThangNam AS [Tháng/Năm],
                       TuNgay AS [Từ ngày],
                       DenNgay AS [Đến ngày],
                       SoSV AS [Số SV],
                       DonGiaPhong AS [Đơn giá],
                       TienPhong AS [Tiền phòng],
                       ChiSoDienCu AS [Điện cũ],
                       ChiSoDienMoi AS [Điện mới],
                       TienDien AS [Tiền điện],
                       ChiSoNuocCu AS [Nước cũ],
                       ChiSoNuocMoi AS [Nước mới],
                       TienNuoc AS [Tiền nước],
                       TongTien AS [Tổng tiền],
                       TrangThai AS [Trạng thái]
                FROM TienPhong";

            SqlParameter[] prms = null;
            if (!string.IsNullOrWhiteSpace(locThangNam))
            {
                sql += " WHERE ThangNam LIKE @tn";
                prms = new[] { new SqlParameter("@tn", "%" + locThangNam.Trim() + "%") };
            }

            sql += " ORDER BY MaTien DESC";
            dgvTienPhong.DataSource = DatabaseHelper.GetDataTable(sql, prms);

            // Ẩn bớt các cột phụ trên lưới để bảng gọn giống hình mẫu, vẫn giữ dữ liệu để đổ lên Form khi CellClick
            string[] cotAn = { "Từ ngày", "Đến ngày", "Đơn giá", "Điện cũ", "Điện mới", "Nước cũ", "Nước mới" };
            foreach (string col in cotAn)
            {
                if (dgvTienPhong.Columns[col] != null)
                    dgvTienPhong.Columns[col].Visible = false;
            }

            string[] cotTien = { "Tiền phòng", "Tiền điện", "Tiền nước", "Tổng tiền" };
            foreach (string col in cotTien)
            {
                if (dgvTienPhong.Columns[col] != null)
                    dgvTienPhong.Columns[col].DefaultCellStyle.Format = "N0";
            }
        }

        private void SetControlState(bool isEditing)
        {
            cboPhongTP.Enabled = isEditing && isAdding;
            txtThangNam.Enabled = isEditing;
            dtpTuNgay.Enabled = isEditing;
            dtpDenNgay.Enabled = isEditing;
            txtDienCu.Enabled = isEditing;
            txtDienMoi.Enabled = isEditing;
            txtNuocCu.Enabled = isEditing;
            txtNuocMoi.Enabled = isEditing;
            cboTrangThai.Enabled = isEditing;

            btnThem.Enabled = !isEditing;
            btnSua.Enabled = !isEditing;
            btnXoa.Enabled = !isEditing;
            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;
            txtLocThangNam.Enabled = !isEditing;
            btnLoc.Enabled = !isEditing;
            dgvTienPhong.Enabled = !isEditing;
            btnXuatHoaDon.Enabled = !isEditing;
        }

        // Tính tiền điện sinh hoạt bậc thang Nhà nước (EVN)
        private decimal TinhTienDienNhaNuoc(int soKWh)
        {
            if (soKWh <= 0) return 0;
            decimal tong = 0;
            int[] bac = { 50, 50, 100, 100, 100 };
            decimal[] gia = { 1984m, 2050m, 2380m, 2998m, 3350m, 3460m };
            int conLai = soKWh;

            for (int i = 0; i < bac.Length && conLai > 0; i++)
            {
                int muc = Math.Min(conLai, bac[i]);
                tong += muc * gia[i];
                conLai -= muc;
            }
            if (conLai > 0) tong += conLai * gia[5];
            return Math.Round(tong, 0);
        }

        // Tính tiền nước sạch sinh hoạt bậc thang Nhà nước
        private decimal TinhTienNuocNhaNuoc(int soM3)
        {
            if (soM3 <= 0) return 0;
            decimal tong = 0;
            int[] bac = { 10, 10, 10 };
            decimal[] gia = { 6000m, 7100m, 8600m, 11000m };
            int conLai = soM3;

            for (int i = 0; i < bac.Length && conLai > 0; i++)
            {
                int muc = Math.Min(conLai, bac[i]);
                tong += muc * gia[i];
                conLai -= muc;
            }
            if (conLai > 0) tong += conLai * gia[3];
            return Math.Round(tong, 0);
        }

        // Cập nhật giá khoán phòng 700.000đ/tháng + đếm số SV đang ở
        private void CapNhatThongTinPhongVaTien()
        {
            if (isLoadingCombo || cboPhongTP.SelectedValue == null) return;
            string maPhong = cboPhongTP.SelectedValue.ToString();

            DataTable dtP = DatabaseHelper.GetDataTable("SELECT GiaPhong FROM Phong WHERE MaPhong = @p",
                new[] { new SqlParameter("@p", maPhong) });
            donGiaPhongHienTai = dtP.Rows.Count > 0 ? Convert.ToDecimal(dtP.Rows[0][0]) : 700000m;

            int soSV = Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM DangKyPhong WHERE MaPhong = @p AND TrangThai = N'Đang ở'",
                new[] { new SqlParameter("@p", maPhong) }));

            txtSoSV.Text = soSV.ToString();
            txtTienPhong.Text = donGiaPhongHienTai.ToString("N0");
            CalcTongHoaDon();
        }

        private void CalcTongHoaDon()
        {
            int.TryParse(txtDienCu.Text.Trim(), out int dienCu);
            int.TryParse(txtDienMoi.Text.Trim(), out int dienMoi);
            int.TryParse(txtNuocCu.Text.Trim(), out int nuocCu);
            int.TryParse(txtNuocMoi.Text.Trim(), out int nuocMoi);

            decimal tienDien = TinhTienDienNhaNuoc(dienMoi - dienCu);
            decimal tienNuoc = TinhTienNuocNhaNuoc(nuocMoi - nuocCu);
            decimal tongTien = donGiaPhongHienTai + tienDien + tienNuoc;

            txtTienDien.Text = tienDien.ToString("N0");
            txtTienNuoc.Text = tienNuoc.ToString("N0");
            txtTongTien.Text = tongTien.ToString("N0");
        }

        private void cboPhongTP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isAdding)
                CapNhatThongTinPhongVaTien();
        }
        private void txtThangNam_Leave(object sender, EventArgs e)
        {
            if (DateTime.TryParseExact("01/" + txtThangNam.Text.Trim(), "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dauThang))
            {
                dtpTuNgay.Value = dauThang;
                dtpDenNgay.Value = dauThang.AddMonths(1).AddDays(-1);
            }
        }
        private void txtDienCu_TextChanged(object sender, EventArgs e) => CalcTongHoaDon();
        private void txtDienMoi_TextChanged(object sender, EventArgs e) => CalcTongHoaDon();
        private void txtNuocCu_TextChanged(object sender, EventArgs e) => CalcTongHoaDon();
        private void txtNuocMoi_TextChanged(object sender, EventArgs e) => CalcTongHoaDon();

        private void dgvTienPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvTienPhong.Rows[e.RowIndex];

            txtMaTien.Text = row.Cells["Mã HĐ"].Value?.ToString();
            isLoadingCombo = true;
            cboPhongTP.SelectedValue = row.Cells["Mã phòng"].Value?.ToString();
            isLoadingCombo = false;

            txtThangNam.Text = row.Cells["Tháng/Năm"].Value?.ToString();
            if (row.Cells["Từ ngày"].Value != DBNull.Value)
                dtpTuNgay.Value = Convert.ToDateTime(row.Cells["Từ ngày"].Value);
            if (row.Cells["Đến ngày"].Value != DBNull.Value)
                dtpDenNgay.Value = Convert.ToDateTime(row.Cells["Đến ngày"].Value);

            txtSoSV.Text = row.Cells["Số SV"].Value?.ToString();
            donGiaPhongHienTai = Convert.ToDecimal(row.Cells["Đơn giá"].Value);
            txtTienPhong.Text = Convert.ToDecimal(row.Cells["Tiền phòng"].Value).ToString("N0");

            txtDienCu.Text = row.Cells["Điện cũ"].Value?.ToString();
            txtDienMoi.Text = row.Cells["Điện mới"].Value?.ToString();
            txtNuocCu.Text = row.Cells["Nước cũ"].Value?.ToString();
            txtNuocMoi.Text = row.Cells["Nước mới"].Value?.ToString();

            cboTrangThai.SelectedItem = row.Cells["Trạng thái"].Value?.ToString();
            CalcTongHoaDon();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboPhongTP.Items.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu Phòng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isAdding = true;
            txtMaTien.Clear();
            cboPhongTP.SelectedIndex = 0;

            DateTime homNay = DateTime.Today;
            txtThangNam.Text = homNay.ToString("MM/yyyy");
            dtpTuNgay.Value = new DateTime(homNay.Year, homNay.Month, 1);
            dtpDenNgay.Value = dtpTuNgay.Value.AddMonths(1).AddDays(-1);

            txtDienCu.Text = "0";
            txtDienMoi.Text = "0";
            txtNuocCu.Text = "0";
            txtNuocMoi.Text = "0";
            cboTrangThai.SelectedItem = "Chưa đóng";

            SetControlState(true);
            CapNhatThongTinPhongVaTien();
            txtDienCu.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTien.Text))
            {
                MessageBox.Show("Vui lòng chọn một Hóa đơn trong danh sách để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isAdding = false;
            SetControlState(true);
            txtDienMoi.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTien.Text))
            {
                MessageBox.Show("Vui lòng chọn một Hóa đơn để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maTien = int.Parse(txtMaTien.Text);
            if (MessageBox.Show($"Bạn có chắc muốn xóa hóa đơn số [{maTien}]?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM TienPhong WHERE MaTien = @id",
                    new[] { new SqlParameter("@id", maTien) });
                MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaTien.Clear();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa hóa đơn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maPhong = cboPhongTP.SelectedValue.ToString();
            string thangNam = txtThangNam.Text.Trim();
            int maTienHienTai = string.IsNullOrWhiteSpace(txtMaTien.Text) ? 0 : int.Parse(txtMaTien.Text);

            if (!DateTime.TryParseExact("01/" + thangNam, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                MessageBox.Show("Tháng/Năm không hợp lệ! Vui lòng nhập đúng định dạng MM/yyyy (ví dụ: 11/2026).",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtThangNam.Focus();
                return;
            }

            if (!int.TryParse(txtDienCu.Text.Trim(), out int dienCu) ||
                !int.TryParse(txtDienMoi.Text.Trim(), out int dienMoi) ||
                !int.TryParse(txtNuocCu.Text.Trim(), out int nuocCu) ||
                !int.TryParse(txtNuocMoi.Text.Trim(), out int nuocMoi))
            {
                MessageBox.Show("Chỉ số điện và nước phải là số nguyên hợp lệ!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dienCu < 0 || dienMoi < dienCu)
            {
                MessageBox.Show("Chỉ số Điện mới phải lớn hơn hoặc bằng Điện cũ!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienMoi.Focus();
                return;
            }

            if (nuocCu < 0 || nuocMoi < nuocCu)
            {
                MessageBox.Show("Chỉ số Nước mới phải lớn hơn hoặc bằng Nước cũ!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNuocMoi.Focus();
                return;
            }

            // Chặn lập trùng 2 hóa đơn cho cùng 1 phòng trong cùng 1 tháng
            int trungHoaDon = Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM TienPhong WHERE MaPhong = @mp AND ThangNam = @tn AND MaTien <> @id",
                new[] {
                    new SqlParameter("@mp", maPhong),
                    new SqlParameter("@tn", thangNam),
                    new SqlParameter("@id", maTienHienTai)
                }));

            if (trungHoaDon > 0)
            {
                MessageBox.Show($"Phòng {maPhong} đã có hóa đơn cho tháng {thangNam} rồi!", "Trùng hóa đơn",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int soSV = int.Parse(txtSoSV.Text);
            decimal tienDien = TinhTienDienNhaNuoc(dienMoi - dienCu);
            decimal tienNuoc = TinhTienNuocNhaNuoc(nuocMoi - nuocCu);
            decimal tongTien = donGiaPhongHienTai + tienDien + tienNuoc;
            string trangThai = cboTrangThai.Text;

            try
            {
                SqlParameter[] prms = {
                    new SqlParameter("@mp", maPhong),
                    new SqlParameter("@tn", thangNam),
                    new SqlParameter("@tu", dtpTuNgay.Value.Date),
                    new SqlParameter("@den", dtpDenNgay.Value.Date),
                    new SqlParameter("@sosv", soSV),
                    new SqlParameter("@dg", donGiaPhongHienTai),
                    new SqlParameter("@tp", donGiaPhongHienTai),
                    new SqlParameter("@dc", dienCu),
                    new SqlParameter("@dm", dienMoi),
                    new SqlParameter("@td", tienDien),
                    new SqlParameter("@nc", nuocCu),
                    new SqlParameter("@nm", nuocMoi),
                    new SqlParameter("@tnc", tienNuoc),
                    new SqlParameter("@tong", tongTien),
                    new SqlParameter("@tt", trangThai),
                    new SqlParameter("@id", maTienHienTai)
                };

                if (isAdding)
                {
                    string sql = @"INSERT INTO TienPhong
                                   (MaPhong, ThangNam, TuNgay, DenNgay, SoSV, DonGiaPhong, TienPhong,
                                    ChiSoDienCu, ChiSoDienMoi, TienDien, ChiSoNuocCu, ChiSoNuocMoi, TienNuoc, TongTien, TrangThai)
                                   VALUES
                                   (@mp, @tn, @tu, @den, @sosv, @dg, @tp, @dc, @dm, @td, @nc, @nm, @tnc, @tong, @tt)";
                    DatabaseHelper.ExecuteNonQuery(sql, prms);
                    MessageBox.Show("Lập hóa đơn tiền phòng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string sql = @"UPDATE TienPhong
                                   SET ThangNam = @tn, TuNgay = @tu, DenNgay = @den,
                                       ChiSoDienCu = @dc, ChiSoDienMoi = @dm, TienDien = @td,
                                       ChiSoNuocCu = @nc, ChiSoNuocMoi = @nm, TienNuoc = @tnc,
                                       TongTien = @tong, TrangThai = @tt
                                   WHERE MaTien = @id";
                    DatabaseHelper.ExecuteNonQuery(sql, prms);
                    MessageBox.Show("Cập nhật hóa đơn thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadData();
                SetControlState(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu hóa đơn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            SetControlState(false);
            LoadData();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadData(txtLocThangNam.Text);
        }

        private void btnXuatHoaDon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaTien.Text))
            {
                MessageBox.Show("Vui lòng chọn một Hóa đơn trong danh sách bên dưới trước khi xuất!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrintDocument pd = new PrintDocument();
            pd.DocumentName = $"HoaDon_{cboPhongTP.SelectedValue}_{txtThangNam.Text.Replace("/", "-")}";
            pd.PrintPage += PrintHoaDon_PrintPage;

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = pd,
                Width = 900,
                Height = 700,
                StartPosition = FormStartPosition.CenterScreen
            };
            preview.ShowDialog();
        }
        private void PrintHoaDon_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            int left = 70;
            int right = e.PageBounds.Width - 70;
            int width = right - left;
            int y = 60;

            Font fHeader = new Font("Segoe UI", 11, FontStyle.Bold);
            Font fSub = new Font("Segoe UI", 10, FontStyle.Regular);
            Font fTitle = new Font("Segoe UI", 16, FontStyle.Bold);
            Font fBold = new Font("Segoe UI", 10, FontStyle.Bold);
            Font fItalic = new Font("Segoe UI", 9.5f, FontStyle.Italic);

            StringFormat center = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat rightAlign = new StringFormat { Alignment = StringAlignment.Far };
            Pen penThin = new Pen(Color.Gray, 1);
            Pen penBold = new Pen(Color.FromArgb(26, 48, 80), 2);

            // 1. Tiêu đề đơn vị
            g.DrawString("TRƯỜNG ĐẠI HỌC AN GIANG", fHeader, Brushes.Navy, left, y);
            g.DrawString($"Mã hóa đơn: #{txtMaTien.Text}", fBold, Brushes.Black, right, y, rightAlign);
            y += 22;
            g.DrawString("BAN QUẢN LÝ KÝ TÚC XÁ SINH VIÊN", fSub, Brushes.Black, left, y);
            g.DrawString($"Ngày in: {DateTime.Now:dd/MM/yyyy}", fSub, Brushes.Black, right, y, rightAlign);
            y += 32;
            g.DrawLine(penBold, left, y, right, y);
            y += 22;

            // 2. Tên phiếu
            g.DrawString("PHIẾU BÁO THU TIỀN PHÒNG & ĐIỆN NƯỚC", fTitle, Brushes.Navy, left + width / 2, y, center);
            y += 30;
            g.DrawString($"Kỳ thu tháng: {txtThangNam.Text}   (Từ {dtpTuNgay.Value:dd/MM/yyyy} đến {dtpDenNgay.Value:dd/MM/yyyy})",
                fItalic, Brushes.DimGray, left + width / 2, y, center);
            y += 38;

            // 3. Thông tin phòng
            g.DrawString($"Phòng ở: {cboPhongTP.Text}", fBold, Brushes.Black, left, y);
            g.DrawString($"Số sinh viên đang ở: {txtSoSV.Text} SV", fSub, Brushes.Black, right, y, rightAlign);
            y += 26;
            g.DrawString($"Trạng thái thanh toán: {cboTrangThai.Text}", fBold,
                cboTrangThai.Text == "Đã đóng" ? Brushes.Green : Brushes.Firebrick, left, y);
            y += 35;

            // 4. Bảng chi tiết các khoản thu
            int col1 = left + 10;          // STT
            int col2 = left + 65;          // Khoản thu & Chi tiết chỉ số
            int col3 = right - 170;        // Tiêu thụ / Định mức
            int col4 = right - 10;         // Thành tiền

            g.FillRectangle(new SolidBrush(Color.FromArgb(26, 48, 80)), left, y, width, 34);
            g.DrawString("STT", fBold, Brushes.White, col1, y + 7);
            g.DrawString("Khoản thu & Chỉ số đồng hồ", fBold, Brushes.White, col2, y + 7);
            g.DrawString("Mức sử dụng", fBold, Brushes.White, col3, y + 7, rightAlign);
            g.DrawString("Thành tiền (VNĐ)", fBold, Brushes.White, col4, y + 7, rightAlign);
            y += 34;

            int.TryParse(txtDienCu.Text, out int dCu);
            int.TryParse(txtDienMoi.Text, out int dMoi);
            int.TryParse(txtNuocCu.Text, out int nCu);
            int.TryParse(txtNuocMoi.Text, out int nMoi);

            string[,] rows = {
        { "1", "Tiền thuê phòng khoán cố định theo tháng", "1 phòng", txtTienPhong.Text },
        { "2", $"Tiền điện sinh hoạt (Cũ: {dCu} → Mới: {dMoi})", $"{Math.Max(0, dMoi - dCu)} kWh", txtTienDien.Text },
        { "3", $"Tiền nước sạch sinh hoạt (Cũ: {nCu} → Mới: {nMoi})", $"{Math.Max(0, nMoi - nCu)} m³", txtTienNuoc.Text }
    };

            for (int i = 0; i < 3; i++)
            {
                g.DrawRectangle(penThin, left, y, width, 36);
                g.DrawString(rows[i, 0], fSub, Brushes.Black, col1 + 8, y + 8);
                g.DrawString(rows[i, 1], fSub, Brushes.Black, col2, y + 8);
                g.DrawString(rows[i, 2], fSub, Brushes.Black, col3, y + 8, rightAlign);
                g.DrawString(rows[i, 3], fSub, Brushes.Black, col4, y + 8, rightAlign);
                y += 36;
            }

            // 5. Dòng tổng cộng
            g.FillRectangle(new SolidBrush(Color.FromArgb(242, 245, 249)), left, y, width, 40);
            g.DrawRectangle(penThin, left, y, width, 40);
            g.DrawString("TỔNG CỘNG THANH TOÁN:", fHeader, Brushes.Navy, col2, y + 9);
            g.DrawString($"{txtTongTien.Text} VNĐ", new Font("Segoe UI", 12, FontStyle.Bold),
                Brushes.Firebrick, col4, y + 8, rightAlign);
            y += 65;

            // 6. Khu vực ký xác nhận
            string nguoiLap = string.IsNullOrWhiteSpace(PhienDangNhap.HoTen) ? "Cán bộ quản lý" : PhienDangNhap.HoTen;
            g.DrawString("Đại diện phòng ở", fBold, Brushes.Black, left + 90, y, center);
            g.DrawString("Người lập phiếu", fBold, Brushes.Black, right - 90, y, center);
            y += 22;
            g.DrawString("(Ký và ghi rõ họ tên)", fItalic, Brushes.Gray, left + 90, y, center);
            g.DrawString("(Ký và ghi rõ họ tên)", fItalic, Brushes.Gray, right - 90, y, center);
            y += 75;
            g.DrawString(nguoiLap, fBold, Brushes.Black, right - 90, y, center);
        }
    }
}
