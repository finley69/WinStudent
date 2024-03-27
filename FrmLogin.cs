using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WinStudent
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            //vs2022modified
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //获取用户输入信息
            string uName = txtUserName.Text.Trim();
            string uPwd = txtUserPwd.Text.Trim();
            //判断是否为空
            if (string.IsNullOrEmpty(uName))
            {
                MessageBox.Show("请输入账号！", "登入提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtUserName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(uPwd))
            {
                MessageBox.Show("请输入密码！", "登入提示", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                txtUserPwd.Focus();
                return;
            }

            string sql = "select count(1) from UserInfo where UserName=@UserName and UserPwd=@UserPwd";
            SqlParameter[] paras =
               {
                    new SqlParameter("@UserName", uName),
                    new SqlParameter("@UserPwd", uPwd)
                };

            /*
            //与数据库通信 检查输入与输入与数据库是否一致
            
                //建立与数据库的连接
                //string connString = "server =.;database = StudentDB;Integrated Security = true";//Windows身份验证
                //string connString = "Data Source=DESKTOP-4RV0QLQ (sa);Initial Catalog = shaohui; Integrated Security = True";
                string connString = "server =DESKTOP-4RV0QLQ;database = StudentNewDB;uid=sa;pwd=flzxsqc668.;";//Sql Server身份验证 
                SqlConnection conn = new SqlConnection(connString);
                //写查询语句 拼接式Sql注入 推荐大家使用参数化Sql
                //string sql = "select count(1) from UserInfo where UserName='"+ uName + "'and UserPwd='" + uPwd + "'";
                //参数化Sql
                string sql = "select count(1) from UserInfo where UserName=@UserName and UserPwd=@UserPwd";
                //添加参数
                //SqlParameter paraUName = new SqlParameter("@UserName", uName);
                //SqlParameter paraUPwd = new SqlParameter("@UserPwd", uPwd);
                SqlParameter[] paras =
                {
                    new SqlParameter("@UserName", uName),
                    new SqlParameter("@UserPwd", uPwd)
                };
                //创建Command对象
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.CommandType = CommandType.StoredProcedure;//存储过程
                cmd.Parameters.Clear();
                //cmd.Parameters.Add(paraUName);
                //cmd.Parameters.Add(paraUPwd);
                cmd.Parameters.AddRange(paras);
                //打开连接
                conn.Open();
                //执行命令 要求必须在连接状态
                object o = cmd.ExecuteScalar();
                //关闭连接
                conn.Close();
                //处理结果

            */
            //调用
            object o = SqlHelper.ExecuteScalar(sql, paras);
            if (o == null || o == DBNull.Value || ((int)o) == 0)
            {
                MessageBox.Show("登入账号或密码有错，请检查！", "登入提示", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("登入成功！", "登入提示", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
                //转到主页面
                FrmMain fMain = new FrmMain();
                fMain.Show();
                this.Hide();//隐藏当前页面
            }

        


            //返回的结果进行不同的提示
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit();
        }
    }
}
