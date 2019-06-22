using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{   
    public partial class AdminForm : Form
    {
        private bool isVirgin = true;    //第一次启动窗体时进行加载
        public AdminForm()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)   //注销账号,返回到登录界面
        {
            DialogResult result = MessageBox.Show("确认注销账号？","提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if(DialogResult.OK == result)
            {
                this.Visible = false;
                new Login().ShowDialog();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            new BookInfomation().ShowDialog();
        }

        private void 添加图书ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AddBooks().ShowDialog();   //打开添加图书界面
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ChangePassword().ShowDialog();    //打开修改密码界面
        }

        private void 注销账号ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Button1_Click(sender,e);
        }

        private void 借书办理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Borrowing().ShowDialog();   //打开借书界面
        }

        private void 还书管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Return().ShowDialog();
        }

        private void 新增用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Register().ShowDialog();
        }

        private void 用户资料ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UserInfomation().ShowDialog();
        }

        private void 书籍列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new BookInfomation().ShowDialog();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            if (isVirgin)
            {
                label1.Text = "您当前的登录身份是：" + Program.loginID;
                label3.Text = Program.realName + ",您好！";
                pictureBox1.Image = Image.FromFile(Program.photoPath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;   //显示图片时按照原比例放大缩小
                isVirgin = false;
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = "当前时间: " + DateTime.Now.ToString();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            System.Environment.Exit(0);    //这是最彻底的退出方式，不管什么线程都被强制退出，把程序结束的很干净
        }
    }
}
