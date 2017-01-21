namespace Arm_tyshkj_design
{
    partial class ChangeClient
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeClient));
            this.CC_label_name = new System.Windows.Forms.Label();
            this.CC_textBox_name = new System.Windows.Forms.TextBox();
            this.CC_textBox_address = new System.Windows.Forms.TextBox();
            this.CC_label_address = new System.Windows.Forms.Label();
            this.CC_textBox_phone = new System.Windows.Forms.TextBox();
            this.CC_label_phone = new System.Windows.Forms.Label();
            this.label_Brand = new System.Windows.Forms.Label();
            this.pictureBox_Brand = new System.Windows.Forms.PictureBox();
            this.CC_button_clear = new System.Windows.Forms.Button();
            this.CC_button_change = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Brand)).BeginInit();
            this.SuspendLayout();
            // 
            // CC_label_name
            // 
            this.CC_label_name.AutoSize = true;
            this.CC_label_name.Location = new System.Drawing.Point(170, 33);
            this.CC_label_name.Name = "CC_label_name";
            this.CC_label_name.Size = new System.Drawing.Size(65, 12);
            this.CC_label_name.TabIndex = 0;
            this.CC_label_name.Text = "客户名称：";
            // 
            // CC_textBox_name
            // 
            this.CC_textBox_name.Location = new System.Drawing.Point(253, 29);
            this.CC_textBox_name.Name = "CC_textBox_name";
            this.CC_textBox_name.Size = new System.Drawing.Size(163, 21);
            this.CC_textBox_name.TabIndex = 1;
            // 
            // CC_textBox_address
            // 
            this.CC_textBox_address.Location = new System.Drawing.Point(253, 62);
            this.CC_textBox_address.Name = "CC_textBox_address";
            this.CC_textBox_address.Size = new System.Drawing.Size(163, 21);
            this.CC_textBox_address.TabIndex = 3;
            // 
            // CC_label_address
            // 
            this.CC_label_address.AutoSize = true;
            this.CC_label_address.Location = new System.Drawing.Point(170, 66);
            this.CC_label_address.Name = "CC_label_address";
            this.CC_label_address.Size = new System.Drawing.Size(65, 12);
            this.CC_label_address.TabIndex = 2;
            this.CC_label_address.Text = "客户地址：";
            // 
            // CC_textBox_phone
            // 
            this.CC_textBox_phone.Location = new System.Drawing.Point(253, 95);
            this.CC_textBox_phone.Name = "CC_textBox_phone";
            this.CC_textBox_phone.Size = new System.Drawing.Size(163, 21);
            this.CC_textBox_phone.TabIndex = 5;
            // 
            // CC_label_phone
            // 
            this.CC_label_phone.AutoSize = true;
            this.CC_label_phone.Location = new System.Drawing.Point(170, 99);
            this.CC_label_phone.Name = "CC_label_phone";
            this.CC_label_phone.Size = new System.Drawing.Size(89, 12);
            this.CC_label_phone.TabIndex = 4;
            this.CC_label_phone.Text = "客户联系方式：";
            // 
            // label_Brand
            // 
            this.label_Brand.AutoSize = true;
            this.label_Brand.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Brand.Location = new System.Drawing.Point(7, 128);
            this.label_Brand.Name = "label_Brand";
            this.label_Brand.Size = new System.Drawing.Size(157, 14);
            this.label_Brand.TabIndex = 38;
            this.label_Brand.Text = "太原山互科技有限公司";
            // 
            // pictureBox_Brand
            // 
            this.pictureBox_Brand.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_Brand.BackgroundImage")));
            this.pictureBox_Brand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_Brand.Location = new System.Drawing.Point(36, 31);
            this.pictureBox_Brand.Name = "pictureBox_Brand";
            this.pictureBox_Brand.Size = new System.Drawing.Size(94, 92);
            this.pictureBox_Brand.TabIndex = 37;
            this.pictureBox_Brand.TabStop = false;
            // 
            // CC_button_clear
            // 
            this.CC_button_clear.Location = new System.Drawing.Point(181, 131);
            this.CC_button_clear.Name = "CC_button_clear";
            this.CC_button_clear.Size = new System.Drawing.Size(75, 23);
            this.CC_button_clear.TabIndex = 39;
            this.CC_button_clear.Text = "清空";
            this.CC_button_clear.UseVisualStyleBackColor = true;
            this.CC_button_clear.Click += new System.EventHandler(this.CC_button_clear_Click);
            // 
            // CC_button_change
            // 
            this.CC_button_change.Location = new System.Drawing.Point(341, 131);
            this.CC_button_change.Name = "CC_button_change";
            this.CC_button_change.Size = new System.Drawing.Size(75, 23);
            this.CC_button_change.TabIndex = 40;
            this.CC_button_change.Text = "添加";
            this.CC_button_change.UseVisualStyleBackColor = true;
            this.CC_button_change.Click += new System.EventHandler(this.CC_button_change_Click);
            // 
            // ChangeClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(429, 173);
            this.Controls.Add(this.CC_button_change);
            this.Controls.Add(this.CC_button_clear);
            this.Controls.Add(this.label_Brand);
            this.Controls.Add(this.pictureBox_Brand);
            this.Controls.Add(this.CC_textBox_phone);
            this.Controls.Add(this.CC_label_phone);
            this.Controls.Add(this.CC_textBox_address);
            this.Controls.Add(this.CC_label_address);
            this.Controls.Add(this.CC_textBox_name);
            this.Controls.Add(this.CC_label_name);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChangeClient";
            this.Text = "客户信息调整";
            this.Load += new System.EventHandler(this.ChangeClient_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Brand)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label CC_label_name;
        private System.Windows.Forms.TextBox CC_textBox_name;
        private System.Windows.Forms.TextBox CC_textBox_address;
        private System.Windows.Forms.Label CC_label_address;
        private System.Windows.Forms.TextBox CC_textBox_phone;
        private System.Windows.Forms.Label CC_label_phone;
        private System.Windows.Forms.Label label_Brand;
        private System.Windows.Forms.PictureBox pictureBox_Brand;
        private System.Windows.Forms.Button CC_button_clear;
        private System.Windows.Forms.Button CC_button_change;
    }
}