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
    public partial class frmSinhVien : Form
    {
        private bool isAdding = false;
        public frmSinhVien()
        {
            InitializeComponent();
        }

        private void frmSinhVien_Load(object sender, EventArgs e)
        {
            LoadData();
            SetControlState(false);
            UIHelper.LamDepFormQuanLy(this);
        }
        private void LoadData(string tuKhoa = "")
        {
            string sql = @"
                SELECT MSSV,
                       HoTen AS [Họ và tên],
                       GioiTinh AS [Giới tính],
                       NgaySinh AS [Ngày sinh],
                       SDT AS [SĐT],
                       QueQuan AS [Quê quán]
                FROM SinhVien";

            SqlParameter[] prms = null;
            if (!string.IsNullOrWhiteSpace(tuKhoa))
            {
                sql += " WHERE MSSV LIKE @kw OR HoTen LIKE @kw";
                prms = new[] { new SqlParameter("@kw", "%" + tuKhoa.Trim() + "%") };
            }

            sql += " ORDER BY MSSV";
            dgvSinhVien.DataSource = DatabaseHelper.GetDataTable(sql, prms);

            if (dgvSinhVien.Columns["Ngày sinh"] != null)
                dgvSinhVien.Columns["Ngày sinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void SetControlState(bool isEditing)
        {
            txtMSSV.Enabled = isEditing && isAdding;
            txtHoTen.Enabled = isEditing;
            radNam.Enabled = isEditing;
            radNu.Enabled = isEditing;
            dtpNgaySinh.Enabled = isEditing;
            txtSDT.Enabled = isEditing;
            txtQueQuan.Enabled = isEditing;

            btnThem.Enabled = !isEditing;
            btnSua.Enabled = !isEditing;
            btnXoa.Enabled = !isEditing;
            btnLuu.Enabled = isEditing;
            btnHuy.Enabled = isEditing;
            btnTimKiem.Enabled = !isEditing;
            txtTimKiem.Enabled = !isEditing;
            dgvSinhVien.Enabled = !isEditing;
        }

        private void dgvSinhVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];

            txtMSSV.Text = row.Cells["MSSV"].Value?.ToString();
            txtHoTen.Text = row.Cells["Họ và tên"].Value?.ToString();

            string gt = row.Cells["Giới tính"].Value?.ToString();
            radNam.Checked = (gt == "Nam");
            radNu.Checked = (gt == "Nữ");

            if (row.Cells["Ngày sinh"].Value != null && row.Cells["Ngày sinh"].Value != DBNull.Value)
                dtpNgaySinh.Value = Convert.ToDateTime(row.Cells["Ngày sinh"].Value);

            txtSDT.Text = row.Cells["SĐT"].Value?.ToString();
            txtQueQuan.Text = row.Cells["Quê quán"].Value?.ToString();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            isAdding = true;
            txtMSSV.Clear();
            txtHoTen.Clear();
            radNam.Checked = true;
            dtpNgaySinh.Value = new DateTime(2006, 1, 1);
            txtSDT.Clear();
            txtQueQuan.Clear();

            SetControlState(true);
            txtMSSV.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMSSV.Text))
            {
                MessageBox.Show("Vui lòng chọn một Sinh viên trong danh sách để sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            isAdding = false;
            SetControlState(true);
            txtHoTen.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string mssv = txtMSSV.Text.Trim();
            if (string.IsNullOrEmpty(mssv))
            {
                MessageBox.Show("Vui lòng chọn một Sinh viên để xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show($"Bạn có chắc muốn xóa sinh viên [{mssv}]?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                DatabaseHelper.ExecuteNonQuery("DELETE FROM SinhVien WHERE MSSV = @mssv",
                    new[] { new SqlParameter("@mssv", mssv) });
                MessageBox.Show("Xóa sinh viên thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMSSV.Clear();
                txtHoTen.Clear();
                txtSDT.Clear();
                txtQueQuan.Clear();
                LoadData();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                MessageBox.Show("Không thể xóa Sinh viên này vì đã có lịch sử Đăng ký lưu trú!",
                    "Ràng buộc khóa ngoại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string mssv = txtMSSV.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string gioiTinh = radNam.Checked ? "Nam" : "Nữ";
            DateTime ngaySinh = dtpNgaySinh.Value.Date;
            string sdt = txtSDT.Text.Trim();
            string queQuan = txtQueQuan.Text.Trim();

            if (string.IsNullOrEmpty(mssv) || string.IsNullOrEmpty(hoTen))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ MSSV và Họ tên!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlParameter[] prms = {
                    new SqlParameter("@mssv", mssv),
                    new SqlParameter("@hoten", hoTen),
                    new SqlParameter("@gt", gioiTinh),
                    new SqlParameter("@ns", ngaySinh),
                    new SqlParameter("@sdt", sdt),
                    new SqlParameter("@qq", queQuan)
                };

                if (isAdding)
                {
                    string sql = @"INSERT INTO SinhVien (MSSV, HoTen, GioiTinh, NgaySinh, SDT, QueQuan)
                                   VALUES (@mssv, @hoten, @gt, @ns, @sdt, @qq)";
                    DatabaseHelper.ExecuteNonQuery(sql, prms);
                    MessageBox.Show("Thêm hồ sơ sinh viên thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    string sql = @"UPDATE SinhVien
                                   SET HoTen = @hoten, GioiTinh = @gt, NgaySinh = @ns,
                                       SDT = @sdt, QueQuan = @qq
                                   WHERE MSSV = @mssv";
                    DatabaseHelper.ExecuteNonQuery(sql, prms);
                    MessageBox.Show("Cập nhật hồ sơ sinh viên thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LoadData();
                SetControlState(false);
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                MessageBox.Show("MSSV này đã tồn tại trong hệ thống!", "Trùng khóa chính",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadData(txtTimKiem.Text);
        }
    }
}
