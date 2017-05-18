using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 无线采集
{
    public partial class Form5 : Form
    {
        public MainFrom mFrm;
        public Form5(MainFrom mf)
        {
            InitializeComponent();
            mFrm = mf;
            txt_HF1.Text = mFrm.UdpSvr[0].ZeroLine.ToString();
            txt_LF1.Text = mFrm.UdpSvr[1].ZeroLine.ToString();
            txt_LF2.Text = mFrm.UdpSvr[2].ZeroLine.ToString();
            txt_LF3.Text = mFrm.UdpSvr[3].ZeroLine.ToString();
            txt_LF4.Text = mFrm.UdpSvr[4].ZeroLine.ToString();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btn_apply_Click(object sender, EventArgs e)
        {
            mFrm.UdpSvr[0].ZeroLine = double.Parse(txt_HF1.Text);
            mFrm.UdpSvr[1].ZeroLine = double.Parse(txt_LF1.Text);
            mFrm.UdpSvr[2].ZeroLine = double.Parse(txt_LF2.Text);
            mFrm.UdpSvr[3].ZeroLine = double.Parse(txt_LF3.Text);
            mFrm.UdpSvr[4].ZeroLine = double.Parse(txt_LF4.Text);
            this.DialogResult = DialogResult.OK;
        }
    }
}
