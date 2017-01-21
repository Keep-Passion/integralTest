namespace Arm_tyshkj_design
{
    partial class ChangeProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeProduct));
            this.label_Brand = new System.Windows.Forms.Label();
            this.pictureBox_Brand = new System.Windows.Forms.PictureBox();
            this.CP_textBox_productSNID = new System.Windows.Forms.TextBox();
            this.CP_label_snid = new System.Windows.Forms.Label();
            this.CP_textBox_productType = new System.Windows.Forms.TextBox();
            this.CP_label_type = new System.Windows.Forms.Label();
            this.CP_button_change = new System.Windows.Forms.Button();
            this.CP_button_clear = new System.Windows.Forms.Button();
            this.CP_label_client = new System.Windows.Forms.Label();
            this.CP_label_manu = new System.Windows.Forms.Label();
            this.CP_comboBox_client = new System.Windows.Forms.ComboBox();
            this.CP_comboBox_manu = new System.Windows.Forms.ComboBox();
            this.CP_comboBox_class = new System.Windows.Forms.ComboBox();
            this.CP_comboBox_dut = new System.Windows.Forms.ComboBox();
            this.CP_label_dut = new System.Windows.Forms.Label();
            this.CP_label_class = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Brand)).BeginInit();
            this.SuspendLayout();
            // 
            // label_Brand
            // 
            this.label_Brand.AutoSize = true;
            this.label_Brand.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Brand.Location = new System.Drawing.Point(10, 125);
            this.label_Brand.Name = "label_Brand";
            this.label_Brand.Size = new System.Drawing.Size(157, 14);
            this.label_Brand.TabIndex = 40;
            this.label_Brand.Text = "太原山互科技有限公司";
            // 
            // pictureBox_Brand
            // 
            this.pictureBox_Brand.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_Brand.BackgroundImage")));
            this.pictureBox_Brand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_Brand.Location = new System.Drawing.Point(39, 28);
            this.pictureBox_Brand.Name = "pictureBox_Brand";
            this.pictureBox_Brand.Size = new System.Drawing.Size(94, 92);
            this.pictureBox_Brand.TabIndex = 39;
            this.pictureBox_Brand.TabStop = false;
            // 
            // CP_textBox_productSNID
            // 
            this.CP_textBox_productSNID.Location = new System.Drawing.Point(470, 17);
            this.CP_textBox_productSNID.Name = "CP_textBox_productSNID";
            this.CP_textBox_productSNID.Size = new System.Drawing.Size(104, 21);
            this.CP_textBox_productSNID.TabIndex = 44;
            // 
            // CP_label_snid
            // 
            this.CP_label_snid.AutoSize = true;
            this.CP_label_snid.Location = new System.Drawing.Point(373, 20);
            this.CP_label_snid.Name = "CP_label_snid";
            this.CP_label_snid.Size = new System.Drawing.Size(89, 12);
            this.CP_label_snid.TabIndex = 43;
            this.CP_label_snid.Text = "产品出厂编号：";
            // 
            // CP_textBox_productType
            // 
            this.CP_textBox_productType.Location = new System.Drawing.Point(255, 17);
            this.CP_textBox_productType.Name = "CP_textBox_productType";
            this.CP_textBox_productType.Size = new System.Drawing.Size(104, 21);
            this.CP_textBox_productType.TabIndex = 42;
            // 
            // CP_label_type
            // 
            this.CP_label_type.AutoSize = true;
            this.CP_label_type.Location = new System.Drawing.Point(171, 21);
            this.CP_label_type.Name = "CP_label_type";
            this.CP_label_type.Size = new System.Drawing.Size(65, 12);
            this.CP_label_type.TabIndex = 41;
            this.CP_label_type.Text = "产品型号：";
            // 
            // CP_button_change
            // 
            this.CP_button_change.Location = new System.Drawing.Point(478, 137);
            this.CP_button_change.Name = "CP_button_change";
            this.CP_button_change.Size = new System.Drawing.Size(75, 23);
            this.CP_button_change.TabIndex = 46;
            this.CP_button_change.Text = "添加";
            this.CP_button_change.UseVisualStyleBackColor = true;
            this.CP_button_change.Click += new System.EventHandler(this.CP_button_change_Click);
            // 
            // CP_button_clear
            // 
            this.CP_button_clear.Location = new System.Drawing.Point(262, 137);
            this.CP_button_clear.Name = "CP_button_clear";
            this.CP_button_clear.Size = new System.Drawing.Size(75, 23);
            this.CP_button_clear.TabIndex = 45;
            this.CP_button_clear.Text = "清空";
            this.CP_button_clear.UseVisualStyleBackColor = true;
            this.CP_button_clear.Click += new System.EventHandler(this.CP_button_clear_Click);
            // 
            // CP_label_client
            // 
            this.CP_label_client.AutoSize = true;
            this.CP_label_client.Location = new System.Drawing.Point(170, 63);
            this.CP_label_client.Name = "CP_label_client";
            this.CP_label_client.Size = new System.Drawing.Size(65, 12);
            this.CP_label_client.TabIndex = 48;
            this.CP_label_client.Text = "产品客户：";
            // 
            // CP_label_manu
            // 
            this.CP_label_manu.AutoSize = true;
            this.CP_label_manu.Location = new System.Drawing.Point(385, 63);
            this.CP_label_manu.Name = "CP_label_manu";
            this.CP_label_manu.Size = new System.Drawing.Size(77, 12);
            this.CP_label_manu.TabIndex = 47;
            this.CP_label_manu.Text = "产品制造商：";
            // 
            // CP_comboBox_client
            // 
            this.CP_comboBox_client.BackColor = System.Drawing.SystemColors.Window;
            this.CP_comboBox_client.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CP_comboBox_client.FormattingEnabled = true;
            this.CP_comboBox_client.Location = new System.Drawing.Point(255, 58);
            this.CP_comboBox_client.Name = "CP_comboBox_client";
            this.CP_comboBox_client.Size = new System.Drawing.Size(104, 20);
            this.CP_comboBox_client.TabIndex = 49;
            // 
            // CP_comboBox_manu
            // 
            this.CP_comboBox_manu.BackColor = System.Drawing.SystemColors.Window;
            this.CP_comboBox_manu.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CP_comboBox_manu.FormattingEnabled = true;
            this.CP_comboBox_manu.Location = new System.Drawing.Point(470, 58);
            this.CP_comboBox_manu.Name = "CP_comboBox_manu";
            this.CP_comboBox_manu.Size = new System.Drawing.Size(104, 20);
            this.CP_comboBox_manu.TabIndex = 50;
            // 
            // CP_comboBox_class
            // 
            this.CP_comboBox_class.BackColor = System.Drawing.SystemColors.Window;
            this.CP_comboBox_class.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CP_comboBox_class.FormattingEnabled = true;
            this.CP_comboBox_class.Location = new System.Drawing.Point(470, 99);
            this.CP_comboBox_class.Name = "CP_comboBox_class";
            this.CP_comboBox_class.Size = new System.Drawing.Size(104, 20);
            this.CP_comboBox_class.TabIndex = 54;
            // 
            // CP_comboBox_dut
            // 
            this.CP_comboBox_dut.BackColor = System.Drawing.SystemColors.Window;
            this.CP_comboBox_dut.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CP_comboBox_dut.FormattingEnabled = true;
            this.CP_comboBox_dut.Location = new System.Drawing.Point(255, 99);
            this.CP_comboBox_dut.Name = "CP_comboBox_dut";
            this.CP_comboBox_dut.Size = new System.Drawing.Size(104, 20);
            this.CP_comboBox_dut.TabIndex = 53;
            // 
            // CP_label_dut
            // 
            this.CP_label_dut.AutoSize = true;
            this.CP_label_dut.Location = new System.Drawing.Point(171, 102);
            this.CP_label_dut.Name = "CP_label_dut";
            this.CP_label_dut.Size = new System.Drawing.Size(89, 12);
            this.CP_label_dut.TabIndex = 52;
            this.CP_label_dut.Text = "产品器具名称：";
            // 
            // CP_label_class
            // 
            this.CP_label_class.AutoSize = true;
            this.CP_label_class.Location = new System.Drawing.Point(373, 102);
            this.CP_label_class.Name = "CP_label_class";
            this.CP_label_class.Size = new System.Drawing.Size(101, 12);
            this.CP_label_class.TabIndex = 51;
            this.CP_label_class.Text = "产品准确度等级：";
            // 
            // ChangeProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 177);
            this.Controls.Add(this.CP_comboBox_class);
            this.Controls.Add(this.CP_comboBox_dut);
            this.Controls.Add(this.CP_label_dut);
            this.Controls.Add(this.CP_label_class);
            this.Controls.Add(this.CP_comboBox_manu);
            this.Controls.Add(this.CP_comboBox_client);
            this.Controls.Add(this.CP_label_client);
            this.Controls.Add(this.CP_label_manu);
            this.Controls.Add(this.CP_button_change);
            this.Controls.Add(this.CP_button_clear);
            this.Controls.Add(this.CP_textBox_productSNID);
            this.Controls.Add(this.CP_label_snid);
            this.Controls.Add(this.CP_textBox_productType);
            this.Controls.Add(this.CP_label_type);
            this.Controls.Add(this.label_Brand);
            this.Controls.Add(this.pictureBox_Brand);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChangeProduct";
            this.Text = "产品信息调整";
            this.Load += new System.EventHandler(this.ChangeProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Brand)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Brand;
        private System.Windows.Forms.PictureBox pictureBox_Brand;
        private System.Windows.Forms.TextBox CP_textBox_productSNID;
        private System.Windows.Forms.Label CP_label_snid;
        private System.Windows.Forms.TextBox CP_textBox_productType;
        private System.Windows.Forms.Label CP_label_type;
        private System.Windows.Forms.Button CP_button_change;
        private System.Windows.Forms.Button CP_button_clear;
        private System.Windows.Forms.Label CP_label_client;
        private System.Windows.Forms.Label CP_label_manu;
        private System.Windows.Forms.ComboBox CP_comboBox_client;
        private System.Windows.Forms.ComboBox CP_comboBox_manu;
        private System.Windows.Forms.ComboBox CP_comboBox_class;
        private System.Windows.Forms.ComboBox CP_comboBox_dut;
        private System.Windows.Forms.Label CP_label_dut;
        private System.Windows.Forms.Label CP_label_class;
    }
}