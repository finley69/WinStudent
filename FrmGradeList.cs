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
    public partial class FrmGradeList : Form
    {
        private int flag = 0;
        private int editGradeId = 0;
        private string oldName = "";
        public FrmGradeList()
        {
            InitializeComponent();
        }

        private void FrmGradeList_Load(object sender, EventArgs e)
        {
            txtGradeName.Text = "";
            flag = 0; //添加状态
            btnSubmit.Text = "添加";
            LoagGradeList();//加载年级列表
           
        }

        private void LoagGradeList()
        {
            string sql = "select GradeId,GradeName from GradeInfo";
            DataTable dtGradeList = SqlHelper.GetDataTable(sql);
            dgvGradeList.DataSource = dtGradeList;
        }
        /// <summary>
        /// 修改或删除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvGradeList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                //获取选择的单元格并判断是修改还是删除
                DataGridViewCell cell = dgvGradeList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                DataRow dr = (dgvGradeList.Rows[e.RowIndex].DataBoundItem as DataRowView).Row;
                editGradeId = (int)dr["GradeId"];//年级编号

                if (cell is DataGridViewLinkCell && cell.FormattedValue.ToString() == "修改")
                {
                    //修改 年级名称加载到上面的文本框
                    txtGradeName.Text = dr["GradeName"].ToString();
                    oldName = dr["GradeName"].ToString();
                    flag = 1;//改为修改状态
                    btnSubmit.Text = "修改";
                }
                else if (cell is DataGridViewLinkCell && cell.FormattedValue.ToString() == "删除")
                {
                    if (MessageBox.Show("您确定要删除该年级及相关的班级及其学生信息吗？", "删除年级提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //班级删除的过程 班级--学生 先删除学生--再删除班级
                        string delStudent = "delete from StudentInfo where ClassId in (select ClassId from ClassInfo where GradeId=@GradeId) ";
                        //班级信息
                        string delClass = "delete from ClassInfo where GradeId=@GradeId";
                        //年级信息
                        string delGrade = "delete from GradeInfo where GradeId=@GradeId";
                        //参数
                        SqlParameter[] para = {
                       new SqlParameter("@GradeId", editGradeId)
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
                        CommandInfo comGrade = new CommandInfo()
                        {
                            CommandText = delGrade,
                            IsProc = false,
                            Parameters = para
                        };
                        listComs.Add(comGrade);

                        //执行 事务
                        bool bl = SqlHelper.ExecuteTrans(listComs);
                        if (bl)
                        {
                            MessageBox.Show("该年级信息删除成功！", "删除年级提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //手动刷新
                            DataTable dtGradeList = (DataTable)dgvGradeList.DataSource;
                            dtGradeList.Rows.Remove(dr);
                            dgvGradeList.DataSource = dtGradeList;
                        }
                        else
                        {
                            MessageBox.Show("该年级信息删除失败！", "删除年级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                    }


                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            //接收输入 信息
            string gradeName = txtGradeName.Text.Trim();
            //判断是否为空
            if (string.IsNullOrEmpty(gradeName))
            {
                MessageBox.Show("年级名称不能为空！", "添加年级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //判读是否已存在
            //添加 整个年级表来查询 现在的年级名称并未入库
            //修改 除了当前的年级这条信息以外的信息来查询 加载的年级名称已在数据库存在
            if (flag==0)//添加
            {
                string sqlExist = "select count(1) from GradeInfo where GradeName=@GradeName";
                SqlParameter paraName = new SqlParameter("@GradeName", gradeName);
                object oCount = SqlHelper.ExecuteScalar(sqlExist, paraName);
                if (oCount!=null&&((int)oCount)>0)
                {
                    MessageBox.Show("年级名称已存在！", "添加年级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //添加入库,返回生成的年级编号
                string sqlAdd = "insert into GradeInfo (GradeName) values (@GradeName);select @@Identity";
                SqlParameter paraNameNew = new SqlParameter("@GradeName", gradeName);
                object oGradeId = SqlHelper.ExecuteScalar(sqlAdd, paraNameNew);
                if (oGradeId != null && int.Parse(oGradeId.ToString()) > 0)
                {
                    
                    MessageBox.Show($"年级：{gradeName}添加成功！", "添加年级提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //刷新
                    DataTable dtGradeList = (DataTable)dgvGradeList.DataSource;
                    //添加一行，把新添加的年级信息加到数据源中
                    DataRow dr = dtGradeList.NewRow();
                    dr["GradeId"] = int.Parse(oGradeId.ToString());
                    dr["GradeName"] = gradeName;
                    dtGradeList.Rows.Add(dr);
                    dgvGradeList.DataSource = dtGradeList;
                }
                else
                {
                    MessageBox.Show($"年级：{gradeName}添加失败！", "添加年级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (flag == 1)//修改
            {
                if (gradeName==oldName)
                {
                    MessageBox.Show("年级名称并未修改！", "修改年级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //判断是否已存在
                string sqlExist = "select count(1) from GradeInfo where GradeName=@GradeName and  GradeId<>@GradeId";
                SqlParameter[] paras =
                 {
                    new SqlParameter("@GradeName",gradeName),
                    new SqlParameter("@GradeId",GradeId)
                };
                object oCount = SqlHelper.ExecuteScalar(sqlExist, paras);
                if (oCount != null && ((int)oCount) > 0)
                {
                    MessageBox.Show("年级名称已存在！", "修改年级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                //修改入库
                string sqlUpdate = "update GradeInfo set GradeName=@GradeName where GradeId=@GradeId";
                SqlParameter paraNameNew = new SqlParameter("@GradeName", gradeName);
                int count = SqlHelper.ExcuteNonQuery(sqlUpdate, paras);
                if (count > 0)
                {

                    MessageBox.Show($"年级：{gradeName}修改成功！", "修改年级提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //刷新
                    DataTable dtGradeList = (DataTable)dgvGradeList.DataSource;
                    //添加一行，把新添加的年级信息加到数据源中
                    DataRow[] rows = dtGradeList.Select("GradeId=" + editGradeId);
                    DataRow dr = rows[0];
                    dr["GradeName"] = gradeName;
                    dgvGradeList.DataSource = dtGradeList;
                }
                else
                {
                    MessageBox.Show($"年级：{gradeName}修改失败！", "修改年级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        /// <summary>
        /// 不是年级信息提交，而是添加状态的设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (flag!=0)
            {
                flag = 0;
                btnSubmit.Text = "添加";
                txtGradeName.Text = "";
            }
        }
        /// <summary>
        /// 多条删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndelete_Click(object sender, EventArgs e)
        {
            List<int> listIds = new List<int>();
            for (int i = 0; i < dgvGradeList.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell cell = dgvGradeList.Rows[i].Cells["colCheck"] as DataGridViewCheckBoxCell;
                bool chk = Convert.ToBoolean(cell.Value);
                if (chk)
                {
                    DataRow dr = (dgvGradeList.Rows[i].DataBoundItem as DataRowView).Row;
                    int gradeId = (int)dr["GradeId"];
                    listIds.Add(gradeId);//添加到选择列表里
                }
            }
            //真删除
            if (listIds.Count == 0)
            {
                MessageBox.Show("请选择删除年级的信息！", "删除年级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                if (MessageBox.Show("您确定要删除该年级信息吗？", "删除年级提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {


                    //班级删除的过程 班级--学生 先删除学生--再删除班级
                    string delStudent = "delete from StudentInfo where ClassId in (select ClassId from ClassInfo where GradeId=@GradeId) ";
                    //班级信息
                    string delClass = "delete from ClassInfo where GradeId=@GradeId";
                    //年级信息
                    string delGrade = "delete from GradeInfo where GradeId=@GradeId";
                    List<CommandInfo> listComs = new List<CommandInfo>();
                    foreach (int id in listIds)
                    {
                        //参数
                        SqlParameter[] para = {
                       new SqlParameter("@GradeId", id)
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
                        CommandInfo comGrade = new CommandInfo()
                        {
                            CommandText = delGrade,
                            IsProc = false,
                            Parameters = para
                        };
                        listComs.Add(comGrade);
                    }

                    bool bl = SqlHelper.ExecuteTrans(listComs);
                    if (bl)
                    {
                        MessageBox.Show("该年级信息删除成功！", "删除年级提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //手动刷新
                        DataTable dtGrade = (DataTable)dgvGradeList.DataSource;
                        string idStr = string.Join(",", listIds);
                        DataRow[] rows = dtGrade.Select("GradeId in (" + idStr + ")");
                        foreach (DataRow dr in rows)
                        {
                            dtGrade.Rows.Remove(dr);
                        }
                        dgvGradeList.DataSource = dtGrade;
                    }
                    else
                    {
                        MessageBox.Show("该年级信息删除失败！", "删除年级提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }


            }
        }
    }
}
