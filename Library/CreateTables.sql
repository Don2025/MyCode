USE [Library]    --使用数据库Library
DROP TABLE IF EXISTS book_table    --table_table表(用来存放书籍的信息),若存在就先将它删除
DROP TABLE IF EXISTS login_table   --login_table表(用来存放管理员和用户信息),若存在就先将它删除
DROP TABLE IF EXISTS borrow_table  --borrow_table表(用来记录借还书籍的信息),若存在就先将它删除

--创建书籍表
CREATE TABLE book_table
(
	bookid NVARCHAR(100) PRIMARY KEY,   --图书序列号
	name NVARCHAR(100) NOT NULL,        --书名
	author NVARCHAR(100) NOT NULL,      --作者
	publisher NVARCHAR(100) NOT NULL,   --出版社
	pubdate NVARCHAR(100) NOT NULL,     --出版日期
	ISBN NVARCHAR(100) NOT NULL,        --国家标准书号ISBN
	price NVARCHAR(100) NOT NULL,       --书的价格
	booknum NVARCHAR(100) NOT NULL      --库存数量
);

INSERT INTO book_table VALUES('201900001','数据库系统概论(第五版)','王珊 萨师煊','高等教育出版社','2014-09','978-7-04-040664-1','42','43');
INSERT INTO book_table VALUES('201900002','大话数据结构','程杰','清华大学出版社','2011-06','978-7-302-25565-9','59','36');
INSERT INTO book_table VALUES('201900003','啊哈！算法','啊哈磊','人民邮电出版社','2014-05','978-7-115-35459-4','45','23');
INSERT INTO book_table VALUES('201900004','编程之美','《编程之美》小组','电子工业出版社','2008-05','978-7-121-06074-8','40','58');
INSERT INTO book_table VALUES('201900005','霍乱时期的爱情','加西亚·马尔克斯','南海出版公司','2012-09','978-7-5442-7761-7','49.5','29');
INSERT INTO book_table VALUES('201900006','你是我不及的梦','三毛','北京十月文艺出版社','2014-02','978-7-5302-1347-6','29.5','51');
INSERT INTO book_table VALUES('201900007','围城','钱钟书','人民文学出版社','1980-10','978-7-02-009809-5','28','24');
INSERT INTO book_table VALUES('201900008','他的国','韩寒','天津人民出版社','2012-10','978-7-201-06626-4','32','27');
INSERT INTO book_table VALUES('201900009','稻草人手记','三毛','北京十月文艺出版社','2017-03','978-7-5302-1483-1','32','36');
INSERT INTO book_table VALUES('201900010','低调做人高调做事','宋犀堃','汕头大学出版社','2018-05','978-7-5658-3471-4','36','61');
INSERT INTO book_table VALUES('201900011','算法之美','布莱恩·克里斯汀 汤姆·格里菲思','中信出版集团','2018-05','978-7-5086-8688-2','59','58');
INSERT INTO book_table VALUES('201900012','趣学算法','陈小玉','人民邮电出版社','2017-07','978-7-1154-5957-2','89','42');
INSERT INTO book_table VALUES('201900013','算法笔记','胡凡 曾磊','机械工业出版社','2016-07','978-7-111-54009-0','65','66');

--创建登录信息表,把账号密码都存放在这张表里面
CREATE TABLE login_table
(
	username NVARCHAR(100) PRIMARY KEY, --账号 
	password NVARCHAR(100) NOT NULL,    --密码
	id NVARCHAR(20) NOT NULL,           --身份
	realname NVARCHAR(20) NOT NULL,     --实名
	birthday NVARCHAR(100) NOT NULL,    --生日
	email NVARCHAR(20),                 --邮箱
	photo NVARCHAR(100)                 --头像
);

INSERT INTO login_table	VALUES('Don','Q1DqxVC4VfrQN1qtAV9+pw==','管理员','谭尧丹','2000/1/3','865811637@qq.com','C:\Users\Don\Pictures\Saved Pictures\QQ图片20190205170005.jpg');   --管理员账号
INSERT INTO login_table VALUES('Jay','6ZoYxCjLONXyYIU2eJIuAw==','用户','周杰伦','1979/1/18','318607839@qq.com','C:\Users\Don\Pictures\Saved Pictures\Jay.jpg');     --用户账号
INSERT INTO login_table VALUES('sicong','6ZoYxCjLONXyYIU2eJIuAw==','用户','王思聪','1988/1/3','wangsicong@qq.com','C:\Users\Don\Pictures\Saved Pictures\王思聪.jpg');  --用户账号
INSERT INTO login_table VALUES('Reyi','6ZoYxCjLONXyYIU2eJIuAw==','用户','刘人语','2001/10/10','liurenyu@qq.com','C:\Users\Don\Pictures\Saved Pictures\Reyi.jpg');    --用户账号
INSERT INTO login_table VALUES('boge','6ZoYxCjLONXyYIU2eJIuAw==','用户','王鹏博','1997/10/23','wangpengbo822@qq.com','C:\Users\Don\Pictures\Saved Pictures\王鹏博.jpg');    --用户账号
INSERT INTO login_table VALUES('lyp','6ZoYxCjLONXyYIU2eJIuAw==','用户','刘昱磐','1999/10/17','853643905@qq.com','C:\Users\Don\Pictures\Saved Pictures\刘昱磐.jpg');    --用户账号   
INSERT INTO login_table VALUES('gl','6ZoYxCjLONXyYIU2eJIuAw==','用户','巩蕾','1999/04/02','630810864@qq.com','C:\Users\Don\Pictures\Saved Pictures\巩蕾.jpg');    --用户账号
 
--创建一个表,用来记录借书记录
CREATE TABLE borrow_table 
(
	borrowid NVARCHAR(10) NOT NULL,   --借书单号
	username NVARCHAR(100) NOT NULL, --借书者姓名
	bookid NVARCHAR(100) NOT NULL,   --书籍序列号
	bookname NVARCHAR(100) NOT NULL, --书名
	price NVARCHAR(100) NOT NULL,	 --书的价格
	borrowdate NVARCHAR(100) NOT NULL,  --借书日期
	borrowtime NVARCHAR(100) NOT NULL,  --借阅时长
	returndate NVARCHAR(100)  --还书日期,允许为NULL
);