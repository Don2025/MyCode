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

namespace Library
{
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
            if(Program.loginID == "用户")
            {
                label1.Text = "您的账号是：";
                textBox1.Text = Program.loginName;
                textBox1.ReadOnly = true;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();  //获取输入的账号
            string oldpassword = textBox2.Text.Trim();  //获取输入的旧密码
            string newpassword1 = textBox3.Text.Trim();      //获取输入的新密码
            string newpassword2 = textBox4.Text.Trim();      //获取再次输入的密码
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(oldpassword) || String.IsNullOrEmpty(newpassword1) || String.IsNullOrEmpty(newpassword2))
            {
                MessageBox.Show("账号或密码不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (newpassword1 != newpassword2)    //判断俩次输入的密码是否一致
            {
                MessageBox.Show("两次输入的密码不一致！\n请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (String.IsNullOrEmpty(textBox5.Text))    //若没有输入验证码
            {
                MessageBox.Show("请输入验证码！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CheckCode_Click(sender, e);    //刷新验证码
            }
            else if (textBox5.Text != checkCode.Text)     //判断验证码是否输入正确
            {
                MessageBox.Show("验证码输入错误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CheckCode_Click(sender, e);
                textBox5.Text = "";
            }
            else
            {
                //使用MD5加密后再和数据库中login_table表中存取的密码进行比对
                oldpassword = Program.GetMd5Password(oldpassword);
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");
                connection.Open();   //打开连接
                //判断输入的账号密码是否存在在数据库中
                SqlCommand command = new SqlCommand("SELECT username,password FROM login_table WHERE username ='" + username + "' AND password='" + oldpassword + "'", connection);
                SqlDataReader data = command.ExecuteReader();
                data.Read();    //读取管理员信息
                if (data.HasRows)    //HasRows用来判断查询结果中是否有数据
                {
                    data.Close();
                    //将新密码newpassword使用MD5加密后再存入数据库中
                    newpassword1 = Program.GetMd5Password(newpassword1);    //使用MD5加密
                    //执行SQL语句: UPDATE admin_table SET password=(输入的新密码) WHERE userid=(输入的username) AND password=(输入的oldpassword);
                    command = new SqlCommand("UPDATE login_table SET password='" + newpassword1 + "' WHERE username='" + username + "' AND password='" + oldpassword + "'",connection);
                    data = command.ExecuteReader();
                    MessageBox.Show("密码修改成功！", "通知", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);    //密码修改成功
                    data.Close();   //关闭SqlDataReader对象
                    connection.Close();  //关闭连接
                    this.Close();   //关闭修改密码界面
                }
                else
                {
                    MessageBox.Show("密码修改失败！\n请检查旧密码是否输入正确","警告",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }

            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();    //关闭该窗口
        }

        private void CheckCode_Click(object sender, EventArgs e)    //更换验证码
        {
            checkCode.Text = Program.CreateRandomCode(4);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CheckCode_Click(sender,e);
        }

        private void TextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Button1_Click(sender, e);
            }
        }
    }
}
