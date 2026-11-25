using QuanLyKTX.DAL;
using QuanLyKTX.Models;
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
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string user = txtTenDangNhap.Text.Trim();
            string pass = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = "SELECT TenDangNhap, HoTen, Quyen FROM TaiKhoan WHERE TenDangNhap = @u AND MatKhau = @p";
                SqlParameter[] prms = {
                    new SqlParameter("@u", user),
                    new SqlParameter("@p", pass)
                };

                DataTable dt = DatabaseHelper.GetDataTable(sql, prms);
                if (dt.Rows.Count > 0)
                {
                    PhienDangNhap.TenDangNhap = dt.Rows[0]["TenDangNhap"].ToString();
                    PhienDangNhap.HoTen = dt.Rows[0]["HoTen"].ToString();
                    PhienDangNhap.Quyen = dt.Rows[0]["Quyen"].ToString();

                    this.Hide();
                    using (frmMain f = new frmMain())
                    {
                        f.ShowDialog();
                        if (f.IsDangXuat)
                        {
                            txtMatKhau.Clear();
                            this.Show();
                            txtTenDangNhap.Focus();
                        }
                        else
                        {
                            Application.Exit();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi đăng nhập",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtMatKhau.Clear();
                    txtMatKhau.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối CSDL: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
