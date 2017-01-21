using System;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Arm_tyshkj_design
{
    public partial class testSerialPort : Form
    {
        private Byte[] outputBytes = new Byte[10];
        public testSerialPort()
        {
            InitializeComponent();
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
                test_Button_scan.Text = "扫描";
                test_Button_scan.Enabled = true;
                return false;
            }
            outputBytes[0] = 0xAA; outputBytes[1] = 0x73; outputBytes[2] = 0x00; outputBytes[3] = 0x00; outputBytes[4] = 0x01;
            outputBytes[5] = 0x01; outputBytes[6] = 0xCC; outputBytes[7] = 0x33; outputBytes[8] = 0xC3; outputBytes[9] = 0x3C;
            serialPort.Write(outputBytes, 0, 10);
            testShowBytes();
            int tick = Environment.TickCount;//因为波特率是按秒算的，我们就检测0.5秒好了
            while (Environment.TickCount - tick < 500)
            {
                if (serialPort.BytesToRead == 2)
                {
                    string tempRec="" + serialPort.ReadByte().ToString("x2") + " " + serialPort.ReadByte().ToString("x2");
                    testlabel_2.Text = "收到的字节为："+tempRec.ToUpper();
                    if (tempRec.ToUpper().CompareTo("A5 5A") == 0)
                    {
                        test_textBox_port.Text = serialPort.PortName;
                        test_Button_scan.Text = "连接成功";
                        test_Button_scan.Enabled = false;
                        return true;
                    }
                }
            }
            return false;
        }

        private void test_Button_scan_Click(object sender, EventArgs e)
        {
            string[] str = SerialPort.GetPortNames();       //获取所有端口号
            Boolean isConnect = false;
            for (int i = 0; i < str.Length; i++)
            {
                serialPort.PortName = str[i];
                MessageBox.Show("正在处理端口号:" + serialPort.PortName.ToString());
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
                MessageBox.Show("未检测到检测设备，请插入该设备", "未检测到设备", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            testlabel_1.Text = "发送字节为：" + tempStr;
        }
    }
}
