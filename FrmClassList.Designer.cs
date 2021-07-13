
namespace WinStudent
{
    partial class FrmClassList
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboGrades = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.dgvClassList = new System.Windows.Forms.DataGridView();
            this.ClassId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GradeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClassList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.txtClassName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cboGrades);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(32, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(523, 60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "年级：";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // cboGrades
            // 
            this.cboGrades.FormattingEnabled = true;
            this.cboGrades.Location = new System.Drawing.Point(83, 24);
            this.cboGrades.Name = "cboGrades";
            this.cboGrades.Size = new System.Drawing.Size(121, 20);
            this.cboGrades.TabIndex = 1;
            this.cboGrades.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(228, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "班名：";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtClassName
            // 
            this.txtClassName.Location = new System.Drawing.Point(286, 22);
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(100, 21);
            this.txtClassName.TabIndex = 3;
            this.txtClassName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(424, 22);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 4;
            this.btnFind.Text = "查询";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvClassList
            // 
            this.dgvClassList.AllowUserToAddRows = false;
            this.dgvClassList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvClassList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClassList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ClassId,
            this.ClassName,
            this.GradeName,
            this.Remark});
            this.dgvClassList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvClassList.Location = new System.Drawing.Point(0, 98);
            this.dgvClassList.Name = "dgvClassList";
            this.dgvClassList.RowTemplate.Height = 23;
            this.dgvClassList.Size = new System.Drawing.Size(567, 342);
            this.dgvClassList.TabIndex = 1;
            // 
            // ClassId
            // 
            this.ClassId.DataPropertyName = "ClassId";
            this.ClassId.HeaderText = "编号";
            this.ClassId.Name = "ClassId";
            this.ClassId.ReadOnly = true;
            // 
            // ClassName
            // 
            this.ClassName.DataPropertyName = "ClassName";
            this.ClassName.HeaderText = "班名";
            this.ClassName.Name = "ClassName";
            this.ClassName.ReadOnly = true;
            // 
            // GradeName
            // 
            this.GradeName.DataPropertyName = "GradeName";
            this.GradeName.HeaderText = "年级";
            this.GradeName.Name = "GradeName";
            this.GradeName.ReadOnly = true;
            // 
            // Remark
            // 
            this.Remark.DataPropertyName = "Remark";
            this.Remark.HeaderText = "描述";
            this.Remark.Name = "Remark";
            this.Remark.ReadOnly = true;
            // 
            // FrmClassList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(567, 440);
            this.Controls.Add(this.dgvClassList);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmClassList";
            this.Text = "班级列表页面";
            this.Load += new System.EventHandler(this.FrmClassList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClassList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboGrades;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvClassList;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassName;
        private System.Windows.Forms.DataGridViewTextBoxColumn GradeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
    }
}