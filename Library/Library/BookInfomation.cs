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
    public partial class BookInfomation : Form
    {
        //全局变量
        private DataSet ds = null;   //数据集对象
        private bool isSave = true;  //用于判断在datagridview编辑后是否保存到数据库中啦
        public BookInfomation()
        {
            InitializeComponent();
        }

        public void GetDataGridView()   //查询书籍列表
        {
            try
            {
                //与sql sever建立连接，并指定到Library这个数据库
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");  //实例化链接对象
                connection.Open();   //打开连接
                ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT bookid '图书序列号',name '书名',author '作者',publisher '出版商',pubdate '出版时间',ISBN '国际标准书号',price '单价',booknum '库存数量' FROM book_table", connection);
                sda.Fill(ds);
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
            //执行SQL语句：SELECT * FROM book_table WHERE name=(需要查询的书名);
            SqlCommand command = new SqlCommand("SELECT * FROM book_table WHERE name LIKE '%" + bookname + "%'", connection);
            SqlDataReader data = command.ExecuteReader();
            data.Read();    //读取用户信息
            if (data.HasRows)
            {
                //MessageBox.Show("您查询的书籍存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                data.Close();
                ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT bookid '图书序列号',name '书名',author '作者',publisher '出版商',pubdate '出版时间',ISBN '国际标准书号',price '单价',booknum '库存数量' FROM book_table WHERE name LIKE '%" + bookname + "%'", connection);
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

        public void InquiryBookId()     //根据序列号来查询书籍信息
        {
            string bookid = textBox1.Text.Trim();     //获取需要查询的书籍的序列号
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");  //实例化连接对象
            connection.Open();   //打开连接
            SqlCommand command = new SqlCommand("SELECT * FROM book_table WHERE bookid LIKE '%" + bookid + "%'", connection);
            SqlDataReader data = command.ExecuteReader();
            data.Read();    //读取用户信息
            if (data.HasRows)
            {
                //MessageBox.Show("您查询的书籍存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                data.Close();
                ds = new DataSet();
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

        public void SaveToSqlServer()    //将书籍信息保存到数据库中
        {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT bookid '图书序列号',name '书名',author '作者',publisher '出版商',pubdate '出版时间',ISBN '国际标准书号',price '单价',booknum '库存数量' FROM book_table", connection);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);   //创建命令重建对象
            try
            {
                //这里是关键
                adapter.Update(ds);
                isSave = true;   //保存啦
                MessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                connection.Close();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            GetDataGridView(); //更新图书列表
        }

        private void Button1_Click(object sender, EventArgs e)    //根据书名来查询书籍信息
        {
            if (comboBox1.Text == "书名")    //根据书名进行查询
            {
                InquiryBookName();
            }
            else    //根据序列号进行查询
            {
                InquiryBookId();
            }
        }

        private void Button2_Click(object sender, EventArgs e)    //添加新图书到数据库
        {
            new AddBooks().ShowDialog();   //打开添加图书窗口
        }

        private void Button3_Click(object sender, EventArgs e)    //删除选中的那一行的书籍
        {
            //先查询这本书有无待归还的情况
            string bookname = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells["书名"].Value.ToString();
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");  //实例化连接对象
            connection.Open();   //打开连接
            SqlCommand command = new SqlCommand("SELECT * FROM borrow_table WHERE bookname='" + bookname + "' AND returndate=NULL", connection);
            SqlDataReader data = command.ExecuteReader();
            data.Read();
            if (data.HasRows)  //若还有人未归还该书
            {
                MessageBox.Show("删除失败\n还有人未归还该书。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else   //删除datagridview中的数据，点击保存可以更新到数据库中
            {
                dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                MessageBox.Show("删除成功!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        private void Button4_Click(object sender, EventArgs e)    //借书办理
        {
            new Borrowing().ShowDialog();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            new Return().ShowDialog();    //打开还书办理窗口 
        }

        private void Button6_Click(object sender, EventArgs e)   //保存datagridview中的信息到数据库
        {
            SaveToSqlServer();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!isSave)    //若更改的数据没有保存,则弹出提示框
            {
                DialogResult result = MessageBox.Show("是否保存更改？", "提示",MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
        
                if (result == DialogResult.Yes)   //保存更改并关闭
                {
                    SaveToSqlServer();
                    base.OnClosing(e);
                }
                else if(result == DialogResult.No) //放弃保存,直接关闭
                {
                    base.OnClosing(e);
                }
                else    //取消关闭,返回界面
                {
                    e.Cancel = true;   
                }
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)   //当表格的值改变时
        {
            isSave = false;
        }

        private void Form4_KeyDown(object sender, KeyEventArgs e)   //注意: KeyPreview设置为True时才有用
        {
            if (e.KeyCode == Keys.Delete)   //使用delete键进行删除
            {
                Button3_Click(sender, e);
            }
            if (e.KeyCode == Keys.S && e.Modifiers == Keys.Control)   //ctrl+s组合键进行保存
            {
                SaveToSqlServer(); 
            }
        }
    }
}
