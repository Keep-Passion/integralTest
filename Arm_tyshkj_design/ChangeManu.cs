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
    public partial class ChangeManu : Form
    {
        public int Control;
        public string ManuID;
        public ChangeManu()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// 添加用重载
        /// </summary>
        /// <param name="Control1">控制变量</param>
        public ChangeManu(int Control1)
        {
            Control = Control1;
            InitializeComponent();

        }

        /// <summary>
        /// 更新用重载
        /// </summary>
        /// <param name="Control1">控制变量</param>
        /// <param name="mid">需要更新的条目主键</param>
        public ChangeManu(int Control1,string mid)
        {
            Control = Control1;
            ManuID = mid;
            InitializeComponent();
        }

        /// <summary>
        /// 窗体基本信息加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeManu_Load(object sender, EventArgs e)
        {
            if (Control == 0)
            {
                CMN_button_change.Text = "添加";
            }
            else
            {
                CMN_button_change.Text = "更新";
                string sql = "select * from E_manu where A_manuID=" + ManuID;
                DataTable table = CMN_Select_Access(sql);
                CMN_textBox_name.Text = table.Rows[0].ItemArray[1].ToString();
                CMN_textBox_phone.Text = table.Rows[0].ItemArray[2].ToString();
            }
        }

        /// <summary>
        /// 数据库查询功能
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private DataTable CMN_Select_Access(string sql)
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
        private bool CMN_Control_Access(string sql)
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
        private void CMN_button_change_Click(object sender, EventArgs e)
        {
            //判断输入是否合法部分
            if (!Validator.checkRequired(this.CMN_textBox_name.Text.ToString()))
            {
                MessageBox.Show("客户名不能为空\n", "错误提示");
                return;
            }
            if (!Validator.checkRequired(this.CMN_textBox_phone.Text.ToString()))
            {
                MessageBox.Show("电话号码不能为空\n", "错误提示");
                return;
            }

            //新建时生成主键序号
            int newmanuID = -1;
            string sql = "select A_manuID from E_manu order by A_manuID asc";
            DataTable IDData = CMN_Select_Access(sql);
            for (int i = 0, j = 1; i < IDData.Rows.Count; i++, j++)
            {
                if (j != Convert.ToInt32(IDData.Rows[i].ItemArray[0].ToString()))
                {
                    newmanuID = j;
                    break;
                }
                else if (j == IDData.Rows.Count)
                    newmanuID = j + 1;

            }
            //将输入信息添加到数据库
            if (Control == 0)
            {
                string sql_insert = "insert into E_manu(A_manuID,A_manuName,A_manuPhone) values('" + newmanuID + "','" + CMN_textBox_name.Text.ToString() + "','" + CMN_textBox_phone.Text.ToString() + "')";
                if (CMN_Control_Access(sql_insert) == false)
                {
                    MessageBox.Show("数据库出错123?"+newmanuID, "错误提示");
                    return;
                }
                else
                {
                    MessageBox.Show("添加成功", "提示");
                    CMN_textBox_name.Text = "";
                    CMN_textBox_phone.Text = "";
                }
            }
            //修改直接更新数据库
            else
            {
                string sql_update = "update E_manu set A_manuName='" + CMN_textBox_name.Text.ToString() + "',A_manuPhone='" + CMN_textBox_phone.Text.ToString() + "' where A_manuID=" + ManuID;
                if (CMN_Control_Access(sql_update) == false)
                {
                    MessageBox.Show("数据库出错", "错误提示");
                    return;
                }
                else
                {
                    MessageBox.Show("更新成功", "提示");
                    CMN_textBox_name.Text = "";
                    CMN_textBox_phone.Text = "";
                    this.Close();
                }
            }
        }

        /// <summary>
        /// 清空按钮功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CMN_button_clear_Click(object sender, EventArgs e)
        {
            CMN_textBox_name.Text = "";
            CMN_textBox_phone.Text = "";
        }
    }
}
