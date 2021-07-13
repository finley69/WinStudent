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
    public partial class FrmStudentList : Form
    {
        public FrmStudentList()
        {
            InitializeComponent();
        }
        ////单例 只有一个实例
        //private static FrmStudentList frmStudentList = null;
        //public static FrmStudentList CreatInstance()
        //{
        //    if (frmStudentList == null || frmStudentList.IsDisposed)
        //        frmStudentList = new FrmStudentList();
        //    return frmStudentList;

        //}
    }
}
