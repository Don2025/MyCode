#include <bits/stdc++.h>
using namespace std;

class Solution {
public:
    string longestCommonPrefix(vector<string>& strs) {
        if(strs.size() == 0) return "";  //strs��û���ַ���
        auto mins = min_element(strs.begin(),strs.end());
        auto maxs = max_element(strs.begin(),strs.end());
        auto pair = mismatch(mins->begin(),mins->end(),maxs->begin());
        return string(mins->begin(),pair.first);
    }
};

int main()
{
    ios::sync_with_stdio(false);
    cin.tie(0),cout.tie(0);
    string str;
    vector<string> v;
    while(cin >> str && str!="#")   //�����Բ�ʱ,��#����������
    {
        v.push_back(str);
    }
    Solution s;
    cout << s.longestCommonPrefix(v) << endl;
    return 0;
}
