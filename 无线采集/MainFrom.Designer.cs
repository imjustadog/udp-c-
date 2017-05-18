namespace 无线采集
{
    partial class MainFrom
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrom));
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全消ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.低频设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.高频设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fFT设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.校准ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.调零ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置采集间隔ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置当前时间ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_zero_LF4 = new System.Windows.Forms.Button();
            this.btn_zero_LF3 = new System.Windows.Forms.Button();
            this.btn_zero_LF2 = new System.Windows.Forms.Button();
            this.btn_zero_LF1 = new System.Windows.Forms.Button();
            this.btn_zero_HF1 = new System.Windows.Forms.Button();
            this.txtTestProgram = new System.Windows.Forms.TextBox();
            this.dataGridState = new System.Windows.Forms.DataGridView();
            this.btn_test_connect = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridState_His = new System.Windows.Forms.DataGridView();
            this.btn_Open_His = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.pic_fft = new System.Windows.Forms.PictureBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.graphShow = new ZedGraph.ZedGraphControl();
            this.graphAnlys = new ZedGraph.ZedGraphControl();
            this.chBLineShow1 = new System.Windows.Forms.CheckBox();
            this.btn_Scope = new System.Windows.Forms.Button();
            this.btn_Capture = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer_draw = new System.Windows.Forms.Timer(this.components);
            this.chBLineShow2 = new System.Windows.Forms.CheckBox();
            this.chBLineShow6 = new System.Windows.Forms.CheckBox();
            this.chBLineShow3 = new System.Windows.Forms.CheckBox();
            this.chBLineShow4 = new System.Windows.Forms.CheckBox();
            this.chBLineShow5 = new System.Windows.Forms.CheckBox();
            this.timer_state_online = new System.Windows.Forms.Timer(this.components);
            this.btn_choose_fft = new System.Windows.Forms.Button();
            this.btn_cancel_fft = new System.Windows.Forms.Button();
            this.btn_wipe_graph = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridState)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridState_His)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_fft)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 全消ToolStripMenuItem
            // 
            this.全消ToolStripMenuItem.Name = "全消ToolStripMenuItem";
            this.全消ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.低频设置ToolStripMenuItem,
            this.高频设置ToolStripMenuItem,
            this.fFT设置ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 低频设置ToolStripMenuItem
            // 
            this.低频设置ToolStripMenuItem.Name = "低频设置ToolStripMenuItem";
            this.低频设置ToolStripMenuItem.Size = new System.Drawing.Size(134, 24);
            this.低频设置ToolStripMenuItem.Text = "低频设置";
            this.低频设置ToolStripMenuItem.Click += new System.EventHandler(this.低频设置ToolStripMenuItem_Click);
            // 
            // 高频设置ToolStripMenuItem
            // 
            this.高频设置ToolStripMenuItem.Name = "高频设置ToolStripMenuItem";
            this.高频设置ToolStripMenuItem.Size = new System.Drawing.Size(134, 24);
            this.高频设置ToolStripMenuItem.Text = "高频设置";
            // 
            // fFT设置ToolStripMenuItem
            // 
            this.fFT设置ToolStripMenuItem.Name = "fFT设置ToolStripMenuItem";
            this.fFT设置ToolStripMenuItem.Size = new System.Drawing.Size(134, 24);
            this.fFT设置ToolStripMenuItem.Text = "FFT设置";
            this.fFT设置ToolStripMenuItem.Click += new System.EventHandler(this.fFT设置ToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.校准ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1260, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 校准ToolStripMenuItem
            // 
            this.校准ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.标定ToolStripMenuItem,
            this.调零ToolStripMenuItem});
            this.校准ToolStripMenuItem.Name = "校准ToolStripMenuItem";
            this.校准ToolStripMenuItem.Size = new System.Drawing.Size(49, 24);
            this.校准ToolStripMenuItem.Text = "校准";
            // 
            // 标定ToolStripMenuItem
            // 
            this.标定ToolStripMenuItem.Name = "标定ToolStripMenuItem";
            this.标定ToolStripMenuItem.Size = new System.Drawing.Size(134, 24);
            this.标定ToolStripMenuItem.Text = "标定";
            this.标定ToolStripMenuItem.Click += new System.EventHandler(this.标定ToolStripMenuItem_Click);
            // 
            // 调零ToolStripMenuItem
            // 
            this.调零ToolStripMenuItem.Name = "调零ToolStripMenuItem";
            this.调零ToolStripMenuItem.Size = new System.Drawing.Size(134, 24);
            this.调零ToolStripMenuItem.Text = "手动调零";
            this.调零ToolStripMenuItem.Click += new System.EventHandler(this.调零ToolStripMenuItem_Click);
            // 
            // 设置采集间隔ToolStripMenuItem
            // 
            this.设置采集间隔ToolStripMenuItem.Name = "设置采集间隔ToolStripMenuItem";
            this.设置采集间隔ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // 设置当前时间ToolStripMenuItem
            // 
            this.设置当前时间ToolStripMenuItem.Name = "设置当前时间ToolStripMenuItem";
            this.设置当前时间ToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 28);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1260, 641);
            this.splitContainer1.SplitterDistance = 308;
            this.splitContainer1.TabIndex = 5;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(308, 641);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.btn_zero_LF4);
            this.tabPage1.Controls.Add(this.btn_zero_LF3);
            this.tabPage1.Controls.Add(this.btn_zero_LF2);
            this.tabPage1.Controls.Add(this.btn_zero_LF1);
            this.tabPage1.Controls.Add(this.btn_zero_HF1);
            this.tabPage1.Controls.Add(this.txtTestProgram);
            this.tabPage1.Controls.Add(this.dataGridState);
            this.tabPage1.Controls.Add(this.btn_test_connect);
            this.tabPage1.Controls.Add(this.button_save);
            this.tabPage1.Controls.Add(this.btn_Clear);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(300, 613);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "显示";
            // 
            // btn_zero_LF4
            // 
            this.btn_zero_LF4.Location = new System.Drawing.Point(216, 143);
            this.btn_zero_LF4.Name = "btn_zero_LF4";
            this.btn_zero_LF4.Size = new System.Drawing.Size(55, 23);
            this.btn_zero_LF4.TabIndex = 36;
            this.btn_zero_LF4.Text = "调零";
            this.btn_zero_LF4.UseVisualStyleBackColor = true;
            this.btn_zero_LF4.Click += new System.EventHandler(this.btn_zero_LF4_Click);
            // 
            // btn_zero_LF3
            // 
            this.btn_zero_LF3.Location = new System.Drawing.Point(216, 118);
            this.btn_zero_LF3.Name = "btn_zero_LF3";
            this.btn_zero_LF3.Size = new System.Drawing.Size(55, 23);
            this.btn_zero_LF3.TabIndex = 35;
            this.btn_zero_LF3.Text = "调零";
            this.btn_zero_LF3.UseVisualStyleBackColor = true;
            this.btn_zero_LF3.Click += new System.EventHandler(this.btn_zero_LF3_Click);
            // 
            // btn_zero_LF2
            // 
            this.btn_zero_LF2.Location = new System.Drawing.Point(217, 93);
            this.btn_zero_LF2.Name = "btn_zero_LF2";
            this.btn_zero_LF2.Size = new System.Drawing.Size(55, 23);
            this.btn_zero_LF2.TabIndex = 34;
            this.btn_zero_LF2.Text = "调零";
            this.btn_zero_LF2.UseVisualStyleBackColor = true;
            this.btn_zero_LF2.Click += new System.EventHandler(this.btn_zero_LF2_Click);
            // 
            // btn_zero_LF1
            // 
            this.btn_zero_LF1.Location = new System.Drawing.Point(217, 68);
            this.btn_zero_LF1.Name = "btn_zero_LF1";
            this.btn_zero_LF1.Size = new System.Drawing.Size(55, 23);
            this.btn_zero_LF1.TabIndex = 33;
            this.btn_zero_LF1.Text = "调零";
            this.btn_zero_LF1.UseVisualStyleBackColor = true;
            this.btn_zero_LF1.Click += new System.EventHandler(this.btn_zero_LF1_Click);
            // 
            // btn_zero_HF1
            // 
            this.btn_zero_HF1.Location = new System.Drawing.Point(217, 43);
            this.btn_zero_HF1.Name = "btn_zero_HF1";
            this.btn_zero_HF1.Size = new System.Drawing.Size(55, 23);
            this.btn_zero_HF1.TabIndex = 32;
            this.btn_zero_HF1.Text = "调零";
            this.btn_zero_HF1.UseVisualStyleBackColor = true;
            this.btn_zero_HF1.Click += new System.EventHandler(this.btn_zero_HF1_Click);
            // 
            // txtTestProgram
            // 
            this.txtTestProgram.Location = new System.Drawing.Point(44, 306);
            this.txtTestProgram.Multiline = true;
            this.txtTestProgram.Name = "txtTestProgram";
            this.txtTestProgram.Size = new System.Drawing.Size(200, 228);
            this.txtTestProgram.TabIndex = 18;
            // 
            // dataGridState
            // 
            this.dataGridState.AllowUserToAddRows = false;
            this.dataGridState.AllowUserToDeleteRows = false;
            this.dataGridState.AllowUserToResizeColumns = false;
            this.dataGridState.AllowUserToResizeRows = false;
            this.dataGridState.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridState.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridState.Location = new System.Drawing.Point(17, 19);
            this.dataGridState.Name = "dataGridState";
            this.dataGridState.RowTemplate.Height = 23;
            this.dataGridState.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridState.Size = new System.Drawing.Size(260, 175);
            this.dataGridState.TabIndex = 31;
            // 
            // btn_test_connect
            // 
            this.btn_test_connect.Location = new System.Drawing.Point(44, 259);
            this.btn_test_connect.Name = "btn_test_connect";
            this.btn_test_connect.Size = new System.Drawing.Size(75, 23);
            this.btn_test_connect.TabIndex = 30;
            this.btn_test_connect.Text = "连接完成";
            this.btn_test_connect.UseVisualStyleBackColor = true;
            this.btn_test_connect.Click += new System.EventHandler(this.btn_test_connect_Click);
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(185, 259);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(59, 23);
            this.button_save.TabIndex = 27;
            this.button_save.Text = "保存";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(125, 259);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(53, 23);
            this.btn_Clear.TabIndex = 20;
            this.btn_Clear.Text = "清空";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.dataGridState_His);
            this.tabPage3.Controls.Add(this.btn_Open_His);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(300, 615);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = " 离线查询";
            // 
            // dataGridState_His
            // 
            this.dataGridState_His.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridState_His.Location = new System.Drawing.Point(50, 21);
            this.dataGridState_His.Name = "dataGridState_His";
            this.dataGridState_His.RowTemplate.Height = 23;
            this.dataGridState_His.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridState_His.Size = new System.Drawing.Size(198, 160);
            this.dataGridState_His.TabIndex = 32;
            // 
            // btn_Open_His
            // 
            this.btn_Open_His.Location = new System.Drawing.Point(109, 241);
            this.btn_Open_His.Name = "btn_Open_His";
            this.btn_Open_His.Size = new System.Drawing.Size(75, 23);
            this.btn_Open_His.TabIndex = 2;
            this.btn_Open_His.Text = "打开";
            this.btn_Open_His.UseVisualStyleBackColor = true;
            this.btn_Open_His.Click += new System.EventHandler(this.btn_Open_His_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.pic_fft);
            this.splitContainer2.Panel1.Controls.Add(this.trackBar1);
            this.splitContainer2.Panel1.Controls.Add(this.graphShow);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.graphAnlys);
            this.splitContainer2.Size = new System.Drawing.Size(948, 641);
            this.splitContainer2.SplitterDistance = 320;
            this.splitContainer2.TabIndex = 3;
            // 
            // pic_fft
            // 
            this.pic_fft.BackColor = System.Drawing.Color.Transparent;
            this.pic_fft.Enabled = false;
            this.pic_fft.ErrorImage = null;
            this.pic_fft.Image = ((System.Drawing.Image)(resources.GetObject("pic_fft.Image")));
            this.pic_fft.InitialImage = null;
            this.pic_fft.Location = new System.Drawing.Point(493, 48);
            this.pic_fft.Name = "pic_fft";
            this.pic_fft.Size = new System.Drawing.Size(2, 210);
            this.pic_fft.TabIndex = 6;
            this.pic_fft.TabStop = false;
            this.pic_fft.Visible = false;
            // 
            // trackBar1
            // 
            this.trackBar1.AutoSize = false;
            this.trackBar1.LargeChange = 1;
            this.trackBar1.Location = new System.Drawing.Point(1, 1);
            this.trackBar1.Maximum = 926;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(946, 22);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.Value = 484;
            this.trackBar1.Visible = false;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // graphShow
            // 
            this.graphShow.Dock = System.Windows.Forms.DockStyle.Right;
            this.graphShow.Location = new System.Drawing.Point(0, 0);
            this.graphShow.Name = "graphShow";
            this.graphShow.ScrollGrace = 0D;
            this.graphShow.ScrollMaxX = 0D;
            this.graphShow.ScrollMaxY = 0D;
            this.graphShow.ScrollMaxY2 = 0D;
            this.graphShow.ScrollMinX = 0D;
            this.graphShow.ScrollMinY = 0D;
            this.graphShow.ScrollMinY2 = 0D;
            this.graphShow.Size = new System.Drawing.Size(948, 320);
            this.graphShow.TabIndex = 3;
            // 
            // graphAnlys
            // 
            this.graphAnlys.AutoScroll = true;
            this.graphAnlys.Dock = System.Windows.Forms.DockStyle.Right;
            this.graphAnlys.IsAutoScrollRange = true;
            this.graphAnlys.IsShowHScrollBar = true;
            this.graphAnlys.IsShowVScrollBar = true;
            this.graphAnlys.Location = new System.Drawing.Point(0, 0);
            this.graphAnlys.Name = "graphAnlys";
            this.graphAnlys.ScrollGrace = 0D;
            this.graphAnlys.ScrollMaxX = 0D;
            this.graphAnlys.ScrollMaxY = 0D;
            this.graphAnlys.ScrollMaxY2 = 0D;
            this.graphAnlys.ScrollMinX = 0D;
            this.graphAnlys.ScrollMinY = 0D;
            this.graphAnlys.ScrollMinY2 = 0D;
            this.graphAnlys.Size = new System.Drawing.Size(948, 317);
            this.graphAnlys.TabIndex = 4;
            // 
            // chBLineShow1
            // 
            this.chBLineShow1.AutoSize = true;
            this.chBLineShow1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chBLineShow1.ForeColor = System.Drawing.Color.Red;
            this.chBLineShow1.Location = new System.Drawing.Point(768, 4);
            this.chBLineShow1.Name = "chBLineShow1";
            this.chBLineShow1.Size = new System.Drawing.Size(67, 20);
            this.chBLineShow1.TabIndex = 12;
            this.chBLineShow1.Text = "高频1";
            this.chBLineShow1.UseVisualStyleBackColor = true;
            this.chBLineShow1.CheckedChanged += new System.EventHandler(this.chBLineShow1_CheckedChanged);
            // 
            // btn_Scope
            // 
            this.btn_Scope.Location = new System.Drawing.Point(312, 0);
            this.btn_Scope.Name = "btn_Scope";
            this.btn_Scope.Size = new System.Drawing.Size(75, 23);
            this.btn_Scope.TabIndex = 6;
            this.btn_Scope.Text = "示波";
            this.btn_Scope.UseVisualStyleBackColor = true;
            this.btn_Scope.Click += new System.EventHandler(this.btn_Scope_Click);
            // 
            // btn_Capture
            // 
            this.btn_Capture.Location = new System.Drawing.Point(403, 0);
            this.btn_Capture.Name = "btn_Capture";
            this.btn_Capture.Size = new System.Drawing.Size(75, 23);
            this.btn_Capture.TabIndex = 7;
            this.btn_Capture.Text = "采样";
            this.btn_Capture.UseVisualStyleBackColor = true;
            this.btn_Capture.Click += new System.EventHandler(this.btn_Capture_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(496, 0);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(75, 23);
            this.btn_Stop.TabIndex = 8;
            this.btn_Stop.Text = "停止";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer_draw
            // 
            this.timer_draw.Tick += new System.EventHandler(this.timer_draw_Tick);
            // 
            // chBLineShow2
            // 
            this.chBLineShow2.AutoSize = true;
            this.chBLineShow2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chBLineShow2.ForeColor = System.Drawing.Color.Green;
            this.chBLineShow2.Location = new System.Drawing.Point(829, 4);
            this.chBLineShow2.Name = "chBLineShow2";
            this.chBLineShow2.Size = new System.Drawing.Size(67, 20);
            this.chBLineShow2.TabIndex = 33;
            this.chBLineShow2.Text = "低频1";
            this.chBLineShow2.UseVisualStyleBackColor = true;
            this.chBLineShow2.CheckedChanged += new System.EventHandler(this.chBLineShow2_CheckedChanged);
            // 
            // chBLineShow6
            // 
            this.chBLineShow6.AutoSize = true;
            this.chBLineShow6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chBLineShow6.ForeColor = System.Drawing.Color.Magenta;
            this.chBLineShow6.Location = new System.Drawing.Point(1078, 4);
            this.chBLineShow6.Name = "chBLineShow6";
            this.chBLineShow6.Size = new System.Drawing.Size(59, 20);
            this.chBLineShow6.TabIndex = 17;
            this.chBLineShow6.Text = "预留";
            this.chBLineShow6.UseVisualStyleBackColor = true;
            this.chBLineShow6.CheckedChanged += new System.EventHandler(this.chBLineShow6_CheckedChanged);
            // 
            // chBLineShow3
            // 
            this.chBLineShow3.AutoSize = true;
            this.chBLineShow3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chBLineShow3.ForeColor = System.Drawing.Color.Black;
            this.chBLineShow3.Location = new System.Drawing.Point(890, 4);
            this.chBLineShow3.Name = "chBLineShow3";
            this.chBLineShow3.Size = new System.Drawing.Size(67, 20);
            this.chBLineShow3.TabIndex = 34;
            this.chBLineShow3.Text = "低频2";
            this.chBLineShow3.UseVisualStyleBackColor = true;
            this.chBLineShow3.CheckedChanged += new System.EventHandler(this.chBLineShow3_CheckedChanged);
            // 
            // chBLineShow4
            // 
            this.chBLineShow4.AutoSize = true;
            this.chBLineShow4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chBLineShow4.ForeColor = System.Drawing.Color.Blue;
            this.chBLineShow4.Location = new System.Drawing.Point(953, 4);
            this.chBLineShow4.Name = "chBLineShow4";
            this.chBLineShow4.Size = new System.Drawing.Size(67, 20);
            this.chBLineShow4.TabIndex = 35;
            this.chBLineShow4.Text = "低频3";
            this.chBLineShow4.UseVisualStyleBackColor = true;
            this.chBLineShow4.CheckedChanged += new System.EventHandler(this.chBLineShow4_CheckedChanged);
            // 
            // chBLineShow5
            // 
            this.chBLineShow5.AutoSize = true;
            this.chBLineShow5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chBLineShow5.ForeColor = System.Drawing.Color.Orange;
            this.chBLineShow5.Location = new System.Drawing.Point(1016, 4);
            this.chBLineShow5.Name = "chBLineShow5";
            this.chBLineShow5.Size = new System.Drawing.Size(67, 20);
            this.chBLineShow5.TabIndex = 36;
            this.chBLineShow5.Text = "低频4";
            this.chBLineShow5.UseVisualStyleBackColor = true;
            this.chBLineShow5.CheckedChanged += new System.EventHandler(this.chBLineShow5_CheckedChanged);
            // 
            // timer_state_online
            // 
            this.timer_state_online.Tick += new System.EventHandler(this.timer_state_online_Tick);
            // 
            // btn_choose_fft
            // 
            this.btn_choose_fft.Location = new System.Drawing.Point(585, 0);
            this.btn_choose_fft.Name = "btn_choose_fft";
            this.btn_choose_fft.Size = new System.Drawing.Size(75, 23);
            this.btn_choose_fft.TabIndex = 37;
            this.btn_choose_fft.Text = "FFT取点";
            this.btn_choose_fft.UseVisualStyleBackColor = true;
            this.btn_choose_fft.Click += new System.EventHandler(this.btn_choose_fft_Click);
            // 
            // btn_cancel_fft
            // 
            this.btn_cancel_fft.Location = new System.Drawing.Point(676, 0);
            this.btn_cancel_fft.Name = "btn_cancel_fft";
            this.btn_cancel_fft.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel_fft.TabIndex = 38;
            this.btn_cancel_fft.Text = "取消FFT";
            this.btn_cancel_fft.UseVisualStyleBackColor = true;
            this.btn_cancel_fft.Click += new System.EventHandler(this.btn_cancel_fft_Click);
            // 
            // btn_wipe_graph
            // 
            this.btn_wipe_graph.Location = new System.Drawing.Point(1141, 2);
            this.btn_wipe_graph.Name = "btn_wipe_graph";
            this.btn_wipe_graph.Size = new System.Drawing.Size(75, 23);
            this.btn_wipe_graph.TabIndex = 7;
            this.btn_wipe_graph.Text = "清空曲线";
            this.btn_wipe_graph.UseVisualStyleBackColor = true;
            this.btn_wipe_graph.Click += new System.EventHandler(this.btn_wipe_graph_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(189, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 23);
            this.textBox1.TabIndex = 39;
            this.textBox1.Visible = false;
            // 
            // MainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1260, 669);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btn_wipe_graph);
            this.Controls.Add(this.btn_cancel_fft);
            this.Controls.Add(this.btn_choose_fft);
            this.Controls.Add(this.chBLineShow6);
            this.Controls.Add(this.chBLineShow5);
            this.Controls.Add(this.chBLineShow4);
            this.Controls.Add(this.chBLineShow3);
            this.Controls.Add(this.chBLineShow2);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.btn_Capture);
            this.Controls.Add(this.btn_Scope);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.chBLineShow1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainFrom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "无线采集系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrom_FormClosing);
            this.Load += new System.EventHandler(this.MainFrom_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridState)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridState_His)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_fft)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全消ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem 设置采集间隔ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置当前时间ToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private ZedGraph.ZedGraphControl graphShow;
        private System.Windows.Forms.ToolStripMenuItem 低频设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 高频设置ToolStripMenuItem;
        private System.Windows.Forms.Button btn_Scope;
        private System.Windows.Forms.Button btn_Capture;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button btn_Open_His;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Timer timer_draw;
        private System.Windows.Forms.Button btn_test_connect;
        public System.Windows.Forms.TextBox txtTestProgram;
        public System.Windows.Forms.CheckBox chBLineShow1;
        public System.Windows.Forms.CheckBox chBLineShow2;
        public System.Windows.Forms.CheckBox chBLineShow6;
        public System.Windows.Forms.CheckBox chBLineShow3;
        public System.Windows.Forms.CheckBox chBLineShow4;
        public System.Windows.Forms.CheckBox chBLineShow5;
        public System.Windows.Forms.DataGridView dataGridState;
        private System.Windows.Forms.Timer timer_state_online;
        public System.Windows.Forms.DataGridView dataGridState_His;
        private ZedGraph.ZedGraphControl graphAnlys;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button btn_choose_fft;
        private System.Windows.Forms.ToolStripMenuItem fFT设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 校准ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 标定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 调零ToolStripMenuItem;
        private System.Windows.Forms.Button btn_zero_HF1;
        private System.Windows.Forms.Button btn_zero_LF2;
        private System.Windows.Forms.Button btn_zero_LF1;
        private System.Windows.Forms.Button btn_zero_LF4;
        private System.Windows.Forms.Button btn_zero_LF3;
        private System.Windows.Forms.PictureBox pic_fft;
        private System.Windows.Forms.Button btn_cancel_fft;
        private System.Windows.Forms.Button btn_wipe_graph;
        private System.Windows.Forms.TextBox textBox1;
    }
}