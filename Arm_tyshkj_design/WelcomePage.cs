using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;     //播放声音文件

namespace Arm_tyshkj_design
{
  
    public partial class WelcomePage : Form
    {
        bool m_isMouseDown = false;//窗体是否移动
        Point m_mousePos;//记录窗体的位置
        public WelcomePage()
        {
            InitializeComponent();
        }
        #region 隐藏标题栏后移动窗口
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            m_isMouseDown = false;
            this.Focus();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (m_isMouseDown)
            {
                Point tempPos = Cursor.Position;
                this.Location = new Point(Location.X + (tempPos.X - m_mousePos.X), Location.Y + (tempPos.Y - m_mousePos.Y));
                m_mousePos = Cursor.Position;
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            m_mousePos = Cursor.Position;
            m_isMouseDown = true;
        }
        #endregion

        private void Exit_pictureBox_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void InsDet_button_Click(object sender, EventArgs e)
        {
            this.Hide();
//            InstrDetectPage IDP = new InstrDetectPage();
            //testSerialPort IDP = new testSerialPort();
//            IDP.ShowDialog();
//            this.Close();

            CustomerMangementPageTest CMPT = new CustomerMangementPageTest();
            CMPT.ShowDialog();

            this.Close();
        }

        private void CusMan_button_Click(object sender, EventArgs e)
        {
 //           this.Hide();
            CustomerMangementPageTest CMPT = new CustomerMangementPageTest();
            CMPT.ShowDialog();
 //           CustomerMangementPage CMP = new CustomerMangementPage();
 //           CMP.ShowDialog();
 //           this.Close();

        }

        private void WelcomePage_Load(object sender, EventArgs e)
        {
            //SoundPlayer p = new SoundPlayer();
            //p.SoundLocation = Application.StartupPath + "//WelcomeVoice.wav";
            //p.Load();
            //p.Play(); 
        }


    }
}
