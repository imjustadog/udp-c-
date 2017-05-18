using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Data.OleDb;
using ZedGraph;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace 无线采集
{
    public partial class MainFrom : Form
    {
        [DllImport("_fft.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        public extern static void do_fft(int FFTPoints, float[] spec, float[] sampleData);

        public IPEndPoint broadRemote;
        public UdpClient broadSend;
        public int trig_set;
        public byte[] mode = new byte[3];
        public UdpClass[] UdpSvr;
        public UdpClass svr;
        public FileStream filedataHis;
        public FileStream filestate;
        public int[] UdpOnline;
        public int num_online = 0;

        public LineItem[,] vibCurve;
        public RollingPointPairList[,] vibLine;

        public LineItem[,] specCurve;
        public RollingPointPairList[,] specLine;

        public Form2 FrmLF, FrmHF;
        public Form3 FrmFFT;
        public Form4 FrmModul;
        public Form5 FrmZero;
        public string dataPath = ""; //路径+名字+编号不带类型
        public string dataPath_state = "";
        public string[] dataPath_His;

        public int time_per_data;
        public double time_interval;
        public double time_interval_His;
        public int time_frequency_His;
        public int iStart = 0, iFirst = 0;
        public int time_frequency;
        public double ZeroLine;
        public double Modulus;

        public int num_FFT = 1024;
        //public float[,] FFTData = new float[MAX_UDP_CONNECT, 4096];
        //public float[,] FFTPower = new float[MAX_UDP_CONNECT, 4096];
        //public float[] FFTData = new float[4096];
        //public float[] FFTPower = new float[4096];
        float[] FFTData = new float[4096];
        float[] FFTPower = new float[4096];
        public bool[] IsFFT = new bool[MAX_UDP_CONNECT];

        public double _XScaleMax;
        public byte mode_setting, mode_now;
        public const int POINTS_PER_SCAN = 5000;
        public const int MAX_UDP_CONNECT = 5;

        public int trsprt_layer_offset;

        public MainFrom()
        {
            InitializeComponent();
        }
        private void MainFrom_Load(object sender, EventArgs e)
        {
            bool IsError = false;

            dataGridVibShow();

            time_per_data = 5;
            time_interval = time_per_data / 5000.0;
            _XScaleMax = time_interval * POINTS_PER_SCAN;

            mode_setting = (byte)('R');
            mode_now = (byte)('P');

            vibCurve = new LineItem[MAX_UDP_CONNECT, 6];
            vibLine = new RollingPointPairList[MAX_UDP_CONNECT, 6];
            specCurve = new LineItem[MAX_UDP_CONNECT, 6];
            specLine = new RollingPointPairList[MAX_UDP_CONNECT, 6];
            dataPath_His = new String[MAX_UDP_CONNECT];
            UdpSvr = new UdpClass[MAX_UDP_CONNECT];
            UdpOnline = new int[MAX_UDP_CONNECT];
       
            FrmLF = new Form2();
            FrmLF.Text = "低频设置";
            FrmHF = new Form2();
            FrmHF.Text = "高频设置";
            FrmFFT = new Form3();

            try
            {
                IPEndPoint localIpepS;

                localIpepS = new IPEndPoint(
                IPAddress.Parse("192.168.0.100"), 900); // 本机IP，指定的端口号

                broadSend = new UdpClient(localIpepS);
                broadRemote = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 900); // 发送到的IP地址和端口号
            }
            catch (Exception ex)
            {
                IsError = true;
            }

            for (int i = 0; i < MAX_UDP_CONNECT; i++)
            {
                IsFFT[i] = false;
                UdpSvr[i] = new UdpClass(i, this);
                try
                {
                    UdpSvr[i].InitRecv();
                 }
                 catch (Exception ex)
                 {
                      IsError = true;
                 }
            }
            if (IsError == true) ShowMessage(txtTestProgram, "IP地址不是192.168.0.100，如要采集的话检查网口连接关了重开吧→v→");
            else
            {
                timer_state_online.Enabled = true;
                txtTestProgram.Text += "检查连接中...\r\n";
            }
            timer_draw.Enabled = false;
            FrmZero = new Form5(this);
            FrmModul = new Form4(this);
        }
        private void dataGridVibShow()
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("通道"));
            dt.Columns.Add(new DataColumn("状态"));
            dt.Columns.Add(new DataColumn("丢包"));
            dt.Columns.Add(new DataColumn("操作"));
            for (int i = 0; i < 6; i++)
            {
                dr = dt.NewRow();
                if (i == 0) dr[0] = "高频1";
                else if (i == 5) dr[0] = "预留";
                else dr[0] = "低频" + i.ToString();
                dt.Rows.Add(dr);
            }

            dataGridState.RowHeadersVisible = false;
            dataGridState.DataSource = dt;
            dataGridState.Columns[0].Width = 65;
            dataGridState.Columns[1].Width = 65;
            dataGridState.Columns[2].Width = 65;
            dataGridState.Columns[3].Width = 65;
            for (int i = 0; i < 6; i++)
            {
                dataGridState.Rows[i].Height = 25;
            }

            DataTable dt_His = new DataTable();
            DataRow dr_His;
            dt_His.Columns.Add(new DataColumn("通道"));
            dt_His.Columns.Add(new DataColumn("丢包"));
            dt_His.Columns.Add(new DataColumn("数值"));
            for (int i = 0; i < 6; i++)
            {
                dr_His = dt_His.NewRow();
                if (i == 0) dr_His[0] = "高频1";
                else if (i == 5) dr_His[0] = "预留";
                else dr_His[0] = "低频" + i.ToString();
                dt_His.Rows.Add(dr_His);
            }

            dataGridState_His.RowHeadersVisible = false;
            dataGridState_His.DataSource = dt_His;
            dataGridState_His.Columns[0].Width = 65;
            dataGridState_His.Columns[1].Width = 65;
            dataGridState_His.Columns[2].Width = 65;

            for (int i = 0; i < 6; i++)
            {
                dataGridState_His.Rows[i].Height = 25;
            }

        }
        #region 异常显示
        delegate void ShowMessageDelegate(TextBox txtbox, string message);
        public void ShowMessage(TextBox txtbox, string message)
        {
            if (txtbox.InvokeRequired)
            {
                ShowMessageDelegate showMessageDelegate = ShowMessage;
                txtbox.Invoke(showMessageDelegate, new object[] { txtbox, message });
            }
            else
            {
                txtbox.Text += message + "\r\n";
            }
        }
        #endregion
        #region 显示配置
        private void zedGraphAxisSet_Realtime()
        {
            chBLineShow1.Visible = true;
            chBLineShow2.Visible = true;
            chBLineShow3.Visible = true;
            chBLineShow4.Visible = true;
            chBLineShow5.Visible = true;
            chBLineShow6.Visible = true;
            GraphPane pane = graphShow.GraphPane;
            pane.CurveList.Clear();
            pane.GraphObjList.Clear();
            pane.Title.Text = "实时数据";
            pane.YAxis.Title.Text = "模数";
            pane.XAxis.Title.Text = "时间";
            pane.XAxis.Scale.Min = 0;        //X轴最小值0
            pane.XAxis.Scale.Max = _XScaleMax;    //X轴最大30
            pane.YAxis.Scale.Min = -2.5;
            pane.YAxis.Scale.Max = 2.5;
            pane.XAxis.Scale.MinorStep = _XScaleMax / 100;//X轴小步长1,也就是小间隔
            pane.XAxis.Scale.MajorStep = _XScaleMax / 20;//X轴大步长为5，也就是显示文字的大间隔
            pane.YAxis.MajorGrid.DashOff = 0;
            pane.YAxis.MinorGrid.DashOff = 0;
            pane.YAxis.MajorGrid.IsZeroLine = false;
            graphShow.IsShowPointValues = false;
            graphShow.IsShowHScrollBar = false;
            graphShow.IsAutoScrollRange = false;
            graphShow.AxisChange();
        }
        private void zedGraphVibShow_Realtime(int i)
        {
            Color colour = GetColor(i);
            GraphPane pane = graphShow.GraphPane;

            UdpSvr[i].vibLine[0] = new RollingPointPairList(POINTS_PER_SCAN);
            if(i == 0)
                UdpSvr[i].vibCurve[0] = pane.AddCurve("高频1", UdpSvr[i].vibLine[0], colour, SymbolType.None);
            else
                UdpSvr[i].vibCurve[0] = pane.AddCurve("低频"+ i.ToString(), UdpSvr[i].vibLine[0], colour, SymbolType.None);

            graphShow.AxisChange();
            graphShow.Refresh();
        }

        private Color GetColor(int i)
        {
            Color colour;
            switch (i)
            {
                case 0:
                    {
                        colour = Color.Red;
                        break;
                    }
                case 1:
                    {
                        colour = Color.Green;
                        break;
                    }
                case 2:
                    {
                        colour = Color.Black;
                        break;
                    }
                case 3:
                    {
                        colour = Color.Orange;
                        break;
                    }
                case 4:
                    {
                        colour = Color.Blue;
                        break;
                    }
                case 5:
                    {
                        colour = Color.Pink;
                        break;
                    }
                case 6:
                    {
                        colour = Color.Magenta;
                        break;
                    }
                default:
                    {
                        colour = Color.Purple;
                        break;
                    }
            }
            return colour;
        }

        private void zedGraphAxisSet_Spectrum()
        {
            GraphPane pane = graphAnlys.GraphPane;
            pane.CurveList.Clear();
            pane.GraphObjList.Clear();
            pane.XAxis.Scale.Min = 0;        //X轴最小值0
            pane.XAxis.Scale.Max = time_frequency_His / 2;
            pane.XAxis.Scale.MinorStep = time_frequency_His / 40;//X轴小步长1,也就是小间隔
            pane.XAxis.Scale.MajorStep = time_frequency_His / 8;//X轴大步长为5，也就是显示文字的大间隔
            pane.YAxis.MajorGrid.DashOff = 0;
            pane.YAxis.MinorGrid.DashOff = 0;
            pane.YAxis.MajorGrid.IsZeroLine = false;
            graphShow.IsShowHScrollBar = true;
            graphShow.IsShowVScrollBar = true;
            graphShow.IsAutoScrollRange = true;
            graphAnlys.IsShowPointValues = true;
            graphAnlys.AxisChange();
        }

        private void zedGraphVibShow_Spectrum(int i)
        {
            Color colour = GetColor(i);
            GraphPane pane = graphAnlys.GraphPane;

            specLine[i, 0] = new RollingPointPairList(num_FFT);
            if (i == 0)
                specCurve[i, 0] = pane.AddCurve("高频1", specLine[i, 0], colour, SymbolType.None);
            else
                specCurve[i, 0] = pane.AddCurve("低频" + i.ToString(), specLine[i, 0], colour, SymbolType.None);

            graphAnlys.AxisChange();
            graphAnlys.Refresh();
        }

        private void zedGraphAxisSet_History()
        {
            chBLineShow1.Visible = true;
            chBLineShow2.Visible = true;
            chBLineShow3.Visible = true;
            chBLineShow4.Visible = true;
            chBLineShow5.Visible = true;
            chBLineShow6.Visible = true;
            GraphPane pane = graphShow.GraphPane;
            pane.CurveList.Clear();
            pane.GraphObjList.Clear();
            pane.Title.Text = "历史数据";
            pane.YAxis.Title.Text = "模数";
            pane.XAxis.Title.Text = "时间";
            pane.YAxis.MajorGrid.DashOff = 0;
            pane.YAxis.MinorGrid.DashOff = 0;
            pane.YAxis.MajorGrid.IsZeroLine = false;
            graphShow.IsShowHScrollBar = true;
            graphShow.IsShowVScrollBar = true;
            graphShow.IsAutoScrollRange = true;

            graphShow.IsShowPointValues = true;
        }
        private void zedGraphVibShow_History(int num,int i)
        {
            Color colour = GetColor(i);
            GraphPane pane = graphShow.GraphPane;
            pane.XAxis.Scale.Max = time_interval * num * 100.0;
            graphShow.ScrollMaxX = time_interval * num * 100.0;
            graphShow.AxisChange();

            vibLine[i, 0] = new RollingPointPairList(num * 100);
            if(i == 0)
                vibCurve[i, 0] = pane.AddCurve("高频1", vibLine[i, 0], colour, SymbolType.None);
            else 
                vibCurve[i, 0] = pane.AddCurve("低频"+ i.ToString(), vibLine[i, 0], colour, SymbolType.None);

            graphShow.AxisChange();
            graphShow.Refresh();
        }
        #endregion
        //重写关闭按钮
        private void MainFrom_FormClosing(object sender, EventArgs e)
        {
            StreamWriter sw1 = new StreamWriter("statezero.txt",false, System.Text.Encoding.Default);
            string strTemp1 = "" ;
            for(int i = 0; i < 5; i ++)
            {
                strTemp1 += UdpSvr[i].ZeroLine.ToString() + " ";
            }
            sw1.WriteLine(strTemp1);
            sw1.Close();

            StreamWriter sw2 = new StreamWriter("statemodulus.txt", false, System.Text.Encoding.Default);
            string strTemp2 = "";
            for (int i = 0; i < 5; i++)
            {
                strTemp2 += UdpSvr[i].Modulus.ToString() + " ";
            }
            sw2.WriteLine(strTemp2);
            sw2.Close();

            for (int i = 0; i < num_online; i++)
            {
                UdpSvr[UdpOnline[i]].DelateFile();
            }
            this.Dispose();
            this.Close();

            System.Environment.Exit(System.Environment.ExitCode);
        }
        #region 复选框设置
        private void chBLineShow1_CheckedChanged(object sender, EventArgs e)
        {
            if (chBLineShow1.Checked)
            {
                if(UdpSvr[0].vibCurve[0] != null)  UdpSvr[0].vibCurve[0].IsVisible = true;
                if (vibCurve[0, 0] != null) vibCurve[0, 0].IsVisible = true;
                IsFFT[0] = true;
            }
            else
            {
                if (UdpSvr[0].vibCurve[0] != null) UdpSvr[0].vibCurve[0].IsVisible = false;
                if (vibCurve[0, 0] != null) vibCurve[0, 0].IsVisible = false;
                IsFFT[0] = false;
            }
            //graphShow.GraphPane.XAxis.Scale.MaxAuto = true;
            graphShow.AxisChange();
            graphShow.Refresh();
        }

        private void chBLineShow2_CheckedChanged(object sender, EventArgs e)
        {
            if (chBLineShow2.Checked)
            {
                if (UdpSvr[1].vibCurve[0] != null) UdpSvr[1].vibCurve[0].IsVisible = true;
                if (vibCurve[1, 0] != null) vibCurve[1, 0].IsVisible = true;
                IsFFT[1] = true;
            }
            else
            {
                if (UdpSvr[1].vibCurve[0] != null) UdpSvr[1].vibCurve[0].IsVisible = false;
                if (vibCurve[1, 0] != null)  vibCurve[1, 0].IsVisible = false;
                IsFFT[1] = false;
            }
            graphShow.AxisChange();
            graphShow.Refresh();
        }

        private void chBLineShow3_CheckedChanged(object sender, EventArgs e)
        {
             if (chBLineShow3.Checked)
             {
                if (UdpSvr[2].vibCurve[0] != null) UdpSvr[2].vibCurve[0].IsVisible = true;
                if (vibCurve[2, 0] != null) vibCurve[2, 0].IsVisible = true;
                IsFFT[2] = true;
            }
             else
             {
                if (UdpSvr[2].vibCurve[0] != null) UdpSvr[2].vibCurve[0].IsVisible = false;
                if (vibCurve[2, 0] != null) vibCurve[2, 0].IsVisible = false;
                IsFFT[2] = false;
            }
             graphShow.AxisChange();
             graphShow.Refresh();
        }
        private void chBLineShow4_CheckedChanged(object sender, EventArgs e)
        {
             if (chBLineShow4.Checked)
             {
                if (UdpSvr[3].vibCurve[0] != null) UdpSvr[3].vibCurve[0].IsVisible = true;
                if (vibCurve[3, 0] != null)  vibCurve[3, 0].IsVisible = true;
                IsFFT[3] = true;
            }
             else
             {
                if (UdpSvr[3].vibCurve[0] != null) UdpSvr[3].vibCurve[0].IsVisible = false;
                if (vibCurve[3, 0] != null) vibCurve[3, 0].IsVisible = false;
                IsFFT[3] = false;
            }
             graphShow.AxisChange();
             graphShow.Refresh();
        }
        private void chBLineShow5_CheckedChanged(object sender, EventArgs e)
        {
            if (chBLineShow5.Checked)
            {
                if (UdpSvr[4].vibCurve[0] != null) UdpSvr[4].vibCurve[0].IsVisible = true;
                if (vibCurve[4, 0] != null) vibCurve[4, 0].IsVisible = true;
                IsFFT[4] = true;
            }
            else
            {
                if (UdpSvr[4].vibCurve[0] != null) UdpSvr[4].vibCurve[0].IsVisible = false;
                if (vibCurve[4, 0] != null) vibCurve[4, 0].IsVisible = false;
                IsFFT[4] = false;
            }
            graphShow.AxisChange();
            graphShow.Refresh();
        }
        private void chBLineShow6_CheckedChanged(object sender, EventArgs e)
        {
            if (chBLineShow6.Checked)
            {
                //UdpSvr[5].vibCurve[0].IsVisible = true;
            }
            else
            {
                //UdpSvr[5].vibCurve[0].IsVisible = false;
            }
            graphShow.AxisChange();
            graphShow.Refresh();
        }
        #endregion

        private double VibrationDataHis_HF(int num, byte[] bOrder, byte[] bData)
        {
            double dTemp;
            Array.Copy(bData, num, bOrder, 0, 3);
            dTemp = (System.BitConverter.ToUInt32(bOrder, 0) / 1258291.2 - ZeroLine) * Modulus; ///8388608.0 ) * 5.0 * 1.33333 ;
            return(Math.Round(dTemp, 3));
        }
        private double VibrationDataHis_LF(int num, byte[] bOrder, byte[] bData)
        {
            double dTemp;
            Array.Copy(bData, num, bOrder, 0, 3);
            dTemp = ((System.BitConverter.ToInt32(bOrder, 0) - 0x800000) / 629145.6 - ZeroLine) * Modulus;
            return(Math.Round(dTemp, 3));
        }
        private delegate double VibrationDataHisEventHandler(int num, byte[] bOrder, byte[] bData);
        private void display_offline(FileStream file,int iNumofClient, double dTimeInterval)
        {
            VibrationDataHisEventHandler VibrationDataHis;
            int j = 0, i, iNumofPacks = 0;
            byte[] bOrder = new byte[4];
            byte[] bData = new byte[1000];
            double dTime = 0;
            double MO;
            Int32 iPackNum, iPackLose = 0, iPackTemp = 0;

            iNumofPacks = (int)(file.Length / 305);
            zedGraphVibShow_History(iNumofPacks, iNumofClient);

            file.Position = 0;
            iFirst = (int)(1 / (100 * dTimeInterval));
            if (iNumofClient == 0) VibrationDataHis = VibrationDataHis_HF;
            else VibrationDataHis = VibrationDataHis_LF;

            while(j < iFirst)
            {
                file.Read(bData, 0, 305);
                Array.Copy(bData, 1, bOrder, 0, 3);
                iPackTemp = System.BitConverter.ToInt32(bOrder, 0);
                j++;
            }
            iStart = iPackTemp + 1;
            iPackNum = iPackTemp;

            while (j < iNumofPacks)
            {
                file.Read(bData, 0, 305);

                Array.Copy(bData, 1, bOrder, 0, 3);
                iPackTemp = System.BitConverter.ToInt32(bOrder, 0);

                dTime = (iPackTemp - iStart) * 100 * dTimeInterval;

                if (iPackTemp - iPackNum != 1)
                {
                    iPackLose++;
                    if(iPackTemp - iPackNum > 1)
                    {
                        vibLine[iNumofClient, 0].Add((iPackNum - iStart) * 100 * dTimeInterval, 2.5);// - vibzero[0, num]);
                        vibLine[iNumofClient, 0].Add((iPackTemp - iStart) * 100 * dTimeInterval, 2.5);
                    }
                }
                iPackNum = iPackTemp;

                for (i = 4; i <= 301; i += 3)
                {
                    MO = VibrationDataHis(i, bOrder, bData);
                    dTime = Math.Round(dTime, 4);
                    vibLine[iNumofClient, 0].Add(dTime, MO);// - vibzero[0, num]);
                    dTime += dTimeInterval;
                }
                
                j++;
            }
            dataGridState_His.Rows[iNumofClient].Cells[1].Value = iPackLose;
            graphShow.AxisChange();
            graphShow.Refresh();
        }
        private void button_save_Click(object sender, EventArgs e)
        {
            int num;

            num = int.Parse(FrmLF.FileNum);
            num++;

            FrmLF.FileNum = num.ToString();
            dataPath = FrmLF.FilePath + "\\" + FrmLF.FileName + FrmLF.FileNum; //不带类型

            for (int i = 0; i < num_online;i ++)
            {
                UdpSvr[UdpOnline[i]].SaveFile();
            }
            zedGraphAxisSet_Realtime();
        }

        private void auto_check(int num)
        {
            switch(num)
            {
                case 0:
                    {
                        chBLineShow1.Checked = true;
                        break;
                    }
                case 1:
                    {
                        chBLineShow2.Checked = true;
                        break;
                    }
                case 2:
                    {
                        chBLineShow3.Checked = true;
                        break;
                    }
                case 3:
                    {
                        chBLineShow4.Checked = true;
                        break;
                    }
                case 4:
                    {
                        chBLineShow5.Checked = true;
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        private void btn_Open_His_Click(object sender, EventArgs e)
        {
            string[] arr;
            string strDataPath;
            int num;

            OpenFileDialog fd = new OpenFileDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            {
                strDataPath = fd.FileName.ToString();
                arr = strDataPath.Split(' ');
                num = int.Parse(arr[arr.Count() - 5]);
                IsFFT[num] = true;
                auto_check(num);
                dataPath_His[num] = strDataPath;
                time_interval_His = 1.0 / int.Parse(arr[arr.Count() - 4]);
                time_frequency_His = int.Parse(arr[arr.Count() - 4]);
                ZeroLine = double.Parse(arr[arr.Count() - 3]);
                Modulus = double.Parse(arr[arr.Count() - 2]);
                filedataHis = new FileStream(strDataPath, FileMode.Open);
                display_offline(filedataHis,num, time_interval_His);
                //txt_lose_his.Text = pack_lose.ToString();
                filedataHis.Dispose();
                filedataHis.Close();
            }
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            timer_draw.Enabled = false;
            for (int i = 0; i < MAX_UDP_CONNECT; i ++)
            {
                if (UdpSvr[i] != null)
                {
                    if (UdpSvr[i].filedata != null)
                    {
                        UdpSvr[i].SaveFile();
                    }
                    if (UdpSvr[i].thrRecv != null)
                    {
                        UdpSvr[i].AbortThread();
                    }
                }
            }
            zedGraphAxisSet_History();
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="obj"></param>

        private void 低频设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mode_now != (byte)('P')) MessageBox.Show("请先停止采集");
            else
            {
                FrmLF.ShowDialog();
                if (FrmLF.DialogResult == DialogResult.OK)
                {
                    //mode_setting = FrmLF.trig_mode;
                    time_per_data = 5000 / FrmLF.Freq;            
                    time_interval = 1.0 / FrmLF.Freq;
                    _XScaleMax = time_per_data;
                    /*if (mode_setting == 'M')
                    {
                        //trig_set = ;
                    }*/
                    if (FrmLF.FileName != "")
                    {
                          dataPath = FrmLF.FilePath + "\\" + FrmLF.FileName + FrmLF.FileNum; //不带类型
                    }
                    FrmLF.Hide();
                }
                if (FrmLF.DialogResult == DialogResult.Cancel)
                {
                    FrmLF.Hide();
                }
            }
        }

        public void SetMode(byte mode_setting, int data)
        {
            if (mode_setting == (byte)('R'))
            {
                time_per_data = data;
                time_interval = time_per_data / 5000.0;
                time_frequency = 5000 / time_per_data;
                mode[0] = (byte)('R');
                mode[1] = (byte)(time_per_data / 256);
                mode[2] = (byte)(time_per_data % 256);
            }
            else if (mode_setting == (byte)('M'))
            {
                trig_set = data;
                mode[0] = (byte)('M');
                mode[1] = (byte)(trig_set / 256);
                mode[2] = (byte)(trig_set % 256);
            }
            else if (mode_setting == (byte)('P'))
            {
                mode[0] = (byte)('P');
                mode[1] = 0;
                mode[2] = 0;
            }
            Thread thrSend = new Thread(broadMessage);
            thrSend.Start(mode);
        }

        private void broadMessage(object obj)
        {
            byte[] sendbytes = (byte[])obj;
            broadSend.Send(sendbytes, 3, broadRemote);
        }

        private void btn_Scope_Click(object sender, EventArgs e)
        {
            zedGraphAxisSet_Realtime();

            mode_now = (byte)('S');
            SetMode(mode_setting, time_per_data);
            for (int i =0; i < num_online; i ++)
            {
                dataGridState.Rows[UdpOnline[i]].Cells[2].Value = 0;
                zedGraphVibShow_Realtime(UdpOnline[i]);
                UdpSvr[UdpOnline[i]].SetMode(mode_setting,time_per_data);
                UdpSvr[UdpOnline[i]].SetScope();
            }

            timer_draw.Enabled = true;
        }

        private void btn_Capture_Click(object sender, EventArgs e)
        {
            if (FrmLF.FileName == "")
            {
                MessageBox.Show("请先在“设置”中设置数据保存路径");
                return;
            }

            zedGraphAxisSet_Realtime();

            mode_now = (byte)('C');
            SetMode(mode_setting, time_per_data);
            for (int i = 0; i < num_online; i++)
            {
                dataGridState.Rows[UdpOnline[i]].Cells[2].Value = 0;
                zedGraphVibShow_Realtime(UdpOnline[i]);
                UdpSvr[UdpOnline[i]].SetMode(mode_setting,time_per_data);
                UdpSvr[UdpOnline[i]].SetCapture(dataPath);
            }

            timer_draw.Enabled = true;
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            string strDataPath;
            string[] arr;
            int num;

            timer_draw.Enabled = false;
            SetMode((byte)('P'), 0);
            for (int i = 0; i < num_online; i++)
            {
                UdpSvr[UdpOnline[i]].SetStop();
                UdpSvr[UdpOnline[i]].SetMode((byte)('P'),0);
            }

            if (mode_now == (byte)('C'))
            {
                zedGraphAxisSet_History();
                for (int i = 0; i < num_online; i++)
                {
                    UdpSvr[UdpOnline[i]].compensate_lost();
                    strDataPath = UdpSvr[UdpOnline[i]].dataPath;
                    arr = strDataPath.Split(' ');
                    num = int.Parse(arr[arr.Count() - 5]);
                    IsFFT[num] = true;
                    dataPath_His[num] = strDataPath;
                    time_interval_His = 1.0 / int.Parse(arr[arr.Count() - 4]);
                    time_frequency_His = int.Parse(arr[arr.Count() - 4]);
                    ZeroLine = double.Parse(arr[arr.Count() - 3]);
                    Modulus = double.Parse(arr[arr.Count() - 2]);
                    display_offline(UdpSvr[UdpOnline[i]].filedata, num, time_interval_His);
                }
            }
            mode_now = (byte)('P');
        }

        private void btn_test_connect_Click(object sender, EventArgs e)
        {
            if(btn_test_connect.Text == "连接完成")
            {
                btn_test_connect.Text = "测试连接";
                timer_state_online.Enabled = false;
                for (int i = 0; i < MAX_UDP_CONNECT; i++)
                {
                    UdpSvr[i].AbortThread();
                    if(UdpSvr[i].state_online == true)
                    {
                        UdpOnline[num_online] = UdpSvr[i].ClientNum;
                        num_online++;
                        auto_check(i);
                    }
                }
            }
            else
            {
                txtTestProgram.Text += "检查连接中...\r\n";
                btn_test_connect.Text = "连接完成";
                for (int i = 0; i < MAX_UDP_CONNECT; i++)
                {
                    UdpSvr[i].StartClientInquire();
                    dataGridState.Rows[i].Cells[1].Value = "";
                    UdpOnline[i] = 0;
                }
                num_online = 0;
                timer_state_online.Enabled = true;
            }
        }

        private void timer_state_online_Tick(object sender, EventArgs e)
        {
            for(int i = 0; i < MAX_UDP_CONNECT; i ++)
            {
                if(UdpSvr[i].state_online == true) dataGridState.Rows[i].Cells[1].Value = "在线";
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            pic_fft.Location = new Point(trackBar1.Value + trsprt_layer_offset, 48);
            textBox1.Text = trackBar1.Value.ToString();
        }

        private void fFT设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFFT.ShowDialog();
            if (FrmFFT.DialogResult == DialogResult.OK)
            {
                num_FFT = FrmFFT.FFTPoints;
            }
        }

        private void btn_zero_HF1_Click(object sender, EventArgs e)
        {
            if (dataGridState.Rows[0].Cells[1].Value != "在线")
            {
                MessageBox.Show("该通道不在线上,无法调零");
                return ;
            }
            UdpSvr[0].IsZero = true;
            UdpSvr[0].StartClientInquire();
            while (UdpSvr[0].IsZero == true)
            {
                Thread.Sleep(10);
            }
            UdpSvr[0].AbortThread();
            ShowMessage(txtTestProgram, "高频1路调零完成");
        }

        private void btn_zero_LF1_Click(object sender, EventArgs e)
        {
            if (dataGridState.Rows[1].Cells[1].Value != "在线")
            {
                MessageBox.Show("该通道不在线上,无法调零");
                return;
            }
            UdpSvr[1].IsZero= true;
            UdpSvr[1].StartClientInquire();
            while (UdpSvr[1].IsZero == true)
            {
                Thread.Sleep(10);
            }
            UdpSvr[1].AbortThread();
            ShowMessage(txtTestProgram, "低频1路调零完成");
        }

        private void btn_zero_LF2_Click(object sender, EventArgs e)
        {
            if (dataGridState.Rows[2].Cells[1].Value != "在线")
            {
                MessageBox.Show("该通道不在线上,无法调零");
                return;
            }
            UdpSvr[2].IsZero = true;
            UdpSvr[2].StartClientInquire();
            while (UdpSvr[2].IsZero == true)
            {
                Thread.Sleep(10);
            }
            UdpSvr[2].AbortThread();
            ShowMessage(txtTestProgram, "低频2路调零完成");
        }

        private void btn_zero_LF3_Click(object sender, EventArgs e)
        {
            if (dataGridState.Rows[3].Cells[1].Value != "在线")
            {
                MessageBox.Show("该通道不在线上,无法调零");
                return;
            }
            UdpSvr[3].IsZero = true;
            UdpSvr[3].StartClientInquire();
            while (UdpSvr[3].IsZero == true)
            {
                Thread.Sleep(10);
            }
            UdpSvr[3].AbortThread();
            ShowMessage(txtTestProgram, "低频3路调零完成");
        }

        private void btn_zero_LF4_Click(object sender, EventArgs e)
        {
            if (dataGridState.Rows[4].Cells[1].Value != "在线")
            {
                MessageBox.Show("该通道不在线上,无法调零");
                return;
            }
            UdpSvr[4].IsZero = true;
            UdpSvr[4].StartClientInquire();
            while (UdpSvr[4].IsZero == true)
            {
                Thread.Sleep(10);
            }
            UdpSvr[4].AbortThread();
            ShowMessage(txtTestProgram, "低频4路调零完成");
        }

        private void 调零ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mode_now != (byte)('P'))
            {
                MessageBox.Show("请先停止采集");
                return;
            }
            FrmZero.ShowDialog();
        }

        private void 标定ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mode_now != (byte)('P'))
            {
                MessageBox.Show("请先停止采集");
                return;
            }
            FrmModul.ShowDialog();
        }

        private void btn_choose_fft_Click(object sender, EventArgs e)
        {
            double sum_points;
            Scale xScale = graphShow.GraphPane.XAxis.Scale;

            if (btn_choose_fft.Text == "FFT取点")
            {
                btn_choose_fft.Text = "FFT微调";
                trackBar1.Value = 484;
                trackBar1.Visible = true;
                pic_fft.Visible = true;
                pic_fft.Enabled = true;
                pic_fft.Parent = graphShow;
                graphShow.IsEnableHZoom = false;    
                sum_points = (xScale.Max - xScale.Min) / time_interval_His;
                pic_fft.Size = new Size((int)(num_FFT * 864 / sum_points), 210);//1024 * (903 - 39) = 892928
                trsprt_layer_offset = 10 - pic_fft.Width / 2;
                pic_fft.Location = new Point(trackBar1.Value + trsprt_layer_offset, 48);
            }
            else if(btn_choose_fft.Text == "FFT微调")
            {
                btn_choose_fft.Text = "FFT变换";
                trackBar1.Visible = false;
                pic_fft.Visible = false;
                pic_fft.Enabled = false;
                xScale.Min = (trackBar1.Value - 39 - pic_fft.Size.Width / 2) / 864.0 * (xScale.Max - xScale.Min) + xScale.Min;
                xScale.Max = xScale.Min + num_FFT * time_interval_His;
                graphShow.AxisChange();
                graphShow.Refresh();
            }
            else if(btn_choose_fft.Text == "FFT变换")
            {
                int i = 0, k = 0, iFFTStart, iPackPosition, iDataPosition, iPackMiddle;
                byte[] bOrder = new byte[4];
                byte[] bData = new byte[1000];
                double MO;

                btn_choose_fft.Text = "FFT取点";
                zedGraphAxisSet_Spectrum();

                for (int num = 0; num < MAX_UDP_CONNECT; num ++)
                {
                    if(IsFFT[num] == true)
                    {
                        zedGraphVibShow_Spectrum(num);

                        filedataHis = new FileStream(dataPath_His[num], FileMode.Open);

                        VibrationDataHisEventHandler VibrationDataHis;
                        if(num == 0) VibrationDataHis = VibrationDataHis_HF;
                        else VibrationDataHis = VibrationDataHis_LF;

                        k = 0;

                        iFFTStart = (int)(xScale.Min / time_interval_His) + iFirst * 100;
                        iPackPosition = iFFTStart / 100;
                        iDataPosition = iFFTStart % 100;
                        iPackMiddle = (num_FFT - (100 - iDataPosition)) / 100;

                        filedataHis.Position = 305 * iPackPosition;

                        filedataHis.Read(bData, 0, 305);
                        for (i = 4 + iDataPosition * 3; i <= 301; i += 3)
                        {
                            MO = VibrationDataHis(i, bOrder, bData);
                            FFTData[k] = (float)(MO);
                            k++;
                        }
                        for (int j = 0; j < iPackMiddle; j++)
                        {
                            filedataHis.Read(bData, 0, 305);
                            for (i = 4; i <= 301; i += 3)
                            {
                                MO = VibrationDataHis(i, bOrder, bData);
                                FFTData[k] = (float)(MO);
                                k++;
                            }
                        }
                        filedataHis.Read(bData, 0, 305);
                        for (i = 4; k < num_FFT; i += 3)
                        {
                            MO = VibrationDataHis(i, bOrder, bData);
                            FFTData[k] = (float)(MO);
                            k++;
                        }
                        filedataHis.Dispose();
                        filedataHis.Close();

                        do_fft(num_FFT, FFTPower, FFTData);

                        for (i = 0; i < num_FFT / 2; i++)
                        {
                            specLine[num, 0].Add((double)(time_frequency_His) * i / num_FFT, FFTPower[i]);// - vibzero[0, num]);
                        }
                    }
                }

                graphAnlys.AxisChange();
                graphAnlys.Refresh();
                graphShow.IsEnableHZoom = true;
            }
        }

        private void btn_wipe_graph_Click(object sender, EventArgs e)
        {
            GraphPane pane = graphShow.GraphPane;
            pane.CurveList.Clear();
            pane.GraphObjList.Clear();
            graphShow.AxisChange();
            graphShow.Refresh();
            chBLineShow1.Checked = false;
            chBLineShow2.Checked = false;
            chBLineShow3.Checked = false;
            chBLineShow4.Checked = false;
            chBLineShow5.Checked = false;
            for (int i = 0; i < MAX_UDP_CONNECT; i ++)
            {
                IsFFT[i] = false;
            }
        }

        private void btn_cancel_fft_Click(object sender, EventArgs e)
        {
            btn_choose_fft.Text = "FFT取点";
            graphShow.IsEnableHZoom = true;
            trackBar1.Visible = false;
            pic_fft.Visible = false;
            pic_fft.Enabled = false;
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < num_online; i++)
            {
                UdpSvr[UdpOnline[i]].filedata.SetLength(0);
            }
            zedGraphAxisSet_Realtime();
        }

        private void timer_draw_Tick(object sender, EventArgs e)
        {
            //txt_Lose.Text = pack_lose.ToString();
            //this.Invoke(new EventHandler(delegate
            double time_min = UdpSvr[UdpOnline[0]].time;

            Scale xScale = graphShow.GraphPane.XAxis.Scale;
                if (time_min > xScale.Max - xScale.MinorStep)
                {
                    xScale.Min = xScale.Max;
                    xScale.Max = time_min + _XScaleMax;
                    for (int i = 0; i < num_online; i++)
                    {
                        dataGridState.Rows[UdpOnline[i]].Cells[2].Value = UdpSvr[UdpOnline[i]].pack_lose;
                        UdpSvr[UdpOnline[i]].time = time_min;
                    }
                }

            try
            {
                graphShow.AxisChange();
                graphShow.Refresh();
            }
            catch(Exception ex)
            {

            }
        }
    }
}
