#include <bits/stdc++.h>
using namespace std;
#define MAX 20
#define INF 252645135

//顶点结构体
typedef struct
{
    int num;      //景点编号
    string name;  //景点名称
    string info;  //景点简介
}VertexNode;

//邻接矩阵结构体,存路径
typedef struct
{
    int Edge[MAX][MAX];  //边集
    int vn;    //顶点数目
    int en;    //边数目
    VertexNode Vex[MAX]; //顶点集
}AdjMatrix;

//读文件创建图，采用邻接矩阵创建无向网
void Create(AdjMatrix &G);
//查询所有景点信息
void Inquiry_All_Vex();
//输入一个景点的编号
int Input(string text);
//根据编号来查询某一景点信息
void Inquiry_One_Vex();
//用DFS算法来无回路游览整个景区路线
void Inquiry_All_Edge();
//递归的DFS算法
void DFS(int n);
//Dijkstra算法搜索俩个景点间的最短路径
void Dijkstra();
//用Prim算法来铺设电路规划
void Prim();
//新增俩景点间的路径
void Insert_Edge();
//修改俩景点间的路径
void Modify_Edge();
//删除俩景点间的路径
void Delete_Edge();
//保存所有路径信息,覆盖原来的Edge.txt文件
void Save_Edge(AdjMatrix &G);
//新增景点信息
void Insert_Vex();
//修改景点信息
void Modify_Vex();
//删除景点信息
void Delete_Vex();
//保存所有景点信息,覆盖原来的Vex.txt文件
void Save_Vex(AdjMatrix &G);
//主菜单
void Menu();
//修改景区信息菜单
void Modify_Menu();

//全局变量
int weight[MAX][MAX];   //用来暂时存放边的权值
AdjMatrix S;    //一定要全局变量,不然添加和删除操作会无法实现
AdjMatrix *G;  //用一个AdjMatrix 指针指向创建好的邻接矩阵S的地址
bool visited[MAX];   //用来存放深度优先搜索访问过的景点
string Vex_path = "C:\\Users\\Don\\Desktop\\临时文件\\商业情景课设\\Vex.txt";   //Vex.txt文件所在路径
string Edge_path = "C:\\Users\\Don\\Desktop\\临时文件\\商业情景课设\\Edge.txt";    //Edge.txt文件所在路径

int main()
{
    Menu();  //主菜单
    return 0;
}

//读文件创建图，采用邻接矩阵创建无向网
void Create(AdjMatrix &G)
{
    //从Vex.txt这个文件中读取地图的地点信息
    fstream vexfile;     //建立文件流对象
    vexfile.open(Vex_path); //根据Vex.txt的所在目录来打开文件
    vexfile >> G.vn;  //赋值点数
    //初始化地点的信息
    int temp;
    while(!vexfile.eof())   //eof()用来侦测是否达到文件结尾,读到文件尾时返回true
    {
        vexfile >> temp;   //读入地点序号
        G.Vex[temp].num = temp;
        vexfile >> G.Vex[temp].name;   //读入地点名称
        vexfile >> G.Vex[temp].info;   //读入地点简介
    }
    vexfile.close();     //vexfile使用结束了,一定要关闭文件流对象
    //从Edge.txt这个文件中读取地图的边的权值
    int i,j,k;
    memset(weight,0,sizeof(weight));
    fstream edgefile;   //建立文件流对象
    edgefile.open(Edge_path); //根据Edge.txt的所在目录来打开文件
    temp = 0;  //前面文件读取时用到过temp,temp在这里用来数有多少条边
    while(!edgefile.eof())   //eof()用来侦测是否达到文件结尾,读到文件尾时返回true
    {
        edgefile >> i;
        edgefile >> j;
        edgefile >> k;
        weight[i][j] = k;
        temp++;
    }
    edgefile.close();   //edgefile使用结束了,一定要关闭文件流对象
    G.en = temp;   //赋值边数
    //初始化边的信息
    memset(G.Edge,INF,sizeof(G.Edge));
    //建立边的关系
    for(i = 0; i < G.vn; i++)
    {
        for(j = 0; j < G.vn; j++)
        {
            if(weight[i][j]!=0 || weight[j][i]!=0)
            {
                if(weight[i][j] != 0)
                {
                    temp = weight[i][j];
                }
                else
                {
                    temp = weight[j][i];
                }
                G.Edge[i][j] = temp;
                G.Edge[j][i] = temp;
            }
        }
    }
    //Inquiry_All_Vex(G);   //查看所有景点信息,验证Vex.txt是否读取成功
    //查看这个无向图的边信息,验证Edge.txt是否读取成功
    /*
    for(i = 0; i < G.vn; i++)
    {
		for(j = 0; j < G.vn; j++)
        {
            printf("%d ",G.Edge[i][j]);
        }
		cout << endl;
	}
	printf("地点数：%d，边数：%d\n",G.vn,G.en);
    */
}

