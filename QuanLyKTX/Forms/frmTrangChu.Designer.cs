namespace QuanLyKTX.Forms
{
    partial class frmTrangChu
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.chartPhong = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartDienNuoc = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvThongKePhong = new System.Windows.Forms.DataGridView();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblIcon4 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPhongDay = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblIcon2 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTongSV = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblIcon3 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPhongTrong = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblIcon1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTongPhong = new System.Windows.Forms.Label();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDienNuoc)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKePhong)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlContent.Controls.Add(this.chartPhong);
            this.pnlContent.Controls.Add(this.chartDienNuoc);
            this.pnlContent.Controls.Add(this.groupBox1);
            this.pnlContent.Controls.Add(this.panel4);
            this.pnlContent.Controls.Add(this.panel3);
            this.pnlContent.Controls.Add(this.panel2);
            this.pnlContent.Controls.Add(this.panel1);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1060, 824);
            this.pnlContent.TabIndex = 5;
            // 
            // chartPhong
            // 
            chartArea1.Name = "ChartArea1";
            this.chartPhong.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartPhong.Legends.Add(legend1);
            this.chartPhong.Location = new System.Drawing.Point(605, 425);
            this.chartPhong.Name = "chartPhong";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Doughnut;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartPhong.Series.Add(series1);
            this.chartPhong.Size = new System.Drawing.Size(414, 363);
            this.chartPhong.TabIndex = 6;
            this.chartPhong.Text = "chart2";
            // 
            // chartDienNuoc
            // 
            chartArea2.Name = "ChartArea1";
            this.chartDienNuoc.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartDienNuoc.Legends.Add(legend2);
            this.chartDienNuoc.Location = new System.Drawing.Point(29, 425);
            this.chartDienNuoc.Name = "chartDienNuoc";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartDienNuoc.Series.Add(series2);
            this.chartDienNuoc.Size = new System.Drawing.Size(538, 363);
            this.chartDienNuoc.TabIndex = 5;
            this.chartDienNuoc.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvThongKePhong);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 131);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1036, 279);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách tình trạng phòng";
            // 
            // dgvThongKePhong
            // 
            this.dgvThongKePhong.AllowUserToAddRows = false;
            this.dgvThongKePhong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvThongKePhong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvThongKePhong.BackgroundColor = System.Drawing.Color.White;
            this.dgvThongKePhong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThongKePhong.Location = new System.Drawing.Point(8, 45);
            this.dgvThongKePhong.Name = "dgvThongKePhong";
            this.dgvThongKePhong.ReadOnly = true;
            this.dgvThongKePhong.RowHeadersVisible = false;
            this.dgvThongKePhong.RowHeadersWidth = 51;
            this.dgvThongKePhong.RowTemplate.Height = 24;
            this.dgvThongKePhong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThongKePhong.Size = new System.Drawing.Size(1022, 219);
            this.dgvThongKePhong.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.panel4.Controls.Add(this.lblIcon4);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.lblPhongDay);
            this.panel4.Location = new System.Drawing.Point(820, 22);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(215, 100);
            this.panel4.TabIndex = 3;
            // 
            // lblIcon4
            // 
            this.lblIcon4.AutoSize = true;
            this.lblIcon4.Font = new System.Drawing.Font("Segoe MDL2 Assets", 31.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIcon4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
            this.lblIcon4.Location = new System.Drawing.Point(99, 46);
            this.lblIcon4.Name = "lblIcon4";
            this.lblIcon4.Size = new System.Drawing.Size(129, 53);
            this.lblIcon4.TabIndex = 6;
            this.lblIcon4.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(7, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 28);
            this.label4.TabIndex = 5;
            this.label4.Text = "PHÒNG ĐÃ ĐẦY";
            // 
            // lblPhongDay
            // 
            this.lblPhongDay.AutoSize = true;
            this.lblPhongDay.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhongDay.ForeColor = System.Drawing.Color.White;
            this.lblPhongDay.Location = new System.Drawing.Point(3, 52);
            this.lblPhongDay.Name = "lblPhongDay";
            this.lblPhongDay.Size = new System.Drawing.Size(331, 54);
            this.lblPhongDay.TabIndex = 4;
            this.lblPhongDay.Text = "PHÒNG ĐÃ ĐẦY";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.panel3.Controls.Add(this.lblIcon2);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.lblTongSV);
            this.panel3.Location = new System.Drawing.Point(283, 22);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(215, 100);
            this.panel3.TabIndex = 2;
            // 
            // lblIcon2
            // 
            this.lblIcon2.AutoSize = true;
            this.lblIcon2.Font = new System.Drawing.Font("Segoe MDL2 Assets", 31.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIcon2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
            this.lblIcon2.Location = new System.Drawing.Point(97, 46);
            this.lblIcon2.Name = "lblIcon2";
            this.lblIcon2.Size = new System.Drawing.Size(129, 53);
            this.lblIcon2.TabIndex = 3;
            this.lblIcon2.Text = "label5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(7, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "SINH VIÊN ĐANG Ở";
            // 
            // lblTongSV
            // 
            this.lblTongSV.AutoSize = true;
            this.lblTongSV.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongSV.ForeColor = System.Drawing.Color.White;
            this.lblTongSV.Location = new System.Drawing.Point(3, 52);
            this.lblTongSV.Name = "lblTongSV";
            this.lblTongSV.Size = new System.Drawing.Size(396, 54);
            this.lblTongSV.TabIndex = 1;
            this.lblTongSV.Text = "SINH VIÊN ĐANG Ở";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.panel2.Controls.Add(this.lblIcon3);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.lblPhongTrong);
            this.panel2.Location = new System.Drawing.Point(550, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(215, 100);
            this.panel2.TabIndex = 1;
            // 
            // lblIcon3
            // 
            this.lblIcon3.AutoSize = true;
            this.lblIcon3.Font = new System.Drawing.Font("Segoe MDL2 Assets", 31.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIcon3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
            this.lblIcon3.Location = new System.Drawing.Point(97, 46);
            this.lblIcon3.Name = "lblIcon3";
            this.lblIcon3.Size = new System.Drawing.Size(129, 53);
            this.lblIcon3.TabIndex = 4;
            this.lblIcon3.Text = "label5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(7, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 28);
            this.label3.TabIndex = 2;
            this.label3.Text = "PHÒNG CÒN CHỖ";
            // 
            // lblPhongTrong
            // 
            this.lblPhongTrong.AutoSize = true;
            this.lblPhongTrong.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhongTrong.ForeColor = System.Drawing.Color.White;
            this.lblPhongTrong.Location = new System.Drawing.Point(3, 52);
            this.lblPhongTrong.Name = "lblPhongTrong";
            this.lblPhongTrong.Size = new System.Drawing.Size(363, 54);
            this.lblPhongTrong.TabIndex = 1;
            this.lblPhongTrong.Text = "PHÒNG CÒN CHỖ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.panel1.Controls.Add(this.lblIcon1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lblTongPhong);
            this.panel1.Location = new System.Drawing.Point(29, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 100);
            this.panel1.TabIndex = 0;
            // 
            // lblIcon1
            // 
            this.lblIcon1.AutoSize = true;
            this.lblIcon1.Font = new System.Drawing.Font("Segoe MDL2 Assets", 31.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIcon1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(240)))), ((int)(((byte)(250)))));
            this.lblIcon1.Location = new System.Drawing.Point(99, 46);
            this.lblIcon1.Name = "lblIcon1";
            this.lblIcon1.Size = new System.Drawing.Size(129, 53);
            this.lblIcon1.TabIndex = 2;
            this.lblIcon1.Text = "label5";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(7, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 28);
            this.label1.TabIndex = 1;
            this.label1.Text = "TỔNG SỐ PHÒNG";
            // 
            // lblTongPhong
            // 
            this.lblTongPhong.AutoSize = true;
            this.lblTongPhong.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongPhong.ForeColor = System.Drawing.Color.White;
            this.lblTongPhong.Location = new System.Drawing.Point(3, 52);
            this.lblTongPhong.Name = "lblTongPhong";
            this.lblTongPhong.Size = new System.Drawing.Size(356, 54);
            this.lblTongPhong.TabIndex = 0;
            this.lblTongPhong.Text = "TỔNG SỐ PHÒNG";
            this.lblTongPhong.Click += new System.EventHandler(this.label1_Click);
            // 
            // frmTrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1060, 824);
            this.Controls.Add(this.pnlContent);
            this.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "frmTrangChu";
            this.Text = "frmTrangChu";
            this.Load += new System.EventHandler(this.frmTrangChu_Load);
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDienNuoc)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKePhong)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTongPhong;
        private System.Windows.Forms.Label lblTongSV;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvThongKePhong;
        private System.Windows.Forms.Label lblPhongDay;
        private System.Windows.Forms.Label lblPhongTrong;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblIcon2;
        private System.Windows.Forms.Label lblIcon1;
        private System.Windows.Forms.Label lblIcon4;
        private System.Windows.Forms.Label lblIcon3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPhong;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDienNuoc;
    }
}