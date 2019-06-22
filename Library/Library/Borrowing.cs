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
    public partial class Borrowing : Form
    {
        private bool isVirgin = true;   //判断是不是刚开始运行该窗体
        public Borrowing()
        {
            InitializeComponent();
        }

        public void GetDataGridView()   //查询书籍列表
        {
            try
            {
                //与sql sever建立连接，并指定到Librar这个数据库
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");  //实例化链接对象
                connection.Open();   //打开连接
                DataSet ds = new DataSet();   //数据在内存区的缓存
                SqlDataAdapter sda = new SqlDataAdapter("SELECT bookid '图书序列号',name '书名',author '作者',publisher '出版商',pubdate '出版时间',ISBN '国际标准书号',price '单价',booknum '库存数量' FROM book_table", connection);
                sda.Fill(ds);    //填充数据 Fill方法其实是隐藏的执行了Sql命令对象的CommandText
                connection.Close();    //关闭连接
                dataGridView1.AutoGenerateColumns = true;   //自动创建列
                dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;  //单击单元格编辑
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;  //自动调整列宽
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;    //自动调整行高
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());

            }
        }

        public void InquiryBookName()    //根据书名来查询书籍信息
        {
            string bookname = textBox1.Text.Trim();    //获取需要查询的书籍名称
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");  //实例化连接对象
            connection.Open();   //打开连接
            //执行SQL语句：SELECT name FROM book_table WHERE name LIKE '%(需要查询的书名)%';
            SqlCommand command = new SqlCommand("SELECT name FROM book_table WHERE name LIKE '%" + bookname + "%'", connection);
            SqlDataReader data = command.ExecuteReader();
            data.Read();    //读取用户信息
            if (data.HasRows)
            {
                //MessageBox.Show("您查询的书籍存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                data.Close();
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT bookid '图书序列号',name '书名',author '作者',publisher '出版商',pubdate '出版时间',ISBN '国际标准书号',price '单价',booknum '库存数量' FROM book_table WHERE name LIKE '%" + bookname + "%'", connection);
                sda.Fill(ds);   //填充数据 Fill方法其实是隐藏的执行了Sql命令对象的CommandText
                connection.Close();
                dataGridView1.AutoGenerateColumns = true;   //自动创建列
                dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;  //单击单元格编辑
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;  //自动调整列宽
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;    //自动调整行高
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("您查询的书籍不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Text = "";    //清空查询书名的输入框
            }
            data.Close();
            connection.Close();
        }

        public void InquiryBookId()     //根据序列号来查询书籍信息
        {
            string bookid = textBox1.Text.Trim();     //获取需要查询的书籍的序列号
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");  //实例化连接对象
            connection.Open();   //打开连接
            //执行SQL语句：SELECT name FROM book_table WHERE name LIKE '%(需要查询的书名)%';
            SqlCommand command = new SqlCommand("SELECT name FROM book_table WHERE bookid LIKE '%" + bookid + "%'", connection);
            SqlDataReader data = command.ExecuteReader();
            data.Read();    //读取用户信息
            if (data.HasRows)
            {
                //MessageBox.Show("您查询的书籍存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                data.Close();
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT bookid '图书序列号',name '书名',author '作者',publisher '出版商',pubdate '出版时间',ISBN '国际标准书号',price '单价',booknum '库存数量' FROM book_table WHERE bookid LIKE '%" + bookid + "%'", connection);
                sda.Fill(ds);
                connection.Close();
                dataGridView1.AutoGenerateColumns = true;   //自动创建列
                dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;  //单击单元格编辑
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;  //自动调整列宽
                dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;    //自动调整行高
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                MessageBox.Show("您查询的书籍不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                textBox1.Text = "";    //清空查询书名的输入框
            }
            data.Close();
            connection.Close();
        }

        private void Button1_Click(object sender, EventArgs e)   //根据书名进行查询
        {
            InquiryBookName();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.label7.Text = "当前时间：" + DateTime.Now.ToString();
            if (isVirgin)    //如果是刚打开这个窗体就执行这段代码
            {
                pictureBox2.Image = Image.FromFile(Program.photoPath);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;   //显示图片时按照原比例放大缩小
                if (Program.loginID == "管理员")
                {
                    textBox7.ReadOnly = false;
                }
                else   //用户只能自己借
                {
                    textBox7.Text = Program.realName;
                }
                isVirgin = false;
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string name = textBox7.Text.Trim();    //借书者姓名
            if (String.IsNullOrEmpty(name))   //若借书者姓名为空
            {
                MessageBox.Show("借阅者姓名不能为空！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");   //实例化连接对象
                connection.Open();    //打开连接
                //获取当前的借书记录条数
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM borrow_table", connection);
                sda.Fill(ds, "table1");
                int cnt = Convert.ToInt32(ds.Tables["table1"].Rows[0][0].ToString()) + 1;   //cnt表示借书总次数
                string borrowdate = label7.Text.Substring(5, 9);   //借书时间
                string borrowtime;   //借阅时长
                if(String.IsNullOrEmpty(comboBox1.Text))    //若没有选择借阅时长,则默认借阅时长为7天
                {
                    borrowtime = "7";  
                }
                else
                {
                    borrowtime = comboBox1.Text.Replace("天", "");   //把 天 字去掉
                }
                //将借书记录存入数据库中的borrow_table表
                //执行SQL语句: INSERT INTO borrow_table VALUES('00001','江疏影','','','');
                SqlCommand command = new SqlCommand("INSERT INTO borrow_table VALUES('" + cnt.ToString().PadLeft(5, '0') + "','" + textBox7.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + borrowdate + "','" + borrowtime + "',NULL)", connection);
                SqlDataReader data = command.ExecuteReader();
                data.Close();

                //被借走了书,就要记得在book_table表中让这本书的库存量减一
                //执行SQL语句: UPDATE book_table SET booknum=(更新后的库存数量) WHERE name='(被借走的书的书名)';
                int booknum = Convert.ToInt32(textBox6.Text) - 1;   //被借走的书的库存量减一
                command = new SqlCommand("UPDATE book_table SET booknum='" + booknum.ToString() + "' WHERE name='" + textBox3.Text + "'",connection);
                data = command.ExecuteReader();
                data.Close();
                connection.Close();
                MessageBox.Show("借阅成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //初始化所有输入框和选择框
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                if (Program.loginID == "管理员")
                {
                    textBox7.Text = "";
                }
                comboBox1.Text = "";
                GetDataGridView();   //显示借阅后的书籍列表
            }
        }

        // 表格单元格鼠标MouseUp事件
        private void DataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            //将表格内的数据显示到文本框内
            int i = dataGridView1.CurrentRow.Index;
            textBox2.Text = dataGridView1.Rows[i].Cells["图书序列号"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells["书名"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells["单价"].Value.ToString();
            textBox5.Text = dataGridView1.Rows[i].Cells["国际标准书号"].Value.ToString();
            textBox6.Text = dataGridView1.Rows[i].Cells["库存数量"].Value.ToString();         
        }

        private void Button3_Click(object sender, EventArgs e)    //根据序列号进行查询
        {
            InquiryBookId();
        }

        private void Button4_Click(object sender, EventArgs e)    //查询书籍列表
        {
            GetDataGridView();
        }
    }
}
