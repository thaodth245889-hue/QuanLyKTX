namespace QuanLyKTX.Forms
{
    partial class frmTienPhong
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTienNuoc = new System.Windows.Forms.TextBox();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.txtNuocMoi = new System.Windows.Forms.TextBox();
            this.txtNuocCu = new System.Windows.Forms.TextBox();
            this.txtTienDien = new System.Windows.Forms.TextBox();
            this.txtDienMoi = new System.Windows.Forms.TextBox();
            this.txtDienCu = new System.Windows.Forms.TextBox();
            this.txtTienPhong = new System.Windows.Forms.TextBox();
            this.txtSoSV = new System.Windows.Forms.TextBox();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.txtThangNam = new System.Windows.Forms.TextBox();
            this.cboPhongTP = new System.Windows.Forms.ComboBox();
            this.txtMaTien = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.btnLoc = new System.Windows.Forms.Button();
            this.txtLocThangNam = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvTienPhong = new System.Windows.Forms.DataGridView();
            this.btnXuatHoaDon = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTienPhong)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtTienNuoc);
            this.groupBox1.Controls.Add(this.cboTrangThai);
            this.groupBox1.Controls.Add(this.txtTongTien);
            this.groupBox1.Controls.Add(this.txtNuocMoi);
            this.groupBox1.Controls.Add(this.txtNuocCu);
            this.groupBox1.Controls.Add(this.txtTienDien);
            this.groupBox1.Controls.Add(this.txtDienMoi);
            this.groupBox1.Controls.Add(this.txtDienCu);
            this.groupBox1.Controls.Add(this.txtTienPhong);
            this.groupBox1.Controls.Add(this.txtSoSV);
            this.groupBox1.Controls.Add(this.dtpDenNgay);
            this.groupBox1.Controls.Add(this.dtpTuNgay);
            this.groupBox1.Controls.Add(this.txtThangNam);
            this.groupBox1.Controls.Add(this.cboPhongTP);
            this.groupBox1.Controls.Add(this.txtMaTien);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1035, 329);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin Hóa đơn";
            // 
            // txtTienNuoc
            // 
            this.txtTienNuoc.Location = new System.Drawing.Point(834, 150);
            this.txtTienNuoc.Name = "txtTienNuoc";
            this.txtTienNuoc.ReadOnly = true;
            this.txtTienNuoc.Size = new System.Drawing.Size(141, 30);
            this.txtTienNuoc.TabIndex = 37;
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(834, 251);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(141, 31);
            this.cboTrangThai.TabIndex = 36;
            // 
            // txtTongTien
            // 
            this.txtTongTien.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTongTien.Location = new System.Drawing.Point(834, 202);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.Size = new System.Drawing.Size(141, 30);
            this.txtTongTien.TabIndex = 35;
            // 
            // txtNuocMoi
            // 
            this.txtNuocMoi.Location = new System.Drawing.Point(834, 100);
            this.txtNuocMoi.Name = "txtNuocMoi";
            this.txtNuocMoi.Size = new System.Drawing.Size(141, 30);
            this.txtNuocMoi.TabIndex = 34;
            // 
            // txtNuocCu
            // 
            this.txtNuocCu.Location = new System.Drawing.Point(834, 50);
            this.txtNuocCu.Name = "txtNuocCu";
            this.txtNuocCu.Size = new System.Drawing.Size(141, 30);
            this.txtNuocCu.TabIndex = 33;
            // 
            // txtTienDien
            // 
            this.txtTienDien.Location = new System.Drawing.Point(498, 252);
            this.txtTienDien.Name = "txtTienDien";
            this.txtTienDien.ReadOnly = true;
            this.txtTienDien.Size = new System.Drawing.Size(141, 30);
            this.txtTienDien.TabIndex = 32;
            // 
            // txtDienMoi
            // 
            this.txtDienMoi.Location = new System.Drawing.Point(498, 202);
            this.txtDienMoi.Name = "txtDienMoi";
            this.txtDienMoi.Size = new System.Drawing.Size(141, 30);
            this.txtDienMoi.TabIndex = 31;
            // 
            // txtDienCu
            // 
            this.txtDienCu.Location = new System.Drawing.Point(498, 146);
            this.txtDienCu.Name = "txtDienCu";
            this.txtDienCu.Size = new System.Drawing.Size(141, 30);
            this.txtDienCu.TabIndex = 30;
            // 
            // txtTienPhong
            // 
            this.txtTienPhong.Location = new System.Drawing.Point(498, 100);
            this.txtTienPhong.Name = "txtTienPhong";
            this.txtTienPhong.ReadOnly = true;
            this.txtTienPhong.Size = new System.Drawing.Size(141, 30);
            this.txtTienPhong.TabIndex = 29;
            // 
            // txtSoSV
            // 
            this.txtSoSV.Location = new System.Drawing.Point(498, 50);
            this.txtSoSV.Name = "txtSoSV";
            this.txtSoSV.ReadOnly = true;
            this.txtSoSV.Size = new System.Drawing.Size(141, 30);
            this.txtSoSV.TabIndex = 28;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(154, 255);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(141, 30);
            this.dtpDenNgay.TabIndex = 27;
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(154, 205);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(141, 30);
            this.dtpTuNgay.TabIndex = 26;
            // 
            // txtThangNam
            // 
            this.txtThangNam.Location = new System.Drawing.Point(154, 146);
            this.txtThangNam.Name = "txtThangNam";
            this.txtThangNam.Size = new System.Drawing.Size(141, 30);
            this.txtThangNam.TabIndex = 25;
            // 
            // cboPhongTP
            // 
            this.cboPhongTP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhongTP.FormattingEnabled = true;
            this.cboPhongTP.Location = new System.Drawing.Point(154, 97);
            this.cboPhongTP.Name = "cboPhongTP";
            this.cboPhongTP.Size = new System.Drawing.Size(141, 31);
            this.cboPhongTP.TabIndex = 24;
            this.cboPhongTP.SelectedIndexChanged += new System.EventHandler(this.cboPhongTP_SelectedIndexChanged);
            // 
            // txtMaTien
            // 
            this.txtMaTien.Location = new System.Drawing.Point(154, 50);
            this.txtMaTien.Name = "txtMaTien";
            this.txtMaTien.ReadOnly = true;
            this.txtMaTien.Size = new System.Drawing.Size(141, 30);
            this.txtMaTien.TabIndex = 23;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(687, 255);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(91, 23);
            this.label15.TabIndex = 22;
            this.label15.Text = "Trạng thái:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(366, 255);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(84, 23);
            this.label10.TabIndex = 22;
            this.label10.Text = "Tiền điện:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(687, 205);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(143, 23);
            this.label14.TabIndex = 21;
            this.label14.Text = "Tổng tiền (VNĐ) :";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(687, 149);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(89, 23);
            this.label13.TabIndex = 20;
            this.label13.Text = "Tiền nước:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(366, 205);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 23);
            this.label9.TabIndex = 21;
            this.label9.Text = "Điện mới (kWh) :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(687, 100);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(125, 23);
            this.label12.TabIndex = 19;
            this.label12.Text = "Nước mới (m³):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(366, 149);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 23);
            this.label6.TabIndex = 20;
            this.label6.Text = "Điện cũ (kWh) :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(687, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(114, 23);
            this.label11.TabIndex = 18;
            this.label11.Text = "Nước cũ (m³):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(366, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 23);
            this.label5.TabIndex = 19;
            this.label5.Text = "Tiền phòng:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(366, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 23);
            this.label4.TabIndex = 18;
            this.label4.Text = "Số SV:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(42, 255);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 23);
            this.label8.TabIndex = 17;
            this.label8.Text = "Đến ngày:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(42, 205);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 23);
            this.label7.TabIndex = 16;
            this.label7.Text = "Từ ngày:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Tháng/Năm:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Phòng:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã HĐ:";
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.Firebrick;
            this.btnThoat.Location = new System.Drawing.Point(579, 347);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(107, 41);
            this.btnThoat.TabIndex = 24;
            this.btnThoat.Text = "🚪 Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.SystemColors.Info;
            this.btnHuy.Location = new System.Drawing.Point(466, 347);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(107, 41);
            this.btnHuy.TabIndex = 23;
            this.btnHuy.Text = "↩ Hủy";
            this.btnHuy.UseVisualStyleBackColor = false;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.SystemColors.Info;
            this.btnLuu.Location = new System.Drawing.Point(353, 347);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(107, 41);
            this.btnLuu.TabIndex = 22;
            this.btnLuu.Text = "💾 Lưu";
            this.btnLuu.UseVisualStyleBackColor = false;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.SystemColors.Info;
            this.btnXoa.Location = new System.Drawing.Point(240, 347);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(107, 41);
            this.btnXoa.TabIndex = 21;
            this.btnXoa.Text = "❌ Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.SystemColors.Info;
            this.btnSua.Location = new System.Drawing.Point(127, 347);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(107, 41);
            this.btnSua.TabIndex = 20;
            this.btnSua.Text = "✏️ Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.SystemColors.Info;
            this.btnThem.Location = new System.Drawing.Point(14, 347);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(107, 41);
            this.btnThem.TabIndex = 19;
            this.btnThem.Text = "➕ Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnLoc
            // 
            this.btnLoc.Location = new System.Drawing.Point(469, 397);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(107, 41);
            this.btnLoc.TabIndex = 27;
            this.btnLoc.Text = "🔍 Lọc";
            this.btnLoc.UseVisualStyleBackColor = true;
            this.btnLoc.Click += new System.EventHandler(this.btnLoc_Click);
            // 
            // txtLocThangNam
            // 
            this.txtLocThangNam.Location = new System.Drawing.Point(149, 403);
            this.txtLocThangNam.Multiline = true;
            this.txtLocThangNam.Name = "txtLocThangNam";
            this.txtLocThangNam.Size = new System.Drawing.Size(314, 32);
            this.txtLocThangNam.TabIndex = 26;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(16, 406);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(137, 23);
            this.label16.TabIndex = 25;
            this.label16.Text = "Lọc Tháng/Năm:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvTienPhong);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 444);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1035, 368);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách Hóa đơn";
            // 
            // dgvTienPhong
            // 
            this.dgvTienPhong.AllowUserToAddRows = false;
            this.dgvTienPhong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTienPhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTienPhong.BackgroundColor = System.Drawing.Color.White;
            this.dgvTienPhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTienPhong.Location = new System.Drawing.Point(21, 43);
            this.dgvTienPhong.Name = "dgvTienPhong";
            this.dgvTienPhong.ReadOnly = true;
            this.dgvTienPhong.RowHeadersVisible = false;
            this.dgvTienPhong.RowHeadersWidth = 51;
            this.dgvTienPhong.RowTemplate.Height = 24;
            this.dgvTienPhong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTienPhong.Size = new System.Drawing.Size(994, 299);
            this.dgvTienPhong.TabIndex = 0;
            this.dgvTienPhong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTienPhong_CellClick);
            // 
            // btnXuatHoaDon
            // 
            this.btnXuatHoaDon.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.btnXuatHoaDon.Location = new System.Drawing.Point(692, 347);
            this.btnXuatHoaDon.Name = "btnXuatHoaDon";
            this.btnXuatHoaDon.Size = new System.Drawing.Size(182, 41);
            this.btnXuatHoaDon.TabIndex = 29;
            this.btnXuatHoaDon.Text = "🖨 Xuất hóa đơn";
            this.btnXuatHoaDon.UseVisualStyleBackColor = false;
            this.btnXuatHoaDon.Click += new System.EventHandler(this.btnXuatHoaDon_Click);
            // 
            // frmTienPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1060, 824);
            this.Controls.Add(this.btnXuatHoaDon);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnLoc);
            this.Controls.Add(this.txtLocThangNam);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmTienPhong";
            this.Text = "frmTienPhong";
            this.Load += new System.EventHandler(this.frmTienPhong_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTienPhong)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtThangNam;
        private System.Windows.Forms.ComboBox cboPhongTP;
        private System.Windows.Forms.TextBox txtMaTien;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDienCu;
        private System.Windows.Forms.TextBox txtTienPhong;
        private System.Windows.Forms.TextBox txtSoSV;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.TextBox txtNuocMoi;
        private System.Windows.Forms.TextBox txtNuocCu;
        private System.Windows.Forms.TextBox txtTienDien;
        private System.Windows.Forms.TextBox txtDienMoi;
        private System.Windows.Forms.TextBox txtTienNuoc;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Button btnLoc;
        private System.Windows.Forms.TextBox txtLocThangNam;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvTienPhong;
        private System.Windows.Forms.Button btnXuatHoaDon;
    }
}