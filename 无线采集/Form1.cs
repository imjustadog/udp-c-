using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.IO;
using ADOX;
using System.Data.OleDb;
using System.Net;
namespace 无线采集
{
    public partial class Form1 : Form
    {

        public Form1( )
        {
            InitializeComponent();        
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                    if (AddressIP == "192.168.0.100") break;
                }
            }
            if (AddressIP == "192.168.0.100") this.label_IP.Text = "IP地址：" + AddressIP + "\r\n\r\n可以采集";
            else this.label_IP.Text = "注意：目前的IP状态只可回读，不可采集，\r\n\r\n如果需要进行数据的采集，请检查网口连接，\r\n\r\n如果网口正常连接，则需要将本地连接的\r\n\r\nIP地址手动设置为192.168.0.100，\r\n\r\n并将掩码设置为255.255.255.0";
        }
        private void button_OK_Click(object sender, EventArgs e)
        {
            MainFrom mFrm = new MainFrom();
            mFrm.Show();
            this.Hide();
        }
    }
}