//查询所有地点信息
void Inquiry_All_Vex()
{
    printf("编号    名称      简介\n");
    for (int i = 0; i < G->vn; i++)
    {
        //cout << G->Vex[i].num << " " << G->Vex[i].name << " " << G->Vex[i].info << endl;
        //还是换成printf算了，把输出弄整齐一点。string型需要用c_str()转换成char*型才能用printf输出
        if(G->Vex[i].num > 0)
        {
            printf("%02d   %10s   %s\n",G->Vex[i].num, G->Vex[i].name.c_str(), G->Vex[i].info.c_str());
        }
    }
    cout << "\n查询完毕,";
    system("pause");   //请按任意键继续
}

//输入一个景点的编号
int Input(string text)
{
    int num;
    string str;
    while(true)
    {
        cout << text;
        //不直接用int型进行输入的原因是害怕用户暴力输入
        getline(cin,str);   //用户输入一行string型字符串
        num = atoi(str.c_str());   //将string型强制转换成int型
        if(num==0 && str!="0")   //当用户输入的非数字时
        {
            cout << "您的输入非法,";
            continue;
        }
        else if(num < 0 || num > G->vn)   //当用户输入的数字不在景点编号范围内时
        {
            cout << "您输入的编号不存在,";
            continue;
        }
        else  //输入合法,返回用户输入的编号
        {
            return num;
        }
    }
}

//根据编号来查询某一景点信息
void Inquiry_One_Vex()
{
    while(true)
    {
        int temp = Input("请输入需要查询的景点编号:");
        int flag = false;   //判断输入的景点编号是否存在
        for(int i = 0; i < G->vn; i++)
        {
            if(G->Vex[i].num == temp)
            {
                flag = true;
                cout << "\n该景点编号:" << G->Vex[i].num << "\n该景点名称:" << G->Vex[i].name << "\n该景点简介:" << G->Vex[i].info << endl;
                cout << "该景点的相邻景点信息如下：" << endl;
                for(int j = 0; j < G->vn; j++)
                {
                    if(i!=j && G->Edge[i][j]!=INF)
                    {
                        cout << "\n相邻景点编号:" << G->Vex[j].num << "\n相邻景点名称:" << G->Vex[j].name << "\n相邻景点简介:" << G->Vex[j].info << endl;
                        printf("从查询的景点%s到该相邻景点%s的路径长度为%d\n",G->Vex[i].name.c_str(),G->Vex[j].name.c_str(),G->Edge[i][j]);  //string型字符串要强制转换成char*型才能用printf输出
                        break;
                    }
                }
            }
        }
        if(flag)   //查询的景点编号存在就跳出while,否则继续输入有效的经i的那编号
        {
            break;
        }
    }
    cout << "\n查询完毕,";
    system("pause");   //请按任意键继续
}

