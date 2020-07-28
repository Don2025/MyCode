#include <bits/stdc++.h>
using namespace std;

class Solution {
public:
    string longestCommonPrefix(vector<string>& strs) {
        if(strs.size() == 0) return "";  //strs中没有字符串
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
    while(cin >> str && str!="#")   //本地自测时,用#来结束输入
    {
        v.push_back(str);
    }
    Solution s;
    cout << s.longestCommonPrefix(v) << endl;
    return 0;
}
