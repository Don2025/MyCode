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
    public partial class Return : Form
    {
        private bool isVirgin = true;   //用来加载用户的还书记录的
        public Return()
        {
            InitializeComponent();
        }

        public void GetDataGridView()   //获取所有借书记录
        {
            try
            {
                //与sql sever建立连接，并指定到Library这个数据库
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");  //实例化链接对象
                connection.Open();   //打开连接
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT borrowid '借阅单号',username '借阅者',bookid '图书序列号',bookname '书名',price '单价',borrowdate '借书日期',borrowtime '借阅期限',returndate '还书日期' FROM borrow_table", connection);
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

        public void InquiryBorrowId()     //根据借阅单号来查询借阅信息
        {
            string borrowid = textBox1.Text.Trim();     //获取需要查询的借阅单号
            if (!String.IsNullOrEmpty(borrowid))   //若输入的借阅单号不为空
            {
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");  //实例化连接对象
                connection.Open();   //打开连接
                //执行SQL语句：SELECT name FROM borrow_table WHERE borrowid LIKE %(需要查询的借阅单号)%;
                SqlCommand command = new SqlCommand("SELECT borrowid FROM borrow_table WHERE borrowid LIKE '%" + borrowid + "%'", connection);
                SqlDataReader data = command.ExecuteReader();
                data.Read();    //读取信息
                if (data.HasRows)
                {
                    //MessageBox.Show("您查询的借阅单号存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    data.Close();
                    DataSet ds = new DataSet();
                    SqlDataAdapter sda = new SqlDataAdapter("SELECT borrowid '借阅单号',username '借阅者',bookid '图书序列号',bookname '书名',price '单价',borrowdate '借书日期',borrowtime '借阅期限',returndate '还书日期' FROM borrow_table WHERE borrowid LIKE '%" + borrowid + "%'", connection);
                    sda.Fill(ds);    //填充数据 Fill方法其实是隐藏的执行了Sql命令对象的CommandText
                    connection.Close();
                    dataGridView1.AutoGenerateColumns = true;   //自动创建列
                    dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;  //单击单元格编辑
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;  //自动调整列宽
                    dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;    //自动调整行高
                    dataGridView1.DataSource = ds.Tables[0];
                }
                else
                {
                    MessageBox.Show("您查询的借阅单号不存在！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    textBox1.Text = "";    //清空查询书名的输入框
                }
                data.Close();
                connection.Close();
            }
        }

        public void GetUserDataGridView()   //获取当前用户的所有未归还的借书记录
        {
            try
            {
                //与sql sever建立连接，并指定到Library这个数据库
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");  //实例化链接对象
                connection.Open();   //打开连接
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT borrowid '借阅单号',username '借阅者',bookid '图书序列号',bookname '书名',price '单价',borrowdate '借书日期',borrowtime '借阅期限',returndate '还书日期' FROM borrow_table WHERE username='" + Program.realName + "'", connection);
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

        private void Button1_Click(object sender, EventArgs e)
        {
            InquiryBorrowId();   
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            GetDataGridView();    //查询历史借阅记录
        }

        // 表格单元格鼠标MouseUp事件
        private void DataGridView1_CellMouseUp_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 开关文本框的锁定，将表格内的数据显示到文本框内
            int i = dataGridView1.CurrentRow.Index;
            textBox2.Text = dataGridView1.Rows[i].Cells["借阅单号"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells["借阅者"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[i].Cells["图书序列号"].Value.ToString();
            textBox5.Text = dataGridView1.Rows[i].Cells["书名"].Value.ToString();
            textBox6.Text = dataGridView1.Rows[i].Cells["借书日期"].Value.ToString();
            textBox7.Text = dataGridView1.Rows[i].Cells["借阅期限"].Value.ToString();
            textBox9.Text = dataGridView1.Rows[i].Cells["单价"].Value.ToString();
            if (dataGridView1.Rows[i].Cells["还书日期"].Value.ToString() == "NULL")   //若还书日期为NULL
            {
                textBox8.Text = "暂未归还";
            }
            else
            {
                textBox8.Text = dataGridView1.Rows[i].Cells["还书日期"].Value.ToString();
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            this.label9.Text = "当前时间：" + DateTime.Now.ToString();
            if(isVirgin)
            {
                this.pictureBox2.Image = Image.FromFile(Program.photoPath);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;   //显示图片时按照原比例放大缩小
                if (Program.loginID == "用户")
                {
                    textBox3.Text = Program.realName;
                    label1.Visible = false;
                    textBox1.Visible = false;
                    button1.Visible = false;
                    button2.Visible = false;
                    GetUserDataGridView();
                }
                isVirgin = false;
            }
        }

        private void Button3_Click(object sender, EventArgs e)    //确认归还按钮
        {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");   //实例化连接对象
            connection.Open();    //打开连接
            if (!String.IsNullOrEmpty(textBox8.Text))
            {
                MessageBox.Show("该书已经归还");
            }
            else
            {
                //获取当前的借书记录条数
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT booknum FROM book_table WHERE name='" + textBox5.Text + "'", connection);
                sda.Fill(ds, "book_table");
                int booknum = Convert.ToInt32(ds.Tables["book_table"].Rows[0][0].ToString()) + 1;   //还书之后,该书的库存量加一 
                //更新book_table表,归还书籍的库存数量加一
                //执行SQL语句:UPDATE book_table SET booknum='(更新后的库存数量)'
                SqlCommand command = new SqlCommand("UPDATE book_table SET booknum='" + booknum.ToString() + "' WHERE name='" + textBox5.Text + "'", connection);
                SqlDataReader data = command.ExecuteReader();
                data.Close();
                ds.Clear();   //清空内存中的缓存数据
                //更新borrow_table表,将还书日期填入
                //执行SQL语句: UPDATE borrow_table SET returndate='(当前时间)'
                string returndate = label9.Text.Substring(5, 9);   //借书时间
                command = new SqlCommand("UPDATE borrow_table SET returndate='" + returndate + "' WHERE borrowid='" + textBox2.Text + "'", connection);
                data = command.ExecuteReader();
                data.Close();
                connection.Close();
                MessageBox.Show("归还成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //初始化文本框
                if (Program.loginID == "管理员")
                {
                    GetDataGridView();   //刷新借书记录
                }
                else
                {
                    isVirgin = true;
                }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
            }
        }
    }
}
