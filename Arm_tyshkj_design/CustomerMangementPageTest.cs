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
    public partial class CustomerMangementPageTest : Form
    {
        private int flag = 1;   //用于窗体初始化时的ComboBox的数据初始化标记

        public CustomerMangementPageTest()
        {
            InitializeComponent();
            CM_ShowData();
        }

        /// <summary>
        /// 详略按钮与窗体大小控制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CM_button_detail_Click(object sender, EventArgs e)
        {
            //简略信息窗体高度为230，详细信息窗体高度为560，通过点击详略按钮控制窗体大小
            if (this.Height == 230)
            {
                this.Height = this.Height + 330;
                CM_button_detail.Text = "简略信息";
                CM_tabControl_detail.Visible = true;
            }
            else if (this.Height == 560)
            {
                this.Height = this.Height - 330;
                CM_button_detail.Text = "详细信息";
                CM_tabControl_detail.Visible = false;
            }
        }

        /// <summary>
        /// 打开窗体时，窗体为显示简略信息状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerMangementPageTest_Load(object sender, EventArgs e)
        {
            this.Height = 230;
            CM_tabControl_detail.Visible = false;
            if (this.Height == 560)
            {
                CM_button_detail.Text = "简略信息";
                CM_tabControl_detail.Visible = true;
            }
        }

        #region 数据库功能

        /// <summary>
        /// 数据库控制功能
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>语句是否成功执行</returns>
        private bool CM_Control_Access(string sql)
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
        /// 数据库查询功能
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>查询得到的table</returns>
        private DataTable CM_Select_Access(string sql)
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
        #endregion

        #region 主体界面功能和信息展示

        /// <summary>
        /// 界面信息显示
        /// </summary>
        private void CM_ShowData()
        {
            //简略信息界面
            //2个comboBox显示控制
            string sql_client = "select * from E_client order by A_clientID asc";
            DataTable table_client = CM_Select_Access(sql_client);
            if (table_client.Rows.Count == 0) return;
            CM_comboBox_clientName.DataSource = table_client;
            CM_comboBox_clientName.DisplayMember = "A_clientName";
            CM_comboBox_clientName.ValueMember = "A_clientID";

            string sql_product = "select * from E_product where A_productClientID=" + CM_comboBox_clientName.SelectedValue;
            DataTable table_product = CM_Select_Access(sql_product);
            CM_comboBox_productSNID.DataSource = table_product;
            CM_comboBox_productSNID.DisplayMember = "A_productSNID";
            CM_comboBox_productSNID.ValueMember = "A_productSNID";

            //详细信息界面
            //客户listView数据显示控制
            SetListViewData(table_client, CM_listView_client);
        }

        /// <summary>
        /// 产品出厂序号下拉菜单与客户下拉菜单绑定功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CM_comboBox_clientName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //第一次运行时首先加载客户ComboBox的数据，不然在窗体初始化时会报错
            //使用了变量flag，如果有更好的设计方法可修改
            if (flag == 1)
            {
                string sql_client = "select * from E_client order by A_clientID asc";
                DataTable table_client = CM_Select_Access(sql_client);
                if (table_client.Rows.Count == 0) return;
                CM_comboBox_clientName.DataSource = table_client;
                CM_comboBox_clientName.DisplayMember = "A_clientName";
                CM_comboBox_clientName.ValueMember = "A_clientID";
                flag = 0;
            }
            //绑定客户下拉菜单与产品下拉菜单
            if (CM_comboBox_clientName.SelectedValue != null)
            {
                string sql_product = "select * from E_product where A_productClientID=" + CM_comboBox_clientName.SelectedValue;
                DataTable table_product = CM_Select_Access(sql_product);
                CM_comboBox_productSNID.DataSource = table_product;
                CM_comboBox_productSNID.DisplayMember = "A_productSNID";
                CM_comboBox_productSNID.ValueMember = "A_productSNID";
            }

        }

        /// <summary>
        /// 切换不同Tab时ListView数据初始化功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CM_tabControl_detail_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.CM_tabControl_detail.SelectedIndex)
            {
                case 0:
                    string sql_client = "select * from E_client order by A_clientID asc";
                    DataTable table_client = CM_Select_Access(sql_client);
                    if (table_client.Rows.Count == 0) return;
                    SetListViewData(table_client, CM_listView_client);
                    break;
                case 1:
                    string sql_manu = "select * from E_manu";
                    DataTable table_manu = CM_Select_Access(sql_manu);
                    SetListViewData(table_manu, CM_listView_manu);
                    break;
                case 2:
                    string sql_product = "select * from E_product";
                    DataTable table_product = CM_Select_Access(sql_product);
                    SetListProductViewData(table_product);
                    break;
            }
        }

        /// <summary>
        /// 客户listView和制造商listView与数据库连接更新
        /// </summary>
        /// <param name="table">查询得到的数据库中的表</param>
        /// <param name="list">要更新的listView</param>
        private void SetListViewData(DataTable table, ListView list)
        {
            list.Items.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = table.Rows[i].ItemArray[0].ToString();
                for (int j = 1; j < table.Columns.Count; j++)
                {
                    lvi.SubItems.Add(table.Rows[i].ItemArray[j].ToString());
                }
                list.Items.Add(lvi);

            }

        }

        /// <summary>
        /// 产品详细信息listView与数据库连接更新
        /// </summary>
        /// <param name="table">查询得到的数据库中的表</param>
        private void SetListProductViewData(DataTable table)
        {
            //产品的详细信息关联数据库中另外四个表
            DataTable table_client, table_manu, table_dut, table_class;
            CM_listView_product.Items.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                ListViewItem lvi = new ListViewItem();
                //填充产品型号和产品出厂编号
                lvi.Text = table.Rows[i].ItemArray[0].ToString();
                lvi.SubItems.Add(table.Rows[i].ItemArray[1].ToString());

                //查询四个表分别根据ID得到名称并填充
                string sql_client = "select A_clientName from E_client where A_clientID=" + table.Rows[i].ItemArray[2];
                string sql_manu = "select A_manuName from E_manu where A_manuID=" + table.Rows[i].ItemArray[3];
                string sql_dut = "select A_dutName from E_dut where A_dutID=" + table.Rows[i].ItemArray[4];
                string sql_class = "select A_className from E_class where A_classID=" + table.Rows[i].ItemArray[5];

                table_client = CM_Select_Access(sql_client);
                table_manu = CM_Select_Access(sql_manu);
                table_dut = CM_Select_Access(sql_dut);
                table_class = CM_Select_Access(sql_class);

                lvi.SubItems.Add(table_client.Rows[0].ItemArray[0].ToString());
                lvi.SubItems.Add(table_manu.Rows[0].ItemArray[0].ToString());
                lvi.SubItems.Add(table_dut.Rows[0].ItemArray[0].ToString());
                lvi.SubItems.Add(table_class.Rows[0].ItemArray[0].ToString());

                CM_listView_product.Items.Add(lvi);
            }


        }

        /// <summary>
        /// 提交信息后事务处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CM_button_submit_Click(object sender, EventArgs e)
        {
            if (CM_comboBox_productSNID.SelectedValue == null)
            {
                MessageBox.Show("未选择产品编号，无法提交", "错误提示");
                return;
            }

            if(CM_comboBox_clientName.SelectedValue == null)
            {
                MessageBox.Show("未选择客户名称，无法提交", "错误提示");
                return;
            }
        
            this.Hide();
            bool isfirst = false;

            //新建时生成主键序号
            int newtestsetID = -1;
            string now = DateTime.Now.ToString("yyyy/MM/dd");
            string sql = "select A_testSetID from E_testSet order by A_testSetID asc";
            DataTable IDData = CM_Select_Access(sql);

            if (IDData.Rows.Count == 0)
            {
                newtestsetID = 1;
            }

            for (int i = 0, j = 1; i < IDData.Rows.Count; i++, j++)
            {
                if (j != Convert.ToInt32(IDData.Rows[i].ItemArray[0].ToString()))
                {
                    newtestsetID = j;
                    break;
                }
                else if (j == IDData.Rows.Count)
                    newtestsetID = j + 1;

            }


            if (CM_radioButton_first.Checked)
            {
                isfirst = true;
                //取得简略信息窗体上两个ComboBox所选值的方法：
                //CM_comboBox_clientName.SelectedValue;
                //CM_comboBox_productSNID.SelectedValue;
            }
            if (CM_radioButton_formal.Checked)
            {
                sql = "insert into E_testSet(A_testSetID,A_testSetTime,A_testProductSNID,A_isFirstTest,A_testProductPolar,A_productProfile) values ('" + newtestsetID + "','" + now + "','" + CM_comboBox_productSNID.SelectedValue.ToString() + "'," + isfirst + ",'" + "一致" + "','" + "合格" + "')";
                if (CM_Control_Access(sql) == false)
                {
                    MessageBox.Show("数据库出错", "错误提示");
                    return;
                }
                else
                {
                    //MessageBox.Show("本组测试已成功添加至数据库，TestSetID为：" + newtestsetID, "提示");
                    InstrDetectPage IDP = new InstrDetectPage(newtestsetID, 1);
                    IDP.ShowDialog();
                    this.Close();
                }
            }
            else
            {
                InstrDetectPage IDP = new InstrDetectPage(newtestsetID, 0);
                IDP.ShowDialog();
                this.Close();
            }
        }
        #endregion

        #region 客户管理菜单按钮功能控制

        /// <summary>
        /// 客户listView获取当前选择客户功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CM_listView_customer_MouseClick(object sender, MouseEventArgs e)
        {
            CM_listView_client.MultiSelect = false;

            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                CM_mouseRight_client.Show(CM_listView_client, p);
                
            }
        }

        /// <summary>
        /// 客户listView右键增加客户功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewClient_button_Click(object sender, EventArgs e)
        {
            ChangeClient CC = new ChangeClient(0);
            CC.ShowDialog();
            string sql = "select * from E_client";
            DataTable table = CM_Select_Access(sql);
            SetListViewData(table, CM_listView_client);

            string sql_client = "select * from E_client order by A_clientID asc";
            DataTable table_client = CM_Select_Access(sql_client);
            if (table_client.Rows.Count == 0) return;
            CM_comboBox_clientName.DataSource = table_client;
            CM_comboBox_clientName.DisplayMember = "A_clientName";
            CM_comboBox_clientName.ValueMember = "A_clientID";


        }

        /// <summary>
        /// 客户listView右键更新客户功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateClient_button_Click(object sender, EventArgs e)
        {
            if (CM_listView_client.SelectedItems.Count == 0)
            {
                MessageBox.Show("未选中任何项，无法更新", "错误提示");
                return;
            }
            else
            {
                string clientid = CM_listView_client.SelectedItems[0].SubItems[0].Text;
                ChangeClient CC = new ChangeClient(1, clientid);
                CC.ShowDialog();
                string sql = "select * from E_client";
                DataTable table = CM_Select_Access(sql);
                SetListViewData(table, CM_listView_client);

                string sql_client = "select * from E_client order by A_clientID asc";
                DataTable table_client = CM_Select_Access(sql_client);
                if (table_client.Rows.Count == 0) return;
                CM_comboBox_clientName.DataSource = table_client;
                CM_comboBox_clientName.DisplayMember = "A_clientName";
                CM_comboBox_clientName.ValueMember = "A_clientID";

            }
        }

        /// <summary>
        /// 客户listView右键删除客户功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteClient_button_Click(object sender, EventArgs e)
        {
            if (CM_listView_client.SelectedItems.Count == 0)
            {
                MessageBox.Show("未选中任何项，无法删除", "错误提示");
                return;
            }
            string local_customer = CM_listView_client.SelectedItems[0].SubItems[0].Text;
            string sql_cus = "delete from E_client where A_clientID=" + local_customer;
            DialogResult result = MessageBox.Show("确定要删除该客户吗？", "确认", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //删除客户时判断该客户下是否有设备，若存在设备不能删除
                string sql = "select * from E_product where A_productClientID=" + local_customer;
                DataTable table_product = CM_Select_Access(sql);
                if (table_product.Rows.Count != 0)
                {
                    MessageBox.Show("该客户名下还有设备，请先删除设备才能删除客户", "错误提示");
                    return;
                }
                else if (CM_Control_Access(sql_cus) == false)
                {
                    MessageBox.Show("数据库出错", "错误提示");
                    return;
                }
                else
                {
                    MessageBox.Show("删除客户成功", "提示");
                    sql_cus = "select * from E_client";
                    DataTable table_customer = CM_Select_Access(sql_cus);
                    SetListViewData(table_customer, CM_listView_client);

                    string sql_client = "select * from E_client order by A_clientID asc";
                    DataTable table_client = CM_Select_Access(sql_client);
                    if (table_client.Rows.Count == 0) return;
                    CM_comboBox_clientName.DataSource = table_client;
                    CM_comboBox_clientName.DisplayMember = "A_clientName";
                    CM_comboBox_clientName.ValueMember = "A_clientID";
                }
            }
        }

        #endregion

        #region 制造商管理菜单按钮功能控制
        
        /// <summary>
        /// 制造商listView获取当前选择制造商功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CM_listView_manu_MouseClick(object sender, MouseEventArgs e)
        {
            CM_listView_manu.MultiSelect = false;

            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                CM_mouseRight_manu.Show(CM_listView_manu, p);
            }
        }

        /// <summary>
        /// 制造商listView右键新增制造商功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewManu_button_Click(object sender, EventArgs e)
        {
            ChangeManu CMN = new ChangeManu(0);
            CMN.ShowDialog();
            string sql = "select * from E_manu";
            DataTable table = CM_Select_Access(sql);
            SetListViewData(table, CM_listView_manu);
        }

        /// <summary>
        /// 制造商listView右键更新制造商功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateManu_button_Click(object sender, EventArgs e)
        {
            if (CM_listView_manu.SelectedItems.Count == 0)
            {
                MessageBox.Show("未选中任何项，无法更新", "错误提示");
                return;
            }
            string manuid = CM_listView_manu.SelectedItems[0].SubItems[0].Text;
            ChangeManu CMN = new ChangeManu(1, manuid);
            CMN.ShowDialog();
            string sql = "select * from E_manu";
            DataTable table = CM_Select_Access(sql);
            SetListViewData(table, CM_listView_manu);
        }

        /// <summary>
        /// 制造商listView右键删除制造商功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteManu_button_Click(object sender, EventArgs e)
        {
            if (CM_listView_manu.SelectedItems.Count == 0)
            {
                MessageBox.Show("未选中任何项，无法删除", "错误提示");
                return;
            }
            string local_manu = CM_listView_manu.SelectedItems[0].SubItems[0].Text;
            string sql_man = "delete from E_manu where A_manuID=" + local_manu;
            DialogResult result = MessageBox.Show("确定要删除该制造商吗？", "确认", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                //删除制造商时首先判定其下有无设备，若有设备，不能删除
                string sql = "select * from E_product where A_productManuID=" + local_manu;
                DataTable table_product = CM_Select_Access(sql);
                if (table_product.Rows.Count != 0)
                {
                    MessageBox.Show("该制造商名下还有设备，请先删除设备才能删除制造商", "错误提示");
                    return;
                }
                else if (CM_Control_Access(sql_man) == false)
                {
                    MessageBox.Show("数据库出错", "错误提示");
                    return;
                }
                else
                {
                    MessageBox.Show("删除制造商成功", "提示");
                    sql_man = "select * from E_manu";
                    DataTable table_manu = CM_Select_Access(sql_man);
                    SetListViewData(table_manu, CM_listView_manu);
                }
            }
        }

        #endregion

        #region 产品管理菜单按钮功能控制

        /// <summary>
        /// 产品listView获取当前选择产品功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CM_listView_product_MouseClick(object sender, MouseEventArgs e)
        {
            CM_listView_product.MultiSelect = false;

            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y);
                CM_mouseRight_product.Show(CM_listView_product, p);

            }
        }

        /// <summary>
        /// 产品listView右键新增产品功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewProduct_button_Click(object sender, EventArgs e)
        {
            ChangeProduct CP = new ChangeProduct(0);
            CP.ShowDialog();
            string sql = "select * from E_product";
            DataTable table = CM_Select_Access(sql);
            SetListProductViewData(table);

            string sql_product = "select * from E_product where A_productClientID=" + CM_comboBox_clientName.SelectedValue;
            DataTable table_product = CM_Select_Access(sql_product);
            CM_comboBox_productSNID.DataSource = table_product;
            CM_comboBox_productSNID.DisplayMember = "A_productSNID";
            CM_comboBox_productSNID.ValueMember = "A_productSNID";
        }

        /// <summary>
        /// 产品listView右键更新产品功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateProduct_button_Click(object sender, EventArgs e)
        {
            if (CM_listView_product.SelectedItems.Count == 0)
            {
                MessageBox.Show("未选中任何项，无法更新", "错误提示");
                return;
            }
            string prosnid = CM_listView_product.SelectedItems[0].SubItems[1].Text;
            ChangeProduct CP = new ChangeProduct(1, prosnid);
            CP.ShowDialog();
            string sql = "select * from E_product";
            DataTable table = CM_Select_Access(sql);
            SetListProductViewData(table);

            string sql_product = "select * from E_product where A_productClientID=" + CM_comboBox_clientName.SelectedValue;
            DataTable table_product = CM_Select_Access(sql_product);
            CM_comboBox_productSNID.DataSource = table_product;
            CM_comboBox_productSNID.DisplayMember = "A_productSNID";
            CM_comboBox_productSNID.ValueMember = "A_productSNID";
        }

        /// <summary>
        /// 产品listView右键删除产品功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteProduct_button_Click(object sender, EventArgs e)
        {
            if (CM_listView_product.SelectedItems.Count == 0)
            {
                MessageBox.Show("未选中任何项，无法删除", "错误提示");
                return;
            }
            string local_pro = CM_listView_product.SelectedItems[0].SubItems[1].Text;
            //设备可直接删除
            string sql_pro = "delete from E_product where A_productSNID='" + local_pro + "'";
            DialogResult result = MessageBox.Show("确定要删除该设备吗？", "确认", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (CM_Control_Access(sql_pro) == false)
                {
                    MessageBox.Show("数据库出错", "错误提示");
                    return;
                }
                else
                {
                    MessageBox.Show("删除设备成功", "提示");
                    sql_pro = "select * from E_product";
                    DataTable table_product = CM_Select_Access(sql_pro);
                    SetListProductViewData(table_product);

                    string sql_product = "select * from E_product where A_productClientID=" + CM_comboBox_clientName.SelectedValue;
                    table_product = CM_Select_Access(sql_product);
                    CM_comboBox_productSNID.DataSource = table_product;
                    CM_comboBox_productSNID.DisplayMember = "A_productSNID";
                    CM_comboBox_productSNID.ValueMember = "A_productSNID";
                }
            }
        }

        #endregion

    }
}
