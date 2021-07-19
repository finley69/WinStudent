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
    public partial class FrmClassList : Form
    {
        public FrmClassList()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 查询班级信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //int gradeId = (int)cboGrades.SelectedValue;
            //string className = txtClassName.Text.Trim();
            ////
            //string sql = "select ClassId,ClassName,GradeName,Remark from ClassInfo c inner join GradeInfo g on c.ClassId = g.GradeId where 1=1";
            //if(gradeId>0)
            //{
            //    sql += " and c.GradeId=@GradeId";
            //}
            //if(!string.IsNullOrEmpty(className))
            //{
            //    sql += " and ClassName like @ClassName";
            //}
            //SqlParameter[] paras =
            //{
            //    new SqlParameter("@GradeId",gradeId),
            //    new SqlParameter("@ClassName","%"+className+"%")
            //};
            //DataTable dtClasses = SqlHelper.GetDataTable(sql, paras);
            //dgvClassList.DataSource = dtClasses;
        }
        /// <summary>
        /// 初始化年级列表、所有班级列表信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmClassList_Load(object sender, EventArgs e)
        {
            InitGrades();//加载年级列表
            InitAllClasses();//加载所有的班级信息
        }

        private void InitAllClasses()
        {
            //string sql = "select ClassId,ClassName,GradeName,Remark from ClassInfo c inner join GradeInfo g on c.ClassId = g.GradeId";
            //DataTable dtClasses = SqlHelper.GetDataTable(sql);
            //dgvClassList.DataSource = dtClasses;
        }

        private void InitGrades()
        {
            //string sql = "select GradeId,GradeName from GradeInfo";
            //DataTable dtGradeList = SqlHelper.GetDataTable(sql);
            ////添加一个请选择项
            ////添加一行
            
            //DataRow dr = dtGradeList.NewRow();
            //dr["GradeId"] = 0;
            //dr["GradeName"] = "请选择";
            ////dtGradeList.Rows.Add(dr);//添加到最后一个
            //dtGradeList.Rows.InsertAt(dr, 0);
            
            //cboGrades.DataSource = dtGradeList;
            ////年级名称
            //cboGrades.DisplayMember = "GradeName";//显示的内容
            //cboGrades.ValueMember = "GradeId";//值

            //cboGrades.SelectedIndex = 0;//默认选择第一个
        }
    }
}
