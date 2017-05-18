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

namespace 无线采集
{
    public partial class UdpClass
    {
        public IPEndPoint IpepRemote;
        public UdpClient udpcSend;
        public UdpClient udpcRecv;
        public BinaryWriter bwData;
        public byte flag_first;
        public string dataPath = "";
        public string dataNamePath = "";
        public FileStream filedata;
        public byte[] lose = new byte[3] { (byte)('L'), 0, 0 };
        public byte[] bOrder = new byte[4];
        public byte[] bData = new byte[1000];
        public Queue<byte[]> qDataWrite = new Queue<byte[]>();
        public double time;
        public int time_per_data;
        public int trig_set;
        public double time_interval;
        public int time_frequency;
        public int bQueueCount;
        public Int32 pack_num = 0, pack_lose = 0, pack_temp;
        public Thread thrRecv;
        public Thread thrDraw;
        static object mylock = new object();
        public LineItem[] vibCurve = new LineItem[6];
        public RollingPointPairList[] vibLine = new RollingPointPairList[6];
        public double[] vibrationMO = new double[6];
        public double[] vibzero = new double[6];
        private delegate void VibrationDataEventHandler(int num);
        private VibrationDataEventHandler VibrationData;
        private MainFrom mFrm;
        public int ClientNum;
        public bool state_online = false;
        public bool IsZero = false;
        public double ZeroLine = 0;
        public double Modulus = 1;

        public UdpClass(int i,MainFrom mf)
        {
            string[] arr1, arr2;

            if (i == 0) VibrationData = VibrationData_HF;
            else VibrationData = VibrationData_LF;
            mFrm = mf;
            ClientNum = i;

            StreamReader srz = new StreamReader("statezero.txt", System.Text.Encoding.Default);
            string tempz = srz.ReadToEnd();
            srz.Close();
            arr1 = tempz.Split(' ');
            ZeroLine = double.Parse(arr1[ClientNum]);

            StreamReader srm = new StreamReader("statemodulus.txt", System.Text.Encoding.Default);
            string tempm = srm.ReadToEnd();
            srm.Close();
            arr2 = tempm.Split(' ');
            Modulus = double.Parse(arr2[ClientNum]);

            pack_num = 1;
            pack_lose = 0;
            bQueueCount = 0;

            time = 0;
        }
        public void InitRecv()
        {
            IPEndPoint localIpepS;
            IPEndPoint localIpepR;
            int IPfour;
            IPfour = 107 + ClientNum;

            localIpepS = new IPEndPoint(
            IPAddress.Parse("192.168.0.100"), 1199 + ClientNum); // 本机IP，指定的端口号
            udpcSend = new UdpClient(localIpepS);

            localIpepR = new IPEndPoint(
            IPAddress.Parse("192.168.0.100"), 1399 + ClientNum); // 本机IP，指定的端口号
            udpcRecv = new UdpClient(localIpepR);

            IpepRemote = new IPEndPoint(
            IPAddress.Parse("192.168.0." + IPfour.ToString()), 1199 + ClientNum); // 发送到的IP地址和端口号
           
            StartClientInquire();
        }

