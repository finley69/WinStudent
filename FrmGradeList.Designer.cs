
namespace WinStudent
{
    partial class FrmGradeList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvGradeList = new System.Windows.Forms.DataGridView();
            this.colCheck = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.GradeId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GradeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEdit = new System.Windows.Forms.DataGridViewLinkColumn();
            this.colDel = new System.Windows.Forms.DataGridViewLinkColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtGradeName = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGradeList)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvGradeList
            // 
            this.dgvGradeList.AllowUserToAddRows = false;
            this.dgvGradeList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvGradeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGradeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCheck,
            this.GradeId,
            this.GradeName,
            this.colEdit,
            this.colDel});
            this.dgvGradeList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvGradeList.Location = new System.Drawing.Point(0, 92);
            this.dgvGradeList.Name = "dgvGradeList";
            this.dgvGradeList.RowHeadersVisible = false;
            this.dgvGradeList.RowTemplate.Height = 23;
            this.dgvGradeList.Size = new System.Drawing.Size(543, 281);
            this.dgvGradeList.TabIndex = 0;
            this.dgvGradeList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGradeList_CellContentClick);
            // 
            // colCheck
            // 
            this.colCheck.HeaderText = "选择";
            this.colCheck.Name = "colCheck";
            this.colCheck.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colCheck.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // GradeId
            // 
            this.GradeId.DataPropertyName = "GradeId";
            this.GradeId.HeaderText = "年级编号";
            this.GradeId.Name = "GradeId";
            this.GradeId.ReadOnly = true;
            // 
            // GradeName
            // 
            this.GradeName.DataPropertyName = "GradeName";
            this.GradeName.HeaderText = "年级名称";
            this.GradeName.Name = "GradeName";
            this.GradeName.ReadOnly = true;
            // 
            // colEdit
            // 
            dataGridViewCellStyle1.NullValue = "修改";
            this.colEdit.DefaultCellStyle = dataGridViewCellStyle1;
            this.colEdit.HeaderText = "修改";
            this.colEdit.Name = "colEdit";
            // 
            // colDel
            // 
            dataGridViewCellStyle2.NullValue = "删除";
            this.colDel.DefaultCellStyle = dataGridViewCellStyle2;
            this.colDel.HeaderText = "删除";
            this.colDel.Name = "colDel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(133, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "年级名称：";
            // 
            // txtGradeName
            // 
            this.txtGradeName.Location = new System.Drawing.Point(205, 32);
            this.txtGradeName.Name = "txtGradeName";
            this.txtGradeName.Size = new System.Drawing.Size(100, 21);
            this.txtGradeName.TabIndex = 2;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(40, 32);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "添加年级";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btndelete
            // 
            this.btndelete.Location = new System.Drawing.Point(453, 32);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(75, 23);
            this.btndelete.TabIndex = 3;
            this.btndelete.Text = "删除";
            this.btndelete.UseVisualStyleBackColor = true;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(342, 32);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "添加";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // FrmGradeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 373);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btndelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtGradeName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvGradeList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmGradeList";
            this.Text = "年级列表页面";
            this.Load += new System.EventHandler(this.FrmGradeList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGradeList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGradeList;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colCheck;
        private System.Windows.Forms.DataGridViewTextBoxColumn GradeId;
        private System.Windows.Forms.DataGridViewTextBoxColumn GradeName;
        private System.Windows.Forms.DataGridViewLinkColumn colEdit;
        private System.Windows.Forms.DataGridViewLinkColumn colDel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGradeName;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnSubmit;
    }
}