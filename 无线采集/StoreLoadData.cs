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
    public partial class StoreLoadData : Form
    {
        public  string loadName;
        public StoreLoadData()
        {
            InitializeComponent();
         
        }

        private void btnok_Click(object sender, EventArgs e)
        {
            if (txtLoad.Text == "")
            {
                MessageBox.Show("请输入加载状态");
                return;
            }
            else
            {
                loadName = txtLoad.Text;
                this.DialogResult = DialogResult.OK;
            }

        }

        private void btncancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

        }
    }
}
