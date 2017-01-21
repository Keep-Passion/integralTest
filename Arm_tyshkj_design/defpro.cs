using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Arm_tyshkj_design
{
    public partial class defpro : Form
    {
        public defpro()
        {
            InitializeComponent();
        }

        public void SetProgressValue(int value)
        {
            if(value<90)
            this.progressBar1.Value = value+10;
            else
                this.progressBar1.Value = value;
            this.label1.Text = "进度：" + value.ToString() + "%";

            // 这里关闭，比较好，呵呵！  
            //if (value == this.progressBar1.Maximum - 1) this.Close();
        }

        public void SetProgressText(String value)
        {
            this.label1.Text = value;
        }
    }
}
