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
    public partial class FrmEditStudent : Form
    {
        public FrmEditStudent()
        {
            InitializeComponent();
        }
        private Action reLoad = null;
        private int stuId = 0;
        //public FrmEditStudent(int _stuId)
        //{
        //    InitializeComponent();
        //    stuId = _stuId;
        //}

        private void FrmEditStudent_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(stuId.ToString());
            InitClasses();//加载班级列表下拉框
            //加载学生信息
            InitStuInfo();
        }

        private void InitStuInfo()
        {
           //获取到StuId
           if(this.Tag!=null)
            {
                TagObject tagObject = (TagObject)this.Tag;
                stuId = tagObject.EditId;
                reLoad = tagObject.Reload;//赋值
            }
            //查询出来
            string sql = "select StuName,Sex,ClassId,Phone from StudentInfo where StuId=@StuId";
            SqlParameter paraId = new SqlParameter("@StuId", stuId);
            SqlDataReader dr = SqlHelper.ExecuteReader(sql, paraId);
            //读取数据 只向前，不能后退，读一条，丢一条
            if (dr.Read())
            {
                txtStuName.Text = dr["StuName"].ToString();
                txtPhone.Text = dr["Phone"].ToString();
                string sex = dr["Sex"].ToString();
                if (sex=="男")
                {
                    rbtMale.Checked = true;
                }
                else
                {
                    rbtFemale.Checked = true;
                }
                int classId = (int)dr["ClassId"];
                cboClasses.SelectedValue = classId;
            }
            dr.Close();
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //获取页面信息输入
            string stuName = txtStuName.Text.Trim();
            int classId = (int)cboClasses.SelectedValue;
            string sex = rbtMale.Checked ? rbtMale.Text.Trim() : rbtFemale.Text.Trim(); 
            string phone = txtPhone.Text.Trim();
            //判空处理 姓名不可以为空 电话不可以为空
            if (string.IsNullOrEmpty(stuName))
            {
                MessageBox.Show("姓名不能为空！", "修改学生提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("电话不能为空！", "修改学生提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //判断是否存在 姓名+电话 除了这个学生自己，其他学生中是否已存在
            string sql = "select count(1) from StudentInfo where StuName=@StuName and Phone=@Phone and StuId<>@StuId ";
            SqlParameter[] paras =
            {
                new SqlParameter("@StuName",stuName),
                new SqlParameter("@Phone",phone),
                 new SqlParameter("@StuId",stuId)
            };
            object o = SqlHelper.ExecuteScalar(sql, paras);
            if (o != null && o != DBNull.Value && ((int)o) > 0)
            {
                MessageBox.Show("该学生已存在！", "添加学生提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //修改
            string sqlUpdate = "update StudentInfo set StuName=@StuName,ClassId=@ClassId,Sex=@Sex,Phone=@Phone where StuId=@StuId";
            SqlParameter[] parasUpdate =
           {
                new SqlParameter("@StuName",stuName),
                new SqlParameter("@ClassId",classId),
                new SqlParameter("@Sex",sex),
                new SqlParameter("@Phone",phone),
                new SqlParameter("@StuId",stuId)

            };
            int count = SqlHelper.ExcuteNonQuery(sqlUpdate, parasUpdate);
            if (count > 0)
            {
                MessageBox.Show($"学生:{stuName} 修改成功！", "修改学生提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //提示成功后，想刷新学生列表 跨页面刷新 委托 列表页面定义委托，列表页面加载数据列表这个方法赋值给委托，同时传过去----修改页面； 修改页面，定义委托，把传过来的委托赋值给本页面定义的委托，修改成功后，调用委托。
                reLoad.Invoke();
                

            }
            else
            {
                MessageBox.Show("该学生修改失败，请检查！", "修改学生提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
