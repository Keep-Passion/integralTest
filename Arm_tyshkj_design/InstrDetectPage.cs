using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;
using Microsoft.Office.Interop.Word;
using System.Threading;

namespace Arm_tyshkj_design
{
    public partial class InstrDetectPage : Form
    {
        private bool enableChanged = true;  //设置是否可更换标签页
        private Byte[] outputBytes = new Byte[10];
        private int global_stdTestID = 0;               //当前标准测试数据的序号
        private int global_currentPage = 1;             //当前选的测试项目
        private int global_testSetID = -1;
        private int isFormalTest = 0;

        private String deviceName = "";
        private String SNID = "";

        private defpro progressForm = new defpro();
        // 代理定义，可以在Invoke时传入相应的参数  
        private delegate void funHandle(int nValue);
        private funHandle myHandle = null;   


        //private sbyte[] outputBytes = new sbyte[10];
        public InstrDetectPage()
        {
            InitializeComponent();
            tabControl.Selecting += new TabControlCancelEventHandler(tabControl_Selecting);
            PT_comboBox_Range.SelectedIndex = 0;
            CT_comboBox_Range.SelectedIndex = 0;
            Z_comboBox_Range.SelectedIndex = 0;
            Y_comboBox_Range.SelectedIndex = 0;
            setAvailability(true);          // 初始设置所有按钮不可以使用
        }


        public InstrDetectPage(int testid,int isformaltest)
        {
            InitializeComponent();
            tabControl.Selecting += new TabControlCancelEventHandler(tabControl_Selecting);
            PT_comboBox_Range.SelectedIndex = 0;
            CT_comboBox_Range.SelectedIndex = 0;
            Z_comboBox_Range.SelectedIndex = 0;
            Y_comboBox_Range.SelectedIndex = 0;
            setAvailability(true); // 初始设置所有按钮不可以使用
            global_testSetID = testid;
            isFormalTest = isformaltest;
            //MessageBox.Show(isFormalTest+"");
            if (isFormalTest == 0)
            {
                // 如果是普通测试，则无法点击导出报表按钮
                Button_ExportDoc.Enabled = false;
                CT_button_save.Enabled = false;
                PT_button_save.Enabled = false;
            }
            else
            {
                // 如果是正式测试，则可以点击导出报表按钮
                Button_ExportDoc.Enabled = true;
                CT_button_save.Enabled = true;
                PT_button_save.Enabled = true;
            }
            radioButton_m_polar_good.Checked = true;
            radioButton_m_profile_good.Checked = true;
        }

