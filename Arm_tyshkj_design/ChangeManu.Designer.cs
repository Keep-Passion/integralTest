namespace Arm_tyshkj_design
{
    partial class ChangeManu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeManu));
            this.CMN_button_change = new System.Windows.Forms.Button();
            this.CMN_button_clear = new System.Windows.Forms.Button();
            this.CMN_textBox_phone = new System.Windows.Forms.TextBox();
            this.CMN_label_phone = new System.Windows.Forms.Label();
            this.CMN_textBox_name = new System.Windows.Forms.TextBox();
            this.CMN_label_name = new System.Windows.Forms.Label();
            this.label_Brand = new System.Windows.Forms.Label();
            this.pictureBox_Brand = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Brand)).BeginInit();
            this.SuspendLayout();
            // 
            // CMN_button_change
            // 
            this.CMN_button_change.Location = new System.Drawing.Point(338, 115);
            this.CMN_button_change.Name = "CMN_button_change";
            this.CMN_button_change.Size = new System.Drawing.Size(75, 23);
            this.CMN_button_change.TabIndex = 54;
            this.CMN_button_change.Text = "添加";
            this.CMN_button_change.UseVisualStyleBackColor = true;
            this.CMN_button_change.Click += new System.EventHandler(this.CMN_button_change_Click);
            // 
            // CMN_button_clear
            // 
            this.CMN_button_clear.Location = new System.Drawing.Point(180, 115);
            this.CMN_button_clear.Name = "CMN_button_clear";
            this.CMN_button_clear.Size = new System.Drawing.Size(75, 23);
            this.CMN_button_clear.TabIndex = 53;
            this.CMN_button_clear.Text = "清空";
            this.CMN_button_clear.UseVisualStyleBackColor = true;
            this.CMN_button_clear.Click += new System.EventHandler(this.CMN_button_clear_Click);
            // 
            // CMN_textBox_phone
            // 
            this.CMN_textBox_phone.Location = new System.Drawing.Point(251, 64);
            this.CMN_textBox_phone.Name = "CMN_textBox_phone";
            this.CMN_textBox_phone.Size = new System.Drawing.Size(163, 21);
            this.CMN_textBox_phone.TabIndex = 52;
            // 
            // CMN_label_phone
            // 
            this.CMN_label_phone.AutoSize = true;
            this.CMN_label_phone.Location = new System.Drawing.Point(154, 68);
            this.CMN_label_phone.Name = "CMN_label_phone";
            this.CMN_label_phone.Size = new System.Drawing.Size(101, 12);
            this.CMN_label_phone.TabIndex = 51;
            this.CMN_label_phone.Text = "制造商联系方式：";
            // 
            // CMN_textBox_name
            // 
            this.CMN_textBox_name.Location = new System.Drawing.Point(251, 23);
            this.CMN_textBox_name.Name = "CMN_textBox_name";
            this.CMN_textBox_name.Size = new System.Drawing.Size(163, 21);
            this.CMN_textBox_name.TabIndex = 50;
            // 
            // CMN_label_name
            // 
            this.CMN_label_name.AutoSize = true;
            this.CMN_label_name.Location = new System.Drawing.Point(168, 27);
            this.CMN_label_name.Name = "CMN_label_name";
            this.CMN_label_name.Size = new System.Drawing.Size(77, 12);
            this.CMN_label_name.TabIndex = 49;
            this.CMN_label_name.Text = "制造商名称：";
            // 
            // label_Brand
            // 
            this.label_Brand.AutoSize = true;
            this.label_Brand.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Brand.Location = new System.Drawing.Point(5, 120);
            this.label_Brand.Name = "label_Brand";
            this.label_Brand.Size = new System.Drawing.Size(157, 14);
            this.label_Brand.TabIndex = 48;
            this.label_Brand.Text = "太原山互科技有限公司";
            // 
            // pictureBox_Brand
            // 
            this.pictureBox_Brand.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_Brand.BackgroundImage")));
            this.pictureBox_Brand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_Brand.Location = new System.Drawing.Point(34, 23);
            this.pictureBox_Brand.Name = "pictureBox_Brand";
            this.pictureBox_Brand.Size = new System.Drawing.Size(94, 92);
            this.pictureBox_Brand.TabIndex = 47;
            this.pictureBox_Brand.TabStop = false;
            // 
            // ChangeManu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 153);
            this.Controls.Add(this.CMN_button_change);
            this.Controls.Add(this.CMN_button_clear);
            this.Controls.Add(this.CMN_textBox_phone);
            this.Controls.Add(this.CMN_label_phone);
            this.Controls.Add(this.CMN_textBox_name);
            this.Controls.Add(this.CMN_label_name);
            this.Controls.Add(this.label_Brand);
            this.Controls.Add(this.pictureBox_Brand);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChangeManu";
            this.Text = "制造商信息调整";
            this.Load += new System.EventHandler(this.ChangeManu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Brand)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CMN_button_change;
        private System.Windows.Forms.Button CMN_button_clear;
        private System.Windows.Forms.TextBox CMN_textBox_phone;
        private System.Windows.Forms.Label CMN_label_phone;
        private System.Windows.Forms.TextBox CMN_textBox_name;
        private System.Windows.Forms.Label CMN_label_name;
        private System.Windows.Forms.Label label_Brand;
        private System.Windows.Forms.PictureBox pictureBox_Brand;
    }
}