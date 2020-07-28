#include <bits/stdc++.h>
using namespace std;

class Solution {
public:
    int strStr(string haystack, string needle) {
        int _ = haystack.find(needle);
        if(_ == string::npos) return -1;
        return _;
    }
};

int main()
{
    Solution s;
    string a,b;
    cin >> a >> b;
    cout << s.strStr(a,b) << endl;
    return 0;
}
