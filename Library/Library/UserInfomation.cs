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
    public partial class UserInfomation : Form
    {
        //全局变量
        private bool isSave = true;
        private DataSet ds = null;
        private bool isVirgin = true;  //判断是不是第一次点击"更换头像"
        public UserInfomation()
        {
            InitializeComponent();
        }

        public void GetDataGridView()   //查询用户列表
        {
            try
            {
                //与sql sever建立连接，并指定到Library这个数据库
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");  //实例化连接对象
                connection.Open();   //打开连接
                ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT username '账号',realname '实名',birthday '生日',email FROM login_table WHERE id='用户'", connection);
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

        // 表格单元格鼠标MouseUp事件
        private void DataGridView1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            // 开关文本框的锁定，将表格内的数据显示到文本框内
            int i = dataGridView1.CurrentRow.Index;
            textBox1.Text = dataGridView1.Rows[i].Cells["email"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[i].Cells["账号"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[i].Cells["实名"].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[i].Cells["生日"].Value.ToString();
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");  //实例化连接对象
            connection.Open();   //打开连接
            DataSet temp = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT photo FROM login_table WHERE id='用户' AND username='" + textBox2.Text + "'", connection);
            sda.Fill(temp,"table1");
            connection.Close();    //关闭连接
            string photo = temp.Tables["table1"].Rows[0][0].ToString();
            pictureBox1.Image = Image.FromFile(photo);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;   //显示图片时按照原比例放大缩小
        }

        private void SaveToSqlServer()
        {
            SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");
            connection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT username '账号', realname '实名', birthday '生日', email FROM login_table", connection);
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

        private void UserInfomation_Load(object sender, EventArgs e)
        {
            GetDataGridView();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SaveToSqlServer();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!isSave)    //若更改的数据没有保存,则弹出提示框
            {
                DialogResult result = MessageBox.Show("是否保存更改？", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)   //保存更改并关闭
                {
                    SaveToSqlServer();
                    base.OnClosing(e);
                }
                else if (result == DialogResult.No) //放弃保存,直接关闭
                {
                    base.OnClosing(e);
                }
                else    //取消关闭,返回界面
                {
                    e.Cancel = true;
                }
            }
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            isSave = false;
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
                string photo = openfile.FileName;    //更换的头像所在路径
                string name = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();  //头像对应的账号
                //把头像保存到数据库中
                SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=Library;User ID=sa;Password=sql123");
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE login_table SET photo='" + photo + "' WHERE username='" + name + "'", connection);
                SqlDataReader data = command.ExecuteReader();
                //MessageBox.Show("头像更换成功！","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            isVirgin = true;
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PictureBox1_Click(sender, e);
        }
    }
}