//用DFS算法来无回路游览整个景区路线
void Inquiry_All_Edge()
{
    //确保输入的景点编号是存在的
    while(true)
    {
        bool flag = false;  //判断新增景点编号是否已经存在
        int start = Input("请输入起始景点的景点编号:");
        for(int i = 0; i < G->vn; i++)
        {
            if(G->Vex[i].num == start)
            {
                flag = true;  //新增景点编号重复
            }
        }
        if(flag)  //若存在,跳出while循环
        {
            memset(visited,false,sizeof(visited));  //全部初始化为false
            printf("从%s开始无回路游览整个景区路径为:",G->Vex[start].name.c_str());
            DFS(start);
            break;
        }
        else
        {
            cout << "该景点编号不存在,请重新输入";
        }
    }
    cout << "\n查询完毕,";
    system("pause");   //请按任意键继续
}

//递归的DFS算法
void DFS(int n)
{
    cout << G->Vex[n].name;
    visited[n] = true;
    //查找相邻景点
    int m;   //顶点n的第一个邻接点m
    for(int j = 0; j < G->vn; j++)
    {
        if(G->Edge[n][j]!=INF)
        {
            m = G->Vex[j].num;
            if(!visited[m])
            {
                cout << "->";
                DFS(m);
            }
        }
    }
}

//Dijkstra算法搜索俩个景点间的最短路径
void Dijkstra()
{
    int start = Input("请输入起始景点的编号:");
    int target = Input("请输入目标景点的编号:");
    int i,j,k;
    int D[MAX][MAX];    //D[][]表示最短距离
    int P[MAX][MAX];    //P[][]记录路径
    //初始化
    for(i = 0; i < G->vn; i++)
    {
        for(j = 0; j < G->vn; j++)
        {
            D[i][j] = G->Edge[i][j];
            P[i][j] = j;
        }
    }
    //Dijkstra算法
    for(k = 0; k < G->vn; k++)
    {
        for(i = 0; i < G->vn; i++)
        {
            for(j = 0; j < G->vn; j++)
            {
                if(D[i][j] > D[i][k] + D[k][j])
                {
                    D[i][j] = D[i][k] + D[k][j];
                    P[i][j] = P[i][k];
                }
            }
        }
    }
    //输出
    for(j = 0; j < G->vn; j++)
    {
        if(j == target)
        {
            printf("从%s到%s的最短路径总长度是：%d,",G->Vex[start].name.c_str(),G->Vex[j].name.c_str(),D[start][j]);
            k = P[start][j];
            cout << "路径为：" << G->Vex[start].name;
            while(k != j)
            {
                cout << "->" << G->Vex[k].name;
                k = P[k][j];
            }
            cout << "->" << G->Vex[j].name << endl;
        }
    }
    cout << "查询完毕,";
    system("pause");
}

//铺设电路规划
//使用Prim算法构造最小生成树,使每个景点都能通电的情况下,铺设线路最短
void Prim()
{
    int min_sum = 0;
    int lowcost[MAX],closest[MAX];  //lowcost用来记录当前最短路径,closest用来记录邻接点
    //初始化,假设起点编号为0
    for(int i = 0; i < G->vn; i++)
    {
        closest[i] = 0;      //初始置各个顶点的邻接结点为0
        lowcost[i] = G->Edge[0][i];    //初始为结点0到结点i的路径权值
    }
    lowcost[0] = 0;   //访问过的顶点置为0
    //开始生成其他结点，即接下来找剩下的n-1个节点
    for(int i = 1; i < G->vn; i++)
    {
        int min = INF;     //min初始化无穷大
        int pos;  //pos用来记录当前最小权重的结点编号
        for(int j = 0; j < G->vn; j++)    //遍历所有结点
        {
            if(lowcost[j]!=0 && lowcost[j]<min) //若该结点未被选中,且权值小于之前遍历所得到的最小值
            {
                min = lowcost[j];    //更新min的值
                pos = j;  //更新pos
            }
        }
        min_sum += min;
        //输出符合最小生成树的顶点
        //cout << closest[pos] << "->" << pos << "道路长度为" << lowcase[pos] << endl;
        printf("需要铺设电路的道路是%s和%s之间的那条路,路径长度是%dm。\n",G->Vex[closest[pos]].name.c_str(),G->Vex[pos].name.c_str(),lowcost[pos]);
        lowcost[pos] = 0;   //将pos定点的权值置为0,表示被访问过了
        for(int j = 0; j < G->vn; j++)    //遍历所有结点
        {
            if(lowcost[j]!=0 && G->Edge[pos][j]<lowcost[j])   //若没有被访问过,且
            {
                //更新lowcost数组,closest数组
                //刚选的结点pos与当前结点j有更小的权重,故closest[j]的被连接节点需作修改为pos
                lowcost[j] = G->Edge[pos][j];
                closest[j] = pos;
            }
        }
    }
    printf("铺设电路的总长度是:%dm\n",min_sum);
}

