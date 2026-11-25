using QuanLyKTX.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKTX.Forms
{
    public partial class frmMain : Form

    {
        private Form currentChildForm;
        private Button activeButton;
        public bool IsDangXuat { get; private set; } = false;
        public frmMain()
        {
            InitializeComponent();
        }

        private void pnlContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            btnTrangChu.Text = "🏠   Trang chủ";
            btnKhu.Text = "🏢   Quản lý Khu";
            btnPhong.Text = "🛏   Quản lý Phòng";
            btnSinhVien.Text = "👥   Quản lý Sinh viên";
            btnDangKy.Text = "📋   Đăng ký lưu trú";
            btnTienPhong.Text = "💳   Quản lý Tiền phòng";
            btnDangXuat.Text = "↪   Đăng xuất";
            btnThoat.Text = "✖   Thoát";
            lblNguoiDung.Text = $"{PhienDangNhap.HoTen}\n({PhienDangNhap.Quyen})";

            // Phân quyền: Staff bị khóa quản lý Khu KTX
            if (PhienDangNhap.Quyen == "Staff")
            {
                btnKhu.Enabled = false;
                btnKhu.ForeColor = Color.Gray;
            }

            OpenChildForm(new frmTrangChu(), "BẢNG ĐIỀU KHIỂN TỔNG QUAN KÝ TÚC XÁ", btnTrangChu);
        }
        private void OpenChildForm(Form childForm, string tieuDe, Button clickedBtn)
        {
            if (currentChildForm != null)
                currentChildForm.Close();

            if (activeButton != null)
                activeButton.BackColor = Color.FromArgb(26, 48, 80);

            activeButton = clickedBtn;
            activeButton.BackColor = Color.FromArgb(0, 120, 215);

            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            pnlContent.Controls.Clear();
            pnlContent.Controls.Add(childForm);
            childForm.BringToFront();
            childForm.Show();

            lblTieuDe.Text = tieuDe;
        }
        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmTrangChu(), "BẢNG ĐIỀU KHIỂN TỔNG QUAN KÝ TÚC XÁ", btnTrangChu);
        }

        private void btnKhu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmKhu(), "QUẢN LÝ KHU KÝ TÚC XÁ", btnKhu);
        }

        private void btnPhong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmPhong(), "QUẢN LÝ PHÒNG Ở", btnPhong);
        }

        private void btnSinhVien_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmSinhVien(), "QUẢN LÝ HỒ SƠ SINH VIÊN", btnSinhVien);
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDangKyPhong(), "QUẢN LÝ ĐĂNG KÝ LƯU TRÚ", btnDangKy);
        }

        private void btnTienPhong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmTienPhong(), "QUẢN LÝ TIỀN PHÒNG & ĐIỆN NƯỚC THEO ĐỒNG HỒ", btnTienPhong);
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn đăng xuất tài khoản hiện tại?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IsDangXuat = true;
                this.Close();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát phần mềm?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlSidebar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblNguoiDung_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblTieuDe_Click(object sender, EventArgs e)
        {

        }
    }
}
