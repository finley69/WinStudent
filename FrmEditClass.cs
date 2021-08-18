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
    public partial class FrmEditClass : Form
    {
        public FrmEditClass()
        {
            InitializeComponent();
        }
        private int classId = 0;
        private string oldName = "";
        private int oldGradeId = 0;
        private Action reLoad = null;//刷新列表页所用
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 打开页面，加载年级列表，加载班级信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmEditClass_Load(object sender, EventArgs e)
        {
            InitGradeList();
            InitClassInfo();
        }
        /// <summary>
        /// 加载年级列表
        /// </summary>
        private void InitGradeList()
        {
            string sql = "select GradeId,GradeName from GradeInfo";
            DataTable dtGradeList = SqlHelper.GetDataTable(sql);

            cboGrades.DataSource = dtGradeList;
            //年级名称
            cboGrades.DisplayMember = "GradeName";//显示的内容
            cboGrades.ValueMember = "GradeId";//值

            cboGrades.SelectedIndex = 0;//默认选择第一个
        }
        /// <summary>
        /// 初始化班级信息
        /// </summary>
        private void InitClassInfo()
        {
            //获取到StuId
            if (this.Tag != null)
            {
                TagObject tagObject = (TagObject)this.Tag;
                classId = tagObject.EditId;
                reLoad = tagObject.Reload;//赋值
            }
            //查询出来
            string sql = "select ClassName,GradeId,Remark from ClassInfo where ClassId=@ClassId";
            SqlParameter paraId = new SqlParameter("@ClassId", classId);
            SqlDataReader dr = SqlHelper.ExecuteReader(sql, paraId);
            //读取数据 只向前，不能后退，读一条，丢一条
            if (dr.Read())
            {
                txtcClassName.Text = dr["ClassName"].ToString();
                oldName = txtcClassName.Text.Trim();
                txtRemark.Text = dr["Remark"].ToString();
                int gradeId = (int)dr["GradeId"];
                oldGradeId = gradeId;
                cboGrades.SelectedValue = gradeId;
            }
            dr.Close();
        }
        /// <summary>
        /// 提交修改信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //获取页面输入
            string className = txtcClassName.Text.Trim();
            int gradeId = (int)cboGrades.SelectedValue;
            string remark = txtRemark.Text.Trim();
            //判断是否为空
            if (string.IsNullOrEmpty(className))
            {
                MessageBox.Show("班级名称不能为空！", "修改班级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //判断是否已存在，在同一年级下，班级名称不能同名
            string sqlExists = "select count(1) from ClassInfo where ClassName=@ClassName and GradeId=@GradeId";
            if(className==oldName&&gradeId == oldGradeId)
            {
                sqlExists += " and ClassId<>@ClassId";
            }
            SqlParameter[] paras =
            {
                    new SqlParameter("@ClassName",className),
                    new SqlParameter("GradeId",gradeId),
                    new SqlParameter("@ClassId",classId)
                };
            object oCount = SqlHelper.ExecuteScalar(sqlExists, paras);
            if (oCount != null ||((int)oCount)>0)
            {

                MessageBox.Show("班级名称已存在！", "添加班级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //修改提交
            string sqlEdit = "update ClassInfo set ClassName=@ClassName,GradeId=@GradeId,Remark=@Remark where ClassId=@ClassId ";
            SqlParameter[] parasEdit =
            {
                        new SqlParameter("@ClassName",className),
                        new SqlParameter("GradeId",gradeId),
                        new SqlParameter("@Remark",remark),
                        new SqlParameter("@ClassId",classId)
                    };
            //执行并返回值
            int count = SqlHelper.ExcuteNonQuery(sqlEdit, parasEdit);
            if (count > 0)
            {
                MessageBox.Show($"班级：{className} 修改成功！", "修改班级提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //刷新列表页面数据
                reLoad();
            }
            else
            {
                MessageBox.Show("班级修改失败！", "修改班级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
