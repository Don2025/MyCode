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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();  //获取用户输入的账号
            string password = textBox2.Text.Trim();  //获取用户输入的密码
            /*
             * 操作sql Server数据库的五大对象：
               SqlConnection：创建数据库连接对象
               SqlCommand：执行SQL语句对象
               SqlDataReader：创建一个查询一条或多条数据的对象
               SqlDataAdapter：创建一个用于检索和保存数据的对象
               DataSet：创建一个本地数据存储对象
             */
            //与sql sever建立连接，并指定到Library这个数据库
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");  //实例化连接对象
            connection.Open();   //打开连接
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))    //若用户名或密码为空
            {
                MessageBox.Show("用户名或密码不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CheckCode_Click(sender, e);   //刷新一遍验证码
            }
            else    //用户名或密码非空
            {

                if (String.IsNullOrEmpty(textBox3.Text))   //判断是否填写了验证码
                {
                    MessageBox.Show("请填写验证码！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    CheckCode_Click(sender, e);
                }
                else if (textBox3.Text.ToLower() != checkCode.Text.ToLower())    //判断验证码输入是否正确
                {
                    MessageBox.Show("您输入的验证码有误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    CheckCode_Click(sender, e);   //刷新一遍验证码
                    textBox3.Text = "";    //清空验证码输入框
                }
                else    //开始连接数据库尝试登录
                {
                    //使用MD5加密后再和数据库中login_table表中存取的密码进行比对
                    password = Program.GetMd5Password(password);      //将字节类型的数组转换为字符串,得到加密后的密码
                    /*
                     * ExecuteReader（）方法可以执行SELECT语句，
                     * 可以用一个叫做SqlDataReader的对象来接收查询的结果集
                     * SELECT语句执行后，在data里就得到了我们想要查询的信息了
                     */
                    //用SqlCommand来执行SQL语句:SELECT userid,password FROM login_table WHERE username = (用户输入的username) AND password = (用户输入的password);
                    SqlCommand command = new SqlCommand("SELECT username,password FROM login_table WHERE username ='" + username + "' AND password ='" + password + "'", connection);
                    SqlDataReader data = command.ExecuteReader();
                    data.Read();
                    if (data.HasRows)    //HasRows用来判断查询结果中是否有数据
                    {
                        data.Close();   //关闭SqlDataReader对象
                        //获取当前登录账号的信息
                        DataSet ds = new DataSet();   //实例化DataSet对象
                        SqlDataAdapter sda = new SqlDataAdapter("SELECT id,realname,photo FROM login_table WHERE username='" + username + "'", connection);
                        sda.Fill(ds, "table1");
                        //将账号信息记录到全局变量中
                        Program.loginName = username;     //当前登录者的昵称
                        Program.loginID = ds.Tables["table1"].Rows[0][0].ToString();     //当前登录者的身份
                        Program.realName = ds.Tables["table1"].Rows[0][1].ToString();    //当前登录者的实名
                        Program.photoPath = ds.Tables["table1"].Rows[0][2].ToString();   //当前登录者的头像
                        connection.Close();  //关闭连接
                        this.Visible = false;   //隐藏登录窗体
                        //MessageBox.Show("登录成功！", "通知", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);    //登录成功
                        if(Program.loginID == "管理员")
                        {
                            new AdminForm().ShowDialog();   //打开管理员界面
                        }
                        else
                        {
                            new UserForm().ShowDialog();  //打开用户界面
                        }
                    }
                    else
                    {
                        MessageBox.Show("请检查账号和密码是否输入有误。", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        CheckCode_Click(sender, e);   //刷新一遍验证码
                        textBox3.Text = "";    //清空验证码输入框
                    }
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)   //用户输入完毕时敲回车键可以进行登录
            {
                PictureBox2_Click(sender, e);
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new Register().Show();   //打开注册窗体，可自由切换这俩个窗体
        }

        private void CheckCode_Click(object sender, EventArgs e)
        {
            checkCode.Text = Program.CreateRandomCode(4);
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new ForgetPassword().Show();   //找回密码
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            System.Environment.Exit(0);    //这是最彻底的退出方式，不管什么线程都被强制退出，把程序结束的很干净
        }

    }
}
