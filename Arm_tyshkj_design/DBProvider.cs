using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace Arm_tyshkj_design
{
    class DBProvider
    {
        const string DATABASE = "tyshkj.mdb";

        /// <summary>
        /// 获取数据库路径
        /// </summary>
        /// <returns></returns>
        public static string getDatabase()
        {
            string fileName;
            fileName = System.AppDomain.CurrentDomain.BaseDirectory + DATABASE;
            return fileName;
        }

        /// <summary>
        /// 调用数据库前先连接数据库
        /// </summary>
        /// <returns></returns>
        public static OleDbConnection getConn()
        {
            String file = getDatabase();
            string connstr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + file;
            OleDbConnection tempconn = new OleDbConnection(connstr);
            return (tempconn);
        }
    }
}
