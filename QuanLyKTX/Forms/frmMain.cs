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
using System.Drawing.Drawing2D;
using System.Reflection;

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
            NangCapGiaoDienFrmMain();
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
        private Button _activeMenuBtn;
        private Button _hoverMenuBtn;
        private System.Windows.Forms.Timer _clockTimer;

        private void BatDoubleBuffer(Control ctrl)
        {
            PropertyInfo prop = typeof(Control).GetProperty(
                "DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            if (prop != null) prop.SetValue(ctrl, true, null);
        }

        private void NangCapGiaoDienFrmMain()
        {
            BatDoubleBuffer(this);
            this.BackColor = Color.FromArgb(241, 245, 249); // Nền nội dung xám sáng hiện đại

            // 1. Tự tìm Sidebar (Panel bên trái chứa các nút menu)
            Panel pnlSidebar = this.Controls.OfType<Panel>()
                .FirstOrDefault(p => p.Controls.OfType<Button>().Count() >= 4);

            // 2. Tự tìm Header (Panel phía trên chứa tiêu đề)
            Panel pnlHeader = TimPanelHeader(this, pnlSidebar);

            Color sidebarTop = Color.FromArgb(15, 23, 42);     // #0F172A (Navy đậm)
            Color sidebarBottom = Color.FromArgb(30, 58, 138); // #1E3A8A (Chàm hiện đại)

            // ===== NÂNG CẤP SIDEBAR =====
            if (pnlSidebar != null)
            {
                BatDoubleBuffer(pnlSidebar);

                // Tìm các thành phần bên trong Sidebar
                PictureBox picLogo = pnlSidebar.Controls.OfType<PictureBox>().FirstOrDefault();
                List<Button> allBtns = pnlSidebar.Controls.OfType<Button>()
                    .OrderBy(b => b.Top).ToList();
                List<Label> allLabels = pnlSidebar.Controls.OfType<Label>()
                    .OrderBy(l => l.Top).ToList();

                Label lblUser = allLabels.LastOrDefault(); // Label "Tên người dùng" bên dưới tên trường
                Button btnDangXuat = allBtns.FirstOrDefault(b =>
                    b.Text.IndexOf("Đăng xuất", StringComparison.OrdinalIgnoreCase) >= 0);

                // Làm mượt logo AGU hình tròn
                if (picLogo != null)
                {
                    picLogo.BackColor = Color.Transparent;
                    picLogo.SizeMode = PictureBoxSizeMode.Zoom;
                    picLogo.Paint += (s, e) =>
                    {
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        using (Pen ring = new Pen(Color.FromArgb(140, 255, 255, 255), 2f))
                        {
                            e.Graphics.DrawEllipse(ring, 2, 2, picLogo.Width - 5, picLogo.Height - 5);
                        }
                    };
                }

                // Làm trong suốt các Label trên nền Gradient
                foreach (Label lbl in allLabels)
                {
                    lbl.BackColor = Color.Transparent;
                    lbl.ForeColor = (lbl == lblUser)
                        ? Color.FromArgb(226, 232, 240)
                        : Color.White;
                }

                // Vẽ nền Gradient + khung Profile card + đường kẻ phân cách trên Sidebar
                pnlSidebar.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    Rectangle rect = pnlSidebar.ClientRectangle;
                    if (rect.Width <= 0 || rect.Height <= 0) return;

                    using (LinearGradientBrush bg = new LinearGradientBrush(
                        rect, sidebarTop, sidebarBottom, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(bg, rect);
                    }

                    // Thẻ Profile kính mờ quanh tên người dùng + chấm xanh Online
                    if (lblUser != null)
                    {
                        Rectangle card = new Rectangle(12, lblUser.Top - 8, pnlSidebar.Width - 24, lblUser.Height + 16);
                        using (GraphicsPath path = TaoBoGoc(card, 10))
                        using (SolidBrush fill = new SolidBrush(Color.FromArgb(28, 255, 255, 255)))
                        using (Pen border = new Pen(Color.FromArgb(45, 255, 255, 255), 1f))
                        {
                            e.Graphics.FillPath(fill, path);
                            e.Graphics.DrawPath(border, path);
                        }
                        using (SolidBrush onlineDot = new SolidBrush(Color.FromArgb(34, 197, 94)))
                        {
                            e.Graphics.FillEllipse(onlineDot, card.X + 10, card.Y + (card.Height - 8) / 2, 8, 8);
                        }
                    }

                    // Đường kẻ mảnh phân cách nhóm nút Đăng xuất / Thoát ở dưới
                    if (btnDangXuat != null)
                    {
                        int yLine = btnDangXuat.Top - 10;
                        using (Pen sep = new Pen(Color.FromArgb(45, 255, 255, 255), 1f))
                        {
                            e.Graphics.DrawLine(sep, 16, yLine, pnlSidebar.Width - 16, yLine);
                        }
                    }
                };

                // Chọn mặc định nút Trang chủ đang active
                _activeMenuBtn = allBtns.FirstOrDefault(b =>
                    b.Text.IndexOf("Trang chủ", StringComparison.OrdinalIgnoreCase) >= 0) ?? allBtns.FirstOrDefault();

                // Nâng cấp từng nút menu (căn thẳng hàng, sửa luôn lỗi lệch nút Đăng ký lưu trú)
                foreach (Button btn in allBtns)
                {
                    BatDoubleBuffer(btn);
                    btn.Left = 0;
                    btn.Width = pnlSidebar.Width;
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
                    btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
                    btn.Cursor = Cursors.Hand;

                    bool laNutThoat = btn.Text.IndexOf("Thoát", StringComparison.OrdinalIgnoreCase) >= 0;
                    bool laNutHeThong = laNutThoat || btn.Text.IndexOf("Đăng xuất", StringComparison.OrdinalIgnoreCase) >= 0;

                    btn.MouseEnter += (s, e) => { _hoverMenuBtn = btn; btn.Invalidate(); };
                    btn.MouseLeave += (s, e) => { if (_hoverMenuBtn == btn) _hoverMenuBtn = null; btn.Invalidate(); };

                    if (!laNutHeThong)
                    {
                        btn.Click += (s, e) =>
                        {
                            _activeMenuBtn = btn;
                            foreach (Button b in allBtns) b.Invalidate();
                        };
                    }

                    btn.Paint += (s, e) =>
                    {
                        Graphics g = e.Graphics;
                        g.SmoothingMode = SmoothingMode.AntiAlias;

                        // Vẽ nền khớp liền mạch với Gradient của Sidebar (chống viền vuông)
                        Rectangle fullBg = new Rectangle(
                            -btn.Left, -btn.Top,
                            Math.Max(1, pnlSidebar.Width), Math.Max(1, pnlSidebar.Height));
                        using (LinearGradientBrush bg = new LinearGradientBrush(
                            fullBg, sidebarTop, sidebarBottom, LinearGradientMode.Vertical))
                        {
                            g.FillRectangle(bg, btn.ClientRectangle);
                        }

                        bool isActive = (btn == _activeMenuBtn) && btn.Enabled;
                        bool isHover = (btn == _hoverMenuBtn) && btn.Enabled;
                        Rectangle pill = new Rectangle(10, 3, btn.Width - 20, btn.Height - 6);

                        if (isActive)
                        {
                            // Pill chuyển sắc Xanh công nghệ + vạch nhấn Cyan bên trái
                            using (GraphicsPath path = TaoBoGoc(pill, 9))
                            using (LinearGradientBrush activeBrush = new LinearGradientBrush(
                                pill, Color.FromArgb(37, 99, 235), Color.FromArgb(14, 165, 233),
                                LinearGradientMode.Horizontal))
                            {
                                g.FillPath(activeBrush, path);
                            }
                            Rectangle bar = new Rectangle(3, 10, 4, btn.Height - 20);
                            using (GraphicsPath barPath = TaoBoGoc(bar, 2))
                            using (SolidBrush barBrush = new SolidBrush(Color.FromArgb(56, 189, 248)))
                            {
                                g.FillPath(barBrush, barPath);
                            }
                        }
                        else if (isHover)
                        {
                            Color hoverColor = laNutThoat
                                ? Color.FromArgb(190, 220, 38, 38)  // Đỏ san hô nhẹ khi hover nút Thoát
                                : Color.FromArgb(32, 255, 255, 255); // Trắng trong suốt nhẹ
                            using (GraphicsPath path = TaoBoGoc(pill, 9))
                            using (SolidBrush hoverBrush = new SolidBrush(hoverColor))
                            {
                                g.FillPath(hoverBrush, path);
                            }
                        }

                        // Vẽ icon (nếu nút có gắn Image)
                        int textX = 22;
                        if (btn.Image != null)
                        {
                            int iconSize = 18;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.DrawImage(btn.Image, 20, (btn.Height - iconSize) / 2, iconSize, iconSize);
                            textX = 48;
                        }

                        // Vẽ chữ sắc nét, tự động xóa khoảng trắng thừa gây lệch dòng
                        Color textColor = !btn.Enabled
                            ? Color.FromArgb(100, 116, 139) // Xám mờ cho nút bị khóa (Staff)
                            : (isActive || isHover ? Color.White : Color.FromArgb(203, 213, 225));
                        FontStyle fs = isActive ? FontStyle.Bold : FontStyle.Regular;

                        using (Font drawFont = new Font("Segoe UI", 10f, fs))
                        {
                            Rectangle textRect = new Rectangle(textX, 0, btn.Width - textX - 14, btn.Height);
                            TextRenderer.DrawText(g, btn.Text.Trim(), drawFont, textRect, textColor,
                                TextFormatFlags.VerticalCenter | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
                        }
                    };
                }
            }

            // ===== NÂNG CẤP HEADER BAR PHÍA TRÊN =====
            if (pnlHeader != null)
            {
                BatDoubleBuffer(pnlHeader);
                pnlHeader.BackColor = Color.White;

                Label lblHeaderTitle = pnlHeader.Controls.OfType<Label>().FirstOrDefault();
                if (lblHeaderTitle != null)
                {
                    lblHeaderTitle.Left = Math.Max(lblHeaderTitle.Left, 28);
                    lblHeaderTitle.ForeColor = Color.FromArgb(15, 23, 42);
                    lblHeaderTitle.Font = new Font("Segoe UI", 13.5f, FontStyle.Bold);
                }

                // Vẽ vạch nhấn dọc trước tiêu đề + viền bóng mảnh phía dưới Header
                pnlHeader.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    Rectangle accent = new Rectangle(14, 14, 5, pnlHeader.Height - 28);
                    using (GraphicsPath p = TaoBoGoc(accent, 3))
                    using (LinearGradientBrush br = new LinearGradientBrush(
                        accent, Color.FromArgb(37, 99, 235), Color.FromArgb(6, 182, 212),
                        LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillPath(br, p);
                    }
                    using (Pen border = new Pen(Color.FromArgb(226, 232, 240), 1.5f))
                    {
                        e.Graphics.DrawLine(border, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
                    }
                };

                // Thêm đồng hồ ngày/giờ thực ở góc phải thanh Header
                Label lblClock = new Label
                {
                    AutoSize = false,
                    Size = new Size(210, 32),
                    Location = new Point(pnlHeader.Width - 226, (pnlHeader.Height - 32) / 2),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                    ForeColor = Color.FromArgb(37, 99, 235),
                    BackColor = Color.FromArgb(239, 246, 255),
                    Text = DateTime.Now.ToString("dd/MM/yyyy  |  HH:mm:ss")
                };
                pnlHeader.Controls.Add(lblClock);

                _clockTimer = new System.Windows.Forms.Timer { Interval = 1000 };
                _clockTimer.Tick += (s, e) => lblClock.Text = DateTime.Now.ToString("dd/MM/yyyy  |  HH:mm:ss");
                _clockTimer.Start();

                this.FormClosed += (s, e) =>
                {
                    if (_clockTimer != null)
                    {
                        _clockTimer.Stop();
                        _clockTimer.Dispose();
                    }
                };
            }
        }

        private Panel TimPanelHeader(Control root, Panel pnlSidebar)
        {
            foreach (Control c in root.Controls)
            {
                if (c is Panel p && p != pnlSidebar)
                {
                    if (p.Height <= 90 && p.Top <= 5 && p.Controls.OfType<Label>().Any())
                        return p;
                    Panel found = TimPanelHeader(p, pnlSidebar);
                    if (found != null) return found;
                }
            }
            return null;
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
