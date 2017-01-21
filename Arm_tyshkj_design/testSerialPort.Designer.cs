namespace Arm_tyshkj_design
{
    partial class testSerialPort
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.test_Button_scan = new System.Windows.Forms.Button();
            this.test_label_port = new System.Windows.Forms.Label();
            this.test_textBox_port = new System.Windows.Forms.TextBox();
            this.serialPort = new System.IO.Ports.SerialPort(this.components);
            this.testlabel_1 = new System.Windows.Forms.Label();
            this.testlabel_2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // test_Button_scan
            // 
            this.test_Button_scan.Location = new System.Drawing.Point(196, 19);
            this.test_Button_scan.Name = "test_Button_scan";
            this.test_Button_scan.Size = new System.Drawing.Size(76, 23);
            this.test_Button_scan.TabIndex = 8;
            this.test_Button_scan.TabStop = false;
            this.test_Button_scan.Text = "扫描";
            this.test_Button_scan.UseVisualStyleBackColor = true;
            this.test_Button_scan.Click += new System.EventHandler(this.test_Button_scan_Click);
            // 
            // test_label_port
            // 
            this.test_label_port.AutoSize = true;
            this.test_label_port.Location = new System.Drawing.Point(25, 24);
            this.test_label_port.Name = "test_label_port";
            this.test_label_port.Size = new System.Drawing.Size(53, 12);
            this.test_label_port.TabIndex = 7;
            this.test_label_port.Text = "串口号：";
            // 
            // test_textBox_port
            // 
            this.test_textBox_port.Enabled = false;
            this.test_textBox_port.Location = new System.Drawing.Point(84, 21);
            this.test_textBox_port.Name = "test_textBox_port";
            this.test_textBox_port.Size = new System.Drawing.Size(87, 21);
            this.test_textBox_port.TabIndex = 6;
            this.test_textBox_port.TabStop = false;
            // 
            // testlabel_1
            // 
            this.testlabel_1.AutoSize = true;
            this.testlabel_1.Location = new System.Drawing.Point(27, 181);
            this.testlabel_1.Name = "testlabel_1";
            this.testlabel_1.Size = new System.Drawing.Size(89, 12);
            this.testlabel_1.TabIndex = 9;
            this.testlabel_1.Text = "发送的字节为：";
            // 
            // testlabel_2
            // 
            this.testlabel_2.AutoSize = true;
            this.testlabel_2.Location = new System.Drawing.Point(27, 207);
            this.testlabel_2.Name = "testlabel_2";
            this.testlabel_2.Size = new System.Drawing.Size(89, 12);
            this.testlabel_2.TabIndex = 10;
            this.testlabel_2.Text = "接收的字节为：";
            // 
            // testSerialPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 262);
            this.Controls.Add(this.testlabel_2);
            this.Controls.Add(this.testlabel_1);
            this.Controls.Add(this.test_Button_scan);
            this.Controls.Add(this.test_label_port);
            this.Controls.Add(this.test_textBox_port);
            this.Name = "testSerialPort";
            this.Text = "testSerialPort";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button test_Button_scan;
        private System.Windows.Forms.Label test_label_port;
        private System.Windows.Forms.TextBox test_textBox_port;
        private System.IO.Ports.SerialPort serialPort;
        private System.Windows.Forms.Label testlabel_1;
        private System.Windows.Forms.Label testlabel_2;
    }
}