        private void InstrDetectPage_Load(object sender, EventArgs e)
        {
            string[] str = SerialPort.GetPortNames();       //获取所有端口号
            Boolean isConnect = false;
            for (int i = 0; i < str.Length; i++)
            {
                serialPort.PortName = str[i];
                //MessageBox.Show("正在处理端口号:" + serialPort.PortName.ToString());
                try { serialPort.Open(); }
                catch
                {// 此端口正在被占用，继续判断下一端口
                    continue;
                }
                isConnect = detectConnect();
                if (!isConnect) serialPort.Close();
                else break;
            }
            if (!isConnect)
            {
                MessageBox.Show("未检测到检测设备或该设备被占用，请插入该设备", "未检测到设备", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        /// <summary>检验是否连接成功
        /// 检验是否连接成功
        /// </summary>
        /// <returns></returns>
        private Boolean detectConnect()
        {
            //先检测端口是否打开，未打开则直接关闭
            if (!serialPort.IsOpen)
            {
                Button_scan.Text = "扫描";
                Button_scan.Enabled = true;
                return false;
            }
            outputBytes[0] = 0xAA; outputBytes[1] = 0x73; outputBytes[2] = 0x00; outputBytes[3] = 0x00; outputBytes[4] = 0x01;
            outputBytes[5] = 0x01; outputBytes[6] = 0xCC; outputBytes[7] = 0x33; outputBytes[8] = 0xC3; outputBytes[9] = 0x3C;
            serialPort.Write(outputBytes, 0, 10);
            testShowBytes();
            int tick = Environment.TickCount;//因为波特率是按秒算的，我们就检测0.5秒好了
            while (Environment.TickCount - tick < 500)
            {
                if (serialPort.BytesToRead == 2){
                    string tempRec = "" + serialPort.ReadByte().ToString("x2") + " " + serialPort.ReadByte().ToString("x2");
                    testlabel_2.Text = "收到的字节为：" + tempRec.ToUpper();
                    //MessageBox.Show("tishi");
                    if (tempRec.ToUpper().CompareTo("A5 5A") == 0)
                    {
                        textBox_port.Text = serialPort.PortName;
                        Button_scan.Text = "连接成功";
                        Button_scan.Enabled = false;
                        setAvailability(true);  //连接成功，将所有按钮设置为可以使用
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>扫描检测设备连接端口
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_scan_Click(object sender, EventArgs e)
        {
            string[] str = SerialPort.GetPortNames();       //获取所有端口号
            Boolean isConnect = false;
            for (int i = 0; i < str.Length; i++)
            {
                serialPort.PortName = str[i];
                //MessageBox.Show("正在处理端口号:" + serialPort.PortName.ToString());
                try { serialPort.Open(); }
                catch
                {// 此端口正在被占用，继续判断下一端口
                    continue;
                }
                isConnect = detectConnect();
                if (!isConnect) serialPort.Close();
                else break;
            }
            if (!isConnect)
            {
                MessageBox.Show("未检测到检测设备或该设备被占用，请插入该设备", "未检测到设备", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>设置各个按钮的可访问性
        /// 设置各个按钮的可访问性
        /// </summary>
        /// <param name="isEnable"></param>
        private void setAvailability(Boolean isEnable)
        {
            if (isEnable == true){
                Button_Over.Enabled = true; Button_Rest.Enabled = true;
                CT_comboBox_Range.Enabled = true; CT_nUD_percentage.Enabled = true; CT_button_comfirm.Enabled = true;
                CT_nUD_ratio.Enabled = true;    CT_trackBar_ratio.Enabled = true;   CT_button_cs_ratio.Enabled = true;
                CT_nUD_angle.Enabled = true;    CT_trackBar_angle.Enabled = true;   CT_button_cs_angle.Enabled = true;
                PT_comboBox_Range.Enabled = true; PT_nUD_percentage.Enabled = true; PT_button_comfirm.Enabled = true;
                PT_nUD_ratio.Enabled = true;    PT_trackBar_ratio.Enabled = true;   PT_button_cs_ratio.Enabled = true;
                PT_nUD_angle.Enabled = true;    PT_trackBar_angle.Enabled = true;   PT_button_cs_angle.Enabled = true;
                Z_comboBox_Range.Enabled = true;Z_nUD_percentage.Enabled = true; Z_button_comfirm.Enabled = true;
                Z_nUD_ratio.Enabled = true;     Z_trackBar_ratio.Enabled = true;    Z_button_cs_ratio.Enabled = true;
                Z_nUD_angle.Enabled = true;     Z_trackBar_angle.Enabled = true;    Z_button_cs_angle.Enabled = true;
                Y_comboBox_Range.Enabled = true;Y_nUD_percentage.Enabled = true; Y_button_comfirm.Enabled = true;
                Y_nUD_ratio.Enabled = true;     Y_trackBar_ratio.Enabled = true;    Y_button_cs_ratio.Enabled = true;
                Y_nUD_angle.Enabled = true;     Y_trackBar_angle.Enabled = true;    Y_button_cs_angle.Enabled = true;
                CT_button_lastStdText.Enabled = false; CT_button_nextStdText.Enabled = true;
                CT_button_save.Enabled = true;
            }
            else{
                Button_Over.Enabled = false; Button_Rest.Enabled = false;
                CT_comboBox_Range.Enabled = false; CT_nUD_percentage.Enabled = false; CT_button_comfirm.Enabled = false;
                CT_nUD_ratio.Enabled = false;   CT_trackBar_ratio.Enabled = false;  CT_button_cs_ratio.Enabled = false;
                CT_nUD_angle.Enabled = false;   CT_trackBar_angle.Enabled = false;  CT_button_cs_angle.Enabled = false;
                PT_comboBox_Range.Enabled = false; PT_nUD_percentage.Enabled = false; PT_button_comfirm.Enabled = false;
                PT_nUD_ratio.Enabled = false;   PT_trackBar_ratio.Enabled = false;  PT_button_cs_ratio.Enabled = false;
                PT_nUD_angle.Enabled = false;    PT_trackBar_angle.Enabled = false;  PT_button_cs_angle.Enabled = false;
                Z_comboBox_Range.Enabled = false; Z_nUD_percentage.Enabled = false; Z_button_comfirm.Enabled = false;
                Z_nUD_ratio.Enabled = false;    Z_trackBar_ratio.Enabled = false;   Z_button_cs_ratio.Enabled = false;
                Z_nUD_angle.Enabled = false;    Z_trackBar_angle.Enabled = false;   Z_button_cs_angle.Enabled = false;
                Y_comboBox_Range.Enabled = false; Y_nUD_percentage.Enabled = false; Y_button_comfirm.Enabled = false;
                Y_nUD_ratio.Enabled = false;    Y_trackBar_ratio.Enabled = false;   Y_button_cs_ratio.Enabled = false;
                Y_nUD_angle.Enabled = false;    Y_trackBar_angle.Enabled = false;   Y_button_cs_angle.Enabled = false;
                CT_button_lastStdText.Enabled = false; CT_button_nextStdText.Enabled = false;
                CT_button_save.Enabled = false;
            }
            if (isFormalTest == 0)
            {
                // 如果是普通测试，则无法点击导出报表按钮
                Button_ExportDoc.Enabled = false;
                CT_button_save.Enabled = false;
                PT_button_save.Enabled = false;
            }
        }
        #region 关于复位和结束的设置
        /// <summary>在选择的时候，如果不能选则禁止操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (enableChanged == false) { e.Cancel = true; return; }
            global_currentPage = tabControl.SelectedIndex + 1;
            if(global_currentPage==1)
            {
                global_stdTestID = 0;
            }
            else if(global_currentPage == 2)
            {
                global_stdTestID = 504;
            }
        }

        /// <summary>点击复位后，可以更换tabpage
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Rest_Click(object sender, EventArgs e)
        {
            clearValue();
            enableChanged = true;
        }

        /// <summary>清空数据
        /// 清空数据
        /// </summary>
        private void clearValue()
        {
            PT_comboBox_Range.SelectedIndex = 0;
            CT_comboBox_Range.SelectedIndex = 0;
            Z_comboBox_Range.SelectedIndex = 0;
            Y_comboBox_Range.SelectedIndex = 0;

            PT_nUD_percentage.Value = 0;
            PT_nUD_angle.Value = 0;
            PT_nUD_ratio.Value = 0;
            PT_textbox_test_percentage.Text = "";
            PT_textbox_test_ratio.Text = "";
            PT_textbox_test_angle.Text = "";

            CT_nUD_percentage.Value = 0;
            CT_nUD_angle.Value = 0;
            CT_nUD_ratio.Value = 0;
            CT_textbox_test_percentage.Text = "";
            CT_textbox_test_ratio.Text = "";
            CT_textbox_test_angle.Text = "";

            Z_nUD_percentage.Value = 0;
            Z_nUD_angle.Value = 0;
            Z_nUD_ratio.Value = 0;

            Y_nUD_percentage.Value = 0;
            Y_nUD_angle.Value = 0;
            Y_nUD_ratio.Value = 0;
        }

        /// <summary>结束按键的设置
        /// 结束按键的设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Over_Click(object sender, EventArgs e)
        {
            PT_nUD_percentage.Value = 0; CT_nUD_percentage.Value = 0;
            Z_nUD_percentage.Value = 0; Y_nUD_percentage.Value = 0;
            SendBytes();
            //关闭本界面，回到主菜单
            this.Hide();
            WelcomePage wp = new WelcomePage();
            wp.ShowDialog();
            this.Close();
            
        }
        #endregion

        #region PT页面比差角差代码

        /// <summary>PT页面输入好百分比后点击确认按钮发送
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PT_button_comfirm_Click(object sender, EventArgs e)
        {
            SendBytes();
        }
        //*****************************************************比差*********************************************
        /// <summary>改变比差的符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PT_button_cs_ratio_Click(object sender, EventArgs e)
        {
            PT_nUD_ratio.Value = PT_nUD_ratio.Value * -1;
        }

        /// <summary>PT的比差的trackbar的value改变，相应的nTD的步长也要发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PT_trackBar_ratio_ValueChanged(object sender, EventArgs e)
        {
            if (PT_trackBar_ratio.Value == PT_trackBar_ratio.Maximum) return;
            double step = 0.00001;
            for (int i = 1; i < PT_trackBar_ratio.Value; i++) { step *= 10; }
            PT_nUD_ratio.Increment = (decimal)step;
        }

        /// <summary>PT的比差的PT_ntd下按左右键切换到trackbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PT_nUD_ratio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                PT_trackBar_ratio.Focus();
                if (e.KeyCode == Keys.Left && PT_trackBar_ratio.Value + 1 <= PT_trackBar_ratio.Maximum)
                    PT_trackBar_ratio.Value = PT_trackBar_ratio.Value + 1;
                else if (e.KeyCode == Keys.Right && PT_trackBar_ratio.Value - 1 >= PT_trackBar_ratio.Minimum)
                    PT_trackBar_ratio.Value = PT_trackBar_ratio.Value - 1;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (PT_trackBar_ratio.Value == PT_trackBar_ratio.Maximum) PT_nUD_ratio.Value = PT_nUD_ratio.Value * (-1);
                else if (e.KeyCode == Keys.Up && PT_nUD_ratio.Value + PT_nUD_ratio.Increment <= PT_nUD_ratio.Maximum)
                    PT_nUD_ratio.Value = PT_nUD_ratio.Value + PT_nUD_ratio.Increment;
                else if (e.KeyCode == Keys.Down && PT_nUD_ratio.Value - PT_nUD_ratio.Increment >= PT_nUD_ratio.Minimum)
                    PT_nUD_ratio.Value = PT_nUD_ratio.Value - PT_nUD_ratio.Increment;
            }
        }

        /// <summary>PT的比差的trackbar检测按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PT_trackBar_ratio_KeyDown(object sender, KeyEventArgs e)
        {
            //按下上下箭头键转到ntD                    
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                PT_nUD_ratio.Focus();
                if (PT_trackBar_ratio.Value == PT_trackBar_ratio.Maximum)
                {
                    PT_nUD_ratio.Value = PT_nUD_ratio.Value * (-1);
                }
                else if (e.KeyCode == Keys.Up && PT_nUD_ratio.Value + PT_nUD_ratio.Increment < PT_nUD_ratio.Maximum)
                { 
                   PT_nUD_ratio.Value = PT_nUD_ratio.Value + PT_nUD_ratio.Increment;
                }
                else if (e.KeyCode == Keys.Down && PT_nUD_ratio.Value - PT_nUD_ratio.Increment >= PT_nUD_ratio.Minimum)
                {
                    PT_nUD_ratio.Value = PT_nUD_ratio.Value - PT_nUD_ratio.Increment;
                }
            }
            //按下左右箭头键分别向左向右移动一个单位
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (e.KeyCode == Keys.Left && PT_trackBar_ratio.Value + 1 <= PT_trackBar_ratio.Maximum)
                {
                    PT_trackBar_ratio.Value = PT_trackBar_ratio.Value + 1;
                }
                else if (e.KeyCode == Keys.Right && PT_trackBar_ratio.Value - 1 >= PT_trackBar_ratio.Minimum)
                {
                    PT_trackBar_ratio.Value = PT_trackBar_ratio.Value - 1;
                }
            }
           
        }

        /// <summary> PT的比差值改变时引发函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PT_nUD_ratio_ValueChanged(object sender, EventArgs e)
        {
            SendBytes();
        }

        //*****************************************************角差*********************************************
        /// <summary>改变角差的符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PT_button_cs_angle_Click(object sender, EventArgs e)
        {
            PT_nUD_angle.Value = PT_nUD_angle.Value * -1;
        }

        /// <summary>角差的trackbar的value改变，相应的nTD的步长也要发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PT_trackBar_angle_ValueChanged(object sender, EventArgs e)
        {
            //显示通知
            if (PT_trackBar_angle.Value == PT_trackBar_angle.Maximum) return;   //  最大值时说明为正负号，不用变化
            double step = 0.0001;
            for (int i = 1; i < PT_trackBar_angle.Value; i++) { step *= 10; }
            PT_nUD_angle.Increment = (decimal)step;
        }

        /// <summary>角差的PT_ntd下按左右键切换到trackbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PT_nUD_angle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                PT_trackBar_angle.Focus();
                if (e.KeyCode == Keys.Left && PT_trackBar_angle.Value + 1 <= PT_trackBar_angle.Maximum)
                    PT_trackBar_angle.Value = PT_trackBar_angle.Value + 1;
                else if (e.KeyCode == Keys.Right && PT_trackBar_angle.Value - 1 >= PT_trackBar_angle.Minimum)
                    PT_trackBar_angle.Value = PT_trackBar_angle.Value - 1;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (PT_trackBar_angle.Value == PT_trackBar_angle.Maximum) PT_nUD_angle.Value = PT_nUD_angle.Value * (-1);
                else if (e.KeyCode == Keys.Up && PT_nUD_angle.Value + PT_nUD_angle.Increment <= PT_nUD_angle.Maximum)
                    PT_nUD_angle.Value = PT_nUD_angle.Value + PT_nUD_angle.Increment;
                else if (e.KeyCode == Keys.Down && PT_nUD_angle.Value - PT_nUD_angle.Increment >= PT_nUD_angle.Minimum)
                    PT_nUD_angle.Value = PT_nUD_angle.Value - PT_nUD_angle.Increment;
            }
        }

        /// <summary>角差的trackbar检测按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PT_trackBar_angle_KeyDown(object sender, KeyEventArgs e)
        {
            //按下上下箭头键转到ntD                    
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                PT_nUD_angle.Focus();
                if (PT_trackBar_angle.Value == PT_trackBar_angle.Maximum)
                {
                    //testlabel_1.Text = "trackbar:" + PT_trackBar_ratio.Value + " nUD:" + PT_nUD_ratio.Value;
                    PT_nUD_angle.Value = PT_nUD_angle.Value * (-1);
                }
                else if (e.KeyCode == Keys.Up && PT_nUD_angle.Value + PT_nUD_angle.Increment < PT_nUD_angle.Maximum)
                {
                    PT_nUD_angle.Value = PT_nUD_angle.Value + PT_nUD_angle.Increment;
                }
                else if (e.KeyCode == Keys.Down && PT_nUD_angle.Value - PT_nUD_angle.Increment >= PT_nUD_angle.Minimum)
                {
                    PT_nUD_angle.Value = PT_nUD_angle.Value - PT_nUD_angle.Increment;
                }
            }
            //按下左右箭头键分别向左向右移动一个单位
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (e.KeyCode == Keys.Left && PT_trackBar_angle.Value + 1 <= PT_trackBar_angle.Maximum)
                {
                    PT_trackBar_angle.Value = PT_trackBar_angle.Value + 1;
                }
                else if (e.KeyCode == Keys.Right && PT_trackBar_angle.Value - 1 >= PT_trackBar_angle.Minimum)
                {
                    PT_trackBar_angle.Value = PT_trackBar_angle.Value - 1;
                }
            }
        }

        /// <summary>PT的角差值改变时引发函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PT_nUD_angle_ValueChanged(object sender, EventArgs e)
        {
            SendBytes();
        }
        /// <summary>
        /// PT的百分比值改变时引发函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PT_nUD_percentage_ValueChanged(object sender, EventArgs e)
        {
            SendBytes();
        }
        #endregion

        #region CT页面比差角差代码

        /// <summary>CT页面输入好百分比后点击确认按钮发送
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CT_button_comfirm_Click(object sender, EventArgs e)
        {
            SendBytes();
        }
        //*****************************************************比差*********************************************
        /// <summary>改变比差的符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CT_button_cs_ratio_Click(object sender, EventArgs e)
        {
            CT_nUD_ratio.Value = CT_nUD_ratio.Value * -1;
        }

        /// <summary>CT的比差的trackbar的value改变，相应的nTD的步长也要发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CT_trackBar_ratio_ValueChanged(object sender, EventArgs e)
        {
            if (CT_trackBar_ratio.Value == CT_trackBar_ratio.Maximum) return;
            double step = 0.00001;
            for (int i = 1; i < CT_trackBar_ratio.Value; i++) { step *= 10; }
            CT_nUD_ratio.Increment = (decimal)step;
        }

        /// <summary>CT的比差的CT_ntd下按左右键切换到trackbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CT_nUD_ratio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                CT_trackBar_ratio.Focus();
                if (e.KeyCode == Keys.Left && CT_trackBar_ratio.Value + 1 <= CT_trackBar_ratio.Maximum)
                    CT_trackBar_ratio.Value = CT_trackBar_ratio.Value + 1;
                else if (e.KeyCode == Keys.Right && CT_trackBar_ratio.Value - 1 >= CT_trackBar_ratio.Minimum)
                    CT_trackBar_ratio.Value = CT_trackBar_ratio.Value - 1;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (CT_trackBar_ratio.Value == CT_trackBar_ratio.Maximum) CT_nUD_ratio.Value = CT_nUD_ratio.Value * (-1);
                else if (e.KeyCode == Keys.Up && CT_nUD_ratio.Value + CT_nUD_ratio.Increment <= CT_nUD_ratio.Maximum)
                    CT_nUD_ratio.Value = CT_nUD_ratio.Value + CT_nUD_ratio.Increment;
                else if (e.KeyCode == Keys.Down && CT_nUD_ratio.Value - CT_nUD_ratio.Increment >= CT_nUD_ratio.Minimum)
                    CT_nUD_ratio.Value = CT_nUD_ratio.Value - CT_nUD_ratio.Increment;
            }
        }

