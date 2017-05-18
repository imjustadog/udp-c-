using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 无线采集
{
    class SendData
    {
        private byte[] StrToHexByte(string s)
        {
            byte[] retByte = new byte[2];
            string sn = "A" + s;
            for (int i = 0; i < 2; i++)
            {
                retByte[i] = Convert.ToByte(sn.Substring(i * 2, 2), 16);
            }
            return retByte;
        }
        private byte StrToHexYu(string s)
        {
            double t=0;
            double j;
            int k= s.Length;
          
            for (int i = 0; i < k; i++)
            {
               double m = k - 1 - i;
                j = int.Parse(s.Substring(i, 1));
                j = j * Math.Pow(16, m);
                t = t+j;
            }
            return (byte)t;
        }
        public byte[] ONline(byte[,] ad, string s,int num)
        {
            byte[] On = new byte[35];
            byte[] tem = new byte[2];
            On[0] = 0x53;
            On[1] = 0x19;
            On[2] = 0x43;
            On[3] = ad[num-1,0];
            On[4] = ad[num-1,1];
            On[5] = ad[num-1,2];
            On[6] = ad[num - 1,3];
            On[7] = ad[num - 1,4];
            On[8] = ad[num - 1,5];
            On[9] = ad[num - 1,6];
            On[10] = ad[num - 1,7];
            tem = StrToHexByte(s);
            On[11] = tem[0];
            On[12] = tem[1];
            On[13] = 0x28;
            for (int i = 14; i < 19; i++)
            {
                On[i] = 0xFF;
            }
            On[19] = 0x00;
            for (int i = 20; i < 34; i++)
            {

                On[i] = 0xFF;
            }
            On[34] = 0x45;
            return On;
        }
   public byte[] SleepLine(byte[,] ad, string s,int num)
        {
            byte[] Sleep = new byte[35];
            byte[] tem = new byte[2];
            Sleep[0] = 0x53;
            Sleep[1] = 0x19;
            Sleep[2] = 0x43;
            Sleep[3] = ad[num-1,0];
            Sleep[4] = ad[num-1,1];
            Sleep[5] = ad[num-1,2];
            Sleep[6] = ad[num-1,3];
            Sleep[7] = ad[num-1,4];
            Sleep[8] = ad[num-1,5];
            Sleep[9] = ad[num-1,6];
            Sleep[10] = ad[num-1,7];
            tem = StrToHexByte(s);
            Sleep[11] = tem[0];
            Sleep[12] = tem[1];
            Sleep[12] = 0xFF;
            Sleep[13] = 0x28;
            Sleep[14] = 0xFF;
            Sleep[15] = 0xFF;
            Sleep[16] = 0x55;
            Sleep[17] = 0xFF;
            Sleep[18] = 0xFF;
            Sleep[19] = 0x00;
            for (int i = 20; i < 26; i++)
            {

                Sleep[i] = 0xFF;
            }
            Sleep[26] = 0x00;
            for (int i = 27; i < 34; i++)
            {

                Sleep[i] = 0xFF;
            }
            Sleep[34] = 0x45;
            return Sleep;
        }
     public byte[] LeaveLine(byte[,] ad, string s,int num)
        {
            byte[] Leave = new byte[35];
            byte[] tem = new byte[2];
            Leave[0] = 0x53;
            Leave[1] = 0x19;
            Leave[2] = 0x43;
            Leave[3] = ad[num-1,0];
            Leave[4] = ad[num-1,1];
            Leave[5] = ad[num-1,2];
            Leave[6] = ad[num-1,3];
            Leave[7] = ad[num-1,4];
            Leave[8] = ad[num-1,5];
            Leave[9] = ad[num-1,6];
            Leave[10] = ad[num-1,7];
            tem = StrToHexByte(s);
            Leave[11] = tem[0];
            Leave[12] = tem[1];
            Leave[13] = 0x28;
            Leave[14] = 0xFF;
            Leave[15] = 0xFF;
            Leave[16] = 0x00;
            Leave[17] = 0xFF;
            Leave[18] = 0xFF;
            Leave[19] = 0x00;
            for (int i = 20; i < 34; i++)
            {

                Leave[i] = 0xFF;
            }
            Leave[34] = 0x45;
            return Leave;

        }
     public byte[] NowTime(byte[,] ad, string s, int num)
     {
         byte[] Now = new byte[35];
         byte[] tem = new byte[2];
         DateTime dt = DateTime.Now;
         string y = dt.Year.ToString();
         string m = dt.Month.ToString();
         string d = dt.Day.ToString();
         string h = dt.Hour.ToString();
         string mi = dt.Minute.ToString();
         string sd = dt.Second.ToString();
         byte year = StrToHexYu(y.Substring(2, 2));
         byte mon = StrToHexYu(m);
         byte day = StrToHexYu(d);
         byte hour = StrToHexYu(h);
         byte min = StrToHexYu(mi);
         byte sec = StrToHexYu(sd);
         Now[0] = 0x53;
         Now[1] = 0x19;
         Now[2] = 0x43;
         Now[3] = ad[num - 1, 0];
         Now[4] = ad[num - 1, 1];
         Now[5] = ad[num - 1, 2];
         Now[6] = ad[num - 1, 3];
         Now[7] = ad[num - 1, 4];
         Now[8] = ad[num - 1, 5];
         Now[9] = ad[num - 1, 6];
         Now[10] = ad[num - 1, 7];
         tem = StrToHexByte(s);
         Now[11] = tem[0];
         Now[12] = tem[1];
         Now[13] = 0x28;
         Now[14] = 0xFF;
         Now[15] = 0xFF;
         Now[16] = 0x55;
         Now[17] = 0xFF;
         Now[18] = 0xFF;
         Now[19] = 0x00;
         Now[20] = year;
         Now[21] = mon;
         Now[22] = day;
         Now[23] = hour;
         Now[24] = min;
         Now[25] = sec;
         for (int i = 26; i < 34; i++)
         {
             Now[i] = 0xFF;
         }
        Now[34] = 0x45;
         return Now;

     }
     public byte[] SetInterTime(byte[,] ad, string s, int num,byte[] intertm)
     {
         byte[] intertime = new byte[35];
         byte[] tem = new byte[2];
         intertime[0] = 0x53;
         intertime[1] = 0x19;
         intertime[2] = 0x43;
         intertime[3] = ad[num - 1, 0];
         intertime[4] = ad[num - 1, 1];
         intertime[5] = ad[num - 1, 2];
         intertime[6] = ad[num - 1, 3];
         intertime[7] = ad[num - 1, 4];
         intertime[8] = ad[num - 1, 5];
         intertime[9] = ad[num - 1, 6];
         intertime[10] = ad[num - 1, 7];
         tem = StrToHexByte(s);
         intertime[11] = tem[0];
         intertime[12] = tem[1];
         intertime[13] = 0x28;
         intertime[14] = 0xFF;
         intertime[15] = 0xFF;
         intertime[16] = 0x55;
         intertime[17] = 0xFF;
         intertime[18] = intertm[1];
         intertime[19] = intertm[0];
         for (int i = 20; i < 34; i++)
         {
             intertime[i] = 0xFF;
         }
         intertime[34] = 0x45;
         return intertime;
     }
     public byte[] ReadLeaveData(byte[,] ad, string s, int num)
     {
         byte[] tem = new byte[2];
         byte[] readleavedata = new byte[35];
         readleavedata[0] = 0x53;
         readleavedata[1] = 0x19;
         readleavedata[2] = 0x43;
         readleavedata[3] = ad[num - 1, 0];
         readleavedata[4] = ad[num - 1, 1];
         readleavedata[5] = ad[num - 1, 2];
         readleavedata[6] = ad[num - 1, 3];
         readleavedata[7] = ad[num - 1, 4];
         readleavedata[8] = ad[num - 1, 5];
         readleavedata[9] = ad[num - 1, 6];
         readleavedata[10] = ad[num - 1, 7];
         tem = StrToHexByte(s);
         readleavedata[11] = tem[0];
         readleavedata[12] = tem[1];
         readleavedata[13] = 0x28;
         for (int i = 14; i < 34; i++)
         {
             readleavedata[i] = 0xFF;
         }
         readleavedata[34] = 0x45;
         return readleavedata;
     }
    }
}
