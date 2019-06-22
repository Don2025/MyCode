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
using System.Net;
using System.Net.Mail;

namespace Library
{
    public partial class ForgetPassword : Form
    {
        private int cnt1 = 60, cnt2 = 300;   //计时器
        private string code = Program.CreateRandomCode(6);   //伪随机的邮箱验证码
        public ForgetPassword()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            string userid = textBox1.Text.Trim();   //账号
            string email = textBox2.Text.Trim();    //邮箱
            string code1 = textBox3.Text.Trim();    //图形验证码
            string code2 = textBox4.Text.Trim();    //邮箱验证码
            string password1 = textBox5.Text.Trim();  //密码
            string password2 = textBox6.Text.Trim();  //再次输入的密码
            if (String.IsNullOrEmpty(userid) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(code1) || String.IsNullOrEmpty(code2) || String.IsNullOrEmpty(password1) || String.IsNullOrEmpty(password2))   //若信息没有填写完整
            {
                MessageBox.Show("请将信息填写完整！","警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if(code.ToLower() != code2.ToLower())    //若输入的邮箱验证码有误
            {
                MessageBox.Show("您输入的邮箱验证码有误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if(password1 != password2)  //若俩次输入的密码不一致
            {
                MessageBox.Show("您俩次输入的密码不一致！","警告",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            else if(checkCode.Text.ToLower() != code1.ToLower())   //若输入的图形验证码有误
            {
                MessageBox.Show("您输入的图形验证码有误！","警告",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                CheckCode_Click(sender, e);   //刷新一遍验证码
            }
            else  
            {
                //连接数据库,并将新密码保存
                password1 = Program.GetMd5Password(password1);   //使用MD5加密
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE login_table SET password='" + password1 + "' WHERE username='" + userid + "'", connection);
                SqlDataReader data = command.ExecuteReader();
                data.Close();
                connection.Close();
                MessageBox.Show("找回密码成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Visible = false;
            }
        }

        private void Button2_Click(object sender, EventArgs e)   //发送邮箱验证码修改密码
        {
            string userid = textBox1.Text.Trim();   //账号
            string email = textBox2.Text.Trim();    //邮箱
            if (!String.IsNullOrEmpty(userid)&&!String.IsNullOrEmpty(email))  //账号、邮箱非空
            {
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT username,email FROM login_table WHERE username='" + userid + "' AND email='" + email + "'", connection);
                SqlDataReader data = command.ExecuteReader();
                if (data.HasRows)   //若输入的电子邮箱是账号注册时填写的邮箱
                {
                    try
                    {
                        MailMessage mail = new MailMessage();  //实例化一个发送邮件类
                        mail.From = new MailAddress(Program.QQemail);   //发件人邮箱地址
                        mail.To.Add(new MailAddress(email));    //收件人邮箱地址
                        mail.Subject = "【HBU图书管理系统】找回密码";    //邮件标题
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
                        button2.Enabled = false;
                        //MessageBox.Show("发送成功！");
                    }
                    catch
                    {
                        MessageBox.Show("发送失败！\n请检查邮箱是否输入有误。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("该邮箱不是账号绑定的邮箱。", "失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("请将账号和邮箱填写完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)    //发送完邮件,需要60秒后才能再次发送邮件
        {
            if (cnt1 > 0)
            {
                cnt1--;
                button2.Text = "发送(" + cnt1 + ")";
            }
            else
            {
                timer1.Enabled = false;
                button2.Enabled = true;
                button2.Text = "发送";
            }
        }

        private void Timer2_Tick(object sender, EventArgs e)    //验证码5分钟内有效,但是如果有新的验证码出现,旧验证码就会GG
        {
            if (cnt2 == 0)
            {
                timer2.Enabled = false;
                code = Program.CreateRandomCode(6);    //旧的验证码过期,生成一个新的验证码
            }
        }

        private void Button3_Click(object sender, EventArgs e)   //取消
        {
            this.Visible = false;   //隐藏当前窗体
        }

        private void ForgetPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                Button1_Click(sender, e);
            }
        }

        private void CheckCode_Click(object sender, EventArgs e)
        {
            checkCode.Text = Program.CreateRandomCode(4);
        }
    }
}
