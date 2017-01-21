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
    public partial class ProductDetailPage : Form
    {
        private int TestSetID = -1;
        public ProductDetailPage()
        {
            InitializeComponent();
        }
        public ProductDetailPage(int testid)
        {
            InitializeComponent();
            TestSetID = testid;
        }

        private void ProductDetailPage_Load(object sender, EventArgs e)
        {

            if (TestSetID == 0)
            {
                label_typeval.Text = "普通测试";
                label_snidval.Text = "普通测试";
                label_clientval.Text = "普通测试";
                label_manuval.Text = "普通测试";
                label_dutval.Text = "普通测试";
                label_classval.Text = "普通测试";
            }
            else
            {

                string sql = "select A_testProductSNID from E_testSet where A_testSetID=" + TestSetID;
                DataTable table = PD_Select_Access(sql);
                string snid = table.Rows[0].ItemArray[0].ToString();

                string sql_pro = "select * from E_product where A_productSNID='" + snid + "'";
                DataTable table_pro = PD_Select_Access(sql_pro);

                //查询四个表分别根据ID得到名称并填充
                string sql_client = "select A_clientName from E_client where A_clientID=" + table_pro.Rows[0].ItemArray[2];
                string sql_manu = "select A_manuName from E_manu where A_manuID=" + table_pro.Rows[0].ItemArray[3];
                string sql_dut = "select A_dutName from E_dut where A_dutID=" + table_pro.Rows[0].ItemArray[4];
                string sql_class = "select A_className from E_class where A_classID=" + table_pro.Rows[0].ItemArray[5];

                DataTable table_client = PD_Select_Access(sql_client);
                DataTable table_manu = PD_Select_Access(sql_manu);
                DataTable table_dut = PD_Select_Access(sql_dut);
                DataTable table_class = PD_Select_Access(sql_class);

                label_typeval.Text = table_pro.Rows[0].ItemArray[0].ToString();
                label_snidval.Text = snid;
                label_clientval.Text = table_client.Rows[0].ItemArray[0].ToString();
                label_manuval.Text = table_manu.Rows[0].ItemArray[0].ToString();
                label_dutval.Text = table_dut.Rows[0].ItemArray[0].ToString();
                label_classval.Text = table_class.Rows[0].ItemArray[0].ToString();
            }

        }

        /// <summary>
        /// 数据库查询功能
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private DataTable PD_Select_Access(string sql)
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
        private bool PD_Control_Access(string sql)
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
    }
}
