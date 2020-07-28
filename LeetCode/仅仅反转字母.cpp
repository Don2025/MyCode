#include <bits/stdc++.h>
using namespace std;

class Solution {
public:
    string reverseOnlyLetters(string S) {
        stack<char> s;
        for(int i = 0; i < S.length(); i++)
        {
            if(isalpha(S[i]))
            {
                s.push(S[i]);
            }
        }
        string ans;
        for(int i = 0; i < S.length(); i++)
        {
            if(isalpha(S[i]))
            {
                ans += s.top();
                s.pop();
            }
            else ans += S[i];
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
    cout << s.reverseOnlyLetters(str) << endl;
    return 0;
}
