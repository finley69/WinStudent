using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WinStudent
{
    class SqlHelper
    {
        //private string connString = "server =DESKTOP-4RV0QLQ;database = StudentNewDB;uid=sa;pwd=flzxsqc668.;";//Sql Server身份验证 
        public static readonly string connString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        public static object ExecuteScalar(string sql,params SqlParameter[] paras)
        {
            //建立与数据库的连接
            object o = null; 
            using (SqlConnection conn = new SqlConnection(connString))
            {
                //创建Command对象
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.CommandType = CommandType.StoredProcedure;//存储过程
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(paras);
                //打开连接
                conn.Open();
                //执行命令 要求必须在连接状态
                o = cmd.ExecuteScalar();
                //关闭连接
                //conn.Close();
            }
            return o;
        }
        /// <summary>
        /// 执行查询，返回SqlDataReader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql,params SqlParameter[] paras)
        {
            SqlConnection conn = new SqlConnection(connString);
            try
            { 
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(paras);
                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                 return dr;

            }
            catch (Exception ex)
            {
                throw new Exception("执行查询出现异常",ex);
            }
           
        }

        /// <summary>
        /// 返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql,params SqlParameter[] paras)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                //创建Command对象
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.CommandType = CommandType.StoredProcedure;//存储过程
                if(paras!=null)
                {
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(paras);
                }
                //打开连接
                 conn.Open();
                //断开式连接
                //执行命令 一定是command来完成的
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                //SqlDataAdapter da = new SqlDataAdapter();
                //数据填充
                da.Fill(dt);
                //关闭连接
                //conn.Close();
            }
            return dt;
        }
        /// <summary>
        /// 返回受影响的行数 Insert Update Delete
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paras"></param>
        /// <returns></returns>
        public static int ExcuteNonQuery(string sql,params SqlParameter[] paras)
        {
            //建立与数据库的连接
            int count = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                //创建Command对象
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.CommandType = CommandType.StoredProcedure;//存储过程
                cmd.Parameters.Clear();
                cmd.Parameters.AddRange(paras);
                //打开连接
                conn.Open();
                //执行命令 要求必须在连接状态
                count = cmd.ExecuteNonQuery();//返回受影响的行数
                //关闭连接
                //conn.Close();
            }
            return count;
        }
    }
}
