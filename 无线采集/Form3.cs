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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            comboFFTPoints.Text = "1024";
        }
        public int FFTPoints
        {
            get { return int.Parse(comboFFTPoints.SelectedItem.ToString()); }
        }
        private void btn_apply_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
