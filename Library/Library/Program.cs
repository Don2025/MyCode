using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;   //用于加密的命名空间

namespace Library
{
    static class Program
    {
        //定义静态的全局变量
        public static string QQemail = "tanyaodan@vip.qq.com";   //发件人邮箱
        public static string AuthorizationCode = "uwyhecvjjpzpbbag";   //QQ邮箱授权码,具有时效性,一段时间后得更新
        public static string loginName;    //当前登录账号的昵称
        public static string realName;     //当前登录账号的实名
        public static string loginID;      //当前登录账号的身份
        public static string photoPath;    //当前登录账号的头像(暂时只支持本地路径)
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]

        public static string CreateRandomCode(int length)  //生成由数字和大小写字母组成的验证码
        {
            string list = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";  //list中存放着验证码的元素
            Random random = new Random();
            string code = "";   //验证码
            for (int i = 0; i < length; i++)   //循环6次得到一个伪随机的六位数验证码
            {
                code += list[random.Next(0, list.Length - 1)];
            }
            return code;
        }

        public static string GetMd5Password(string password)    //得到MD5加密后的密码字符串
        {
            MD5 md5 = new MD5CryptoServiceProvider();  //实例化MD5对象
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));   //调用ComputeHash方法将字符串进行加密处理
            password = Convert.ToBase64String(s);      //将字节类型的数组转换为字符串,得到加密后的密码
            return password;
        }

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }

}
