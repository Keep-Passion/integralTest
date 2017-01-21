using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Arm_tyshkj_design
{
    public partial class ChangeProduct : Form
    {
        public int Control;
        public string ProductSNID;

        public ChangeProduct()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 新增用重载
        /// </summary>
        /// <param name="Control1"></param>
        public ChangeProduct(int Control1)
        {
            Control = Control1;
            InitializeComponent();
        }

        /// <summary>
        /// 添加用重载
        /// </summary>
        /// <param name="Control1"></param>
        /// <param name="prosnid"></param>
        public ChangeProduct(int Control1,string prosnid)
        {
            Control = Control1;
            ProductSNID = prosnid;
            InitializeComponent();
        }

        /// <summary>
        /// 窗体基本信息加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeProduct_Load(object sender, EventArgs e)
        {
            if (Control == 0)
            {
                CP_button_change.Text = "添加";
                CP_comboBoxInit();
            }
            else
            {
                //修改时不允许修改产品出厂编号吗？？？
                //CP_textBox_productSNID.ReadOnly = true;
                CP_button_change.Text = "更新";
                CP_comboBoxInit();
                string sql = "select * from E_product where A_productSNID='" + ProductSNID + "'"; 
                DataTable table_product = CP_Select_Access(sql);
                CP_textBox_productType.Text = table_product.Rows[0].ItemArray[0].ToString();
                CP_textBox_productSNID.Text = table_product.Rows[0].ItemArray[1].ToString();
                CP_comboBox_client.SelectedValue = table_product.Rows[0].ItemArray[2];
                CP_comboBox_manu.SelectedValue = table_product.Rows[0].ItemArray[3];
                CP_comboBox_dut.SelectedValue = table_product.Rows[0].ItemArray[4];
                CP_comboBox_class.SelectedValue = table_product.Rows[0].ItemArray[5];
            }
        }

        /// <summary>
        /// 产品信息修改页面comboBox信息与数据库绑定功能
        /// </summary>
        private void CP_comboBoxInit()
        {
            //DisplayMember为显示值，使用名称
            //ValueMember为事务处理值，使用序号
            string sql_client = "select * from E_client order by A_clientID asc";
            DataTable table_client = CP_Select_Access(sql_client);
            CP_comboBox_client.DataSource = table_client;
            CP_comboBox_client.DisplayMember = "A_clientName";
            CP_comboBox_client.ValueMember = "A_clientID";

            string sql_manu = "select * from E_manu order by A_manuID asc";
            DataTable table_manu = CP_Select_Access(sql_manu);
            CP_comboBox_manu.DataSource = table_manu;
            CP_comboBox_manu.DisplayMember = "A_manuName";
            CP_comboBox_manu.ValueMember = "A_manuID";

            string sql_dut = "select * from E_dut order by A_dutID asc";
            DataTable table_dut = CP_Select_Access(sql_dut);
            CP_comboBox_dut.DataSource = table_dut;
            CP_comboBox_dut.DisplayMember = "A_dutName";
            CP_comboBox_dut.ValueMember = "A_dutID";

            string sql_class = "select * from E_class order by A_classID asc";
            DataTable table_class = CP_Select_Access(sql_class);
            CP_comboBox_class.DataSource = table_class;
            CP_comboBox_class.DisplayMember = "A_className";
            CP_comboBox_class.ValueMember = "A_classID";
        }

        /// <summary>
        /// 数据库查询功能
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private DataTable CP_Select_Access(string sql)
        {
            OleDbDataAdapter adapter;
            DataTable table = new DataTable();
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
                throw (new Exception("数据库出错" + exc.Message));
            }
        }

        /// <summary>
        /// 数据库控制功能
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private bool CP_Control_Access(string sql)
        {
            try
            {
                OleDbConnection conn = DBProvider.getConn();
                conn.Open();
                OleDbCommand sqlcmd = new OleDbCommand(sql, conn);
                sqlcmd.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception exc)
            {
                return false;
                throw (new Exception("数据库出错" + exc.Message));
            }
        }

        /// <summary>
        /// 修改按钮功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CP_button_change_Click(object sender, EventArgs e)
        {
            //判断输入是否合法部分
            if (!Validator.checkRequired(this.CP_textBox_productType.Text.ToString()))
            {
                MessageBox.Show("产品型号不能为空\n", "错误提示");
                return;
            }
            if (!Validator.checkRequired(this.CP_textBox_productSNID.Text.ToString()))
            {
                MessageBox.Show("产品出厂编号不能为空\n", "错误提示");
                return;
            }
            if (CP_comboBox_client.SelectedValue == null)
            {
                MessageBox.Show("产品客户名称未选择\n", "错误提示");
                return;
            }
            if (CP_comboBox_manu.SelectedValue == null)
            {
                MessageBox.Show("产品制造商名称未选择\n", "错误提示");
                return;
            }
            if (CP_comboBox_dut.SelectedValue == null)
            {
                MessageBox.Show("产品器具名称未选择\n", "错误提示");
                return;
            }
            if (CP_comboBox_class.SelectedValue == null)
            {
                MessageBox.Show("产品准确度未选择\n", "错误提示");
                return;
            }
            //产品添加时不需要产生序号，以出厂编号为唯一主键
            if (Control == 0)
            {
                string sql_insert = "insert into E_Product(A_productType,A_productSNID,A_productClientID,A_productManuID,A_productDutID,A_productClassID) values('" 
                    + CP_textBox_productType.Text.ToString() + "','" + CP_textBox_productSNID.Text.ToString() + "','" + CP_comboBox_client.SelectedValue.ToString() + "','" 
                    + CP_comboBox_manu.SelectedValue.ToString() + "','" + CP_comboBox_dut.SelectedValue.ToString() + "','" + CP_comboBox_class.SelectedValue.ToString() + "')";

                if (CP_Control_Access(sql_insert) == false)
                {
                    MessageBox.Show("数据库出错", "错误提示");
                    return;
                }
                else
                {
                    MessageBox.Show("添加成功", "提示");
                    CP_textBox_productSNID.Text = "";
                    CP_textBox_productType.Text = "";
                    CP_comboBox_client.SelectedItem = null;
                    CP_comboBox_manu.SelectedItem = null;
                    CP_comboBox_dut.SelectedItem = null;
                    CP_comboBox_class.SelectedItem = null;
                }
            }
            //修改直接更新数据库
            else
            {
                string sql_update = "update E_product set A_productType='" + CP_textBox_productType.Text.ToString() + "',A_productSNID='" + CP_textBox_productSNID.Text.ToString() 
                    + "',A_productClientID='" + CP_comboBox_client.SelectedValue.ToString() + "',A_productManuID='" + CP_comboBox_manu.SelectedValue.ToString() 
                    + "',A_productDutID='" + CP_comboBox_dut.SelectedValue.ToString() + "',A_productClassID='" + CP_comboBox_class.SelectedValue.ToString() 
                    + "' where A_productSNID='" + ProductSNID + "'";

                if (CP_Control_Access(sql_update) == false)
                {
                    MessageBox.Show("数据库出错", "错误提示");
                    return;
                }
                else
                {
                    MessageBox.Show("更新成功", "提示");
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 清空按钮功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CP_button_clear_Click(object sender, EventArgs e)
        {
            CP_textBox_productSNID.Text = "";
            CP_textBox_productType.Text = "";
            CP_comboBox_client.SelectedItem = null;
            CP_comboBox_manu.SelectedItem = null;
            CP_comboBox_dut.SelectedItem = null;
            CP_comboBox_class.SelectedItem = null;
        }
    }
}
