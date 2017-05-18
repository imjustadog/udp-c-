using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
namespace 无线采集
{
    class ReceiveData
    {
        private List<byte> receiveData = new List<byte>();
       
        public byte[,] webAddress = new byte[216,8];
        public double[,] vibrationDataMo = new double[216, 6];
        public double[,] vibrationDataHz = new double[216, 6];
        public double[,] vibrationDataTm = new double[216, 6];
        public double Pre_power_vib = new double();
        public double Pre_power_def = new double();
        public double[] vibrationDataPow = new double[216];
        //public bool[,] vibrationStartCompare = new bool[45, 6];
        public double[,] deflectionDataVol = new double[216, 2];
        public double[] deflectionDataPow = new double[216];
        public List<Button> btnlist_temp =new List<Button>();
        public  ReceiveData(List<byte> redata)
        {
            receiveData = redata;
        }
        public ReceiveData( )
        {
           
        }
        public void WebData(int num)
        {
            for (int i = 0; i < 8; i++)
            {
                webAddress[num - 1, i] = receiveData[3 + i];
            }
            //return webAddress;
        }
        public void VibrationData(int num,double[,] sensorparameter)
        {
            for (int i = 0; i < 6; i++)
            {
                if ((int)receiveData[3 + 5 * i] == i)
                {
                    double temp = (double)((int)receiveData[4 + 5 * i] * 256 + (int)receiveData[5 + 5 * i]);
                    double mo ;
                    temp = 10000000 / temp;
                    double hz = Math.Round(temp, 2);
                    vibrationDataHz[num - 1, i] = hz;
                   mo = sensorparameter[num - 1, i] * hz * hz / 1000.0;
                   vibrationDataMo[num - 1, i] = Math.Round(mo, 2);
                    double tep = (double)((int)receiveData[6 + 5 * i] * 256 + (int)receiveData[7 + 5 * i]);
                    tep = tep * 4.5185;
                    double A = 1.4051 * (1e-3);
                    double B = 2.369 * (1e-4);
                    double C = 1.019 * (1e-7);
                    tep = 1 / (A + B * Math.Log(tep) + C * Math.Log(tep) * Math.Log(tep) * Math.Log(tep)) - 273.2;
                    vibrationDataTm[num - 1, i] = Math.Round(tep, 1);
                    double pw = (double)((int)receiveData[33] * 256 + (int)receiveData[34]);
                    pw = Pre_power_vib * 0.9 + pw * 0.1;
                    Pre_power_vib = pw;

                    if (pw > 1600)
                    {
                        pw = 1600;
                    }
                    if (pw < 1350)
                    {
                        pw = 1350;
                    }
                    vibrationDataPow[num-1]=Math.Round((pw-1350)/250,2);
                }
            }
        }
        public void DeflectionData(int num, double[,] sensorparameter)
        {
            for (int i = 0; i < 2; i++)
            {
                if ((int)receiveData[3 + 3 * i] == (i + 1))
                {
                    double Vol = (double)((int)receiveData[4 + 3 * i] * 256 + (int)receiveData[5 + 3 * i]);
                    deflectionDataVol[num - 1, i] = (Math.Round(Vol / 65535 * 5 / sensorparameter[num - 1, i], 2));
                }
            }
            double pw = (double)((int)receiveData[9] * 256 + (int)receiveData[10]);
            pw = Pre_power_def * 0.9 + pw * 0.1;
            Pre_power_def = pw;
            if (pw > 400)
            {
                pw = 400;
            }
            if (pw < 337.5)
            {
                pw = 337.5;
            }
           deflectionDataPow[num - 1] = Math.Round((pw - 337.5) / 62.5 , 2);

        }
        public void StoreNetAdd(byte[,] data, int num,string path,string projectname)
        {
            FileStream afile = new FileStream(path + "\\" + projectname + "\\地址\\" + num + ".txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(afile);
            for (int i = 0; i < 8; i++)
            {
                sw.WriteLine(data[num - 1, i].ToString());
            }
            sw.Close();
            afile.Close();
        }
        public void ReControlData(int num,List<Button> btnlist)
        {
            btnlist_temp = btnlist;
            int yushu = num % 15;
            int shang=num/15;
            int tem = shang* 45 + yushu - 1;
            int temp = (shang-1) * 45 +15-1;
            #region
            if (yushu == 0)
            {
                ///////在线状态回复
                if (receiveData[32] == 0xff && receiveData[33] == 0x00 && receiveData[34] == 0xff)
                {
                    btnlist[temp].BackColor = Color.Red;
                    btnlist[temp + 15].BackColor = Color.WhiteSmoke;
                    btnlist[temp + 30].BackColor = Color.WhiteSmoke;
                }
                //////离线状态回复
                if (receiveData[32] == 0x00 && receiveData[33] == 0x00 && receiveData[34] == 0xff)
                {
                    btnlist[temp].BackColor = Color.WhiteSmoke;
                    btnlist[temp+ 15].BackColor = Color.Red;
                    btnlist[temp + 30].BackColor = Color.WhiteSmoke;
                    Thread time_inter = new Thread((() => change_color_1(temp)));
                    time_inter.Start();
                }
                /////休眠状态回复
                if (receiveData[32] == 0x55 && receiveData[33] == 0x00 && receiveData[34] == 0x00)
                {
                    btnlist[temp].BackColor = Color.WhiteSmoke;
                    btnlist[temp + 15].BackColor = Color.WhiteSmoke;
                    btnlist[temp + 30].BackColor = Color.Red;
                    Thread time_inter = new Thread((() => change_color_2(temp)));
                    time_inter.Start();
                }
                /////时间间隔回复
                if (receiveData[32] == 0x00 && receiveData[33] == 0x00 && receiveData[34] == 0x55)
                {
                    btnlist[temp].BackColor = Color.WhiteSmoke;
                    btnlist[temp + 15].BackColor = Color.Blue;
                    btnlist[temp + 30].BackColor = Color.WhiteSmoke;
                    Thread time_inter = new Thread((() => change_color(temp)));
                    time_inter.Start();
                }
                /////时间回复
                if (receiveData[32] == 0x00 && receiveData[33] == 0xFF && receiveData[34] == 0x00)
                {
                    btnlist[temp].BackColor = Color.WhiteSmoke;
                    btnlist[temp + 15].BackColor = Color.Purple;
                    btnlist[temp + 30].BackColor = Color.WhiteSmoke;
                    Thread time_inter = new Thread((() => change_color(temp)));
                    time_inter.Start();
                }
              
            }
            else
            {
                ///////在线状态回复
                if (receiveData[32] == 0xff && receiveData[33] == 0x00 && receiveData[34] == 0xff)
                {
                    btnlist[tem].BackColor = Color.Red;
                    btnlist[tem + 15].BackColor = Color.WhiteSmoke;
                    btnlist[tem + 30].BackColor = Color.WhiteSmoke;
                }
                //////离线状态回复
                if (receiveData[32] == 0x00 && receiveData[33] == 0x00 && receiveData[34] == 0xff)
                {
                    btnlist[tem].BackColor = Color.WhiteSmoke;
                    btnlist[tem+ 15].BackColor = Color.Red;
                    btnlist[tem + 30].BackColor = Color.WhiteSmoke;
                    Thread time_inter = new Thread((() => change_color_1(tem)));
                    time_inter.Start();
                }
                /////休眠状态回复
                if (receiveData[32] == 0x55 && receiveData[33] == 0x00 && receiveData[34] == 0x00)
                {
                    btnlist[tem].BackColor = Color.WhiteSmoke;
                    btnlist[tem + 15].BackColor = Color.WhiteSmoke;
                    btnlist[tem + 30].BackColor = Color.Red;
                    Thread time_inter = new Thread((() => change_color_2(tem)));
                    time_inter.Start();
                }
                /////时间间隔回复
                if (receiveData[32] == 0x00 && receiveData[33] == 0x00 && receiveData[34] == 0x55)
                {
                    btnlist[tem].BackColor = Color.WhiteSmoke;
                    btnlist[tem + 15].BackColor = Color.Blue;
                    btnlist[tem + 30].BackColor = Color.WhiteSmoke;
                    Thread time_inter = new Thread((() => change_color(tem)));
                    time_inter.Start();
                }
                /////时间回复
                if (receiveData[32] == 0x00 && receiveData[33] == 0xFF && receiveData[34] == 0x00)
                {
                    btnlist[tem].BackColor = Color.WhiteSmoke;
                    btnlist[tem + 15].BackColor = Color.Purple;
                    btnlist[tem+ 30].BackColor = Color.WhiteSmoke;
                    Thread time_inter = new Thread((() => change_color(tem)));
                    time_inter.Start();
                }

            }
            #endregion
        
        }
        private void change_color(int num)
        {
            //string num = obj as string;
            
            Thread.Sleep(1000);
            btnlist_temp[num + 15].BackColor = Color.WhiteSmoke;
        }
        private void change_color_1(int num)
        {
            //string num = obj as string;

            Thread.Sleep(2000);
            btnlist_temp[num].BackColor = Color.WhiteSmoke;
        }
        private void change_color_2(int num)
        {
            //string num = obj as string;

            Thread.Sleep(2000);
            btnlist_temp[num].BackColor = Color.WhiteSmoke;
        }
    }
}