        /// <summary>CT的比差的trackbar检测按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CT_trackBar_ratio_KeyDown(object sender, KeyEventArgs e)
        {
            //按下上下箭头键转到ntD                    
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                CT_nUD_ratio.Focus();
                if (CT_trackBar_ratio.Value == CT_trackBar_ratio.Maximum)
                {
                    CT_nUD_ratio.Value = CT_nUD_ratio.Value * (-1);
                }
                else if (e.KeyCode == Keys.Up && CT_nUD_ratio.Value + CT_nUD_ratio.Increment < CT_nUD_ratio.Maximum)
                {
                    CT_nUD_ratio.Value = CT_nUD_ratio.Value + CT_nUD_ratio.Increment;
                }
                else if (e.KeyCode == Keys.Down && CT_nUD_ratio.Value - CT_nUD_ratio.Increment >= CT_nUD_ratio.Minimum)
                {
                    CT_nUD_ratio.Value = CT_nUD_ratio.Value - CT_nUD_ratio.Increment;
                }
            }
            //按下左右箭头键分别向左向右移动一个单位
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (e.KeyCode == Keys.Left && CT_trackBar_ratio.Value + 1 <= CT_trackBar_ratio.Maximum)
                {
                    CT_trackBar_ratio.Value = CT_trackBar_ratio.Value + 1;
                }
                else if (e.KeyCode == Keys.Right && CT_trackBar_ratio.Value - 1 >= CT_trackBar_ratio.Minimum)
                {
                    CT_trackBar_ratio.Value = CT_trackBar_ratio.Value - 1;
                }
            }

        }

        /// <summary> CT的比差值改变时引发函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CT_nUD_ratio_ValueChanged(object sender, EventArgs e)
        {
            SendBytes();
        }
        /// <summary>
        ///  CT的百分比值改变时引发函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CT_nUD_percentage_ValueChanged(object sender, EventArgs e)
        {
            SendBytes();
        }
        //*****************************************************角差*********************************************
        /// <summary>改变角差的符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CT_button_cs_angle_Click(object sender, EventArgs e)
        {
            CT_nUD_angle.Value = CT_nUD_angle.Value * -1;
        }

        /// <summary>角差的trackbar的value改变，相应的nTD的步长也要发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CT_trackBar_angle_ValueChanged(object sender, EventArgs e)
        {
            //显示通知
            if (CT_trackBar_angle.Value == CT_trackBar_angle.Maximum) return;   //  最大值时说明为正负号，不用变化
            double step = 0.0001;
            for (int i = 1; i < CT_trackBar_angle.Value; i++) { step *= 10; }
            CT_nUD_angle.Increment = (decimal)step;
        }

        /// <summary>角差的CT_ntd下按左右键切换到trackbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CT_nUD_angle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                CT_trackBar_angle.Focus();
                if (e.KeyCode == Keys.Left && CT_trackBar_angle.Value + 1 <= CT_trackBar_angle.Maximum)
                    CT_trackBar_angle.Value = CT_trackBar_angle.Value + 1;
                else if (e.KeyCode == Keys.Right && CT_trackBar_angle.Value - 1 >= CT_trackBar_angle.Minimum)
                    CT_trackBar_angle.Value = CT_trackBar_angle.Value - 1;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (CT_trackBar_angle.Value == CT_trackBar_angle.Maximum) CT_nUD_angle.Value = CT_nUD_angle.Value * (-1);
                else if (e.KeyCode == Keys.Up && CT_nUD_angle.Value + CT_nUD_angle.Increment <= CT_nUD_angle.Maximum)
                    CT_nUD_angle.Value = CT_nUD_angle.Value + CT_nUD_angle.Increment;
                else if (e.KeyCode == Keys.Down && CT_nUD_angle.Value - CT_nUD_angle.Increment >= CT_nUD_angle.Minimum)
                    CT_nUD_angle.Value = CT_nUD_angle.Value - CT_nUD_angle.Increment;
            }
        }

        /// <summary>角差的trackbar检测按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CT_trackBar_angle_KeyDown(object sender, KeyEventArgs e)
        {
            //按下上下箭头键转到ntD                    
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                CT_nUD_angle.Focus();
                if (CT_trackBar_angle.Value == CT_trackBar_angle.Maximum)
                {
                    //testlabel_1.Text = "trackbar:" + CT_trackBar_ratio.Value + " nUD:" + CT_nUD_ratio.Value;
                    CT_nUD_angle.Value = CT_nUD_angle.Value * (-1);
                }
                else if (e.KeyCode == Keys.Up && CT_nUD_angle.Value + CT_nUD_angle.Increment < CT_nUD_angle.Maximum)
                {
                    CT_nUD_angle.Value = CT_nUD_angle.Value + CT_nUD_angle.Increment;
                }
                else if (e.KeyCode == Keys.Down && CT_nUD_angle.Value - CT_nUD_angle.Increment >= CT_nUD_angle.Minimum)
                {
                    CT_nUD_angle.Value = CT_nUD_angle.Value - CT_nUD_angle.Increment;
                }
            }
            //按下左右箭头键分别向左向右移动一个单位
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (e.KeyCode == Keys.Left && CT_trackBar_angle.Value + 1 <= CT_trackBar_angle.Maximum)
                {
                    CT_trackBar_angle.Value = CT_trackBar_angle.Value + 1;
                }
                else if (e.KeyCode == Keys.Right && CT_trackBar_angle.Value - 1 >= CT_trackBar_angle.Minimum)
                {
                    CT_trackBar_angle.Value = CT_trackBar_angle.Value - 1;
                }
            }
        }

        /// <summary>CT的角差值改变时引发函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CT_nUD_angle_ValueChanged(object sender, EventArgs e)
        {
            SendBytes();
        }
        #endregion

        #region Z页面比差角差代码

        /// <summary>Z页面输入好百分比后点击确认按钮发送
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_button_comfirm_Click(object sender, EventArgs e)
        {
            SendBytes();
        }
        //*****************************************************比差*********************************************
        /// <summary>改变比差的符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_button_cs_ratio_Click(object sender, EventArgs e)
        {
            Z_nUD_ratio.Value = Z_nUD_ratio.Value * -1;
        }

        /// <summary>Z的比差的trackbar的value改变，相应的nTD的步长也要发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_trackBar_ratio_ValueChanged(object sender, EventArgs e)
        {
            if (Z_trackBar_ratio.Value == Z_trackBar_ratio.Maximum) return;
            double step = 0.00001;
            for (int i = 1; i < Z_trackBar_ratio.Value; i++) { step *= 10; }
            Z_nUD_ratio.Increment = (decimal)step;
        }

        /// <summary>Z的比差的Z_ntd下按左右键切换到trackbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_nUD_ratio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                Z_trackBar_ratio.Focus();
                if (e.KeyCode == Keys.Left && Z_trackBar_ratio.Value + 1 <= Z_trackBar_ratio.Maximum)
                    Z_trackBar_ratio.Value = Z_trackBar_ratio.Value + 1;
                else if (e.KeyCode == Keys.Right && Z_trackBar_ratio.Value - 1 >= Z_trackBar_ratio.Minimum)
                    Z_trackBar_ratio.Value = Z_trackBar_ratio.Value - 1;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (Z_trackBar_ratio.Value == Z_trackBar_ratio.Maximum) Z_nUD_ratio.Value = Z_nUD_ratio.Value * (-1);
                else if (e.KeyCode == Keys.Up && Z_nUD_ratio.Value + Z_nUD_ratio.Increment <= Z_nUD_ratio.Maximum)
                    Z_nUD_ratio.Value = Z_nUD_ratio.Value + Z_nUD_ratio.Increment;
                else if (e.KeyCode == Keys.Down && Z_nUD_ratio.Value - Z_nUD_ratio.Increment >= Z_nUD_ratio.Minimum)
                    Z_nUD_ratio.Value = Z_nUD_ratio.Value - Z_nUD_ratio.Increment;
            }
        }

        /// <summary>Z的比差的trackbar检测按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_trackBar_ratio_KeyDown(object sender, KeyEventArgs e)
        {
            //按下上下箭头键转到ntD                    
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                Z_nUD_ratio.Focus();
                if (Z_trackBar_ratio.Value == Z_trackBar_ratio.Maximum)
                {
                    Z_nUD_ratio.Value = Z_nUD_ratio.Value * (-1);
                }
                else if (e.KeyCode == Keys.Up && Z_nUD_ratio.Value + Z_nUD_ratio.Increment < Z_nUD_ratio.Maximum)
                {
                    Z_nUD_ratio.Value = Z_nUD_ratio.Value + Z_nUD_ratio.Increment;
                }
                else if (e.KeyCode == Keys.Down && Z_nUD_ratio.Value - Z_nUD_ratio.Increment >= Z_nUD_ratio.Minimum)
                {
                    Z_nUD_ratio.Value = Z_nUD_ratio.Value - Z_nUD_ratio.Increment;
                }
            }
            //按下左右箭头键分别向左向右移动一个单位
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (e.KeyCode == Keys.Left && Z_trackBar_ratio.Value + 1 <= Z_trackBar_ratio.Maximum)
                {
                    Z_trackBar_ratio.Value = Z_trackBar_ratio.Value + 1;
                }
                else if (e.KeyCode == Keys.Right && Z_trackBar_ratio.Value - 1 >= Z_trackBar_ratio.Minimum)
                {
                    Z_trackBar_ratio.Value = Z_trackBar_ratio.Value - 1;
                }
            }

        }

        /// <summary> Z的比差值改变时引发函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_nUD_ratio_ValueChanged(object sender, EventArgs e)
        {
            SendBytes();
        }

        //*****************************************************角差*********************************************
        /// <summary>改变角差的符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_button_cs_angle_Click(object sender, EventArgs e)
        {
            Z_nUD_angle.Value = Z_nUD_angle.Value * -1;
        }

        /// <summary>角差的trackbar的value改变，相应的nTD的步长也要发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_trackBar_angle_ValueChanged(object sender, EventArgs e)
        {
            //显示通知
            if (Z_trackBar_angle.Value == Z_trackBar_angle.Maximum) return;   //  最大值时说明为正负号，不用变化
            double step = 0.0001;
            for (int i = 1; i < Z_trackBar_angle.Value; i++) { step *= 10; }
            Z_nUD_angle.Increment = (decimal)step;
        }

        /// <summary>角差的Z_ntd下按左右键切换到trackbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_nUD_angle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                Z_trackBar_angle.Focus();
                if (e.KeyCode == Keys.Left && Z_trackBar_angle.Value + 1 <= Z_trackBar_angle.Maximum)
                    Z_trackBar_angle.Value = Z_trackBar_angle.Value + 1;
                else if (e.KeyCode == Keys.Right && Z_trackBar_angle.Value - 1 >= Z_trackBar_angle.Minimum)
                    Z_trackBar_angle.Value = Z_trackBar_angle.Value - 1;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (Z_trackBar_angle.Value == Z_trackBar_angle.Maximum) Z_nUD_angle.Value = Z_nUD_angle.Value * (-1);
                else if (e.KeyCode == Keys.Up && Z_nUD_angle.Value + Z_nUD_angle.Increment <= Z_nUD_angle.Maximum)
                    Z_nUD_angle.Value = Z_nUD_angle.Value + Z_nUD_angle.Increment;
                else if (e.KeyCode == Keys.Down && Z_nUD_angle.Value - Z_nUD_angle.Increment >= Z_nUD_angle.Minimum)
                    Z_nUD_angle.Value = Z_nUD_angle.Value - Z_nUD_angle.Increment;
            }
        }

        /// <summary>角差的trackbar检测按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_trackBar_angle_KeyDown(object sender, KeyEventArgs e)
        {
            //按下上下箭头键转到ntD                    
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                Z_nUD_angle.Focus();
                if (Z_trackBar_angle.Value == Z_trackBar_angle.Maximum)
                {
                    //testlabel_1.Text = "trackbar:" + Z_trackBar_ratio.Value + " nUD:" + Z_nUD_ratio.Value;
                    Z_nUD_angle.Value = Z_nUD_angle.Value * (-1);
                }
                else if (e.KeyCode == Keys.Up && Z_nUD_angle.Value + Z_nUD_angle.Increment < Z_nUD_angle.Maximum)
                {
                    Z_nUD_angle.Value = Z_nUD_angle.Value + Z_nUD_angle.Increment;
                }
                else if (e.KeyCode == Keys.Down && Z_nUD_angle.Value - Z_nUD_angle.Increment >= Z_nUD_angle.Minimum)
                {
                    Z_nUD_angle.Value = Z_nUD_angle.Value - Z_nUD_angle.Increment;
                }
            }
            //按下左右箭头键分别向左向右移动一个单位
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (e.KeyCode == Keys.Left && Z_trackBar_angle.Value + 1 <= Z_trackBar_angle.Maximum)
                {
                    Z_trackBar_angle.Value = Z_trackBar_angle.Value + 1;
                }
                else if (e.KeyCode == Keys.Right && Z_trackBar_angle.Value - 1 >= Z_trackBar_angle.Minimum)
                {
                    Z_trackBar_angle.Value = Z_trackBar_angle.Value - 1;
                }
            }
        }

        /// <summary>Z的角差值改变时引发函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Z_nUD_angle_ValueChanged(object sender, EventArgs e)
        {
            SendBytes();
        }
        #endregion

        #region Y页面比差角差代码

        /// <summary>Y页面输入好百分比后点击确认按钮发送
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_button_comfirm_Click(object sender, EventArgs e)
        {
            SendBytes();
        }
        //*****************************************************比差*********************************************
        /// <summary>改变比差的符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_button_cs_ratio_Click(object sender, EventArgs e)
        {
            Y_nUD_ratio.Value = Y_nUD_ratio.Value * -1;
        }

        /// <summary>Y的比差的trackbar的value改变，相应的nTD的步长也要发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_trackBar_ratio_ValueChanged(object sender, EventArgs e)
        {
            if (Y_trackBar_ratio.Value == Y_trackBar_ratio.Maximum) return;
            double step = 0.00001;
            for (int i = 1; i < Y_trackBar_ratio.Value; i++) { step *= 10; }
            Y_nUD_ratio.Increment = (decimal)step;
        }

        /// <summary>Y的比差的Y_ntd下按左右键切换到trackbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_nUD_ratio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                Y_trackBar_ratio.Focus();
                if (e.KeyCode == Keys.Left && Y_trackBar_ratio.Value + 1 <= Y_trackBar_ratio.Maximum)
                    Y_trackBar_ratio.Value = Y_trackBar_ratio.Value + 1;
                else if (e.KeyCode == Keys.Right && Y_trackBar_ratio.Value - 1 >= Y_trackBar_ratio.Minimum)
                    Y_trackBar_ratio.Value = Y_trackBar_ratio.Value - 1;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (Y_trackBar_ratio.Value == Y_trackBar_ratio.Maximum) Y_nUD_ratio.Value = Y_nUD_ratio.Value * (-1);
                else if (e.KeyCode == Keys.Up && Y_nUD_ratio.Value + Y_nUD_ratio.Increment <= Y_nUD_ratio.Maximum)
                    Y_nUD_ratio.Value = Y_nUD_ratio.Value + Y_nUD_ratio.Increment;
                else if (e.KeyCode == Keys.Down && Y_nUD_ratio.Value - Y_nUD_ratio.Increment >= Y_nUD_ratio.Minimum)
                    Y_nUD_ratio.Value = Y_nUD_ratio.Value - Y_nUD_ratio.Increment;
            }
        }

        /// <summary>Y的比差的trackbar检测按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_trackBar_ratio_KeyDown(object sender, KeyEventArgs e)
        {
            //按下上下箭头键转到ntD                    
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                Y_nUD_ratio.Focus();
                if (Y_trackBar_ratio.Value == Y_trackBar_ratio.Maximum)
                {
                    Y_nUD_ratio.Value = Y_nUD_ratio.Value * (-1);
                }
                else if (e.KeyCode == Keys.Up && Y_nUD_ratio.Value + Y_nUD_ratio.Increment < Y_nUD_ratio.Maximum)
                {
                    Y_nUD_ratio.Value = Y_nUD_ratio.Value + Y_nUD_ratio.Increment;
                }
                else if (e.KeyCode == Keys.Down && Y_nUD_ratio.Value - Y_nUD_ratio.Increment >= Y_nUD_ratio.Minimum)
                {
                    Y_nUD_ratio.Value = Y_nUD_ratio.Value - Y_nUD_ratio.Increment;
                }
            }
            //按下左右箭头键分别向左向右移动一个单位
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (e.KeyCode == Keys.Left && Y_trackBar_ratio.Value + 1 <= Y_trackBar_ratio.Maximum)
                {
                    Y_trackBar_ratio.Value = Y_trackBar_ratio.Value + 1;
                }
                else if (e.KeyCode == Keys.Right && Y_trackBar_ratio.Value - 1 >= Y_trackBar_ratio.Minimum)
                {
                    Y_trackBar_ratio.Value = Y_trackBar_ratio.Value - 1;
                }
            }

        }

        /// <summary> Y的比差值改变时引发函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_nUD_ratio_ValueChanged(object sender, EventArgs e)
        {
            SendBytes();
        }

        //*****************************************************角差*********************************************
        /// <summary>改变角差的符号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_button_cs_angle_Click(object sender, EventArgs e)
        {
            Y_nUD_angle.Value = Y_nUD_angle.Value * -1;
        }

        /// <summary>角差的trackbar的value改变，相应的nTD的步长也要发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_trackBar_angle_ValueChanged(object sender, EventArgs e)
        {
            //显示通知
            if (Y_trackBar_angle.Value == Y_trackBar_angle.Maximum) return;   //  最大值时说明为正负号，不用变化
            double step = 0.0001;
            for (int i = 1; i < Y_trackBar_angle.Value; i++) { step *= 10; }
            Y_nUD_angle.Increment = (decimal)step;
        }

        /// <summary>角差的Y_ntd下按左右键切换到trackbar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_nUD_angle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                Y_trackBar_angle.Focus();
                if (e.KeyCode == Keys.Left && Y_trackBar_angle.Value + 1 <= Y_trackBar_angle.Maximum)
                    Y_trackBar_angle.Value = Y_trackBar_angle.Value + 1;
                else if (e.KeyCode == Keys.Right && Y_trackBar_angle.Value - 1 >= Y_trackBar_angle.Minimum)
                    Y_trackBar_angle.Value = Y_trackBar_angle.Value - 1;
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                if (Y_trackBar_angle.Value == Y_trackBar_angle.Maximum) Y_nUD_angle.Value = Y_nUD_angle.Value * (-1);
                else if (e.KeyCode == Keys.Up && Y_nUD_angle.Value + Y_nUD_angle.Increment <= Y_nUD_angle.Maximum)
                    Y_nUD_angle.Value = Y_nUD_angle.Value + Y_nUD_angle.Increment;
                else if (e.KeyCode == Keys.Down && Y_nUD_angle.Value - Y_nUD_angle.Increment >= Y_nUD_angle.Minimum)
                    Y_nUD_angle.Value = Y_nUD_angle.Value - Y_nUD_angle.Increment;
            }
        }

        /// <summary>角差的trackbar检测按键
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_trackBar_angle_KeyDown(object sender, KeyEventArgs e)
        {
            //按下上下箭头键转到ntD                    
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                Y_nUD_angle.Focus();
                if (Y_trackBar_angle.Value == Y_trackBar_angle.Maximum)
                {
                    //testlabel_1.Text = "trackbar:" + Y_trackBar_ratio.Value + " nUD:" + Y_nUD_ratio.Value;
                    Y_nUD_angle.Value = Y_nUD_angle.Value * (-1);
                }
                else if (e.KeyCode == Keys.Up && Y_nUD_angle.Value + Y_nUD_angle.Increment < Y_nUD_angle.Maximum)
                {
                    Y_nUD_angle.Value = Y_nUD_angle.Value + Y_nUD_angle.Increment;
                }
                else if (e.KeyCode == Keys.Down && Y_nUD_angle.Value - Y_nUD_angle.Increment >= Y_nUD_angle.Minimum)
                {
                    Y_nUD_angle.Value = Y_nUD_angle.Value - Y_nUD_angle.Increment;
                }
            }
            //按下左右箭头键分别向左向右移动一个单位
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (e.KeyCode == Keys.Left && Y_trackBar_angle.Value + 1 <= Y_trackBar_angle.Maximum)
                {
                    Y_trackBar_angle.Value = Y_trackBar_angle.Value + 1;
                }
                else if (e.KeyCode == Keys.Right && Y_trackBar_angle.Value - 1 >= Y_trackBar_angle.Minimum)
                {
                    Y_trackBar_angle.Value = Y_trackBar_angle.Value - 1;
                }
            }
        }

        /// <summary>Y的角差值改变时引发函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Y_nUD_angle_ValueChanged(object sender, EventArgs e)
        {
            SendBytes();
        }
        #endregion

        /// <summary>将标准测试值显示在控件上（同时适用于CT，PT）
        /// 
        /// </summary>
        /// <param name="local_stdTestID"></param>
        private void F_showStdTestData(int local_stdTestID)
        {
            //string sql = "select * from D_stdTestData where A_stdTestID=" + local_stdTestID+" and A_stdTestProjectID="+ global_currentPage;
            string sql = "select * from D_stdTestData where A_stdTestID=" + local_stdTestID;
            System.Data.DataTable table = F_select_Access(sql);
            if (table.Rows.Count == 0) return;
            if (global_currentPage == 1)
            {
                CT_comboBox_Range.SelectedIndex = int.Parse(table.Rows[0].ItemArray[2].ToString()) - 1;
                CT_nUD_percentage.Value = decimal.Parse(table.Rows[0].ItemArray[4].ToString());
                CT_nUD_ratio.Value = decimal.Parse(table.Rows[0].ItemArray[5].ToString());
                CT_nUD_angle.Value = decimal.Parse(table.Rows[0].ItemArray[6].ToString());
            }
            else if (global_currentPage == 2)
            {
                PT_comboBox_Range.SelectedIndex = int.Parse(table.Rows[0].ItemArray[2].ToString()) - 1;
                PT_nUD_percentage.Value = decimal.Parse(table.Rows[0].ItemArray[4].ToString());
                PT_nUD_ratio.Value = decimal.Parse(table.Rows[0].ItemArray[5].ToString());
                PT_nUD_angle.Value = decimal.Parse(table.Rows[0].ItemArray[6].ToString());
            }
            //以下是控制上一条、下一条显示模块
            CT_button_nextStdText.Enabled = true; CT_button_lastStdText.Enabled = true;
            PT_button_nextStdText.Enabled = true; PT_button_lastStdText.Enabled = true;
            int temp_low=local_stdTestID-1;int temp_high=local_stdTestID+1;
            testlabel_3.Text = "当前ID：" + local_stdTestID;
            if (global_currentPage == 1)
            {
                if (temp_low <= 0) { CT_button_lastStdText.Enabled = false; return; }
                if (temp_high > 504) { CT_button_nextStdText.Enabled = false; return; }
            }
            else if (global_currentPage == 2)
            {
                if (temp_low <= 504) { PT_button_lastStdText.Enabled = false; return; }
                if (temp_high > 1512) { PT_button_nextStdText.Enabled = false; return; }
            }
        }

        /// <summary>CT页面下下一个标准数据
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CT_button_nextStdText_Click(object sender, EventArgs e)
        {
            F_showStdTestData(++global_stdTestID);
        }
        /// <summary> CT页面下上一个标准数据

        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CT_button_lastStdText_Click(object sender, EventArgs e)
        {
            F_showStdTestData(--global_stdTestID);
        }
        /// <summary> CT页面下点击保存记录按钮
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CT_button_save_Click(object sender, EventArgs e)
        {
            #region 检查CT页面下填入数值是否符合规范
            if (!Validator.checkRequired(this.CT_textbox_test_percentage.Text))
            {
                MessageBox.Show("被测百分表不能为空\n", "错误提示！"); return;
            }
            if (!Validator.checkRequired(this.CT_textbox_test_ratio.Text))
            {
                MessageBox.Show("被测比差不能为空\n", "错误提示！"); return;
            }
            if (!Validator.checkRequired(this.CT_textbox_test_angle.Text))
            {
                MessageBox.Show("被测角差不能为空\n", "错误提示！"); return;
            }
            if (!Validator.checkDouble(this.CT_textbox_test_percentage.Text) && !Validator.checkInteger(this.CT_textbox_test_percentage.Text))
            {
                MessageBox.Show("被测百分表应为数值类型\n", "错误提示！"); return;
            }
            if (!Validator.checkDouble(this.CT_textbox_test_ratio.Text) && !Validator.checkInteger(this.CT_textbox_test_ratio.Text))
            {
                MessageBox.Show("被测比差应为数值类型\n", "错误提示！"); return;
            }
            if (!Validator.checkDouble(this.CT_textbox_test_angle.Text) && !Validator.checkInteger(this.CT_textbox_test_angle.Text))
            {
                MessageBox.Show("被测角差应为数值类型\n", "错误提示！"); return;
            }
            #endregion
            #region 检查CT页面下填入数据极性是否相同
            if (F_isTwoValueSameSymbol((float)CT_nUD_percentage.Value, float.Parse(CT_textbox_test_percentage.Text.ToString())) == true && F_isTwoValueSameSymbol((float)CT_nUD_ratio.Value, float.Parse(CT_textbox_test_ratio.Text.ToString())) == true && F_isTwoValueSameSymbol((float)CT_nUD_angle.Value, float.Parse(CT_textbox_test_angle.Text.ToString())) == true)
            {
                // 极性相同设置checked
                radioButton_m_polar_good.Checked = true;
            }
            else 
            {
                // 极性不同提醒
                DialogResult result = MessageBox.Show("测试数据极性不一致，请核实！确定要保留吗？", "极性不一致", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //1.DialogResult属性用来获取或设置MessageBox.Show返回的一个值，该值再单击揿钮时返回到父窗体
                //2.返回是，否
                if (result == DialogResult.No)
                {
                    // 用户取消保存
                    return;
                }
                else
                {
                    radioButton_m_polar_bad.Checked = true;
                }
            }
            # endregion
            string sql = "insert into D_testData(A_testSetID,A_testProjectID,A_testRangeID,A_stdPercentage,A_testPercentage,A_stdRatio,A_testRatio,A_stdAngle,A_testAngle) values("
                + global_testSetID + "," + global_currentPage + "," + (CT_comboBox_Range.SelectedIndex + 1) + "," + CT_nUD_percentage.Value + "," + float.Parse(CT_textbox_test_percentage.Text.ToString()) + ","
                + CT_nUD_ratio.Value + "," + float.Parse(CT_textbox_test_ratio.Text.ToString()) + "," + CT_nUD_angle.Value + "," + float.Parse(CT_textbox_test_angle.Text.ToString()) + ")";
                F_control_Access(sql);
                MessageBox.Show("本记录保存成功", "保存成功");
        }


        /// <summary>PT页面下下一个标准数据
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PT_button_nextStdText_Click(object sender, EventArgs e)
        {
            F_showStdTestData(++global_stdTestID);
        }
        /// <summary>PT页面下上一个标准数据
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PT_button_lastStdText_Click(object sender, EventArgs e)
        {
            F_showStdTestData(--global_stdTestID);
        }
        /// <summary>PT页面下点击保存记录按钮
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PT_button_save_Click(object sender, EventArgs e)
        {
            #region 检查PT页面下填入数值是否符合规范
            if (!Validator.checkRequired(this.PT_textbox_test_percentage.Text))
            {
                MessageBox.Show("被测百分表不能为空\n", "错误提示！"); return;
            }
            if (!Validator.checkRequired(this.PT_textbox_test_ratio.Text))
            {
                MessageBox.Show("被测比差不能为空\n", "错误提示！"); return;
            }
            if (!Validator.checkRequired(this.PT_textbox_test_angle.Text))
            {
                MessageBox.Show("被测角差不能为空\n", "错误提示！"); return;
            }
            if (!Validator.checkDouble(this.PT_textbox_test_percentage.Text) && !Validator.checkInteger(this.PT_textbox_test_percentage.Text))
            {
                MessageBox.Show("被测百分表应为数值类型\n", "错误提示！"); return;
            }
            if (!Validator.checkDouble(this.PT_textbox_test_ratio.Text) && !Validator.checkInteger(this.PT_textbox_test_ratio.Text))
            {
                MessageBox.Show("被测比差应为数值类型\n", "错误提示！"); return;
            }
            if (!Validator.checkDouble(this.PT_textbox_test_angle.Text) && !Validator.checkInteger(this.PT_textbox_test_angle.Text))
            {
                MessageBox.Show("被测角差应为数值类型\n", "错误提示！"); return;
            }
            #endregion
            #region 检查PT页面下填入数据极性是否相同
            if (F_isTwoValueSameSymbol((float)PT_nUD_percentage.Value, float.Parse(PT_textbox_test_percentage.Text.ToString())) == true && F_isTwoValueSameSymbol((float)PT_nUD_ratio.Value, float.Parse(PT_textbox_test_ratio.Text.ToString())) == true && F_isTwoValueSameSymbol((float)PT_nUD_angle.Value, float.Parse(PT_textbox_test_angle.Text.ToString())) == true)
            {
                // 极性相同设置checked
                radioButton_m_polar_good.Checked = true;
            }
            else
            {
                // 极性不同提醒
                DialogResult result = MessageBox.Show("测试数据极性不一致，请核实！确定要保留吗？", "极性不一致", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //1.DialogResult属性用来获取或设置MessageBox.Show返回的一个值，该值再单击揿钮时返回到父窗体
                //2.返回是，否
                if (result == DialogResult.No)
                {
                    // 用户取消保存
                    return;
                }
                else
                {
                    radioButton_m_polar_bad.Checked = true;
                }
            }
            # endregion
            string sql = "insert into D_testData(A_testSetID,A_testProject_ID,A_testRangeID,A_stdPercentage,A_testPercentage,A_stdRatio,A_testRatio,A_stdAngle,A_testAngle) values("
                + global_testSetID + "," + global_currentPage + "," + (PT_comboBox_Range.SelectedIndex + 1) + "," + PT_nUD_percentage.Value + "," + float.Parse(PT_textbox_test_percentage.Text.ToString()) + ","
                + PT_nUD_ratio.Value + "," + float.Parse(PT_textbox_test_ratio.Text.ToString()) + "," + PT_nUD_angle.Value + "," + float.Parse(PT_textbox_test_angle.Text.ToString()) + ")";
            Console.WriteLine("global_currentPage=" + global_currentPage);
            Console.WriteLine("sql"+sql);
            F_control_Access(sql);

        }

        /// <summary>详细信息按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_detail_Click(object sender, EventArgs e)
        {
            if (isFormalTest == 0)
            {
                ProductDetailPage PDP = new ProductDetailPage(0);
                PDP.Show();
            }
            else
            {
                ProductDetailPage PDP = new ProductDetailPage(global_testSetID);
                PDP.Show();
            }
        }

        /// <summary>判断两数是否符号相同 
        /// </summary>
        /// <param name="x">第一个数</param>
        /// <param name="y">第二个数</param>
        /// <returns>相同则返回true,不同返回false</returns>
        private Boolean F_isTwoValueSameSymbol(float x, float y)
        {
            if(x == 0 && y == 0) return true;
            if ((x > 0 && y > 0) || (x < 0 && y < 0)) return true;
            else { return false; }
        }

        /// <summary>发送字节
        /// 
        /// </summary>
        private void SendBytes()
        {
            //清空Bytes值
            for (int i = 0; i < 10; i++) outputBytes[i] = 0x00;
            if (tabControl.SelectedIndex == 0)
            {
                #region CT页面下数据内容
                //设置第一个字节
                if (CT_comboBox_Range.SelectedIndex == 0) outputBytes[0] = 0x10;
                else if (CT_comboBox_Range.SelectedIndex == 1) outputBytes[0] = 0x11;
                //设置第2、3、4个字节
                string tempStrP = ((int)(CT_nUD_percentage.Value * 1000)).ToString("x6");
                outputBytes[1] = Convert.ToByte(tempStrP.Substring(0, 2), 16);
                outputBytes[2] = Convert.ToByte(tempStrP.Substring(2, 2), 16);
                outputBytes[3] = Convert.ToByte(tempStrP.Substring(4, 2), 16);
                //设置第5、6、7个字节
                string tempStrR = Math.Abs((int)(CT_nUD_ratio.Value * 100000)).ToString("x6");
                outputBytes[4] = Convert.ToByte(tempStrR.Substring(0, 2), 16);
                if (CT_nUD_ratio.Value < 0) outputBytes[4] |= 0x80;
                outputBytes[5] = Convert.ToByte(tempStrR.Substring(2, 2), 16);
                outputBytes[6] = Convert.ToByte(tempStrR.Substring(4, 2), 16);
                //设置第8、9、10个字节
                string tempStrA = Math.Abs((int)(CT_nUD_angle.Value * 10000)).ToString("x6");
                outputBytes[7] = Convert.ToByte(tempStrA.Substring(0, 2), 16);
                if (CT_nUD_angle.Value < 0) outputBytes[7] |= 0x80;
                outputBytes[8] = Convert.ToByte(tempStrA.Substring(2, 2), 16);
                outputBytes[9] = Convert.ToByte(tempStrA.Substring(4, 2), 16);

                #endregion
            }
            else if (tabControl.SelectedIndex == 1)
            {
                #region PT页面下数据内容
                //设置第一个字节
                if (PT_comboBox_Range.SelectedIndex == 0) outputBytes[0] = 0x20;
                else if (PT_comboBox_Range.SelectedIndex == 1) outputBytes[0] = 0x21;
                else if (PT_comboBox_Range.SelectedIndex == 2) outputBytes[0] = 0x22;
                else if (PT_comboBox_Range.SelectedIndex == 3) outputBytes[0] = 0x23;
                //设置第2、3、4个字节
                string tempStrP = ((int)(PT_nUD_percentage.Value * 1000)).ToString("x6");
                outputBytes[1] = Convert.ToByte(tempStrP.Substring(0, 2), 16);
                outputBytes[2] = Convert.ToByte(tempStrP.Substring(2, 2), 16);
                outputBytes[3] = Convert.ToByte(tempStrP.Substring(4, 2), 16);
                //设置第5、6、7个字节
                string tempStrR = Math.Abs((int)(PT_nUD_ratio.Value * 100000)).ToString("x6");
                outputBytes[4] = Convert.ToByte(tempStrR.Substring(0, 2), 16);
                if (PT_nUD_ratio.Value < 0) outputBytes[4] |= 0x80;
                outputBytes[5] = Convert.ToByte(tempStrR.Substring(2, 2), 16);
                outputBytes[6] = Convert.ToByte(tempStrR.Substring(4, 2), 16);
                //设置第8、9、10个字节
                string tempStrA = Math.Abs((int)(PT_nUD_angle.Value * 10000)).ToString("x6");
                outputBytes[7] = Convert.ToByte(tempStrA.Substring(0, 2), 16);
                if (PT_nUD_angle.Value < 0) outputBytes[7] |= 0x80;
                outputBytes[8] = Convert.ToByte(tempStrA.Substring(2, 2), 16);
                outputBytes[9] = Convert.ToByte(tempStrA.Substring(4, 2), 16);

                #endregion
            }
            else if (tabControl.SelectedIndex == 2)
            {
                #region Z页面下数据内容
                //设置第一个字节
                if (Z_comboBox_Range.SelectedIndex == 0) outputBytes[0] = 0x30;
                else if (Z_comboBox_Range.SelectedIndex == 1) outputBytes[0] = 0x31;
                //设置第2、3、4个字节
                string tempStrP = ((int)(Z_nUD_percentage.Value * 1000)).ToString("x6");
                outputBytes[1] = Convert.ToByte(tempStrP.Substring(0, 2), 16);
                outputBytes[2] = Convert.ToByte(tempStrP.Substring(2, 2), 16);
                outputBytes[3] = Convert.ToByte(tempStrP.Substring(4, 2), 16);
                //设置第5、6、7个字节
                string tempStrR = Math.Abs((int)(Z_nUD_ratio.Value * 100000)).ToString("x6");
                outputBytes[4] = Convert.ToByte(tempStrR.Substring(0, 2), 16);
                if (Z_nUD_ratio.Value < 0) outputBytes[4] |= 0x80;
                outputBytes[5] = Convert.ToByte(tempStrR.Substring(2, 2), 16);
                outputBytes[6] = Convert.ToByte(tempStrR.Substring(4, 2), 16);
                //设置第8、9、10个字节
                string tempStrA = Math.Abs((int)(Z_nUD_angle.Value * 10000)).ToString("x6");
                outputBytes[7] = Convert.ToByte(tempStrA.Substring(0, 2), 16);
                if (Z_nUD_angle.Value < 0) outputBytes[7] |= 0x80;
                outputBytes[8] = Convert.ToByte(tempStrA.Substring(2, 2), 16);
                outputBytes[9] = Convert.ToByte(tempStrA.Substring(4, 2), 16);

                #endregion
            }
            else if (tabControl.SelectedIndex == 3)
            {
                #region Y页面下数据内容
                //设置第一个字节
                if (Y_comboBox_Range.SelectedIndex == 0) outputBytes[0] = 0x40;
                else if (Y_comboBox_Range.SelectedIndex == 1) outputBytes[0] = 0x41;
                else if (Y_comboBox_Range.SelectedIndex == 2) outputBytes[0] = 0x42;
                else if (Y_comboBox_Range.SelectedIndex == 3) outputBytes[0] = 0x43;
                //设置第2、3、4个字节
                string tempStrP = ((int)(Y_nUD_percentage.Value * 1000)).ToString("x6");
                outputBytes[1] = Convert.ToByte(tempStrP.Substring(0, 2), 16);
                outputBytes[2] = Convert.ToByte(tempStrP.Substring(2, 2), 16);
                outputBytes[3] = Convert.ToByte(tempStrP.Substring(4, 2), 16);
                //设置第5、6、7个字节
                string tempStrR = Math.Abs((int)(Y_nUD_ratio.Value * 100000)).ToString("x6");
                outputBytes[4] = Convert.ToByte(tempStrR.Substring(0, 2), 16);
                if (Y_nUD_ratio.Value < 0) outputBytes[4] |= 0x80;
                outputBytes[5] = Convert.ToByte(tempStrR.Substring(2, 2), 16);
                outputBytes[6] = Convert.ToByte(tempStrR.Substring(4, 2), 16);
                //设置第8、9、10个字节
                string tempStrA = Math.Abs((int)(Y_nUD_angle.Value * 10000)).ToString("x6");
                outputBytes[7] = Convert.ToByte(tempStrA.Substring(0, 2), 16);
                if (Y_nUD_angle.Value < 0) outputBytes[7] |= 0x80;
                outputBytes[8] = Convert.ToByte(tempStrA.Substring(2, 2), 16);
                outputBytes[9] = Convert.ToByte(tempStrA.Substring(4, 2), 16);

                #endregion
            }
            else if (tabControl.SelectedIndex == 4)
            {
            }
            testShowBytes();
            enableChanged = false;      //只要发送字节就不允许再变tabpage
            try
            {
                serialPort.Write(outputBytes, 0, 10);   //发送字节
            }
            catch
            {
                //MessageBox.Show("端口连接错误，请检查连接", "端口连接错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //测试发送字节,MessageBox显示
        private void testShowBytes()
        {
            string tempStr = "";
            for (int i = 0; i < 10; i++)
            {
                tempStr += outputBytes[i].ToString("x2") + ",";
            }
            testlabel_1.Text = "发送的字节为：" + tempStr;
            testlabel_2.Text = "收到的字节为：";
        }

        #region 数据库控制语句
        /// <summary>
        /// 控制数据表
        /// </summary>
        /// <param name="sql">输入的插入、删除语句</param>
        /// <returns></returns>
        private bool F_control_Access(string sql)
        {
            try
            {
                OleDbConnection conn = DBProvider.getConn(); //getConn():得到连接对象
                conn.Open();
                //string sql = "Insert Into users(username,[password]) VALUES ('" + a + "','" + b + "')";//password是access数据库的特殊关键字，需要加[]
                OleDbCommand insertcmd = new OleDbCommand(sql, conn);
                insertcmd.ExecuteNonQuery();
                conn.Close();
                return true;            //返回操作正常
            }
            catch (Exception exc)
            {
                return false;           //返回操作异常
                throw (new Exception("数据库出错:" + exc.Message));
                Console.WriteLine("数据库出错:" + exc.Message);
            }
        }

        /// <summary>
        /// 查询数据表-返回查询结果至DataTable
        /// </summary>
        /// <param name="sql">数据库查询语句</param>
        /// <returns></returns>
        private System.Data.DataTable F_select_Access(string sql)
        {
            OleDbDataAdapter adapter;
            System.Data.DataTable table = new System.Data.DataTable();
            string str = @"Provider = Microsoft.Jet.OLEDB.4.0;Data Source =tyshkj.mdb";
            OleDbConnection conn = new OleDbConnection();
            try
            {
                adapter = new OleDbDataAdapter(sql, str);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                adapter.InsertCommand = builder.GetInsertCommand();
                conn.Close();
                adapter.Fill(table);
                return table;
            }
            catch (Exception exc)
            {
                throw (new Exception("数据库出错:" + exc.Message));
                Console.WriteLine(exc.ToString());
            }
        }
        #endregion

        private void Button_ExportDoc_Click(object sender, EventArgs e)
        {
            #region 将外观、极性状态更新
            string tempProfile = "";
            if (radioButton_m_profile_good.Checked == true)
            {
                tempProfile = "合格";
            }
            else
            {
                tempProfile = "不合格";
            }
            string tempPolar = "";
            if (radioButton_m_polar_good.Checked == true)
            {
                tempPolar = "一致";
            }
            else
            {
                tempPolar = "不一致";
            }

            string sql = "update E_testSet SET A_productProfile='" + tempProfile + "',A_testProductPolar='" + tempPolar + "' where A_testSetID=" + global_testSetID;
            F_control_Access(sql);
            #endregion
            
            #region 调用导出报表函数 @AniChikage
            progressForm.Show();
            exportTable();
            #endregion
        }

        #region 导出报表函数 @AniChikage
        private void exportTable()
        {
            killWord();
            //初始化参数
            int[,] cate = new int[6, 20];
            string prePer = "-1";
            int n = 1;
            double ct5a = 0;
            double ct1a = 0;
            double ct150v = 0;
            double ct100v = 0;
            double ct100rsqrt3v = 0;
            double ct100r3 = 0;

            //log
            global_testSetID = 41;
            Console.WriteLine(global_testSetID);

            //获取testSetID=argv的全部行
            string getRowsCount = "select A_testProjectID,A_testRangeID,A_stdPercentage, " +
                "A_stdRatio,A_testRatio,A_stdAngle,A_testAngle from D_testData where A_testSetID=" + global_testSetID;
            System.Data.DataTable tableGetRowsCount = F_select_Access(getRowsCount);
            Console.WriteLine(tableGetRowsCount.Rows.Count);

            //判断是否是初次检测
            string chek = "select A_isFirstTest from E_testSet where A_testSetID=" + global_testSetID;
            System.Data.DataTable checktable = F_select_Access(chek);
            Console.WriteLine(checktable.Rows[0].ItemArray[0]);

            
            Report report = new Report();
            //初次检测
            if (Convert.ToBoolean(checktable.Rows[0].ItemArray[0]))
                report.CreateNewDocument(System.Windows.Forms.Application.StartupPath+"\\template\\bbb2.dot");
            else
                report.CreateNewDocument(System.Windows.Forms.Application.StartupPath + "\\template\\bbb3.dotx");

            Table table = report.InsertTable("newtable", tableGetRowsCount.Rows.Count + 2, 10, 70);
            
            //defpro
            progressForm.SetProgressValue(5);

            report.PreProcess(table);
            report.UseBorder(1, true);
            report.SetFont_Table(table, "宋体", 9);
            report.Table_Merge_Row(table, 9, 10);
            report.Table_Merge_Row(table, 7, 8);
            report.Table_Merge_Row(table, 5, 6);
            report.Table_Merge_Row(table, 3, 4);
            report.Table_Merge_2(table, 1, 2);
            report.Table_Merge(table, 1, 2);
            report.Table_Merge_Row(table, 1, 2);

            string sql = "select A_testSetTime,A_testProductSNID,A_testProductPolar,A_productProfile from E_testSet where A_testSetID=" + global_testSetID;
            System.Data.DataTable table0 = F_select_Access(sql);
            report.insertBookmark("ishege", table0.Rows[0].ItemArray[3].ToString());
            string[] mydate1 = System.Text.RegularExpressions.Regex.Split(table0.Rows[0].ItemArray[0].ToString(), " ");
            string[] mydate = System.Text.RegularExpressions.Regex.Split(mydate1[0], "/");
            Console.WriteLine(mydate[2]);
            report.insertBookmark("kkyear",mydate[0]);
            report.insertBookmark("kkmonth", mydate[1]);
            report.insertBookmark("kkday", mydate[2]);
            report.insertBookmark("out_profile", table0.Rows[0].ItemArray[3].ToString());
            report.insertBookmark("out_profile_", table0.Rows[0].ItemArray[3].ToString());
            report.insertBookmark("out_polar", table0.Rows[0].ItemArray[2].ToString());
            report.insertBookmark("out_polar_", table0.Rows[0].ItemArray[2].ToString());
            SNID = table0.Rows[0].ItemArray[1].ToString();
            report.insertBookmark("all_polar", table0.Rows[0].ItemArray[2].ToString());

            //defpro
            progressForm.SetProgressValue(10);

            Console.WriteLine(table0.Rows[0].ItemArray[1].ToString());
            try
            {
                sql = "select A_productType,A_productSNID,A_productClientID,A_productManuID,A_productDutID,A_productClassID from E_product where A_productSNID='" + table0.Rows[0].ItemArray[1].ToString()+"'";
                System.Data.DataTable table00 = F_select_Access(sql);
                report.insertBookmark("type",table00.Rows[0].ItemArray[0].ToString());
                report.insertBookmark("sn", table00.Rows[0].ItemArray[1].ToString());
            

                sql = "select A_clientName,A_clientAddress,A_clientPhone from E_client where A_clientID=" + table00.Rows[0].ItemArray[2].ToString();
                System.Data.DataTable table1 = F_select_Access(sql);
                report.insertBookmark("client",table1.Rows[0].ItemArray[0].ToString());
                report.insertBookmark("address", table1.Rows[0].ItemArray[1].ToString());

                sql = "select A_dutName from E_dut where A_dutID=" + table00.Rows[0].ItemArray[4].ToString();
                System.Data.DataTable table2 = F_select_Access(sql);
                report.insertBookmark("dut",table2.Rows[0].ItemArray[0].ToString());

                deviceName = table2.Rows[0].ItemArray[0].ToString();

                sql = "select A_manuName,A_manuPhone from E_manu where A_manuID=" + table00.Rows[0].ItemArray[3].ToString();
                System.Data.DataTable table3 = F_select_Access(sql);
                report.insertBookmark("manu",table3.Rows[0].ItemArray[0].ToString());

                sql = "select A_className from E_class where A_classID=" + table00.Rows[0].ItemArray[5].ToString();
                System.Data.DataTable table4 = F_select_Access(sql);
                report.insertBookmark("class", table4.Rows[0].ItemArray[0].ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //defpro
            progressForm.SetProgressValue(20);
            
            ///
            ///<1-1>
            ///
            prePer = "-1";
            n = 1;
            string ct11 = "select A_stdPercentage, " +
                "A_stdRatio,A_testRatio,A_stdAngle,A_testAngle,A_testPercentage from D_testData where A_testSetID=" + global_testSetID + " and A_testProjectID=1 and A_testRangeID=1 order by A_stdPercentage";
            System.Data.DataTable tablect11 = F_select_Access(ct11);
            Console.WriteLine(tablect11.Rows.Count);
            for (int i = 0; i < tablect11.Rows.Count; i++)
            {
                if (Convert.ToDouble(tablect11.Rows[i].ItemArray[0].ToString()) != 0)
                {
                    double temp_percent = (Convert.ToDouble(tablect11.Rows[i].ItemArray[5].ToString()) - Convert.ToDouble(tablect11.Rows[i].ItemArray[0].ToString())) / Convert.ToDouble(tablect11.Rows[i].ItemArray[0].ToString());
                    if (System.Math.Abs(temp_percent) > ct5a)
                    {
                        ct5a = temp_percent;
                    }
                }
                if (Convert.ToDouble(tablect11.Rows[i].ItemArray[1].ToString()) != 0)
                {
                    report.InsertCell(table, i + 3, 3, Convert.ToDouble(tablect11.Rows[i].ItemArray[1].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3, 5, Convert.ToDouble(tablect11.Rows[i].ItemArray[2].ToString()).ToString("0.0000"));
                    double error_ratio = (Convert.ToDouble(tablect11.Rows[i].ItemArray[2].ToString()) - Convert.ToDouble(tablect11.Rows[i].ItemArray[1].ToString())) / Convert.ToDouble(tablect11.Rows[i].ItemArray[1].ToString());
                    report.InsertCell(table, i + 3, 7, error_ratio.ToString("0.00"));
                }
                else
                {
                    report.InsertCell(table, i + 3, 3, Convert.ToDouble(tablect11.Rows[i].ItemArray[1].ToString()).ToString());
                    report.InsertCell(table, i + 3, 5, Convert.ToDouble(tablect11.Rows[i].ItemArray[2].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3, 7, "0");
                }
                if (Convert.ToDouble(tablect11.Rows[i].ItemArray[3].ToString()) != 0)
                {
                    report.InsertCell(table, i + 3, 4, Convert.ToDouble(tablect11.Rows[i].ItemArray[3].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3, 6, Convert.ToDouble(tablect11.Rows[i].ItemArray[4].ToString()).ToString("0.0000"));
                    double error_angle = (Convert.ToDouble(tablect11.Rows[i].ItemArray[4].ToString()) - Convert.ToDouble(tablect11.Rows[i].ItemArray[3].ToString())) / Convert.ToDouble(tablect11.Rows[i].ItemArray[3].ToString());
                    report.InsertCell(table, i + 3, 8, error_angle.ToString("0.00"));
                }
                else
                {
                    report.InsertCell(table, i + 3, 4, Convert.ToDouble(tablect11.Rows[i].ItemArray[3].ToString()).ToString());
                    report.InsertCell(table, i + 3, 6, Convert.ToDouble(tablect11.Rows[i].ItemArray[4].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3, 8, "0");
                }
                report.InsertCell(table, i + 3, 2, tablect11.Rows[i].ItemArray[0].ToString() + "%");

                //////////////////
                if (tablect11.Rows[i].ItemArray[0].ToString() != prePer)
                {
                    cate[0, n] = i;
                    prePer = tablect11.Rows[i].ItemArray[0].ToString();
                    n = n + 1;
                    Console.WriteLine(n);
                }
                //插入量程
                report.InsertCell(table, i + 3, 1, "CT(5A)");
            }
            cate[0, 0] = n + 1;
            cate[0, n] = tablect11.Rows.Count;
            int up = tablect11.Rows.Count;

            //defpro
            progressForm.SetProgressValue(30);
            ///
            ///<1-2>
            ///
            prePer = "-1";
            n = 1;
            string ct12 = "select A_stdPercentage, " +
                "A_stdRatio,A_testRatio,A_stdAngle,A_testAngle,A_testPercentage from D_testData where A_testSetID="+global_testSetID+" and A_testProjectID=1 and A_testRangeID=2 order by A_stdPercentage";
            System.Data.DataTable tablect12 = F_select_Access(ct12);
            Console.WriteLine(tablect12.Rows.Count);
            for (int i = 0; i < tablect12.Rows.Count; i++)
            {

                if (Convert.ToDouble(tablect12.Rows[i].ItemArray[0].ToString()) != 0)
                { 
                    double temp_percent = (Convert.ToDouble(tablect12.Rows[i].ItemArray[5].ToString()) - Convert.ToDouble(tablect12.Rows[i].ItemArray[0].ToString())) / Convert.ToDouble(tablect12.Rows[i].ItemArray[0].ToString());
                    if (System.Math.Abs(temp_percent) > ct1a)
                    {
                        ct1a = temp_percent;
                    }
                }
                if (Convert.ToDouble(tablect12.Rows[i].ItemArray[1].ToString()) != 0)
                {
                    report.InsertCell(table, i + 3 + up, 3, Convert.ToDouble(tablect12.Rows[i].ItemArray[1].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 5, Convert.ToDouble(tablect12.Rows[i].ItemArray[2].ToString()).ToString("0.0000"));
                    double error_ratio = (Convert.ToDouble(tablect12.Rows[i].ItemArray[2].ToString()) - Convert.ToDouble(tablect12.Rows[i].ItemArray[1].ToString())) / Convert.ToDouble(tablect12.Rows[i].ItemArray[1].ToString());
                    report.InsertCell(table, i + 3 + up, 7, error_ratio.ToString("0.00"));  
                }
                else
                {
                    report.InsertCell(table, i + 3 + up, 3, Convert.ToDouble(tablect12.Rows[i].ItemArray[1].ToString()).ToString());
                    report.InsertCell(table, i + 3 + up, 5, Convert.ToDouble(tablect12.Rows[i].ItemArray[2].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 7, "0");
                }
                if (Convert.ToDouble(tablect12.Rows[i].ItemArray[3].ToString()) != 0)
                {
                    report.InsertCell(table, i + 3 + up, 4, Convert.ToDouble(tablect12.Rows[i].ItemArray[3].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 6, Convert.ToDouble(tablect12.Rows[i].ItemArray[4].ToString()).ToString("0.0000"));
                    double error_angle = (Convert.ToDouble(tablect12.Rows[i].ItemArray[4].ToString()) - Convert.ToDouble(tablect12.Rows[i].ItemArray[3].ToString())) / Convert.ToDouble(tablect12.Rows[i].ItemArray[3].ToString());
                    report.InsertCell(table, i + 3 + up, 8, error_angle.ToString("0.00"));
                }
                else
                {
                    report.InsertCell(table, i + 3 + up, 4, Convert.ToDouble(tablect12.Rows[i].ItemArray[3].ToString()).ToString());
                    report.InsertCell(table, i + 3 + up, 6, Convert.ToDouble(tablect12.Rows[i].ItemArray[4].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 8, "0");
                }
                report.InsertCell(table, i + 3 + up, 2, tablect12.Rows[i].ItemArray[0].ToString() + "%");

                //////////////////
                if (tablect12.Rows[i].ItemArray[0].ToString() != prePer)
                {
                    cate[1, n] = i + up;
                    prePer = tablect12.Rows[i].ItemArray[0].ToString();
                    n = n + 1;
                    Console.WriteLine(n);
                }
                report.InsertCell(table, i + 3 + up, 1, "CT(1A)");
            }
            cate[1, 0] = n + 1;
            cate[1, n] = tablect12.Rows.Count + up;
            up = tablect11.Rows.Count + tablect12.Rows.Count;

            //defpro
            progressForm.SetProgressValue(40);
            ///
            ///<2-1>
            ///
            prePer = "-1";
            n = 1;
            string ct21 = "select A_stdPercentage, " +
                "A_stdRatio,A_testRatio,A_stdAngle,A_testAngle,A_testPercentage from D_testData where A_testSetID="+global_testSetID+" and A_testProjectID=2 and A_testRangeID=1 order by A_stdPercentage";
            System.Data.DataTable tablect21 = F_select_Access(ct21);
            Console.WriteLine(tablect21.Rows.Count);
            for (int i = 0; i < tablect21.Rows.Count; i++)
            {
                if (Convert.ToDouble(tablect21.Rows[i].ItemArray[0].ToString()) != 0)
                {
                    double temp_percent = (Convert.ToDouble(tablect21.Rows[i].ItemArray[5].ToString()) - Convert.ToDouble(tablect21.Rows[i].ItemArray[0].ToString())) / Convert.ToDouble(tablect21.Rows[i].ItemArray[0].ToString());
                    if (System.Math.Abs(temp_percent) > ct150v)
                    {
                        ct150v = temp_percent;
                    }
                }

                if (Convert.ToDouble(tablect21.Rows[i].ItemArray[1].ToString()) != 0)
                {
                    report.InsertCell(table, i + 3 + up, 3, Convert.ToDouble(tablect21.Rows[i].ItemArray[1].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 5, Convert.ToDouble(tablect21.Rows[i].ItemArray[2].ToString()).ToString("0.0000"));
                    double error_ratio = (Convert.ToDouble(tablect21.Rows[i].ItemArray[2].ToString()) - Convert.ToDouble(tablect21.Rows[i].ItemArray[1].ToString())) / Convert.ToDouble(tablect21.Rows[i].ItemArray[1].ToString());
                    report.InsertCell(table, i + 3 + up, 7, error_ratio.ToString("0.00"));
                }
                else
                {
                    report.InsertCell(table, i + 3 + up, 3, Convert.ToDouble(tablect21.Rows[i].ItemArray[1].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 5, Convert.ToDouble(tablect21.Rows[i].ItemArray[2].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 7, " ");
                }
                if (Convert.ToDouble(tablect21.Rows[i].ItemArray[3].ToString()) != 0)
                {
                    report.InsertCell(table, i + 3 + up, 4, Convert.ToDouble(tablect21.Rows[i].ItemArray[3].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 6, Convert.ToDouble(tablect21.Rows[i].ItemArray[4].ToString()).ToString("0.0000"));
                    double error_angle = (Convert.ToDouble(tablect21.Rows[i].ItemArray[4].ToString()) - Convert.ToDouble(tablect21.Rows[i].ItemArray[3].ToString())) / Convert.ToDouble(tablect21.Rows[i].ItemArray[3].ToString());
                    report.InsertCell(table, i + 3 + up, 8, error_angle.ToString("#.##"));
                }
                else
                {
                    report.InsertCell(table, i + 3 + up, 4, Convert.ToDouble(tablect21.Rows[i].ItemArray[3].ToString()).ToString());
                    report.InsertCell(table, i + 3 + up, 6, Convert.ToDouble(tablect21.Rows[i].ItemArray[4].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 8, "0");
                }
                report.InsertCell(table, i + 3 + up, 2, tablect21.Rows[i].ItemArray[0].ToString() + "%");

                //////////////////
                if (tablect21.Rows[i].ItemArray[0].ToString() != prePer)
                {
                    cate[2, n] = i + up;
                    prePer = tablect21.Rows[i].ItemArray[0].ToString();
                    n = n + 1;
                    Console.WriteLine(n);
                }
                report.InsertCell(table, i + 3 + up, 1, "VT(150V)");
            }
            cate[2, 0] = n + 1;
            cate[2, n] = tablect21.Rows.Count + up;
            up = tablect11.Rows.Count + tablect12.Rows.Count + tablect21.Rows.Count;

            //defpro
            progressForm.SetProgressValue(50);
            ///
            ///<2-2>
            ///
            prePer = "-1";
            n = 1;
            string ct22 = "select A_stdPercentage, " +
                "A_stdRatio,A_testRatio,A_stdAngle,A_testAngle,A_testPercentage from D_testData where A_testSetID="+global_testSetID+" and A_testProjectID=2 and A_testRangeID=2 order by A_stdPercentage";
            System.Data.DataTable tablect22 = F_select_Access(ct22);
            Console.WriteLine(tablect22.Rows.Count);
            for (int i = 0; i < tablect22.Rows.Count; i++)
            {
                if (Convert.ToDouble(tablect22.Rows[i].ItemArray[0].ToString()) != 0)
                {
                    double temp_percent = (Convert.ToDouble(tablect22.Rows[i].ItemArray[5].ToString()) - Convert.ToDouble(tablect22.Rows[i].ItemArray[0].ToString())) / Convert.ToDouble(tablect22.Rows[i].ItemArray[0].ToString());
                    if (System.Math.Abs(temp_percent) > ct100v)
                    {
                        ct100v = temp_percent;
                    }
                }

                if (Convert.ToDouble(tablect22.Rows[i].ItemArray[1].ToString()) != 0)
                {
                    report.InsertCell(table, i + 3 + up, 3, Convert.ToDouble(tablect22.Rows[i].ItemArray[1].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 5, Convert.ToDouble(tablect22.Rows[i].ItemArray[2].ToString()).ToString("0.0000"));
                    double error_ratio = (Convert.ToDouble(tablect22.Rows[i].ItemArray[2].ToString()) - Convert.ToDouble(tablect22.Rows[i].ItemArray[1].ToString())) / Convert.ToDouble(tablect22.Rows[i].ItemArray[1].ToString());
                    report.InsertCell(table, i + 3 + up, 7, error_ratio.ToString("0.00"));
                }
                else
                {
                    report.InsertCell(table, i + 3 + up, 3, Convert.ToDouble(tablect22.Rows[i].ItemArray[1].ToString()).ToString());
                    report.InsertCell(table, i + 3 + up, 5, Convert.ToDouble(tablect22.Rows[i].ItemArray[2].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 7, "0");
                }
                if (Convert.ToDouble(tablect22.Rows[i].ItemArray[3].ToString()) != 0)
                {
                    report.InsertCell(table, i + 3 + up, 4, Convert.ToDouble(tablect22.Rows[i].ItemArray[3].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 6, Convert.ToDouble(tablect22.Rows[i].ItemArray[4].ToString()).ToString("0.0000"));
                    double error_angle = (Convert.ToDouble(tablect22.Rows[i].ItemArray[4].ToString()) - Convert.ToDouble(tablect22.Rows[i].ItemArray[3].ToString())) / Convert.ToDouble(tablect22.Rows[i].ItemArray[3].ToString());
                    report.InsertCell(table, i + 3 + up, 8, error_angle.ToString("0.00"));
                }
                else
                {
                    report.InsertCell(table, i + 3 + up, 4, Convert.ToDouble(tablect22.Rows[i].ItemArray[3].ToString()).ToString());
                    report.InsertCell(table, i + 3 + up, 6, Convert.ToDouble(tablect22.Rows[i].ItemArray[4].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 8, "0");
                }
                report.InsertCell(table, i + 3 + up, 2, tablect22.Rows[i].ItemArray[0].ToString() + "%");

                //////////////////
                if (tablect22.Rows[i].ItemArray[0].ToString() != prePer)
                {
                    cate[3, n] = i + up;
                    prePer = tablect22.Rows[i].ItemArray[0].ToString();
                    n = n + 1;
                    Console.WriteLine(n);
                }
                report.InsertCell(table, i + 3 + up, 1, "VT(100V)");
            }
            cate[3, 0] = n + 1;
            cate[3, n] = tablect22.Rows.Count + up;
            up = tablect11.Rows.Count + tablect12.Rows.Count + tablect21.Rows.Count + tablect22.Rows.Count;

            /////////////////////////////////

            //defpro
            progressForm.SetProgressValue(60);
            ///
            ///<2-3>
            ///
            prePer = "-1";
            n = 1;
            string ct23 = "select A_stdPercentage, " +
                "A_stdRatio,A_testRatio,A_stdAngle,A_testAngle,A_testPercentage from D_testData where A_testSetID="+global_testSetID+" and A_testProjectID=2 and A_testRangeID=3 order by A_stdPercentage";
            System.Data.DataTable tablect23 = F_select_Access(ct23);
            Console.WriteLine(tablect23.Rows.Count);
            for (int i = 0; i < tablect23.Rows.Count; i++)
            {
                if (Convert.ToDouble(tablect23.Rows[i].ItemArray[0].ToString()) != 0)
                {
                    double temp_percent = (Convert.ToDouble(tablect23.Rows[i].ItemArray[5].ToString()) - Convert.ToDouble(tablect23.Rows[i].ItemArray[0].ToString())) / Convert.ToDouble(tablect23.Rows[i].ItemArray[0].ToString());
                    if (System.Math.Abs(temp_percent) > ct100rsqrt3v)
                    {
                        ct100rsqrt3v = temp_percent;
                    }
                }

                if (Convert.ToDouble(tablect23.Rows[i].ItemArray[1].ToString()) != 0)
                {
                    report.InsertCell(table, i + 3 + up, 3, Convert.ToDouble(tablect23.Rows[i].ItemArray[1].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 5, Convert.ToDouble(tablect23.Rows[i].ItemArray[2].ToString()).ToString("0.0000"));
                    double error_ratio = (Convert.ToDouble(tablect23.Rows[i].ItemArray[2].ToString()) - Convert.ToDouble(tablect23.Rows[i].ItemArray[1].ToString())) / Convert.ToDouble(tablect23.Rows[i].ItemArray[1].ToString());
                    report.InsertCell(table, i + 3 + up, 7, error_ratio.ToString("0.00"));
                }
                else
                {
                    report.InsertCell(table, i + 3 + up, 3, Convert.ToDouble(tablect23.Rows[i].ItemArray[1].ToString()).ToString());
                    report.InsertCell(table, i + 3 + up, 5, Convert.ToDouble(tablect23.Rows[i].ItemArray[2].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 7, "0");
                }
                if (Convert.ToDouble(tablect23.Rows[i].ItemArray[3].ToString()) != 0)
                {
                    report.InsertCell(table, i + 3 + up, 4, Convert.ToDouble(tablect23.Rows[i].ItemArray[3].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 6, Convert.ToDouble(tablect23.Rows[i].ItemArray[4].ToString()).ToString("0.0000"));
                    double error_angle = (Convert.ToDouble(tablect23.Rows[i].ItemArray[4].ToString()) - Convert.ToDouble(tablect23.Rows[i].ItemArray[3].ToString())) / Convert.ToDouble(tablect23.Rows[i].ItemArray[3].ToString());
                    report.InsertCell(table, i + 3 + up, 8, error_angle.ToString("0.00"));
                }
                else
                {
                    report.InsertCell(table, i + 3 + up, 4, Convert.ToDouble(tablect23.Rows[i].ItemArray[3].ToString()).ToString());
                    report.InsertCell(table, i + 3 + up, 6, Convert.ToDouble(tablect23.Rows[i].ItemArray[4].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 8, "0");
                }
                report.InsertCell(table, i + 3 + up, 2, tablect23.Rows[i].ItemArray[0].ToString() + "%");

                //////////////////
                if (tablect23.Rows[i].ItemArray[0].ToString() != prePer)
                {
                    cate[4, n] = i + up;
                    prePer = tablect23.Rows[i].ItemArray[0].ToString();
                    n = n + 1;
                    Console.WriteLine(n);
                }
                report.InsertCell(table, i + 3 + up, 1, "VT(100/sqrt(3) V)");
            }
            cate[4, 0] = n + 1;
            cate[4, n] = tablect23.Rows.Count + up;
            up = tablect11.Rows.Count + tablect12.Rows.Count + tablect21.Rows.Count + tablect22.Rows.Count + tablect23.Rows.Count;

            /////////////////////////////////
            //defpro
            progressForm.SetProgressValue(75);
            ///
            ///<2-4>
            ///
            prePer = "-1";
            n = 1;
            string ct24 = "select A_stdPercentage, " +
                "A_stdRatio,A_testRatio,A_stdAngle,A_testAngle,A_testPercentage from D_testData where A_testSetID="+global_testSetID+" and A_testProjectID=2 and A_testRangeID=4 order by A_stdPercentage";
            System.Data.DataTable tablect24 = F_select_Access(ct24);
            Console.WriteLine(tablect24.Rows.Count);
            for (int i = 0; i < tablect24.Rows.Count; i++)
            {
                if (Convert.ToDouble(tablect24.Rows[i].ItemArray[0].ToString()) != 0)
                {
                    double temp_percent = (Convert.ToDouble(tablect24.Rows[i].ItemArray[5].ToString()) - Convert.ToDouble(tablect24.Rows[i].ItemArray[0].ToString())) / Convert.ToDouble(tablect24.Rows[i].ItemArray[0].ToString());
                    if (System.Math.Abs(temp_percent) > ct100r3)
                    {
                        ct100r3 = temp_percent;
                    }
                }

                if (Convert.ToDouble(tablect24.Rows[i].ItemArray[1].ToString()) != 0)
                {
                    report.InsertCell(table, i + 3 + up, 3, Convert.ToDouble(tablect24.Rows[i].ItemArray[1].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 5, Convert.ToDouble(tablect24.Rows[i].ItemArray[2].ToString()).ToString("0.0000"));
                    double error_ratio = (Convert.ToDouble(tablect24.Rows[i].ItemArray[2].ToString()) - Convert.ToDouble(tablect24.Rows[i].ItemArray[1].ToString())) / Convert.ToDouble(tablect24.Rows[i].ItemArray[1].ToString());
                    report.InsertCell(table, i + 3 + up, 7, error_ratio.ToString("0.00"));
                }
                else
                {
                    report.InsertCell(table, i + 3 + up, 3, Convert.ToDouble(tablect24.Rows[i].ItemArray[1].ToString()).ToString());
                    report.InsertCell(table, i + 3 + up, 5, Convert.ToDouble(tablect24.Rows[i].ItemArray[2].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 7, "0");
                }
                if (Convert.ToDouble(tablect24.Rows[i].ItemArray[3].ToString()) != 0)
                {
                    report.InsertCell(table, i + 3 + up, 4, Convert.ToDouble(tablect24.Rows[i].ItemArray[3].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 6, Convert.ToDouble(tablect24.Rows[i].ItemArray[4].ToString()).ToString("0.0000"));
                    double error_angle = (Convert.ToDouble(tablect24.Rows[i].ItemArray[4].ToString()) - Convert.ToDouble(tablect24.Rows[i].ItemArray[3].ToString())) / Convert.ToDouble(tablect24.Rows[i].ItemArray[3].ToString());
                    report.InsertCell(table, i + 3 + up, 8, error_angle.ToString("0.00"));
                }
                else
                {
                    report.InsertCell(table, i + 3 + up, 4, Convert.ToDouble(tablect24.Rows[i].ItemArray[3].ToString()).ToString());
                    report.InsertCell(table, i + 3 + up, 6, Convert.ToDouble(tablect24.Rows[i].ItemArray[4].ToString()).ToString("0.0000"));
                    report.InsertCell(table, i + 3 + up, 8, "0");
                }
                report.InsertCell(table, i + 3 + up, 2, tablect24.Rows[i].ItemArray[0].ToString() + "%");

                //////////////////
                if (tablect24.Rows[i].ItemArray[0].ToString() != prePer)
                {
                    cate[5, n] = i + up;
                    prePer = tablect24.Rows[i].ItemArray[0].ToString();
                    n = n + 1;
                    Console.WriteLine(n);
                }
                report.InsertCell(table, i + 3 + up, 1, "VT(100/3 V)");
            }
            cate[5, 0] = n + 1;
            cate[5, n] = tablect24.Rows.Count + up;
            up = tablect11.Rows.Count + tablect12.Rows.Count + tablect21.Rows.Count + tablect22.Rows.Count + tablect23.Rows.Count + tablect24.Rows.Count;

            //defpro
            progressForm.SetProgressValue(95);

            /////////////////////////////////

            for (int i = 0; i < 6; i++)
            {
                for (int j = 1; j < cate[i, 0]; j++)
                {
                    cate[i, j] = cate[i, j] + 3;
                }
            }
            for (int i = 0; i < 6; i++)
            {
                //cate[i, cate[i, 0] - 1] += 1;
            }

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < cate[i, 0]; j++)
                {
                    Console.Write(cate[i, j] + ",");
                }
                Console.WriteLine("");
            }


            for (int i = 5; i >= 0; i--)
            {
                for (int j = cate[i, 0] - 1; j > 1; j--)
                {
                    if(cate[i, j - 1]!=(cate[i, j] - 1))
                        report.Table_Merge_kaka(table, cate[i, j - 1], cate[i, j] - 1);
                    Console.WriteLine(i + "," + j);
                }
            }
            try
            {
                 for (int i = 5; i >= 0; i--)
                {
                    if (cate[i, 1] != (cate[i, cate[i, 0] - 1] - 1)&&cate[i,0]!=2)
                    {
                         Console.WriteLine(cate[i, 1].ToString()+","+(cate[i, cate[i, 0] - 1] - 1).ToString());
                         report.Table_Merge_kk(table, cate[i, 1], cate[i, cate[i, 0] - 1]-1);
                    }
                       
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("merge error!");
            }

            //defpro
            //progressForm.SetProgressValue(90);
            

            Console.WriteLine("ok!");
            try{
                report.insertBookmark("amax", (ct5a * 100).ToString("0.00") + "%");
            }
            catch (Exception ex){
                Console.WriteLine("insert error!");
            }
            try
            {
                report.insertBookmark("amin", (ct1a * 100).ToString("0.00") + "%");
            }
            catch (Exception ex)
            {
                Console.WriteLine("insert error!");
            }
            try
            {
                report.insertBookmark("vi", (ct150v * 100).ToString("0.00") + "%");
            }
            catch (Exception ex)
            {
                Console.WriteLine("insert error!");
            }
            try
            {
                report.insertBookmark("vii", (ct100v * 100).ToString("0.00") + "%");
            }
            catch (Exception ex)
            {
                Console.WriteLine("insert error!");
            }
            try
            {
                report.insertBookmark("viii", (ct100rsqrt3v * 100).ToString("0.00") + "%");
            }
            catch (Exception ex)
            {
                Console.WriteLine("insert error!");
            }
            try
            {
                report.insertBookmark("viiii", (ct100r3 * 100).ToString("0.00") + "%");
            }
            catch (Exception ex)
            {
                Console.WriteLine("insert error!");
            }

            //defpro
            progressForm.SetProgressValue(100);
            progressForm.SetProgressText("正在保存...");
            /** */
            try
            {
                DateTime dt = DateTime.Now;//获取当前时间
                dt.ToLongTimeString().ToString();  //格式化当前时间 11：05：12
                dt.Date.ToString();//格式当前时间 2012-4-15 00：00：00
                String mytime = dt.Date.ToString().Replace("/", "-");
                mytime = mytime.Replace(":","-");
                report.SaveDocument("E:\\kaka1.doc", SNID + "-" + dt.Year+"-"+dt.Month+"-"+dt.Day);
            }
            catch(Exception ex){
                Console.WriteLine(ex.ToString());
            }
            progressForm.SetProgressText("保存成功");
            //progressForm.Close();
        }
        #endregion


        #region 关闭word进程 @AniChikage
        private void killWord()
        {
            //杀死打开的word进程
            Process myProcess = new Process();
            Process[] wordProcess = Process.GetProcessesByName("winword");
            try
            {
                foreach (Process pro in wordProcess) //这里是找到那些没有界面的Word进程
                {
                    pro.Kill();
                    Console.WriteLine("ok");
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        #endregion
    }
}
