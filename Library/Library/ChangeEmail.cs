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
    public partial class ChangeEmail : Form
    {
        private int cnt1 = 60, cnt2 = 300;   //计时器
        private string code = Program.CreateRandomCode(6);   //伪随机的邮箱验证码
        public ChangeEmail()
        {
            InitializeComponent();
        }

        private void CheckCode_Click(object sender, EventArgs e)
        {
            checkCode.Text = Program.CreateRandomCode(4);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string email = textBox2.Text.Trim();    //邮箱
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

        private void Button1_Click(object sender, EventArgs e)
        {
            string userid = textBox1.Text.Trim();   //账号
            string email = textBox2.Text.Trim();    //邮箱
            string code1 = textBox3.Text.Trim();    //邮箱验证码
            string code2 = textBox4.Text.Trim();    //图形验证码
            if (String.IsNullOrEmpty(userid) || String.IsNullOrEmpty(email) || String.IsNullOrEmpty(code1) || String.IsNullOrEmpty(code2))   //若信息没有填写完整
            {
                MessageBox.Show("请将信息填写完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (code.ToLower() != code1.ToLower())    //若输入的邮箱验证码有误
            {
                MessageBox.Show("您输入的邮箱验证码有误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (checkCode.Text.ToLower() != code2.ToLower())   //若输入的图形验证码有误
            {
                MessageBox.Show("您输入的图形验证码有误！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                //连接数据库,并将邮箱地址更新
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE login_table SET email='" + email + "' WHERE username='" + userid + "'", connection);
                SqlDataReader data = command.ExecuteReader();
                data.Close();
                connection.Close();
                MessageBox.Show("更改邮箱成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Visible = false;
            }
        }

        private void ChangeEmail_Load(object sender, EventArgs e)
        {
            textBox1.Text = Program.loginName;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void ChangeEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                Button1_Click(sender, e);
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
    }
}
