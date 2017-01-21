namespace Arm_tyshkj_design
{
    partial class CustomerMangementPageTest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomerMangementPageTest));
            this.CM_comboBox_clientName = new System.Windows.Forms.ComboBox();
            this.CM_label_customer = new System.Windows.Forms.Label();
            this.CM_label_pronum = new System.Windows.Forms.Label();
            this.CM_comboBox_productSNID = new System.Windows.Forms.ComboBox();
            this.CM_groupBox_verification = new System.Windows.Forms.GroupBox();
            this.CM_radioButton_nofirst = new System.Windows.Forms.RadioButton();
            this.CM_radioButton_first = new System.Windows.Forms.RadioButton();
            this.CM_button_detail = new System.Windows.Forms.Button();
            this.CM_button_submit = new System.Windows.Forms.Button();
            this.label_Brand = new System.Windows.Forms.Label();
            this.CM_tabControl_detail = new System.Windows.Forms.TabControl();
            this.CM_tabPage_customer = new System.Windows.Forms.TabPage();
            this.CM_listView_client = new System.Windows.Forms.ListView();
            this.A_clientID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.A_clientName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.A_clientAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.A_clientPhone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CM_mouseRight_client = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewClient_button = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateClient_button = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteClient_button = new System.Windows.Forms.ToolStripMenuItem();
            this.CM_tabPage_manu = new System.Windows.Forms.TabPage();
            this.CM_listView_manu = new System.Windows.Forms.ListView();
            this.A_manuID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.A_manuName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.A_manuPhone = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CM_mouseRight_manu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewManu_button = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateManu_button = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteManu_button = new System.Windows.Forms.ToolStripMenuItem();
            this.CM_tabPage_sensor = new System.Windows.Forms.TabPage();
            this.CM_listView_product = new System.Windows.Forms.ListView();
            this.A_productType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.A_productSNID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.A_productClientID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.A_productManuID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.A_productDutID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.A_productClassID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CM_mouseRight_product = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewProduct_button = new System.Windows.Forms.ToolStripMenuItem();
            this.UpdateProduct_button = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteProduct_button = new System.Windows.Forms.ToolStripMenuItem();
            this.CM_groupBox_test = new System.Windows.Forms.GroupBox();
            this.CM_radioButton_normal = new System.Windows.Forms.RadioButton();
            this.CM_radioButton_formal = new System.Windows.Forms.RadioButton();
            this.pictureBox_Brand = new System.Windows.Forms.PictureBox();
            this.CM_groupBox_verification.SuspendLayout();
            this.CM_tabControl_detail.SuspendLayout();
            this.CM_tabPage_customer.SuspendLayout();
            this.CM_mouseRight_client.SuspendLayout();
            this.CM_tabPage_manu.SuspendLayout();
            this.CM_mouseRight_manu.SuspendLayout();
            this.CM_tabPage_sensor.SuspendLayout();
            this.CM_mouseRight_product.SuspendLayout();
            this.CM_groupBox_test.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Brand)).BeginInit();
            this.SuspendLayout();
            // 
            // CM_comboBox_clientName
            // 
            this.CM_comboBox_clientName.BackColor = System.Drawing.SystemColors.Window;
            this.CM_comboBox_clientName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CM_comboBox_clientName.FormattingEnabled = true;
            this.CM_comboBox_clientName.Location = new System.Drawing.Point(212, 30);
            this.CM_comboBox_clientName.Name = "CM_comboBox_clientName";
            this.CM_comboBox_clientName.Size = new System.Drawing.Size(113, 20);
            this.CM_comboBox_clientName.TabIndex = 37;
            this.CM_comboBox_clientName.SelectedIndexChanged += new System.EventHandler(this.CM_comboBox_clientName_SelectedIndexChanged);
            // 
            // CM_label_customer
            // 
            this.CM_label_customer.AutoSize = true;
            this.CM_label_customer.Location = new System.Drawing.Point(146, 34);
            this.CM_label_customer.Name = "CM_label_customer";
            this.CM_label_customer.Size = new System.Drawing.Size(65, 12);
            this.CM_label_customer.TabIndex = 38;
            this.CM_label_customer.Text = "客户名称：";
            // 
            // CM_label_pronum
            // 
            this.CM_label_pronum.AutoSize = true;
            this.CM_label_pronum.Location = new System.Drawing.Point(335, 34);
            this.CM_label_pronum.Name = "CM_label_pronum";
            this.CM_label_pronum.Size = new System.Drawing.Size(65, 12);
            this.CM_label_pronum.TabIndex = 42;
            this.CM_label_pronum.Text = "产品编号：";
            // 
            // CM_comboBox_productSNID
            // 
            this.CM_comboBox_productSNID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CM_comboBox_productSNID.FormattingEnabled = true;
            this.CM_comboBox_productSNID.Location = new System.Drawing.Point(400, 30);
            this.CM_comboBox_productSNID.Name = "CM_comboBox_productSNID";
            this.CM_comboBox_productSNID.Size = new System.Drawing.Size(81, 20);
            this.CM_comboBox_productSNID.TabIndex = 41;
            // 
            // CM_groupBox_verification
            // 
            this.CM_groupBox_verification.Controls.Add(this.CM_radioButton_nofirst);
            this.CM_groupBox_verification.Controls.Add(this.CM_radioButton_first);
            this.CM_groupBox_verification.Location = new System.Drawing.Point(161, 77);
            this.CM_groupBox_verification.Name = "CM_groupBox_verification";
            this.CM_groupBox_verification.Size = new System.Drawing.Size(164, 48);
            this.CM_groupBox_verification.TabIndex = 43;
            this.CM_groupBox_verification.TabStop = false;
            this.CM_groupBox_verification.Text = "检定";
            // 
            // CM_radioButton_nofirst
            // 
            this.CM_radioButton_nofirst.AutoSize = true;
            this.CM_radioButton_nofirst.Location = new System.Drawing.Point(83, 20);
            this.CM_radioButton_nofirst.Name = "CM_radioButton_nofirst";
            this.CM_radioButton_nofirst.Size = new System.Drawing.Size(71, 16);
            this.CM_radioButton_nofirst.TabIndex = 1;
            this.CM_radioButton_nofirst.Text = "后续检定";
            this.CM_radioButton_nofirst.UseVisualStyleBackColor = true;
            // 
            // CM_radioButton_first
            // 
            this.CM_radioButton_first.AutoSize = true;
            this.CM_radioButton_first.Checked = true;
            this.CM_radioButton_first.Location = new System.Drawing.Point(6, 20);
            this.CM_radioButton_first.Name = "CM_radioButton_first";
            this.CM_radioButton_first.Size = new System.Drawing.Size(71, 16);
            this.CM_radioButton_first.TabIndex = 0;
            this.CM_radioButton_first.TabStop = true;
            this.CM_radioButton_first.Text = "首次检定";
            this.CM_radioButton_first.UseVisualStyleBackColor = true;
            // 
            // CM_button_detail
            // 
            this.CM_button_detail.Location = new System.Drawing.Point(206, 146);
            this.CM_button_detail.Name = "CM_button_detail";
            this.CM_button_detail.Size = new System.Drawing.Size(75, 23);
            this.CM_button_detail.TabIndex = 44;
            this.CM_button_detail.Text = "详细信息";
            this.CM_button_detail.UseVisualStyleBackColor = true;
            this.CM_button_detail.Click += new System.EventHandler(this.CM_button_detail_Click);
            // 
            // CM_button_submit
            // 
            this.CM_button_submit.Location = new System.Drawing.Point(369, 146);
            this.CM_button_submit.Name = "CM_button_submit";
            this.CM_button_submit.Size = new System.Drawing.Size(75, 23);
            this.CM_button_submit.TabIndex = 45;
            this.CM_button_submit.Text = "提交";
            this.CM_button_submit.UseVisualStyleBackColor = true;
            this.CM_button_submit.Click += new System.EventHandler(this.CM_button_submit_Click);
            // 
            // label_Brand
            // 
            this.label_Brand.AutoSize = true;
            this.label_Brand.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_Brand.Location = new System.Drawing.Point(1, 134);
            this.label_Brand.Name = "label_Brand";
            this.label_Brand.Size = new System.Drawing.Size(157, 14);
            this.label_Brand.TabIndex = 36;
            this.label_Brand.Text = "太原山互科技有限公司";
            // 
            // CM_tabControl_detail
            // 
            this.CM_tabControl_detail.Controls.Add(this.CM_tabPage_customer);
            this.CM_tabControl_detail.Controls.Add(this.CM_tabPage_manu);
            this.CM_tabControl_detail.Controls.Add(this.CM_tabPage_sensor);
            this.CM_tabControl_detail.Location = new System.Drawing.Point(8, 194);
            this.CM_tabControl_detail.Name = "CM_tabControl_detail";
            this.CM_tabControl_detail.SelectedIndex = 0;
            this.CM_tabControl_detail.Size = new System.Drawing.Size(478, 315);
            this.CM_tabControl_detail.TabIndex = 46;
            this.CM_tabControl_detail.SelectedIndexChanged += new System.EventHandler(this.CM_tabControl_detail_SelectedIndexChanged);
            // 
            // CM_tabPage_customer
            // 
            this.CM_tabPage_customer.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CM_tabPage_customer.Controls.Add(this.CM_listView_client);
            this.CM_tabPage_customer.Location = new System.Drawing.Point(4, 22);
            this.CM_tabPage_customer.Name = "CM_tabPage_customer";
            this.CM_tabPage_customer.Padding = new System.Windows.Forms.Padding(3);
            this.CM_tabPage_customer.Size = new System.Drawing.Size(470, 289);
            this.CM_tabPage_customer.TabIndex = 0;
            this.CM_tabPage_customer.Text = "客户信息";
            // 
            // CM_listView_client
            // 
            this.CM_listView_client.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.A_clientID,
            this.A_clientName,
            this.A_clientAddress,
            this.A_clientPhone});
            this.CM_listView_client.ContextMenuStrip = this.CM_mouseRight_client;
            this.CM_listView_client.Cursor = System.Windows.Forms.Cursors.Default;
            this.CM_listView_client.FullRowSelect = true;
            this.CM_listView_client.GridLines = true;
            this.CM_listView_client.Location = new System.Drawing.Point(20, 8);
            this.CM_listView_client.Name = "CM_listView_client";
            this.CM_listView_client.Size = new System.Drawing.Size(440, 270);
            this.CM_listView_client.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.CM_listView_client.TabIndex = 0;
            this.CM_listView_client.UseCompatibleStateImageBehavior = false;
            this.CM_listView_client.View = System.Windows.Forms.View.Details;
            this.CM_listView_client.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CM_listView_customer_MouseClick);
            // 
            // A_clientID
            // 
            this.A_clientID.Text = "客户序号";
            this.A_clientID.Width = 0;
            // 
            // A_clientName
            // 
            this.A_clientName.Text = "客户名称";
            this.A_clientName.Width = 180;
            // 
            // A_clientAddress
            // 
            this.A_clientAddress.Text = "客户地址";
            this.A_clientAddress.Width = 150;
            // 
            // A_clientPhone
            // 
            this.A_clientPhone.Text = "客户联系方式";
            this.A_clientPhone.Width = 84;
            // 
            // CM_mouseRight_client
            // 
            this.CM_mouseRight_client.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewClient_button,
            this.UpdateClient_button,
            this.DeleteClient_button});
            this.CM_mouseRight_client.Name = "CM_contextMenuStrip_manage";
            this.CM_mouseRight_client.Size = new System.Drawing.Size(125, 70);
            // 
            // NewClient_button
            // 
            this.NewClient_button.Name = "NewClient_button";
            this.NewClient_button.Size = new System.Drawing.Size(124, 22);
            this.NewClient_button.Text = "新建客户";
            this.NewClient_button.Click += new System.EventHandler(this.NewClient_button_Click);
            // 
            // UpdateClient_button
            // 
            this.UpdateClient_button.Name = "UpdateClient_button";
            this.UpdateClient_button.Size = new System.Drawing.Size(124, 22);
            this.UpdateClient_button.Text = "更新客户";
            this.UpdateClient_button.Click += new System.EventHandler(this.UpdateClient_button_Click);
            // 
            // DeleteClient_button
            // 
            this.DeleteClient_button.Name = "DeleteClient_button";
            this.DeleteClient_button.Size = new System.Drawing.Size(124, 22);
            this.DeleteClient_button.Text = "删除客户";
            this.DeleteClient_button.Click += new System.EventHandler(this.DeleteClient_button_Click);
            // 
            // CM_tabPage_manu
            // 
            this.CM_tabPage_manu.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.CM_tabPage_manu.Controls.Add(this.CM_listView_manu);
            this.CM_tabPage_manu.Location = new System.Drawing.Point(4, 22);
            this.CM_tabPage_manu.Name = "CM_tabPage_manu";
            this.CM_tabPage_manu.Padding = new System.Windows.Forms.Padding(3);
            this.CM_tabPage_manu.Size = new System.Drawing.Size(470, 289);
            this.CM_tabPage_manu.TabIndex = 1;
            this.CM_tabPage_manu.Text = "制造商信息";
            // 
            // CM_listView_manu
            // 
            this.CM_listView_manu.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.A_manuID,
            this.A_manuName,
            this.A_manuPhone});
            this.CM_listView_manu.ContextMenuStrip = this.CM_mouseRight_manu;
            this.CM_listView_manu.FullRowSelect = true;
            this.CM_listView_manu.GridLines = true;
            this.CM_listView_manu.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CM_listView_manu.Location = new System.Drawing.Point(20, 8);
            this.CM_listView_manu.Name = "CM_listView_manu";
            this.CM_listView_manu.Size = new System.Drawing.Size(432, 270);
            this.CM_listView_manu.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.CM_listView_manu.TabIndex = 0;
            this.CM_listView_manu.UseCompatibleStateImageBehavior = false;
            this.CM_listView_manu.View = System.Windows.Forms.View.Details;
            this.CM_listView_manu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CM_listView_manu_MouseClick);
            // 
            // A_manuID
            // 
            this.A_manuID.Text = "制造厂序号";
            this.A_manuID.Width = 0;
            // 
            // A_manuName
            // 
            this.A_manuName.Text = "制造厂名称";
            this.A_manuName.Width = 130;
            // 
            // A_manuPhone
            // 
            this.A_manuPhone.Text = "制造厂联系方式";
            this.A_manuPhone.Width = 184;
            // 
            // CM_mouseRight_manu
            // 
            this.CM_mouseRight_manu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewManu_button,
            this.UpdateManu_button,
            this.DeleteManu_button});
            this.CM_mouseRight_manu.Name = "CM_mouseRight_manu";
            this.CM_mouseRight_manu.Size = new System.Drawing.Size(137, 70);
            // 
            // NewManu_button
            // 
            this.NewManu_button.Name = "NewManu_button";
            this.NewManu_button.Size = new System.Drawing.Size(136, 22);
            this.NewManu_button.Text = "新建制造商";
            this.NewManu_button.Click += new System.EventHandler(this.NewManu_button_Click);
            // 
            // UpdateManu_button
            // 
            this.UpdateManu_button.Name = "UpdateManu_button";
            this.UpdateManu_button.Size = new System.Drawing.Size(136, 22);
            this.UpdateManu_button.Text = "更新制造商";
            this.UpdateManu_button.Click += new System.EventHandler(this.UpdateManu_button_Click);
            // 
            // DeleteManu_button
            // 
            this.DeleteManu_button.Name = "DeleteManu_button";
            this.DeleteManu_button.Size = new System.Drawing.Size(136, 22);
            this.DeleteManu_button.Text = "删除制造商";
            this.DeleteManu_button.Click += new System.EventHandler(this.DeleteManu_button_Click);
            // 
            // CM_tabPage_sensor
            // 
            this.CM_tabPage_sensor.BackColor = System.Drawing.SystemColors.Control;
            this.CM_tabPage_sensor.Controls.Add(this.CM_listView_product);
            this.CM_tabPage_sensor.Location = new System.Drawing.Point(4, 22);
            this.CM_tabPage_sensor.Name = "CM_tabPage_sensor";
            this.CM_tabPage_sensor.Padding = new System.Windows.Forms.Padding(3);
            this.CM_tabPage_sensor.Size = new System.Drawing.Size(470, 289);
            this.CM_tabPage_sensor.TabIndex = 2;
            this.CM_tabPage_sensor.Text = "被检设备信息";
            // 
            // CM_listView_product
            // 
            this.CM_listView_product.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.A_productType,
            this.A_productSNID,
            this.A_productClientID,
            this.A_productManuID,
            this.A_productDutID,
            this.A_productClassID});
            this.CM_listView_product.ContextMenuStrip = this.CM_mouseRight_product;
            this.CM_listView_product.FullRowSelect = true;
            this.CM_listView_product.GridLines = true;
            this.CM_listView_product.Location = new System.Drawing.Point(20, 8);
            this.CM_listView_product.Name = "CM_listView_product";
            this.CM_listView_product.Size = new System.Drawing.Size(430, 270);
            this.CM_listView_product.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.CM_listView_product.TabIndex = 0;
            this.CM_listView_product.UseCompatibleStateImageBehavior = false;
            this.CM_listView_product.View = System.Windows.Forms.View.Details;
            this.CM_listView_product.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CM_listView_product_MouseClick);
            // 
            // A_productType
            // 
            this.A_productType.Text = "产品型号";
            this.A_productType.Width = 100;
            // 
            // A_productSNID
            // 
            this.A_productSNID.Text = "产品出厂编号";
            this.A_productSNID.Width = 100;
            // 
            // A_productClientID
            // 
            this.A_productClientID.Text = "产品提供商名称";
            this.A_productClientID.Width = 120;
            // 
            // A_productManuID
            // 
            this.A_productManuID.Text = "产品制造商名称";
            this.A_productManuID.Width = 120;
            // 
            // A_productDutID
            // 
            this.A_productDutID.Text = "器具名称";
            this.A_productDutID.Width = 140;
            // 
            // A_productClassID
            // 
            this.A_productClassID.Text = "产品准确度等级";
            this.A_productClassID.Width = 96;
            // 
            // CM_mouseRight_product
            // 
            this.CM_mouseRight_product.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewProduct_button,
            this.UpdateProduct_button,
            this.DeleteProduct_button});
            this.CM_mouseRight_product.Name = "CM_mouseRight_product";
            this.CM_mouseRight_product.Size = new System.Drawing.Size(125, 70);
            // 
            // NewProduct_button
            // 
            this.NewProduct_button.Name = "NewProduct_button";
            this.NewProduct_button.Size = new System.Drawing.Size(124, 22);
            this.NewProduct_button.Text = "新建设备";
            this.NewProduct_button.Click += new System.EventHandler(this.NewProduct_button_Click);
            // 
            // UpdateProduct_button
            // 
            this.UpdateProduct_button.Name = "UpdateProduct_button";
            this.UpdateProduct_button.Size = new System.Drawing.Size(124, 22);
            this.UpdateProduct_button.Text = "更新设备";
            this.UpdateProduct_button.Click += new System.EventHandler(this.UpdateProduct_button_Click);
            // 
            // DeleteProduct_button
            // 
            this.DeleteProduct_button.Name = "DeleteProduct_button";
            this.DeleteProduct_button.Size = new System.Drawing.Size(124, 22);
            this.DeleteProduct_button.Text = "删除设备";
            this.DeleteProduct_button.Click += new System.EventHandler(this.DeleteProduct_button_Click);
            // 
            // CM_groupBox_test
            // 
            this.CM_groupBox_test.Controls.Add(this.CM_radioButton_normal);
            this.CM_groupBox_test.Controls.Add(this.CM_radioButton_formal);
            this.CM_groupBox_test.Location = new System.Drawing.Point(331, 77);
            this.CM_groupBox_test.Name = "CM_groupBox_test";
            this.CM_groupBox_test.Size = new System.Drawing.Size(155, 48);
            this.CM_groupBox_test.TabIndex = 44;
            this.CM_groupBox_test.TabStop = false;
            this.CM_groupBox_test.Text = "测试";
            // 
            // CM_radioButton_normal
            // 
            this.CM_radioButton_normal.AutoSize = true;
            this.CM_radioButton_normal.Location = new System.Drawing.Point(79, 20);
            this.CM_radioButton_normal.Name = "CM_radioButton_normal";
            this.CM_radioButton_normal.Size = new System.Drawing.Size(71, 16);
            this.CM_radioButton_normal.TabIndex = 1;
            this.CM_radioButton_normal.Text = "普通测试";
            this.CM_radioButton_normal.UseVisualStyleBackColor = true;
            // 
            // CM_radioButton_formal
            // 
            this.CM_radioButton_formal.AutoSize = true;
            this.CM_radioButton_formal.Checked = true;
            this.CM_radioButton_formal.Location = new System.Drawing.Point(6, 20);
            this.CM_radioButton_formal.Name = "CM_radioButton_formal";
            this.CM_radioButton_formal.Size = new System.Drawing.Size(71, 16);
            this.CM_radioButton_formal.TabIndex = 0;
            this.CM_radioButton_formal.TabStop = true;
            this.CM_radioButton_formal.Text = "正式测试";
            this.CM_radioButton_formal.UseVisualStyleBackColor = true;
            // 
            // pictureBox_Brand
            // 
            this.pictureBox_Brand.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox_Brand.BackgroundImage")));
            this.pictureBox_Brand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox_Brand.Location = new System.Drawing.Point(30, 36);
            this.pictureBox_Brand.Name = "pictureBox_Brand";
            this.pictureBox_Brand.Size = new System.Drawing.Size(94, 92);
            this.pictureBox_Brand.TabIndex = 35;
            this.pictureBox_Brand.TabStop = false;
            // 
            // CustomerMangementPageTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 521);
            this.Controls.Add(this.CM_groupBox_test);
            this.Controls.Add(this.CM_tabControl_detail);
            this.Controls.Add(this.CM_button_submit);
            this.Controls.Add(this.CM_button_detail);
            this.Controls.Add(this.CM_groupBox_verification);
            this.Controls.Add(this.CM_label_pronum);
            this.Controls.Add(this.CM_comboBox_productSNID);
            this.Controls.Add(this.CM_label_customer);
            this.Controls.Add(this.CM_comboBox_clientName);
            this.Controls.Add(this.label_Brand);
            this.Controls.Add(this.pictureBox_Brand);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CustomerMangementPageTest";
            this.Text = "资产信息管理";
            this.Load += new System.EventHandler(this.CustomerMangementPageTest_Load);
            this.CM_groupBox_verification.ResumeLayout(false);
            this.CM_groupBox_verification.PerformLayout();
            this.CM_tabControl_detail.ResumeLayout(false);
            this.CM_tabPage_customer.ResumeLayout(false);
            this.CM_mouseRight_client.ResumeLayout(false);
            this.CM_tabPage_manu.ResumeLayout(false);
            this.CM_mouseRight_manu.ResumeLayout(false);
            this.CM_tabPage_sensor.ResumeLayout(false);
            this.CM_mouseRight_product.ResumeLayout(false);
            this.CM_groupBox_test.ResumeLayout(false);
            this.CM_groupBox_test.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Brand)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox CM_comboBox_clientName;
        private System.Windows.Forms.Label CM_label_customer;
        private System.Windows.Forms.Label CM_label_pronum;
        private System.Windows.Forms.ComboBox CM_comboBox_productSNID;
        private System.Windows.Forms.GroupBox CM_groupBox_verification;
        private System.Windows.Forms.RadioButton CM_radioButton_first;
        private System.Windows.Forms.RadioButton CM_radioButton_nofirst;
        private System.Windows.Forms.Button CM_button_detail;
        private System.Windows.Forms.Button CM_button_submit;
        private System.Windows.Forms.PictureBox pictureBox_Brand;
        private System.Windows.Forms.Label label_Brand;
        private System.Windows.Forms.TabControl CM_tabControl_detail;
        private System.Windows.Forms.TabPage CM_tabPage_customer;
        private System.Windows.Forms.ListView CM_listView_client;
        private System.Windows.Forms.ColumnHeader A_clientName;
        private System.Windows.Forms.ColumnHeader A_clientAddress;
        private System.Windows.Forms.ColumnHeader A_clientPhone;
        private System.Windows.Forms.TabPage CM_tabPage_manu;
        private System.Windows.Forms.TabPage CM_tabPage_sensor;
        private System.Windows.Forms.ContextMenuStrip CM_mouseRight_client;
        private System.Windows.Forms.ToolStripMenuItem NewClient_button;
        private System.Windows.Forms.ToolStripMenuItem UpdateClient_button;
        private System.Windows.Forms.ToolStripMenuItem DeleteClient_button;
        private System.Windows.Forms.ListView CM_listView_manu;
        private System.Windows.Forms.ColumnHeader A_manuID;
        private System.Windows.Forms.ColumnHeader A_manuName;
        private System.Windows.Forms.ColumnHeader A_manuPhone;
        private System.Windows.Forms.ListView CM_listView_product;
        private System.Windows.Forms.ColumnHeader A_productType;
        private System.Windows.Forms.ColumnHeader A_productSNID;
        private System.Windows.Forms.ColumnHeader A_productClientID;
        private System.Windows.Forms.ColumnHeader A_productManuID;
        private System.Windows.Forms.ColumnHeader A_productDutID;
        private System.Windows.Forms.ColumnHeader A_productClassID;
        private System.Windows.Forms.GroupBox CM_groupBox_test;
        private System.Windows.Forms.RadioButton CM_radioButton_normal;
        private System.Windows.Forms.RadioButton CM_radioButton_formal;
        private System.Windows.Forms.ContextMenuStrip CM_mouseRight_manu;
        private System.Windows.Forms.ToolStripMenuItem NewManu_button;
        private System.Windows.Forms.ToolStripMenuItem UpdateManu_button;
        private System.Windows.Forms.ToolStripMenuItem DeleteManu_button;
        private System.Windows.Forms.ContextMenuStrip CM_mouseRight_product;
        private System.Windows.Forms.ToolStripMenuItem NewProduct_button;
        private System.Windows.Forms.ToolStripMenuItem UpdateProduct_button;
        private System.Windows.Forms.ToolStripMenuItem DeleteProduct_button;
        private System.Windows.Forms.ColumnHeader A_clientID;
    }
}