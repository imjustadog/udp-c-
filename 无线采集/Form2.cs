using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace 无线采集
{
    public partial class Form2 : Form
    {
        public byte trig_mode = (byte)('R');
        public Form2( )
        {
            InitializeComponent();
            numericUpDownNum.Value = 0;
            txtName.Text = "";
            txtReadPath.Text = "C:\\Users\\zwq\\Desktop";
            comboFreq.Text = "1000";
        }
        public int Freq
        {
            get { return int.Parse(comboFreq.SelectedItem.ToString()); }
        }
        public string FilePath
        {
            get { return txtReadPath.Text; }
        }
        public string FileName
        {
            get { return txtName.Text; }
        }
        public string FileNum
        {
            get { return numericUpDownNum.Text; }
            set { numericUpDownNum.Text = value; }
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            if (comboFreq.Text == "") MessageBox.Show("请先选择频率");
            else this.DialogResult = DialogResult.OK;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
              {
                  txtReadPath.Text = fd.SelectedPath;
              }
        }

        private void rdobtn_AutoTrig_CheckedChanged(object sender, EventArgs e)
        {
            trig_mode = (byte)('R');
            groupBox4.Enabled = false;
        }

        private void rdobtn_SigTrig_CheckedChanged(object sender, EventArgs e)
        {
            trig_mode = (byte)('M');
            groupBox4.Enabled = true;
        }
    }
}
