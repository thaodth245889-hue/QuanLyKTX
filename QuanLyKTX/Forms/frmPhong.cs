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
    public partial class frmPhong : Form
    {
        private bool isAdding = false;
        public frmPhong()
        {
            InitializeComponent();
        }

        private void frmPhong_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            LoadData();
            SetControlState(false);
        }
        private void LoadComboBoxes()
        {
            // Đổ danh sách Khu từ SQL
            DataTable dtKhu = DatabaseHelper.GetDataTable("SELECT MaKhu, TenKhu FROM Khu ORDER BY MaKhu");
            cboKhu.DataSource = dtKhu;
            cboKhu.DisplayMember = "TenKhu";
            cboKhu.ValueMember = "MaKhu";

            // Đổ Loại phòng & Sức chứa (4 hoặc 6 SV)
            cboLoaiPhong.Items.Clear();
            cboLoaiPhong.Items.AddRange(new object[] { "Nam", "Nữ" });
            cboLoaiPhong.SelectedIndex = 0;

            cboSucChua.Items.Clear();
            cboSucChua.Items.AddRange(new object[] { "4", "6" });
            cboSucChua.SelectedIndex = 0;
        }

        private void LoadData()
        {
            string sql = @"
                SELECT p.MaPhong AS [Mã phòng],
                       p.TenPhong AS [Tên phòng],
                       p.MaKhu AS [Mã khu],
                       k.TenKhu AS [Tên khu],
                       p.LoaiPhong AS [Loại phòng],
                       p.SucChua AS [Sức chứa],
                       p.GiaPhong AS [Giá phòng]
                FROM Phong p
                INNER JOIN Khu k ON p.MaKhu = k.MaKhu
                ORDER BY p.MaPhong";

            dgvPhong.DataSource = DatabaseHelper.GetDataTable(sql);

            if (dgvPhong.Columns["Giá phòng"] != null)
                dgvPhong.Columns["Giá phòng"].DefaultCellStyle.Format = "N0";
        }

        private void SetControlState(bool isEditing)
        {
            txtMaPhong.Enabled = isEditing && isAdding;
            txtTenPhong.Enabled = isEditing;
            cboKhu.Enabled = isEditing;
            cboLoaiPhong.Enabled = isEditing;
            cboSucChua.Enabled = isEditing;
            txtGiaPhong.Enabled = isEditing;

            btnThem.Enabled = !isEditing;
            btnSua.Enabled = !isEditing;
            btnXoa.Enabled = !isEditing;
            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;
            dgvPhong.Enabled = !isEditing;
        }

        private void dgvPhong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvPhong.Rows[e.RowIndex];

            txtMaPhong.Text = row.Cells["Mã phòng"].Value?.ToString();
            txtTenPhong.Text = row.Cells["Tên phòng"].Value?.ToString();
            cboKhu.SelectedValue = row.Cells["Mã khu"].Value?.ToString();
            cboLoaiPhong.SelectedItem = row.Cells["Loại phòng"].Value?.ToString();
            cboSucChua.SelectedItem = row.Cells["Sức chứa"].Value?.ToString();

            if (row.Cells["Giá phòng"].Value != null && row.Cells["Giá phòng"].Value != DBNull.Value)
                txtGiaPhong.Text = Convert.ToDecimal(row.Cells["Giá phòng"].Value).ToString("0");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboKhu.Items.Count == 0)
            {
                MessageBox.Show("Chưa có dữ liệu Khu KTX! Vui lòng thêm Khu trước.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isAdding = true;
            txtMaPhong.Clear();
            txtTenPhong.Clear();
            cboKhu.SelectedIndex = 0;
            cboLoaiPhong.SelectedIndex = 0;
            cboSucChua.SelectedIndex = 0;
            txtGiaPhong.Text = "700000";

            SetControlState(true);
            txtMaPhong.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaPhong.Text))
            {
                MessageBox.Show("Vui lòng chọn một Phòng trong danh sách để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isAdding = false;
            SetControlState(true);
            txtTenPhong.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maPhong = txtMaPhong.Text.Trim();
            if (string.IsNullOrEmpty(maPhong))
            {
                MessageBox.Show("Vui lòng chọn một Phòng để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa phòng [{maPhong}]?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM Phong WHERE MaPhong = @ma",
                    new[] { new SqlParameter("@ma", maPhong) });
                MessageBox.Show("Xóa phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaPhong.Clear();
                txtTenPhong.Clear();
                LoadData();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("Không thể xóa Phòng này vì đang có dữ liệu Đăng ký lưu trú hoặc Hóa đơn liên kết!",
                    "Ràng buộc khóa ngoại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string maPhong = txtMaPhong.Text.Trim();
            string tenPhong = txtTenPhong.Text.Trim();

            if (string.IsNullOrEmpty(maPhong) || string.IsNullOrEmpty(tenPhong))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã phòng và Tên phòng!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtGiaPhong.Text.Trim(), out decimal giaPhong) || giaPhong <= 0)
            {
                MessageBox.Show("Giá phòng phải là số dương hợp lệ (ví dụ: 700000)!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiaPhong.Focus();
                return;
            }

            string maKhu = cboKhu.SelectedValue.ToString();
            string loaiPhong = cboLoaiPhong.Text;
            int sucChua = int.Parse(cboSucChua.Text);

            try
            {
                SqlParameter[] prms = {
                    new SqlParameter("@ma", maPhong),
                    new SqlParameter("@ten", tenPhong),
                    new SqlParameter("@khu", maKhu),
                    new SqlParameter("@loai", loaiPhong),
                    new SqlParameter("@sc", sucChua),
                    new SqlParameter("@gia", giaPhong)
                };

                if (isAdding)
                {
                    string sql = @"INSERT INTO Phong (MaPhong, TenPhong, MaKhu, LoaiPhong, SucChua, GiaPhong)
                                   VALUES (@ma, @ten, @khu, @loai, @sc, @gia)";
                    DatabaseHelper.ExecuteNonQuery(sql, prms);
                    MessageBox.Show("Thêm phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string sql = @"UPDATE Phong
                                   SET TenPhong = @ten, MaKhu = @khu, LoaiPhong = @loai,
                                       SucChua = @sc, GiaPhong = @gia
                                   WHERE MaPhong = @ma";
                    DatabaseHelper.ExecuteNonQuery(sql, prms);
                    MessageBox.Show("Cập nhật phòng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadData();
                SetControlState(false);
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                MessageBox.Show("Mã phòng này đã tồn tại! Vui lòng nhập mã khác.", "Trùng khóa chính",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
