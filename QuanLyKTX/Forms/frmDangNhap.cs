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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace QuanLyKTX.Forms
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
            ApDungGiaoDienKinhMo();
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
        private void ApDungGiaoDienKinhMo()
        {
            this.DoubleBuffered = true;

            // 1. Tự động căn toàn bộ cụm control vào chính giữa Form
            int minX = int.MaxValue, minY = int.MaxValue;
            int maxX = int.MinValue, maxY = int.MinValue;

            foreach (Control c in this.Controls)
            {
                if (!c.Visible) continue;
                if (c.Left < minX) minX = c.Left;
                if (c.Top < minY) minY = c.Top;
                if (c.Right > maxX) maxX = c.Right;
                if (c.Bottom > maxY) maxY = c.Bottom;
            }

            if (minX < maxX && minY < maxY)
            {
                int groupW = maxX - minX;
                int groupH = maxY - minY;
                int offsetX = (this.ClientSize.Width - groupW) / 2 - minX;
                int offsetY = (this.ClientSize.Height - groupH) / 2 - minY;

                foreach (Control c in this.Controls)
                {
                    c.Left += offsetX;
                    c.Top += offsetY;
                }

                // Khung kính mờ ôm quanh cụm đăng nhập
                Rectangle cardRect = new Rectangle(
                    (this.ClientSize.Width - groupW) / 2 - 48,
                    (this.ClientSize.Height - groupH) / 2 - 30,
                    groupW + 96,
                    groupH + 60);

                // 2. Tạo ảnh nền KTX phủ kín + làm mờ nhẹ + vẽ khung kính mờ bo góc
                if (this.BackgroundImage != null)
                {
                    this.BackgroundImage = TaoNenKinhMo(this.BackgroundImage, this.ClientSize, cardRect);
                    this.BackgroundImageLayout = ImageLayout.None;
                }
            }

            // 3. Làm trong suốt chữ, cắt tròn logo AGU và bo góc 2 nút bấm
            foreach (Control c in this.Controls)
            {
                if (c is Label || c is CheckBox)
                {
                    c.BackColor = Color.Transparent;
                }
                else if (c is PictureBox pic)
                {
                    pic.BackColor = Color.Transparent;
                    if (pic.Image != null)
                        pic.Image = CatAnhHinhTron(pic.Image, Math.Min(pic.Width, pic.Height));
                }
                else if (c is Button btn)
                {
                    bool laNutChinh = btn.Text.IndexOf("Đăng nhập", StringComparison.OrdinalIgnoreCase) >= 0;
                    TrangTriNutBoGoc(btn, laNutChinh);
                }
            }
        }

        private Bitmap TaoNenKinhMo(Image anhGoc, Size kichThuoc, Rectangle cardRect)
        {
            int w = kichThuoc.Width;
            int h = kichThuoc.Height;

            // Cắt ảnh phủ kín khung (không bị méo hình tòa nhà)
            Bitmap cover = new Bitmap(w, h);
            using (Graphics g = Graphics.FromImage(cover))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                float ratio = Math.Max((float)w / anhGoc.Width, (float)h / anhGoc.Height);
                int drawW = (int)Math.Ceiling(anhGoc.Width * ratio);
                int drawH = (int)Math.Ceiling(anhGoc.Height * ratio);
                int drawX = (w - drawW) / 2;
                int drawY = (h - drawH) / 2;
                g.DrawImage(anhGoc, drawX, drawY, drawW, drawH);
            }

            // Làm mờ nhẹ toàn nền (số 5 càng lớn nền càng mờ)
            Bitmap nenMo = LamMoAnh(cover, 5);
            // Làm mờ sâu hơn bên trong khung kính
            Bitmap kinhMo = LamMoAnh(cover, 14);
            cover.Dispose();

            using (Graphics g = Graphics.FromImage(nenMo))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Phủ lớp bóng nhẹ dưới thẻ kính
                Rectangle shadowRect = new Rectangle(cardRect.X + 4, cardRect.Y + 6, cardRect.Width, cardRect.Height);
                using (GraphicsPath shadowPath = TaoDuongBoGoc(shadowRect, 18))
                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(45, 0, 0, 0)))
                {
                    g.FillPath(shadowBrush, shadowPath);
                }

                // Vẽ phần nền mờ sâu + lớp trắng sữa trong suốt bên trong khung bo góc
                using (GraphicsPath cardPath = TaoDuongBoGoc(cardRect, 18))
                {
                    g.SetClip(cardPath);
                    g.DrawImage(kinhMo, 0, 0);
                    // Số 195 là độ trắng đục của thẻ kính (tăng lên 215 nếu muốn nổi chữ hơn nữa)
                    using (SolidBrush frostBrush = new SolidBrush(Color.FromArgb(195, 255, 255, 255)))
                    {
                        g.FillPath(frostBrush, cardPath);
                    }
                    g.ResetClip();

                    // Viền trắng mảnh tạo cảm giác mặt kính
                    using (Pen borderPen = new Pen(Color.FromArgb(230, 255, 255, 255), 1.6f))
                    {
                        g.DrawPath(borderPen, cardPath);
                    }
                }
            }

            kinhMo.Dispose();
            return nenMo;
        }

        private Bitmap LamMoAnh(Bitmap src, int doMo)
        {
            int smallW = Math.Max(1, src.Width / doMo);
            int smallH = Math.Max(1, src.Height / doMo);

            Bitmap small = new Bitmap(smallW, smallH);
            using (Graphics g = Graphics.FromImage(small))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (ImageAttributes ia = new ImageAttributes())
                {
                    ia.SetWrapMode(WrapMode.TileFlipXY);
                    g.DrawImage(src, new Rectangle(0, 0, smallW, smallH), 0, 0, src.Width, src.Height, GraphicsUnit.Pixel, ia);
                }
            }

            Bitmap blurred = new Bitmap(src.Width, src.Height);
            using (Graphics g = Graphics.FromImage(blurred))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (ImageAttributes ia = new ImageAttributes())
                {
                    ia.SetWrapMode(WrapMode.TileFlipXY);
                    g.DrawImage(small, new Rectangle(0, 0, src.Width, src.Height), 0, 0, smallW, smallH, GraphicsUnit.Pixel, ia);
                }
            }

            small.Dispose();
            return blurred;
        }

        private Bitmap CatAnhHinhTron(Image anhGoc, int kichThuoc)
        {
            int size = Math.Max(60, kichThuoc);
            Bitmap vuong = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(vuong))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(anhGoc, 0, 0, size, size);
            }

            Bitmap ketQua = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(ketQua))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                using (TextureBrush brush = new TextureBrush(vuong))
                {
                    // Thu vào 2px để cắt sạch viền góc đen của file jpg
                    g.FillEllipse(brush, 2, 2, size - 4, size - 4);
                }
            }

            vuong.Dispose();
            return ketQua;
        }

        private void TrangTriNutBoGoc(Button btn, bool laNutChinh)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn.BackColor = Color.Transparent;
            btn.Cursor = Cursors.Hand;

            bool dangHover = false;
            btn.MouseEnter += (s, e) => { dangHover = true; btn.Invalidate(); };
            btn.MouseLeave += (s, e) => { dangHover = false; btn.Invalidate(); };

            btn.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                Rectangle rect = new Rectangle(1, 1, btn.Width - 3, btn.Height - 3);

                Color mauNen = laNutChinh
                    ? (dangHover ? Color.FromArgb(37, 72, 122) : Color.FromArgb(26, 48, 80))
                    : (dangHover ? Color.FromArgb(241, 245, 249) : Color.White);
                Color mauChu = laNutChinh ? Color.White : Color.FromArgb(30, 41, 59);

                using (GraphicsPath path = TaoDuongBoGoc(rect, 8))
                using (SolidBrush brush = new SolidBrush(mauNen))
                {
                    e.Graphics.FillPath(brush, path);
                    if (!laNutChinh)
                    {
                        using (Pen pen = new Pen(Color.FromArgb(203, 213, 225), 1f))
                            e.Graphics.DrawPath(pen, path);
                    }
                }

                TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, rect, mauChu,
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
        }

        private GraphicsPath TaoDuongBoGoc(Rectangle rect, int radius)
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
