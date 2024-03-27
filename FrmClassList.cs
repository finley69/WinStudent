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
        private Action reLoad = null;
        public FrmClassList()
        {
            InitializeComponent();
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

        private void InitAllClasses()//加载所有班级列表
        {
            string sql = "select ClassId,ClassName,GradeName,Remark from ClassInfo c inner join GradeInfo g on c.ClassId = g.GradeId";
            DataTable dtClasses = SqlHelper.GetDataTable(sql);
            dgvClassList.DataSource = dtClasses;
        }

        private void InitGrades()
        {
            string sql = "select GradeId,GradeName from GradeInfo";
            DataTable dtGradeList = SqlHelper.GetDataTable(sql);
            //添加一个请选择项
            //添加一行

            DataRow dr = dtGradeList.NewRow();
            dr["GradeId"] = 0;
            dr["GradeName"] = "请选择";
            //dtGradeList.Rows.Add(dr);//添加到最后一个111112222
            dtGradeList.Rows.InsertAt(dr, 0);//添加到第一个

            cboGrades.DataSource = dtGradeList;
            //年级名称
            cboGrades.DisplayMember = "GradeName";//显示的内容
            cboGrades.ValueMember = "GradeId";//值

            cboGrades.SelectedIndex = 0;//默认选择第一个
        }
        /// <summary>
        /// 查询班级信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFind_Click(object sender, EventArgs e)
        {

            int gradeId = (int)cboGrades.SelectedValue;
            string className = txtClassName.Text.Trim();
            //
            string sql = "select ClassId,ClassName,GradeName,Remark from ClassInfo c inner join GradeInfo g on c.ClassId = g.GradeId where 1=1";
            if (gradeId > 0)
            {
                sql += " and c.GradeId=@GradeId";
            }
            if (!string.IsNullOrEmpty(className))
            {
                sql += " and ClassName like @ClassName";
            }
            SqlParameter[] paras =
            {
                new SqlParameter("@GradeId",gradeId),
                new SqlParameter("@ClassName","%"+className+"%")
            };
            DataTable dtClasses = SqlHelper.GetDataTable(sql, paras);
            dgvClassList.DataSource = dtClasses;

        }

        private void dgvClassList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                //获取选择的单元格并判断是修改还是删除
                DataGridViewCell cell = dgvClassList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                DataRow dr = (dgvClassList.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
                int classId = (int)dr["ClassId"];//班级编号

                if (cell is DataGridViewLinkCell && cell.FormattedValue.ToString() == "修改")
                {
                    //修改 打卡修改页面，并把编号传过去
                    reLoad = InitAllClasses;//赋值
                    FrmEditClass frmEdit = new FrmEditClass();
                    frmEdit.Tag = new TagObject()
                    {
                        EditId = classId,
                        Reload = reLoad
                    };
                    frmEdit.MdiParent = this.MdiParent;
                    frmEdit.Show();
                }
                else if (cell is DataGridViewLinkCell && cell.FormattedValue.ToString() == "删除")
                {
                    if (MessageBox.Show("您确定要删除该班级及其学生信息吗？", "删除班级提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //单条数据删除
                        //班级删除的过程 班级--学生 先删除学生--再删除班级
                        string delStudent = "delete from StudentInfo where ClassId=@ClassId";
                        //班级信息
                        string delClass = "delete from ClassInfo where ClassId=@ClassId";
                        //参数
                        SqlParameter[] para = {
                       new SqlParameter("@ClassId", classId)
                           };
                        List<CommandInfo> listComs = new List<CommandInfo>();
                        CommandInfo comStudent = new CommandInfo()
                        {
                            CommandText = delStudent,
                            IsProc = false,
                            Parameters = para
                        };
                        listComs.Add(comStudent);
                        CommandInfo comClass = new CommandInfo()
                        {
                            CommandText = delClass,
                            IsProc = false,
                            Parameters = para
                        };
                        listComs.Add(comClass);
                        //执行 事务
                        bool bl = SqlHelper.ExecuteTrans(listComs);
                        if (bl)
                        {
                            MessageBox.Show("该班级信息删除成功！", "删除班级提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //手动刷新
                            DataTable dtClass = (DataTable)dgvClassList.DataSource;
                            dtClass.Rows.Remove(dr);
                            dgvClassList.DataSource = dtClass;
                        }
                        else
                        {
                            MessageBox.Show("该班级信息删除失败！", "删除班级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }


                }
            }
        }
        /// <summary>
        /// 删除多条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //选择
            //获取要删除的数据StuId
            //判断选择的个数，=0 没有选择 提示用户选择要删除的数据 >0 继续
            //删除操作 事务 sql事务 代码里启动事务
            List<int> listIds = new List<int>();
            for (int i = 0; i < dgvClassList.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell cell = dgvClassList.Rows[i].Cells["colCheck"] as DataGridViewCheckBoxCell;
                bool chk = Convert.ToBoolean(cell.Value);
                if (chk)
                {
                    DataRow dr = (dgvClassList.Rows[i].DataBoundItem as DataRowView).Row;
                    int classId = (int)dr["ClassId"];
                    listIds.Add(classId);//添加到选择列表里
                }
            }
            //真删除
            if (listIds.Count == 0)
            {
                MessageBox.Show("请选择删除班级的信息！", "删除班级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (MessageBox.Show("您确定要删除该班级信息吗？", "删除班级提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {


                    string delStudent = "delete from StudentInfo where ClassId=@ClassId";
                    //班级信息
                    string delClass = "delete from ClassInfo where ClassId=@ClassId";
                    List<CommandInfo> listComs = new List<CommandInfo>();
                    foreach (int id in listIds)
                    {
                        //参数
                        SqlParameter[] para = {
                       new SqlParameter("@ClassId", id)
                           };
                       
                        CommandInfo comStudent = new CommandInfo()
                        {
                            CommandText = delStudent,
                            IsProc = false,
                            Parameters = para
                        };
                        listComs.Add(comStudent);
                        CommandInfo comClass = new CommandInfo()
                        {
                            CommandText = delClass,
                            IsProc = false,
                            Parameters = para
                        };
                        listComs.Add(comClass);
                    }

                    bool bl = SqlHelper.ExecuteTrans(listComs);
                    if (bl)
                    {
                        MessageBox.Show("该班级信息删除成功！", "删除班级提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //手动刷新
                        DataTable dtClass = (DataTable)dgvClassList.DataSource;
                        string idStr = string.Join(",", listIds);
                        DataRow[] rows = dtClass.Select("ClassId in (" + idStr + ")");
                        foreach (DataRow dr in rows)
                        {
                            dtClass.Rows.Remove(dr);
                        }
                        dgvClassList.DataSource = dtClass;
                    }
                    else
                    {
                        MessageBox.Show("该班级信息删除失败！", "删除班级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }


            }
        }
    }
}
