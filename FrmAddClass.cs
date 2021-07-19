using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinStudent
{
    public partial class FrmAddClass : Form
    {
        public FrmAddClass()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FrmAddClass_Load(object sender, EventArgs e)
        {
            InitGradeList();//加载年级列表
        }
        /// <summary>
        /// 一个班级，必须属于某个年级
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
        /// 添加班级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //信息获取
            string className = txtcClassName.Text.Trim();
            int gradeId = (int)cboGrades.SelectedValue;
            string remark = txtRemark.Text.Trim();
            //判断是否为空
            if (string.IsNullOrEmpty(className))
            {
                MessageBox.Show("班级名称不能为空！", "添加班级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //判断是否存在 数据库里去检查与数据库进行交互
        }
    }
}