//新增俩景点间的路径
void Insert_Edge()
{
    while(true)
    {
        int start = Input("请输入新增路径的起始景点编号:");
        int target = Input("请输入新增路径的目标景点编号:");
        if(G->Edge[start][target] != INF)   //若该路径已存在
        {
            cout << "这俩个景点的路径已存在,是否修改路径长度？(yes/no)" << endl;
            string str;
            cin >> str;  //避免用户暴力输入
            if(str == "YES" || str == "yes" || str == "Yes")
            {
                cout << "请修改这条路径的长度:";
                int distance;
                while(cin >> distance && distance <= 0)
                {
                    cout << "请重新输入这俩个景点间的路径长度:";
                }
                G->Edge[start][target] = distance;
                G->Edge[target][target] = distance;
                printf("从%s到%s的路径修改成功,",G->Vex[start].name.c_str(),G->Vex[target].name.c_str());
                break;
            }
            else break;   //不考虑No和非法输入了，直接break
        }
        else
        {
            cout << "请输入路径长度:";
            int distance;
            while(cin >> distance && distance <= 0)
            {
                cout << "请重新输入这俩个景点间的路径长度:";
            }
            G->Edge[start][target] = distance;
            G->Edge[target][start] = distance;
            Save_Edge(S);  //保存到Edge.txt中
            printf("从%s到%s的新路径添加成功,已保存至Edge.txt中。\n",G->Vex[start].name.c_str(),G->Vex[target].name.c_str());
            break;
        }
    }
    cout << "即将返回主菜单,";
    system("pause");  //请按任意键继续
    getchar();  //吃回车
    Menu();
}

//修改俩景点间的路径长度
void Modify_Edge()
{
    int start = Input("请输入要修改的路径的起始景点编号:");
    int target = Input("请输入要修改的路径的目标景点编号:");
    cout << "请修改这条路径的长度:";
    int distance;
    while(cin >> distance && distance <= 0)
    {
        cout << "请重新输入这俩个景点间的路径长度:";
    }
    G->Edge[start][target] = distance;
    G->Edge[target][start] = distance;
    Save_Edge(S); //保存至Edge.txt中
    printf("从%s到%s的路径修改成功,已保存至Edge.txt中。\n",G->Vex[start].name.c_str(),G->Vex[target].name.c_str());
    cout << "即将返回主菜单,";
    system("pause");  //请按任意键继续
    Menu();
}

//删除俩景点间的路径
void Delete_Edge()
{
    int start = Input("请输入要删除的路径的起始景点编号:");
    int target = Input("请输入要删除的路径的目标景点编号:");
    //不用考虑删除的路径是否存在,直接把值换成INF
    G->Edge[start][target] = INF;
    G->Edge[target][start] = INF;
    Save_Edge(S);   //保存至Edge.txt中
    printf("从%s到%s的路径删除成功,已保存至Edge.txt中。\n",G->Vex[start].name.c_str(),G->Vex[target].name.c_str());
    cout << "即将返回主菜单,";
    system("pause");  //请按任意键继续
    Menu();
}

