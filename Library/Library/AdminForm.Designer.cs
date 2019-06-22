namespace Library
{
    partial class AdminForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.个人ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增用户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.资料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.注销账号ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.图书管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.书籍列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加图书ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.借书ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.还书管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.个人ToolStripMenuItem,
            this.图书管理ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(854, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 个人ToolStripMenuItem
            // 
            this.个人ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增用户ToolStripMenuItem,
            this.资料ToolStripMenuItem,
            this.修改密码ToolStripMenuItem,
            this.注销账号ToolStripMenuItem1});
            this.个人ToolStripMenuItem.Image = global::Library.Properties.Resources.个人;
            this.个人ToolStripMenuItem.Name = "个人ToolStripMenuItem";
            this.个人ToolStripMenuItem.Size = new System.Drawing.Size(88, 24);
            this.个人ToolStripMenuItem.Text = "用户管理";
            // 
            // 新增用户ToolStripMenuItem
            // 
            this.新增用户ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("新增用户ToolStripMenuItem.Image")));
            this.新增用户ToolStripMenuItem.Name = "新增用户ToolStripMenuItem";
            this.新增用户ToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.新增用户ToolStripMenuItem.Text = "新增用户";
            this.新增用户ToolStripMenuItem.Click += new System.EventHandler(this.新增用户ToolStripMenuItem_Click);
            // 
            // 资料ToolStripMenuItem
            // 
            this.资料ToolStripMenuItem.Image = global::Library.Properties.Resources.资料;
            this.资料ToolStripMenuItem.Name = "资料ToolStripMenuItem";
            this.资料ToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.资料ToolStripMenuItem.Text = "用户资料";
            this.资料ToolStripMenuItem.Click += new System.EventHandler(this.用户资料ToolStripMenuItem_Click);
            // 
            // 修改密码ToolStripMenuItem
            // 
            this.修改密码ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("修改密码ToolStripMenuItem.Image")));
            this.修改密码ToolStripMenuItem.Name = "修改密码ToolStripMenuItem";
            this.修改密码ToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.修改密码ToolStripMenuItem.Text = "修改密码";
            this.修改密码ToolStripMenuItem.Click += new System.EventHandler(this.修改密码ToolStripMenuItem_Click);
            // 
            // 注销账号ToolStripMenuItem1
            // 
            this.注销账号ToolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("注销账号ToolStripMenuItem1.Image")));
            this.注销账号ToolStripMenuItem1.Name = "注销账号ToolStripMenuItem1";
            this.注销账号ToolStripMenuItem1.Size = new System.Drawing.Size(128, 26);
            this.注销账号ToolStripMenuItem1.Text = "注销账号";
            this.注销账号ToolStripMenuItem1.Click += new System.EventHandler(this.注销账号ToolStripMenuItem1_Click);
            // 
            // 图书管理ToolStripMenuItem
            // 
            this.图书管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.书籍列表ToolStripMenuItem,
            this.添加图书ToolStripMenuItem,
            this.借书ToolStripMenuItem,
            this.还书管理ToolStripMenuItem});
            this.图书管理ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("图书管理ToolStripMenuItem.Image")));
            this.图书管理ToolStripMenuItem.Name = "图书管理ToolStripMenuItem";
            this.图书管理ToolStripMenuItem.Size = new System.Drawing.Size(88, 24);
            this.图书管理ToolStripMenuItem.Text = "图书管理";
            // 
            // 书籍列表ToolStripMenuItem
            // 
            this.书籍列表ToolStripMenuItem.Image = global::Library.Properties.Resources.图书管理;
            this.书籍列表ToolStripMenuItem.Name = "书籍列表ToolStripMenuItem";
            this.书籍列表ToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.书籍列表ToolStripMenuItem.Text = "书籍列表";
            this.书籍列表ToolStripMenuItem.Click += new System.EventHandler(this.书籍列表ToolStripMenuItem_Click);
            // 
            // 添加图书ToolStripMenuItem
            // 
            this.添加图书ToolStripMenuItem.Image = global::Library.Properties.Resources.填加图书;
            this.添加图书ToolStripMenuItem.Name = "添加图书ToolStripMenuItem";
            this.添加图书ToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.添加图书ToolStripMenuItem.Text = "添加图书";
            this.添加图书ToolStripMenuItem.Click += new System.EventHandler(this.添加图书ToolStripMenuItem_Click);
            // 
            // 借书ToolStripMenuItem
            // 
            this.借书ToolStripMenuItem.Image = global::Library.Properties.Resources.借书;
            this.借书ToolStripMenuItem.Name = "借书ToolStripMenuItem";
            this.借书ToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.借书ToolStripMenuItem.Text = "借书办理";
            this.借书ToolStripMenuItem.Click += new System.EventHandler(this.借书办理ToolStripMenuItem_Click);
            // 
            // 还书管理ToolStripMenuItem
            // 
            this.还书管理ToolStripMenuItem.Image = global::Library.Properties.Resources.还书;
            this.还书管理ToolStripMenuItem.Name = "还书管理ToolStripMenuItem";
            this.还书管理ToolStripMenuItem.Size = new System.Drawing.Size(128, 26);
            this.还书管理ToolStripMenuItem.Text = "还书办理";
            this.还书管理ToolStripMenuItem.Click += new System.EventHandler(this.还书管理ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(634, 444);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "您当前的登录身份是：";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(624, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "当前时间";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(12, 427);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 36);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(50, 444);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 19);
            this.label3.TabIndex = 6;
            this.label3.Text = "您好！";
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.BackgroundImage = global::Library.Properties.Resources.图书馆;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(854, 475);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AdminForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HBU图书管理系统";
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 个人ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 资料ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 修改密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 注销账号ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 图书管理ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加图书ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 借书ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 还书管理ToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem 新增用户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 书籍列表ToolStripMenuItem;
    }
}