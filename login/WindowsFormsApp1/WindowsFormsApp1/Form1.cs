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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, EventArgs e)
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
            //与sql sever建立连接
            SqlConnection connection = new SqlConnection("server=.;User ID=sa;Password=sql123");
            connection.Open();   //打开连接

            /*
             * ExecuteReader（）方法可以执行SELECT语句，
             * 我们可以用一个叫做SqlDataReader的对象来接收查询的结果集
             * SELECT语句执行后，在data里就得到了所有的usertable 表里的信息了
             */
            //用SqlCommand来执行SQL语句:SELECT userid,password FROM usertable WHERE userid = (用户输入的username) AND password = (用户输入的password);
            SqlCommand SqlCommand = new SqlCommand("SELECT userid,password FROM usertable WHERE userid ='"+username+"' AND password ='"+password+"'", connection);
            SqlDataReader data = SqlCommand.ExecuteReader();
            data.Read();
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password))
            {
                MessageBox.Show("用户名或密码不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (data.HasRows)  //HasRows用来判断查询结果中是否有数据
            {
                MessageBox.Show("登录成功！","通知",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);             //登录成功
            }
            else
            {
                MessageBox.Show("登录失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            data.Close();   //关闭SqlDataReader对象
            connection.Close();  //关闭连接
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)   //用户输入完毕时敲回车键可以进行登录
            {
                ButtonLogin_Click(sender,e);
            }
        }
    }
}
