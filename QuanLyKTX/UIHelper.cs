using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKTX
{
    public static class UIHelper
    {
        public static void LamDepFormQuanLy(Form form)
        {
            if (form == null) return;
            BatDoubleBuffer(form);
            form.BackColor = Color.FromArgb(241, 245, 249);

            var groupBoxes = form.Controls.OfType<GroupBox>().OrderBy(g => g.Top).ToList();

            // Căn thẳng mép trái & mép phải các khung GroupBox
            if (groupBoxes.Count >= 2)
            {
                int leftChuan = groupBoxes[0].Left;
                int widthChuan = groupBoxes[0].Width;
                for (int i = 1; i < groupBoxes.Count; i++)
                {
                    groupBoxes[i].Left = leftChuan;
                    groupBoxes[i].Width = widthChuan;
                }
            }

            foreach (GroupBox grp in groupBoxes)
            {
                TrangTriGroupBox(grp, form.BackColor);
            }

            TrangTriControlsBenTrong(form);
        }

        private static void BatDoubleBuffer(Control ctrl)
        {
            PropertyInfo prop = typeof(Control).GetProperty(
                "DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            if (prop != null) prop.SetValue(ctrl, true, null);
        }

        private static void TrangTriControlsBenTrong(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button btn)
                {
                    TrangTriNut(btn);
                }
                else if (c is DataGridView dgv)
                {
                    TrangTriDataGridView(dgv);
                }
                else if (c is TextBox tb)
                {
                    TrangTriTextBox(tb);
                }
                else if (c is ComboBox cbo)
                {
                    cbo.Font = new Font("Segoe UI", 9.5f);
                    cbo.FlatStyle = FlatStyle.Flat;
                }
                else if (c is DateTimePicker dtp)
                {
                    dtp.Font = new Font("Segoe UI", 9.5f);
                }
                else if (c is RadioButton rb)
                {
                    rb.BackColor = Color.Transparent;
                    rb.ForeColor = Color.FromArgb(30, 41, 59);
                    rb.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
                }
                else if (c is Label lbl)
                {
                    lbl.BackColor = Color.Transparent;
                    lbl.ForeColor = Color.FromArgb(51, 65, 85);
                    if (lbl.Font.Size < 10f)
                        lbl.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                }

                if (c.HasChildren && !(c is DataGridView))
                {
                    TrangTriControlsBenTrong(c);
                }
            }
        }

        private static void TrangTriGroupBox(GroupBox grp, Color nenForm)
        {
            BatDoubleBuffer(grp);
            string tieuDe = grp.Text;
            DataGridView dgvCon = grp.Controls.OfType<DataGridView>().FirstOrDefault();

            grp.Paint += (s, e) =>
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(nenForm);

                Rectangle cardRect = new Rectangle(1, 1, grp.Width - 3, grp.Height - 3);
                using (GraphicsPath path = TaoBoGoc(cardRect, 12))
                using (SolidBrush whiteBg = new SolidBrush(Color.White))
                using (Pen borderPen = new Pen(Color.FromArgb(226, 232, 240), 1.2f))
                {
                    g.FillPath(whiteBg, path);
                    g.DrawPath(borderPen, path);
                }

                Rectangle bar = new Rectangle(16, 13, 4, 18);
                using (GraphicsPath barPath = TaoBoGoc(bar, 2))
                using (SolidBrush barBrush = new SolidBrush(Color.FromArgb(37, 99, 235)))
                {
                    g.FillPath(barBrush, barPath);
                }

                using (Font titleFont = new Font("Segoe UI", 10.5f, FontStyle.Bold))
                {
                    TextRenderer.DrawText(g, tieuDe, titleFont, new Point(26, 12), Color.FromArgb(15, 23, 42));
                }

                using (Pen sepPen = new Pen(Color.FromArgb(241, 245, 249), 1f))
                {
                    g.DrawLine(sepPen, 16, 39, grp.Width - 16, 39);
                }

                // Badge đếm tổng số dòng ở góc phải thẻ danh sách
                if (dgvCon != null)
                {
                    int tongDong = dgvCon.AllowUserToAddRows
                        ? Math.Max(0, dgvCon.Rows.Count - 1)
                        : dgvCon.Rows.Count;
                    string badgeText = "Tổng: " + tongDong + " mục";

                    using (Font badgeFont = new Font("Segoe UI", 8.5f, FontStyle.Bold))
                    {
                        Size textSz = TextRenderer.MeasureText(badgeText, badgeFont);
                        Rectangle badgeRect = new Rectangle(
                            grp.Width - textSz.Width - 30, 10, textSz.Width + 14, 24);
                        using (GraphicsPath bp = TaoBoGoc(badgeRect, 11))
                        using (SolidBrush bb = new SolidBrush(Color.FromArgb(239, 246, 255)))
                        {
                            g.FillPath(bb, bp);
                        }
                        TextRenderer.DrawText(g, badgeText, badgeFont, badgeRect,
                            Color.FromArgb(37, 99, 235),
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    }
                }

                // Viền nhấn màu xanh khi ô TextBox được chọn
                foreach (TextBox tb in grp.Controls.OfType<TextBox>())
                {
                    if (!tb.Visible) continue;
                    Color borderCol = tb.Focused
                        ? Color.FromArgb(37, 99, 235)
                        : (tb.Enabled ? Color.FromArgb(203, 213, 225) : Color.FromArgb(226, 232, 240));
                    float penW = tb.Focused ? 1.8f : 1f;
                    Rectangle tbRect = new Rectangle(tb.Left - 2, tb.Top - 2, tb.Width + 3, tb.Height + 3);
                    using (Pen p = new Pen(borderCol, penW))
                    {
                        g.DrawRectangle(p, tbRect);
                    }
                }
            };

            if (dgvCon != null)
            {
                dgvCon.Location = new Point(16, 48);
                dgvCon.Size = new Size(grp.ClientSize.Width - 32, grp.ClientSize.Height - 62);
                dgvCon.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                dgvCon.DataBindingComplete += (s, e) => grp.Invalidate();
                dgvCon.RowsAdded += (s, e) => grp.Invalidate();
                dgvCon.RowsRemoved += (s, e) => grp.Invalidate();
            }
        }

        private static void TrangTriTextBox(TextBox tb)
        {
            bool laTongTien = tb.Name.IndexOf("TongTien", StringComparison.OrdinalIgnoreCase) >= 0;
            tb.Font = new Font("Segoe UI", 9.5f, laTongTien ? FontStyle.Bold : FontStyle.Regular);
            tb.BorderStyle = BorderStyle.FixedSingle;
            tb.BackColor = tb.Enabled ? Color.White : Color.FromArgb(248, 250, 252);
            if (laTongTien) tb.ForeColor = Color.FromArgb(185, 28, 28);

            tb.Enter += (s, e) => { if (tb.Parent != null) tb.Parent.Invalidate(); };
            tb.Leave += (s, e) => { if (tb.Parent != null) tb.Parent.Invalidate(); };
            tb.EnabledChanged += (s, e) =>
            {
                tb.BackColor = tb.Enabled ? Color.White : Color.FromArgb(248, 250, 252);
                if (tb.Parent != null) tb.Parent.Invalidate();
            };
        }

        private static void TrangTriNut(Button btn)
        {
            BatDoubleBuffer(btn);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btn.Cursor = Cursors.Hand;
            btn.Height = Math.Max(btn.Height, 34);

            string t = btn.Text.ToLower();
            Color bgNormal = Color.FromArgb(239, 246, 255);
            Color bgHover = Color.FromArgb(219, 234, 254);
            Color borderCol = Color.FromArgb(59, 130, 246);
            Color textCol = Color.FromArgb(29, 78, 216);

            if (t.Contains("thêm"))
            {
                bgNormal = Color.FromArgb(239, 246, 255);
                bgHover = Color.FromArgb(219, 234, 254);
                borderCol = Color.FromArgb(59, 130, 246);
                textCol = Color.FromArgb(29, 78, 216);
            }
            else if (t.Contains("sửa"))
            {
                bgNormal = Color.FromArgb(255, 251, 235);
                bgHover = Color.FromArgb(254, 243, 199);
                borderCol = Color.FromArgb(245, 158, 11);
                textCol = Color.FromArgb(180, 83, 9);
            }
            else if (t.Contains("xóa"))
            {
                bgNormal = Color.FromArgb(254, 242, 242);
                bgHover = Color.FromArgb(254, 226, 226);
                borderCol = Color.FromArgb(239, 68, 68);
                textCol = Color.FromArgb(185, 28, 28);
            }
            else if (t.Contains("lưu"))
            {
                bgNormal = Color.FromArgb(236, 253, 245);
                bgHover = Color.FromArgb(209, 250, 229);
                borderCol = Color.FromArgb(16, 185, 129);
                textCol = Color.FromArgb(4, 120, 87);
            }
            else if (t.Contains("hủy"))
            {
                bgNormal = Color.FromArgb(248, 250, 252);
                bgHover = Color.FromArgb(226, 232, 240);
                borderCol = Color.FromArgb(148, 163, 184);
                textCol = Color.FromArgb(71, 85, 105);
            }
            else if (t.Contains("thoát"))
            {
                bgNormal = Color.FromArgb(255, 241, 242);
                bgHover = Color.FromArgb(255, 228, 230);
                borderCol = Color.FromArgb(225, 29, 72);
                textCol = Color.FromArgb(190, 18, 60);
            }
            else if (t.Contains("xuất") || t.Contains("in"))
            {
                bgNormal = Color.FromArgb(238, 242, 255);
                bgHover = Color.FromArgb(224, 231, 255);
                borderCol = Color.FromArgb(99, 102, 241);
                textCol = Color.FromArgb(67, 56, 202);
            }
            else if (t.Contains("tìm") || t.Contains("lọc"))
            {
                bgNormal = Color.FromArgb(224, 242, 254);
                bgHover = Color.FromArgb(186, 230, 253);
                borderCol = Color.FromArgb(2, 132, 199);
                textCol = Color.FromArgb(3, 105, 161);
            }

            bool isHover = false;
            btn.MouseEnter += (s, e) => { isHover = true; btn.Invalidate(); };
            btn.MouseLeave += (s, e) => { isHover = false; btn.Invalidate(); };
            btn.EnabledChanged += (s, e) => btn.Invalidate();

            btn.Paint += (s, e) =>
            {
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                Color parentBg = btn.Parent != null ? btn.Parent.BackColor : Color.FromArgb(241, 245, 249);
                g.Clear(parentBg);

                Rectangle rect = new Rectangle(1, 1, btn.Width - 3, btn.Height - 3);
                Color fill = !btn.Enabled
                    ? Color.FromArgb(241, 245, 249)
                    : (isHover ? bgHover : bgNormal);
                Color border = !btn.Enabled ? Color.FromArgb(203, 213, 225) : borderCol;
                Color txt = !btn.Enabled ? Color.FromArgb(148, 163, 184) : textCol;

                using (GraphicsPath path = TaoBoGoc(rect, 8))
                using (SolidBrush br = new SolidBrush(fill))
                using (Pen pen = new Pen(border, 1.3f))
                {
                    g.FillPath(br, path);
                    g.DrawPath(pen, path);
                }

                using (Font drawFont = new Font("Segoe UI", 9.5f, FontStyle.Bold))
                {
                    string label = btn.Text.Trim();
                    Size textSize = TextRenderer.MeasureText(label, drawFont);

                    if (btn.Image != null)
                    {
                        int iconSz = 16;
                        int gap = 6;
                        int totalW = iconSz + gap + textSize.Width;
                        int startX = (btn.Width - totalW) / 2;
                        int iconY = (btn.Height - iconSz) / 2;

                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        if (btn.Enabled)
                        {
                            g.DrawImage(btn.Image, startX, iconY, iconSz, iconSz);
                        }
                        else
                        {
                            ColorMatrix cm = new ColorMatrix { Matrix33 = 0.35f };
                            using (ImageAttributes ia = new ImageAttributes())
                            {
                                ia.SetColorMatrix(cm, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                                g.DrawImage(btn.Image, new Rectangle(startX, iconY, iconSz, iconSz),
                                    0, 0, btn.Image.Width, btn.Image.Height, GraphicsUnit.Pixel, ia);
                            }
                        }

                        Rectangle textRect = new Rectangle(startX + iconSz + gap, 0, textSize.Width + 6, btn.Height);
                        TextRenderer.DrawText(g, label, drawFont, textRect, txt,
                            TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                    }
                    else
                    {
                        TextRenderer.DrawText(g, label, drawFont, rect, txt,
                            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                    }
                }
            };
        }

        private static void TrangTriDataGridView(DataGridView dgv)
        {
            BatDoubleBuffer(dgv);
            dgv.BorderStyle = BorderStyle.None;
            dgv.BackgroundColor = Color.White;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = Color.FromArgb(241, 245, 249);
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.AllowUserToResizeRows = false;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersHeight = 38;
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                Padding = new Padding(8, 0, 8, 0),
                Alignment = DataGridViewContentAlignment.MiddleLeft
            };

            dgv.RowTemplate.Height = 34;
            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.White,
                ForeColor = Color.FromArgb(30, 41, 59),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                SelectionBackColor = Color.FromArgb(239, 246, 255),
                SelectionForeColor = Color.FromArgb(29, 78, 216),
                Padding = new Padding(8, 0, 8, 0)
            };
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252);

            Action apDungCot = () =>
            {
                foreach (DataGridViewRow row in dgv.Rows) row.Height = 34;

                if (dgv.Columns.Count > 0)
                {
                    dgv.Columns[0].DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                    dgv.Columns[0].DefaultCellStyle.ForeColor = Color.FromArgb(30, 64, 175);
                }

                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    string h = col.HeaderText.ToLower();
                    if (h.Contains("giá") || h.Contains("tiền"))
                    {
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                        if (h.Contains("tổng"))
                        {
                            col.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f, FontStyle.Bold);
                            col.DefaultCellStyle.ForeColor = Color.FromArgb(185, 28, 28);
                        }
                    }
                    else if (h.Contains("trạng thái") || h.Contains("giới tính") || h.Contains("loại")
                          || h.Contains("sức chứa") || h.Contains("số sv") || h.Contains("ngày") || h.Contains("tháng"))
                    {
                        col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                }
            };

            // Tô màu badge tự động cho Trạng thái (Đăng ký / Hóa đơn) và Giới tính / Loại phòng
            dgv.CellFormatting += (s, e) =>
            {
                if (e.RowIndex < 0 || e.Value == null) return;
                string h = dgv.Columns[e.ColumnIndex].HeaderText.ToLower();
                string val = e.Value.ToString();

                if (h.Contains("trạng thái"))
                {
                    e.CellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                    if (val.Contains("Đang ở") || val.Contains("Đã đóng"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(5, 150, 105);
                        e.CellStyle.BackColor = Color.FromArgb(236, 253, 245);
                    }
                    else if (val.Contains("Chưa đóng"))
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                        e.CellStyle.BackColor = Color.FromArgb(254, 242, 242);
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.FromArgb(100, 116, 139);
                        e.CellStyle.BackColor = Color.FromArgb(241, 245, 249);
                    }
                }
                else if (h.Contains("giới tính") || h.Contains("loại"))
                {
                    e.CellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                    e.CellStyle.ForeColor = val.Contains("Nữ")
                        ? Color.FromArgb(219, 39, 119)
                        : Color.FromArgb(37, 99, 235);
                }
            };

            apDungCot();
            dgv.DataBindingComplete += (s, e) => apDungCot();
        }

        private static GraphicsPath TaoBoGoc(Rectangle rect, int radius)
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