//保存所有路径信息,覆盖原来的Edge.txt文件
void Save_Edge(AdjMatrix &G)
{
    fstream edgefile;   //建立文件流
    edgefile.open(Edge_path,ios::out|ios::trunc);
    //根据Edge.txt的所在目录来打开文件,ios::out表示写入文件操作,ios::trunc是当文件不为空时清空文件内容
    for(int i = 0; i < G.vn; i++)
    {
        for(int j = i; j < G.vn; j++)
        {
            if(G.Edge[i][j] != INF)
            {
                edgefile << i << "\t\t\t" << j << "\t\t\t" << G.Edge[i][j] << endl;
            }
        }
    }
    edgefile.close();  //edgefile使用结束了,一定要关闭文件流对象
}

//新增景点信息
void Insert_Vex()
{
    VertexNode v;  //新增景点
    while(true)
    {
        bool flag = false;  //判断新增景点编号是否已经存在
        cout << "请输入新增景点的景点编号:";
        string str;
        //不直接用int型进行输入的原因是害怕用户暴力输入
        getline(cin,str);   //用户输入一行string型字符串
        int n = atoi(str.c_str());   //将string型强制转换成int型
        if(n<=0 && str!="0")   //当用户输入非法
        {
            cout << "您的输入非法,";
            continue;
        }
        else  //输入合法,判断用户输入的编号是否重复
        {
            for(int i = 0; i < G->vn; i++)
            {
                if(G->Vex[i].num == v.num)
                {
                    flag = true;  //新增景点编号重复
                }
            }

        }
        if(!flag)  //若没重复,跳出while循环
        {
            v.num = n;
            break;
        }
        else
        {
            cout << "该景点编号已存在,请重新输入";
        }
    }
    while(true)
    {
        bool flag = false;    //判断新增景点名称是否已经存在
        cout << "请输入新增景点的景点名称:" << endl;
        cin >> v.name;
        for(int i = 0; i < G->vn; i++)
        {
            if(G->Vex[i].name == v.name)
            {
                flag = true;  //新增景点名称重复
            }
        }
        if(!flag)  //若没重复,跳出while循环
        {
            break;
        }
        else
        {
            cout << "该景点名称已存在,请重新输入";
        }
    }
    cout << "请输入新增景点的景点简介:" << endl;
    cin >> v.info;
    //根据景点编号来插入新增景点
    //在末尾添加新增景点信息
    G->vn++;
    G->Vex[G->vn-1].name = v.name;
    G->Vex[G->vn-1].num = v.num;
    G->Vex[G->vn-1].info = v.info;
    //对添加新地点信息后的所有地点进行排序
    for (int i = 0; i < G->vn-1; i++)
    {
        for (int j = i; j < G->vn; j++)
        {
            if(G->Vex[i].num > G->Vex[j].num)
            {
                swap(G->Vex[i],G->Vex[j]);
            }
        }
    }
    cout << "这是一个孤立的景点,";
    while(true)
    {
        cout << "是否新增路径使该景点与其他景点相连通？(yes/no)" << endl;
        string str;
        getchar();
        getline(cin,str);
        if(str == "Yes" || str == "yes" || str == "YES")
        {
            int start = v.num;
            int target = Input("请输入一个与该景点相连通的景点编号:");
            cout << "请输入这俩个景点间的路径长度:";
            int distance;
            while(cin >> distance && distance <= 0)
            {
                cout << "请重新输入这俩个景点间的路径长度:";
            }
            G->Edge[start][target] = distance;
            G->Edge[target][start] = distance;
            cout << "新增路径成功。" << endl;
        }
        else break;  //NO和非法输入都直接跳出while循环
    }
    Save_Vex(S);  //保存至Vex.txt中
    Save_Edge(S); //因为新增了路径,所以也要更新Edge.txt
    cout << "成功新增景点信息,已保存至Vex.txt和Edge.txt中。\n";
    cout << "即将返回主菜单,";
    system("pause");   //请按任意键继续
    Menu();
}

