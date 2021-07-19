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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 新增学生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subAddStudent_Click(object sender, EventArgs e)
        {
            FrmAddStudent fAddStudent = new FrmAddStudent();
            fAddStudent.MdiParent = this;
            fAddStudent.Show();//顶级窗体 不能显示到MDI容器中
        }
        /// <summary>
        /// 学生列表 不可以同时打开多个页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subStudentList_Click(object sender, EventArgs e)
        {
            bool b1 = CheckForm(typeof(FrmStudentList).Name);  
            if (!b1)
            {
                FrmStudentList fStudentList = new FrmStudentList();
                fStudentList.MdiParent = this;
                fStudentList.Show();//顶级窗体 不能显示到MDI容器中
            }
            
        }
        /// <summary>
        /// 检查窗体是否已经打开
        /// </summary>
        /// <param name="formName"></param>
        /// <returns></returns>
        private bool CheckForm(string formName)
        {
            bool b1 = false;
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == "FrmStudentList")
                {
                    b1 = true;
                    f.Activate();
                    break;
                }
            }
            return b1;
        }
        /// <summary>
        /// 新增班级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subAddClass_Click(object sender, EventArgs e)
        {
            FrmAddClass fStudentClass = new FrmAddClass();
            fStudentClass.MdiParent = this;
            fStudentClass.Show();
        }
        /// <summary>
        /// 班级列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subClassList_Click(object sender, EventArgs e)
        {
            bool b1 = CheckForm(typeof(FrmClassList).Name);
            if (!b1)
            {
                FrmClassList fClassList = new FrmClassList();
                fClassList.MdiParent = this;
                fClassList.Show();//顶级窗体 不能显示到MDI容器中
            }
        }
        /// <summary>
        /// 年级列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void subGradeList_Click(object sender, EventArgs e)
        {
            bool b1 = CheckForm(typeof(FrmGradeList).Name);
            if (!b1)
            {
                FrmGradeList fGradeList = new FrmGradeList();
                fGradeList.MdiParent = this;
                fGradeList.Show();//顶级窗体 不能显示到MDI容器中
            }
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miExit_Click(object sender, EventArgs e)
        {
            Application.Exit();//退出应用程序
        }
        /// <summary>
        /// 窗体关闭，退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("你确定要退出系统吗？", "退出提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.ExitThread();//退出当前线程上的消息循环
            }
            else
            {
                e.Cancel = true;//手动取消
            }
        }
    }
}
