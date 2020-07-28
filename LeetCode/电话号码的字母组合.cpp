#include <bits/stdc++.h>
using namespace std;

class Solution {
public:
    vector<string> letterCombinations(string digits) {
        vector<string> ans;   //用于存放结果
        map<char,string> m;   //用map来还原手机键盘
        m['2'] = "abc", m['3'] = "def", m['4'] = "ghi";
        m['5'] = "jkl", m['6'] = "mno", m['7'] = "pqrs";
        m['8'] = "tuv", m['9'] = "wxyz";
        int len = digits.length();
        queue<string> q;
        for(int i = 0; i < m[digits[0]].length(); i++) //把第一个字符的手机按键入队
        {
            string _;
            _ += m[digits[0]][i];
            q.push(_);
        }
        string s;
        for(int i = 1; i < len; i++)
        {
            int sz = q.size(); 
            while(sz--)
            {
                for(int j = 0; j < m[digits[i]].length(); j++)
                {
                    s = q.front();
                    s += m[digits[i]][j];
                    q.push(s);
                }
                q.pop();
            }
        }
        while(!q.empty())
        {
            ans.push_back(q.front());
            q.pop();
        }
        return ans;
    }
};

int main()
{
    ios::sync_with_stdio(false);
    cin.tie(0),cout.tie(0);
    string str;
    getline(cin,str);
    Solution s;
    vector<string> v = s.letterCombinations(str) << endl;
    return 0;
}