//修改景点信息
void Modify_Vex()
{
    int temp;  //临时的景点编号
    while(true)
    {
        bool flag = false;  //判断新增景点编号是否已经存在
        temp = Input("请输入需要修改的景点的景点编号:");
        for(int i = 0; i < G->vn; i++)
        {
            if(G->Vex[i].num == temp)
            {
                flag = true;  //新增景点编号重复
            }
        }
        if(flag)  //若存在,跳出while循环
        {
            break;
        }
        else
        {
            cout << "该景点编号不存在,请重新输入";
        }
    }
    cout << "是否为该景点重新命名？(yes/no)" << endl;
    string str;
    getline(cin,str);
    if(str == "yes" || str == "YES" || str == "Yes")
    {
        while(true)
        {
            bool flag = false;    //判断新增景点名称是否已经存在
            cout << "请修改该景点的景点名称:" << endl;
            cin >> str;
            getchar();
            for(int i = 0; i < G->vn; i++)
            {
                if(G->Vex[i].name == str)
                {
                    flag = true;  //新增景点名称重复
                }
            }
            if(!flag)  //若没重复,跳出while循环
            {
                G->Vex[temp].name = str;
                break;
            }
            else
            {
                cout << "该景点名称已存在,请重新输入";
            }
        }
    }
    //景点简介就不修改了，全部都是风景优美，气候宜人。
    Save_Vex(S);  //保存至Vex.txt中
    cout << "景点信息修改完成,已保存至Vex.txt中。\n";
    cout << "即将返回主菜单,";
    system("pause");   //请按任意键继续
    Menu();
}

//删除景点信息
void Delete_Vex()
{
    int temp;  //临时的景点编号
    while(true)
    {
        bool flag = false;  //判断新增景点编号是否已经存在
        temp = Input("请输入需要修改的景点的景点编号:");
        for(int i = 0; i < G->vn; i++)
        {
            if(G->Vex[i].num == temp)
            {
                flag = true;  //新增景点编号重复
            }
        }
        if(flag)  //若存在,跳出while循环
        {
            break;
        }
        else
        {
            cout << "该景点编号不存在,请重新输入";
        }
    }
    for(int i = 0; i < G->vn; i++)
    {
        if(G->Vex[i].num == temp)
        {
            //先删除景点的所有相关路径
            for(int j = 0; j < G->vn; j++)
            {
                if(G->Edge[i][j] != INF)
                {
                    //置为无穷大就是删除
                    G->Edge[i][j] = INF;
                    G->Edge[j][i] = INF;
                }
            }
            //再删除景点信息
            G->Vex[i].num = -1;   //因为景点编号是从零开始的,置为-1删除
            G->Vex[i].name = "";
            G->Vex[i].info = "";
            G->vn--;
        }
    }
    Save_Vex(S);  //保存至Vex.txt中
    cout << "景点信息删除完成,已保存至Vex.txt中。\n";
    cout << "即将返回主菜单,";
    system("pause");   //请按任意键继续
    Menu();
}

//保存所有经i的那信息,覆盖原来的Vex.txt文件
void Save_Vex(AdjMatrix &G)
{
    fstream vexfile;   //建立文件流
    vexfile.open(Vex_path,ios::out|ios::trunc);
    //根据Vex.txt的所在目录来打开文件,ios::out表示写入文件操作,ios::trunc是当文件不为空时清空文件内容
    vexfile << G.vn << endl;   //景点数
    for(int i = 0; i < G.vn; i++)
    {
        vexfile << G.Vex[i].num << endl;   //景点编号
        vexfile << G.Vex[i].name << endl;  //景点名称
        vexfile << G.Vex[i].info << endl;  //景点简介
    }
    vexfile.close();  //vexfile使用结束了,一定要关闭文件流对象
}