        #region UDP接收线程
        private void ReceiveMessage(object obj)
        {
            IPEndPoint remoteIpepS = new IPEndPoint(IPAddress.Any, 0);//new IPEndPoint(IPAddress.Parse("192.168.0.108"), 1001);
            while (true)
            { 
                try
                {
                    byte[] bytRecv = udpcRecv.Receive(ref remoteIpepS);
                    if (bytRecv.Length != 0)
                    {
                        qDataWrite.Enqueue(bytRecv);
                        bQueueCount++;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
        }
        #endregion
        #region 在线查询线程
        public void StartClientInquire()
        {
            state_online = false;

            bQueueCount = 0;

            thrRecv = new Thread(ReceiveMessage);
            thrRecv.Start();

            thrDraw = new Thread(ClientInquire);
            thrDraw.Start();
        }
        private void ClientInquire(object obj)
        {
            double sum = 0;
            while (true)
            {
                try
                {
                    if (bQueueCount > 0)
                    {
                        bData = (byte[])qDataWrite.Dequeue();
                        state_online = true;
                        if(IsZero == true)
                        {
                            for (int i = 4; i <= 301; i += 3)
                            {
                                VibrationData(i);
                                sum += vibrationMO[0];
                            }
                            ZeroLine = Math.Round(sum/100, 2);
                            IsZero = false;
                            break;
                        }
                        bQueueCount--;
                    }
                    Thread.Sleep(3);
                }

                catch (Exception ex)
                {
                    mFrm.ShowMessage(mFrm.txtTestProgram, ex.Message);
                    break;
                }
            }
        }
        #endregion
        #region 数据显示线程
        private void DataDisplayer(object obj)
        {
            while (true)
            {
                try
                {
                    if (bQueueCount > 0)
                    {
                        bData = (byte[])qDataWrite.Dequeue();

                        Array.Copy(bData, 1, bOrder, 0, 3);

                        pack_temp = System.BitConverter.ToInt32(bOrder, 0);
                        if (pack_temp - pack_num != 1)
                        {
                            pack_lose++;
                        }
                        pack_num = pack_temp;

                        for (int i = 4; i <= 301; i += 3)
                        {
                            VibrationData(i);
                            vibCurveAddPiont(vibrationMO[0], time);
                            time += time_interval;
                        }

                        bQueueCount--;
                    }
                    Thread.Sleep(3);
                }

                catch (Exception ex)
                {
                    mFrm.ShowMessage(mFrm.txtTestProgram, ex.Message);
                    break;
                }
            }
        }
        #endregion
        #region 数据处理线程
        private void DataHandler(object obj)
        {
            while (true)
            {
                try
                {
                    if (bQueueCount > 0)
                    {
                        bData = (byte[])qDataWrite.Dequeue();

                        if (bData[0] == 'R')
                        {
                            bwData.Write(bData);

                            Array.Copy(bData, 1, bOrder, 0, 3);

                            pack_temp = System.BitConverter.ToInt32(bOrder, 0);
                            if (pack_temp - pack_num != 1)
                            {
                                if (flag_first == 0)
                                {
                                    Thread thrSend = new Thread(SendMessage);
                                    thrSend.Start(lose);
                                    pack_lose++;
                                }
                                else if (flag_first == 1 && time > 0.5)
                                {
                                    Thread thrSend = new Thread(SendMessage);
                                    thrSend.Start(lose);
                                    pack_lose++;
                                    flag_first = 0;
                                }
                                else flag_first = 0;
                            }
                            pack_num = pack_temp;

                            for (int i = 4; i <= 301; i += 3)
                            {
                                VibrationData(i);
                                vibCurveAddPiont(vibrationMO[0], time);
                                time += time_interval;
                            }
                        }
                        else if (bData[0] == 'P') ;
                        else
                        {
                            filedata.Position -= 305;
                            bwData.Write(bData);
                            pack_lose--;
                            flag_first = 2;
                        }
                        bQueueCount--;
                    }
                    Thread.Sleep(3);
                }

                catch (Exception ex)
                {
                    mFrm.ShowMessage(mFrm.txtTestProgram,ex.Message);
                    break;
                }
            }
        }
        #endregion
        private void VibrationData_HF(int num)
        {
            double dTemp;
            Array.Copy(bData, num, bOrder, 0, 3);
            dTemp = System.BitConverter.ToUInt32(bOrder, 0) / 1258291.2; ///8388608.0 ) * 5.0 * 1.33333 ;
            vibrationMO[0] = Math.Round(dTemp, 2);
        }
        private void VibrationData_LF(int num)
        {
            double dTemp;
            Array.Copy(bData, num, bOrder, 0, 3);
            dTemp = (System.BitConverter.ToInt32(bOrder, 0) - 0x800000) / 629145.6;
            vibrationMO[0] = Math.Round(dTemp, 2);
        }
        private void vibCurveAddPiont(double data, double time)
        {
            vibLine[0].Add(time, data);// - vibzero[0, num]);
        }

        public void compensate_lost()
        {
            int k = 0;
            int NumofPacks;
            byte[] temp = new byte[915];

            filedata.Position = 0;
            NumofPacks = (int)(filedata.Length / 305);

            while (k < NumofPacks)
            {
                filedata.Read(bData, 0, 1);
                filedata.Position += 304;
                if (bData[0] != (byte)('R'))
                {
                    filedata.Position -= 305;
                    filedata.Read(temp, 0, 915);
                    filedata.Position -= 915;
                    if (temp[0] == (byte)('O'))
                    {
                        filedata.Write(temp, 305, 305);
                        filedata.Write(temp, 610, 305);
                        filedata.Write(temp, 0, 305);
                    }
                    else if (temp[305] == (byte)('O'))
                    {
                        filedata.Write(temp, 610, 305);
                        filedata.Write(temp, 0, 305);
                        filedata.Write(temp, 305, 305);
                    }
                    else if (temp[610] == (byte)('O'))
                    {
                        filedata.Position += 915;
                    }
                    k = k + 3;
                }
                else k++;
            }
        }

        private void SendMessage(object obj)
        {
            byte[] sendbytes = (byte[])obj;

            lock (mylock)
            {
                udpcSend.Send(sendbytes, 3, IpepRemote);
            }
        }
        public void AbortThread()
        {
            if (thrRecv.IsAlive) thrRecv.Abort();
            if (thrDraw.IsAlive) thrDraw.Abort();
        }
        public void SetMode(byte mode_setting, int data)
        {
            if (mode_setting == (byte)('R'))
            {
                time_per_data = data;
                time_interval = time_per_data / 5000.0;
                time_frequency = 5000 / time_per_data;
            }
            else if (mode_setting == (byte)('M'))
            {
                trig_set = data;
            }
        }

        public void SetScope()
        {
            flag_first = 1;
            pack_num = 1;
            bQueueCount = 0;

            thrRecv = new Thread(ReceiveMessage);
            thrRecv.Start();

            thrDraw = new Thread(DataDisplayer);
            thrDraw.Start();

            time = 0;
        }

        public void SetCapture(string strDataPath) //只有位置和名字的路径
        {
            flag_first = 1;
            pack_num = 1;
            bQueueCount = 0;

            if (dataNamePath != strDataPath)
            {
                dataNamePath = strDataPath;
                strDataPath += (" " + ClientNum.ToString() + " " + time_frequency.ToString() + " " + ZeroLine.ToString() + " " + Modulus.ToString() + " .txt");
                filedata = new FileStream(strDataPath, FileMode.Create);
                bwData = new BinaryWriter(filedata);
                dataPath = strDataPath;
            }

            filedata.SetLength(0);

            thrRecv = new Thread(ReceiveMessage);
            thrRecv.Start();

            thrDraw = new Thread(DataHandler);
            thrDraw.Start();

            time = 0;
        }

        public void SetStop()
        {
            AbortThread();
            qDataWrite.Clear();
            pack_lose = 0;
            bQueueCount = 0;
        }

        public void SaveFile()
        {
            filedata.Dispose();
            filedata.Close();
        }

        public void DelateFile()
        {
            if (File.Exists(dataPath))
            {
                try
                {
                    if (filedata.Length == 0)
                    {
                        filedata.Dispose();
                        filedata.Close();
                        File.Delete(dataPath);
                    }
                }
                catch(Exception ex)
                {

                }
            }
        }
    }
}