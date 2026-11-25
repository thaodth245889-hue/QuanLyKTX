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
    public partial class frmKhu : Form
    {
        private bool isAdding = false;
        public frmKhu()
        {
            InitializeComponent();
        }

        private void frmKhu_Load(object sender, EventArgs e)
        {
            LoadData();
            SetControlState(false);
            UIHelper.LamDepFormQuanLy(this);
        }
        private void LoadData()
        {
            string sql = "SELECT MaKhu AS [Mã khu], TenKhu AS [Tên khu] FROM Khu ORDER BY MaKhu";
            dgvKhu.DataSource = DatabaseHelper.GetDataTable(sql);
        }

        private void SetControlState(bool isEditing)
        {
            txtMaKhu.Enabled = isEditing && isAdding;
            txtTenKhu.Enabled = isEditing;

            btnThem.Enabled = !isEditing;
            btnSua.Enabled = !isEditing;
            btnXoa.Enabled = !isEditing;
            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;
            dgvKhu.Enabled = !isEditing;
        }
        private void dgvKhu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvKhu.Rows[e.RowIndex];
            txtMaKhu.Text = row.Cells["Mã khu"].Value?.ToString();
            txtTenKhu.Text = row.Cells["Tên khu"].Value?.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            txtMaKhu.Clear();
            txtTenKhu.Clear();
            SetControlState(true);
            txtMaKhu.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaKhu.Text))
            {
                MessageBox.Show("Vui lòng chọn một Khu trong danh sách để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isAdding = false;
            SetControlState(true);
            txtTenKhu.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = txtMaKhu.Text.Trim();
            if (string.IsNullOrEmpty(ma))
            {
                MessageBox.Show("Vui lòng chọn một Khu để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa khu [{ma}]?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM Khu WHERE MaKhu = @ma",
                    new[] { new SqlParameter("@ma", ma) });
                MessageBox.Show("Xóa khu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhu.Clear();
                txtTenKhu.Clear();
                LoadData();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("Không thể xóa Khu này vì đang có Phòng trực thuộc!", "Ràng buộc khóa ngoại",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string ma = txtMaKhu.Text.Trim();
            string ten = txtTenKhu.Text.Trim();

            if (string.IsNullOrEmpty(ma) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Không được để trống Mã khu hoặc Tên khu!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (isAdding)
                {
                    string sql = "INSERT INTO Khu (MaKhu, TenKhu) VALUES (@ma, @ten)";
                    DatabaseHelper.ExecuteNonQuery(sql, new[] {
                        new SqlParameter("@ma", ma),
                        new SqlParameter("@ten", ten)
                    });
                    MessageBox.Show("Thêm khu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string sql = "UPDATE Khu SET TenKhu = @ten WHERE MaKhu = @ma";
                    DatabaseHelper.ExecuteNonQuery(sql, new[] {
                        new SqlParameter("@ma", ma),
                        new SqlParameter("@ten", ten)
                    });
                    MessageBox.Show("Cập nhật khu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadData();
                SetControlState(false);
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                MessageBox.Show("Mã khu này đã tồn tại! Vui lòng nhập mã khác.", "Trùng khóa chính",
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