//主菜单
void Menu()
{
    //读文件创建图
    Create(S);  //创建一个邻接矩阵S
    G = &S;  //用一个AdjMatrix*指针指向创建好的邻接矩阵S的地址
    //主菜单界面
    cout << "\n\t┌───────────────────────────────────────────────────────┐" << endl;
    cout << "\t│\t\t欢迎使用景区信息管理系统\t\t│\n";
    cout << "\t│\t\t\t\t\t\t\t│\n";
    cout << "\t│\t\t  景点信息读取成功！\t\t\t│\n";
    cout << "\t│\t\t\t\t\t\t\t│\n";
    cout << "\t│\t『1』\t-----------------  查询景点\t\t│\n";
    cout << "\t│\t『2』\t-----------------  旅游景点导航\t\t│\n";
    cout << "\t│\t『3』\t-----------------  搜索最短路径\t\t│\n";
    cout << "\t│\t『4』\t-----------------  铺设电路规划\t\t│\n";
    cout << "\t│\t『5』\t-----------------  修改景区信息\t\t│\n";
    cout << "\t│\t『0』\t-----------------  退出系统\t\t│\n";
    cout << "\t│\t\t\t\t\t\t\t│\n";
    cout << "\t└───────────────────────────────────────────────────────┘\n" << endl;
    while(true)
    {
        string str;  //防止暴力输入
        cout << "请输入您的选项:" << endl;
        getline(cin,str);
        int temp = atoi(str.c_str());
        if(str == "0")   //若用户输入了0
        {
            exit(0);   //退出系统
        }
        else if(temp <= 0 || temp > 6)   //若用户非法输入
        {
            cout << "您的输入非法,";
            continue;
        }
        else  //若合法输入
        {
            switch(temp)
            {
                //case 0: exit(0); break;  //退出系统
                case 1: Inquiry_One_Vex();  break; //查询某一景点信息以及它的相邻景点信息
                case 2: Inquiry_All_Edge(); break; //用DFS算法来无回路游览整个景区路线
                case 3: Dijkstra(); break; //用Dijkstra算法来搜索最短路径
                case 4: Prim(); break; //用Prim算法来铺设电路规划
                case 5: Modify_Menu(); break;   //修改景区信息
                default : break;
            }
        }
    }
}

//修改景区信息菜单
void Modify_Menu()
{
    cout << "\n\t┌───────────────────────────────────────────────────────┐" << endl;
    cout << "\t│\t\t  正在修改景区信息......\t\t│\n";
    cout << "\t│\t\t\t\t\t\t\t│\n";
    cout << "\t│\t『1』\t-----------------  新增景点\t\t│\n";
    cout << "\t│\t『2』\t-----------------  新增道路\t\t│\n";
    cout << "\t│\t『3』\t-----------------  修改景点\t\t│\n";
    cout << "\t│\t『4』\t-----------------  修改道路\t\t│\n";
    cout << "\t│\t『5』\t-----------------  删除景点\t\t│\n";
    cout << "\t│\t『6』\t-----------------  删除道路\t\t│\n";
    cout << "\t│\t『0』\t-----------------  回主菜单\t\t│\n";
    cout << "\t│\t\t\t\t\t\t\t│\n";
    cout << "\t└───────────────────────────────────────────────────────┘\n" << endl;
    while(true)
    {
        string str;  //防止暴力输入
        cout << "请输入您的选项:" << endl;
        getline(cin,str);
        int temp = atoi(str.c_str());
        if(str == "0")   //若用户输入了0
        {
            Menu();   //返回主菜单
        }
        else if(temp <= 0 || temp > 6)   //若用户非法输入
        {
            cout << "您的输入非法,";
            continue;
        }
        else  //若合法输入
        {
            switch(temp)
            {
                //case 0: Menu(); break;  //返回主菜单
                case 1: Insert_Vex();  break; //新增景点信息
                case 2: Insert_Edge(); break; //新增道路信息
                case 3: Modify_Vex();  break; //修改景点信息
                case 4: Modify_Edge(); break; //修改道路信息
                case 5: Delete_Vex(); break;  //删除景点信息
                case 6: Delete_Edge(); break; //删除道路信息
                default : break;
            }
        }
    }
}
