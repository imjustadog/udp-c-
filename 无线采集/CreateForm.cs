using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
namespace 无线采集
{
    public partial class CreateForm : Form
    {
        public List<CheckBox> checkBoxList = new List<CheckBox>();
        public List<Button> btnList = new List<Button>();
        private int startnum;
        public byte[,] netAdd = new byte[216,8];
        public bool[] comadd = new bool[216];
        public SerialPort sr;
        public string[] moduleName = new string[216];
        public string path;
        public string projectname;
        public CreateForm(int stanum)
        {
            InitializeComponent();
            startnum = stanum;
            checkBoxList.Add(chkBox1); checkBoxList.Add(chkBox2); checkBoxList.Add(chkBox3); checkBoxList.Add(chkBox4); checkBoxList.Add(chkBox5);
            checkBoxList.Add(chkBox6); checkBoxList.Add(chkBox7); checkBoxList.Add(chkBox8); checkBoxList.Add(chkBox9); checkBoxList.Add(chkBox10);
            checkBoxList.Add(chkBox11); checkBoxList.Add(chkBox12); checkBoxList.Add(chkBox13); checkBoxList.Add(chkBox14); checkBoxList.Add(chkBox15);
            btnList.Add(btnOn1); btnList.Add(btnOn2); btnList.Add(btnOn3); btnList.Add(btnOn4); btnList.Add(btnOn5); btnList.Add(btnOn6); btnList.Add(btnOn7);
            btnList.Add(btnOn8); btnList.Add(btnOn9); btnList.Add(btnOn10); btnList.Add(btnOn11); btnList.Add(btnOn12); btnList.Add(btnOn13); btnList.Add(btnOn14); btnList.Add(btnOn15);
            btnList.Add(btnLeave1); btnList.Add(btnLeave2); btnList.Add(btnLeave3); btnList.Add(btnLeave4); btnList.Add(btnLeave5); btnList.Add(btnLeave6); btnList.Add(btnLeave7);
            btnList.Add(btnLeave8); btnList.Add(btnLeave9); btnList.Add(btnLeave10); btnList.Add(btnLeave11); btnList.Add(btnLeave12); btnList.Add(btnLeave13); btnList.Add(btnLeave14); btnList.Add(btnLeave15);
            btnList.Add(btnSleep1); btnList.Add(btnSleep2); btnList.Add(btnSleep3); btnList.Add(btnSleep4); btnList.Add(btnSleep5); btnList.Add(btnSleep6); btnList.Add(btnSleep7);
            btnList.Add(btnSleep8); btnList.Add(btnSleep9); btnList.Add(btnSleep10); btnList.Add(btnSleep11); btnList.Add(btnSleep12); btnList.Add(btnSleep13); btnList.Add(btnSleep14); btnList.Add(btnSleep15);
        }

        private void CreateForm_Load(object sender, EventArgs e)
        {
           
        }
        public void creatCheckBox()
        {
            for (int i = startnum; i < startnum + 15; i++)
            {
                checkBoxList[i - startnum].Name = "chkBox" + i.ToString();
                checkBoxList[i - startnum].Text = i.ToString();

            }
        }
        public void creatButton()
        {
         
            for (int i = startnum; i < startnum + 15; i++)
            {
               
                btnList[i - startnum].Name = "btnOn" + i.ToString();//在线按钮名字
                btnList[i - startnum + 15].Name = "btnLeave" + i.ToString();//离线按钮名字
                btnList[i - startnum + 30].Name = "btnSleep" + i.ToString();//休眠按钮名字
                btnList[i - startnum].Click += new EventHandler(CreateForm_btnOnClick);
                btnList[i - startnum + 15].Click+=new EventHandler(CreateForm_btnLeaveClick);
                btnList[i - startnum + 30].Click+=new EventHandler(CreateForm_btnSleepClick);
            }
        }
        ///发送控制指令//////////
        public void CreateForm_btnOnClick(object sender, EventArgs e)
        {
            
            Button btn = sender as Button;
            string idName = btn.Name.Remove(0, 5);
            int id = int.Parse(idName);
            netAdd = Record.netAdd;
            comadd = Record.comadd;
            path = Record.path;
            projectname = Record.projectName;
            sr = Record.sr;
            if (sr.IsOpen)
            {
                if (!comadd[id - 1])
                {
                    byte[] tempnet = new byte[8];
                    tempnet = mrm.GetAdd(id);
                    for (int i = 0; i < 8; i++)
                    {
                        netAdd[id - 1, i] = tempnet[i];
                    }
                }
                SendData sd = new SendData();
                sr.Write(sd.ONline(netAdd, Record.modulename[id - 1], id), 0, 35);
                Thread.Sleep(100);
                SendData sd_inter = new SendData();
                byte[] intervib = new byte[4];
                byte[] interdef = new byte[4];
                int vibt = 0;
                int deft = 0;
                intervib = BitConverter.GetBytes(vibt);
                interdef = BitConverter.GetBytes(deft);
                Record.interdef = interdef;
                Record.intervib = intervib;
                sr.Write(sd_inter.SetInterTime(netAdd, Record.modulename[id - 1], id, Record.intervib), 0, 35);
                Thread.Sleep(100);
            }
            else
                MessageBox.Show("串口未打开！");
            //break;
           // MessageBox.Show(id + "," + btn.Text + btn.Size);
        }
        public void CreateForm_btnLeaveClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string idName = btn.Name.Remove(0, 8);
            int id = int.Parse(idName);
            netAdd = Record.netAdd;
            comadd = Record.comadd;
            path = Record.path;
            projectname = Record.projectName;
            sr = Record.sr;
            if (sr.IsOpen)
            {
                if (!comadd[id - 1])
                {
                    MainFrom mrm = new MainFrom(projectname, path);
                    for (int i = 0; i < 8; i++)
                    {
                        netAdd[id - 1, i] = mrm.GetAdd(id)[i];
                    }
                }
                SendData sd = new SendData();
                sr.Write(sd.LeaveLine(netAdd, Record.modulename[id - 1], id), 0, 35);
            }
            else
                MessageBox.Show("串口未打开！");
            //添加设置采集当前时间、间隔
           // Thread.Sleep(100);
          // sr.Write(sd.NowTime(netAdd, Record.modulename[id - 1], id), 0, 35);
           // Thread.Sleep(100);
           //sr.Write(sd.SetInterTime(netAdd, Record.modulename[id - 1], id,Record.intervib), 0, 35);
           // MessageBox.Show(btn.Name + "," + btn.Text + btn.Size);
        }
        public void CreateForm_btnSleepClick(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string idName = btn.Name.Remove(0, 8);
            int id = int.Parse(idName);
            netAdd = Record.netAdd;
            comadd = Record.comadd;
            path = Record.path;
            projectname = Record.projectName;
            sr = Record.sr;
            if (sr.IsOpen)
            {
                if (!comadd[id - 1])
                {
                    MainFrom mrm = new MainFrom(projectname, path);
                    for (int i = 0; i < 8; i++)
                    {
                        netAdd[id - 1, i] = mrm.GetAdd(id)[i];
                    }
                }
                SendData sd = new SendData();
                sr.Write(sd.SleepLine(netAdd, Record.modulename[id - 1], id), 0, 35);
            }
            else
                MessageBox.Show("串口未打开！");
           // MessageBox.Show(btn.Name + "," + btn.Text + btn.Size);
        }

    }
}
