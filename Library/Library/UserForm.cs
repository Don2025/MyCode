using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class UserForm : Form
    {
        private bool isVirgin = true;   //第一次启动UserForm时进行加载
        public UserForm()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = "当前时间: " + DateTime.Now.ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            new Borrowing().ShowDialog();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            new Return().ShowDialog();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            new ChangePassword().ShowDialog();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            new ChangeEmail().ShowDialog();
        }

        private void Button5_Click(object sender, EventArgs e)  //编辑资料
        {
            new EditInformation().ShowDialog();
        }

        private void Button6_Click(object sender, EventArgs e)   //注销账号
        {
            DialogResult result = MessageBox.Show("确认注销账号？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (DialogResult.OK == result)
            {
                this.Visible = false;
                new Login().ShowDialog();
            }
        }

        private void UserForm_Load(object sender, EventArgs e)
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

        protected override void OnClosing(CancelEventArgs e)
        {
            System.Environment.Exit(0);    //这是最彻底的退出方式，不管什么线程都被强制退出，把程序结束的很干净
        }
    }
}
