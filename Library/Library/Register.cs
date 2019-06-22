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
using System.Threading;
using System.Net;
using System.Net.Mail;

namespace Library
{
    public partial class Register : Form
    {
        //全局变量
        private bool isVirgin = true;   //判断是不是第一次点击"上传头像"
        private int cnt1 = 60, cnt2 = 300;   //计时器
        private string code = Program.CreateRandomCode(6);   //伪随机的邮箱验证码
        public Register()
        {
            InitializeComponent();
        }
        public string photo = "";   //头像
        private void Button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text.Trim();  //获取用户输入的账号
            string password1 = textBox2.Text.Trim();  //获取用户输入的密码
            string password2 = textBox3.Text.Trim();      //获取用户再次输入的密码
            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(password1) || String.IsNullOrEmpty(password2) || String.IsNullOrEmpty(textBox4.Text) || String.IsNullOrEmpty(textBox5.Text) || String.IsNullOrEmpty(textBox6.Text) || String.IsNullOrEmpty(textBox7.Text))    //若信息没填写完整
            {
                MessageBox.Show("请将信息填写完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CheckCode_Click(sender, e);    //刷新一遍图形验证码
            }
            else if (password1 != password2)    //判断俩次输入的密码是否一致
            {
                MessageBox.Show("两次输入的密码不一致！\n请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CheckCode_Click(sender,e);   //刷新一遍验证码
            }
            else if(String.IsNullOrEmpty(photo))   //若没有选择头像
            {
                MessageBox.Show("请上传头像！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if(textBox7.Text.ToLower() != code.ToLower())   //邮箱验证码输入有误
            {
                MessageBox.Show("您输入的邮箱验证码有误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else if (textBox4.Text.ToLower() != checkCode.Text.ToLower())   //图形验证码输入错误
            {
                MessageBox.Show("您输入的图形验证码有误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CheckCode_Click(sender,e);   //刷新一遍验证码
                textBox4.Text = "";    //清空验证码输入框
            }
            else if (!checkBox1.Checked)
            {
                MessageBox.Show("请阅读并同意相关服务条款和隐私政策！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else  //开始连接数据库尝试注册账号
            {
                string password = Program.GetMd5Password(password1);     //使用MD5对密码进行加密
                string name = textBox5.Text.Trim();     //获取真实姓名
                string birthday = dateTimePicker1.Value.ToString().Substring(0, dateTimePicker1.Value.ToString().IndexOf(' ')).Trim();   //获取生日
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");
                connection.Open();   //打开连接
                //查询新注册的管理员账号是否存在
                //用SqlCommand来执行SQL语句:SELECT userid,password FROM login_table WHERE userid = (用户输入的username);
                SqlCommand command = new SqlCommand("SELECT username,password FROM login_table WHERE username ='" + username + "'", connection);
                SqlDataReader data = command.ExecuteReader();
                data.Read();    //读取管理员信息
                if (data.HasRows)    //若账号已存在,注册失败
                {
                    MessageBox.Show("注册失败\n您输入的账号已存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    string email = textBox6.Text.Trim();
                    //将新注册的管理员账户信息保存到数据库中
                    command = new SqlCommand("INSERT INTO login_table VALUES('" + username + "','" + password + "','用户','" + name + "','" + birthday + "','" + email + "','" + photo + "')", connection);
                    data.Close();
                    data = command.ExecuteReader();
                    MessageBox.Show("注册成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                data.Close();
                connection.Close();   //关闭链接
            }
        }

        private void CheckCode_Click(object sender, EventArgs e)
        {
            checkCode.Text = Program.CreateRandomCode(4);
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CheckCode_Click(sender, e);
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)   //上传头像
        {
            PictureBox2_Click(sender, e);
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            if (isVirgin)
            {
                //错误内容: 在可以调用OLE之前，必须将当前线程设置为单线程单元（STA）模式，请确保您的Main函数带有STAThreadAttribute
                Thread thread = new Thread(new ThreadStart(PictureDialog));
                thread.SetApartmentState(ApartmentState.STA); //重点
                thread.Start();
                isVirgin = false;
            }
        }

        public void PictureDialog()   //打开一个选择图片的对话框
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Title = "选择头像";   //左上角标题
            openfile.Filter = "图片(*.jpg;*.bmp;*png)|*.jpeg;*.jpg;*.bmp;*.png|所有文件(*.*)|*.*";  //可供选择的文件类型
            if (openfile.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = Image.FromFile(openfile.FileName);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;   //显示图片时按照原比例放大缩小
                photo = openfile.FileName;
            }
            isVirgin = true;
        }

        private void Register_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Button1_Click(sender, e);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (cnt1 > 0)
            {
                cnt1--;
                button3.Text = "发送(" + cnt1 + ")";
            }
            else
            {
                timer1.Enabled = false;
                button3.Enabled = true;
                button3.Text = "发送";
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)
        {
            if (cnt2 == 0)
            {
                timer2.Enabled = false;
                code = Program.CreateRandomCode(6);    //旧的验证码过期,生成一个新的验证码
            }
        }

        private void Button3_Click(object sender, EventArgs e)      //发送邮箱验证码
        {
            string email = textBox6.Text.Trim();    //邮箱
            if (!String.IsNullOrEmpty(email))  //账号、邮箱非空
            {
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");
                connection.Open();
                try
                {
                    MailMessage mail = new MailMessage();  //实例化一个发送邮件类
                    mail.From = new MailAddress(Program.QQemail);   //发件人邮箱地址
                    mail.To.Add(new MailAddress(email));    //收件人邮箱地址
                    mail.Subject = "【HBU图书管理系统】注册账号";    //邮件标题
                    code = Program.CreateRandomCode(6);   //生成伪随机的6位数验证码
                    mail.Body = "验证码是: " + code + "，请在5分钟内进行验证。验证码提供给他人可能导致账号被盗，请勿泄露，谨防被骗。系统邮件请勿回复。";  //邮件内容          
                    SmtpClient client = new SmtpClient("smtp.qq.com");   //实例化一个SmtpClient类。
                    client.EnableSsl = true;    //使用安全加密连接
                    client.Credentials = new NetworkCredential(Program.QQemail, Program.AuthorizationCode);//验证发件人身份(发件人的邮箱，邮箱里的生成授权码);        
                    client.Send(mail);
                    //计时器初始化
                    cnt1 = 60;
                    cnt2 = 300;
                    timer1.Enabled = true;   //time1用来记录1分钟
                    timer2.Enabled = true;   //time2用来记录5分钟
                    button3.Enabled = false;
                    //MessageBox.Show("发送成功！");
                }
                catch
                {
                    MessageBox.Show("发送失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
        }
    }
}