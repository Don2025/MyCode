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
    public partial class AddBooks : Form
    {
        public AddBooks()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)   //确认提交
        {
            string bookid = textBox1.Text.Trim();     //序列号
            string name = textBox2.Text.Trim();       //书名
            string author = textBox3.Text.Trim();     //作者
            string publisher = textBox4.Text.Trim();  //出版商
            string pubdate = textBox5.Text.Trim();    //出版日期
            string ISBN = textBox6.Text.Trim();       //国家标准书号ISBN
            string price = textBox7.Text.Trim();      //书的价格
            string booknum = textBox8.Text.Trim();    //库存数量
            if (String.IsNullOrEmpty(bookid) || String.IsNullOrEmpty(name) || String.IsNullOrEmpty(author) || String.IsNullOrEmpty(publisher) || String.IsNullOrEmpty(pubdate) || String.IsNullOrEmpty(ISBN) || String.IsNullOrEmpty(price) || String.IsNullOrEmpty(booknum))  //信息是否填写完整
            {
                MessageBox.Show("请将图书信息填写完整！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");  //实例化连接对象
                connection.Open();   //打开连接
                //执行SQL语句进行插入,例如：INSERT INTO book_table VALUES('201900013','趣学算法','陈小玉','人民邮电出版社','2017-07','978-7-1154-5957-2','89','42'); 
                SqlCommand command = new SqlCommand("INSERT INTO book_table VALUES('" + bookid + "','" + name + "','" + author + "','" + publisher + "','" + pubdate + "','" + ISBN + "','" + price + "','" + booknum + "')", connection);
                SqlDataReader data = command.ExecuteReader();
                MessageBox.Show("添加成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                data.Close();
                connection.Close();   //关闭连接
                this.Close();   //关闭当前窗体

            }
        }
        private void Button2_Click(object sender, EventArgs e)   //取消添加图书
        {
            this.Visible = false;   //隐藏当前窗体
        }
    }
}
