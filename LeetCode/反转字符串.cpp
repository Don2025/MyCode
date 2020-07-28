#include <bits/stdc++.h>
using namespace std;

class Solution {
public:
    void reverseString(vector<char>& s) {
        int i = 0;
        int j = s.size() - 1;
        while (i < j) {
            swap(s[i], s[j]);
            i++;
            j--;
        }
    }
};

int main()
{
    Solution s;
    vector<char> v;
    while(true)
    {
        char ch;
        cin >> ch;
        if(ch=='.') break;
        v.push_back(ch);
    }
    s.reverseString(v);
    for(int i = 0; i < v.size(); i++)
    {
        cout << v[i] << " ";
    }
    return 0;
}
