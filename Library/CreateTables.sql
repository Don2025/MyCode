USE [Library]    --ʹ�����ݿ�Library
DROP TABLE IF EXISTS book_table    --table_table��(��������鼮����Ϣ),�����ھ��Ƚ���ɾ��
DROP TABLE IF EXISTS login_table   --login_table��(������Ź���Ա���û���Ϣ),�����ھ��Ƚ���ɾ��
DROP TABLE IF EXISTS borrow_table  --borrow_table��(������¼�軹�鼮����Ϣ),�����ھ��Ƚ���ɾ��

--�����鼮��
CREATE TABLE book_table
(
	bookid NVARCHAR(100) PRIMARY KEY,   --ͼ�����к�
	name NVARCHAR(100) NOT NULL,        --����
	author NVARCHAR(100) NOT NULL,      --����
	publisher NVARCHAR(100) NOT NULL,   --������
	pubdate NVARCHAR(100) NOT NULL,     --��������
	ISBN NVARCHAR(100) NOT NULL,        --���ұ�׼���ISBN
	price NVARCHAR(100) NOT NULL,       --��ļ۸�
	booknum NVARCHAR(100) NOT NULL      --�������
);

INSERT INTO book_table VALUES('201900001','���ݿ�ϵͳ����(�����)','��ɺ ��ʦ��','�ߵȽ���������','2014-09','978-7-04-040664-1','42','43');
INSERT INTO book_table VALUES('201900002','�����ݽṹ','�̽�','�廪��ѧ������','2011-06','978-7-302-25565-9','59','36');
INSERT INTO book_table VALUES('201900003','�������㷨','������','�����ʵ������','2014-05','978-7-115-35459-4','45','23');
INSERT INTO book_table VALUES('201900004','���֮��','�����֮����С��','���ӹ�ҵ������','2008-05','978-7-121-06074-8','40','58');
INSERT INTO book_table VALUES('201900005','����ʱ�ڵİ���','�����ǡ������˹','�Ϻ����湫˾','2012-09','978-7-5442-7761-7','49.5','29');
INSERT INTO book_table VALUES('201900006','�����Ҳ�������','��ë','����ʮ�����ճ�����','2014-02','978-7-5302-1347-6','29.5','51');
INSERT INTO book_table VALUES('201900007','Χ��','Ǯ����','������ѧ������','1980-10','978-7-02-009809-5','28','24');
INSERT INTO book_table VALUES('201900008','���Ĺ�','����','������������','2012-10','978-7-201-06626-4','32','27');
INSERT INTO book_table VALUES('201900009','�������ּ�','��ë','����ʮ�����ճ�����','2017-03','978-7-5302-1483-1','32','36');
INSERT INTO book_table VALUES('201900010','�͵����˸ߵ�����','��Ϭ��','��ͷ��ѧ������','2018-05','978-7-5658-3471-4','36','61');
INSERT INTO book_table VALUES('201900011','�㷨֮��','������������˹͡ ��ķ�������˼','���ų��漯��','2018-05','978-7-5086-8688-2','59','58');
INSERT INTO book_table VALUES('201900012','Ȥѧ�㷨','��С��','�����ʵ������','2017-07','978-7-1154-5957-2','89','42');
INSERT INTO book_table VALUES('201900013','�㷨�ʼ�','���� ����','��е��ҵ������','2016-07','978-7-111-54009-0','65','66');

--������¼��Ϣ��,���˺����붼��������ű�����
CREATE TABLE login_table
(
	username NVARCHAR(100) PRIMARY KEY, --�˺� 
	password NVARCHAR(100) NOT NULL,    --����
	id NVARCHAR(20) NOT NULL,           --���
	realname NVARCHAR(20) NOT NULL,     --ʵ��
	birthday NVARCHAR(100) NOT NULL,    --����
	email NVARCHAR(20),                 --����
	photo NVARCHAR(100)                 --ͷ��
);

INSERT INTO login_table	VALUES('Don','Q1DqxVC4VfrQN1qtAV9+pw==','����Ա','̷Ң��','2000/1/3','865811637@qq.com','C:\Users\Don\Pictures\Saved Pictures\QQͼƬ20190205170005.jpg');   --����Ա�˺�
INSERT INTO login_table VALUES('Jay','6ZoYxCjLONXyYIU2eJIuAw==','�û�','�ܽ���','1979/1/18','318607839@qq.com','C:\Users\Don\Pictures\Saved Pictures\Jay.jpg');     --�û��˺�
INSERT INTO login_table VALUES('sicong','6ZoYxCjLONXyYIU2eJIuAw==','�û�','��˼��','1988/1/3','wangsicong@qq.com','C:\Users\Don\Pictures\Saved Pictures\��˼��.jpg');  --�û��˺�
INSERT INTO login_table VALUES('Reyi','6ZoYxCjLONXyYIU2eJIuAw==','�û�','������','2001/10/10','liurenyu@qq.com','C:\Users\Don\Pictures\Saved Pictures\Reyi.jpg');    --�û��˺�
INSERT INTO login_table VALUES('boge','6ZoYxCjLONXyYIU2eJIuAw==','�û�','������','1997/10/23','wangpengbo822@qq.com','C:\Users\Don\Pictures\Saved Pictures\������.jpg');    --�û��˺�
INSERT INTO login_table VALUES('lyp','6ZoYxCjLONXyYIU2eJIuAw==','�û�','������','1999/10/17','853643905@qq.com','C:\Users\Don\Pictures\Saved Pictures\������.jpg');    --�û��˺�   
INSERT INTO login_table VALUES('gl','6ZoYxCjLONXyYIU2eJIuAw==','�û�','����','1999/04/02','630810864@qq.com','C:\Users\Don\Pictures\Saved Pictures\����.jpg');    --�û��˺�
 
--����һ����,������¼�����¼
CREATE TABLE borrow_table 
(
	borrowid NVARCHAR(10) NOT NULL,   --���鵥��
	username NVARCHAR(100) NOT NULL, --����������
	bookid NVARCHAR(100) NOT NULL,   --�鼮���к�
	bookname NVARCHAR(100) NOT NULL, --����
	price NVARCHAR(100) NOT NULL,	 --��ļ۸�
	borrowdate NVARCHAR(100) NOT NULL,  --��������
	borrowtime NVARCHAR(100) NOT NULL,  --����ʱ��
	returndate NVARCHAR(100)  --��������,����ΪNULL
);