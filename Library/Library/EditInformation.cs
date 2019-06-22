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
using System.Threading;

namespace Library
{
    public partial class EditInformation : Form
    {
        private bool isVirgin = true;   //判断是不是第一次点击"上传头像"
        private bool isFirst = true;
        private string photo = Program.photoPath;
        public EditInformation()
        {
            InitializeComponent();
        }

        private void GetInformation()   //获取信息
        {
            textBox1.Text = Program.loginName;  //账号
            pictureBox1.Image = Image.FromFile(Program.photoPath);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;   //显示图片时按照原比例放大缩小
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");  //实例化连接对象
            connection.Open();   //打开连接
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT birthday,realname FROM login_table WHERE username='" + textBox1.Text + "'", connection);
            sda.Fill(ds,"table1");
            connection.Close();    //关闭连接
            dateTimePicker1.Text = ds.Tables["table1"].Rows[0][0].ToString();
            textBox2.Text = ds.Tables["table1"].Rows[0][1].ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            new ChangePassword().ShowDialog();       //更改密码
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            new ChangeEmail().ShowDialog();    //修改密码
        }

        private void PictureBox1_Click(object sender, EventArgs e)
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
                pictureBox1.Image = Image.FromFile(openfile.FileName);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;   //显示图片时按照原比例放大缩小
                photo = openfile.FileName;
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE login_table SET photo='" + photo + "' WHERE username='" + Program.loginName + "'", connection);
                SqlDataReader data = command.ExecuteReader();
                //MessageBox.Show("头像更换成功！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                connection.Close();
            }
            isVirgin = true;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PictureBox1_Click(sender, e);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(isFirst)
            {
                isFirst = false;
                GetInformation();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            string birthday = dateTimePicker1.Value.ToString().Substring(0, dateTimePicker1.Value.ToString().IndexOf(' ')).Trim();   //获取生日
            string realname = textBox2.Text;
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");  //实例化连接对象
            connection.Open();   //打开连接
            SqlCommand command = new SqlCommand("UPDATE login_table SET birthday='" + birthday +"',realname = '" + realname + "' WHERE username='" + Program.loginName + "'",connection);
            SqlDataReader data = command.ExecuteReader();
            //MessageBox.Show("信息更新成功","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            data.Close();
            connection.Close();    //关闭连接
            Program.realName = realname;
            Program.photoPath = photo;
        }

        private void Button4_Click(object sender, EventArgs e)   //若取消了编辑资料
        {
            this.Visible = false;
            //把头像换回编辑资料前的头像,因为我是直接更改的数据库里的头像信息
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");
            connection.Open();
            SqlCommand command = new SqlCommand("UPDATE login_table SET photo='" + Program.photoPath + "' WHERE username='" + Program.loginName + "'", connection);
            SqlDataReader data = command.ExecuteReader();
            //MessageBox.Show("头像更换成功！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            connection.Close();
        }
    }
}
