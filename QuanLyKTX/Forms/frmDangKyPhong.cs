using QuanLyKTX.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKTX.Forms
{
    public partial class frmDangKyPhong : Form
    {
        private bool isAdding = false;
        public frmDangKyPhong()
        {
            InitializeComponent();
        }

        private void frmDangKyPhong_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            LoadData();
            SetControlState(false);
            UIHelper.LamDepFormQuanLy(this);
        }
        private void LoadComboBoxes()
        {
            DataTable dtSV = DatabaseHelper.GetDataTable(
                "SELECT MSSV, (MSSV + ' - ' + HoTen + ' (' + GioiTinh + ')') AS HienThi FROM SinhVien ORDER BY MSSV");
            cboSinhVien.DataSource = dtSV;
            cboSinhVien.DisplayMember = "HienThi";
            cboSinhVien.ValueMember = "MSSV";

            DataTable dtPhong = DatabaseHelper.GetDataTable(
                "SELECT MaPhong, (MaPhong + ' - ' + TenPhong + ' (' + LoaiPhong + ')') AS HienThi FROM Phong ORDER BY MaPhong");
            cboPhong.DataSource = dtPhong;
            cboPhong.DisplayMember = "HienThi";
            cboPhong.ValueMember = "MaPhong";

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] { "Đang ở", "Đã chuyển đi" });
            cboTrangThai.SelectedIndex = 0;
        }

        private void LoadData()
        {
            string sql = @"
                SELECT dk.MaDangKy AS [Mã ĐK],
                       dk.MSSV,
                       sv.HoTen AS [Họ tên],
                       dk.MaPhong AS [Mã phòng],
                       dk.NgayNhanPhong AS [Ngày nhận],
                       dk.NgayTra AS [Ngày trả],
                       dk.TrangThai AS [Trạng thái]
                FROM DangKyPhong dk
                INNER JOIN SinhVien sv ON dk.MSSV = sv.MSSV
                ORDER BY dk.MaDangKy DESC";

            dgvDangKy.DataSource = DatabaseHelper.GetDataTable(sql);

            if (dgvDangKy.Columns["Ngày nhận"] != null)
                dgvDangKy.Columns["Ngày nhận"].DefaultCellStyle.Format = "dd/MM/yyyy";
            if (dgvDangKy.Columns["Ngày trả"] != null)
                dgvDangKy.Columns["Ngày trả"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void SetControlState(bool isEditing)
        {
            cboSinhVien.Enabled = isEditing && isAdding;
            cboPhong.Enabled = isEditing;
            dtpNgayNhan.Enabled = isEditing;
            dtpNgayTra.Enabled = isEditing;
            cboTrangThai.Enabled = isEditing;

            btnThem.Enabled = !isEditing;
            btnSua.Enabled = !isEditing;
            btnXoa.Enabled = !isEditing;
            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;
            dgvDangKy.Enabled = !isEditing;
        }

        private void dgvDangKy_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvDangKy.Rows[e.RowIndex];

            txtMaDangKy.Text = row.Cells["Mã ĐK"].Value?.ToString();
            cboSinhVien.SelectedValue = row.Cells["MSSV"].Value?.ToString();
            cboPhong.SelectedValue = row.Cells["Mã phòng"].Value?.ToString();

            if (row.Cells["Ngày nhận"].Value != null && row.Cells["Ngày nhận"].Value != DBNull.Value)
                dtpNgayNhan.Value = Convert.ToDateTime(row.Cells["Ngày nhận"].Value);

            if (row.Cells["Ngày trả"].Value != null && row.Cells["Ngày trả"].Value != DBNull.Value)
            {
                dtpNgayTra.Checked = true;
                dtpNgayTra.Value = Convert.ToDateTime(row.Cells["Ngày trả"].Value);
            }
            else
            {
                dtpNgayTra.Checked = false;
            }

            cboTrangThai.SelectedItem = row.Cells["Trạng thái"].Value?.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboSinhVien.Items.Count == 0 || cboPhong.Items.Count == 0)
            {
                MessageBox.Show("Cần có dữ liệu Sinh viên và Phòng trước khi đăng ký!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isAdding = true;
            txtMaDangKy.Clear();
            cboSinhVien.SelectedIndex = 0;
            cboPhong.SelectedIndex = 0;
            dtpNgayNhan.Value = DateTime.Today;
            dtpNgayTra.Checked = false;
            cboTrangThai.SelectedItem = "Đang ở";

            SetControlState(true);
            cboSinhVien.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDangKy.Text))
            {
                MessageBox.Show("Vui lòng chọn một lượt đăng ký trong danh sách để cập nhật!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isAdding = false;
            SetControlState(true);
            cboTrangThai.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaDangKy.Text))
            {
                MessageBox.Show("Vui lòng chọn một dòng đăng ký để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maDK = int.Parse(txtMaDangKy.Text);
            if (MessageBox.Show($"Bạn có chắc muốn xóa lượt đăng ký số [{maDK}]?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM DangKyPhong WHERE MaDangKy = @id",
                    new[] { new SqlParameter("@id", maDK) });
                MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaDangKy.Clear();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string mssv = cboSinhVien.SelectedValue.ToString();
            string maPhong = cboPhong.SelectedValue.ToString();
            DateTime ngayNhan = dtpNgayNhan.Value.Date;
            string trangThai = cboTrangThai.Text;
            int maDKHienTai = string.IsNullOrWhiteSpace(txtMaDangKy.Text) ? 0 : int.Parse(txtMaDangKy.Text);

            // Kiểm tra ngày trả phòng
            if (trangThai == "Đã chuyển đi" && !dtpNgayTra.Checked)
            {
                dtpNgayTra.Checked = true;
                dtpNgayTra.Value = DateTime.Today;
            }

            if (dtpNgayTra.Checked && dtpNgayTra.Value.Date < ngayNhan)
            {
                MessageBox.Show("Ngày trả phòng không được nhỏ hơn Ngày nhận phòng!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra 3 ràng buộc nghiệp vụ khi xếp trạng thái "Đang ở"
            if (trangThai == "Đang ở")
            {
                // 1. Kiểm tra giới tính SV khớp Loại phòng
                string gioiTinhSV = DatabaseHelper.ExecuteScalar(
                    "SELECT GioiTinh FROM SinhVien WHERE MSSV = @mssv",
                    new[] { new SqlParameter("@mssv", mssv) })?.ToString();

                DataTable dtP = DatabaseHelper.GetDataTable(
                    "SELECT LoaiPhong, SucChua FROM Phong WHERE MaPhong = @mp",
                    new[] { new SqlParameter("@mp", maPhong) });

                string loaiPhong = dtP.Rows[0]["LoaiPhong"].ToString();
                int sucChua = Convert.ToInt32(dtP.Rows[0]["SucChua"]);

                if (gioiTinhSV != loaiPhong)
                {
                    MessageBox.Show($"Không thể xếp sinh viên {gioiTinhSV} vào phòng dành cho {loaiPhong}!",
                        "Vi phạm ràng buộc giới tính", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2. Kiểm tra sinh viên đang ở một phòng khác chưa trả
                int dangOChoKhac = Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM DangKyPhong WHERE MSSV = @mssv AND TrangThai = N'Đang ở' AND MaDangKy <> @id",
                    new[] { new SqlParameter("@mssv", mssv), new SqlParameter("@id", maDKHienTai) }));

                if (dangOChoKhac > 0)
                {
                    MessageBox.Show("Sinh viên này hiện đang lưu trú tại một phòng khác chưa trả phòng!",
                        "Ràng buộc lưu trú", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 3. Kiểm tra sức chứa phòng (4 hoặc 6 sinh viên)
                int soDangO = Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM DangKyPhong WHERE MaPhong = @mp AND TrangThai = N'Đang ở' AND MaDangKy <> @id",
                    new[] { new SqlParameter("@mp", maPhong), new SqlParameter("@id", maDKHienTai) }));

                if (soDangO >= sucChua)
                {
                    MessageBox.Show($"Phòng {maPhong} đã đủ sĩ số ({soDangO}/{sucChua} sinh viên)!",
                        "Phòng đã đầy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                object ngayTraVal = dtpNgayTra.Checked ? (object)dtpNgayTra.Value.Date : DBNull.Value;

                if (isAdding)
                {
                    string sql = @"INSERT INTO DangKyPhong (MSSV, MaPhong, NgayNhanPhong, NgayTra, TrangThai)
                                   VALUES (@mssv, @mp, @nn, @nt, @tt)";
                    DatabaseHelper.ExecuteNonQuery(sql, new[] {
                        new SqlParameter("@mssv", mssv),
                        new SqlParameter("@mp", maPhong),
                        new SqlParameter("@nn", ngayNhan),
                        new SqlParameter("@nt", ngayTraVal),
                        new SqlParameter("@tt", trangThai)
                    });
                    MessageBox.Show("Đăng ký lưu trú thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string sql = @"UPDATE DangKyPhong
                                   SET MaPhong = @mp, NgayNhanPhong = @nn, NgayTra = @nt, TrangThai = @tt
                                   WHERE MaDangKy = @id";
                    DatabaseHelper.ExecuteNonQuery(sql, new[] {
                        new SqlParameter("@mp", maPhong),
                        new SqlParameter("@nn", ngayNhan),
                        new SqlParameter("@nt", ngayTraVal),
                        new SqlParameter("@tt", trangThai),
                        new SqlParameter("@id", maDKHienTai)
                    });
                    MessageBox.Show("Cập nhật thông tin lưu trú thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadData();
                SetControlState(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi",
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
    }
}
