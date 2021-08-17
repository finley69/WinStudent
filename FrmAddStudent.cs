using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinStudent
{
    public partial class FrmAddStudent : Form
    {
        public FrmAddStudent()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载班级列表\性别选择男
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAddStudent_Load(object sender, EventArgs e)
        {
            InitClasses();//
            rbtMale.Checked = true;
        }

        private void InitClasses()
        {
            //获取数据------------查询-------------写sql
            string sql = "select ClassId,ClassName,GradeName from ClassInfo c, GradeInfo g where c.GradeId=g.GradeId";

            DataTable dtClasses = SqlHelper.GetDataTable(sql);
            //组合班级列表显示项
            if (dtClasses.Rows.Count > 0)
            {
                foreach (DataRow dr in dtClasses.Rows)
                {
                    string className = dr["ClassName"].ToString();
                    string gradeName = dr["GradeName"].ToString();
                    dr["ClassName"] = gradeName + "--" + className;
                }
            }
            //指定数据源
            cboClasses.DataSource = dtClasses;
            cboClasses.DisplayMember = "ClassName";
            cboClasses.ValueMember = "ClassId";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //获取页面信息输入
            string stuName = txtStuName.Text.Trim();
            int classId = (int)cboClasses.SelectedValue;
            string sex = rbtMale.Checked ? rbtMale.Text.Trim() : rbtFemale.Text.Trim(); ;
            string phone = txtPhone.Text.Trim();
            //判空处理 姓名不可以为空 电话不可以为空
            if (string.IsNullOrEmpty(stuName))
            {
                MessageBox.Show("姓名不能为空！", "添加学生提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("电话不能为空！", "添加学生提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //判断 姓名+电话 是否在数据库里已存在 姓名+电话
            string sql = "select count(1) from StudentInfo where StuName=@StuName and Phone=@Phone";
            SqlParameter[] paras =
            {
                new SqlParameter("@StuName",stuName),
                new SqlParameter("@Phone",phone)
            };
            object o = SqlHelper.ExecuteScalar(sql, paras);
            if (o != null && o != DBNull.Value && ((int)o) > 0)
            {
                MessageBox.Show("该学生已存在！", "添加学生提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //添加入库 sql 参数 执行 完成返回受影响行数
            string sqlAdd = "insert into StudentInfo (StuName,ClassId,Sex,Phone) values (@StuName,@ClassId,@Sex,@Phone)";
            SqlParameter[] parasAdd =
           {
                new SqlParameter("@StuName",stuName),
                new SqlParameter("@ClassId",classId),
                new SqlParameter("@Sex",sex),
                new SqlParameter("@Phone",phone)
            };
            int count = SqlHelper.ExcuteNonQuery(sqlAdd, parasAdd);
            if (count > 0)
            {
                MessageBox.Show($"学生:{stuName} 添加成功！", "添加学生提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("该学生添加失败，请检查！", "添加学生提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
